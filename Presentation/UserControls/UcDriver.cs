using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Presentation.Base;
using Presentation.Constants;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.UserControls
{
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

            Label lblIdle = new Label();
            lblIdle.Text = "Đang rảnh, chờ yêu cầu…";
            lblIdle.Dock = DockStyle.Fill;
            lblIdle.TextAlign = ContentAlignment.MiddleCenter;
            lblIdle.Font = UiConstants.Typography.Header;
            lblIdle.ForeColor = UiConstants.Colors.TextMuted;
            pnlNoTrip.Controls.Add(lblIdle);
            pnlNoTrip.Dock = DockStyle.Fill;

            // Fix [High 4]: Avoid docking conflict by using Anchor for dgvRequests
            dgvRequests.Dock = DockStyle.None;
            dgvRequests.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRequests.Location = new Point(0, 75);
            dgvRequests.Size = new Size(pnlRequests.Width, pnlRequests.Height - 110);

            Button btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(8, 35),
                Size = UiConstants.ButtonSizes.Default,
                BackColor = UiConstants.Colors.Info,
                ForeColor = UiConstants.Colors.TextOnKey,
                Font = UiConstants.Typography.Default,
                FlatStyle = FlatStyle.Flat,
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            pnlRequests.Controls.Add(btnRefresh);


            SetupEvents(btnRefresh);
            UpdateHeaderStatus();
            UpdateTripPanel();
            _ = LoadRequestsAsync();
        }

        private void SetupEvents(Button btnRefresh)
        {
            // Fix [Medium 2]: Register event directly
            btnToggleStatus.Click += ToggleActive;
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

            btnRefresh.Click += async (s, e) => await LoadRequestsAsync();
        }

        private async System.Threading.Tasks.Task LoadRequestsAsync()
        {
            if (_driver.IsOffline())
            {
                // Fix [Critical 1]: Use Rows.Clear() for manual rows
                dgvRequests.Rows.Clear();
                return;
            }

            IsLoading = true;
            try
            {
                // Fix [High 3]: Explicit type instead of var
                System.Collections.Generic.List<Trip> pendingTrips = await _tripService.GetPendingTripsAsync();
                dgvRequests.Rows.Clear();
                foreach (Trip trip in pendingTrips)
                {
                    dgvRequests.Rows.Add(
                        trip.Id,
                        FormatLocation(trip.TripRoute.Pickup),
                        FormatLocation(trip.TripRoute.Destination),
                        trip.TripRoute.Distance.ToString("F2") + " km",
                        trip.TripFare.TotalAmount.Amount.ToString("N0") + "đ"
                    );
                }
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Tải danh sách chuyến đi");
            }
            finally
            {
                IsLoading = false;
            }
        }


        private void OnTripStatusChanged(object sender, Application.Events.TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    lblTripStatus.Text = "Trạng thái: " + e.NewStatus;
                    UpdateActionButtons(e.NewStatus);
                }
            });
        }

        private void UpdateHeaderStatus()
        {
            if (_driver == null) return;

            lblDriverName.Text = _driver.Name;
            
            // Modernize labels into pills/cards with icons
            lblWallet.Text = $"💰 {_driver.Wallet?.Amount.ToString("N0") ?? "0"}đ";
            lblWallet.BackColor = Color.FromArgb(232, 245, 233); // Light green
            lblWallet.ForeColor = Color.FromArgb(46, 125, 50);
            lblWallet.Padding = new Padding(8, 4, 8, 4);

            lblRating.Text = $"⭐ {_driver.AverageRating.ToString("F1")}";
            lblRating.BackColor = Color.FromArgb(255, 248, 225); // Light amber
            lblRating.ForeColor = Color.FromArgb(255, 143, 0);
            lblRating.Padding = new Padding(8, 4, 8, 4);

            btnToggleStatus.Text = _driver.IsOffline() ? "Bắt đầu hoạt động" : "Nghỉ ngơi";
            btnToggleStatus.BackColor = _driver.IsOffline() ? Presentation.Constants.UiConstants.Colors.Primary : Presentation.Constants.UiConstants.Colors.Danger;
            
            // Style the grid if it's initialized
            if (dgvRequests != null)
            {
                dgvRequests.BackgroundColor = Color.White;
                dgvRequests.GridColor = Presentation.Constants.UiConstants.Colors.BorderSubtle;
                dgvRequests.RowTemplate.Height = 40;
                dgvRequests.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 241);
                dgvRequests.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        // Fix [Medium 2]: Changed signature to private async void with event params
        private async void ToggleActive(object sender, EventArgs e)
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
                _ = LoadRequestsAsync();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cập nhật trạng thái tài xế");
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
                lblTripStatus.Text = "Đang rảnh";
            }
            else
            {
                pnlNoTrip.Visible = false;
                pnlTripActions.Visible = true;
                lblTripStatus.Text = "Trạng thái: " + _currentTrip.Status;
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

            if (_currentTrip != null)
            {
                ShowWarning("Bạn đang có chuyến đi khác.");
                return;
            }

            IsLoading = true;
            try
            {
                DataGridViewRow selectedRow = dgvRequests.SelectedRows[0];
                string tripIdStr = selectedRow.Cells["colTripId"].Value?.ToString();

                if (string.IsNullOrEmpty(tripIdStr))
                {
                    ShowWarning("Không thể xác định chuyến đi được chọn.");
                    return;
                }

                Guid tripId = Guid.Parse(tripIdStr);

                await _tripService.MatchDriverAsync(tripId, _driver.Id);
                _currentTrip = await _tripService.GetTripAsync(tripId);
                _driver.SetOnTrip();
                UpdateHeaderStatus();
                UpdateTripPanel();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Chấp nhận chuyến đi");
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
                _currentTrip = await _tripService.GetTripAsync(_currentTrip.Id);
                UpdateTripPanel();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cập nhật trạng thái chuyến đi");
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
                _currentTrip = await _tripService.GetTripAsync(_currentTrip.Id);
                UpdateTripPanel();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Bắt đầu chuyến đi");
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
                await _userService.UpdateUserAsync(_driver);
                _currentTrip = null;
                UpdateHeaderStatus();
                UpdateTripPanel();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Hoàn thành chuyến đi");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async System.Threading.Tasks.Task OnCancelTripClicked()
        {
            if (_currentTrip == null) return;
            if (!Confirm("Bạn có chắc muốn huỷ chuyến đi này?")) return;

            IsLoading = true;
            try
            {
                await _tripService.CancelTripAsync(_currentTrip.Id, "Tài xế huỷ");
                _driver.SetAvailable();
                _currentTrip = null;
                UpdateHeaderStatus();
                UpdateTripPanel();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Huỷ chuyến đi");
            }
            finally
            {
                IsLoading = false;
            }
        }
        private static string FormatLocation(Domain.ValueObjects.Location loc)
        {
            if (loc == null) return "N/A";
            return $"{loc.Address?.Street}, {loc.Address?.District}";
        }
    }
}

