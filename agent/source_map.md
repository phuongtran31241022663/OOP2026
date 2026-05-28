# Agent Source Map - Bản đồ Dự án

## Cấu trúc Dự án (OOP2026)

### 1. Tầng Presentation (Giao diện)
- `OOP2026/Program.cs` - Điểm vào ứng dụng
- `OOP2026/Form/` - Các form chính (FrmAuth, FrmMultiRole, etc.)
- `OOP2026/UserControl/` - Các điều khiển người dùng tái sử dụng

### 2. Tầng Application (Logic nghiệp vụ)
- `OOP2026/Service.cs` - Các dịch vụ nghiệp vụ chính
- `OOP2026/Repository.cs` - Lớp truy cập dữ liệu
- `OOP2026/Interface.cs` - Định nghĩa các interface

### 3. Tầng Domain (Đối tượng nghiệp vụ)
- `OOP2026/Entity.cs` - Các thực thể cơ bản
- `OOP2026/ValueObject.cs` - Các đối tượng giá trị
- `OOP2026/Pattern.cs` - Các mẫu thiết kế được áp dụng

### 4. Tầng Infrastructure (Cơ sở hạ tầng)
- `OOP2026/DataSeeder.cs` - Khởi tạo dữ liệu mẫu
- `OOP2026/Simulation.cs` - Mô phỏng các tình huống

### 5. Cấu hình & Tài nguyên
- `OOP2026/App.config` - Cấu hình ứng dụng
- `OOP2026/Properties/` - Tài nguyên và cài đặt
- `docs/README.md` - Hướng dẫn chung
- `docs/FINAL.md` - **Single Source of Truth** cho kiến trúc và mapping code
- `REPORT.md` - Báo cáo đồ án chi tiết

## Quy trình Cập nhật
- **Sau mỗi thay đổi kiến trúc lớn**: Cập nhật file này để phản ánh cấu trúc mới.
- **Tag phiên bản**: Mỗi mục cập nhật phải có ngày tháng và mô tả ngắn gọn.
- **TRIM & COMPRESS**: Khi file vượt quá 500 dòng, tóm tắt các mục cũ vào phần Lịch sử.

## Lịch sử Thay đổi (Append-only)

### [2026-05-28] Tạo bản đồ nguồn ban đầu
- Được tạo ra để hỗ trợ hệ thống Self-Improving và Context Management.
