using Application.Interfaces;
using Domain.Entities;
using Presentation.Components;
using Presentation.Shells;
using System;
using System.Windows.Forms;

namespace Presentation.Screens.DriverScreen
{
    public partial class DriverDashboardForm : BaseForm
    {
        private readonly DriverShell _shell;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareService;

        private Trip _trip;

        public DriverDashboardForm()
        {
            InitializeComponent();
        }

        public DriverDashboardForm(
            DriverShell shell,
            ITripService tripService,
            IUserService userService,
            ISimulationService simulationService,
            IFareService fareService)
            : this()
        {
            _shell = shell;
            _tripService = tripService;
            _userService = userService;
            _simulationService = simulationService;
            _fareService = fareService;
        }

        public void OnNavigatedTo(Trip trip = null)
        {
            _trip = trip;

            if (_trip == null)
            {
                _statusLabel.Text = "Khong co chuyen dang hoat dong";
                _paymentPanel.Visible = false;
                return;
            }

            _statusLabel.Text = "Dang xu ly chuyen: " + _trip.Id.ToString();
        }

        public void OnNavigatingFrom()
        {
            _trip = null;
        }

        private MapControl _mapControl;
        private Panel _infoPanel;
        private Label _statusLabel;
        private Panel _stepBar;
        private Button _actionButton;
        private Panel _paymentPanel;
        private Label _fareLabel;
        private Label _commissionLabel;
        private Label _netEarningsLabel;
        private Button _confirmPaymentButton;
    }
}
