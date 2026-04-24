# Domain Layer Technical Documentation

## 1. Purpose of Domain Layer

The Domain Layer contains core business entities, value objects, enums, domain events, and state machines for the Ride-Hailing System. All classes inherit from `Entity<Guid>` base with domain event support. Implements basic DDD patterns: entities with behavior, immutable value objects, state machines for lifecycles, repository interfaces.

Independent of infrastructure; persistence impls in Infrastructure layer.

## 2. Actual Folder / Namespace Structure

```
Domain/
├── Enums/                    # DriverStatus.cs, TripStatus.cs, VehicleType.cs
├── FareRules/                # FareRule.cs, IFareRuleRepository.cs
├── Reviews/                  # Review.cs, IReviewRepository.cs
├── SharedKernel/             # Entity.cs, DomainEvent.cs, IRepository.cs, ValueObject.cs
├── StateMachines/            # DriverStateMachine.cs, TripStateMachine.cs
├── Trips/                    # ITripRepository.cs, Trip.cs
│   └── Events/               # 9 Trip*Event.cs
├── Users/                    # Admin.cs, IUserRepository.cs, User.cs (abstract)
│   ├── Drivers/              # Driver.cs, IDriverRepository.cs
│   │   └── Events/           # DriverStatusChangedEvent.cs
│   └── Passengers/           # IPassengerRepository.cs, Passenger.cs
├── ValueObjects/             # Address.cs, Coordinate.cs, Fare.cs, Location.cs, Money.cs, Route.cs
└── Vehicles/                 # Car.cs, IVehicleRepository.cs, Motorbike.cs, Vehicle.cs
```

## 3. Key Entities & Methods

### Base: `Entity<Guid>` (abstract)
- Properties: `Guid Id` (protected set), `IReadOnlyCollection<DomainEvent> DomainEvents`
- Methods: `AddEvent(DomainEvent)`, `ClearEvents()`, `Equals/HashCode` by Id.

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
  - `UpdatePosition(Location)`, `AddTrip()`, `UpdateReviews(Review)`, `DepositToWallet(Money)`, `PayCommission(Fare)`.
  - `GetDisplayString(DriverStatus)` (static).

### `Trip` (: Entity)
- Properties: `Status` (TripStatus), `PassengerId/DriverId?` (Guid), `TripVehicleType`, `TripRoute` (Route), `TripFare` (Fare), `Distance/Duration?` (computed), `IsPaid`, `RequestAt`.
- Methods: State transitions (validate via TripStateMachine, emit events):
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

## 7. State Machines (static)
- `TripStateMachine.CanTransition(from,to)`: Dictionary-defined valid flows.
- `DriverStateMachine.CanTransition(from,to)`.

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
- No advanced DDD (no AggregateRoot sep, no specs/policies/services beyond state machines)."

