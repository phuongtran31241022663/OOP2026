# Agent Knowledge Base - Tri thức Ngành & Kỹ thuật

## Quy tắc Kỹ thuật (Technical Rules)
*Các quy tắc đã được học và áp dụng trong dự án này.*

- **C# WinForms Best Practices**: Sử dụng `ConfigureAwait(false)` để tránh deadlock.
- **JSON Serialization on .NET 4.8**: Đảm bảo tên tham số constructor khớp với Property.
- **Thread-safe Repository**: Sử dụng `SemaphoreSlim(1,1)` và sao chép danh sách trước khi trả về.
- **Event Cleanup**: Luôn unsubcribe sự kiện trong `OnHandleDestroyed` hoặc `Dispose`.

## Kiến trúc & Design Patterns
*Các mẫu đã được áp dụng và tài liệu hóa.*

- **State Pattern**: Quản lý trạng thái chuyến đi và tài xế.
- **Observer Pattern**: Thay thế Polling bằng Push notifications.
- **Generic Repository**: Lớp lưu trữ JSON dùng chung.
- **Factory Pattern**: Tách logic khởi tạo đối tượng.
- **Single-Form Shell**: `FrmMultiRole` làm khung nền duy nhất.

## Quy trình Làm việc (Workflow Knowledge)
*Các bài học từ quá trình phát triển.*

- **Plan-Then-Execute**: Luôn tạo kế hoạch trước khi sửa code.
- **Defensive Programming**: Kiểm tra null và bọc try-catch ở biên API.
- **Documentation-First**: Cập nhật tài liệu ngay sau refactor lớn.
- **Learning Loop**: Tích hợp kinh nghiệm từ phiên làm việc này vào phiên sau.

## Technical Debt & Cải thiện
*Các điểm cần cải thiện trong tương lai.*

- **Nullability Warnings (CS8618, CS8600, CS8604)**: Khởi tạo trường trong constructor hoặc làm nullable.
- **UI Transition Effects**: Thêm hiệu ứng chuyển cảnh giữa các UserControl.
- **Admin Tab Completion**: Hoàn thiện chi tiết nội dung các tab Admin.

## Lịch sử Cập nhật Knowledge (Append-only)

### [2026-05-28] Tạo Knowledge Base ban đầu
- Tổng hợp các quy tắc kỹ thuật, kiến trúc và quy trình làm việc từ dự án OOP2026.

--- 
*Áp dụng nguyên tắc TRIM & COMPRESS: khi file vượt quá 500 dòng, tóm tắt mục cũ vào phần Lịch sử.*
