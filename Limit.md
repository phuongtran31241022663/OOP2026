# ⚠️ GIỚI HẠN KỸ THUẬT CỦA DỰ ÁN OOP2026

> **Áp dụng cho tất cả code, tài liệu, và câu trả lời từ AI.**
> Mọi đề xuất vi phạm các giới hạn này đều không được chấp nhận.

---

## 1. Môi trường & Phiên bản

- **.NET Framework:** 4.8 (không phải .NET Core/.NET 5+)
- **Ngôn ngữ C#:** 7.3 (mặc định của .NET Framework 4.8)
- **IDE:** Visual Studio 2019/2022 (có hỗ trợ .NET Framework 4.8)
- **Hệ điều hành:** Windows (WinForms)

**Lưu ý:** Tuyệt đối không thay đổi `TargetFrameworkVersion` hoặc `LangVersion` trong file `.csproj`.

---

## 2. Các tính năng C# bị CẤM (từ C# 8.0 trở lên)

| Phiên bản | Tính năng / Từ khóa cần tránh |
|-----------|-------------------------------|
| **C# 8.0** | `Nullable reference types` (`string?`, `#nullable enable`), `switch expression` (`x switch { ... }`), `using var`, `await foreach`, `IAsyncEnumerable<T>`, `^1`, `..`, `??=`, `static local functions`, default interface methods |
| **C# 9.0** | `record`, `init`, `with`, `target-typed new`, pattern matching với `and`/`or`/`not`, top-level statements |
| **C# 10.0** | `global using`, file-scoped namespaces (`namespace X;`), `record struct` |
| **C# 11.0** | `required`, raw string literals (`"""`), list patterns, generic attributes |
| **C# 12.0** | Primary constructors (`class X(string s) {}`), collection expressions (`[1,2,3]`) |

**Nguyên tắc chung:** Nếu bạn (hoặc AI) thấy một từ khóa lạ, hãy kiểm tra xem nó có thuộc C# 8.0+ không. Nếu có → **không dùng**.

---

## 3. Các mẫu thiết kế (Design Patterns) & Thư viện bị CẤM

| Cấm dùng | Chi tiết |
|-----------|----------|
| **Dependency Injection** (DI) | Không sử dụng `Microsoft.Extensions.DependencyInjection`, `IServiceCollection`, `IServiceProvider`, Autofac, Ninject, v.v. <br>Không dùng constructor injection, property injection, method injection với mục đích DI. <br>Các dependency được phép `new` trực tiếp hoặc truyền qua constructor nhưng do chính code tự quản lý (không container). |
| **LINQ** | Không dùng `Where`, `Select`, `OrderBy`, `GroupBy`, `Join`, `Aggregate`, v.v. (chỉ dùng vòng lặp `foreach` truyền thống) |
| **`var`** | Không dùng `var` để khai báo biến (phải khai báo kiểu tường minh) |

---

## 4. Quy tắc chung cho AI và người đọc

1. **Mọi thay đổi file `.csproj` hoặc `.sln` phải được phê duyệt thủ công.** Không tự ý thêm package NuGet, thay đổi `LangVersion`, `TargetFrameworkVersion`.
2. **Code sinh ra phải tương thích C# 7.3.** Biên dịch được trên .NET Framework 4.8.
3. **Không dùng bất kỳ từ khóa nào có trong danh sách cấm ở trên.**
4. **Nếu không chắc chắn, hãy hỏi lại trước khi thêm code.**
5. **Tài liệu tham khảo (như bài giảng có chứa DI, C# 9, LINQ) chỉ để đọc hiểu, không được áp dụng vào dự án này.**

---

## 5. Cách kiểm tra nhanh

- Mở file `.csproj`, đảm bảo không có dòng `<LangVersion>` (hoặc nếu có thì phải là `7.3`).
- Sau khi build, kiểm tra cảnh báo (warning) và lỗi (error) liên quan đến phiên bản ngôn ngữ.