# Ride-Hailing System

## 1. Tổng quan

Hệ thống gọi xe mô phỏng (ride-hailing simulation) xây dựng bằng C# WinForms, áp dụng OOP và 5-Layer Architecture. Hệ thống mô phỏng toàn bộ workflow chuyến đi: đặt xe, tìm tài xế, di chuyển, thanh toán và đánh giá.

**Mục tiêu:**
- Xây dựng logic nghiệp vụ (business logic)
- Áp dụng OOP: kế thừa, đa hình, encapsulation, domain events
- Tổ chức 5-Layer Architecture: Common → Domain → Application → Infrastructure → Presentation
- Mô phỏng workflow chuyến đi với dữ liệu ảo

**Công nghệ:**
- .NET Framework 4.8 Windows Forms
- GMap.NET 2.1.7 (Google Maps provider)
- Newtonsoft.Json
- Microsoft.Extensions.DependencyInjection

---

## 1.1 Tóm tắt Kiến trúc và Điểm nổi bật

### 1. Kiến Trúc Hệ Thống

Hệ thống được tổ chức theo mô hình 5 lớp:

- **Common Layer**: Tiện ích chung, constants, extension methods.
- **Domain Layer**: Core business — entities, value objects, state machines, domain events, repository interfaces. Pure, không phụ thuộc hạ tầng.
- **Application Layer**: Orchestration — services (TripService, UserService, FareService, MatchingService), DTOs, Features (Commands/Handlers), interfaces.
- **Infrastructure Layer**: Technical implementations — JSON persistence (JsonRepository, JsonStorage), external APIs (MapService), repository implementations.
- **Presentation Layer**: WinForms UI — Shells, Screens, Components, ViewModels, DI composition root.

**Lưu ý:** Presentation tham chiếu trực tiếp Domain và Infrastructure (vi phạm Clean Architecture). Cần refactor để Presentation chỉ phụ thuộc vào Application.

### 2. Luồng Xử Lý Chính (Core Workflows)

Hệ thống giải quyết được các bài toán chính:

- **Quản lý trạng thái Trip**: TripStateMachine đảm bảo chỉ chuyển trạng thái hợp lệ. Flow: Requested → Searching → Matched → Arrived → Started → Completed/Cancelled/Timeout.
- **Ghép tài xế (Matching)**: MatchingService kiểm tra driverAvailable + vehicle type match (chưa đầy đủ).
- **Cơ chế Event-Driven**: TripService phát sự kiện `TripStatusChanged` để UI real-time update.

### 3. Các Thực Thể Chính (Entities & Aggregates)

| Thực thể | Loại | Vai trò |
|---|---|---|
| **Trip** | Aggregate Root | Quản lý vòng đời chuyến đi; chứa Route, Fare |
| **User** (abstract) | Aggregate Root | Base class Passenger, Driver, Admin |
| **Driver** | Entity | Vị trí, xe, ví, thu nhập, trạng thái |
| **Passenger** | Entity (sealed) | Số chuyến đã đi |
| **Admin** | Entity | Quản lý hệ thống |
| **FareRule** | Entity | Quy định giá cước theo xe |
| **Vehicle** (abstract) | Entity | Thông tin xe (Car, Motorbike) |

**Value Objects:**
Money, Coordinate, Address, Location, Route, Fare.

**Domain Events:**
Trip: Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout (9 events).
Driver: StatusChanged, LocationUpdated.
Review: ReviewCreated.

**State Machines:**
TripStateMachine (8 states), DriverStateMachine (3 states).

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
- `ITripService` / `TripService` — trip lifecycle
- `IUserService` / `UserService` — registration, auth, profile
- `IFareService` / `FareService` — fare calculation
- `ISimulationService` / `SimulationService` — stub
- `IDriverSimulationService` — missing interface (stub)
- `IReviewService` / `ReviewService` — rating (in `Services` namespace)

**Domain Services:**
- `TripStateMachine`, `DriverStateMachine` — state validation
- `FareCalculationService` (not found) — fare calculation logic inside `FareRule`

**Infrastructure Services:**
- `IMapService` / `MapService` — geocoding + routing (Photon + OSRM)

**Missing Interfaces (referenced but not implemented):**
- `IRouteService` — referenced in `FareService`, `BookTripForm`, `TripViewModel`, `PassengerViewModel`, DI registration. Needs implementation.
- `TripTimeoutWorker`, `TripMatchingWorker` — referenced in `Program.cs`, but files not found.
- `IDriverSimulationService` — referenced in `SimulationService` and DI, interface missing.

---

## 4. Repository & Persistence

**Domain Repository Interfaces** (`Domain/Repositories`):
- `IRepository<T>` (base), `IReadRepository<T>`
- `IUserRepository`, `IDriverRepository`, `IPassengerRepository`, `ITripRepository`, `IVehicleRepository`, `IReviewRepository`, `IFareRuleRepository`

**Infrastructure Implementations** (`Infrastructure/Repositories`):
- `JsonRepository<T>` — base JSON repo
- Concrete repos: `UserRepository`, `DriverRepository`, `PassengerRepository`, `TripRepository`, `VehicleRepository`, `ReviewRepository`, `FareRuleRepository`
- `FileStorage` — low-level JSON file I/O

**Storage:** File JSON under `Data/` folder. Thread-safety via `ReaderWriterLockSlim` inside `JsonStorage` (needs verification).

---

## 5. State Machines

**TripStateMachine** — 8 states:
Requested → Searching → Matched → Arrived → Started → Completed (terminal)
↘︎ Cancelled (terminal), Timeout (terminal) có thể xảy ra từ Searching/Matched/Arrived/Started.

**DriverStateMachine** — 3 states:
Offline ↔ Available ↔ OnTrip (vòng lặp).

Transition rules enforced via static `CanTransition(from, to)` methods.

---

## 6. Design Patterns

| Pattern | Usage |
|---|---|
| Repository | `IRepository<T>` → `JsonRepository<T>` |
| State Machine | `TripStateMachine`, `DriverStateMachine` |
| Domain Events | Aggregates emit events (9 Trip events, 2 Driver events, 1 Review event) |
| Value Object | Immutable VOs with operator overloading |
| DTO | `Application.DTOs` — separation between Presentation and Domain |
| CQRS-lite | `Features/` folder with Commands/Queries/Handlers (no MediatR) |
| Observer | `TripService.TripStatusChanged` event — UI subscribes |
| DI | `Microsoft.Extensions.DependencyInjection` in `Presentation/Program.cs` |

---

## 7. Known Issues & Gaps

1. **Missing Types** (compile errors):
   - `IRouteService` + `RouteService` — registered in DI but not implemented.
   - `TripTimeoutWorker` + `TripMatchingWorker` — referenced in Program.cs but not found.
   - `IDriverSimulationService` — missing interface.

2. **Architecture Violations**:
   - Presentation directly references Domain and Infrastructure (violates Clean Architecture).
   - Infrastructure references Domain (acceptable, but Domain should not depend on Infrastructure).

3. **Incomplete Implementations**:
   - `AdminService` — empty shell.
   - Driver matching algorithm — chỉ check status, chưa lọc vehicle type, distance, wallet.
   - Address-based driver filtering — chưa có.
   - Race condition handling — chưa có semaphore.
   - Simulation service — stub, không có timer.
   - Polyline decoding / map route rendering — chưa hoàn chỉnh.

4. **Inconsistent Async**:
   - `ITripService` uses async signatures, but `TripService` mixes sync and async (`.Result` usage).

5. **Namespace Issues**:
   - `ReviewService` uses `Services` namespace (should be `Application.Services`).
   - `MatchingService` uses `Domain.Services` (should be `Application.Services`).

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
