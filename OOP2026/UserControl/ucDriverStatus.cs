using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucDriverStatus : UserControl
    {
        private Drv _driver;
        private IDrvCmd _driverCmd;
        private IDrvQry _driverQuery;
        private ITripQry _tripQuery;
        private IWalletSvc _walletService;

        private System.Windows.Forms.Timer _refreshTimer;
        private bool _isRefreshing;

        public ucDriverStatus()
        {
            InitializeComponent();
            this.Disposed += UcDriverStatus_Disposed;
        }

        public void Initialize(
            Drv driver,
            IDrvCmd driverCmd,
            IDrvQry driverQuery,
            ITripQry tripQuery,
            IWalletSvc walletService)
        {
            if (_driver != null)
            {
                _driver.StatusChanged -= OnDriverStatusChanged;
            }
            if (_walletService != null)
            {
                _walletService.WalletChanged -= OnWalletChangedGlobal;
            }

            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _driverCmd = driverCmd ?? throw new ArgumentNullException(nameof(driverCmd));
            _driverQuery = driverQuery ?? throw new ArgumentNullException(nameof(driverQuery));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));

            _driver.StatusChanged += OnDriverStatusChanged;
            _walletService.WalletChanged += OnWalletChangedGlobal;

            if (_refreshTimer == null)
            {
                _refreshTimer = new System.Windows.Forms.Timer { Interval = 5000 };
                _refreshTimer.Tick += OnRefreshTimerTick;
            }

            _refreshTimer.Stop();
            _refreshTimer.Start();

            _ = RefreshStatusAsync();
        }

        private void OnWalletChangedGlobal(object sender, WalletChangedEventArgs e)
        {
            if (e.DriverId != _driver.Id) return;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => OnWalletChangedGlobal(sender, e)));
                return;
            }
            _ = RefreshStatusAsync();
        }

        private void OnDriverStatusChanged(object sender, DriverStatusChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => OnDriverStatusChanged(sender, e)));
                return;
            }

            if (!IsHandleCreated || this.IsDisposed) return;

            _ = RefreshStatusAsync();
        }

        private async void OnRefreshTimerTick(object sender, EventArgs e)
        {
            if (!IsHandleCreated || this.IsDisposed) return;

            // TỐI ƯU CHỐNG RACE CONDITION: Tạm dừng Timer để luồng cũ chạy xong dứt điểm
            _refreshTimer.Stop();

            await RefreshStatusAsync();

            // Chỉ kích hoạt lại bộ đếm khi dữ liệu đã được nạp xong an toàn
            if (!this.IsDisposed && _refreshTimer != null)
            {
                _refreshTimer.Start();
            }
        }

        public async Task RefreshStatusAsync()
        {
            if (_isRefreshing || this.IsDisposed) return;
            _isRefreshing = true;

            try
            {
                var freshDriver = await _driverQuery.GetDriverByIdAsync(_driver.Id);
                if (freshDriver != null)
                {
                    _driver = freshDriver;
                }

                bool isOnline = (_driver.Status == DriverStatus.Online);
                btnToggleStatus.Text = isOnline ? "Đang nhận cuốc" : "Đang tắt nhận cuốc";
                btnToggleStatus.BackColor = isOnline ? Colors.LightYellow : Colors.Gray;
                btnToggleStatus.ForeColor = isOnline ? Colors.Black : Colors.White;

                decimal wallet = await _walletService.GetWalletAsync(_driver.Id);
                decimal income = await _walletService.GetIncomeAsync(_driver.Id);

                lblWalletBalance.Text = $"{wallet:N0}đ";
                lblTodayIncome.Text = $"{income:N0}đ";

                if (_driver.VehId != Guid.Empty)
                {
                    var vehicle = await _driverQuery.GetVehicleByIdAsync(_driver.VehId);
                    if (vehicle != null)
                    {
                        lblVehicleName.Text = $"{vehicle.Brand} {vehicle.Model}";
                        lblVehiclePlate.Text = $"Biển số: {vehicle.Plate}";
                        lblVehicleColor.Text = $"Màu sắc: {vehicle.Color}";
                        lblVehicleType.Text = $"Loại xe: {(vehicle.Type == VehicleType.Car ? "Ô tô" : "Xe máy")}";
                    }
                    else
                    {
                        SetVehicleEmptyFields("Không tìm thấy phương tiện");
                    }
                }
                else
                {
                    SetVehicleEmptyFields("Chưa có phương tiện");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucDriverStatus] Refresh error: {ex.Message}");
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void SetVehicleEmptyFields(string message)
        {
            lblVehicleName.Text = message;
            lblVehiclePlate.Text = "Biển số: -";
            lblVehicleColor.Text = "Màu sắc: -";
            lblVehicleType.Text = "Loại xe: -";
        }

        private async void BtnToggleStatus_Click(object sender, EventArgs e)
        {
            if (_driverCmd == null || _isRefreshing) return;

            try
            {
                if (_driver.IsOnline())
                {
                    await _driverCmd.GoOfflineAsync(_driver.Id);
                }
                else
                {
                    await _driverCmd.GoOnlineAsync(_driver.Id);
                }

                await RefreshStatusAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thay đổi trạng thái thất bại: {ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAllResources()
        {
            if (_driver != null)
            {
                _driver.StatusChanged -= OnDriverStatusChanged;
            }
            if (_walletService != null)
            {
                _walletService.WalletChanged -= OnWalletChangedGlobal;
            }

            if (_refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Tick -= OnRefreshTimerTick;
                _refreshTimer.Dispose();
                _refreshTimer = null;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClearAllResources();
            base.OnHandleDestroyed(e);
        }

        private void UcDriverStatus_Disposed(object sender, EventArgs e)
        {
            ClearAllResources();
        }
    }
}
