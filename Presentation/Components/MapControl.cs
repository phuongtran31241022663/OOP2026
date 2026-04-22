using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using DomainLocation = Domain.ValueObjects.Location;
using Domain.Users.Drivers;

namespace Presentation.Components
{
    /// <summary>
    /// UserControl hiển thị bản đồ với các marker và route.
    /// Sử dụng GMap.NET để render bản đồ OpenStreetMap.
    /// </summary>
    public partial class MapControl : UserControl
    {
        // Logic fields – NOT designer controls
        private GMapOverlay _staticOverlay; // Static markers (pickup, dropoff)
        private GMapOverlay _routeOverlay; // Trip route
        private GMapOverlay _dynamicOverlay; // Dynamic markers (drivers)
        private GMapMarker _pickupMarker;
        private GMapMarker _destinationMarker;
        private GMapMarker _driverMarker;

        /// <summary>
        /// Sự kiện khi người dùng click vào bản đồ
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
        /// Set vị trí pickup trên bản đồ
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
                new PointLatLng(location.Lat, location.Lng),
                GMarkerGoogleType.green_pushpin);
            _pickupMarker.ToolTipText = $"Điểm đón: {location.Address}";
            _staticOverlay.Markers.Add(_pickupMarker);
        }

        /// <summary>
        /// Set vị trí destination trên bản đồ
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
                new PointLatLng(location.Lat, location.Lng),
                GMarkerGoogleType.red_pushpin);
            _destinationMarker.ToolTipText = $"Điểm đến: {location.Address}";
            _staticOverlay.Markers.Add(_destinationMarker);
        }

        /// <summary>
        /// Thêm marker tài xế trên bản đồ
        /// </summary>
        public void AddDriverMarker(DomainLocation location)
        {
            UpdateDriverLocation(location);
        }

        /// <summary>
        /// Cập nhật vị trí tài xế trên bản đồ
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
                new PointLatLng(location.Lat, location.Lng),
                GMarkerGoogleType.blue_dot);
            _driverMarker.ToolTipText = $"Tài xế: {location.Lat:F5}, {location.Lng:F5}";
            _dynamicOverlay.Markers.Add(_driverMarker);

            // Follow driver
            _gMapControl.Position = new PointLatLng(location.Lat, location.Lng);
        }

        /// <summary>
        /// Hiển thị danh sách tài xế trên bản đồ
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
                        new PointLatLng(driver.Position.Lat, driver.Position.Lng),
                        GMarkerGoogleType.blue_dot);
                    marker.ToolTipText = $"{driver.Name} - {driver.Vehicle.GetVehicleType()}";
                    _dynamicOverlay.Markers.Add(marker);
                }
            }
        }

        /// <summary>
        /// Vẽ route giữa 2 điểm
        /// </summary>
        public void DrawRoute(DomainLocation from, DomainLocation to, Color color)
        {
            DrawRoute(new List<DomainLocation> { from, to });
        }

        /// <summary>
        /// Vẽ route trên bản đồ
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
                routePoints.Add(new PointLatLng(wp.Lat, wp.Lng));
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
                _gMapControl.Position = new PointLatLng(location.Lat, location.Lng);
            }
        }

        /// <summary>
        /// Xóa tất cả markers
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
        /// Xóa route
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
                // Convert screen coordinates to lat/lng
                var point = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                var location = new DomainLocation("Custom Location", "", point.Lat, point.Lng);

                // Raise event
                MapClicked?.Invoke(this, location);
            }
        }
    }
}
