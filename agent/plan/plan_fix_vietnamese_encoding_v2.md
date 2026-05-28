# Kế hoạch sửa lỗi hiển thị tiếng Việt (Encoding) - V2

**Ngày:** 2026-05-27
**Mục tiêu:** Khắc phục triệt để tình trạng lỗi font/encoding tiếng Việt trong toàn bộ dự án, đảm bảo các file được lưu ở định dạng UTF-8 with BOM và nội dung hiển thị đúng tiếng Việt.

## Tình trạng hiện tại
- Nhiều file `.cs` và `.Designer.cs` chứa các ký tự lỗi như `??`, ``, `Ð`, `nh?`, `kho?n`, `ti x?`.
- Giao diện người dùng hiển thị các ký tự này thay vì tiếng Việt có dấu.
- Nguyên nhân: File được lưu với encoding không phù hợp (có thể là ANSI hoặc UTF-8 không BOM) và bị trình chỉnh sửa xử lý sai.

## Các bước thực hiện

### 1. Khảo sát và lập danh sách file lỗi
- Sử dụng regex để tìm các mẫu ký tự lỗi phổ biến.
- Danh sách file đã nhận diện sơ bộ:
    - `OOP2026/UserControl/ucPassengerHome.cs`
    - `OOP2026/UserControl/ucBooking.cs`
    - `OOP2026/UserControl/ucHistoryCard.Designer.cs`
    - `OOP2026/UserControl/UcProfile.Designer.cs`
    - `OOP2026/UserControl/ucRequest.cs`
    - `OOP2026/Form/FrmDriverAuth.Designer.cs`
    - `OOP2026/Form/FrmDriverAuth.cs`
    - `OOP2026/Form/FrmPassengerAuth.Designer.cs`
    - `OOP2026/Form/FrmPassenger.cs`
    - `OOP2026/Form/FrmMultiRole.cs`
    - `OOP2026/UserControl/ucDriverStatus.cs`
    - `OOP2026/UserControl/ucHistory.cs`
    - `OOP2026/UserControl/ucWallet.cs`
    - `OOP2026/UserControl/ucDriverCard.cs`
    - `OOP2026/UserControl/ucDriverHome.Designer.cs`
    - `OOP2026/UserControl/ucDriverHome.cs`
    - `OOP2026/UserControl/UcBooking.Designer.cs`
    - `OOP2026/UserControl/ucHistoryCard.cs`
    - `OOP2026/Form/FrmMultiRole.Designer.cs`

### 2. Bảng mã chuyển đổi dự kiến
| Chuỗi lỗi | Chuỗi đúng | Ngữ cảnh |
|-----------|------------|----------|
| `` | `Đã` | Trạng thái |
| `Ð` | `Đ` | Đầu câu |
| `nh?` | `nhập` | Đăng nhập |
| `kho?n` | `khoản` | Tài khoản |
| `chuy?n` | `chuyến` | Chuyến đi |
| `ti x?` | `tài xế` | Vai trò |
| `dang` | `đang` | Trạng thái |
| `yu c?u` | `yêu cầu` | Hành động |
| `h? th?ng` | `hệ thống` | Thông báo |
| `tm ki?m` | `tìm kiếm` | Hành động |
| `g?n nh?t` | `gần nhất` | Vị trí |
| `Ðnh gi` | `Đánh giá` | Nút bấm |
| `H? so` | `Hồ sơ` | Tab |
| `L?ch s?` | `Lịch sử` | Tab |
| `Tr?ng thi` | `Trạng thái` | Tab |
| `Cu?c` | `Cuốc` | Tab |
| `?t xe` | `Đặt xe` | Nút bấm |
| `i?m dn` | `Điểm đón` | Label |
| `i?m d?n` | `Điểm đến` | Label |
| `pht` | `phút` | Thời gian |

### 3. Thực hiện sửa đổi
- Chuyển đổi encoding file sang **UTF-8 with BOM**.
- Thay thế các chuỗi lỗi bằng tiếng Việt chuẩn dựa trên bảng mã và ngữ cảnh code.
- Kiểm tra kỹ các file `.Designer.cs` để không làm hỏng cấu trúc WinForms.

### 4. Kiểm tra và xác minh
- Kiểm tra nội dung file sau khi sửa.
- Đảm bảo ứng dụng vẫn biên dịch được.

## Rủi ro
- **Rủi ro trung bình**: Thay thế nhầm các ký tự `?` trong logic code (ví dụ: toán tử null-conditional `?.`). Cần sử dụng regex thông minh hoặc kiểm tra thủ công.
- **Rủi ro thấp**: Sai sót nhỏ trong chính tả tiếng Việt.

## Nguồn tham khảo
- Hình ảnh đính kèm từ người dùng.
- Kết quả search regex trong dự án.
