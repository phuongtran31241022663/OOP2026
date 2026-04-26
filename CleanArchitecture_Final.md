# Clean Architecture — Final Reference (DDD Lite)

> **Áp dụng cho:** RideGo — .NET Framework 4.8 WinForms
> **Ràng buộc:** Không DI container, không LINQ, không `var`, không Lambda, không C# 8.0+

---

## Dependency Rule

```
Outer → Inner (outer layers depend on inner layers)

Presentation → Application → Infrastructure → Domain → Common
```

> **RideGo project references (actual `.csproj`):**
> - `Presentation` → Application, Common, Domain, Infrastructure *(vi phạm — xem note bên dưới)*
> - `Application` → Common, Domain
> - `Infrastructure` → Application, Common, Domain
> - `Domain` → Common
> - `Common` → *(none)*

| Layer | Vai trò | Biết về | Không biết về |
|---|---|---|---|
| Common | Primitive types, pure functions, constants | Chính nó | Tất cả layer khác |
| Domain | Business rules, core logic | Common | Infrastructure, Application, Presentation |
| Application | Use cases, orchestration | Common, Domain | Infrastructure, Presentation |
| Infrastructure | Implementation, I/O thực tế | Common, Domain, Application | Presentation |
| Presentation | WinForms UI, composition root | Application, Common | Infrastructure trực tiếp *(nên tránh)* |

> **Vi phạm hiện tại:** Presentation references Domain và Infrastructure trực tiếp. Đúng Clean Architecture, Presentation chỉ nên biết Application (và Common cho shared types). Cần refactor dần.

---

## Pipeline (RideGo WinForms)

```
User Action (Button Click / Timer Tick)
  → WinForms Event Handler (Presentation)
    → Application Service Interface (ITripService, IUserService...)
      → Application Service Implementation
        → Domain Entity / State Machine
          → Repository Interface (Domain)
            → JsonRepository<T> (Infrastructure)
              → FileStorage → data/*.json
```

> **Observer Pattern** thay thế middleware: `TripService.TripStatusChanged` event được các Form subscribe để cập nhật UI real-time mà không polling.
>
> **Composition Root** tại `Program.cs` (Presentation) — khởi tạo toàn bộ service graph bằng `new` trực tiếp (manual composition, không dùng DI container).

**Lưu ý so với Web API pattern:**
- Không có HTTP middleware — thay bằng WinForms event pipeline.
- Không dùng MediatR — UI event handler gọi Application Service trực tiếp.
- Không dùng EF Core — thay bằng `JsonRepository<T>` + `FileStorage`.

---

## Common

> Chỉ chứa primitive types và pure functions. Không có business logic. Không có infra logic. Không dependency vào layer nào.

```
Common/
├── Constants/                 # Application-wide constants
│   ├── FareConstants.cs
│   └── SimulationConstants.cs
├── Extensions/                # Pure only — không side-effect
│   ├── StringExtensions.cs
│   └── DecimalExtensions.cs
├── Helpers/                   # Utility helper classes
│   └── PasswordHasher.cs
└── Utilities/                 # Duplicate location (cần consolidate)
    └── PasswordHasher.cs
```

**Thực tế codebase:**
- Không có `Result<T>`, `Error`, `Guard` — project sử dụng exception-based flow truyền thống.
- Không có `AppException` base class — dùng `System.Exception` trực tiếp.
- `PasswordHasher` tồn tại ở cả `Helpers/` và `Utilities/` (duplicate).

---

## Domain

> Business rules. Không biết Infrastructure tồn tại. Không reference EF Core, HTTP, hay bất kỳ thư viện infra nào.

```
Domain/
├── Enums/                     # DriverStatus, TripStatus, VehicleType
├── Entities/                  # Core business entities
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
├── Events/                    # Domain events
│   ├── DriverLocationUpdatedEvent.cs
│   ├── DriverStatusChangedEvent.cs
│   ├── ReviewCreatedEvent.cs
│   └── Trip*.cs (9 trip events)
├── SharedKernel/              # Entity.cs, ValueObject.cs, DomainEvent.cs
├── StateMachines/             # DriverStateMachine.cs
├── States/                    # ITripState + 8 state implementations
│   ├── ITripState.cs
│   ├── RequestedState.cs
│   ├── SearchingState.cs
│   ├── MatchedState.cs
│   ├── ArrivedState.cs
│   ├── StartedState.cs
│   ├── CompletedState.cs
│   ├── CancelledState.cs
│   └── TimeoutState.cs
├── Repositories/              # Repository interfaces
│   ├── IRepository.cs
│   ├── ITripRepository.cs
│   ├── IDriverRepository.cs
│   └── ...
└── ValueObjects/              # Immutable value objects
    ├── Address.cs
    ├── Coordinate.cs
    ├── Fare.cs
    ├── Location.cs
    ├── Money.cs
    └── Route.cs
```

**Thực tế codebase:**
- Không có `AggregateRoot<T>`, `Entity<T>` (chỉ có `Entity` non-generic), `StronglyTypedId`.
- Không có `Specifications/`, `Factories/`.
- `Trip` dùng State Pattern (`ITripState`) thay vì State Machine static class.
- `Driver` dùng `DriverStateMachine` (static dictionary) để validate transitions.
- `IUnitOfWork` không tồn tại — mỗi repository tự gọi `SaveChangesAsync`.

---

## Application

> Use cases. Orchestration. Không chứa business rule — đó là việc của Domain.

```
Application/
├── Interfaces/                # Service interfaces
│   ├── IAdminService.cs
│   ├── IDriverService.cs
│   ├── IFareService.cs
│   ├── IMapService.cs
│   ├── IMatchingService.cs
│   ├── IPassengerService.cs
│   ├── IReviewService.cs
│   ├── ISimulationService.cs
│   ├── ITripService.cs
│   ├── IUserQueryService.cs
│   └── IUserService.cs
├── Services/                  # Service implementations
│   ├── AdminService.cs
│   ├── DriverService.cs
│   ├── FareService.cs
│   ├── MapService.cs (wrapper, infra implementation)
│   ├── MatchingService.cs
│   ├── PassengerService.cs
│   ├── ReviewService.cs
│   ├── SimulationService.cs
│   ├── TripService.cs
│   └── UserService.cs
└── bin/, obj/, Properties/    # Build output
```

**Thực tế codebase:**
- Không có `Features/`, `Commands/`, `Queries/`, `Handlers/` — không dùng CQRS/MediatR.
- Không có `DTOs/` folder — domain entities được truyền trực tiếp giữa Application và Presentation.
- Không có `Behaviors/` (MediatR pipeline) — validation nằm trong service methods hoặc UI.
- Không có `Mappings/MappingProfile.cs` (AutoMapper) — mapping thủ công nếu cần.
- Không có `DependencyInjection.cs`.

**Lưu ý về async:**
- Một số methods trong `TripService` mix sync và async (dùng `.Result`) — cần refactor để tránh deadlock trong WinForms message loop.

---

## Infrastructure

> Implementation của tất cả interfaces từ Domain và Application. Layer duy nhất biết về file I/O, HTTP, external libraries.

```
Infrastructure/
├── ExternalServices/          # API clients
│   ├── GMapService.cs
│   └── MapApiService.cs
├── Interfaces/                # Infrastructure-specific interfaces
│   ├── IFileStorageService.cs
│   └── IFareRuleRepository.cs
├── Repositories/              # Concrete implementations
│   ├── DriverRepository.cs
│   ├── FareRuleRepository.cs
│   ├── FileStorage.cs
│   ├── JsonRepository.cs
│   ├── JsonStorage.cs
│   ├── PassengerRepository.cs
│   ├── ReviewRepository.cs
│   ├── TripRepository.cs
│   ├── UserRepository.cs
│   └── VehicleRepository.cs
└── bin/, obj/, Properties/    # Build output
```

**Thực tế codebase:**
- Không có EF Core (`AppDbContext`, `Migrations`, `Interceptors`, `ReadDbContext`).
- Không có `Persistence/Write/Read` separation.
- Không có `Identity/`, `Caching/`, `Logging/Serilog`, `Messaging/Outbox/Bus`, `Observability/`, `HealthChecks/`.
- Không có `DependencyInjection.cs`.
- `FileStorage` dùng `ReaderWriterLockSlim` để đảm bảo thread-safe JSON I/O.
- `JsonStorage` dùng `Newtonsoft.Json` với `TypeNameHandling.All` để preserve polymorphism (`List<User>` chứa `Driver` + `Passenger`).

**Domain Events flow (thực tế):**
- `Aggregate Root raises event` → Application Service lưu aggregate → Events dispatch sau save (hoặc inline trong service).
- Không có `DomainEventInterceptor` — events được xử lý thủ công hoặc qua `TripService.TripStatusChanged`.

**Background Workers:**
- `TripTimeoutWorker` — poll trips `Searching` quá hạn, chuyển sang `Timeout`.
- `TripMatchingWorker` — poll trips `Searching`, gọi `MatchingService`.
- Cả hai được khởi tạo bằng `new` trong `Program.cs` (không dùng hosted service/background job framework).

---

## Presentation

> I/O layer. Nhận user action, gọi Application, update UI. Không chứa business logic.

```
Presentation/
├── Components/                # Reusable UI controls
│   ├── DriverCardControl.cs
│   ├── FarePanel.cs
│   ├── LocationCard.cs
│   ├── LocationPickerControl.cs
│   ├── MapControl.cs
│   ├── StatusPanel.cs
│   ├── TripCard.cs
│   └── TripStatusPanel.cs
├── Helpers/                   # UI-specific helpers
│   ├── AlertHelper.cs
│   ├── DataMapper.cs
│   ├── EventHelper.cs
│   ├── MapHelper.cs
│   └── UIHelper.cs
├── Screens/                   # Forms theo chức năng
│   ├── Auth/
│   ├── Passenger/
│   ├── Driver/
│   └── Admin/
├── Shells/                    # Container forms
├── ViewModels/                # UI state (không dùng data binding phức tạp)
├── BaseForm.cs                # Base form class
├── BaseShell.cs               # Main container
├── BaseUserControl.cs         # Base UC
└── Program.cs                 # Composition root — new all services manually
```

**Thực tế codebase:**
- Không có `Endpoints/`, `Middleware/`, `Filters/`, `Contracts/` — đây là WinForms, không phải Web API.
- Không có `appsettings.json`.
- `Program.cs` khởi tạo toàn bộ services bằng `new` trực tiếp (manual composition):

```csharp
// Ví dụ manual composition trong Program.cs
JsonStorage<User> userStorage = new JsonStorage<User>("data/users.json");
JsonStorage<Trip> tripStorage = new JsonStorage<Trip>("data/trips.json");

UserRepository userRepo = new UserRepository(userStorage);
TripRepository tripRepo = new TripRepository(tripStorage);

TripService tripService = new TripService(tripRepo, userRepo, ...);
UserService userService = new UserService(userRepo, ...);

// Truyền services vào Forms qua constructor
MainShell shell = new MainShell(tripService, userService, ...);
Application.Run(shell);
```

---

## Tests

> Hiện tại chưa có test project trong codebase.

```
tests/ (đề xuất)
├── Domain.UnitTests/
│   └── TripStateTests.cs          # Test state transitions
├── Application.UnitTests/
│   └── MatchingServiceTests.cs    # Test matching logic với mock repo
└── Presentation.UnitTests/
    └── (rarely needed for WinForms)
```

**Lưu ý:**
- Không dùng `NetArchTest` — project không cần architecture test automation ở quy mô đồ án.
- Không dùng `Testcontainers` — không có Docker, không cần integration test với DB.

---

## Naming Conventions

```
// Service interfaces — bắt đầu bằng I
ITripService            ✅
TripServiceInterface    ❌

// Service implementations — không prefix I
TripService             ✅
TripServiceImpl         ❌

// Domain Events — quá khứ, rõ ràng
TripMatchedEvent        ✅
TripMatched             ✅ (acceptable)
OnTripMatched           ❌ (On = JS/event handler style)

// Repository interfaces — bắt đầu bằng I
ITripRepository         ✅
TripRepo                ❌ (abbreviation)

// Enums — PascalCase
TripStatus              ✅
tripStatus              ❌

// Value Objects — immutable, sealed
Money                   ✅
MoneyVO                 ❌ (không cần suffix VO)

// Methods trong Services — Verb + Noun
RequestTrip()           ✅
TripRequest()           ❌ (danh từ trước)
```

---

## Checklist trước khi tạo file mới

| Câu hỏi                                              | Nếu Có → Đặt ở                         |
|------------------------------------------------------|-----------------------------------------|
| Có phải business rule không?                         | Domain / Entity hoặc State Machine      |
| Có phải business logic không thuộc Entity nào?       | Domain Service (static class nếu đơn giản) |
| Có phải use case / flow điều phối?                   | Application / Service                   |
| Có phải validate input của use case?                 | Application / Service method            |
| Có phải cross-cutting cho UI?                        | Presentation / Helper                   |
| Có phải implementation của interface?                | Infrastructure / Repository hoặc ExternalService |
| Có phải primitive type / pure function?              | Common / Constants, Extensions, Helpers |

---

## Ràng buộc Kỹ thuật (Từ Limit.md)

| Cấm | Giải pháp thay thế |
|-----|-------------------|
| Dependency Injection container | Manual `new` + constructor pass |
| LINQ (`Where`, `Select`, ...) | `foreach` + `if-else` |
| `var` | Khai báo kiểu tường minh (`Trip trip = new Trip(...)`) |
| Lambda (`x => x.Id`) | Phương thức tường minh hoặc vòng lặp |
| C# 8.0+ (`record`, `init`, `with`, `??=`, `switch expression`, `^1`, `..`, `static local functions`, default interface methods) | Dùng `class`, constructor, `if-else` truyền thống |
| C# 9.0+ (`target-typed new`, pattern `and/or/not`, top-level statements) | `new TypeName(...)`, `if` lồng nhau, `class Program` |
| C# 10.0+ (`global using`, file-scoped namespaces `namespace X;`) | `using` statement cục bộ, block namespace `namespace X { }` |
| C# 11.0+ (`required`, raw string literals, list patterns) | Constructor parameters, regular string, `if` chain |
| C# 12.0+ (primary constructors, collection expressions `[1,2,3]`) | Constructor thường, `new List<T> { 1, 2, 3 }` |

---

*Document version: 2.0 — Updated cho RideGo .NET Framework 4.8 WinForms. Loại bỏ tất cả references đến DI container, EF Core, MediatR, AutoMapper, Web API patterns, C# 8+ syntax.*

