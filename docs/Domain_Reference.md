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
7. [State Pattern](#7-state-pattern)
8. [Repository Interfaces](#8-repository-interfaces)
9. [Relationships & Rules](#9-relationships--rules)

---

## 1. Folder Structure

```
Domain/
├── Enums/                    # VehicleType
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
│       └── Vehicle.cs (abstract)
├── Events/                   # 12 domain events
├── SharedKernel/             # Entity.cs, ValueObject.cs, DomainEvent.cs
├── States/                   # ITripState + 8 implementations, IDriverState + 3 implementations
│   └── Drivers/              # DriverAvailableState, DriverOfflineState, DriverOnTripState
├── Repositories/             # Repository interfaces (async)
└── ValueObjects/             # Money, Location, Route, Fare, etc.
```

---

## 2. SharedKernel

### `Entity` (abstract)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Guid Id` | protected set |
| 🟢 Method | `IReadOnlyList<DomainEvent> GetEvents()` | Undispatched events |
| 🟢 Method | `AddEvent(DomainEvent)` | protected internal |
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

### `Passenger` (: User)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `int TotalTrips` | Trip count |
| 🟢 Method | `AddTrip()` | Increment count |
| 🟢 Method | `GetInfo()` | override with [Passenger] label |

### `Driver` (: User)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `string Status` | Current status name (derived from IDriverState) |
| 🔵 Property | `Location Position` | GPS location |
| 🔵 Property | `Guid VehicleId` | Reference to vehicle |
| 🔵 Property | `string LicenseNumber` | Driver license |
| 🔵 Property | `Money Wallet` | Available balance |
| 🔵 Property | `Money Income` | Total earnings |
| 🔵 Property | `int TotalTrips` | Completed trips |
| 🔵 Property | `int TotalReviews` | Review count |
| 🔵 Property | `int RatingSum` | Sum of all ratings |
| 🔵 Computed | `decimal AverageRating` | `RatingSum / TotalReviews` |
| 🟢 State | `SetAvailable()`, `SetOnTrip()`, `SetOffline()` | Delegate to IDriverState |
| 🟢 Method | `IsAvailable()`, `IsOnTrip()`, `IsOffline()` | State check helpers |
| 🟢 Method | `UpdatePosition(Location)` | Update GPS (emits `DriverLocationUpdatedEvent`) |
| 🟢 Method | `AddTrip()` | Increment count |
| 🟢 Method | `UpdateReviews(int rating)` | Add rating to stats |
| 🟢 Method | `DepositToWallet(Money)` | Add balance |
| 🟢 Method | `PayCommission(Fare)` | Deduct commission from wallet |
| 🟢 Static | `GetDisplayString(string status)` | Status text |

### `Admin` (: User)

| Type | Name | Description |
|------|------|-------------|
| 🟢 Method | `GetInfo()` | override with admin title |

### `Trip` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `string Status` | Current trip status (derived from ITripState) |
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
| 🟢 Method | `IsSearching(), IsMatched(), IsArrived(), IsStarted(), IsCompleted(), IsCancelled(), IsTimeout()` | State check helpers |
| 🟢 Method | `IsTerminal()` | Terminal state check |

**Constructors:** Business (Requested + emits TripRequestedEvent and TripSearchingEvent via SetSearching()), ORM.

### `Vehicle` (abstract : Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `string PlateNumber` | License plate |
| 🔵 Property | `string Brand` | Vehicle brand |
| 🔵 Property | `string Model` | Vehicle model |
| 🔵 Property | `string Color` | Vehicle color |
| 🔵 Property | `int Capacity` | Passenger capacity |
| 🔵 Property | `VehicleType Type` | Car/Motorbike |
| 🟢 Abstract | `GetAvgSpeed()` | Average speed |

### `Car` (: Vehicle)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Type = Car` | Fixed |
| 🔵 Property | `AvgSpeed = 60km/h` | - |

### `Motorbike` (: Vehicle)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Type = Motorbike` | Fixed |
| 🔵 Property | `AvgSpeed = 40km/h` | - |

### `FareRule` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🟢 Method | `UpdateRule(...)` | Modify pricing |
| 🟢 Method | `CalculateFare(double distanceKm)` | Returns `Fare` |

### `Review` (: Entity)

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Guid PassengerId` | Reviewing passenger |
| 🔵 Property | `Guid DriverId` | Reviewed driver |
| 🔵 Property | `int Rating` | 1-5 scale |
| 🔵 Property | `string Comment` | Optional |
| 🟢 Method | `UpdateReview(int rating, string comment)` | Modify content |

---

## 4. Value Objects (sealed : ValueObject, immutable)

### `Money`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `decimal Amount` | ≥0 (enforced by callers, not constructor) |
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
| 🔵 Property | `double Latitude` | Vĩ độ |
| 🔵 Property | `double Longitude` | Kinh độ|

### `Address`

| Type | Name | Description |
|------|------|-------------|
| 🔵 Property | `Name` | Place name |
| 🔵 Property | `Street` | Tên đường |
| 🔵 Property | `District` | District/Quận |
| 🔵 Property | `City` | City/Thành phố |
| 🔵 Property | `Country` | Quốc gia|
| 🔵 Property | `HouseNumber` | Số nhà |
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
| `VehicleType` | Unknown(0), Motorbike(1), Car(2) |

> **Note:** `DriverStatus` and `TripStatus` enums do not exist in the codebase. Driver status is managed via the `IDriverState` pattern (string `Status` property). Trip status is managed via the `ITripState` pattern (string `Status` property with `IsXxx()` helpers).

---

## 6. Domain Events

All inherit `DomainEvent` (base with `Id`, `OccurredOn`).

### Trip Events

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `TripRequestedEvent` | Constructor | Id, PassengerId, Pickup, Destination, VehicleType |
| `TripSearchingEvent` | Constructor (via SetSearching()) | Id, AttemptNumber |
| `TripMatchedEvent` | `MatchDriver()` | Id, DriverId |
| `TripArrivedEvent` | `MarkAsArrived()` | Id |
| `TripStartedEvent` | `StartTrip()` | Id |
| `TripCompletedEvent` | `CompleteTrip()` | Id, PassengerId, DriverId, Fare |
| `TripPaidEvent` | `ConfirmPayment()` | Id, Amount, PaidAt |
| `TripCancelledEvent` | `Cancel()` | Id, Reason |
| `TripTimeoutEvent` | `MarkTimeout()` | Id |

### Driver Events

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `DriverStatusChangedEvent` | `TransitionTo()` | Id, OldStatus(string), NewStatus(string) |
| `DriverLocationUpdatedEvent` | `UpdatePosition()` | Id, NewLocation |

### Review Event

| Event | Emitted By | Parameters |
|-------|-----------|------------|
| `ReviewCreatedEvent` | Review submission | Id, RiderId, DriverId, Rating, Comment |

---

## 7. State Pattern

### Driver States (`IDriverState`)

> `Driver` uses the **State Pattern** with `IDriverState` implementations. It delegates `SetAvailable/SetOnTrip/SetOffline` to the current state.

| State Class | Description |
|-------------|-------------|
| `DriverAvailableState` | Driver is available for matching |
| `DriverOfflineState` | Driver is offline |
| `DriverOnTripState` | Driver is currently on a trip |

**Valid transitions:**
```
Offline → Available
Available → OnTrip, Offline
OnTrip → Available
```

### Trip State Pattern

> `Trip` uses the **State Pattern** with `ITripState` implementations that manage the lifecycle:

| State Class | Valid Next States |
|-------------|-------------------|
| `RequestedState` | Searching, Cancelled |
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

> **Note:** `IRepository<T>` and its specific interfaces define **data access contracts**, not a GoF Design Pattern. The implementation (`JsonRepository<T>`) is infrastructure-specific.

### Base Interface

```csharp
public interface IReadRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
}

public interface IRepository<T> : IReadRepository<T> where T : class
{
    Task InitializeAsync();
    Task SaveChangesAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
```

### Specific Interfaces

| Interface | Inherits | Extra Methods |
|-----------|----------|--------------|
| `IUserRepository` | *(standalone)* | `GetByPhoneAsync(string)`, `ExistsByPhoneAsync(string)`, `GetDriversAsync()`, `GetPassengersAsync()`, `GetAvailableDriversAsync()`, `GetDriverByIdAsync(Guid)` |
| `IDriverRepository` | `IRepository<Driver>` | `GetByPhoneAsync(string)`, `GetAvailableDriversAsync()`, `ExistsByPhoneAsync(string)` |
| `IPassengerRepository` | `IRepository<Passenger>` | `GetByPhoneAsync(string)`, `ExistsByPhoneAsync(string)` |
| `ITripRepository` | `IRepository<Trip>` | `GetByDriverIdAsync(Guid)`, `GetByPassengerIdAsync(Guid)` |
| `IVehicleRepository` | *(standalone)* | `GetByTypeAsync(VehicleType)` |
| `IReviewRepository` | `IRepository<Review>` | `GetByDriverIdAsync(Guid)`, `GetByTripIdAsync(Guid)` |
| `IFareRuleRepository` | `IRepository<FareRule>` | `GetByVehicleTypeAsync(VehicleType)`, `EnsureSeededAsync()` |

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

- State transitions validated by `ITripState` / `IDriverState` — cannot bypass
- `Money.Amount` must be ≥ 0 (enforced by callers, not constructor)
- `Review.Rating` must be 1-5
- Driver `Wallet` must cover commission before accepting trip
- Events accumulated in-memory, cleared post-dispatch
- No explicit optimistic concurrency (no `Version` field)

---

*Document version: 3.1 — Updated: fixed Entity non-generic, Driver State Pattern, Vehicle properties, Event parameters, Repository async interfaces, removed DriverStateMachine references.*
