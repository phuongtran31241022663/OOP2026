using Domain.ValueObjects;
using Domain.Entities.Users;
using Application.Events;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    public partial class TripTrackingForm : BaseForm
    {
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly PassengerShell _parentShell;
        private Trip _currentTrip;

        public TripTrackingForm(ITripService tripService, IUserService userService, PassengerShell parentShell)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _parentShell = parentShell ?? throw new ArgumentNullException(nameof(parentShell));
            InitializeComponent();
            // ÄÄƒng kÃ½ sá»± kiá»‡n Observer
            _tripService.TripStatusChanged += OnTripStatusChanged;

            // Äáº£m báº£o há»§y Ä‘Äƒng kÃ½ khi form Ä‘Ã³ng (Ä‘á»ƒ trÃ¡nh memory leak)
            this.FormClosed += (s, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;
        }
        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            // Chá»‰ xá»­ lÃ½ náº¿u sá»± kiá»‡n liÃªn quan Ä‘áº¿n chuyáº¿n hiá»‡n táº¡i
            if (_currentTrip == null || e.TripId != _currentTrip.Id)
                return;

            // Láº¥y trip má»›i nháº¥t tá»« repository
            Trip updatedTrip = _tripService.GetTripAsync(e.TripId).GetAwaiter().GetResult();
            if (updatedTrip == null) return;

            // Cáº­p nháº­t UI (thread-safe)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ApplyTripUpdate(updatedTrip)));
            }
            else
            {
                ApplyTripUpdate(updatedTrip);
            }
        }
        private void TripTrackingForm_Load(object sender, EventArgs e)
        {
            ShowEmpty();
        }

        // Update from trip
        public void ApplyTripUpdate(Trip trip)
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

            _pickupLabel.Text = $"Äiá»ƒm Ä‘Ã³n: {_currentTrip.Pickup?.Address ?? "--"}";
            _destLabel.Text = $"Äiá»ƒm Ä‘áº¿n: {_currentTrip.Destination?.Address ?? "--"}";
            _fareLabel.Text = $"GiÃ¡: {_currentTrip.Fare:N0} Ä‘";

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
                    text = "Äang tÃ¬m tÃ i xáº¿...";
                    color = Color.LightYellow;
                    break;
                case TripStatus.Matched:
                    text = "TÃ i xáº¿ Ä‘ang Ä‘áº¿n";
                    color = Color.FromArgb(255, 224, 160);
                    break;
                case TripStatus.Started:
                    text = "Äang trÃªn Ä‘Æ°á»ng Ä‘áº¿n Ä‘Ã­ch";
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
            _cancelBtn.Text = canCancel ? "Há»§y chuyáº¿n" : "KhÃ´ng thá»ƒ há»§y";
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
            _driverNameLabel.Text = $"TÃªn: {_currentDriver.Name}";
            _driverPhoneLabel.Text = $"SÄT: {_currentDriver.Phone}";
            _driverReviewLabel.Text = $"ÄÃ¡nh giÃ¡: {_currentDriver.Review.ToString("F1")} â˜…";
            _vehicleLabel.Text = $"Biá»ƒn sá»‘: {_currentDriver.VehiclePlate}";
            _driverCard.Visible = true;
        }

        // Panel switches
        private void ShowEmpty()
        {
            _emptyPanel.Visible = true;
            _activePanel.Visible = false;
            _statusBarLabel.Text = "KhÃ´ng cÃ³ chuyáº¿n Ä‘i Ä‘ang hoáº¡t Ä‘á»™ng";
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
                "XÃ¡c nháº­n há»§y chuyáº¿n Ä‘i?",
                "Há»§y chuyáº¿n",
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
                    $"KhÃ´ng thá»ƒ há»§y chuyáº¿n: {ex.Message}",
                    "Lá»—i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void OnTripStarted(Trip trip) => ApplyTripUpdate(_tripService.GetTrip(trip.Id));

        public void RefreshData()
        {
            if (_currentTrip != null)
            {
                Render();
            }
        }
    }
}

