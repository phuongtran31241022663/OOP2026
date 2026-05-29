using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmMultiRole : Form
    {
        // Nhóm các service dùng chung để tránh trùng lặp field
        private Psg _passenger;
        private Drv _driver;
        private Adm _admin;

        private readonly IUsrSvc _userService;
        private readonly ITripCmd _tripCmd;
        private readonly ITripQry _tripQuery;
        private readonly IFareSvc _fareService;
        private readonly IMapSvc _mapService;
        private readonly IRevSvc _reviewService;
        private readonly IDrvCmd _driverCmd;
        private readonly IDrvQry _driverQuery;
        private readonly IWalletSvc _walletService;
        private readonly IUsrRepo _userRepo;
        private readonly IVehRepo _vehicleRepo;
        private readonly IPsgSvc _passengerService;
        private readonly INotiSvc _notificationSvc;

        private readonly ITripRepo _tripRepo;
        private readonly IPolRepo _policyRepo;
        private readonly IRevRepo _reviewRepo;
        private readonly IAdmSvc _adminService;

        private System.Windows.Forms.Timer _onlineDriverTimer;

        public FrmMultiRole(
            IUsrSvc us,
            IVehRepo vr,
            IUsrRepo ur,
            ITripRepo tr,
            IPolRepo pr,
            IRevRepo rr,
            ITripCmd tc,
            ITripQry tq,
            IDrvCmd dc,
            IDrvQry dq,
            IMapSvc ms,
            IFareSvc fs,
            IRevSvc rs,
            IWalletSvc ws,
            IPsgSvc ps,
            IAdmSvc ads,
            INotiSvc ns)
        {
            _userService = us;
            _vehicleRepo = vr;
            _userRepo = ur;
            _tripRepo = tr;
            _policyRepo = pr;
            _reviewRepo = rr;
            _tripCmd = tc;
            _tripQuery = tq;
            _driverCmd = dc;
            _driverQuery = dq;
            _mapService = ms;
            _fareService = fs;
            _reviewService = rs;
            _walletService = ws;
            _passengerService = ps;
            _adminService = ads;
            _notificationSvc = ns;

            InitializeComponent();
            this.DoubleBuffered = true;

            // Tự động load các tài khoản demo để hiển thị dữ liệu
            LoadDemoAccounts();

            InitializePanels();
            AttachEvents();
            StartOnlineDriverTimer();
        }

        private void LoadDemoAccounts()
        {
            // Sử dụng GetAwaiter().GetResult() vì Constructor không hỗ trợ async/await
            // Trong thực tế nên xử lý ở màn hình Loading, nhưng đây là đồ án demo nên ưu tiên sự tiện lợi
            _passenger = _userRepo.GetByPhoneAsync("0911111111").GetAwaiter().GetResult() as Psg;
            _driver = _userRepo.GetByPhoneAsync("0900000000").GetAwaiter().GetResult() as Drv;
            _admin = _userRepo.GetByPhoneAsync("0999999999").GetAwaiter().GetResult() as Adm;
        }

        private void InitializePanels()
        {
            if (_passenger != null)
            {
                _passengerPanel.Initialize(_passenger, _userService, _tripCmd, _tripQuery, _reviewService, _fareService, _mapService, _passengerService, _notificationSvc);
                _passengerPanel.SetMap(_map);
            }
            
            if (_driver != null)
            {
                _driverHome.Initialize(_driver, _driverCmd, _driverQuery, _tripQuery, _tripCmd, _walletService, _userService, _map, _notificationSvc);
            }
        }

        private void AttachEvents()
        {
            if (_driver != null) _driver.StatusChanged += OnDriverStatusChanged;
        }

        private void OnDriverStatusChanged(object sender, DriverStatusChangedEventArgs e)
            => UpdateDriverOnlineStatus();

        private void UpdateDriverOnlineStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateDriverOnlineStatus));
                return;
            }
            // Logic cập nhật UI nếu cần
        }

        private void StartOnlineDriverTimer()
        {
            _onlineDriverTimer = new System.Windows.Forms.Timer { Interval = 10000 };
            _onlineDriverTimer.Tick += async (s, e) => await UpdateOnlineDriverCount();
            _onlineDriverTimer.Start();
            _ = UpdateOnlineDriverCount();
        }

        private async Task UpdateOnlineDriverCount()
        {
            if (_driverQuery == null || this.IsDisposed) return;
            try
            {
                var onlineDrivers = await _driverQuery.GetOnlineDriversAsync();
                lblDriverCount.Text = $"{onlineDrivers.Count} tài xế online";
            }
            catch
            {
                lblDriverCount.Text = "? tài xế online";
            }
        }

        private void btnSwitchAcc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Có thể thêm logic logout hoặc chuyển về FrmAuth nếu cần
        }

        private void CleanupResources()
        {
            if (_driver != null) _driver.StatusChanged -= OnDriverStatusChanged;
            _onlineDriverTimer?.Stop();
            _onlineDriverTimer?.Dispose();
            _onlineDriverTimer = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CleanupResources();
            base.OnFormClosing(e);
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            if (_admin == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản Admin demo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var adminForm = new FrmAdmin(_admin, _userRepo, _tripRepo, _policyRepo, _reviewRepo, _adminService);
            adminForm.Show();
            this.Hide();
        }
    }
}
