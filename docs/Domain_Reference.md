# Domain Layer Reference — RideGo

> **Platform:** .NET Framework 4.8 · C# 7.3
> **Scope:** Core business entities, value objects, state machines, events, repository interfaces

---

## Table of Contents

1. [Folder Structure](#1-folder-structure)
2. [SharedKernel](#2-sharedkernel)
3. [Entities](#3-entities)
4. [Value Objects](#4-value-objects)
5. [Enums](#5-enums)
6. [Domain Events](#6-domain-events)
7. [State Machines](#7-state-machines)
8. [Repository Interfaces](#8-repository-interfaces)
9. [Relationships & Rules](#9-relationships--rules)

---

## 1. Folder Structure

```
Domain/
├── Enums/                    # DriverStatus, TripStatus, VehicleType
├── Entities/                 # Core business entities
│   ├── FareRule.cs
│   ├── Review.cs
│   ├── Trip.cs
│   ├── User.cs (abstract)
│   ├── Users/
│   │   ├── Admin.cs
│   │   ├── Driver.cs
│   │   └── Passenger.cs
│   └── Vehicles/
│       ├── Car.cs
│       ├── Motorbike.cs
│       └── Vehicle.cs
├── Events/                   # 12 domain events
├── SharedKernel/             # Entity.cs, ValueObject.cs, DomainEvent.cs
├── StateMachines/            # DriverStateMachine.cs
├── States/                   # ITripState + 8 implementations
├── Repositories/             # Repository interfaces
└── ValueObjects/             # Money, Location, Route, etc.
```

---

## 2. SharedKernel

### `Entity<Guid>` (abstract)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Guid Id` | protected set |
| 🔵 Property | `IReadOnlyCollection<DomainEvent> DomainEvents` | Undispatched events |
| 🟢 Method | `AddEvent(DomainEvent)` | protected |
| 🟢 Method | `ClearEvents()` | Clear event list |
| 🟢 Method | `Equals/GetHashCode` | By Id |

---

## 3. Entities

### `User` (abstract : Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `string Name/Phone` | Validated |
| 🔵 Property | `string Password` | Hashed, readonly |
| 🟢 Method | `UpdateName(string)`, `UpdatePhone(string)` | - |
| 🟢 Method | `ChangePassword(oldRaw, newRaw)` | Verify old password |
| 🟢 Method | `VerifyPassword(raw)` | Check password match |
| 🟢 Method | `GetInfo()` | virtual |

**Constructors:** Business (hash password), persistence, ORM.

### `Passenger` (sealed : User)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `int TotalTrips` | Trip count |
| 🟢 Method | `AddTrip()` | Increment count |
| 🟢 Method | `GetInfo()` | override with [Passenger] label |

### `Driver` (: User)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `DriverStatus Status` | Current work status |
| 🔵 Property | `Location Position` | GPS location |
| 🔵 Property | `Guid VehicleId` | Reference to vehicle |
| 🔵 Property | `string LicenseNumber` | Driver license |
| 🔵 Property | `Money Wallet` | Available balance |
| 🔵 Property | `Money Income` | Total earnings |
| 🔵 Property | `int TotalTrips` | Completed trips |
| 🔵 Property | `int TotalReviews` | Review count |
| 🔵 Property | `int RatingSum` | Sum of all ratings |
| 🔵 Computed | `decimal AverageRating` | `RatingSum / TotalReviews` |
| 🟢 State | `SetAvailable()`, `SetOnTrip()`, `SetOffline()` | Validate via DriverStateMachine + emit DriverStatusChangedEvent |
| 🟢 Method | `UpdatePosition(Location)` | Update GPS |
| 🟢 Method | `AddTrip()` | Increment count |
| 🟢 Method | `UpdateReviews(int rating)` | Add rating to stats |
| 🟢 Method | `DepositToWallet(Money)` | Add balance |
| 🟢 Method | `PayCommission(Fare)` | Deduct commission from wallet |
| 🟢 Static | `GetDisplayString(DriverStatus)` | Status text |

**Event:** `DriverStatusChangedEvent(Id, OldStatus, NewStatus)`

### `Admin` (: User)

| Type | Name | Description |
|------|------|-------------|
| 🟢 Method | `GetInfo()` | override with admin title |

### `Trip` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `TripStatus Status` | Current trip status |
| 🔵 Property | `Guid PassengerId` | Booking passenger |
| 🔵 Property | `Guid? DriverId` | Assigned driver |
| 🔵 Property | `VehicleType TripVehicleType` | Requested vehicle type |
| 🔵 Property | `Route TripRoute` | Pickup → destination |
| 🔵 Property | `Fare TripFare` | Calculated fare |
| 🔵 Computed | `double? Distance/Duration` | From route |
| 🔵 Computed | `bool IsPaid` | Payment status |
| 🔵 Property | `DateTime RequestAt` | Request timestamp |
| 🟢 State | `SetSearching()` | Status → Searching, emits TripSearchingEvent |
| 🟢 State | `MatchDriver(Guid)` | Status → Matched, emits TripMatchedEvent |
| 🟢 State | `MarkAsArrived()` | Status → Arrived, emits TripArrivedEvent |
| 🟢 State | `StartTrip()` | Status → Started, emits TripStartedEvent |
| 🟢 State | `CompleteTrip()` | Status → Completed, emits TripCompletedEvent |
| 🟢 State | `ConfirmPayment()` | Emits TripPaidEvent |
| 🟢 State | `Cancel(string reason)` | Status → Cancelled, emits TripCancelledEvent |
| 🟢 State | `MarkTimeout()` | Status → Timeout, emits TripTimeoutEvent |
| 🔒 Private | `SetStatus(newStatus)` | Internal setter |

**Constructors:** Business (Requested + events), ORM.

### `Vehicle` (abstract : Entity)

| Type | Name | Description |
|------|------|-------------|
| 🟢 Abstract | `GetVehicleType()` | Returns VehicleType |
| 🟢 Abstract | `IsCar()` | Boolean check |
| 🟢 Abstract | `GetMinSpeed()`, `GetMaxSpeed()` | Speed range |
| 🟢 Abstract | `GetMaxPickupDistance()` | Search radius |

### `Car` (: Vehicle)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Type = Car` | Fixed |
| 🔵 Property | `AvgSpeed = 60km/h` | - |
| 🔵 Property | `MaxPickupDistance = 7km` | - |

### `Motorbike` (: Vehicle)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Type = Motorbike` | Fixed |
| 🔵 Property | `AvgSpeed = 40km/h` | - |
| 🔵 Property | `MaxPickupDistance = 5km` | - |

### `FareRule` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🟢 Method | `UpdateRule(...)` | Modify pricing |
| 🟢 Method | `CalculateFare(double distanceKm)` | Returns `Fare` |

### `Review` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `int Rating` | 1-5 scale |
| 🔵 Property | `string Comment` | Optional |
| 🟢 Method | `UpdateReview()` | Modify content |

---

## 4. Value Objects (sealed : ValueObject, immutable)

### `Money`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `decimal Amount` | ≥0, rounded 2dp |
| 🔵 Property | `string Currency` | Default "VND" |
| ➕ Operator | `+ - < > <= >=` | Same currency only |
| 🟢 Method | `ToString()` | `"{0:N0} {1}"` format |

### `Location`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Coordinate Coordinate` | GPS coords |
| 🔵 Property | `Address Address` | Structured address |

### `Coordinate`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `double Latitude` | - |
| 🔵 Property | `double Longitude` | - |

### `Address`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Name` | Place name |
| 🔵 Property | `Street` | Street name |
| 🔵 Property | `District` | District/Quận |
| 🔵 Property | `City` | City/Thành phố |
| 🔵 Property | `Country` | - |
| 🔵 Property | `HouseNumber` | - |
| 🔵 Property | `Osm_Value` | OSM tag |
| 🔵 Property | `Locality` | Phường |

### `Route`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Pickup (Location)` | Start point |
| 🔵 Property | `Destination (Location)` | End point |
| 🔵 Property | `double Distance` | km |
| 🔵 Property | `TimeSpan Duration` | Estimated time |
| 🔵 Property | `string Polyline` | Encoded polyline |

### `Fare`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Money TotalAmount` | Full fare |
| 🔵 Property | `Money Commission` | Platform cut |
| 🔵 Property | `Money DriverIncome` | `TotalAmount - Commission` |

---

## 5. Enums

| Enum | Values |
|------|--------|
| `TripStatus` | Requested(0), Searching(1), Matched(2), Arrived(3), Started(4), Completed(5), Cancelled(6), Timeout(7) |
| `DriverStatus` | Offline(0), Available(1), OnTrip(2) |
| `VehicleType` | Unknown(0), Motorbike(1), Car(2) |

---

## 6. Domain Events

All inherit `DomainEvent` (base with `Id`, `OccurredOn`).

### Trip Events

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `TripRequestedEvent` | Constructor | Id, PassengerId, Pickup, Destination, VehicleType |
| `TripSearchingEvent` | `SetSearching()` | Id |
| `TripMatchedEvent` | `MatchDriver()` | Id, DriverId, VehicleType |
| `TripArrivedEvent` | `MarkAsArrived()` | Id |
| `TripStartedEvent` | `StartTrip()` | Id |
| `TripCompletedEvent` | `CompleteTrip()` | Id, PassengerId, DriverId, Fare |
| `TripPaidEvent` | `ConfirmPayment()` | Id, PassengerId, DriverId, TotalAmount |
| `TripCancelledEvent` | `Cancel()` | Id, Reason |
| `TripTimeoutEvent` | `MarkTimeout()` | Id |

### Driver Events

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `DriverStatusChangedEvent` | Status change | Id, OldStatus, NewStatus |
| `DriverLocationUpdatedEvent` | Position update | Id, OldPosition, NewPosition |

### Review Event

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `ReviewCreatedEvent` | Review submission | Id, DriverId, PassengerId, TripId, Rating |

---

## 7. State Machines

### DriverStateMachine (static class)

```csharp
public static class DriverStateMachine
{
    private static readonly Dictionary<DriverStatus, DriverStatus[]> _transitions = ...
    
    public static bool CanTransition(DriverStatus from, DriverStatus to)
}
```

**Valid transitions:**
```
Offline → Available
Available → OnTrip, Offline
OnTrip → Available
```

### Trip State Pattern

`Trip` delegates state behavior to `ITripState` implementations:

| State Class | Valid Next States |
|-------------|-------------------|
| `RequestedState` | Searching |
| `SearchingState` | Matched, Cancelled, Timeout |
| `MatchedState` | Arrived, Cancelled, Searching |
| `ArrivedState` | Started, Cancelled |
| `StartedState` | Completed, Cancelled |
| `CompletedState` | (terminal) |
| `CancelledState` | (terminal) |
| `TimeoutState` | (terminal) |

Each state validates transition before calling `trip.TransitionTo(...)`.

---

## 8. Repository Interfaces

### Base Interface

```csharp
public interface IRepository<T> where T : Entity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    List<T> GetAll();
    T GetById(Guid id);
    Task SaveChangesAsync();
    Task InitializeAsync();
}
```

### Specific Interfaces

| Interface | Extra Methods |
|-----------|--------------|
| `IUserRepository` | `GetByPhoneAsync(string)`, `GetDriversAsync()`, `GetAvailableDriversAsync()` |
| `IDriverRepository` | (inherits IUserRepository pattern) |
| `IPassengerRepository` | - |
| `ITripRepository` | `GetByDriverIdAsync(Guid)`, `GetByPassengerIdAsync(Guid)`, `GetPendingTripsAsync()` |
| `IVehicleRepository` | - |
| `IReviewRepository` | - |
| `IFareRuleRepository` | - |

---

## 9. Relationships & Rules

### OOP Relationships

| Relationship | Example |
|-------------|---------|
| **Inheritance** | `Entity → User → Driver/Passenger/Admin`; `Entity → Vehicle → Car/Motorbike` |
| **Composition** | `Trip` contains `Route` and `Fare`; `Route` contains `Location` |
| **Aggregation** | `Driver` references `VehicleId` (does not own) |
| **Association** | `Trip` references `PassengerId`, `DriverId` |
| **Abstraction** | `Vehicle.GetAvgSpeed()` abstract, subclasses override |

### Business Rules

- State transitions validated by `ITripState` / `DriverStateMachine` — cannot bypass
- `Money.Amount` must be ≥ 0
- `Review.Rating` must be 1-5
- Driver `Wallet` must cover commission before accepting trip
- Events accumulated in-memory, cleared post-dispatch
- No explicit optimistic concurrency (no `Version` field)

---

*Document version: 2.0 — Consolidated from Domain_Model.md and domain_class_library.md.*
