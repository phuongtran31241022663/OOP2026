using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class FrmAdmin : Form
    {
        private readonly Adm _admin;
        private readonly IPolRepo _policyRepo;
        private readonly IAdmSvc _adminService;

        // Danh sách ucPolicyCard đang hiển thị để tái sử dụng Dispose đúng cách
        private readonly List<ucPolicyCard> _policyCards = new List<ucPolicyCard>();

        public FrmAdmin()
        {
            InitializeComponent();
        }

        public FrmAdmin(Adm admin, IUsrRepo userRepo, ITripRepo tripRepo,
                        IPolRepo policyRepo, IRevRepo reviewRepo, IAdmSvc adminService)
        {
            _admin = admin ?? throw new ArgumentNullException(nameof(admin));
            _policyRepo = policyRepo ?? throw new ArgumentNullException(nameof(policyRepo));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));

            InitializeComponent();

            // Hiển thị tên admin trên TopBar
            lblAdminTitle.Text = "RideGo — Quản trị hệ thống";
            lblAdminRole.Text = "Đăng nhập: " + _admin.Name;
        }


        private async void FrmAdmin_Load(object sender, EventArgs e)
        {
            // Nạp combobox loại xe cho tab Biểu phí
            cboVehicleType.Items.Clear();
            cboVehicleType.Items.Add("Xe máy (Moto)");
            cboVehicleType.Items.Add("Ô tô (Car)");
            cboVehicleType.SelectedIndex = 0;

            // Các giá trị giới hạn và mặc định đã được thiết lập trong Designer,
            // không cần gán lại ở đây để tránh trùng lặp.
            // (Nếu muốn ghi đè, bạn có thể giữ nguyên, nhưng tôi khuyến nghị giữ nguyên Designer)

            await LoadStatisticsAsync();
            await LoadPolicyCardsAsync();
        }


        private async Task LoadStatisticsAsync()
        {
            if (_adminService == null) return;

            try
            {
                var totalRev = await _adminService.GetTotalRevenueAsync();
                var totalCom = await _adminService.GetTotalCommissionAsync();
                var stats = await _adminService.GetTripStatisticsAsync();
                var users = await _adminService.GetAllUsersAsync();

                // Clear old cards
                pnlStatCardsRow.Controls.Clear();

                // Thẻ Tổng doanh thu (Xanh lá)
                AddStatCard(pnlStatCardsRow, "Tổng doanh thu", totalRev.ToString("N0") + "đ", "từ chuyến hoàn thành",
                    Color.FromArgb(236, 253, 245), Color.FromArgb(16, 185, 129),
                    Color.FromArgb(6, 78, 59), Color.FromArgb(52, 211, 153));

                // Thẻ Hoa hồng (Tím)
                AddStatCard(pnlStatCardsRow, "Hoa hồng", totalCom.ToString("N0") + "đ", "doanh thu RideGo",
                    Color.FromArgb(238, 242, 255), Color.FromArgb(99, 102, 241),
                    Color.FromArgb(67, 56, 202), Color.FromArgb(165, 180, 252));

                // Thẻ Tổng chuyến (Xanh dương)
                AddStatCard(pnlStatCardsRow, "Tổng chuyến", stats.Total.ToString(), $"{stats.Completed} HT · {stats.Cancelled} Hủy",
                    Color.FromArgb(239, 246, 255), Color.FromArgb(59, 130, 246),
                    Color.FromArgb(30, 64, 175), Color.FromArgb(147, 197, 253));

                // Thẻ Tỉ lệ hoàn thành (Vàng)
                AddStatCard(pnlStatCardsRow, "Tỉ lệ hoàn thành", $"{stats.CompletionRate:F1}%", $"{stats.Completed}/{stats.Total} chuyến",
                    Color.FromArgb(255, 251, 235), Color.FromArgb(217, 119, 6),
                    Color.FromArgb(146, 64, 14), Color.FromArgb(252, 211, 77));

                // Update User Stats
                int drivers = users.Count(u => u is Drv);
                int onlineDrivers = users.Count(u => u is Drv d && d.IsOnline());
                int passengers = users.Count(u => u is Psg);
                int admins = users.Count(u => u is Adm);
                ucAdminUser.SetData(drivers, onlineDrivers, passengers, admins);

                // Update Trip Status Stats
                ucActive.SetData(stats.Completed, stats.Cancelled, stats.Timeout, stats.Active);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp dữ liệu: {ex.Message}", "Lỗi Admin",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddStatCard(Control parent, string title, string value, string desc,
            Color back, Color titleC, Color valC, Color descC)
        {
            ucStatCard card = new ucStatCard();
            card.SetData(title, value, desc);
            card.SetTheme(back, titleC, valC, descC);
            card.Margin = new Padding(4);
            card.Dock = DockStyle.Fill;
            parent.Controls.Add(card);
        }


        private async Task LoadPolicyCardsAsync()
        {
            if (_policyRepo == null) return;

            try
            {
                // Xóa card cũ và giải phóng RAM
                flpPolicies.Controls.Clear();
                foreach (var old in _policyCards)
                {
                    old.Dispose();
                }
                _policyCards.Clear();

                List<Pol> policies = await _policyRepo.ReadAsync();
                if (policies == null) return;

                foreach (Pol pol in policies)
                {
                    if (pol == null) continue;
                    ucPolicyCard card = new ucPolicyCard();
                    card.SetPolicy(pol);
                    card.Margin = new Padding(8);
                    _policyCards.Add(card);
                    flpPolicies.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FrmAdmin] Lỗi nạp biểu phí: {ex.Message}");
            }
        }

        private async void BtnSavePolicy_Click(object sender, EventArgs e)
        {
            if (_policyRepo == null)
            {
                MessageBox.Show("Kho dữ liệu chính sách chưa được liên kết.", "Lỗi cấu hình",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            VehicleType vtype = (cboVehicleType.SelectedIndex == 0)
                                    ? VehicleType.Moto
                                    : VehicleType.Car;
            decimal basePrice = nudBasePrice.Value;
            decimal kmPrice = nudKmPrice.Value;
            decimal commRate = nudCommission.Value;

            if (basePrice <= 0 || kmPrice <= 0)
            {
                MessageBox.Show("Giá mở cửa và giá/km phải lớn hơn 0.", "Kiểm tra dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSavePolicy.Enabled = false;
            string originalText = btnSavePolicy.Text;
            btnSavePolicy.Text = "⌛ Đang lưu...";

            try
            {
                Pol newPol = new Pol(vtype, basePrice, kmPrice, commRate);
                await _policyRepo.CreateAsync(newPol);

                MessageBox.Show(
                    "Đã thêm biểu phí thành công!\n\n" +
                    "Loại xe: " + (vtype == VehicleType.Moto ? "Xe máy" : "Ô tô") + "\n" +
                    "Mở cửa: " + basePrice.ToString("N0") + "đ\n" +
                    "Giá/km: " + kmPrice.ToString("N0") + "đ\n" +
                    "Hoa hồng: " + commRate + "%",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadPolicyCardsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu biểu phí: {ex.Message}",
                    "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSavePolicy.Text = originalText;
                btnSavePolicy.Enabled = true;
            }
        }


        private void BtnNavStats_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            HighlightNavButton(btnNavStats);
            _ = LoadStatisticsAsync();
        }

        private void BtnNavUsers_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            HighlightNavButton(btnNavUsers);
            _ = LoadUsersAsync();
        }

        private void BtnNavTrips_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 2;
            HighlightNavButton(btnNavTrips);
            _ = LoadTripsAsync();
        }

        private void BtnNavFees_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 3;
            HighlightNavButton(btnNavFees);
            _ = LoadPolicyCardsAsync();
        }

        private async Task LoadUsersAsync()
        {
            if (_adminService == null) return;
            try
            {
                flpUsers.Controls.Clear();
                var users = await _adminService.GetAllUsersAsync();
                foreach (var user in users)
                {
                    var card = new UcUserCard();
                    card.SetUser(user);
                    card.Width = flpUsers.Width - 25;
                    flpUsers.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp người dùng: {ex.Message}");
            }
        }

        private async Task LoadTripsAsync()
        {
            if (_adminService == null) return;
            try
            {
                flpTrips.Controls.Clear();
                var trips = await _adminService.GetAllTripsAsync();
                foreach (var trip in trips)
                {
                    var card = new ucTripCard();
                    card.SetTrip(trip);
                    card.Width = flpTrips.Width - 25;
                    flpTrips.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nạp chuyến đi: {ex.Message}");
            }
        }

        private void HighlightNavButton(Button activeBtn)
        {
            Color activeColor = Color.FromArgb(80, 60, 236);
            Color inactiveColor = Color.FromArgb(156, 163, 175);

            btnNavStats.ForeColor = inactiveColor;
            btnNavUsers.ForeColor = inactiveColor;
            btnNavTrips.ForeColor = inactiveColor;
            btnNavFees.ForeColor = inactiveColor;

            activeBtn.ForeColor = activeColor;
        }

        // Nút đóng form (X) đã bị loại bỏ, không cần phương thức BtnCloseAdmin_Click

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Dọn sạch Policy cards trước khi đóng form
            for (int i = _policyCards.Count - 1; i >= 0; i--)
            {
                ucPolicyCard card = _policyCards[i];
                _policyCards.RemoveAt(i);
                card.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}