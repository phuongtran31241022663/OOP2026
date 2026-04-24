using Application.Interfaces;
using Application.DTOs;
using Domain.Enums;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation;

namespace Presentation.Screens.Passenger
{
    public partial class TripTrackingForm : BaseForm
    {
        // Dependencies
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IDriverSimulationService _driverSimulationService;
        private readonly PassengerShell _parentShell;

        // State
        private TripDto _currentTrip;
        private DriverDto _currentDriver;

        public TripTrackingForm(ITripService tripService, IUserService userService, IDriverSimulationService driverSimulationService, PassengerShell parentShell)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _driverSimulationService = driverSimulationService ?? throw new ArgumentNullException(nameof(driverSimulationService));
            _parentShell = parentShell ?? throw new ArgumentNullException(nameof(parentShell));

            InitializeComponent();
        }

        private void TripTrackingForm_Load(object sender, EventArgs e)
        {
            ShowEmpty();
        }

        // Update from trip
        public void ApplyTripUpdate(TripDto trip)
        {
            _currentTrip = trip;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Render));
                return;
            }

            Render();
        }

        private void Render()
        {
            if (_currentTrip == null)
            {
                ShowEmpty();
                return;
            }

            if (_currentTrip.Status == TripStatus.Completed || _currentTrip.Status == TripStatus.Cancelled)
            {
                _parentShell.OnTripFinished();
                ShowEmpty();
                return;
            }

            ShowActive();
            UpdateBanner();

            _pickupLabel.Text = $"Điểm đón: {_currentTrip.Pickup?.Address ?? "--"}";
            _destLabel.Text = $"Điểm đến: {_currentTrip.Destination?.Address ?? "--"}";
            _fareLabel.Text = $"Giá: {_currentTrip.Fare:N0} đ";

            UpdateCancelButton();

            if (_currentTrip.DriverId.HasValue &&
                _currentTrip.Status != TripStatus.Requested &&
                _currentTrip.Status != TripStatus.Searching)
            {
                LoadDriverAsync(_currentTrip.DriverId.Value);
            }
            else
            {
                _driverCard.Visible = false;
            }
        }

        private void UpdateBanner()
        {
            string text;
            Color color;

            switch (_currentTrip.Status)
            {
                case TripStatus.Requested:
                case TripStatus.Searching:
                    text = "Đang tìm tài xế...";
                    color = Color.LightYellow;
                    break;
                case TripStatus.Matched:
                    text = "Tài xế đang đến";
                    color = Color.FromArgb(255, 224, 160);
                    break;
                case TripStatus.Started:
                    text = "Đang trên đường đến đích";
                    color = Color.FromArgb(200, 255, 200);
                    break;
                default:
                    text = "--";
                    color = SystemColors.ControlLight;
                    break;
            }

            _statusBannerLabel.Text = text;
            _statusBanner.BackColor = color;
            _statusBarLabel.Text = text;
        }

        private void UpdateCancelButton()
        {
            bool canCancel = _tripService.CanTripBeCancelled(_currentTrip.Id);

            _cancelBtn.Enabled = canCancel;
            _cancelBtn.Text = canCancel ? "Hủy chuyến" : "Không thể hủy";
            _cancelBtn.ForeColor = canCancel ? Color.DarkRed : SystemColors.GrayText;
        }

        private async void LoadDriverAsync(Guid driverId)
        {
            try
            {
                _currentDriver = await _userService.GetDriverById(driverId);
                if (_currentDriver == null)
                {
                    return;
                }

                if (InvokeRequired)
                {
                    BeginInvoke(new Action(ShowDriverCard));
                    return;
                }

                ShowDriverCard();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load driver error: {ex.Message}");
            }
        }

        private void ShowDriverCard()
        {
            _driverNameLabel.Text = $"Tên: {_currentDriver.Name}";
            _driverPhoneLabel.Text = $"SĐT: {_currentDriver.Phone}";
            _driverReviewLabel.Text = $"Đánh giá: {_currentDriver.Review.ToString("F1")} ★";
            _vehicleLabel.Text = $"Biển số: {_currentDriver.VehiclePlate}";
            _driverCard.Visible = true;
        }

        // Panel switches
        private void ShowEmpty()
        {
            _emptyPanel.Visible = true;
            _activePanel.Visible = false;
            _statusBarLabel.Text = "Không có chuyến đi đang hoạt động";
        }

        private void ShowActive()
        {
            _emptyPanel.Visible = false;
            _activePanel.Visible = true;
        }

        private void OnGoHomeClicked(object sender, EventArgs e)
        {
            _parentShell.NavigateTo("Home");
        }

        // Event handlers
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            if (_currentTrip == null)
            {
                return;
            }

            var confirm = MessageBox.Show(
                "Xác nhận hủy chuyến đi?",
                "Hủy chuyến",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await Task.Run(() => _tripService.CancelTrip(_currentTrip.Id, "Cancelled by passenger"));
                _parentShell.OnTripFinished();
                ShowEmpty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể hủy chuyến: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void OnTripStarted(Trip trip) => ApplyTripUpdate(_tripService.GetTripDto(trip.Id));

        public void RefreshData()
        {
            if (_currentTrip != null)
            {
                Render();
            }
        }
    }
}
