# Kế hoạch sửa FrmAuth để chuyển hướng Form theo vai trò

**Ngày:** 2026-05-27
**Người dùng:** PC
**Mục tiêu:** Sửa `FrmAuth.cs` để mở `FrmPassenger` hoặc `FrmDriver` khi đăng nhập thành công hoặc đăng ký thành công (tự động đăng nhập). Hỗ trợ phím Enter.

## Các bước

- [ ] Bước 1: Cập nhật constructor của `FrmAuth` để nhận tất cả các service cần thiết từ `Program.cs`.
- [ ] Bước 2: Thêm phương thức `NavigateToRoleForm(Usr user)` vào `FrmAuth` để xử lý việc khởi tạo và hiển thị Form tương ứng với vai trò của người dùng.
- [ ] Bước 3: Cập nhật `OnLoginClicked` để gọi `NavigateToRoleForm` khi đăng nhập thành công.
- [ ] Bước 4: Cập nhật `OnRegisterClicked` để gọi `NavigateToRoleForm` sau khi đăng ký và tự động đăng nhập thành công.
- [ ] Bước 5: Cập nhật `Program.cs` để truyền đầy đủ các instance của service vào `FrmAuth`.
- [ ] Bước 6: Kiểm tra phím Enter (đã có logic `OnAuthKeyDown` gọi `PerformClick`, cần đảm bảo nó hoạt động đúng với logic mới).

## Kiểm chứng

- Test: Chạy ứng dụng, đăng nhập bằng tài khoản Passenger/Driver/Admin demo và kiểm tra xem Form tương ứng có mở ra không.
- Test: Đăng ký tài khoản mới và kiểm tra xem có tự động chuyển vào Form chính không.
- Test: Nhấn Enter trong các ô nhập liệu để kiểm tra tính năng đăng nhập/đăng ký bằng phím.

## Rủi ro

- Thiếu service dependency: Mức độ Trung bình. Cần đảm bảo tất cả service được truyền đúng từ `Program.cs`.
- Lỗi ép kiểu (Casting): Mức độ Thấp. Cần kiểm tra kỹ kiểu của `Usr` trước khi ép sang `Psg`, `Drv`, hoặc `Adm`.

## Nguồn tham khảo

- `OOP2026/Form/FrmAuth.cs`
- `OOP2026/Program.cs`
- `OOP2026/Form/FrmPassenger.cs`
- `OOP2026/Form/FrmDriver.cs`
- `OOP2026/Form/FrmAdmin.cs`
