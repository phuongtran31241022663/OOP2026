using Application.Events;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Screens.AdminScreen
{
    public partial class AdminDashboardForm : BaseForm
    {
        // ── Direct color palette (replaces AppTheme) ──────────────────────────
        private static readonly Color CoPrimary = Color.FromArgb(37, 99, 235);
        private static readonly Color CoSuccess = Color.FromArgb(22, 163, 74);
        private static readonly Color CoWarning = Color.FromArgb(234, 179, 8);
        private static readonly Color CoDanger = Color.FromArgb(220, 38, 38);
        private static readonly Color CoAccent = Color.FromArgb(139, 92, 246);
        private static readonly Color CoPageBg = Color.FromArgb(243, 244, 246);
        private static readonly Color CoCardBg = Color.White;
        private static readonly Color CoTextMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color CoTextPrim = Color.FromArgb(17, 24, 39);
        private static readonly Color CoHighlight = Color.FromArgb(219, 234, 254);

        // ── Services ──────────────────────────────────────────────────────────
        private readonly IAdminService _adminService;
        private readonly ITripService _tripService;

        // ── Domain data ───────────────────────────────────────────────────────
        private readonly Admin _admin;
        private List<User> _allUsers = new List<User>();
        private List<Driver> _allDrivers = new List<Driver>();
        private List<Passenger> _allPassengers = new List<Passenger>();
        private List<Trip> _allTrips = new List<Trip>();

        // ── Section titles ────────────────────────────────────────────────────
        private static readonly string[] SectionTitles =
        {
            "Dashboard", "Người dùng", "Tài xế",
            "Hành khách", "Chuyến đi", "Bảng giá", "Báo cáo"
        };

        // ─────────────────────────────────────────────────────────────────────
        public AdminDashboardForm(Admin admin, IAdminService adminService, ITripService tripService)
        {
            _admin = admin ?? throw new ArgumentNullException(nameof(admin));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));

            InitializeComponent();
            Text = $"Quản trị hệ thống  ({_admin.Name})";

            BuildUI();
            Shown += async (sender, e) => await LoadAllData();

            _tripService.TripStatusChanged += OnTripStatusChanged;
            FormClosed += (sender, e) => _tripService.TripStatusChanged -= OnTripStatusChanged;
        }

        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(async () => await ReloadTripsData()));
            else
                _ = ReloadTripsData();
        }

        private async Task ReloadTripsData()
        {
            await LoadTrips();
            UpdateStatsBar();
        }

        // ── Build UI ──────────────────────────────────────────────────────────

        private void BuildUI()
        {
            BuildHeader();
            BuildSidebar();

            var statsBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 96,
                BackColor = CoPageBg,
                Padding = new Padding(16, 10, 16, 10)
            };
            BuildStatsBar(statsBar);

            var contentArea = new Panel { Dock = DockStyle.Fill, BackColor = CoPageBg };

            RegisterPanel(TAB_DASHBOARD, BuildDashboardPanel(), contentArea);
            RegisterPanel(TAB_USERS, BuildUsersPanel(), contentArea);
            RegisterPanel(TAB_DRIVERS, BuildDriversPanel(), contentArea);
            RegisterPanel(TAB_PASSENGERS, BuildPassengersPanel(), contentArea);
            RegisterPanel(TAB_TRIPS, BuildTripsPanel(), contentArea);
            RegisterPanel(TAB_FARE_RULES, BuildFareRulesPanel(), contentArea);
            RegisterPanel(TAB_REPORTS, BuildReportsPanel(), contentArea);

            ContentPanel.Controls.Add(contentArea);
            ContentPanel.Controls.Add(statsBar);

            ShowSection(TAB_DASHBOARD);
        }

        private void RegisterPanel(int index, Panel panel, Panel host)
        {
            panel.Visible = false;
            panel.Dock = DockStyle.Fill;
            host.Controls.Add(panel);
            _contentPanels[index] = panel;
        }

        // ── Sidebar ───────────────────────────────────────────────────────────

        private void BuildSidebar()
        {
            var items = new (int idx, string icon, string label)[]
            {
                (TAB_DASHBOARD,  "🏠", "Dashboard"),
                (TAB_USERS,      "👤", "Người dùng"),
                (TAB_DRIVERS,    "🛵", "Tài xế"),
                (TAB_PASSENGERS, "👥", "Hành khách"),
                (TAB_TRIPS,      "🚗", "Chuyến đi"),
                (TAB_FARE_RULES, "💰", "Bảng giá"),
                (TAB_REPORTS,    "📊", "Báo cáo"),
            };

            foreach (var (idx, icon, label) in items)
            {
                int capturedIdx = idx;
                var btn = AddSidebarNav(icon, label, (sender, e) => ShowSection(capturedIdx));
                _navButtons[idx] = btn;
            }
        }

        // ── Header ────────────────────────────────────────────────────────────

        private void BuildHeader()
        {
            HeaderPanel.Padding = new Padding(16, 0, 16, 0);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));

            _lblSectionTitle = new Label
            {
                Text = "🔧  Dashboard",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            var lblAdmin = new Label
            {
                Text = $"👤 {_admin.Name}",
                ForeColor = Color.FromArgb(200, 225, 255),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9f)
            };

            var btnLogout = new Button
            {
                Text = "← Đăng xuất",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = CoDanger,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += OnLogoutClicked;

            layout.Controls.Add(_lblSectionTitle, 0, 0);
            layout.Controls.Add(lblAdmin, 1, 0);
            layout.Controls.Add(btnLogout, 2, 0);
            HeaderPanel.Controls.Add(layout);
        }

        // ── Section switching ─────────────────────────────────────────────────

        private void ShowSection(int index)
        {
            _currentSection = index;

            foreach (var kv in _contentPanels)
                kv.Value.Visible = kv.Key == index;

            _lblSectionTitle.Text = $"🔧  {SectionTitles[index]}";

            if (_navButtons.TryGetValue(index, out var activeBtn))
                SetActiveNav(activeBtn);
        }

        // ── Stats bar ─────────────────────────────────────────────────────────

        private void BuildStatsBar(Panel container)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            for (int i = 0; i < 4; i++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            _lblStatsTotalUsers = MakeStatCard(layout, 0, "👥", "Tổng người dùng", CoPrimary);
            _lblStatsActiveDrivers = MakeStatCard(layout, 1, "🟢", "Tài xế đang hoạt động", CoSuccess);
            _lblStatsOnTripDrivers = MakeStatCard(layout, 2, "🔴", "Tài xế đang bận", CoWarning);
            _lblStatsOngoingTrips = MakeStatCard(layout, 3, "🚗", "Chuyến đang diễn ra", CoAccent);
            container.Controls.Add(layout);
        }

        // ── Content panels ────────────────────────────────────────────────────

        private Panel BuildDashboardPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            panel.Controls.Add(new Label
            {
                Text = "Chào mừng đến hệ thống quản trị.\nChọn mục trong sidebar bên trái để bắt đầu.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12),
                ForeColor = CoTextMuted
            });
            return panel;
        }

        private Panel BuildUsersPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var searchPanel = MakeSearchPanel(out _txtSearchUsers, "Tìm theo tên, SĐT...");
            _txtSearchUsers.TextChanged += async (sender, e) => await FilterUsers();

            var toolbar = MakeToolbar();
            var btnRefresh = MakeToolbarBtn("🔄  Làm mới", CoPrimary);
            var btnToggle = MakeToolbarBtn("🔒  Khóa/Mở", CoWarning);
            btnRefresh.Click += async (sender, e) => await LoadUsers();
            btnToggle.Click += async (sender, e) => await ToggleUserActive();
            toolbar.Controls.Add(btnRefresh);
            toolbar.Controls.Add(btnToggle);

            _dgvUsers = MakeGrid();
            _dgvUsers.Columns.AddRange(
                MakeCol("UserId", "ID", 60, hidden: true),
                MakeCol("Name", "Họ tên", 200),
                MakeCol("Phone", "SĐT", 140),
                MakeCol("Role", "Vai trò", 100),
                MakeCol("Active", "Trạng thái", 110),
                MakeCol("JoinedAt", "Ngày tham gia", 150)
            );

            panel.Controls.Add(_dgvUsers);
            panel.Controls.Add(toolbar);
            panel.Controls.Add(searchPanel);
            return panel;
        }

        private Panel BuildDriversPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var searchPanel = MakeSearchPanel(out _txtSearchDrivers, "Tìm theo tên, SĐT...");
            _txtSearchDrivers.TextChanged += async (sender, e) => await FilterDrivers();

            var toolbar = MakeToolbar();
            var btnRefresh = MakeToolbarBtn("🔄  Làm mới", CoPrimary);
            var btnActivate = MakeToolbarBtn("✅  Kích hoạt", CoSuccess);
            var btnDeactivate = MakeToolbarBtn("🔒  Vô hiệu hóa", CoDanger);
            btnRefresh.Click += async (sender, e) => await LoadDrivers();
            btnActivate.Click += async (sender, e) => await SetDriverActive(true);
            btnDeactivate.Click += async (sender, e) => await SetDriverActive(false);
            toolbar.Controls.Add(btnRefresh);
            toolbar.Controls.Add(btnActivate);
            toolbar.Controls.Add(btnDeactivate);

            _dgvDrivers = MakeGrid();
            _dgvDrivers.Columns.AddRange(
                MakeCol("DriverId", "ID", 60, hidden: true),
                MakeCol("Name", "Họ tên", 180),
                MakeCol("Phone", "SĐT", 130),
                MakeCol("VehicleType", "Loại xe", 110),
                MakeCol("Plate", "Biển số", 110),
                MakeCol("Status", "Trạng thái", 120),
                MakeCol("Rating", "Đánh giá", 100),
                MakeCol("Trips", "Chuyến", 90),
                MakeCol("Active", "Hoạt động", 100)
            );

            panel.Controls.Add(_dgvDrivers);
            panel.Controls.Add(toolbar);
            panel.Controls.Add(searchPanel);
            return panel;
        }

        private Panel BuildPassengersPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var searchPanel = MakeSearchPanel(out _txtSearchPassengers, "Tìm theo tên, SĐT...");
            _txtSearchPassengers.TextChanged += async (sender, e) => await FilterPassengers();

            var toolbar = MakeToolbar();
            var btnRefresh = MakeToolbarBtn("🔄  Làm mới", CoPrimary);
            var btnActivate = MakeToolbarBtn("✅  Kích hoạt", CoSuccess);
            var btnDeactivate = MakeToolbarBtn("🔒  Vô hiệu hóa", CoDanger);
            btnRefresh.Click += async (sender, e) => await LoadPassengers();
            btnActivate.Click += async (sender, e) => await SetPassengerActive(true);
            btnDeactivate.Click += async (sender, e) => await SetPassengerActive(false);
            toolbar.Controls.Add(btnRefresh);
            toolbar.Controls.Add(btnActivate);
            toolbar.Controls.Add(btnDeactivate);

            _dgvPassengers = MakeGrid();
            _dgvPassengers.Columns.AddRange(
                MakeCol("PassengerId", "ID", 60, hidden: true),
                MakeCol("Name", "Họ tên", 200),
                MakeCol("Phone", "SĐT", 140),
                MakeCol("Trips", "Số chuyến", 100),
                MakeCol("Active", "Hoạt động", 100),
                MakeCol("JoinedAt", "Ngày tham gia", 150)
            );

            panel.Controls.Add(_dgvPassengers);
            panel.Controls.Add(toolbar);
            panel.Controls.Add(searchPanel);
            return panel;
        }

        private Panel BuildTripsPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var searchPanel = MakeSearchPanel(out _txtSearchTrips, "Tìm theo địa chỉ...");
            _txtSearchTrips.TextChanged += async (sender, e) => await FilterTrips();

            var reportStrip = new Panel
            {
                Dock = DockStyle.Top,
                Height = 38,
                BackColor = CoHighlight,
                Padding = new Padding(16, 0, 16, 0)
            };
            var stripLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            for (int i = 0; i < 4; i++)
                stripLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            _lblTotalTrips = MakeStripLabel("Tổng chuyến: --");
            _lblTotalRevenue = MakeStripLabel("Doanh thu: --");
            _lblDriverIncome = MakeStripLabel("Thu nhập TX: --");
            _lblCommission = MakeStripLabel("Hoa hồng: --");

            stripLayout.Controls.Add(_lblTotalTrips, 0, 0);
            stripLayout.Controls.Add(_lblTotalRevenue, 1, 0);
            stripLayout.Controls.Add(_lblDriverIncome, 2, 0);
            stripLayout.Controls.Add(_lblCommission, 3, 0);
            reportStrip.Controls.Add(stripLayout);

            var toolbar = MakeToolbar();
            var btnRefresh = MakeToolbarBtn("🔄  Làm mới", CoPrimary);
            btnRefresh.Click += async (sender, e) => await LoadTrips();
            toolbar.Controls.Add(btnRefresh);

            _dgvTrips = MakeGrid();
            _dgvTrips.Columns.AddRange(
                MakeCol("TripId", "ID", 60, hidden: true),
                MakeCol("Passenger", "Hành khách", 160),
                MakeCol("Driver", "Tài xế", 160),
                MakeCol("VehicleType", "Loại xe", 90),
                MakeCol("Pickup", "Điểm đón", 200),
                MakeCol("Destination", "Điểm đến", 200),
                MakeCol("Distance", "Khoảng cách", 110),
                MakeCol("Fare", "Cước phí", 110),
                MakeCol("Status", "Trạng thái", 120),
                MakeCol("RequestedAt", "Thời gian", 140)
            );

            panel.Controls.Add(_dgvTrips);
            panel.Controls.Add(toolbar);
            panel.Controls.Add(reportStrip);
            panel.Controls.Add(searchPanel);
            return panel;
        }

        private Panel BuildFareRulesPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var toolbar = MakeToolbar();
            var btnRefresh = MakeToolbarBtn("🔄  Làm mới", CoPrimary);
            var btnAdd = MakeToolbarBtn("➕  Thêm mới", CoSuccess);
            var btnEdit = MakeToolbarBtn("✏️  Chỉnh sửa", CoSuccess);
            btnRefresh.Click += async (sender, e) => await LoadFareRules();
            btnAdd.Click += async (sender, e) => await OnAddFareRule();
            btnEdit.Click += async (sender, e) => await OnEditFareRule();
            toolbar.Controls.Add(btnRefresh);
            toolbar.Controls.Add(btnAdd);
            toolbar.Controls.Add(btnEdit);

            _dgvFareRules = MakeGrid();
            _dgvFareRules.Columns.AddRange(
                MakeCol("FareRuleId", "ID", 60, hidden: true),
                MakeCol("VehicleType", "Loại xe", 120),
                MakeCol("BaseFare", "Giá mở cửa", 140),
                MakeCol("PricePerKm", "Giá / km", 130),
                MakeCol("CommissionRate", "Hoa hồng (%)", 130),
                MakeCol("UpdatedAt", "Cập nhật lúc", 150)
            );

            panel.Controls.Add(_dgvFareRules);
            panel.Controls.Add(toolbar);
            return panel;
        }

        private Panel BuildReportsPanel()
        {
            var panel = new Panel { BackColor = CoPageBg };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2,
                Padding = new Padding(20)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(MakeReportCard("🚗", "Tổng chuyến", "--", CoPrimary), 0, 0);
            layout.Controls.Add(MakeReportCard("💰", "Tổng doanh thu", "--", CoSuccess), 1, 0);

            var detailPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 8, 0, 0),
                BorderStyle = BorderStyle.FixedSingle
            };
            detailPanel.Controls.Add(new Label
            {
                Text = "Dữ liệu chi tiết sẽ xuất hiện tại đây.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray
            });
            layout.SetColumnSpan(detailPanel, 2);
            layout.Controls.Add(detailPanel, 0, 1);
            panel.Controls.Add(layout);
            return panel;
        }

        private static Panel MakeReportCard(string icon, string title, string value, Color color)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(8),
                BackColor = CoCardBg,
                Padding = new Padding(16),
                BorderStyle = BorderStyle.FixedSingle
            };
            card.Controls.Add(new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 22f),
                Location = new Point(16, 16),
                AutoSize = true
            });
            card.Controls.Add(new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = CoTextMuted,
                Location = new Point(60, 16),
                AutoSize = true
            });
            card.Controls.Add(new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(60, 36),
                AutoSize = true
            });
            return card;
        }

        // ── Data loading ──────────────────────────────────────────────────────

        private async Task LoadAllData()
        {
            try
            {
                await Task.WhenAll(
                    LoadUsers(), LoadDrivers(), LoadPassengers(),
                    LoadTrips(), LoadFareRules());
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task LoadUsers()
        {
            try
            {
                _allUsers = (await _adminService.GetAllUsers()).ToList();
                PopulateUsers(_allUsers);
                UpdateStatsBar();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void PopulateUsers(IEnumerable<User> users)
        {
            _dgvUsers.Rows.Clear();
            foreach (var u in users.OrderBy(u => u.Name))
            {
                bool isActive = u is Driver d ? d.IsActive : u is Passenger p ? p.IsActive : true;
                var createdAt = u is Driver d2 ? d2.CreatedAt : u is Passenger p2 ? p2.CreatedAt : DateTime.UtcNow;
                _dgvUsers.Rows.Add(
                    u.Id, u.Name, u.Phone,
                    u is Driver ? "Tài xế" : u is Passenger ? "Hành khách" : "Admin",
                    isActive ? "✅ Hoạt động" : "🔒 Bị khóa",
                    createdAt.ToString("dd/MM/yyyy"));
            }
        }

        private async Task FilterUsers()
        {
            var q = _txtSearchUsers.Text.ToLower();
            PopulateUsers(_allUsers.Where(u =>
                u.Name.ToLower().Contains(q) || u.Phone.Contains(q)));
            await Task.CompletedTask;
        }

        private async Task ToggleUserActive()
        {
            if (_dgvUsers.CurrentRow == null) return;
            var id = (Guid)_dgvUsers.CurrentRow.Cells["UserId"].Value;
            try
            {
                var user = _allUsers.FirstOrDefault(u => u.Id == id);
                bool current = user is Driver d ? d.IsActive : user is Passenger p ? p.IsActive : true;
                if (current) await _adminService.DeActivateAccountUser(id, _admin.Id);
                else await _adminService.ActivateAccountUser(id);
                await LoadUsers();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task LoadDrivers()
        {
            try
            {
                _allDrivers = _allUsers.OfType<Driver>().ToList();
                if (!_allDrivers.Any())
                {
                    var all = await _adminService.GetAllUsers();
                    _allDrivers = all.OfType<Driver>().ToList();
                }
                PopulateDrivers(_allDrivers);
                UpdateStatsBar();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void PopulateDrivers(IEnumerable<Driver> drivers)
        {
            _dgvDrivers.Rows.Clear();
            foreach (var d in drivers.OrderBy(d => d.Name))
                _dgvDrivers.Rows.Add(
                    d.Id, d.Name, d.Phone,
                    d.Vehicle.GetVehicleType(),
                    d.Vehicle?.PlateNumber ?? "–",
                    d.Status.ToString(),
                    $"⭐ {d.AverageRating:F1}",
                    d.TotalTrips,
                    d.IsActive ? "✅" : "🔒");
        }

        private async Task FilterDrivers()
        {
            var q = _txtSearchDrivers.Text.ToLower();
            PopulateDrivers(_allDrivers.Where(d =>
                d.Name.ToLower().Contains(q) || d.Phone.Contains(q)));
            await Task.CompletedTask;
        }

        private async Task SetDriverActive(bool activate)
        {
            if (_dgvDrivers.CurrentRow == null) return;
            var id = (Guid)_dgvDrivers.CurrentRow.Cells["DriverId"].Value;
            try
            {
                if (activate) await _adminService.ActivateAccountUser(id);
                else await _adminService.DeActivateAccountUser(id, _admin.Id);
                await LoadDrivers();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task LoadPassengers()
        {
            try
            {
                _allPassengers = _allUsers.OfType<Passenger>().ToList();
                if (!_allPassengers.Any())
                {
                    var all = await _adminService.GetAllUsers();
                    _allPassengers = all.OfType<Passenger>().ToList();
                }
                PopulatePassengers(_allPassengers);
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void PopulatePassengers(IEnumerable<Passenger> passengers)
        {
            _dgvPassengers.Rows.Clear();
            foreach (var p in passengers.OrderBy(p => p.Name))
                _dgvPassengers.Rows.Add(
                    p.Id, p.Name, p.Phone,
                    p.TotalTrips,
                    p.IsActive ? "✅" : "🔒",
                    p.CreatedAt.ToString("dd/MM/yyyy"));
        }

        private async Task FilterPassengers()
        {
            var q = _txtSearchPassengers.Text.ToLower();
            PopulatePassengers(_allPassengers.Where(p =>
                p.Name.ToLower().Contains(q) || p.Phone.Contains(q)));
            await Task.CompletedTask;
        }

        private async Task SetPassengerActive(bool activate)
        {
            if (_dgvPassengers.CurrentRow == null) return;
            var id = (Guid)_dgvPassengers.CurrentRow.Cells["PassengerId"].Value;
            try
            {
                if (activate) await _adminService.ActivateAccountUser(id);
                else await _adminService.DeActivateAccountUser(id, _admin.Id);
                await LoadPassengers();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task LoadTrips()
        {
            try
            {
                _allTrips = (await _adminService.GetAllTrips()).ToList();
                var users = await _adminService.GetAllUsers();
                var nameMap = users.ToDictionary(u => u.Id, u => u.Name);
                PopulateTrips(_allTrips, nameMap);
                await LoadTripReport();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void PopulateTrips(IEnumerable<Trip> trips, Dictionary<Guid, string> nameMap)
        {
            _dgvTrips.Rows.Clear();
            foreach (var t in trips.OrderByDescending(t => t.RequestedAt))
            {
                string passenger = nameMap.TryGetValue(t.PassengerId, out var pn)
                    ? pn : t.PassengerId.ToString()[..8];
                string driver = t.DriverId.HasValue && nameMap.TryGetValue(t.DriverId.Value, out var dn)
                    ? dn : "Chưa có";
                _dgvTrips.Rows.Add(
                    t.Id, passenger, driver, t.VehicleType,
                    t.Pickup?.Address ?? "–",
                    t.Destination?.Address ?? "–",
                    t.Distance > 0 ? $"{t.Distance:F1} km" : "–",
                    t.Fare > 0 ? $"{t.Fare:N0} đ" : "–",
                    TripStatusLabel(t.Status),
                    t.RequestedAt.ToString("dd/MM/yyyy HH:mm"));
            }
        }

        private async Task FilterTrips()
        {
            var q = _txtSearchTrips.Text.ToLower();
            var users = await _adminService.GetAllUsers();
            var nameMap = users.ToDictionary(u => u.Id, u => u.Name);
            PopulateTrips(_allTrips.Where(t =>
                (t.Pickup?.Address.ToLower().Contains(q) ?? false) ||
                (t.Destination?.Address.ToLower().Contains(q) ?? false)), nameMap);
        }

        private async Task LoadTripReport()
        {
            try
            {
                var report = await _adminService.GetTripReport();
                _lblTotalTrips.Text = $"Tổng: {report.TotalTrips} chuyến";
                _lblTotalRevenue.Text = $"Doanh thu: {report.TotalRevenue:N0} đ";
                _lblDriverIncome.Text = $"Thu nhập TX: {report.TotalDriverIncome:N0} đ";
                _lblCommission.Text = $"Hoa hồng: {report.TotalCommission:N0} đ";
            }
            catch { _lblTotalTrips.Text = "Tổng chuyến: --"; }
        }

        private async Task LoadFareRules()
        {
            try
            {
                var rules = await _adminService.GetFareRules();
                _dgvFareRules.Rows.Clear();
                foreach (var r in rules)
                    _dgvFareRules.Rows.Add(
                        r.Id, r.VehicleType,
                        $"{r.BaseFare:N0} đ",
                        $"{r.PricePerKm:N0} đ",
                        $"{r.CommissionRate * 100:F0}%",
                        r.UpdatedAt.ToString("dd/MM/yyyy HH:mm"));
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task OnEditFareRule()
        {
            if (_dgvFareRules.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hàng cần chỉnh sửa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var id = (Guid)_dgvFareRules.CurrentRow.Cells["FareRuleId"].Value;
            var rules = await _adminService.GetFareRules();
            var rule = rules.FirstOrDefault(r => r.Id == id);
            if (rule == null) return;

            using var form = new EditFareRuleForm(rule);
            if (form.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                rule.UpdateRule(form.NewBaseFare, form.NewPricePerKm, form.NewCommissionRate);
                await _adminService.UpdateFareRule(rule);
                await LoadFareRules();
                MessageBox.Show("Cập nhật bảng giá thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private async Task OnAddFareRule()
        {
            using var form = new AddFareRuleForm();
            if (form.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                var vehicleType = Enum.Parse<VehicleType>(form.NewVehicleType, true);
                var rule = new Fare(vehicleType, form.NewBaseFare, form.NewPricePerKm, form.NewCommissionRate);
                await _adminService.CreateFareRule(rule);
                await LoadFareRules();
                MessageBox.Show("Thêm bảng giá thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        // ── Stats update ──────────────────────────────────────────────────────

        private void UpdateStatsBar()
        {
            if (InvokeRequired) { BeginInvoke(UpdateStatsBar); return; }
            _lblStatsTotalUsers.Text = _allUsers.Count.ToString();
            _lblStatsActiveDrivers.Text = _allUsers.OfType<Driver>()
                .Count(d => d.IsActive && d.Status == DriverStatus.Available).ToString();
            _lblStatsOnTripDrivers.Text = _allUsers.OfType<Driver>()
                .Count(d => d.IsActive && d.Status == DriverStatus.OnTrip).ToString();
            _lblStatsOngoingTrips.Text = _allTrips.Count(t =>
                t.Status is TripStatus.Matched or TripStatus.Arrived or TripStatus.Started).ToString();
        }

        // ── Logout ────────────────────────────────────────────────────────────

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        // ── UI factory methods (replaces FormHelper) ──────────────────────────

        private static Panel MakeSearchPanel(out TextBox txt, string placeholder)
        {
            var box = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.Gray,
                Text = placeholder,
                BorderStyle = BorderStyle.FixedSingle
            };
            box.GotFocus += (sender, e) => { if (box.Text == placeholder) { box.Text = ""; box.ForeColor = SystemColors.WindowText; } };
            box.LostFocus += (sender, e) => { if (box.Text == "") { box.Text = placeholder; box.ForeColor = Color.Gray; } };
            txt = box;

            var panel = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(8, 4, 8, 4) };
            panel.Controls.Add(box);
            return panel;
        }

        private static Panel MakeToolbar()
        {
            return new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(229, 231, 235),
                Padding = new Padding(8, 4, 8, 4)
            };
        }

        private static Button MakeToolbarBtn(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Left,
                Width = 130,
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Margin = new Padding(0, 0, 6, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private static DataGridView MakeGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9f)
            };
        }

        private static DataGridViewTextBoxColumn MakeCol(
            string name, string header, int width, bool hidden = false)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                Width = width,
                Visible = !hidden
            };
        }

        private static Label MakeStripLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = CoTextPrim
            };
        }

        /// <summary>
        /// Creates a stat card inside the given TableLayoutPanel column.
        /// Returns the value Label so the caller can update it at runtime.
        /// </summary>
        private static Label MakeStatCard(
            TableLayoutPanel layout, int col, string icon, string title, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(6),
                BackColor = CoCardBg,
                Padding = new Padding(12),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 18f),
                AutoSize = false,
                Size = new Size(40, 40),
                Location = new Point(12, 12)
            };
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8f),
                ForeColor = CoTextMuted,
                AutoSize = false,
                Size = new Size(150, 18),
                Location = new Point(58, 12)
            };
            var lblValue = new Label
            {
                Text = "--",
                Font = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = accent,
                AutoSize = false,
                Size = new Size(150, 32),
                Location = new Point(58, 30)
            };

            card.Controls.Add(lblIcon);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            layout.Controls.Add(card, col, 0);
            return lblValue;
        }

        private static void ShowError(string msg) =>
            MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        // ── Helpers ───────────────────────────────────────────────────────────

        private static string TripStatusLabel(TripStatus s) => s switch
        {
            TripStatus.Requested => "⏳ Đang tìm",
            TripStatus.Searching => "🔎 Đang tìm",
            TripStatus.Matched => "🤝 Đã ghép",
            TripStatus.Arrived => "📍 Đã đến",
            TripStatus.Started => "🚗 Đang chạy",
            TripStatus.Completed => "✅ Hoàn thành",
            TripStatus.Cancelled => "❌ Đã hủy",
            TripStatus.Timeout => "⌛ Hết thời gian",
            _ => s.ToString()
        };
    }

    // ── EditFareRuleForm ───────────────────────────────────────────────────────

    public class EditFareRuleForm : Form
    {
        // Direct colors (no AppTheme)
        protected static readonly Color ColPrimary = Color.FromArgb(37, 99, 235);
        protected static readonly Color ColSuccess = Color.FromArgb(22, 163, 74);
        protected static readonly Color ColText = Color.FromArgb(17, 24, 39);

        private readonly NumericUpDown _numBaseFare;
        private readonly NumericUpDown _numPricePerKm;
        private readonly NumericUpDown _numCommission;

        public decimal NewBaseFare { get; protected set; }
        public decimal NewPricePerKm { get; protected set; }
        public decimal NewCommissionRate { get; protected set; }

        public EditFareRuleForm(Fare rule)
        {
            Text = $"Chỉnh sửa bảng giá – {rule.VehicleType}";
            Size = new Size(420, 340);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            var layout = MakeFormLayout(6);
            AddHeaderLabel(layout, $"Loại xe: {rule.VehicleType}", 0);
            _numBaseFare = AddNumRow(layout, "Giá mở cửa (đ):", 1, rule.BaseFare, 0, 500_000);
            _numPricePerKm = AddNumRow(layout, "Giá / km (đ):", 2, rule.PricePerKm, 0, 100_000);
            _numCommission = AddNumRow(layout, "Hoa hồng (%):", 4, rule.CommissionRate * 100m, 0, 100, decimals: 0);
            AddButtons(layout, 5, OnSave);
            Controls.Add(layout);
        }

        private void OnSave(object sender, EventArgs e)
        {
            NewBaseFare = _numBaseFare.Value;
            NewPricePerKm = _numPricePerKm.Value;
            NewCommissionRate = _numCommission.Value / 100m;
            DialogResult = DialogResult.OK;
        }

        protected static TableLayoutPanel MakeFormLayout(int rows)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = rows,
                Padding = new Padding(24, 16, 24, 12)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54));
            for (int i = 0; i < rows; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
            return layout;
        }

        protected static void AddHeaderLabel(TableLayoutPanel layout, string text, int row)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = ColPrimary,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            layout.Controls.Add(lbl, 0, row);
            layout.SetColumnSpan(lbl, 2);
        }

        protected static NumericUpDown AddNumRow(
            TableLayoutPanel layout, string label, int row,
            decimal value, decimal min, decimal max, int decimals = 0)
        {
            layout.Controls.Add(new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = ColText
            }, 0, row);

            var num = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = min,
                Maximum = max,
                Value = Math.Clamp(value, min, max),
                DecimalPlaces = decimals,
                ThousandsSeparator = decimals == 0,
                Font = new Font("Segoe UI", 10.5f)
            };
            layout.Controls.Add(num, 1, row);
            return num;
        }

        protected void AddButtons(TableLayoutPanel layout, int row, EventHandler saveHandler)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 6, 0, 0)
            };

            var btnCancel = new Button
            {
                Text = "Hủy",
                Width = 90,
                Height = 34,
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            var btnSave = new Button
            {
                Text = "Lưu",
                Width = 90,
                Height = 34,
                BackColor = ColSuccess,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += saveHandler;

            panel.Controls.Add(btnCancel);
            panel.Controls.Add(btnSave);
            layout.Controls.Add(panel, 0, row);
            layout.SetColumnSpan(panel, 2);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }
    }

    // ── AddFareRuleForm ────────────────────────────────────────────────────────

    public class AddFareRuleForm : EditFareRuleForm
    {
        public string NewVehicleType { get; private set; } = "Motorbike";

        private readonly ComboBox _cmbType;
        private readonly NumericUpDown _numBase;
        private readonly NumericUpDown _numPerKm;
        private readonly NumericUpDown _numCommission2;

        public AddFareRuleForm() : base(new Fare(VehicleType.Motorbike, 0, 0, 0))
        {
            Text = "Thêm bảng giá mới";
            Size = new Size(420, 380);
            Controls.Clear();

            var layout = MakeFormLayout(7);
            AddHeaderLabel(layout, "Thêm loại xe mới", 0);

            layout.Controls.Add(new Label
            {
                Text = "Loại xe:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = ColText
            }, 0, 1);

            _cmbType = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10.5f)
            };
            _cmbType.Items.AddRange(new object[] { "Motorbike", "Car" });
            _cmbType.SelectedIndex = 0;
            layout.Controls.Add(_cmbType, 1, 1);

            _numBase = AddNumRow(layout, "Giá mở cửa (đ):", 2, 10_000, 0, 500_000);
            _numPerKm = AddNumRow(layout, "Giá / km (đ):", 3, 5_000, 0, 100_000);
            _numCommission2 = AddNumRow(layout, "Hoa hồng (%):", 5, 20, 0, 100, decimals: 0);
            AddButtons(layout, 6, OnSaveNew);
            Controls.Add(layout);
        }

        private void OnSaveNew(object sender, EventArgs e)
        {
            NewVehicleType = _cmbType.SelectedItem?.ToString() ?? "Motorbike";
            NewBaseFare = _numBase.Value;
            NewPricePerKm = _numPerKm.Value;
            NewCommissionRate = _numCommission2.Value / 100m;
            DialogResult = DialogResult.OK;
        }
    }
}