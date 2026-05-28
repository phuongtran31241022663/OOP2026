# Kế hoạch sửa lỗi "Tên địa điểm không được để trống"

**Ngày:** 2026-05-28
**Mục tiêu:** Khắc phục triệt để lỗi ArgumentException khi tạo đối tượng Addr với tên rỗng.

## Nguyên nhân
Trong class `Addr` (ValueObject.cs), constructor kiểm tra `string.IsNullOrWhiteSpace(name)`. Tuy nhiên, trong `Service.cs`, phương thức `EnsureLocationAsync` lại truyền `""` vào tham số này.

## Các bước thực hiện

### 1. Sửa MapSvc trong Service.cs
- **File:** `OOP2026/Service.cs`
- **Hành động:** Cập nhật phương thức `EnsureLocationAsync`.
- **Logic:** Nếu `lat` và `lng` có giá trị, dùng `address` làm `name` thay vì để trống.

### 2. Cập nhật ucMap.cs
- **File:** `OOP2026/UserControl/ucMap.cs`
- **Hành động:** Trong `OnMapMouseClick`, nếu `_mapService.GetAddressAsync` trả về `null`, tạo một `Loc` mặc định với tên "Selected Location" thay vì để crash.

### 3. Rà soát ValueObject.cs (Tùy chọn)
- Cân nhắc việc cho phép tên trống nhưng tự động chuyển thành "Unknown Location" thay vì ném Exception để tăng tính ổn định của UI (nếu cần thiết).

## Kiểm chứng
- [ ] Chạy ứng dụng.
- [ ] Mở bản đồ và click vào một điểm bất kỳ.
- [ ] Tìm kiếm một địa chỉ không có trong DB để kích hoạt `EnsureLocationAsync`.

## Rủi ro
- Thấp. Chỉ ảnh hưởng đến cách hiển thị tên địa điểm.
