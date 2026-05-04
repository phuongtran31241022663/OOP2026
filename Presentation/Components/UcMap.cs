using Domain.Entities.Users;
using Domain.ValueObjects;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Presentation;
using DomainLocation = Domain.ValueObjects.Location;

namespace Presentation.Components
{
    public partial class UcMap : UserControl
    {
        private GMapOverlay _staticOverlay;
        private GMapOverlay _routeOverlay;
        private GMapOverlay _dynamicOverlay;
        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

        public event Action<UcMap, DomainLocation> MapClicked;

        public UcMap()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            _gMapControl.MapProvider = GMapProviders.GoogleMap;
            _gMapControl.Position = new PointLatLng(10.8231, 106.6297);
            _gMapControl.MinZoom = 5;
            _gMapControl.MaxZoom = 20;
            _gMapControl.Zoom = 15;
            _gMapControl.CanDragMap = true;
            _gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            _gMapControl.IgnoreMarkerOnMouseWheel = true;

            _gMapControl.OnMapZoomChanged += OnMapZoomChanged;
            _gMapControl.OnMarkerClick += OnMarkerClick;

            _staticOverlay = new GMapOverlay("static");
            _routeOverlay = new GMapOverlay("route");
            _dynamicOverlay = new GMapOverlay("dynamic");

            _gMapControl.Overlays.Add(_staticOverlay);
            _gMapControl.Overlays.Add(_routeOverlay);
            _gMapControl.Overlays.Add(_dynamicOverlay);
        }

        public void SetPickup(DomainLocation location)
        {
            if (location == null) return;
            if (_pickupMarker != null) _staticOverlay.Markers.Remove(_pickupMarker);

            _pickupMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.green_pushpin);
            _pickupMarker.ToolTipText = "Điểm đón: " + location.Address?.ToString();
            _staticOverlay.Markers.Add(_pickupMarker);
        }

        public void SetDestination(DomainLocation location)
        {
            if (location == null) return;
            if (_destinationMarker != null) _staticOverlay.Markers.Remove(_destinationMarker);

            _destinationMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.red_pushpin);
            _destinationMarker.ToolTipText = "Điểm đến: " + location.Address?.ToString();
            _staticOverlay.Markers.Add(_destinationMarker);
        }

        public void UpdateDriverLocation(DomainLocation location)
        {
            if (location == null) return;
            if (_driverMarker != null) _dynamicOverlay.Markers.Remove(_driverMarker);

            _driverMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.blue_dot);
            _dynamicOverlay.Markers.Add(_driverMarker);

            _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
        }

        public void DrawRoute(string polyline)
        {
            if (string.IsNullOrEmpty(polyline)) return;
            List<PointLatLng> points = DecodePolyline(polyline);
            if (points == null || points.Count < 2) return;

            _routeOverlay.Routes.Clear();
            var route = new GMapRoute(points, "Trip Route")
            {
                Stroke = new Pen(Color.Blue, 3)
            };
            _routeOverlay.Routes.Add(route);
        }

        private static List<PointLatLng> DecodePolyline(string encodedPolyline)
        {
            var polylineChars = encodedPolyline.ToCharArray();
            int index = 0;
            int currentLat = 0;
            int currentLng = 0;
            List<PointLatLng> points = new List<PointLatLng>();

            while (index < polylineChars.Length)
            {
                int sum = 0;
                int shifter = 0;
                int next5Bits;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                } while (next5Bits >= 32 && index < polylineChars.Length);
                int deltaLat = ((sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1));
                currentLat += deltaLat;

                sum = 0;
                shifter = 0;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                } while (next5Bits >= 32 && index < polylineChars.Length);
                int deltaLng = ((sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1));
                currentLng += deltaLng;

                double lat = currentLat / 1E5;
                double lng = currentLng / 1E5;
                points.Add(new PointLatLng(lat, lng));
            }
            return points;
        }

        public void ClearMarkers()
        {
            _staticOverlay.Markers.Clear();
            _dynamicOverlay.Markers.Clear();
            _pickupMarker = null;
            _destinationMarker = null;
            _driverMarker = null;
        }

        public void ClearRoute() => _routeOverlay.Routes.Clear();

        private void OnMapZoomChanged() { }
        private void OnMarkerClick(GMapMarker item, MouseEventArgs e) { }
      
        public void EnableLocationSelection(bool enable)
        {
            _gMapControl.CanDragMap = !enable;
        }
    }
}