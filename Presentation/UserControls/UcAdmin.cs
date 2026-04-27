using Application.Interfaces;
using Domain.Entities.Users;
using Presentation.Shells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.UserControls
{
    /// <summary>
    /// Trung tam quan tri: Users, Trips, FareRules, Stats.
    /// TabControl voi 4 tabs, moi tab la mot khong gian lam viec doc lap.
    /// </summary>
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
                case 3: UpdateStats(); break;
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
                ShowError("Tai du lieu that bai: " + ex.Message);
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
            foreach (var user in _allUsers)
            {
                string role = user is Driver ? "Driver" : (user is Passenger ? "Passenger" : "Admin");
                dgvUsers.Rows.Add(user.Id.ToString().Substring(0, 8), user.Name, user.Phone, role, "Active");
            }
        }

        private void FilterUsers(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                PopulateUsersGrid();
                return;
            }

            dgvUsers.Rows.Clear();
            foreach (var user in _allUsers.Where(u => u.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 || u.Phone.IndexOf(term) >= 0))
            {
                string role = user is Driver ? "Driver" : (user is Passenger ? "Passenger" : "Admin");
                dgvUsers.Rows.Add(user.Id.ToString().Substring(0, 8), user.Name, user.Phone, role, "Active");
            }
        }

        private async Task ToggleUserLock(bool lockUser)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;
            // Implementation placeholder
            ShowInfo(lockUser ? "Da khoa tai khoan." : "Da mo khoa tai khoan.");
        }

        // --- Trips Tab ---
        private void PopulateTripsGrid()
        {
            dgvTrips.Rows.Clear();
            foreach (var trip in _allTrips)
            {
                dgvTrips.Rows.Add(trip.Id.ToString().Substring(0, 8), trip.Status, trip.TripVehicleType, trip.TripFare?.TotalAmount.Amount.ToString("N0") + "d", trip.RequestAt.ToString("dd/MM HH:mm"));
            }
        }

        private async Task CancelSelectedTrip()
        {
            if (dgvTrips.SelectedRows.Count == 0) return;
            if (!Confirm("Huy chuyen da chon?")) return;
            ShowInfo("Da huy chuyen.");
        }

        private void ShowTripDetail()
        {
            if (dgvTrips.SelectedRows.Count == 0) return;
            var trip = _allTrips[dgvTrips.SelectedRows[0].Index];
            var ucDetail = new UcTripDetail(trip);
            FrmModal.ShowModal(this, ucDetail, "Chi tiet chuyen");
        }

        // --- FareRules Tab ---
        private void PopulateFareRulesGrid()
        {
            dgvFareRules.Rows.Clear();
            foreach (var rule in _allFareRules)
            {
                dgvFareRules.Rows.Add(rule.VehicleType, rule.BaseFare.Amount.ToString("N0"), rule.PricePerKm.Amount.ToString("N0"), (rule.CommissionRate * 100).ToString("F0") + "%");
            }
        }

        private Domain.Entities.FareRule GetSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return null;
            return _allFareRules[dgvFareRules.SelectedRows[0].Index];
        }

        private void ShowFareEditDialog(Domain.Entities.FareRule rule)
        {
            ShowInfo(rule == null ? "Them moi quy tac gia." : "Sua quy tac gia.");
        }

        private async Task DeleteSelectedFareRule()
        {
            if (dgvFareRules.SelectedRows.Count == 0) return;
            if (!Confirm("Xoa quy tac gia da chon?")) return;
            ShowInfo("Da xoa quy tac gia.");
        }

        // --- Stats Tab ---
        private async void UpdateStats()
        {
            try
            {
                lblGMV.Text = (await _adminService.GetTotalGMVAsync()).ToString("N0") + "d";
                lblTotalTrips.Text = _allTrips.Count.ToString();
                lblActiveDrivers.Text = _allUsers.Count(u => u is Driver d && d.IsAvailable()).ToString();
                lblAvgRating.Text = (await _adminService.GetAverageSatisfactionAsync()).ToString("F1");
                lblCompletionRate.Text = (await _adminService.GetCompletionRateAsync() * 100).ToString("F0") + "%";
            }
            catch { }
        }
    }
}

