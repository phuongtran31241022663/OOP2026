using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Application.DTOs;
using Application.Interfaces;
using Presentation.Screens.Driver;
using Domain.Enums;

using Presentation;

namespace Presentation.Shells
{
    public partial class DriverShell : BaseShell
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;

        private DriverDto _driver;
        private TripDto _currentTrip;

        public TripDto CurrentTrip => _currentTrip;
        public void SetCurrentTrip(TripDto trip) => _currentTrip = trip;
        public DriverDto Driver => _driver;
        private bool _activeToggleState;
        private HashSet<Guid> _notifiedTripIds = new HashSet<Guid>();
        private Dictionary<Guid, DateTime> _recentNotifications = new Dictionary<Guid, DateTime>();

        private System.Timers.Timer _refreshTimer;
        private Dictionary<string, Form> _screens = new Dictionary<string, Form>();

        public DriverShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService,
            IFareService fareService,
            DriverDto driver)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));

            InitializeComponent();
            FormClosed += DriverShell_FormClosed;
        }

        private async void DriverShell_Load(object sender, EventArgs e)
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

            // Update header
            UpdateHeaderStatus();

            // Start refresh timer
            StartRefreshTimer();
        }

        private void RegisterScreens()
        {
            // TODO: Create screen instances
            // _screens["Dashboard"] = new DriverDashboardForm();
            // _screens["Trip"] = new TripNavigationForm();
            // _screens["History"] = new EarningsForm(); // Assuming EarningsForm is history
            // _screens["Profile"] = new ProfileForm(); // TODO: Create if needed

            // Navigate to default
            NavigateTo("Dashboard");
        }



        private void StartRefreshTimer()
        {
            _refreshTimer = new System.Timers.Timer(3000); // 3 seconds
            _refreshTimer.Elapsed += OnRefreshTimerElapsed;
            _refreshTimer.AutoReset = true;
            _refreshTimer.Enabled = true;
        }

        private void OnRefreshTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    // Refresh dashboard if current screen is dashboard
                    if (_contentPanel.Controls.Count > 0 && _contentPanel.Controls[0] is DriverDashboardForm)
                    {
                        RefreshDashboard();
                    }
                }));
            }
            else
            {
                // Refresh dashboard if current screen is dashboard
                if (_contentPanel.Controls.Count > 0 && _contentPanel.Controls[0] is DriverDashboardForm)
                {
                    RefreshDashboard();
                }
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            ToggleActive();
        }

        private async Task RestoreActiveTrip()
        {
            // TODO: Get active trip for driver from ITripService.GetActiveTripForDriver(_driver.Id)
            // For now, assume no active trip
            _currentTrip = null;
            await Task.CompletedTask;
        }

        public void NavigateTo(string screenName)
        {
            _contentPanel.Controls.Clear();

            switch (screenName)
            {
                case "Dashboard":
                    var dashboard = new DriverDashboardForm(
                        this,
                        _tripService,
                        _userService,
                        _simulationService,
                        _fareService);
                    dashboard.OnNavigatedTo(_currentTrip);
                    _contentPanel.Controls.Add(dashboard);
                    break;

                case "Earnings":
                    var earnings = new EarningsForm();
                    _contentPanel.Controls.Add(earnings);
                    break;

                case "History":
                    // Using passenger TripHistoryForm - adapt for driver
                    var history = new Presentation.Screens.Passenger.TripHistoryForm(_driver.Id, _tripService);
                    _contentPanel.Controls.Add(history);
                    break;

                default:
                    NavigateTo("Dashboard");
                    break;
            }
        }

        private void RefreshDashboard()
        {
            if (_contentPanel.Controls.Count > 0 && _contentPanel.Controls[0] is DriverDashboardForm dashboard)
            {
                dashboard.OnNavigatedTo(_currentTrip);
            }
        }

        public async void ToggleActive()
        {
            if (_driver == null) return;

            try
            {
                var newStatus = _driver.Status == DriverStatus.Offline ? DriverStatus.Available : DriverStatus.Offline;
                await _userService.UpdateDriverStatus(_driver.Id, newStatus);
                _driver.Status = newStatus;

                // Update UI
                UpdateHeaderStatus();

                // Navigate to dashboard if going online
                if (newStatus == DriverStatus.Available)
                {
                    NavigateTo("Dashboard");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update status: {ex.Message}");
            }
        }

        private void UpdateHeaderStatus()
        {
            if (_driver == null) return;

            lblDriverName.Text = _driver.Name;
            btnToggleStatus.Text = _driver.Status == DriverStatus.Offline ? "Go Online" : "Go Offline";
        }

        private void DriverShell_FormClosed(object sender, FormClosedEventArgs e)
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

            if (_refreshTimer != null)
            {
                _refreshTimer.Elapsed -= OnRefreshTimerElapsed;
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
                _refreshTimer = null;
            }
        }

        private void OnTripStatusChanged(TripDto trip)
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

        // Public methods for external control (called by TripNavigationForm)
        public void OnTripAccepted(TripDto trip)
        {
            SetCurrentTrip(trip);
            // Refresh dashboard if needed
            RefreshDashboard();
        }

        public void OnTripEnded()
        {
            SetCurrentTrip(null);
            NavigateTo("Dashboard");
        }

        private void UpdateTripDisplay(TripDto trip)
        {
            // Handle navigation based on trip status
            if (trip.DriverId == _driver.Id)
            {
                switch (trip.Status)
                {
                    case TripStatus.Matched:
                        NavigateTo("Trip");
                        break;
                    case TripStatus.Completed:
                    case TripStatus.Cancelled:
                        NavigateTo("Dashboard");
                        break;
                }
            }
        }
    }
}

