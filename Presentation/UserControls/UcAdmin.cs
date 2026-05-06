using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Presentation.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    public partial class UcAdmin : BaseUserControl
    {
        private readonly Admin _admin;
        private readonly IAdminService _adminService;


        private List<User> _allUsers = new List<User>();
        private List<Trip> _allTrips = new List<Trip>();
        private List<FareRule> _allFareRules = new List<FareRule>();

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

            // Trips tab
            txtSearchTrips.TextChanged += (s, e) => FilterTrips(txtSearchTrips.Text);
            btnCancelTrip.Click += async (s, e) => await CancelSelectedTrip();
            btnTripDetail.Click += (s, e) => ShowTripDetailUnavailable();

            // FareRules tab
            btnAddFare.Click += async (s, e) => await AddFareRuleAsync();
            btnEditFare.Click += async (s, e) => await EditSelectedFareRuleAsync();
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
                _allUsers = await _adminService.GetAllUsersAsync() ?? new List<User>();
                _allTrips = await _adminService.GetAllTripsAsync() ?? new List<Trip>();
                _allFareRules = await _adminService.GetFareRulesAsync() ?? new List<FareRule>();
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

            // Fix [High 3]: Explicit type instead of var
            List<User> filteredUsers = new List<User>();
            foreach (User u in _allUsers)
            {
                if (u.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    u.Phone.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredUsers.Add(u);
                }
            }

            dgvUsers.Rows.Clear();
            foreach (User user in filteredUsers)
            {
                string role = user is Driver ? "Tài xế" : (user is Passenger ? "Hành khách" : "Quản trị viên");
                dgvUsers.Rows.Add(user.Id.ToString().Substring(0, 8), user.Name, user.Phone, role, "Hoạt động");
            }
        }


        // Removed ToggleUserLock method


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

            // Fix [High 3]: Explicit type instead of var
            List<Trip> filteredTrips = new List<Trip>();
            foreach (Trip t in _allTrips)
            {
                if (t.Id.ToString().IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTrips.Add(t);
                }
            }

            dgvTrips.Rows.Clear();
            foreach (Trip trip in filteredTrips)
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

            // Fix [High 3]: Explicit type instead of var
            Trip trip = dgvTrips.SelectedRows[0].Tag as Trip;
            if (trip == null)
            {
                ShowInfo("Không thể xác định chuyến đi.");
                return;
            }

            if (!Confirm("Huỷ chuyến đi đã chọn?")) return;

            IsLoading = true;
            try
            {
                await _adminService.CancelTripAsync(trip.Id, "Admin cancelled trip");
                _allTrips = await _adminService.GetAllTripsAsync() ?? new List<Trip>();
                PopulateTripsGrid();
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

        private void ShowTripDetailUnavailable()
        {
            ShowInfo("Chức năng chi tiết chuyến đi đã bị gỡ.");
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

        private FareRule GetSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return null;
            return dgvFareRules.SelectedRows[0].Tag as FareRule;
        }

        private async Task AddFareRuleAsync()
        {
            FareRuleInputResult input = ShowFareRuleInputDialog(null);
            if (input == null) return;

            IsLoading = true;
            try
            {
                await _adminService.UpdateFareRuleAsync(
                    input.VehicleType,
                    new Money(input.BaseFare),
                    new Money(input.PricePerKm),
                    input.CommissionRate);

                _allFareRules = await _adminService.GetFareRulesAsync() ?? new List<FareRule>();
                PopulateFareRulesGrid();
                ShowInfo("Đã thêm quy tắc giá.");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Thêm quy tắc giá");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task EditSelectedFareRuleAsync()
        {
            FareRule selectedRule = GetSelectedFareRule();
            if (selectedRule == null)
            {
                ShowInfo("Vui lòng chọn quy tắc giá cần sửa.");
                return;
            }

            FareRuleInputResult input = ShowFareRuleInputDialog(selectedRule);
            if (input == null) return;

            IsLoading = true;
            try
            {
                await _adminService.UpdateFareRuleAsync(
                    input.VehicleType,
                    new Money(input.BaseFare),
                    new Money(input.PricePerKm),
                    input.CommissionRate);

                _allFareRules = await _adminService.GetFareRulesAsync() ?? new List<FareRule>();
                PopulateFareRulesGrid();
                ShowInfo("Đã cập nhật quy tắc giá.");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Cập nhật quy tắc giá");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private FareRuleInputResult ShowFareRuleInputDialog(FareRule existingRule)
        {
            using (Form dialog = new Form())
            using (TableLayoutPanel layout = new TableLayoutPanel())
            using (Label lblVehicleType = new Label())
            using (ComboBox cmbVehicleType = new ComboBox())
            using (Label lblBaseFare = new Label())
            using (TextBox txtBaseFare = new TextBox())
            using (Label lblPricePerKm = new Label())
            using (TextBox txtPricePerKm = new TextBox())
            using (Label lblCommissionRate = new Label())
            using (TextBox txtCommissionRate = new TextBox())
            using (FlowLayoutPanel buttonPanel = new FlowLayoutPanel())
            using (Button btnOk = new Button())
            using (Button btnCancel = new Button())
            {
                dialog.Text = existingRule == null ? "Thêm quy tắc giá" : "Sửa quy tắc giá";
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.ShowInTaskbar = false;
                dialog.ClientSize = new System.Drawing.Size(360, 220);

                layout.Dock = DockStyle.Fill;
                layout.ColumnCount = 2;
                layout.RowCount = 5;
                layout.Padding = new Padding(12);
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

                lblVehicleType.Text = "Loại xe";
                lblVehicleType.Anchor = AnchorStyles.Left;
                cmbVehicleType.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbVehicleType.Items.Add("Motorbike");
                cmbVehicleType.Items.Add("Car");
                cmbVehicleType.SelectedItem = existingRule == null
                    ? (object)"Motorbike"
                    : existingRule.VehicleType?.ToString();
                cmbVehicleType.Enabled = existingRule == null;

                lblBaseFare.Text = "Giá mở cửa";
                lblBaseFare.Anchor = AnchorStyles.Left;
                txtBaseFare.Text = existingRule == null ? string.Empty : existingRule.BaseFare.Amount.ToString("F0");

                lblPricePerKm.Text = "Giá mỗi km";
                lblPricePerKm.Anchor = AnchorStyles.Left;
                txtPricePerKm.Text = existingRule == null ? string.Empty : existingRule.PricePerKm.Amount.ToString("F0");

                lblCommissionRate.Text = "Hoa hồng (%)";
                lblCommissionRate.Anchor = AnchorStyles.Left;
                txtCommissionRate.Text = existingRule == null ? string.Empty : (existingRule.CommissionRate * 100m).ToString("F2");

                buttonPanel.FlowDirection = FlowDirection.RightToLeft;
                buttonPanel.Dock = DockStyle.Fill;

                btnOk.Text = "Lưu";
                btnOk.DialogResult = DialogResult.OK;
                btnCancel.Text = "Huỷ";
                btnCancel.DialogResult = DialogResult.Cancel;

                buttonPanel.Controls.Add(btnOk);
                buttonPanel.Controls.Add(btnCancel);

                layout.Controls.Add(lblVehicleType, 0, 0);
                layout.Controls.Add(cmbVehicleType, 1, 0);
                layout.Controls.Add(lblBaseFare, 0, 1);
                layout.Controls.Add(txtBaseFare, 1, 1);
                layout.Controls.Add(lblPricePerKm, 0, 2);
                layout.Controls.Add(txtPricePerKm, 1, 2);
                layout.Controls.Add(lblCommissionRate, 0, 3);
                layout.Controls.Add(txtCommissionRate, 1, 3);
                layout.Controls.Add(buttonPanel, 0, 4);
                layout.SetColumnSpan(buttonPanel, 2);

                dialog.Controls.Add(layout);
                dialog.AcceptButton = btnOk;
                dialog.CancelButton = btnCancel;

                // Fix [High 2]: Validate before closing dialog
                FareRuleInputResult result = null;
                btnOk.Click += (s, ev) =>
                {
                    decimal baseFare;
                    decimal pricePerKm;
                    decimal commissionPercent;

                    if (!decimal.TryParse(txtBaseFare.Text.Trim(), out baseFare))
                    {
                        ShowWarning("Giá mở cửa không hợp lệ.");
                        dialog.DialogResult = DialogResult.None;
                        return;
                    }

                    if (!decimal.TryParse(txtPricePerKm.Text.Trim(), out pricePerKm))
                    {
                        ShowWarning("Giá mỗi km không hợp lệ.");
                        dialog.DialogResult = DialogResult.None;
                        return;
                    }

                    if (!decimal.TryParse(txtCommissionRate.Text.Trim(), out commissionPercent))
                    {
                        ShowWarning("Hoa hồng không hợp lệ.");
                        dialog.DialogResult = DialogResult.None;
                        return;
                    }

                    if (baseFare < 0m || pricePerKm < 0m)
                    {
                        ShowWarning("Giá cước phải lớn hơn hoặc bằng 0.");
                        dialog.DialogResult = DialogResult.None;
                        return;
                    }

                    if (commissionPercent < 0m || commissionPercent > 100m)
                    {
                        ShowWarning("Hoa hồng phải nằm trong khoảng 0 đến 100.");
                        dialog.DialogResult = DialogResult.None;
                        return;
                    }

                    result = new FareRuleInputResult();
                    result.VehicleType = (string)cmbVehicleType.SelectedItem;
                    result.BaseFare = baseFare;
                    result.PricePerKm = pricePerKm;
                    result.CommissionRate = (double)(commissionPercent / 100m);
                    dialog.DialogResult = DialogResult.OK;
                };

                if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                {
                    return null;
                }

                return result;
            }
        }

        private async Task DeleteSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return;

            FareRule rule = GetSelectedFareRule();
            if (rule == null)
            {
                ShowInfo("Không thể xác định quy tắc giá.");
                return;
            }

            if (!Confirm("Xoá quy tắc giá đã chọn?")) return;

            IsLoading = true;
            try
            {
                await _adminService.DeleteFareRuleAsync(rule.Id);
                _allFareRules = await _adminService.GetFareRulesAsync() ?? new List<FareRule>();
                PopulateFareRulesGrid();
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
        private class FareRuleInputResult
        {
            public string VehicleType { get; set; }
            public decimal BaseFare { get; set; }
            public decimal PricePerKm { get; set; }
            public double CommissionRate { get; set; }
        }

        private async Task UpdateStatsAsync()
        {
            IsLoading = true;
            try
            {
                lblGMV.Text = "Tổng doanh thu: " + (await _adminService.GetTotalGMVAsync()).ToString("N0") + "đ";
                lblTotalTrips.Text = "Tổng chuyến: " + _allTrips.Count.ToString();

                int activeCount = 0;
                foreach (User u in _allUsers)
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

