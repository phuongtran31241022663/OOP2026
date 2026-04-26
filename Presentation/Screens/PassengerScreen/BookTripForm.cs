using Presentation.Shells;
using System.Windows.Forms;
using Application.Interfaces;
using Domain.Enums;
using System;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain.ValueObjects;
using Domain.Entities;
using Domain.Entities.Users;

namespace Presentation.Screens.PassengerScreen
{
    public partial class BookTripForm : BaseForm
    {
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;
        private readonly HttpClient _httpClient;
        private readonly PassengerShell _parentShell;

        private VehicleType _selectedVehicle = VehicleType.Motorbike;
        private bool _isRequesting;
        private bool _isPickupSlotActive;
        private Location _Pickup;
        private Location _destination;
        private List<Location> _history = new List<Location>();
        private List<Driver> _nearbyDrivers = new List<Driver>();
        private Trip _currentTrip;
        private decimal _Fare;
        private double _Distance;
        private TimeSpan _Duration;

        private System.Timers.Timer _refreshTimer;
        private int _isRefreshingNearbyDrivers;

        public BookTripForm(
            ITripService tripService,
            IUserService userService,
            IMapService mapService,
            IFareService fareService,
            HttpClient httpClient,
            PassengerShell parentShell)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _parentShell = parentShell ?? throw new ArgumentNullException(nameof(parentShell));

            InitializeComponent();
        }

        private void BookTripForm_Load(object sender, EventArgs e)
        {
            InitializeTimer();
            LoadLocationHistory();
            ResetToIdle();
        }

        private void InitializeTimer()
        {
            _refreshTimer = new System.Timers.Timer(4000) { AutoReset = true, Enabled = true };
            _refreshTimer.Elapsed += OnRefreshTimerElapsed;
        }

        private void LoadLocationHistory()
        {
            _history = new List<Location>
            {
                new Location("UEH Campus", "", 10.8751, 106.8003),
                new Location("Ben Thanh Market", "", 10.7718, 106.6983),
                new Location("Tân Sơn Nhất Airport", "", 10.8231, 106.6297),
                new Location("Bến Bạch Đằng", "", 10.7626, 106.6602),
                new Location("Nhà thờ Đức Bà", "", 10.7769, 106.7009)
            };
        }

        // State changes
        private void ResetToIdle()
        {
            _isRequesting = false;
            _currentTrip = null;

            _requestBtn.Enabled = true;
            _cancelBtn.Visible = false;

            SetStatus("Sẵn sàng đặt xe", SystemColors.ControlDark);

            _pickupPicker.Enabled = true;
            _destinationPicker.Enabled = true;
            _vehicleCombo.Enabled = true;

            _driverCard.Visible = false;

            _fareLabel.Text = "Giá: --";
            _distanceLabel.Text = "Khoảng cách: --";
            _durationLabel.Text = "Thời gian: --";

            _mapControl.ClearMarkers();
            _mapControl.ClearRoute();

            _refreshTimer.Enabled = true;
        }

        private void LockForRequesting()
        {
            _isRequesting = true;

            _requestBtn.Enabled = false;
            _cancelBtn.Visible = true;

            _pickupPicker.Enabled = false;
            _destinationPicker.Enabled = false;
            _vehicleCombo.Enabled = false;

            _refreshTimer.Enabled = false;
            SetStatus("Đang tìm tài xế...", Color.Khaki);
        }

        private void SetStatus(string text, Color backColor)
        {
            _statusLabel.Text = text;
            _statusBar.BackColor = backColor;
        }

        // Event handlers
        private void OnMapClicked(object sender, Location location)
        {
            ApplyLocation(location, _isPickupSlotActive);
        }

        private void OnPickupSelected(object sender, Location location) => ApplyLocation(location, true);
        private void OnDestinationLocationSelected(object sender, Location location) => ApplyLocation(location, false);

        private void ApplyLocation(Location location, bool isPickup)
        {
            if (isPickup)
            {
                _Pickup = location;
                // _pickupPicker.SetLocation(location.Address);
                _isPickupSlotActive = false;
            }
            else
            {
                _destination = location;
                // _destinationPicker.SetLocation(location.Address);
            }

            if (!_history.Any(l => l.Lat == location.Lat && l.Lng == location.Lng))
            {
                _history.Insert(0, location);
                if (_history.Count > 10)
                {
                    _history.RemoveAt(_history.Count - 1);
                }
            }

            _mapControl.SetPickup(_Pickup);
            _mapControl.SetDestination(_destination);

            if (_Pickup != null && _destination != null)
            {
                UpdateEstimation();
            }
        }

        private async void UpdateEstimation()
        {
            // try
            // {
            //     var routes = await _routeService.GetRoutesAsync(_Pickup, _destination);
            //     if (!routes.Any())
            //     {
            //         return;
            //     }

            //     var route = routes.First();
            //     _Distance = route.Distance;
            //     _Duration = route.Time;
            //     _Fare = await _fareService.CalculateFare(_selectedVehicle, _Distance);

            //     _fareLabel.Text = $"Giá: {_Fare:N0} đ";
            //     _distanceLabel.Text = $"Khoảng cách: {_Distance:F1} km";
            //     _durationLabel.Text = $"Thời gian: {_Duration.TotalMinutes:F0} phút";

            //     _mapControl.SetRoute(route.Waypoints);
            //     _requestBtn.Enabled = true;
            // }
            // catch (Exception ex)
            // {
            //     SetStatus("Lỗi tính giá", SystemColors.ControlDark);
            //     Console.WriteLine($"Fare estimation error: {ex.Message}");
            // }
        }

        private void OnVehicleChanged(object sender, EventArgs e)
        {
            _selectedVehicle = _vehicleCombo.SelectedItem?.ToString() == "Car (4 chỗ)"
                ? VehicleType.Car
                : VehicleType.Motorbike;

            if (_Pickup != null && _destination != null)
            {
                UpdateEstimation();
            }
        }

        private void OnRequestClicked(object sender, EventArgs e)
        {
            if (_Pickup == null || _destination == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn điểm đón và điểm đến.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            LockForRequesting();
            RequestTripAsync();
        }

        private async void RequestTripAsync()
        {
            try
            {
                _currentTrip = _tripService.RequestTrip(
                    _parentShell.Passenger.Id,
                    _Pickup,
                    _destination,
                    _selectedVehicle);

                _parentShell.OnTripStarted(_currentTrip);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể đặt xe: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ResetToIdle();
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            if (_currentTrip != null)
            {
                try
                {
                    _parentShell.OnTripFinished();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cancel error: {ex.Message}");
                }
            }

            ResetToIdle();
        }

        private async void OnRefreshTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_isRequesting && _Pickup != null)
            {
                if (Interlocked.Exchange(ref _isRefreshingNearbyDrivers, 1) == 1)
                {
                    return;
                }

                try
                {
                    await RefreshNearbyDriversAsync();
                }
                finally
                {
                    Interlocked.Exchange(ref _isRefreshingNearbyDrivers, 0);
                }
            }
        }

        private async System.Threading.Tasks.Task RefreshNearbyDriversAsync()
        {
            try
            {
                // TODO: _nearbyDrivers = await _userService.GetNearbyDriversAsync(_Pickup, 5.0);
                var count = _nearbyDrivers.Count;

                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(() =>
                    {
                        _nearbyLabel.Text = count > 0
                            ? $"{count} tài xế trong vòng 5 km"
                            : "Chưa có tài xế gần đây";
                        _nearbyLabel.ForeColor = count > 0 ? SystemColors.ControlText : SystemColors.GrayText;
                        // TODO: Update MapControl to accept DTOs
                        // _mapControl.UpdateDriverMarkers(_nearbyDrivers);
                    }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Refresh drivers error: {ex.Message}");
            }
        }

        // Suggestions
        private void ShowSuggestions(bool isPickup)
        {
            _suggestions.Items.Clear();

            _suggestions.Items.Add(new SuggestionItem { IsHeader = true, Header = "Địa điểm gần đây" });
            foreach (var loc in _history.Take(3))
            {
                _suggestions.Items.Add(new SuggestionItem { Location = loc, DisplayText = loc.Address });
            }

            _suggestions.Items.Add(new SuggestionItem { IsHeader = true, Header = "Địa điểm phổ biến" });
            foreach (var loc in _history.Skip(3))
            {
                _suggestions.Items.Add(new SuggestionItem { Location = loc, DisplayText = loc.Address });
            }

            _suggestions.Tag = isPickup;
            _suggestions.Visible = true;
        }

        private void HideSuggestions() => _suggestions.Visible = false;

        private void OnDrawSuggestionItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            var item = (SuggestionItem)_suggestions.Items[e.Index];
            e.DrawBackground();

            if (item.IsHeader)
            {
                using (var headerBrush = new SolidBrush(SystemColors.ControlDark))
                {
                    e.Graphics.FillRectangle(headerBrush, e.Bounds);
                }
                TextRenderer.DrawText(
                    e.Graphics,
                    item.Header,
                    new Font("Segoe UI", 8, FontStyle.Bold),
                    new Point(e.Bounds.X + 4, e.Bounds.Y + 5),
                    SystemColors.ControlText);
            }
            else
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    "  " + item.DisplayText,
                    new Font("Segoe UI", 8),
                    new Point(e.Bounds.X + 16, e.Bounds.Y + 5),
                    (e.State & DrawItemState.Selected) == DrawItemState.Selected
                        ? SystemColors.HighlightText
                        : SystemColors.ControlText);
            }

            e.DrawFocusRectangle();
        }

        private void OnSuggestionSelected(object sender, EventArgs e)
        {
            var item = _suggestions.SelectedItem as SuggestionItem;
            if (item != null && !item.IsHeader)
            {
                ApplyLocation(item.Location, (bool)_suggestions.Tag);
                HideSuggestions();
            }
        }

        // Public API
        public void OnTripFinished() => BeginInvoke(new Action(ResetToIdle));

        public void UpdateDriverInfo(Driver driver)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateDriverInfo(driver)));
                return;
            }

            _driverNameLabel.Text = $"Tài xế: {driver.Name}";
            _driverPhoneLabel.Text = $"SĐT: {driver.Phone}";
            _driverCard.Visible = true;
            SetStatus("Tài xế đang đến...", Color.LightYellow);
        }

        // Inner types
        private class SuggestionItem
        {
            public bool IsHeader { get; set; }
            public string Header { get; set; }
            public Location Location { get; set; }
            public string DisplayText { get; set; }

            public override string ToString() => IsHeader ? Header : DisplayText;
        }
    }
}
