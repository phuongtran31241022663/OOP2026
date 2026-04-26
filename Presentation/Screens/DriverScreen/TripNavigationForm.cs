using Domain.ValueObjects;
using Domain.Entities.Users;
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
            // Cáº§n kiá»ƒm tra xem trip má»›i nÃ y cÃ³ dÃ nh cho driver hiá»‡n táº¡i khÃ´ng
            // CÃ³ thá»ƒ láº¥y trip tá»« service vÃ  kiá»ƒm tra DriverId (náº¿u Ä‘Ã£ matched)
            // Hoáº·c dá»±a vÃ o logic nghiá»‡p vá»¥: náº¿u driver chÆ°a cÃ³ trip active thÃ¬ tÃ¬m trip Searching
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
            AddLog("Äang Ä‘á»“ng bá»™...");

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
                    ShowEmpty("TÃ i xáº¿ offline");
                    UpdateStats();
                    return;
                }

                // Step 3: Recovery state error
                if (_shell.Driver.Status == DriverStatus.OnTrip && _shell.CurrentTrip == null)
                {
                    await _userService.ForceRecoverDriverStatus(_shell.Driver.Id);
                    AddLog("KhÃ´i phá»¥c tráº¡ng thÃ¡i tÃ i xáº¿");
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
                        AddLog("CÃ³ chuyáº¿n má»›i!");
                    }
                }
                else if (_shell.CurrentTrip == null)
                {
                    ShowEmpty("KhÃ´ng cÃ³ chuyáº¿n nÃ o");
                }
                else
                {
                    ShowActiveTrip();
                }

                UpdateStats();
            }
            catch (Exception ex)
            {
                AddLog($"Lá»—i Ä‘á»“ng bá»™: {ex.Message}");
                ShowEmpty("Lá»—i káº¿t ná»‘i");
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
            _requestInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} â†’ {trip.Destination?.Address ?? "Unknown"}\n" +
                                   $"GiÃ¡: {trip.Fare:N0} Ä‘\n" +
                                   $"Thá»i gian: {trip.CreatedAt:dd/MM HH:mm}";

            _emptyPanel.Visible = false;
            _requestPanel.Visible = true;
            _activeTripPanel.Visible = false;
        }

        private void ShowActiveTrip()
        {
            if (_shell.CurrentTrip == null) return;

            var trip = _shell.CurrentTrip;
            _routeInfoLabel.Text = $"{trip.Pickup?.Address ?? "Unknown"} â†’ {trip.Destination?.Address ?? "Unknown"}\n" +
                                 $"GiÃ¡: {trip.Fare:N0} Ä‘";

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
                    _actionButton.Text = "ÄÃ£ Ä‘áº¿n Ä‘iá»ƒm Ä‘Ã³n";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Arrived:
                    _actionButton.Text = "Báº¯t Ä‘áº§u chuyáº¿n";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Started:
                    _actionButton.Text = "HoÃ n thÃ nh chuyáº¿n";
                    _actionButton.Enabled = true;
                    break;
                case TripStatus.Completed:
                    _actionButton.Text = "HoÃ n thÃ nh";
                    _actionButton.Enabled = false;
                    break;
                default:
                    _actionButton.Text = "Chá»...";
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
                AddLog("Cháº¥p nháº­n chuyáº¿n thÃ nh cÃ´ng");
            }
            catch (Exception ex)
            {
                AddLog($"Lá»—i cháº¥p nháº­n: {ex.Message}");
                MessageBox.Show($"KhÃ´ng thá»ƒ cháº¥p nháº­n chuyáº¿n: {ex.Message}", "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void OnRejectClicked(object sender, EventArgs e)
        {
            if (_pendingTrip == null) return;

            try
            {
                // TODO: await _tripService.RejectTripAsync(_pendingTrip.Id, _shell.Driver.Id);
                _pendingTrip = null;
                AddLog("Tá»« chá»‘i chuyáº¿n");
                await Task.Delay(500); // Small delay
                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lá»—i tá»« chá»‘i: {ex.Message}");
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
                        AddLog("ÄÃ£ Ä‘áº¿n Ä‘iá»ƒm Ä‘Ã³n");
                        break;
                    case TripStatus.Matched:
                        // TODO: await _tripService.StartTripAsync(trip.Id);
                        AddLog("Báº¯t Ä‘áº§u chuyáº¿n");
                        break;
                    case TripStatus.Started:
                        // TODO: await _tripService.CompleteTripAsync(trip.Id);
                        _shell.OnTripEnded();
                        AddLog("HoÃ n thÃ nh chuyáº¿n");
                        break;
                }

                RefreshAsync();
            }
            catch (Exception ex)
            {
                AddLog($"Lá»—i hÃ nh Ä‘á»™ng: {ex.Message}");
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
            _incomeLabel.Text = $"Income: {0:N0} Ä‘"; // TODO: Calculate total earnings
            _walletLabel.Text = $"Wallet: {driver.WalletAmount:N0} Ä‘";
            _revenueTodayLabel.Text = $"Today: {0:N0} Ä‘"; // TODO: Calculate today's revenue
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
            ShowEmpty("Chuyáº¿n Ä‘Ã£ káº¿t thÃºc");
        }
    }
 
}

