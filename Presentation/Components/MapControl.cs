using Domain.Entities.Users;
using Domain.Entities;
using Domain.ValueObjects;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DomainLocation = Domain.ValueObjects.Location;

namespace Presentation.Components
{
    public partial class MapControl : BaseUserControl

    {
        private GMapOverlay _staticOverlay;
        private GMapOverlay _routeOverlay;
        private GMapOverlay _dynamicOverlay;
        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

// Removed: MapSlot and MapClicked event - location selection now handled by LocationPickerControl directly
        
        public event Action<MapControl, DomainLocation> MapClicked;

        public MapControl()

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
            _gMapControl.MouseClick += OnMapMouseClick;

            _staticOverlay = new GMapOverlay("static");
            _routeOverlay = new GMapOverlay("route");
            _dynamicOverlay = new GMapOverlay("dynamic");

            _gMapControl.Overlays.Add(_staticOverlay);
            _gMapControl.Overlays.Add(_routeOverlay);
            _gMapControl.Overlays.Add(_dynamicOverlay);
        }

        public void AddPickupMarker(DomainLocation location) => SetPickup(location);

        public void SetPickup(DomainLocation location)
        {
            if (location == null) return;
            if (_pickupMarker != null) _staticOverlay.Markers.Remove(_pickupMarker);

            _pickupMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.green_pushpin);
            string displayText = location.Address != null && !string.IsNullOrWhiteSpace(location.Address.ToString())
                ? location.Address.ToString()
                : $"{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
            _pickupMarker.ToolTipText = $"�i?m d�n: {displayText}";
            _staticOverlay.Markers.Add(_pickupMarker);

        }

        public void AddDestinationMarker(DomainLocation location) => SetDestination(location);

        public void SetDestination(DomainLocation location)
        {
            if (location == null) return;
            if (_destinationMarker != null) _staticOverlay.Markers.Remove(_destinationMarker);

            _destinationMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.red_pushpin);
            string displayText = location.Address != null && !string.IsNullOrWhiteSpace(location.Address.ToString())
                ? location.Address.ToString()
                : $"{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
            _destinationMarker.ToolTipText = $"�i?m d?n: {displayText}";
            _staticOverlay.Markers.Add(_destinationMarker);

        }

        public void AddDriverMarker(DomainLocation location) => UpdateDriverLocation(location);

        public void UpdateDriverLocation(DomainLocation location)
        {
            if (location == null) return;
            if (_driverMarker != null) _dynamicOverlay.Markers.Remove(_driverMarker);

            _driverMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.blue_dot);
            _driverMarker.ToolTipText = $"T�i x?: {location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
            _dynamicOverlay.Markers.Add(_driverMarker);

            _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
        }

        public void UpdateDriverMarkers(List<Driver> drivers)
        {
            if (drivers == null) return;
            _dynamicOverlay.Markers.Clear();

            for (int i = 0; i < drivers.Count; i++)
            {
                Driver driver = drivers[i];
                if (driver.Position != null)
                {
                    GMarkerGoogle marker = new GMarkerGoogle(
                        new PointLatLng(driver.Position.Coordinate.Latitude, driver.Position.Coordinate.Longitude),
                        GMarkerGoogleType.blue_dot);
                    marker.ToolTipText = $"{driver.Name} - {driver.VehicleId}";
                    _dynamicOverlay.Markers.Add(marker);
                }
            }
        }

        public void DrawRoute(DomainLocation from, DomainLocation to, Color color)
        {
            DrawRoute(new List<DomainLocation> { from, to });
        }

        public void DrawRoute(List<DomainLocation> waypoints)
        {
            if (waypoints == null || waypoints.Count < 2) return;

            _routeOverlay.Routes.Clear();
            List<PointLatLng> routePoints = new List<PointLatLng>();
            for (int i = 0; i < waypoints.Count; i++)
            {
                DomainLocation wp = waypoints[i];
                routePoints.Add(new PointLatLng(wp.Coordinate.Latitude, wp.Coordinate.Longitude));
            }

            var route = new GMapRoute(routePoints, "Trip Route")
            {
                Stroke = new Pen(Color.Blue, 3)
            };
            _routeOverlay.Routes.Add(route);
        }

        /// <summary>
        /// V? route t? chu?i polyline d� m� h�a (OSRM / Google polyline).
        /// </summary>
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

        public void SetCamera(DomainLocation location)
        {
            if (location != null)
            {
                _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
            }
        }

        public void ClearMarkers()
        {
            _staticOverlay.Markers.Clear();
            _dynamicOverlay.Markers.Clear();
            _pickupMarker = null;
            _destinationMarker = null;
            _driverMarker = null;
        }

        public void ClearRoute()
        {
            _routeOverlay.Routes.Clear();
        }

        private void OnMapZoomChanged() { }
        private void OnMarkerClick(GMapMarker item, MouseEventArgs e) { }

        private Address MockReverseGeocode(Coordinate coord)
        {
            double lat = coord.Latitude;
            double lng = coord.Longitude;

            if (lat >= 10.75 && lat <= 10.85 && lng >= 106.65 && lng <= 106.75)
                return new Address("District 1 area", "Mock Street", "Ben Nghe", "Ho Chi Minh", "Vietnam");
            else if (lat >= 10.85 && lat <= 10.95 && lng >= 106.75 && lng <= 106.85)
                return new Address("Thu Duc area", "Mock Ave", "Linh Chieu", "Ho Chi Minh", "Vietnam");
            else if (lat >= 10.0 && lat <= 10.1 && lng >= 105.0 && lng <= 105.1)
                return new Address("Chau Doc rural", "Rural Rd", "Chau Phong", "An Giang", "Vietnam");
            else
                return new Address("Unknown district", "", "Unknown", "Ho Chi Minh", "Vietnam");
        }

private void OnMapMouseClick(object sender, MouseEventArgs e)
        {
            // Map click now handled by LocationPickerControl directly - no need to set markers from map click
            // This event can be used for map navigation/zooming in the future if needed
        }

    }
}
