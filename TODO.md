# Sửa tài liệu (Documentation Fixes) - Theo dõi tiến độ

## Mục tiêu
Sửa các điểm sai/lệch trong tài liệu `.md` so với code C# thực tế.

## Checklist

- [ ] 1. Sửa `Common_Project_Documentation.md`
  - [ ] Xóa/sửa `BaseException` (không tồn tại)
  - [ ] Cập nhật `PasswordHasher` có cả ở `Helpers/` và `Utilities/`
- [ ] 2. Sửa `Domain_Model.md`
  - [ ] Sửa đường dẫn folder thành `Domain/Entities/...`
  - [ ] Xóa `TripStateMachine` (không tồn tại)
  - [ ] Sửa `Driver.UpdateReviews(int rating)`
  - [ ] Chuyển `IRepository.cs` ra `Domain/Repositories/`
- [ ] 3. Sửa `Presentation_and_UI.md`
  - [ ] Sửa tên method `ITripService` cho đúng (`CreateTripAsync`, `MatchDriverAsync`, `GetTripAsync`)
  - [ ] Thay `IDriverMatchingService` → `IMatchingService`
  - [ ] Xóa các lớp Command/Query không tồn tại
- [ ] 4. Sửa `README.md`
  - [ ] Xóa/sửa phần DTOs/Features/Commands (không tồn tại)
  - [ ] Xóa `TripStateMachine`
  - [ ] Cập nhật `MatchingService` (đã kiểm tra VehicleType)
  - [ ] Cập nhật `AdminService` (đã implement)
  - [ ] Cập nhật workers (`TripTimeoutWorker`, `TripMatchingWorker` đã tồn tại)
  - [ ] Sửa `FareService` (không inject `IRouteService`)
  - [ ] Sửa `ITripService` event (đã có trong interface)
  - [ ] Sửa namespace `ReviewService` → `Application.Services`
- [ ] 5. Sửa `RideGo_Architecture.md`
  - [ ] Sửa namespace `MatchingService` → `Application.Services`
  - [ ] Sửa kiểu event `TripService` → `EventHandler<TripStatusChangedEventArgs>`
  - [ ] Sửa vị trí `IMapService` → `Application.Interfaces`
  - [ ] Xóa optimistic concurrency / Version
  - [ ] Cập nhật `BackgroundJobs` đã tồn tại
  - [ ] Cập nhật `AdminService` đã implement
- [ ] 6. Sửa `Technical_Architecture.md`
  - [ ] Sửa namespace các service cho đúng
  - [ ] Sửa dependency `FareService`
  - [ ] Sửa kiểu event `TripService`
  - [ ] Sửa `IMapService` vị trí
  - [ ] Xóa Version/concurrency
  - [ ] Cập nhật `BackgroundJobs`
- [ ] 7. Sửa `Business_Logic_and_Workflows.md`
  - [ ] Thay `FareCalculationService` / Policies bằng `FareRule.CalculateFare`
  - [ ] Sửa `MatchingService` mô tả

