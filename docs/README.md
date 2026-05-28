# RideGo - Hệ Thống Kết Nối Tài Xế & Hành Khách

RideGo là ứng dụng mô phỏng nền tảng gọi xe công nghệ, xây dựng bằng WinForms trên .NET Framework 4.8. Dự án tập trung vào lập trình hướng đối tượng, State Pattern, Repository Pattern, event-driven UI và lưu trữ JSON.

## Tính Năng Cốt Lõi

- **Đa vai trò:** Passenger, Driver và Admin có giao diện và luồng nghiệp vụ riêng.
- **Vòng đời chuyến đi:** `Trip` dùng State Pattern với các trạng thái `Pending`, `Searching`, `Matched`, `Arrived`, `Started`, `DropOff`, `Completed`, `Cancelled`, `Timeout`.
- **Ghép tài xế:** `MatchingService` dùng spatial grid, lọc tài xế `Online` và khớp `VehicleType`; điều kiện `Wallet >= Commission` đã được ghi nhận là điểm còn thiếu ở tầng matching.
- **Bản đồ và định tuyến:** `MapService` dùng Photon cho geocoding và OSRM cho routing; UI hiển thị qua `ucMap`.
- **Giá cước:** `FareService` dùng `Policy.CalculateFare()` để tính tổng cước, hoa hồng và thu nhập tài xế.
- **Quản trị:** `FrmAdmin` và `AdminService` hỗ trợ thống kê, đọc dữ liệu người dùng/chuyến đi/chính sách/đánh giá.

## Tài Liệu Chính

- **Single source of truth:** [`FINAL.md`](FINAL.md) là tài liệu kiến trúc và mapping code chính.
- **Bản dẫn hướng:** [`docs/FINAL.md`](docs/FINAL.md) chỉ giữ mục lục/link về [`FINAL.md`](FINAL.md), không còn là bản đặc tả độc lập.

## Cấu Trúc Code Chính

1. **Domain:** `OOP2026/Entity.cs`, `OOP2026/ValueObject.cs`, `OOP2026/Pattern.cs`.
2. **Application:** `OOP2026/Interface.cs`, `OOP2026/Service.cs`.
3. **Infrastructure:** `OOP2026/Repository.cs`, `OOP2026/DataSeeder.cs`.
4. **Presentation:** `OOP2026/Form/*.cs`, `OOP2026/UserControl/*.cs`, `OOP2026/UIHelper.cs`.
5. **Composition root:** `OOP2026/Program.cs` khởi tạo repository/service và chạy `FrmMultiRole`.

## Hướng Dẫn Chạy Nhanh

1. Mở [`OOP2026.slnx`](OOP2026.slnx) bằng Visual Studio 2022.
2. Restore NuGet Packages nếu Visual Studio yêu cầu.
3. Chọn project `OOP2026` làm Startup Project.
4. Nhấn F5 để build và chạy ứng dụng.
5. Tài khoản demo được seed trong `DataSeeder`:
   - Passenger: `0911111111` / `123456`
   - Driver: `0900000000` / `123456`
   - Admin: `0999999999` / `admin123`

---

Dự án phục vụ mục đích học tập môn Lập trình hướng đối tượng (OOP).
