using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using Presentation.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Shells
{
    /// <summary>
    /// Single-Form Shell duy nhat cua ung dung.
    /// Chua mot Panel noi dung chinh; hoan doi UserControl de chuyen man hinh.
    /// Quản lý hiển thị các UserControl và thông báo hệ thống.
    /// </summary>
    public partial class FrmMain : BaseForm
    {
        private readonly IUserService _userService;
        private readonly ITripService _tripService;
        private readonly IFareService _fareService;
        private readonly IMapService _mapService;
        private readonly ISimulationService _simulationService;
        private readonly IAdminService _adminService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;
        private readonly IVehicleRepository _vehicleRepository;

        private User _currentUser; // sao cái này không dùng
        private UcAuth _ucAuth;
private UcPassenger _ucPassenger;
private UcDriver _ucDriver;
private UcAdmin _ucAdmin;

        public FrmMain(
            IUserService userService,
            ITripService tripService,
            IFareService fareService,
            IMapService mapService,
            ISimulationService simulationService,
            IAdminService adminService,
            IMatchingService matchingService,
            IReviewService reviewService,
            IVehicleRepository vehicleRepository)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));

            InitializeComponent();
            SetupShell();
        }

        private void SetupShell()
        {
            Text = "OOP";
            Size = new Size(1200, 800);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.5f);
            // BaseForm giờ là shell đơn giản, pnlContent đã Dock = Fill trong Designer

            // Thêm sự kiện cho nút Multi-Role
            btnMultiRole.Click += btnMultiRole_Click;
            btnMultiRole.BringToFront();
        }

        private void btnMultiRole_Click(object sender, EventArgs e)
        {
            ExecuteWithHandling("Mở giao diện Multi-Role", () =>
            {
                // ơ cái này ngộ, tạo trong data seed rồi lấy nó làm cái test cố định, chứ sao tạo ở đây
                // Tạo tài khoản demo cho Passenger và Driver
                var passenger = new Passenger(
                    "Hành khách Demo",
                    "0911111111",
                    "123456");

                var demoLocation = new Location(
                    new Coordinate(10.7769, 106.7009),
                    new Address("Điểm demo", "Lê Lợi", "Quận 1", "Hồ Chí Minh", "Việt Nam", "1"));

                var driver = new Driver(
                    "Tài xế Demo",
                    "0900000000",
                    "123456",
                    "DL123456",
                    Guid.NewGuid(),
                    demoLocation);

                var multiRoleForm = new FrmMultiRole(
                    _userService,
                    _tripService,
                    _fareService,
                    _mapService,
                    _simulationService,
                    _matchingService,
                    _reviewService,
                    _vehicleRepository,
                    passenger,
                    driver);

                multiRoleForm.Show();
            });
        }
        // 1 method deadcode
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ExecuteWithHandling("Mo man hinh dang nhap", ShowAuthScreen);
        }

        // --- Navigation ----------------------------------------------------

public void ShowAuthScreen()
        {
            ExecuteWithHandling("Tai man hinh dang nhap", () =>
            {
                _currentUser = null;
                if (_ucAuth == null)
                {
                    _ucAuth = new UcAuth(_userService, _vehicleRepository);
                    _ucAuth.LoginSucceeded += OnLoginSucceeded;
                    _ucAuth.RegisterSucceeded += OnRegisterSucceeded;
                }
                ShowUserControl(_ucAuth);
            });
        }

        public void ShowPassengerScreen(Passenger passenger)
        {
            ExecuteWithHandling("Mo man hinh hanh khach", () =>
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
                }
                ShowUserControl(_ucPassenger);
            });
        }

        public void ShowDriverScreen(Driver driver)
        {
            ExecuteWithHandling("Mo man hinh tai xe", () =>
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
                }
                ShowUserControl(_ucDriver);
            });
        }

        public void ShowAdminScreen(Admin admin)
        {
            ExecuteWithHandling("Mo man hinh quan tri", () =>
            {
                _currentUser = admin;
                if (_ucAdmin == null)
                {
                    _ucAdmin = new UcAdmin(admin, _adminService);
                    _ucAdmin.RequestLogout += OnRequestLogout;
                }
                ShowUserControl(_ucAdmin);
            });
        }

        private void ShowUserControl(UserControl uc)
        {
            ExecuteWithHandling("Hien thi noi dung man hinh", () =>
            {
                if (uc == null)
                {
                    throw new InvalidOperationException("Noi dung man hinh khong hop le.");
                }

                // Dispose controls khong nam trong cache truoc khi clear de tranh memory leak
                foreach (Control existing in pnlContent.Controls)
                {
                    bool isCached = existing == _ucAuth || existing == _ucPassenger
                                 || existing == _ucDriver || existing == _ucAdmin;
                    if (!isCached)
                    {
                        existing.Dispose();
                    }
                }

                pnlContent.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
                uc.Focus();
            });
        }

        // --- Event Handlers ------------------------------------------------

        private void OnLoginSucceeded(object sender, User user)
        {
            ExecuteWithHandling("Dieu huong sau dang nhap", () =>
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
                    default:
                        throw new InvalidOperationException("Vai tro nguoi dung khong duoc ho tro.");
                }
            });
        }

        private void OnRegisterSucceeded(object sender, User user)
        {
            OnLoginSucceeded(sender, user);
        }

        private void OnRequestLogout(object sender, EventArgs e)
        {
            ExecuteWithHandling("Dang xuat", () =>
            {
if (_ucPassenger is UcPassenger ucP)
                {
                    ucP.RequestLogout -= OnRequestLogout;
                    ucP.Dispose();
                }
if (_ucDriver is UcDriver ucD)
                {
                    ucD.RequestLogout -= OnRequestLogout;
                    ucD.Dispose();
                }
                if (_ucAdmin != null)
                {
                    _ucAdmin.Dispose();
                }
                if (_ucAuth != null)
                {
                    _ucAuth.LoginSucceeded -= OnLoginSucceeded;
                    _ucAuth.RegisterSucceeded -= OnRegisterSucceeded;
                    _ucAuth.Dispose();
                }
            }, () =>
            {
                _ucPassenger = null;
                _ucDriver = null;
                _ucAdmin = null;
                _ucAuth = null; 
                ShowAuthScreen();
            });
        }

    }
}
