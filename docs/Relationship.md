# Các mối quan hệ trong thiết kế hướng đối tượng và cách thể hiện trong C#

## 1. Kế thừa — Quan hệ "is-a" (là một)

**Tên gọi khác:** Generalization – Specialization, Derivation, Parent-Child.

**Ý nghĩa:** Lớp dẫn xuất là một phiên bản cụ thể của lớp cơ sở. Ví dụ: `Dog` là một `Animal`, `Car` là một `Vehicle`.

```csharp
public class Animal { }
public class Dog : Animal { }   // Dog is-a Animal
```

**Ưu điểm:** Tái sử dụng code, đa hình (`virtual/override`), dễ mở rộng cây phân cấp.

**Nhược điểm:** Tạo liên kết chặt chẽ — khó thay đổi lớp cha mà không ảnh hưởng lớp con ("fragile base class").

**Khi nào dùng:** Khi có mối quan hệ tự nhiên và lớp con thực sự là một loại của lớp cha. Tuân thủ **Liskov Substitution Principle (LSP)**: lớp con có thể thay thế lớp cha mà không gây lỗi.

> **Nguyên tắc:** "Ưu tiên thành phần hơn kế thừa" (Composition over Inheritance) — nếu chỉ đơn thuần muốn dùng lại chức năng, hãy dùng quan hệ "has-a".

---

## 2. Ủy thác — Quan hệ "has-a" và "part-of"

### 2.1 Composition (Thành phần) — "part-of" mạnh

**Tên gọi khác:** Strong ownership, Whole-Part (phần không thể tồn tại độc lập).

**Ý nghĩa:** Đối tượng chứa **sở hữu** và **kiểm soát vòng đời** của thành phần. Nếu đối tượng chứa bị hủy, thành phần cũng bị hủy theo.

> **Ví dụ:** `House` composed of `Room` — phòng không thể tồn tại nếu nhà bị phá.

```csharp
public class House
{
    private readonly List<Room> _rooms = new List<Room>();

    public House()
    {
        _rooms.Add(new Room("Living room"));
        _rooms.Add(new Room("Kitchen"));
    }
}
```

**Đặc điểm:** Đối tượng thành phần không được truyền từ bên ngoài, thường là `private readonly`, không có setter công khai. Thể hiện tính đóng gói cao.

### 2.2 Aggregation (Tập hợp) — "has-a" yếu

**Tên gọi khác:** Weak ownership, Whole-Part (phần có thể tồn tại độc lập).

**Ý nghĩa:** Đối tượng chứa nhóm các đối tượng khác, nhưng chúng có **vòng đời độc lập** — được tạo bên ngoài và truyền vào qua constructor hoặc setter.

> **Ví dụ:** `University` aggregates `Student` — sinh viên vẫn tồn tại nếu trường đóng cửa.

```csharp
public class University
{
    private readonly List<Student> _students;

    public University(IEnumerable<Student> students)
    {
        _students = new List<Student>(students);
    }
}
```

**So sánh Composition vs Aggregation:**

| Tiêu chí | Composition | Aggregation |
|---|---|---|
| Vòng đời thành phần | Phụ thuộc đối tượng chứa | Độc lập |
| Khởi tạo | Trong constructor của đối tượng chứa | Truyền từ bên ngoài |
| Độ chặt | Chặt hơn | Lỏng hơn, phù hợp DI |

---

## 3. Liên kết (Association) — Quan hệ "knows-a" (biết về)

**Tên gọi khác:** Uses-a (có sự khác biệt với Dependency), Communication, Navigability.

**Ý nghĩa:** Hai lớp có liên kết, một đối tượng có thể tương tác với đối tượng kia mà không nhất thiết phải sở hữu. Composition và Aggregation là những dạng đặc biệt của Association.

```csharp
public class Driver
{
    public Car Vehicle { get; set; } // Driver biết về Car, nhưng không sở hữu nó
}
```

**So sánh với Aggregation:** Association là thuật ngữ tổng quát; Aggregation là association nhưng có ngữ nghĩa nhóm/container.

---

## 4. Phụ thuộc (Dependency) — Quan hệ "uses-a" (sử dụng tạm thời)

**Tên gọi khác:** Uses-a (phổ biến nhất), Method-local, Parameter-level.

**Ý nghĩa:** Lớp A phụ thuộc vào lớp B nếu A sử dụng B như **tham số phương thức**, giá trị trả về, hoặc biến cục bộ. Sự phụ thuộc **không kéo dài** (không lưu trong field).

```csharp
public class ReportPrinter
{
    public void Print(Report report, IPrinter printer)
    {
        printer.Print(report); // ReportPrinter phụ thuộc vào IPrinter
    }
}
```

**Phân biệt rõ:**
- Giữ `reference` của B bên trong A (field) → **Association / Aggregation / Composition**.
- Chỉ dùng B trong một phương thức (tham số, local variable) → **Dependency thực sự**.

---

## 5. Hiện thực hóa (Realization) — Quan hệ "can-do" (có thể làm)

**Tên gọi khác:** Implementation, Interface realization, Contract.

**Ý nghĩa:** Một lớp cam kết thực hiện hành vi được định nghĩa bởi interface.

```csharp
public interface IShippingStrategy { decimal Calculate(...); }
public class UpsStrategy : IShippingStrategy { ... }
```

**So sánh với Kế thừa:**

| Tiêu chí | Kế thừa (is-a) | Hiện thực hóa (can-do) |
|---|---|---|
| Chia sẻ | Trạng thái + hành vi | Chỉ hành vi (hợp đồng) |
| Đa kế thừa | Không hỗ trợ trong C# | Hỗ trợ (đa interface) |
| Diamond problem | Có thể gặp | Tránh được |

> **DIP:** Strategy, Observer, State đều dựa trên Realization — Context phụ thuộc vào interface, ConcreteStrategy hiện thực hóa interface. "Phụ thuộc vào abstraction, không phụ thuộc vào concrete."

---

## 6. Áp dụng vào XML Documentation

```csharp
/// <summary>
/// Form chính, hiển thị bản đồ GMap.NET và theo dõi vị trí xe.
/// <para>Quan hệ: Form này <b>has-a</b> <see cref="GMapControl"/> (Composition)
/// và <b>uses</b> <see cref="DataFetcher"/> thông qua Dependency Injection.</para>
/// </summary>
public partial class MainForm : Form
{
    private readonly DataFetcher _fetcher;          // Aggregation: inject, vòng đời độc lập
    private readonly List<VehicleMarker> _markers = new(); // Composition: sở hữu marker
}
```

---

## 7. Cạm bẫy thường gặp

1. **Nhầm "is-a" và "has-a"**: Muốn dùng lại code của `List<T>` bèn kế thừa `MyList : List<T>` — vi phạm đóng gói. Đúng ra: `class MyList { private List<T> _items; }`.
2. **Quan hệ quá chặt**: Dùng Composition mà lại truyền reference ra ngoài — phá hỏng "ownership".
3. **Aggregation nhưng lại dispose đối tượng**: Nếu đối tượng được chia sẻ, không được `Dispose` khi đối tượng chứa chết đi.
4. **Lười phân biệt Association và Dependency**: Field reference → Association; tham số/local variable → Dependency.

---

## 8. Kết luận

Hiểu rõ các mối quan hệ và biết cách gọi tên chúng giúp:

- **Thiết kế rõ ràng hơn**: biết khi nào kế thừa, khi nào dùng interface, khi nào dùng thành phần.
- **Giao tiếp hiệu quả**: "A có quan hệ Aggregation với B" thay vì nói mơ hồ "class A dùng class B".
- **Viết tài liệu chính xác**: XML docs phản ánh đúng ý đồ thiết kế.
- **Áp dụng Design Pattern đúng cách**: Strategy dùng Realization + Dependency/Aggregation; Observer dùng Realization + Association.
