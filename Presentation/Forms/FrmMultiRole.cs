using Application.Interfaces;
using Domain.Entities.Users;
using Domain.Repositories;
using Presentation.Base;
using Presentation.Constants;
using Presentation.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Shells
{
    /// <summary>
    /// Form cho phép mở song song 2 giao diện (Driver và Passenger) trên cùng một nguồn dữ liệu.
    /// </summary>
    public partial class FrmMultiRole : BaseForm
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly IFareService _fareService;
        private readonly IMapService _mapService;
        private readonly ISimulationService _simulationService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;
        private readonly IVehicleRepository _vehicleRepository;

        private UcPassenger _ucPassenger;
        private UcDriver _ucDriver;

        private Passenger _passenger;
        private Driver _driver;

        public FrmMultiRole(
            IUserService userService,
            ITripService tripService,
            IFareService fareService,
            IMapService mapService,
            ISimulationService simulationService,
            IMatchingService matchingService,
            IReviewService reviewService,
            IVehicleRepository vehicleRepository,
            Passenger passenger,
            Driver driver)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));

            InitializeComponent();
            SetupForm();
            InitializeControls();
        }

        private void SetupForm()
        {
            Text = "Multi-Role View";
            Size = new Size(1400, 800);
            MinimumSize = new Size(1200, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = UiConstants.Colors.SurfaceWhite;
            Font = UiConstants.Typography.Default;
        }

        private void InitializeControls()
        {
            // Passenger panel (left)
            _ucPassenger = new UcPassenger(
                _passenger,
                _tripService,
                _userService,
                _mapService,
                _fareService,
                _simulationService,
                _matchingService,
                _reviewService);
            _ucPassenger.Dock = DockStyle.Fill;
            pnlPassenger.Controls.Add(_ucPassenger);

            // Driver panel (right)
            _ucDriver = new UcDriver(
                _driver,
                _tripService,
                _userService,
                _simulationService,
                _fareService,
                _matchingService);
            _ucDriver.Dock = DockStyle.Fill;
            pnlDriver.Controls.Add(_ucDriver);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _ucPassenger?.Dispose();
            _ucDriver?.Dispose();
        }
    }
}