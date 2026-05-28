using System;
using System.Windows.Forms;
using GMap.NET;

namespace OOP2026
{
    public partial class FrmDriver : Form
    {
        // 1. Chỉ định nghĩa các Field Readonly để đảm bảo tính bất biến
        private readonly Drv _driver;
        private readonly IDrvCmd _driverCmd;
        private readonly IDrvQry _driverQuery;
        private readonly ITripQry _tripQuery;
        private readonly ITripCmd _tripCmd;
        private readonly IWalletSvc _walletService;
        private readonly IUsrSvc _userService;
        private readonly IUsrRepo _userRepo;
        private readonly IVehRepo _vehicleRepo;
        private readonly IMapSvc _mapService;
        private readonly INotificationSvc _notificationSvc;

        public FrmDriver(Drv d, IUsrSvc us, IDrvCmd dc, IDrvQry dq,
                          ITripQry tq, ITripCmd tc, IWalletSvc ws,
                          IMapSvc ms, IUsrRepo ur, IVehRepo vr,
                          INotificationSvc ns)
        {
            // Kiểm tra null chặt chẽ
            _driver = d ?? throw new ArgumentNullException(nameof(d));
            _userService = us ?? throw new ArgumentNullException(nameof(us));
            _driverCmd = dc ?? throw new ArgumentNullException(nameof(dc));
            _driverQuery = dq ?? throw new ArgumentNullException(nameof(dq));
            _tripQuery = tq ?? throw new ArgumentNullException(nameof(tq));
            _tripCmd = tc ?? throw new ArgumentNullException(nameof(tc));
            _walletService = ws ?? throw new ArgumentNullException(nameof(ws));
            _mapService = ms ?? throw new ArgumentNullException(nameof(ms));
            _userRepo = ur ?? throw new ArgumentNullException(nameof(ur));
            _vehicleRepo = vr ?? throw new ArgumentNullException(nameof(vr));
            _notificationSvc = ns ?? throw new ArgumentNullException(nameof(ns));

            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = $"OOP2026 - Tài xế {_driver.Name}";

            home.Initialize(_driver, _driverCmd, _driverQuery, _tripQuery, _tripCmd,
                                    _walletService, _userService, map, _notificationSvc);

            // map.SetPickup += OnMapLocationSelected; // Removed due to compile error
        }

        private async void OnMapLocationSelected(PointLatLng point)
        {
            if (_driver == null || _driverCmd == null || _mapService == null) return;

            try
            {
                var location = await _mapService.GetAddressAsync(point.Lat, point.Lng);
                if (location != null)
                {
                    await _driverCmd.UpdateLocationAsync(_driver.Id, location);
                    await home.RefreshAllAsync();
                    map.UpdateDriverLocation(new Loc(new Coord(point.Lat, point.Lng), new Addr("Vị trí hiện tại", "", "", "", "")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật vị trí: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tối ưu hóa việc dùng Form: Giải phóng tài nguyên an toàn
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Tắt trạng thái Online an toàn trước khi đóng ứng dụng
            if (_driver != null && _driver.IsOnline() && _driverCmd != null)
            {
                try
                {
                    await _driverCmd.GoOfflineAsync(_driver.Id);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi ngắt kết nối khi đóng Form: {ex.Message}");
                }
            }

            // Giải phóng Control để tránh rò rỉ RAM (Memory Leak)
            home?.Dispose();
            map?.Dispose();

            base.OnFormClosing(e);
        }
    }
}