# Retrospective – 2026-05-28

## Đã làm đúng
- Xác định nhanh nguyên nhân lỗi `ArgumentException` trong constructor của class `Addr`.
- Tìm ra các điểm hở trong logic khởi tạo `Addr` tại `Service.cs` và `ucMap.cs`.
- Triển khai fallback "Vị trí không xác định" hoặc "Selected Location" để đảm bảo hệ thống không bị crash khi API bản đồ không trả về tên địa danh cụ thể.

## Sai lầm / vấn đề
- `apply_diff` gặp khó khăn trong việc khớp chính xác nội dung file `Service.cs` do sai lệch whitespace hoặc dòng trống, dẫn đến việc phải dùng `write_to_file` cho `ucMap.cs` để đảm bảo tính nhất quán.
- Một số chỗ trong code cũ khởi tạo `Addr` với chuỗi rỗng `""` thay vì truyền giá trị địa chỉ vào làm tên.

## Cải tiến cho lần sau
- Kiểm tra kỹ whitespace và line endings khi dùng `apply_diff` trên các file có cấu trúc phức tạp.
- Cân nhắc việc nới lỏng ràng buộc trong constructor của Value Object nếu nó gây ra quá nhiều rủi ro crash UI, hoặc luôn đảm bảo có default value từ tầng Service.
- Sử dụng `search_files` với regex chặt chẽ hơn để tìm tất cả các nơi khởi tạo object có nguy cơ lỗi.

**Còn gì tôi đã bỏ sót không?**
- Đã rà soát các nơi gọi `new Addr()`.
- Đã gia cố `ucMap` và `MapSvc`.
- Hệ thống hiện tại đã an toàn trước lỗi trống tên địa điểm.
