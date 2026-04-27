# TODO — Đồng bộ Source Code & Tài liệu

## Phase 1: Domain Model — Bỏ MaxPickupDistance, TripStatus enum, điều chỉnh Passenger

- [x] 1.1 `Domain/Entities/Vehicle.cs` — Xóa `GetMaxPickupDistance()` abstract
- [x] 1.2 `Domain/Entities/Vehicles/Car.cs` — Xóa override `GetMaxPickupDistance()`
- [x] 1.3 `Domain/Entities/Vehicles/Motorbike.cs` — Xóa override `GetMaxPickupDistance()`
- [x] 1.4 `Domain/Entities/Trip.cs` — Xóa `_status` field, `SetStatusInternal`, property `Status` cũ. Thêm `Status` string + helper `IsXxx()`
- [ ] 1.5 `Domain/Enums/TripStatus.cs` — Xóa toàn bộ file enum *(giữ lại tạm để backward compatibility persistence — cần refactor UI xong mới xóa)*
- [ ] 1.6 `Domain/States/RequestedState.cs` — Xóa `SetStatusInternal` calls *(vẫn còn trong code — cần refactor)*
- [ ] 1.7 `Domain/States/SearchingState.cs` — Xóa `SetStatusInternal` calls *(vẫn còn trong code — cần refactor)*
- [ ] 1.8 `Domain/States/MatchedState.cs` — Xóa `SetStatusInternal` calls *(vẫn còn trong code — cần refactor)*
- [ ] 1.9 `Domain/States/ArrivedState.cs` — Xóa `SetStatusInternal` calls *(vẫn còn trong code — cần refactor)*
- [ ] 1.10 `Domain/States/StartedState.cs` — Xóa `SetStatusInternal` calls *(vẫn còn trong code — cần refactor)*
- [ ] 1.11 `Domain/States/CancelledState.cs` — Kiểm tra & xóa `SetStatusInternal` calls *(cần refactor)*
- [ ] 1.12 `Domain/States/TimeoutState.cs` — Kiểm tra & xóa `SetStatusInternal` calls *(cần refactor)*
- [ ] 1.13 `Domain/Events/TripStatusChangedEventArgs.cs` — Đổi `TripStatus NewStatus` → `string NewStatus` *(cần refactor UI/Shells xong mới đổi)*

## Phase 2: Application Services — Wallet check, SemaphoreSlim, Async cleanup

- [x] 2.1 `Application/Services/MatchingService.cs` — Thêm `SemaphoreSlim`, kiểm tra wallet đủ commission trước khi ghép
- [x] 2.2 `Application/Services/TripService.cs` — Thêm `SemaphoreSlim` trong `MatchDriverAsync`, đổi `TripStatus` enum → string/`IsXxx()` *(SemaphoreSlim ✅; TripStatus vẫn còn trong một số query method — cần tiếp tục refactor)*
- [ ] 2.3 `Application/Services/AdminService.cs` — Đổi `TripStatus` enum → string trong các method filter *(vẫn còn TripStatus — cần refactor)*
- [x] 2.4 `Application/Services/PassengerService.cs` — Đổi `trip.Status != TripStatus.Completed` → `!trip.IsCompleted()`
- [ ] 2.5 `Application/Interfaces/IAdminService.cs` — Đổi signature `GetTripsByStatusAsync(TripStatus)` → `GetTripsByStatusAsync(string)` *(cần refactor)*
- [x] 2.6 `Application/Services/SimulationService.cs` — Implement Timer, đổi `CreateDefault` → `CreateDefaultAsync()`
- [ ] 2.7 `Application/Interfaces/ISimulationService.cs` — Thêm/cập nhật interface nếu cần *(chưa cập nhật — cần review)*

## Phase 3: Presentation — Polyline decode, TripStatus string

- [ ] 3.1 `Presentation/Program.cs` — Đổi `static void Main()` → `static async Task Main()`, gọi `await CreateDefaultAsync()` *(cần verify)*
- [x] 3.2 `Presentation/Components/MapControl.cs` — Thêm `DrawRoute(string polyline)` decode polyline
- [ ] 3.3 `Presentation/Components/TripStatusPanel.cs` — Đổi `UpdateTripStatus(TripStatus)` → `UpdateTripStatus(string)` *(cần refactor)*
- [ ] 3.4 `Presentation/Shells/AdminShell.cs` — Đổi `TripStatus` enum → string trong filter logic *(cần refactor)*
- [ ] 3.5 `Presentation/Helpers/EventHelper.cs` — Đổi event args dùng string status *(cần refactor)*

## Phase 4: Tài liệu Markdown — Đồng bộ

- [x] 4.1 `docs/Architecture_and_Design.md` — Bỏ MaxPickupDistance, phân biệt State Pattern vs State Machine, bỏ Repository khỏi Design Patterns, cập nhật Matching Algorithm + Race condition
- [x] 4.2 `docs/Domain_Reference.md` — Sửa Passenger (bỏ sealed), bỏ GetMaxPickupDistance, bỏ/cập nhật TripStatus enum
- [x] 4.3 `docs/PSPEC.md` — Sửa Passenger sealed, chuyển Repository → Architectural Abstraction, đổi Trip State Machine → Trip State Pattern
- [ ] 4.4 `docs/OOP.md` — Xóa Repository khỏi ví dụ Design Pattern *(cần kiểm tra)*
- [ ] 4.5 `docs/State.md` — Thêm ghi chú: State Pattern dùng cho Trip, Driver dùng State Machine *(cần kiểm tra)*
- [x] 4.6 `docs/Business_and_UI_Guide.md` — Xóa step Distance check < vehicle MaxPickupDistance, cập nhật Matching
- [x] 4.7 `README.md` — Cập nhật Incomplete Implementations (wallet, semaphore, simulation, polyline)
- [x] 4.8 `DEVLOG.md` — Cập nhật các dòng liên quan MaxPickupDistance, Passenger sealed, TripStatus, Repository, Simulation, Race condition
- [x] 4.9 `SOURCE_MAP.md` — Xóa dòng GetMaxPickupDistance

## Phase 5: Build & Verify

- [ ] 5.1 Build solution `OOP2026.slnx`
- [ ] 5.2 Fix compile errors nếu có
- [ ] 5.3 Review các file còn sót tham chiếu cũ (TripStatus trong States, AdminService, IAdminService, UI components)

---

## Tóm tắt trạng thái

**Đã hoàn thành:**
- Bỏ `GetMaxPickupDistance` khỏi Vehicle hierarchy
- Passenger bỏ `sealed`
- Trip chuyển sang `Status` string + `IsXxx()` helpers (domain model)
- MatchingService: SemaphoreSlim + wallet/commission check
- SimulationService: Timer + `CreateDefaultAsync()`
- MapControl: `DecodePolyline()` thêm vào
- Docs: `Architecture_and_Design.md`, `Domain_Reference.md`, `PSPEC.md`, `README.md`, `DEVLOG.md`, `SOURCE_MAP.md`, `Business_and_UI_Guide.md` đã đồng bộ

**Còn lại (cần refactor tiếp):**
- State classes vẫn gọi `SetStatusInternal(TripStatus.Xxx)` — cần xóa khi UI đã chuyển sang string status
- `TripStatusChangedEventArgs` vẫn dùng `TripStatus` enum — cần đổi sang `string` + cập nhật UI subscribers
- `AdminService`, `IAdminService`, `TripStatusPanel`, `AdminShell`, `EventHelper` vẫn tham chiếu `TripStatus` enum
- Cần build & verify compile
