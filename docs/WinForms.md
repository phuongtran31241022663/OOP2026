# Windows Forms trong C#

## 1. Kiến trúc nền tảng

Windows Forms hoạt động dựa trên **mô hình hướng sự kiện** và được xây dựng như một lớp bọc (wrapper) xung quanh Windows API, cung cấp môi trường phát triển nhanh chóng cho ứng dụng desktop trên Windows.

### Message Pump (Vòng lặp thông điệp)

Mọi ứng dụng WinForms đều dựa trên `Application.Run()` — một vòng lặp liên tục nhận thông điệp từ hệ điều hành (click chuột, gõ phím) và chuyển đổi chúng thành các sự kiện C#.

### Vấn đề UI bị "đơ" (Blocking)

Tất cả code xử lý giao diện đều chạy trên cùng **một luồng chính (UI thread)**. Bất kỳ tác vụ nặng nào (truy vấn DB, đọc/ghi file, gọi API) sẽ chặn vòng lặp thông điệp, khiến giao diện treo cứng cho đến khi tác vụ hoàn thành.

---

## 2. Giải pháp: Lập trình bất đồng bộ

| Công cụ | Mô tả | Khi nào dùng |
|---|---|---|
| `async/await` + `InvokeAsync` | Mô hình bất đồng bộ hiện đại — `await` giải phóng UI thread; `Control.InvokeAsync` cập nhật UI an toàn từ tác vụ nền | Tiêu chuẩn vàng cho mọi tác vụ I/O-bound |
| `BackgroundWorker` | Component từ .NET Framework 2.0 — có sẵn `ProgressChanged`, `RunWorkerCompleted` | Phù hợp tác vụ CPU-bound đơn giản hoặc code cũ; `async/await` được ưu tiên hơn |

> **Không khuyến khích:** `Application.DoEvents()` trong vòng lặp dài — tiềm ẩn nhiều rủi ro và lỗi khó lường.

---

## 3. Cải tiến đáng giá trong .NET 9

### Dark Mode

```csharp
// Thiết lập ứng dụng luôn dùng Dark Mode
Application.SetColorMode(SystemColorMode.Dark);
```

### Loại bỏ BinaryFormatter

`BinaryFormatter` (vốn dùng cho clipboard, drag-and-drop nội bộ) đã bị loại bỏ khỏi .NET 9 vì lý do bảo mật. Windows Forms tự động chuyển sang phương thức an toàn hơn.

---

## 4. Tối ưu hiệu năng

### Quản lý vòng đời và tài nguyên

- **Luôn hủy đăng ký sự kiện** khi form đóng — tránh rò rỉ bộ nhớ.
- Với đối tượng đồ họa (`Font`, `Pen`, `Brush`) — dùng khối `using` hoặc gọi `Dispose()` ngay sau khi dùng vì chúng dùng tài nguyên GDI+ **unmanaged**.

```csharp
protected override void OnPaint(PaintEventArgs e)
{
    using (Pen myPen = new Pen(Color.Red, 2))
    {
        e.Graphics.DrawRectangle(myPen, 1, 1, this.Width - 3, this.Height - 3);
    }
}
```

### Tối ưu UI Render

- Tránh lồng nhiều `Panel` vào nhau — càng sâu càng chậm.
- `DataGridView` với dữ liệu lớn — dùng phân trang hoặc **virtual mode** thay vì tải tất cả.
- Tạm dừng vẽ lại khi cập nhật hàng loạt:

```csharp
panel.SuspendLayout();
// ... thêm/sửa nhiều control ...
panel.ResumeLayout();
```

---

## 5. Tổ chức code — Viết ứng dụng dễ bảo trì

### Tuân thủ DRY

```csharp
// Tạo hàm dùng chung thay vì copy-paste
private bool IsFilled(TextBox txt, string fieldName)
{
    if (string.IsNullOrWhiteSpace(txt.Text))
    {
        MessageBox.Show($"{fieldName} không được để trống.");
        return false;
    }
    return true;
}
```

### Phân tách UI và Logic

Không nhồi mọi thứ vào code-behind của Form. Tạo **Helper Class** hoặc **Service Class** để chứa logic nghiệp vụ, validation, tính toán — giúp code dễ kiểm thử (unit test) mà không cần khởi chạy giao diện.

---

## 6. Custom Painting với GDI+

Khi các control có sẵn không đáp ứng yêu cầu thẩm mỹ, override `OnPaint` để vẽ tùy biến:

```csharp
protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);                // Vẽ các phần cơ bản
    Graphics g = e.Graphics;

    using (Pen myPen = new Pen(Color.Red, 2))
    {
        g.DrawRectangle(myPen, 1, 1, this.Width - 3, this.Height - 3);
    }
}
```

> **OnPaint là trái tim của custom drawing.** Mọi thứ nhìn thấy trên Form đều được vẽ ra — đây là đa hình runtime khi hệ thống gọi `control.OnPaint()`.

---

## 7. Triển khai ứng dụng

| Phương pháp | Mô tả |
|---|---|
| **ClickOnce** | Publish lên web server hoặc file share; tự động kiểm tra và cập nhật phiên bản khi khởi chạy |
| **MSIX** | Giải pháp đóng gói hiện đại được Microsoft khuyên dùng — kết hợp ưu điểm của MSI, ClickOnce và App-V; dễ tạo qua Visual Studio |

---

## 8. Khi nào dùng WinForms

**Nên dùng:**
- Ứng dụng doanh nghiệp (LOB — Line of Business).
- Công cụ nội bộ, tiện ích hệ thống cần tương tác sâu với Windows API.
- Ứng dụng desktop nhỏ gọn, cần khởi động nhanh.

**Nên cân nhắc thay thế:**
- Giao diện cực kỳ phức tạp, giàu hoạt ảnh, hiệu ứng → **WPF**.
- Cần chạy đa nền tảng (Windows, macOS, Linux) → **.NET MAUI** hoặc **Avalonia**.
