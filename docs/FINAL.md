# Tài liệu chung nhất

> **Phạm vi:** WinForms .NET Framework 4.8 · GMap.NET · Dữ liệu JSON

---

## 1. Nguyên Lý Thiết Kế Giao Diện

Bốn nguyên tắc dưới đây chi phối **mọi** quyết định giao diện. Bất kỳ thành phần nào vi phạm một trong bốn nguyên tắc này đều cần được xem xét lại.

| #   | Nguyên tắc                   | Hệ quả thực tế                                                                                                                                                                     |
| --- | ---------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1   | **Tối thiểu Form**           | Chỉ tạo `Form` mới khi cần ngữ cảnh làm việc hoàn toàn biệt lập. Mọi điều hướng còn lại dùng `UserControl`.                                                                        |
| 2   | **Single-Form Shell**        | Một `FrmMain` duy nhất làm khung nền. Chuyển cảnh = hoán đổi `UserControl` bên trong Shell, không đóng/mở cửa sổ.                                                                  |
| 3   | **Container theo nghiệp vụ** | Chọn loại container xuất phát từ câu hỏi: _"Người dùng tương tác với màn hình này như thế nào?"_, không phải từ sở thích cá nhân.                                                  |
| 4   | **Tái sử dụng tối đa**       | Các component dùng chung (`UcMap`, `UcLocationPicker`, `UcTripStatus`, `UcDriverCard`, `UcVehicleFareSelector`…) phải được tái sử dụng, không được viết lại với mục đích tương tự. |

---

## 2. Tổng Quan Kiến Trúc Hệ Thống

### 2.1 Sơ Đồ Layer

Hệ thống RideGo sử dụng **5-Layer Architecture** (Layered — không phải Clean Architecture thuần; có vi phạm dependency đã được ghi nhận).

```
Common → Domain → Application → Infrastructure → Presentation
```

| Layer          | Project                                | Output  | Phụ thuộc                                   |
| -------------- | -------------------------------------- | ------- | ------------------------------------------- |
| Common         | `Common/Common.csproj`                 | Library | —                                           |
| Domain         | `Domain/Domain.csproj`                 | Library | Common                                      |
| Application    | `Application/Application.csproj`       | Library | Common, Domain                              |
| Infrastructure | `Infrastructure/Infrastructure.csproj` | Library | Application, Common, Domain                 |
| Presentation   | `Presentation/Presentation.csproj`     | WinExe  | Application, Common, Domain, Infrastructure |

> **Vi phạm kiến trúc (đã ghi nhận trong DEVLOG):** Presentation → Domain/Infrastructure trực tiếp (nên chỉ phụ thuộc Application). Đây là technical debt cần giải quyết trong phiên bản tới.

### 2.2 Sơ Đồ Phân Cấp UI

```
FrmMain  (1 Form duy nhất – khung nền)
├── FrmMultiRole  (Form phụ – hỗ trợ đa vai trò)
│
└── [UserControl được nạp động vào Shell]
    ├── UcAuth          ← Màn hình đầu tiên (Login / Register)
    ├── UcPassenger     ← Sau khi đăng nhập với vai trò Passenger
    ├── UcDriver        ← Sau khi đăng nhập với vai trò Driver
    └── UcAdmin         ← Sau khi đăng nhập với vai trò Admin

[UserControl phụ – dùng bên trong các UC chính hoặc trong Form]
├── UcReview         ← Đánh giá sau chuyến
├── UcProfile        ← Hồ sơ cá nhân / Nạp ví
├── UcTripDetail     ← Chi tiết một chuyến đi
├── UcMap            ← Bản đồ GMap.NET
├── UcLocationPicker ← Chọn địa điểm trên bản đồ
├── UcTripStatus     ← Hiển thị trạng thái chuyến
├── UcTripCard       ← Card hiển thị thông tin chuyến
├── UcDriverCard     ← Card hiển thị thông tin tài xế
└── UcVehicleFareSelector ← Chọn loại xe và xem giá
```

### 2.3 Bảng Tổng Hợp Thành Phần UI

| Thành phần              | Loại        | Actor                    | Mô tả ngắn                                                                      |
| ----------------------- | ----------- | ------------------------ | ------------------------------------------------------------------------------- |
| `FrmMain`               | Form        | Tất cả                   | Khung nền duy nhất; quản lý nạp/đổi UC, đăng ký event cấp ứng dụng              |
| `FrmMultiRole`          | Form        | Driver, Passenger        | Hộp thoại dùng chung cho mọi tác vụ cần sự tập trung (CRUD, đánh giá, hồ sơ)    |
| `UcAuth`                | UserControl | Tất cả                   | Gộp Login + Register; sau đăng nhập thành công → Shell nạp UC theo vai trò      |
| `UcPassenger`           | UserControl | Passenger                | Toàn bộ vòng đời đặt xe: Đặt → Theo dõi → Thanh toán                            |
| `UcDriver`              | UserControl | Driver                   | Bật/tắt trạng thái, nhận/từ chối chuyến, cập nhật tiến trình                    |
| `UcAdmin`               | UserControl | Admin                    | Quản lý Users/Trips/FareRules + Thống kê                                        |
| `UcReview`              | UserControl | Passenger                | Form đánh giá 5 sao + comment; mở trong `FrmMultiRole`                          |
| `UcProfile`             | UserControl | Passenger, Driver        | Xem/sửa hồ sơ, nạp ví; mở trong `FrmMultiRole`                                  |
| `UcTripDetail`          | UserControl | Admin, Passenger, Driver | Chi tiết một chuyến đi; dùng trong Tab (Admin) hoặc Modal (các vai trò còn lại) |
| `UcMap`                 | UserControl | Passenger, Driver        | Hiển thị bản đồ GMap.NET với markers và routes                                  |
| `UcLocationPicker`      | UserControl | Passenger                | Cho phép chọn điểm đón/đến trên bản đồ                                          |
| `UcTripStatus`          | UserControl | Passenger, Driver        | Hiển thị trạng thái hiện tại của chuyến                                         |
| `UcTripCard`            | UserControl | Passenger, Driver        | Card hiển thị thông tin chuyến trong danh sách                                  |
| `UcDriverCard`          | UserControl | Passenger                | Card hiển thị thông tin tài xế (tên, sao, loại xe, biển số)                     |
| `UcVehicleFareSelector` | UserControl | Passenger                | Chọn loại dịch vụ (Car/Motorbike) và xem giá dự kiến                            |

---

## 3. Kiến Trúc Domain

### 3.1 Entities Chính

#### 3.1.1 Trip (Aggregate Root)

**File:** `Domain/Entities/Trip.cs`

**Mục đích:** Quản lý lifecycle chuyến đi (State Pattern)

**Thuộc tính:**

- `string Status` {get; private set} - Derived từ ITripState (Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout)
- `Guid PassengerId` {get; private set}
- `Guid? DriverId` {get}
- `VehicleType TripVehicleType` {get; private set}
- `Route TripRoute` {get; private set}
- `Fare TripFare` {get; private set}
- `double? Distance` {get} - from Route
- `double? Duration` {get} - from Route (seconds)
- `bool IsPaid` {get}
- `DateTime RequestAt` {get; private set}

**Phương thức (public behavior - delegated to state):**

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

**Constructors:**

- `Trip(Guid, Route, Fare, VehicleType)` - Business constructor
- `[JsonConstructor] Trip(...)` - Persistence constructor

**Thread Safety:** Uses `Interlocked.Exchange` for atomic state transitions (per DEVLOG Section 3.3)

**Sự kiện:** TripRequestedEvent, TripSearchingEvent, TripMatchedEvent, TripArrivedEvent, TripStartedEvent, TripCompletedEvent, TripCancelledEvent, TripPaidEvent, TripTimeoutEvent

**Ràng buộc nghiệp vụ:**

- PassengerId != Empty
- Route/Fare không null
- VehicleType != 0
- Không thể ConfirmPayment 2 lần
- State pattern enforce: cannot Start before Arrived, etc.

#### 3.1.2 User (Abstract Entity)

**File:** `Domain/Entities/User.cs`

**Mục đích:** Base user (Passenger/Driver/Admin inheritance)

**Thuộc tính:**

- `string Name` {get; protected set} - validate not empty
- `string Phone` {get; protected set} - validate not empty
- `string Password` {get} - hashed

**Phương thức:**

- `UpdateName(string) : void`
- `UpdatePhone(string) : void`
- `ChangePassword(string oldRaw, string newRaw) : void` - verify old, validate new
- `VerifyPassword(string raw) : bool`
- `virtual string GetInfo() : string`

**Constructors:**

- `User(string, string, string)` - Business constructor (hashes password)
- `[JsonConstructor] User(Guid, string, string, string)` - Persistence constructor

**Phụ thuộc:** PasswordHasher

#### 3.1.3 Driver

**File:** `Domain/Entities/Users/Driver.cs`

**Mục đích:** Tài xế với state pattern + financial tracking

**Thread Safety:** Uses `Interlocked.Exchange` for atomic state transitions (per DEVLOG Section 3.3)

**Thuộc tính:**

- `string Status` {get; private set} - Derived từ IDriverState (Offline, Available, OnTrip)
- `Location Position` {get; private set}
- `string LicenseNumber` {get; private set}
- `Guid VehicleId` {get; private set}
- `Money Wallet` {get; private set}
- `Money Income` {get; private set}
- `int TotalTrips` {get; private set}
- `int RatingSum` {get; private set}
- `int TotalReviews` {get; private set}
- `decimal AverageRating` {get} - computed

**Phương thức:**

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

**Ràng buộc nghiệp vụ:** Không thể Offline khi OnTrip; Wallet ≥ Commission để PayCommission

#### 3.1.4 Passenger

**File:** `Domain/Entities/Users/Passenger.cs`

**Thuộc tính:** `int TotalTrips` {get; private set}

**Phương thức:** `AddTrip() : void`, `override GetInfo()`

#### 3.1.5 Admin

**File:** `Domain/Entities/Users/Admin.cs`

**Phương thức:** `override GetInfo()`

#### 3.1.6 FareRule

**File:** `Domain/Entities/FareRule.cs`

**Mục đích:** Quy định giá cước theo loại xe

**Thuộc tính:**

- `VehicleType` {get}
- `Money BaseFare` {get}
- `Money PricePerKm` {get}
- `decimal CommissionRate` {get}
- `DateTime UpdatedAt` {get}

**Phương thức:**

- `CalculateFare(double distanceKm) : Fare` - Tính tổng cước + hoa hồng
- `UpdateRule(VehicleType, Money, Money, decimal) : void` - Update và validate (tạo rule mới)

**Ràng buộc nghiệp vụ:** BaseFare ≥ 0, PricePerKm ≥ 0, CommissionRate ∈ [0,1]

#### 3.1.7 Vehicle (Abstract Entity)

**File:** `Domain/Entities/Vehicle.cs`

**Mục đích:** Base vehicle (polymorphism)

**Thuộc tính:** PlateNumber, Brand, Model, Color, Capacity, Type

**Phương thức (abstract):** `GetAvgSpeed() : double`

#### 3.1.8 Motorbike / Car

**Files:** `Domain/Entities/Vehicles/Motorbike.cs`, `Car.cs`

**Mục đích:** Concrete vehicle types

**Motorbike:** Capacity=2, AvgSpeed=40km/h

**Car:** Capacity variable, AvgSpeed=60km/h

#### 3.1.9 Review

**File:** `Domain/Entities/Review.cs`

**Mục đích:** Đánh giá chuyến đi

**Thuộc tính:**

- `Guid DriverId, PassengerId, TripId` {get}
- `int Star` {get; private set} - 1-5
- `string Comment` {get; private set}
- `DateTime CreatedAt` {get}

**Phương thức:** `UpdateReview(int, string) : void`

### 3.2 Value Objects

#### 3.2.1 Money

**File:** `Domain/ValueObjects/Money.cs`

**Thuộc tính:** `Amount` (decimal), `Currency` (string, default "VND")

**Operators:** +, -, <, >, <=, >= (đảm bảo cùng currency)

#### 3.2.2 Location

**File:** `Domain/ValueObjects/Location.cs`

**Thuộc tính:** `Coordinate`, `Address`

**Phụ thuộc:** Coordinate, Address

#### 3.2.3 Coordinate

**File:** `Domain/ValueObjects/Coordinate.cs`

**Thuộc tính:** `Latitude`, `Longitude` (double)

#### 3.2.4 Address

**File:** `Domain/ValueObjects/Address.cs`

**Thuộc tính:** Name, Street, District, City, Country, HouseNumber, Osm_Value, Locality

#### 3.2.5 Route

**File:** `Domain/ValueObjects/Route.cs`

**Thuộc tính:** Pickup, Destination (Location), Distance (km), Duration (TimeSpan), Polyline

**Ràng buộc:** Distance > 0, Duration >= 0

#### 3.2.6 Fare

**File:** `Domain/ValueObjects/Fare.cs`

**Thuộc tính:** TotalAmount, Commission, DriverIncome (Money)

**Ràng buộc:** Commission <= TotalAmount

### 3.3 State Pattern

#### 3.3.1 Trip States (ITripState)

**Interface:** `Domain/States/ITripState.cs`

**Interface:** 7 methods: SetSearching, MatchDriver, MarkAsArrived, StartTrip, CompleteTrip, Cancel, MarkTimeout

**Concrete States (8 classes):**

| Class          | File                              | Mô tả                                      |
| -------------- | --------------------------------- | ------------------------------------------ |
| RequestedState | `Domain/States/RequestedState.cs` | Cho phép: SetSearching, Cancel             |
| SearchingState | `Domain/States/SearchingState.cs` | Cho phép: MatchDriver, Cancel, MarkTimeout |
| MatchedState   | `Domain/States/MatchedState.cs`   | Cho phép: MarkAsArrived, Cancel            |
| ArrivedState   | `Domain/States/ArrivedState.cs`   | Cho phép: StartTrip, Cancel                |
| StartedState   | `Domain/States/StartedState.cs`   | Cho phép: CompleteTrip, Cancel             |
| CompletedState | `Domain/States/CompletedState.cs` | Không cho phép gì (final)                  |
| CancelledState | `Domain/States/CancelledState.cs` | Không cho phép gì (final)                  |
| TimeoutState   | `Domain/States/TimeoutState.cs`   | Không cho phép gì (final)                  |

#### 3.3.2 Driver States (IDriverState)

**Interface:** `Domain/States/IDriverState.cs`

**Interface:** 3 methods: SetAvailable, SetOnTrip, SetOffline

**Driver Concrete States (3 classes):**

| Class                | File                                            | Mô tả                           |
| -------------------- | ----------------------------------------------- | ------------------------------- |
| DriverOfflineState   | `Domain/States/Drivers/DriverOfflineState.cs`   | Cho phép: SetAvailable          |
| DriverAvailableState | `Domain/States/Drivers/DriverAvailableState.cs` | Cho phép: SetOnTrip, SetOffline |
| DriverOnTripState    | `Domain/States/Drivers/DriverOnTripState.cs`    | Cho phép: SetAvailable          |

**Pattern:** Mỗi state gọi `driver.TransitionTo(new XxxState())` + `driver.AddEvent()` nếu hợp lệ; throw `InvalidOperationException` nếu transition không hợp lệ. **Trip và Driver đều dùng State Pattern.**

### 3.4 Domain Events

| Event                      | File                                   | Payload                                               |
| -------------------------- | -------------------------------------- | ----------------------------------------------------- |
| TripRequestedEvent         | `Events/TripRequestedEvent.cs`         | TripId, PassengerId, Pickup, Destination, VehicleType |
| TripSearchingEvent         | `Events/TripSearchingEvent.cs`         | TripId, AttemptNumber                                 |
| TripMatchedEvent           | `Events/TripMatchedEvent.cs`           | TripId, DriverId                                      |
| TripArrivedEvent           | `Events/TripArrivedEvent.cs`           | TripId                                                |
| TripStartedEvent           | `Events/TripStartedEvent.cs`           | TripId                                                |
| TripCompletedEvent         | `Events/TripCompletedEvent.cs`         | TripId, PassengerId, DriverId, Fare                   |
| TripCancelledEvent         | `Events/TripCancelledEvent.cs`         | TripId, Reason                                        |
| TripPaidEvent              | `Events/TripPaidEvent.cs`              | TripId, PassengerId, DriverId, TotalAmount, PaidAt    |
| TripTimeoutEvent           | `Events/TripTimeoutEvent.cs`           | TripId                                                |
| DriverStatusChangedEvent   | `Events/DriverStatusChangedEvent.cs`   | DriverId, OldStatus, NewStatus                        |
| DriverLocationUpdatedEvent | `Events/DriverLocationUpdatedEvent.cs` | DriverId, NewLocation                                 |
| ReviewCreatedEvent         | `Events/ReviewCreatedEvent.cs`         | ReviewId, DriverId, RiderId, Rating, Comment          |

### 3.5 Repository Interfaces

#### 3.5.1 IRepository<T>

**File:** `Domain/Repositories/IRepository.cs`

**Extends:** `IReadRepository<T>` (GetByIdAsync, GetAllAsync)

**Phương thức:** InitializeAsync, SaveChangesAsync, AddAsync, UpdateAsync, DeleteAsync

#### 3.5.2 Specialized Interfaces

| Interface            | File                      | Extra Methods                                                                                      |
| -------------------- | ------------------------- | -------------------------------------------------------------------------------------------------- |
| ITripRepository      | `ITripRepository.cs`      | GetByDriverIdAsync, GetByPassengerIdAsync                                                          |
| IDriverRepository    | `IDriverRepository.cs`    | GetByPhoneAsync, GetAvailableDriversAsync, ExistsByPhoneAsync                                      |
| IPassengerRepository | `IPassengerRepository.cs` | GetByPhoneAsync, ExistsByPhoneAsync                                                                |
| IUserRepository      | `IUserRepository.cs`      | GetDriverByIdAsync, GetByPhoneAsync, GetDriversAsync, GetPassengersAsync, GetAvailableDriversAsync |
| IFareRuleRepository  | `IFareRuleRepository.cs`  | GetByVehicleTypeAsync, EnsureSeededAsync                                                           |
| IReviewRepository    | `IReviewRepository.cs`    | GetByDriverIdAsync, GetByTripIdAsync                                                               |
| IVehicleRepository   | `IVehicleRepository.cs`   | GetByTypeAsync                                                                                     |

---

## 4. Kiến Trúc Application Layer

### 4.1 Service Interfaces

#### 4.1.1 ITripService

**File:** `Application/Interfaces/ITripService.cs`

**Sự kiện:** `TripStatusChanged : EventHandler<TripStatusChangedEventArgs>` (Observer pattern)

**Phương thức:**

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

#### 4.1.2 IDriverService

**File:** `Application/Interfaces/IDriverService.cs`

**Phương thức:** GetDriverAsync, UpdateLocationAsync, SetAvailableAsync, SetOfflineAsync, GetAvailableDriversAsync

#### 4.1.3 IPassengerService

**File:** `Application/Interfaces/IPassengerService.cs`

**Phương thức:**

- `RequestTripAsync(Guid, Location, Location, VehicleType) : Task<Trip>` - Tính route + fare + create
- `CancelTripAsync(Guid, Guid, string) : Task`
- `GetTripHistoryAsync(Guid) : Task<List<Trip>>`
- `GetActiveTripAsync(Guid) : Task<Trip>`
- `RateDriverAsync(Guid, Guid, int, string) : Task`
- `GetPassengerInfoAsync(Guid) : Task<Passenger>`

#### 4.1.4 IUserService

**File:** `Application/Interfaces/IUserService.cs`

**Phương thức:** LoginAsync, RegisterDriverAsync, RegisterPassengerAsync, GetDriverByIdAsync, GetPassengerByIdAsync, UpdateDriverStatusAsync, UpdateDriverLocationAsync, GetAvailableDriversAsync, DriverExistsAsync

#### 4.1.5 IAdminService

**File:** `Application/Interfaces/IAdminService.cs`

**Phương thức:** GetAllUsersAsync, GetAllDriversAsync, GetAllPassengersAsync, GetAllTripsAsync, GetTripsByStatusAsync(string status), GetFareRulesAsync, CreateFareRuleAsync, UpdateFareRuleAsync, GetTotalGMVAsync, GetTotalNTRAsync, GetCompletionRateAsync, GetAverageSatisfactionAsync

#### 4.1.6 IFareService

**File:** `Application/Interfaces/IFareService.cs`

**Phương thức:** `CalculateFare(VehicleType, double) : Task<Fare>`

#### 4.1.7 IMapService

**File:** `Application/Interfaces/IMapService.cs`

**Phương thức:** GetRouteAsync, SearchLocationAsync, ReverseGeocodeAsync

#### 4.1.8 IMatchingService

**File:** `Application/Interfaces/IMatchingService.cs`

**Phương thức:** `MatchDriverToTripAsync(Guid, Guid) : Task<bool>` - Validate + match (có SemaphoreSlim + wallet check)

#### 4.1.9 IReviewService

**File:** `Application/Interfaces/IReviewService.cs`

**Phương thức:** AddReviewAsync, GetReviewsForDriverAsync

#### 4.1.10 ISimulationService

**File:** `Application/Interfaces/ISimulationService.cs`

**Phương thức:** StartSimulation, StopSimulation, StartTripSimulation, IsTripSimulating, Tick, SimulateTripProgress

### 4.2 Service Implementations

#### 4.2.1 TripService

**File:** `Application/Services/TripService.cs`

**Phụ thuộc:** ITripRepository, IDriverRepository, IPassengerRepository

**Phương thức:** Implement ITripService - orchestrate trip workflow + publish TripStatusChanged events

**Thread safety:** SemaphoreSlim `_matchLock` trong `MatchDriverAsync` để tránh 2 tài xế cùng nhận 1 chuyến

#### 4.2.2 DriverService

**File:** `Application/Services/DriverService.cs`

**Phụ thuộc:** IDriverRepository

#### 4.2.3 PassengerService

**File:** `Application/Services/PassengerService.cs`

**Phụ thuộc:** IPassengerRepository, ITripService, IReviewService, IMapService, IFareService

**Flow RequestTripAsync:** Validate passenger → GetRoute (MapService) → CalculateFare → CreateTrip

#### 4.2.4 UserService

**File:** `Application/Services/UserService.cs`

**Phụ thuộc:** IDriverRepository, IPassengerRepository

**Flow LoginAsync:** Check Driver → Check Passenger → VerifyPassword

#### 4.2.5 AdminService

**File:** `Application/Services/AdminService.cs`

**Phụ thuộc:** IDriverRepository, IPassengerRepository, ITripRepository, IFareRuleRepository, IReviewRepository

**Stats:** GMV (total completed fares), NTR (commission), CompletionRate, AverageRating

#### 4.2.6 FareService

**File:** `Application/Services/FareService.cs`

**Phụ thuộc:** IFareRuleRepository

**Phương thức:** CalculateFare (lookup rule → rule.CalculateFare)

#### 4.2.7 MapService

**File:** `Application/Services/MapService.cs`

**Phụ thuộc:** IGMapService (decorator/wrapper)

#### 4.2.8 MatchingService

**File:** `Application/Services/MatchingService.cs`

**Phụ thuộc:** ITripRepository, IDriverRepository, IVehicleRepository

**Flow MatchDriverToTripAsync:** Validate trip (Searching) → Validate driver (Available) → Validate vehicle type → Validate wallet ≥ commission → trip.MatchDriver + driver.SetOnTrip → Save both

**Thread safety:** SemaphoreSlim `_matchLock` để tránh race condition

#### 4.2.9 ReviewService

**File:** `Application/Services/ReviewService.cs`

**Phụ thuộc:** IReviewRepository, IDriverRepository, ITripRepository

**Flow AddReviewAsync:** Validate trip+driver+passenger match → Create Review → Save → Update driver stats

#### 4.2.10 SimulationService

**File:** `Application/Services/SimulationService.cs`

**Mục đích:** Trip simulation với System.Threading.Timer

**Phương thức:** StartSimulation (khởi động timer), StopSimulation, StartTripSimulation, IsTripSimulating, Tick, SimulateTripProgress

#### 4.2.11 AppServiceBundle

**File:** `Application/Services/AppServiceBundle.cs`

**Mục đích:** Service locator / DI container

**Phương thức:** `CreateDefaultAsync() : Task<AppServiceBundle>` - Wire-up tất cả services với JSON repos (async)

**Phụ thuộc:** All repositories + services

---

## 5. Kiến Trúc Infrastructure Layer

### 5.1 Repositories

#### 5.1.1 JsonRepository<T>

**File:** `Infrastructure/Repositories/JsonRepository.cs`

**Mục đích:** Generic JSON file-based repository với thread-safe file mutex

**File:** `Data/{filename}.json`

**Phương thức:** InitializeAsync, SaveChangesAsync, GetByIdAsync, GetAllAsync, AddAsync, UpdateAsync, DeleteAsync

**Phụ thuộc:** Newtonsoft.Json

#### 5.1.2 JsonStorage<T>

**File:** `Infrastructure/Repositories/JsonStorage.cs`

**Mục đích:** Simple JSON storage cho Presentation DI

**Phương thức:** InitializeAsync, SaveAsync

#### 5.1.3 Concrete Repositories

| Class               | File                     | Base Class                | Extra                                     |
| ------------------- | ------------------------ | ------------------------- | ----------------------------------------- |
| TripRepository      | `TripRepository.cs`      | JsonRepository<Trip>      | GetByDriverIdAsync, GetByPassengerIdAsync |
| DriverRepository    | `DriverRepository.cs`    | JsonRepository<Driver>    | GetByPhoneAsync, GetAvailableDriversAsync |
| PassengerRepository | `PassengerRepository.cs` | JsonRepository<Passenger> | GetByPhoneAsync                           |
| UserRepository      | `UserRepository.cs`      | JsonRepository<User>      | GetDriverByIdAsync, GetDriversAsync...    |
| FareRuleRepository  | `FareRuleRepository.cs`  | JsonRepository<FareRule>  | GetByVehicleTypeAsync, EnsureSeededAsync  |
| ReviewRepository    | `ReviewRepository.cs`    | JsonRepository<Review>    | GetByDriverIdAsync, GetByTripIdAsync      |
| VehicleRepository   | `VehicleRepository.cs`   | JsonRepository<Vehicle>   | GetByTypeAsync                            |

#### 5.1.4 FileStorage (static)

**File:** `Infrastructure/Repositories/FileStorage.cs`

**Phương thức:** LoadAsync<T>, SaveAsync<T>

### 5.2 External Services

#### 5.2.1 GMapService

**File:** `Infrastructure/ExternalServices/GMapService.cs`

**Mục đích:** Map rendering (GMap.NET)

**Phương thức:** SearchLocationAsync (mock empty), ReverseGeocodeAsync, GetRouteAsync

**Phụ thuộc:** GMap.NET, IGMapService

#### 5.2.2 MapApiService

**File:** `Infrastructure/ExternalServices/MapApiService.cs`

**Mục đích:** HTTP-based routing/geocoding (Photon + OSRM)

**Phương thức:** SearchLocation, ReverseGeocodeAsync, GetDistanceAsync, GetRouteAsync

**Phụ thuộc:** HttpClient, Newtonsoft.Json, PhotonResponse DTOs, OsrmResponse DTOs

---

## 6. Kiến Trúc Presentation Layer

### 6.1 Base Classes

#### 6.1.1 BaseForm

**File:** `Presentation/Base/BaseForm.cs`

**Mục đích:** Base cho dialog/screen forms (non-shell)

**Features:** Loading state, Escape key, MessageBox helpers, Invoke helper

**Phương thức:** ShowInfo, ShowWarning, ShowError, Confirm, ShowWaitCursor, RunOnUI

#### 6.1.2 BaseUserControl

**File:** `Presentation/Base/BaseUserControl.cs`

**Mục đích:** Base cho reusable controls

**Features:** Loading state, MessageBox helpers, Invoke helper

### 6.2 Forms Chính

#### 6.2.1 FrmMain

**File:** `Presentation/Forms/FrmMain.cs`

**Mục đích:** Entry window - Login/Register/Dual mode

**Phụ thuộc:** IUserService, factories cho LoginForm/RegisterForm/PassengerShell/DriverShell

**Flow:** ButtonLogin → LoginForm.ShowDialog → Passenger/Driver Shell

#### 6.2.2 FrmMultiRole

**File:** `Presentation/Forms/FrmMultiRole.cs`

**Mục đích:** Shell cho hành khách/tài xế/admin

**Phụ thuộc:** IUserService, ITripService, ISimulationService

**Screens:** BookTripForm, TripTrackingForm, TripHistoryForm

**Navigation:** RegisterScreens → NavigateTo(key)

### 6.3 UserControls Chính

#### 6.3.1 UcAuth – Xác Thực Tập Trung

**Container chính:** `TableLayoutPanel` (1 cột, 3 hàng: Logo · Form · Footer)

**Lý do:** Cần căn giữa theo cả chiều ngang lẫn dọc khi Shell co giãn. `TableLayoutPanel` với `Anchor = None` và `AutoSize` trên ô giữa cho phép điều này mà không cần tọa độ cứng.

```
UcAuth (Dock Fill)
└── TableLayoutPanel (1 cột × 3 hàng, Row 2 = Fill)
    ├── Row 1 – Logo / Tên ứng dụng (cố định Height)
    ├── Row 2 – Panel trung tâm (Fill, căn giữa ngang)
    │   └── Panel "pnlCenter" (AutoSize, Anchor = None)
    │       ├── pnlLogin  (Label, TextBox phone, TextBox password, Button "Đăng nhập", LinkLabel "→ Đăng ký")
    │       └── pnlRegister (TextBox name/phone/password, ComboBox Role, Button "Tạo tài khoản", LinkLabel "→ Đăng nhập")
    │           [Hai panel ẩn/hiện luân phiên – KHÔNG dùng TabControl]
    └── Row 3 – Footer nhỏ (cố định Height)
```

> **Quyết định thiết kế:** Dùng hai `Panel` ẩn/hiện thay vì `TabControl` vì tab bar của `TabControl` tạo ra affordance "người dùng có thể tự chọn tab", điều không phù hợp với flow xác thực tuyến tính.

#### 6.3.2 UcPassenger – Vòng Đời Đặt Xe

**Container chính:** `SplitContainer` (Vertical – bản đồ trái, bảng điều khiển phải)

**Lý do:** Bản đồ cần chiếm phần lớn không gian và cho phép người dùng kéo thanh chia; bảng bên phải thay đổi nội dung theo trạng thái chuyến mà không ảnh hưởng bản đồ.

```
UcPassenger (Dock Fill)
└── SplitContainer (Orientation = Vertical, Panel1 ~70%, Panel2 ~30%)
    │
    ├── Panel1 – Bản đồ
    │   └── UcMap (Dock Fill)
    │       [Hiển thị marker Pickup / Destination, mô phỏng vị trí tài xế]
    │
    └── Panel2 – Bảng điều khiển động
        └── TableLayoutPanel (1 cột × 3 hàng)
            ├── Row 1 – Header (cố định, hiển thị tên + Button "Lịch sử" + Button "Hồ sơ")
            ├── Row 2 – pnlActionStage (Fill) ← "sân khấu" thay nội dung theo trạng thái Trip
            │   ├── [Idle / Timeout]     → BookingPanel
            │   │   · UcLocationPicker (Pickup + Destination)
            │   │   · UcVehicleFareSelector (VehicleType: Car / Motorbike)
            │   │   · FarePanel (Base fare, /km, Tổng dự kiến)
            │   │   · Button "Đặt xe" [lớn, full-width]
            │   │
            │   ├── [Searching]          → SearchingPanel
            │   │   · ProgressBar (marquee) + Label "Đang tìm tài xế…"
            │   │   · Button "Hủy yêu cầu"
            │   │
            │   ├── [Matched / Arrived / Started] → TrackingPanel
            │   │   · UcTripStatus (hiển thị trạng thái hiện tại)
            │   │   · UcDriverCard (tên, sao, loại xe, biển số)
            │   │   · Button "Hủy chuyến" [chỉ Enable khi Status < Started]
            │   │
            │   └── [Completed]          → PaymentPanel
            │       · Label Tổng tiền (lớn, nổi bật)
            │       · Button "Xác nhận thanh toán"
            │       · Button "Đánh giá tài xế" → mở UcReview trong FrmMultiRole
            │
            └── Row 3 – StatusBar nhỏ (cố định, hiển thị trạng thái kết nối / thông báo)
```

> **Cơ chế chuyển Panel:** `TripStatusChanged` event → Shell hoặc `UcPassenger` gọi helper `ShowStage(TripStatus status)` → ẩn tất cả panel con, hiện đúng panel tương ứng. Không dùng `TabControl` vì người dùng không được phép tự chuyển tab.

#### 6.3.3 UcDriver – Trạm Chỉ Huy Tài Xế

**Container chính:** `TableLayoutPanel` (ngoài) + `SplitContainer` (trong)

**Lý do:** Tài xế cần đồng thời thấy danh sách request mới (trái) **và** nút hành động chuyến hiện tại (phải). `SplitContainer` phản ánh đúng đặc thù làm việc song song này.

```
UcDriver (Dock Fill)
└── TableLayoutPanel (1 cột × 2 hàng)
    │
    ├── Row 1 – TopBar (cố định Height = 64px)
    │   └── FlowLayoutPanel (ngang)
    │       · ToggleButton "Online / Offline" [lớn, đổi màu theo trạng thái]
    │       · Label "Ví: {amount} VNĐ"
    │       · Label "Sao: {rating} ★"
    │       · Button "Hồ sơ / Nạp ví" → mở UcProfile trong FrmMultiRole
    │
    └── Row 2 – Nội dung chính (Fill)
        └── SplitContainer (Orientation = Vertical, Panel1 ~35%, Panel2 ~65%)
            │
            ├── Panel1 – Danh sách yêu cầu chờ
            │   └── TableLayoutPanel (1 cột × 2 hàng)
            │       ├── Label "Yêu cầu mới" (cố định)
            │       └── DataGridView "dgvRequests" (Dock Fill, ReadOnly = true)
            │           Columns: Pickup · Destination · VehicleType · Fare
            │           [Double-click hoặc nút bên dưới để Chấp nhận / Từ chối]
            │           · Button "Chấp nhận" · Button "Từ chối"
            │
            └── Panel2 – Xử lý chuyến hiện tại
                └── pnlCurrentTrip (Dock Fill)
                    ├── [Không có chuyến] → Label căn giữa "Đang rảnh, chờ yêu cầu…"
                    └── [Có chuyến] → TableLayoutPanel dọc
                        · UcTripStatus (thông tin Passenger + điểm đón/đến)
                        · Button "Đã đến điểm đón"  [Height = 80px]
                        · Button "Bắt đầu chuyến"   [Height = 80px]
                        · Button "Hoàn thành"        [Height = 80px]
                        · Button "Hủy chuyến"        [Height = 48px, màu cảnh báo]
                        [Chỉ button phù hợp với trạng thái hiện tại mới Enable = true]
```

> **Quyết định thiết kế:** Các nút hành động cao 80px là bắt buộc – tài xế thao tác trong khi lái xe (hoặc vừa dừng xe), diện tích bấm nhỏ là lỗi nghiêm trọng về UX.

#### 6.3.4 UcAdmin – Trung Tâm Quản Trị

**Container chính:** `TabControl` (Dock Fill)

**Lý do:** Admin làm việc theo module chức năng độc lập (Users, Trips, Fares, Stats). `TabControl` là chuẩn mực back-office: mỗi tab = một không gian làm việc không ảnh hưởng lẫn nhau.

```
UcAdmin (Dock Fill)
└── TabControl (Dock Fill)
    │
    ├── TabPage "Người dùng"
    │   └── TableLayoutPanel (1 cột × 2 hàng)
    │       ├── Toolbar (cố định Height = 48px)
    │       │   · TextBox tìm kiếm · ComboBox lọc Role
    │       │   · Button "Khóa tài khoản" · Button "Mở khóa"
    │       └── DataGridView "dgvUsers" (Dock Fill)
    │           Columns: ID · Tên · Phone · Role · Trạng thái · Ngày tạo
    │
    ├── TabPage "Chuyến đi"
    │   └── Cấu trúc tương tự Tab "Người dùng"
    │       · Thêm Button "Xem chi tiết" → mở UcTripDetail trong FrmMultiRole
    │       · Thêm Button "Hủy chuyến" (chỉ Enable khi Status chưa terminal)
    │
    ├── TabPage "Giá cước"
    │   └── TableLayoutPanel (1 cột × 2 hàng)
    │       ├── Toolbar: Button "Thêm mới" · Button "Sửa" · Button "Xóa"
    │       └── DataGridView "dgvFareRules" (Dock Fill)
    │           Columns: VehicleType · BaseFare · PricePerKm · CommissionRate
    │           [Thêm/Sửa → mở form nhập liệu trong FrmMultiRole]
    │
    └── TabPage "Thống kê"
        └── TableLayoutPanel (2 cột × 2 hàng, padding 24px)
            · Panel "GMV"              – Label số lớn + Label đơn vị
            · Panel "Tỷ lệ hoàn thành" – Label % + ProgressBar minh họa
            · Panel "Điểm hài lòng"    – Label trung bình sao
            · Panel "Tổng chuyến"      – Label số chuyến theo trạng thái
            [Button "Làm mới dữ liệu" ở góc trên phải]
```

### 6.4 UserControls Phụ

#### 6.4.1 UcMap – Bản Đồ

**File:** `Presentation/Components/UcMap.cs`

**Mục đích:** Hiển thị bản đồ GMap.NET với markers và routes

**Features:**

- Hiển thị marker Pickup/Destination
- Vẽ route từ OSRM response (polyline decoding)
- Cập nhật vị trí tài xế mô phỏng

**Phụ thuộc:** GMap.NET, IGMapService

#### 6.4.2 UcLocationPicker – Chọn Địa Điểm

**File:** `Presentation/Components/UcLocationPicker.cs`

**Mục đích:** Cho phép người dùng chọn điểm đón/đến trên bản đồ hoặc nhập địa chỉ

**Features:**

- TextBox nhập địa chỉ
- Nút chọn trên bản đồ
- Autocomplete từ Photon API

#### 6.4.3 UcTripStatus – Trạng Thái Chuyến

**File:** `Presentation/Components/UcTripStatus.cs`

**Mục đích:** Hiển thị trạng thái hiện tại của chuyến đi

**States hiển thị:**

- Requested: "Đang chờ xử lý..."
- Searching: "Đang tìm tài xế..."
- Matched: "Đã tìm được tài xế"
- Arrived: "Tài xế đã đến điểm đón"
- Started: "Đang di chuyển..."
- Completed: "Chuyến đi hoàn thành"
- Cancelled: "Chuyến đi đã hủy"
- Timeout: "Hết thời gian chờ"

#### 6.4.4 UcDriverCard – Thông Tin Tài Xế

**File:** `Presentation/Components/UcDriverCard.cs`

**Mục đích:** Hiển thị thông tin tài xế (tên, sao, loại xe, biển số)

**Layout:**

- Avatar/Icon
- Tên tài xế
- Rating (sao)
- Loại xe + biển số
- Nút gọi (nếu cần)

#### 6.4.5 UcTripCard – Card Chuyến Đi

**File:** `Presentation/Components/UcTripCard.cs`

**Mục đích:** Hiển thị thông tin chuyến trong danh sách

**Layout:**

- Thời gian yêu cầu
- Điểm đón → Điểm đến
- Loại xe
- Trạng thái (màu sắc theo trạng thái)
- Giá tiền

#### 6.4.6 UcVehicleFareSelector – Chọn Dịch Vụ Và Giá

**File:** `Presentation/Components/UcVehicleFareSelector.cs`

**Mục đích:** Chọn loại dịch vụ (Car/Motorbike) và xem giá dự kiến

**Features:**

- RadioButton/ComboBox chọn Car/Motorbike
- Hiển thị giá cơ bản (BaseFare)
- Hiển thị giá theo km (PricePerKm)
- Tính và hiển thị tổng giá dự kiến

#### 6.4.7 UcReview – Đánh Giá

**File:** `Presentation/Components/UcReview.cs`

**Mục đích:** Form đánh giá 5 sao + comment sau chuyến

**Layout:**

- 5 RadioButton/Star cho rating (1-5 sao)
- TextBox cho comment
- Button "Gửi đánh giá"
- Button "Bỏ qua"

#### 6.4.8 UcProfile – Hồ Sơ Cá Nhân

**File:** `Presentation/Components/UcProfile.cs`

**Mục đích:** Xem/sửa hồ sơ, nạp ví

**Layout:**

- Thông tin cá nhân (tên, số điện thoại)
- Button sửa thông tin
- Số dư ví
- Button nạp tiền
- Lịch sử giao dịch (nếu cần)

#### 6.4.9 UcTripDetail – Chi Tiết Chuyến

**File:** `Presentation/Components/UcTripDetail.cs`

**Mục đích:** Xem chi tiết một chuyến đi

**Layout:**

- Thông tin chuyến (ID, thời gian, trạng thái)
- Thông tin hành khách
- Thông tin tài xế (nếu đã matched)
- Điểm đón → Điểm đến
- Quãng đường + Thời gian ước tính
- Giá tiền + Trạng thái thanh toán

---

## 7. Quy Tắc Modal vs. Inline

| Hành động người dùng               | Phương thức                                                       | Lý do                                                                           |
| ---------------------------------- | ----------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| Đăng nhập / Đăng ký                | **Inline** (UcAuth)                                               | Là điểm khởi đầu duy nhất; không cần cô lập                                     |
| Đặt xe, Theo dõi, Thanh toán       | **Inline** (UcPassenger – Panel động)                             | Cập nhật real-time liên tục; gián đoạn bởi Form mới = mất trạng thái            |
| Tìm kiếm tài xế (Searching)        | **Inline**                                                        | Trạng thái trung gian ngắn; không cần cửa sổ riêng                              |
| Chấp nhận / Từ chối chuyến         | **Inline** (UcDriver)                                             | Phản xạ tức thì, là core workflow của tài xế                                    |
| Cập nhật tiến trình chuyến         | **Inline** (UcDriver)                                             | Thao tác liên tiếp; cần giao diện luôn hiển thị                                 |
| Xem lịch sử chuyến đi              | **Inline** (Panel ẩn/hiện trong UcPassenger / Tab trong UcDriver) | Không cần cô lập; người dùng có thể cần xem lại khi đặt chuyến mới              |
| Đánh giá tài xế                    | **Modal** (FrmMultiRole + UcReview)                               | Tác vụ một lần sau chuyến; cần sự tập trung; không liên quan đến màn hình chính |
| Xem/Sửa hồ sơ, Nạp ví              | **Modal** (FrmMultiRole + UcProfile)                              | Tác vụ ít dùng, cô lập; không nên chiếm không gian màn hình làm việc            |
| Chi tiết chuyến (Passenger/Driver) | **Modal** (FrmMultiRole + UcTripDetail)                           | Xem thêm thông tin, không thay thế màn hình chính                               |
| Chi tiết chuyến (Admin)            | **Inline** (trong Tab "Chuyến đi")                                | Admin xem nhiều chuyến liên tiếp; Modal gây gián đoạn không cần thiết           |
| CRUD FareRule (Admin)              | **Modal** (FrmMultiRole)                                          | Nhập liệu cô lập; tránh làm nhiễu DataGrid chính                                |
| Xác nhận hủy chuyến                | **Modal** (FrmMultiRole – dialog Yes/No)                          | Hành động không thể hoàn tác; cần xác nhận rõ ràng                              |

---

## 8. Cơ Chế Real-time & Sự Kiện

Ứng dụng không dùng cơ sở dữ liệu quan hệ; cập nhật trạng thái dựa hoàn toàn vào **event-driven model**.

```
[Luồng sự kiện chính]

TripService.RaiseTripStatusChanged(trip)
    │
    ├──→ UcPassenger.OnTripStatusChanged(status)
    │       └── ShowStage(status) – chuyển panel phù hợp
    │
    ├──→ UcDriver.OnTripStatusChanged(status)
    │       └── Cập nhật nút hành động Enable/Disable
    │
    └──→ FrmMain.OnTripStatusChanged(status)
            └── FrmToast.Show(message) – thông báo nổi

[Timer polling – dùng System.Windows.Forms.Timer]
· Interval: 1000ms (1 giây) cho Passenger tracking
· Interval: 2000ms (2 giây) cho Driver request check
· Tất cả cập nhật UI phải trên UI thread (Control.Invoke nếu cần)
```

---

## 9. Quy Tắc Layout & Responsive

Mọi control phải tuân theo các quy tắc sau để tránh tọa độ cứng:

| Tình huống                        | Quy tắc bắt buộc                                                                                   |
| --------------------------------- | -------------------------------------------------------------------------------------------------- |
| Control chiếm toàn bộ container   | `Dock = Fill`                                                                                      |
| Control bám theo cạnh container   | `Anchor = Top, Left` (mặc định) hoặc cụ thể theo vị trí                                            |
| Control căn giữa động             | Đặt trong `TableLayoutPanel` với ô có `AutoSize`; đặt `Anchor = None` trên control                 |
| Khu vực cố định + khu vực co giãn | `TableLayoutPanel` với `RowStyle`: hàng cố định dùng `Absolute`, hàng co giãn dùng `Percent = 100` |
| Hai khu vực kéo được              | `SplitContainer` với `IsSplitterFixed = false`                                                     |
| **Cấm tuyệt đối**                 | Đặt `Location.X`, `Location.Y` trực tiếp trên control trong Designer                               |

---

## 10. Luồng Nghiệp Vụ Chi Tiết

### 10.1 Luồng Đặt Chuyến

```
[Passenger] Nhập pickup, dest, vehicleType
    → TripService.RequestTrip(passengerId, pickup, dest, vehicleType)
        → new Trip(passengerId, pickup, dest, vehicleType)  // emit TripRequestedEvent
        → trip.SetSearching()                              // emit TripSearchingEvent
        → TripRepository.AddAsync(trip)
        → return trip

[System - TripMatchingWorker] Poll trips đang Searching (dùng trip.IsSearching())
    → MatchingService.MatchDriverToTripAsync(tripId, driverId)
        → lọc driver: Status == Available AND VehicleType match AND Wallet >= Commission
        → trip.MatchDriver(driverId)                       // emit TripMatchedEvent
        → driver.SetOnTrip()                               // emit DriverStatusChangedEvent
        → save Trip + Driver

[Driver] Đến điểm đón
    → TripService.ArriveAtPickup(tripId)
        → trip.MarkAsArrived()                             // emit TripArrivedEvent

[Driver] Bắt đầu chuyến
    → TripService.StartTrip(tripId)
        → trip.StartTrip()                                 // emit TripStartedEvent

[Driver] Hoàn thành chuyến
    → TripService.CompleteTrip(tripId, distanceKm)
        → fare = FareService.CalculateFare(vehicleType, distanceKm)
        → trip.CompleteTrip()                              // emit TripCompletedEvent
        → trip.ConfirmPayment()                            // emit TripPaidEvent
        → driver.PayCommission(fare)
        → driver.SetAvailable()
        → passenger.AddTrip()
        → save all

[Passenger] Đánh giá
    → ReviewService.AddReviewAsync(driverId, passengerId, tripId, rating, comment)
        → new Review(...)                                  // emit ReviewCreatedEvent
        → driver.UpdateReviews(rating)
        → save Review + Driver
```

### 10.2 Luồng Timeout

```
[System - TripTimeoutWorker] Poll trips đang Searching (dùng trip.IsSearching())
    → trip.MarkTimeout()                                   // emit TripTimeoutEvent
    → save Trip
```

### 10.3 Tính Giá Cước

```
FareRule.CalculateFare(distanceKm):
    totalAmount = BaseFare + (PerKmRate × distanceKm)
    commission = totalAmount × CommissionRate
    driverIncome = totalAmount - commission
    return Fare(totalAmount, commission, driverIncome)
```

---

## 11. Bài Học Từ DEVLOG

### 11.1 Problems Đã Giải Quyết

| #   | Ngày       | Ngữ cảnh     | Vấn đề                                                                       | Nguyên nhân                                                                                                                  | Hướng nghĩ                                                                                                                                                              |
| --- | ---------- | ------------ | ---------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| P01 | 2026-04-24 | Matching     | Race condition: hai trip Searching có thể cùng nhận một driver               | `MatchDriverAsync` không có lock — không có `SemaphoreSlim`                                                                  | Đã thêm `SemaphoreSlim(1,1)` trong `MatchingService` và `TripService` để tránh double-assignment                                                                        |
| P02 | 2026-04-24 | Matching     | Thuật toán lọc driver chưa đầy đủ                                            | Chỉ lọc `Status == Available` + `VehicleType` match; chưa lọc: địa chỉ hành chính (phường→quận→thành phố), số dư `Wallet` đủ | Đã thêm kiểm tra `Wallet >= Commission` trong `MatchingService`; bỏ `MaxPickupDistance` khỏi Vehicle hierarchy (dùng `SimulationConstants.MaxPickupDistanceKm` nếu cần) |
| P03 | 2026-04-24 | Architecture | Presentation phụ thuộc trực tiếp vào Domain và Infrastructure                | Vi phạm Clean Architecture — Presentation nên chỉ phụ thuộc Application                                                      | Về lâu dài: tách DTO layer, chỉ expose Application interfaces ra Presentation. Trước mắt: ghi nhận, không block MVP                                                     |
| P04 | 2026-04-24 | Async        | `TripService` mix sync và async (`.Result` usage) trong một số call chain    | `RequestTrip()` là sync nhưng gọi async internals — `.Result` có thể gây deadlock trong WinForms message loop                | Tách hẳn sync và async path; hoặc dùng `Task.Run()` bọc ở Presentation, không gọi `.Result` trực tiếp                                                                   |
| P05 | 2026-04-24 | Map          | Polyline decoding chưa hoàn chỉnh — route không hiển thị trên MapControl     | OSRM trả về encoded polyline string; GMapRoute cần list Coordinate; chưa implement decoder                                   | Đã thêm `DecodePolyline` trong `MapControl`; chưa test tích hợp đầy đủ                                                                                                  |
| P06 | 2026-04-24 | Simulation   | `SimulationService` là stub — không có timer, không tự động di chuyển driver | `IDriverSimulationService` interface thiếu định nghĩa; class là no-op                                                        | Đã implement `SimulationService` với `System.Threading.Timer`, `StartSimulation/StopSimulation`, `CreateDefaultAsync()` factory                                         |

### 11.2 Bugs Đã Sửa

| #   | Ngày       | Module                                        | Lỗi                                                                        | Nguyên nhân                                                                                                       | Cách fix                                                                                                                                                  |
| --- | ---------- | --------------------------------------------- | -------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------- |
| B01 | 2026-04-24 | `Presentation/Program.cs`                     | Compile error: `RouteService` không tìm thấy                               | `new RouteService(...)` — `RouteService` class chưa tồn tại trong codebase                                        | Xoá dòng khởi tạo này hoặc tạo class `SimpleRouteService : IRouteService` wrapper `MapService`; cập nhật `IRouteService` → `IMapService` trong Program.cs |
| B02 | 2026-04-24 | `Presentation/Program.cs`                     | Compile error: `IDriverSimulationService` undefined                        | Interface được reference trong Program.cs (manual composition) nhưng chưa khai báo trong `Application.Interfaces` | Tạo `IDriverSimulationService` interface trong `Application/Interfaces/`; tạo stub impl `DriverSimulationService`                                         |
| B03 | 2026-04-24 | `Infrastructure/Repositories`                 | `JsonStorage` không tìm thấy từ `Program.cs`                               | Missing `using` statement cho Infrastructure namespace trong Presentation                                         | Thêm `using Infrastructure.Repositories;` hoặc dùng fully-qualified name                                                                                  |
| B04 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Reference `Domain.Interfaces.IDriverRepository` (namespace không tồn tại)  | Namespace sai — đúng là `Domain.Repositories`                                                                     | Sửa `using Domain.Interfaces` → `using Domain.Repositories`                                                                                               |
| B05 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Gọi `ITripService.TryAssignDriver()` — method không tồn tại trên interface | Method name sai — interface expose `MatchDriverAsync(Guid, Guid)`                                                 | Đổi call → `MatchDriverAsync(tripId, driverId)`                                                                                                           |
| B06 | 2026-04-24 | `Presentation/ViewModels/DriverViewModel.cs`  | Missing using cho `Driver`, `Trip`, `Vehicle`, `Location`                  | Không có `using Domain.Entities;` và `using Domain.ValueObjects;`                                                 | Thêm đúng using statements                                                                                                                                |
| B07 | 2026-04-24 | `Infrastructure/Repositories/JsonStorage`     | Thread-safety chưa xác minh đầy đủ                                         | `ReaderWriterLockSlim` khai báo nhưng chưa chắc bao phủ hết read path                                             | Audit lại toàn bộ public methods của `JsonStorage` — đảm bảo mọi read dùng `EnterReadLock`, mọi write dùng `EnterWriteLock`                               |

### 11.3 Design Decisions Quan Trọng

| #   | Ngày       | Phạm vi                | Quyết định                                                                                            | Lý do                                                                                                                                                                                   | Ảnh hưởng                                                                                                       |
| --- | ---------- | ---------------------- | ----------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| D02 | 2026-04-24 | Domain / Trip          | Dùng **State Pattern** (`ITripState` + 8 state classes) thay vì switch-case trên `TripStatus` enum    | Đảm bảo chỉ chuyển trạng thái hợp lệ; dễ thêm state mới không sửa Trip class; tách biệt behavior theo trạng thái                                                                        | Mọi transition phải đi qua state object → không thể bypass business rule từ bên ngoài.                          |
| D03 | 2026-05-01 | Domain / Driver        | Chuyển từ `DriverStateMachine` sang **State Pattern** (`IDriverState` + 3 states)                     | Đồng bộ kiến trúc với Trip; State Pattern giúp quản lý behavior (SetAvailable, SetOnTrip, SetOffline) sạch hơn static dictionary; hỗ trợ tốt hơn cho persistence                        | Loại bỏ `DriverStateMachine` hoàn toàn; Driver class giờ chỉ delegate hành vi sang `_currentState`.             |
| D04 | 2026-04-24 | Domain / Persistence   | Driver tham chiếu `VehicleId` (Guid) thay vì embed `Vehicle` object                                   | Giữ Vehicle là Aggregate riêng, tránh circular dependency; Vehicle có vòng đời độc lập (có thể đổi xe)                                                                                  | Query thông tin xe phải join qua VehicleRepository — 2 query thay vì 1                                          |
| D05 | 2026-04-24 | Infrastructure         | JSON file storage với `TypeNameHandling.All` (Newtonsoft.Json)                                        | Ràng buộc giáo khoa không dùng SQL; `TypeNameHandling.All` cần thiết để deserialized `List<User>` giữ đúng subtype (Driver / Passenger)                                                 | Dữ liệu JSON lưu thêm `$type` field — coupling với Newtonsoft; migration sang DB sau này cần rewrite repository |
| D06 | 2026-04-24 | Application / Event    | `ITripService.TripStatusChanged` là `EventHandler<TripStatusChangedEventArgs>` declare trên interface | Cho phép UI subscribe qua interface mà không cần cast sang `TripService` concrete — decoupling giữa Presentation và Application                                                         | UI Forms phải unsubscribe khi close để tránh memory leak                                                        |
| D07 | 2026-04-24 | Application / Matching | Không implement `InMemoryDriverCache`, không có `Policies/` (EligibilityPolicy, AssignmentPolicy)     | MVP scope — inline logic trong `MatchingService` đủ; cache và policy object là over-engineering cho project đơn luồng                                                                   | Khi scale lên (nhiều concurrent request), cần extract policy và cache                                           |
| D08 | 2026-04-24 | Infrastructure / Map   | Dùng Photon (geocoding) + OSRM (routing) thay vì Google Maps API                                      | Google Maps yêu cầu API key có phí; Photon + OSRM là open-source, không cần key                                                                                                         | OSRM trả về encoded polyline — đã thêm decoder trong `MapControl` (chưa test đầy đủ)                            |
| D10 | 2026-05-01 | Domain / Persistence   | Sử dụng `[JsonConstructor]` và private state setters trong Domain Entities                            | Cho phép tái tạo (reconstitute) trạng thái nội bộ của object (như `_currentState`, `_isPaid`) từ JSON mà không cần public setters hay constructor tham số rỗng (Always Valid principle) | Khắc phục lỗi dữ liệu bị null/0 khi load từ file; đảm bảo tính đóng gói (encapsulation).                        |
| D11 | 2026-05-01 | Documentation          | Đồng bộ hóa toàn bộ tài liệu (README, SOURCE_MAP, docs/) với code thực tế                             | Đảm bảo tài liệu phản ánh chính xác các thay đổi về State Pattern, Persistence và Project Structure                                                                                     | Tăng tính tin cậy của tài liệu cho Agent và Developer.                                                          |

### 11.4 Lessons Learned

- [2026-05-04] [Doc] Đã cấu trúc lại hệ thống tài liệu: PSPEC (nghiệp vụ + limit), TECH_SPEC (kiến trúc), KNOWLEDGE (tri thức), SOURCE_MAP (codebase). Ưu tiên đọc theo thứ tự này giúp Agent nắm context nhanh mà không cần đọc lại toàn bộ code.
- [2026-05-04] [Agent] Tích hợp Learning Loop: Mỗi task xong phải ghi kết luận/bài học vào DEVLOG để Agent sau kế thừa, tránh lặp lại phân tích tốn token.
- [2026-05-06] [Doc] Kiểm tra & cập nhật tài liệu theo hướng "code-first": đối chiếu SOURCE_MAP/TECH_SPEC/Business_and_UI_Guide với contracts & flow trong codebase, giảm drift giữa tài liệu và implementation.
- [2026-05-06] [UI] Modernize UI/UX: Vehicle card selection, Driver status pills, and Passenger layout polishing.

---

## 12. Use Cases

| ID   | Tên                      | Tác nhân         | Mô tả                                                                                                      | Trạng thái |
| ---- | ------------------------ | ---------------- | ---------------------------------------------------------------------------------------------------------- | ---------- |
| UC1  | Đăng nhập                | User             | Phone + password → `UserService.Login()`                                                                   | ✅         |
| UC2  | Đăng ký tài xế           | Driver           | Tạo Driver + Vehicle → `UserService.RegisterDriver()`                                                      | ✅         |
| UC3  | Đăng ký hành khách       | Passenger        | Tạo Passenger → `UserService.RegisterPassenger()`                                                          | ✅         |
| UC4  | Đặt chuyến               | Passenger        | Nhập pickup/dest/vehicleType → `TripService.RequestTrip()` (sync)                                          | ✅         |
| UC5  | Ghép tài xế              | System           | Lọc driver Available + VehicleType match + Wallet đủ hoa hồng → `MatchingService.MatchDriverToTripAsync()` | ✅         |
| UC6  | Đến điểm đón             | Driver           | → `TripService.ArriveAtPickup()`                                                                           | ✅         |
| UC7  | Bắt đầu chuyến           | Driver           | → `TripService.StartTrip()`                                                                                | ✅         |
| UC8  | Hoàn thành chuyến        | Driver           | → `TripService.CompleteTrip()` + payment inline                                                            | ✅         |
| UC9  | Đánh giá tài xế          | Passenger        | → `ReviewService.AddReviewAsync()` → cập nhật AverageRating                                                | ✅         |
| UC10 | Hủy chuyến               | Passenger/Driver | → `TripService.CancelTrip()` — được phép trước Started                                                     | ✅         |
| UC11 | Lịch sử chuyến           | Passenger/Driver | → `TripRepository.GetByPassengerId/DriverId`                                                               | ✅         |
| UC12 | Thông tin tài xế matched | Passenger        | Trip DTO chứa DriverId → UI hiển thị thông tin                                                             | ✅         |
| UC13 | Bật/tắt trạng thái       | Driver           | Offline ↔ Available → `UserService.UpdateDriverStatus()`                                                   | ✅         |
| UC14 | Nhận thông tin chuyến    | Driver           | → `TripService.GetTripAsync()`                                                                             | ✅         |
| UC15 | Chấp nhận/Từ chối chuyến | Driver           | `AcceptTripHandler` tồn tại; UI flow chưa hoàn chỉnh                                                       | ⚠️         |
| UC16 | Admin theo dõi real-time | Admin            | AdminShell + DataGridView; chưa có live map                                                                | ⚠️         |
| UC17 | Cấu hình FareRule        | Admin            | `AdminService.CreateFareRuleAsync/UpdateFareRuleAsync`                                                     | ✅         |
| UC18 | Driver Radar (proximity) | Passenger        | Chưa implement proximity search                                                                            | ❌         |
| UC19 | Thu nhập tài xế          | Driver           | Driver entity có Income + Wallet (Money)                                                                   | ✅         |
| UC20 | Báo cáo thống kê         | Admin            | GMV, NTR, CompletionRate, AverageSatisfaction                                                              | ✅         |
| UC21 | Dẫn đường (routing)      | Driver           | MapControl hiển thị route nếu có polyline; routing chưa integrated                                         | ⚠️         |
| UC22 | Sửa thông tin cá nhân    | User             | `UserService` có methods; UI partial                                                                       | ⚠️         |

---

## 13. Ràng Buộc Kỹ Thuật

> **Áp dụng cho tất cả code, tài liệu, và câu trả lời từ AI.**

- **Môi trường:** .NET Framework 4.8, C# 8.0, WinForms (Windows).
- **Cấm các tính năng C# 8.0+:** `Nullable reference types` (`string?`), `using var`, `await foreach`, `IAsyncEnumerable<T>`, `record`, `init`, `with`, `target-typed new`, `global using`, file-scoped namespaces, `required`.
  - **Lưu ý:** `switch expression` (`x switch { ... }`) hiện **ĐƯỢC PHÉP**.
- **Cấm thư viện/Pattern:**
  - **Dependency Injection (DI):** Không dùng container (`Microsoft.Extensions.DependencyInjection`, v.v.). Tự quản lý dependency (truyền qua constructor hoặc `new` trực tiếp).
  - **LINQ:** Không dùng `Where`, `Select`, `OrderBy`, ... (dùng `foreach` + `if` truyền thống).
  - **`var`:** Không dùng `var`. Phải khai báo kiểu tường minh.
- **Ràng buộc khác:** Không tự ý sửa `.csproj`/`.sln` (NuGet, LangVersion) khi chưa duyệt.

---

## 14. Tổng Kết

### 14.1 Số Lượng Thành Phần

| Hạng mục             | Số lượng | Danh sách                                                                                                                                   |
| -------------------- | -------- | ------------------------------------------------------------------------------------------------------------------------------------------- |
| Form                 | 2        | `FrmMain`, `FrmMultiRole`                                                                                                                   |
| UserControl chính    | 4        | `UcAuth`, `UcPassenger`, `UcDriver`, `UcAdmin`                                                                                              |
| UserControl phụ      | 9        | `UcReview`, `UcProfile`, `UcTripDetail`, `UcMap`, `UcLocationPicker`, `UcTripStatus`, `UcTripCard`, `UcDriverCard`, `UcVehicleFareSelector` |
| Domain Entities      | 9        | `Trip`, `User`, `Driver`, `Passenger`, `Admin`, `FareRule`, `Vehicle`, `Motorbike`, `Car`, `Review`                                         |
| Value Objects        | 6        | `Money`, `Location`, `Coordinate`, `Address`, `Route`, `Fare`                                                                               |
| Trip States          | 8        | `RequestedState`, `SearchingState`, `MatchedState`, `ArrivedState`, `StartedState`, `CompletedState`, `CancelledState`, `TimeoutState`      |
| Driver States        | 3        | `DriverOfflineState`, `DriverAvailableState`, `DriverOnTripState`                                                                           |
| Domain Events        | 12       | Trip (9) + Driver (2) + Review (1)                                                                                                          |
| Application Services | 11       | Trip, Driver, Passenger, User, Admin, Fare, Map, Matching, Review, Simulation, AppServiceBundle                                             |
| Repositories         | 7        | Trip, Driver, Passenger, User, FareRule, Review, Vehicle                                                                                    |

### 14.2 Lợi Ích Của Kiến Trúc Này

- **Mượt mà:** Chuyển từ Login → Màn hình chính → Đặt xe → Theo dõi không có cửa sổ nào đóng/mở.
- **Rõ ràng:** Mỗi actor có không gian làm việc riêng biệt, layout tối ưu cho nghiệp vụ của họ.
- **Mở rộng được:** Thêm chức năng mới = thêm `UserControl` mới và cắm vào đúng vị trí (Panel động, Tab mới, hoặc Modal).
- **Bảo trì được:** Tối đa tái sử dụng component; số lượng file `.Designer.cs` tối thiểu.
- **State Pattern:** Đảm bảo tính hợp lệ của mọi state transition, không thể bypass business rules.
- **Event-driven:** UI luôn cập nhật theo trạng thái thực tế, không cần polling.

### 14.3 Công Nghệ Sử Dụng

- **Backend (Logic):** C# .NET Framework 4.8
- **Frontend:** Windows Forms (WinForms)
- **Database:** JSON file storage (`Data/` folder)
- **Map:** GMap.NET 2.1.7 (`GMap.NET.WinForms` ở Presentation, `GMap.NET.Core` ở Infrastructure); Provider: Photon (geocoding) + OSRM (routing)
- **Serialization:** Newtonsoft.Json
- **Service Composition:** Manual — khởi tạo bằng `new` trong `Program.cs` (không dùng DI container)

---

## 15. Tham Khảo

| #   | Tên                                      | Link                                                                               | Dùng cho                                                                             |
| --- | ---------------------------------------- | ---------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| R01 | OSRM HTTP API                            | http://router.project-osrm.org                                                     | Tính tuyến đường, ETA, trả về encoded polyline                                       |
| R02 | Photon Geocoding API                     | https://photon.komoot.io                                                           | Geocoding địa chỉ tiếng Việt → Coordinate                                            |
| R03 | GMap.NET GitHub                          | https://github.com/judero01pol/GMap.NET                                            | Tích hợp bản đồ vào WinForms, overlay, marker, route                                 |
| R04 | Google Encoded Polyline Algorithm        | https://developers.google.com/maps/documentation/utilities/polylinealgorithm       | Decode polyline string từ OSRM response → List\<PointLatLng\>                        |
| R05 | Newtonsoft.Json TypeNameHandling         | https://www.newtonsoft.com/json/help/html/SerializeTypeNameHandling.htm            | Serialize/Deserialize đa hình (List\<User\> giữ Driver / Passenger subtype)          |
| R06 | Microsoft.Extensions.DependencyInjection | https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection      | DI container cho .NET Framework 4.8                                                  |
| R07 | Haversine Formula                        | https://en.wikipedia.org/wiki/Haversine_formula                                    | Tính khoảng cách giữa 2 Coordinate (dùng trong TripService + MatchingService filter) |
| R08 | ReaderWriterLockSlim (.NET)              | https://learn.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlockslim | Thread-safe read/write cho JsonStorage                                               |
| R09 | State Pattern (GoF)                      | https://refactoring.guru/design-patterns/state                                     | Tham chiếu implement ITripState + 8 state classes                                    |
| R10 | SemaphoreSlim (.NET)                     | https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim        | Giải race condition trong MatchingService                                            |
| R11 | Nominatim API                            | https://nominatim.org/release-docs/latest/api/Overview/                            | Geocoding địa chỉ (dùng trong MapService)                                            |

---

> **Tài liệu được cập nhật lần cuối:** 2026-05-06  
> **Người cập nhật:** Agent
