# OOP Course Summaries (B2 - B9)

## Table of Contents
- [B2. Classes & Objects](#b2-classes--objects--detailed-summary)
- [B3. Inheritance & Polymorphism](#b3-inheritance--polymorphism--detailed-summary)
- [B4. Interface & Operator Overloading](#b4-interface--operator-overloading--detailed-summary)
- [B5. Delegate and Event](#b5-delegate-and-event--detailed-summary)
- [B6. Generics & Collections](#b6-generics--collections--detailed-summary)
- [B7. Exceptions](#b7-exceptions--detailed-summary)
- [B8. Design Patterns](#b8-design-patterns-en--detailed-summary)
- [B9. Serialization](#b9-serialization--detailed-summary)

---

# B2. Classes & Objects – Detailed Summary

## 1. Giới thiệu chung
File này giới thiệu **lập trình hướng đối tượng (OOP)** trong C# với trọng tâm vào **class**, **object** và **namespace**. Nội dung được chia thành các phần: khái niệm, các thành phần của class, cách khai báo, khởi tạo, truy cập, và các tính năng nâng cao (constructor, destructor, static, access modifiers, property, method, event).

## 2. Khái niệm cơ bản
| Thuật ngữ | Định nghĩa |
|-----------|------------|
| **Class** | Mẫu (blueprint) mô tả **thuộc tính (field, property)** và **hành vi (method, event)** của một nhóm đối tượng. |
| **Object** | Thực thể (instance) được tạo ra từ class, có trạng thái riêng (giá trị các field). |
| **Namespace** | Không gian tên, dùng để **phân nhóm** các class, tránh xung đột tên. |

## 3. Các thành phần của class
1. **Fields (Biến thành viên)** – lưu trữ dữ liệu nội bộ, thường khai báo `private`.
2. **Properties** – cung cấp **cách truy cập** an toàn tới fields (getter/setter).  
   ```csharp
   private string _hoTen;
   public string HoTen
   {
       get => _hoTen;
       set => _hoTen = value;
   }
   ```
3. **Methods** – thực hiện hành động, có thể trả về giá trị hoặc `void`.
4. **Constructors** – khởi tạo đối tượng, có thể **overload** (nhiều constructor) và **chaining** (`: this(...)`).
5. **Destructors (Finalizers)** – được gọi khi GC thu dọn đối tượng, thường không cần viết trừ khi quản lý tài nguyên không quản lý.
6. **Events** – cơ chế publish/subscribe, thường dùng cùng **delegate**.
7. **Static members** – thuộc về **class** chứ không phải instance; dùng cho dữ liệu/chức năng chung.

## 4. Cú pháp khai báo class (C#)
```csharp
namespace MyApp.Models
{
    public class NhanVien
    {
        // Fields
        private string _hoTen;
        private DateTime _ngaySinh;
        private double _heSoLuong;
        private int _soCon;

        // Property (encapsulation)
        public string HoTen
        {
            get => _hoTen;
            set => _hoTen = value;
        }

        // Auto‑implemented property (C# 3.0+)
        public DateTime NgaySinh { get; set; }

        // Constructor (no‑arg)
        public NhanVien() { }

        // Constructor (parameterized)
        public NhanVien(string hoTen, double heSoLuong)
        {
            HoTen = hoTen;
            _heSoLuong = heSoLuong;
        }

        // Method
        public double TienLuong()
        {
            return _heSoLuong * 1_000_000;
        }

        // Static member
        public static int SoLuongNhanVien { get; private set; }

        // Static constructor (run once)
        static NhanVien()
        {
            SoLuongNhanVien = 0;
        }
    }
}
```

## 5. Access Modifiers (phạm vi truy cập)
| Modifier | Trong class | Trong class con | Bên ngoài |
|----------|-------------|----------------|-----------|
| `public` | ✓ | ✓ | ✓ |
| `private`| ✓ | ✗ | ✗ |
| `protected`| ✓ | **✓** | ✗ |
| `internal`| ✓ | ✓ (cùng assembly) | ✓ (cùng assembly) |
| `protected internal`| ✓ | ✓ | ✓ (cùng assembly) |

- **`protected`** cho phép class con truy cập thành viên nhưng không cho các code bên ngoài.
- **`internal`** giới hạn trong cùng **assembly**.
- **`protected internal`** là hợp nhất của hai trên.

## 6. Constructor & Destructor chi tiết
- **Constructor** không có kiểu trả về, tên trùng class. Có thể **overload** và **chaining**.
- **Destructor** khai báo `~ClassName()`; không thể overload, không có tham số, không gọi trực tiếp – chỉ được GC gọi.
- Khi cần giải phóng tài nguyên không quản lý, nên **implement `IDisposable`** và dùng `Dispose()` thay vì destructor.

## 7. Property nâng cao
- **Auto‑implemented** (`public int Id { get; set; }`) – compiler tự tạo backing field.
- **Read‑only** (`public int Id { get; }`) – chỉ có getter, giá trị chỉ được gán trong constructor.
- **Validation** trong setter:
  ```csharp
  public double HeSoLuong
  {
      get => _heSoLuong;
      set
      {
          if (value < 0) throw new ArgumentException("Hệ số lương không âm");
          _heSoLuong = value;
      }
  }
  ```

## 8. Event & Delegate (liên quan)
- **Event** khai báo: `public event EventHandler OnChanged;`
- **Delegate** định nghĩa chữ ký: `delegate void MyHandler(object sender, EventArgs e);`
- **Publisher** gọi `OnChanged?.Invoke(this, EventArgs.Empty);`
- **Subscriber** đăng ký: `obj.OnChanged += HandlerMethod;`

## 9. Ví dụ thực tế (đối tượng Xe ô tô)
```csharp
public class XeOto
{
    public string MaSoXe { get; set; }
    public string HieuXe { get; set; }
    public string MauSon { get; set; }
    public int NamSanXuat { get; set; }

    public void KhoiDong()
    {
        Console.WriteLine($"{HieuXe} ({MaSoXe}) đang khởi động.");
    }
}
```
- **Object**: `XeOto oto = new XeOto { MaSoXe = "123ABC", HieuXe = "Toyota", MauSon = "Đỏ", NamSanXuat = 2020 };`
- **Method call**: `oto.KhoiDong();`

## 10. Tổng kết
- **Class** là khung, **object** là thực thể.  
- **Encapsulation** được thực hiện qua **private fields + public properties**.  
- **Static**, **constructor**, **destructor**, **access modifiers** cung cấp kiểm soát sâu hơn về vòng đời và phạm vi.  
- Hiểu rõ các thành phần này là nền tảng để học **inheritance**, **interface**, **delegates**, **generics**, và **design patterns** trong các bài học tiếp theo.

---

# B3. Inheritance & Polymorphism – Detailed Summary

## 1. Bài này dạy gì?
Bài này giải thích 2 ý rất quan trọng trong OOP:

- **Kế thừa (Inheritance)**: tạo class mới từ class đã có
- **Đa hình (Polymorphism)**: cùng 1 lời gọi nhưng object khác nhau sẽ xử lý khác nhau

Nếu hiểu bài này, bạn sẽ hiểu vì sao OOP có thể:
- tái sử dụng code
- mở rộng hệ thống dễ hơn
- viết code linh hoạt hơn

---

## 2. Kế thừa là gì?
Kế thừa cho phép 1 class con lấy lại đặc điểm từ class cha.

- **Class cha**: base class / parent class
- **Class con**: derived class / child class

### Cú pháp
```csharp
class DerivedClass : BaseClass
{
}
```

### Ví dụ đơn giản
```csharp
class Animal
{
    public string Name { get; set; }

    public void Eat()
    {
        Console.WriteLine(Name + " is eating");
    }
}

class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine(Name + " is barking");
    }
}
```

### Giải thích
- `Dog` kế thừa `Animal`
- `Dog` dùng lại được `Name`
- `Dog` dùng lại được `Eat()`
- `Dog` có thêm `Bark()`

Tức là class con **không phải viết lại mọi thứ từ đầu**.

---

## 3. Ý nghĩa của kế thừa
Kế thừa giúp:

1. **Tái sử dụng code**
2. **Giảm lặp**
3. **Mở rộng chương trình dễ**
4. **Mô hình hóa đúng thế giới thực**

### Ví dụ
- `Vehicle` → `Car`, `Truck`, `Bus`
- `Person` → `Student`, `Employee`
- `Animal` → `Dog`, `Cat`

Ý chính: nếu nhiều đối tượng có phần chung, ta đưa phần chung lên class cha.

---

## 4. Access modifier trong kế thừa
Bài nhấn mạnh vai trò của `protected`.

| Modifier | Trong class | Class con | Bên ngoài |
|---|---|---|---|
| `public` | Có | Có | Có |
| `private` | Có | Không | Không |
| `protected` | Có | Có | Không |

### Ví dụ `protected`
```csharp
class Person
{
    protected string name;
}

class Student : Person
{
    public void SetName(string n)
    {
        name = n;
    }
}
```

### Ý nghĩa
- `private`: chỉ class hiện tại dùng được
- `protected`: class con dùng được
- `public`: ai cũng dùng được

Đây là điểm rất quan trọng khi thiết kế class cha.

---

## 5. Từ khóa `base`
`base` dùng để truy cập phần của class cha.

### 5.1. Gọi constructor class cha
```csharp
class Person
{
    public Person(string name)
    {
        Console.WriteLine("Person constructor: " + name);
    }
}

class Student : Person
{
    public Student(string name) : base(name)
    {
        Console.WriteLine("Student constructor");
    }
}
```

### Giải thích
- `base(name)` gọi constructor của `Person`
- nhờ đó phần khởi tạo ở class cha vẫn chạy đúng

### 5.2. Gọi method class cha
```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal speaks");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        base.Speak();
        Console.WriteLine("Dog barks");
    }
}
```

### Giải thích
- `base.Speak()` gọi bản của class cha
- sau đó class con thêm xử lý riêng

---

## 6. Đa hình là gì?
Đa hình nghĩa là:

> Cùng 1 lời gọi method, nhưng object thật sự khác nhau sẽ xử lý khác nhau.

### Ví dụ ý tưởng
Nếu ta có:
- `Dog`
- `Cat`

Cả 2 đều là `Animal`, nhưng khi gọi `Speak()`:
- `Dog` sủa
- `Cat` kêu meo

Đó là đa hình.

---

## 7. Hai kiểu đa hình trong bài
### 7.1. Compile-time polymorphism
Chính là **overloading**

### 7.2. Run-time polymorphism
Chính là **overriding**

Bài này giải thích rõ cả hai.

---

## 8. Method Overloading
Overloading = nhiều method cùng tên nhưng khác tham số.

### Khác ở đâu?
- khác số lượng tham số
- khác kiểu tham số
- khác thứ tự tham số

### Ví dụ
```csharp
class MathDemo
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}
```

### Giải thích
Cùng tên `Add`, nhưng:
- bản 1 cộng 2 số nguyên
- bản 2 cộng 2 số thực
- bản 3 cộng 3 số nguyên

Compiler sẽ chọn method phù hợp dựa trên tham số truyền vào.

### Ý nghĩa
- cùng 1 hành động nghiệp vụ
- nhưng hỗ trợ nhiều kiểu dữ liệu khác nhau
- code dễ đọc hơn

---

## 9. Method Overriding
Overriding = class con viết lại cách hoạt động của method ở class cha.

### Điều kiện
- class cha dùng `virtual`
- class con dùng `override`
- chữ ký method giống nhau

### Ví dụ
```csharp
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal sound");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}

class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Meow");
    }
}
```

### Gọi đa hình
```csharp
Animal a1 = new Dog();
Animal a2 = new Cat();

a1.Speak();
a2.Speak();
```

### Kết quả
- `a1.Speak()` → `Bark`
- `a2.Speak()` → `Meow`

### Ý nghĩa
Biến đều có kiểu `Animal`, nhưng hành vi phụ thuộc object thật sự là `Dog` hay `Cat`.

Đây chính là đa hình lúc chạy.

---

## 10. Phân biệt Overloading và Overriding

| Tiêu chí | Overloading | Overriding |
|---|---|---|
| Bản chất | Cùng tên, khác tham số | Viết lại method của cha |
| Quan hệ cha-con | Không bắt buộc | Bắt buộc |
| Thời điểm quyết định | Compile-time | Run-time |
| Từ khóa | Không bắt buộc | `virtual`, `override` |

### Hiểu ngắn
- **Overloading**: nhiều cách gọi cùng tên
- **Overriding**: thay đổi hành vi inherited method

---

## 11. Abstract class
Abstract class là class dùng làm nền, chưa hoàn chỉnh.

### Đặc điểm
- không tạo object trực tiếp được
- có thể có method bình thường
- có thể có method abstract

### Ví dụ
```csharp
abstract class Shape
{
    public abstract double Area();

    public void Show()
    {
        Console.WriteLine("This is a shape");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double r)
    {
        Radius = r;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}
```

### Giải thích
- `Shape` không đủ cụ thể để tạo object
- nhưng nó định nghĩa khung chung cho mọi hình
- `Circle` phải cài đặt `Area()`

### Ý nghĩa
Abstract class dùng khi:
- muốn gom phần chung
- muốn ép class con phải cài đặt phần quan trọng

---

## 12. Sealed class
`sealed` dùng để chặn kế thừa tiếp.

```csharp
sealed class FinalClass
{
}
```

### Ý nghĩa
- không cho class khác kế thừa
- bảo vệ thiết kế
- tránh mở rộng sai ý đồ

Ngoài ra còn có `sealed override` để chặn ghi đè tiếp ở lớp sâu hơn.

---

## 13. C# chỉ hỗ trợ single inheritance
Bài nhấn mạnh:

- 1 class chỉ kế thừa **1 class cha**
- nhưng có thể implement **nhiều interface**

### Ý nghĩa
C# tránh rắc rối của đa kế thừa class, nhưng vẫn giữ tính linh hoạt qua interface.

---

## 14. Ví dụ mô hình thường gặp trong bài
Các mô hình tiêu biểu:
- `Person` → `Student`
- `Animal` → `Dog`, `Cat`
- `Shape` → `Circle`, `Rectangle`

Người học cần nhìn ra:
- class cha chứa phần chung
- class con thêm phần riêng
- class con có thể ghi đè hành vi

---

## 15. Khi nào nên dùng kế thừa?
Nên dùng khi có quan hệ **is-a**.

### Ví dụ đúng
- Dog is an Animal
- Car is a Vehicle

### Ví dụ sai
- Engine is a Car

Ở ví dụ sai, `Engine` không phải là `Car`.  
Trường hợp này nên dùng quan hệ **has-a** chứ không phải inheritance.

---

## 16. Khi nào nên dùng đa hình?
Dùng khi:
- muốn code làm việc qua class cha chung
- muốn thêm loại mới mà ít sửa code cũ
- muốn viết code linh hoạt hơn

Ví dụ:
```csharp
List<Animal> animals = new List<Animal>
{
    new Dog(),
    new Cat()
};

foreach (Animal a in animals)
{
    a.Speak();
}
```

Code không cần biết từng object cụ thể là gì, nhưng vẫn chạy đúng hành vi.

---

## 17. Lỗi hay gặp
- nhầm kế thừa với quan hệ chứa đối tượng
- kế thừa chỉ để tái sử dụng code dù không có quan hệ `is-a`
- quên `virtual` ở class cha
- quên `override` ở class con
- dùng `private` rồi thắc mắc vì sao class con không truy cập được
- dùng inheritance quá mức làm cây class rối

---

## 18. Tóm tắt dễ hiểu
Bài này nói rằng:

- **Kế thừa** giúp class con dùng lại và mở rộng khả năng của class cha.
- **Đa hình** giúp cùng 1 lời gọi method nhưng mỗi object xử lý khác nhau.
- **Overloading** là nhiều method cùng tên nhưng khác tham số.
- **Overriding** là class con viết lại hành vi method của class cha.
- **Abstract class** dùng để tạo bộ khung chung.
- **Sealed** dùng để chặn kế thừa tiếp.

Đây là bài cực quan trọng vì nó là nền cho:
- interface
- delegate/event
- design patterns
- lập trình theo abstraction

---

# B4. Interface & Operator Overloading – Detailed Summary

## 1. Giới thiệu
Bài này gồm 2 phần:
1. **Interface** – cách mô tả "hợp đồng hành vi" cho class
2. **Operator Overloading** – cho phép kiểu dữ liệu tự định nghĩa dùng toán tử như kiểu built-in

Đây là bài rất quan trọng vì nó giúp code:
- linh hoạt
- mở rộng tốt
- chuẩn hóa cách các object tương tác

---

## 2. Interface là gì?
Interface là một **bản mô tả hành vi**.  
Nó chỉ nói: object phải làm được gì.  
Nó **không tập trung vào cách làm**.

### Thành phần được phép trong interface
Interface chỉ chứa khai báo các **non-static function member**:
- **Method** (phương thức)
- **Property** (thuộc tính)
- **Event** (sự kiện)
- **Indexer** (bộ chỉ mục)

Interface **không được phép** chứa:
- Field (trường dữ liệu)
- Static member (thành phần tĩnh)

### Bổ từ truy xuất
- Các thành phần trong interface mặc định là `public`
- **Không được phép** đặt bổ từ truy xuất (public, private, protected) trong interface

### Ví dụ ý tưởng
- Chim và máy bay đều có thể "bay"
- Ta có thể định nghĩa `IFlyable`
- Bất kỳ class nào implement `IFlyable` đều phải có method `Fly()`

### Cú pháp
```csharp
interface IFlyable
{
    void Fly();
}
```

### Interface kế thừa interface
Một interface có thể kế thừa từ một hoặc nhiều interface khác:
```csharp
interface IMovable
{
    void Move();
}

interface IFlyable : IMovable
{
    void Fly();
}
```

---

## 3. Class implement interface
```csharp
class Bird : IFlyable
{
    public void Fly()
    {
        Console.WriteLine("Bird is flying");
    }
}
```

Điểm quan trọng:
- class implement interface **phải cài đặt toàn bộ member bắt buộc**
- nếu không, class phải là `abstract`

### Thứ tự khi vừa kế thừa class vừa implement interface
Khi class vừa kế thừa từ class khác vừa implement interface, **tên class cha phải đứng trước**:
```csharp
class Document : BaseDocument, IStorable, ICloneable
{
    // implementation
}
```

### Toán tử 'as' để ép kiểu an toàn
Khi muốn ép kiểu từ object sang interface mà không chắc chắn, dùng `as`:
```csharp
IStorable obj = myDoc as IStorable;
if (obj != null)
{
    obj.Read();
}
```
Toán tử `as` trả về `null` nếu object không implement interface, thay vì văng exception.

---

## 4. Interface khác abstract class thế nào?

| Tiêu chí | Interface | Abstract class |
|---|---|---|
| Mục tiêu | Mô tả hành vi | Mô tả khuôn mẫu chung |
| Có field không | Không | Có |
| Có constructor không | Không | Có |
| Một class dùng nhiều được không | Có, nhiều interface | Không, chỉ kế thừa 1 class |
| Quan hệ | Can-do | Is-a |

Hiểu ngắn:
- **Abstract class**: "là một loại của"
- **Interface**: "có khả năng làm"

---

## 5. Nhiều interface
C# không cho đa kế thừa class, nhưng cho implement nhiều interface.

```csharp
interface IPrint
{
    void Print();
}

interface ISave
{
    void Save();
}

class Report : IPrint, ISave
{
    public void Print()
    {
        Console.WriteLine("Printing report");
    }

    public void Save()
    {
        Console.WriteLine("Saving report");
    }
}
```

Ý nghĩa:
- class có thể mang nhiều vai trò hành vi
- thiết kế linh hoạt hơn

---

## 6. Explicit và Implicit implementation

### 6.1. Implicit implementation
Cài đặt public bình thường:
```csharp
interface IRun
{
    void Start();
}

class Car : IRun
{
    public void Start()
    {
        Console.WriteLine("Car starts");
    }
}
```

Dùng trực tiếp:
```csharp
Car c = new Car();
c.Start();
```

### 6.2. Explicit implementation
Dùng khi muốn method interface chỉ gọi được qua biến interface.

```csharp
interface IEnglish
{
    void Speak();
}

interface IVietnamese
{
    void Speak();
}

class Person : IEnglish, IVietnamese
{
    void IEnglish.Speak()
    {
        Console.WriteLine("Hello");
    }

    void IVietnamese.Speak()
    {
        Console.WriteLine("Xin chào");
    }
}
```

Gọi:
```csharp
Person p = new Person();
// p.Speak(); // lỗi

IEnglish e = p;
e.Speak();

IVietnamese v = p;
v.Speak();
```

### Khi nào cần explicit?
- 2 interface có member trùng tên
- muốn ẩn method khỏi public API của class

---

## 7. IComparable
`IComparable` dùng để so sánh object với nhau, rất quan trọng cho sort.

### Cú pháp
```csharp
class Student : IComparable
{
    public string Name { get; set; }

    public int CompareTo(object obj)
    {
        Student other = (Student)obj;
        return this.Name.CompareTo(other.Name);
    }
}
```

### Quy ước trả về
- `< 0` : object hiện tại nhỏ hơn
- `= 0` : bằng nhau
- `> 0` : lớn hơn

### Ý nghĩa
Nếu class implement `IComparable`, ta có thể:
- sắp xếp mảng
- sắp xếp list
- dùng các API sort sẵn có

---

## 8. IDisposable
`IDisposable` dùng để giải phóng tài nguyên.

### Dispose Pattern (mô hình chuẩn)
```csharp
class Resource : IDisposable
{
    private bool disposed = false;

    public void Dispose()
    {
        Dispose(true);
        // Ngăn finalizer chạy lại vì đã dọn dẹp xong
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Giải phóng managed resources (object C#)
            }
            // Giải phóng unmanaged resources (handle OS, con trỏ)
            disposed = true;
        }
    }

    ~Resource() // Finalizer
    {
        Dispose(false);
    }
}
```

### Ý nghĩa các thành phần
- `Dispose()`: gọi từ code, giải phóng cả managed và unmanaged
- `Dispose(bool disposing)`: logic thực tế
  - `disposing = true`: gọi từ Dispose(), giải phóng tất cả
  - `disposing = false`: gọi từ finalizer, chỉ giải phóng unmanaged
- `GC.SuppressFinalize(this)`: ngăn finalizer chạy lại vì đã dọn xong
- `~Resource()`: finalizer, backup khi quên gọi Dispose()

### Thường dùng với
- file
- stream
- database connection
- unmanaged resource

### Kết hợp `using`
```csharp
using (MyFile f = new MyFile())
{
    // use f
}
```
Ra khỏi khối `using`, `Dispose()` tự gọi.

---

## 9. ICloneable
`ICloneable` dùng để sao chép object.

```csharp
class Student : ICloneable
{
    public string Name { get; set; }

    public object Clone()
    {
        return new Student { Name = this.Name };
    }
}
```

### Lưu ý
Cần hiểu:
- **shallow copy**
- **deep copy**

Bài chủ yếu nhấn mạnh ý nghĩa interface và cách tạo bản sao object.

---

## 10. Operator overloading là gì?
Cho phép class tự định nghĩa ý nghĩa của toán tử như:
- `+`
- `-`
- `*`
- `/`
- `==`
- `!=`

Điều này làm cho code với kiểu tự định nghĩa tự nhiên hơn.

---

## 11. Ví dụ phân số
```csharp
class Fraction
{
    public int Tu { get; set; }
    public int Mau { get; set; }

    public Fraction(int tu, int mau)
    {
        Tu = tu;
        Mau = mau;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Tu * b.Mau + b.Tu * a.Mau,
            a.Mau * b.Mau
        );
    }

    public override string ToString()
    {
        return Tu + "/" + Mau;
    }
}
```

Dùng:
```csharp
Fraction f1 = new Fraction(1, 2);
Fraction f2 = new Fraction(1, 3);
Fraction f3 = f1 + f2;
Console.WriteLine(f3);
```

### Ý nghĩa
Người dùng class có thể viết:
```csharp
f1 + f2
```
thay vì:
```csharp
Fraction.Add(f1, f2)
```

---

## 12. Quy tắc khi nạp chồng toán tử
- Method operator phải là `public static`
- Nên giữ ý nghĩa tự nhiên của toán tử
- Nếu overload `==` thì thường nên overload luôn `!=`
- Nếu object có logic so sánh bằng nhau, nên cân nhắc override:
  - `Equals`
  - `GetHashCode`

---

## 13. Tại sao bài này quan trọng?
### Interface
Giúp:
- lập trình hướng abstraction
- giảm phụ thuộc vào implementation cụ thể
- dễ mở rộng, test, thay thế

### Operator overloading
Giúp:
- kiểu dữ liệu tự định nghĩa dễ dùng hơn
- code đọc giống toán học/nghiệp vụ hơn

---

## 14. Lỗi hay gặp
- Quên `public` khi implement interface
- Implement chưa đủ member
- Nhầm abstract class với interface
- Overload toán tử nhưng không giữ ngữ nghĩa rõ ràng
- Overload `==` mà quên `Equals/GetHashCode`

---

## 15. Tóm tắt dễ hiểu
Bài này dạy:

- **Interface** là bản cam kết hành vi. Class nào nhận cam kết thì phải làm đủ.
- Một class có thể implement nhiều interface để mang nhiều vai trò.
- `IComparable`, `IDisposable`, `ICloneable` là các interface chuẩn hay dùng.
- **Operator overloading** giúp object tự định nghĩa cách hoạt động của toán tử để code tự nhiên và dễ đọc hơn.

Nếu hiểu bài này, bạn sẽ học dễ hơn các phần:
- generic sort
- delegate/event
- design patterns
- lập trình theo interface thay vì theo implementation.

---

# B5. Delegate and Event – Detailed Summary

## 1. Giới thiệu
Bài này dạy 2 cơ chế rất quan trọng trong C#:
- **Delegate**: biến method thành thứ có thể truyền đi như dữ liệu
- **Event**: cơ chế thông báo giữa các object theo kiểu publish/subscribe

Nói ngắn:
- delegate giúp "giữ địa chỉ hàm"
- event giúp object này báo cho object khác biết "đã có chuyện xảy ra"

---

## 2. Delegate là gì?
Delegate là kiểu tham chiếu (reference type) đến một hay nhiều method có cùng signature.
Delegate là **con trỏ tới hàm** (pointer to function), nhưng **an toàn kiểu** (type-safe) và là **cơ chế hướng đối tượng** (object-oriented mechanism).
Mỗi delegate instance chứa một **invocation list** (danh sách các method được tham chiếu).

### Signature gồm
- kiểu trả về
- danh sách tham số

### Ví dụ khai báo
```csharp
public delegate void MyDelegate1(int x, int y);
public delegate string MyDelegate2(float f);
```

Ý nghĩa:
- `MyDelegate1` chỉ trỏ tới hàm dạng `void Method(int, int)`
- `MyDelegate2` chỉ trỏ tới hàm dạng `string Method(float)`

---

## 3. Tạo delegate instance
```csharp
public void Method1(int x, int y)
{
    Console.WriteLine(x + y);
}

MyDelegate1 del1 = new MyDelegate1(Method1);
```

Hoặc kiểu ngắn hơn:
```csharp
MyDelegate1 del1 = Method1;
```

### Gọi delegate
```csharp
del1(10, 20);
```

Lúc này thực chất delegate gọi method mà nó đang trỏ tới.

---

## 4. Delegate là type-safe
Không phải hàm nào cũng gán được cho delegate.  
Method bắt buộc phải khớp:
- cùng kiểu trả về
- cùng kiểu tham số
- cùng số lượng tham số

Đây là điểm bài nhấn mạnh mạnh.

---

## 5. Multicast delegate
Một delegate có thể trỏ tới **nhiều hàm cùng lúc**.

### Ví dụ
```csharp
void Print(int x, int y)
{
    Console.WriteLine("x = {0}, y = {1}", x, y);
}

void Sum(int x, int y)
{
    Console.WriteLine("Tong = {0}", x + y);
}

MyDelegate1 mulDel = new MyDelegate1(Print);
mulDel += new MyDelegate1(Sum);

mulDel(5, 10);
```

Khi gọi `mulDel(5,10)`, cả `Print` và `Sum` đều chạy **theo thứ tự đã thêm vào** invocation list (Print trước, Sum sau).
Nếu một method trong chuỗi văng exception, các method sau sẽ không được gọi.

### Bỏ bớt method
```csharp
mulDel -= new MyDelegate1(Print);
```

### Lưu ý
Bài có nhắc:
- multicast delegate phù hợp nhất khi kiểu trả về là `void`
- vì nếu nhiều hàm cùng trả về giá trị thì việc dùng kết quả sẽ không rõ ràng

---

## 6. Delegate dùng để giải quyết bài toán Sort tổng quát
Đây là ví dụ trọng tâm của bài.

### Bài toán
Cần viết hàm `Sort` cho mảng object bất kỳ.

Vấn đề:
- mỗi loại object có cách so sánh riêng
- ví dụ Person so theo tên, theo tuổi, theo năm sinh...

### Giải pháp
Truyền **hàm so sánh** từ bên ngoài vào `Sort` qua delegate.

### Delegate so sánh
```csharp
public delegate bool CompareObj(object o1, object o2);
```

Ý nghĩa:
- trả về `true` nếu `o1` đứng trước `o2`
- `false` nếu ngược lại

### Hàm sort
```csharp
public static void Sort(object[] objs, CompareObj cmp)
{
    for (int i = 0; i < objs.Length - 1; i++)
        for (int j = objs.Length - 1; j > i; j--)
            if (cmp(objs[j], objs[j - 1]))
            {
                // swap
            }
}
```

### Class tự quyết định cách so sánh
```csharp
class Person
{
    private string name;
    private int weight;
    private int yearOfBirth;

    public static bool CompareName(object p1, object p2)
    {
        return string.Compare(((Person)p1).name, ((Person)p2).name) < 0;
    }
}
```

### Gọi sort
```csharp
CompareObj cmp = new CompareObj(Person.CompareName);
Lib.Sort(persons, cmp);
```

**Ý nghĩa lớn của ví dụ này**:  
Thuật toán sort không cần biết chi tiết class cụ thể.  
Nó chỉ cần 1 "chiến lược so sánh" được truyền vào.

---

## 7. Event là gì?
Event là cơ chế để object thông báo cho object khác khi có việc xảy ra.

### Khái niệm trong bài
- **Publisher**: lớp phát sinh sự kiện
- **Subscriber**: lớp đăng ký và xử lý sự kiện

Ví dụ thực tế:
- Button phát sinh event `Click`
- Form lắng nghe và xử lý khi người dùng click

---

## 8. Event và Delegate liên hệ thế nào?
Bài nói rõ:
> Event trong C# được thực thi nhờ delegate.

Nghĩa là:
- delegate mô tả chữ ký hàm xử lý
- event dùng delegate đó để quản lý danh sách subscriber

---

## 9. Chuẩn event handler trong .NET
Bài đưa mẫu xử lý sự kiện:
```csharp
void Handler(object sender, EventArgs e)
```

Giải thích:
- `sender`: đối tượng phát sinh sự kiện
- `EventArgs e`: dữ liệu bổ sung của sự kiện

---

## 10. Khai báo delegate và event
```csharp
public delegate void HandlerName(object obj, EventArgs arg);
public event HandlerName OnEventName;
```

---

## 11. Ví dụ minh họa Clock
Bài minh họa class `Clock`:
- cứ mỗi giây phát sinh 1 event
- 2 class khác subscribe:
  - `AnalogClock`
  - `DigitalClock`

### Delegate xử lý
```csharp
delegate void SecondChangeHandler(object clock, EventArgs info);
```

### Event trong Clock
```csharp
event SecondChangeHandler OnSecondChange;
```

### Phát sinh event
```csharp
if (OnSecondChange != null)
    OnSecondChange(this, new EventArgs());
```

---

## 12. Class Clock
```csharp
public class Clock
{
    public delegate void SecondChangeHandler(object clock, EventArgs info);
    public event SecondChangeHandler OnSecondChange;

    public void Run()
    {
        while (true)
        {
            Thread.Sleep(1000);
            if (OnSecondChange != null)
                OnSecondChange(this, new EventArgs());
        }
    }
}
```

Ý tưởng:
- Clock làm publisher
- mỗi giây báo cho mọi subscriber

---

## 13. Subscriber: DigitalClock
```csharp
public class DigitalClock
{
    public void Subscribe(Clock theClock)
    {
        theClock.OnSecondChange += new Clock.SecondChangeHandler(Show);
    }

    public void Show(object obj, EventArgs args)
    {
        DateTime date = DateTime.Now;
        Console.WriteLine("Digital Clock:{0}:{1}:{2}",
            date.Hour, date.Minute, date.Second);
    }
}
```

## 14. Subscriber: AnalogClock
```csharp
public class AnalogClock
{
    public void Subscribe(Clock theClock)
    {
        theClock.OnSecondChange += new Clock.SecondChangeHandler(Show);
    }

    public void Show(object obj, EventArgs args)
    {
        DateTime date = DateTime.Now;
        Console.WriteLine("Analog Clock:{0}:{1}:{2}",
            date.Hour, date.Minute, date.Second);
    }
}
```

---

## 15. Chương trình chạy thử
```csharp
public class Tester
{
    public static void Main()
    {
        Clock myClock = new Clock();
        AnalogClock c1 = new AnalogClock();
        DigitalClock c2 = new DigitalClock();

        c1.Subscribe(myClock);
        c2.Subscribe(myClock);

        myClock.Run();
    }
}
```

Ý nghĩa:
- `myClock` phát sự kiện
- `c1`, `c2` đều nhận được thông báo
- cùng một event nhưng mỗi class xử lý theo cách riêng

---

## 16. Hạn chế của ví dụ đầu và EventArgs nâng cao
Bài có nói hạn chế:
- mỗi subscriber tự lấy thời gian hiện tại
- bị trùng lặp
- không tối ưu

### Giải pháp
Publisher truyền luôn dữ liệu thời gian khi phát event.

Tức là:
- tạo class kế thừa `EventArgs`
- chứa thông tin giờ/phút/giây
- subscriber dùng dữ liệu đó thay vì tự lấy lại

Đây là lý do phần cuối bài nói về `TimeEventArgs`.

---

## 17. Ý nghĩa thực tế của Event
Event rất quan trọng trong:
- GUI
- game
- hệ thống cảnh báo
- hệ thống theo dõi trạng thái
- kiến trúc hướng sự kiện

Ví dụ:
- rút tiền ATM xong → gửi SMS
- chuyển tiền xong → phát event thông báo

Bài còn cho bài tập ATM đúng theo hướng này.

---

## 18. Lỗi hay gặp
- Method không khớp signature delegate
- Quên kiểm tra `null` trước khi phát event
- Nhầm delegate với event
- Dùng event nhưng để subscriber tự xử lý quá nhiều logic lặp
- Không tách publisher/subscriber rõ

---

## 19. Tóm tắt dễ hiểu
Bài này dạy:

- **Delegate** là cách lưu tham chiếu tới hàm.
- Delegate có thể trỏ tới 1 hàm hoặc nhiều hàm.
- Delegate rất hữu ích khi muốn truyền hành vi vào hàm khác, ví dụ truyền hàm so sánh vào `Sort`.
- **Event** là cơ chế thông báo giữa các object.
- Event dựa trên delegate, thường theo mô hình **publisher/subscriber**.
- Ví dụ đồng hồ cho thấy: 1 object phát sự kiện, nhiều object khác có thể đăng ký và phản ứng khác nhau.

Hiểu bài này rất quan trọng để học:
- WinForms / WPF
- callback
- asynchronous programming
- observer pattern
- event-driven architecture

---

# B6. Generics & Collections – Detailed Summary

## 1. Giới thiệu
Bài này gồm 2 mảng lớn:
1. **Collections** – các cấu trúc dữ liệu có sẵn trong .NET để lưu trữ nhiều phần tử
2. **Generics** – cơ chế giúp code an toàn kiểu hơn và dùng lại tốt hơn

Mục tiêu bài:
- biết chọn collection phù hợp
- hiểu nhược điểm collection cũ
- hiểu vì sao generic tốt hơn
- dùng được các generic collections hiện đại

---

## 2. Collections là gì?
Collection là tập hợp hỗ trợ:
- lưu trữ nhiều đối tượng
- thêm/xóa
- tìm kiếm
- duyệt phần tử

Namespace chính:
- `System.Collections`
- `System.Collections.Specialized`
- `System.Collections.Generic`

---

## 3. ArrayList
`ArrayList` là collection động cơ bản, chứa được nhiều kiểu dữ liệu khác nhau vì lưu dưới dạng `object`.

### Thêm phần tử
```csharp
ArrayList coll = new ArrayList();
coll.Add("Hello");
coll.Add("Hi");
coll.Add(50);
coll.Add(new object());
```

### AddRange
```csharp
string[] anArray = new string[] { "more", "or", "less" };
coll.AddRange(anArray);
```

### Insert / InsertRange
```csharp
coll.Insert(1, "Hey all");
coll.InsertRange(3, new string[] { "good night", "see you" });
```

### Xóa
```csharp
coll.Remove("dulieu");
coll.RemoveAt(0);
coll.RemoveRange(0, 4);
```

### Tìm
```csharp
if (coll.Contains("MyString"))
{
    int index = coll.IndexOf("MyString");
    coll.RemoveAt(index);
}
```

### Duyệt
```csharp
for (int i = 0; i < coll.Count; i++)
    Console.WriteLine(coll[i]);

IEnumerator e = coll.GetEnumerator();
while (e.MoveNext())
    Console.WriteLine(e.Current);

foreach (object item in coll)
    Console.WriteLine(item);
```

### Sort
```csharp
coll.Sort();
```

Có thể truyền `IComparer` riêng để custom thứ tự sắp xếp.

### Lưu ý ArrayList
- `coll[index] = value` chỉ **ghi đè** giá trị tại index đã có, **không thêm phần tử mới**
- `coll.Clear()` xóa toàn bộ phần tử

### Nhược điểm
- không an toàn kiểu
- phải ép kiểu khi lấy ra
- value type bị boxing/unboxing

---

## 4. Danh sách tuần tự
Bài gọi Queue và Stack là danh sách tuần tự vì truy xuất tuần tự từng phần tử.

---

## 5. Queue
Queue làm việc theo FIFO:
- vào trước ra trước

### Method chính
- `Enqueue`
- `Dequeue`
- `Peek`
- `Count`

### Ví dụ
```csharp
Queue q = new Queue();
q.Enqueue("Item1");
q.Enqueue("Item2");
q.Enqueue("Item3");

Console.WriteLine(q.Dequeue());
Console.WriteLine(q.Dequeue());
```

### Peek
```csharp
if (q.Peek() is string)
{
    Console.WriteLine(q.Dequeue());
}
```

`Peek()` cho xem phần tử đầu mà không xóa khỏi queue.

---

## 6. Stack
Stack làm việc theo LIFO:
- vào sau ra trước

### Method chính
- `Push`
- `Pop`
- `Peek`
- `Count`

### Ví dụ
```csharp
Stack s = new Stack();
s.Push("Item1");
s.Push("Item2");
Console.WriteLine(s.Pop());
s.Push("Item3");
Console.WriteLine(s.Pop());
```

### Peek
```csharp
if (s.Peek() is string)
{
    Console.WriteLine(s.Pop());
}
```

---

## 7. Dictionary collections
Dictionary lưu theo cặp:
- **key**
- **value**

Mục tiêu chính:
- tìm nhanh theo key

---

## 8. Hashtable
`Hashtable` là dictionary cổ điển.

### Ví dụ
```csharp
Hashtable emailLookup = new Hashtable();
emailLookup.Add("sbishop@contoso.com", "Bishop, Scott");
emailLookup["djump@contoso.com"] = "Jump, Dan";
```

### Duyệt đúng cách
```csharp
foreach (DictionaryEntry entry in emailLookup)
{
    Console.WriteLine(entry.Key);
    Console.WriteLine(entry.Value);
}
```

Bài nhấn mạnh:
- nếu duyệt sai kiểu sẽ không thấy rõ cặp key/value
- `DictionaryEntry` chứa cả key và value

### Hash code
Hashtable dùng hash để tăng tốc tìm kiếm.
Bài còn giải thích:
- 2 object khác nhau có thể phải định nghĩa `Equals` và `GetHashCode` đúng nếu muốn coi là "giống nhau"

---

## 9. SortedList
`SortedList` cũng là dictionary nhưng:
- các phần tử được sắp theo key
- có thể truy xuất qua index

### Ví dụ
```csharp
SortedList s = new SortedList();
s["First"] = "1st";
s["Second"] = "2nd";
s["Third"] = "3rd";

foreach (DictionaryEntry e in s)
{
    Console.WriteLine("{0}={1}", e.Key, e.Value);
}
```

### Ví dụ SortedList với IComparer
```csharp
SortedList s = new SortedList(new DescendingComparer());
```

---

## 10. Specialized Dictionaries
Bài giới thiệu 3 dictionary chuyên biệt:

### 10.1. ListDictionary
Phù hợp tập hợp nhỏ. Hiệu quả hơn Hashtable khi số phần tử rất ít.

### 10.2. HybridDictionary
Dùng khi chưa biết số phần tử.
- lúc nhỏ hoạt động như `ListDictionary`
- khi lớn tự chuyển sang `Hashtable`

### 10.3. OrderedDictionary
Kết hợp: truy cập nhanh kiểu dictionary + giữ thứ tự phần tử + truy cập qua chỉ mục.

---

## 11. Specialized Collections

### 11.1. BitArray
- lưu chuỗi bit
- hỗ trợ `And`, `Or`, `Not`, `Xor`
- **không hỗ trợ** `Add` / `Remove` (kích thước cố định khi khởi tạo)
- giá trị mặc định các phần tử khi khởi tạo là `false`
- các phép toán bit yêu cầu 2 BitArray phải có **cùng kích thước**

### 11.2. BitVector32
- lưu trong 32 bit
- hỗ trợ bit mask và section

### 11.3. StringCollection
- chỉ lưu `string`
- giống ArrayList nhưng type-safe cho chuỗi

### 11.4. StringDictionary
- key/value đều là `string`

### 11.5. NameValueCollection
- gần giống `StringDictionary`
- nhưng **1 key có thể có nhiều value**

```csharp
NameValueCollection nv = new NameValueCollection();
nv.Add("Key", "SomeText");
nv.Add("Key", "MoreText");

foreach (string s in nv.GetValues("Key"))
{
    Console.WriteLine(s);
}
```

---

## 12. Generic là gì?
Generic cho phép tạo class/method/collection với kiểu chưa xác định trước.

### So sánh tư duy
Không generic:
```csharp
class Obj
{
    public object t;
    public object u;
}
```

Generic:
```csharp
class Gen<T, U>
{
    public T t;
    public U u;
}
```

### Dùng
```csharp
Gen<double, int> ga = new Gen<double, int>();
Gen<string, string> gs = new Gen<string, string>();
```

---

## 13. Lợi ích của generic
- compiler kiểm tra kiểu ngay lúc biên dịch
- code rõ hơn
- lỗi ít hơn
- performance tốt hơn với value type (tránh boxing/unboxing)

---

## 14. Generic constraints
Khi generic quá tự do, ta không gọi được method riêng của kiểu đó. Giải pháp: đặt ràng buộc.

### Ví dụ
```csharp
class CompGen<T> where T : IComparable
{
    public T t1;
    public T t2;

    public T Max()
    {
        if (t2.CompareTo(t1) < 0)
            return t1;
        return t2;
    }
}
```

### Các kiểu ràng buộc
- interface
- base class
- constructor
- reference type / value type

---

## 15. Generic Collections
Namespace: `System.Collections.Generic`

### 15.1. List\<T\>
```csharp
List<int> intList = new List<int>();
intList.Add(1);
intList.Add(2);
intList.Add(3);

int number = intList[0];
foreach (int i in intList)
{
    Console.WriteLine(i);
}
```

### 15.2. Queue\<T\>
```csharp
Queue<string> q = new Queue<string>();
q.Enqueue("Hello");
string qString = q.Dequeue();
```

### 15.3. Stack\<T\>
```csharp
Stack<string> s = new Stack<string>();
s.Push("Hello");
string sString = s.Pop();
```

### 15.4. Dictionary\<TKey, TValue\>
```csharp
Dictionary<int, string> dict = new Dictionary<int, string>();
dict[3] = "Three";
dict[4] = "Four";
dict[2] = "Two";
dict[1] = "One";

string str = dict[3];
Console.WriteLine(str);
```

### Duyệt Dictionary
```csharp
foreach (KeyValuePair<int, string> i in dict)
{
    Console.WriteLine("{0}={1}", i.Key, i.Value);
}
```

### 15.5. SortedList\<TKey, TValue\>
- tương tự Dictionary nhưng sắp theo key

### 15.6. SortedDictionary\<TKey, TValue\>
- cũng sắp theo key, khác cài đặt nội bộ so với SortedList

### 15.7. LinkedList\<T\>
Collection dạng liên kết đôi.

```csharp
LinkedList<string> links = new LinkedList<string>();
LinkedListNode<string> first = links.AddLast("First");
LinkedListNode<string> last = links.AddFirst("Last");
LinkedListNode<string> second = links.AddBefore(last, "Second");
links.AddAfter(second, "Third");

foreach (string s in links)
{
    Console.WriteLine(s);
}
```

---

## 16. Chọn collection nào?
- `List<T>`: danh sách tuần tự, truy cập theo index
- `Queue<T>`: hàng chờ FIFO
- `Stack<T>`: ngăn xếp LIFO, undo, backtracking
- `Dictionary<TKey,TValue>`: tìm nhanh theo key
- `SortedList` / `SortedDictionary`: tự động sắp theo key
- `LinkedList<T>`: chèn/xóa node linh hoạt ở giữa

---

## 17. Lỗi hay gặp
- dùng `ArrayList` khi nên dùng `List<T>`
- ép kiểu sai khi lấy dữ liệu từ non-generic collection
- nhầm FIFO với LIFO
- dùng dictionary nhưng không hiểu key phải duy nhất
- không hiểu vì sao `GetHashCode` ảnh hưởng tìm kiếm trong Hashtable
- dùng generic nhưng quên constraints khi cần gọi method đặc thù

---

## 18. Tóm tắt dễ hiểu
Bài này dạy cách lưu nhiều dữ liệu trong .NET.

- `ArrayList`, `Queue`, `Stack`, `Hashtable` là collection cũ.
- `List<T>`, `Queue<T>`, `Stack<T>`, `Dictionary<TKey,TValue>` là bản generic hiện đại hơn.
- **Generic** giúp code an toàn kiểu, ít lỗi, nhanh hơn.
- Cần chọn đúng collection theo cách truy cập dữ liệu:
  - hàng chờ → Queue
  - ngăn xếp → Stack
  - tra cứu theo key → Dictionary
  - danh sách tổng quát → List

Đây là bài nền rất mạnh cho việc viết code thực tế về sau.

---

# B7. Exceptions – Detailed Summary

## 1. Giới thiệu
Bài này nói về **ngoại lệ (exception)** trong C#.

Mục tiêu:
- hiểu lỗi runtime là gì
- hiểu vì sao cần exception
- biết dùng `try`, `catch`, `finally`, `throw`
- biết chọn loại exception phù hợp
- biết khi nào nên tự tạo exception riêng

---

## 2. Error, Runtime Error, Exception
Bài phân biệt:

- **Error** = compile error + runtime error
- **Exception** = runtime error

### Ví dụ lỗi runtime
- nhập sai định dạng
- chia cho 0
- file không tồn tại
- ổ đĩa đầy
- mạng mất kết nối

Điểm quan trọng:
Exception là tình huống không mong đợi xảy ra khi chương trình đang chạy, làm đoạn mã không thể tiếp tục bình thường.

---

## 3. Code path
Bài giới thiệu khái niệm **code path**:
- là chuỗi lời gọi method đang diễn ra
- được giữ trên stack

Ví dụ:
`Main()` → `Divide()` → `Three()` → `Two()` → `One()`

Nếu lỗi xuất hiện ở sâu bên trong, exception sẽ đi ngược lại trên đường gọi này cho tới nơi bắt được.

---

## 4. Vì sao cần exception?
Bài so sánh 2 cách xử lý lỗi.

### 4.1. Cách cũ: completion code
Mỗi bước đều phải kiểm tra thành công hay thất bại.
Nhược điểm:
- code rối
- khó đọc
- luồng nghiệp vụ chính bị lẫn với luồng xử lý lỗi

### 4.2. Cách dùng exception
Tách rõ:
- phần "làm việc chính"
- phần "xử lý lỗi"

Ưu điểm:
- code sạch hơn
- dễ thấy nhiệm vụ chính của method
- dễ bảo trì hơn

---

## 5. Bộ từ khóa chính
- `try`
- `catch`
- `finally`
- `throw`

---

## 6. Cú pháp cơ bản
```csharp
try
{
    // code có thể phát sinh lỗi
}
catch (Exception ex)
{
    // xử lý lỗi
}
finally
{
    // luôn chạy
}
```

### Ý nghĩa
- `try`: đặt code có nguy cơ lỗi
- `catch`: bắt lỗi
- `finally`: dọn dẹp tài nguyên, luôn chạy dù có lỗi hay không

---

## 7. Cơ chế hoạt động của exception
Khi exception xuất hiện:
1. CLR tạo object exception
2. object này bị **throw**
3. nó đi ngược lại theo code path
4. nếu có `catch` phù hợp thì dừng ở đó
5. nếu không ai bắt, CLR in lỗi và kết thúc chương trình

Đây là ý trung tâm của bài.

---

## 8. Các lớp exception thông dụng
Bài liệt kê nhiều class có sẵn:

- `Exception`
- `SystemException`
- `ArgumentException`
- `ArgumentNullException`
- `ArgumentOutOfRangeException`
- `ArithmeticException`
- `DivideByZeroException`
- `OverflowException`
- `NotFiniteNumberException`
- `IOException`
- `FileNotFoundException`
- `DirectoryNotFoundException`
- `FileLoadException`
- `EndOfStreamException`
- `NotImplementedException`
- `InvalidCastException`
- `FormatException`
- `IndexOutOfRangeException`
- `NullReferenceException`
- `RankException`
- `StackOverflowException`
- `ApplicationException`

Nên tận dụng exception có sẵn trước khi tự tạo loại mới.

---

## 9. Các thuộc tính quan trọng của Exception
- `Message`
- `Source`
- `StackTrace`
- `TargetSite`

Chúng giúp debug và hiểu lỗi xảy ra ở đâu.

---

## 10. Nhiều catch
```csharp
try
{
    // ...
}
catch (FormatException ex)
{
    // ...
}
catch (Exception ex)
{
    // ...
}
```

Lưu ý:
- catch cụ thể phải đặt trước
- catch tổng quát đặt sau
- nếu nhiều catch cùng phù hợp, catch gần nhất đúng thứ tự sẽ được dùng

---

## 11. Bắt mọi exception
Có 2 cách:
```csharp
catch (Exception ex)
{
}
```

hoặc:
```csharp
catch
{
}
```

Bài có ví dụ nhập số:
```csharp
double input;
try
{
    input = Convert.ToDouble(Console.ReadLine());
}
catch
{
    Console.WriteLine("Ban danh so khong hop le");
    input = double.NaN;
}
```

---

## 12. Ví dụ nhập số hợp lệ bằng vòng lặp
Bài có ví dụ `GetDouble()`:
- yêu cầu người dùng nhập
- nếu sai định dạng thì báo lỗi
- tiếp tục nhập lại tới khi đúng

Ý chính:
- exception có thể dùng để điều khiển quy trình nhập an toàn
- nhưng chỉ nên dùng khi thật sự là tình huống ngoại lệ

---

## 13. Checked và Unchecked
Bài giới thiệu kiểm tra tràn số học với số nguyên.

### Checked
```csharp
int number = int.MaxValue;
checked
{
    int willThrow = number++;
}
```
Nếu tràn → ném `OverflowException`

### Unchecked
```csharp
int number = int.MaxValue;
unchecked
{
    int wontThrow = number++;
}
```
Nếu tràn → bỏ qua, kết quả quay vòng

Có thể dùng với biểu thức riêng lẻ:
```csharp
int x = checked(int.MaxValue + 1);
int y = unchecked(int.MaxValue + 1);
```

---

## 14. Ném ngoại lệ với throw
Khi method phát hiện lỗi mà tự nó không xử lý được, nó nên báo cho nơi gọi.

### Cú pháp
```csharp
throw;
```
Dùng trong `catch` để ném lại lỗi đang bắt.

Hoặc:
```csharp
throw new ArgumentNullException();
```

### Ví dụ
```csharp
if (strInput == null)
    throw new ArgumentNullException();
```

Ý nghĩa:
- method fail fast
- báo lỗi rõ ngay chỗ phát hiện sai

---

## 15. Ví dụ MyParse trong bài
Bài có ví dụ tự parse chuỗi sang `uint`.

Các bước:
- nếu `str == null` → `ArgumentNullException`
- trim chuỗi
- nếu rỗng → `FormatException`
- duyệt từng ký tự
- nếu có ký tự không phải số → `FormatException`
- dùng `checked` khi cộng dồn để phát hiện overflow

Ví dụ này cho thấy:
- cách kiểm tra điều kiện đầu vào
- cách chọn đúng loại exception
- cách kết hợp `checked`

---

## 16. User-defined exception
Bài cuối nói về ngoại lệ tự định nghĩa.

### Ví dụ
```csharp
public class UserDefinedException : Exception
{
    public UserDefinedException(string message) : base(message) { }
}
```

Ý nghĩa:
- dùng cho lỗi nghiệp vụ riêng
- giúp thông báo rõ hơn

---

## 17. Ví dụ Fraction trong bài
Bài có class `Fraction`:
- constructor kiểm tra mẫu số
- nếu mẫu số bằng 0 → ném `UserDefinedException`
- operator `/` cũng kiểm tra trường hợp dẫn tới mẫu số 0

### Điểm người học cần hiểu
- exception có thể phát sinh ở constructor
- exception có thể phát sinh trong operator overloading
- có thể bắt và fallback về giá trị an toàn, nhưng cần dùng cẩn thận

---

## 18. Khi nào nên ném exception?
Nên ném khi:
- dữ liệu đầu vào sai nghiêm trọng
- method không thể tiếp tục đúng logic
- trạng thái đối tượng không hợp lệ
- tài nguyên không truy cập được

Không nên lạm dụng exception cho luồng xử lý bình thường.

---

## 19. Lỗi hay gặp
- bắt `Exception` quá rộng ở mọi nơi
- nuốt lỗi mà không log gì
- dùng exception để thay cho kiểm tra logic bình thường
- ném sai loại exception
- catch xong không xử lý gì hữu ích
- dùng `finally` sai mục đích

---

## 20. Tóm tắt dễ hiểu
Bài này dạy:

- **Exception** là lỗi runtime làm chương trình không tiếp tục bình thường.
- Dùng `try-catch-finally` để tách phần xử lý lỗi khỏi luồng nghiệp vụ chính.
- Dùng `throw` để báo lỗi lên nơi gọi.
- C# có sẵn nhiều class exception, nên dùng đúng loại.
- Có thể tự tạo exception riêng cho nghiệp vụ đặc biệt.
- `checked` và `unchecked` giúp kiểm soát tràn số.

Nếu hiểu bài này tốt, bạn sẽ viết được code an toàn, dễ debug, và ít crash hơn.

---

# B8. Design Patterns EN – Detailed Summary

## 1. Giới thiệu
Bài này giới thiệu **Design Patterns** – các mẫu thiết kế phần mềm hướng đối tượng kinh điển.

Nguồn gốc:
- cuốn sách "Design Patterns: Elements of Reusable Object-Oriented Software" (GoF – Gang of Four, 1994)
- 23 mẫu thiết kế được chia thành 3 nhóm

---

## 2. Ba nhóm Design Pattern

### 2.1. Creational Patterns
Quản lý cách tạo object.
- Singleton
- Factory Method
- Abstract Factory
- Builder
- Prototype

### 2.2. Structural Patterns
Quản lý cách các class/object kết hợp với nhau.
- Adapter
- Bridge
- Composite
- Decorator
- Facade
- Flyweight
- Proxy

### 2.3. Behavioral Patterns
Quản lý cách các object tương tác và phân chia trách nhiệm.
- Chain of Responsibility
- Command
- Interpreter
- Iterator
- Mediator
- Memento
- Observer
- State
- Strategy
- Template Method
- Visitor

---

## 3. Singleton Pattern
### Mục đích
Đảm bảo một class chỉ có **đúng 1 instance** trong toàn chương trình.

### Cách làm
- constructor `private`
- field `static` giữ instance duy nhất
- method/property `static` trả về instance

### Ví dụ
```csharp
public class Singleton
{
    private static Singleton instance;

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }
    }
}
```

### Khi nào dùng
- quản lý cấu hình
- connection pool
- logger
- bất kỳ khi nào cần duy nhất 1 instance toàn cục

---

## 4. Factory Method Pattern
### Mục đích
Định nghĩa interface để tạo object, nhưng để subclass quyết định tạo class cụ thể nào.

### Cấu trúc
- `Creator` (abstract): có method `FactoryMethod()` trả về `Product`
- `ConcreteCreator`: override `FactoryMethod()` để tạo `ConcreteProduct`
- `Product` (abstract/interface)
- `ConcreteProduct`

### Ví dụ trong bài
Bài dùng ví dụ `Document` và `Application`:
- `Application` là Creator, có `CreateDocument()`
- `MyApplication` override tạo `MyDocument`

### Khi nào dùng
- khi class không biết trước sẽ tạo object thuộc class nào
- khi muốn subclass quyết định việc tạo object

---

## 5. Abstract Factory Pattern
### Mục đích
Cung cấp interface để tạo **nhóm các object liên quan** mà không cần biết class cụ thể.

### So với Factory Method
- Factory Method tạo 1 loại product
- Abstract Factory tạo **họ product** (nhiều loại liên quan)

### Ví dụ
Bài dùng ví dụ GUI cross-platform:
- `IWidgetFactory` có `CreateScrollBar()`, `CreateWindow()`
- `MotifWidgetFactory` tạo Motif widgets
- `PMWidgetFactory` tạo PM widgets

### Khi nào dùng
- hệ thống cần độc lập với cách tạo object
- cần đảm bảo các product trong cùng nhóm tương thích với nhau

---

## 6. Observer Pattern
### Mục đích
Khi 1 object thay đổi trạng thái, tất cả object phụ thuộc được **tự động thông báo**.

### Cấu trúc
- `Subject`: giữ danh sách observer, có `Attach()`, `Detach()`, `Notify()`
- `Observer`: có `Update()`
- `ConcreteSubject`: giữ state, gọi `Notify()` khi state thay đổi
- `ConcreteObserver`: implement `Update()` để phản ứng

### Liên hệ với C#
Observer pattern chính là nền tảng của **event** trong C#.

### Khi nào dùng
- 1 object thay đổi cần báo cho nhiều object khác
- không muốn các object phụ thuộc chặt vào nhau

---

## 7. Strategy Pattern
### Mục đích
Đóng gói thuật toán vào class riêng, cho phép thay đổi thuật toán runtime.

### Cấu trúc
- `Strategy` (interface): định nghĩa method chung
- `ConcreteStrategy`: cài đặt thuật toán cụ thể
- `Context`: giữ reference tới Strategy, gọi method của nó

### Ví dụ
Bài dùng ví dụ sort:
- `SortStrategy` interface có `Sort()`
- `QuickSort`, `ShellSort`, `MergeSort` là các ConcreteStrategy
- `SortedList` (Context) dùng strategy được truyền vào

### Khi nào dùng
- cần thay đổi thuật toán linh hoạt
- muốn tách thuật toán khỏi class sử dụng nó
- nhiều class chỉ khác nhau ở hành vi

---

## 8. Decorator Pattern
### Mục đích
Thêm chức năng cho object **động** mà không cần sửa class gốc.

### Cấu trúc
- `Component` (interface/abstract)
- `ConcreteComponent`: class gốc
- `Decorator` (abstract): giữ reference tới Component
- `ConcreteDecorator`: thêm chức năng mới

### Ví dụ
Bài dùng ví dụ LibraryItem và Decorator:
- `LibraryItem` là Component
- `Book`, `Video` là ConcreteComponent
- `Decorator` wrap LibraryItem
- `Borrowable` là ConcreteDecorator thêm khả năng mượn

### Khi nào dùng
- muốn thêm tính năng mà không sửa class gốc
- muốn kết hợp nhiều tính năng linh hoạt
- thay thế kế thừa sâu

---

## 9. Adapter Pattern
### Mục đích
Chuyển đổi interface của class này sang interface mà client mong đợi.

### Cấu trúc
- `Target`: interface client dùng
- `Adaptee`: class có sẵn nhưng interface không tương thích
- `Adapter`: chuyển đổi interface Adaptee sang Target

### Ví dụ
Bài dùng ví dụ chemical compound:
- `Compound` là Target
- `ChemicalDatabank` là Adaptee (có data nhưng interface khác)
- `RichCompound` là Adapter

### Khi nào dùng
- muốn dùng class có sẵn nhưng interface không khớp
- muốn tích hợp hệ thống cũ với code mới

---

## 10. Composite Pattern
### Mục đích
Tổ chức object theo cấu trúc **cây** (tree), xử lý leaf và composite giống nhau.

### Cấu trúc
- `Component` (abstract): interface chung
- `Leaf`: node lá, không có con
- `Composite`: có danh sách con, ủy quyền xuống con

### Khi nào dùng
- cấu trúc phân cấp (folder/file, menu/submenu)
- muốn client xử lý đồng nhất leaf và composite

---

## 11. Template Method Pattern
### Mục đích
Định nghĩa **khung thuật toán** trong base class, cho subclass override các bước cụ thể.

### Cấu trúc
- `AbstractClass`: có `TemplateMethod()` gọi các bước, một số bước là abstract
- `ConcreteClass`: override các bước abstract

### Khi nào dùng
- nhiều class có cùng khung xử lý nhưng khác chi tiết
- muốn kiểm soát luồng xử lý chung từ base class

---

## 12. State Pattern
### Mục đích
Cho phép object thay đổi hành vi khi **trạng thái nội bộ thay đổi**.

### Cấu trúc
- `Context`: giữ state hiện tại
- `State` (abstract): định nghĩa hành vi theo trạng thái
- `ConcreteState`: cài đặt hành vi cho từng trạng thái cụ thể

### Khi nào dùng
- object có nhiều trạng thái, hành vi khác nhau theo trạng thái
- thay thế if/switch dài về trạng thái

---

## 13. Tại sao học Design Patterns?
Bài nhấn mạnh:
- pattern giúp giải quyết vấn đề đã được chứng minh hiệu quả
- tạo ngôn ngữ chung giữa lập trình viên
- code dễ mở rộng, bảo trì
- không phải phát minh lại giải pháp

### Lưu ý
- không nên ép pattern vào mọi bài toán
- chọn pattern phù hợp với vấn đề thực tế
- hiểu bản chất hơn là học thuộc cấu trúc

---

## 14. Lỗi hay gặp
- áp dụng pattern sai ngữ cảnh
- over-engineering: dùng pattern khi bài toán đơn giản
- nhầm lẫn giữa các pattern gần giống nhau (Strategy vs State, Adapter vs Decorator)
- không hiểu mối quan hệ giữa pattern và nguyên lý OOP (abstraction, polymorphism)

---

## 15. Tóm tắt dễ hiểu
Bài này dạy:

- **Design Pattern** là giải pháp mẫu cho các vấn đề thiết kế phần mềm lặp lại.
- 3 nhóm: Creational, Structural, Behavioral.
- Các pattern trọng tâm trong bài:
  - **Singleton**: chỉ 1 instance
  - **Factory Method**: subclass quyết định tạo gì
  - **Abstract Factory**: tạo họ object liên quan
  - **Observer**: thông báo khi thay đổi (nền tảng event C#)
  - **Strategy**: đổi thuật toán linh hoạt
  - **Decorator**: thêm chức năng động
  - **Adapter**: chuyển đổi interface
  - **Composite**: cấu trúc cây
  - **Template Method**: khung thuật toán chung
  - **State**: hành vi thay đổi theo trạng thái

Nếu hiểu bài này, bạn sẽ thiết kế phần mềm tốt hơn, code dễ mở rộng và bảo trì hơn.

---

# B9. Serialization – Detailed Summary

## 1. Giới thiệu
Bài này nói về **Serialization** trong C#.

Serialization là quá trình:
- **chuyển object thành chuỗi byte** để lưu hoặc truyền đi
- **khôi phục object từ chuỗi byte** (Deserialization)

Mục tiêu thực tế:
- lưu dữ liệu xuống file
- truyền object qua mạng
- giao tiếp giữa các ứng dụng

---

## 2. Tại sao cần Serialization?
Object trong bộ nhớ là cấu trúc phức tạp:
- field
- reference tới object khác
- giá trị có kiểu

Để lưu hay truyền, cần chuyển thành dạng tuyến tính (stream of bytes).

---

## 3. Các loại Serialization trong .NET
Bài liệt kê 3 loại chính:

### 3.1. Binary Serialization
- lưu dạng nhị phân
- nhanh, nhỏ
- không đọc được bằng mắt thường

### 3.2. XML Serialization
- lưu dạng XML
- đọc được bằng mắt thường
- phổ biến trong giao tiếp web service cũ

### 3.3. SOAP Serialization
- lưu dạng SOAP (Simple Object Access Protocol)
- dùng cho web service kiểu cũ

---

## 4. ISerializable Interface
### Mục đích
Để serialize/deserialize object, có nhiều cách triển khai. Cách chính thống là class **implement interface ISerializable**.

### Yêu cầu
Class cần:
- implement `ISerializable`
- có method `GetObjectData(SerializationInfo info, StreamingContext context)`
- có constructor đặc biệt nhận `SerializationInfo` và `StreamingContext`

### Cấu trúc
```csharp
using System.Runtime.Serialization;

[Serializable]
class Student : ISerializable
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Constructor bình thường
    public Student() { }

    // Constructor cho deserialization
    public Student(SerializationInfo info, StreamingContext context)
    {
        Name = info.GetString("Name");
        Age = info.GetInt32("Age");
    }

    // Method cho serialization
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Name", Name);
        info.AddValue("Age", Age);
    }
}
```

### GetObjectData
Method này được gọi khi serialize:
```csharp
public void GetObjectData(SerializationInfo info, StreamingContext context)
{
    info.AddValue("key", value);
    // thêm từng field cần lưu
}
```

### Constructor deserialization
Constructor này được gọi khi deserialize:
```csharp
public Student(SerializationInfo info, StreamingContext context)
{
    PropertyName = info.GetValue("key", typeof(Type));
    // hoặc dùng GetString, GetInt32, v.v.
}
```

### Lưu ý
- `SerializationInfo` chứa dữ liệu dạng key-value
- `StreamingContext` chứa thông tin ngữ cảnh (ít dùng)
- key trong `AddValue` và `GetValue` phải khớp nhau

---

## 5. Binary Serialization
### Namespace cần
```csharp
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
```

### Cách đơn giản: dùng [Serializable]
Class muốn serialize có thể chỉ cần attribute `[Serializable]`:
```csharp
[Serializable]
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

C# tự động serialize tất cả field/property.

### Serialize (lưu)
```csharp
Student s = new Student { Name = "Alice", Age = 20 };

FileStream fs = new FileStream("data.bin", FileMode.Create);
BinaryFormatter bf = new BinaryFormatter();
bf.Serialize(fs, s);
fs.Close();
```

### Deserialize (đọc lại)
```csharp
FileStream fs = new FileStream("data.bin", FileMode.Open);
BinaryFormatter bf = new BinaryFormatter();
Student s = (Student)bf.Deserialize(fs);
fs.Close();
```

### NonSerialized
Nếu không muốn serialize 1 field:
```csharp
[Serializable]
public class Student
{
    public string Name { get; set; }

    [NonSerialized]
    public string Password;
}
```

---

## 6. XML Serialization
### Namespace cần
```csharp
using System.Xml.Serialization;
using System.IO;
```

### Yêu cầu class
- class phải `public`
- có constructor không tham số
- các member cần serialize phải là `public`

### Serialize (lưu)
```csharp
Student s = new Student { Name = "Alice", Age = 20 };

XmlSerializer xs = new XmlSerializer(typeof(Student));
StreamWriter sw = new StreamWriter("data.xml");
xs.Serialize(sw, s);
sw.Close();
```

### Deserialize (đọc lại)
```csharp
XmlSerializer xs = new XmlSerializer(typeof(Student));
StreamReader sr = new StreamReader("data.xml");
Student s = (Student)xs.Deserialize(sr);
sr.Close();
```

### Kiểm soát tên XML
```csharp
[XmlElement("TenSinhVien")]
public string Name { get; set; }

[XmlAttribute]
public int Age { get; set; }
```

Dùng:
- `[XmlElement]`: member là element con
- `[XmlAttribute]`: member là attribute trong tag
- `[XmlIgnore]`: bỏ qua member khi serialize

### Serialize danh sách
```csharp
[XmlArray("DanhSachSinhVien")]
[XmlArrayItem("SinhVien")]
public List<Student> Students { get; set; }
```

---

## 7. So sánh Binary và XML Serialization

| Tiêu chí | Binary | XML |
|---|---|---|
| Đọc được bằng mắt | Không | Có |
| Kích thước | Nhỏ hơn | Lớn hơn |
| Tốc độ | Nhanh hơn | Chậm hơn |
| Tương thích nền tảng | Kém hơn | Tốt hơn |
| Giao tiếp web service | Không phổ biến | Phổ biến |

---

## 8. SOAP Serialization
### Đặc điểm
- dùng cho giao tiếp qua web service dạng cũ
- dữ liệu được đóng gói trong envelope SOAP
- bài nhắc đến nhưng không nói chi tiết nhiều

### Namespace
```csharp
using System.Runtime.Serialization.Formatters.Soap;
```

### Cú pháp tương tự binary nhưng dùng SoapFormatter
```csharp
SoapFormatter sf = new SoapFormatter();
sf.Serialize(fs, obj);
```

---

## 9. DataContractSerializer và JsonConvert
Bài còn giới thiệu 2 cách serialize hiện đại hơn:

### DataContractSerializer
```csharp
using System.Runtime.Serialization;

DataContractSerializer serializer = new DataContractSerializer(typeof(StudentList));
serializer.WriteObject(filestream, list_of_students);
StudentList list = (StudentList)serializer.ReadObject(filestream);
```

### JsonConvert (Newtonsoft.Json)
```csharp
using Newtonsoft.Json;

string json = JsonConvert.SerializeObject(list_of_students);
StudentList students = JsonConvert.DeserializeObject<StudentList>(json);
```

Cả 2 đều hỗ trợ serialize mà không bắt buộc implement `ISerializable`, nhưng để đảm bảo OOP, vẫn khuyến cáo implement interface.

### Lưu ý quan trọng
- `System.Xml.Serialization.XmlSerializer` và `System.Text.Json.JsonSerializer` hỗ trợ serialize tự động mà không cần `ISerializable`
- Nhưng chỉ serialize property có **getter và setter public**
- Field private hoặc property chỉ có getter sẽ bị bỏ qua

---

## 10. File I/O cơ bản
Bài kết hợp giới thiệu File I/O vì serialization thường lưu vào file.

### Đọc file text
```csharp
StreamReader sr = new StreamReader("file.txt");
string line;
while ((line = sr.ReadLine()) != null)
{
    Console.WriteLine(line);
}
sr.Close();
```

### Ghi file text
```csharp
StreamWriter sw = new StreamWriter("file.txt");
sw.WriteLine("Hello");
sw.Close();
```

### Dùng using để đảm bảo đóng file
```csharp
using (StreamReader sr = new StreamReader("file.txt"))
{
    string content = sr.ReadToEnd();
    Console.WriteLine(content);
}
```

### FileStream
```csharp
FileStream fs = new FileStream("data.bin", FileMode.Create);
// đọc/ghi binary
fs.Close();
```

---

## 11. Các class I/O quan trọng
- `File`: utility method đọc/ghi nhanh
- `FileStream`: đọc/ghi binary
- `StreamReader`: đọc text
- `StreamWriter`: ghi text
- `BinaryReader`: đọc kiểu binary theo kiểu dữ liệu
- `BinaryWriter`: ghi kiểu binary theo kiểu dữ liệu

---

## 12. Đọc ghi với BinaryReader/BinaryWriter
```csharp
// Ghi
BinaryWriter bw = new BinaryWriter(File.Open("data.bin", FileMode.Create));
bw.Write(1.25);
bw.Write("Hello");
bw.Write(true);
bw.Close();

// Đọc
BinaryReader br = new BinaryReader(File.Open("data.bin", FileMode.Open));
double d = br.ReadDouble();
string s = br.ReadString();
bool b = br.ReadBoolean();
br.Close();
```

Lưu ý:
- đọc theo đúng thứ tự đã ghi
- đọc đúng kiểu đã ghi

---

## 13. Directory và Path
```csharp
string current = Directory.GetCurrentDirectory();
string[] files = Directory.GetFiles(".");
string[] dirs = Directory.GetDirectories(".");
```

Kết hợp đường dẫn:
```csharp
string path = Path.Combine("folder", "file.txt");
string ext = Path.GetExtension("file.txt");
string name = Path.GetFileNameWithoutExtension("file.txt");
```

---

## 14. Lỗi hay gặp
- quên đóng file (dùng `using` để tránh)
- quên attribute `[Serializable]` với binary serialization
- dùng field `private` trong XML serialization nhưng không serialize được
- đọc nhầm thứ tự hoặc sai kiểu với BinaryReader
- serialization class có field không serializable mà không dùng `[NonSerialized]`

---

## 15. Lưu ý thực tế
- Hiện nay, `BinaryFormatter` bị Microsoft cảnh báo **deprecated** và không nên dùng trong code mới vì lý do bảo mật. Nhưng bài học dùng nó cho mục đích hiểu khái niệm.
- Trong thực tế hiện đại, thường dùng **JSON Serialization** (`System.Text.Json` hoặc `Newtonsoft.Json`) thay thế.
- XML Serialization vẫn được dùng trong nhiều hệ thống cũ và cấu hình.

---

## 16. Tóm tắt dễ hiểu
Bài này dạy:

- **Serialization** là chuyển object thành chuỗi byte/text để lưu hoặc truyền.
- **Deserialization** là chiều ngược lại.
- Cách chính thống: class implement **ISerializable**, có `GetObjectData()` và constructor nhận `SerializationInfo`.
- Cách đơn giản: dùng `[Serializable]` attribute, C# tự serialize.
- C# hỗ trợ 3 loại: Binary, XML, SOAP.
- **Binary**: nhanh, nhỏ, nhưng không đọc được bằng mắt.
- **XML**: đọc được, tương thích tốt, phổ biến hơn cho giao tiếp.
- Các công cụ hiện đại: `DataContractSerializer`, `JsonConvert`, `XmlSerializer`, `JsonSerializer`.
- File I/O là nền tảng để lưu kết quả serialization.
- Dùng `using` để đảm bảo tài nguyên file luôn được giải phóng.

Nếu hiểu bài này, bạn có thể:
- lưu trạng thái ứng dụng
- đọc/ghi cấu hình
- chuẩn bị nền tảng cho JSON serialization hiện đại
