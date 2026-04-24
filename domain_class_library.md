# 📚 Domain Class Library – Thuộc tính, Phương thức & Sự kiện

Tài liệu chính xác từ code Domain hiện tại (Ride-Hailing OOP2026).

## 🏗️ Cấu trúc Thư mục

```
Domain/ (xem Domain_Model.md cho chi tiết)
├── SharedKernel/Entity.cs (base)
├── ValueObjects/{Location,Money,...}.cs
├── Users/{User,Passenger,Driver,Admin,...}
├── Trips/Trip.cs + Events/
└── ...
```

## 1. SharedKernel

### `Entity<Guid>` (abstract)
| Loại | Tên | Mô tả |
|------|-----|-------|
| 🔵 | `Guid Id` | protected set |
| 🔵 | `IReadOnlyCollection<DomainEvent> DomainEvents` | Events chưa dispatch |
| 🟢 | `AddEvent(DomainEvent)` | protected |
| 🟢 | `ClearEvents()` | - |
| 🟢 | `Equals/GetHashCode` | By Id |

## 2. ValueObjects (sealed : ValueObject)

### `Money`
| 🔵 | `decimal Amount` (≥0, round 2dp) |
| 🔵 | `string Currency` (VND default) |
| ➕ | Operators: `+ - < > <= >=` (same currency) |
| 🟢 | `ToString()`: \"{0:N0} {1}\" |

### `Location`
| 🔵 | `Coordinate Coordinate` |
| 🔵 | `Address Address` |

## 3. Users Aggregate

### `User` (abstract : Entity)
| 🔵 | `string Name/Phone` | validated |
| 🔵 | `string Password` | hashed readonly |
| 🟢 | `UpdateName/Phone(string)` |
| 🟢 | `ChangePassword(oldRaw,newRaw)` | verify old |
| 🟢 | `VerifyPassword(raw)` |
| 🟢 | `GetInfo()` | virtual |

**Ctors:** Business (hash pass), persistence, ORM.

### `Passenger` (sealed : User)
| 🔵 | `int TotalTrips` |
| 🟢 | `AddTrip()` |
| 🟢 | `GetInfo()` | override w/ [Hành khách] |

### `Driver` (: User)
| 🔵 | `DriverStatus Status` |
| 🔵 | `Location Position` |
| 🔵 | `Guid VehicleId`, `string LicenseNumber` |
| 🔵 | `Money Wallet/Income` |
| 🔵 | `int TotalTrips/TotalReviews/RatingSum` |
| 🔵 computed | `decimal AverageRating` |
| 🟢 State | `SetAvailable/OnTrip/Offline()` | StateMachine + Event |
| 🟢 | `UpdatePosition(Location)` |
| 🟢 | `AddTrip()`, `UpdateReviews(Review)` |
| 🟢 | `DepositToWallet(Money)`, `PayCommission(Fare)` |
| 🟢 static | `GetDisplayString(DriverStatus)` |

**Event:** DriverStatusChangedEvent(Id, Old/NewStatus)

### `Admin` (: User)
| 🟢 | `GetInfo()` | override admin title |

## 4. Trip (: Entity)
| 🔵 | `TripStatus Status` |
| 🔵 | `Guid PassengerId/DriverId?` |
| 🔵 | `VehicleType TripVehicleType` |
| 🔵 | `Route TripRoute`, `Fare TripFare` |
| 🔵 computed | `double? Distance/Duration`, `bool IsPaid`, `DateTime RequestAt` |
| 🟢 State | `SetSearching()`, `MatchDriver(Guid)`, `MarkAsArrived()`, `StartTrip()`, `CompleteTrip()`, `ConfirmPayment()`, `Cancel(reason)`, `MarkTimeout()` | StateMachine + Events |
| private | `SetStatus(newStatus)` |

**Ctors:** Business (Requested + events), ORM.

**Events (9):**
| Event | Params |
|-------|--------|
| TripRequested | Id,PassengerId,Pickup,Dest,VehicleType |
| TripSearching/Matched/Arrived/Started | Id (+DriverId/VType for Matched) |
| TripCompleted | Id,PassengerId,DriverId,Fare |
| TripPaid | Id,PassengerId,DriverId,TotalAmount |
| TripCancelled | Id,Reason |
| TripTimeout | Id |

## 5. Vehicles
### `Vehicle` (abstract)
🟢 Abstract methods: CloneWithDriver, GetVehicleType/IsCar/Min/MaxSpeed/MaxPickupDistance.

### `Car/Motorbike`
Specific capacity/speed/pickup values.

## 6. Other
- `FareRule`: `UpdateRule`, `CalculateFare(double)`.
- `Review`: Rating/comment.

## 7. State Machines (static)
- `TripStateMachine.CanTransition(from,to)`
- `DriverStateMachine.CanTransition(from,to)`

**Notes:** Docs match code exactly. No inventions."

