# Kế hoạch kiểm soát Separation UI/Logic cho WinForms (Forms & UserControl)

**Ngày:** 2026-05-26 00:44  
**Người dùng:** [Chủ dự án]  
**Mục tiêu:** Đảm bảo mọi Form/UserControl đều tách biệt khởi tạo UI (Designer) và logic (.cs) đúng chuẩn WinForms.

---

## Các bước kiểm soát

- [x] Kiểm tra các Form chính (`Form1`, `FrmAuth`, `FrmMultiRole`, `FrmAdmin`, ...)
- [x] Kiểm tra các UserControl lớn (`ucBooking`, `ucTripCard`, ...)
- [x] Kiểm tra các UserControl nhỏ (sample: không phát hiện vi phạm)
- [x] Đánh giá các trường hợp tạo control động (NotifyIcon, Timer, Dialog): hợp lệ nếu là UI tạm thời, không phải layout cố định.
- [ ] Nếu phát hiện vi phạm, lập danh sách file cần refactor.

---

## Quy tắc kiểm soát

- Mọi thao tác khởi tạo control, set thuộc tính layout, đăng ký event UI phải nằm trong `.Designer.cs`.
- File `.cs` chỉ chứa:
    - Logic xử lý, event handler.
    - Dependency injection, truyền service/data.
    - Không được tạo control/layout động (trừ UI tạm thời như NotifyIcon, Dialog, Timer).
- Khi thêm mới Form/UserControl, bắt buộc kiểm tra lại separation UI/Logic.

---

## Kết quả rà soát 2026-05-26

- Dự án hiện tại đã tuân thủ tốt separation UI/Logic.
- Không phát hiện vi phạm nghiêm trọng.
- Một số control động (NotifyIcon, Timer) là hợp lệ.
- Nếu phát hiện vi phạm mới, cập nhật checklist này và refactor lại theo đúng chuẩn.

---

## Kiểm chứng

- Test: Review code, kiểm tra `.cs` không có new Control/add/remove Controls động cho layout.
- Lint: Không có tool tự động, kiểm tra thủ công.
- Build: Không ảnh hưởng build.

---

## Rủi ro

- Thay đổi lớn có thể ảnh hưởng UI nếu refactor sai.
- Dễ vi phạm khi thêm mới UserControl/Form nếu không kiểm soát.

---

## Nguồn tham khảo

- [Microsoft Docs: Partial Classes and WinForms Designer](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/walkthrough-creating-a-windows-forms-control-at-design-time)
- [Cline .clinerules/knowledge.md]