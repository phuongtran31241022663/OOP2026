# Lập trình hướng đối tượng (OOP) trong C#

## 1. Tổng quan

C# do Microsoft phát triển, được thiết kế là một ngôn ngữ thuần hướng đối tượng từ lõi. Mọi thứ trong C# đều là đối tượng, kể cả các kiểu dữ liệu cơ sở (thông qua cơ chế boxing/unboxing).

OOP trong C# xoay quanh `class`, `struct`, `interface`, `record` và các mối quan hệ giữa chúng. Triết lý OOP được thể hiện qua **bốn trụ cột cơ bản**: Đóng gói, Kế thừa, Đa hình, Trừu tượng.

---

## 2. Bốn trụ cột OOP trong C#

### 2.1 Đóng gói (Encapsulation) — Kiểm soát truy xuất và bảo vệ trạng thái

Đóng gói không chỉ là ẩn dữ liệu bằng `private`. Trong C#, nó bao gồm:

- **Access Modifiers**: `public`, `private`, `protected`, `internal`, `protected internal`, `private protected`.
- **Properties**: truy xuất dữ liệu như trường nhưng thực chất là phương thức.

```csharp
public class Account
{
    public decimal Balance { get; private set; } // chỉ đọc từ bên ngoài

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException();
        Balance += amount;
    }
}
```

- **Encapsulation ở mức assembly**: `internal` để ẩn chi tiết triển khai giữa các assembly.

### 2.2 Kế thừa (Inheritance) — Chia sẻ hành vi và phân cấp

C# chỉ hỗ trợ **đơn kế thừa lớp** nhưng đa kế thừa hành vi thông qua interface.

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("...");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Woof");
}

class Cat : Animal
{
    public new void Speak() => Console.WriteLine("Meow"); // Không đa hình!
}

Animal a = new Cat();
a.Speak(); // "..." — vì Cat dùng new, không override
```

| Từ khóa | Ý nghĩa |
|---|---|
| `virtual` | Phương thức có thể ghi đè |
| `override` | Ghi đè tường minh — tạo đa hình |
| `new` | Ẩn phương thức cơ sở — không đa hình |
| `sealed override` | Ngăn ghi đè tiếp theo |
| `abstract` | Không có thân — bắt buộc override |

### 2.3 Đa hình (Polymorphism) — Linh hoạt tại thời gian chạy

**Compile-time polymorphism:**
- Nạp chồng phương thức (overloading).
- Nạp chồng toán tử (operator overloading).
- Generic (đa hình tham số).

**Run-time polymorphism:**
- Dựa trên phương thức ảo — hành vi quyết định bởi kiểu thực tế của đối tượng.
- Dựa trên interface — một lớp có nhiều "bộ mặt" khác nhau.

### 2.4 Trừu tượng (Abstraction) — Định nghĩa hợp đồng, ẩn chi tiết

| Công cụ | Đặc điểm |
|---|---|
| `abstract class` | Có thể có trạng thái (fields), constructor, triển khai một phần |
| `interface` | Chỉ hành vi thuần túy (đến C# 7); từ C# 8 có default implementation |

> **Nguyên tắc phổ biến:** Ưu tiên interface, vì C# chỉ có đơn kế thừa và interface cho phép mô-đun hóa tốt hơn.

---

## 3. Các cơ chế OOP nâng cao đặc trưng trong C#

### 3.1 Generic và Đa hình tham số

```csharp
// Ràng buộc constraint
public T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) > 0 ? a : b;
```

- **Covariance** (`out T`): `IEnumerable<string>` có thể gán cho `IEnumerable<object>`.
- **Contravariance** (`in T`): `Action<object>` có thể nhận `Action<string>` (ngược chiều).

### 3.2 Delegates và Events — Hướng đối tượng cho hành vi

- `delegate` là kiểu đối tượng trỏ tới phương thức, hỗ trợ multicast.
- `event` là cơ chế đóng gói delegate — ngăn gán lại từ bên ngoài.
- Các delegate generic có sẵn: `Action`, `Func`, `Predicate`.

### 3.3 LINQ và phong cách hướng đối tượng

LINQ biến thao tác dữ liệu thành biểu thức truy vấn trực tiếp trong C#. Cùng một câu LINQ có thể chạy trên mảng, list, database thông qua `IQueryProvider` — đây là đa hình tại runtime.

---

## 4. Nguyên lý SOLID và cách C# hỗ trợ

| Nguyên lý | Viết tắt | C# hỗ trợ |
|---|---|---|
| Single Responsibility | SRP | `Partial class`, namespace, tổ chức file riêng biệt |
| Open/Closed | OCP | `abstract class`, `interface`, `default interface methods` (C# 8) |
| Liskov Substitution | LSP | Hệ thống kiểu mạnh, covariance/contravariance, cảnh báo khi dùng `new` thay `override` |
| Interface Segregation | ISP | Nhiều interface nhỏ thay vì một "fat interface" |
| Dependency Inversion | DIP | Dependency Injection, DI Container tích hợp (.NET Core) |

---

## 5. Kết luận

Lập trình hướng đối tượng trong C# là một hệ sinh thái phong phú, vượt xa các khái niệm sách giáo khoa. Để viết code OOP chuyên sâu, cần nắm vững:

- Các cơ chế đóng gói, kế thừa, đa hình, trừu tượng ở mức triển khai thực tế.
- Khai thác các nguyên lý SOLID.
- Áp dụng đúng mô hình thiết kế (Strategy, State, Observer, Repository...).
- Sử dụng các cải tiến hiện đại: `record`, `init-only`, pattern matching, generic constraints.
