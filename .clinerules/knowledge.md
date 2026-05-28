# Knowledge - RideGo Technical Patterns

## Constraints
- .NET Framework 4.8, WinForms.
- Use C# syntax compatible with the project compiler settings; avoid modern-only features unless the project file explicitly supports them.
- No LINQ, lambda-heavy pipelines, `var`, or DI container.
- No nullable reference types, records, init-only setters, `with`, target-typed `new`, global using, or file-scoped namespace.
- Do not block the UI thread with `.Result` or `.Wait()` for async work.

## Serialization
- Persistence uses JSON with Newtonsoft.Json.
- Domain entities and value objects use `ISerializable` with:
  - `[Serializable]`
  - `GetObjectData(SerializationInfo info, StreamingContext context)`
  - protected deserialization constructor where needed.
- Polymorphic persistence uses `TypeNameHandling.Auto`.
- `SafeSerializationBinder` must whitelist allowed domain types.
- Relations should prefer IDs over deep object graphs where practical.
- Keep entity invariants intact during deserialization; do not bypass validation unless restoring trusted persisted state.

## State Pattern
- Trip states are implemented under `Domain/States/Trips`.
- Driver states are implemented under `Domain/States/Drivers`.
- Trip lifecycle: Pending -> Searching -> Matched -> Arrived -> Started -> Completed.
- Alternative terminal states: Cancelled, TimeOut.
- Driver lifecycle: Offline <-> Available -> OnTrip -> Available.
- Transitions must go through state objects instead of scattered `if`/`switch` logic.

## Chain of Responsibility - Matching
- Matching validation lives in `Domain/Services/Matching`.
- Default chain: TripStatusHandler -> DriverStatusHandler -> VehicleTypeHandler -> WalletBalanceHandler.
- `MatchingContext` carries Trip, Driver, Vehicle, and `FailureReason`.
- Each handler validates one rule and passes to the next handler through `BaseMatchingHandler`.
- Add new matching rules by creating a new handler and updating `MatchingChainBuilder`.

## Domain Events
- Domain/application side effects are coordinated through event classes and handlers.
- `TripCompletedEvent` carries TripId, DriverId, and PassengerId.
- `DriverWalletHandler` updates driver wallet/commission settlement after trip completion.
- `PassengerStatsHandler` updates passenger statistics after trip completion.
- Keep event handlers focused and idempotent where possible.
- Do not put cross-aggregate side effects directly inside entity methods.

## Observer Pattern
- Runtime status notifications use C# events with `EventHandler<TEventArgs>`.
- Trip status changes use `TripStatusChangedEventArgs`.
- Driver status changes use `DriverStatusChangedEventArgs`.
- Services may bubble domain events to Presentation.
- WinForms subscribers must use `Control.Invoke`/`BeginInvoke` when updating UI from non-UI threads.

## Business Rules
- Active Trip: Status in {Pending, Searching, Matched, Arrived, Started}.
- Matching requires:
  - Trip status allows matching.
  - Driver is Available.
  - Driver vehicle type matches requested vehicle type.
  - Driver wallet >= required commission.
- Settlement: If PayCommission fails, throw InvalidOperationException.
- Payment: CompleteTrip and ConfirmPayment are separate steps.
- Cancellation is allowed before Started state.

## Routing and Fare
- Routing integrates with external map/routing services through Application services.
- OSRM route calls should use timeout and retry policies where implemented.
- Route cache should be bounded to avoid unbounded storage growth.
- Fare calculation belongs in Application service logic and uses Policy/Vehicle data.

## Validation
- Value Object: constructor validation, throw `ArgumentException`.
- Entity: behavior/method validation, throw `InvalidOperationException` for invalid state.
- Application: cross-aggregate existence and workflow validation.
- UI: format feedback and presentation-only validation; do not duplicate business rules.

## Security
- Password handling must not expose raw password outside intended authentication/update flow.
- TypeNameHandling requires a whitelist binder; never deserialize arbitrary type names without `SafeSerializationBinder`.
- Validate all user-provided strings before storing.

## Change Discipline (Quality Gate)
- **Làm ít nhưng làm chắc, làm đúng.** Mọi thao tác trên codebase (tạo mới/xóa bỏ/sửa đổi/đọc) phải đi kèm kiểm tra & đánh giá kỹ lưỡng.
- Trước khi quyết định giữ lại/loại bỏ/chỉnh sửa: cân nhắc mức độ cần thiết, đúng vấn đề, và có làm code tốt hơn không.
- Tránh thay đổi hời hợt; tránh thêm code không cần thiết.
- Mỗi dòng code thêm/sửa phải có giá trị rõ ràng và đóng góp vào sự hoàn chỉnh của hệ thống.
- Tham khảo: `docs/RULES_WORKFLOW_NOTES.md` để biết checklist trước khi thay đổi.
