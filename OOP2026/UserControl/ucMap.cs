using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace OOP2026
{
    public partial class ucMap : UserControl
    {
        private GMapOverlay _pickupOverlay;
        private GMapOverlay _dropoffOverlay;
        private GMapOverlay _driverOverlay;
        private GMapOverlay _routeOverlay;
        private GMapOverlay _poiOverlay;

        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

        private IMapSvc _mapService;
        private bool _isSelectingLocation = false;
        private bool _isDragging = false;

        // Events for UI coordination
        public event Action<ucMap, Loc> MapClicked;
        public event Action<PointLatLng> LocationSelected;
        public event EventHandler MapDragStarted;
        public event EventHandler MapDragEnded;

        public ucMap()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            try
            {
                GMaps.Instance.Mode = AccessMode.ServerAndCache;

                // Configure UserAgent for the project
                GMapProvider.UserAgent = "RideGoApp/1.0 (Windows; Educational Project; OOP2026)";

                // Set default map provider to GoogleMap
                _gMapControl.MapProvider = GMapProviders.GoogleMap;
                _gMapControl.Position = new PointLatLng(10.7626, 106.6601); // Default to HCM City

                _gMapControl.MinZoom = 5;
                _gMapControl.MaxZoom = 18;
                _gMapControl.Zoom = 15;
                _gMapControl.CanDragMap = true;
                _gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
                _gMapControl.IgnoreMarkerOnMouseWheel = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Map initialization error: {ex.Message}");
                _gMapControl.MapProvider = GMapProviders.GoogleMap;
                _gMapControl.Position = new PointLatLng(10.7626, 106.6601);
                _gMapControl.Zoom = 15;
            }

            // Register mouse interaction events
            _gMapControl.OnMarkerClick += OnMarkerClick;
            _gMapControl.OnMapDrag += OnMapDrag;
            _gMapControl.MouseDown += OnMapMouseDown;
            _gMapControl.MouseUp += OnMapMouseUp;
            _gMapControl.MouseClick += OnMapMouseClick;

            // Initialize overlays
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

        public void SetMapService(IMapSvc mapService)
        {
            _mapService = mapService;
        }

        /// <summary>
        /// Enable/Disable location selection mode via map click
        /// </summary>
        public void SetSelectionMode(bool enable)
        {
            _isSelectingLocation = enable;
        }

        public void SetPickup(Loc location)
        {
            if (_pickupMarker != null) _pickupOverlay.Markers.Remove(_pickupMarker);
            _pickupMarker = null;
            if (location == null) return;

            _pickupMarker = new GMarkerGoogle(
                new PointLatLng(location.Coord.Latitude, location.Coord.Longitude),
                GMarkerGoogleType.green_pushpin);

            _pickupMarker.ToolTipText = "Pickup: " + (location.Addr?.Name ?? location.Addr?.Street ?? "Pickup");
            _pickupOverlay.Markers.Add(_pickupMarker);
        }

        public void SetDestination(Loc location)
        {
            if (_destinationMarker != null) _dropoffOverlay.Markers.Remove(_destinationMarker);
            _destinationMarker = null;
            if (location == null) return;

            _destinationMarker = new GMarkerGoogle(
                new PointLatLng(location.Coord.Latitude, location.Coord.Longitude),
                GMarkerGoogleType.red_pushpin);

            _destinationMarker.ToolTipText = "Destination: " + (location.Addr?.Name ?? location.Addr?.Street ?? "Destination");
            _dropoffOverlay.Markers.Add(_destinationMarker);
        }

        public void UpdateDriverLocation(Loc location)
        {
            if (location == null) return;
            if (_driverMarker != null) _driverOverlay.Markers.Remove(_driverMarker);

            _driverMarker = new GMarkerGoogle(
                new PointLatLng(location.Coord.Latitude, location.Coord.Longitude),
                GMarkerGoogleType.blue_dot);
            _driverOverlay.Markers.Add(_driverMarker);

            // Center map on moving driver
            _gMapControl.Position = new PointLatLng(location.Coord.Latitude, location.Coord.Longitude);
        }

        public void UpdateDriverLocation(Guid driverId, PointLatLng point, bool isDemo, bool isCar)
        {
            UpdateDriverLocation(new Loc(new Coord(point.Lat, point.Lng), new Addr("Current Location", "", "", "", "")));
        }

        public void DrawRoute(Route route)
        {
            if (route == null || string.IsNullOrEmpty(route.Polyline)) return;
            DrawRoute(route.Polyline);
        }

        public void DrawRoute(string polyline)
        {
            if (string.IsNullOrEmpty(polyline)) return;
            List<PointLatLng> points = DecodePolyline(polyline);
            if (points == null || points.Count < 2) return;

            _routeOverlay.Routes.Clear();
            GMapRoute route = new GMapRoute(points, "Trip Route")
            {
                Stroke = new Pen(Color.FromArgb(180, Color.RoyalBlue), 5)
            };
            _routeOverlay.Routes.Add(route);
        }

        /// <summary>
        /// Decodes Google Maps encoded polyline string to GPS coordinates
        /// </summary>
        private static List<PointLatLng> DecodePolyline(string encodedPolyline)
        {
            char[] polylineChars = encodedPolyline.ToCharArray();
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

        private void OnMapDrag()
        {
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

        private async void OnMapMouseClick(object sender, MouseEventArgs e)
        {
            // Handle map click in selection mode to get address from coordinates
            if (_isSelectingLocation && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
            {
                PointLatLng point = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                
                Loc location;
                if (_mapService != null)
                {
                    location = await _mapService.GetAddressAsync(point.Lat, point.Lng);
                }
                else
                {
                    location = null;
                }

                if (location == null)
                {
                    location = new Loc(new Coord(point.Lat, point.Lng), new Addr("Selected Location", "", "", "", ""));
                }

                MapClicked?.Invoke(this, location);
                LocationSelected?.Invoke(point);
            }
        }
    }
}
