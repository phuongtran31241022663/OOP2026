using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

using Application.Interfaces;
using Domain.Enums;

namespace Presentation.Shells
{
    public partial class PassengerShell : BaseShell
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly ISimulationService _simulationService;

        private PassengerDto _passenger;
        private Trip _currentTrip;

        public PassengerDto Passenger => _passenger;
        private Dictionary<string, Form> _screens = new Dictionary<string, Form>();
        private System.Timers.Timer _pollTimer;
        private Dictionary<Guid, DateTime> _recentNotifications = new Dictionary<Guid, DateTime>();

        public PassengerShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService,
            PassengerDto passenger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));

            InitializeComponent();
            StartPollingTimer();
            FormClosed += PassengerShell_FormClosed;
        }

        public PassengerShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService)
            : this(userService, tripService, simulationService, new PassengerDto())
        {
        }

        private async void PassengerShell_Load(object sender, EventArgs e)
        {
            await InitializeShell();
        }

        private async Task InitializeShell()
        {
            // Register screens
            RegisterScreens();

            // Subscribe to trip events
            _tripService.TripStatusChanged += OnTripStatusChanged;

            // Restore active trip if any
            await RestoreActiveTrip();

            // Ensure the polling timer is running
            if (_pollTimer == null)
            {
                StartPollingTimer();
            }

            // Navigate to default
            NavigateTo("Home");
        }

        private void OnTripStatusChanged(Trip trip)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateTripDisplay(trip)));
            }
            else
            {
                UpdateTripDisplay(trip);
            }
        }

        private void UpdateTripDisplay(Trip trip)
        {
            if (trip.Status == TripStatus.Searching)
            {
                // Driver matching is handled by ITripService
            }
        }

        private void RegisterScreens()
        {
            // TODO: Create screen instances
            // _screens["Home"] = new BookTripForm(); // Assuming BookTripForm is Home
            _screens["Trip"] = new Screens.Passenger.TripTrackingForm(_tripService, _userService, _simulationService, this);
            // _screens["History"] = new TripHistoryForm();
            // _screens["Review"] = new ReviewForm();
            // _screens["Profile"] = new ProfileForm(); // TODO: Create if needed
        }

        public void NavigateTo(string screenKey)
        {
            if (!_screens.ContainsKey(screenKey))
                return;

            // Clear current content
            _contentPanel.Controls.Clear();

            // Add new screen
            var screen = _screens[screenKey];
            screen.TopLevel = false;
            screen.FormBorderStyle = FormBorderStyle.None;
            screen.Dock = DockStyle.Fill;
            _contentPanel.Controls.Add(screen);
            screen.Show();
        }

        private void StartPollingTimer()
        {
            _pollTimer = new System.Timers.Timer(4000); // 4 seconds
            _pollTimer.Elapsed += OnPollTimerElapsed;
            _pollTimer.AutoReset = true;
            _pollTimer.Enabled = true;
        }

        private async void OnPollTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await PollTripStatus();
        }

        private async Task PollTripStatus()
        {
            if (_currentTrip == null)
                return;

            try
            {
                // TODO: Fetch updated trip from ITripService.GetTripStatus(_currentTrip.Id)
                // For now, placeholder
                await Task.CompletedTask;

                // Update UI if status changed
                UpdateTripUI();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Poll error: {ex.Message}");
            }
        }

        private void UpdateTripUI()
        {
            // TODO: Update current trip screen with new data
        }



        private void HandleTripNotification(Guid tripId, string message)
        {
            // Prevent spam notifications
            if (_recentNotifications.ContainsKey(tripId))
            {
                var lastNotify = _recentNotifications[tripId];
                if ((DateTime.Now - lastNotify).TotalSeconds < 30) // 30 second debounce
                    return;
            }

            _recentNotifications[tripId] = DateTime.Now;

            // Thread-safe UI update
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowNotification(message)));
            }
            else
            {
                ShowNotification(message);
            }
        }

        private void ShowNotification(string message)
        {
            // TODO: Show notification in UI (toast, status bar, etc.)
            MessageBox.Show(message, "Trip Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OnTripStarted(Trip trip)
        {
            _currentTrip = trip;
            NavigateTo("Trip");
            UpdateTripBadge(true);
        }

        public async void OnTripFinished()
        {
            if (_currentTrip == null)
                return;

            // Check trip status
            // TODO: Assuming Completed for now
            bool isCompleted = true; // _currentTrip.Status == TripStatus.Completed;

            if (isCompleted)
            {
                // Ask user if they want to rate
                var result = MessageBox.Show("Trip completed! Would you like to rate your driver?", "Rate Driver",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NavigateTo("Review");
                }
            }
            else
            {
                // Cancelled/Timeout
                MessageBox.Show("Trip was cancelled or timed out.", "Trip Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateTripScreen();
            }

            _currentTrip = null;
            UpdateTripBadge(false);
        }

        private void UpdateTripScreen()
        {
            // TODO: Update trip tracking screen with ended status
        }

        private async Task RestoreActiveTrip()
        {
            // TODO: Get active trip for passenger from ITripService.GetActiveTripForPassenger(_passenger.Id)
            // For now, assume no active trip
            _currentTrip = null;
            await Task.CompletedTask;
        }

        private void UpdateTripBadge(bool hasActiveTrip)
        {
            // TODO: Update UI badge on Trip tab
        }

        private void UpdateReviewBadge(bool hasPendingReview)
        {
            // TODO: Update UI badge on Review tab
        }

        private void PassengerShell_FormClosed(object sender, FormClosedEventArgs e)
        {
            CleanupShell();
        }

        private void CleanupShell()
        {
            try
            {
                _tripService.TripStatusChanged -= OnTripStatusChanged;
            }
            catch
            {
                // Ignore if unsubscribe fails because event was not attached
            }

            if (_pollTimer != null)
            {
                _pollTimer.Elapsed -= OnPollTimerElapsed;
                _pollTimer.Stop();
                _pollTimer.Dispose();
                _pollTimer = null;
            }
        }

        // TODO: Implement other methods as per specification
    }
}
