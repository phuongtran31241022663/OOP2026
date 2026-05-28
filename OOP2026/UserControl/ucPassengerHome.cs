namespace OOP2026
{
    public partial class ucPassengerHome : UserControl
    {
        private Psg _passenger;
        private IUsrSvc _userService;
        private ITripCmd _tripCmdService;
        private ITripQry _tripQueryService;
        private IRevSvc _reviewService;
        private IFareSvc _fareService;
        private IMapSvc _mapService;
        private IPsgSvc _passengerService;
        private INotificationSvc _notificationSvc;
        private ucMap _map;

        // Định nghĩa tập hợp các nút tab để dùng vòng lặp for chỉ mục (Khử LINQ tối ưu hiệu năng)
        private Button[] _tabButtons;

        private ucBooking ucBooking;
        private ucTrip ucTrip;
        private ucHistory ucHistory;
        private ucProfile ucProfile;

        private Button _currentTabButton;

        public ucPassengerHome()
        {
            InitializeComponent();
            InitializeChildControls();

            // Đăng ký sự kiện hủy hệ thống để dọn sạch RAM chủ động
            this.Disposed += UcPassengerHome_Disposed;
        }

        public void SetMap(ucMap map)
        {
            _map = map;
        }

        public void Initialize(Psg passenger,
                                 IUsrSvc userService,
                                 ITripCmd tripCommandService,
                                 ITripQry tripQueryService,
                                 IRevSvc reviewService,
                                 IFareSvc fareService,
                                 IMapSvc mapService,
                                 IPsgSvc passengerService,
                                 INotificationSvc notificationSvc)
        {
            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tripCmdService = tripCommandService ?? throw new ArgumentNullException(nameof(tripCommandService));
            _tripQueryService = tripQueryService ?? throw new ArgumentNullException(nameof(tripQueryService));
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _notificationSvc = notificationSvc ?? throw new ArgumentNullException(nameof(notificationSvc));

            // Khởi tạo mảng nút điều hướng cố định sau khi các nút thành phần đã nạp xong từ designer
            _tabButtons = new Button[] { btnBooking, btnTrip, btnHistory, btnProfile };

            LoadPassengerInfo();
            InitializeTabContents();
            ShowTab("Booking");
        }

        private void InitializeChildControls()
        {
            ucBooking = new ucBooking();
            ucTrip = new ucTrip();
            ucHistory = new ucHistory();
            ucProfile = new ucProfile();

            ucBooking.Dock = DockStyle.Fill;
            ucTrip.Dock = DockStyle.Fill;
            ucHistory.Dock = DockStyle.Fill;
            ucProfile.Dock = DockStyle.Fill;
        }

        private void InitializeTabContents()
        {
            ucBooking.Initialize(_passenger, _mapService, _fareService, _passengerService, _tripCmdService, _map, this);
            ucTrip.Initialize(_passenger, _tripCmdService, _tripQueryService, _userService, _notificationSvc);
            ucHistory.InitializePassenger(_passenger, _tripQueryService, _reviewService);
            ucProfile.Initialize(_passenger, _userService);


            // KHỬ LAMBDA: Đăng ký sự kiện tường minh có đặt tên rõ ràng để chống rò rỉ RAM
            ucTrip.TripCancelled += OnTripStateChangedEvent;
            ucTrip.TripCompleted += OnTripCompletedShowRating;
            ucTrip.RequestNewTripRequired += OnRequestNewTripRequired;
        }

        // ========== QUẢN LÝ SỰ KIỆN ĐIỀU PHỐI ĐA LUỒNG TƯỜNG MINH ==========

        private async void OnTripStateChangedEvent(object sender, EventArgs e)
        {
            try
            {
                await RefreshAfterTripChange();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi cập nhật trạng thái chuyến đi: {ex.Message}");
            }
        }

        private void OnTripCompletedShowRating(object sender, EventArgs e)
        {
            // Điều hướng lập tức sang Tab lịch sử chuyến đi để người dùng chọn xem và chấm sao đánh giá
            ShowTab("History");
        }

        private void OnRequestNewTripRequired(object sender, EventArgs e)
        {
            // Đưa khách hàng quay về màn hình bản đồ và đặt xe ban đầu
            ShowTab("Booking");
        }

        private async Task RefreshAfterTripChange()
        {
            await LoadPassengerInfoAsync();

            // Ép chạy an toàn bất đồng bộ tùy theo ngữ cảnh Tab đang được bật
            if (_currentTabButton == btnTrip)
            {
                await ucTrip.RefreshAsync();
            }
            else if (_currentTabButton == btnHistory)
            {
                await ucHistory.RefreshAsync();
            }
        }

        private void LoadPassengerInfo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(LoadPassengerInfo));
                return;
            }

            if (_passenger == null) return;
            lblName.Text = _passenger.Name;
            lblPhone.Text = $"SĐT: {_passenger.Phone}";
            lblTrips.Text = $"Đã thực hiện {_passenger.TotTrip} chuyến";
        }

        /// <summary>
        /// Dùng UsrSvc để làm mới dữ liệu khách hàng, đảm bảo tính đóng gói kiến trúc hạ tầng
        /// </summary>
        private async Task LoadPassengerInfoAsync()
        {
            try
            {
                var freshUser = await _userService.RefreshUserAsync(_passenger.Id);
                if (freshUser is Psg freshPassenger)
                {
                    _passenger = freshPassenger;
                    LoadPassengerInfo();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đồng bộ thông tin khách hàng: {ex.Message}");
            }
        }

        private void Tab_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            string tabName = btn.Tag?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(tabName))
            {
                ShowTab(tabName);
            }
        }

        /// <summary>
        /// Bộ điều phối Tab thông minh - Giải phóng vùng nhớ hiển thị tránh Overlap Layout
        /// </summary>
        public void ShowTab(string tabName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ShowTab(tabName)));
                return;
            }

            ResetTabStyles();

            Button activeButton = null;
            UserControl activeControl = null;

            switch (tabName)
            {
                case "Booking":
                    activeButton = btnBooking;
                    activeControl = ucBooking;
                    break;
                case "Trip":
                    activeButton = btnTrip;
                    activeControl = ucTrip;
                    break;
                case "History":
                    activeButton = btnHistory;
                    activeControl = ucHistory;
                    break;
                case "Profile":
                    activeButton = btnProfile;
                    activeControl = ucProfile;
                    break;
                default:
                    return;
            }

            // Đổi phong cách giao diện hiển thị cho nút đang hoạt động (Đồng bộ Colors.Green với Panel tiêu đề)
            activeButton.BackColor = System.Drawing.Color.FromArgb(13, 190, 123);
            activeButton.ForeColor = Color.White;
            _currentTabButton = activeButton;

            // FIX UI OVERLAP: Dọn sạch các tiến trình giao diện cũ đang neo trên khung nội dung trước khi nạp mới
            if (pnlContent != null)
            {
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(activeControl);
            }

            // Kích hoạt nạp lại luồng dữ liệu tức thời tương ứng với từng UserControl đích bất đồng bộ
            if (activeControl == ucTrip)
            {
                _ = ucTrip.RefreshAsync();
            }
            else if (activeControl == ucHistory)
            {
                _ = ucHistory.RefreshAsync();
            }
            else if (activeControl == ucProfile)
            {
                _ = ucProfile.RefreshProfileAsync();
            }
        }

        private void ResetTabStyles()
        {
            if (_tabButtons == null) return;

            // Duyệt vòng lặp truyền thống để cấu trúc màu sắc sạch, loại bỏ hoàn toàn LINQ
            for (int i = 0; i < _tabButtons.Length; i++)
            {
                if (_tabButtons[i] != null)
                {
                    _tabButtons[i].BackColor = Color.White;
                    _tabButtons[i].ForeColor = Color.Gray;
                }
            }
        }

        public async Task RefreshPanelAsync()
        {
            await LoadPassengerInfoAsync();
            if (_currentTabButton == btnTrip)
            {
                await ucTrip.RefreshAsync();
            }
            else if (_currentTabButton == btnHistory)
            {
                await ucHistory.RefreshAsync();
            }
        }

        // ========== BỘ TIÊU HỦY ĐỒ HỌA CHỦ ĐỘNG KHI KHÔNG CÒN SỬ DỤNG MÀN HÌNH ==========

        private void UcPassengerHome_Disposed(object sender, EventArgs e)
        {
            // Ngắt toàn bộ liên kết sự kiện tránh lỗi rò rỉ bộ nhớ (Memory Leak) do Con trỏ chết
            if (ucTrip != null)
            {
                ucTrip.TripCancelled -= OnTripStateChangedEvent;
                ucTrip.TripCompleted -= OnTripCompletedShowRating;
                ucTrip.RequestNewTripRequired -= OnRequestNewTripRequired;
                ucTrip.Dispose();
            }

            if (ucBooking != null) ucBooking.Dispose();
            if (ucHistory != null) ucHistory.Dispose();
            if (ucProfile != null) ucProfile.Dispose();
        }
    }
}
