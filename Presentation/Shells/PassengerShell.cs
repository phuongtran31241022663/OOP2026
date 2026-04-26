using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Presentation.Screens.PassengerScreen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace Presentation.Shells
{
    public partial class PassengerShell : BaseShell
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;
        private readonly IMapService _mapService;

        private Passenger _passenger;
        private Trip _currentTrip;
        private readonly Dictionary<string, Form> _screens = new Dictionary<string, Form>();

        public Passenger Passenger
        {
            get { return _passenger; }
        }

        public PassengerShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService,
            Passenger passenger)
        {
            _userService = userService;
            _tripService = tripService;
            _simulationService = simulationService;
            _passenger = passenger;

            _fareService = null;
            _mapService = null;

            InitializeComponent();
        }

        public PassengerShell(
            IUserService userService,
            ITripService tripService,
            ISimulationService simulationService)
            : this(
                userService,
                tripService,
                simulationService,
                new Passenger("Passenger Demo", "0911111111", "123456"))
        {
        }

        private void PassengerShell_Load(object sender, EventArgs e)
        {
            RegisterScreens();
            NavigateTo("Home");
        }

        private void RegisterScreens()
        {
            HttpClient httpClient = new HttpClient();

            _screens["Home"] = new BookTripForm(
                _tripService,
                _userService,
                _mapService,
                _fareService,
                httpClient,
                this);

            _screens["Trip"] = new TripTrackingForm(_tripService, _userService, this);
            _screens["History"] = new TripHistoryForm(_passenger.Id, _tripService);
        }

        public void NavigateTo(string screenKey)
        {
            if (!_screens.ContainsKey(screenKey))
            {
                return;
            }

            _contentPanel.Controls.Clear();
            Form screen = _screens[screenKey];
            screen.TopLevel = false;
            screen.FormBorderStyle = FormBorderStyle.None;
            screen.Dock = DockStyle.Fill;
            _contentPanel.Controls.Add(screen);
            screen.Show();
        }

        public void OnTripStarted(Trip trip)
        {
            _currentTrip = trip;
            NavigateTo("Trip");
        }

        public void OnTripFinished()
        {
            _currentTrip = null;
            NavigateTo("Home");
        }

        private void CleanupShell()
        {
        }
    }
}
