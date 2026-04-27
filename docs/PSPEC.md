# RideGo — Ride-Hailing Simulation System

## 1. Tổng quan
- **System Objectives:** Mô phỏng toàn bộ workflow đặt xe (ride-hailing) theo kiểu Grab/Uber bằng C# WinForms. Mục tiêu cốt lõi: xây dựng business logic hoàn chỉnh, áp dụng OOP (kế thừa, đa hình, encapsulation, domain events), tổ chức 5-Layer Architecture, và mô phỏng vòng đời chuyến đi với dữ liệu ảo.
- **In‑scope:**
  - Đặt xe, tìm tài xế (matching), quản lý vòng đời chuyến đi (Trip lifecycle)
  - Thanh toán mô phỏng (tiền mặt), đánh giá sau chuyến
  - Quản lý tài khoản Passenger / Driver / Admin
  - Lưu trữ dữ liệu bằng JSON file
  - Tích hợp bản đồ GMap.NET (hiển thị marker, route)
  - Background workers: TripTimeoutWorker, TripMatchingWorker
  - Admin dashboard: CRUD user/trip, cấu hình FareRule, thống kê (GMV, NTR, completion rate, satisfaction)
- **Out‑of‑scope:**
  - Thanh toán online (ví điện tử, cổng thanh toán thực)
  - Real-time GPS (dùng mô phỏng vị trí)
  - Push notification thực
  - Cơ sở dữ liệu quan hệ (SQL Server / PostgreSQL)
  - LINQ và Lambda (ràng buộc giáo khoa — dùng foreach + if-else)
- **Actors:**
  - **Passenger** — đặt xe, theo dõi trạng thái, thanh toán, đánh giá
  - **Driver** — nhận chuyến, cập nhật trạng thái, xem thu nhập
  - **Admin** — quản lý toàn hệ thống, cấu hình giá, xem báo cáo
  - **System** — tự động matching, timeout, domain events

---

## 2. Yêu cầu

### 2.1 Functional Requirements

**Passenger:**
- Đăng ký / đăng nhập bằng số điện thoại + mật khẩu
- Nhập điểm đón, điểm đến, chọn loại xe (Car / Motorbike)
- Xem giá dự kiến trước khi đặt
- Gửi yêu cầu đặt xe → nhận thông báo trạng thái (Matched / Arrived / Started / Completed)
- Hủy chuyến (trước khi Started)
- Thanh toán mô phỏng (tiền mặt)
- Đánh giá tài xế sau chuyến (1–5 sao + bình luận)
- Xem lịch sử chuyến đi

**Driver:**
- Đăng ký / đăng nhập, cập nhật thông tin xe, bằng lái
- Bật / tắt trạng thái (Offline ↔ Available)
- Nhận request chuyến phù hợp (VehicleType match + Available + Wallet đủ hoa hồng)
- Cập nhật trạng thái chuyến: Arrived → Started → Completed
- Xem thu nhập (Wallet, Income), lịch sử chuyến, điểm đánh giá
- Nạp tiền ví

**Admin:**
- Đăng nhập quản trị
- CRUD hành khách / tài xế (xem, khoá tài khoản)
- Xem và quản lý chuyến đi, hủy chuyến
- Cấu hình FareRule (base fare, per-km, surge theo loại xe)
- Xem thống kê: tổng GMV, NTR, tỷ lệ hoàn thành, điểm hài lòng trung bình

**System:**
- Tự động chuyển trạng thái Trip sang Timeout nếu không ghép được tài xế trong thời gian quy định
- Background matching: TripMatchingWorker dò trip Searching → gọi MatchingService
- Domain events emit theo từng state transition

### 2.2 Non‑functional Requirements
- **Ngôn ngữ / Runtime:** C# .NET Framework 4.8, Windows Forms
- **Không dùng:** LINQ, Lambda, `var` (ràng buộc giáo khoa — static typing, explicit loop)
- **Persistence:** JSON file (Newtonsoft.Json), `TypeNameHandling.All` cho đa hình (List\<User\> chứa Driver + Passenger)
- **Thread-safety:** `ReaderWriterLockSlim` trong JsonStorage (cần xác minh đầy đủ); `SemaphoreSlim` trong MatchingService và TripService để tránh race condition khi ghép tài xế
- **Serialization:** Newtonsoft.Json 13.x — `JsonConvert.SerializeObject` / `DeserializeObject<List<T>>`
- **UI update:** Event-driven — `ITripService.TripStatusChanged` (`EventHandler<TripStatusChangedEventArgs>`) — UI subscribe không polling
- **Bảo mật mật khẩu:** `PasswordHasher` trong `Common/Utilities/`

---

## 3. Kiến trúc và công nghệ

- **Architecture:** 5-Layer Architecture (Layered — không phải Clean Architecture thuần; có vi phạm dependency)

```
Common → Domain → Application → Infrastructure → Presentation
```

| Layer | Project | Output | Phụ thuộc |
|---|---|---|---|
| Common | `Common/Common.csproj` | Library | — |
| Domain | `Domain/Domain.csproj` | Library | Common |
| Application | `Application/Application.csproj` | Library | Common, Domain |
| Infrastructure | `Infrastructure/Infrastructure.csproj` | Library | Application, Common, Domain |
| Presentation | `Presentation/Presentation.csproj` | WinExe | Application, Common, Domain, Infrastructure |

> **Vi phạm kiến trúc (đã ghi nhận):** Presentation → Domain/Infrastructure trực tiếp (nên chỉ phụ thuộc Application).

- **Tech stack:**
  - **Backend (Logic):** C# .NET Framework 4.8
  - **Frontend:** Windows Forms (WinForms)
  - **Database:** JSON file storage (`Data/` folder)
  - **Map:** GMap.NET 2.1.7 (`GMap.NET.WinForms` ở Presentation, `GMap.NET.Core` ở Infrastructure); Provider: Photon (geocoding) + OSRM (routing)
  - **Serialization:** Newtonsoft.Json
  - **Service Composition:** Manual — khởi tạo bằng `new` trong `Program.cs` (không dùng DI container)

- **Pattern:**
  | Pattern | Áp dụng |
  |---|---|
  | Repository (data access contract) | `IRepository<T>` → `JsonRepository<T>` (generic base) |
  | State Pattern | `ITripState` + 8 state classes cho Trip lifecycle |
  | State Machine | `DriverStateMachine` (static, dictionary-defined transitions) — **chỉ dùng cho Driver** |
  | Domain Events | Aggregate emit events (9 Trip + 2 Driver + 1 Review) |
  | Observer | `ITripService.TripStatusChanged` — UI subscribe |
  | Manual Service Composition | Khởi tạo services bằng `new` trong `Program.cs` |
  | Value Object | Immutable VOs: Money, Location, Coordinate, Address, Route, Fare |

- **Deployment:** Local — chạy `.exe` trực tiếp trên Windows, dữ liệu lưu file JSON cục bộ

---

## 4. Use Case

| ID | Tên | Tác nhân | Mô tả | Trạng thái |
|----|-----|----------|-------|------------|
| UC1 | Đăng nhập | User | Phone + password → `UserService.Login()` | ✅ |
| UC2 | Đăng ký tài xế | Driver | Tạo Driver + Vehicle → `UserService.RegisterDriver()` | ✅ |
| UC3 | Đăng ký hành khách | Passenger | Tạo Passenger → `UserService.RegisterPassenger()` | ✅ |
| UC4 | Đặt chuyến | Passenger | Nhập pickup/dest/vehicleType → `TripService.RequestTrip()` (sync) | ✅ |
| UC5 | Ghép tài xế | System | Lọc driver Available + VehicleType match + Wallet đủ hoa hồng → `MatchingService.MatchDriverToTripAsync()` | ✅ |
| UC6 | Đến điểm đón | Driver | → `TripService.ArriveAtPickup()` | ✅ |
| UC7 | Bắt đầu chuyến | Driver | → `TripService.StartTrip()` | ✅ |
| UC8 | Hoàn thành chuyến | Driver | → `TripService.CompleteTrip()` + payment inline | ✅ |
| UC9 | Đánh giá tài xế | Passenger | → `ReviewService.AddReviewAsync()` → cập nhật AverageRating | ✅ |
| UC10 | Hủy chuyến | Passenger/Driver | → `TripService.CancelTrip()` — được phép trước Started | ✅ |
| UC11 | Lịch sử chuyến | Passenger/Driver | → `TripRepository.GetByPassengerId/DriverId` | ✅ |
| UC12 | Thông tin tài xế matched | Passenger | Trip DTO chứa DriverId → UI hiển thị thông tin | ✅ |
| UC13 | Bật/tắt trạng thái | Driver | Offline ↔ Available → `UserService.UpdateDriverStatus()` | ✅ |
| UC14 | Nhận thông tin chuyến | Driver | → `TripService.GetTripAsync()` | ✅ |
| UC15 | Chấp nhận/Từ chối chuyến | Driver | `AcceptTripHandler` tồn tại; UI flow chưa hoàn chỉnh | ⚠️ |
| UC16 | Admin theo dõi real-time | Admin | AdminShell + DataGridView; chưa có live map | ⚠️ |
| UC17 | Cấu hình FareRule | Admin | `AdminService.CreateFareRuleAsync/UpdateFareRuleAsync` | ✅ |
| UC18 | Driver Radar (proximity) | Passenger | Chưa implement proximity search | ❌ |
| UC19 | Thu nhập tài xế | Driver | Driver entity có Income + Wallet (Money) | ✅ |
| UC20 | Báo cáo thống kê | Admin | GMV, NTR, CompletionRate, AverageSatisfaction | ✅ |
| UC21 | Dẫn đường (routing) | Driver | MapControl hiển thị route nếu có polyline; routing chưa integrated | ⚠️ |
| UC22 | Sửa thông tin cá nhân | User | `UserService` có methods; UI partial | ⚠️ |

---

## 5. Mô hình

### 5.1 Model

**Entities (có Identity + Lifecycle):**

| Entity | Loại | Vai trò chính |
|---|---|---|
| `Trip` | Aggregate Root | Vòng đời chuyến đi; chứa Route, Fare; emit 9 domain events |
| `User` (abstract) | Aggregate Root | Base class: Passenger, Driver, Admin |
| `Driver` | Entity | Trạng thái, vị trí, xe, ví, thu nhập, đánh giá |
| `Passenger` | Entity | TotalTrips |
| `Admin` | Entity | Quản trị hệ thống |
| `FareRule` | Entity | Quy định giá cước theo VehicleType; `CalculateFare(double distanceKm)` |
| `Vehicle` (abstract) | Entity | Car, Motorbike — PlateNumber, Brand, Model, Color, Type |
| `Review` | Entity | Rating (1–5), Comment, liên kết DriverId/PassengerId/TripId |

**Value Objects (immutable, identified by value):**

| VO | Thuộc tính chính |
|---|---|
| `Money` | Amount (decimal ≥ 0, rounded), Currency ("VND") — operators: `+ - < > <= >=` |
| `Location` | Compose `Coordinate` + `Address` |
| `Coordinate` | Latitude, Longitude |
| `Address` | Street, Ward, District, City |
| `Route` | Pickup (Location), Destination (Location), encoded polyline |
| `Fare` | BaseFare, PerKmRate, TotalAmount (Money) |

**Domain Services:**
- `DriverStateMachine` (static) — validate Driver state transitions
- `FareRule.CalculateFare(double)` — inline fare logic (không tách thành FareCalculationService riêng)

**Repository Interfaces (Domain layer):**
`IRepository<T>`, `IUserRepository`, `IDriverRepository`, `IPassengerRepository`, `ITripRepository`, `IVehicleRepository`, `IReviewRepository`, `IFareRuleRepository`

**Enums:**
- `TripStatus`: Requested(0), Searching(1), Matched(2), Arrived(3), Started(4), Completed(5), Cancelled(6), Timeout(7) — *(deprecated, dùng ITripState)*
- `DriverStatus`: Offline, Available, OnTrip
- `VehicleType`: Car, Motorbike

### 5.2 UML (State Transitions)

**Trip State Pattern:**
> Trip sử dụng State Pattern (ITripState), không phải State Machine.

```
Requested → Searching
Searching → Matched | Cancelled | Timeout
Matched   → Arrived | Searching | Cancelled
Arrived   → Started | Cancelled
Started   → Completed | Cancelled
Completed → (terminal)
Cancelled → (terminal)
Timeout   → (terminal)
```

**Driver State Machine:**
> Driver sử dụng State Machine (static dictionary transitions).

```
Offline   → Available
Available → OnTrip | Offline
OnTrip    → Available
```

**User Inheritance:**
```
User (abstract)
├── Passenger
├── Driver
└── Admin

Vehicle (abstract)
├── Car
└── Motorbike
```

---

## 6. Business Flow & Pseudocode

### Trip Request Flow
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

### Timeout Flow
```
[System - TripTimeoutWorker] Poll trips đang Searching (dùng trip.IsSearching())
    → trip.MarkTimeout()                                   // emit TripTimeoutEvent
    → save Trip
```

### Fare Calculation
```
FareRule.CalculateFare(distanceKm):
    totalAmount = BaseFare + (PerKmRate × distanceKm)
    return Money(totalAmount, "VND")
```

---

## 7. Testing & Acceptance Criteria

| ID | Mô tả | Kết quả mong đợi | Trạng thái |
|----|-------|------------------|------------|
| T01 | Đặt chuyến với pickup/dest hợp lệ | Trip tạo thành công, Status = Searching | ⬜ Chưa test |
| T02 | Ghép tài xế có Status = Available + VehicleType match + Wallet đủ | Trip → Matched, Driver → OnTrip | ⬜ Chưa test |
| T03 | Ghép tài xế khi không có driver phù hợp | Trip giữ Searching đến Timeout | ⬜ Chưa test |
| T04 | Trip không tìm được tài xế sau X giây | Status → Timeout, driver không bị lock | ⬜ Chưa test |
| T05 | Hoàn thành chuyến | Status → Completed, Fare tính đúng, Driver income cập nhật | ⬜ Chưa test |
| T06 | Hủy chuyến ở trạng thái Searched/Matched/Arrived | Status → Cancelled, Driver → Available | ⬜ Chưa test |
| T07 | Hủy chuyến sau Started | Từ chối — CanBeCancelled = false | ⬜ Chưa test |
| T08 | Đánh giá sau chuyến Completed | Review lưu, Driver.AverageRating cập nhật | ⬜ Chưa test |
| T09 | Admin cấu hình FareRule mới | FareRule lưu JSON, FareService dùng rule mới | ⬜ Chưa test |
| T10 | Đăng nhập sai mật khẩu | Trả về lỗi, không vào được hệ thống | ⬜ Chưa test |
| T11 | Driver Offline không được ghép | MatchingService bỏ qua driver Offline/OnTrip | ⬜ Chưa test |
| T12 | Nạp tiền ví tài xế | Driver.Wallet tăng đúng số tiền, lưu xuống JSON | ⬜ Chưa test |
