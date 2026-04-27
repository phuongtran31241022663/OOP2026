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

namespace Presentation.Shells
{
    public partial class AdminShell : BaseShell
    {
        private readonly Admin _admin;
        private readonly IAdminService _adminService;
        private AdminViewModel _viewModel;
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
            _viewModel = new AdminViewModel(_admin, _adminService);
            await _viewModel.LoadDataAsync();
            PopulatePanels();
            UpdateStatsBar();
            ShowSection(0);
        }

        private void ShowSection(int index)
        {
            foreach (var panel in _panels.Values)
                panel.Visible = false;

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
        }

        private void UpdateStatsBar()
        {
            int totalUsers = _viewModel.TotalUsers;
            int activeDrivers = _viewModel.ActiveDrivers;
            int onTripDrivers = _viewModel.OnTripDrivers;
            int ongoingTrips = _viewModel.OngoingTrips;
        }

        private void PopulatePanels()
        {
            _panels[0] = new Panel() { Name = "UsersPanel", Dock = DockStyle.Fill, BackColor = Color.LightBlue };
            _panels[1] = new Panel() { Name = "DriversPanel", Dock = DockStyle.Fill, BackColor = Color.LightGreen };
            _panels[2] = new Panel() { Name = "PassengersPanel", Dock = DockStyle.Fill, BackColor = Color.LightYellow };
            _panels[3] = new Panel() { Name = "TripsPanel", Dock = DockStyle.Fill, BackColor = Color.LightPink };
            _panels[4] = new Panel() { Name = "FareRulesPanel", Dock = DockStyle.Fill, BackColor = Color.LightCyan };
            _panels[5] = new Panel() { Name = "ReportsPanel", Dock = DockStyle.Fill, BackColor = Color.LightGray };

            foreach (var panel in _panels.Values)
                this.Controls.Add(panel);
        }

        private void PopulateUsersPanel() { }
        private void FilterUsers(string searchTerm) { }
        private async void ToggleUserActive(Guid userId) { }
        private void PopulateDriversPanel() { }
        private void PopulatePassengersPanel() { }
        private void PopulateTripsPanel() { }

        private void FilterTrips(string searchTerm)
        {
            var filtered = _viewModel.GetFilteredTrips(searchTerm);
        }

        private void PopulateTripsPanelLogic()
        {
            foreach (var trip in _viewModel.GetFilteredTrips(""))
            {
                // Các điều kiện lọc status đã chuyển sang string
                bool isOngoing = trip.Status == "Searching" ||
                                 trip.Status == "Matched" ||
                                 trip.Status == "Arrived" ||
                                 trip.Status == "Started";
            }
        }
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
            if (trips != null) _allTrips.AddRange(trips);

            TotalUsers = AllUsers.Count;
            TotalTrips = _allTrips.Count;
            ActiveDrivers = 0;
            OnTripDrivers = 0;
            OngoingTrips = 0;

            for (int i = 0; i < AllDrivers.Count; i++)
            {
                Driver driver = AllDrivers[i];
                if (driver.Status == DriverStatus.Available) ActiveDrivers++;
                if (driver.Status == DriverStatus.OnTrip) OnTripDrivers++;
            }

            for (int i = 0; i < _allTrips.Count; i++)
            {
                Trip trip = _allTrips[i];
                if (trip.Status == "Searching" ||
                    trip.Status == "Matched" ||
                    trip.Status == "Arrived" ||
                    trip.Status == "Started")
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
                    trip.Status.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
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
