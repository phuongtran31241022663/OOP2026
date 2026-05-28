# Kế hoạch sửa lỗi ListBox gợi ý bị che khuất trong ucBooking

**Ngày:** 2026-05-28
**Người dùng:** Cline
**Mục tiêu:** Sửa lỗi `lstSuggestions` trong `ucLocationPicker` bị che khuất khi đặt trong `ucBooking` do các container cha có chiều cao cố định.

## Các bước

- [x] Phân tích nguyên nhân: `tlpMain`, `pnlPickup`, `tlpPickup` (và tương tự cho Dropoff) có chiều cao cố định, ngăn cản `ucLocationPicker` mở rộng chiều cao khi hiển thị gợi ý.
- [ ] Cập nhật `OOP2026/UserControl/UcBooking.Designer.cs`:
    - [ ] Thay đổi `RowStyle` của `tlpMain` (hàng 0 và 2) thành `AutoSize`.
    - [ ] Thiết lập `AutoSize = true` và `AutoSizeMode = GrowAndShrink` cho `pnlPickup` và `pnlDropoff`.
    - [ ] Thiết lập `AutoSize = true` và `AutoSizeMode = GrowAndShrink` cho `tlpPickup` và `tlpDropoff`.
    - [ ] Loại bỏ các thuộc tính `Size` cố định gây xung đột với `AutoSize`.
- [ ] Kiểm tra lại giao diện (nếu có thể chạy được project).

## Kiểm chứng

- Test: Chạy ứng dụng, vào màn hình đặt chuyến, nhập địa chỉ vào ô tìm kiếm và kiểm tra xem danh sách gợi ý có hiển thị đầy đủ không.
- Build: Đảm bảo project vẫn compile thành công sau khi sửa file Designer.

## Rủi ro

- Thay đổi `AutoSize` có thể làm xê dịch các thành phần UI khác nếu không được kiểm soát kỹ. Tuy nhiên, do sử dụng `TableLayoutPanel` nên rủi ro này được giảm thiểu. (Mức độ: Thấp)

## Nguồn tham khảo

- Phân tích code hiện tại trong `ucLocationPicker.cs` và `UcBooking.Designer.cs`.