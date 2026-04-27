# XML Documentation Comments trong C#

Tất cả XML docs bắt đầu bằng `///` (ba dấu slash). IDE tự động sinh khung khi gõ `///` phía trên một thành phần có thể có tài liệu (`class`, `method`, `property`, `enum`, `delegate`, `event`...).

---

## 1. Các thẻ cơ bản

| Thẻ | Mục đích | Ví dụ |
|---|---|---|
| `<summary>` | Mô tả ngắn gọn — "Đối tượng/phương thức này làm gì?" | `<summary>Lấy danh sách các xe đang hoạt động.</summary>` |
| `<remarks>` | Bổ sung chi tiết: thuật toán, ràng buộc, chú ý khi sử dụng | `<remarks>Phương thức này không an toàn luồng.</remarks>` |
| `<param name="...">` | Mô tả tham số — tên phải khớp chính xác | `<param name="vehicleId">ID của xe cần tìm.</param>` |
| `<returns>` | Mô tả giá trị trả về | `<returns>Đối tượng VehicleData, hoặc null nếu không tìm thấy.</returns>` |
| `<exception cref="...">` | Liệt kê ngoại lệ có thể bị ném | `<exception cref="ArgumentNullException">Nếu vehicleId là null.</exception>` |
| `<example>` | Ví dụ sử dụng — thường chứa `<code>` bên trong | Xem mục 3 |

---

## 2. Các thẻ nâng cao và liên kết

| Thẻ | Mục đích |
|---|---|
| `<see cref="..."/>` | Tạo liên kết đến class, method, property khác — dùng trong `<summary>` hoặc `<remarks>` |
| `<seealso cref="..."/>` | Tạo mục "Xem thêm" — thường đặt ở cuối comment |
| `<typeparam name="...">` | Mô tả tham số kiểu generic (tương tự `<param>` nhưng dành cho `<T>`) |
| `<inheritdoc/>` | Kế thừa toàn bộ tài liệu từ lớp cha hoặc interface — hữu ích khi ghi đè phương thức không thay đổi ngữ nghĩa |
| `<list type="bullet">` | Tạo danh sách trong `<remarks>` |
| `<para>` | Ngắt đoạn trong `<summary>` hoặc `<remarks>` |
| `<c>` | Đánh dấu đoạn code ngắn (inline) |
| `<code>` | Khối code — thường dùng kèm `<example>` |

---

## 3. Ví dụ đầy đủ

```csharp
/// <summary>
/// Tính tiền cước vận chuyển dựa trên trọng lượng và tuyến đường.
/// </summary>
/// <remarks>
/// Phương thức này sử dụng chiến lược tính phí được inject qua constructor.
/// <para>Không thread-safe khi thay đổi strategy đồng thời.</para>
/// </remarks>
/// <param name="weight">Trọng lượng hàng hóa (kg). Phải lớn hơn 0.</param>
/// <param name="origin">Tỉnh/thành phố gửi hàng.</param>
/// <param name="destination">Tỉnh/thành phố nhận hàng.</param>
/// <returns>
/// Chi phí vận chuyển tính bằng VND.
/// </returns>
/// <exception cref="ArgumentException">
/// Ném khi <paramref name="weight"/> nhỏ hơn hoặc bằng 0.
/// </exception>
/// <example>
/// <code>
/// var calculator = new ShippingCalculator(new UpsShipping());
/// decimal cost = calculator.Calculate(2.5m, "HCMC", "Hanoi");
/// Console.WriteLine(cost); // 16.25
/// </code>
/// </example>
/// <seealso cref="IShippingStrategy"/>
public decimal Calculate(decimal weight, string origin, string destination)
{
    if (weight <= 0) throw new ArgumentException("Weight must be positive.", nameof(weight));
    return _strategy.CalculateCost(weight, origin, destination);
}
```

---

## 4. `<inheritdoc/>` — Kế thừa tài liệu

```csharp
public interface IShippingStrategy
{
    /// <summary>
    /// Tính chi phí vận chuyển dựa trên trọng lượng và tuyến đường.
    /// </summary>
    decimal CalculateCost(decimal weight, string origin, string dest);
}

public class UpsShipping : IShippingStrategy
{
    /// <inheritdoc/>  ← Kế thừa toàn bộ doc từ interface, không cần viết lại
    public decimal CalculateCost(decimal weight, string origin, string dest)
        => weight * 2.5m + 10;
}
```

---

## 5. `<see cref>` và `<seealso cref>` — Liên kết chéo

```csharp
/// <summary>
/// Xử lý yêu cầu đặt xe. Sử dụng <see cref="RideMatchingService"/> để tìm tài xế
/// và <see cref="FareCalculationService"/> để tính giá.
/// </summary>
/// <seealso cref="IRideRepository"/>
/// <seealso cref="IDriverRepository"/>
public Task<Trip> RequestTripAsync(Guid passengerId, Location pickup, Location destination) { ... }
```

---

## 6. `<list>` — Danh sách trong `<remarks>`

```csharp
/// <remarks>
/// Các trạng thái hợp lệ để hủy chuyến:
/// <list type="bullet">
///   <item><description>Searching — đang tìm tài xế</description></item>
///   <item><description>Matched — đã ghép nhưng chưa đón</description></item>
///   <item><description>Arrived — tài xế đã đến điểm đón</description></item>
/// </list>
/// Không thể hủy khi trạng thái là <c>Started</c> hoặc <c>Completed</c>.
/// </remarks>
```

---

## 7. `<typeparam>` — Generic Documentation

```csharp
/// <summary>
/// Repository generic cho bất kỳ entity nào có định danh kiểu <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TEntity">Kiểu entity được quản lý.</typeparam>
/// <typeparam name="TId">Kiểu định danh của entity (thường là <see cref="Guid"/> hoặc <c>int</c>).</typeparam>
public interface IRepository<TEntity, TId>
{
    /// <summary>Lấy entity theo ID.</summary>
    /// <param name="id">Định danh duy nhất của entity.</param>
    /// <returns>Entity nếu tìm thấy; ngược lại <c>null</c>.</returns>
    TEntity GetById(TId id);
}
```

---

## 8. Các quan hệ OOP trong XML docs

| Quan hệ | Cách ghi trong XML doc |
|---|---|
| Kế thừa | `<inheritdoc/>` hoặc `<see cref="BaseClass"/>` |
| Composition | `<remarks>: "Lớp này sở hữu instance của B và kiểm soát vòng đời (Composition)."` |
| Dependency | `<param name="strategy">Chiến lược tính phí.</param>` + `<seealso cref="IShippingStrategy"/>` |
| Realization | `<summary>Triển khai <see cref="IShippingStrategy"/> cho dịch vụ UPS.</summary>` |
