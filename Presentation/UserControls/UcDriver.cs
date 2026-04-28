using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Tram chi huy tai xe: Danh sach yeu cau + Xu ly chuyen hien tai.
    /// TableLayoutPanel ngoai + SplitContainer trong.
    /// </summary>
    public partial class UcDriver : BaseUserControl
    {
        private readonly Driver _driver;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;
        private readonly IMatchingService _matchingService;

        private Trip _currentTrip;

        public event EventHandler RequestLogout;
        public event EventHandler<User> RequestShowProfile;

        public UcDriver(
            Driver driver,
            ITripService tripService,
            IUserService userService,
            ISimulationService simulationService,
            IFareService fareService,
            IMatchingService matchingService)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _simulationService = simulationService;
            _fareService = fareService;
            _matchingService = matchingService;

            InitializeComponent();
            SetupEvents();
            UpdateHeaderStatus();
            UpdateTripPanel();
        }

        private void SetupEvents()
        {
            btnToggleStatus.Click += (s, e) => ToggleActive();
            btnProfile.Click += (s, e) => RequestShowProfile?.Invoke(this, _driver);
            btnLogout.Click += (s, e) => RequestLogout?.Invoke(this, e);

            btnArrived.Click += async (s, e) => await OnArrivedClicked();
            btnStartTrip.Click += async (s, e) => await OnStartTripClicked();
            btnCompleteTrip.Click += async (s, e) => await OnCompleteTripClicked();
            btnCancelTrip.Click += async (s, e) => await OnCancelTripClicked();

            dgvRequests.CellDoubleClick += async (s, e) => await OnAcceptRequestClicked();
            btnAcceptRequest.Click += async (s, e) => await OnAcceptRequestClicked();
            btnRejectRequest.Click += (s, e) => OnRejectRequestClicked();

            _tripService.TripStatusChanged += OnTripStatusChanged;
            Disposed += (s, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;
        }

        private void OnTripStatusChanged(object sender, Application.Events.TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    lblTripStatus.Text = "Trang thai: " + e.NewStatus;
                    UpdateActionButtons(e.NewStatus);
                }
            });
        }

        private void UpdateHeaderStatus()
        {
            if (_driver == null) return;

            lblDriverName.Text = _driver.Name;
            lblWallet.Text = "Vi: " + (_driver.Wallet?.Amount.ToString("N0") ?? "0") + "d";
            lblRating.Text = "Sao: " + _driver.AverageRating.ToString("F1") + " *";

            btnToggleStatus.Text = _driver.IsOffline() ? "Bat hoat dong" : "Tat hoat dong";
            btnToggleStatus.BackColor = _driver.IsOffline() ? Color.FromArgb(0, 150, 136) : Color.FromArgb(211, 47, 47);
        }

        public async void ToggleActive()
        {
            if (_driver == null) return;

            string newStatus = _driver.IsOffline() ? "Available" : "Offline";

            IsLoading = true;
            try
            {
                await _userService.UpdateDriverStatusAsync(_driver.Id, newStatus);
                if (newStatus == "Available")
                    _driver.SetAvailable();
                else
                    _driver.SetOffline();
                UpdateHeaderStatus();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai tai xe");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai tai xe");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai tai xe");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void UpdateTripPanel()
        {
            if (_currentTrip == null)
            {
                pnlNoTrip.Visible = true;
                pnlTripActions.Visible = false;
                lblTripStatus.Text = "Dang ranh";
            }
            else
            {
                pnlNoTrip.Visible = false;
                pnlTripActions.Visible = true;
                lblTripStatus.Text = "Trang thai: " + _currentTrip.Status;
                UpdateActionButtons(_currentTrip.Status);
            }
        }

        private void UpdateActionButtons(string status)
        {
            btnArrived.Enabled = status == "Matched";
            btnStartTrip.Enabled = status == "Arrived";
            btnCompleteTrip.Enabled = status == "Started";
            btnCancelTrip.Enabled = status == "Matched" || status == "Arrived" || status == "Started";
        }

        private async System.Threading.Tasks.Task OnAcceptRequestClicked()
        {
            if (dgvRequests.SelectedRows.Count == 0) return;
            // Demo: accept a pending trip
            if (_currentTrip != null)
            {
                ShowWarning("Ban dang co chuyen dang xu ly.");
                return;
            }

            IsLoading = true;
            try
            {
                var trips = await _tripService.GetPendingTripsAsync();
                if (trips.Count > 0)
                {
                    _currentTrip = trips[0];
                    await _tripService.MatchDriverAsync(_currentTrip.Id, _driver.Id);
                    _driver.SetOnTrip();
                    UpdateHeaderStatus();
                    UpdateTripPanel();
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Chap nhan chuyen");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Chap nhan chuyen");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Chap nhan chuyen");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnRejectRequestClicked()
        {
            dgvRequests.ClearSelection();
        }

        private async System.Threading.Tasks.Task OnArrivedClicked()
        {
            if (_currentTrip == null) return;

            IsLoading = true;
            try
            {
                await _tripService.MarkAsArrivedAsync(_currentTrip.Id);
                UpdateTripPanel();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cap nhat trang thai chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnStartTripClicked()
        {
            if (_currentTrip == null) return;

            IsLoading = true;
            try
            {
                await _tripService.StartTripAsync(_currentTrip.Id);
                UpdateTripPanel();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Bat dau chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Bat dau chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Bat dau chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnCompleteTripClicked()
        {
            if (_currentTrip == null) return;

            IsLoading = true;
            try
            {
                await _tripService.CompleteTripAsync(_currentTrip.Id);
                _driver.SetAvailable();
                _driver.AddTrip();
                _currentTrip = null;
                UpdateHeaderStatus();
                UpdateTripPanel();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Hoan thanh chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Hoan thanh chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Hoan thanh chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnCancelTripClicked()
        {
            if (_currentTrip == null) return;
            if (!Confirm("Ban co chac muon huy chuyen?")) return;

            IsLoading = true;
            try
            {
                await _tripService.CancelTripAsync(_currentTrip.Id, "Tai xe huy");
                _driver.SetAvailable();
                _currentTrip = null;
                UpdateHeaderStatus();
                UpdateTripPanel();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Huy chuyen di");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

