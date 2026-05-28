using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    /// <summary>
    /// Hiển thị 4 thẻ thống kê tổng quan cho Admin:
    /// Tổng Users / Tổng Tài xế / Tổng Hành khách / Tổng Chuyến đi
    /// </summary>
    public partial class ucUserCount : UserControl
    {
        private IAdmSvc _adminService;
        private ITripQry _tripQuery;

        public ucUserCount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Liên kết dịch vụ và nạp số liệu lần đầu
        /// </summary>
        public void Initialize(IAdmSvc adminService, ITripQry tripQuery)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _tripQuery = tripQuery ?? throw new ArgumentNullException(nameof(tripQuery));
            _ = LoadStatsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadStatsAsync();
        }

        private async Task LoadStatsAsync()
        {
            if (_adminService == null || _tripQuery == null) return;

            try
            {
                var users = await _adminService.GetAllUsersAsync();
                var trips = await _adminService.GetAllTripsAsync();

                int total = 0;
                int drivers = 0;
                int passengers = 0;

                if (users != null)
                {
                    foreach (var u in users)
                    {
                        total++;
                        if (u is Drv) drivers++;
                        else if (u is Psg) passengers++;
                    }
                }

                int tripCount = trips == null ? 0 : trips.Count;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateLabels(total, drivers, passengers, tripCount)));
                }
                else
                {
                    UpdateLabels(total, drivers, passengers, tripCount);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ucUserCount] Lỗi tải thống kê: {ex.Message}");
            }
        }

        private void UpdateLabels(int total, int drivers, int passengers, int trips)
        {
            statTotal.SetData("👥 Tổng người dùng", total.ToString());
            statDrivers.SetData("🚗 Tài xế", drivers.ToString());
            statPassengers.SetData("🧍 Hành khách", passengers.ToString());
            statTrips.SetData("📋 Tổng chuyến đi", trips.ToString());
        }
    }
}