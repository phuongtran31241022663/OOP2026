using Application.Interfaces;
using Application.Events;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Constants;
using Presentation.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation.Base;

namespace Presentation.UserControls
{
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
        private UcReview _ucRating;
private List<UcTripCard> _tripCards = new List<UcTripCard>();
        private Presentation.Components.UcVehicleFareSelector vehicleFareSelector;
        private string _selectedVehicleType;

        private void ShowStage(string status)
        {
            pnlBooking.Visible = false;
            pnlSearching.Visible = false;
            pnlTracking.Visible = false;
            pnlPayment.Visible = false;
            pnlHistory.Visible = false;

            Panel activePanel = null;

            if (status == null || status == "Idle" || status == "Timeout" || status == "Cancelled")
            {
                pnlBooking.Visible = true;
                lblStatus.Text = "Sẵn sàng đặt xe";
                lblStatus.ForeColor = UiConstants.Colors.TextMuted;
                activePanel = pnlBooking;
                _currentTrip = null;
                ShowStageIdle();
            }

            else if (status == "Searching")
            {
                pnlSearching.Visible = true;
                lblStatus.Text = "Đang tìm tài xế…";
                lblStatus.ForeColor = UiConstants.Colors.Warning;
                activePanel = pnlSearching;
            }
            else if (status == "Matched" || status == "Arrived" || status == "Started")
            {
                pnlTracking.Visible = true;
                btnCancelTrip.Enabled = (status != "Started");
                lblStatus.Text = "Trạng thái: " + status;
                lblStatus.ForeColor = UiConstants.Colors.Info;
                activePanel = pnlTracking;
            }
            else if (status == "Completed")
            {
                pnlPayment.Visible = true;
                if (_currentTrip != null && _currentTrip.TripFare != null)
                    lblTotalAmount.Text = _currentTrip.TripFare.TotalAmount.Amount.ToString("N0") + "đ";
                lblStatus.Text = "Chuyến đi đã hoàn thành";
                lblStatus.ForeColor = UiConstants.Colors.Success;
                activePanel = pnlPayment;
            }

            if (activePanel != null)
            {
                activePanel.BringToFront();
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

            // Style headers
            lblPickup.Font = Presentation.Constants.UiConstants.Typography.Small;
            lblPickup.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;
            lblDestination.Font = Presentation.Constants.UiConstants.Typography.Small;
            lblDestination.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;

            vehicleFareSelector = new UcVehicleFareSelector();
            pnlBooking.Controls.Add(vehicleFareSelector);
            vehicleFareSelector.Dock = DockStyle.Top;
            vehicleFareSelector.Height = 300;
            vehicleFareSelector.BringToFront(); // Ensure cards are visible
            vehicleFareSelector.SetServices(_mapService, _fareService);
            vehicleFareSelector.VehicleSelected += OnVehicleSelected;
            vehicleFareSelector.VehicleTypeChanged += (type) => OnVehicleSelected(null, type);

            // Initialize integrated unused controls
            InitializeIntegratedControls();

            // Populate location pickers
            pickupPicker.SetMapService(_mapService);
            destinationPicker.SetMapService(_mapService);
            pickupPicker.PopulateDropdown(_passenger.Id.ToString());
            destinationPicker.PopulateDropdown(_passenger.Id.ToString());

            // Initialize map control with map service for POI and IP location
            mapControl.SetMapService(_mapService);

            lblPassengerName.Text = "Xin chào, " + _passenger.Name;

            ShowStage(null);

            pickupPicker.Click += PickupPicker_Click;
            destinationPicker.Click += DestinationPicker_Click;

            // Subscribe location selection events for fare estimation
            pickupPicker.LocationSelected += OnLocationSelected;
            destinationPicker.LocationSelected += OnLocationSelected;

            // Subscribe map click for location selection
            mapControl.MapClicked += OnMapClicked;

            // Subscribe map drag events for interactive UI
            mapControl.MapDragStarted += (s, e) => {
                if (pnlBooking.Visible) pnlBooking.Hide();
            };
            mapControl.MapDragEnded += (s, e) => {
                if (!pnlBooking.Visible && _currentTrip == null) pnlBooking.Show();
            };



            btnBook.Click += btnBook_Click;
            btnHistory.Click += btnHistory_Click;
            btnLogout.Click += btnLogout_Click;
            btnProfile.Click += btnProfile_Click;
            btnMenu.Click += btnMenu_Click;
            btnRateDriver.Click += (s, e) => ShowRatingForm();

            // Style Sidebar Buttons
            StyleSidebarButtons();

            _tripService.TripStatusChanged += OnTripStatusChanged;
                Disposed += (s, e) =>
                {
                    _tripService.TripStatusChanged -= OnTripStatusChanged;
                    pickupPicker.LocationSelected -= OnLocationSelected;
                    destinationPicker.LocationSelected -= OnLocationSelected;
                    if (vehicleFareSelector != null)
                        vehicleFareSelector.VehicleSelected -= OnVehicleSelected;
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
            // Fix [High 3]: Explicit type instead of var
            Location otherLocation = isPickup ? destinationPicker.SelectedLocation : pickupPicker.SelectedLocation;
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
            // Fix [Medium 1]: Replace lambda RemoveAll with manual loop
            for (int i = _recentLocations.Count - 1; i >= 0; i--)
            {
                if (IsSameLocation(_recentLocations[i], location))
                {
                    _recentLocations.RemoveAt(i);
                }
            }
            
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


        private void OnVehicleSelected(object sender, string vehicleType)
        {
            _selectedVehicleType = vehicleType;
            btnBook.Text = $"Đặt xe {vehicleType}";
            btnBook.Enabled = true;
            lblStatus.Text = $"Đã chọn {vehicleType}";
        }



        private async Task UpdateFareEstimateAsync()
        {
            if (pickupPicker.SelectedLocation == null || destinationPicker.SelectedLocation == null)
            {
                return;
            }
            lblStatus.Text = $"Khoảng cách ước tính sẽ hiển thị khi chọn đầy đủ điểm";


            try
            {
                await vehicleFareSelector.UpdateFaresAsync(
                    pickupPicker.SelectedLocation,
                    destinationPicker.SelectedLocation);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Không thể tính giá cước: " + ex.Message;
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
                // Fix [High 3]: Explicit type instead of var
                List<Trip> trips = await _tripService.GetTripsByPassengerAsync(_passenger.Id);
                _flowHistory.Controls.Clear();
                _tripCards.Clear();
                
                if (trips.Count == 0)
                {
                    lblStatus.Text = "Chưa có chuyến đi nào";
                    return;
                }
                
                foreach (Trip trip in trips)
                {
                    // Fix [High 3]: Explicit type instead of var
                    UcTripCard tripCard = new UcTripCard();
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

        private void StyleSidebarButtons()
        {
            foreach (Control c in pnlSidebar.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = UiConstants.Typography.Body;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(10, 0, 0, 0);
                    btn.ForeColor = UiConstants.Colors.TextPrimary;
                    
                    if (btn == btnLogout)
                    {
                        btn.ForeColor = UiConstants.Colors.Danger;
                    }
                }
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
                btnMenu.TextAlign = ContentAlignment.MiddleLeft;
            }
            else
            {
                pnlSidebar.Width = 50;
                btnMenu.Text = "☰";
                btnHistory.Text = "🕒";
                btnProfile.Text = "👤";
                btnLogout.Text = "⏻";
                btnMenu.TextAlign = ContentAlignment.MiddleCenter;
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
            if (string.IsNullOrEmpty(_selectedVehicleType))
            {
                ShowWarning("Vui lòng chọn loại xe.");
                return;
            }

            IsLoading = true;
            try
            {
                Trip trip = await _tripService.RequestTripAsync(
                    _passenger.Id,
                    pickupPicker.SelectedLocation,
                    destinationPicker.SelectedLocation,
                    _selectedVehicleType);

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

        private void ShowStageIdle()
        {
            _selectedVehicleType = null;
            btnBook.Text = "Đặt xe";
            btnBook.Enabled = false;
            if (vehicleFareSelector != null)
                vehicleFareSelector.ClearFare();

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
            
            // Fix [Critical 2] & [Medium 3]: Overlay instead of clear, and dispose old
            if (_ucRating != null)
            {
                _ucRating.Dispose();
            }
            _ucRating = new UcReview(_reviewService, _currentTrip);
            
            // Add to payment panel as overlay
            _ucRating.Dock = DockStyle.Fill;
            pnlPayment.Controls.Add(_ucRating);
            _ucRating.BringToFront();
        }

        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pickupPicker_Load(object sender, EventArgs e)
        {

        }
    }
}
