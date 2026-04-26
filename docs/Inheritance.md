# Tính đóng gói (Encapsulation) trong C#

## 1. Bản chất của đóng gói

Đóng gói là cơ chế gói gọn dữ liệu (fields) và phương thức (methods) vào một đơn vị duy nhất (`class/struct`), đồng thời hạn chế quyền truy cập trực tiếp từ bên ngoài.

Mục tiêu cốt lõi:
- **Bảo vệ tính toàn vẹn dữ liệu** — ngăn chặn trạng thái không hợp lệ.
- **Che giấu chi tiết triển khai** — thay đổi nội bộ không ảnh hưởng mã khách hàng.
- **Kiểm soát tương tác** — quy định rõ "cửa" (phương thức, thuộc tính) bên ngoài có thể dùng.
- **Hỗ trợ bảo trì và mở rộng** — giảm coupling giữa các phần hệ thống.

---

## 2. Bộ công cụ đóng gói trong C#

### 2.1 Access Modifiers — Tường rào kiểm soát truy cập

| Modifier | Cùng class | Dẫn xuất (cùng assembly) | Dẫn xuất (khác assembly) | Không kế thừa (cùng assembly) | Không kế thừa (khác assembly) |
|---|:---:|:---:|:---:|:---:|:---:|
| `public` | ✓ | ✓ | ✓ | ✓ | ✓ |
| `protected internal` | ✓ | ✓ | ✓ | ✓ | ✗ |
| `internal` | ✓ | ✓ | ✗ | ✓ | ✗ |
| `protected` | ✓ | ✓ | ✓ | ✗ | ✗ |
| `private protected` | ✓ | ✓ (cùng assembly) | ✗ | ✗ | ✗ |
| `private` | ✓ | ✗ | ✗ | ✗ | ✗ |

```csharp
public class BaseClass
{
    private protected int _secretValue = 42; // Chỉ dành cho dẫn xuất trong cùng assembly
}
```

### 2.2 Properties — Cánh cửa đóng gói

Properties trông như field nhưng thực chất là phương thức (getter/setter), cho phép thêm logic kiểm tra:

```csharp
public class Account
{
    private decimal _balance;

    public decimal Balance
    {
        get => _balance;
        private set
        {
            if (value < 0) throw new ArgumentException("Balance cannot be negative");
            _balance = value;
        }
    }

    public void Deposit(decimal amount) => Balance += amount;
}
```

- **Auto-properties** (`{ get; set; }`) tự sinh field ẩn, giảm boilerplate.
- **Expression-bodied** (`=>`) làm code sạch sẽ.

### 2.3 Indexers — Đóng gói dạng chỉ mục

```csharp
public class Week
{
    private readonly string[] _days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

    public string this[int index]
    {
        get => (index >= 0 && index < _days.Length)
            ? _days[index]
            : throw new IndexOutOfRangeException();
    }
}
```

### 2.4 Partial Classes — Phân tán nhưng vẫn đóng gói

Partial class chia nhỏ định nghĩa lớp ra nhiều file (thường dùng với designer code). Dù ở nhiều file, vẫn là cùng một đơn vị đóng gói và có quyền truy cập đầy đủ các thành viên `private` của nhau.

### 2.5 `internal` và `InternalsVisibleTo`

```csharp
[assembly: InternalsVisibleTo("MyApp.Tests")]
```

Vừa giữ đóng gói, vừa linh hoạt cho kiểm thử mà không phơi bày ra public.

---

## 3. Đóng gói hướng tới bất biến (Immutability)

- `readonly` fields: chỉ gán được trong constructor hoặc lúc khai báo.
- `readonly struct`: đảm bảo toàn bộ struct là bất biến (C# 8+).
- `record`: đóng gói immutable data tự nhiên, hỗ trợ `with` để tạo bản sao đã sửa đổi.

---

## 4. Đóng gói trong quan hệ OOP

| Tình huống | Cơ chế |
|---|---|
| Ngăn ghi đè tiếp | `sealed override` |
| Ẩn tường minh thành viên lớp cơ sở | `new` keyword (không khuyến khích lạm dụng) |
| Hạn chế dẫn xuất ở assembly khác | `private protected` |
| Abstract vs Interface | Abstract class có state (fields) + constructor — đóng gói cao hơn Interface thuần túy |

> **Nguyên tắc "Tell, Don't Ask"**: Không lấy dữ liệu ra để xử lý bên ngoài — hãy bảo đối tượng thực hiện hành vi. Đây là tận dụng đóng gói để giữ logic bên trong.

---

## 5. Đóng gói trong Serialization

| Vấn đề | Giải pháp |
|---|---|
| `private` setter không thể deserialize | `[JsonInclude]` hoặc `[JsonConstructor]` |
| Không muốn phơi bày thuộc tính | `[JsonIgnore]` |
| Bất biến (record) | `[JsonConstructor]` — tôn trọng đóng gói, không cần setter public |

---

## 6. Đóng gói trong Design Patterns

| Pattern | Đóng gói ở đâu |
|---|---|
| Strategy | Mỗi strategy đóng gói thuật toán, che giấu cách tính |
| State | Mỗi concrete state đóng gói hành vi, lộ ra qua interface chung |
| Observer | Subject đóng gói danh sách observer và logic thông báo |
| Singleton | Đóng gói constructor, quản lý instance duy nhất |

---

## 7. Best Practices

- **Luôn dùng properties thay vì public fields** — giữ nhất quán và cho phép thêm logic sau.
- **Không lộ `List<T>` dạng property chỉ đọc reference** — người dùng có thể gọi `.Add()`. Thay vào đó, dùng `IReadOnlyList<T>` hoặc trả về copy.
- **Tránh lạm dụng `protected`** — tạo liên kết chặt giữa lớp cơ sở và dẫn xuất. Ưu tiên `private protected`.
- **`sealed` cho class không được thiết kế để kế thừa** — cải thiện hiệu năng (devirtualization) và bảo vệ đóng gói.
- **Cẩn thận với `[JsonInclude]` trên private fields** — dễ vô tình phơi bày dữ liệu nhạy cảm.
- **Tận dụng `required`** (C# 11) — đảm bảo object luôn ở trạng thái hợp lệ ngay sau khởi tạo.
