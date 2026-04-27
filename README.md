# Ride-Hailing System

## 1. Tổng quan

Hệ thống gọi xe mô phỏng (ride-hailing simulation) xây dựng bằng C# WinForms, áp dụng OOP. Hệ thống mô phỏng toàn bộ workflow chuyến đi: đặt xe, tìm tài xế, di chuyển, thanh toán và đánh giá.

**Mục tiêu:**
- Xây dựng logic nghiệp vụ (business logic)
- Áp dụng OOP: kế thừa, đa hình, encapsulation, domain events
- Tổ chức 5-Layer: Common → Domain → Application → Infrastructure → Presentation
- Mô phỏng workflow chuyến đi với dữ liệu ảo

**Công nghệ:**
- .NET Framework 4.8 Windows Forms
- GMap.NET 2.1.7 (Google Maps provider)
- Newtonsoft.Json
- Manual Service Composition (khởi tạo bằng `new` trong `Program.cs`)

---

## 1.1 Kiến trúc và Điểm nổi bật

### 1. Kiến Trúc Hệ Thống

Hệ thống được tổ chức theo mô hình 5 lớp:

- **Common Layer**: Tiện ích chung, constants, extension methods.
- **Domain Layer**: Core business — entities, value objects, state machines, domain events, repository interfaces. Pure, không phụ thuộc hạ tầng.
- **Application Layer**: Orchestration — services (TripService, UserService, FareService, MatchingService), interfaces.
- **Infrastructure Layer**: Technical implementations — JSON persistence (JsonRepository, JsonStorage), external APIs (MapService — Photon + OSRM), `GMapProviders` (Routing/Geocoding API calls — chỉ cần `GMap.NET.Core`).
- **Presentation Layer**: WinForms UI — Shells, Screens, Components (`GMapControl` — UI widget, cài `GMap.NET.WinForms`), ViewModels, manual composition root.

**Lưu ý:** Presentation tham chiếu trực tiếp Domain và Infrastructure (vi phạm Clean Architecture do tạm thời chưa triển khai Data Transfer Objects (DTOs)). Cần refactor sau để Presentation chỉ phụ thuộc vào Application.

### 2. Luồng Xử Lý Chính (Core Workflows)

Hệ thống giải quyết được các bài toán chính:

- **Quản lý trạng thái Trip**: Trip sử dụng **State Pattern** (`ITripState`) đảm bảo chỉ chuyển trạng thái hợp lệ. Flow: Requested → Searching → Matched → Arrived → Started → Completed/Cancelled/Timeout. **Driver** sử dụng **State Machine** (`DriverStateMachine`) — phân biệt rõ 2 cách quản lý trạng thái.
- **Ghép tài xế (Matching)**: MatchingService lọc driver theo: `Status == Available` + `VehicleType` match + `Wallet` đủ trả hoa hồng (Commission). Có `SemaphoreSlim` để tránh race condition khi 2 tài xế cùng nhận 1 chuyến.
- **Cơ chế Event-Driven**: `ITripService.TripStatusChanged` event (`EventHandler<TripStatusChangedEventArgs>`) để UI real-time update.

### 3. Các Thực Thể Chính (Entities & Aggregates)

| Thực thể | Loại | Vai trò |
|---|---|---|
| **Trip** | Aggregate Root | Quản lý vòng đời chuyến đi; chứa Route, Fare |
| **User** (abstract) | Aggregate Root | Base class Passenger, Driver, Admin |
| **Driver** | Entity | Vị trí, xe, ví, thu nhập, trạng thái |
| **Passenger** | Entity | Số chuyến đã đi |
| **Admin** | Entity | Quản lý hệ thống |
| **FareRule** | Entity | Quy định giá cước theo xe |
| **Vehicle** (abstract) | Entity | Thông tin xe (Car, Motorbike) |

**Value Objects:**
Money, Coordinate, Address, Location, Route, Fare.

**Domain Events:**
Trip: Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout (9 events).
Driver: StatusChanged, LocationUpdated.
Review: ReviewCreated.

**State Validation:**
Trip State Pattern (8 states via `ITripState`), DriverStateMachine (3 states).

---

## 2. Solution Structure (Projects + Layer Mapping)

| Project | Path | Output | Layer | Direct References |
|---|---|---|---|---|
| Application | `Application/Application.csproj` | Library | Application | Common, Domain |
| Common | `Common/Common.csproj` | Library | Common | — |
| Domain | `Domain/Domain.csproj` | Library | Domain | Common |
| Infrastructure | `Infrastructure/Infrastructure.csproj` | Library | Infrastructure | Application, Common, Domain |
| Presentation | `Presentation/Presentation.csproj` | WinExe | Presentation | Application, Common, Domain, Infrastructure |

**Note:** Architecture violations: Presentation → Domain/Infrastructure. Infrastructure → Domain.

---

## 3. Key Services & Interfaces

**Application Services** (`Application.Interfaces` + `Application.Services`):
- `ITripService` / `TripService` — trip lifecycle (async, có SemaphoreSlim lock trong MatchDriverAsync)
- `IUserService` / `UserService` — registration, auth, profile
- `IFareService` / `FareService` — fare calculation
- `ISimulationService` / `SimulationService` — có System.Threading.Timer, async CreateDefaultAsync()
- `IReviewService` / `ReviewService` — rating (in `Services` namespace)

**Domain Services:**
- `DriverStateMachine` — state validation for Driver
- `FareRule.CalculateFare(double)` — fare calculation logic (no separate `FareCalculationService`)

**Infrastructure Services:**
- `IMapService` / `MapService` — geocoding + routing (Photon + OSRM)

**Note on interfaces:**
- `IRouteService` was previously referenced but has been replaced by `IMapService` in current DI registration.
- `TripTimeoutWorker` + `TripMatchingWorker` — implemented in `Infrastructure/BackgroundJobs/`.

---

## 4. Repository & Persistence

> **Note:** `IRepository<T>` định nghĩa **data access contract** ở Domain layer, được triển khai bởi `JsonRepository<T>` ở Infrastructure. Đây là **Architectural Abstraction**, không phải GoF Design Pattern.

**Domain Repository Interfaces** (`Domain/Repositories`):
- `IRepository<T>` (base), `IReadRepository<T>`
- `IUserRepository`, `IDriverRepository`, `IPassengerRepository`, `ITripRepository`, `IVehicleRepository`, `IReviewRepository`, `IFareRuleRepository`

**Infrastructure Implementations** (`Infrastructure/Repositories`):
- `JsonRepository<T>` — base JSON repo
- Concrete repos: `UserRepository`, `DriverRepository`, `PassengerRepository`, `TripRepository`, `VehicleRepository`, `ReviewRepository`, `FareRuleRepository`
- `FileStorage` — low-level JSON file I/O

**Storage:** File JSON under `Data/` folder. Thread-safety via `ReaderWriterLockSlim` inside `JsonStorage` (needs verification).

---

## 5. State Validation

**Trip — State Pattern** — 8 states via `ITripState` implementations:
Requested → Searching → Matched → Arrived → Started → Completed (terminal)
↘︎ Cancelled (terminal), Timeout (terminal) có thể xảy ra từ Searching/Matched/Arrived/Started.

> Trip dùng **State Pattern** (behavior delegation), không phải State Machine.

**Driver — State Machine** — 3 states:
Offline ↔ Available ↔ OnTrip (vòng lặp).

> Driver dùng **State Machine** (static dictionary transitions).

Transition rules enforced via `DriverStateMachine.CanTransition(from, to)` static method and `ITripState` state classes.

---

## 6. Patterns & Abstractions

| Pattern / Abstraction | Usage |
|---|---|
| Repository (Data Access Contract) | `IRepository<T>` → `JsonRepository<T>` — *không phải GoF Pattern* |
| State Pattern | `ITripState` + state classes cho Trip lifecycle |
| State Machine | `DriverStateMachine` cho Driver status transitions |
| Domain Events | Aggregates emit events (9 Trip events, 2 Driver events, 1 Review event) |
| Value Object | Immutable VOs with operator overloading |
| Observer | `ITripService.TripStatusChanged` event — UI subscribes |
| Manual Service Composition | Khởi tạo services bằng `new` trong `Presentation/Program.cs` |

---

## 7. Known Issues & Gaps

1. **Đã hoàn thành**:
   - `MatchingService` — đã thêm kiểm tra `Wallet >= Commission` và `SemaphoreSlim` để tránh race condition.
   - `SimulationService` — đã implement `System.Threading.Timer` với `StartSimulation/StopSimulation`.
   - `MapControl` — đã thêm `DecodePolyline` và `DrawRoute` với route points.
   - `Trip` — đã chuyển từ `TripStatus` enum sang `Status` string + `IsXxx()` helpers từ `ITripState`.
   - `Passenger` — đã bỏ `sealed`.
   - `Vehicle` — đã bỏ `GetMaxPickupDistance()`.

2. **Architecture Violations**:
   - Presentation directly references Domain and Infrastructure (violates Clean Architecture).
   - Infrastructure references Domain (acceptable per layered architecture, but Domain should not depend on Infrastructure).

3. **Incomplete Implementations**:
   - Driver matching algorithm — chưa lọc theo địa chỉ hành chính, khoảng cách Haversine.
   - Simulation service — có timer nhưng logic simulation chưa hoàn chỉnh (chỉ stub Tick/SimulateTripProgress).
   - Polyline decoding / map route rendering — DecodePolyline đã thêm nhưng chưa test tích hợp đầy đủ.

4. **Namespace Notes**:
   - `ReviewService` is in `Application.Services` namespace ✅.
   - `MatchingService` is in `Application.Services` namespace ✅.

---

## 8. Build & Run

Restore NuGet packages:
```
nuget restore
```

Build solution (requires Visual Studio 2022 or MSBuild):
```
msbuild RideGo.sln
```

Run:
```
.\Presentation\bin\Debug\Presentation.exe
```

---

*Last updated: 2026-04-24 — synced with current codebase state.*
