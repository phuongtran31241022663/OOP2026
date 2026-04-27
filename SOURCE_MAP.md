# Source Map - OOP Ride-Hailing System

## Tổng quan kiến trúc

**Layered Architecture** (DDD-inspired): Common → Domain → Application → Infrastructure → Presentation  
**Core Domain**: Trip lifecycle (State Pattern), User (Inheritance), Fare calculation, Matching  
**Persistence**: JSON files (Data/trips.json, drivers.json, passengers.json, farerule.json, reviews.json, vehicles.json)  
**External**: GMap.NET (map), OSRM/Photon (routing/geocoding)  
**UI**: WinForms với custom controls (MapControl, DriverCard...)

---

## Layer 1: Common

### 1.1 Namespace: `Common.Constants`

#### `FareConstants`
- **File**: `Common/Constants/FareConstants.cs`
- **Mục đích**: Constants cho tính giá cước
- **Thuộc tính** (static):
  - `CommissionRate`: `decimal` = 0.20m
  - `MinimumFare`: `decimal` = 12000m
  - `WaitingFeePerMinute`: `decimal` = 1000m
  - `Motorbike.BaseFare`: `decimal` = 12000m
  - `Motorbike.PricePerKm`: `decimal` = 4000m
  - `Car.BaseFare`: `decimal` = 25000m
  - `Car.PricePerKm`: `decimal` = 12000m
- **Phụ thuộc**: None

#### `SimulationConstants`
- **File**: `Common/Constants/SimulationConstants.cs`
- **Mục đích**: Constants cho simulation
- **Thuộc tính** (static):
  - `TickIntervalMs`: `int` = 2000
  - `InterpolationSteps`: `int` = 20
  - `DefaultSpeedKmh`: `double` = 40.0
  - `MaxPickupDistanceKm`: `double` = 5.0

### 1.2 Namespace: `Common.Extensions`

#### `DecimalExtensions`
- **File**: `Common/Extensions/DecimalExtensions.cs`
- **Mục đích**: Extension methods cho decimal (financial rounding, formatting)
- **Phương thức**:
  - `RoundToVnd(this decimal) : decimal` - Làm tròn theo đơn vị nghìn đồng
  - `SubtractPercentage(this decimal, decimal) : decimal` - Trừ tỷ lệ phần trăm
  - `ToVndCurrency(this decimal) : string` - Định dạng "50.000đ"
  - `ToDistanceString(this decimal) : string` - Định dạng "1.2 km"

#### `StringExtensions`
- **File**: `Common/Extensions/StringExtensions.cs`
- **Mục đích**: Validation + formatting strings
- **Phương thức**:
  - `IsValidPhone(this string) : bool` - Regex SĐT VN (10 số, bắt đầu 0)
  - `IsValidEmail(this string) : bool` - Kiểm tra email cơ bản
  - `ToTitleCase(this string) : string` - Viết hoa chữ đầu
  - `Truncate(this string, int) : string` - Rút gọn chuỗi

### 1.3 Namespace: `Common.Utilities` / `Common.Helpers`

#### `PasswordHasher`
- **File**: `Common/Utilities/PasswordHasher.cs` (duplicate tại `Common/Helpers/PasswordHasher.cs`)
- **Mục đích**: Băm mật khẩu an toàn (PBKDF2-SHA256)
- **Phương thức**:
  - `HashPassword(string) : string` - Format: "pbkdf2-sha256$iterations$salt$hash"
  - `VerifyPassword(string, string) : bool` - Xác minh + hỗ trợ định dạng legacy
  - `NeedsRehash(string) : bool` - Kiểm tra cần rehash không
- **Phụ thuộc**: System.Security.Cryptography

---

## Layer 2: Domain

### 2.1 Namespace: `Domain.SharedKernel`

#### `Entity`
- **File**: `Domain/SharedKernel/Entity.cs`
- **Mục đích**: Base class cho tất cả domain entities (Id-based equality)
- **Thuộc tính**:
  - `Id`: `Guid` {get; protected set;}
- **Phương thức**:
  - `AddEvent(DomainEvent) : void` - Thêm domain event
  - `GetEvents() : IReadOnlyList<DomainEvent>` - Lấy events chưa publish
  - `ClearEvents() : void` - Xóa events
- **Phụ thuộc**: DomainEvent

#### `ValueObject`
- **File**: `Domain/SharedKernel/ValueObject.cs`
- **Mục đích**: Base cho immutable value objects (value-based equality)
- **Phương thức** (abstract):
  - `GetEqualityComponents() : IEnumerable<object>` - Lớp con override để so sánh

#### `DomainEvent`
- **File**: `Domain/SharedKernel/DomainEvent.cs`
- **Mục đích**: Base class cho domain events
- **Thuộc tính**:
  - `Id`: `Guid` (auto-generated)
  - `OccurredOn`: `DateTime` (UTC)

### 2.2 Namespace: `Domain.Enums`

#### `DriverStatus`
- **File**: `Domain/Enums/DriverStatus.cs`
- **Values**: Offline(0) → Available(1) → OnTrip(2)

#### `VehicleType`
- **File**: `Domain/Enums/VehicleType.cs`
- **Values**: Unknown(0), Motorbike(1), Car(2)

### 2.3 Namespace: `Domain.ValueObjects`

#### `Coordinate`
- **File**: `Domain/ValueObjects/Coordinate.cs`
- **Thuộc tính**: `Latitude`, `Longitude` (double)

#### `Address`
- **File**: `Domain/ValueObjects/Address.cs`
- **Thuộc tính**: Name, Street, District, City, Country, HouseNumber, Osm_Value, Locality

#### `Location`
- **File**: `Domain/ValueObjects/Location.cs`
- **Thuộc tính**: `Coordinate`, `Address`
- **Phụ thuộc**: Coordinate, Address

#### `Money`
- **File**: `Domain/ValueObjects/Money.cs`
- **Thuộc tính**: `Amount` (decimal), `Currency` (string, default "VND")
- **Operators**: +, -, <, >, <=, >= (đảm bảo cùng currency)

#### `Route`
- **File**: `Domain/ValueObjects/Route.cs`
- **Thuộc tính**: Pickup, Destination (Location), Distance (km), Duration (TimeSpan), Polyline
- **Ràng buộc**: Distance > 0, Duration >= 0

#### `Fare`
- **File**: `Domain/ValueObjects/Fare.cs`
- **Thuộc tính**: TotalAmount, Commission, DriverIncome (Money)
- **Ràng buộc**: Commission <= TotalAmount

### 2.4 Namespace: `Domain.Entities`

#### `Trip` (Core Entity)
- **File**: `Domain/Entities/Trip.cs`
- **Mục đích**: Quản lý lifecycle chuyến đi (State Pattern)
- **Thuộc tính**:
  - `string Status` {get} - Derived từ ITripState (Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout)
  - `Guid PassengerId` {get}
  - `Guid? DriverId` {get; private set}
  - `VehicleType TripVehicleType` {get}
  - `Route TripRoute` {get}
  - `Fare TripFare` {get}
  - `double? Distance` {get} - from Route
  - `double? Duration` {get} - from Route (seconds)
  - `bool IsPaid` {get}
  - `DateTime RequestAt` {get}
- **Phương thức** (public behavior - delegated to state):
  - `SetSearching() : void` - Transition to SearchingState
  - `MatchDriver(Guid) : void` - Transition to MatchedState
  - `MarkAsArrived() : void` - Transition to ArrivedState
  - `StartTrip() : void` - Transition to StartedState
  - `CompleteTrip() : void` - Transition to CompletedState
  - `Cancel(string reason) : void` - Transition to CancelledState
  - `MarkTimeout() : void` - Transition to TimeoutState
  - `ConfirmPayment() : void` - Mark paid, throw nếu đã paid
  - `IsSearching() : bool`, `IsMatched() : bool`, `IsArrived() : bool`, `IsStarted() : bool`, `IsCompleted() : bool`, `IsCancelled() : bool`, `IsTimeout() : bool` - State check helpers
  - `IsTerminal() : bool` - True nếu Completed, Cancelled, hoặc Timeout
- **Internal helpers**: TransitionTo(ITripState), SetDriverId(Guid)
- **Sự kiện**: TripRequestedEvent, TripSearchingEvent, TripMatchedEvent, TripArrivedEvent, TripStartedEvent, TripCompletedEvent, TripCancelledEvent, TripPaidEvent, TripTimeoutEvent
- **Phụ thuộc**: ITripState, Route, Fare, VehicleType
- **Ràng buộc nghiệp vụ**:
  - PassengerId != Empty
  - Route/Fare không null
  - VehicleType != 0
  - Không thể ConfirmPayment 2 lần
  - State pattern enforce: cannot Start before Arrived, etc.

#### `User` (Abstract Entity)
- **File**: `Domain/Entities/User.cs`
- **Mục đích**: Base user (Passenger/Driver/Admin inheritance)
- **Thuộc tính**:
  - `string Name` {get; protected set} - validate not empty
  - `string Phone` {get; protected set} - validate not empty
  - `string Password` {get} - hashed
- **Phương thức**:
  - `UpdateName(string) : void`
  - `UpdatePhone(string) : void`
  - `ChangePassword(string oldRaw, string newRaw) : void` - verify old, validate new
  - `VerifyPassword(string raw) : bool`
  - `virtual string GetInfo() : string`
- **Phụ thuộc**: PasswordHasher

#### `FareRule`
- **File**: `Domain/Entities/FareRule.cs`
- **Mục đích**: Quy tắc tính giá theo loại xe
- **Thuộc tính**:
  - `VehicleType` {get}
  - `Money BaseFare` {get}
  - `Money PricePerKm` {get}
  - `decimal CommissionRate` {get}
  - `DateTime UpdatedAt` {get}
- **Phương thức**:
  - `CalculateFare(double distanceKm) : Fare` - Tính tổng cước + hoa hồng
  - `UpdateRule(Money, Money, decimal) : void` - Update và validate
- **Ràng buộc nghiệp vụ**: BaseFare ≥ 0, PricePerKm ≥ 0, CommissionRate ∈ [0,1]

#### `Review`
- **File**: `Domain/Entities/Review.cs`
- **Mục đích**: Đánh giá chuyến đi
- **Thuộc tính**:
  - `Guid DriverId, PassengerId, TripId` {get}
  - `int Rating` {get; private set} - 1-5
  - `string Comment` {get; private set}
  - `DateTime CreatedAt` {get}
- **Phương thức**: `UpdateReview(int, string) : void`

#### `Vehicle` (Abstract Entity)
- **File**: `Domain/Entities/Vehicle.cs`
- **Mục đích**: Base vehicle (polymorphism)
- **Thuộc tính**: PlateNumber, Brand, Model, Color, Capacity, Type
- **Phương thức** (abstract):
  - `GetAvgSpeed() : double`

### 2.5 Namespace: `Domain.Entities.Users`

#### `Driver`
- **File**: `Domain/Entities/Users/Driver.cs`
- **Mục đích**: Tài xế với state pattern + financial tracking
- **Thuộc tính**:
  - `string Status` {get} - Derived từ IDriverState (Offline, Available, OnTrip)
  - `DriverStatus SerializedStatus` {get; private set} - Chỉ dùng cho JSON backward compatibility
  - `Location Position` {get; set}
  - `string LicenseNumber` {get}
  - `Guid VehicleId` {get}
  - `Money Wallet` {get; private set}
  - `Money Income` {get; private set}
  - `int TotalTrips` {get; private set}
  - `int RatingSum` {get; private set}
  - `int TotalReviews` {get; private set}
  - `decimal AverageRating` {get} - computed
- **Phương thức**:
  - `SetAvailable() : void` - Transition to DriverAvailableState
  - `SetOnTrip() : void` - Transition to DriverOnTripState
  - `SetOffline() : void` - Transition to DriverOfflineState; Không thể nếu đang OnTrip
  - `IsAvailable() : bool`
  - `IsOnTrip() : bool`
  - `IsOffline() : bool`
  - `UpdatePosition(Location) : void`
  - `AddTrip() : void`
  - `UpdateReviews(int) : void` - Rating 1-5
  - `DepositToWallet(Money) : void`
  - `PayCommission(Fare) : void` - Trừ wallet, cộng income
- **Sự kiện**: DriverStatusChangedEvent, DriverLocationUpdatedEvent
- **Phụ thuộc**: IDriverState, Money, VehicleId
- **Ràng buộc nghiệp vụ**: Không thể Offline khi OnTrip; Wallet ≥ Commission để PayCommission

#### `Passenger`
- **File**: `Domain/Entities/Users/Passenger.cs`
- **Thuộc tính**: `int TotalTrips` {get; private set}
- **Phương thức**: `AddTrip() : void`, `override GetInfo()`

#### `Admin`
- **File**: `Domain/Entities/Users/Admin.cs`
- **Phương thức**: `override GetInfo()`

### 2.6 Namespace: `Domain.Entities.Vehicles`

#### `Motorbike` / `Car`
- **Files**: `Domain/Entities/Vehicles/Motorbike.cs`, `Car.cs`
- **Mục đích**: Concrete vehicle types
- **Motorbike**: Capacity=2, AvgSpeed=40km/h
- **Car**: Capacity variable, AvgSpeed=60km/h

### 2.7 Namespace: `Domain.States`

#### `ITripState`
- **File**: `Domain/States/ITripState.cs`
- **Interface**: 7 methods: SetSearching, MatchDriver, MarkAsArrived, StartTrip, CompleteTrip, Cancel, MarkTimeout

#### Concrete States (8 classes)
| Class | File | Mô tả |
|-------|------|-------|
| RequestedState | `Domain/States/RequestedState.cs` | Cho phép: SetSearching, Cancel |
| SearchingState | `Domain/States/SearchingState.cs` | Cho phép: MatchDriver, Cancel, MarkTimeout |
| MatchedState | `Domain/States/MatchedState.cs` | Cho phép: MarkAsArrived, Cancel |
| ArrivedState | `Domain/States/ArrivedState.cs` | Cho phép: StartTrip, Cancel |
| StartedState | `Domain/States/StartedState.cs` | Cho phép: CompleteTrip, Cancel |
| CompletedState | `Domain/States/CompletedState.cs` | Không cho phép gì (final) |
| CancelledState | `Domain/States/CancelledState.cs` | Không cho phép gì (final) |
| TimeoutState | `Domain/States/TimeoutState.cs` | Không cho phép gì (final) |

#### `IDriverState`
- **File**: `Domain/States/IDriverState.cs`
- **Interface**: 3 methods: SetAvailable, SetOnTrip, SetOffline

#### Driver Concrete States (3 classes)
| Class | File | Mô tả |
|-------|------|-------|
| DriverOfflineState | `Domain/States/Drivers/DriverOfflineState.cs` | Cho phép: SetAvailable |
| DriverAvailableState | `Domain/States/Drivers/DriverAvailableState.cs` | Cho phép: SetOnTrip, SetOffline |
| DriverOnTripState | `Domain/States/Drivers/DriverOnTripState.cs` | Cho phép: SetAvailable |

**Pattern**: Mỗi state gọi `driver.TransitionTo(new XxxState())` + `driver.AddEvent()` nếu hợp lệ; throw `InvalidOperationException` nếu transition không hợp lệ. **Trip và Driver đều dùng State Pattern.**

### 2.8 Namespace: `Domain.StateMachines`

> **Đã xóa.** `DriverStateMachine` đã được thay thế bằng State Pattern trong `Domain.States` (IDriverState + 3 concrete states).

### 2.9 Namespace: `Domain.Events`

| Event | File | Payload |
|-------|------|---------|
| TripRequestedEvent | `TripRequestedEvent.cs` | TripId, PassengerId, Pickup, Destination, VehicleType |
| TripSearchingEvent | `TripSearchingEvent.cs` | TripId, AttemptNumber |
| TripMatchedEvent | `TripMatchedEvent.cs` | TripId, DriverId |
| TripArrivedEvent | `TripArrivedEvent.cs` | TripId |
| TripStartedEvent | `TripStartedEvent.cs` | TripId |
| TripCompletedEvent | `TripCompletedEvent.cs` | TripId, PassengerId, DriverId, Fare |
| TripCancelledEvent | `TripCancelledEvent.cs` | TripId, Reason |
| TripPaidEvent | `TripPaidEvent.cs` | TripId, PassengerId, DriverId, TotalAmount, PaidAt |
| TripTimeoutEvent | `TripTimeoutEvent.cs` | TripId |
| DriverStatusChangedEvent | `DriverStatusChangedEvent.cs` | DriverId, OldStatus, NewStatus |
| DriverLocationUpdatedEvent | `DriverLocationUpdatedEvent.cs` | DriverId, NewLocation |
| ReviewCreatedEvent | `ReviewCreatedEvent.cs` | ReviewId, DriverId, RiderId, Rating, Comment |
| TripStatusChangedEventArgs | `TripStatusChangedEventArgs.cs` | TripId, NewStatus (string), DriverId? |

### 2.10 Namespace: `Domain.Repositories`

#### `IRepository<T>`
- **File**: `Domain/Repositories/IRepository.cs`
- **Extends**: `IReadRepository<T>` (GetByIdAsync, GetAllAsync)
- **Phương thức**: InitializeAsync, SaveChangesAsync, AddAsync, UpdateAsync, DeleteAsync

#### Specialized Interfaces
| Interface | File | Extra Methods |
|-----------|------|--------------|
| ITripRepository | `ITripRepository.cs` | GetByDriverIdAsync, GetByPassengerIdAsync |
| IDriverRepository | `IDriverRepository.cs` | GetByPhoneAsync, GetAvailableDriversAsync, ExistsByPhoneAsync |
| IPassengerRepository | `IPassengerRepository.cs` | GetByPhoneAsync, ExistsByPhoneAsync |
| IUserRepository | `IUserRepository.cs` | GetDriverByIdAsync, GetByPhoneAsync, GetDriversAsync, GetPassengersAsync, GetAvailableDriversAsync |
| IFareRuleRepository | `IFareRuleRepository.cs` | GetByVehicleTypeAsync, EnsureSeededAsync |
| IReviewRepository | `IReviewRepository.cs` | GetByDriverIdAsync, GetByTripIdAsync |
| IVehicleRepository | `IVehicleRepository.cs` | GetByTypeAsync |

---

## Layer 3: Application

### 3.1 Namespace: `Application.Interfaces`

#### `ITripService`
- **File**: `Application/Interfaces/ITripService.cs`
- **Sự kiện**: `TripStatusChanged : EventHandler<TripStatusChangedEventArgs>` (Observer pattern)
- **Phương thức**:
  - `CreateTripAsync(Guid, Route, Fare, VehicleType) : Task<Trip>` - Tạo chuyến
  - `MatchDriverAsync(Guid, Guid) : Task` - Ghép tài xế (có SemaphoreSlim lock + wallet check)
  - `MarkAsArrivedAsync(Guid) : Task`
  - `StartTripAsync(Guid) : Task`
  - `CompleteTripAsync(Guid) : Task`
  - `CancelTripAsync(Guid, string) : Task`
  - `GetTripAsync(Guid) : Task<Trip>`
  - `GetActiveTripForDriverAsync(Guid) : Task<Trip>`
  - `GetActiveTripForPassengerAsync(Guid) : Task<Trip>`
  - `GetPendingTripsAsync() : Task<List<Trip>>`
  - `GetTripsByDriverAsync(Guid) : Task<List<Trip>>`
  - `GetTripsByPassengerAsync(Guid) : Task<List<Trip>>`
  - `CanTripBeCancelledAsync(Guid) : Task<bool>`

#### `IDriverService`
- **File**: `Application/Interfaces/IDriverService.cs`
- **Phương thức**: GetDriverAsync, UpdateLocationAsync, SetAvailableAsync, SetOfflineAsync, GetAvailableDriversAsync

#### `IPassengerService`
- **File**: `Application/Interfaces/IPassengerService.cs`
- **Phương thức**:
  - `RequestTripAsync(Guid, Location, Location, VehicleType) : Task<Trip>` - Tính route + fare + create
  - `CancelTripAsync(Guid, Guid, string) : Task`
  - `GetTripHistoryAsync(Guid) : Task<List<Trip>>`
  - `GetActiveTripAsync(Guid) : Task<Trip>`
  - `RateDriverAsync(Guid, Guid, int, string) : Task`
  - `GetPassengerInfoAsync(Guid) : Task<Passenger>`

#### `IUserService`
- **File**: `Application/Interfaces/IUserService.cs`
- **Phương thức**: LoginAsync, RegisterDriverAsync, RegisterPassengerAsync, GetDriverByIdAsync, GetPassengerByIdAsync, UpdateDriverStatusAsync, UpdateDriverLocationAsync, GetAvailableDriversAsync, DriverExistsAsync

#### `IAdminService`
- **File**: `Application/Interfaces/IAdminService.cs`
- **Phương thức**: GetAllUsersAsync, GetAllDriversAsync, GetAllPassengersAsync, GetAllTripsAsync, GetTripsByStatusAsync(string status), GetFareRulesAsync, CreateFareRuleAsync, UpdateFareRuleAsync, GetTotalGMVAsync, GetTotalNTRAsync, GetCompletionRateAsync, GetAverageSatisfactionAsync

#### `IFareService`
- **File**: `Application/Interfaces/IFareService.cs`
- **Phương thức**: `CalculateFare(VehicleType, double) : Task<Fare>`

#### `IMapService`
- **File**: `Application/Interfaces/IMapService.cs`
- **Phương thức**: GetRouteAsync, SearchLocationAsync, ReverseGeocodeAsync

#### `IMatchingService`
- **File**: `Application/Interfaces/IMatchingService.cs`
- **Phương thức**: `MatchDriverToTripAsync(Guid, Guid) : Task<bool>` - Validate + match (có SemaphoreSlim + wallet check)

#### `IReviewService`
- **File**: `Application/Interfaces/IReviewService.cs`
- **Phương thức**: AddReviewAsync, GetReviewsForDriverAsync

#### `ISimulationService`
- **File**: `Application/Interfaces/ISimulationService.cs`
- **Phương thức**: StartSimulation, StopSimulation, StartTripSimulation, IsTripSimulating, Tick, SimulateTripProgress

#### `IUserQueryService`
- **File**: `Application/Interfaces/IUserQueryService.cs`
- **Phương thức**: GetByIdAsync, GetByPhoneAsync, GetAllAsync, ExistsByPhoneAsync

### 3.2 Namespace: `Application.Services`

#### `TripService`
- **File**: `Application/Services/TripService.cs`
- **Phụ thuộc**: ITripRepository, IDriverRepository, IPassengerRepository
- **Phương thức**: Implement ITripService - orchestrate trip workflow + publish TripStatusChanged events
- **Thread safety**: SemaphoreSlim `_matchLock` trong `MatchDriverAsync` để tránh 2 tài xế cùng nhận 1 chuyến

#### `DriverService`
- **File**: `Application/Services/DriverService.cs`
- **Phụ thuộc**: IDriverRepository

#### `PassengerService`
- **File**: `Application/Services/PassengerService.cs`
- **Phụ thuộc**: IPassengerRepository, ITripService, IReviewService, IMapService, IFareService
- **Flow RequestTripAsync**: Validate passenger → GetRoute (MapService) → CalculateFare → CreateTrip

#### `UserService`
- **File**: `Application/Services/UserService.cs`
- **Phụ thuộc**: IDriverRepository, IPassengerRepository
- **Flow LoginAsync**: Check Driver → Check Passenger → VerifyPassword

#### `AdminService`
- **File**: `Application/Services/AdminService.cs`
- **Phụ thuộc**: IDriverRepository, IPassengerRepository, ITripRepository, IFareRuleRepository, IReviewRepository
- **Stats**: GMV (total completed fares), NTR (commission), CompletionRate, AverageRating

#### `FareService`
- **File**: `Application/Services/FareService.cs`
- **Phụ thuộc**: IFareRuleRepository
- **Phương thức**: CalculateFare (lookup rule → rule.CalculateFare)

#### `MapService`
- **File**: `Application/Services/MapService.cs`
- **Phụ thuộc**: IGMapService (decorator/wrapper)

#### `MatchingService`
- **File**: `Application/Services/MatchingService.cs`
- **Phụ thuộc**: ITripRepository, IDriverRepository, IVehicleRepository
- **Flow MatchDriverToTripAsync**: Validate trip (Searching) → Validate driver (Available) → Validate vehicle type → Validate wallet ≥ commission → trip.MatchDriver + driver.SetOnTrip → Save both
- **Thread safety**: SemaphoreSlim `_matchLock` để tránh race condition

#### `ReviewService`
- **File**: `Application/Services/ReviewService.cs`
- **Phụ thuộc**: IReviewRepository, IDriverRepository, ITripRepository
- **Flow AddReviewAsync**: Validate trip+driver+passenger match → Create Review → Save → Update driver stats

#### `SimulationService`
- **File**: `Application/Services/SimulationService.cs`
- **Mục đích**: Trip simulation với System.Threading.Timer
- **Phương thức**: StartSimulation (khởi động timer), StopSimulation, StartTripSimulation, IsTripSimulating, Tick, SimulateTripProgress

#### `AppServiceBundle`
- **File**: `Application/Services/SimulationService.cs`
- **Mục đích**: Service locator / DI container
- **Phương thức**: `CreateDefaultAsync() : Task<AppServiceBundle>` - Wire-up tất cả services với JSON repos (async)
- **Phụ thuộc**: All repositories + services

---

## Layer 4: Infrastructure

### 4.1 Namespace: `Infrastructure.Repositories`

#### `JsonRepository<T>`
- **File**: `Infrastructure/Repositories/JsonRepository.cs`
- **Mục đích**: Generic JSON file-based repository với thread-safe file mutex
- **File**: `Data/{filename}.json`
- **Phương thức**: InitializeAsync, SaveChangesAsync, GetByIdAsync, GetAllAsync, AddAsync, UpdateAsync, DeleteAsync
- **Phụ thuộc**: Newtonsoft.Json

#### `JsonStorage<T>`
- **File**: `Infrastructure/Repositories/JsonStorage.cs`
- **Mục đích**: Simple JSON storage cho Presentation DI
- **Phương thức**: InitializeAsync, SaveAsync

#### Concrete Repositories
| Class | File | Base Class | Extra |
|-------|------|-----------|-------|
| TripRepository | `TripRepository.cs` | JsonRepository<Trip> | GetByDriverIdAsync, GetByPassengerIdAsync |
| DriverRepository | `DriverRepository.cs` | JsonRepository<Driver> | GetByPhoneAsync, GetAvailableDriversAsync |
| PassengerRepository | `PassengerRepository.cs` | JsonRepository<Passenger> | GetByPhoneAsync |
| UserRepository | `UserRepository.cs` | JsonRepository<User> | GetDriverByIdAsync, GetDriversAsync... |
| FareRuleRepository | `FareRuleRepository.cs` | JsonRepository<FareRule> | GetByVehicleTypeAsync, EnsureSeededAsync |
| ReviewRepository | `ReviewRepository.cs` | JsonRepository<Review> | GetByDriverIdAsync, GetByTripIdAsync |
| VehicleRepository | `VehicleRepository.cs` | JsonRepository<Vehicle> | GetByTypeAsync |

#### `FileStorage` (static)
- **File**: `Infrastructure/Repositories/FileStorage.cs`
- **Phương thức**: LoadAsync<T>, SaveAsync<T>

### 4.2 Namespace: `Infrastructure.ExternalServices`

#### `GMapService`
- **File**: `Infrastructure/ExternalServices/GMapService.cs`
- **Mục đích**: Map rendering (GMap.NET)
- **Phương thức**: SearchLocationAsync (mock empty), ReverseGeocodeAsync, GetRouteAsync
- **Phụ thuộc**: GMap.NET, IGMapService

#### `MapApiService`
- **File**: `Infrastructure/ExternalServices/MapApiService.cs`
- **Mục đích**: HTTP-based routing/geocoding (Photon + OSRM)
- **Phương thức**: SearchLocation, ReverseGeocodeAsync, GetDistanceAsync, GetRouteAsync
- **Phụ thuộc**: HttpClient, Newtonsoft.Json, PhotonResponse DTOs, OsrmResponse DTOs

### 4.3 Namespace: `Infrastructure.Interfaces`

#### `IGMapService`
- **File**: `Infrastructure/Interfaces/IGMapService.cs`
- **Phương thức**: SearchLocationAsync, ReverseGeocodeAsync, GetRouteAsync

#### `IMapApiService`
- **File**: `Infrastructure/Interfaces/IMapApiService.cs`
- **Phương thức**: GetDistanceAsync, GetRouteAsync, SearchLocation, ReverseGeocodeAsync

#### `IFileStorageService`
- **File**: `Infrastructure/Interfaces/IFileStorageService.cs`
- **Phương thức**: UploadFileAsync, DownloadFileAsync, DeleteFileAsync, FileExistsAsync

---

## Layer 5: Presentation

### 5.1 Namespace: `Presentation` (Base Classes)

#### `BaseForm`
- **File**: `Presentation/BaseForm.cs`
- **Mục đích**: Base cho dialog/screen forms (non-shell)
- **Features**: Loading state, Escape key, MessageBox helpers, Invoke helper
- **Phương thức**: ShowInfo, ShowWarning, ShowError, Confirm, ShowWaitCursor, RunOnUI

#### `BaseShell`
- **File**: `Presentation/BaseShell.cs`
- **Mục đích**: Base cho main shells (Passenger/Driver/Admin)
- **Features**: TabControl, StatusBar, AddTab/CloseTab, keyboard shortcuts (Ctrl+Tab)
- **Phương thức**: AddTab(BaseForm, string), CloseTab(TabPage), SetStatusText, ShowProgress

#### `BaseUserControl`
- **File**: `Presentation/BaseUserControl.cs`
- **Mục đích**: Base cho reusable controls
- **Features**: Loading state, MessageBox helpers, Invoke helper

### 5.2 Namespace: `Presentation.Shells`

#### `MainShell`
- **File**: `Presentation/Shells/MainShell.cs`
- **Mục đích**: Entry window - Login/Register/Dual mode
- **Phụ thuộc**: IUserService, factories cho LoginForm/RegisterForm/PassengerShell/DriverShell
- **Flow**: ButtonLogin → LoginForm.ShowDialog → Passenger/Driver Shell

#### `PassengerShell`
- **File**: `Presentation/Shells/PassengerShell.cs`
- **Mục đích**: Shell cho hành khách
- **Phụ thuộc**: IUserService, ITripService, ISimulationService, Passenger
- **Screens**: BookTripForm, TripTrackingForm, TripHistoryForm
- **Navigation**: RegisterScreens → NavigateTo(key)

#### `DriverShell`
- **File**: `Presentation/Shells/DriverShell.cs`
- **Mục đích**: Shell cho tài xế
- **Phụ thuộc**: IUserService, ITripService, ISimulationService, IFareService, Driver
- **Features**: Toggle active/offline, trip management
- **Screens**: DriverDashboardForm, EarningsForm

#### `AdminShell`
- **File**: `Presentation/Shells/AdminShell.cs`
- **Mục đích**: Shell cho admin
- **Phụ thuộc**: Admin, IAdminService
- **Inner class**: `AdminViewModel` - Load data, calculate stats (TotalUsers, ActiveDrivers, OnTripDrivers, OngoingTrips, TotalRevenue)
- **Sections**: Users, Drivers, Passengers, Trips, Fare Rules, Reports (6 panels)

### 5.3 Namespace: `Presentation.Screens.Auth`

#### `LoginForm`
- **File**: `Presentation/Screens/Auth/LoginForm.cs`
- **Phụ thuộc**: IUserService
- **Flow**: btnLogin_Click → _userService.LoginAsync → DialogResult.OK

#### `RegisterForm`
- **File**: `Presentation/Screens/Auth/RegisterForm.cs`
- **Phụ thuộc**: IUserService
- **Flow**: btnRegister_Click → RegisterDriverAsync (default location) hoặc RegisterPassengerAsync

### 5.4 Namespace: `Presentation.Screens.PassengerScreen`

#### `BookTripForm`
- **File**: `Presentation/Screens/PassengerScreen/BookTripForm.cs`
- **Phụ thuộc**: ITripService, IUserService, IMapService, IFareService, HttpClient, PassengerShell
- **Flow**: Pick pickup → Pick destination → Select vehicle → RequestTrip (currently disabled in UI)
- **Events**: MapClicked, PickupSelected, DestinationSelected

#### `TripTrackingForm`
- **File**: `Presentation/Screens/PassengerScreen/TripTrackingForm.cs`
- **Phụ thuộc**: ITripService, IUserService, PassengerShell
- **Mục đích**: Theo dõi chuyến đang diễn ra

#### `TripHistoryForm`
- **File**: `Presentation/Screens/PassengerScreen/TripHistoryForm.cs`
- **Phụ thuộc**: ITripService, Guid (userId)
- **Flow**: LoadTrips → GetTripsByPassengerAsync → DataGridView
- **Features**: Cell formatting (Completed=green, Cancelled=red)

#### `RatingForm`
- **File**: `Presentation
