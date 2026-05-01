using Application.Interfaces;
using Domain.Entities.Users;
using Presentation.Shells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    public partial class UcAdmin : BaseUserControl
    {
        private readonly Admin _admin;
        private readonly IAdminService _adminService;

        private List<Domain.Entities.User> _allUsers = new List<Domain.Entities.User>();
        private List<Domain.Entities.Trip> _allTrips = new List<Domain.Entities.Trip>();
        private List<Domain.Entities.FareRule> _allFareRules = new List<Domain.Entities.FareRule>();

        public event EventHandler RequestLogout;

        public UcAdmin(Admin admin, IAdminService adminService)
        {
            _admin = admin ?? throw new ArgumentNullException(nameof(admin));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnRefresh.Click += async (s, e) => await LoadAllData();
            btnLogout.Click += (s, e) => RequestLogout?.Invoke(this, e);
            tabControlAdmin.SelectedIndexChanged += async (s, e) => await OnTabChanged();

            // Users tab
            txtSearchUsers.TextChanged += (s, e) => FilterUsers(txtSearchUsers.Text);
            btnLockUser.Click += async (s, e) => await ToggleUserLock(true);
            btnUnlockUser.Click += async (s, e) => await ToggleUserLock(false);

            // Trips tab
            txtSearchTrips.TextChanged += (s, e) => FilterTrips(txtSearchTrips.Text);
            btnCancelTrip.Click += async (s, e) => await CancelSelectedTrip();
            btnTripDetail.Click += (s, e) => ShowTripDetail();

            // FareRules tab
            btnAddFare.Click += (s, e) => ShowFareEditDialog(null);
            btnEditFare.Click += (s, e) => ShowFareEditDialog(GetSelectedFareRule());
            btnDeleteFare.Click += async (s, e) => await DeleteSelectedFareRule();

            this.Load += async (s, e) => await LoadAllData();
        }

        private async Task OnTabChanged()
        {
            switch (tabControlAdmin.SelectedIndex)
            {
                case 0: PopulateUsersGrid(); break;
                case 1: PopulateTripsGrid(); break;
                case 2: PopulateFareRulesGrid(); break;
                case 3: await UpdateStatsAsync(); break;
            }
        }

        private async Task LoadAllData()
        {
            IsLoading = true;
            try
            {
                _allUsers = await _adminService.GetAllUsersAsync() ?? new List<Domain.Entities.User>();
                _allTrips = await _adminService.GetAllTripsAsync() ?? new List<Domain.Entities.Trip>();
                _allFareRules = await _adminService.GetFareRulesAsync() ?? new List<Domain.Entities.FareRule>();
                await OnTabChanged();
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Tải dữ liệu quản trị");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // --- Users Tab ---
        private void PopulateUsersGrid()
        {
            dgvUsers.Rows.Clear();
            if (_allUsers == null) return;
            foreach (var user in _allUsers)
            {
                string role = user is Driver ? "Tài xế" : (user is Passenger ? "Hành khách" : "Quản trị viên");
                dgvUsers.Rows.Add(user.Id.ToString().Substring(0, 8), user.Name, user.Phone, role, "Hoạt động");
            }
        }

        private void FilterUsers(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                PopulateUsersGrid();
                return;
            }

            var filteredUsers = new List<Domain.Entities.User>();
            foreach (var u in _allUsers)
            {
                if (u.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    u.Phone.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredUsers.Add(u);
                }
            }

            dgvUsers.Rows.Clear();
            foreach (var user in filteredUsers)
            {
                string role = user is Driver ? "Tài xế" : (user is Passenger ? "Hành khách" : "Quản trị viên");
                dgvUsers.Rows.Add(user.Id.ToString().Substring(0, 8), user.Name, user.Phone, role, "Hoạt động");
            }
        }

        private async Task ToggleUserLock(bool lockUser)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            IsLoading = true;
            try
            {
                await Task.CompletedTask; // TODO: goi service khoa/mo khoa user
                ShowInfo(lockUser ? "Đã khoá tài khoản." : "Đã mở khoá tài khoản.");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cập nhật trạng thái tài khoản");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // --- Trips Tab ---
        private void PopulateTripsGrid()
        {
            dgvTrips.Rows.Clear();
            if (_allTrips == null) return;
            foreach (var trip in _allTrips)
            {
                int rowIndex = dgvTrips.Rows.Add(
                    trip.Id.ToString().Substring(0, 8),
                    trip.Status,
                    trip.TripVehicleType,
                    (trip.TripFare?.TotalAmount.Amount.ToString("N0") ?? "N/A") + "đ",
                    trip.RequestAt.ToString("dd/MM HH:mm"));
                dgvTrips.Rows[rowIndex].Tag = trip;
            }
        }
        
        private void FilterTrips(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                PopulateTripsGrid();
                return;
            }
            
            var filteredTrips = new List<Domain.Entities.Trip>();
            foreach (var t in _allTrips)
            {
                if (t.Id.ToString().IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTrips.Add(t);
                }
            }
                
            dgvTrips.Rows.Clear();
            foreach (var trip in filteredTrips)
            {
                int rowIndex = dgvTrips.Rows.Add(
                    trip.Id.ToString().Substring(0, 8), 
                    trip.Status, 
                    trip.TripVehicleType, 
                    (trip.TripFare?.TotalAmount.Amount.ToString("N0") ?? "N/A") + "đ", 
                    trip.RequestAt.ToString("dd/MM HH:mm"));
                dgvTrips.Rows[rowIndex].Tag = trip;
            }
        }

        private async Task CancelSelectedTrip()
        {
            if (dgvTrips.SelectedRows.Count == 0) return;
            if (!Confirm("Huỷ chuyến đi đã chọn?")) return;

            IsLoading = true;
            try
            {
                await Task.CompletedTask; // TODO: goi service huy chuyen
                ShowInfo("Đã huỷ chuyến đi.");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Huỷ chuyến đi");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ShowTripDetail()
        {
            ExecuteWithHandling("Mở chi tiết chuyến đi", () =>
            {
                if (dgvTrips.SelectedRows.Count == 0)
                {
                    throw new InvalidOperationException("Vui lòng chọn chuyến đi cần xem.");
                }
                
                var trip = dgvTrips.SelectedRows[0].Tag as Domain.Entities.Trip;
                if (trip == null)
                {
                    throw new InvalidOperationException("Không thể xác định chuyến đi.");
                }

                var ucDetail = new UcTripDetail(trip);
                Form parentForm = this.FindForm();
                // Thay thế FrmModal bằng ShowInfo theo yêu cầu
                ShowInfo($"Đang hiển thị: Chi tiết chuyến đi (ID: {trip.Id})");
                // Ghi chú: MessageBox không thể hiển thị UserControl, 
                // cần giải pháp thay thế cho UcTripDetail nếu muốn xem chi tiết đầy đủ.
            });
        }

        // --- FareRules Tab ---
        private void PopulateFareRulesGrid()
        {
            dgvFareRules.Rows.Clear();
            if (_allFareRules == null) return;
            foreach (var rule in _allFareRules)
            {
                int rowIndex = dgvFareRules.Rows.Add(
                    rule.VehicleType, 
                    rule.BaseFare.Amount.ToString("N0"), 
                    rule.PricePerKm.Amount.ToString("N0"), 
                    (rule.CommissionRate * 100).ToString("F0") + "%");
                dgvFareRules.Rows[rowIndex].Tag = rule;
            }
        }

        private Domain.Entities.FareRule GetSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return null;
            return dgvFareRules.SelectedRows[0].Tag as Domain.Entities.FareRule;
        }

        private void ShowFareEditDialog(Domain.Entities.FareRule rule)
        {
            ShowInfo(rule == null ? "Thêm mới quy tắc giá." : "Sửa quy tắc giá.");
        }

        private async Task DeleteSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return;
            if (!Confirm("Xoá quy tắc giá đã chọn?")) return;

            IsLoading = true;
            try
            {
                await Task.CompletedTask; // TODO: goi service xoa quy tac gia
                ShowInfo("Đã xoá quy tắc giá.");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Xoá quy tắc giá");
            }
            finally
            {
                IsLoading = false;
            }
        }

        // --- Stats Tab ---
        private async System.Threading.Tasks.Task UpdateStatsAsync()
        {
            IsLoading = true;
            try
            {
                lblGMV.Text = "Tổng doanh thu: " + (await _adminService.GetTotalGMVAsync()).ToString("N0") + "đ";
                lblTotalTrips.Text = "Tổng chuyến: " + _allTrips.Count.ToString();

                int activeCount = 0;
                foreach (Domain.Entities.User u in _allUsers)
                {
                    if (u is Driver d && d.IsAvailable())
                        activeCount++;
                }
                lblActiveDrivers.Text = "Tài xế hoạt động: " + activeCount.ToString();

                lblAvgRating.Text = "Điểm hài lòng: " + (await _adminService.GetAverageSatisfactionAsync()).ToString("F1");
                lblCompletionRate.Text = "Tỷ lệ hoàn thành: " + (await _adminService.GetCompletionRateAsync() * 100).ToString("F0") + "%";
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cập nhật thống kê");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

