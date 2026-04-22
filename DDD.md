# Domain-Driven Design (DDD) - Tài liệu Thực tế

## 1. Thiết kế Chiến lược (Strategic Design)

### 1.1 Ubiquitous Language (Ngôn ngữ Chung)
Các thuật ngữ thống nhất trong toàn bộ hệ thống:
- **Trip** (Chuyến đi): Một yêu cầu di chuyển từpassenger
- **Driver** (Tài xế): Người cung cấp dịch vụ di chuyển
- **Passenger** (Hành khách): Người sử dụng dịch vụ
- **Vehicle** (Phương tiện): Xe máy, ô tô thuộc về Driver
- **Payment** (Thanh toán): Giao dịch tài chính cho một Trip
- **Fare** (Cước phí): Giá tiền cho chuyến đi
- **Location** (Vị trí): Địa điểm đón/trả
- **Route** (Tuyến đường): Lộ trình di chuyển

### 1.2 Bounded Contexts (Ngữ cảnh Giới hạn)
- **User Context**: Quản lý tài khoản người dùng (`User` abstract → `Passenger`, `Driver`, `Admin`)
- **Trip Context**: Quản lý chuyến đi từ khi yêu cầu đến khi hoàn thành/hủy
- **Vehicle Context**: Quản lý phương tiện của tài xế (embedded trong Driver aggregate)
- **Payment Context**: Xử lý thanh toán và tài chính
- **Matching Context**: Hệ thống ghép cặp passenger và driver (Services/Matching)

---

## 2. Thiết kế Chiến thuật (Tactical Design)

### 2.1 Cấu trúc Thư mục Thực tế

```
Domain/
├── Aggregates/
│   ├── Trip/
│   │   ├── Trip.cs                 (Aggregate Root)
│   │   ├── TripId.cs               (Strongly-typed ID)
│   │   ├── Entities/
│   │   │   └── Feedback.cs         (Entity con)
│   │   ├── ValueObjects/
│   │   │   ├── Route.cs            (Distance, Duration, Points, Start/End)
│   │   │   └── Fare.cs             (TotalAmount, CommissionRate, computed Commission/Income)
│   │   ├── Repositories/
│   │   │   └── ITripRepository.cs  (GetByDriverId, GetByPassengerId, GetPendingTrips, UpdateWhereVersionMatches)
│   │   ├── Events/
│   │   │   ├── TripRequestedEvent.cs
│   │   │   ├── TripSearchingEvent.cs
│   │   │   ├── TripMatchedEvent.cs
│   │   │   ├── TripArrivedEvent.cs
│   │   │   ├── TripStartedEvent.cs
│   │   │   ├── TripCompletedEvent.cs
│   │   │   ├── TripCancelledEvent.cs
│   │   │   └── TripTimeoutEvent.cs
│   │   └── TripStatus.cs           (Enum)
│   │
│   ├── Driver/
│   │   ├── Driver.cs               (Aggregate Root, kế thừa từ User)
│   │   ├── DriverId.cs
│   │   ├── DriverStatus.cs         (Enum: Offline, Available, OnTrip)
│   │   ├── IDriverRepository.cs    (GetByPhone, GetAvailableDrivers, ExistsByPhone)
│   │   ├── Events/
│   │   │   ├── DriverStatusChangedEvent.cs
│   │   │   └── DriverLocationUpdatedEvent.cs
│   │  └── ValueObjects/Vehicle/
│   │       ├── Vehicle.cs          (Abstract entity, không phải Aggregate Root)
│   │       ├── VehicleType.cs      (Enum: Motorbike, Car)
│   │       └── Repositories/
│   │           └── IVehicleRepository.cs
│   │
│   ├── User/
│   │   ├── User.cs                 (Abstract Aggregate Root)
│   │   ├── UserId.cs
│   │   ├── IUserRepository.cs      (GetByPhone, ExistsByPhone)
│   │   ├── Passenger/
│   │   │   ├── Passenger.cs        (Concrete, sealed)
│   │   │   └── PassengerId.cs
│   │   ├── Admin/
│   │   │   └── Admin.cs            (Concrete)
│   │   └── Repositories/
│   │       └── IPassengerRepository.cs
│   │
│   ├── Payment/
│   │   ├── Payment.cs              (Aggregate Root)
│   │   ├── PaymentId.cs            (Strongly-typed ID)
│   │   └── Repositories/
│   │       └── IPaymentRepository.cs
│   │
│   └── ... (các aggregates khác)
│
├── ValueObjects/
│   ├── Finance/
│   │   └── Money.cs                (Immutable, value-based equality, toán tử số học)
│   ├── Map/
│   │   ├── Location.cs             (Vị trí tọa độ, tính khoảng cách Haversine)
│   │   ├── Address.cs              (Địa chỉ chi tiết)
│   │   └── Coordinate.cs
│   ├── Rating.cs                   (Đánh giá 1-5 sao)
│   └── ...
│
├── Policies/                       (Chính sách nghiệp vụ - Strategy pattern)
│   ├── FareRule.cs                 (Quy tắc tính cước: BaseFare, PricePerKm, CommissionRate)
│   ├── TripAssignmentPolicy.cs     (Validate driver assignment)
│   ├── ITripAssignmentPolicy.cs
│   ├── DriverEligibilityPolicy.cs  (Kiểm tra driver có thể nhận chuyến)
│   └── IDriverEligibilityPolicy.cs
│
├── Specifications/                 (Specification pattern - query/validation封装)
│   ├── DriverAvailableSpec.cs      (internal, stub implementation)
│   └── TripMatchableSpec.cs        (exists but minimal)
│
├── StateMachines/                  (State Machine cho lifecycle management)
│   ├── TripStateMachine.cs         (Valid transitions: Requested→Searching/Cancel; Searching→Matched/Cancel/Timeout; Matched→Arrived/Searching/Cancel; Arrived→Start/Cancel; Started→Complete/Cancel)
│   └── DriverStateMachine.cs       (Offline↔Available↔OnTrip)
│
├── Services/                       (Domain Services - logic không thuộc về任何 Aggregate)
│   ├── FareCalculationService.cs
│   ├── PaymentCalculationService.cs
│   ├── RouteValidationService.cs
│   ├── DriverTripCompatibilityService.cs
│   └── Matching/
│       ├── DriverCandidateSelector.cs  (Filter + sort eligible drivers by distance)
│       └── DispatchArbitrator.cs       (Orchestrate assignment: policy check → MatchDriver → SetOnTrip)
│
├── Primatives/                     (Lớp cơ sở reusable cho toàn Domain)
│   ├── Entity.cs                  (Generic: Entity<TId>)
│   ├── ValueObject.cs             (Base for immutable VOs)
│   ├── AggregateRoot.cs           (Id, CreatedAt, Version, DomainEvents, ClearDomainEvents)
│   ├── StronglyTypedId.cs         (Abstract base cho strongly-typed IDs)
│   ├── DomainEvent.cs             (Id, OccurredOn, equality)
│   ├── DomainException.cs         (Base exception, inherits from Common.Exceptions.BaseException)
│   ├── IRepository.cs             (IRepository<T>, IReadRepository<T>, IUnitOfWork)
│   └── ...
│
├── Exceptions/                     (Business Exceptions cụ thể)
│   ├── BusinessRuleViolationException.cs  (Có RuleName để logging)
│   ├── InvalidStatusTransitionException.cs
│   ├── DriverUnavailableException.cs
│   ├── TripAlreadyAssignedException.cs
│   └── ConcurrencyException.cs
│
├── SharedKernel/                   (Reusable value objects cross-context - có thể duplicate với ValueObjects/)
│   ├── Finance/
│   │   └── Money.cs              (Trùng với ValueObjects/Finance/Money.cs)
│   └── Map/
│       └── Location.cs           (Trùng với ValueObjects/Map/Location.cs)
│
└── Properties/                    (AssemblyInfo.cs)
```

---

## 3. Các Building Blocks (Chi tiết Implementation)

### 3.1 Entity & Strongly-Typed ID

**Entity Base Class (Generic với TId)**
```csharp
namespace Domain.Primitives
{
    /// <summary>
    /// Base class for entities that are not aggregate roots.
    /// Generic TId cho flexible identifier types (Guid, int, string, hoặc StronglyTypedId).
    /// </summary>
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId>)) return false;
            var other = (Entity<TId>)obj;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
```

**Lưu ý quan trọng:**
- `Entity<TId>` dùng cho **non-root entities** (ví dụ: nếu có Sub-Entity trong Aggregate)
- `AggregateRoot` KHÔNG kế thừa `Entity<TId>` – nó có riêng `Guid Id` và `CreatedAt`, `Version`
- Trong codebase hiện tại, hầu hết Entities (Driver, User, Passenger, Admin) đều kế thừa trực tiếp `AggregateRoot`, không dùng `Entity<TId>`

**Strongly-Typed ID Pattern (Recommended)**
```csharp
namespace Domain.Primitives
{
    /// <summary>
    /// Abstract base for strongly-typed identifiers.
    /// Tạo type-safe wrapper quanh Guid/int để tránh lỗi type confusion.
    /// </summary>
    public abstract class StronglyTypedId<T> : ValueObject where T : IEquatable<T>
    {
        public T Value { get; protected set; }

        protected StronglyTypedId(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "StronglyTypedId value cannot be null");
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is StronglyTypedId<T> other)
                return Value.Equals(other.Value);
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
```

**Concrete Strongly-Typed IDs trong codebase:**
```csharp
// TripId.cs
public sealed class TripId : StronglyTypedId<Guid>
{
    public TripId(Guid value) : base(value) { }
    public static TripId New() => new TripId(Guid.NewGuid());
}

// UserId.cs (dùng cho cả Passenger, Admin)
public sealed class UserId : StronglyTypedId<Guid>
{
    public UserId(Guid value) : base(value) { }
    public static UserId New() => new UserId(Guid.NewGuid());
}

// DriverId.cs (nên có, nhưng có thể đang dùng Guid trực tiếp)
// PassengerId.cs (tương tự)
```

**Cách sử dụng trong Aggregate:**
```csharp
// OPTION 1: Dùng Guid trực tiếp (hiện tại Trip đang dùng)
public class Trip : AggregateRoot
{
    public Guid Id { get; protected set; } // từ AggregateRoot
    // ...
}

// OPTION 2: Dùng StronglyTypedId (recommended cho type safety)
public class Trip : AggregateRoot
{
    public TripId Id { get; protected set; }  // Strongly-typed

    public Trip(TripId tripId, PassengerId passengerId, ...)
    {
        Id = tripId ?? TripId.New();
        // ...
    }
}
```

**Khi nào dùng `Entity<TId>`:** Chỉ khi có Entity con (non-root) bên trong Aggregate, ví dụ:
```csharp
public class OrderLine : Entity<int>  // Child entity với identity trong aggregate
{
    public int LineNumber { get; }
    public ProductId ProductId { get; }
    public int Quantity { get; }
    // Entity con thường không có StronglyTypedId riêng, dùng int hoặc Guid
}
```
Trong codebase hiện tại, `Feedback` (Trip/Entities/Feedback.cs) có thể là Entity con, nhưng file chưa có nội dung.


---

### 3.2 Value Object

**Base Class**
```csharp
namespace Domain.Primitives
{
    /// <summary>
    /// Base class for value objects.
    /// Value objects là bất biến (immutable), không có identity, so sánh theo giá trị.
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (!(obj is ValueObject)) return false;
            var other = (ValueObject)obj;
            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (hash, obj) => 
                    unchecked(hash * 31 + (obj != null ? obj.GetHashCode() : 0)));
        }
    }
}
```

**Ví dụ thực tế: Money (Immutable, toán tử số học)**
```csharp
namespace Domain.ValueObjects  // Hoặc Domain.SharedKernel.Finance
{
    /// <summary>
    /// Value Object representing monetary amount with currency.
    /// Bất biến, value-based equality, hỗ trợ toán tử số học và so sánh.
    /// </summary>
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        // Constructor cho ORM/persistence
        private Money() { }

        public Money(decimal amount, string currency = "VND")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = string.IsNullOrWhiteSpace(currency)
                ? "VND"
                : currency.Trim().ToUpperInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        // Toán tử số học (immutable nên trả về instance mới)
        public static Money operator +(Money a, Money b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            EnsureSameCurrency(a, b);
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            EnsureSameCurrency(a, b);
            decimal result = a.Amount - b.Amount;
            if (result < 0)
                throw new InvalidOperationException("Money cannot be negative");
            return new Money(result, a.Currency);
        }

        // Comparison operators (<, >, <=, >=) với currency check
        public static bool operator <(Money a, Money b) { ... }
        public static bool operator >(Money a, Money b) { ... }
        public static bool operator <=(Money a, Money b) { ... }
        public static bool operator >=(Money a, Money b) { ... }

        private static void EnsureSameCurrency(Money a, Money b)
        {
            if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Currency mismatch");
        }
    }
}
```

**Ví dụ khác: Location (có behavior)**
```csharp
namespace Domain.ValueObjects  // Hoặc Domain.SharedKernel.Map
{
    /// <summary>
    /// Value Object representing a geographic location.
    /// Immutable, value-based equality by coordinates.
    /// </summary>
    public sealed class Location : ValueObject
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public double Lat { get; private set; }
        public double Lng { get; private set; }

        // Private constructor cho ORM
        private Location() { }

        public Location(string name, Address address, double lat, double lng)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên địa điểm không được để trống.");
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException(nameof(lat));
            if (lng < -180 || lng > 180)
                throw new ArgumentOutOfRangeException(nameof(lng));

            Name = name;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Lat = lat;
            Lng = lng;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Lat;
            yield return Lng;
        }

        /// <summary>
        /// Tính khoảng cách geodesic đến another location (Haversine formula)
        /// </summary>
        public double GetDistanceKm(Location other)
        {
            if (other == null) return 0;
            const double EarthRadiusKm = 6371.0;
            // ... Haversine implementation
        }

        public override string ToString() => $"{Name} ({Lat:F5}, {Lng:F5})";
    }
}
```

**Ví dụ khác: Fare (Value Object trong Aggregate)**
```csharp
namespace Domain.Aggregates.Trip.ValueObjects
{
    /// <summary>
    /// Fare value object: tổng tiền + tỉ lệ hoa hồng.
    /// Tính toán được Commission và DriverIncome từ TotalAmount × CommissionRate.
    /// </summary>
    public sealed class Fare : ValueObject
    {
        private readonly Money _totalAmount;
        private readonly decimal _commissionRate;

        public Money TotalAmount => _totalAmount;
        public decimal CommissionRate => _commissionRate;

        // Computed properties
        public Money Commission => new Money(_totalAmount.Amount * _commissionRate);
        public Money DriverIncome => new Money(_totalAmount.Amount * (1 - _commissionRate));

        public Fare(Money totalAmount, decimal commissionRate)
        {
            if (totalAmount.Amount < 0) throw new ArgumentException("Số tiền không hợp lệ.");
            if (commissionRate < 0 || commissionRate > 1) throw new ArgumentException("Tỉ lệ không hợp lệ.");

            _totalAmount = totalAmount;
            _commissionRate = commissionRate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _totalAmount;
            yield return _commissionRate;
        }
    }
}
```

**Value Object Best Practices (từ codebase):**
- `sealed` class (không cho kế thừa)
- Properties `get` only, khởi tạo qua constructor
- Private parameterless constructor dành cho ORM/deserialization
- Validation trong constructor
- Override `Equals`/`GetHashCode` qua `GetEqualityComponents()`
- Có thể có behavior (methods) liên quan đến giá trị (ví dụ: `GetDistanceKm()`, `CalculateCommission()`)

** Ví dụ thực tế: Money (Immutable, toán tử số học)**
```csharp
namespace Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money() { } // For ORM

        public Money(decimal amount, string currency = "VND")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = string.IsNullOrWhiteSpace(currency)
                ? "VND"
                : currency.Trim().ToUpperInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        // Toán tử số học (immutable nên trả về instance mới)
        public static Money operator +(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            decimal result = a.Amount - b.Amount;
            if (result < 0)
                throw new InvalidOperationException("Money cannot be negative");
            return new Money(result, a.Currency);
        }

        // Comparison operators...
}
```

**Ví dụ khác: Location (có behavior)**
```csharp
public sealed class Location : ValueObject
{
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public double Lat { get; private set; }
    public double Lng { get; private set; }

    public double GetDistanceKm(Location other)
    {
        // Haversine formula để tính khoảng cách địa lý
        const double EarthRadiusKm = 6371.0;
        // ... implementation
    }
}
```

---

### 3.3 Aggregate Root

**Base Class**
```csharp
namespace Domain.Primitives
{
    using System;
    using System.Collections.Generic;
    using Domain.Events;

    /// <summary>
    /// Base class for aggregate roots.
    /// Aggregate Root là entry point duy nhất cho truy cập và mutation của toàn bộ Aggregate.
    /// </summary>
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public int Version { get; set; } = 1;  // Optimistic concurrency control

        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(DomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected AggregateRoot() : this(Guid.NewGuid(), DateTime.UtcNow) { }

        protected AggregateRoot(Guid id) : this(id, DateTime.UtcNow) { }

        protected AggregateRoot(Guid id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }
    }
}
```

**Lưu ý quan trọng:**
- `AggregateRoot` KHÔNG kế thừa từ `Entity<TId>` – nó là concept riêng với `Guid Id` hardcoded
- `Version` property dùng cho optimistic concurrency (kiểm soát đồng thời)
- `DomainEvents` là collection in-memory, cần được clear sau khi published
- Constructor mặc định tạo `Guid.NewGuid()` và `DateTime.UtcNow`

**Ví dụ thực tế: Trip Aggregate**
```csharp
namespace Domain.Aggregates.Trip
{
    using Domain.StateMachines;
    using Domain.ValueObjects;
    using Domain.Exceptions;
    using Domain.Aggregates.Trip.Events;

    public class Trip : AggregateRoot
    {
        #region Fields (private, readonly nếu có thể)
        private readonly VehicleType _vehicleType;  // Immutable sau khi tạo
        private readonly Location _pickup;          // Immutable
        private readonly Location _destination;     // Immutable
        private TripStatus _status;                 // Mutable qua business methods
        private Guid _passengerId;                  // Immutable sau khi tạo
        #endregion

        #region Properties (public read-only, private set cho mutation)
        public VehicleType VehicleType => _vehicleType;
        public Location Pickup => _pickup;
        public Location Destination => _destination;

        public TripStatus Status
        {
            get => _status;
            private set => _status = value;  // Chỉ Aggregate Root được set
        }

        public Guid PassengerId
        {
            get => _passengerId;
            private set
            {
                if (value == Guid.Empty)
                    throw new BusinessRuleViolationException(
                        nameof(PassengerId), "PassengerId không hợp lệ.");
                _passengerId = value;
            }
        }

        public Guid? DriverId { get; private set; }
        public double? Distance { get; private set; }
        public double? Duration { get; private set; }
        public Money Fare { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public decimal? PassengerRating { get; private set; }
        public string PassengerComment { get; private set; }
        public decimal? DriverRating { get; private set; }
        public int Version { get; set; } = 1;  // từ AggregateRoot, dùng cho optimistic concurrency
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor cho business logic (tạo mới Trip).
        /// Auto-generate Id và CreatedAt.
        /// </summary>
        public Trip(Guid passengerId, VehicleType vehicleType, 
                     Location pickup, Location destination) : base()
        {
            _vehicleType = vehicleType;
            _pickup = pickup ?? throw new ArgumentNullException(nameof(pickup));
            _destination = destination ?? throw new ArgumentNullException(nameof(destination));
            PassengerId = passengerId;
            Status = TripStatus.Requested;
            
            // Emit domain event ngay khi tạo
            AddDomainEvent(new TripRequestedEvent(Id, passengerId, pickup, destination, vehicleType));
        }

        /// <summary>
        /// Constructor cho persistence (ORM/JSON).
        /// Private parameterless để ORM có thể instantiate, sau đó set properties.
        /// </summary>
        private Trip() { }  // EF Core / JSON deserialization
        #endregion

        #region Business Methods (State transitions + invariants)
        /// <summary>
        /// Chuyển trạng thái được validate bởi State Machine.
        /// </summary>
        public void SetStatus(TripStatus newStatus)
        {
            if (!TripStateMachine.CanTransition(_status, newStatus))
            {
                throw new InvalidStatusTransitionException(
                    _status.ToString(),
                    newStatus.ToString(),
                    "Quy tắc nghiệp vụ: Chuyển đổi trạng thái không hợp lệ.");
            }
            _status = newStatus;
        }

        public void SetSearching()
        {
            SetStatus(TripStatus.Searching);
            AddDomainEvent(new TripSearchingEvent(Id));
        }

        /// <summary>
        /// Gán driver cho trip. Không cho phép gán nếu đã có driver.
        /// </summary>
        public void MatchDriver(Guid driverId)
        {
            if (DriverId.HasValue)
                throw new BusinessRuleViolationException(
                    nameof(DriverId), "Trip đã được gán driver.");

            DriverId = driverId;
            SetStatus(TripStatus.Matched);
            AddDomainEvent(new TripMatchedEvent(Id, driverId, _vehicleType));
        }

        public void MarkAsArrived()
        {
            if (!DriverId.HasValue)
                throw new BusinessRuleViolationException(
                    nameof(DriverId), "Trip chưa được gán driver.");

            SetStatus(TripStatus.Arrived);
            AddDomainEvent(new TripArrivedEvent(Id));
        }

        public void StartTrip()
        {
            if (!DriverId.HasValue)
                throw new BusinessRuleViolationException(
                    nameof(DriverId), "Trip chưa được gán driver.");

            SetStatus(TripStatus.Started);
            StartedAt = DateTime.UtcNow;
            AddDomainEvent(new TripStartedEvent(Id));
        }

        /// <summary>
        /// Hoàn thành trip với distance, duration, fare.
        /// Chỉ cho phép khi Status = Started.
        /// </summary>
        public void CompleteTrip(double distance, double duration, Money fare)
        {
            if (Status != TripStatus.Started)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Trip phải đang trong trạng thái Started.");

            if (!DriverId.HasValue)
                throw new BusinessRuleViolationException(
                    nameof(DriverId), "Trip phải được gán driver trước khi hoàn thành.");

            Distance = distance;
            Duration = duration;
            Fare = fare;
            EndedAt = DateTime.UtcNow;
            SetStatus(TripStatus.Completed);
            AddDomainEvent(new TripCompletedEvent(Id, distance, duration, fare, DriverId.Value));
        }

        public void Cancel(string reason)
        {
            if (Status == TripStatus.Completed)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Không thể hủy trip đã hoàn thành.");

            if (Status == TripStatus.Cancelled)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Trip đã bị hủy.");

            SetStatus(TripStatus.Cancelled);
            AddDomainEvent(new TripCancelledEvent(Id, reason));
        }

        public void MarkTimeout()
        {
            if (Status != TripStatus.Searching)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Chỉ có thể timeout khi đang tìm tài xế.");

            SetStatus(TripStatus.Timeout);
            AddDomainEvent(new TripTimeoutEvent(Id));
        }
        #endregion

        #region Rating
        public void RateByPassenger(decimal rating, string comment)
        {
            if (Status != TripStatus.Completed)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Chỉ được đánh giá sau khi hoàn thành chuyến.");

            PassengerRating = rating;
            PassengerComment = comment ?? string.Empty;
        }

        public void RateByDriver(decimal rating)
        {
            if (Status != TripStatus.Completed)
                throw new BusinessRuleViolationException(
                    nameof(Status), "Chỉ được đánh giá sau khi hoàn thành chuyến.");

            DriverRating = rating;
        }
        #endregion

        #region Query Methods
        public bool CanBeCancelled()
        {
            return TripStateMachine.CanBeCancelled(Status);
        }
        #endregion
    }
}
```

**Aggregate Design Rules (từ codebase):**
1. **Invariants**: Tất cả business rules được enforce trong Aggregate Root methods – không bao giờ allow object vào invalid state
2. **Encapsulation**: 
   - Private fields (immutable nếu có thể)
   - Public read-only properties
   - Mutations chỉ qua public methods (no public setters cho business properties)
3. **Domain Events**: Gọi `AddDomainEvent()` sau mỗi state change quan trọng
4. **Concurrency**: `Version` property dùng cho optimistic concurrency control. Repository cung cấp `UpdateWhereVersionMatches()` để atomic update với version check
5. **Collections**: Expose `IReadOnlyCollection<T>` hoặc `IReadOnlyList<T>` (không bao giờ `List<T>` trực tiếp)
6. **Constructors**:
   - Public constructor cho business use (validation, domain events)
   - Private parameterless constructor cho ORM (EF Core) / JSON deserialization
   - Optional: Protected constructor với `(Guid id, DateTime createdAt)` cho reconstitution từ persistence

---

### 3.4 Domain Service

**Khi nào dùng Domain Service:** Logic nghiệp vụ phức tạp không thuộc về một Entity hay Value Object cụ thể nào, hoặc cần orchestrate nhiều aggregates.

**Ví dụ thực tế từ codebase:**

**1. FareCalculationService** – tính cước dựa trên route, vehicle type, và optional fare rule
```csharp
namespace Domain.Services
{
    using Domain.Aggregates.Driver.ValueObjects.Vehicle;  // Vehicle
    using Domain.Aggregates.Trip.ValueObjects;          // Route, Fare
    using Domain.Policies;                              // FareRule
    using Domain.ValueObjects;                          // Money

    public class FareCalculationService
    {
        // Overload 1: Dùng route + vehicle
        public Money CalculateFare(Route route, Vehicle vehicle)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

            decimal baseFare = vehicle.Type == VehicleType.Car ? 15000m : 10000m;
            decimal pricePerKm = vehicle.Type == VehicleType.Car ? 10000m : 5000m;
            decimal amount = baseFare + ((decimal)Math.Max(0, route.Distance) * pricePerKm);
            return new Money(amount, "VND");
        }

        // Overload 2: Dùng distance/duration trực tiếp
        public Money CalculateFare(double distanceKm, int durationMinutes, Vehicle vehicle)
        {
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

            decimal baseFare = vehicle.Type == VehicleType.Car ? 15000m : 10000m;
            decimal pricePerKm = vehicle.Type == VehicleType.Car ? 10000m : 5000m;
            decimal amount = baseFare + ((decimal)Math.Max(0, distanceKm) * pricePerKm);
            return new Money(amount, "VND");
        }

        // Overload 3: Dùng FareRule (policy)
        public Money CalculateFare(Route route, Vehicle vehicle, FareRule fareRule)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
            if (fareRule == null) throw new ArgumentNullException(nameof(fareRule));

            Money baseDistanceFare = fareRule.CalculateBaseFare(route.Distance);
            decimal vehicleMultiplier = GetVehicleMultiplier(vehicle);
            decimal finalAmount = baseDistanceFare.Amount * vehicleMultiplier;
            return new Money(finalAmount, baseDistanceFare.Currency);
        }

        private decimal GetVehicleMultiplier(Vehicle vehicle)
        {
            return vehicle.Type switch
            {
                VehicleType.Car => vehicle.Capacity <= 4 ? 1.2m :
                                   vehicle.Capacity <= 7 ? 1.5m : 2.0m,
                VehicleType.Motorbike => 1.0m,
                _ => 1.0m
            };
        }
    }
}
```

**2. PaymentCalculationService** – tính tiền cho driver và platform
```csharp
public class PaymentCalculationService
{
    public Money CalculateDriverPayment(Money fare, decimal commissionRate)
    {
        if (fare == null) throw new ArgumentNullException(nameof(fare));
        if (commissionRate < 0 || commissionRate > 1) 
            throw new ArgumentOutOfRangeException(nameof(commissionRate));

        decimal commission = fare.Amount * commissionRate;
        return new Money(fare.Amount - commission, fare.Currency);
    }

    public Money CalculatePlatformFee(Money fare, decimal commissionRate)
    {
        if (fare == null) throw new ArgumentNullException(nameof(fare));
        if (commissionRate < 0 || commissionRate > 1) 
            throw new ArgumentOutOfRangeException(nameof(commissionRate));

        decimal commission = fare.Amount * commissionRate;
        return new Money(commission, fare.Currency);
    }
}
```

**3. RouteValidationService** – validate Route
```csharp
public class RouteValidationService
{
    public bool IsValid(Route route)
    {
        if (route == null) throw new ArgumentNullException(nameof(route));

        if (route.Distance <= 0) return false;
        if (route.Duration.TotalMinutes <= 0) return false;
        if (route.Distance > 100) return false;           // Max 100km
        if (route.Duration.TotalMinutes > 240) return false; // Max 4 hours

        return true;
    }
}
```

**4. DriverTripCompatibilityService** – kiểm tra driver có ghép được với trip không
```csharp
public class DriverTripCompatibilityService
{
    public bool IsCompatible(Driver driver, Trip trip)
    {
        if (driver == null) throw new ArgumentNullException(nameof(driver));
        if (trip == null) throw new ArgumentNullException(nameof(trip));

        // Check active + available status
        if (!driver.IsActive || driver.Status != DriverStatus.Available)
            return false;

        // Check proximity (within max pickup distance)
        double distanceToPickup = CalculateDistance(driver.Position, trip.Pickup);
        if (distanceToPickup > driver.Vehicle.GetMaxPickupDistance())
            return false;

        // Vehicle type compatibility có thể check thêm
        return true;
    }

    private double CalculateDistance(Location loc1, Location loc2)
    {
        // Simple Euclidean approx (111km per degree)
        double deltaLat = loc2.Lat - loc1.Lat;
        double deltaLon = loc2.Lng - loc1.Lng;
        return Math.Sqrt(deltaLat * deltaLat + deltaLon * deltaLon) * 111;
    }
}
```

**5. Matching Services** (`Domain/Services/Matching/`)

**DriverCandidateSelector** – filter + sort eligible drivers:
```csharp
public class DriverCandidateSelector
{
    private readonly IDriverEligibilityPolicy _driverEligibilityPolicy;

    public DriverCandidateSelector(IDriverEligibilityPolicy driverEligibilityPolicy)
    {
        _driverEligibilityPolicy = driverEligibilityPolicy;
    }

    public IReadOnlyList<Driver> SelectEligibleCandidates(IEnumerable<Driver> drivers, Trip trip)
    {
        if (drivers == null || trip == null) return new List<Driver>();

        List<Driver> eligible = new List<Driver>();
        foreach (Driver driver in drivers)
        {
            if (_driverEligibilityPolicy.CanReceiveTrip(driver) &&
                driver.Vehicle != null && 
                driver.Vehicle.Type == trip.VehicleType)
            {
                eligible.Add(driver);
            }
        }

        // Sort by distance to pickup (bubble sort – có thể thay bằng LINQ OrderBy)
        for (int i = 0; i < eligible.Count - 1; i++)
        {
            for (int j = 0; j < eligible.Count - i - 1; j++)
            {
                double distJ = eligible[j].Position == null ? 
                    double.MaxValue : CalculateDistanceKm(eligible[j].Position, trip.Pickup);
                double distJ1 = eligible[j + 1].Position == null ? 
                    double.MaxValue : CalculateDistanceKm(eligible[j + 1].Position, trip.Pickup);
                if (distJ > distJ1)
                {
                    Driver temp = eligible[j];
                    eligible[j] = eligible[j + 1];
                    eligible[j + 1] = temp;
                }
            }
        }

        return eligible;
    }

    private static double CalculateDistanceKm(Location from, Location to)
    {
        // Haversine
        const double EarthRadiusKm = 6371.0;
        double dLat = DegreesToRadians(to.Lat - from.Lat);
        double dLon = DegreesToRadians(to.Lng - from.Lng);
        double lat1 = DegreesToRadians(from.Lat);
        double lat2 = DegreesToRadians(to.Lat);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                   + Math.Cos(lat1) * Math.Cos(lat2)
                   * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;
}
```

**DispatchArbitrator** – orchestrate assignment:
```csharp
public class DispatchArbitrator
{
    private readonly ITripAssignmentPolicy _tripAssignmentPolicy;

    public DispatchArbitrator(ITripAssignmentPolicy tripAssignmentPolicy)
    {
        _tripAssignmentPolicy = tripAssignmentPolicy;
    }

    /// <summary>
    /// Cố gắng gán driver cho trip.
    /// Policy kiểm tra → match driver → set driver status.
    /// </summary>
    public bool TryAssign(Trip trip, Driver driver)
    {
        if (trip == null || driver == null) return false;

        _tripAssignmentPolicy.EnsureCanAssign(trip, driver);
        
        // Both methods emit domain events
        trip.MatchDriver(driver.Id);
        driver.SetOnTrip();
        
        return true;
    }
}
```

**Chú ý:** Domain Services nên là **stateless** (no instance fields cần persist). Chúng là pure business logic orchestrators.

---

### 3.5 Repository

**Nguyên tắc:**
- Repository **interface** nằm trong Domain layer
- Repository **implementation** nằm trong Infrastructure layer (EF Core, JSON, etc.)
- Domain không phụ thuộc vào bất kỳ infrastructure cụ thể nào
- Repository abstract away persistence details, expose only domain-relevant operations

**Generic Repository Interfaces** (`Domain/Primitives/IRepository.cs`)
```csharp
namespace Domain.Interfaces.Repositories
{
    /// <summary>
    /// Base repository cho CRUD operations.
    /// Generic để tái sử dụng cho mọi Aggregate.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task InitializeAsync();      // Seeding/init nếu cần
        Task SaveChangesAsync();     // Unit of Work style
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }

    public interface IReadRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }

    /// <summary>
    /// Unit of Work: phối hợp nhiều repositories trong một transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
```

**Lưu ý:** Generic `IRepository<T>` có thể không cần thiết nếu mỗi Aggregate có đặc thù riêng. Trong codebase, các repository cụ thể thường kế thừa `IRepository<T>` và bổ sung query methods.

**Specific Repository Interfaces (ví dụ thực tế):**

**ITripRepository** (`Domain/Aggregates/Trip/Repositories/ITripRepository.cs`)
```csharp
public interface ITripRepository
{
    Task InitializeAsync();
    Task SaveChangesAsync();
    
    // Basic CRUD
    Trip GetById(Guid id);
    IEnumerable<Trip> GetAll();
    void Add(Trip trip);
    void Update(Trip trip);
    void Delete(Guid id);

    // Query-specific methods
    IEnumerable<Trip> GetTripsByDriverId(Guid driverId);
    IEnumerable<Trip> GetTripsByPassengerId(Guid passengerId);
    IEnumerable<Trip> GetPendingTrips();  // Trips đang tìm driver, chưa hoàn thành

    // Concurrency control
    bool UpdateWhereVersionMatches(Guid id, int expectedVersion, Action<Trip> mutation);
}
```

**Giải thích `UpdateWhereVersionMatches`:**
```csharp
// Pattern sử dụng:
bool success = tripRepository.UpdateWhereVersionMatches(
    tripId, 
    expectedVersion,  // client gửi version hiện tại
    (trip) => {
        trip.Status = TripStatus.Matched;
        trip.DriverId = driverId;
    });

if (!success)
    throw new ConcurrencyException("Trip đã bị ai đó thay đổi trước đó.");
```

**IDriverRepository** (`Domain/Aggregates/Driver/IDriverRepository.cs`)
```csharp
public interface IDriverRepository
{
    Task InitializeAsync();
    Task SaveChangesAsync();
    Driver GetById(Guid id);
    IEnumerable<Driver> GetAll();
    Driver GetByPhone(string phone);           // Lookup by phone
    IEnumerable<Driver> GetAvailableDrivers(); // Drivers Available + Active
    void Add(Driver driver);
    void Update(Driver driver);
    void Delete(Guid id);
    Task<bool> ExistsByPhone(string phone);
}
```

**IUserRepository** (`Domain/Aggregates/User/IUserRepository.cs`)
```csharp
public interface IUserRepository
{
    Task InitializeAsync();
    Task SaveChangesAsync();
    User GetById(Guid id);
    IEnumerable<User> GetAll();
    User GetByPhone(string phone);
    void Add(User user);
    void Update(User user);
    void Delete(Guid id);
    Task<bool> ExistsByPhone(string phone);
}
```

**IVehicleRepository** (`Domain/Aggregates/Driver/ValueObjects/Vehicle/Repositories/IVehicleRepository.cs`)
```csharp
// Hiện tại là stub rỗng – cần implement
public interface IVehicleRepository
{
    // Có thể kế thừa IRepository<Vehicle> nếu Vehicle là aggregate root riêng
    //Nhưng hiện tại Vehicle là part của Driver, nên có thể dùng trực tiếp qua DriverRepository
}
```

**Repository Implementation Notes:**
- Implementation trong **Infrastructure** layer (ví dụ: `Infrastructure/Repositories/TripRepository.cs`)
- Có thể dùng Entity Framework, Dapper, hoặc JSON file storage
- `InitializeAsync()` thường dùng để seed data hoặc tạo connection
- `SaveChangesAsync()` có thể được gọi từ Unit of Work hoặc repository riêng lẻ
- **Không bao giờ** expose `IQueryable<T>` hoặc `DbSet<T>` ra ngoài Domain (tránh leak infrastructure)
- Query methods nên trả về `IEnumerable<T>` hoặc DTOs, không nên trả về domain entities nếu query cross-aggregate

**Example Infrastructure Implementation (conceptual):**
```csharp
// Infrastructure/Repositories/TripRepository.cs
public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    }

    public Trip GetById(Guid id)
    {
        return _context.Trips
            .Include(t => t.Driver)   // Eager load nếu cần
            .FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Trip> GetTripsByPassengerId(Guid passengerId)
    {
        return _context.Trips
            .Where(t => t.PassengerId == passengerId)
            .ToList();
    }

    public bool UpdateWhereVersionMatches(Guid id, int expectedVersion, Action<Trip> mutation)
    {
        var trip = _context.Trips.FirstOrDefault(t => t.Id == id && t.Version == expectedVersion);
        if (trip == null) return false;

        mutation(trip);
        trip.Version++;  // Increment version
        _context.Trips.Update(trip);
        return true;
    }

    // ... các methods khác
}
```

### 3.6 Domain Event

**Base Class** (`Domain/Primitives/DomainEvent.cs`)
```csharp
namespace Domain.Events
{
    /// <summary>
    /// Base class for all domain events.
    /// Mỗi event có Id duy nhất và timestamp OccurredOn.
    /// </summary>
    public abstract class DomainEvent : IEquatable<DomainEvent>
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        public bool Equals(DomainEvent other) => other != null && Id.Equals(other.Id);
        public override bool Equals(object obj) => Equals(obj as DomainEvent);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
```



### 3.7 State Machine

**Vì sao cần State Machine:**  
Quản lý lifecycle của Aggregate (Trip, Driver) với các quy tắc chuyển trạng thái phức tạp, tránh "spaghetti code" trong các business methods. Tất cả transitions được định nghĩa tập trung tại một nơi.

**TripStateMachine.cs** (code thực tế):
```csharp
namespace Domain.StateMachines
{
    using Domain.Enums;

    public static class TripStateMachine  // static class – không cần instance
    {
        // Dictionary định nghĩa tất cả valid transitions
        private static readonly Dictionary<TripStatus, HashSet<TripStatus>> ValidTransitions
            = new Dictionary<TripStatus, HashSet<TripStatus>>()
        {
            [TripStatus.Requested] = new HashSet<TripStatus>
            {
                TripStatus.Searching,   // Bắt đầu tìm tài xế
                TripStatus.Cancelled    // Hủy ngay từ đầu
            },

            [TripStatus.Searching] = new HashSet<TripStatus>
            {
                TripStatus.Matched,     // Tìm thấy tài xế
                TripStatus.Cancelled,   // Hủy khi đang tìm
                TripStatus.Timeout      // Hết thời gian tìm
            },

            [TripStatus.Matched] = new HashSet<TripStatus>
            {
                TripStatus.Arrived,     // Tài xế đã đến điểm đón
                TripStatus.Searching,   // Quay lại searching (nếu driver từ chối)
                TripStatus.Cancelled    // Hủy khi đã matched
            },

            [TripStatus.Arrived] = new HashSet<TripStatus>
            {
                TripStatus.Started,     // Hành khách lên xe
                TripStatus.Cancelled    // Hủy khi tài xế đã đến
            },

            [TripStatus.Started] = new HashSet<TripStatus>
            {
                TripStatus.Completed,   // Hoàn thành chuyến
                TripStatus.Cancelled    // Hủy khi đang chạy
            },

            // Terminal states – không có outgoing transitions
            [TripStatus.Completed] = new HashSet<TripStatus>(),
            [TripStatus.Cancelled] = new HashSet<TripStatus>(),
            [TripStatus.Timeout] = new HashSet<TripStatus>()
        };

        /// <summary>
        /// Kiểm tra có thể chuyển từ `from` sang `to` không.
        /// </summary>
        public static bool CanTransition(TripStatus from, TripStatus to)
        {
            return ValidTransitions.TryGetValue(from, out var validTargets) 
                   && validTargets.Contains(to);
        }

        /// <summary>
        /// Kiểm tra trip có thể bị hủy ở trạng thái hiện tại không.
        /// </summary>
        public static bool CanBeCancelled(TripStatus status)
        {
            return status == TripStatus.Requested ||
                   status == TripStatus.Searching ||
                   status == TripStatus.Matched ||
                   status == TripStatus.Arrived;
        }
    }
}
```

**DriverStateMachine.cs** (code thực tế):
```csharp
namespace Domain.StateMachines
{
    using Domain.Enums;

    public static class DriverStateMachine
    {
        private static readonly Dictionary<DriverStatus, HashSet<DriverStatus>> ValidTransitions =
            new Dictionary<DriverStatus, HashSet<DriverStatus>>()
        {
            { DriverStatus.Offline, new HashSet<DriverStatus> { DriverStatus.Available } },
            { DriverStatus.Available, new HashSet<DriverStatus> { DriverStatus.OnTrip, DriverStatus.Offline } },
            { DriverStatus.OnTrip, new HashSet<DriverStatus> { DriverStatus.Available } }
        };

        public static bool CanTransition(DriverStatus from, DriverStatus to)
        {
            return ValidTransitions.TryGetValue(from, out var validTargets)
                   && validTargets.Contains(to);
        }
    }
}
```

**Sử dụng trong Aggregate:**
```csharp
public class Trip : AggregateRoot
{
    public void SetStatus(TripStatus newStatus)
    {
        if (!TripStateMachine.CanTransition(_status, newStatus))
        {
            throw new InvalidStatusTransitionException(
                _status.ToString(),
                newStatus.ToString(),
                "Quy tắc nghiệp vụ: Chuyển đổi trạng thái không hợp lệ.");
        }
        _status = newStatus;
    }

    public void Cancel(string reason)
    {
        if (!TripStateMachine.CanBeCancelled(Status))
        {
            throw new BusinessRuleViolationException(
                nameof(Status), "Trip không thể bị hủy ở trạng thái này.");
        }
        SetStatus(TripStatus.Cancelled);
        AddDomainEvent(new TripCancelledEvent(Id, reason));
    }
}
```

**Benefits:**
- Centralized transition logic → dễ test, dễ thay đổi
- Tránh "if-else spaghetti" trong mỗi business method
- Rõ ràng terminal states (Completed, Cancelled, Timeout)
- Dễ visualize thành state diagram

---

### 3.8 Exception & Business Rules

**DomainException Base** (`Domain/Primitives/DomainException.cs` – inherits từ `Common.Exceptions.BaseException`)
```csharp
namespace Domain.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using Common.Exceptions;

    [Serializable]
    public class DomainException : BaseException
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
```

**BusinessRuleViolationException** – Rule invariant bị vi phạm:
```csharp
[Serializable]
public class BusinessRuleViolationException : DomainException
{
    public string RuleName { get; }

    public BusinessRuleViolationException(string ruleName, string message)
        : base(message)
    {
        RuleName = ruleName ?? throw new ArgumentNullException(nameof(ruleName));
    }

    public BusinessRuleViolationException(string ruleName, string message, Exception inner)
        : base(message, inner)
    {
        RuleName = ruleName;
    }

    protected BusinessRuleViolationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        RuleName = info.GetString("RuleName");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("RuleName", RuleName);
    }
}
```

**InvalidStatusTransitionException** – State machine transition không hợp lệ:
```csharp
[Serializable]
public class InvalidStatusTransitionException : DomainException
{
    public string FromStatus { get; }
    public string ToStatus { get; }

    public InvalidStatusTransitionException(string from, string to, string message)
        : base($"{message} (From: {from}, To: {to})")
    {
        FromStatus = from;
        ToStatus = to;
    }

    public InvalidStatusTransitionException(string message) : base(message) { }

    protected InvalidStatusTransitionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        FromStatus = info.GetString("FromStatus");
        ToStatus = info.GetString("ToStatus");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("FromStatus", FromStatus);
        info.AddValue("ToStatus", ToStatus);
    }
}
```

**DriverUnavailableException** – Driver không khả dụng:
```csharp
[Serializable]
public class DriverUnavailableException : DomainException
{
    public Guid DriverId { get; }

    public DriverUnavailableException() { }
    public DriverUnavailableException(string message) : base(message) { }
    public DriverUnavailableException(Guid driverId, string message) : base(message)
    {
        DriverId = driverId;
    }
    public DriverUnavailableException(string message, Exception inner) : base(message, inner) { }
    protected DriverUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
```

**TripAlreadyAssignedException** – Trip đã có driver:
```csharp
[Serializable]
public class TripAlreadyAssignedException : DomainException
{
    public Guid TripId { get; }

    public TripAlreadyAssignedException() { }
    public TripAlreadyAssignedException(string message) : base(message) { }
    public TripAlreadyAssignedException(Guid tripId, string message) : base(message)
    {
        TripId = tripId;
    }
    protected TripAlreadyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
```

**ConcurrencyException** – Version conflict:
```csharp
[Serializable]
public class ConcurrencyException : DomainException
{
    public ConcurrencyException() { }
    public ConcurrencyException(string message) : base(message) { }
    public ConcurrencyException(string message, Exception inner) : base(message, inner) { }
    protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
```

---

### 3.9 Specification Pattern

**Mục đích:** Encapsulate business predicates thành objects reusable, testable, và combinable (AND/OR/NOT).

**Current State in Codebase:** Chỉ `DriverAvailableSpec` (internal, minimal implementation).

**Example (improved):**
```csharp
namespace Domain.Specifications
{
    using Domain.Aggregates.Driver;
    using Domain.ValueObjects;
    using System;

    /// <summary>
    /// Specification kiểm tra driver có sẵn sàng nhận trip không.
    /// </summary>
    internal class DriverAvailableSpec : ISpecification<Driver>
    {
        private readonly Location _pickupLocation;
        private readonly double _maxDistanceKm;

        public DriverAvailableSpec(Location pickupLocation, double maxDistanceKm = 5.0)
        {
            _pickupLocation = pickupLocation;
            _maxDistanceKm = maxDistanceKm;
        }

        public bool IsSatisfiedBy(Driver driver)
        {
            if (driver == null) return false;
            if (!driver.IsActive) return false;
            if (driver.Status != DriverStatus.Available) return false;

            double distance = driver.Position.GetDistanceKm(_pickupLocation);
            return distance <= _maxDistanceKm;
        }
    }

    // Composite Specification (AND)
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
    }
}
```

---

### 3.10 Policy Pattern (Strategy)

**Mục đích:** Định nghĩa family of algorithms có thể thay thế nhau (ví dụ: fare calculation, driver eligibility, trip assignment).

#### Policy 1: FareRule (`Domain/Policies/FareRule.cs`)
```csharp
namespace Domain.Policies
{
    using Domain.Aggregates.Trip.ValueObjects;
    using Domain.Enums;
    using Domain.ValueObjects;

    /// <summary>
    /// Policy cho việc tính cước phí.
    /// Lưu trữ: BaseFare, PricePerKm, CommissionRate.
    /// Có thể có nhiều implementations khác nhau (SurgePricingFareRule, TimeBasedFareRule, etc.)
    /// </summary>
    public class FareRule
    {
        public Guid Id { get; }
        public VehicleType VehicleType { get; }
        public Money BaseFare { get; private set; }
        public Money PricePerKm { get; private set; }
        public decimal CommissionRate { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected FareRule() { } // For ORM

        public FareRule(VehicleType type, Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            Id = Guid.NewGuid();
            VehicleType = type;
            UpdateRule(baseFare, pricePerKm, commissionRate);
        }

        public void UpdateRule(Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            if (commissionRate < 0 || commissionRate > 1)
                throw new ArgumentException("CommissionRate không hợp lệ.");

            BaseFare = baseFare;
            PricePerKm = pricePerKm;
            CommissionRate = commissionRate;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Tính cước cơ bản từ distance (chưa bao gồm multiplier).
        /// </summary>
        public Money CalculateBaseFare(double distanceKm)
        {
            decimal total = BaseFare.Amount + ((decimal)distanceKm * PricePerKm.Amount);
            return new Money(total, BaseFare.Currency);
        }

        /// <summary>
        /// Tạo Fare object hoàn chỉnh với commission rate.
        /// </summary>
        public Fare CreateFare(double distanceKm)
        {
            Money baseFare = CalculateBaseFare(distanceKm);
            return new Fare(baseFare, this.CommissionRate);
        }

        public decimal CalculateCommission(decimal tripFare)
        {
            return tripFare * CommissionRate;
        }
    }
}
```

#### Policy 2: DriverEligibilityPolicy (`Domain/Policies/IDriverEligibilityPolicy.cs`)
```csharp
public interface IDriverEligibilityPolicy
{
    /// <summary>
    /// Kiểm tra driver có đủ điều kiện nhận trip không.
    /// </summary>
    bool CanReceiveTrip(Driver driver);
}

public class DriverEligibilityPolicy : IDriverEligibilityPolicy
{
    public bool CanReceiveTrip(Driver driver)
    {
        return driver != null
               && driver.IsActive
               && driver.Status == DriverStatus.Available;
    }
}
```

#### Policy 3: TripAssignmentPolicy (`Domain/Policies/ITripAssignmentPolicy.cs`)
```csharp
public interface ITripAssignmentPolicy
{
    /// <summary>
    /// Validate xem có thể gán driver cho trip không.
    /// Throw exception nếu không hợp lệ.
    /// </summary>
    void EnsureCanAssign(Trip trip, Driver driver);
}

public class TripAssignmentPolicy : ITripAssignmentPolicy
{
    private readonly IDriverEligibilityPolicy _driverEligibilityPolicy;

    public TripAssignmentPolicy(IDriverEligibilityPolicy driverEligibilityPolicy)
    {
        _driverEligibilityPolicy = driverEligibilityPolicy;
    }

    public void EnsureCanAssign(Trip trip, Driver driver)
    {
        if (trip == null)
            throw new BusinessRuleViolationException(nameof(trip), "Trip không hợp lệ.");
        if (driver == null)
            throw new BusinessRuleViolationException(nameof(driver), "Driver không hợp lệ.");
        if (trip.DriverId.HasValue)
            throw new TripAlreadyAssignedException(trip.Id, "Chuyến đi đã có tài xế.");
        if (!_driverEligibilityPolicy.CanReceiveTrip(driver))
            throw new DriverUnavailableException(driver.Id, "Tài xế không khả dụng.");
        if (trip.Status != TripStatus.Searching && trip.Status != TripStatus.Requested)
            throw new InvalidStatusTransitionException(
                trip.Status.ToString(), TripStatus.Matched.ToString(),
                "Chỉ có thể gán tài xế cho chuyến đang Requested/Searching.");
    }
}
```

---

### 3.11 Domain Event Details

**Domain Events in Codebase** (`Aggregates/*/Events/`):

**Trip Events (9 events):**
| Event | Data | Purpose |
|-------|------|---------|
| `TripRequestedEvent` | TripId, PassengerId, Pickup, Destination, VehicleType | Trip mới tạo |
| `TripSearchingEvent` | TripId, AttemptNumber | Bắt đầu tìm tài xế |
| `TripMatchedEvent` | TripId, DriverId, VehicleType | Tìm thấy tài xế |
| `TripArrivedEvent` | TripId | Tài xế đến điểm đón |
| `TripStartedEvent` | TripId | Hành khách lên xe |
| `TripCompletedEvent` | TripId, Distance, Duration, Fare, DriverId | Hoàn thành |
| `TripCancelledEvent` | TripId, Reason | Trip bị hủy |
| `TripTimeoutEvent` | TripId | Hết thời gian tìm |

**Driver Events (2 events):**
| Event | Data | Purpose |
|-------|------|---------|
| `DriverStatusChangedEvent` | DriverId, OldStatus, NewStatus | Thay đổi trạng thái |
| `DriverLocationUpdatedEvent` | DriverId, Location | Cập nhật vị trí |

**Event Properties (Common):**
```csharp
public abstract class DomainEvent
{
    public Guid Id { get; }              // Unique event ID
    public DateTime OccurredOn { get; }  // UTC timestamp
}
```

**Publishing Pattern (Application Layer):**
```csharp
public async Task<TripId> Handle(RequestTripCommand command)
{
    var trip = new Trip(...);
    _tripRepository.Add(trip);
    await _tripRepository.SaveChangesAsync();

    // Publish events AFTER transaction commit
    foreach (var @event in trip.DomainEvents)
    {
        await _eventBus.PublishAsync(@event);
    }
    trip.ClearDomainEvents();

    return new TripId(trip.Id);
}
```

**Domain Event vs Integration Event:**
- **Domain Event**: Xảy ra bên trong Domain, phản ánh state change (TripCompletedEvent)
- **Integration Event**: Dùng cho communication giữa Bounded Contexts/microservices (có thể có schema khác, serializable format)
- **Mapping**: Application layer có thể map Domain Event → Integration Event trước khi publish qua message bus

---

## 4. Rich Domain Model vs Anemic Domain Model

### Rich Domain Model (Đúng cách - theo code thực tế)
```csharp
public class Trip : AggregateRoot
{
    // Data + Behavior together – invariants được bảo vệ bên trong aggregate
    public void MatchDriver(Guid driverId)
    {
        if (DriverId.HasValue) 
            throw new BusinessRuleViolationException(nameof(DriverId), "Trip đã có driver.");

        DriverId = driverId;
        SetStatus(TripStatus.Matched);
        AddDomainEvent(new TripMatchedEvent(Id, driverId));
    }

    public void CompleteTrip(double distance, double duration, Money fare)
    {
        // Tất cả logic nghiệp vụ nằm trong aggregate
        // Invariants được guarantee
        if (Status != TripStatus.Started)
            throw new BusinessRuleViolationException(...);

        Fare = fare;
        SetStatus(TripStatus.Completed);
        AddDomainEvent(new TripCompletedEvent(Id, distance, duration, fare, DriverId.Value));
    }
}
```

### Anemic Domain Model (Sai – anti-pattern)
```csharp
// ANTI-PATTERN: Chỉ có properties, không có behavior
public class Trip
{
    public Guid Id { get; set; }
    public TripStatus Status { get; set; }
    public Guid? DriverId { get; set; }
    // No methods – đây là data bag, không phải domain object
}

// Logic nằm ở Service ngoài – KHÔNG đảm bảo invariants
public class TripService
{
    public void MatchDriver(Trip trip, Guid driverId)
    {
        // Logic spread ra ngoài,Trip có thể bị set vào invalid state
        trip.DriverId = driverId;
        trip.Status = TripStatus.Matched;
    }
}
```

**Anti-pattern consequences:**
- Không đảm bảo business invariants
- Logic bị scatter khắp nhiều services
- Khó test, khó maintain
- Domain model becomes just DTOs

---

## 5. CQRS (Command Query Responsibility Segregation)

**Commands (Write Operations):** Đi qua Aggregates, đảm bảo business rules.
```csharp
// Command
public record RequestTripCommand(
    Guid PassengerId,
    VehicleType VehicleType,
    Location Pickup,
    Location Destination) : ICommand;

// Handler (Application layer)
public class RequestTripHandler : ICommandHandler<RequestTripCommand, TripId>
{
    private readonly ITripRepository _tripRepository;

    public async Task<TripId> Handle(RequestTripCommand command)
    {
        var trip = new Trip(
            command.PassengerId,
            command.VehicleType,
            command.Pickup,
            command.Destination);

        _tripRepository.Add(trip);
        await _tripRepository.SaveChangesAsync();

        // Events are published after commit
        foreach (var @event in trip.DomainEvents)
            await _eventBus.PublishAsync(@event);
        trip.ClearDomainEvents();

        return new TripId(trip.Id);
    }
}
```

**Queries (Read Operations):** Có thể dùng Read Model riêng (denormalized view).
```csharp
// Query
public record GetTripStatusQuery(Guid TripId) : IQuery<TripStatusDto>;

// Read Model DTO (không phải domain entity)
public class TripStatusDto
{
    public Guid TripId { get; set; }
    public string Status { get; set; }
    public string DriverName { get; set; }
    public string VehiclePlate { get; set; }
    public double? EtaMinutes { get; set; }
}

// Query Handler
public class GetTripStatusHandler : IQueryHandler<GetTripStatusQuery, TripStatusDto>
{
    private readonly IReadRepository<Trip> _tripReadRepository;

    public async Task<TripStatusDto> Handle(GetTripStatusQuery query)
    {
        var trip = await _tripReadRepository.GetById(query.TripId);
        return new TripStatusDto
        {
            TripId = trip.Id,
            Status = trip.Status.ToString(),
            // Có thể join data từ Driver, Vehicle nếu cần
        };
    }
}
```

**Lưu ý:** CQRS không phải mandatory. Có thể dùng chung read/write model nếu domain đơn giản. Apply CQRS khi có nhu cầu:
- Read performance khác write
- Complex queries không phù hợp với domain model
- Cần audit trail đầy đủ

---

## 6. Event Sourcing (Optional – Advanced)

**Khuyến nghị:** Chỉ áp dụng cho Aggregate quan trọng (Payment, Trip) nếu cần full audit trail hoặc rebuild state từ events.

**Event-sourced Aggregate skeleton:**
```csharp
public class Trip : AggregateRoot
{
    private readonly List<DomainEvent> _uncommittedChanges = new();

    public void Apply(TripCreatedEvent @event)
    {
        // Rebuild state từ event
        Id = @event.TripId;
        PassengerId = @event.PassengerId;
        _pickup = @event.Pickup;
        _destination = @event.Destination;
        Status = TripStatus.Requested;
    }

    public void MatchDriver(Guid driverId)
    {
        // Business validation...
        var @event = new TripMatchedEvent(Id, driverId);
        Apply(@event);                // Update state
        _uncommittedChanges.Add(@event); // Queue event
    }

    // Replay all events để reconstruct state
    public static Trip Replay(IEnumerable<DomainEvent> events)
    {
        var trip = new Trip();
        foreach (var @event in events)
        {
            trip.Apply(@event);
        }
        return trip;
    }
}
```

**Trade-offs:**
- ✅ Full audit trail, debug-ability
- ✅ Temporal queries (state tại thời điểm nào)
- ❌ Complexity cao (snapshotting, event versioning)
- ❌ Learning curve, tooling support

---

## 7. Testing Domain Logic

**Unit Test (không cần database):**
```csharp
using NUnit.Framework;

[TestFixture]
public class TripTests
{
    private Trip _trip;
    private Location _pickup;
    private Location _destination;
    private VehicleType _vehicleType;

    [SetUp]
    public void SetUp()
    {
        _pickup = new Location("A", new Address("...", "...", "...", "..."), 10.0, 106.0);
        _destination = new Location("B", new Address("...", "...", "...", "..."), 10.1, 106.1);
        _vehicleType = VehicleType.Car;
    }

    [Test]
    public void CompleteTrip_ShouldThrow_WhenStatusNotStarted()
    {
        // Arrange: Trip mới tạo có status = Requested
        var trip = new Trip(Guid.NewGuid(), _vehicleType, _pickup, _destination);

        // Act & Assert
        var ex = Assert.Throws<BusinessRuleViolationException>(
            () => trip.CompleteTrip(5.0, 10.0, new Money(50000, "VND")));

        Assert.That(ex.RuleName, Is.EqualTo(nameof(Trip.Status)));
    }

    [Test]
    public void MatchDriver_ShouldAddDomainEvent_WhenSuccessful()
    {
        var trip = new Trip(Guid.NewGuid(), _vehicleType, _pickup, _destination);
        var driverId = Guid.NewGuid();

        trip.MatchDriver(driverId);

        Assert.AreEqual(driverId, trip.DriverId);
        Assert.AreEqual(TripStatus.Matched, trip.Status);
        Assert.AreEqual(1, trip.DomainEvents.Count);
        Assert.IsInstanceOf<TripMatchedEvent>(trip.DomainEvents[0]);
    }

    [Test]
    public void Cancel_ShouldThrow_WhenStatusCompleted()
    {
        var trip = new Trip(Guid.NewGuid(), _vehicleType, _pickup, _destination);
        // Simulate completed trip...
        trip.CompleteTrip(5.0, 10.0, new Money(50000, "VND"));

        var ex = Assert.Throws<BusinessRuleViolationException>(
            () => trip.Cancel("Change mind"));

        StringAssert.Contains("không thể hủy", ex.Message);
    }
}
```

**Testing State Machine:**
```csharp
[TestFixture]
public class TripStateMachineTests
{
    [TestCase(TripStatus.Requested, TripStatus.Searching, true)]
    [TestCase(TripStatus.Requested, TripStatus.Cancelled, true)]
    [TestCase(TripStatus.Requested, TripStatus.Matched, false)]
    [TestCase(TripStatus.Completed, TripStatus.Cancelled, false)]
    public void CanTransition_ShouldReturnExpected(TripStatus from, TripStatus to, bool expected)
    {
        bool result = TripStateMachine.CanTransition(from, to);
        Assert.AreEqual(expected, result);
    }
}
```

**Testing Domain Services:**
```csharp
[Test]
public void FareCalculationService_ShouldCalculateCorrectFare_ForCar()
{
    var service = new FareCalculationService();
    var route = new Route(pickup, destination, 10.0, TimeSpan.FromMinutes(20), points);
    var vehicle = new Car(...);

    Money fare = service.CalculateFare(route, vehicle);

    // Expected: baseFare 15000 + 10km × 10000 = 115000 VND
    Assert.AreEqual(115000m, fare.Amount);
    Assert.AreEqual("VND", fare.Currency);
}
```

---

## 8. Common Patterns in Codebase

### 8.1 Versioning & Optimistic Concurrency
```csharp
public class Trip : AggregateRoot
{
    public int Version { get; set; } = 1;
}

// Repository method: atomic update với version check
bool UpdateWhereVersionMatches(Guid id, int expectedVersion, Action<Trip> mutation)
{
    var trip = GetById(id);
    if (trip.Version != expectedVersion)
        return false;  // Concurrency conflict

    mutation(trip);
    trip.Version++;  // Increment
    Update(trip);
    SaveChangesAsync();
    return true;
}

// Usage:
bool updated = repo.UpdateWhereVersionMatches(
    tripId, 
    clientVersion, 
    t => t.Status = TripStatus.Matched);
if (!updated) throw new ConcurrencyException();
```

### 8.2 Strongly-Typed IDs
```csharp
// Thay vì Guid trực tiếp, dùng wrapper:
public sealed class TripId : StronglyTypedId<Guid>
{
    public TripId(Guid value) : base(value) { }
    public static TripId New() => new TripId(Guid.NewGuid());
}

// Benefits:
// - Compile-time type safety (không nhầm lẫn TripId vs DriverId)
// - Self-documenting API
// - Có override Equals/GetHashCode
```

### 8.3 Private Constructors cho ORM
```csharp
public sealed class Money : ValueObject
{
    private Money() { } // EF Core / JSON binder

    public Money(decimal amount, string currency) { ... }
}

public class Trip : AggregateRoot
{
    private Trip() { } // EF Core
    public Trip(...) { ... } // Business constructor
}
```

### 8.4 Read-Only Collections
```csharp
private readonly List<OrderLine> _lines = new();
public IReadOnlyList<OrderLine> Lines => _lines.AsReadOnly();

// Không expose List<T> – ngăn external code modify internal state
```

---

## 9. Best Practices from Codebase

### 9.1 Immutability
- Value Objects: `sealed`, getter-only properties, constructor validation
- Entity IDs: `protected set`, chỉ Aggregate Root được thay đổi
- Collections: Expose `IReadOnlyCollection<T>`

### 9.2 Validation
- Constructor: validate ngay khi create
- Public methods: validate trước mutation
- Throw `BusinessRuleViolationException` với `RuleName` để logging/monitoring

### 9.3 Domain Events
- Emit sau mỗi state change quan trọng
- Đặt trong `Aggregates/{Name}/Events/`
- Publishing do Application layer (sau transaction commit)

### 9.4 State Machines
- Tách file riêng: `TripStateMachine.cs`, `DriverStateMachine.cs`
- Static `CanTransition()`, `CanBeCancelled()`
- Dictionary-driven transitions

### 9.5 Repository Pattern
- Interface trong Domain, implementation trong Infrastructure
- Specific query methods trong repository interface (không dùng IQueryable)
- `UpdateWhereVersionMatches()` cho optimistic concurrency

### 9.6 Layering
```
Presentation (UI) 
    → Application (UseCases, DTOs, Interfaces)
    → Domain (Entities, VO, Services, Policies, Repository Interfaces)
    → Infrastructure (Persistence, External Services)
```

**Dependencies:**
- Domain không phụ thuộc vào bất kỳ layer nào khác
- Domain reference: Domain.Primitives, Domain.ValueObjects, Common (utilities)
- Infrastructure implements Domain interfaces

---

## 10. Anti-Patterns Cần Tránh

❌ **Anemic Domain Model** – Entities chỉ có properties, không behavior  
❌ **God Object** – Aggregate quá lớn, nhiều responsibilities  
❌ **Leaky Abstractions** – Expose `DbContext`, `DbSet`, `IQueryable` ra ngoài Domain  
❌ **Anemic Repository** – Repository chỉ là CRUD wrapper, không query methods cụ thể  
❌ **Domain Events trong Infrastructure** – Domain Events phải thuộc Domain layer  
❌ **Public setters** – State nên được thay đổi qua business methods

---

## 11. .NET Framework 4.8 Considerations

**Project file:**
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <Nullable>enable</Nullable>  <!-- Có thể bật nullable reference types -->
  </PropertyGroup>
  <!-- References, imports... -->
</Project>
```

**Serialization Support:**
- Domain Events và Exceptions đã có `[Serializable]` và constructor `SerializationInfo`
- Dùng cho distributed caching, session state, WCF (nếu cần)

**Entity Framework Compatibility:**
- EF 6.x compatible với .NET 4.8
- Parameterless constructors cho EF
- Navigation properties có thể là virtual nếu cần lazy loading (không khuyến nghị trong DDD)

---

## 12. Implementation Checklist

When implementing a new Aggregate:

- [ ] **Aggregate Root** extends `AggregateRoot`, has Guid `Id`, `CreatedAt`, `Version`
- [ ] **Value Objects** are `sealed`, immutable, extend `ValueObject`, override `GetEqualityComponents()`
- [ ] **Entity IDs** dùng `protected set`, không public set
- [ ] **Fields** là `private readonly` nếu có thể
- [ ] **Properties** expose `IReadOnlyCollection<T>` hoặc getter-only
- [ ] **Business methods** enforce invariants, throw exceptions với `RuleName`
- [ ] **Domain Events** emitted sau mỗi state change quan trọng
- [ ] **State Machine** nếu lifecycle phức tạp (tách file riêng)
- [ ] **Repository interface** nằm trong Domain, implementation trong Infrastructure
- [ ] **Specification/Policy** interfaces nếu cần strategy/swap
- [ ] **Unit tests** cho business logic (no DB)
- [ ] **Integration tests** cho repository + ORM mapping
---

**Tài liệu này phản ánh cấu trúc Domain layer thực tế của dự án (ride-hailing system).**  
Các khái niệm, patterns, và code snippets đều được trích xuất từ codebase hiện tại.