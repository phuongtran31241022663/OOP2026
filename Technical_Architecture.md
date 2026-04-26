# Technical Architecture Documentation — RideGo System

> **Based on actual codebase state** — reflects implemented features, known gaps, and current project structure.

---

## 1. Purpose of Application Layer

The Application Layer orchestrates use cases between Presentation and Domain. It coordinates domain objects, enforces application-level validation, and exposes service interfaces for the Presentation layer. Services are organized under `Application/Services` and `Application/Interfaces`.

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
│   │   ├── MatchingService.cs  (namespace: Application.Services)
│   │   ├── ReviewService.cs     (namespace: Application.Services)
│   │   ├── SimulationService.cs (stub)
│   │   ├── AdminService.cs (fully implemented)
│   │   ├── DriverService.cs
│   │   ├── PassengerService.cs
│   │   └── MapService.cs (Infrastructure wrapper)
│   ├── Features/         # Reserved for future vertical slices (currently empty)
│   ├── DTOs/             # Reserved for future DTOs (currently empty)
│   ├── Behaviors/        # Reserved for cross-cutting concerns (currently empty)
│   └── Mappings/         # Reserved for object mapping utilities (currently empty)
├── Domain/               # Domain layer (pure)
│   ├── StateMachines/    # DriverStateMachine.cs (static class), ITripState + state implementations in Domain/States/
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
│   ├── Program.cs        # Manual composition root
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
| UC5 | Ghép tài xế | System | ⚠️ | Kiểm tra `Status == Available` và `VehicleType` match; chưa lọc đầy đủ: cần thêm lọc thô theo địa chỉ hành chính (phường→quận→thành phố), kiểm tra khoảng cách < MaxPickupDistance, và kiểm tra số dư Wallet |
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
| UC17 | Cấu hình FareRule | Admin | ✅ | AdminService.CreateFareRuleAsync, AdminService.UpdateFareRuleAsync |
| UC18 | Driver Radar | Passenger | ❌ | No proximity search implemented |
| UC19 | Thu nhập tài xế | Driver | ✅ | Driver entity has Income + Wallet |
| UC20 | Báo cáo thống kê | Admin | ✅ | AdminService.GetTotalGMVAsync, GetTotalNTRAsync, GetCompletionRateAsync, GetAverageSatisfactionAsync |
| UC21 | Dẫn đường | Driver | ⚠️ | MapControl displays route if polyline provided; routing not integrated |
| UC22 | Sửa thông tin cá nhân | User | ⚠️ | UserService has methods; UI partial |

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
- `CanTripBeCancelledAsync(Guid)` — check cancellation eligibility
- `TripStatusChanged` — event `EventHandler<TripStatusChangedEventArgs>` (declared on `ITripService` interface)

**UserService** (`Application.Services.UserService`):
- `RegisterPassenger()`, `RegisterDriver()` with Vehicle
- `Login()` — phone + password verification
- `GetUserProfile()`, `UpdateProfileName()`, `ChangePhone()`, `ChangePassword()`
- `UpdateDriverLocation()`, `TopUpDriverWallet()`, `UpdateDriverLicense()`, `UpdateDriverVehicle()`
- `UpdateDriverStatus()`, `ForceRecoverDriverStatus()`

**FareService** (`Application.Services.FareService`):
- Constructor injects `IFareRuleRepository`
- `CalculateFare(VehicleType, double distanceKm)` — uses FareRule

**MatchingService** (`Application.Services.MatchingService`):
- `MatchDriverToTripAsync(Guid tripId, Guid driverId)` — match with status + VehicleType check

**ReviewService** (`Application.Services.ReviewService`):
- `AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)` — creates review + updates driver rating

**SimulationService** (`Application.Services.SimulationService`):
- Stub implementation; no background tick; methods are no-ops.

**AdminService** (`Application.Services.AdminService`):
- Fully implemented: user/trip/fare rule management, statistics (GMV, NTR, completion rate, satisfaction).

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
- State transitions validated by `ITripState` implementations before delegating to `Trip.TransitionTo(...)`; emit domain events
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

**Concurrency:** No explicit optimistic concurrency or `Version` field in aggregates. `JsonRepository<T>` uses a static `Mutex` per type (`"Global\\RideGo_JsonRepo_" + typeof(T).Name`) to serialize file access, preventing concurrent writes but not read-modify-write races.

---

## 7. External Services & Integration

**MapService** (`Infrastructure.ExternalServices.MapService`):
- Implements `IMapService` interface (`Application.Interfaces`)
- Methods:
  - `GetDistanceAsync(Location, Location)` → `(double distance, double duration)`
  - `GetRouteAsync(Location start, Location end)` → `Route`
  - `SearchLocation(string query)` → `List<Location>`
  - `ReverseGeocodeAsync(double lat, double lng)` → `Location`
- Integrations: Photon Geocoding API + OSRM Routing API (HTTP endpoints)
- Custom HTTP client with timeout (10s), User-Agent header, fallback logic (Photon → Nominatim not implemented here)

**GMap.NET — Phân Tách Theo Kiến Trúc:**

| Thành phần | Layer | Lý do |
|---|---|---|
| `GMapControl` (UI widget) | **Presentation** | Control WinForms — chỉ dùng được trong UI |
| `GMapProviders` (Routing, Geocoding) | **Infrastructure** | Chỉ cần `GMap.NET.Core`, không cần WinForms control; là external service call |

- Presentation cài `GMap.NET.WinForms` — bao gồm cả Core.
- Infrastructure cài `GMap.NET.Core` — không kéo theo WinForms dependency.

**MapControl** (Presentation UserControl):
- Wraps `GMapControl`; provider = `GMapProviders.GoogleMap`; `AccessMode.ServerAndCache`
- Overlays: markers (pickup, destination, driver), route (GMapRoute)
- Route rendering dùng GMapRoute (cần giải mã Encoded Polyline — chưa hoàn chỉnh)

---

## 8. Known Gaps & Missing Implementations

| Missing Item | Where Referenced | Impact |
|---|---|---|
| `IRouteService` / `RouteService` | `Program.cs` (manual), `BookTripForm` ctor, `TripViewModel.LoadRouteInfoAsync`, `PassengerViewModel` ctor | **Compile error** — `RouteService` class referenced but not found in codebase |
| `TripTimeoutWorker` | `Program.cs` | Implemented in `Infrastructure/BackgroundJobs/TripTimeoutWorker.cs` ✅ |
| `TripMatchingWorker` | `Program.cs` | Implemented in `Infrastructure/BackgroundJobs/TripMatchingWorker.cs` ✅ |
| `IDriverSimulationService` | `SimulationService.cs`, `Program.cs` | **Compile error** — interface referenced but not defined |
| `SimpleRouteService` | docs only | Not referenced; could replace missing IRouteService |
| `PaymentService`, `PaymentRepository` | Technical_Architecture.md only | Not in code; payment is handled in `TripService.CompleteTrip()` inline |
| `RatingService` vs `ReviewService` | Interface named `IReviewService`, impl named `ReviewService`; README calls it `RatingService` | Naming inconsistency (acceptable) |
| `BackgroundJobs/` folder | `Infrastructure/BackgroundJobs/` | Present: `TripTimeoutWorker.cs`, `TripMatchingWorker.cs` ✅ |
| `Simulation/` folder | docs reference | Not present; simulation stub in Application.Services |
| `Persistence/Repositories/` vs `Repositories/` | docs reference both | Actual: only `Infrastructure/Repositories` exists |
| `Common/Exceptions/` folder | docs reference | Not present; no custom exceptions found |
| `Policies/` (DriverEligibilityPolicy, TripAssignmentPolicy) | docs reference | Not present; matching logic inline in MatchingService |
| `InMemoryDriverCache` | docs reference | Not present |

**LSP/Compiler Errors Present:**
- `Presentation.Program.cs`: Missing using statements for Infrastructure namespaces; JsonStorage not found; missing worker classes; manual composition required.
- `SimulationService.cs`: `IDriverSimulationService` undefined.
- `PassengerShell.cs`: `ITripService` defines `TripStatusChanged` event (`EventHandler<TripStatusChangedEventArgs>`) ✅.
- `DriverViewModel.cs`: Missing using statements for Domain types (`Driver`, `Trip`, `Vehicle`, `Location`).
- `AssignDriverHandler.cs`: References `Domain.Interfaces.IDriverRepository` (non-existent namespace; should be `Domain.Repositories`) and `ITripService.TryAssignDriver` (method not in interface; use `MatchDriverAsync`).

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

## 10. Manual Service Composition (Presentation/Program.cs)

Dependencies are composed manually using `new` (no DI container):

```csharp
static class Program
{
    [STAThread]
    static void Main()
    {
        // Storage (Infrastructure)
        JsonStorage<User> userStorage = new JsonStorage<User>("data/users.json");
        JsonStorage<Trip> tripStorage = new JsonStorage<Trip>("data/trips.json");
        JsonStorage<Driver> driverStorage = new JsonStorage<Driver>("data/drivers.json");
        JsonStorage<Passenger> passengerStorage = new JsonStorage<Passenger>("data/passengers.json");

        // Repositories (Infrastructure)
        IUserRepository userRepository = new UserRepository(userStorage);
        ITripRepository tripRepository = new TripRepository(tripStorage);
        IDriverRepository driverRepository = new DriverRepository(driverStorage);
        IPassengerRepository passengerRepository = new PassengerRepository(passengerStorage);

        // External services
        IMapService mapService = new MapService(); // uses new HttpClient() internally

        // Application Services
        IUserService userService = new UserService(userRepository, driverRepository, passengerRepository);
        ITripService tripService = new TripService(tripRepository, driverRepository, passengerRepository, mapService);
        IFareService fareService = new FareService(new FareRuleRepository(new JsonStorage<FareRule>("data/farerules.json")));
        IMatchingService matchingService = new MatchingService(tripRepository, driverRepository);
        IReviewService reviewService = new ReviewService(new ReviewRepository(new JsonStorage<Review>("data/reviews.json")), driverRepository);
        IAdminService adminService = new AdminService(userRepository, tripRepository, new FareRuleRepository(new JsonStorage<FareRule>("data/farerules.json")));

        // Background workers
        TripTimeoutWorker timeoutWorker = new TripTimeoutWorker(tripRepository);
        TripMatchingWorker matchingWorker = new TripMatchingWorker(tripRepository, driverRepository, matchingService);

        // Launch main shell with composed services
        Application.Run(new MainShell(userService, tripService, fareService, matchingService, reviewService, adminService));
    }
}
```

---

## 11. State Machines (Detailed)

**Trip State Pattern** (`Domain.States.ITripState` + implementations):
- Each state class (`RequestedState`, `SearchingState`, `MatchedState`, ...) validates transitions before delegating to `Trip.TransitionTo(...)`.
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

**Trip Events** (`Domain.Events`):
- `TripRequestedEvent` — ctor
- `TripSearchingEvent` — SetSearching()
- `TripMatchedEvent` — MatchDriver()
- `TripArrivedEvent` — MarkAsArrived()
- `TripStartedEvent` — StartTrip()
- `TripCompletedEvent` — CompleteTrip()
- `TripPaidEvent` — ConfirmPayment() (called in TripService.CompleteTripAsync)
- `TripCancelledEvent` — Cancel()
- `TripTimeoutEvent` — MarkTimeout()

**Driver Events** (`Domain.Events`):
- `DriverStatusChangedEvent` — status change
- `DriverLocationUpdatedEvent` — position update

**Review Event** (`Domain.Events`):
- `ReviewCreatedEvent` — review submission

---

## 13. UI Threading & Real-time Updates

- `ITripService` exposes `event EventHandler<TripStatusChangedEventArgs> TripStatusChanged`
- Presentation forms subscribe to this event to refresh UI without polling
- Example: `PassengerShell`, `PassengerViewModel`, `DriverViewModel`, `TripTrackingForm`
- **Note:** Event is declared on the interface, so UI can subscribe via `ITripService` without casting to concrete `TripService`.

---

## 14. Testing Strategy (Current State)

No tests found in repository. Suggested:
- Unit tests for Domain entities (state transitions, invariants)
- Unit tests for Application Services using mocked repositories
- Integration tests for JSON repository + file I/O

---

*Document version: 1.1 — Updated 2026-04-24 after full codebase audit. Highlights: removed outdated structures, listed missing implementations, noted namespacing inconsistencies, recorded actual references vs docs.*
