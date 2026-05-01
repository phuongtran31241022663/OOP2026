using Application.Interfaces;
using Application.Events;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Constants;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    public partial class UcPassenger : BaseUserControl
    {
        private readonly Passenger _passenger;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;
        private readonly ISimulationService _simulationService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;

        public event EventHandler RequestLogout;
        public event EventHandler<User> RequestShowProfile;

        private Trip _currentTrip;
        private bool _isSidebarExpanded = true;

        private void ShowStage(string status)
        {
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            pnlHistory.Visible = false;

            if (status == null || status == "Idle" || status == "Timeout" || status == "Cancelled")
            {
                pnlBooking.Visible = true;
                lblStatus.Text = "Sẵn sàng đặt xe";
                _currentTrip = null;
            }
            else if (status == "Searching")
            {
                pnlSearching.Visible = true;
                lblStatus.Text = "Đang tìm tài xế…";
            }
            else if (status == "Matched" || status == "Arrived" || status == "Started")
            {
                pnlTracking.Visible = true;
                btnCancelTrip.Enabled = (status != "Started");
                lblStatus.Text = "Trạng thái: " + status;
            }
            else if (status == "Completed")
            {
                pnlPayment.Visible = true;
                if (_currentTrip != null && _currentTrip.TripFare != null)
                    lblTotalAmount.Text = _currentTrip.TripFare.TotalAmount.Amount.ToString("N0") + "đ";
                lblStatus.Text = "Chuyến đi đã hoàn thành";
            }
        }

        public UcPassenger(
            Passenger passenger,
            ITripService tripService,
            IUserService userService,
            IMapService mapService,
            IFareService fareService,
            ISimulationService simulationService,
            IMatchingService matchingService,
            IReviewService reviewService)
        {
            _passenger = passenger;
            _tripService = tripService;
            _userService = userService;
            _mapService = mapService;
            _fareService = fareService;
            _simulationService = simulationService;
            _matchingService = matchingService;
            _reviewService = reviewService;
            InitializeComponent();
            
            // Populate location pickers
            pickupPicker.SetMapService(_mapService);
            destinationPicker.SetMapService(_mapService);
            pickupPicker.PopulateDropdown(_passenger.Id.ToString());
            destinationPicker.PopulateDropdown(_passenger.Id.ToString());

            lblPassengerName.Text = "Xin chào, " + _passenger.Name;

            ShowStage(null);

pickupPicker.Click += PickupPicker_Click;
            destinationPicker.Click += DestinationPicker_Click;

            btnBook.Click += btnBook_Click;
            btnHistory.Click += btnHistory_Click;
            btnLogout.Click += btnLogout_Click;
            btnProfile.Click += btnProfile_Click;
            btnMenu.Click += btnMenu_Click;

            _tripService.TripStatusChanged += OnTripStatusChanged;
            Disposed += (s, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;
        }

        private void PickupPicker_Click(object sender, EventArgs e)
        {
            // Location selection handled by LocationPickerControl directly
            // Show hint to user
            lblStatus.Text = "Chọn điểm đón từ danh sách";
        }

        private void DestinationPicker_Click(object sender, EventArgs e)
        {
            // Location selection handled by LocationPickerControl directly
            // Show hint to user
            lblStatus.Text = "Chọn điểm đến từ danh sách";
        }

        private void btnLogout_Click(object sender, EventArgs e) => RequestLogout?.Invoke(this, EventArgs.Empty);
        private void btnProfile_Click(object sender, EventArgs e) => RequestShowProfile?.Invoke(this, _passenger);
        private void btnHistory_Click(object sender, EventArgs e) { /* Show history */ }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            _isSidebarExpanded = !_isSidebarExpanded;
            if (_isSidebarExpanded)
            {
                pnlSidebar.Width = 200;
                btnMenu.Text = "☰  Menu";
                btnHistory.Text = "🕒  Lịch sử";
                btnProfile.Text = "👤  Hồ sơ";
                btnLogout.Text = "⏻  Thoát";
            }
            else
            {
                pnlSidebar.Width = 50;
                btnMenu.Text = "☰";
                btnHistory.Text = "🕒";
                btnProfile.Text = "👤";
                btnLogout.Text = "⏻";
            }
        }
        private async void btnBook_Click(object sender, EventArgs e)
        {
            if (pickupPicker.SelectedLocation == null)
            {
                ShowWarning("Vui lòng chọn điểm đón.");
                return;
            }
            if (destinationPicker.SelectedLocation == null)
            {
                ShowWarning("Vui lòng chọn điểm đến.");
                return;
            }
            if (cmbVehicleType.SelectedIndex < 0)
            {
                ShowWarning("Vui lòng chọn loại xe.");
                return;
            }

            VehicleType vehicleType = cmbVehicleType.SelectedIndex == 0
                ? VehicleType.Motorbike
                : VehicleType.Car;

            IsLoading = true;
            try
            {
                Trip trip = await _tripService.RequestTripAsync(
                    _passenger.Id,
                    pickupPicker.SelectedLocation,
                    destinationPicker.SelectedLocation,
                    vehicleType);

                _currentTrip = trip;
                ShowStage("Searching");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Đặt xe");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    ShowStage(e.NewStatus);
                }
            });
        }
    }
}

