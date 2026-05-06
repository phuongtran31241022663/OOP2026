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
using Application.Interfaces;
using DomainLocation = Domain.ValueObjects.Location;

namespace Presentation.Components
{
    public partial class UcMap : UserControl
    {
        private GMapOverlay _pickupOverlay;
        private GMapOverlay _dropoffOverlay;
        private GMapOverlay _driverOverlay;
        private GMapOverlay _routeOverlay;
        private GMapOverlay _poiOverlay;

        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

        private IMapService _mapService;
        private bool _isSelectingLocation = false;
        private bool _isDragging = false;

        public event Action<UcMap, DomainLocation> MapClicked;
        public event EventHandler MapDragStarted;
        public event EventHandler MapDragEnded;

        public UcMap()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            _gMapControl.MapProvider = GMapProviders.OpenStreetMap;
            _gMapControl.Position = new PointLatLng(10.7626, 106.6601); // HCM City
            _gMapControl.MinZoom = 5;
            _gMapControl.MaxZoom = 20;
            _gMapControl.Zoom = 15;
            _gMapControl.CanDragMap = true;
            _gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            _gMapControl.IgnoreMarkerOnMouseWheel = true;

            _gMapControl.OnMapZoomChanged += OnMapZoomChanged;
            _gMapControl.OnMarkerClick += OnMarkerClick;
            _gMapControl.OnMapDrag += OnMapDrag;
            
            _gMapControl.MouseDown += OnMapMouseDown;
            _gMapControl.MouseUp += OnMapMouseUp;
            _gMapControl.MouseClick += OnMapMouseClick;

            _pickupOverlay = new GMapOverlay("pickup");
            _dropoffOverlay = new GMapOverlay("dropoff");
            _driverOverlay = new GMapOverlay("driver");
            _routeOverlay = new GMapOverlay("route");
            _poiOverlay = new GMapOverlay("poi");

            _gMapControl.Overlays.Add(_poiOverlay);
            _gMapControl.Overlays.Add(_routeOverlay);
            _gMapControl.Overlays.Add(_driverOverlay);
            _gMapControl.Overlays.Add(_dropoffOverlay);
            _gMapControl.Overlays.Add(_pickupOverlay);
        }

        public void SetMapService(IMapService mapService)
        {
            _mapService = mapService;
            LoadPOIs();
        }

        public void SetPickup(DomainLocation location)
        {
            if (location == null) return;
            if (_pickupMarker != null) _pickupOverlay.Markers.Remove(_pickupMarker);

            _pickupMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.green_pushpin);
            _pickupMarker.ToolTipText = "Điểm đón: " + (location.Address?.Name ?? location.Address?.Street ?? "Pickup");
            _pickupOverlay.Markers.Add(_pickupMarker);
        }

        public void SetDestination(DomainLocation location)
        {
            if (location == null) return;
            if (_destinationMarker != null) _dropoffOverlay.Markers.Remove(_destinationMarker);

            _destinationMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.red_pushpin);
            _destinationMarker.ToolTipText = "Điểm đến: " + (location.Address?.Name ?? location.Address?.Street ?? "Destination");
            _dropoffOverlay.Markers.Add(_destinationMarker);
        }

        public void UpdateDriverLocation(DomainLocation location)
        {
            if (location == null) return;
            if (_driverMarker != null) _driverOverlay.Markers.Remove(_driverMarker);

            _driverMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.blue_dot);
            _driverOverlay.Markers.Add(_driverMarker);

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
            _pickupOverlay.Markers.Clear();
            _dropoffOverlay.Markers.Clear();
            _driverOverlay.Markers.Clear();
            _pickupMarker = null;
            _destinationMarker = null;
            _driverMarker = null;
        }

        public void ClearRoute() => _routeOverlay.Routes.Clear();

        private void OnMapZoomChanged()
        {
            LoadPOIs();
        }

        private void OnMapDrag()
        {
            LoadPOIs();
            if (!_isDragging)
            {
                _isDragging = true;
                MapDragStarted?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnMarkerClick(GMapMarker item, MouseEventArgs e) { }

        private void OnMapMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        private void OnMapMouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                MapDragEnded?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnMapMouseClick(object sender, MouseEventArgs e)
        {
            if (_isSelectingLocation && e.Button == MouseButtons.Left)
            {
                var point = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                var location = new DomainLocation(new Coordinate(point.Lat, point.Lng), null);
                MapClicked?.Invoke(this, location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Quick set destination on right click
                if (_isSelectingLocation)
                {
                    var point = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                    var location = new DomainLocation(new Coordinate(point.Lat, point.Lng), null);
                    MapClicked?.Invoke(this, location);
                }
            }
        }

        private async void LoadPOIs()
        {
            if (_mapService == null || _gMapControl.Zoom < 14)
            {
                _poiOverlay.Markers.Clear();
                return;
            }

            try
            {
                RectLatLng viewArea = _gMapControl.ViewArea;
                List<DomainLocation> pois = await _mapService.GetPOIsAsync(viewArea.Bottom, viewArea.Left, viewArea.Top, viewArea.Right);

                _poiOverlay.Markers.Clear();
                foreach (var poi in pois)
                {
                GMarkerGoogle marker = new GMarkerGoogle(
                        new PointLatLng(poi.Coordinate.Latitude, poi.Coordinate.Longitude),
                        GMarkerGoogleType.yellow_small);
                    marker.ToolTipText = poi.Address?.Name;
                    _poiOverlay.Markers.Add(marker);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading POIs: " + ex.Message);
            }
        }

        public async void ZoomToMyLocation()
        {
            if (_mapService == null) return;
            var location = await _mapService.GetIpLocationAsync();
            if (location != null)
            {
                _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
                _gMapControl.Zoom = 15;
            }
        }

        public void EnableLocationSelection(bool enable)
        {
            _isSelectingLocation = enable;
            // We don't disable dragging anymore, just use click logic
        }
    }
}