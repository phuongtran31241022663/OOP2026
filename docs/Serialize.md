# Serialize và Deserialize trong C#

## 1. Định nghĩa và vai trò trong OOP

- **Serialization (tuần tự hóa)**: Chuyển đổi đối tượng trong bộ nhớ thành định dạng phẳng có thể lưu trữ hoặc truyền tải (JSON, XML, binary).
- **Deserialization**: Quá trình ngược — tái tạo đối tượng từ dữ liệu đã tuần tự hóa.

Serialization tập trung vào **bảo tồn trạng thái**; hành vi được phục hồi thông qua metadata của lớp.

---

## 2. Các kỹ thuật Serialization trong C#

### 2.1 `System.Text.Json` — Tiêu chuẩn hiện đại, hiệu năng cao

Ra mắt từ .NET Core 3.0, là lựa chọn mặc định cho JSON trong ứng dụng mới.

```csharp
using System.Text.Json;

var person = new Person { Name = "John", Age = 30 };
string json = JsonSerializer.Serialize(person);
// {"Name":"John","Age":30}

Person deserialized = JsonSerializer.Deserialize<Person>(json);
```

**Kiểm soát bằng Attributes:**
- `[JsonIgnore]` — loại trừ thuộc tính.
- `[JsonPropertyName("custom_name")]` — ánh xạ tên.
- `[JsonInclude]` — bao gồm thuộc tính private.
- `[JsonConstructor]` — chỉ định constructor dùng khi deserialize.

**Record bất biến:**

```csharp
public record Person(string Name, int Age);

var p  = new Person("Jane", 25);
string json = JsonSerializer.Serialize(p);
var p2 = JsonSerializer.Deserialize<Person>(json);
// System.Text.Json tự tìm constructor có tham số khớp tên
```

### 2.2 `Newtonsoft.Json` — Cực kỳ linh hoạt

Vẫn cần thiết cho các kịch bản phức tạp: type handling, custom converters, LINQ to JSON.

**Xử lý tham chiếu vòng:**

```csharp
var settings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    PreserveReferencesHandling = PreserveReferencesHandling.Objects
};
```

**Bảo toàn đa hình:**

```csharp
var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.Objects,
    SerializationBinder = new KnownTypesBinder()
};
```

---

## 3. Xử lý tính chất OOP

### 3.1 Đóng gói và quyền truy xuất

`System.Text.Json` serialize/deserialize các thành viên `private` nếu được đánh dấu `[JsonInclude]` hoặc `JsonSerializerOptions.IncludeFields = true`.

### 3.2 Kế thừa và đa hình — `[JsonDerivedType]`

```csharp
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
public abstract class Animal { }
```

JSON sẽ chứa discriminator `$type` để deserialize đúng kiểu dẫn xuất.

### 3.3 Object graph và tham chiếu vòng

```csharp
var options = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.Preserve
};
// Lưu metadata $id, $ref để xử lý tham chiếu vòng
```

---

## 4. Kiểm soát quá trình tuần tự hóa

### 4.1 Callbacks

`[OnSerializing]`, `[OnSerialized]`, `[OnDeserializing]`, `[OnDeserialized]` — thực thi code trước/sau serialize để chuẩn bị trạng thái, kiểm tra tính toàn vẹn.

### 4.2 Custom Converters

```csharp
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateOnly.Parse(reader.GetString());

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
}
// Đăng ký: JsonSerializerOptions.Converters.Add(new DateOnlyConverter())
```

---

## 5. So sánh `System.Text.Json` vs `Newtonsoft.Json`

| Tiêu chí | `System.Text.Json` | `Newtonsoft.Json` |
|---|---|---|
| Tốc độ serialize | Rất nhanh | Trung bình |
| Quản lý bộ nhớ | Ít cấp phát | Nhiều hơn |
| Tính năng | Cơ bản + discriminator, reference handler | Rất phong phú (LINQ to JSON, converter dễ dàng...) |
| Tương thích | .NET Core 3.0+ | Mọi nền tảng |
| Bảo mật | Cao hơn (không tự type loading) | Cần cấu hình cẩn thận |

---

## 6. Bảo mật và nguy cơ

- Tránh `BinaryFormatter`, `NetDataContractSerializer` với kiểu không kiểm soát.
- Với `TypeNameHandling` (Newtonsoft) — phải kết hợp `SerializationBinder` để whitelist các kiểu được phép.
- `System.Text.Json` an toàn hơn nhờ không tự động resolve type từ payload.

---

## 7. Ứng dụng trong kiến trúc OOP

| Tầng | Cách dùng |
|---|---|
| DTO | Object chỉ chứa dữ liệu để truyền qua network — kết hợp `record` |
| DDD Aggregate | Serialize cả aggregate sang JSON/BSON cho NoSQL |
| Message Queuing | Serialize trước khi gửi (RabbitMQ, Azure Service Bus), deserialize khi nhận |

---

## 8. Kết luận

Serialize và deserialize trong C# đòi hỏi cân bằng giữa tính đóng gói, kế thừa, hiệu năng và bảo mật. Với sự phát triển của `System.Text.Json` và các tính năng như `record`, `JsonDerivedType`, C# đã cung cấp nền tảng mạnh mẽ để xử lý tuần tự hóa an toàn, nhanh chóng và phù hợp với nguyên lý hướng đối tượng hiện đại.
