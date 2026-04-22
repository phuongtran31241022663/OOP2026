# Application Layer Technical Documentation

## 1. Purpose of Application Layer

The Application Layer acts as an orchestration layer between the Presentation Layer and the Domain Layer. It handles use cases, business workflows, and cross-cutting concerns like validation, logging, and authorization. It uses CQRS pattern with MediatR-like structure (Commands/Queries/Handlers) to process requests, coordinate domain objects, and manage DTOs for data transfer. This layer ensures domain invariants are maintained while providing application-level services and behaviors.

## 3. Use Cases Overview (Command / Query breakdown)

- **Commands** (State-changing operations):
  - RequestTrip, AssignDriver, StartTrip, CompleteTrip, CancelTrip (Trip management)
  - RegisterPassenger, UpdateProfile (Passenger management)
  - AcceptTrip, UpdateDriverStatus (Driver management)
  - ProcessPayment (Payment processing)
  - RateDriver (Rating submission)
  - DeactivateUser, CreateFareRule, UpdateFareRule (Admin actions)
- **Queries** (Read operations):
  - GetTripStatus, GetTripById, GetActiveTrips, GetTripHistory (Trip queries)
  - GetPassengerHistory (Passenger data)
  - GetDriverEarnings, GetNearbyDrivers (Driver data)
  - GetPaymentHistory (Payment history)
  - GetUsers, GetFareRules (Admin queries)

## 4. Application Services (Responsibilities + Methods)

Application Services are defined as interfaces in Application.Interfaces, with implementations in Application.Services (e.g., TripService in Implementations/). They orchestrate domain logic:

- **ITripService**: Manages trip lifecycle. Methods: RequestTrip(), TryAssignDriver(), StartTrip(), CompleteTrip(), CancelTrip(), GetTrip(), etc. Events: TripStatusChanged.
- **IUserService**: Handles user operations. Methods: RegisterPassenger(), Login(), GetUser().
- **IDriverMatchingService**: Coordinates driver matching. Methods: FindNearbyDrivers().
- **IPaymentService**: Processes payments. Methods: ProcessPayment().
- **IRatingService**: Manages ratings. Methods: CreateRating().
- **IFareRuleService**: Calculates fares. Methods: CalculateFare().
- **IAdminService**: Admin functions. Methods: GetAllUsers(), DeactivateUser(), CreateFareRule(), UpdateFareRule(), GetFareRules().
- **INotificationService**: Sends notifications. Methods: NotifyDriver(), NotifyPassenger().
- **IEventDispatcher**: Dispatches domain events. Methods: Dispatch().
- **IAuthorizationService**: Handles RBAC for admin operations. Methods: IsAdmin(), HasPermission().
- **Other interfaces**: IRouteService, ILocationService, ISimulationService.

## 5. Commands / Queries / Handlers

- **Commands**: Simple DTOs with input data (e.g., RequestTripCommand with PassengerId, Pickup, Destination). No logic, just data carriers.
- **Queries**: DTOs for read requests (e.g., GetTripStatusQuery with TripId).
- **Handlers**: Process commands/queries, orchestrate domain calls. Examples:
  - RequestTripHandler: Validates command, calls TripService.RequestTrip(), returns Trip.
  - AssignDriverHandler: Calls TripService.TryAssignDriver() with concurrency handling.
  - GetTripStatusHandler: Retrieves trip via TripService.GetTrip(), maps to DTO.
  - Handlers use dependency injection for services (e.g., ITripService).

## 6. DTOs / Response Models

- **DTOs**: Transfer data between layers without exposing domain objects.
  - TripDto: Id, PassengerName, DriverName, Pickup, Destination, Status, Fare, CreatedAt.
  - DriverDto: Id, Name, Vehicle, Status, Location.
  - PassengerDto: Id, Name, Phone, TotalTrips.
  - FareDto: BaseFare, PricePerKm, CommissionRate.
- **Response Models**: Specialized for queries (e.g., TripReport with aggregated data).

## 7. Validators / Business Rule Enforcement (Application-level)

- **Validators**: Static classes enforcing input validation before domain logic.
  - RequestTripValidator: Checks PassengerId, Pickup/Destination presence, uniqueness.
  - AssignDriverValidator: Validates TripId, DriverId.
  - RegisterPassengerValidator: Ensures required fields for registration.
- **Enforcement**: Application-level rules (e.g., input sanitization) vs. Domain rules (business invariants). Validators return ValidationResult with errors.

## 8. Workflows (End-to-end flow orchestration)

- **Trip Request Workflow**: RequestTripCommand → RequestTripHandler → Validate → TripService.RequestTrip() → Create Trip aggregate → Emit TripRequestedEvent → Map to TripDto → Return TripDto.
- **Driver Assignment Workflow**: AcceptTripCommand → AcceptTripHandler → TripService.TryAssignDriver() → Update Trip/Driver states → Handle concurrency → Emit TripMatchedEvent → Map to TripDto → Return TripDto.
- **Trip Completion Workflow**: CompleteTripCommand → CompleteTripHandler → TripService.CompleteTrip() → Create Payment (Pending) → Update Driver/Passenger TotalTrips → Emit TripCompletedEvent → Map to TripDto → Return TripDto.
- **Payment Processing Workflow**: ProcessPaymentCommand → ProcessPaymentHandler → IPaymentService.ProcessPayment() → Mark Payment Paid → Driver.PayCommission() → Map to PaymentDto → Return PaymentDto.
- **Rating Workflow**: RateDriverCommand → RateDriverHandler → IRatingService.CreateRating() → Update Driver AverageRating → Map to RatingDto → Return RatingDto.

## 9. Dependency Mapping (Application → Domain interaction)

- Application Handlers inject Domain interfaces (e.g., ITripService, IDriverRepository via DI).
- Handlers orchestrate Domain objects: Call Domain services/policies, modify aggregates, handle events.
- Domain dependencies: Entities (Trip, Driver), Services (FareCalculationService), Policies (ITripAssignmentPolicy, IDriverEligibilityPolicy — interface-based), Repositories (ITripRepository).
- Flow: Handler → Application Service (interface) → Domain Logic → Repository (interface) → Infrastructure Implementation.

## 10. Design Issues / Inconsistencies (if any)

- **Mixed Orchestration**: Some handlers call Domain services directly (e.g., TripService), others use Application services. Inconsistent; should standardize via Application services.
- **Thin Handlers**: Handlers are thin, delegating to services, but some include minimal logic. Consider thicker handlers for complex orchestration.
- **Validation Placement**: Validators are Application-level, but some rules overlap Domain (e.g., Trip state validation). Potential duplication.

# Infrastructure Layer Technical Documentation

## 1. Purpose of Infrastructure Layer

The Infrastructure Layer provides concrete implementations for abstractions defined in Application and Domain layers. It handles external concerns like data persistence, external API integrations, caching, simulation, and background jobs. This layer isolates technical implementations (e.g., JSON storage, HTTP clients) from business logic, ensuring the system remains decoupled from specific technologies.

## 2. Folder / Namespace Structure

```
Infrastructure/
├── BackgroundJobs/       # Background workers (TripTimeoutWorker, TripMatchingWorker)
├── Caching/              # In-memory caching (InMemoryDriverCache)
├── Config/               # Configuration and JSON storage (JsonStorage, ConfigService)
├── DependencyInjection/  # DI service registrations (InfrastructureServiceExtensions)
├── ExternalServices/     # API clients (PhotonGeocodingService, NominatimGeocodingService, SimpleRouteService)
├── Persistence/          # Persistence layer
│   ├── Repositories/     # Repository implementations (TripRepository, etc.)
│   └── JsonFileContext.cs # File-based context
├── Repositories/         # Alternative repository folder (some duplicates)
└── Simulation/           # Simulation services (DriverSimulationService, InterpolationEngine)
```

## 3. Database / Persistence Layer

- **No ORM/Database**: Uses file-based JSON persistence instead of relational databases. Designed for demo/scalability simulation without real DB overhead.
- **JsonStorage<T>**: Generic class for JSON file storage. Key features:
  - ConcurrentDictionary for in-memory access.
  - ReaderWriterLockSlim for thread safety.
  - Atomic writes (temp file → rename).
  - System.Text.Json for serialization.
- **Configuration**: File paths configured via ConfigService. No entity mappings (direct JSON serialization of Domain entities).

## 4. Repository Implementations

- **Interfaces → Implementations Mapping**:
  - ITripRepository (Domain) → TripRepository (Persistence/Repositories): CRUD with version-based concurrency.
  - IDriverRepository (Domain) → DriverRepository: Driver-specific queries.
  - IPassengerRepository (Domain) → PassengerRepository: Passenger data access.
  - IPaymentRepository (Domain) → PaymentRepository: Payment persistence.
  - IRatingRepository (Domain) → RatingRepository: Rating storage.
  - IUserRepository (Domain) → UserRepository: Generic user access.
  - IEventRepository (Domain) → EventRepository: Domain event persistence.
- **Key Features**: All use JsonStorage<T>, support optimistic concurrency via Version field. Thread-safe with locks.

## 5. External Services Integration

- **APIs / HTTP Clients**:
  - PhotonGeocodingService: Uses Photon API (OpenStreetMap) for geocoding. HttpClient with retry logic, rate limiting.
  - NominatimGeocodingService: Fallback geocoding via Nominatim API.
  - GMapService: Integrates GMap.NET for map rendering (no direct API calls).
- **Third-party Services**: SimpleRouteService: Implements IRouteService with Haversine distance calculation (no external API, local math).
- **Network Handling**: HttpClient configured with timeouts, retries. Fallback mechanisms (Photon → Nominatim).

## 6. File / System / Messaging / Cache (if any)

- **File Access**: JsonStorage uses File.ReadAllText/WriteAllText with atomic operations.
- **System**: ReaderWriterLockSlim for concurrency, ConcurrentDictionary for in-memory data.
- **Messaging**: No message queues; domain events handled in-memory via IEventDispatcher.
- **Cache**: InMemoryDriverCache: Caches active drivers by VehicleType, refreshes every 5 minutes from repository.

## 7. Dependency Injection Setup

- **InfrastructureServiceExtensions**: Extension method for IServiceCollection registration.
- **Registrations** (commented in code, assume implemented):
  - Application Services: TripService, AdminService (scoped).
  - Persistence: JsonStorage<> (singleton).
  - External: HttpClient for geocoding services, GMapService (singleton).
  - Caching: InMemoryDriverCache (singleton).
- **Scope**: Services registered as scoped/singleton based on need (e.g., JsonStorage singleton for shared state).

## 8. Data Mapping Strategy (Domain ↔ Persistence models)

- **No Separate Persistence Models**: Direct serialization of Domain entities to JSON. No DTOs or mappers.
- **Mapping Logic**: JsonStorage serializes/deserializes Domain objects directly. Polymorphism handled via [KnownType] or type handling in JSON.
- **Concurrency**: Version field in entities for optimistic locking; checked in Update methods.
- **Strategy**: Embedded in repositories; no explicit mappers (e.g., AutoMapper). Simple, but tightly coupled to Domain structure.

## 9. Cross-layer Interaction Flow (Application → Infrastructure)

- Application Handlers inject Infrastructure services (e.g., ITripService → TripService → TripRepository).
- Flow: Handler → Application Service → Domain Repository Interface → Infrastructure Repository Implementation → JsonStorage → File I/O.
- Domain Events: Emitted by aggregates, dispatched via IEventDispatcher to handlers (e.g., NotificationService).
- External Calls: Application calls (e.g., IRouteService) → Infrastructure implementations (e.g., SimpleRouteService) → Local calc or HTTP.

## 10. Design Issues / Inconsistencies (if any)

- **Critical Issue - Compile Error Risk**: Duplicate repository files in Persistence/Repositories and Repositories folders cause namespace collisions or build errors. Identify the canonical implementations (likely Persistence/Repositories) and remove duplicates from Repositories/.
- **Red Flag - Incomplete DI Setup**: Registrations commented in InfrastructureServiceExtensions; system cannot run via DI if not implemented. Verify actual registrations in code and ensure all services are properly registered.
- **No ORM Abstraction**: JSON storage lacks migration/versioning; not scalable for production DB.
- **Tight Coupling**: Direct serialization of Domain entities limits evolution; changes to entities break persistence.
- **Simulation vs. Real**: External services (e.g., SimpleRouteService) use mocks; no real API integration for production readiness.

## 11. Common Project Documentation

### Overview

The `Common` project serves as a shared utilities library across all layers of the Ride-Hailing System architecture. It contains cross-cutting concerns and reusable components that are not domain-specific but provide foundational support for the entire application.

### Layer Mapping

- **Layer**: Cross-cutting / Shared Infrastructure
- **Purpose**: Provide common utilities, extensions, constants, and base classes used across Domain, Application, Infrastructure, and Presentation layers
- **Dependencies**: None (standalone utility library)

### Project Structure

### Constants/

Shared constants used throughout the application, such as default values, configuration keys, and business rules constants.

### Exceptions/

Base exception classes and custom exceptions for consistent error handling across the system.

### Extensions/

Extension methods that provide additional functionality to .NET types, promoting clean and readable code.

### Helpers/

Utility helper classes for common operations like password hashing, validation helpers, and other miscellaneous utilities.

## Usage Guidelines

- Classes in Common should be stateless and thread-safe
- Avoid domain-specific logic; keep it generic and reusable
- Use extension methods judiciously to avoid cluttering the API
- Exceptions should follow a consistent hierarchy for proper error handling

## Dependencies

This project has no external dependencies and is referenced by all other projects in the solution.

## 3. File-Level (Important `.cs` Files, Categorized)

**Application**

- Service: `Services/TripService.cs`, `Services/PaymentService.cs`, `Services/RatingService.cs`, `Services/RouteService.cs`, `Services/SimulationService.cs`, `Services/AdminService.cs`
- Service (active compiled impl): `Services/Implementations/UserService.cs`
- Interface: `Interfaces/ITripService.cs`, `Interfaces/IUserService.cs`, `Interfaces/IRouteService.cs`, `Interfaces/ISimulationService.cs`, `Interfaces/IDriverMatchingService.cs`
- DTO: `DTOs/TripDto.cs`, `DTOs/DriverDto.cs`, `DTOs/PassengerDto.cs`, `DTOs/UserDto.cs`, `DTOs/FareDto.cs`
- Mapper: `DTOs/DriverMapper.cs`, `DTOs/PassengerMapper.cs`
- Other (use-case handlers/commands): `Features/**`

**Domain**

- Entity: `Entities/Trip.cs`, `Entities/Driver.cs`, `Entities/Passenger.cs`, `Entities/User.cs`, `Entities/Payment.cs`, `Entities/Rating.cs`, `Entities/FareRule.cs`, `Entities/Car.cs`, `Entities/Motorbike.cs`
 - Interface: `Interfaces/ITripRepository.cs`, `Interfaces/IDriverRepository.cs`, `Interfaces/IPassengerRepository.cs`, `Interfaces/IUserRepository.cs`, `Interfaces/IPaymentRepository.cs`, `Interfaces/IRatingRepository.cs`
 - Service: `Services/FareCalculationService.cs`, `Services/DriverTripCompatibilityService.cs`, `Services/PaymentCalculationService.cs`, `Services/RouteValidationService.cs`, `Services/Matching/DriverCandidateSelector.cs`, `Services/Matching/DispatchArbitrator.cs`
 - Policy Interface: `Policies/IDriverEligibilityPolicy.cs`, `Policies/ITripAssignmentPolicy.cs`
 - Policy Implementation: `Policies/DriverEligibilityPolicy.cs`, `Policies/TripAssignmentPolicy.cs`
 - Other: `StateMachines/*.cs`, `ValueObjects/*.cs`, `Events/*.cs` (note: orphaned `Event.cs` removed)

**Infrastructure**

- Repository: `Persistence/Repositories/UserRepository.cs`, `DriverRepository.cs`, `PassengerRepository.cs`, `TripRepository.cs`, `PaymentRepository.cs`, `RatingRepository.cs`, `EventRepository.cs`
- Service/Integration: `ExternalServices/SimpleRouteService.cs`, `PhotonGeocodingService.cs`, `NominatimGeocodingService.cs`, `GMapService.cs`
- Other infra runtime: `Config/JsonStorage.cs`, `Persistence/JsonFileContext.cs`, `Caching/InMemoryDriverCache.cs`, `BackgroundJobs/*.cs`, `Simulation/*.cs`

**Presentation**

- Form: `Screens/Auth/LoginForm.cs`, `Screens/Auth/RegisterForm.cs`, `Screens/Passenger/BookTripForm.cs`, `Screens/Passenger/TripTrackingForm.cs`, `Screens/Passenger/TripHistoryForm.cs`, `Screens/Passenger/RatingForm.cs`, `Screens/Driver/TripNavigationForm.cs`, `Shells/DriverShell.cs`, `Shells/PassengerShell.cs`, `Shells/AdminShell.cs`, `Shells/MainShell.cs`
- UserControl: `Components/MapControl.cs`, `FarePanel.cs`, `DriverCardControl.cs`, `LocationPickerControl.cs`, `TripCard.cs`, `TripStatusPanel.cs`, `LocationCard.cs`
- Other: `Program.cs`, `ViewModels/*.cs`, `Helpers/*.cs`

## 4. Class-Level Main Responsibilities

- `Application.Services.TripService`: orchestrates trip lifecycle with repos + simulation trigger.
- `Application.Services.Implementations.UserService`: authentication/registration/profile actions.
- `Application.Services.PaymentService`: payment creation and payment status processing.
- `Application.Services.RatingService`: rating submission and driver-rating updates.
- `Application.Services.RouteService`: route/distance calculation (in-memory/simple).
- `Application.Services.SimulationService`: background tick loop for trip auto-completion simulation.
- `Infrastructure.Persistence.Repositories.*Repository`: JSON-backed persistence adapters for aggregates.
- `Domain.Entities.Trip`: trip state machine owner and trip invariants.
- `Domain.Entities.Driver`: driver state and wallet/rating behavior.
- `Domain.Services.FareCalculationService`: domain-level fare calculation policy.
- `Presentation.Shells.DriverShell` / `PassengerShell`: UI container + navigation + trip event wiring using DTOs.
- `Presentation.Screens.Passenger.BookTripForm`: booking flow UI using DTOs.
- `Presentation.Screens.Passenger.TripTrackingForm`: active-trip tracking UI using DTOs.
- `Presentation.Screens.Driver.TripNavigationForm`: driver accept/start/complete trip UI using DTOs.
- `Presentation.Components.MapControl`: map markers/route rendering wrapper over GMap.NET.

## 5. Method Summary (Controlled Depth: Important Classes Only)

**Application Services**

`AdminService`

- `GetAllUsers()` — merge passenger+driver collections. Interaction: Domain repos/entities. Type: Read.
- `GetAllTrips()` — return trip list. Interaction: Domain repo/entity. Type: Read.
- `ActivateAccountUser(Guid)` — activate passenger/driver and persist. Interaction: Domain entity + repo. Type: State change + Write.
- `DeActivateAccountUser(Guid, Guid)` — deactivate passenger/driver and persist. Interaction: Domain entity + repo. Type: State change + Write.
- `GetTripReport()` — aggregate trip totals/revenue. Interaction: Domain repo/entity. Type: Read.

`TripService`

- `RequestTrip(...)` — validate passenger, create trip, set searching, store, emit DTO event. Interaction: Domain entities + repos + app event. Type: Write + State change.
- `TryAssignDriver(...)` — assign driver and set driver `OnTrip`. Interaction: Domain entities + repos + simulation service. Type: State change + Write.
- `ArriveAtPickup(Guid)` — mark trip arrived at pickup + persist. Interaction: Domain entity + repo. Type: State change + Write.
- `StartTrip(Guid)` — transition trip started + persist. Interaction: Domain entity + repo. Type: State change + Write.
- `CompleteTrip(...)` — complete trip, recover driver state, persist. Interaction: Domain entities + repos. Type: State change + Write.
- `CancelTrip(...)` — cancel trip, recover driver state, persist. Interaction: Domain entities + repos. Type: State change + Write.
- `GetTripDto(Guid)` / `GetTripsByPassenger(Guid)` / `GetTripsByDriver(Guid)` / `GetPendingTrips()` / `CanTripBeCancelled(Guid)` — query/view mapping returning DTOs. Interaction: Domain repos/entities. Type: Read.

`UserService` (`Services/Implementations/UserService.cs`)

- `RegisterPassenger(...)` / `RegisterDriver(...)` — create user entity and return DTO. Interaction: Domain entities + user repo. Type: Write.
- `Login(...)` — lookup + password verify, return DTO. Interaction: Domain entity + repo. Type: Read.
- `GetUserProfile(Guid)` — user fetch, return DTO. Interaction: repo. Type: Read.
- `UpdateProfileName(...)`, `ChangePhone(...)`, `ChangePassword(...)`, `UpdateUserProfile(...)`, `UpdateDriverLocation(...)`, `TopUpDriverWallet(...)`, `UpdateDriverLicense(...)`, `UpdateDriverVehicle(...)` — mutate entity + update repo. Interaction: Domain entity + repo. Type: State change + Write.
- `UpdateDriverVehicleInfo(...)` — update vehicle info. Interaction: Domain entity + repo. Type: State change + Write.
- `UpdateDriverStatus(...)`, `ForceRecoverDriverStatus(...)` — update driver status. Interaction: Domain entity + repo. Type: State change + Write.

`PaymentService`

- `CreatePayment(Trip)` — create payment from completed trip and persist. Interaction: Domain entity + payment repo. Type: Write.
- `ProcessPayment(Guid)` — mark payment paid + update. Interaction: Domain entity + payment repo. Type: State change + Write.
- `GetPayment(Guid)`, `GetPaymentByTrip(Guid)` — retrieval. Interaction: payment repo. Type: Read.

`RatingService`

- `CreateRating(...)` — validate trip and ownership, create rating, update trip+driver. Interaction: trip/rating/driver repos + domain entities. Type: State change + Write.
- `GetRatingByTrip(Guid)`, `GetRatingsByDriver(Guid)` — retrieval. Interaction: rating repo. Type: Read.
- `SubmitRatingAsync(...)` — wrapper over `CreateRating`. Interaction: trip/rating repos. Type: State change + Write.

`DriverMatchingService`

- `FindBestDriver(Trip)` — select available driver by vehicle type (first match). Interaction: driver repo + domain entities. Type: Read/decision.

`FareRuleService`

- `CalculateFare(Trip)` — compute fare estimate. Interaction: Domain `Trip`. Type: Read/compute.
- `GetFareRule(VehicleType)` — construct fare rule object. Interaction: Domain entity/value object creation. Type: Read/compute.

`RouteService`

- `CalculateDistanceAsync(...)`, `GetRoutePointsAsync(...)`, `GetFullRouteAsync(...)`, `IsNearAsync(...)`, `GetRoutesAsync(...)` — route math and route object generation. Interaction: Domain value objects only. Type: Read/compute.

`SimulationService`

- `SetTripService(...)` — inject dependency. Interaction: Application service. Type: State change.
- `StartSimulation()`, `StopSimulation()` — start/stop background loop. Interaction: runtime task system. Type: State change.
- `Tick()` — advance simulation phases (ToPickup → Arrived → ToDestination → Completed), handle timeouts. Interaction: `_tripService` methods. Type: State change + Write (indirect).
- `StartTripSimulation(Guid)` — register trip in simulation map. Type: State change.
- `IsTripSimulating(Guid)` — lookup in map. Type: Read.
- `SimulateTripProgress(Guid)` — placeholder (not implemented). Type: UNCLEAR.

`NotificationService`

- `NotifyPassenger(...)`, `NotifyDriver(...)`, `NotifyTripUpdate(...)` — raise in-process notification events. Interaction: subscribers/UI. Type: State change (event emission).

`AuthorizationService` (orphan file, not compiled into `Application.csproj`)

- `HasPermission(...)`, `IsInRole(...)` — user-role permission checks. Interaction: user repo/domain entities. Type: Read.

**Repositories**

`UserRepository`

- `Add(User)` — add to in-memory dictionary. Type: Write.
- `GetById(Guid)`, `GetByPhone(string)` — lookup. Type: Read.
- `Update(User)` — reference update placeholder. Type: Write (no-op style).

`DriverRepository`

- `InitializeAsync()`, `SaveChangesAsync()` — load/save JSON storage. Type: Read+Write.
- `GetById(...)`, `GetAll()`, `GetByPhone(...)`, `GetAvailableDrivers()`, `ExistsByPhone(...)` — queries. Type: Read.
- `Add(...)`, `Update(...)`, `Delete(...)` — persistence writes. Type: Write.

`PassengerRepository`

- `InitializeAsync()`, `SaveChangesAsync()` — JSON sync. Type: Read+Write.
- `GetById(...)`, `GetAll()`, `GetByPhone(...)`, `ExistsByPhone(...)` — queries. Type: Read.
- `Add(...)`, `Update(...)`, `Delete(...)` — writes. Type: Write.

`TripRepository`

- `InitializeAsync()`, `SaveChangesAsync()` — JSON sync. Type: Read+Write.
- `GetById(...)`, `GetAll()`, `GetTripsByDriverId(...)`, `GetTripsByPassengerId(...)`, `GetPendingTrips()` — queries. Type: Read.
- `Add(...)`, `Delete(...)` — writes. Type: Write.
- `Update(Trip)` — optimistic-concurrency check + version increment. Type: State change + Write.
- `UpdateWhereVersionMatches(...)` — conditional mutation with version guard. Type: State change + Write.

`PaymentRepository`

- `InitializeAsync()`, `SaveChangesAsync()` — JSON sync. Type: Read+Write.
- `GetById(...)`, `GetByTripId(...)` — queries. Type: Read.
- `Add(...)`, `Update(...)` — writes. Type: Write.

`RatingRepository`

- `InitializeAsync()`, `SaveChangesAsync()` — JSON sync. Type: Read+Write.
- `GetById(...)`, `GetByDriverId(...)`, `GetByTripId(...)` — queries. Type: Read.
- `Add(...)`, `Update(...)` — writes. Type: Write.

`EventRepository`

- `Save(DomainEvent)` — append event to JSON file. Type: Write + External call (filesystem).
- `LoadAll()` / `LoadById(Guid)` — read event log. Type: Read + External call (filesystem).

**Forms/UserControls (Public Methods Only)**

`BookTripForm`

- `OnTripFinished()` — reset booking UI through UI thread invoke. Interaction: UI only. Type: State change.
- `UpdateDriverInfo(DriverDto)` — render driver card/status. Interaction: Application DTO + UI. Type: State change.

`RatingForm`

- `RefreshData()` — reload pending ratings list. Interaction: UI + service flow in `LoadPending` (currently partially TODO). Type: Read (UI refresh).

`TripHistoryForm`

- `RefreshData()` — reload trip history grid. Interaction: UI + trip service (currently TODO data fetch). Type: Read (UI refresh).

`TripTrackingForm`

- `ApplyTripUpdate(TripDto)` — apply trip DTO and rerender. Interaction: Application DTO + UI. Type: State change.
- `OnTripStarted(TripDto)` — apply trip DTO and navigate. Interaction: Application DTO + UI. Type: State change.
- `RefreshData()` — rerender current state. Interaction: UI only. Type: Read/State change.

`DriverTripExecutionForm` (declared in `DriverDashboardForm.cs`)

- `OnNavigatedTo(TripDto)` — initialize screen, load commission, start timer refresh. Interaction: services + UI. Type: State change.
- `OnNavigatingFrom()` — stop timer. Interaction: UI runtime. Type: State change.

`TripNavigationForm`

- `OnTripAccepted(TripDto)` — switch to active trip view. Interaction: UI + shell state. Type: State change.
- `OnTripEnded()` — switch to empty/end view. Interaction: UI + shell state. Type: State change.

`DriverShell`

- `SetCurrentTrip(TripDto)` — set shell trip context. Interaction: Application DTO + UI shell. Type: State change.
- `NavigateTo(string)` — switch displayed child form. Interaction: UI container. Type: State change.
- `OnTripAccepted(TripDto)`, `OnTripEnded()` — state transition + navigation updates. Interaction: UI shell + application DTO. Type: State change.
- `ToggleActive()` — toggles driver status and syncs `_userService.UpdateDriverStatus`. Interaction: Application service + UI. Type: State change + Write.

`PassengerShell`

- `NavigateTo(string)` — switch child screen. Interaction: UI container. Type: State change.
- `OnTripStarted(TripDto)` — set current trip and navigate. Interaction: Application DTO + UI. Type: State change.
- `OnTripFinished()` — close trip flow + optional rating prompt. Interaction: UI + current trip state. Type: State change.

`MapControl`

- `AddPickupMarker`, `SetPickup`, `AddDestinationMarker`, `SetDestination`, `AddDriverMarker`, `UpdateDriverLocation`, `UpdateDriverMarkers`, `DrawRoute(...)`, `SetCamera`, `ClearMarkers`, `ClearRoute` — visual map state operations. Interaction: UI map + domain location/driver models. Type: State change (UI rendering).

`FarePanel`

- `SetFareFromTrip`, `SetFareDetails`, `SetFare`, `ClearFare` — fare presentation and formatting. Interaction: Domain `Trip`/`Money` + UI. Type: Read + State change.

`DriverCardControl`

- `SetDriver(Driver, double)` — bind driver info to card. Interaction: Domain `Driver` + UI. Type: Read + State change.

`LocationPickerControl`

- `SetPickup(object)`, `SetDestination(object)` — set selected locations. Interaction: UI state. Type: State change.

`TripCard`

- `SetTrip(...)` — bind trip summary display. Interaction: UI data binding. Type: State change.

`StatusPanel`

- `AddLog(...)`, `ShowError(...)`, `ShowWarning(...)`, `ShowInfo(...)` — append logs and show messages. Interaction: UI + filesystem process launch (`OpenLogFile` private). Type: State change (+ external call indirectly for error dialogs/files).
