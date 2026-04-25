# Clean Architecture — Final Reference (DDD + CQRS)

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
| Presentation | WinForms UI, DI root | Application, Common | Infrastructure trực tiếp *(nên tránh)* |

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
> **DI Root** tại `Program.cs` (Presentation) — khởi tạo toàn bộ service graph bằng `Microsoft.Extensions.DependencyInjection`.

**Lưu ý so với Web API pattern:**
- Không có HTTP middleware — thay bằng WinForms event pipeline.
- Không dùng MediatR — handler gọi Application Service trực tiếp.
- Không dùng EF Core — thay bằng `JsonRepository<T>` + `FileStorage`.

---

## Common

> Chỉ chứa primitive types và pure functions. Không có business logic. Không có infra logic. Không dependency vào layer nào.

```
Common/
├── Primitives/
│   ├── Result/
│   │   ├── Result.cs          # Wrapper success/failure, thay thế exception flow
│   │   └── ResultT.cs
│   ├── Error/
│   │   ├── Error.cs           # Mô tả lỗi chuẩn hóa (code, message, metadata)
│   │   └── ErrorType.cs
│   └── Guard/
│       └── Guard.cs           # Validate input nhanh (fail-fast), không throw exception phức tạp
│
├── Exceptions/
│   └── AppException.cs        # Base exception toàn hệ thống — không chứa business rule
│
├── Extensions/                # Pure only — không side-effect
│   ├── StringExtensions.cs
│   ├── EnumerableExtensions.cs
│   └── DateTimeExtensions.cs
│
├── Utilities/                 # Pure only
│   ├── MathUtils.cs
│   └── IdGenerator.cs
│
└── Attributes/                # Cross-cutting metadata annotation only
    ├── AuditableAttribute.cs
    └── IgnoreAuditAttribute.cs
```

---

## Domain

> Business rules. Không biết Infrastructure tồn tại. Không reference EF Core, HTTP, hay bất kỳ thư viện infra nào.

```
Domain/
├── Primitives/                    # Base types — dùng chung trong toàn Domain
│   ├── AggregateRoot.cs
│   ├── AggregateRootT.cs
│   ├── Entity.cs
│   ├── EntityT.cs
│   ├── ValueObject.cs
│   ├── DomainEvent.cs
│   └── StronglyTypedId.cs         # Strongly-typed ID, tránh primitive obsession
│
├── {Aggregate1}/                  # Mỗi Aggregate là một thư mục riêng
│   ├── Aggregate1Root.cs          # Aggregate Root — bảo vệ business rules
│   ├── Aggregate1ChildEntity.cs   # Child Entity bên trong Aggregate
│   ├── Aggregate1Id.cs            # Strongly-typed ID
│   ├── Aggregate1Status.cs        # Enum gắn với Aggregate này
│   ├── Events/
│   │   ├── Aggregate1CreatedEvent.cs
│   │   └── Aggregate1DeletedEvent.cs
│   └── Specifications/            # Dùng khi query logic / business rule phức tạp, tái sử dụng được
│       └── ActiveAggregate1Spec.cs
│
├── {Aggregate2}/
│   ├── Aggregate2Root.cs
│   ├── Aggregate2Id.cs
│   └── Events/
│       └── Aggregate2CreatedEvent.cs
│
├── SharedKernel/                  # Value Objects dùng chung nhiều Aggregate
│   ├── Money.cs
│   └── Address.cs
│
├── Interfaces/
│   └── Repositories/              # Chỉ khai báo — implementation ở Infrastructure
│       ├── IRepository.cs
│       ├── IReadRepository.cs
│       ├── IUnitOfWork.cs
│       ├── IAggregate1Repository.cs
│       └── IAggregate2Repository.cs
│
├── Services/                      # (Optional) Chỉ dùng khi business logic không thuộc Entity/Aggregate nào
│   └── IDomainService1.cs
│
├── Factories/                     # (Optional) Chỉ dùng khi tạo Aggregate/Entity phức tạp
│   └── Aggregate1Factory.cs
│
└── Exceptions/
    └── DomainException.cs         # Base exception cho lỗi business — extend từ AppException
```

**Quy tắc Specification:** Dùng khi query condition phức tạp, tái sử dụng ở nhiều nơi, hoặc cần combine nhiều điều kiện. Không dùng Specification cho query đơn giản — over-engineering.

**Quy tắc Domain Service:** Chỉ tạo khi logic cần phối hợp nhiều Aggregate mà không thể đặt trong bất kỳ Aggregate Root nào. Nếu logic chỉ liên quan một Aggregate, đặt trong Aggregate Root đó.

---

## Application

> Use cases. Orchestration. Không chứa business rule — đó là việc của Domain.

### Khi nào dùng CQRS

| Dùng CQRS khi                        | Không cần CQRS khi                        |
|--------------------------------------|--------------------------------------------|
| Read/write có logic khác nhau rõ rệt | CRUD đơn giản, không có business rule phức tạp |
| Write cần transaction, Read cần projection riêng | Handler chỉ gọi repo rồi return, không có logic |
| Cần audit trail rõ ràng cho write    | Team nhỏ, feature ít                       |

> **Over-CQRS warning:** Nếu mỗi feature đều có Command + Query + Handler + Validator mà handler chỉ làm `repo.GetById(id)` rồi map → DTO thì đang làm phức tạp vô nghĩa. CQRS là pattern giải quyết vấn đề, không phải nghi lễ bắt buộc.

```
Application/
├── Features/                          # Vertical Slice — theo từng Aggregate
│   ├── {Aggregate1}/
│   │   ├── Commands/
│   │   │   ├── Create{Aggregate1}/
│   │   │   │   ├── Create{Aggregate1}Command.cs
│   │   │   │   ├── Create{Aggregate1}CommandHandler.cs
│   │   │   │   ├── Create{Aggregate1}CommandValidator.cs
│   │   │   │   └── Create{Aggregate1}Response.cs
│   │   │   ├── Update{Aggregate1}/
│   │   │   │   ├── Update{Aggregate1}Command.cs
│   │   │   │   ├── Update{Aggregate1}CommandHandler.cs
│   │   │   │   └── Update{Aggregate1}CommandValidator.cs
│   │   │   └── Delete{Aggregate1}/
│   │   │       ├── Delete{Aggregate1}Command.cs
│   │   │       └── Delete{Aggregate1}CommandHandler.cs
│   │   ├── Queries/
│   │   │   ├── Get{Aggregate1}ById/
│   │   │   │   ├── Get{Aggregate1}ByIdQuery.cs
│   │   │   │   ├── Get{Aggregate1}ByIdQueryHandler.cs
│   │   │   │   └── {Aggregate1}DetailDto.cs
│   │   │   └── Get{Aggregate1}List/
│   │   │       ├── Get{Aggregate1}ListQuery.cs
│   │   │       ├── Get{Aggregate1}ListQueryHandler.cs
│   │   │       └── {Aggregate1}SummaryDto.cs
│   │   └── EventHandlers/
│   │       └── {Aggregate1}CreatedEventHandler.cs
│   │
│   └── {Aggregate2}/
│       ├── Commands/
│       └── Queries/
│
├── DTOs/                              # DTO dùng chung nhiều Features — không đặt trong Feature folder
│
├── Interfaces/                        # Interface cho External Services
│   ├── IEmailService.cs               # Implementation ở Infrastructure
│   ├── IFileStorageService.cs
│   ├── ICacheService.cs
│   └── ICurrentUserService.cs
│
├── Behaviors/                         # MediatR Pipeline — Application pipeline (không phải HTTP)
│   ├── ValidationBehavior.cs          # Tự động chạy validator trước Handler
│   ├── LoggingBehavior.cs
│   ├── TransactionBehavior.cs
│   └── PerformanceBehavior.cs
│
├── Mappings/
│   └── MappingProfile.cs              # Domain ↔ DTO mapping
│
├── Exceptions/
│   └── ApplicationException.cs        # Use-case specific exceptions
│
└── DependencyInjection.cs
```

---

## Infrastructure

> Implementation của tất cả interfaces từ Domain và Application. Layer duy nhất biết về EF Core, HttpClient, Redis, v.v.

```
Infrastructure/
├── Persistence/
│   ├── Write/                         # Command side
│   │   ├── AppDbContext.cs
│   │   ├── Configurations/            # Fluent API — EF Core entity config
│   │   │   ├── {Aggregate1}Configuration.cs
│   │   │   └── {Aggregate2}Configuration.cs
│   │   ├── Repositories/              # Implementation của IRepository từ Domain
│   │   │   ├── BaseRepository.cs
│   │   │   ├── {Aggregate1}Repository.cs
│   │   │   └── {Aggregate2}Repository.cs
│   │   ├── Interceptors/
│   │   │   ├── DomainEventInterceptor.cs   # Publish domain events sau SaveChanges
│   │   │   ├── AuditInterceptor.cs
│   │   │   └── SoftDeleteInterceptor.cs
│   │   └── Migrations/
│   │
│   └── Read/                          # Query side — tối ưu cho read, không cần tracking
│       └── ReadDbContext.cs
│
├── ExternalServices/
│   ├── Email/
│   │   ├── EmailService.cs            # Implement IEmailService từ Application
│   │   └── EmailOptions.cs
│   ├── Payment/
│   ├── Notification/
│   └── ApiClients/
│       ├── ExternalApiClient.cs
│       └── ExternalApiOptions.cs
│
├── Identity/
│   ├── TokenService.cs
│   ├── PasswordHasher.cs
│   ├── CurrentUserService.cs          # Implement ICurrentUserService từ Application
│   └── IdentityOptions.cs
│
├── Caching/
│   ├── CacheService.cs                # Implement ICacheService từ Application
│   └── CacheOptions.cs
│
├── Logging/
│   └── LoggingConfiguration.cs        # Serilog enrichers, sinks
│
├── Messaging/
│   ├── Outbox/
│   │   ├── OutboxMessage.cs
│   │   ├── OutboxConfiguration.cs
│   │   └── ProcessOutboxJob.cs        # Chạy định kỳ để publish events chưa gửi
│   └── Bus/
│       ├── IMessageBus.cs
│       └── MessageBusService.cs
│
├── Observability/
│   ├── DiagnosticsConfiguration.cs
│   └── HealthChecks/
│       ├── DatabaseHealthCheck.cs
│       └── ExternalServiceHealthCheck.cs
│
├── BackgroundJobs/
│   └── RecurringJobSetup.cs
│
└── DependencyInjection.cs             # Đăng ký tất cả services — entry point duy nhất
```

**Domain Events flow:** `Aggregate Root raises event` → `SaveChanges` → `DomainEventInterceptor` publish → `EventHandler` trong Application xử lý side effects.

---

## Presentation

> I/O layer. Nhận request, gọi Application, trả response. Không chứa business logic.

```
Presentation/
├── Endpoints/                         # Minimal API — .NET 8+
│   ├── {Aggregate1}Endpoints.cs
│   └── {Aggregate2}Endpoints.cs
│
│   # Hoặc dùng Controllers nếu project cần MVC / Razor:
│   # Controllers/
│   # Views/
│   # wwwroot/
│
├── Middleware/                        # HTTP pipeline — khác với Behavior (Application pipeline)
│   ├── ExceptionMiddleware.cs         # Global exception handling → chuẩn hóa error response
│   └── CorrelationIdMiddleware.cs
│
├── Filters/
│   └── ValidationFilter.cs
│
├── Contracts/                         # API contract — tách khỏi Application DTO
│   ├── Requests/
│   │   ├── Create{Aggregate1}Request.cs
│   │   ├── Update{Aggregate1}Request.cs
│   │   └── Create{Aggregate2}Request.cs
│   └── Responses/
│       ├── ApiResponse.cs
│       ├── PagedResponse.cs
│       └── ErrorResponse.cs
│
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   └── WebApplicationExtensions.cs
│
├── Program.cs
└── appsettings.json
```

> **Tại sao Contracts tách khỏi Application DTOs?** Application DTOs phục vụ internal use case logic. API Contracts phục vụ HTTP contract với client bên ngoài — versioning, serialization attributes, OpenAPI annotations. Hai thứ thay đổi vì lý do khác nhau.

---

## Tests

```
tests/
├── Domain.UnitTests/
│   └── {Aggregate1}/
│       └── {Aggregate1}RootTests.cs       # Test business rules trong Aggregate
│
├── Application.UnitTests/
│   └── Features/
│       └── {Aggregate1}/
│           └── Create{Aggregate1}CommandHandlerTests.cs
│
├── Application.IntegrationTests/
│   ├── Fixtures/
│   │   └── DatabaseFixture.cs             # Test DB setup (Testcontainers)
│   └── Features/
│       └── {Aggregate1}/
│           └── {Aggregate1}EndpointTests.cs
│
└── Architecture.Tests/
    ├── DependencyRuleTests.cs             # Tự động kiểm tra dependency rule (NetArchTest)
    ├── NamingConventionTests.cs
    └── LayerAccessTests.cs
```

> **Architecture.Tests** là lớp bảo vệ quan trọng nhất — tự động fail nếu ai đó vô tình reference Infrastructure từ Domain, hoặc đặt business logic vào Presentation.

---

## Naming Conventions

```
// Commands — động từ + danh từ
PlaceOrderCommand           ✅
PlaceOrderRequest           ❌  (Request = từ của HTTP layer)
CreateOrderDto              ❌  (đây là Command, không phải DTO)

// Queries
GetOrderByIdQuery           ✅
FetchOrderQuery             ❌  (dùng Get, không dùng Fetch)

// Handlers
PlaceOrderCommandHandler    ✅
PlaceOrderHandler           ✅  (acceptable)
OrderCommandHandler         ❌  (quá chung, không rõ)

// Domain Events
OrderPlacedDomainEvent      ✅
OrderPlacedEvent            ✅  (acceptable)
OnOrderPlaced               ❌  (On = naming của JS/event handler)

// Responses / DTOs
OrderDetailResponse         ✅  (return từ Query)
OrderSummaryDto             ✅  (DTO dùng nhiều nơi)
OrderViewModel              ❌  (ViewModel = term của MVC)

// Repository
IOrderRepository            ✅
OrderRepo                   ❌  (abbreviation)
IOrderStore                 ❌  (không nhất quán với convention)
```

---

## Checklist trước khi tạo file mới

| Câu hỏi                                              | Nếu Có → Đặt ở                         |
|------------------------------------------------------|-----------------------------------------|
| Có phải business rule không?                         | Domain / Aggregate Root                 |
| Có phải business logic không thuộc Aggregate nào?   | Domain Service                          |
| Có phải use case / flow điều phối?                  | Application / Handler                   |
| Có phải validate input của use case?                 | Application / Validator                 |
| Có phải cross-cutting cho Application pipeline?      | Application / Behavior                  |
| Có phải cross-cutting cho HTTP pipeline?             | Presentation / Middleware               |
| Có phải implementation của interface?               | Infrastructure                          |
| Có phải API contract với client bên ngoài?          | Presentation / Contracts                |
| Có phải primitive type / pure function?             | Common                                  |
