# Sửa tài liệu (Documentation Fixes) - Theo dõi tiến độ

## Mục tiêu
Sửa các điểm sai/lệch trong tài liệu `.md` so với code C# thực tế và vi phạm Limit.md.

## Checklist

- [x] 1. Sửa `Business_Logic_and_Workflows.md`
  - [x] Xóa/sửa mục 9 "Dependency Mapping" vi phạm DI (thay bằng manual dependency)
  - [x] Giữ nguyên phần educational constraints
- [x] 2. Sửa `CleanArchitecture_Final.md`
  - [x] Xóa toàn bộ tham chiếu `Microsoft.Extensions.DependencyInjection`, `IServiceCollection`, `IServiceProvider`, `DI Root`
  - [x] Xóa EF Core (`AppDbContext`, `Migrations`, `Interceptors`, `ReadDbContext`)
  - [x] Xóa MediatR (`Behaviors/`, `IRequest`, `IRequestHandler`)
  - [x] Xóa `AutoMapper` (`MappingProfile`)
  - [x] Xóa `NetArchTest`
  - [x] Xóa C# 8+ keywords: `record`, `global using`, file-scoped namespaces, `required`, `init`, `IAsyncEnumerable`, `await foreach`
  - [x] Sửa pipeline mô tả về WinForms event-driven thay vì HTTP pipeline
  - [x] Sửa `Presentation` → không dùng Minimal API, không có `Endpoints/`, `Middleware/`, `Filters/`, `Contracts/`
- [x] 3. Sửa `DEVLOG.md`
  - [x] Thay "services.AddSingleton/DI container/Microsoft.Extensions.DependencyInjection" → "manual instantiation/new trực tiếp"
  - [x] Giữ nguyên bug/fix hợp lệ
- [x] 4. Sửa `PSPEC.md`
  - [x] Xóa "DI Container: Microsoft.Extensions.DependencyInjection" khỏi tech stack
  - [x] Sửa bảng Pattern: thay "Dependency Injection" thành "Manual Service Composition"
- [x] 5. Sửa `README.md`
  - [x] Xóa "Microsoft.Extensions.DependencyInjection" khỏi công nghệ
  - [x] Sửa bảng Design Patterns: thay "DI" thành "Manual Service Composition"
- [x] 6. Sửa `RideGo_Architecture.md`
  - [x] Xóa toàn bộ section "Dependency Injection"
  - [x] Sửa mô tả khởi tạo service graph: thay "DI composition root" = "manual instantiation trong Program.cs"
  - [x] Xóa `Microsoft.Extensions.DependencyInjection` ở header
  - [x] Sửa mục 6.7
- [x] 7. Sửa `Technical_Architecture.md`
  - [x] Xóa section 10 "DI Registration"
  - [x] Sửa mô tả khởi tạo repositories/services = manual `new`
  - [x] Xóa `AddHttpClient` (thay bằng `new HttpClient()` hoặc `WebRequest`)
  - [x] Sửa mô tả `MapService`
- [x] 8. Sửa `Presentation_and_UI.md`
  - [x] Sửa "Direct Service Injection" → "Direct Service Reference (manual)"
  - [x] Xóa đề cập "injection" nếu mang nghĩa DI container
- [x] 9. Sửa `Common_Project_Documentation.md`
  - [x] Xóa/sửa `BaseException` (không tồn tại)
  - [x] Cập nhật `PasswordHasher` có cả ở `Helpers/` và `Utilities/`
- [x] 10. Sửa `Domain_Model.md`
  - [x] Sửa đường dẫn folder thành `Domain/Entities/...`
  - [x] Xóa `TripStateMachine` (không tồn tại)
  - [x] Sửa `Driver.UpdateReviews(int rating)`
  - [x] Chuyển `IRepository.cs` ra `Domain/Repositories/`
- [x] 11. Cập nhật `TODO.md` trạng thái

