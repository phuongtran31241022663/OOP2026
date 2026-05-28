# Kế hoạch Refactor UI - RideGo

**Ngày:** 2026-05-26
**Mục tiêu:** Refactor toàn bộ giao diện ứng dụng RideGo theo đặc tả "ĐẶC TẢ GIAO DIỆN ĐA VAI TRÒ".

## Các bước

### Phase 1: Global Styles & Header Bar
- [ ] Cập nhật `UIHelper.cs` nếu thiếu màu sắc hoặc font chữ.
- [ ] Refactor `FrmMultiRole.Designer.cs`:
    - Cấu trúc lại Header Bar (Top Panel).
    - Thiết lập Role Switcher với 2 tag (Passenger, Driver).
    - Style lại nút Admin (Tím, icon khiên).
    - Thêm label trạng thái tìm tài xế.
- [ ] Refactor `FrmMultiRole.cs`:
    - Cập nhật logic hiển thị thông tin trên Header.

### Phase 2: Giao diện Hành khách (Passenger)
- [ ] Refactor `ucPassengerHome.Designer.cs`:
    - Bố cục 3 phần: Top (Info), Middle (Menu), Bottom (Content).
- [ ] Refactor `ucBooking.Designer.cs`:
    - Khung nhập lộ trình với vòng tròn màu (Xanh/Đỏ).
    - Khung chọn phương tiện (Ô tô/Xe máy) dạng card.
    - Nút "Đặt chuyến" lớn.
- [ ] Refactor `ucTrip.Designer.cs`:
    - Trạng thái tìm tài xế (Thanh thông báo, Progress bar).
- [ ] Refactor `ucProfile.Designer.cs`:
    - Form đổi mật khẩu và thông tin cá nhân.

### Phase 3: Giao diện Tài xế (Driver)
- [ ] Refactor `ucDriverHome.Designer.cs`:
    - Bố cục 3 phần: Top (Info - Cam), Middle (Menu), Bottom (Content).
- [ ] Refactor `ucDriverStatus.Designer.cs`:
    - Hiển thị số dư ví lớn, tổng thu nhập, tổng chuyến.
- [ ] Refactor `ucWallet.Designer.cs`:
    - Các mức nạp tiền nhanh (50k, 100k, 200k).
- [ ] Refactor `ucRequest.Designer.cs`:
    - Danh sách chuyến đề nghị dạng card (Service icon, Amount, Route).

### Phase 4: Giao diện Quản trị (Admin)
- [ ] Refactor `FrmAdmin.Designer.cs`:
    - Chuyển sang giao diện Tab (Thống kê, Người dùng, Chuyến đi, Biểu phí).
    - Tab Thống kê: 4 thẻ chỉ số.
    - Tab Biểu phí: Form cập nhật giá.

### Phase 5: Giao diện Auth
- [ ] Refactor `ucPassengerAuth.Designer.cs` & `ucDriverAuth.Designer.cs`:
    - Header màu theo vai trò.
    - Khung tài khoản demo.
    - Form nhập liệu với icon.

## Kiểm chứng
- [ ] Chạy ứng dụng, kiểm tra layout 3 cột.
- [ ] Kiểm tra chuyển đổi tab ở các vai trò.
- [ ] Kiểm tra giao diện Admin.
- [ ] Kiểm tra giao diện Auth.

## Rủi ro
- [ ] Thay đổi Designer.cs thủ công có thể gây lỗi nếu không cẩn thận với các reference. (Mức độ: Trung bình)
- [ ] Ảnh hưởng đến logic binding dữ liệu hiện có. (Mức độ: Thấp)