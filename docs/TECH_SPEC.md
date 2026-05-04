# TECH SPEC — Cầu nối Nghiệp vụ & Kỹ thuật

## 1. Quyết định Kiến trúc (Architecture Decisions)
- **5-Layer Architecture:** Tổ chức code theo Common, Domain, Application, Infrastructure, Presentation.
- **State Pattern cho Lifecycle:** Thay thế switch-case bằng các class trạng thái để quản lý vòng đời Trip và Driver.
- **Persistence:** Dùng JSON file thay vì Database để đơn giản hóa môi trường chạy (không cần cài đặt SQL).
- **Manual DI:** Không dùng thư viện DI, khởi tạo tập trung tại `Program.cs`.

## 2. Mô hình Dữ liệu (Data Model)
- **Aggregate Roots:** `Trip`, `User` (Passenger, Driver, Admin).
- **Value Objects:** `Money`, `Location`, `Coordinate`, `Address`, `Route`, `Fare`.
- **Đa hình trong JSON:** Sử dụng `$type` (Newtonsoft.Json) để lưu trữ danh sách User/Vehicle hỗn hợp.

## 3. Luồng dữ liệu chính (Data Flow)
1. **Đặt xe:** Passenger → `TripService` → `Trip` (Searching).
2. **Matching:** `TripMatchingWorker` → `MatchingService` → Lọc Driver → `Trip` (Matched).
3. **Di chuyển:** `SimulationService` cập nhật vị trí Driver ảo → UI update qua Event.
4. **Thanh toán:** `Trip.CompleteTrip()` → Trừ tiền ví Driver (hoa hồng) → Cập nhật thu nhập.

## 4. Xử lý kỹ thuật đặc thù
- **Race Condition:** Dùng `SemaphoreSlim` khi ghép tài xế để đảm bảo 1 tài xế không nhận 2 chuyến cùng lúc.
- **Thread-safety:** `ReaderWriterLockSlim` bảo vệ file JSON khi có nhiều luồng đọc/ghi.
- **Map Integration:** OSRM cho routing, Photon cho geocoding, GMap.NET cho hiển thị.