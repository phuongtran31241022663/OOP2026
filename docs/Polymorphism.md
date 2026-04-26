# Tính đa hình (Polymorphism) trong C#

## 1. Đa hình thời gian chạy (Run-time Polymorphism)

Hành vi thực sự được quyết định tại thời điểm thực thi, dựa trên kiểu thực tế của đối tượng.

### 1.1 Phương thức ảo, ghi đè và lớp trừu tượng

```csharp
public abstract class Animal
{
    public string Name { get; set; }
    public abstract string MakeSound();               // Bắt buộc override
    public virtual string Describe() => $"I am {Name}"; // Có thân mặc định
}

public class Dog : Animal
{
    public override string MakeSound() => "Woof";
    public override string Describe() => $"I am a dog named {Name}";
}

public class Cat : Animal
{
    public override string MakeSound() => "Meow";
    // Không ghi đè Describe → dùng hành vi mặc định từ Animal
}
```

```csharp
Animal a = new Dog { Name = "Rex" };
Console.WriteLine(a.MakeSound()); // "Woof"   — đa hình runtime
Console.WriteLine(a.Describe());  // "I am a dog named Rex"
```

> **Cơ chế VTable:** Mỗi lớp có một bảng chứa địa chỉ các phương thức ảo. CLR tra vtable của đối tượng thực tế khi gọi phương thức ảo.

### 1.2 Interface — Đa hình thông qua hợp đồng

```csharp
public interface IFlyable  { void Fly(); }
public interface ISwimmable { void Swim(); }

public class Duck : Animal, IFlyable, ISwimmable
{
    public override string MakeSound() => "Quack";
    public void Fly()  => Console.WriteLine("Duck flying");
    public void Swim() => Console.WriteLine("Duck swimming");
}
```

`Duck` có thể được dùng ở bất kỳ đâu cần `IFlyable` hoặc `ISwimmable`.

### 1.3 `sealed override` — Điểm dừng của chuỗi đa hình

```csharp
public class Wolf : Dog
{
    // Lớp cháu không thể ghi đè MakeSound nữa
    public sealed override string MakeSound() => "Howl";
}
```

Vừa bảo vệ tính toàn vẹn, vừa cho phép JIT **devirtualize** các lời gọi đến `MakeSound` trên kiểu `Wolf`.

---

## 2. Đa hình thời gian biên dịch (Compile-time Polymorphism)

Hành vi được quyết định ngay khi biên dịch.

### 2.1 Nạp chồng phương thức (Method Overloading)

```csharp
public class Calculator
{
    public int    Add(int a, int b)          => a + b;
    public double Add(double a, double b)    => a + b;
    public int    Add(int a, int b, int c)   => a + b + c;
}
```

### 2.2 Nạp chồng toán tử (Operator Overloading)

```csharp
public readonly struct Complex
{
    public double Real { get; }
    public double Imag { get; }

    public Complex(double r, double i) => (Real, Imag) = (r, i);

    public static Complex operator +(Complex a, Complex b)
        => new Complex(a.Real + b.Real, a.Imag + b.Imag);

    public override string ToString() => $"{Real}+{Imag}i";
}

// Dùng: var c = a + b;
```

### 2.3 Đa hình tham số (Generic)

```csharp
public T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) > 0 ? a : b;
```

**Covariance & Contravariance:**

- `IEnumerable<string>` → `IEnumerable<object>` (**covariant** — `out T`).
- `Action<object>` → `Action<string>` (**contravariant** — `in T`).

---

## 3. Đa hình trong Design Patterns

| Pattern | Vai trò của đa hình |
|---|---|
| Strategy | Context gọi `IStrategy.Execute()` → ConcreteStrategy thực thi |
| State | Context ủy thác cho `IState` → hành vi thay đổi nhờ đa hình |
| Observer | `delegate` gọi nhiều phương thức khác nhau (subscriber) |

---

## 4. Đa hình trong Serialization

Khi serialize đối tượng có kiểu khai báo là lớp cơ sở nhưng thực tế là lớp dẫn xuất, cần **discriminator** để deserialize đúng kiểu:

```csharp
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
public abstract class Animal { }
```

---

## 5. Đa hình trong UI (Windows Forms)

Mọi control đều kế thừa từ `Control`, và thường ghi đè `OnPaint` để vẽ tùy biến. Khi hệ thống gọi `control.OnPaint()`, nó dùng virtual dispatch để chạy code vẽ — đây chính là đa hình.

---

## 6. Cạm bẫy thường gặp

- **Vi phạm LSP**: Lớp dẫn xuất thay đổi hành vi kỳ vọng của lớp cơ sở. Ví dụ: `Square : Rectangle` override `SetWidth/SetHeight` ép cả hai cạnh bằng nhau → vi phạm LSP.
- **Lạm dụng `is/as`**: Kiểm tra kiểu thường xuyên làm mất đi vẻ đẹp của đa hình. Ưu tiên đưa hành vi vào phương thức ảo.
- **Virtual method trong constructor**: Không bao giờ gọi phương thức ảo từ constructor lớp cơ sở — lớp dẫn xuất chưa được khởi tạo hoàn toàn.
- **Hiệu năng**: `sealed` trên lớp/phương thức cuối cùng giúp JIT tối ưu (devirtualization).

---

## 7. Kết luận

Đa hình trong C# là sự tổng hòa của nhiều cơ chế: phương thức ảo, interface, generic, pattern matching. Nó thấm sâu vào mọi ngóc ngách của lập trình .NET:

- Cho phép viết code **mở, dễ mở rộng, đóng với sửa đổi** (Open/Closed Principle).
- Áp dụng **Dependency Injection** hiệu quả.
- Kết hợp nhuần nhuyễn các công nghệ như GMap.NET, WinForms, async/await.

> **Nguyên tắc:** Hãy chọn đúng hình thức đa hình cho từng bài toán, và để các nguyên lý SOLID dẫn đường cho thiết kế.
