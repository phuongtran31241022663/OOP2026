using GMap.NET;

namespace OOP2026
{
	public partial class ucBooking : UserControl
	{
		private Psg _passenger;
		private IMapSvc _mapService;
		private IFareSvc _fareService;
		private IPsgSvc _passengerService;
		private ITripCmd _tripCmdService; // Assigned during Initialize
		private ucMap _map;
		private ucPassengerHome _parentPanel;
		private ucLocationPicker _activePicker; // Tracks the picker currently receiving map clicks

		private Loc _pickupLocation;
		private Loc _dropoffLocation;
		private VehicleType _selectedVehicleType = VehicleType.Moto;
		private Route _currentRoute;
		private int _requestId = 0;

		public bool HasValidPickup => _pickupLocation != null;
		public bool HasValidDropoff => _dropoffLocation != null;

		private const double DefaultCenterLat = 10.762622;
		private const double DefaultCenterLng = 106.660172;

		public ucBooking()
		{
			InitializeComponent();
		}

		// Adds ITripCmd to keep navigation events synchronized
		public void Initialize(Psg passenger, IMapSvc mapService, IFareSvc fareService,
			IPsgSvc passengerService, ITripCmd tripCmdService, ucMap map, ucPassengerHome parentPanel)
		{
			_passenger = passenger ?? throw new ArgumentNullException(nameof(passenger));
			_mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
			_fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
			_passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
			_tripCmdService = tripCmdService ?? throw new ArgumentNullException(nameof(tripCmdService));
			_map = map;
			_parentPanel = parentPanel;

			ucPickupPicker.SetMapService(_mapService);
			ucDropoffPicker.SetMapService(_mapService);

			// Pickup is the default target for map click selection
			_activePicker = ucPickupPicker;
			ucPickupPicker.Enter += (s, e) => _activePicker = ucPickupPicker;
			ucDropoffPicker.Enter += (s, e) => _activePicker = ucDropoffPicker;

			if (ucPickupPicker != null)
				ucPickupPicker.AddressSelected += PickupPicker_AddressSelected;
			if (ucDropoffPicker != null)
				ucDropoffPicker.AddressSelected += DropoffPicker_AddressSelected;

			if (_map != null)
			{
				_map.SetMapService(_mapService);
				_map.SetSelectionMode(true);
				_map.MapClicked += OnMapLocationSelected;
			}
		}

		private async void PickupPicker_AddressSelected(object sender, LocationSelectedEventArgs e)
		{
			if (e == null) return;
			if (e.Loc == null && string.IsNullOrEmpty(e.Addr)) return;

			double? lat = null, lng = null;
			if (e.Loc.HasValue)
			{
				lat = e.Loc.Value.Lat;
				lng = e.Loc.Value.Lng;
			}

			_pickupLocation = await _mapService.EnsureLocationAsync(e.Addr, lat, lng);
			if (_pickupLocation != null)
			{
				_map?.SetPickup(_pickupLocation);
				await UpdateRouteAsync();
			}
		}

		private async void DropoffPicker_AddressSelected(object sender, LocationSelectedEventArgs e)
		{
			if (e == null) return;
			if (e.Loc == null && string.IsNullOrEmpty(e.Addr)) return;

			double? lat = null, lng = null;
			if (e.Loc.HasValue)
			{
				lat = e.Loc.Value.Lat;
				lng = e.Loc.Value.Lng;
			}

			_dropoffLocation = await _mapService.EnsureLocationAsync(e.Addr, lat, lng);
			if (_dropoffLocation != null)
			{
				_map?.SetDestination(_dropoffLocation);
				await UpdateRouteAsync();
			}
		}

		private async void OnMapLocationSelected(ucMap mapControl, Loc selectedLocation)
		{
			if (_mapService == null || _activePicker == null || selectedLocation == null) return;

			int currentRequestId = ++_requestId;
			Loc location = selectedLocation;
			if (location.Addr == null || string.IsNullOrWhiteSpace(location.Addr.ToString()))
			{
				location = await _mapService.GetAddressAsync(selectedLocation.Coord.Latitude, selectedLocation.Coord.Longitude);
			}

			if (_requestId != currentRequestId) return;
			if (location == null) return;

			PointLatLng point = new PointLatLng(location.Coord.Latitude, location.Coord.Longitude);
			if (_activePicker == ucPickupPicker)
			{
				_pickupLocation = location;
				await ucPickupPicker.SetLocationFromCoordinates(point, _mapService);
				_map?.SetPickup(location);
			}
			else
			{
				_dropoffLocation = location;
				await ucDropoffPicker.SetLocationFromCoordinates(point, _mapService);
				_map?.SetDestination(location);
			}
			await UpdateRouteAsync();
		}

		private void BtnClearPickup_Click(object sender, EventArgs e)
		{
			_pickupLocation = null;
			_map?.SetPickup(null);
			ucPickupPicker.Clear();
			_currentRoute = null;
			ucFareSelector.SetPrices(0, 0);
			lblEstimateInfo.Text = "--- km --- phút";
		}

		private void BtnClearDropoff_Click(object sender, EventArgs e)
		{
			_dropoffLocation = null;
			_map?.SetDestination(null);
			ucDropoffPicker.Clear();
			_currentRoute = null;
			ucFareSelector.SetPrices(0, 0);
			lblEstimateInfo.Text = "--- km --- phút";
		}

		private async Task UpdateRouteAsync()
		{
			if (_pickupLocation == null || _dropoffLocation == null)
			{
				lblEstimateInfo.Text = "--- km --- phút";
				ucFareSelector.SetPrices(0, 0);
				_currentRoute = null;
				_map?.ClearRoute();
				return;
			}
			try
			{
				var route = await _mapService.GetDirectionsAsync(_pickupLocation, _dropoffLocation);
				if (route == null)
				{
					lblEstimateInfo.Text = "Không tìm thấy đường đi";
					ucFareSelector.SetPrices(0, 0);
					_currentRoute = null;
					_map?.ClearRoute();
					return;
				}
				_currentRoute = route;
				_map?.DrawRoute(route.Polyline);

				var motorbikeFare = await _fareService.CalculateFareAsync(VehicleType.Moto, route.Distance);
				var carFare = await _fareService.CalculateFareAsync(VehicleType.Car, route.Distance);

				lblEstimateInfo.Text = $"{Math.Ceiling(route.Duration.TotalMinutes)} phút • {route.Distance:F1} km";
				ucFareSelector.SetPrices(motorbikeFare.TotalAmount, carFare.TotalAmount);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"UpdateRoute error: {ex.Message}");
				lblEstimateInfo.Text = "Lỗi tính đường";
				ucFareSelector.SetPrices(0, 0);
				_currentRoute = null;
				_map?.ClearRoute();
			}
		}

		private async void BtnRequestTrip_Click(object sender, EventArgs e)
		{
			if (_passenger == null) return;

			if (_pickupLocation == null || _dropoffLocation == null || _currentRoute == null)
			{
				MessageBox.Show("Vui lòng chọn điểm đón và điểm đến hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			btnRequestTrip.Enabled = false;

			try
			{
				var trip = await _passengerService.RequestTripAsync(_passenger.Id, _pickupLocation, _dropoffLocation, _selectedVehicleType);
				_parentPanel?.ShowTab("Trip");
				ShowSafeNotification("Đã gửi yêu cầu", "Hệ thống đang tìm kiếm tài xế gần nhất!");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi đặt xe: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnRequestTrip.Enabled = true;
			}
		}

		private void ShowSafeNotification(string title, string message)
		{
			NotifyIcon sysNotify = new NotifyIcon();
			try
			{
				sysNotify.Icon = SystemIcons.Information;
				sysNotify.Visible = true;
				sysNotify.ShowBalloonTip(2000, title, message, ToolTipIcon.Info);

				System.Windows.Forms.Timer cleanupTimer = new System.Windows.Forms.Timer();
				cleanupTimer.Interval = 4000;
				cleanupTimer.Tick += (s, e) =>
				{
					cleanupTimer.Stop();
					cleanupTimer.Dispose();
					sysNotify.Visible = false;
					sysNotify.Dispose();
				};
				cleanupTimer.Start();
			}
			catch
			{
				sysNotify.Dispose();
			}
		}

		private async void BtnLocatePickup_Click(object sender, EventArgs e)
		{
			if (_mapService == null) return;
			var point = new PointLatLng(DefaultCenterLat, DefaultCenterLng);
			await ucPickupPicker.SetLocationFromCoordinates(point, _mapService);
			await UpdateRouteAsync();
		}

		private async void BtnLocateDropoff_Click(object sender, EventArgs e)
		{
			if (_mapService == null) return;
			var point = new PointLatLng(DefaultCenterLat, DefaultCenterLng);
			await ucDropoffPicker.SetLocationFromCoordinates(point, _mapService);
			await UpdateRouteAsync();
		}

		private void ucFareSelector_SelectionChanged(object sender, VehicleType type)
		{
			_selectedVehicleType = type;
		}

		public void ResetForm()
		{
			_pickupLocation = null;
			_dropoffLocation = null;
			_currentRoute = null;
			ucPickupPicker.Clear();
			ucDropoffPicker.Clear();
			_map?.ClearRoute();
			_map?.SetPickup(null);
			_map?.SetDestination(null);
			lblEstimateInfo.Text = "--- km --- phút";
			ucFareSelector.SetPrices(0, 0);
			btnRequestTrip.Enabled = true;
		}
	}
}
