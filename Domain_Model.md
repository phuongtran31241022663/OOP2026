# Domain Layer Technical Documentation

## 1. Purpose of Domain Layer

The Domain Layer contains core business entities, value objects, enums, domain events, and state machines for the Ride-Hailing System. All classes inherit from `Entity<Guid>` base with domain event support. Implements basic DDD patterns: entities with behavior, immutable value objects, state machines for lifecycles, repository interfaces.

Independent of infrastructure; persistence impls in Infrastructure layer.

## 2. Actual Folder / Namespace Structure

```
Domain/
├── Enums/                    # DriverStatus.cs, TripStatus.cs, VehicleType.cs
├── Entities/                 # Core business entities
│   ├── FareRule.cs
│   ├── Review.cs
│   ├── Trip.cs
│   ├── User.cs (abstract)
│   ├── Users/
│   │   ├── Admin.cs
│   │   ├── Driver.cs
│   │   └── Passenger.cs
│   ├── Vehicles/
│   │   ├── Car.cs
│   │   ├── Motorbike.cs
│   │   └── Vehicle.cs
│   └── Vehicles/             # (alias for clarity)
├── Events/                   # Domain events
│   ├── Trip*.cs (9 events)
│   ├── DriverStatusChangedEvent.cs
│   └── ReviewCreatedEvent.cs
├── SharedKernel/             # Entity.cs, DomainEvent.cs, ValueObject.cs
├── StateMachines/            # DriverStateMachine.cs
├── States/                   # State Pattern for Trip lifecycle
│   ├── ITripState.cs
│   ├── RequestedState.cs
│   ├── SearchingState.cs
│   ├── MatchedState.cs
│   ├── ArrivedState.cs
│   ├── StartedState.cs
│   ├── CompletedState.cs
│   ├── CancelledState.cs
│   └── TimeoutState.cs
├── Repositories/             # IRepository.cs, ITripRepository.cs, IDriverRepository.cs, ...
└── ValueObjects/             # Address.cs, Coordinate.cs, Fare.cs, Location.cs, Money.cs, Route.cs
```

## 3. Key Entities & Methods

### Base: `Entity` (abstract)
- Properties: `Guid Id` (protected set), `IReadOnlyList<DomainEvent> DomainEvents`
- Methods: `AddEvent(DomainEvent)`, `GetEvents()`, `ClearEvents()`, `Equals/HashCode` by Id.

### `User` (abstract : Entity)
- Properties: `Name`, `Phone`, `Password` (hashed readonly).
- Methods: `UpdateName`, `UpdatePhone`, `ChangePassword`, `VerifyPassword`, `GetInfo()` (virtual).

### `Passenger` (sealed : User)
- Properties: `TotalTrips`.
- Methods: `AddTrip()`, `GetInfo()` (override).

### `Admin` (: User)
- Methods: `GetInfo()` (override w/ admin title).

### `Driver` (: User)
- Properties: `Status` (DriverStatus), `Position` (Location), `VehicleId` (Guid), `Wallet/Income` (Money), `TotalTrips`, `RatingSum/TotalReviews`, `AverageRating` (computed), `LicenseNumber`.
- Methods: State: `SetAvailable/OnTrip/Offline` (validate via DriverStateMachine, emit `DriverStatusChangedEvent`).
  - `UpdatePosition(Location)`, `AddTrip()`, `UpdateReviews(int rating)`, `DepositToWallet(Money)`, `PayCommission(Fare)`.
  - `GetDisplayString(DriverStatus)` (static).

### `Trip` (: Entity)
- Properties: `Status` (TripStatus), `PassengerId/DriverId?` (Guid), `TripVehicleType`, `TripRoute` (Route), `TripFare` (Fare), `Distance/Duration?` (computed), `IsPaid`, `RequestAt`.
- Methods: State transitions (validate via ITripState implementations, emit events):
  | Method | New Status | Emits Event |
  |--------|------------|-------------|
  | `SetSearching()` | Searching | TripSearchingEvent |
  | `MatchDriver(Guid)` | Matched | TripMatchedEvent |
  | `MarkAsArrived()` | Arrived | TripArrivedEvent |
  | `StartTrip()` | Started | TripStartedEvent |
  | `CompleteTrip()` | Completed | TripCompletedEvent |
  | `ConfirmPayment()` | - | TripPaidEvent |
  | `Cancel(string)` | Cancelled | TripCancelledEvent |
  | `MarkTimeout()` | Timeout | TripTimeoutEvent |
- Constructor: Business (sets Requested, emits Requested/Searching), ORM private.

### Other Entities
- `Vehicle` (abstract), `Car/Motorbike` impls.
- `FareRule`: Pricing rules w/ `CalculateFare(double)`.
- `Review`: Rating/comment.

## 4. Value Objects (sealed : ValueObject, immutable)
- `Money`: `Amount` (decimal≥0 rounded), `Currency` ("VND" default); operators `+ - < > <= >=`.
- `Location`: Composed `Coordinate`, `Address`.
- `Route`, `Fare`, `Address`, `Coordinate`.

## 5. Enums
- `TripStatus`: Requested(0), Searching(1), Matched(2), Arrived(3), Started(4), Completed(5), Cancelled(6), Timeout(7).
- `DriverStatus`: Offline, Available, OnTrip.
- `VehicleType`: Car, Motorbike (code implies Unknown possible).

## 6. Domain Events (: DomainEvent)
- **Trip Events (9):** RequestedEvent(PassengerId,Pickup,Dest,VehicleType), SearchingEvent(Id), MatchedEvent(Id,DriverId,VehicleType), Arrived/StartedEvent(Id), CompletedEvent(Id,PassengerId,DriverId,Fare), PaidEvent(Id,PassengerId,DriverId,TotalAmount), CancelledEvent(Id,Reason), TimeoutEvent(Id).
- **Driver:** StatusChangedEvent(Id,OldStatus,NewStatus).

## 7. State Machines & State Pattern
- **DriverStateMachine** (`static class`): `CanTransition(DriverStatus from, DriverStatus to)` — dictionary-defined valid flows.
- **Trip State Pattern**: `Trip` delegates state behavior to `ITripState` implementations (`RequestedState`, `SearchingState`, `MatchedState`, `ArrivedState`, `StartedState`, `CompletedState`, `CancelledState`, `TimeoutState`). Each state validates transitions before calling `trip.TransitionTo(...)`.

## 8. Repository Interfaces (Domain only)
- `IFareRuleRepository`, `IReviewRepository`, `ITripRepository`, `IUserRepository`, `IDriverRepository`, `IPassengerRepository`, `IVehicleRepository`.

## 9. Relationships & Rules
- **Inheritance:** User → Passenger/Driver/Admin.
- **Composition:** Trip composes Route/Fare; Driver refs VehicleId; Location comp Coordinate/Address.
- **Associations:** Trip refs PassengerId/DriverId.
- **Rules:** Enforced in methods (e.g. state transitions, wallet balance checks); events on changes.
- **Concurrency:** No explicit Version (commented out in some).

## 10. Implementation Notes
- Events accumulated in-memory (`AddEvent`), cleared post-dispatch.
- Validation in init setters/ctors.
- No advanced DDD (no AggregateRoot sep, no specs/policies/services beyond DriverStateMachine).

