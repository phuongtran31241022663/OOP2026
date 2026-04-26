using Application.Interfaces;
using Domain.Entities;
using Presentation.Shells;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    public partial class TripTrackingForm : BaseForm
    {
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly PassengerShell _parentShell;
        private Trip _currentTrip;

        public TripTrackingForm(ITripService tripService, IUserService userService, PassengerShell parentShell)
        {
            _tripService = tripService;
            _userService = userService;
            _parentShell = parentShell;
            InitializeComponent();
        }

        public TripTrackingForm(
            ITripService tripService,
            IUserService userService,
            ISimulationService simulationService,
            PassengerShell parentShell)
            : this(tripService, userService, parentShell)
        {
        }

        private void TripTrackingForm_Load(object sender, EventArgs e)
        {
            _statusBarLabel.Text = "Khong co chuyen di dang hoat dong";
            _emptyPanel.Visible = true;
            _activePanel.Visible = false;
        }

        private void OnGoHomeClicked(object sender, EventArgs e)
        {
            if (_parentShell != null)
            {
                _parentShell.NavigateTo("Home");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            _statusBarLabel.Text = "Chuc nang huy chuyen tam thoi duoc vo hieu hoa.";
        }

        public void ApplyTripUpdate(Trip trip)
        {
            _currentTrip = trip;
            _emptyPanel.Visible = trip == null;
            _activePanel.Visible = trip != null;
        }

        public void OnTripStarted(Trip trip)
        {
            ApplyTripUpdate(trip);
        }

        public void RefreshData()
        {
            ApplyTripUpdate(_currentTrip);
        }
    }
}
