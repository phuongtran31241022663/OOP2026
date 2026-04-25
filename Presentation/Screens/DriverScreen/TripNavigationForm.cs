using Application.Events;
using Application.Interfaces;
using Domain.Enums;
using Presentation.Shells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Entities;

namespace Presentation.Screens.DriverScreen
{
    public partial class TripNavigationForm : BaseForm
    {
        // Dependencies
        private readonly DriverShell _shell;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;

        // State
        private Trip _pendingTrip;
        private bool _isLoading;
        private HashSet<Guid> _notifiedIds;

        

        public TripNavigationForm(DriverShell shell, ITripService tripService,
            IUserService userService, ISimulationService simulationService)
        {
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));

            _notifiedIds = new HashSet<Guid>();

            InitializeComponent();
            RefreshAsync();
            _tripService.TripStatusChanged += OnTripStatusChanged;
            this.FormClosed += (s, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;
        }

        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            // Cần kiểm tra xem trip mới này có dành cho driver hiện tại không
            // Có thể lấy trip từ service và kiểm tra DriverId (nếu đã matched)
            // Hoặc dựa vào logic nghiệp vụ: nếu driver chưa có trip active thì tìm trip Searching
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshAsync()));
            }
            else
            {
                RefreshAsync();
            }
        }

        private async void RefreshAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            AddLog("Đang đồng bộ...");

            try
            {
                // Step 1: Sync user profile
                var Driver = await _userService.GetDriverById(_shell.Driver.Id);
                if (Driver != null)
                {
                    // Update shell driver if needed
                    // _shell.Driver = Driver; // Uncomment if shell needs updating
                }

                // Step 2: Check offline
                if (_shell.Driver.Status == DriverStatus.Offline)
                {
                    ShowEmpty("Tài xế offline");
                    UpdateStats();
                    return;
                }

                // Step 3: Recovery state error
                if (_shell.Driver.Status == DriverStatus.OnTrip && _shell.CurrentTrip == null)
                {
                    await _userService.ForceRecoverDriverStatus(_shell.Driver.Id);
                    AddLog("Khôi phục trạng thái tài xế");
                }

                // Step 4: Sync current trip
                // TODO: Get active trips for driver
                // var activeTrips = await _tripService.GetActiveTripsForDriverAsync(_shell.Driver.Id);
                // if (activeTrips.Any())
                // {
                //     _shell.SetCurrentTrip(activeTrips.First());
                // }

                // Step 5: Find new trips
                // TODO: var newTrips = await _tripService.GetActiveTripsForDriverAsync(_shell.Driver.Id);
                var newTrips = new List<Trip>(); // Placeholder

                if (newTrips.Any())
                {
                    var newTrip = newTrips.First();
                    if (!_notifiedIds.Contains(newTrip.Id))
                    {
                        _pendingTrip = newTrip;
                        _notifiedIds.Add(newTrip.Id);
                        ShowRequestCard(newTrip);
                        PlayNotificationSound();
                        AddLog("Có chuyến mới!");
                    }
                }
                else if (_shell.CurrentTrip == null)
                {
                    ShowEmpty("Không có chuyến nào");
                }
                else
                {
                    ShowActiveTrip();
                }

                UpdateStats();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi đồng bộ: {ex.Message}");
                ShowEmpty("Lỗi kết nối");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void ShowEmpty(string message)
        {
            _emptyMessageLabel.Text = message;
            _emptyPanel.Visible = true;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = false;
        }

        private void ShowRequestCard(Trip trip)
        {
            _requestInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} → {trip.Destination?.Address ?? "Unknown"}\n" +
                                   $"Giá: {trip.Fare:N0} đ\n" +
                                   $"Thời gian: {trip.CreatedAt:dd/MM HH:mm}";

            _emptyPanel.Visible = false;
            _requestPanel.Visible = true;
            _activeTripPanel.Visible = false;
        }

        private void ShowActiveTrip()
        {
            if (_shell.CurrentTrip == null) return;

            var trip = _shell.CurrentTrip;
            _routeInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} → {trip.Destination?.Address ?? "Unknown"}\n" +
                                 $"Giá: {trip.Fare:N0} đ";

            UpdateStepBar(trip.Status);

            _emptyPanel.Visible = false;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = true;
        }

        private void UpdateStepBar(TripStatus status)
        {
            // Map status to step progress
            int currentStep = status switch
            {
                TripStatus.Matched => 1,
                TripStatus.Started => 2,
                TripStatus.Completed => 3,
                _ => 0
            };

            for (int i = 0; i < 4; i++)
            {
                string state = i < currentStep ? "done" :
                              i == currentStep ? "active" : "todo";
                SetStepDot(i, state);
            }

            // Update action button
            UpdateActionButton(status);
        }

        private void SetStepDot(int index, string state)
        {
            Color color = state switch
            {
                "done" => Color.Green,
                "active" => Color.Blue,
                "todo" => Color.Gray,
                _ => Color.Gray
            };

            // Create colored circle bitmap
            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(new SolidBrush(color), 0, 0, 20, 20);
            }
            _stepDots[index].Image = bmp;
        }

        private void UpdateActionButton(TripStatus status)
        {
            switch (status)
            {
                case TripStatus.Matched:
                    _actionButton.Text = "Đã đến điểm đón";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Arrived:
                    _actionButton.Text = "Bắt đầu chuyến";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Started:
                    _actionButton.Text = "Hoàn thành chuyến";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Completed:
                    _actionButton.Text = "Hoàn thành";
                    _actionButton.Enabled = false;
                    break;
                default:
                    _actionButton.Text = "Chờ...";
                    _actionButton.Enabled = false;
                    break;
            }
        }

        private async void OnAcceptClicked(object sender, EventArgs e)
        {
            if (_pendingTrip == null || _shell.Driver.Status != DriverStatus.Available) return;

            try
            {
                // TODO: await _tripService.TryAssignDriverAsync(_pendingTrip.Id, _shell.Driver.Id);
                // var updatedTrip = await _tripService.GetTripAsync(_pendingTrip.Id);
                // _shell.SetCurrentTrip(updatedTrip);

                // For now, simulate
                _shell.SetCurrentTrip(_pendingTrip);

                _shell.OnTripAccepted(_pendingTrip);
                _pendingTrip = null;

                ShowActiveTrip();
                AddLog("Chấp nhận chuyến thành công");
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi chấp nhận: {ex.Message}");
                MessageBox.Show($"Không thể chấp nhận chuyến: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OnRejectClicked(object sender, EventArgs e)
        {
            if (_pendingTrip == null) return;

            try
            {
                // TODO: await _tripService.RejectTripAsync(_pendingTrip.Id, _shell.Driver.Id);
                _pendingTrip = null;
                AddLog("Từ chối chuyến");
                await Task.Delay(500); // Small delay
                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi từ chối: {ex.Message}");
            }
        }

        private async void OnActionClicked(object sender, EventArgs e)
        {
            if (_shell.CurrentTrip == null) return;

            try
            {
                var trip = _shell.CurrentTrip;
                switch (trip.Status)
                {
                    case TripStatus.Arrived:
                        // TODO: await _tripService.MarkArrivedAsync(trip.Id);
                        AddLog("Đã đến điểm đón");
                        break;
                    case TripStatus.Matched:
                        // TODO: await _tripService.StartTripAsync(trip.Id);
                        AddLog("Bắt đầu chuyến");
                        break;
                    case TripStatus.Started:
                        // TODO: await _tripService.CompleteTripAsync(trip.Id);
                        _shell.OnTripEnded();
                        AddLog("Hoàn thành chuyến");
                        break;
                }

                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lỗi hành động: {ex.Message}");
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            RefreshAsync();
        }

        private void UpdateStats()
        {
            var driver = _shell.Driver;
            if (driver == null) return;

            _ReviewLabel.Text = $"Review: {driver.Review.ToString("F1")}";
            _totalTripsLabel.Text = $"Trips: {0}"; // TODO: Calculate from trip history
            _incomeLabel.Text = $"Income: {0:N0} đ"; // TODO: Calculate total earnings
            _walletLabel.Text = $"Wallet: {driver.WalletAmount:N0} đ";
            _revenueTodayLabel.Text = $"Today: {0:N0} đ"; // TODO: Calculate today's revenue
        }

        private void AddLog(string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm}] {message}";
            _logListBox.Items.Add(logEntry);

            // Limit to 200 entries
            while (_logListBox.Items.Count > 200)
            {
                _logListBox.Items.RemoveAt(0);
            }

            // Auto scroll to bottom
            _logListBox.SelectedIndex = _logListBox.Items.Count - 1;
            _logListBox.ClearSelected();
        }

        private void PlayNotificationSound()
        {
            try
            {
                SystemSounds.Beep.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        // Public methods for external control
        public void OnTripAccepted(Trip trip)
        {
            ShowActiveTrip();
        }

        public void OnTripEnded()
        {
            ShowEmpty("Chuyến đã kết thúc");
        }
    }
 
}
