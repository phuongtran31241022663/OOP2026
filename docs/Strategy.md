# Strategy Pattern trong C#

## 1. Khái niệm và mục đích

**Strategy Pattern** (mẫu chiến lược) là một mẫu thiết kế hành vi cho phép định nghĩa một họ các thuật toán, đóng gói mỗi thuật toán vào một lớp riêng biệt và làm cho chúng có thể hoán đổi cho nhau tại thời điểm chạy. Strategy giúp thay đổi hành vi của một đối tượng một cách độc lập với client sử dụng đối tượng đó.

Mục tiêu cốt lõi:
- Tách biệt phần thay đổi (thuật toán) ra khỏi phần ổn định (context).
- Tuân thủ nguyên lý **Open/Closed**: mở rộng bằng cách thêm strategy mới mà không sửa đổi context hay các strategy hiện có.
- Tránh các khối `switch-case` hoặc `if-else` dài dựa trên kiểu hay cờ quyết định hành vi.

> **Ví dụ kinh điển:** ứng dụng tính phí vận chuyển, nơi thuật toán tính phí thay đổi theo nhà vận chuyển (UPS, FedEx, VNPost...). Thay vì nhồi tất cả công thức vào một phương thức với điều kiện rẽ nhánh, Strategy cho phép mỗi nhà vận chuyển có một lớp riêng.

---

## 2. Cấu trúc cổ điển trong C#

Dựa trên mẫu GoF, Strategy Pattern gồm ba vai trò:

- **Strategy**: Interface hoặc abstract class khai báo phương thức thực thi thuật toán.
- **ConcreteStrategy**: Các lớp cụ thể triển khai interface Strategy, mỗi lớp một thuật toán.
- **Context**: Lớp sử dụng Strategy, duy trì một tham chiếu đến Strategy và ủy thác công việc cho nó.

### Ví dụ: Tính phí vận chuyển

```csharp
// 1. Strategy interface
public interface IShippingStrategy
{
    decimal CalculateCost(decimal weight, string origin, string destination);
}

// 2. Concrete strategies
public class UpsShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight, string origin, string destination)
        => weight * 2.5m + 10;
}

public class FedExShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight, string origin, string destination)
        => weight * 3.0m + 8;
}

public class VnPostShipping : IShippingStrategy
{
    public decimal CalculateCost(decimal weight, string origin, string destination)
        => weight * 1.8m + (destination.StartsWith("Hanoi") ? 5 : 12);
}

// 3. Context
public class ShippingCalculator
{
    private readonly IShippingStrategy _strategy;

    public ShippingCalculator(IShippingStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public decimal Calculate(decimal weight, string origin, string destination)
        => _strategy.CalculateCost(weight, origin, destination);
}
```

```csharp
// Sử dụng
IShippingStrategy strategy = new UpsShipping();
var calculator = new ShippingCalculator(strategy);
decimal cost = calculator.Calculate(2.5m, "HCMC", "Hanoi");
Console.WriteLine($"Shipping cost: {cost}");
```

---

## 3. Sự tiến hóa với C# hiện đại

### 3.1 Dùng `delegate` / `Action` / `Func` thay cho Interface riêng

Khi strategy chỉ có một phương thức duy nhất, có thể dùng `Func<>` thay vì tạo interface và class riêng:

```csharp
public class ShippingCalculator
{
    private readonly Func<decimal, string, string, decimal> _calculateFunc;

    public ShippingCalculator(Func<decimal, string, string, decimal> calculateFunc)
        => _calculateFunc = calculateFunc;

    public decimal Calculate(decimal weight, string origin, string dest)
        => _calculateFunc(weight, origin, dest);
}

// Sử dụng
var upsStrategy = (decimal w, string o, string d) => w * 2.5m + 10;
var calculator = new ShippingCalculator(upsStrategy);
```

### 3.2 Strategy với Generic và Constraint

```csharp
public class ShippingCalculator<TStrategy> where TStrategy : IShippingStrategy, new()
{
    private readonly TStrategy _strategy = new();

    public decimal Calculate(decimal weight, string origin, string dest)
        => _strategy.CalculateCost(weight, origin, dest);
}
// Sử dụng: new ShippingCalculator<UpsShipping>().Calculate(...)
```

### 3.3 Strategy với Dependency Injection

```csharp
// Đăng ký
services.AddTransient<IShippingStrategy, UpsShipping>();
services.AddTransient<IShippingStrategy, FedExShipping>();
services.AddTransient<IShippingStrategy, VnPostShipping>();

// Context
public class ShippingCalculator
{
    private readonly IEnumerable<IShippingStrategy> _strategies;

    public ShippingCalculator(IEnumerable<IShippingStrategy> strategies)
        => _strategies = strategies;

    public decimal Calculate(string carrier, decimal weight, string origin, string dest)
    {
        var strategy = _strategies.FirstOrDefault(s => s.CarrierName == carrier);
        if (strategy == null) throw new ArgumentException($"Unknown carrier: {carrier}");
        return strategy.CalculateCost(weight, origin, dest);
    }
}
```

### 3.4 Pattern matching để lựa chọn strategy

```csharp
public record Order(decimal Amount, CustomerTier Tier);

public decimal CalculateDiscount(Order order) => order.Tier switch
{
    CustomerTier.Regular => order.Amount * 0.05m,
    CustomerTier.Premium => order.Amount * 0.10m,
    CustomerTier.VIP     => order.Amount * 0.15m,
    _                    => 0
};
```

---

## 4. So sánh với các mẫu liên quan

### Strategy vs State

| Tiêu chí | Strategy | State |
|---|---|---|
| Ai chọn thuật toán? | Client / bên ngoài | Bản thân đối tượng (từ bên trong) |
| Các strategy có biết nhau không? | Không | Có thể biết nhau và tự chuyển đổi |
| Thay đổi runtime? | Có thể, theo ý muốn tường minh | Tự động theo điều kiện trạng thái |

### Strategy vs Template Method

| Tiêu chí | Strategy | Template Method |
|---|---|---|
| Cơ chế | Delegation (composition) | Kế thừa (inheritance) |
| Thay đổi thuật toán | Tại runtime | Tại compile-time (chọn subclass) |
| Nguyên lý | Composition over Inheritance | Kế thừa có cấu trúc |

---

## 5. Các biến thể nâng cao

### 5.1 Strategy Factory

```csharp
public interface IShippingStrategyFactory
{
    IShippingStrategy Create(string carrierName);
}
```

### 5.2 Default Implementation trong Interface (C# 8+)

```csharp
public interface IShippingStrategy
{
    decimal CalculateCost(decimal weight, string origin, string dest);
    decimal CalculateInsurance(decimal value) => value * 0.01m; // mặc định 1%
}
```

---

## 6. Ứng dụng thực tế trong .NET

| Kịch bản | Strategy qua |
|---|---|
| Sắp xếp danh sách | `IComparer<T>`, `OrderBy(Func<>)` |
| Xác thực dữ liệu | FluentValidation validators |
| Gửi email | `IEmailService` → SmtpClient, SendGrid, MailChimp |
| Thanh toán | `IPaymentGateway` → Paypal, Stripe, Momo |
| Nén dữ liệu | `ICompressionStrategy` → GZip, Brotli, None |
| ASP.NET Core | `UseWhen`, `MapWhen` → branch theo predicate |

---

## 7. Best Practices

1. **Ưu tiên `delegate` cho strategy đơn giản** — nếu strategy chỉ là một hàm, không có trạng thái.
2. **Không lạm dụng** — nếu chỉ có 2 lựa chọn và ít thay đổi, `if-else` vẫn đủ.
3. **Vòng đời DI** — strategy không trạng thái nên là `Singleton`; có trạng thái dùng `Transient`.
4. **Đặt tên rõ ràng** — `UpsShipping`, `FedExShipping` thay vì `Strategy1`, `Strategy2`.
5. **Dùng Factory để chọn strategy** — tránh để context chứa logic phức tạp để chọn.
6. **Null Object Pattern** — cung cấp strategy mặc định thay vì để trống.
7. **Strategy không biết quá nhiều về context** — nhận đủ dữ liệu qua tham số, tránh phụ thuộc ngược.

---

## 8. Kết luận

Strategy Pattern trong C# đã phát triển vượt xa mẫu thiết kế nguyên thủy nhờ delegate, generic, dependency injection và record. Sự kết hợp giữa Strategy và các nguyên lý **Composition over Inheritance**, **DI**, **Open/Closed** đã khiến nó trở thành một trong những mẫu linh hoạt nhất trong hệ sinh thái .NET — hiện diện khắp nơi từ sắp xếp, xác thực, thanh toán đến pipeline xử lý HTTP.
