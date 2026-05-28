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
        private INotificationSvc _notificationSvc;

        private ucDriverStatus _statusControl;
        private ucRequest _requestControl;
        private ucWallet _walletControl;
        private ucHistory _historyControl;
        private ucProfile _profileControl;

        private Control _currentControl;
        private System.Windows.Forms.Timer _refreshRequestsTimer;

        public ucDriverHome()
        {
            InitializeComponent();
            InitializeChildControls();

            // Đăng ký sự kiện hủy hệ thống để giải phóng RAM triệt để
            this.Disposed += UcDriverHome_Disposed;
        }

        private void InitializeChildControls()
        {
            _statusControl = new ucDriverStatus();
            _requestControl = new ucRequest();
            _walletControl = new ucWallet();
            _historyControl = new ucHistory();
            _profileControl = new ucProfile();

            _requestControl.Dock = DockStyle.Fill;
            _walletControl.Dock = DockStyle.Fill;
            _historyControl.Dock = DockStyle.Fill;
            _profileControl.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Khởi tạo màn hình chính tài xế - SỬA CHÀO MỪNG: Khử hoàn toàn IUsrRepo và IVehRepo
        /// </summary>
        public void Initialize(Drv d, IDrvCmd dc, IDrvQry dq,
                                 ITripQry tq, ITripCmd tc, IWalletSvc ws,
                                 IUsrSvc us, ucMap map, INotificationSvc notificationSvc)
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

            // Đăng ký sự kiện thay đổi trạng thái từ lõi Domain
            _driver.StatusChanged += OnDriverStatusChanged;

            // Thiết lập đồng bộ hẹn giờ quét cuốc xe mới định kỳ 5 giây
            _refreshRequestsTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            _refreshRequestsTimer.Tick += OnRefreshRequestsTick;
            _refreshRequestsTimer.Start();

            ShowTab("Requests");
        }

        private void InitializeDependencies()
        {
            // ĐỒNG BỘ KIẾN TRÚC: Gọi hàm nạp ucDriverStatus mới đã lược bỏ 2 Repository
            _statusControl.Initialize(_driver, _driverCmd, _driverQuery, _tripQuery, _walletService);

            _requestControl.Initialize(_driver, _driverCmd, _tripCmd, _tripQuery, _notificationSvc);
            _walletControl.Initialize(_driver, _walletService, _driverQuery, _tripQuery);
            _historyControl.InitializeDriver(_driver, _tripQuery);
            _profileControl.Initialize(_driver, _userService);

            // KHỬ LAMBDA: Thay thế bằng hàm đăng ký sự kiện tường minh để chặn dừng lại Task Leak
            _requestControl.TripAccepted += OnDriverTripAcceptedEvent;

            if (tlpDriverContent != null)
            {
                tlpDriverContent.Controls.Clear();
                tlpDriverContent.Controls.Add(_requestControl, 0, 0);
                tlpDriverContent.Controls.Add(_walletControl, 0, 1);
            }
        }

        private async void OnDriverTripAcceptedEvent(object? sender, Trip e)
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
            if (_currentControl == tlpDriverContent && _requestControl.Visible)
            {
                await RefreshPendingRequestsAsync();
            }
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
                System.Diagnostics.Debug.WriteLine($"Refresh pending error: {ex.Message}");
            }
        }

        private void OnDriverStatusChanged(object sender, DriverStatusChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatusUI()));
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
            lblStats.Text = $"⭐ {_driver.AvgRat:F1} • {_driver.TotTrip} chuyến";
            UpdateStatusUI();
        }

        private async void lblStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (_driver == null || _driverCmd == null) return;
            bool requestedOnline = !_driver.IsOnline();

            try
            {
                lblStatus.Enabled = false;

                if (_driver.IsOnline())
                    await _driverCmd.GoOfflineAsync(_driver.Id);
                else
                    await _driverCmd.GoOnlineAsync(_driver.Id);

                // SỬA CHÀO MỪNG: Dùng DrvQry thay thế hoàn toàn cho UsrRepo
                var fresh = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                if (fresh != null)
                {
                    _driver = fresh;
                }

                BindDriverData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật trạng thái hoạt động thất bại: {ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusUI();
            }
            finally
            {
                lblStatus.Enabled = true;
            }
        }

        private void Tab_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag == null) return;
            ShowTab(btn.Tag.ToString());
        }

        public void ShowTab(string tag)
        {
            if (flpTabs?.Controls != null)
            {
                // KHỬ LINQ: Duyệt vòng lặp for/foreach truyền thống quản lý kiểu nút bấm
                foreach (Control ctrl in flpTabs.Controls)
                {
                    if (ctrl is Button btn && btn.Tag != null)
                    {
                        bool isSelected = btn.Tag.ToString() == tag;
                        btn.BackColor = isSelected ? Colors.Orange : Color.White;
                        btn.ForeColor = isSelected ? Color.White : Color.Gray;
                    }
                }
            }

            // FIX UI OVERLAP: Clear all controls from pnlContent to prevent overlapping
            if (pnlContent != null)
            {
                pnlContent.Controls.Clear();
            }

            // Hide tlpDriverContent by default
            if (tlpDriverContent != null)
            {
                tlpDriverContent.Visible = false;
                // Also remove it from pnlContent if it was added there
                if (pnlContent?.Controls.Contains(tlpDriverContent) == true)
                {
                    pnlContent.Controls.Remove(tlpDriverContent);
                }
            }

            // Điều phối nạp dữ liệu tức thời theo ngữ cảnh Tab được chọn
            switch (tag)
            {
                case "Requests":
                    _currentControl = tlpDriverContent;
                    if (tlpDriverContent != null) tlpDriverContent.Visible = true;
                    _ = RefreshPendingRequestsAsync();
                    _ = _walletControl.RefreshWalletAsync();
                    break;
                case "Wallet":
                    _currentControl = _walletControl;
                    _ = _walletControl.RefreshWalletAsync();
                    break;
                case "History":
                    _currentControl = _historyControl;
                    _ = _historyControl.RefreshAsync();
                    break;
                case "Profile":
                    _currentControl = _profileControl;
                    _ = _profileControl.RefreshProfileAsync();
                    break;
                default:
                    return;
            }

            if (_currentControl != null && !pnlContent.Controls.Contains(_currentControl))
            {
                _currentControl.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(_currentControl);
                _currentControl.BringToFront();
            }
        }

        /// <summary>
        /// Đồng bộ hàng loạt dữ liệu của toàn bộ hệ thống màn hình con tài xế
        /// </summary>
        public async Task RefreshAllAsync()
        {
            if (_driver == null || _driverQuery == null || this.IsDisposed) return;

            try
            {
                // SỬA CHÀO MỪNG: Sử dụng dịch vụ truy vấn DriverQuery thay cho Repo gốc
                var fresh = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                if (fresh != null)
                {
                    _driver = fresh;
                }

                BindDriverData();

                // Chuỗi tác vụ nạp bất đồng bộ an toàn
                if (_statusControl != null) await _statusControl.RefreshStatusAsync();
                if (_requestControl != null) await _requestControl.RefreshAsync();
                if (_walletControl != null) await _walletControl.RefreshWalletAsync();
                if (_historyControl != null) await _historyControl.RefreshAsync();
                if (_profileControl != null) await _profileControl.RefreshProfileAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucDriverHome] Toàn cục refresh lỗi: {ex.Message}");
            }
        }

        // ========== BỘ TIÊU HỦY CHỦ ĐỘNG KHỬ HOÀN TOÀN RÒ RỈ RAM ==========

        private void CleanUpAllResources()
        {
            // Hủy toàn bộ liên kết sự kiện để dọn sạch con trỏ chết
            if (_driver != null)
            {
                _driver.StatusChanged -= OnDriverStatusChanged;
            }

            if (_requestControl != null)
            {
                _requestControl.TripAccepted -= OnDriverTripAcceptedEvent;
            }

            if (_refreshRequestsTimer != null)
            {
                _refreshRequestsTimer.Stop();
                _refreshRequestsTimer.Tick -= OnRefreshRequestsTick;
                _refreshRequestsTimer.Dispose();
                _refreshRequestsTimer = null;
            }

            // Kích hoạt giải phóng vùng nhớ đồ họa của 5 màn hình con
            _statusControl?.Dispose();
            _requestControl?.Dispose();
            _walletControl?.Dispose();
            _historyControl?.Dispose();
            _profileControl?.Dispose();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            CleanUpAllResources();
            base.OnHandleDestroyed(e);
        }

        private void UcDriverHome_Disposed(object? sender, EventArgs e)
        {
            CleanUpAllResources();
        }
    }
}
