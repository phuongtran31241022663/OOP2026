# DEVLOG.md – Nhật ký phát triển — RideGo

## 1. Problems (level: hệ thống)
> **Cách ghi:** [Ngữ cảnh] → vấn đề → nguyên nhân → hướng nghĩ

| # | Ngày | Ngữ cảnh | Vấn đề | Nguyên nhân | Hướng nghĩ |
|---|------|----------|--------|-------------|------------|
| P01 | 2026-04-24 | Matching | Race condition: hai trip Searching có thể cùng nhận một driver | `MatchDriverAsync` không có lock — không có `SemaphoreSlim` | Thêm `SemaphoreSlim(1,1)` bọc quanh block check status + assign trong `MatchingService` |
| P02 | 2026-04-24 | Matching | Thuật toán lọc driver chưa đầy đủ | Chỉ lọc `Status == Available` + `VehicleType` match; chưa lọc: địa chỉ hành chính (phường→quận→thành phố), khoảng cách < `MaxPickupDistance`, số dư `Wallet` đủ | Thêm 3 điều kiện lọc tường minh bằng foreach + if (không dùng LINQ) |
| P03 | 2026-04-24 | Architecture | Presentation phụ thuộc trực tiếp vào Domain và Infrastructure | Vi phạm Clean Architecture — Presentation nên chỉ phụ thuộc Application | Về lâu dài: tách DTO layer, chỉ expose Application interfaces ra Presentation. Trước mắt: ghi nhận, không block MVP |
| P04 | 2026-04-24 | Async | `TripService` mix sync và async (`.Result` usage) trong một số call chain | `RequestTrip()` là sync nhưng gọi async internals — `.Result` có thể gây deadlock trong WinForms message loop | Tách hẳn sync và async path; hoặc dùng `Task.Run()` bọc ở Presentation, không gọi `.Result` trực tiếp |
| P05 | 2026-04-24 | Map | Polyline decoding chưa hoàn chỉnh — route không hiển thị trên MapControl | OSRM trả về encoded polyline string; GMapRoute cần list Coordinate; chưa implement decoder | Implement Encoded Polyline Algorithm decode (Google format) trả về `List<PointLatLng>`, feed vào `GMapRoute` |
| P06 | 2026-04-24 | Simulation | `SimulationService` là stub — không có timer, không tự động di chuyển driver | `IDriverSimulationService` interface thiếu định nghĩa; class là no-op | Implement `SimulationService` với `System.Windows.Forms.Timer` theo phases: ToPickup (3s) → Arrived (3s) → ToDestination (4s) → Completed |

---

## 2. Bugs (level: code)
> **Cách ghi:** [Module] → lỗi → nguyên nhân → fix

| # | Ngày | Module | Lỗi | Nguyên nhân | Cách fix |
|---|------|--------|------|-------------|----------|
| B01 | 2026-04-24 | `Presentation/Program.cs` | Compile error: `RouteService` không tìm thấy | `new RouteService(...)` — `RouteService` class chưa tồn tại trong codebase | Xoá dòng khởi tạo này hoặc tạo class `SimpleRouteService : IRouteService` wrapper `MapService`; cập nhật `IRouteService` → `IMapService` trong Program.cs |
| B02 | 2026-04-24 | `Presentation/Program.cs` | Compile error: `IDriverSimulationService` undefined | Interface được reference trong Program.cs (manual composition) nhưng chưa khai báo trong `Application.Interfaces` | Tạo `IDriverSimulationService` interface trong `Application/Interfaces/`; tạo stub impl `DriverSimulationService` |
| B03 | 2026-04-24 | `Infrastructure/Repositories` | `JsonStorage` không tìm thấy từ `Program.cs` | Missing `using` statement cho Infrastructure namespace trong Presentation | Thêm `using Infrastructure.Repositories;` hoặc dùng fully-qualified name |
| B04 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Reference `Domain.Interfaces.IDriverRepository` (namespace không tồn tại) | Namespace sai — đúng là `Domain.Repositories` | Sửa `using Domain.Interfaces` → `using Domain.Repositories` |
| B05 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Gọi `ITripService.TryAssignDriver()` — method không tồn tại trên interface | Method name sai — interface expose `MatchDriverAsync(Guid, Guid)` | Đổi call → `MatchDriverAsync(tripId, driverId)` |
| B06 | 2026-04-24 | `Presentation/ViewModels/DriverViewModel.cs` | Missing using cho `Driver`, `Trip`, `Vehicle`, `Location` | Không có `using Domain.Entities;` và `using Domain.ValueObjects;` | Thêm đúng using statements |
| B07 | 2026-04-24 | `Infrastructure/Repositories/JsonStorage` | Thread-safety chưa xác minh đầy đủ | `ReaderWriterLockSlim` khai báo nhưng chưa chắc bao phủ hết read path | Audit lại toàn bộ public methods của `JsonStorage` — đảm bảo mọi read dùng `EnterReadLock`, mọi write dùng `EnterWriteLock` |

---

## 3. Design Decisions (level: kiến trúc / hướng đi)
> **Cách ghi:** [Phạm vi] → quyết định → lý do → ảnh hưởng

| # | Ngày | Phạm vi | Quyết định | Lý do | Ảnh hưởng |
|---|------|---------|------------|-------|------------|
| D01 | 2026-04-24 | Domain / Payment | Không tạo `PaymentRepository` riêng — xử lý payment inline trong `TripService.CompleteTrip()` | MVP scope: Payment chỉ là hành động confirm tại thời điểm complete, không cần audit trail độc lập ở giai đoạn này | Nếu mở rộng (payment gateway, refund), cần tách `PaymentService` + `IPaymentRepository` ra khỏi `TripService` |
| D02 | 2026-04-24 | Domain / Trip | Dùng State Pattern (`ITripState` + 8 state classes) thay vì switch-case trên `TripStatus` enum | Đảm bảo chỉ chuyển trạng thái hợp lệ; dễ thêm state mới không sửa Trip class; tách biệt behavior theo trạng thái | Mọi transition phải đi qua state object → không thể bypass business rule từ bên ngoài |
| D03 | 2026-04-24 | Domain / Driver | Dùng `DriverStateMachine` (static class, dictionary) thay vì State Pattern đầy đủ | Driver chỉ có 3 state đơn giản (Offline/Available/OnTrip) — State Pattern là overkill; static dictionary đủ validate | Nếu thêm state Driver (ví dụ: Suspended), chỉ cần update dictionary — không cần tạo class mới |
| D04 | 2026-04-24 | Domain / Persistence | Driver tham chiếu `VehicleId` (Guid) thay vì embed `Vehicle` object | Giữ Vehicle là Aggregate riêng, tránh circular dependency; Vehicle có vòng đời độc lập (có thể đổi xe) | Query thông tin xe phải join qua VehicleRepository — 2 query thay vì 1 |
| D05 | 2026-04-24 | Infrastructure | JSON file storage với `TypeNameHandling.All` (Newtonsoft.Json) | Ràng buộc giáo khoa không dùng SQL; `TypeNameHandling.All` cần thiết để deserialized `List<User>` giữ đúng subtype (Driver / Passenger) | Dữ liệu JSON lưu thêm `$type` field — coupling với Newtonsoft; migration sang DB sau này cần rewrite repository |
| D06 | 2026-04-24 | Application / Event | `ITripService.TripStatusChanged` là `EventHandler<TripStatusChangedEventArgs>` declare trên interface | Cho phép UI subscribe qua interface mà không cần cast sang `TripService` concrete — decoupling giữa Presentation và Application | UI Forms phải unsubscribe khi close để tránh memory leak |
| D07 | 2026-04-24 | Application / Matching | Không implement `InMemoryDriverCache`, không có `Policies/` (EligibilityPolicy, AssignmentPolicy) | MVP scope — inline logic trong `MatchingService` đủ; cache và policy object là over-engineering cho project đơn luồng | Khi scale lên (nhiều concurrent request), cần extract policy và cache |
| D08 | 2026-04-24 | Infrastructure / Map | Dùng Photon (geocoding) + OSRM (routing) thay vì Google Maps API | Google Maps yêu cầu API key có phí; Photon + OSRM là open-source, không cần key | OSRM trả về encoded polyline — cần implement decoder riêng (chưa xong) |
| D09 | 2026-04-24 | Presentation / GMap | Tách `GMap.NET.WinForms` (Presentation) và `GMap.NET.Core` (Infrastructure) | Infrastructure chỉ cần routing/geocoding API calls qua GMapProviders — không cần UI widget | Đúng separation: UI dependency không leak vào Infrastructure |

---

## 4. Todo (level: execution)

1. **[MatchingService]** Thêm `SemaphoreSlim(1,1)` bao quanh block check-driver-status + assign-driver để tránh race condition
2. **[MatchingService]** Bổ sung 3 điều kiện lọc driver: (a) địa chỉ hành chính match (phường→quận→thành phố), (b) khoảng cách Haversine < `SimulationConstants.MaxPickupDistance`, (c) `Driver.Wallet >= minimum threshold`
3. **[Program.cs]** Xoá `IRouteService / RouteService` khỏi khởi tạo → thay bằng `IMapService / MapService` (khởi tạo bằng `new`); hoặc tạo `SimpleRouteService` wrapper
4. **[Application/Interfaces]** Tạo interface `IDriverSimulationService` + stub implementation `DriverSimulationService` để giải compile error
5. **[Infrastructure/Repositories]** Audit `JsonStorage` — xác minh `ReaderWriterLockSlim` bao phủ đầy đủ cả read path và write path
6. **[Infrastructure/Map]** Implement Encoded Polyline decoder (Google Polyline Algorithm) — decode string → `List<PointLatLng>` → feed vào `GMapRoute`
7. **[Application/Services]** Implement `SimulationService` với `Timer` theo phases: ToPickup (3s) → Arrived wait (3s) → ToDestination (4s) → Completed; cập nhật `Driver.Position` mỗi tick
8. **[AssignDriverHandler]** Sửa namespace `Domain.Interfaces` → `Domain.Repositories`; sửa `TryAssignDriver` → `MatchDriverAsync`
9. **[Presentation/ViewModels/DriverViewModel]** Thêm đúng using statements cho `Domain.Entities`, `Domain.ValueObjects`
10. **[TripService]** Audit các chỗ dùng `.Result` trên async call — thay bằng `Task.Run()` + `await` hoặc tách hẳn thành async path
11. **[Presentation]** Đảm bảo mọi Form unsubscribe `ITripService.TripStatusChanged` trong `Form_FormClosed` để tránh memory leak
12. **[Testing]** Viết unit test cho Trip state transitions (8 states × valid/invalid transitions) không cần UI
13. **[UC15]** Hoàn thiện UI flow Driver chấp nhận / từ chối chuyến (AcceptTripHandler đã có, UI chưa nối)
14. **[UC18]** Implement proximity search (Driver Radar) — hiển thị danh sách driver Available trong bán kính cho Passenger

---

## 5. References (level: support)
> **Cách ghi:** Tên → link → dùng cho việc gì

| # | Tên | Link | Dùng cho |
|---|-----|------|----------|
| R01 | OSRM HTTP API | http://router.project-osrm.org | Tính tuyến đường, ETA, trả về encoded polyline |
| R02 | Photon Geocoding API | https://photon.komoot.io | Geocoding địa chỉ tiếng Việt → Coordinate |
| R03 | GMap.NET GitHub | https://github.com/judero01pol/GMap.NET | Tích hợp bản đồ vào WinForms, overlay, marker, route |
| R04 | Google Encoded Polyline Algorithm | https://developers.google.com/maps/documentation/utilities/polylinealgorithm | Decode polyline string từ OSRM response → List\<PointLatLng\> |
| R05 | Newtonsoft.Json TypeNameHandling | https://www.newtonsoft.com/json/help/html/SerializeTypeNameHandling.htm | Serialize/Deserialize đa hình (List\<User\> giữ Driver / Passenger subtype) |
| R06 | Microsoft.Extensions.DependencyInjection | https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection | DI container cho .NET Framework 4.8 |
| R07 | Haversine Formula | https://en.wikipedia.org/wiki/Haversine_formula | Tính khoảng cách giữa 2 Coordinate (dùng trong TripService + MatchingService filter) |
| R08 | ReaderWriterLockSlim (.NET) | https://learn.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlockslim | Thread-safe read/write cho JsonStorage |
| R09 | State Pattern (GoF) | https://refactoring.guru/design-patterns/state | Tham chiếu implement ITripState + 8 state classes |
| R10 | SemaphoreSlim (.NET) | https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim | Giải race condition trong MatchingService |
