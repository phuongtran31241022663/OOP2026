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
    /// <summary>
    /// BaseUserControl hiá»ƒn thá»‹ báº£n Ä‘á»“ vá»›i cÃ¡c marker vÃ  route.
    /// Sá»­ dá»¥ng GMap.NET Ä‘á»ƒ render báº£n Ä‘á»“ OpenStreetMap.
    /// </summary>
    public partial class MapControl : BaseUserControl
    {
        // Logic fields â€“ NOT designer controls
        private GMapOverlay _staticOverlay; // Static markers (pickup, dropoff)
        private GMapOverlay _routeOverlay; // Trip route
        private GMapOverlay _dynamicOverlay; // Dynamic markers (drivers)
        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

        /// <summary>
        /// Sá»± kiá»‡n khi ngÆ°á»i dÃ¹ng click vÃ o báº£n Ä‘á»“
        /// </summary>
        public event Action<MapControl, DomainLocation> MapClicked;

        public MapControl()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            _gMapControl.MapProvider = GMapProviders.OpenStreetMap;
            _gMapControl.Position = new PointLatLng(10.8231, 106.6297); // Ho Chi Minh City
            _gMapControl.MinZoom = 5;
            _gMapControl.MaxZoom = 20;
            _gMapControl.Zoom = 15;
            _gMapControl.CanDragMap = true;
            _gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            _gMapControl.IgnoreMarkerOnMouseWheel = true;

            // Configure map events
            _gMapControl.OnMapZoomChanged += OnMapZoomChanged;
            _gMapControl.OnMarkerClick += OnMarkerClick;
            _gMapControl.MouseClick += OnMapMouseClick;

            // Create overlays according to layering strategy
            _staticOverlay = new GMapOverlay("static"); // Pickup, dropoff markers
            _routeOverlay = new GMapOverlay("route"); // Trip route
            _dynamicOverlay = new GMapOverlay("dynamic"); // Driver markers

            _gMapControl.Overlays.Add(_staticOverlay);
            _gMapControl.Overlays.Add(_routeOverlay);
            _gMapControl.Overlays.Add(_dynamicOverlay);
        }

        /// <summary>
        /// Set vá»‹ trÃ­ pickup trÃªn báº£n Ä‘á»“
        /// </summary>
        public void AddPickupMarker(DomainLocation location)
        {
            SetPickup(location);
        }

        public void SetPickup(DomainLocation location)
        {
            if (location == null) return;

            // Remove existing pickup marker
            if (_pickupMarker != null)
            {
                _staticOverlay.Markers.Remove(_pickupMarker);
            }

            // Create new marker
            _pickupMarker = new GMarkerGoogle(
         new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
         GMarkerGoogleType.green_pushpin);
            _pickupMarker.ToolTipText = $"Äiá»ƒm Ä‘Ã³n: {location.Address}";
            _staticOverlay.Markers.Add(_pickupMarker);
        }

        /// <summary>
        /// Set vá»‹ trÃ­ destination trÃªn báº£n Ä‘á»“
        /// </summary>
        public void AddDestinationMarker(DomainLocation location)
        {
            SetDestination(location);
        }

        public void SetDestination(DomainLocation location)
        {
            if (location == null) return;

            // Remove existing destination marker
            if (_destinationMarker != null)
            {
                _staticOverlay.Markers.Remove(_destinationMarker);
            }

            // Create new marker
            _destinationMarker = new GMarkerGoogle(
                new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.red_pushpin);
            _destinationMarker.ToolTipText = $"Äiá»ƒm Ä‘áº¿n: {location.Address}";
            _staticOverlay.Markers.Add(_destinationMarker);
        }

        /// <summary>
        /// ThÃªm marker tÃ i xáº¿ trÃªn báº£n Ä‘á»“
        /// </summary>
        public void AddDriverMarker(DomainLocation location)
        {
            UpdateDriverLocation(location);
        }

        /// <summary>
        /// Cáº­p nháº­t vá»‹ trÃ­ tÃ i xáº¿ trÃªn báº£n Ä‘á»“
        /// </summary>
        public void UpdateDriverLocation(DomainLocation location)
        {
            if (location == null) return;

            // Remove existing driver marker
            if (_driverMarker != null)
            {
                _dynamicOverlay.Markers.Remove(_driverMarker);
            }

            // Create new marker
            _driverMarker = new GMarkerGoogle(
         new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude),
                GMarkerGoogleType.blue_dot);
            _driverMarker.ToolTipText = $"TÃ i xáº¿: {location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
            _dynamicOverlay.Markers.Add(_driverMarker);

            // Follow driver
            _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
        }

        /// <summary>
        /// Hiá»ƒn thá»‹ danh sÃ¡ch tÃ i xáº¿ trÃªn báº£n Ä‘á»“
        /// </summary>
        public void UpdateDriverMarkers(List<Driver> drivers)
        {
            if (drivers == null) return;

            // Clear existing driver markers
            _dynamicOverlay.Markers.Clear();

            // Add new driver markers
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

        /// <summary>
        /// Váº½ route giá»¯a 2 Ä‘iá»ƒm
        /// </summary>
        public void DrawRoute(DomainLocation from, DomainLocation to, Color color)
        {
            DrawRoute(new List<DomainLocation> { from, to });
        }

        /// <summary>
        /// Váº½ route trÃªn báº£n Ä‘á»“
        /// </summary>
        public void DrawRoute(List<DomainLocation> waypoints)
        {
            if (waypoints == null || waypoints.Count < 2) return;

            // Clear existing routes
            _routeOverlay.Routes.Clear();

            // Create route points
            List<PointLatLng> routePoints = new List<PointLatLng>();
            for (int i = 0; i < waypoints.Count; i++)
            {
                DomainLocation wp = waypoints[i];
                routePoints.Add(new PointLatLng(wp.Coordinate.Latitude, wp.Coordinate.Longitude));
            }

            // Create route
            var route = new GMapRoute(routePoints, "Trip Route")
            {
                Stroke = new Pen(Color.Blue, 3)
            };

            _routeOverlay.Routes.Add(route);
        }

        /// <summary>
        /// Set camera focus
        /// </summary>
        public void SetCamera(DomainLocation location)
        {
            if (location != null)
            {
                _gMapControl.Position = new PointLatLng(location.Coordinate.Latitude, location.Coordinate.Longitude);
            }
        }

        /// <summary>
        /// XÃ³a táº¥t cáº£ markers
        /// </summary>
        public void ClearMarkers()
        {
            _staticOverlay.Markers.Clear();
            _dynamicOverlay.Markers.Clear();
            _pickupMarker = null;
            _destinationMarker = null;
            _driverMarker = null;
        }

        /// <summary>
        /// XÃ³a route
        /// </summary>
        public void ClearRoute()
        {
            _routeOverlay.Routes.Clear();
        }

        private void OnMapZoomChanged()
        {
            // Handle zoom changes if needed
        }

        private void OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // Handle marker clicks if needed
        }

        private void OnMapMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng point = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                // Táº¡o Location táº¡m vá»›i coordinate, address Ä‘á»ƒ trá»‘ng (hoáº·c gá»i reverse geocoding)
                Coordinate coord = new Coordinate(point.Lat, point.Lng);
                Address addr = new Address("", "", "", "", "", "", "", "");
                DomainLocation location = new DomainLocation(coord, addr);
                MapClicked?.Invoke(this, location);
            }
        }
    }
}

