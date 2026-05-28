# Kế hoạch sửa lỗi hiển thị tiếng Việt (Encoding)

**Ngày:** 2026-05-27
**Mục tiêu:** Khắc phục tình trạng lỗi font/encoding tiếng Việt trong toàn bộ dự án, đảm bảo các file được lưu ở định dạng UTF-8 with BOM và nội dung hiển thị đúng tiếng Việt.

## Các bước thực hiện

- [ ] **Bước 1: Khảo sát và lập danh sách file lỗi**
    - Sử dụng regex để tìm các chuỗi ký tự lạ (Ð, nh?, ký, kho?n, v.v.)
    - Liệt kê các file cần xử lý.
- [ ] **Bước 2: Phân tích và định nghĩa bảng mã chuyển đổi**
    - Xác định các cụm từ bị lỗi và từ tương ứng đúng.
    - Ví dụ: `Ðang nh?p` -> `Đăng nhập`, `Tài kho?n` -> `Tài khoản`.
- [ ] **Bước 3: Thực hiện sửa đổi file**
    - Ưu tiên các file Designer và Form trước vì chứa nhiều UI text.
    - Chuyển đổi encoding sang UTF-8 with BOM.
    - Thay thế các chuỗi lỗi bằng tiếng Việt chuẩn.
- [ ] **Bước 4: Kiểm tra và xác minh**
    - Chạy thử ứng dụng (nếu có thể) hoặc kiểm tra nội dung file sau khi sửa.
    - Đảm bảo không làm hỏng cấu trúc code (đặc biệt là các file `.Designer.cs`).

## Danh sách file dự kiến xử lý (dựa trên search ban đầu)
- `OOP2026/Entity.cs`
- `OOP2026/Service.cs`
- `OOP2026/Form/FrmAuth.Designer.cs`
- `OOP2026/Form/FrmAuth.cs`
- `OOP2026/Form/FrmDriverAuth.Designer.cs`
- `OOP2026/Form/FrmDriverAuth.cs`
- `OOP2026/Form/FrmPassengerAuth.Designer.cs`
- `OOP2026/Form/FrmPassengerAuth.cs`
- `OOP2026/UserControl/ucBooking.cs`
- `OOP2026/UserControl/ucDriverHome.cs`
- `OOP2026/UserControl/UcDriverStatus.Designer.cs`
- `OOP2026/UserControl/UcStatCard.Designer.cs`
- `OOP2026/UserControl/UcWallet.Designer.cs`
- `OOP2026/UserControl/ucWallet.cs`
- ... và các file khác phát hiện thêm.

## Rủi ro
- **Rủi ro thấp**: Thay đổi text hiển thị không ảnh hưởng logic.
- **Rủi ro trung bình**: Sửa nhầm trong file Designer có thể làm hỏng giao diện nếu không cẩn thận với các ký tự điều khiển.

## Nguồn tham khảo
- Chuẩn tiếng Việt UTF-8.