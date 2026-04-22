# 📚 Domain Class Library – Thuộc tính, Phương thức & Sự kiện

> Tài liệu tổng hợp toàn bộ các lớp trong tầng **Domain** của dự án OOP2026 (Ride-Hailing App).

---

## 🏗️ Cấu trúc tổng quan

```
Domain/
├── SharedKernel/       ← Lớp cơ sở (base classes & interfaces)
├── Enums/              ← Các kiểu liệt kê
├── ValueObjects/       ← Value Objects (bất biến)
├── Users/              ← Aggregate: User (Admin, Passenger, Driver)
│   └── Drivers/
│       ├── Vehicles/   ← Vehicle, Car, Motorbike
│       └── Events/     ← DriverStatusChangedEvent
├── Trips/              ← Aggregate: Trip
│   └── Events/         ← 9 TripEvents
├── FareRule/           ← Entity: FareRule
├── Services/           ← Domain Services
└── StateMachines/      ← State Machines
```

---

## 1. SharedKernel – Lớp Cơ Sở

### `Entity<TId>` *(abstract)*
**Namespace:** `Domain.SharedKernel`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `TId Id` | Định danh duy nhất của entity |
| 🔵 Property | `IReadOnlyCollection<DomainEvent> DomainEvents` | Danh sách domain events chưa dispatch |
| 🟢 Method | `AddDomainEvent(DomainEvent)` | *(protected)* Thêm event vào danh sách |
| 🟢 Method | `ClearDomainEvents()` | Xóa toàn bộ events sau khi dispatch |
| 🟢 Method | `Equals(object)` | So sánh theo Id |
| 🟢 Method | `GetHashCode()` | Hash code theo Id |

---

### `ValueObject` *(abstract)*
**Namespace:** `Domain.SharedKernel`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🟢 Method | `GetEqualityComponents()` *(abstract protected)* | Trả về các thành phần dùng để so sánh bằng |
| 🟢 Method | `Equals(object)` | So sánh theo từng component |
| 🟢 Method | `GetHashCode()` | Hash code theo components |

---

### `DomainEvent` *(abstract)*
**Namespace:** `Domain.SharedKernel`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `Guid Id` | ID duy nhất của event |
| 🔵 Property | `DateTime OccurredOn` | Thời điểm event xảy ra (UTC) |
| 🟢 Method | `Equals(DomainEvent)` | So sánh theo Id |

---

### `IRepository<T>` *(interface)*

| Phương thức | Mô tả |
|-------------|--------|
| `Task InitializeAsync()` | Khởi tạo repository |
| `Task SaveChangesAsync()` | Lưu thay đổi |
| `T GetById(Guid id)` | Lấy theo Id |
| `IEnumerable<T> GetAll()` | Lấy toàn bộ |
| `void Add(T entity)` | Thêm mới |
| `void Update(T entity)` | Cập nhật |
| `void Delete(Guid id)` | Xóa |

### `IUnitOfWork` *(interface)*

| Phương thức | Mô tả |
|-------------|--------|
| `Task SaveChangesAsync()` | Lưu thay đổi |
| `Task BeginTransactionAsync()` | Bắt đầu transaction |
| `Task CommitAsync()` | Commit transaction |
| `Task RollbackAsync()` | Rollback transaction |

---

## 2. Enums – Kiểu Liệt Kê

### `TripStatus`
| Giá trị | Số | Ý nghĩa |
|---------|----|---------|
| `Requested` | 0 | Khách vừa yêu cầu chuyến |
| `Searching` | 1 | Hệ thống đang tìm tài xế |
| `Matched` | 2 | Đã tìm được & tài xế chấp nhận |
| `Arrived` | 3 | Tài xế đã đến điểm đón |
| `Started` | 4 | Chuyến đang diễn ra |
| `Completed` | 5 | Hoàn thành |
| `Cancelled` | 6 | Đã hủy |
| `Timeout` | 7 | Hết thời gian tìm tài xế |

### `DriverStatus`
| Giá trị | Ý nghĩa |
|---------|---------|
| `Offline` | Đang nghỉ |
| `Available` | Sẵn sàng nhận chuyến |
| `OnTrip` | Đang trong chuyến |

### `VehicleType`
| Giá trị | Ý nghĩa |
|---------|---------|
| `Car` | Xe ô tô |
| `Motorbike` | Xe máy |

---

## 3. Value Objects

### `Money` *(sealed)*
**Namespace:** `Domain.ValueObjects`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `decimal Amount` | Số tiền (làm tròn 2 chữ số thập phân) |
| 🔵 Property | `string Currency` | Loại tiền tệ (mặc định: `"VND"`) |
| ➕ Operator | `Money + Money` | Cộng tiền (cùng currency) |
| ➖ Operator | `Money - Money` | Trừ tiền (không được âm) |
| 🔵 Operator | `<`, `>`, `<=`, `>=` | So sánh số tiền |
| 🟢 Method | `ToString()` | Format: `"100,000 VND"` |

> **Bất biến (Immutable):** Mọi phép tính trả về object `Money` mới.

---

### `Fare` *(sealed)*
**Namespace:** `Domain.ValueObjects`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `Money TotalAmount` | Tổng cước hành khách trả |
| 🔵 Property | `Money Commission` | Hoa hồng hệ thống |
| 🔵 Property | `Money DriverIncome` | Thu nhập tài xế = TotalAmount − Commission |

---

### `Location` *(sealed)*
**Namespace:** `Domain.ValueObjects`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `Address Address` | Địa chỉ văn bản |
| 🔵 Property | `double Lat` | Vĩ độ (−90 đến 90) |
| 🔵 Property | `double Lng` | Kinh độ (−180 đến 180) |
| 🟢 Method | `GetDistanceKm(Location other)` | Tính khoảng cách theo công thức Haversine (km) |
| 🟢 Method | `IsDriverArrived(Location driverLoc, double thresholdMeters = 100)` | Kiểm tra tài xế đã đến điểm đón chưa |
| 🟢 Method | `ToString()` | Trả về chuỗi địa chỉ |

---

### `Address` *(sealed)*
**Namespace:** `Domain.ValueObjects`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `string PlaceName` | Tên địa danh (optional) |
| 🔵 Property | `string Street` | Tên đường *(bắt buộc)* |
| 🔵 Property | `string Ward` | Phường/Xã |
| 🔵 Property | `string District` | Quận/Huyện |
| 🔵 Property | `string City` | Thành phố *(bắt buộc)* |
| 🟢 Method | `ToString()` | `"[PlaceName] Street, Ward, District, City"` |

---

### `Route` *(sealed)*
**Namespace:** `Domain.Trips`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `Location Pickup` | Điểm đón |
| 🔵 Property | `Location Destination` | Điểm đến |
| 🔵 Property | `double Distance` | Khoảng cách (km, ≥ 0) |
| 🔵 Property | `TimeSpan Duration` | Thời gian ước tính |

---

### `Review` *(sealed)*
**Namespace:** `Domain.Trips`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `int Value` | Điểm đánh giá (1–5) |
| 🔵 Property | `string Comment` | Bình luận |
| 🔵 Property | `DateTime CreatedAt` | Thời điểm đánh giá |

---

## 4. Users – Aggregate

### `User` *(abstract)* → kế thừa `Entity<Guid>`
**Namespace:** `Domain.Users`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `string Name` | Tên người dùng *(validated, không trống)* |
| 🔵 Property | `string Phone` | Số điện thoại *(validated)* |
| 🔵 Property | `string Password` | Mật khẩu đã hash (readonly) |
| 🔵 Property | `bool IsActive` | Trạng thái tài khoản |
| 🟢 Method | `UpdateName(string newName)` | Cập nhật tên |
| 🟢 Method | `UpdatePhone(string newPhone)` | Cập nhật số điện thoại |
| 🟢 Method | `ChangePassword(string oldRaw, string newRaw)` | Đổi mật khẩu (validate old pass) |
| 🟢 Method | `VerifyPassword(string rawInput)` | Kiểm tra mật khẩu |
| 🟢 Method | `GetInfo()` *(virtual)* | Thông tin cơ bản |

---

### `Admin` → kế thừa `User`
**Namespace:** `Domain.Users`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🟢 Method | `GetInfo()` *(override)* | Tiêu đề "TÀI KHOẢN QUẢN TRỊ VIÊN" + thông tin cơ bản |

---

### `Passenger` *(sealed)* → kế thừa `User`
**Namespace:** `Domain.Users.Passengers`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `int TotalTrips` | Tổng số chuyến đã đi |
| 🔵 Property | `int Version` | Phiên bản (optimistic concurrency) |
| 🟢 Method | `AddTrip()` | Tăng tổng số chuyến (khi tài khoản active) |
| 🟢 Method | `Deactivate()` | Khóa tài khoản |
| 🟢 Method | `Activate()` | Mở khóa tài khoản |
| 🟢 Method | `GetInfo()` *(override)* | Trạng thái + tổng chuyến |

---

### `Driver` → kế thừa `User`
**Namespace:** `Domain.Users.Drivers`

#### Thuộc tính (Properties)

| Tên | Kiểu | Mô tả |
|-----|------|--------|
| `Status` | `DriverStatus` | Trạng thái hiện tại (Offline/Available/OnTrip) |
| `Position` | `Location` | Vị trí hiện tại |
| `Vehicle` | `Vehicle` | Phương tiện đang sử dụng |
| `Wallet` | `Money` | Số dư ví |
| `Income` | `Money` | Tổng thu nhập |
| `TotalTrips` | `int` | Tổng số chuyến đã hoàn thành |
| `TotalReviews` | `int` | Tổng số lượt đánh giá |
| `AverageReview` | `decimal` | Điểm đánh giá trung bình |
| `Version` | `int` | Phiên bản (optimistic concurrency) |

#### Phương thức quản lý trạng thái tài khoản

| Tên | Mô tả |
|-----|--------|
| `Deactivate(Guid actorId)` | Khóa tài khoản (không tự khóa, không khóa khi đang OnTrip) |
| `Activate()` | Mở khóa tài khoản |

#### Phương thức quản lý trạng thái lái xe

| Tên | Mô tả |
|-----|--------|
| `SetAvailable()` | Chuyển sang sẵn sàng (kích hoạt `DriverStatusChangedEvent`) |
| `SetOnTrip()` | Chuyển sang đang trong chuyến |
| `SetOffline()` | Chuyển sang nghỉ (không được khi OnTrip) |

#### Phương thức nghiệp vụ

| Tên | Mô tả |
|-----|--------|
| `UpdateVehicle(Vehicle newVehicle)` | Cập nhật phương tiện |
| `AddTrip()` | Cộng số chuyến (chỉ khi Available) |
| `UpdateReview(int newScore, int? oldScore)` | Cập nhật điểm trung bình |
| `DepositToWallet(Money amount)` | Nạp tiền vào ví |
| `ConfirmCashPayment(Fare fare)` | Trừ hoa hồng từ ví, cộng thu nhập |
| `CanReceiveTrip()` | Kiểm tra có thể nhận chuyến không (Active + Available) |

#### 🔔 Sự kiện (Domain Events)

| Sự kiện | Kích hoạt khi |
|---------|---------------|
| `DriverStatusChangedEvent` | `SetAvailable()`, `SetOnTrip()`, `SetOffline()` |

---

## 5. Vehicle – Hierarchy

### `Vehicle` *(abstract)* → kế thừa `ValueObject`
**Namespace:** `Domain.Users.Drivers.Vehicles`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `Guid DriverId` | ID tài xế sở hữu |
| 🔵 Property | `string PlateNumber` | Biển số xe |
| 🔵 Property | `string Brand` | Hãng xe |
| 🔵 Property | `string Model` | Dòng xe |
| 🔵 Property | `string Color` | Màu xe |
| 🔵 Property | `int Capacity` | Số chỗ ngồi |
| 🔵 Property | `VehicleType Type` | Loại xe |
| 🟢 Method | `CloneWithDriver(Guid driverId)` *(abstract)* | Tạo bản sao gắn với tài xế |
| 🟢 Method | `GetVehicleType()` *(abstract)* | Tên loại xe |
| 🟢 Method | `IsCar()` *(abstract)* | Có phải ô tô không |
| 🟢 Method | `GetMinSpeed()` *(abstract)* | Tốc độ tối thiểu (km/h) |
| 🟢 Method | `GetMaxSpeed()` *(abstract)* | Tốc độ tối đa (km/h) |
| 🟢 Method | `GetMaxPickupDistance()` *(abstract)* | Khoảng cách đón tối đa (km) |

### `Car` → kế thừa `Vehicle`
- **Capacity:** 4 chỗ | **Type:** `VehicleType.Car`
- `GetMinSpeed()` → 40 km/h | `GetMaxSpeed()` → 70 km/h
- `GetMaxPickupDistance()` → 7 km | `IsCar()` → `true`

### `Motorbike` → kế thừa `Vehicle`
- **Capacity:** 2 chỗ | **Type:** `VehicleType.Motorbike`
- `GetMinSpeed()` → 25 km/h | `GetMaxSpeed()` → 45 km/h
- `GetMaxPickupDistance()` → 5 km | `IsCar()` → `false`

---

## 6. Trip – Aggregate Root → kế thừa `Entity<Guid>`
**Namespace:** `Domain.Trips`

### Thuộc tính (Properties)

| Tên | Kiểu | Mô tả |
|-----|------|--------|
| `Status` | `TripStatus` | Trạng thái hiện tại |
| `PassengerId` | `Guid` | ID hành khách |
| `DriverId` | `Guid?` | ID tài xế (nullable, trước khi matched) |
| `VehicleType` | `VehicleType` | Loại xe yêu cầu |
| `Route` | `Route` | Thông tin tuyến đường |
| `Fare` | `Fare` | Thông tin cước |
| `Distance` | `double?` | Khoảng cách (km) |
| `Duration` | `double?` | Thời gian (giây) |
| `IsRated` | `bool` | Đã được đánh giá chưa |
| `IsPaid` | `bool` | Đã thanh toán chưa |
| `RequestAt` | `DateTime` | Thời điểm yêu cầu |
| `PassengerReview` | `Review` | Đánh giá của hành khách |
| `Version` | `int` | Optimistic concurrency |

### Phương thức quản lý trạng thái

| Tên | Mô tả | Kích hoạt Event |
|-----|--------|-----------------|
| `SetSearching()` | → Searching | `TripSearchingEvent` |
| `MatchDriver(Guid driverId)` | → Matched, gán tài xế | `TripMatchedEvent` |
| `MarkAsArrived()` | → Arrived | `TripArrivedEvent` |
| `StartTrip()` | → Started | `TripStartedEvent` |
| `CompleteTrip(Fare fare)` | → Completed, gán cước | `TripCompletedEvent` |
| `ConfirmPayment()` | Xác nhận đã thanh toán | `TripPaidEvent` |
| `Cancel(string reason)` | → Cancelled | `TripCancelledEvent` |
| `MarkTimeout()` | → Timeout (chỉ từ Searching) | `TripTimeoutEvent` |
| `SetStatus(TripStatus)` | Chuyển trạng thái (qua StateMachine) | — |

### Phương thức nghiệp vụ khác

| Tên | Mô tả |
|-----|--------|
| `RateByPassenger(Guid, Review)` | Hành khách đánh giá (chỉ sau Completed) |
| `MarkAsRated()` | Đánh dấu đã đánh giá |
| `CanBeCancelled()` | Kiểm tra có thể hủy không |
| `EnsureCanAssign(Trip, Driver)` | *(internal)* Validate điều kiện gán tài xế |

---

## 7. FareRule – Entity → kế thừa `Entity<Guid>`
**Namespace:** `Domain.FareRule`

| Loại | Tên | Mô tả |
|------|-----|--------|
| 🔵 Property | `VehicleType VehicleType` | Loại xe áp dụng |
| 🔵 Property | `Money BaseFare` | Cước cố định |
| 🔵 Property | `Money PricePerKm` | Giá mỗi km |
| 🔵 Property | `decimal CommissionRate` | Tỷ lệ hoa hồng (0–1) |
| 🔵 Property | `DateTime UpdatedAt` | Lần cập nhật cuối |
| 🔵 Property | `int Version` | Phiên bản |
| 🟢 Method | `UpdateRule(Money, Money, decimal)` | Cập nhật quy tắc giá |
| 🟢 Method | `CalculateFare(double distanceKm)` | Tính tiền cước theo km → trả về `Fare` |

---

## 8. Domain Events – Sự Kiện Miền

### Trip Events

| Sự kiện | Properties | Kích hoạt bởi |
|---------|-----------|---------------|
| `TripRequestedEvent` | `TripId`, `PassengerId`, `Pickup`, `Destination`, `VehicleType` | `new Trip(...)` |
| `TripSearchingEvent` | `TripId` | `Trip.SetSearching()` |
| `TripMatchedEvent` | `TripId`, `DriverId`, `VehicleType` | `Trip.MatchDriver()` |
| `TripArrivedEvent` | `TripId` | `Trip.MarkAsArrived()` |
| `TripStartedEvent` | `TripId` | `Trip.StartTrip()` |
| `TripCompletedEvent` | `TripId`, `PassengerId`, `DriverId`, `Fare` | `Trip.CompleteTrip()` |
| `TripPaidEvent` | `TripId`, `PassengerId`, `DriverId`, `TotalAmount`, `PaidAt` | `Trip.ConfirmPayment()` |
| `TripCancelledEvent` | `TripId`, `Reason` | `Trip.Cancel()` |
| `TripTimeoutEvent` | `TripId` | `Trip.MarkTimeout()` |

### Driver Events

| Sự kiện | Properties | Kích hoạt bởi |
|---------|-----------|---------------|
| `DriverStatusChangedEvent` | `DriverId`, `OldStatus`, `NewStatus` | `SetAvailable()`, `SetOnTrip()`, `SetOffline()` |

> **Tất cả events** đều kế thừa `DomainEvent` và có: `Guid Id` + `DateTime OccurredOn`.

---

## 9. Luồng Trạng Thái (State Machine)

### TripStatus Flow
```
Requested → Searching → Matched → Arrived → Started → Completed
                                                     → Cancelled
                ↓ (timeout)
             Timeout
```

### DriverStatus Flow
```
Offline ↔ Available → OnTrip → Available
```

---

## 📝 Ghi chú quan trọng

- **Bất biến (Immutability):** `Money`, `Location`, `Address`, `Fare`, `Route`, `Review` là **Value Objects** – không thể thay đổi sau khi tạo.
- **Optimistic Concurrency:** `Driver`, `Passenger`, `Trip`, `FareRule` đều có property `Version`.
- **Domain Events:** Được tích lũy trong entity và dispatch sau khi `SaveChanges` – không dispatch trực tiếp.
- **State Machine:** Mọi chuyển trạng thái của `Trip` và `Driver` phải đi qua `TripStateMachine` / `DriverStateMachine` để đảm bảo quy tắc nghiệp vụ.
