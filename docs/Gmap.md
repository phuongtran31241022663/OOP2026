# GMap.NET

GMap.NET là bộ thư viện mạnh mẽ, miễn phí và mã nguồn mở, cho phép bạn tích hợp bản đồ từ đa dạng nhà cung cấp vào ứng dụng .NET của mình.

## Kiến trúc thư viện: Sự phân chia giữa "Lõi" và "Vỏ"

Để đạt được sự linh hoạt và khả năng tương thích đa nền tảng, GMap.NET được tổ chức theo kiến trúc phân lớp rất rõ ràng, tương tự như cách bạn đã biết về việc tách biệt Interface và Implementation trong các Design Pattern:

### GMap.NET.Core

Đây là thư viện nền tảng, chứa toàn bộ logic nghiệp vụ (business logic) và dữ liệu của hệ thống bản đồ nhưng hoàn toàn không phụ thuộc vào bất kỳ một công nghệ giao diện (UI) cụ thể nào. Nó bao gồm các thành phần cốt lõi như:

- **Map Providers**: Định nghĩa cách kết nối và tải bản đồ (dạng ô - tile) từ hàng chục nhà cung cấp như Google Maps, Bing Maps, OpenStreetMap, Here, Yandex, và thậm chí cả các nhà cung cấp trong nước như VNMap, các nhà cung cấp Trung Quốc như Amap (AutoNavi) hay Baidu.
- **Geocoding & Routing**: Các dịch vụ chuyển đổi địa chỉ thành tọa độ (Geocoding) và ngược lại (Reverse Geocoding), cùng với các thuật toán tìm đường (Routing).
- **Projections & Transformations**: Xử lý các phép chiếu bản đồ và chuyển đổi giữa tọa độ địa lý (kinh độ/vĩ độ) và tọa độ điểm ảnh trên màn hình.
- **Caching System**: Một hệ thống cache mạnh mẽ để lưu trữ các ô bản đồ (tiles) đã tải về, cho phép ứng dụng của bạn hoạt động ổn định ngay cả khi không có kết nối mạng, một tính năng cực kỳ quan trọng cho các ứng dụng thực địa.

### GMap.NET.WinForms

Đây chính là "lớp vỏ" giao diện được xây dựng dành riêng cho nền tảng Windows Forms. Nhiệm vụ duy nhất của nó là kế thừa mọi sức mạnh từ `GMap.NET.Core` và "bọc" chúng lại thành một control trực quan mà bạn có thể kéo thả vào Form của mình. Control này (`GMapControl`) chịu trách nhiệm:

- Hiển thị các ô bản đồ (tiles) được cung cấp bởi `Core`.
- Xử lý các tương tác người dùng như kéo, thả, click chuột, và chuyển chúng thành các sự kiện (events) trong C#.
- Quản lý việc vẽ các đối tượng phủ (overlays) như marker, đường đi, hình đa giác lên trên bản đồ.

> Sự phân chia này cho phép mã nguồn của bạn dễ dàng được tái sử dụng. Nếu sau này bạn quyết định chuyển ứng dụng từ Windows Forms sang WPF hay Avalonia, bạn chỉ cần thay đổi gói UI (ví dụ: `GMap.NET.WinPresentation`) mà không cần phải viết lại bất kỳ logic bản đồ nào từ `GMap.NET.Core`.

---

## Ứng dụng thực tế trong Windows Forms

### 1. Khởi tạo bản đồ cơ bản

Sau khi cài đặt các gói NuGet cần thiết, bạn chỉ cần kéo thả `GMapControl` từ Toolbox vào Form của mình. Mọi thao tác cấu hình đều có thể thực hiện bằng code C# một cách tường minh, ví dụ:

```csharp
// Sử dụng using statement đúng như các thư viện khác
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

// Trong hàm khởi tạo của Form (Form_Load)
private void Form1_Load(object sender, EventArgs e)
{
    // 1. Chọn nhà cung cấp bản đồ (Map Provider)
    gMapControl1.MapProvider = GMapProviders.GoogleMap;

    // 2. Thiết lập vị trí trung tâm ban đầu (ví dụ: Hà Nội)
    gMapControl1.Position = new PointLatLng(21.0278, 105.8342);

    // 3. Thiết lập mức zoom (càng cao càng chi tiết)
    gMapControl1.Zoom = 12;

    // 4. (Tùy chọn) Cấu hình chế độ cache
    gMapControl1.CacheLocation = @"D:\MyMapCache"; // Thư mục lưu cache
    gMapControl1.Manager.Mode = AccessMode.ServerAndCache; // Dùng cả online & offline
}
```
