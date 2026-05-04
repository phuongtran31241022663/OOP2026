using Application.Interfaces;
using Application.Events;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Constants;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentation;

namespace Presentation.UserControls
{
    // nên thêm panel hay gì đó của đặt xe là 1 thằng trong menu, mà chữ menu không nên nằm chung ngang hàng dọc với từng chức năng, khó phân biệt
    public partial class UcPassenger : BaseUserControl
    {
        private readonly Passenger _passenger;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;
        private readonly ISimulationService _simulationService;
        private readonly IMatchingService _matchingService;
        private readonly IReviewService _reviewService;

        public event EventHandler RequestLogout;
        public event EventHandler<User> RequestShowProfile;

        private Trip _currentTrip;
        private bool _isSidebarExpanded = true;
        private bool _isPickupSlotActive = false; // true = đang chọn pickup, false = đang chọn destination
        
        // Lịch sử địa điểm (10 địa điểm gần nhất)
        private List<Location> _recentLocations = new List<Location>();
        private const int MaxRecentLocations = 10;

        // Integrated unused controls
        private FlowLayoutPanel _flowHistory;
        private UcRating _ucRating;
        private List<UcTripCard> _tripCards = new List<UcTripCard>();

        private void ShowStage(string status)
        {
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            pnlHistory.Visible = false;

            if (status == null || status == "Idle" || status == "Timeout" || status == "Cancelled")
            {
                pnlBooking.Visible = true;
                lblStatus.Text = "Sẵn sàng đặt xe";
                _currentTrip = null;
            }
            else if (status == "Searching")
            {
                pnlSearching.Visible = true;
                lblStatus.Text = "Đang tìm tài xế…";
            }
            else if (status == "Matched" || status == "Arrived" || status == "Started")
            {
                pnlTracking.Visible = true;
                btnCancelTrip.Enabled = (status != "Started");
                lblStatus.Text = "Trạng thái: " + status;
            }
            else if (status == "Completed")
            {
                pnlPayment.Visible = true;
                if (_currentTrip != null && _currentTrip.TripFare != null)
                    lblTotalAmount.Text = _currentTrip.TripFare.TotalAmount.Amount.ToString("N0") + "đ";
                lblStatus.Text = "Chuyến đi đã hoàn thành";
            }
        }

        public UcPassenger(
            Passenger passenger,
            ITripService tripService,
            IUserService userService,
            IMapService mapService,
            IFareService fareService,
            ISimulationService simulationService,
            IMatchingService matchingService,
            IReviewService reviewService)
        {
            _passenger = passenger;
            _tripService = tripService;
            _userService = userService;
            _mapService = mapService;
            _fareService = fareService;
            _simulationService = simulationService;
            _matchingService = matchingService;
            _reviewService = reviewService;
            InitializeComponent();

            // Initialize integrated unused controls
            InitializeIntegratedControls();
            
            // Populate location pickers
            pickupPicker.SetMapService(_mapService);
            destinationPicker.SetMapService(_mapService);
            pickupPicker.PopulateDropdown(_passenger.Id.ToString());
            destinationPicker.PopulateDropdown(_passenger.Id.ToString());

            lblPassengerName.Text = "Xin chào, " + _passenger.Name;

            ShowStage(null);

            pickupPicker.Click += PickupPicker_Click;
            destinationPicker.Click += DestinationPicker_Click;

            // Subscribe location selection events for fare estimation
            pickupPicker.LocationSelected += OnLocationSelected;
            destinationPicker.LocationSelected += OnLocationSelected;

            // Subscribe map click for location selection
            mapControl.MapClicked += OnMapClicked;

            // Subscribe vehicle type change for fare estimation
            cmbVehicleType.SelectedIndexChanged += OnVehicleTypeChanged;

            btnBook.Click += btnBook_Click;
            btnHistory.Click += btnHistory_Click;
            btnLogout.Click += btnLogout_Click;
            btnProfile.Click += btnProfile_Click;
            btnMenu.Click += btnMenu_Click;
            btnRateDriver.Click += (s, e) => ShowRatingForm();

            _tripService.TripStatusChanged += OnTripStatusChanged;
            Disposed += (s, e) =>
            {
                _tripService.TripStatusChanged -= OnTripStatusChanged;
                pickupPicker.LocationSelected -= OnLocationSelected;
                destinationPicker.LocationSelected -= OnLocationSelected;
                cmbVehicleType.SelectedIndexChanged -= OnVehicleTypeChanged;
                mapControl.MapClicked -= OnMapClicked;
            };

            // Fix Bug 2: Collapse sidebar ngay khi khởi tạo
            _isSidebarExpanded = true;
            btnMenu_Click(this, EventArgs.Empty);
        }

        private void PickupPicker_Click(object sender, EventArgs e)
        {
            _isPickupSlotActive = true;
            lblStatus.Text = "Click trên bản đồ để chọn điểm đón, hoặc chọn từ danh sách";
            mapControl.EnableLocationSelection(true);
        }

        private void DestinationPicker_Click(object sender, EventArgs e)
        {
            _isPickupSlotActive = false;
            lblStatus.Text = "Click trên bản đồ để chọn điểm đến, hoặc chọn từ danh sách";
            mapControl.EnableLocationSelection(true);
        }

        private void OnMapClicked(UcMap sender, Domain.ValueObjects.Location location)
        {
            // Sử dụng hàm trung tâm ApplyLocation
            ApplyLocation(location, _isPickupSlotActive);
            
            // Tắt chế độ chọn sau khi đã chọn
            mapControl.EnableLocationSelection(false);
        }

        private void OnLocationSelected(object sender, UcLocationPicker.LocationSelectedEventArgs e)
        {
            bool isPickup = (sender == pickupPicker);
            ApplyLocation(e.Location, isPickup);
        }

        /// <summary>
        /// Hàm trung tâm ApplyLocation - "Trái tim" của quá trình chọn địa điểm
        /// </summary>
        private void ApplyLocation(Location location, bool isPickup)
        {
            if (location == null) return;

            // 1. Xác thực: Kiểm tra điểm đón/đến có trùng nhau không
            var otherLocation = isPickup ? destinationPicker.SelectedLocation : pickupPicker.SelectedLocation;
            if (otherLocation != null && IsSameLocation(location, otherLocation))
            {
                lblStatus.Text = "Điểm đón và đến không được trùng nhau!";
                ShowWarning("Vui lòng chọn điểm đến khác với điểm đón.");
                return;
            }

            // 2. Cập nhật UI: Gọi SetPickup hoặc SetDestination
            if (isPickup)
            {
                pickupPicker.SetSelectedLocation(location);
                _isPickupSlotActive = false; // Reset trạng thái
            }
            else
            {
                destinationPicker.SetSelectedLocation(location);
                _isPickupSlotActive = true; // Reset về pickup cho lần sau
            }

            // 3. Đánh dấu bản đồ
            if (isPickup)
                mapControl.SetPickup(location);
            else
                mapControl.SetDestination(location);

            // 4. Lưu lịch sử: Cập nhật danh sách 10 địa điểm gần nhất
            SaveToHistory(location);

            // 5. Cập nhật danh sách gợi ý cho cả 2 picker
            RefreshRecentLocations();

            // 6. Tính giá cước & hiển thị nút đặt xe
            _ = UpdateFareEstimateAsync();

            lblStatus.Text = $"Đã chọn {(isPickup ? "điểm đón" : "điểm đến")}: {location.Address?.District ?? "Unknown"}";
        }

        /// <summary>
        /// Kiểm tra 2 địa điểm có trùng nhau không (so sánh tọa độ)
        /// </summary>
        private bool IsSameLocation(Location a, Location b)
        {
            if (a?.Coordinate == null || b?.Coordinate == null) return false;
            
            const double tolerance = 0.0001; // ~10m
            return Math.Abs(a.Coordinate.Latitude - b.Coordinate.Latitude) < tolerance &&
                   Math.Abs(a.Coordinate.Longitude - b.Coordinate.Longitude) < tolerance;
        }

        /// <summary>
        /// Lưu địa điểm vào lịch sử (max 10 items)
        /// </summary>
        private void SaveToHistory(Location location)
        {
            if (location == null) return;

            // Xóa nếu đã tồn tại (đưa lên đầu)
            _recentLocations.RemoveAll(l => IsSameLocation(l, location));
            
            // Thêm vào đầu danh sách
            _recentLocations.Insert(0, location);
            
            // Giới hạn 10 items
            if (_recentLocations.Count > MaxRecentLocations)
                _recentLocations.RemoveAt(_recentLocations.Count - 1);
        }

        /// <summary>
        /// Cập nhật danh sách gợi ý cho cả 2 picker
        /// </summary>
        private void RefreshRecentLocations()
        {
            // Cập nhật cho pickup picker
            pickupPicker.SetRecentLocations(_recentLocations);
            
            // Cập nhật cho destination picker
            destinationPicker.SetRecentLocations(_recentLocations);
        }

        private void OnVehicleTypeChanged(object sender, EventArgs e)
        {
            // Cập nhật giá cước khi thay đổi loại xe
            // Fire-and-forget: không await vì cập nhật UI async, không ảnh hưởng logic chính
            UpdateFareEstimateAsync();
        }

        private async Task UpdateFareEstimateAsync()
        {
            // Kiểm tra đã chọn đủ 2 điểm và loại xe chưa
            if (pickupPicker.SelectedLocation == null || destinationPicker.SelectedLocation == null)
            {
                farePanel.ClearFare();
                return;
            }

            if (cmbVehicleType.SelectedIndex < 0)
            {
                farePanel.ClearFare();
                return;
            }

            try
            {
                // Lấy tọa độ và tính khoảng cách
                var route = await _mapService.GetRouteAsync(
                    pickupPicker.SelectedLocation,
                    destinationPicker.SelectedLocation);

                // Xác định loại xe
                VehicleType vehicleType = cmbVehicleType.SelectedIndex == 0
                    ? VehicleType.Motorbike
                    : VehicleType.Car;

                // Tính giá cước
                var fare = await _fareService.CalculateFareAsync(vehicleType, route.Distance);

                // Hiển thị giá
                RunOnUI(() =>
                {
                    farePanel.SetFare(fare.TotalAmount.Amount);
                    lblStatus.Text = $"Khoảng cách: {route.Distance:F1} km - Thời gian ước tính: {route.Duration.TotalMinutes:F0} phút";
                });
            }
            catch (Exception ex)
            {
                RunOnUI(() =>
                {
                    farePanel.ClearFare();
                    lblStatus.Text = "Không thể tính giá cước: " + ex.Message;
                });
            }
        }

        private void btnLogout_Click(object sender, EventArgs e) => RequestLogout?.Invoke(this, EventArgs.Empty);
        private void btnProfile_Click(object sender, EventArgs e) => RequestShowProfile?.Invoke(this, _passenger);
        
        private async void btnHistory_Click(object sender, EventArgs e)
        {
            // Show history with TripCard integration
            pnlHistory.Visible = true;
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            lblStatus.Text = "Đang tải lịch sử chuyến đi...";
            
            // Switch between dgvHistory and _flowHistory
            dgvHistory.Visible = false;
            _flowHistory.Visible = true;
            _flowHistory.BringToFront();
            
            try
            {
                var trips = await _tripService.GetTripsByPassengerAsync(_passenger.Id);
                _flowHistory.Controls.Clear();
                _tripCards.Clear();
                
                if (trips.Count == 0)
                {
                    lblStatus.Text = "Chưa có chuyến đi nào";
                    return;
                }
                
                foreach (var trip in trips)
                {
                    var tripCard = new UcTripCard();
                    tripCard.SetTrip(trip);
                    tripCard.Clicked += OnTripCardClicked;
                    _tripCards.Add(tripCard);
                    _flowHistory.Controls.Add(tripCard);
                }
                
                lblStatus.Text = $"Có {trips.Count} chuyến đi";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tải lịch sử: " + ex.Message;
            }
        }
        
        private void OnTripCardClicked(UcTripCard card)
        {
            if (card.DisplayedTrip != null)
            {
                _currentTrip = card.DisplayedTrip;
                ShowInfo($"Đã chọn chuyến đi: {card.DisplayedTrip.Id}");
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            _isSidebarExpanded = !_isSidebarExpanded;
            if (_isSidebarExpanded)
            {
                pnlSidebar.Width = 200;
                btnMenu.Text = "☰  Menu";
                btnHistory.Text = "🕒  Lịch sử";
                btnProfile.Text = "👤  Hồ sơ";
                btnLogout.Text = "⏻  Thoát";
            }
            else
            {
                pnlSidebar.Width = 50;
                btnMenu.Text = "☰";
                btnHistory.Text = "🕒";
                btnProfile.Text = "👤";
                btnLogout.Text = "⏻";
            }
        }
        private async void btnBook_Click(object sender, EventArgs e)
        {
            if (pickupPicker.SelectedLocation == null)
            {
                ShowWarning("Vui lòng chọn điểm đón.");
                return;
            }
            if (destinationPicker.SelectedLocation == null)
            {
                ShowWarning("Vui lòng chọn điểm đến.");
                return;
            }
            if (cmbVehicleType.SelectedIndex < 0)
            {
                ShowWarning("Vui lòng chọn loại xe.");
                return;
            }

            VehicleType vehicleType = cmbVehicleType.SelectedIndex == 0
                ? VehicleType.Motorbike
                : VehicleType.Car;

            IsLoading = true;
            try
            {
                Trip trip = await _tripService.RequestTripAsync(
                    _passenger.Id,
                    pickupPicker.SelectedLocation,
                    destinationPicker.SelectedLocation,
                    vehicleType);

                _currentTrip = trip;
                ShowStage("Searching");
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, "Đặt xe");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
        {
            RunOnUI(() =>
            {
                if (_currentTrip != null && _currentTrip.Id == e.TripId)
                {
                    ShowStage(e.NewStatus);
                }
            });
        }
        
        private void InitializeIntegratedControls()
        {
            // NOTE: StatusPanel removed from runtime initialization to avoid layout overlap
            // If needed, add it properly in UcPassenger.Designer.cs instead
            // _statusPanel field kept for potential future use
            
            // Initialize FlowLayoutPanel for history TripCards inside pnlHistory
            _flowHistory = new FlowLayoutPanel();
            _flowHistory.Dock = DockStyle.Fill;
            _flowHistory.FlowDirection = FlowDirection.TopDown;
            _flowHistory.WrapContents = false;
            _flowHistory.AutoScroll = true;
            _flowHistory.Padding = new Padding(5);
            
            // Add FlowLayoutPanel alongside dgvHistory instead of clearing
            // Both can coexist - hide dgvHistory when showing TripCards
            if (pnlHistory != null)
            {
                _flowHistory.Visible = false; // Hidden by default, shown when needed
                pnlHistory.Controls.Add(_flowHistory);
            }
        }
        
        // Show rating form when clicking rate button
        private void ShowRatingForm()
        {
            if (_currentTrip == null)
            {
                ShowWarning("Vui lòng chọn chuyến đi để đánh giá.");
                return;
            }
            
            _ucRating = new UcRating(_reviewService, _currentTrip);
            
            // Add to payment panel
            pnlPayment.Controls.Clear();
            pnlPayment.Controls.Add(_ucRating);
            _ucRating.Dock = DockStyle.Fill;
        }

        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pickupPicker_Load(object sender, EventArgs e)
        {

        }
    }
}
