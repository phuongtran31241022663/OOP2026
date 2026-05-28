# Memory - RideGo Context

## Identity
- **Name**: RideGo
- **Type**: Ride-sharing (WinForms .NET 4.8)
- **Architecture**: Clean Architecture (Domain -> Application -> Infrastructure -> Presentation)

## Core Layers

### Domain
- **SharedKernel**: Entity, ValueObject base classes
- **Entities**: 
  - User (abstract) -> Admin, Driver, Passenger
  - Vehicle (abstract) -> Car, Motorbike
  - Trip (aggregate root), Policy, Review
- **ValueObjects**: Coordinate, Address, Location, Route, Fare
- **States**: 
  - Trip: Pending, Searching, Matched, Arrived, Started, Completed, Cancelled, TimeOut
  - Driver: Offline, Available, OnTrip
- **Events**: TripCompletedEvent, TripStatusChangedEventArgs, DriverStatusChangedEventArgs
- **Services/Matching**: Grid-based Spatial Indexing (InMemoryDriverGrid, MatchSvc)
- **Repositories**: IRepository<T>, ITripRepository, IUserRepository, IVehicleRepository, IPolicyRepository, IReviewRepository

### Application
- **Services**: UserService, TripService, DriverService, PassengerService, AdminService, MatchingService, FareService, MapService, SimulationService
- **Handlers**: DriverWalletHandler, PassengerStatsHandler (Domain Event handlers)
- **Interfaces**: Service contracts (IUserService, ITripService, etc.)
- **Root**: AppServiceBundle (Composition Root)

### Infrastructure
- **Repositories**: JsonRepository<T> (base), TripRepository, UserRepository, VehicleRepository, PolicyRepository, ReviewRepository
- **Data**: DataSeeder, SafeSerializationBinder
- **Storage**: JSON files (users.json, trips.json, vehicles.json, policy.json, reviews.json, route_cache.json)

### Presentation
- **Forms**: FrmMain (Shell), FrmMultiRole (Multi-role view)
- **UserControls**: UcAuth, UcPassenger, UcDriver, UcAdmin (Main role views)
- **Components**: UcMap, UcLocationPicker, UcVehicleFareSelector, UcTripCard, UcDriverCard, UcProfile, UcReview, UcTripDetail, UcTripStatus (Reusable UI components)
- **Presenters**: AuthPresenter, PassengerPresenter, DriverPresenter, AdminPresenter (MVP pattern)
- **Contracts**: IAuthView, IPassengerView, IDriverView, IAdminView (View interfaces)
- **Constants**: UiConstants

## Domain Model

### Aggregates
- **Trip** (Root): Manages trip lifecycle, coordinates with Driver and Passenger
- **Vehicle** (Root): Owned by Driver, determines fare strategy

### Entities
- **User hierarchy**: Base User -> Admin, Driver, Passenger
- **Vehicle hierarchy**: Base Vehicle -> Car, Motorbike
- **Policy**: System-wide pricing and commission rules
- **Review**: Trip feedback from Passenger to Driver

### Value Objects
- **Coordinate**: Lat/Lng pair
- **Address**: Street address with display name
- **Location**: Coordinate + Address
- **Route**: Origin, Destination, Distance, Duration
- **Fare**: Base, Distance, Duration, Total, Commission

### States
- **Trip States**: Pending -> Searching -> Matched -> Arrived -> Started -> Completed (or Cancelled/TimeOut)
- **Driver States**: Offline <-> Available -> OnTrip -> Available

## Persistence
- **Format**: JSON (System.Text.Json)
- **Strategy**: Eager load all data, `JsonDerivedType` for polymorphic serialization
- **Serialization**: `[JsonConstructor]` and private setters for reconstitution
- **Polymorphism**: Usr (Drv, Psg, Adm), Veh (Car, Motorbike)

## Business Rules
- **Matching**: Driver must be Available, VehicleType match, Wallet >= Commission
- **Payment**: CompleteTrip and ConfirmPayment are separate steps
- **Cancellation**: Allowed before Started state
- **Settlement**: If PayCommission fails, throw InvalidOperationException
- **Active Trip**: Status in {Pending, Searching, Matched, Arrived, Started}

## Design Patterns
- **State Pattern**: Trip and Driver lifecycle management
- **Grid-based Spatial Indexing**: Matching logic
- **Domain Events**: Cross-aggregate coordination (TripCompleted -> Wallet/Stats updates)
- **Observer Pattern**: C# events for status changes (TripStatusChanged, DriverStatusChanged)
- **Repository Pattern**: Data access abstraction
- **MVP Pattern**: Presentation layer (Presenter-View-Model)
- **Composition Root**: AppServiceBundle for dependency injection

## UI Architecture
- **Shell**: FrmMain hosts all UserControls
- **Role Views**: UcAuth, UcPassenger, UcDriver, UcAdmin
- **Components**: Reusable controls (Map, LocationPicker, TripCard, etc.)
- **Events**: Services raise events, Presenters subscribe, Views update UI
- **Threading**: Control.Invoke for cross-thread UI updates