using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace OOP2026;

public class UsrSvc : IUsrSvc
{
	#region Fields and Constructor
	private readonly IUsrRepo _userRepo;
	private readonly IVehRepo _vehicleRepo;

	public UsrSvc(IUsrRepo userRepository, IVehRepo vehicleRepository)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
	}
	#endregion

	#region Private Methods
	private async Task<Usr?> GetUserByPhoneAsync(string phone)
	{
		return await _userRepo.GetByPhoneAsync(phone).ConfigureAwait(false);
	}
	#endregion

	#region Public Methods - Authentication & Profile
	public async Task<Usr> LoginAsync(string phone, string password)
	{
		if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Vui lòng nhập số điện thoại.");
		if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Vui lòng nhập mật khẩu.");

		Usr? user = await GetUserByPhoneAsync(phone).ConfigureAwait(false);

		if (user == null)
			throw new InvalidOperationException("Số điện thoại này chưa được đăng ký trong hệ thống.");

		if (!user.VerifyPassword(password))
			throw new UnauthorizedAccessException("Mật khẩu không chính xác. Vui lòng thử lại.");

		return user;
	}
	public async Task<Psg> RegisterPassengerAsync(string name, string phone, string password)
	{
		if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Tên không được để trống.");
		if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Số điện thoại không được để trống.");
		if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
			throw new ArgumentException("Mật khẩu phải có độ dài từ 6 ký tự trở lên.");

		Usr? existingUser = await GetUserByPhoneAsync(phone).ConfigureAwait(false);
		if (existingUser != null)
			throw new InvalidOperationException("Số điện thoại này đã tồn tại trên hệ thống.");

		var passenger = UserFactory.CreatePassenger(name.Trim(), phone.Trim(), password) as Psg;
		if (passenger == null)
			throw new InvalidOperationException("Hệ thống khởi tạo tài khoản Hành khách thất bại.");

		await _userRepo.CreateAsync(passenger).ConfigureAwait(false);
		return passenger;
	}
	public async Task<Drv> RegisterDriverAsync(string name, string phone, string password, string licenseNumber, string plate, string brand, string model, string color, int capacity, VehicleType vehicleType, Loc initialPosition)
	{
		if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Tên tài xế không được để trống.");
		if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Số điện thoại không được để trống.");
		if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
			throw new ArgumentException("Mật khẩu tài xế phải từ 6 ký tự trở lên.");
		if (string.IsNullOrWhiteSpace(licenseNumber)) throw new ArgumentException("Số giấy phép lái xe không hợp lệ.");
		if (initialPosition == null) throw new ArgumentNullException(nameof(initialPosition), "Vị trí khởi tạo không được null.");
		
		Usr? existingUser = await GetUserByPhoneAsync(phone).ConfigureAwait(false);
		if (existingUser != null)
			throw new InvalidOperationException("Số điện thoại này đã được một tài xế hoặc hành khách khác sử dụng.");
		
		// 1. Tạo phương tiện trước thông qua Factory
		var vehicle = VehicleFactory.CreateVehicle(vehicleType, plate.Trim(), brand.Trim(), model.Trim(), color.Trim(), capacity);
		await _vehicleRepo.CreateAsync(vehicle).ConfigureAwait(false);
		
		// 2. Khởi tạo thực thể Tài xế thông qua Factory với vehicleId vừa tạo
		var driver = UserFactory.CreateDriver(name.Trim(), phone.Trim(), password, licenseNumber.Trim(), vehicle.Id, initialPosition) as Drv;
		if (driver == null)
		{
			// Rollback vehicle if driver creation fails
			await _vehicleRepo.DeleteAsync(vehicle.Id).ConfigureAwait(false);
			throw new InvalidOperationException("Hệ thống khởi tạo tài khoản Tài xế thất bại.");
		}
		
		await _userRepo.CreateAsync(driver).ConfigureAwait(false);
		return driver;
	}
	public async Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
	{
		if (userId == Guid.Empty) throw new ArgumentException("Định danh người dùng không hợp lệ.");
		if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
			throw new ArgumentException("Mật khẩu mới bắt buộc phải có ít nhất 6 ký tự.");

		Usr user = await _userRepo.GetByIdAsync(userId).ConfigureAwait(false)
			?? throw new InvalidOperationException("Không tìm thấy thông tin tài khoản yêu cầu đổi mật khẩu.");

		if (!user.VerifyPassword(oldPassword))
			throw new UnauthorizedAccessException("Mật khẩu hiện tại không chính xác.");

		user.ChangePassword(oldPassword, newPassword);

		// Đẩy cập nhật xuống file JSON
		await _userRepo.UpdateAsync(user).ConfigureAwait(false);
	}
	public async Task UpdateProfileAsync(Guid userId, string name, string phone)
	{
		if (userId == Guid.Empty) throw new ArgumentException("Định danh người dùng không hợp lệ.");
		if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Tên hiển thị không được để trống.");
		if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Số điện thoại không được để trống.");

		Usr user = await _userRepo.GetByIdAsync(userId).ConfigureAwait(false)
			?? throw new InvalidOperationException("Không tìm thấy hồ sơ người dùng để cập nhật.");

		string cleanedPhone = phone.Trim();
		if (user.Phone != cleanedPhone)
		{
			Usr? existingUser = await GetUserByPhoneAsync(cleanedPhone).ConfigureAwait(false);
			if (existingUser != null && existingUser.Id != userId)
				throw new InvalidOperationException("Số điện thoại mới này đã thuộc về một tài khoản khác.");
		}

		user.UpdateProfile(name.Trim(), cleanedPhone);
		await _userRepo.UpdateAsync(user).ConfigureAwait(false);
	}
	public async Task<Usr?> GetUserByIdAsync(Guid userId)
	{
		if (userId == Guid.Empty) return null;
		return await _userRepo.GetByIdAsync(userId).ConfigureAwait(false);
	}

	public async Task<Usr> RefreshUserAsync(Guid userId)
	{
		var user = await GetUserByIdAsync(userId).ConfigureAwait(false);
		if (user == null)
			throw new InvalidOperationException($"Dữ liệu tài khoản với mã {userId} không tồn tại hoặc đã bị xóa.");
		return user;
	}
	#endregion

	public async Task<string> GetDriverVehicleSummaryAsync(Guid driverId)
	{
		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver == null) return "Không tìm thấy tài xế.";

		var vehicle = await _vehicleRepo.GetByIdAsync(driver.VehId).ConfigureAwait(false);
		if (vehicle == null) return $"{driver.Name} (Không có thông tin xe)";

		return $"{driver.Name} - {vehicle.Brand} {vehicle.Model} ({vehicle.Plate})";
	}
}
public class AdmSvc : IAdmSvc
{
	#region Fields and Constructor
	private readonly IUsrRepo _userRepo;
	private readonly ITripRepo _tripRepository;
	private readonly IPolRepo _policyRepository;
	private readonly IRevRepo _reviewRepository;
	private readonly IVehRepo _vehicleRepo;
	public AdmSvc(IUsrRepo userRepository, ITripRepo tripRepository,
				IPolRepo policyRepository, IRevRepo reviewRepository, IVehRepo vehicleRepository)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
		_policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
		_reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
		_vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
	}
	#endregion
	public async Task<Pol> CreatePolicyAsync(VehicleType vehicleType, decimal baseFare, decimal pricePerKm, decimal commissionRate)
	{
		var policy = new Pol(vehicleType, baseFare, pricePerKm, commissionRate);
		await _policyRepository.CreateAsync(policy).ConfigureAwait(false);
		return policy;
	}

	#region Read all data
	public async Task<List<Usr>> GetAllUsersAsync() => await _userRepo.ReadAsync().ConfigureAwait(false);
	public async Task<List<Trip>> GetAllTripsAsync() => await _tripRepository.ReadAsync().ConfigureAwait(false);
	public async Task<List<Rev>> GetAllReviewsAsync() => await _reviewRepository.ReadAsync().ConfigureAwait(false);
	public async Task<List<Pol>> GetAllPoliciesAsync() => await _policyRepository.ReadAsync().ConfigureAwait(false);
	public async Task<List<Veh>> GetAllVehiclesAsync() => await _vehicleRepo.ReadAsync().ConfigureAwait(false);
	#endregion

	#region Thống kê tài chính
	public async Task<decimal> GetTotalRevenueAsync()
	{
		var trips = await _tripRepository.ReadAsync().ConfigureAwait(false);
		decimal totalRevenue = 0;

		foreach (Trip t in trips)
		{
			if (t != null && t.IsCompleted() && t.TripFare != null)
			{
				totalRevenue += t.TripFare.TotalAmount;
			}
		}
		return totalRevenue;
	}

	public async Task<decimal> GetTotalCommissionAsync()
	{
		var trips = await _tripRepository.ReadAsync().ConfigureAwait(false);
		decimal totalCommission = 0;

		foreach (Trip t in trips)
		{
			if (t != null && t.IsCompleted() && t.TripFare != null)
			{
				totalCommission += t.TripFare.Commission;
			}
		}
		return totalCommission;
	}
	#endregion

	#region Thống kê chuyến đi
	public async Task<(int Total, int Completed, int Cancelled, double CompletionRate)> GetTripStatisticsAsync()
	{
		var trips = await _tripRepository.ReadAsync().ConfigureAwait(false);

		int total = trips.Count;
		int completed = 0;
		int cancelled = 0;

		foreach (Trip t in trips)
		{
			if (t != null)
			{
				if (t.IsCompleted()) completed++;
				else if (t.IsCancelled()) cancelled++;
			}
		}

		double rate = total == 0 ? 0 : (double)completed / total * 100;
		return (total, completed, cancelled, rate);
	}
	#endregion
}
public class FareSvc : IFareSvc
{
	#region Field and Constructor
	private readonly IPolRepo _policyRepo;

	public FareSvc(IPolRepo policyRepo)
	{
		_policyRepo = policyRepo ?? throw new ArgumentNullException(nameof(policyRepo));
	}
	#endregion

	public async Task<Fare> CalculateFareAsync(VehicleType vehicleType, decimal distanceKm)
	{
		Pol? rule = await _policyRepo.GetLatestByVehicleTypeAsync(vehicleType).ConfigureAwait(false);

		if (rule == null)
			throw new InvalidOperationException($"Hệ thống chưa cấu hình quy tắc tính giá (Pol) cho loại phương tiện '{vehicleType}'.");

		return rule.CalculateFare(distanceKm);
	}
}
public class PsgSvc : IPsgSvc
{
	#region Fields and Constructor
	private readonly IUsrRepo _userRepo;
	private readonly ITripCmd _tripCmdService;
	private readonly ITripQry _tripQueryService;
	private readonly IRevSvc _reviewService;
	private readonly IMapSvc _mapService;
	private readonly IFareSvc _fareService;

	public PsgSvc(IUsrRepo userRepository, ITripCmd tripCommandService,
		ITripQry tripQueryService, IRevSvc reviewService, IMapSvc mapService, IFareSvc fareService)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_tripCmdService = tripCommandService ?? throw new ArgumentNullException(nameof(tripCommandService));
		_tripQueryService = tripQueryService ?? throw new ArgumentNullException(nameof(tripQueryService));
		_reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
		_mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
		_fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
	}
	#endregion

	#region Public Methods - Trip Operations
	public async Task<Trip> RequestTripAsync(Guid passengerId, Loc pickup, Loc dropoff, VehicleType vehicleType)
	{
		if (passengerId == Guid.Empty) throw new ArgumentException("Mã hành khách không hợp lệ.", nameof(passengerId));
		if (pickup == null) throw new ArgumentNullException(nameof(pickup), "Vị trí điểm đón không được để trống.");
		if (dropoff == null) throw new ArgumentNullException(nameof(dropoff), "Vị trí điểm đến không được để trống.");

		if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
			throw new ArgumentException("Loại phương tiện yêu cầu không hợp lệ trên hệ thống.", nameof(vehicleType));

		// Kiểm tra thực thể người dùng có phải hành khách không
		var user = await _userRepo.GetByIdAsync(passengerId).ConfigureAwait(false);
		if (user is not Psg)
			throw new InvalidOperationException("Tài khoản yêu cầu chuyến đi không tồn tại hoặc không có quyền hành khách.");

		// Nghiệp vụ nâng cao: Kiểm tra xem hành khách có đang kẹt trong chuyến đi nào chưa hoàn thành không
		var activeTrip = await _tripQueryService.GetActiveTripForPassengerAsync(passengerId).ConfigureAwait(false);
		if (activeTrip != null)
			throw new InvalidOperationException("Bạn hiện đang có một chuyến đi chưa hoàn thành. Không thể đặt thêm chuyến mới.");

		// Gọi dịch vụ bản đồ tính toán tuyến đường
		Route? route = await _mapService.GetDirectionsAsync(pickup, dropoff).ConfigureAwait(false);
		if (route == null)
			throw new InvalidOperationException("Hệ thống không thể tính toán được đường đi giữa hai vị trí này.");

		// Tính toán giá tiền dựa trên khoảng cách của tuyến đường
		Fare fare = await _fareService.CalculateFareAsync(vehicleType, (decimal)route.Distance).ConfigureAwait(false);

		// Đẩy lệnh tạo chuyến đi xuống dịch vụ Command
		return await _tripCmdService.CreateTripAsync(passengerId, route, fare, vehicleType).ConfigureAwait(false);
	}
	public async Task CancelTripAsync(Guid passengerId, Guid tripId, string reason)
	{
		if (passengerId == Guid.Empty) throw new ArgumentException("Mã hành khách không hợp lệ.");
		if (tripId == Guid.Empty) throw new ArgumentException("Mã chuyến đi không hợp lệ.");
		if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Vui lòng cung cấp lý do hủy chuyến.");

		var trip = await _tripQueryService.GetTripByIdAsync(tripId).ConfigureAwait(false);
		if (trip == null)
			throw new InvalidOperationException("Chuyến đi yêu cầu hủy không tồn tại.");

		if (trip.PassengerId != passengerId)
			throw new UnauthorizedAccessException("Bạn không có quyền hủy chuyến đi của người khác.");

		await _tripCmdService.CancelTripAsync(tripId, reason.Trim()).ConfigureAwait(false);
	}
	#endregion
}
public class MatchSvc : IMatchSvc
{
	#region Fields and Constructor
	private readonly InMemoryDriverGrid _driverGrid;
	private readonly ITripRepo _tripRepository;
	private readonly IUsrRepo _userRepo;
	private readonly IVehRepo _vehicleRepo;
	private readonly SemaphoreSlim _matchLock = new(1, 1);

	public MatchSvc(InMemoryDriverGrid driverGrid, ITripRepo tripRepository,
		IUsrRepo userRepository, IVehRepo vehicleRepository)
	{
		_driverGrid = driverGrid ?? throw new ArgumentNullException(nameof(driverGrid));
		_tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
	}
	#endregion
	public async Task<List<Drv>> FindBestDriversAsync(Guid tripId)
	{
		if (tripId == Guid.Empty) return new List<Drv>();

		var trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
		if (trip == null || !trip.IsSearching())
			return new List<Drv>();

		var pickup = trip.TripRoute.Pickup.Coord;

		// Lấy danh sách ID tài xế trong các ô lưới lân cận ( Spatial Indexing )
		var nearbyDriverIds = _driverGrid.GetDriversInCellAndNeighbors(
			pickup.Latitude,
			pickup.Longitude,
			radiusCells: 2);

		if (nearbyDriverIds == null) return new List<Drv>();

		bool hasDriversXungQuanh = false;
		foreach (var id in nearbyDriverIds)
		{
			hasDriversXungQuanh = true;
			break; // Có ít nhất 1 phần tử là đạt yêu cầu, dừng vòng lặp ngay
		}
		if (!hasDriversXungQuanh) return new List<Drv>();
		// CHIẾN LƯỢC TỐI ƯU: Đọc toàn bộ danh sách 1 lần duy nhất để tránh nghẽn I/O đọc file JSON nhiều lần
		var allUsers = await _userRepo.ReadAsync().ConfigureAwait(false);
		var allVehicles = await _vehicleRepo.ReadAsync().ConfigureAwait(false);

		var eligibleDrivers = new List<Drv>();

		// Vòng lặp viết tay thay thế hoàn toàn cho LINQ .Where() và .Select()
		foreach (var driverId in nearbyDriverIds)
		{
			// 1. Tìm thực thể Drv tương ứng từ danh sách bộ nhớ đệm
			Drv? currentDriver = null;
			foreach (var user in allUsers)
			{
				if (user is Drv d && d.Id == driverId)
				{
					currentDriver = d;
					break;
				}
			}

			// Kiểm tra điều kiện: Phải tồn tại tài xế và tài xế đang trực tuyến (Online)
			if (currentDriver == null || currentDriver.Status != DriverStatus.Online)
				continue;

			// 2. Tìm thực thể Xe (Veh) liên kết với tài xế này
			Veh? currentVehicle = null;
			foreach (var vehicle in allVehicles)
			{
				if (vehicle != null && vehicle.Id == currentDriver.VehId)
				{
					currentVehicle = vehicle;
					break;
				}
			}

			// Kiểm tra điều kiện cấu hình: Loại xe của tài xế phải khớp chính xác với loại xe khách đặt
			if (currentVehicle != null && currentVehicle.Type == trip.TripVehicleType)
			{
				eligibleDrivers.Add(currentDriver);
			}
		}

		return eligibleDrivers;
	}
	public async Task ProposeDriversForTripAsync(Guid tripId, int maxDrivers = 3)
	{
		if (tripId == Guid.Empty) return;

		var trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
		if (trip == null || !trip.IsSearching()) return;

		// Tìm danh sách tài xế phù hợp xung quanh
		List<Drv> drivers = await FindBestDriversAsync(tripId).ConfigureAwait(false);
		if (drivers.Count == 0) return;

		var topDriverIds = new List<Guid>();
		int limit = drivers.Count < maxDrivers ? drivers.Count : maxDrivers;

		for (int i = 0; i < limit; i++)
		{
			if (drivers[i] != null)
			{
				topDriverIds.Add(drivers[i].Id);
			}
		}

		if (topDriverIds.Count == 0) return;

		// Gọi đề xuất cuốc xe đến các tài xế được chọn trong thời gian quy định
		trip.ProposeDrivers(topDriverIds, expirySeconds: 60);
		await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
	}
	public async Task<bool> TryAssignDriverAsync(Guid tripId, Guid driverId)
	{
		if (tripId == Guid.Empty || driverId == Guid.Empty) return false;

		await _matchLock.WaitAsync().ConfigureAwait(false);
		try
		{
			var trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
			if (trip == null || !trip.IsSearching()) return false;
			if (!trip.IsPendingDriver(driverId)) return false;

			var user = await _userRepo.GetByIdAsync(driverId).ConfigureAwait(false);
			if (user is not Drv driver || driver.Status != DriverStatus.Online) return false;

			// Thực hiện các thay đổi trạng thái mang tính nguyên tử (Atomic state transition)
			trip.TryAssignFromPending(driverId);
			driver.SetOnTrip();

			// ĐỒNG BỘ LƯỚI: Xóa tài xế ra khỏi bản đồ tìm kiếm ngay lập tức vì họ đã nhận chuyến, tránh bị spam cuốc xe khác
			_driverGrid.RemoveDriver(driverId);

			// Lưu các thay đổi xuống tệp lưu trữ cục bộ
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
			await _userRepo.UpdateAsync(driver).ConfigureAwait(false);

			return true;
		}
		finally
		{
			_matchLock.Release();
		}
	}
}

public class DrvCmd : IDrvCmd
{
	#region Fields and Constructor
	private readonly IUsrRepo _userRepo;
	private readonly ITripRepo _tripRepository;
	private readonly IMatchSvc _matchingService;
	private readonly InMemoryDriverGrid _driverGrid;

	public event EventHandler<DriverStatusChangedEventArgs>? DriverStatusChanged;

	public DrvCmd(IUsrRepo userRepository, ITripRepo tripRepository,
		IMatchSvc matchingService, InMemoryDriverGrid driverGrid)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
		_matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
		_driverGrid = driverGrid ?? throw new ArgumentNullException(nameof(driverGrid));
	}
	#endregion

	public async Task GoOnlineAsync(Guid driverId)
	{
		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");

		AttachStatusChangedEvent(driver);
		driver.SetOnline();
		await _userRepo.UpdateAsync(driver).ConfigureAwait(false);

		if (driver.Position != null)
			_driverGrid.UpdateDriverLocation(driverId, driver.Position.Coord.Latitude, driver.Position.Coord.Longitude);
	}
	public async Task GoOfflineAsync(Guid driverId)
	{
		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");

		AttachStatusChangedEvent(driver);
		driver.SetOffline();
		await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
		_driverGrid.RemoveDriver(driverId);
	}
	public async Task AcceptTripAsync(Guid driverId, Guid tripId)
	{
		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");

		// Kích hoạt lắng nghe sự kiện đổi trạng thái (Ví dụ: Từ Online sang OnTrip)
		AttachStatusChangedEvent(driver);

		bool green = await _matchingService.TryAssignDriverAsync(tripId, driverId).ConfigureAwait(false);
		if (!green)
			throw new InvalidOperationException("Không thể nhận chuyến (Chuyến đi đã có tài xế khác nhận, hoặc yêu cầu đã hết hạn).");
	}
	public async Task RejectTripAsync(Guid driverId, Guid tripId)
	{
		var trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
		if (trip == null) return;

		if (trip.RemovePendingDriver(driverId))
		{
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);

			if (trip.PendingCount == 0 && trip.Status == TripStatus.Searching)
			{
				await _matchingService.ProposeDriversForTripAsync(tripId).ConfigureAwait(false);
			}
		}
	}
	public async Task UpdateLocationAsync(Guid driverId, Loc newLocation)
	{
		if (newLocation == null) throw new ArgumentNullException(nameof(newLocation));

		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");

		driver.UpdatePosition(newLocation);
		await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
		_driverGrid.UpdateDriverLocation(driverId, newLocation.Coord.Latitude, newLocation.Coord.Longitude);
	}

	private void AttachStatusChangedEvent(Drv driver)
	{
		driver.StatusChanged -= OnDriverStatusChanged;
		driver.StatusChanged += OnDriverStatusChanged;
	}

	private void OnDriverStatusChanged(object? sender, DriverStatusChangedEventArgs e)
	{
		DriverStatusChanged?.Invoke(this, e);
	}
}
public class DrvQry : IDrvQry
{
	private readonly IUsrRepo _userRepo;
	private readonly IVehRepo _vehicleRepo;

	public DrvQry(IUsrRepo userRepository, IVehRepo vehicleRepository)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
	}

	public async Task<List<Drv>> GetOnlineDriversAsync()
	{
		var allUsers = await _userRepo.ReadAsync().ConfigureAwait(false);
		var onlineDrivers = new List<Drv>();

		foreach (var user in allUsers)
		{
			if (user is Drv driver && driver.Status == DriverStatus.Online)
			{
				onlineDrivers.Add(driver);
			}
		}

		return onlineDrivers;
	}

	public async Task<List<Drv>> GetNearbyDriversAsync(Loc passengerLocation, double radiusKm = 5)
	{
		if (passengerLocation == null) return new List<Drv>();

		var onlineDrivers = await GetOnlineDriversAsync().ConfigureAwait(false);
		var nearby = new List<Drv>();

		foreach (var driver in onlineDrivers)
		{
			if (driver == null || driver.Position == null)
				continue;

			double distance = CalculateDistance(
				passengerLocation.Coord.Latitude,
				passengerLocation.Coord.Longitude,
				driver.Position.Coord.Latitude,
				driver.Position.Coord.Longitude
			);

			if (distance <= radiusKm)
				nearby.Add(driver);
		}

		return nearby;
	}

	public async Task<Drv> GetDriverByIdAsync(Guid driverId)
	{
		var user = await _userRepo.GetByIdAsync(driverId).ConfigureAwait(false);
		if (user is not Drv driver)
			throw new InvalidOperationException($"Không tìm thấy tài xế hợp lệ với định danh mã {driverId}.");
		return driver;
	}

	public async Task<Veh?> GetVehicleByIdAsync(Guid vehicleId)
	{
		return await _vehicleRepo.GetByIdAsync(vehicleId).ConfigureAwait(false);
	}

	private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
	{
		const double rEarth = 6371; // Bán kính Trái Đất (km)
		double dLat = ToRadians(lat2 - lat1);
		double dLon = ToRadians(lon2 - lon1);
		double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
				   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
				   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
		return rEarth * c;
	}

	private double ToRadians(double degrees) => degrees * Math.PI / 180.0;
}

public class WalletSvc : IWalletSvc
{
	private readonly IUsrRepo _userRepo;

	public WalletSvc(IUsrRepo userRepository, ITripQry tripQueryService)
	{
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	public async Task<decimal> GetWalletAsync(Guid driverId)
	{
		Drv driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false)
			?? throw new InvalidOperationException("Không tìm thấy tài xế.");
		return driver.Wallet;
	}

	public async Task<decimal> GetIncomeAsync(Guid driverId)
	{
		Drv driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false)
			?? throw new InvalidOperationException("Không tìm thấy tài xế.");
		return driver.Income;
	}
	public async Task DepositAsync(Guid driverId, decimal amount)
	{
		Drv driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false)
			?? throw new InvalidOperationException("Không tìm thấy tài xế.");

		driver.Deposit(amount);
		await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
	}
}
public class MapSvc : IMapSvc
{
    private readonly HttpClient _httpClient;
    private const string PhotonBaseUrl = "https://photon.komoot.io/";
    private const string OsrmBaseUrl = "http://router.project-osrm.org";

    public MapSvc(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.DefaultRequestHeaders.Add("Usr-Agent", "Mozilla/5.0");
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<List<Loc>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Loc>();
        try
        {
            string url = $"{PhotonBaseUrl}api?q={Uri.EscapeDataString(query)}&limit=20&countrycode=VN";
            using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode) return new List<Loc>();

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<PhotonResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result?.Features == null) return new List<Loc>();

            var locations = new List<Loc>();
            foreach (var f in result.Features)
            {
                if (f?.Properties == null || f.Geometry?.Coordinates == null || f.Geometry.Coordinates.Count < 2) continue;

                var p = f.Properties;
                if (string.IsNullOrWhiteSpace(p.Name)) continue;

                string city = p.City ?? "";
                if (!city.Contains("Hồ Chí Minh", StringComparison.OrdinalIgnoreCase) &&
                    !city.Contains("Ho Chi Minh", StringComparison.OrdinalIgnoreCase) &&
                    !city.Contains("TP HCM", StringComparison.OrdinalIgnoreCase) &&
                    !city.Contains("TPHCM", StringComparison.OrdinalIgnoreCase))
                    continue;

                string name = p.Name.Trim();
                string street = p.Street ?? "";
                string district = p.District ?? "";
                string country = string.IsNullOrWhiteSpace(p.Country) ? "Việt Nam" : p.Country;

                var addr = new Addr(name, street, district, city, country, p.HouseNumber, p.OsmValue, p.Locality);
                var coord = new Coord(f.Geometry.Coordinates[1], f.Geometry.Coordinates[0]);
                locations.Add(new Loc(coord, addr));
            }

            var normalizedQuery = query.ToLowerInvariant().Trim();
            var sorted = locations
                .OrderByDescending(loc => loc.Addr.Name.ToLowerInvariant().Contains(normalizedQuery) ? 2 :
                    (normalizedQuery.Split(' ').Length > 1 && normalizedQuery.Split(' ').All(w => loc.Addr.Name.ToLowerInvariant().Contains(w)) ? 1 : 0))
                .ThenBy(loc => loc.Addr.Name)
                .Take(10)
                .ToList();
            return sorted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Geocode error: {ex.Message}");
            return new List<Loc>();
        }
    }

    public async Task<Loc?> GetAddressAsync(double latitude, double longitude)
    {
        try
        {
            string url = $"{PhotonBaseUrl}reverse?lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}";
            using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode) return null;

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<PhotonResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result?.Features == null || result.Features.Count == 0) return null;

            var feature = result.Features[0];
            if (feature?.Properties == null || feature.Geometry?.Coordinates == null || feature.Geometry.Coordinates.Count < 2) return null;

            var p = feature.Properties;
            if (string.IsNullOrWhiteSpace(p.Name)) return null;

            string name = p.Name.Trim();
            string street = p.Street ?? "";
            string district = p.District ?? "";
            string city = p.City ?? "";
            string country = string.IsNullOrWhiteSpace(p.Country) ? "Việt Nam" : p.Country;
            var addr = new Addr(name, street, district, city, country, p.HouseNumber, p.OsmValue, p.Locality);
            var coord = new Coord(feature.Geometry.Coordinates[1], feature.Geometry.Coordinates[0]);
            return new Loc(coord, addr);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Reverse geocode error: {ex.Message}");
            return null;
        }
    }

    private async Task<(bool Green, string content, string error)> GetWithRetryAsync(string url, int maxRetries = 3)
    {
        int delay = 500;
        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            try
            {
                using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                    return (false, null, $"HTTP {(int)response.StatusCode}");
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return (true, content, null);
            }
            catch (HttpRequestException ex) when (attempt < maxRetries - 1)
            {
                await Task.Delay(delay).ConfigureAwait(false);
                delay *= 2;
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
        return (false, null, "Hết số lần thử lại");
    }

    private string FormatCoord(double coord) => coord.ToString("F6", CultureInfo.InvariantCulture).Replace(" ", "");
    private string CleanUrl(string url) => url.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");

    public async Task<Route?> GetDirectionsAsync(Loc origin, Loc destination)
    {
        if (origin == null || destination == null) return null;
        if (origin.Coord.Equals(destination.Coord)) return null;

        string url = $"{OsrmBaseUrl}/route/v1/driving/{FormatCoord(origin.Coord.Longitude)},{FormatCoord(origin.Coord.Latitude)};{FormatCoord(destination.Coord.Longitude)},{FormatCoord(destination.Coord.Latitude)}?overview=full&geometries=polyline";
        url = CleanUrl(url);
        var (ok, content, _) = await GetWithRetryAsync(url).ConfigureAwait(false);
        if (!ok) return null;

        try
        {
            var data = JsonSerializer.Deserialize<OsrmResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var osrmRoute = data?.Routes?.FirstOrDefault();
            if (osrmRoute == null || osrmRoute.Distance <= 0) return null;
            return new Route(origin, destination, (decimal)osrmRoute.Distance / 1000, TimeSpan.FromSeconds(osrmRoute.Duration), osrmRoute.Geometry);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[QA_LOG] MapSvc.GetDirectionsAsync Error: {ex.Message}");
            return null;
        }
    }
	public async Task<Loc> EnsureLocationAsync(string address, double? lat = null, double? lng = null)
	{
		string finalName = string.IsNullOrWhiteSpace(address) ? "Địa điểm đã chọn" : address;
		
		if (lat.HasValue && lng.HasValue) 
			return new Loc(new Coord(lat.Value, lng.Value), new Addr(finalName, address, "", "", ""));

		var res = await SearchAsync(address).ConfigureAwait(false);

		if (res != null && res.Count > 0)
		{
			return res[0];
		}

		return new Loc(new Coord(10.762622, 106.660172), new Addr(finalName, address, "", "", ""));
	}
}

public class TripCmd : ITripCmd
{
	#region Fields and Constructor
	private static readonly SemaphoreSlim _tripCommandLock = new(1, 1);
	private readonly ITripRepo _tripRepository;
	private readonly IUsrRepo _userRepo;
	private readonly IVehRepo _vehicleRepo;
	private readonly IFareSvc _fareService;
	private readonly IMapSvc _mapService;
	private readonly IMatchSvc _matchingService;
	private readonly Stack<Trip> _recentTrips = new();

	public event EventHandler<TripStatusChangedEventArgs>? TripStatusChanged;

	public TripCmd(
		ITripRepo tripRepository,
		IUsrRepo userRepository,
		IVehRepo vehicleRepository,
		IFareSvc fareService,
		IMapSvc mapService,
		IMatchSvc matchingService)
	{
		_tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
		_userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_vehicleRepo = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
		_fareService = fareService ?? throw new ArgumentNullException(nameof(fareService));
		_mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
		_matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
	}
	#endregion
	private void AttachTripToEvents(Trip trip)
	{
		trip.StatusChanged -= HandleTripStatusChanged;
		trip.StatusChanged += HandleTripStatusChanged;
	}

	private void HandleTripStatusChanged(object? sender, TripStatusChangedEventArgs e)
	{
		if (sender is Trip) TripStatusChanged?.Invoke(this, e);
	}

	public async Task<Trip> CreateTripAsync(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
	{
		if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
			throw new ArgumentException("Loại xe không hợp lệ.", nameof(vehicleType));

		var trip = new Trip(passengerId, route, fare, vehicleType);
		AttachTripToEvents(trip);
		trip.StartSearching();

		await _tripRepository.CreateAsync(trip).ConfigureAwait(false);

		_ = Task.Run(async () =>
		{
		    try
		    {
		        await _matchingService.ProposeDriversForTripAsync(trip.Id).ConfigureAwait(false);
		    }
		    catch (Exception ex)
		    {
		        Console.WriteLine($"[TripCmd] ProposeDrivers failed for trip {trip.Id}: {ex.Message}");
		    }
		});

		// Start timeout task
		_ = Task.Run(async () =>
		{
		    try
		    {
		        // Wait for 60 seconds (timeout period)
		        await Task.Delay(60000).ConfigureAwait(false);
		        
		        // Check if trip is still searching after timeout
		        var currentTrip = await _tripRepository.GetByIdAsync(trip.Id).ConfigureAwait(false);
		        if (currentTrip != null && currentTrip.IsSearching())
		        {
		            currentTrip.Timeout();
		            await _tripRepository.UpdateAsync(currentTrip).ConfigureAwait(false);
		            Console.WriteLine($"[TripCmd] Trip {trip.Id} timed out after 60 seconds of searching.");
		        }
		    }
		    catch (Exception ex)
		    {
		        Console.WriteLine($"[TripCmd] Timeout check failed for trip {trip.Id}: {ex.Message}");
		    }
		});

		_recentTrips.Push(trip);
		return trip;
	}

	public async Task AssignDriverAsync(Guid tripId, Guid driverId)
	{
		bool green = await _matchingService.TryAssignDriverAsync(tripId, driverId).ConfigureAwait(false);
		if (!green)
			throw new InvalidOperationException("Không thể gán tài xế cho chuyến đi này.");
	}

	public async Task DriverArrivedAtPickupAsync(Guid tripId)
	{
		await _tripCommandLock.WaitAsync().ConfigureAwait(false);
		try
		{
			Trip trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false)
				?? throw new InvalidOperationException("Không tìm thấy chuyến.");
			AttachTripToEvents(trip);
			trip.DriverArrived();
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
		}
		finally { _tripCommandLock.Release(); }
	}

	public async Task StartTripAsync(Guid tripId)
	{
		await _tripCommandLock.WaitAsync().ConfigureAwait(false);
		try
		{
			Trip trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false)
				?? throw new InvalidOperationException("Không tìm thấy chuyến.");
			AttachTripToEvents(trip);
			trip.BeginTrip();
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
		}
		finally { _tripCommandLock.Release(); }
	}

	public async Task DriverArrivedAtDropoffAsync(Guid tripId)
	{
		await _tripCommandLock.WaitAsync().ConfigureAwait(false);
		try
		{
			Trip trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false)
				?? throw new InvalidOperationException("Không tìm thấy chuyến.");
			AttachTripToEvents(trip);
			trip.FinishTrip();
			if (!trip.IsDropOff())
				throw new InvalidOperationException("Trạng thái không hợp lệ sau khi đến điểm trả.");
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
		}
		finally { _tripCommandLock.Release(); }
	}

	public async Task CompleteTripAsync(Guid tripId)
	{
		await _tripCommandLock.WaitAsync().ConfigureAwait(false);
		try
		{
			Trip trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false)
				?? throw new InvalidOperationException("Không tìm thấy chuyến.");
			AttachTripToEvents(trip);

			if (!trip.IsDropOff())
				throw new InvalidOperationException("Chỉ có thể hoàn thành chuyến khi đã đến điểm trả (DropOff).");
			if (!trip.DriverId.HasValue || trip.DriverId.Value == Guid.Empty)
				throw new InvalidOperationException("Chuyến đi không có tài xế.");

			Drv driver = await _userRepo.GetByIdAsync(trip.DriverId.Value).ConfigureAwait(false) as Drv
				?? throw new InvalidOperationException("Không tìm thấy tài xế.");
			Psg passenger = await _userRepo.GetByIdAsync(trip.PassengerId).ConfigureAwait(false) as Psg
				?? throw new InvalidOperationException("Không tìm thấy hành khách.");

			driver.AddIncome(trip.TripFare.DriverIncome);
			driver.SetOnline();
			driver.IncrementTotalTrips();
			passenger.IncrementTotalTrips();

			trip.ConfirmPayment();

			await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
			await _userRepo.UpdateAsync(passenger).ConfigureAwait(false);
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
		}
		finally { _tripCommandLock.Release(); }
	}

	public async Task CancelTripAsync(Guid tripId, string reason)
	{
		await _tripCommandLock.WaitAsync().ConfigureAwait(false);
		try
		{
			Trip? trip = await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
			if (trip == null)
				throw new InvalidOperationException("Không tìm thấy chuyến.");

			AttachTripToEvents(trip);
			trip.Cancel(reason);

			if (trip.DriverId.HasValue)
			{
				if (await _userRepo.GetByIdAsync(trip.DriverId.Value).ConfigureAwait(false) is Drv driver)
				{
					driver.SetOnline();
					await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
				}
			}
			await _tripRepository.UpdateAsync(trip).ConfigureAwait(false);
		}
		finally
		{
			_tripCommandLock.Release();
		}
	}

	public IEnumerable<Trip> GetRecentTrips()
	{
		var list = new List<Trip>();
		foreach (var trip in _recentTrips)
		{
			if (trip != null) list.Add(trip);
		}
		return list;
	}
}

public class TripQry : ITripQry
{
	private readonly ITripRepo _tripRepository;

	public TripQry(ITripRepo tripRepository)
	{
		_tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
	}
	public async Task<Trip?> GetTripByIdAsync(Guid tripId)
	{
		return await _tripRepository.GetByIdAsync(tripId).ConfigureAwait(false);
	}
	public async Task<List<Trip>> GetTripsByPassengerAsync(Guid passengerId)
	{
		return await _tripRepository.GetByPassengerIdAsync(passengerId).ConfigureAwait(false);
	}
	public async Task<List<Trip>> GetTripsByDriverAsync(Guid driverId)
	{
		return await _tripRepository.GetByDriverIdAsync(driverId).ConfigureAwait(false);
	}

	public async Task<List<Trip>> GetPendingTripsAsync()
	{
		var allTrips = await _tripRepository.ReadAsync().ConfigureAwait(false);
		var pendingTrips = new List<Trip>();

		foreach (var t in allTrips)
		{
			if (t != null && t.Status == TripStatus.Searching)
			{
				pendingTrips.Add(t);
			}
		}
		return pendingTrips;
	}

	public async Task<List<Trip>> GetTripsByStatusAsync(TripStatus status)
	{
		var allTrips = await _tripRepository.ReadAsync().ConfigureAwait(false);
		var filteredTrips = new List<Trip>();

		foreach (var t in allTrips)
		{
			if (t != null && t.Status == status)
			{
				filteredTrips.Add(t);
			}
		}
		return filteredTrips;
	}
	public async Task<List<Trip>> GetTripsWithPendingDriverAsync(Guid driverId)
	{
		var allTrips = await _tripRepository.ReadAsync().ConfigureAwait(false);
		var filteredTrips = new List<Trip>();

		foreach (var t in allTrips)
		{
			if (t != null && t.Status == TripStatus.Searching && t.IsPendingDriver(driverId))
			{
				filteredTrips.Add(t);
			}
		}
		return filteredTrips;
	}

	public async Task<Trip?> GetActiveTripForDriverAsync(Guid driverId)
	{
		var trips = await _tripRepository.GetByDriverIdAsync(driverId).ConfigureAwait(false);
		foreach (var t in trips)
		{
			if (t != null && !t.IsTerminal())
			{
				return t; // Trả về chuyến đi active đầu tiên tìm thấy
			}
		}
		return null;
	}

	public async Task<Trip?> GetActiveTripForPassengerAsync(Guid passengerId)
	{
		var trips = await _tripRepository.GetByPassengerIdAsync(passengerId).ConfigureAwait(false);
		foreach (var t in trips)
		{
			if (t != null && !t.IsTerminal())
			{
				return t; // Trả về chuyến đi active đầu tiên tìm thấy
			}
		}
		return null;
	}

	public async Task<int> GetTotalTripsForDriverAsync(Guid driverId)
	{
		var trips = await _tripRepository.GetByDriverIdAsync(driverId).ConfigureAwait(false);
		return trips.Count;
	}
}

public class InMemoryDriverGrid
{
	private readonly double _cellSizeDegrees;
	private readonly ConcurrentDictionary<(int, int), ConcurrentDictionary<Guid, byte>> _grid = new();

	public InMemoryDriverGrid(double cellSizeKm = 1.0)
	{
		_cellSizeDegrees = cellSizeKm / 111.0;
	}

	private (int, int) GetCell(double lat, double lon)
	{
		return ((int)Math.Floor(lat / _cellSizeDegrees), (int)Math.Floor(lon / _cellSizeDegrees));
	}

	public void UpdateDriverLocation(Guid driverId, double lat, double lon)
	{
		RemoveDriver(driverId);
		var cell = GetCell(lat, lon);
		var driversInCell = _grid.GetOrAdd(cell, _ => new ConcurrentDictionary<Guid, byte>());
		driversInCell.TryAdd(driverId, 0);
	}

	public void RemoveDriver(Guid driverId)
	{
		foreach (var cell in _grid.Values)
		{
			cell.TryRemove(driverId, out _);
		}
	}

	public IEnumerable<Guid> GetDriversInCellAndNeighbors(double lat, double lon, int radiusCells = 1)
	{
		var centerCell = GetCell(lat, lon);
		var foundDriverIds = new HashSet<Guid>();

		for (int x = -radiusCells; x <= radiusCells; x++)
		{
			for (int y = -radiusCells; y <= radiusCells; y++)
			{
				var cell = (centerCell.Item1 + x, centerCell.Item2 + y);
				if (_grid.TryGetValue(cell, out var driversInCell))
				{
					foreach (var id in driversInCell.Keys)
					{
						foundDriverIds.Add(id);
					}
				}
			}
		}
		return foundDriverIds;
	}
}

public class RevSvc : IRevSvc
{
	private readonly IRevRepo _reviewRepo;
	private readonly IUsrRepo _userRepo;
	private readonly ITripRepo _tripRepo;

	public RevSvc(IRevRepo reviewRepo, IUsrRepo userRepo, ITripRepo tripRepo)
	{
		_reviewRepo = reviewRepo ?? throw new ArgumentNullException(nameof(reviewRepo));
		_userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
		_tripRepo = tripRepo ?? throw new ArgumentNullException(nameof(tripRepo));
	}

	public async Task CreateReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)
	{
		var review = new Rev(driverId, passengerId, tripId, rating, comment);
		await _reviewRepo.CreateAsync(review).ConfigureAwait(false);

		// Update driver rating
		var driver = await _userRepo.GetDriverByIdAsync(driverId).ConfigureAwait(false);
		if (driver != null)
		{
			driver.AddReview(rating);
			await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
		}
	}

	public async Task UpdateReviewAsync(Guid reviewId, int rating, string comment)
	{
		var review = await _reviewRepo.GetByIdAsync(reviewId).ConfigureAwait(false);
		if (review == null) throw new InvalidOperationException("Review not found.");

		review.UpdateReview(rating, comment);
		await _reviewRepo.UpdateAsync(review).ConfigureAwait(false);
	}

	public async Task DeleteReviewAsync(Guid reviewId)
	{
		await _reviewRepo.DeleteAsync(reviewId).ConfigureAwait(false);
	}
}

public class NotificationSvc : INotificationSvc
{
	public string GetDriverNotificationMessage(DriverStatusChangedEventArgs e)
	{
		return string.Format("Trạng thái của bạn đã thay đổi thành: {0}", e.NewStatus);
	}

	public string GetPassengerNotificationMessage(TripStatusChangedEventArgs e)
	{
		switch (e.NewStatus)
		{
			case TripStatus.Searching: return "Đang tìm tài xế cho bạn...";
			case TripStatus.Matched: return "Đã tìm thấy tài xế!";
			case TripStatus.Arrived: return "Tài xế đã đến điểm đón.";
			case TripStatus.Started: return "Chuyến đi bắt đầu.";
			case TripStatus.DropOff: return "Đã đến điểm trả khách.";
			case TripStatus.Completed: return "Chuyến đi đã hoàn thành. Cảm ơn bạn!";
			case TripStatus.Cancelled: return "Chuyến đi đã bị hủy.";
			case TripStatus.Timeout: return "Không tìm thấy tài xế, yêu cầu đã hết hạn.";
			default: return string.Format("Trạng thái chuyến đi: {0}", e.NewStatus);
		}
	}

	public string GetDriverAcceptedMessage() => "Bạn đã nhận chuyến đi thành công.";
	public string GetDriverArrivedPickupMessage() => "Bạn đã đến điểm đón khách.";
	public string GetDriverStartTripMessage() => "Chuyến đi đã bắt đầu.";
	public string GetDriverCompleteTripMessage() => "Bạn đã hoàn thành chuyến đi.";
	public string GetPassengerRequestSentMessage() => "Yêu cầu của bạn đã được gửi đi.";
	public string GetPassengerDriverFoundMessage() => "Tài xế đã nhận lời mời của bạn.";
	public string GetPassengerDriverArrivedMessage() => "Tài xế đang đợi bạn ở điểm đón.";
	public string GetPassengerTripStartedMessage() => "Chuyến đi của bạn đã bắt đầu.";
	public string GetPassengerTripCompletedMessage() => "Chúc mừng! Bạn đã hoàn thành chuyến đi.";
}
