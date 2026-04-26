---

## 6. APM.md

```markdown
# Lập trình bất đồng bộ (Asynchronous Programming) trong C#

## 1. Tầm quan trọng của bất đồng bộ

Lập trình bất đồng bộ là kỹ thuật cho phép ứng dụng thực hiện công việc mà **không chặn luồng gọi** (calling thread). Điều này vô cùng quan trọng trong hai ngữ cảnh chính:

- **Ứng dụng giao diện (GUI)**: Giữ cho UI thread luôn sẵn sàng xử lý thông điệp từ người dùng. Chặn UI thread sẽ đóng băng giao diện, gây trải nghiệm tệ.
- **Dịch vụ máy chủ (server)**: Tối đa hóa thông lượng bằng cách không giữ luồng trong khi chờ I/O. Nếu một yêu cầu chặn luồng, server sẽ nhanh chóng cạn kiệt luồng, không thể phục vụ thêm yêu cầu dù CPU còn rất rảnh.

C# và .NET đã trải qua một hành trình dài từ những mô hình bất đồng bộ phức tạp đến `async`/`await` — một trong những trải nghiệm lập trình bất đồng bộ thân thiện nhất trong các ngôn ngữ hiện đại.

---

## 2. Lịch sử các mô hình bất đồng bộ trong .NET

### 2.1 APM (Asynchronous Programming Model)

Mẫu cũ với cặp phương thức `BeginXxx`/`EndXxx`, sử dụng `IAsyncResult`. Code rối, khó bảo trì.

```csharp
FileStream fs = File.OpenRead("data.bin");
byte[] buffer = new byte[1024];
fs.BeginRead(buffer, 0, buffer.Length, ar =>
{
    int bytesRead = fs.EndRead(ar);
    // Xử lý dữ liệu...
}, null);
```
