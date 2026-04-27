using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Vong doi dat xe: Dat -> Theo doi -> Thanh toan.
    /// SplitContainer: Ban do trai, bang dieu khien dong phai.
    /// </summary>
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

        private Trip _currentTrip;
        private Location _pickup;
        private Location _destination;
        private VehicleType _selectedVehicle = VehicleType.Motorbike;

        public event EventHandler RequestLogout;
        public event EventHandler<User> RequestShowProfile;

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
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapService = mapService;
            _fareService = fareService;
            _simulationService = simulationService;
            _matchingService = matchingService;
            _reviewService = reviewService;

            InitializeComponent();
            SetupEvents();
            ShowStage(TripStage.Idle);
        }

        private void SetupEvents()
        {
            btnHistory.Click += (s, e) => ShowStage(TripStage.History);
            btnProfile.Click += (s, e) => RequestShowProfile?.Invoke(this, _passenger);
            btnLogout.Click += (s, e) => RequestLogout?.Invoke(this, e);

            btnBook.Click += async (s, e) => await OnBookClicked();
            btnCancelSearch.Click += async (s, e) => await OnCancelSearchClicked();
            btnCancelTrip.Click += async (s, e) => await OnCancelTripClicked();
            btnConfirmPayment.Click += async (s, e) => await OnConfirmPaymentClicked();
            btnRateDriver.Click += (s, e) => OnRateDriverClicked();

            cmbVehicleType.SelectedIndexChanged += (s, e) =>
            {
                _selectedVehicle = cmbVehicleType.SelectedIndex == 1 ? VehicleType.Car : VehicleType.Motorbike;
                UpdateFareEstimate();
            };

            _tripService.TripStatusChanged += OnTripStatusChanged;
        }

        private void OnTripStatusChanged(object sender, Application.Events.TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    lblStatus.Text = "Trang thai: " + e.NewStatus;
                    UpdateStageFromStatus(e.NewStatus);
                }
            });
        }

        private void UpdateStageFromStatus(string status)
        {
            switch (status)
            {
                case "Searching": ShowStage(TripStage.Searching); break;
                case "Matched": ShowStage(TripStage.Tracking); break;
                case "Arrived": ShowStage(TripStage.Tracking); break;
                case "Started": ShowStage(TripStage.Tracking); break;
                case "Completed": ShowStage(TripStage.Payment); break;
                case "Cancelled": ShowStage(TripStage.Idle); break;
            }
        }

        private void ShowStage(TripStage stage)
        {
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            pnlHistory.Visible = false;

            switch (stage)
            {
                case TripStage.Idle:
                    pnlBooking.Visible = true;
                    break;
                case TripStage.Searching:
                    pnlSearching.Visible = true;
                    break;
                case TripStage.Tracking:
                    pnlTracking.Visible = true;
                    break;
                case TripStage.Payment:
                    pnlPayment.Visible = true;
                    break;
                case TripStage.History:
                    pnlHistory.Visible = true;
                    LoadHistory();
                    break;
            }
        }

        private void UpdateFareEstimate()
        {
            if (_pickup != null && _destination != null && _fareService != null)
            {
                // Async estimate could be added here
            }
        }

        private async System.Threading.Tasks.Task OnBookClicked()
        {
            if (_pickup == null || _destination == null)
            {
                ShowWarning("Vui long chon diem don va diem den.");
                return;
            }

            ShowStage(TripStage.Searching);

            try
            {
                Route route = null;
                if (_mapService != null)
                {
                    route = await _mapService.GetRouteAsync(_pickup, _destination);
                }

                if (route == null)
                {
                    route = new Route(_pickup, _destination, 5.0, TimeSpan.FromMinutes(15), "");
                }

                Fare fare = null;
                if (_fareService != null)
                {
                    fare = await _fareService.CalculateFareAsync(_selectedVehicle, route.Distance);
                }
                else
                {
                    var total = _selectedVehicle == VehicleType.Car ? 25000m + (decimal)(route.Distance * 12000) : 12000m + (decimal)(route.Distance * 4000);
                    fare = new Fare(new Money(total, "VND"), new Money(total * 0.2m, "VND"), new Money(total * 0.8m, "VND"));
                }

                _currentTrip = await _tripService.CreateTripAsync(_passenger.Id, route, fare, _selectedVehicle);
                _currentTrip.SetSearching();
                ShowStage(TripStage.Searching);

                // Simulate matching
                if (_matchingService != null)
                {
                    bool matched = await _matchingService.MatchDriverToTripAsync(_currentTrip.Id, Guid.Empty);
                    if (!matched)
                    {
                        ShowToast("Khong tim thay tai xe phu hop. Vui long thu lai.");
                        ShowStage(TripStage.Idle);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Dat xe that bai: " + ex.Message);
                ShowStage(TripStage.Idle);
            }
        }

        private async System.Threading.Tasks.Task OnCancelSearchClicked()
        {
            if (_currentTrip != null)
            {
                await _tripService.CancelTripAsync(_currentTrip.Id, "Hanh khach huy tim kiem");
                _currentTrip = null;
            }
            ShowStage(TripStage.Idle);
        }

        private async System.Threading.Tasks.Task OnCancelTripClicked()
        {
            if (_currentTrip == null) return;

            bool canCancel = await _tripService.CanTripBeCancelledAsync(_currentTrip.Id);
            if (!canCancel)
            {
                ShowWarning("Khong the huy chuyen o trang thai nay.");
                return;
            }

            if (Confirm("Ban co chac muon huy chuyen?"))
            {
                await _tripService.CancelTripAsync(_currentTrip.Id, "Hanh khach huy");
                _currentTrip = null;
                ShowStage(TripStage.Idle);
            }
        }

        private async System.Threading.Tasks.Task OnConfirmPaymentClicked()
        {
            if (_currentTrip == null) return;
            await _tripService.ConfirmPaymentAsync(_currentTrip.Id);
            ShowInfo("Thanh toan thanh cong!");
            ShowStage(TripStage.Idle);
        }

        private void OnRateDriverClicked()
        {
            if (_currentTrip == null) return;
            var ucRating = new UcRating(_reviewService, _currentTrip);
            FrmModal.ShowModal(this, ucRating, "Danh gia tai xe");
        }

        private async void LoadHistory()
        {
            IsLoading = true;
            try
            {
                var trips = await _tripService.GetTripsByPassengerAsync(_passenger.Id);
                dgvHistory.Rows.Clear();
                foreach (var trip in trips)
                {
                    dgvHistory.Rows.Add(
                        trip.Id.ToString().Substring(0, 8),
                        trip.Status,
                        trip.TripFare?.TotalAmount.Amount.ToString("N0") + "d",
                        trip.RequestAt.ToString("dd/MM HH:mm"));
                }
            }
            catch (Exception ex)
            {
                ShowError("Tai lich su that bai: " + ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private enum TripStage { Idle, Searching, Tracking, Payment, History }
    }
}

