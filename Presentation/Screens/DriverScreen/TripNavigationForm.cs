using Application.Interfaces;
using Domain.Entities;
using Presentation.Shells;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.DriverScreen
{
    public partial class TripNavigationForm : BaseForm
    {
        private readonly DriverShell _shell;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;
        private Trip _pendingTrip;

        public TripNavigationForm(
            DriverShell shell,
            ITripService tripService,
            IUserService userService,
            ISimulationService simulationService)
        {
            _shell = shell;
            _tripService = tripService;
            _userService = userService;
            _simulationService = simulationService;
            InitializeComponent();
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            _emptyMessageLabel.Text = "Da lam moi danh sach chuyen.";
            _emptyPanel.Visible = true;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = false;
        }

        private void OnAcceptClicked(object sender, EventArgs e)
        {
            if (_pendingTrip != null)
            {
                _shell.SetCurrentTrip(_pendingTrip);
                _shell.OnTripAccepted(_pendingTrip);
            }

            _requestPanel.Visible = false;
            _activeTripPanel.Visible = true;
            _emptyPanel.Visible = false;
        }

        private void OnRejectClicked(object sender, EventArgs e)
        {
            _pendingTrip = null;
            _requestPanel.Visible = false;
            _activeTripPanel.Visible = false;
            _emptyPanel.Visible = true;
            _emptyMessageLabel.Text = "Da tu choi chuyen.";
        }

        private void OnActionClicked(object sender, EventArgs e)
        {
            _shell.OnTripEnded();
            _activeTripPanel.Visible = false;
            _emptyPanel.Visible = true;
            _emptyMessageLabel.Text = "Da cap nhat trang thai chuyen.";
        }
    }
}
