using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucDriverHome : UserControl
    {
        private Drv _driver;
        private IDrvCmd _driverCmd;
        private IDrvQry _driverQuery;
        private ITripQry _tripQuery;
        private ITripCmd _tripCmd;
        private IWalletSvc _walletService;
        private IUsrSvc _userService;
        private ucMap _map;
        private INotiSvc _notificationSvc;

        private ucDriverStatus _statusControl;
        private ucRequest _requestControl;
        private ucWallet _walletControl;
        private ucHistory _historyControl;
        private ucProfile _profileControl;

        private System.Windows.Forms.Timer _refreshRequestsTimer;

        public ucDriverHome()
        {
            InitializeComponent();
            InitializeChildControls();
            this.Disposed += UcDriverHome_Disposed;
        }

        private void InitializeChildControls()
        {
            _statusControl = new ucDriverStatus();
            _requestControl = new ucRequest();
            _walletControl = new ucWallet();
            _historyControl = new ucHistory();
            _profileControl = new ucProfile();

            _statusControl.Dock = DockStyle.Fill;
            _requestControl.Dock = DockStyle.Fill;
            _walletControl.Dock = DockStyle.Fill;
            _historyControl.Dock = DockStyle.Fill;
            _profileControl.Dock = DockStyle.Fill;

            tabStatus.Controls.Add(_statusControl);
            tabRequests.Controls.Add(_requestControl);
            tabWallet.Controls.Add(_walletControl);
            tabHistory.Controls.Add(_historyControl);
            tabProfile.Controls.Add(_profileControl);
        }

        // ĐÃ SỬA: tham số INotiSvc → INotificationSvc
        public void Initialize(Drv d, IDrvCmd dc, IDrvQry dq,
                               ITripQry tq, ITripCmd tc, IWalletSvc ws,
                               IUsrSvc us, ucMap map, INotiSvc notificationSvc)
        {
            _driver = d ?? throw new ArgumentNullException(nameof(d));
            _driverCmd = dc ?? throw new ArgumentNullException(nameof(dc));
            _driverQuery = dq ?? throw new ArgumentNullException(nameof(dq));
            _tripQuery = tq ?? throw new ArgumentNullException(nameof(tq));
            _tripCmd = tc ?? throw new ArgumentNullException(nameof(tc));
            _walletService = ws ?? throw new ArgumentNullException(nameof(ws));
            _userService = us ?? throw new ArgumentNullException(nameof(us));
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            BindDriverData();
            InitializeDependencies();

            _driver.StatusChanged += OnDriverStatusChanged;
            //if (_reviewService != null) _reviewService.TripReviewed += OnTripReviewedGlobal;

            _refreshRequestsTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            _refreshRequestsTimer.Tick += OnRefreshRequestsTick;
            _refreshRequestsTimer.Start();

            ShowTab("Requests");
        }

        private void InitializeDependencies()
        {
            _statusControl.Initialize(_driver, _driverCmd, _driverQuery, _tripQuery, _walletService);
            _requestControl.Initialize(_driver, _driverCmd, _tripCmd, _tripQuery, _notificationSvc);
            _walletControl.Initialize(_driver, _walletService, _driverQuery, _tripQuery);
            _historyControl.InitializeDriver(_driver, _tripQuery);
            _profileControl.Initialize(_driver, _userService);

            _requestControl.TripAccepted += OnDriverTripAcceptedEvent;
        }

        private async void OnDriverTripAcceptedEvent(object sender, Trip e)
        {
            try
            {
                await RefreshAllAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi làm mới khi tài xế nhận cuốc: {ex.Message}");
            }
        }

        private async void OnRefreshRequestsTick(object sender, EventArgs e)
        {
            if (tab.SelectedTab == tabRequests)
                await RefreshPendingRequestsAsync();
        }

        private async Task RefreshPendingRequestsAsync()
        {
            if (_driver == null || _tripQuery == null || this.IsDisposed) return;
            try
            {
                var pendingTrips = await _tripQuery.GetTripsWithPendingDriverAsync(_driver.Id);
                _requestControl.UpdateRequests(pendingTrips);
                await _requestControl.LoadActiveTripFromService();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucDriverHome] Refresh pending error: {ex.Message}");
            }
        }

        private void OnDriverStatusChanged(object sender, DriverStatusChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateStatusUI));
                return;
            }
            UpdateStatusUI();
        }

        private void UpdateStatusUI()
        {
            if (_driver == null) return;
            bool isOnline = _driver.IsOnline();
            lblStatus.BackColor = isOnline ? Colors.Green : Colors.Gray;
            lblStatus.Text = isOnline ? "Trực tuyến" : "Ngoại tuyến";
        }

        private void BindDriverData()
        {
            if (_driver == null) return;
            lblName.Text = _driver.Name;
            lblPhone.Text = _driver.Phone;
            lblStats.Text = $"⭐ {_driver.AvgRat:F1}  •  {_driver.TotTrip} chuyến";
            UpdateStatusUI();
        }

        // ── Click nhãn Trạng thái → bật/tắt Online ────────────────────────
        private async void lblStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_driver == null || _driverCmd == null) return;
            try
            {
                lblStatus.Enabled = false;
                if (_driver.IsOnline())
                    await _driverCmd.GoOfflineAsync(_driver.Id);
                else
                    await _driverCmd.GoOnlineAsync(_driver.Id);

                var fresh = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                if (fresh != null) _driver = fresh;
                BindDriverData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật trạng thái thất bại: {ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusUI();
            }
            finally
            {
                lblStatus.Enabled = true;
            }
        }

        // ── Điều hướng Tab ─────────────────────────────────────────────────
        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selected = tab.SelectedTab;
            if (selected == tabStatus)
                _ = _statusControl.RefreshStatusAsync();
            else if (selected == tabRequests)
                _ = RefreshPendingRequestsAsync();
            else if (selected == tabWallet)
                _ = _walletControl.RefreshWalletAsync();
            else if (selected == tabHistory)
                _ = _historyControl.RefreshAsync();
            else if (selected == tabProfile)
                _ = _profileControl.RefreshProfileAsync();
        }

        public void ShowTab(string tag)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowTab(tag)));
                return;
            }

            switch (tag)
            {
                case "Status":
                    tab.SelectedTab = tabStatus;
                    break;
                case "Requests":
                    tab.SelectedTab = tabRequests;
                    break;
                case "Wallet":
                    tab.SelectedTab = tabWallet;
                    break;
                case "History":
                    tab.SelectedTab = tabHistory;
                    break;
                case "Profile":
                    tab.SelectedTab = tabProfile;
                    break;
            }
        }

        public async Task RefreshAllAsync()
        {
            if (_driver == null || _driverQuery == null || this.IsDisposed) return;
            try
            {
                var fresh = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                if (fresh != null) _driver = fresh;
                BindDriverData();

                if (_statusControl != null) await _statusControl.RefreshStatusAsync();
                if (_requestControl != null) await _requestControl.RefreshAsync();
                if (_walletControl != null) await _walletControl.RefreshWalletAsync();
                if (_historyControl != null) await _historyControl.RefreshAsync();
                if (_profileControl != null) await _profileControl.RefreshProfileAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucDriverHome] RefreshAll error: {ex.Message}");
            }
        }

        // ── Dọn dẹp RAM ───────────────────────────────────────────────────
        private void CleanUpAllResources()
        {
            if (_driver != null)
                _driver.StatusChanged -= OnDriverStatusChanged;

            if (_requestControl != null)
                _requestControl.TripAccepted -= OnDriverTripAcceptedEvent;

            if (_refreshRequestsTimer != null)
            {
                _refreshRequestsTimer.Stop();
                _refreshRequestsTimer.Tick -= OnRefreshRequestsTick;
                _refreshRequestsTimer.Dispose();
                _refreshRequestsTimer = null;
            }

            if (_statusControl != null) { _statusControl.Dispose(); _statusControl = null; }
            if (_requestControl != null) { _requestControl.Dispose(); _requestControl = null; }
            if (_walletControl != null) { _walletControl.Dispose(); _walletControl = null; }
            if (_historyControl != null) { _historyControl.Dispose(); _historyControl = null; }
            if (_profileControl != null) { _profileControl.Dispose(); _profileControl = null; }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            CleanUpAllResources();
            base.OnHandleDestroyed(e);
        }

        private void UcDriverHome_Disposed(object sender, EventArgs e)
        {
            CleanUpAllResources();
        }
    }
}