using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public enum LocationPickerMode
    {
        Pickup,
        Dropoff
    }

    public partial class ucBooking : UserControl
    {
        #region Fields

        private Psg _passenger;
        private IMapSvc _mapService;
        private IFareSvc _fareService;
        private IPsgSvc _passengerService;
        private ITripCmd _tripCmdService;
        private ucMap _map;
        private ucPassengerHome _parentPanel;

        private Loc _pickupLocation;
        private Loc _dropoffLocation;

        private VehicleType _selectedVehicleType = VehicleType.Moto;
        private Route _currentRoute;

        private int _reverseGeocodeRequestId;
        private int _searchVersion;

        private bool _suppressTextChanged;
        private bool _initialized;

        private LocationPickerMode _activePicker = LocationPickerMode.Pickup;

        private readonly NotifyIcon _notifyIcon = new NotifyIcon();

        private const double DefaultCenterLat = 10.762622;
        private const double DefaultCenterLng = 106.660172;

        #endregion

        #region Properties

        public bool HasValidPickup => _pickupLocation != null;
        public bool HasValidDropoff => _dropoffLocation != null;

        #endregion

        #region Constructor

        public ucBooking()
        {
            InitializeComponent();

            ConfigureSuggestionList(lstPickupSuggestions);
            ConfigureSuggestionList(lstDropoffSuggestions);

            lblEstimateInfo.Visible = false;
            pnlVehicleSelector.Visible = false;   // ẩn khung chọn xe cho đến khi có tuyến đường

            UpdateVehicleSelectionUI();           // đánh dấu mặc định chọn xe máy
        }

        #endregion

        #region Initialize

        public void Initialize(
            Psg passenger,
            IMapSvc mapService,
            IFareSvc fareService,
            IPsgSvc passengerService,
            ITripCmd tripCmdService,
            ucMap map,
            ucPassengerHome parentPanel)
        {
            if (_initialized)
                return;

            _passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
            _fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _tripCmdService = tripCmdService ?? throw new ArgumentNullException(nameof(tripCmdService));

            _map = map;
            _parentPanel = parentPanel;

            BindEvents();

            if (_map != null)
            {
                _map.SetMapService(_mapService);
                _map.SetSelectionMode(true);
                _map.MapClicked += OnMapLocationSelected;
            }

            _notifyIcon.Icon = SystemIcons.Information;

            _initialized = true;
        }

        #endregion

        #region Event Binding

        private void BindEvents()
        {
            txtPickup.Enter += (_, _) => SetActivePicker(LocationPickerMode.Pickup);
            txtDropoff.Enter += (_, _) => SetActivePicker(LocationPickerMode.Dropoff);

            txtPickup.Click += TxtPickup_Click;
            txtDropoff.Click += TxtDropoff_Click;

            txtPickup.TextChanged += TxtPickup_TextChanged;
            txtDropoff.TextChanged += TxtDropoff_TextChanged;

            // lstPickupSuggestions.SelectionChangeCommitted += LstPickupSuggestions_SelectionChangeCommitted;
            // lstDropoffSuggestions.SelectionChangeCommitted += LstDropoffSuggestions_SelectionChangeCommitted;

            this.Resize += (_, _) => RepositionSuggestionBoxes();
        }

        #endregion

        #region Vehicle Selection (thay thế ucFareSelector)

        private void PnlMoto_Click(object sender, EventArgs e)
        {
            _selectedVehicleType = VehicleType.Moto;
            UpdateVehicleSelectionUI();
        }

        private void PnlCar_Click(object sender, EventArgs e)
        {
            _selectedVehicleType = VehicleType.Car;
            UpdateVehicleSelectionUI();
        }

        private void UpdateVehicleSelectionUI()
        {
            bool isMoto = (_selectedVehicleType == VehicleType.Moto);
            pnlMoto.BackColor = isMoto ? Color.LightGreen : Color.White;
            pnlCar.BackColor = !isMoto ? Color.LightGreen : Color.White;
            lblMotoIcon.ForeColor = isMoto ? Color.DarkGreen : Color.Black;
            lblCarIcon.ForeColor = !isMoto ? Color.DarkGreen : Color.Black;
        }

        /// <summary>
        /// Cập nhật giá hiển thị cho cả hai loại xe.
        /// </summary>
        public void SetPrices(decimal motorbikePrice, decimal carPrice)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetPrices(motorbikePrice, carPrice)));
                return;
            }
            lblMotoPrice.Text = $"{motorbikePrice:N0}đ";
            lblCarPrice.Text = $"{carPrice:N0}đ";
        }

        #endregion

        #region Text Events

        private void TxtPickup_Click(object? sender, EventArgs e)
        {
            SetActivePicker(LocationPickerMode.Pickup);
        }

        private void TxtDropoff_Click(object? sender, EventArgs e)
        {
            SetActivePicker(LocationPickerMode.Dropoff);
        }

        private async void TxtPickup_TextChanged(object? sender, EventArgs e)
        {
            await HandleTextChangedAsync(txtPickup.Text, LocationPickerMode.Pickup);
        }

        private async void TxtDropoff_TextChanged(object? sender, EventArgs e)
        {
            await HandleTextChangedAsync(txtDropoff.Text, LocationPickerMode.Dropoff);
        }

        #endregion

        #region Suggestion Events

        private async void LstPickupSuggestions_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            await HandleSuggestionSelectedAsync(lstPickupSuggestions, LocationPickerMode.Pickup);
        }

        private async void LstDropoffSuggestions_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            await HandleSuggestionSelectedAsync(lstDropoffSuggestions, LocationPickerMode.Dropoff);
        }

        #endregion

        #region Search

        private async Task HandleTextChangedAsync(string query, LocationPickerMode mode)
        {
            if (_suppressTextChanged)
                return;

            if (_mapService == null ||
                string.IsNullOrWhiteSpace(query) ||
                query.Length < 3)
            {
                ClearSuggestions(mode);
                return;
            }

            try
            {
                int version = ++_searchVersion;

                var suggestions = await _mapService.SearchAsync(query);
                Debug.WriteLine($"[QA_LOG] Search for '{query}' returned {suggestions.Count} results.");

                if (version != _searchVersion)
                    return;

                UpdateSuggestions(suggestions, mode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search error: {ex.Message}");
            }
        }

        private async Task HandleSuggestionSelectedAsync(ListBox listBox, LocationPickerMode mode)
        {
            if (listBox.SelectedItem is not Loc selectedLocation)
                return;

            await ApplyLocationAsync(selectedLocation, mode);

            ClearSuggestions(mode);

            await UpdateRouteAsync();
        }

        private void UpdateSuggestions(List<Loc> suggestions, LocationPickerMode mode)
        {
            ListBox listBox = GetSuggestionList(mode);
            TextBox textBox = GetTextBox(mode);

            listBox.DataSource = null;
            listBox.DataSource = suggestions.ToList();

            PositionSuggestionBox(textBox, listBox);

            listBox.Visible = suggestions.Count > 0;
            listBox.BringToFront();
        }

        private void ClearSuggestions(LocationPickerMode mode)
        {
            ListBox listBox = GetSuggestionList(mode);

            listBox.DataSource = null;
            listBox.Visible = false;
        }

        #endregion

        #region Map Selection

        private async void OnMapLocationSelected(ucMap mapControl, Loc selectedLocation)
        {
            if (_mapService == null || selectedLocation == null)
                return;

            try
            {
                int requestId = ++_reverseGeocodeRequestId;

                Loc finalLocation = await _mapService.EnsureLocationAsync(
                    selectedLocation.Addr?.ToString(),
                    selectedLocation.Coord.Latitude,
                    selectedLocation.Coord.Longitude);

                if (requestId != _reverseGeocodeRequestId)
                    return;

                if (finalLocation == null)
                    return;

                await ApplyLocationAsync(finalLocation, _activePicker);

                await UpdateRouteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[QA_LOG] Map selection error: {ex.Message}");
            }
        }

        #endregion

        #region Location Apply

        private async Task ApplyLocationAsync(Loc location, LocationPickerMode mode)
        {
            _suppressTextChanged = true;

            try
            {
                if (mode == LocationPickerMode.Pickup)
                {
                    _pickupLocation = location;

                    txtPickup.Text = location.Addr.Name;
                    txtPickup.ReadOnly = true;
                    lblPickupAddressDetail.Text = location.Addr.ToString();
                    lblPickupAddressDetail.Visible = true;

                    _map?.SetPickup(location);
                }
                else
                {
                    _dropoffLocation = location;

                    txtDropoff.Text = location.Addr.Name;
                    txtDropoff.ReadOnly = true;
                    lblDropoffAddressDetail.Text = location.Addr.ToString();
                    lblDropoffAddressDetail.Visible = true;

                    _map?.SetDestination(location);
                }
            }
            finally
            {
                _suppressTextChanged = false;
            }

            await Task.CompletedTask;
        }

        #endregion

        #region Route

        private async Task UpdateRouteAsync()
        {
            if (_pickupLocation == null || _dropoffLocation == null)
            {
                ResetRouteUi();
                return;
            }

            try
            {
                var route = await _mapService.GetDirectionsAsync(
                    _pickupLocation,
                    _dropoffLocation);

                if (route == null)
                {
                    lblEstimateInfo.Visible = true;
                    lblEstimateInfo.Text = "Không tìm thấy đường đi";

                    SetPrices(0, 0);                     // <-- thay ucFareSelector.SetPrices

                    _currentRoute = null;

                    _map?.ClearRoute();

                    return;
                }

                _currentRoute = route;

                _map?.DrawRoute(route.Polyline);

                var motoFare = await _fareService.CalculateFareAsync(
                    VehicleType.Moto,
                    route.Distance);

                var carFare = await _fareService.CalculateFareAsync(
                    VehicleType.Car,
                    route.Distance);

                lblEstimateInfo.Visible = true;
                pnlVehicleSelector.Visible = true;      // <-- hiện khung chọn xe

                lblEstimateInfo.Text =
                    $"{Math.Ceiling(route.Duration.TotalMinutes)} phút • {route.Distance:F1} km";

                SetPrices(                               // <-- gọi phương thức nội bộ
                    motoFare.TotalAmount,
                    carFare.TotalAmount);
            }
            catch (ArgumentOutOfRangeException ex) when (ex.ParamName == "distance")
            {
                Debug.WriteLine($"[QA_LOG] UpdateRoute distance error: {ex.Message}");
                lblEstimateInfo.Visible = true;
                lblEstimateInfo.Text = $"Lỗi khoảng cách: {ex.Message}";
                SetPrices(0, 0);
                _currentRoute = null;
                _map?.ClearRoute();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[QA_LOG] UpdateRoute general error: {ex.Message}");

                lblEstimateInfo.Visible = true;
                lblEstimateInfo.Text = "Lỗi tính đường";

                SetPrices(0, 0);

                _currentRoute = null;

                _map?.ClearRoute();
            }
        }

        private void ResetRouteUi()
        {
            lblEstimateInfo.Visible = false;
            pnlVehicleSelector.Visible = false;         // <-- ẩn khung chọn xe

            lblEstimateInfo.Text = "--- km --- phút";

            SetPrices(0, 0);                            // <-- đặt lại giá

            _currentRoute = null;

            _map?.ClearRoute();
        }

        #endregion

        #region Buttons

        private void BtnClearPickup_Click(object sender, EventArgs e)
        {
            _pickupLocation = null;
            _map?.SetPickup(null);
            _suppressTextChanged = true;
            txtPickup.Clear();
            txtPickup.ReadOnly = false;
            lblPickupAddressDetail.Visible = false;
            _suppressTextChanged = false;
            lstPickupSuggestions.Visible = false;
            ResetRouteUi();
        }

        private void BtnClearDropoff_Click(object sender, EventArgs e)
        {
            _dropoffLocation = null;
            _map?.SetDestination(null);
            _suppressTextChanged = true;
            txtDropoff.Clear();
            txtDropoff.ReadOnly = false;
            lblDropoffAddressDetail.Visible = false;
            _suppressTextChanged = false;
            ClearSuggestions(LocationPickerMode.Dropoff);
            ResetRouteUi();
        }


        private async void BtnRequestTrip_Click(object sender, EventArgs e)
        {
            if (_passenger == null)
                return;

            if (_pickupLocation == null ||
                _dropoffLocation == null ||
                _currentRoute == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn điểm đón và điểm đến hợp lệ.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            btnRequestTrip.Enabled = false;

            try
            {
                await _passengerService.RequestTripAsync(
                    _passenger.Id,
                    _pickupLocation,
                    _dropoffLocation,
                    _selectedVehicleType);

                _parentPanel?.ShowTab("Trip");

                ShowNotification(
                    "Đã gửi yêu cầu",
                    "Hệ thống đang tìm kiếm tài xế gần nhất!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(
                    $"Lỗi khoảng cách: {ex.Message}",
                    "Lỗi đặt xe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi đặt xe: {ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRequestTrip.Enabled = true;
            }
        }

        #endregion

        #region Notification

        private void ShowNotification(string title, string message)
        {
            try
            {
                _notifyIcon.Visible = true;

                _notifyIcon.ShowBalloonTip(
                    2000,
                    title,
                    message,
                    ToolTipIcon.Info);

                var timer = new System.Windows.Forms.Timer();

                timer.Interval = 4000;

                timer.Tick += (_, _) =>
                {
                    timer.Stop();
                    timer.Dispose();

                    _notifyIcon.Visible = false;
                };

                timer.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Notification error: {ex.Message}");
            }
        }

        #endregion

        #region Helpers

        private void ConfigureSuggestionList(ListBox listBox)
        {
            listBox.Visible = false;
            listBox.IntegralHeight = false;
        }

        private void SetActivePicker(LocationPickerMode mode)
        {
            _activePicker = mode;

            if (mode == LocationPickerMode.Pickup)
                lstDropoffSuggestions.Visible = false;
            else
                lstPickupSuggestions.Visible = false;
        }

        private TextBox GetTextBox(LocationPickerMode mode)
        {
            return mode == LocationPickerMode.Pickup
                ? txtPickup
                : txtDropoff;
        }

        private ListBox GetSuggestionList(LocationPickerMode mode)
        {
            return mode == LocationPickerMode.Pickup
                ? lstPickupSuggestions
                : lstDropoffSuggestions;
        }

        private void PositionSuggestionBox(TextBox textBox, ListBox listBox)
        {
            Point point =
                this.PointToClient(
                    textBox.Parent.PointToScreen(textBox.Location));

            listBox.Location = new Point(
                point.X,
                point.Y + textBox.Height + 2);

            listBox.Width = textBox.Width;
        }

        private void RepositionSuggestionBoxes()
        {
            PositionSuggestionBox(txtPickup, lstPickupSuggestions);
            PositionSuggestionBox(txtDropoff, lstDropoffSuggestions);
        }

        #endregion

        #region Public Methods

        public void ResetForm()
        {
            _pickupLocation = null;
            _dropoffLocation = null;
            _currentRoute = null;

            _suppressTextChanged = true;

            txtPickup.Clear();
            txtPickup.ReadOnly = false;
            lblPickupAddressDetail.Visible = false;
            
            txtDropoff.Clear();
            txtDropoff.ReadOnly = false;
            lblDropoffAddressDetail.Visible = false;

            _suppressTextChanged = false;

            ClearSuggestions(LocationPickerMode.Pickup);
            ClearSuggestions(LocationPickerMode.Dropoff);

            _map?.ClearRoute();

            _map?.SetPickup(null);
            _map?.SetDestination(null);

            ResetRouteUi();

            btnRequestTrip.Enabled = true;
        }

        #endregion
    }
}