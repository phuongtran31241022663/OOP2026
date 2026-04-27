using Application.Interfaces;
using Domain.Entities.Users;
using Presentation.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;
using Domain.Entities;

namespace Presentation.Shells
{
    /// <summary>
    /// Single-Form Shell duy nhat cua ung dung.
    /// Chua mot Panel noi dung chinh; hoan doi UserControl de chuyen man hinh.
    /// Quan ly FrmModal va FrmToast.
    /// </summary>
    public partial class FrmMainShell : Form
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly IFareService _fareService;
        private readonly IMapService _mapService;
        private readonly ISimulationService _simulationService;
        private readonly IAdminService _adminService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;

        private User _currentUser;
        private UcAuth _ucAuth;
        private UcPassenger _ucPassenger;
        private UcDriver _ucDriver;
        private UcAdmin _ucAdmin;

        public FrmMainShell(
            IUserService userService,
            ITripService tripService,
            IFareService fareService,
            IMapService mapService,
            ISimulationService simulationService,
            IAdminService adminService,
            IMatchingService matchingService,
            IReviewService reviewService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));

            InitializeComponent();
            SetupShell();
        }

        private void SetupShell()
        {
            Text = "RideGo";
            Size = new Size(1200, 800);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.5f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowAuthScreen();
        }

        // --- Navigation ----------------------------------------------------

        public void ShowAuthScreen()
        {
            _currentUser = null;
            if (_ucAuth == null)
            {
                _ucAuth = new UcAuth(_userService);
                _ucAuth.LoginSucceeded += OnLoginSucceeded;
                _ucAuth.RegisterSucceeded += OnRegisterSucceeded;
            }
            ShowUserControl(_ucAuth);
        }

        public void ShowPassengerScreen(Passenger passenger)
        {
            _currentUser = passenger;
            if (_ucPassenger == null)
            {
                _ucPassenger = new UcPassenger(
                    passenger,
                    _tripService,
                    _userService,
                    _mapService,
                    _fareService,
                    _simulationService,
                    _matchingService,
                    _reviewService);
                _ucPassenger.RequestLogout += OnRequestLogout;
                _ucPassenger.RequestShowProfile += OnRequestShowProfile;
            }
            ShowUserControl(_ucPassenger);
        }

        public void ShowDriverScreen(Driver driver)
        {
            _currentUser = driver;
            if (_ucDriver == null)
            {
                _ucDriver = new UcDriver(
                    driver,
                    _tripService,
                    _userService,
                    _simulationService,
                    _fareService,
                    _matchingService);
                _ucDriver.RequestLogout += OnRequestLogout;
                _ucDriver.RequestShowProfile += OnRequestShowProfile;
            }
            ShowUserControl(_ucDriver);
        }

        public void ShowAdminScreen(Admin admin)
        {
            _currentUser = admin;
            if (_ucAdmin == null)
            {
                _ucAdmin = new UcAdmin(admin, _adminService);
                _ucAdmin.RequestLogout += OnRequestLogout;
            }
            ShowUserControl(_ucAdmin);
        }

        private void ShowUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
            uc.Focus();
        }

        // --- Event Handlers ------------------------------------------------

        private void OnLoginSucceeded(object sender, User user)
        {
            switch (user)
            {
                case Passenger passenger:
                    ShowPassengerScreen(passenger);
                    break;
                case Driver driver:
                    ShowDriverScreen(driver);
                    break;
                case Admin admin:
                    ShowAdminScreen(admin);
                    break;
            }
        }

        private void OnRegisterSucceeded(object sender, User user)
        {
            OnLoginSucceeded(sender, user);
        }

        private void OnRequestLogout(object sender, EventArgs e)
        {
            _ucPassenger?.Dispose();
            _ucPassenger = null;
            _ucDriver?.Dispose();
            _ucDriver = null;
            _ucAdmin?.Dispose();
            _ucAdmin = null;
            ShowAuthScreen();
        }

        private void OnRequestShowProfile(object sender, User user)
        {
            var ucProfile = new UcProfile(user, _userService);
            FrmModal.ShowModal(this, ucProfile, "Ho so ca nhan");
        }

        // --- Modal & Toast Helpers -----------------------------------------

        public static void ShowModal(UserControl content, string title)
        {
            FrmModal.ShowModal(null, content, title);
        }

        public void ShowToast(string message, int durationMs = 3000)
        {
            FrmToast.Show(this, message, durationMs);
        }
    }
}

