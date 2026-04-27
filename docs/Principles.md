# Nguyên tắc lập trình (Principles)

## 1. Nguyên lý SOLID

### S — Single Responsibility Principle (SRP)

> Một lớp chỉ nên có **một trách nhiệm duy nhất**.

Nếu lớp thay đổi vì nhiều lý do khác nhau → vi phạm SRP. Tách thành nhiều lớp nhỏ, mỗi lớp một nhiệm vụ.

### O — Open/Closed Principle (OCP)

> **Mở rộng** tính năng bằng cách thêm mới; **không sửa đổi** code hiện hữu.

Dùng `abstract class`, `interface`, kế thừa và Strategy Pattern để mở rộng mà không phá vỡ code cũ.

### L — Liskov Substitution Principle (LSP)

> Lớp con có thể **thay thế lớp cha** mà không làm hỏng hệ thống.

Vi phạm điển hình: `Square : Rectangle` ghi đè `SetWidth/SetHeight` ép cả hai cạnh bằng nhau → hành vi bất ngờ khi code dùng kiểu `Rectangle`.

### I — Interface Segregation Principle (ISP)

> Chia nhỏ interface; không ép buộc lớp triển khai **các phương thức không dùng**.

Tránh "fat interface". Tạo nhiều interface nhỏ, chuyên biệt thay vì một interface lớn chứa tất cả.

### D — Dependency Inversion Principle (DIP)

> **Phụ thuộc vào abstraction (interface)**, không phụ thuộc vào lớp cụ thể (concrete class).

```csharp
// Vi phạm DIP
public class OrderService
{
    private readonly SqlOrderRepository _repo = new SqlOrderRepository(); // phụ thuộc concrete
}

// Tuân thủ DIP
public class OrderService
{
    private readonly IOrderRepository _repo; // phụ thuộc abstraction

    public OrderService(IOrderRepository repo) => _repo = repo; // inject từ bên ngoài
}
```

---

## 2. Nguyên tắc phổ biến khác

### DRY — Don't Repeat Yourself

> Không lặp lại code. Nếu một đoạn code được dùng lại, hãy đóng gói nó vào hàm hoặc lớp riêng.

```csharp
// Vi phạm DRY — copy paste
if (string.IsNullOrWhiteSpace(textBoxName.Text)) { ... }
if (string.IsNullOrWhiteSpace(textBoxEmail.Text)) { ... }

// Tuân thủ DRY
bool IsFilled(TextBox txt, string fieldName) => !string.IsNullOrWhiteSpace(txt.Text);
```

### KISS — Keep It Simple, Stupid

> Giữ cho code **đơn giản nhất có thể**. Code phức tạp khó bảo trì và sửa lỗi.

Đừng dùng Design Pattern chỉ vì muốn "trông chuyên nghiệp" khi giải pháp đơn giản đã đủ.

### YAGNI — You Ain't Gonna Need It

> Không viết chức năng khi **chưa cần thiết**. Tập trung vào yêu cầu hiện tại.

Tránh over-engineering — không tạo `IPaymentGateway`, `PaymentFactory`, `PaymentProxy` khi hệ thống chỉ cần thanh toán tiền mặt.

### LoD — Law of Demeter (Nguyên lý tối thiểu tri thức)

> Một đối tượng chỉ nên giao tiếp với **các đối tượng gần gũi**, không can thiệp quá sâu.

```csharp
// Vi phạm LoD
decimal price = order.GetCustomer().GetWallet().GetBalance();

// Tuân thủ LoD
decimal price = order.GetCustomerBalance();
```

---

## 3. Nguyên lý OOP (4 trụ cột)

| Nguyên lý | Mô tả |
|---|---|
| **Đóng gói** (Encapsulation) | Che giấu dữ liệu bên trong lớp, chỉ cho phép tương tác qua phương thức/property |
| **Kế thừa** (Inheritance) | Cho phép lớp con sử dụng lại thuộc tính/phương thức của lớp cha |
| **Đa hình** (Polymorphism) | Một tên phương thức có thể thể hiện các hành vi khác nhau ở các đối tượng khác nhau |
| **Trừu tượng** (Abstraction) | Tập trung vào đặc điểm chung, bỏ qua chi tiết triển khai cụ thể |

---

## 4. Quy ước đặt tên trong C# (Naming Conventions)

### Nguyên tắc chung (Clean Code)

- **Ý nghĩa rõ ràng**: Tránh `a`, `b`, `data`, `temp`. Tên phải mô tả đúng mục đích.
- **Dùng tiếng Anh**: Ngôn ngữ chuẩn trong lập trình.
- **Nhất quán**: Chọn một phong cách và tuân thủ trong toàn bộ dự án.
- **Danh từ/động từ**: Lớp/đối tượng nên là danh từ (`UserManager`); phương thức nên là động từ (`CalculateTotal`).

### Phong cách đặt tên trong C#

| Phong cách | Áp dụng cho | Ví dụ |
|---|---|---|
| `PascalCase` | Class, Struct, Enum, Interface, Method, Property, Namespace | `CustomerManager`, `CalculateTotal()`, `IsActive` |
| `camelCase` | Biến cục bộ, tham số | `userName`, `totalAmount`, `userId` |
| `_camelCase` | Private field | `_customerId`, `_balance` |
| `I` + `PascalCase` | Interface | `IRepository`, `IUserService` |
| `PascalCase` hoặc `SCREAMING_SNAKE_CASE` | Hằng số | `MaxValue`, `MAX_SIZE` |
| `Is`, `Has`, `Can` + `PascalCase` | Boolean property | `IsValid`, `HasError`, `CanExecute` |

### Quy tắc cú pháp

- Tên không được có dấu cách.
- Tên bắt đầu bằng chữ cái hoặc dấu `_`, không bắt đầu bằng số.
- Không dùng từ khóa của C# (`class`, `int`, `void`, `if`, ...).
- Không dùng tiếng Việt có dấu hoặc ký tự đặc biệt.

### Ví dụ tổng hợp

```csharp
namespace Company.Product.Module         // PascalCase namespace
{
    public interface IUserRepository { } // I + PascalCase

    public class UserService             // PascalCase class
    {
        private readonly IUserRepository _userRepo; // _camelCase field

        public UserService(IUserRepository userRepo) // camelCase param
        {
            _userRepo = userRepo;
        }

        public bool IsActive { get; private set; } // PascalCase, Is prefix

        public string GetFullName(string firstName, string lastName) // PascalCase method, camelCase params
        {
            string fullName = firstName + " " + lastName; // camelCase local var
            return fullName;
        }
    }
}
```
