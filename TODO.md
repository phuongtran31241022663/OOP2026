# TODO — Đồng bộ Source Code & Tài liệu

## Phase 1: Domain Model — Bỏ MaxPickupDistance, TripStatus enum, điều chỉnh Passenger

- [x] 1.1 `Domain/Entities/Vehicle.cs` — Xóa `GetMaxPickupDistance()` abstract
- [x] 1.2 `Domain/Entities/Vehicles/Car.cs` — Xóa override `GetMaxPickupDistance()`
- [x] 1.3 `Domain/Entities/Vehicles/Motorbike.cs` — Xóa override `GetMaxPickupDistance()`
- [x] 1.4 `Domain/Entities/Trip.cs` — Xóa `_status` field, `SetStatusInternal`, property `Status` cũ. Thêm `Status` string + helper `IsXxx()`
- [x] 1.5 `Domain/Enums/TripStatus.cs` — Đã xóa toàn bộ file enum
- [x] 1.6 `Domain/States/RequestedState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.7 `Domain/States/SearchingState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.8 `Domain/States/MatchedState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.9 `Domain/States/ArrivedState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.10 `Domain/States/StartedState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.11 `Domain/States/CancelledState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.12 `Domain/States/TimeoutState.cs` — Không còn `SetStatusInternal` calls
- [x] 1.13 `Application/Events/TripStatusChangedEventArgs.cs` — Đã đổi `TripStatus NewStatus` → `string NewStatus`

## Phase 2: Application Services — Wallet check, SemaphoreSlim, Async cleanup

- [x] 2.1 `Application/Services/MatchingService.cs` — Thêm `SemaphoreSlim`, kiểm tra wallet đủ commission trước khi ghép
- [x] 2.2 `Application/Services/TripService.cs` — Thêm `SemaphoreSlim` trong `MatchDriverAsync`, đổi `TripStatus` enum → string/`IsXxx()`
- [x] 2.3 `Application/Services/AdminService.cs` — Đã đổi `TripStatus` enum → string trong các method filter
- [x] 2.4 `Application/Services/PassengerService.cs` — Đổi `trip.Status != TripStatus.Completed` → `!trip.IsCompleted()`
- [x] 2.5 `Application/Interfaces/IAdminService.cs` — Đã đổi signature `GetTripsByStatusAsync(TripStatus)` → `GetTripsByStatusAsync(string)`
- [x] 2.6 `Application/Services/SimulationService.cs` — Implement Timer, đổi `CreateDefault` → `CreateDefaultAsync()`
- [x] 2.7 `Application/Interfaces/ISimulationService.cs` — Đã review, không cần cập nhật thêm

## Phase 3: Presentation — Polyline decode, TripStatus string

- [x] 3.1 `Presentation/Program.cs` — Đã đổi `static void Main()` → `static async Task Main()`, gọi `await AppServiceBundle.CreateDefaultAsync()`
- [x] 3.2 `Presentation/Components/MapControl.cs` — Thêm `DrawRoute(string polyline)` decode polyline
- [x] 3.3 `Presentation/Components/TripStatusPanel.cs` — Đã đổi `UpdateTripStatus(TripStatus)` → `UpdateTripStatus(string)`
- [x] 3.4 `Presentation/Shells/AdminShell.cs` — Đã đổi `TripStatus` enum → string trong filter logic
- [x] 3.5 `Presentation/Helpers/EventHelper.cs` — Đã đổi event args dùng string status

## Phase 4: Driver State Pattern refactor

- [x] 4.1 `Domain/States/IDriverState.cs` — Tạo interface `IDriverState` với `SetAvailable`, `SetOnTrip`, `SetOffline`
- [x] 4.2 `Domain/States/Drivers/DriverOfflineState.cs` — Tạo state `Offline`
- [x] 4.3 `Domain/States/Drivers/DriverAvailableState.cs` — Tạo state `Available`
- [x] 4.4 `Domain/States/Drivers/DriverOnTripState.cs` — Tạo state `OnTrip`
- [x] 4.5 `Domain/Entities/Users/Driver.cs` — Chuyển từ `DriverStatus` enum + `DriverStateMachine` sang `IDriverState` pattern. `Status` trả về string. Thêm `IsAvailable()`, `IsOnTrip()`, `IsOffline()`. Giữ `SerializedStatus` cho JSON backward compatibility.
- [x] 4.6 `Domain/Events/DriverStatusChangedEvent.cs` — Đổi `OldStatus`/`NewStatus` từ `DriverStatus` enum sang `string`
- [x] 4.7 `Application/Interfaces/IUserService.cs` — Đổi `UpdateDriverStatusAsync(Guid, DriverStatus)` → `UpdateDriverStatusAsync(Guid, string)`
- [x] 4.8 `Application/Services/UserService.cs` — Cập nhật `UpdateDriverStatusAsync` dùng string status
- [x] 4.9 `Presentation/Shells/DriverShell.cs` — Đổi `DriverStatus` enum → string status
- [x] 4.10 `Presentation/Components/DriverCardControl.cs` — Đổi `DriverStatus` enum → string status
- [x] 4.11 `Infrastructure/Repositories/DriverRepository.cs` — Đổi `d.Status == DriverStatus.Available` → `d.IsAvailable()`
- [x] 4.12 `Infrastructure/Repositories/UserRepository.cs` — Đổi `d.Status == DriverStatus.Available` → `d.IsAvailable()`
- [x] 4.13 `Application/Services/MatchingService.cs` — Đổi `driver.Status != DriverStatus.Available` → `!driver.IsAvailable()`
- [x] 4.14 `Domain/StateMachines/DriverStateMachine.cs` — Đã xóa

## Phase 5: Build & Verify

- [x] 5.1 Build solution `OOP2026.slnx` — **Build thành công, không lỗi**
- [x] 5.2 Fix compile errors — Đã fix namespace collision `Application` vs `System.Windows.Forms.Application` trong `Program.cs`; thêm `Events\TripStatusChangedEventArgs.cs` vào `Application.csproj`
- [x] 5.3 Review các file còn sót tham chiếu cũ — `TripStatus` enum đã xóa hoàn toàn; `DriverStatus` enum chỉ còn trong `Driver.cs` (SerializedStatus cho JSON backward compatibility) và `Domain/Enums/DriverStatus.cs`

## Phase 6: Tài liệu Markdown — Đồng bộ

- [x] 6.1 `docs/Architecture_and_Design.md` — Bỏ MaxPickupDistance, phân biệt State Pattern vs State Machine, bỏ Repository khỏi Design Patterns, cập nhật Matching Algorithm + Race condition
- [x] 6.2 `docs/Domain_Reference.md` — Sửa Passenger (bỏ sealed), bỏ GetMaxPickupDistance, bỏ/cập nhật TripStatus enum
- [x] 6.3 `docs/PSPEC.md` — Sửa Passenger sealed, chuyển Repository → Architectural Abstraction, đổi Trip State Machine → Trip State Pattern
- [x] 6.4 `docs/OOP.md` — Đã kiểm tra, không có Repository trong ví dụ Design Pattern (chỉ nhắc đến ở mục áp dụng pattern)
- [x] 6.5 `docs/State.md` — Đã thêm ghi chú: State Pattern dùng cho Trip **và Driver**, bỏ State Machine cho Driver
- [x] 6.6 `docs/Business_and_UI_Guide.md` — Xóa step Distance check < vehicle MaxPickupDistance, cập nhật Matching
- [x] 6.7 `README.md` — Cập nhật Incomplete Implementations (wallet, semaphore, simulation, polyline)
- [x] 6.8 `DEVLOG.md` — Cập nhật các dòng liên quan MaxPickupDistance, Passenger sealed, TripStatus, Repository, Simulation, Race condition
- [x] 6.9 `SOURCE_MAP.md` — Cập nhật: Driver State Pattern, xóa DriverStateMachine, thêm IDriverState + 3 concrete states

---

## Tóm tắt trạng thái

**Đã hoàn thành:**
- Bỏ `GetMaxPickupDistance` khỏi Vehicle hierarchy
- Passenger bỏ `sealed`
- Trip chuyển sang `Status` string + `IsXxx()` helpers (domain model)
- Xóa `TripStatus` enum khỏi toàn bộ codebase
- `TripStatusChangedEventArgs` dùng `string NewStatus`
- MatchingService: SemaphoreSlim + wallet/commission check
- SimulationService: Timer + `CreateDefaultAsync()`
- MapControl: `DecodePolyline()` thêm vào
- **Driver chuyển sang State Pattern**: `IDriverState` + `DriverOfflineState`/`DriverAvailableState`/`DriverOnTripState`
- `DriverStatusChangedEvent` dùng `string` status
- `AdminService`, `IAdminService`, `TripStatusPanel`, `AdminShell`, `EventHelper` đã chuyển sang string status
- `DriverRepository`, `UserRepository`, `MatchingService` dùng `IsAvailable()` thay vì `DriverStatus.Available`
- Build solution thành công (0 errors)
- Docs: `Architecture_and_Design.md`, `Domain_Reference.md`, `PSPEC.md`, `README.md`, `DEVLOG.md`, `Business_and_UI_Guide.md` đã đồng bộ

**Tất cả các hạng mục đã hoàn thành. Không còn công việc nào.**
