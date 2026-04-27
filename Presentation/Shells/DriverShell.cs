using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Presentation.Screens.DriverScreen;
using System;
using System.Windows.Forms;

namespace Presentation.Shells
{
    public partial class DriverShell : BaseShell
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;

        private Driver _driver;
        private Trip _currentTrip;

        public Driver Driver
        {
            get { return _driver; }
        }

        public Trip CurrentTrip
        {
            get { return _currentTrip; }
        }

        public DriverShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService,
            IFareService fareService,
            Driver driver)
        {
            _userService = userService;
            _tripService = tripService;
            _simulationService = simulationService;
            _fareService = fareService;
            _driver = driver;

            InitializeComponent();
        }

        private void DriverShell_Load(object sender, EventArgs e)
        {
            UpdateHeaderStatus();
            NavigateTo("Dashboard");
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            ToggleActive();
        }

        public void SetCurrentTrip(Trip trip)
        {
            _currentTrip = trip;
        }

        public void NavigateTo(string screenName)
        {
            _contentPanel.Controls.Clear();

            Form screen;
            if (string.Equals(screenName, "Dashboard", StringComparison.OrdinalIgnoreCase))
            {
                DriverDashboardForm dashboard = new DriverDashboardForm();
                dashboard.OnNavigatedTo(_currentTrip);
                screen = dashboard;
            }
            else
            {
                screen = new EarningsForm();
            }

            screen.TopLevel = false;
            screen.FormBorderStyle = FormBorderStyle.None;
            screen.Dock = DockStyle.Fill;
            _contentPanel.Controls.Add(screen);
            screen.Show();
        }

        public async void ToggleActive()
        {
            if (_driver == null)
            {
                return;
            }

            string newStatus = _driver.IsOffline() ? "Available" : "Offline";

            try
            {
                await _userService.UpdateDriverStatusAsync(_driver.Id, newStatus);
                if (newStatus == "Available")
                {
                    _driver.SetAvailable();
                }
                else
                {
                    _driver.SetOffline();
                }
                UpdateHeaderStatus();
            }
            catch
            {
                MessageBox.Show("Khong the cap nhat trang thai tai xe.");
            }
        }

        private void UpdateHeaderStatus()
        {
            if (_driver == null)
            {
                lblDriverName.Text = "Driver";
                btnToggleStatus.Text = "Go Online";
                return;
            }

            lblDriverName.Text = _driver.Name;
            btnToggleStatus.Text = _driver.IsOffline() ? "Go Online" : "Go Offline";
        }

        public void OnTripAccepted(Trip trip)
        {
            _currentTrip = trip;
            NavigateTo("Dashboard");
        }

        public void OnTripEnded()
        {
            _currentTrip = null;
            NavigateTo("Dashboard");
        }

        private void CleanupShell()
        {
        }
    }
}
