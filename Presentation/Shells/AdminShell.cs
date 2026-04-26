using Domain.ValueObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Entities.Users;
using Application.Interfaces;
using Domain.Enums;

using Presentation;

namespace Presentation.Shells
{
    public partial class AdminShell : BaseShell
    {
        // Dependencies
        private readonly Admin _admin;
        private readonly IAdminService _adminService;

        // ViewModel
        private AdminViewModel _viewModel;

        // UI components
        private Dictionary<int, Panel> _panels = new Dictionary<int, Panel>();

        public AdminShell(Admin admin, IAdminService adminService)
        {
            _admin = admin ?? throw new ArgumentNullException(nameof(admin));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));

            InitializeComponent();
        }

        private async void AdminShell_Load(object sender, EventArgs e)
        {
            await InitializeShell();
        }

        private async Task InitializeShell()
        {
            // Initialize view model
            _viewModel = new AdminViewModel(_admin, _adminService);

            // Load data via view model
            await _viewModel.LoadDataAsync();

            // Populate panels
            PopulatePanels();

            // Update stats bar
            UpdateStatsBar();

            // Show default section (Users)
            ShowSection(0);
        }

        private void ShowSection(int index)
        {
            // Hide all panels
            foreach (var panel in _panels.Values)
            {
                panel.Visible = false;
            }

            // Show selected panel
            if (_panels.ContainsKey(index))
            {
                _panels[index].Visible = true;
                UpdateContentTitle(index);
            }
        }

        private void UpdateContentTitle(int index)
        {
            string title = index switch
            {
                0 => "Users Management",
                1 => "Drivers Management",
                2 => "Passengers Management",
                3 => "Trips Management",
                4 => "Fare Rules",
                5 => "Reports",
                _ => "Admin Dashboard"
            };
            // TODO: Update header title
        }

        private void UpdateStatsBar()
        {
            // Stats from ViewModel
            int totalUsers = _viewModel.TotalUsers;
            int activeDrivers = _viewModel.ActiveDrivers;
            int onTripDrivers = _viewModel.OnTripDrivers;
            int ongoingTrips = _viewModel.OngoingTrips;

            // TODO: Update stats bar UI elements
        }

        private void PopulatePanels()
        {
            // Create panels for each section
            _panels[0] = new Panel() { Name = "UsersPanel", Dock = DockStyle.Fill, BackColor = Color.LightBlue };
            _panels[1] = new Panel() { Name = "DriversPanel", Dock = DockStyle.Fill, BackColor = Color.LightGreen };
            _panels[2] = new Panel() { Name = "PassengersPanel", Dock = DockStyle.Fill, BackColor = Color.LightYellow };
            _panels[3] = new Panel() { Name = "TripsPanel", Dock = DockStyle.Fill, BackColor = Color.LightPink };
            _panels[4] = new Panel() { Name = "FareRulesPanel", Dock = DockStyle.Fill, BackColor = Color.LightCyan };
            _panels[5] = new Panel() { Name = "ReportsPanel", Dock = DockStyle.Fill, BackColor = Color.LightGray };

            // Add to form
            foreach (var panel in _panels.Values)
            {
                this.Controls.Add(panel);
            }
        }

        // Users Panel
        private void PopulateUsersPanel()
        {
            // TODO: Populate DataGridView with _allUsers
            // Columns: Name, Phone, Role, Status, Actions
        }

        private void FilterUsers(string searchTerm)
        {
            // Filter users by name or phone
            var filtered = _viewModel.AllUsers.Where(u =>
                u.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                u.Phone.Contains(searchTerm)
            ).ToList();
            // TODO: Update DataGridView with filtered list
        }

        private async void ToggleUserActive(Guid userId)
        {
            try
            {
                MessageBox.Show(
                    "Toggle active/inactive is not implemented in current admin service.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Refresh data
                await _viewModel.LoadDataAsync();
                PopulateUsersPanel();
                UpdateStatsBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error toggling user status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Drivers Panel
        private void PopulateDriversPanel()
        {
            // Drivers from ViewModel
            var drivers = _viewModel.AllDrivers;

            // TODO: Populate DataGridView with drivers
            // Columns: Name, Phone, Status, Review, Trips Count, Vehicle, Actions
        }

        // Passengers Panel
        private void PopulatePassengersPanel()
        {
            // Passengers from ViewModel
            var passengers = _viewModel.AllPassengers;

            // TODO: Populate DataGridView with passengers
            // Columns: Name, Phone, Status, Trips Count, Actions
        }

        // Trips Panel
        private void PopulateTripsPanel()
        {
            // Get trips with passenger/driver names from ViewModel
            var trips = _viewModel.GetFilteredTrips("");

            // TODO: Populate DataGridView with trips
            // The Trip already contains PassengerName, DriverName, Pickup, Destination, Status, Fare, CreatedAt
        }

        private void FilterTrips(string searchTerm)
        {
            // Get filtered trips from ViewModel
            var filtered = _viewModel.GetFilteredTrips(searchTerm);
            // TODO: Update DataGridView with filtered list
        }

        private void GenerateTripReport()
        {
            var report = _viewModel.GetTripReport();

            // TODO: Display report in UI
            // Total trips, revenue, driver income, commission
        }

        // Fare Rules Panel
        private void PopulateFareRulesPanel()
        {
            // TODO: Load and display fare rules
            // TODO: Add CRUD operations
        }

        // Reports Panel
        private void PopulateReportsPanel()
        {
            // Summary stats from ViewModel
            int totalTrips = _viewModel.TotalTrips;
            decimal totalRevenue = _viewModel.TotalRevenue;

            // TODO: Display in UI
        }

        // TODO: Implement panel-specific methods
    }

    internal class AdminViewModel
    {
        private readonly Admin _admin;
        private readonly IAdminService _adminService;
        private readonly List<Trip> _allTrips = new List<Trip>();

        public int TotalUsers { get; private set; }
        public int ActiveDrivers { get; private set; }
        public int OnTripDrivers { get; private set; }
        public int OngoingTrips { get; private set; }
        public int TotalTrips { get; private set; }
        public decimal TotalRevenue { get; private set; }

        public List<User> AllUsers { get; private set; }
        public List<Driver> AllDrivers { get; private set; }
        public List<Passenger> AllPassengers { get; private set; }

        public AdminViewModel(Admin admin, IAdminService adminService)
        {
            _admin = admin;
            _adminService = adminService;
            AllUsers = new List<User>();
            AllDrivers = new List<Driver>();
            AllPassengers = new List<Passenger>();
        }

        public async Task LoadDataAsync()
        {
            List<User> users = await _adminService.GetAllUsersAsync();
            List<Driver> drivers = await _adminService.GetAllDriversAsync();
            List<Passenger> passengers = await _adminService.GetAllPassengersAsync();
            List<Trip> trips = await _adminService.GetAllTripsAsync();

            AllUsers = users ?? new List<User>();
            AllDrivers = drivers ?? new List<Driver>();
            AllPassengers = passengers ?? new List<Passenger>();

            _allTrips.Clear();
            if (trips != null)
            {
                _allTrips.AddRange(trips);
            }

            TotalUsers = AllUsers.Count;
            TotalTrips = _allTrips.Count;
            ActiveDrivers = 0;
            OnTripDrivers = 0;
            OngoingTrips = 0;

            for (int i = 0; i < AllDrivers.Count; i++)
            {
                Driver driver = AllDrivers[i];
                if (driver.Status == DriverStatus.Available)
                {
                    ActiveDrivers++;
                }
                if (driver.Status == DriverStatus.OnTrip)
                {
                    OnTripDrivers++;
                }
            }

            for (int i = 0; i < _allTrips.Count; i++)
            {
                Trip trip = _allTrips[i];
                if (trip.Status == TripStatus.Searching ||
                    trip.Status == TripStatus.Matched ||
                    trip.Status == TripStatus.Arrived ||
                    trip.Status == TripStatus.Started)
                {
                    OngoingTrips++;
                }
            }

            decimal gmv = await _adminService.GetTotalGMVAsync();
            TotalRevenue = gmv;
        }

        public List<Trip> GetFilteredTrips(string searchTerm)
        {
            List<Trip> result = new List<Trip>();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                result.AddRange(_allTrips);
                return result;
            }

            string term = searchTerm.Trim();
            for (int i = 0; i < _allTrips.Count; i++)
            {
                Trip trip = _allTrips[i];
                if (trip.Id.ToString().IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    trip.Status.ToString().IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(trip);
                }
            }

            return result;
        }

        public string GetTripReport()
        {
            return string.Format(
                "Admin: {0}\nTotal trips: {1}\nTotal users: {2}\nTotal revenue: {3:N0}",
                _admin != null ? _admin.Name : "N/A",
                TotalTrips,
                TotalUsers,
                TotalRevenue);
        }
    }
}

