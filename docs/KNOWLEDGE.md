# KNOWLEDGE BASE — OOP2026

## 1. Design Patterns
### 1.1 State Pattern
- **Áp dụng:** Quản lý vòng đời `Trip` (8 states) và `Driver` (3 states).
- **Lợi ích:** Tách biệt logic hành vi theo trạng thái, đảm bảo transition hợp lệ.
- **Chi tiết:** Xem `docs/State.md`.

### 1.2 Strategy Pattern
- **Áp dụng:** Tính giá cước (`Fare`) theo loại xe (`CarFareStrategy`, `MotorbikeFareStrategy`).
- **Lợi ích:** Dễ dàng thêm loại xe hoặc công thức tính giá mới.
- **Chi tiết:** Xem `docs/Strategy.md`.

### 1.3 Observer Pattern
- **Áp dụng:** `ITripService.TripStatusChanged` để cập nhật UI khi trạng thái chuyến đi thay đổi.
- **Lưu ý:** UI phải unsubscribe khi đóng Form để tránh memory leak.
- **Chi tiết:** Xem `docs/Observer.md`.

## 2. OOP Principles
- **Encapsulation:** Sử dụng private setters và `[JsonConstructor]` để bảo vệ tính toàn vẹn của Domain Entities khi lưu/load JSON.
- **Inheritance:** Phân cấp `User` (Passenger, Driver, Admin) và `Vehicle` (Car, Motorbike).
- **Polymorphism:** Xử lý danh sách `List<User>` đa hình nhờ `TypeNameHandling.All` trong Newtonsoft.Json.

## 3. Kiến thức nền tảng
- **WinForms:** Kiến trúc UI, control custom, vẽ Route trên GMap.
- **APM (Asynchronous Programming Model):** Xử lý tác vụ nền, tránh treo UI.
- **Domain Events:** Cách Aggregate emit events để decouple các module.

---
*Tài liệu này gộp tri thức từ các file: Inheritance.md, OOP.md, Polymorphism.md, Principles.md, Relationship.md, Serialize.md, XML.md.*