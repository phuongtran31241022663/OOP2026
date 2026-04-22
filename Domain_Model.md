# Domain Layer Technical Documentation

## 1. Purpose of Domain Layer

The Domain Layer encapsulates the core business logic and rules of the Ride-Hailing System. It defines entities, value objects, aggregates, domain services, and state machines that enforce business invariants. This layer is independent of external concerns like UI, persistence, or infrastructure, focusing purely on modeling the problem domain. It implements DDD principles with event-driven architecture for trip lifecycle management, driver matching, and fare calculation.

## 2. Folder / Namespace Structure

```
Domain/
├── Aggregates/          # Aggregate root abstractions (AggregateRoot)
├── Entities/            # Domain entities (User, Driver, Trip, etc.)
├── Enums/               # Enumeration types (TripStatus, DriverStatus, VehicleType)
├── Events/              # Domain events (TripRequestedEvent, etc.)
├── Exceptions/          # Custom domain exceptions
├── Interfaces/          # Domain interfaces (repositories, services)
├── Policies/            # Business policies (TripAssignmentPolicy, etc.)
├── Services/            # Domain services (FareCalculationService, etc.)
│   └── Matching/        # Sub-services for driver matching
├── StateMachines/       # State transition logic (TripStateMachine, DriverStateMachine)
└── ValueObjects/        # Immutable value objects (Location, Money, Route)
```

## 3. Entities (with responsibilities + key methods)

- **AggregateRoot** (Base class): Manages domain events and identity for aggregates. Key methods: AddDomainEvent(), ClearDomainEvents(). Properties: Id (Guid), CreatedAt (DateTime).
- **User** (Abstract Aggregate Root): Represents system users with authentication. Responsibilities: User management and validation. Key methods: UpdateName(), UpdatePhone(), ChangePassword(), VerifyPassword().
- **Passenger**: Inherits User. Manages trip history. Responsibilities: Track passenger trips and active status. Key methods: AddTrip(), Deactivate(), Activate().
- **Driver**: Inherits User. Manages vehicle, location, wallet, and status. Responsibilities: Handle driver availability, location updates, and financials. Key methods: SetAvailable(), SetOnTrip(), SetOffline(), UpdateLocation(), PayCommission(), AddTrip(), UpdateRating().
- **Admin**: Inherits User. Responsibilities: Administrative access (no additional methods beyond User).
- **Vehicle** (Abstract entity): Represents vehicles with identity. Responsibilities: Define vehicle characteristics. Key methods: GetVehicleType(), GetMinSpeed(), GetMaxSpeed(), GetMaxPickupDistance().
- **Car**: Inherits Vehicle. Concrete car implementation.
- **Motorbike**: Inherits Vehicle. Concrete motorbike implementation.
- **Trip** (Aggregate Root): Manages trip lifecycle, state transitions, and financials. Responsibilities: Orchestrate trip states, validate transitions, emit events. Key methods: MatchDriver(), MarkAsArrived(), StartTrip(), CompleteTrip(), Cancel(), MarkTimeout().
- **FareRule**: Entity for pricing rules. Responsibilities: Define fare calculation parameters. Key methods: CalculateBaseFare(), CalculateCommission().
- **Payment** (Aggregate Root): Entity for payment records. Responsibilities: Track payment status, commission, and driver income. Key methods: MarkPaid().
- **Rating** (Aggregate Root): Entity for trip ratings. Responsibilities: Store rating data (score, comment). Note: Rating is a separate aggregate with its own identity, linked to Trip and Driver via foreign keys.

## 4. Value Objects

- **Location**: Immutable VO with Name, Address, Latitude, Longitude. Used for pickup/destination points.
- **Route**: Immutable VO with Start, End, Distance, Duration, Points list. Represents calculated routes.
- **Money**: Immutable VO with Amount and Currency. Handles monetary values.

## 5. Enums

- **TripStatus**: Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout.
- **DriverStatus**: Offline, Available, OnTrip.
- **VehicleType**: Motorbike, Car.

## 6. Domain Services

- **FareCalculationService**: Calculates trip fares and commissions based on FareRule. Key methods: CalculateFare(), CalculateCommission().
- **RouteValidationService**: Validates route data. Key methods: ValidateRoute().
- **DriverTripCompatibilityService**: Checks driver-trip compatibility. Key methods: IsCompatible().
- **DriverCandidateSelector** (in Matching subfolder): Selects eligible drivers based on location and availability criteria. Key methods: SelectEligibleCandidates().
- **DispatchArbitrator** (in Matching subfolder): Resolves assignment conflicts and selects final driver from candidates. Key methods: TryAssign().

## 7. Aggregates & Boundaries

- **Trip Aggregate**: Root entity is Trip. Contains trip lifecycle, status, and references to Passenger and Driver (by ID). No child entities owned by Trip.
- **User Aggregate**: Root entity is User (abstract) with subclasses Passenger, Driver, Admin. Driver composes Vehicle (owned entity) and Wallet/Income (Value Objects).
- **Payment Aggregate**: Root entity is Payment. Represents payment transaction for a trip with commission calculation. Independent aggregate referenced by TripId.
- **Rating Aggregate**: Root entity is Rating. Represents a rating given for a completed trip. Independent aggregate referenced by TripId and DriverId.
- **Aggregate Relationships**: 
  - Trip → Passenger, Driver: Association via foreign keys only (no ownership).
  - Trip → Payment, Rating: No direct ownership; both are independent aggregates linked by TripId.

## 8. Business Rules Implemented

- **Trip State Transitions**: Enforced by TripStateMachine. Trip is created in Requested state, then transitions to Searching upon RequestTrip() execution. Invalid transitions throw InvalidStatusTransitionException.
- **Driver Assignment**: `TripAssignmentPolicy` (instance-based, implements `ITripAssignmentPolicy`) ensures only one driver per trip. `DriverEligibilityPolicy` (instance-based, implements `IDriverEligibilityPolicy`) validates driver availability (active + available status).
- **Fare Calculation**: Base fare + distance-based pricing with commission deduction.
- **Driver Rating**: AverageRating updated after each rating (sum/count).
- **Vehicle Constraints**: Speed, capacity, pickup distance vary by VehicleType.
- **Payment Lifecycle**: Created as Pending in CompleteTrip, marked Paid in ConfirmPayment.
- **Concurrency Control**: Version-based optimistic concurrency prevents race conditions.
- **Event Emission**: Domain events fired on state changes (e.g., TripRequestedEvent on creation).

## 9. Design Issues / Inconsistencies (if any)

- **Vehicle as Entity**: Vehicle is correctly modeled as Entity due to mutable properties and lifecycle. Decision: Entity.
- **State Machine Coupling**: StateMachine separation is intentional — maintains SRP and testability.
- **Repository Interfaces in Domain**: Interfaces like ITripRepository are in Domain, but implementations in Infrastructure. Acceptable for DDD, but ensure no infra leaks.
- **Exception Hierarchy**: Multiple specific exceptions (e.g., TripAlreadyAssignedException) vs. generic BusinessRuleViolationException. Inconsistent usage.
- **Domain Events Refactoring** (completed):
  - Removed orphaned `Event.cs` (unused, non-standard base).
  - `TripRequestedEvent` no longer carries empty Distance/Fare.
  - `TripMatchedEvent` no longer carries denormalized DriverName/DriverPhone.
  - Removed `[Serializable]` from `DomainEvent` (infrastructure concern).
  - Policies converted to instance-based with interfaces for DI/testability.

## 10. Relationships

1. Core Relationships
   User hierarchy:
   User ⟶ Passenger, Driver, Admin
   → Inheritance (User is Aggregate Root)
   
   Driver – Vehicle:
   Driver ◼── Vehicle (1)
   → Composition (Vehicle lifecycle bound to Driver)
   Vehicle không tồn tại độc lập khi Driver bị xóa.
   
   Driver – Wallet/Income:
   Driver ◼── Money (Wallet, Income)
   → Composition (Value Object)
   
   Trip – Passenger – Driver:
   Trip ─── Passenger (1) via PassengerId
   Trip ─── Driver (0..1) via DriverId
   → Association (reference by ID only, no ownership)
   Trip không sở hữu lifecycle của Passenger/Driver.
   
   Trip – Location:
   Trip ◼── Location (Pickup, Destination)
   → Composition (Value Object, immutable)
   
   Trip – Payment:
   Trip ─── Payment (1)
   → Association (Payment là Aggregate Root riêng, linked by TripId)
   Payment có lifecycle độc lập, không thuộc về Trip.
   
   Trip – Rating:
   Trip ─── Rating (0..1)
   → Association (Rating là Aggregate Root riêng, linked by TripId)
   Rating có lifecycle độc lập, không thuộc về Trip.

2. Supporting Entities
   Payment – Driver:
   Payment ─── Driver
   → Association (used for commission/income tracking via DriverId)
   
   Rating – Driver / Passenger:
   Rating ─── Driver
   Rating ─── Passenger
   → Association (foreign key references)
   
   FareRule – Trip:
   FareRule → Trip
   → Dependency (used for fare calculation only, no persisted reference)

3. Domain Events
   AggregateRoot ◇── DomainEvent (base class, no longer [Serializable])
   → Aggregation
   Trip emits:
   - TripRequestedEvent (TripId, PassengerId, Pickup, Destination, VehicleType — no Distance/Fare as they're unknown at request time)
   - TripSearchingEvent (TripId)
   - TripMatchedEvent (TripId, DriverId, VehicleType — denormalized DriverName/Phone removed)
   - TripArrivedEvent (TripId)
   - TripStartedEvent (TripId)
   - TripCompletedEvent (TripId, Distance, Duration, Fare, DriverId)
   - TripCancelledEvent (TripId, Reason)
   - TripTimeoutEvent (TripId)
   Driver emits: DriverStatusChangedEvent, DriverLocationUpdatedEvent

   Note: Orphaned `Event.cs` class removed. Events contain only data available at the time of emission.

4. Application Layer Relationships
   Services:
   TripService → TripRepository, DriverRepository, PassengerRepository
   → Dependency
   TripService → DriverMatchingService
   → Dependency
   DriverMatchingService → RouteService
   → Dependency
   RatingService → RatingRepository, DriverRepository, TripRepository
   → Dependency
   PaymentService → PaymentRepository
   → Dependency
   
    Policies:
    TripAssignmentPolicy → ITripAssignmentPolicy (interface)
    TripAssignmentPolicy → IDriverEligibilityPolicy (dependency)
    DriverEligibilityPolicy → IDriverEligibilityPolicy (implements)
    → Dependency (interface-based injection)

5. Infrastructure
   JsonStorage<T> ─── Aggregate Root entities
   → Association (generic JSON persistence for all aggregates)
   TripMatchingWorker → TripService
   → Dependency
   DriverSimulationService → Driver
   → Association
