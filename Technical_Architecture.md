# Technical Architecture Documentation — RideGo System

> **Based on actual codebase state** — reflects implemented features, known gaps, and current project structure.

---

## 1. Purpose of Application Layer

The Application Layer orchestrates use cases between Presentation and Domain. It coordinates domain objects, enforces application-level validation, and provides DTOs for data transfer. The codebase uses a lightweight CQRS-like structure with `Features/` folders (Commands/Queries/Handlers), but handlers call Application Services directly (no MediatR).

---

## 2. Project Structure & Layer Mapping

```
RideGo2026/
├── Application/          # Application layer
│   ├── Interfaces/       # Service interfaces (ITripService, IUserService, IFareService, ISimulationService, IReviewService, IAdminService, IEventHandler)
│   ├── Services/         # Service implementations
│   │   ├── TripService.cs
│   │   ├── UserService.cs
│   │   ├── FareService.cs
│   │   ├── MatchingService.cs  (namespace: Domain.Services — should be Application.Services)
│   │   ├── ReviewService.cs     (namespace: Services — should be Application.Services)
│   │   ├── SimulationService.cs (stub)
│   │   └── AdminService.cs (empty)
│   ├── Features/         # Use-case vertical slices
│   │   ├── Trips/        # RequestTrip, AssignDriver, CancelTrip, CompleteTrip, GetTripStatus
│   │   ├── Drivers/      # AcceptTrip, RegisterDriver, UpdateDriverStatus
│   │   └── Passengers/   # RegisterPassenger
│   ├── DTOs/             # Data transfer objects
│   ├── Behaviors/        # Pipeline behaviors (Validation, ExceptionHandling)
│   ├── Mappings/         # AutoMapper profile (MappingProfile.cs)
│   └── Services folder also contains PassengerService (namespace Services)
├── Domain/               # Domain layer (pure)
│   ├── StateMachines/    # TripStateMachine.cs, DriverStateMachine.cs
│   ├── Repositories/     # Repository interfaces (IRepository<T>, ITripRepository, IDriverRepository, etc.)
│   ├── Trips/            # Trip aggregate + Events/
│   ├── Users/            # User, Passenger, Driver + Drivers/Events/, Passengers/
│   ├── Vehicles/         # Vehicle (abstract), Car, Motorbike
│   ├── ValueObjects/     # Money, Coordinate, Address, Location, Route, Fare
│   ├── FareRules/        # FareRule entity
│   ├── Reviews/          # Review entity + Events/
│   └── SharedKernel/     # Entity base, ValueObject base, DomainEvent base
├── Infrastructure/       # Infrastructure layer
│   ├── Repositories/     # Concrete repository implementations
│   │   ├── JsonRepository<T> (generic base)
│   │   ├── TripRepository.cs
│   │   ├── DriverRepository.cs
│   │   ├── PassengerRepository.cs
│   │   ├── UserRepository.cs
│   │   ├── VehicleRepository.cs
│   │   ├── FareRuleRepository.cs
│   │   ├── ReviewRepository.cs
│   │   └── FileStorage.cs
│   ├── ExternalServices/ # API clients
│   │   └── MapService.cs (IMapService implementation)
│   └── Interfaces/       # Infrastructure interfaces
│       ├── IMapService.cs
│       ├── IFileStorageService.cs
│       └── IFareRuleRepository.cs (duplicate of Domain interface? — in Domain.Repositories namespace)
├── Presentation/         # WinForms UI
│   ├── Program.cs        # DI composition root
│   ├── Shells/           # MainShell, PassengerShell, DriverShell, AdminShell
│   ├── Screens/          # Auth, Passenger, Driver, Admin screens
│   ├── Components/       # Reusable UserControls (MapControl, FarePanel, DriverCardControl, etc.)
│   ├── ViewModels/       # UI state (PassengerViewModel, DriverViewModel, TripViewModel, AdminViewModel)
│   └── Helpers/          # AlertHelper, DataMapper, EventHelper, MapHelper, UIHelper
├── Common/               # Cross-cutting utilities
│   ├── Constants/        # SimulationConstants, FareConstants
│   ├── Utilities/        # PasswordHasher
│   └── Extensions/       # StringExtensions, DecimalExtensions
└── packages/             # NuGet packages (offline)
```

---

## 3. Use Cases (Implemented + Missing)

| ID | Use Case | Actor | Status | Implementation Notes |
|---|---|---|---|---|
| UC1 | Đăng nhập | User | ✅ | UserService.Login() |
| UC2 | Đăng ký tài xế | Driver | ✅ | UserService.RegisterDriver() |
| UC3 | Đăng ký hành khách | Passenger | ✅ | UserService.RegisterPassenger() |
| UC4 | Đặt chuyến | Passenger | ✅ | Sync RequestTrip() Không dùng routing API |
| UC5 | Ghép tài xế | System | ⚠️ | Chỉ kiểm tra `Status == Available`, chưa lọc VehicleType/Khoảng cách |
| UC6 | Đến điểm đón | Driver | ✅ | TripService.ArriveAtPickup() |
| UC7 | Bắt đầu chuyến | Driver | ✅ | TripService.StartTrip() |
| UC8 | Hoàn thành chuyến | Driver | ✅ | TripService.CompleteTrip() |
| UC9 | Đánh giá | Passenger | ✅ | ReviewService.AddReviewAsync() |
| UC10 | Hủy chuyến | Passenger/Driver | ✅ | TripService.CancelTrip() |
| UC11 | Lịch sử chuyến | Passenger/Driver | ✅ | TripRepository.GetByPassengerId/DriverId |
| UC12 | Thông tin tài xế matched | Passenger | ✅ | Trip DTO contains DriverId |
| UC13 | Trạng thái làm việc | Driver | ✅ | UserService.UpdateDriverStatus() |
| UC14 | Nhận thông tin chuyến | Driver | ✅ | TripService.GetTripAsync() |
| UC15 | Chấp nhận/Từ chối | Driver | ⚠️ | AcceptTripHandler exists, but UI flow incomplete |
| UC16 | Admin theo dõi real-time | Admin | ⚠️ | AdminShell + DataGridView, no live map |
| UC17 | Cấu hình FareRule | Admin | ❌ | AdminService empty, UI missing |
| UC18 | Driver Radar | Passenger | ❌ | No proximity search implemented |
| UC19 | Thu nhập tài xế | Driver | ✅ | Driver entity has Income + Wallet |
| UC20 | Báo cáo thống kê | Admin | ❌ | No reporting service |
| UC21 | Dẫn đường | Driver | ⚠️ | MapControl displays route if polyline provided; routing not integrated |
| UC22 | Sửi thông tin cá nhân | User | ⚠️ | UserService has methods; UI partial |

---

## 4. Application Services (Responsibilities)

**TripService** (`Application.Services.TripService`):
- `CreateTripAsync()` — internal async trip creation (uses Haversine distance)
- `RequestTrip(Guid, Location, Location, VehicleType)` — sync version (BookTripForm uses this via `ITripService`)
- `MatchDriverAsync(Guid, Guid)` — assigns driver, updates states
- `ArriveAtPickup(Guid)` — mark arrived
- `StartTrip(Guid)` — mark started
- `CompleteTrip(Guid, decimal)` — complete + payment
- `CancelTrip(Guid, string)` — cancel
- `CanTripBeCancelled(Guid)` — check cancellation eligibility
- `TripStatusChanged` — event `Action<TripDto>`

**UserService** (`Application.Services.UserService`):
- `RegisterPassenger()`, `RegisterDriver()` with Vehicle
- `Login()` — phone + password verification
- `GetUserProfile()`, `UpdateProfileName()`, `ChangePhone()`, `ChangePassword()`
- `UpdateDriverLocation()`, `TopUpDriverWallet()`, `UpdateDriverLicense()`, `UpdateDriverVehicle()`
- `UpdateDriverStatus()`, `ForceRecoverDriverStatus()`

**FareService** (`Application.Services.FareService`):
- Constructor injects `IFareRuleRepository` and `IRouteService` (IRouteService missing → **compile error**)
- `CalculateFare(VehicleType, double distanceKm)` — uses FareRule

**MatchingService** (`Domain.Services.MatchingService` — namespace mismatch):
- `MatchDriverToTripAsync(Guid tripId, Guid driverId)` — basic match (status only)

**ReviewService** (`Services.ReviewService` — namespace mismatch):
- `AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)` — creates review + updates driver rating

**SimulationService** (`Application.Services.SimulationService`):
- Stub implementation; no background tick; methods are no-ops.

**AdminService** (`Application.Services.AdminService`):
- Empty stub.

---

## 5. Domain Model

### Entities Base
`Entity` (abstract) in `Domain.SharedKernel`:
- `Guid Id`
- `IReadOnlyCollection<DomainEvent> DomainEvents`
- Methods: `AddEvent()`, `ClearEvents()`, value-based equality

### User Hierarchy
`User` (abstract) → `Passenger` (sealed), `Driver`, `Admin`.

Key properties:
- `User`: Name, Phone, Password (hashed), IsActive
- `Driver`: Status (DriverStatus), Position (Location), VehicleId, Wallet (Money), Income (Money), TotalTrips, AverageRating, RatingSum, TotalReviews, LicenseNumber
- `Passenger`: TotalTrips
- `Admin`: no additional properties

### Vehicle Hierarchy
`Vehicle` (abstract) → `Car`, `Motorbike`.

Properties: PlateNumber, Brand, Model, Color, Capacity, Type (VehicleType)
Abstract methods: `GetAvgSpeed()`, `GetMaxPickupDistance()`
- `Car`: AvgSpeed = 60km/h, MaxPickupDistance = 7km
- `Motorbike`: AvgSpeed = 40km/h, MaxPickupDistance = 5km

**Note:** Vehicle is an **Entity** (inherits `Entity`), not a Value Object. The documentation previously confusingly referred to VehicleInfo as VO; code shows Vehicle as Entity in `Domain/Vehicles`.

### Trip Aggregate
`Trip` (`Domain.Trips.Trip`):
- Properties: Id, Status (TripStatus), PassengerId, DriverId?, VehicleType, Route (Route), Fare (Fare), IsPaid, RequestAt
- State transitions call `TripStateMachine.CanTransition()` and emit domain events
- `SetSearching()`, `MatchDriver(Guid driverId)`, `MarkAsArrived()`, `StartTrip()`, `CompleteTrip(Fare fare)`, `Cancel(string reason)`, `MarkTimeout()`

### FareRule Entity
`FareRule` (`Domain.FareRules.FareRule`):
- VehicleType, BaseFare (Money), PricePerKm (Money), CommissionRate (double)
- `CalculateFare(double distanceKm)` → `Fare` VO

### Review Entity
`Review` (`Domain.Reviews.Review`):
- DriverId, PassengerId, TripId, Rating (1–5), Comment, CreatedAt

### Value Objects
**Money**: decimal Amount (2dp), string Currency (default "VND") — immutable, operators
**Coordinate**: double Latitude, Longitude
**Address**: structured fields (Name, Street, District, City, Country, HouseNumber, Osm_Value, Locality)
**Location**: Coordinate + Address — composite VO
**Route**: Pickup (Location), Destination (Location), Distance (km), Duration (TimeSpan), Polyline (string)
**Fare**: TotalAmount (Money), Commission (Money), DriverIncome (computed as Total − Commission)

### Enums
`TripStatus`: Requested (0), Searching (1), Matched (2), Arrived (3), Started (4), Completed (5), Cancelled (6), Timeout (7)
`DriverStatus`: Offline (0), Available (1), OnTrip (2)
`VehicleType`: Unknown (0), Motorbike (1), Car (2)

---

## 6. Repository & Persistence

**Repository Interfaces** (Domain):
- `IRepository<T>` — Add, Update, Delete, GetAll, GetById, SaveChangesAsync, InitializeAsync
- `IReadRepository<T>` — GetAllAsync, GetByIdAsync
- Specific: `IUserRepository` (plus GetByPhoneAsync, GetDriversAsync, GetAvailableDriversAsync), `IDriverRepository`, `IPassengerRepository`, `ITripRepository` (plus GetByDriverIdAsync, GetByPassengerIdAsync, GetPendingTripsAsync), `IVehicleRepository`, `IReviewRepository`, `IFareRuleRepository`

**Infrastructure Implementations** (`Infrastructure.Repositories`):
- `JsonRepository<T>` — generic base using `FileStorage.LoadAsync<T>` / `SaveAsync`
- Concrete repos inherit `JsonRepository<T>`: `UserRepository`, `DriverRepository`, `PassengerRepository`, `TripRepository`, `VehicleRepository`, `ReviewRepository`, `FareRuleRepository`
- `FileStorage` — static class handling file I/O with `ReaderWriterLockSlim` for thread safety
- Data stored in `Data/*.json` files under application base directory

**Concurrency:** Optimistic concurrency via `Version` field in aggregates (Trip, Driver), checked in `UpdateWhereVersionMatches`. However, actual usage in repositories varies; `TripRepository.Update` uses version increment; `JsonRepository.GetById` returns entity without version check; potential race conditions exist.

---

## 7. External Services & Integration

**MapService** (`Infrastructure.ExternalServices.MapService`):
- Implements `IMapService` interface (`Infrastructure.Interfaces`)
- Methods:
  - `GetDistanceAsync(Location, Location)` → `(double distance, double duration)`
  - `GetRouteAsync(Location start, Location end)` → `Route`
  - `SearchLocation(string query)` → `List<Location>`
  - `ReverseGeocodeAsync(double lat, double lng)` → `Location`
- Integrations: Photon Geocoding API + OSRM Routing API (HTTP endpoints)
- Custom HTTP client with timeout (10s), User-Agent header, fallback logic (Photon → Nominatim not implemented here)

**GMap.NET** (Presentation):
- `MapControl` UserControl wraps `GMapControl`; provider = GoogleMap; `AccessMode.ServerAndCache`
- Markers overlay (pickup, destination, driver)
- Route rendering using GMapRoute (requires polyline decoding — not implemented)

---

## 8. Known Gaps & Missing Implementations

| Missing Item | Where Referenced | Impact |
|---|---|---|
| `IRouteService` / `RouteService` | `Program.cs` (DI), `FareService` ctor, `BookTripForm` ctor, `TripViewModel.LoadRouteInfoAsync`, `PassengerViewModel` ctor | **Compile error** — FareService cannot be constructed |
| `TripTimeoutWorker` | `Program.cs` lines 40, 99 | **Compile error** — missing class |
| `TripMatchingWorker` | `Program.cs` lines 41, 100 | **Compile error** — missing class |
| `IDriverSimulationService` | `SimulationService.cs` line 17, `Program.cs` line 96 | **Compile error** — missing interface |
| `SimpleRouteService` | docs only | Not referenced; could replace missing IRouteService |
| `PaymentService`, `PaymentRepository` | Technical_Architecture.md only | Not in code; payment is handled in `TripService.CompleteTrip()` inline |
| `RatingService` vs `ReviewService` | Interface named `IReviewService`, impl named `ReviewService`; README calls it `RatingService` | Naming inconsistency (acceptable) |
| `BackgroundJobs/` folder | docs reference | Not present; workers missing |
| `Simulation/` folder | docs reference | Not present; simulation stub in Application.Services |
| `Persistence/Repositories/` vs `Repositories/` | docs reference both | Actual: only `Infrastructure/Repositories` exists |
| `Common/Exceptions/` folder | docs reference | Not present; no custom exceptions found |
| `Policies/` (DriverEligibilityPolicy, TripAssignmentPolicy) | docs reference | Not present; matching logic inline in MatchingService |
| `InMemoryDriverCache` | docs reference | Not present |

**LSP/Compiler Errors Present:**
- `Presentation.Program.cs`: Missing using statements for Infrastructure namespaces; JsonStorage not found; missing worker classes; DI extension methods not available.
- `SimulationService.cs`: `IDriverSimulationService` undefined.
- `PassengerShell.cs`: `ITripService` does not define `TripStatusChanged` event (event exists on `TripService` implementation, not on interface `ITripService`) — UI should subscribe to implementation or interface must expose event.
- `DriverViewModel.cs`: Missing using statements for Domain types (`Driver`, `Trip`, `Vehicle`, `Location`).
- `AssignDriverHandler.cs`: References `Domain.Interfaces.IDriverRepository` (non-existent) and `ITripService.TryAssignDriver` (method not in interface).

---

## 9. Dependency Mapping (Actual References)

From `.csproj` files:

```
Presentation -> Application, Common, Domain, Infrastructure
Application -> Common, Domain
Infrastructure -> Application, Common, Domain
Domain -> Common
Common -> (none)
```

**Violations of Clean Architecture:**
- Presentation depends on Infrastructure (should only depend on Application)
- Presentation depends on Domain (should only depend on Application)
- Infrastructure depends on Domain (should depend on Domain interfaces only, not concrete domain layer project reference) — acceptable if Domain defines interfaces; but reference direction still inward to Domain.

**Recommended fix:** Move `IRepository<T>` and related interfaces to a separate `Domain.Contracts` project or `Application.Interfaces`, then have Infrastructure reference that. Presentation should depend only on Application (and possibly shared DTOs).

---

## 10. DI Registration (Presentation/Program.cs)

Current registrations (lines 63–101):

```csharp
// Storage (Infrastructure)
services.AddSingleton(new JsonStorage<User>("data/users.json"));
services.AddSingleton(new JsonStorage<Trip>("data/trips.json"));
services.AddSingleton(new JsonStorage<Driver>("data/drivers.json"));
services.AddSingleton(new JsonStorage<Passenger>("data/passengers.json"));

// Repositories (Infrastructure)
services.AddSingleton<IUserRepository, UserRepository>();
services.AddSingleton<ITripRepository, TripRepository>();
services.AddSingleton<IDriverRepository, DriverRepository>();
services.AddSingleton<IPassengerRepository, PassengerRepository>();

// HttpClient for MapService
services.AddHttpClient<IMapService, MapService>(...);

// Application Services
services.AddSingleton<IUserService, UserService>();
services.AddSingleton<ITripService, TripService>();
services.AddSingleton<IRouteService, RouteService>(); // ❌ RouteService missing
services.AddSingleton<IFareService, FareService>();
services.AddSingleton<ISimulationService, SimulationService>();
services.AddSingleton<IDriverSimulationService, DriverSimulationService>(); // ❌ interface missing

// Background workers (❌ classes missing)
services.AddSingleton<TripTimeoutWorker>();
services.AddSingleton<TripMatchingWorker>();
```

---

## 11. State Machines (Detailed)

**TripStateMachine** (`Domain.StateMachines.TripStateMachine`):
- `CanTransition(TripStatus from, TripStatus to)` — dictionary lookup
- `CanBeCancelled(TripStatus status)` — returns true for Requested, Searching, Matched, Arrived (before Started)

Valid transition graph:
```
Requested → Searching
Searching → Matched, Cancelled, Timeout
Matched → Arrived, Searching, Cancelled
Arrived → Started, Cancelled
Started → Completed, Cancelled
Completed → (none)
Cancelled → (none)
Timeout → (none)
```

**DriverStateMachine** (`Domain.StateMachines.DriverStateMachine` — static class):
```
Offline → Available
Available → OnTrip, Offline
OnTrip → Available
```

---

## 12. Domain Events

All inherit `DomainEvent` (base with `Id`, `OccurredOn`).

**Trip Events** (`Domain.Trips.Events`):
- `TripRequestedEvent` — ctor
- `TripSearchingEvent` — SetSearching()
- `TripMatchedEvent` — MatchDriver()
- `TripArrivedEvent` — MarkAsArrived()
- `TripStartedEvent` — StartTrip()
- `TripCompletedEvent` — CompleteTrip()
- `TripPaidEvent` — ConfirmPayment() (not called in current code)
- `TripCancelledEvent` — Cancel()
- `TripTimeoutEvent` — MarkTimeout()

**Driver Events** (`Domain.Users.Drivers.Events`):
- `DriverStatusChangedEvent` — status change
- `DriverLocationUpdatedEvent` — position update

**Review Event** (`Domain.Reviews.Events`):
- `ReviewCreatedEvent` — review submission

---

## 13. UI Threading & Real-time Updates

- `TripService` exposes `public event Action<TripDto> TripStatusChanged`
- Presentation forms subscribe to this event to refresh UI without polling
- Example: `PassengerShell`, `PassengerViewModel`, `DriverViewModel`, `TripTrackingForm`
- **Issue:** `ITripService` interface does NOT declare this event, so Presentation cannot subscribe via interface — violates Liskov; UI must cast to concrete `TripService`.

---

## 14. Testing Strategy (Current State)

No tests found in repository. Suggested:
- Unit tests for Domain entities (state transitions, invariants)
- Unit tests for Application Services using mocked repositories
- Integration tests for JSON repository + file I/O

---

*Document version: 1.1 — Updated 2026-04-24 after full codebase audit. Highlights: removed outdated structures, listed missing implementations, noted namespacing inconsistencies, recorded actual references vs docs.*
