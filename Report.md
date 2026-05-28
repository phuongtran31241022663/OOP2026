**BỘ GIÁO DỤC VÀ ĐÀO TẠO**

**ĐẠI HỌC KINH TẾ TP HỒ CHÍ MINH (UEH)**

**TRƯỜNG CÔNG NGHỆ VÀ THIẾT KẾ**

---

**ĐỒ ÁN MÔN HỌC**

**ĐỀ TÀI**  
**XÂY DỰNG CHƯƠNG TRÌNH KẾT NỐI TÀI XẾ (RIDEGO)**

**Học phần**: Lập trình hướng đối tượng  
**Danh sách nhóm**: 
1. TRẦN YẾN PHƯỢNG
2. LÊ TÔ NGUYỆT MINH

**Chuyên ngành**: CÔNG NGHỆ PHẦN MỀM  
**Khóa**: K50  
**Giảng viên**: TS. Đặng Ngọc Hoàng Thành

**Tp. Hồ Chí Minh, Ngày 29 tháng 5 năm 2026**

---

# **MỤC LỤC**

**[CHƯƠNG 1: PHẦN MỞ ĐẦU](#chương-1-phần-mở-đầu)**
- [1.1. Tính cấp thiết của đề tài](#11-tính-cấp-thiết-của-đề-tài)
- [1.2. Giới thiệu về Kỹ thuật lập trình hướng đối tượng (LTHĐT)](#12-giới-thiệu-về-kỹ-thuật-lập-trình-hướng-đối-tượng-lthđt)
- [1.3. Tầm quan trọng của LTHĐT trong Kỹ thuật phần mềm](#13-tầm-quan-trọng-của-lthđt-trong-kỹ-thuật-phần-mềm)

**[CHƯƠNG 2: PHÂN TÍCH VÀ THIẾT KẾ LỚP](#chương-2-phân-tích-và-thiết-kế-lớp)**
- [2.1. Phân tích bài toán](#21-phân-tích-bài-toán)
- [2.2. Thiết kế lớp và Sơ đồ lớp (Class diagrams)](#22-thiết-kế-lớp-và-sơ-đồ-lớp-class-diagrams)
- [2.3. Cài đặt các lớp chức năng](#23-cài-đặt-các-lớp-chức-năng)

**[CHƯƠNG 3: XÂY DỰNG ỨNG DỤNG](#chương-3-xây-dựng-ứng-dụng)**
- [3.1. Thiết kế giao diện chương trình](#31-thiết-kế-giao-diện-chương-trình)
- [3.2. Phát triển các chức năng của ứng dụng](#32-phát-triển-các-chức-năng-của-ứng-dụng)
- [3.3. Các kịch bản thực thi ứng dụng](#33-các-kịch-bản-thực-thi-ứng-dụng)

**[CHƯƠNG 4: THẢO LUẬN VÀ ĐÁNH GIÁ](#chương-4-thảo-luận-và-đánh-giá)**
- [4.1. Các kết quả nhận được](#41-các-kết-quả-nhận-được)
- [4.2. Một số tồn tại](#42-một-số-tồn-tại)
- [4.3. Hướng phát triển](#43-hướng-phát-triển)

**[PHỤ LỤC](#phụ-lục)**

**[TÀI LIỆU THAM KHẢO](#tài-liệu-tham-khảo)**

---

# CHƯƠNG 1: PHẦN MỞ ĐẦU {#chương-1-phần-mở-đầu}

## 1.1. Tính cấp thiết của đề tài {#11-tính-cấp-thiết-của-đề-tài}

Sự phát triển đô thị hóa và nhu cầu di chuyển cá nhân gia tăng đã đưa các ứng dụng gọi xe công nghệ trở thành một phần thiết yếu của đời sống hiện đại. Người dùng mong muốn có thể đặt xe nhanh chóng, biết trước chi phí, theo dõi lộ trình và đánh giá chất lượng dịch vụ ngay trên một nền tảng thống nhất. Về phía tài xế, họ cần một hệ thống hỗ trợ tiếp nhận chuyến đi một cách thuận tiện, quản lý trạng thái làm việc và theo dõi thu nhập một cách minh bạch. Vì vậy, việc xây dựng một hệ thống kết nối tài xế và hành khách là một bài toán mang tính thực tế cao.

Đề tài này, thông qua việc mô phỏng mô hình nền tảng trung gian trong lĩnh vực vận tải công nghệ, mang lại ý nghĩa kinh tế - xã hội quan trọng. Hệ thống minh họa cách tối ưu hóa việc sử dụng phương tiện, giảm thiểu thời gian chờ của hành khách và tạo ra thêm cơ hội việc làm linh hoạt cho tài xế. Mặc dù chỉ là mô hình học thuật, đề tài vẫn phản ánh được các nghiệp vụ cốt lõi thường gặp trong các hệ thống thương mại điện tử và dịch vụ số hiện đại.

Về mặt ý nghĩa học tập và nghiên cứu, đề tài cung cấp môi trường lý tưởng để nhóm áp dụng kiến thức về phân tích yêu cầu, thiết kế lớp, xây dựng giao diện và tổ chức mã nguồn theo hướng đối tượng. Qua quá trình phát triển một hệ thống có nhiều tác nhân, nhiều trạng thái nghiệp vụ và cơ chế lưu trữ dữ liệu, nhóm có cơ hội trải nghiệm quy trình xây dựng phần mềm tương đối hoàn chỉnh, từ giai đoạn phân tích bài toán cho đến hiện thực chương trình.

## 1.2. Giới thiệu về Kỹ thuật lập trình hướng đối tượng (LTHĐT) {#12-giới-thiệu-về-kỹ-thuật-lập-trình-hướng-đối-tượng-lthđt}

Lập trình hướng đối tượng (Object-Oriented Programming - OOP) là phương pháp lập trình dựa trên việc mô hình hóa chương trình thành các lớp và đối tượng. Mỗi đối tượng đại diện cho một thực thể trong bài toán thực tế, sở hữu dữ liệu riêng và các hành vi tương ứng. Trong đề tài này, các thực thể như hành khách, tài xế, phương tiện, chuyến đi và chính sách giá cước được xây dựng thành các lớp độc lập, phản ánh chính xác cấu trúc của hệ thống thực tế.

Bốn đặc điểm cốt lõi của lập trình hướng đối tượng được áp dụng trong đề tài bao gồm:

- **Đóng gói (Encapsulation):** Dữ liệu bên trong lớp được bảo vệ nghiêm ngặt thông qua các thuộc tính (Properties) và phương thức (Methods). Việc truy cập và thay đổi dữ liệu chỉ được thực hiện qua các giao diện công khai, đảm bảo tính toàn vẹn của đối tượng.
- **Kế thừa (Inheritance):** Các lớp con kế thừa những đặc điểm chung từ lớp cha, giúp tái sử dụng mã nguồn hiệu quả và thiết lập mối quan hệ phân cấp logic giữa các thực thể.
- **Đa hình (Polymorphism):** Cho phép các đối tượng khác nhau phản hồi khác nhau đối với cùng một thông điệp hoặc phương thức trừu tượng, tăng tính linh hoạt cho hệ thống.
- **Trừu tượng (Abstraction):** Tập trung vào những thông tin và hành vi cần thiết của đối tượng đối với thế giới bên ngoài, ẩn đi các chi tiết cài đặt phức tạp bên trong.

Trong đồ án, nguyên lý đóng gói được cụ thể hóa bằng việc bảo vệ dữ liệu qua các thuộc tính `private`/`protected` và chỉ cho phép truy cập thông qua `Properties`. Trạng thái chuyến đi chỉ được thay đổi theo đúng quy trình nghiệp vụ do `State Pattern` định nghĩa. Tính kế thừa được tận dụng triệt để khi `Psg` và `Drv` đều kế thừa từ lớp `Usr`, `Car` và `Moto` kế thừa từ `Veh`, còn `JsonRepository<T>` đóng vai trò lớp cha tổng quát cho mọi repository. Đa hình thể hiện qua việc `Trip` tương tác với interface `ITripState` mà không cần biết trạng thái cụ thể, hay `MatchSvc` xử lý danh sách `Usr` nhưng vẫn áp dụng logic riêng cho `Drv`. Tính trừu tượng được đảm bảo nhờ sử dụng các `Interface` (`ITripCmd`, `IUsrRepo`) để tách biệt định nghĩa hành vi khỏi chi tiết cài đặt, giúp tầng giao diện tương tác với chức năng nghiệp vụ mà không cần quan tâm đến cơ chế lưu trữ dữ liệu dưới dạng tệp JSON.

## 1.3. Tầm quan trọng của LTHĐT trong Kỹ thuật phần mềm {#13-tầm-quan-trọng-của-lthđt-trong-kỹ-thuật-phần-mềm}

Trong kỹ thuật phần mềm, lập trình hướng đối tượng giữ vai trò quan trọng vì cung cấp một cách tiếp cận tự nhiên để chuyển đổi bài toán thực tế thành mô hình phần mềm. Thay vì viết chương trình theo các hàm rời rạc, lập trình hướng đối tượng cho phép nhà phát triển phân chia hệ thống thành những đơn vị có trách nhiệm rõ ràng, từ đó dễ kiểm soát độ phức tạp của phần mềm.

Một trong những lợi ích lớn nhất của lập trình hướng đối tượng là khả năng tái sử dụng mã nguồn. Khi các lớp được thiết kế hợp lý, những thành phần chung có thể được dùng lại ở nhiều vị trí khác nhau mà không cần sao chép logic. Bên cạnh đó, lập trình hướng đối tượng còn giúp hệ thống dễ bảo trì hơn vì mỗi lớp chịu trách nhiệm cho một phần chức năng tương đối độc lập; khi phát sinh lỗi hoặc cần thay đổi yêu cầu, nhóm có thể xác định đúng vị trí cần chỉnh sửa.

Ngoài ra, lập trình hướng đối tượng còn hỗ trợ khả năng mở rộng. Khi hệ thống cần bổ sung loại phương tiện mới, thay đổi chiến lược tính cước hoặc thêm các vai trò người dùng khác, cấu trúc hướng đối tượng cho phép bổ sung lớp mới mà ít ảnh hưởng đến phần mã nguồn hiện có. Đối với một dự án, việc áp dụng lập trình hướng đối tượng không chỉ giúp hoàn thành sản phẩm mà còn rèn luyện tư duy thiết kế phần mềm bền vững và có khả năng phát triển lâu dài.

# CHƯƠNG 2: PHÂN TÍCH VÀ THIẾT KẾ LỚP {#chương-2-phân-tích-và-thiết-kế-lớp}

## 2.1. Phân tích bài toán {#21-phân-tích-bài-toán}

Hệ thống được xây dựng nhằm mô phỏng quy trình đặt xe công nghệ giữa hành khách và tài xế trên một nền tảng phần mềm. Người dùng có thể đăng nhập theo vai trò phù hợp, hành khách tạo yêu cầu đặt xe, hệ thống tìm tài xế phù hợp, tài xế nhận chuyến và thực hiện chuyến đi cho đến khi hoàn tất thanh toán và đánh giá. Bài toán đòi hỏi sự phối hợp giữa nhiều đối tượng và nhiều trạng thái nghiệp vụ khác nhau.

Các tác nhân chính của hệ thống gồm:
- **Hành khách:** đăng ký, đăng nhập, tạo yêu cầu đặt xe, xem thông tin tài xế, theo dõi trạng thái chuyến đi và đánh giá sau chuyến.  
- **Tài xế:** đăng ký, đăng nhập, bật hoặc tắt trạng thái hoạt động, nhận chuyến, cập nhật tiến trình chuyến đi và theo dõi thu nhập.  
- **Quản trị viên:** quản lý dữ liệu hệ thống, cấu hình chính sách giá cước và theo dõi báo cáo tổng quan.  
- **Hệ thống:** thực hiện ghép tài xế, tính toán giá cước, lưu trữ dữ liệu và phát sinh các sự kiện cập nhật trạng thái.

Các thực thể chính trong hệ thống gồm người dùng, chuyến đi, phương tiện, tuyến đường, giá cước, đánh giá và chính sách vận hành. Đây là những đối tượng cốt lõi phản ánh cấu trúc của một nền tảng gọi xe. Trong đó, chuyến đi là thực thể trung tâm vì liên kết trực tiếp giữa hành khách, tài xế, lộ trình và trạng thái thực hiện.

Luồng xử lý của một chuyến đi diễn ra theo trình tự cơ bản sau: hành khách chọn điểm đón và điểm đến, hệ thống tính quãng đường và chi phí, sau đó tạo yêu cầu tìm tài xế. Khi một tài xế khả dụng phù hợp với loại xe được chọn, chuyến đi chuyển sang trạng thái đã ghép. Tài xế tiếp tục xác nhận đến điểm đón, bắt đầu chuyến đi, đến điểm trả và tài xế xác nhận thanh toán để hoàn tất. Cuối cùng, hành khách có thể gửi đánh giá cho tài xế.

Vòng đời chuyến đi (Lifecycle): Trạng thái chuyến đi bắt đầu từ `Pending`, chuyển sang `Searching` để tìm tài xế, sau đó là `Matched`, `Arrived`, `Started`. Sau khi hoàn thành hành trình sẽ chuyển sang `DropOff`, tiếp theo là xác nhận thanh toán để đạt trạng thái `Completed`.

**Hình 2.1.** Luồng đặt xe và hoàn thành chuyến đi (Core Flow).

**Hình 2.2.** Luồng xử lý khi không tìm thấy tài xế (Timeout) hoặc bị từ chối.

**Hình 2.3.** Luồng hủy chuyến đi từ phía Hành khách.

## 2.2. Thiết kế lớp và Sơ đồ lớp (Class diagrams) {#22-thiết-kế-lớp-và-sơ-đồ-lớp-class-diagrams}

Hệ thống được thiết kế theo kiến trúc phân tầng (Layered Architecture) kết hợp với các mẫu thiết kế hướng đối tượng để đảm bảo tính linh hoạt và dễ bảo trì. Sơ đồ lớp tổng quát dưới đây thể hiện cấu trúc các thực thể chính và mối quan hệ giữa chúng.

**Hình 2.4.** Sơ đồ lớp (Class Diagram) tổng quát của hệ thống.

Dựa trên phân tích bài toán, hệ thống được thiết kế xoay quanh một số lớp chính được tổ chức như sau:
- **Models (Entity.cs)**: `Usr` (abstract), `Drv`, `Psg`, `Adm`, `Trip`, `Veh` (abstract), `Car`, `Moto`, `Pol`, `Rev`.
- **Value Objects (ValueObject.cs)**: `Loc`, `Coord`, `Addr`, `Route`, `Fare`.
- **State Pattern (Pattern.cs)**: `IDriverState`, `ITripState` và các concrete states.
- **Factories (Pattern.cs)**: `UserFactory`, `VehicleFactory`, `DriverStateFactory`, `TripStateFactory`.
- **Services (Service.cs)**: `TripCmd`, `TripQry`, `UsrSvc`, `DrvCmd`, `DrvQry`, `WalletSvc`, `PsgSvc`, `AdmSvc`, `FareSvc`, `MapSvc`, `MatchSvc`, `RevSvc`.
- **Repositories (Repository.cs)**: `JsonRepository<T>`, `TripRepo`, `UsrRepo`, `VehRepo`, `PolRepo`, `RevRepo`.

Các mối quan hệ giữa các lớp được thể hiện rõ qua mô hình hướng đối tượng:
- **Inheritance:** Các lớp người dùng cụ thể kế thừa từ lớp người dùng trừu tượng; các loại phương tiện cụ thể kế thừa từ lớp phương tiện chung.  
- **Association:** Chuyến đi liên kết với hành khách, tài xế và phương tiện trong suốt vòng đời xử lý.  
- **Aggregation:** Một dịch vụ ứng dụng sử dụng nhiều repository hoặc nhiều đối tượng hỗ trợ để hoàn thành một ca sử dụng.  
- **Composition:** Một chuyến đi bao gồm thông tin lộ trình và giá cước như những thành phần gắn chặt với chính chuyến đi đó.  
- **Dependency:** Giao diện người dùng phụ thuộc vào các dịch vụ ứng dụng để gửi yêu cầu và nhận kết quả xử lý.

## 2.3. Cài đặt các lớp chức năng {#23-cài-đặt-các-lớp-chức-năng}

Toàn bộ mã nguồn của hệ thống được tổ chức thành các tệp, mỗi tệp đảm nhận một nhóm lớp có cùng trách nhiệm:

`Entity.cs` chứa toàn bộ các lớp thực thể cốt lõi của miền bài toán (domain entities). Quan hệ kế thừa được áp dụng triệt để: `Usr` là lớp trừu tượng chứa các thuộc tính chung; `Drv` và `Psg` kế thừa từ `Usr`. Tương tự, `Veh` là lớp trừu tượng, trong khi `Car` và `Moto` kế thừa để định nghĩa loại phương tiện cụ thể.

```csharp
public class Trip
{
    public event EventHandler<TripStatusChangedEventArgs>? StatusChanged;
    private readonly object _stateLock = new object();
    private ITripState _currentState;
    private TripStatus _status;
    // ...
    public void StartSearching() => GetCurrentStateSafe().StartSearching(this);
    public void AssignDriver(Guid driverId) => GetCurrentStateSafe().AssignDriver(this, driverId);
    internal void TransitionTo(ITripState newState)
    {
        TripStatus oldStatus;
        TripStatus currentNewStatus;
        lock (_stateLock)
        {
            oldStatus = _currentState.Status;
            _currentState = newState;
            _status = newState.Status;
            currentNewStatus = _status;
        }
        StatusChanged?.Invoke(this, new TripStatusChangedEventArgs(Id, oldStatus, currentNewStatus));
    }
}
```

`Pattern.cs` tập trung hiện thực các mẫu thiết kế (design patterns) quan trọng. `State Pattern` được dùng để quản lý vòng đời của `Trip` và trạng thái làm việc của `Driver`.

```csharp
public interface ITripState
{
    TripStatus Status { get; }
    void StartSearching(Trip trip);
    void AssignDriver(Trip trip, Guid driverId);
    void DriverArrived(Trip trip);
    // ...
}
public class TripSearchingState : AbstractTripState
{
    public override TripStatus Status => TripStatus.Searching;
    public override void AssignDriver(Trip trip, Guid driverId)
    {
        trip.SetDriverId(driverId);
        ChangeState(trip, new TripMatchedState());
    }
}
```

`Repository.cs` triển khai tầng lưu trữ dữ liệu. Lớp `JsonRepository<T>` là một generic repository cung cấp các thao tác đọc/ghi file JSON một cách an toàn theo luồng (thread-safe).

```csharp
public class JsonRepository<T> : IJsonRepository<T>, IDisposable where T : class
{
    protected readonly string _filePath;
    protected readonly SemaphoreSlim _fileLock = new(1, 1);
    protected List<T> _items = new();
    public async Task CreateAsync(T entity)
    {
        await _fileLock.WaitAsync();
        try {
            _items.Add(entity);
            await SaveInternalAsync();
        } finally { _fileLock.Release(); }
    }
}
```

# CHƯƠNG 3: XÂY DỰNG ỨNG DỤNG {#chương-3.-xây-dựng-ứng-dụng}

## 3.1. Thiết kế giao diện chương trình {#31-thiết-kế-giao-diện-chương-trình}

Ứng dụng được xây dựng trên nền tảng Windows Forms với định hướng giao diện trực quan, dễ thao tác và phù hợp cho từng vai trò người dùng. Giao diện tổng thể được tổ chức theo mô hình một cửa sổ chính kết hợp các vùng chức năng động.

**Hình 3.1.** Giao diện chính của ứng dụng dành cho Hành khách và Tài xế.

**Hình 3.2.** Luồng Quản trị viên điều chỉnh chính sách giá cước.

## 3.2. Phát triển các chức năng của ứng dụng {#32-phát-triển-các-chức-năng-của-ứng-dụng}

Hệ thống sử dụng kỹ thuật phân vùng không gian (Spatial Indexing) bằng ô lưới để tối ưu hiệu năng tìm kiếm tài xế trong bán kính gần khách hàng.

```csharp
public class InMemoryDriverGrid
{
    private const double CELL_SIZE = 0.01; // ~1km
    private readonly ConcurrentDictionary<string, HashSet<Guid>> _grid = new();

    public List<Guid> GetDriversInCellAndNeighbors(double lat, double lon, int radiusCells = 2)
    {
        var center = GetCellIndex(lat, lon);
        var resultSet = new HashSet<Guid>();
        for (int dx = -radiusCells; dx <= radiusCells; dx++) {
            for (int dy = -radiusCells; dy <= radiusCells; dy++) {
                string key = GetCellKey(center.LatIdx + dx, center.LonIdx + dy);
                if (_grid.TryGetValue(key, out var driverSet)) {
                    lock (driverSet) {
                        foreach (var id in driverSet) resultSet.Add(id);
                    }
                }
            }
        }
        return resultSet.ToList();
    }
}
```

Hệ thống sử dụng thư viện `System.Text.Json` để lưu trữ dữ liệu bền vững. Tính năng `JsonDerivedType` được áp dụng để xử lý đa hình cho lớp `Usr`.

```csharp
[JsonDerivedType(typeof(Drv), typeDiscriminator: "driver")]
[JsonDerivedType(typeof(Psg), typeDiscriminator: "passenger")]
[JsonDerivedType(typeof(Adm), typeDiscriminator: "admin")]
public abstract class Usr { ... }
```

## 3.3. Các kịch bản thực thi ứng dụng {#33-các-kịch-bản-thực-thi-ứng-dụng}

**Kịch bản 1: Hành khách đặt chuyến thành công**
1. Hành khách đăng nhập, chọn điểm đón, điểm đến và loại xe.
2. Hệ thống tính toán lộ trình và hiển thị giá cước dự kiến.
3. Yêu cầu được tạo, hệ thống tìm và ghép tài xế phù hợp.
4. Tài xế thực hiện chuyến đi, hành khách thanh toán và đánh giá.

**Kịch bản 2: Tài xế thay đổi trạng thái làm việc**
1. Tài xế đăng nhập và bật trạng thái sẵn sàng.
2. Khi nhận chuyến, trạng thái chuyển sang đang phục vụ.
3. Hoàn thành chuyến, tài xế trở lại trạng thái sẵn sàng.

**Kịch bản 3: Hành khách hủy chuyến đi**
1. Hành khách hủy yêu cầu khi đang tìm tài xế hoặc khi tài xế chưa đến điểm đón.
2. Hệ thống xác nhận hủy, giải phóng tài xế về trạng thái sẵn sàng.

# CHƯƠNG 4: THẢO LUẬN VÀ ĐÁNH GIÁ {#chương-4.-thảo-luận-&-đánh-giá}

## 4.1. Các kết quả nhận được {#41-các-kết-quả-nhận-được}

Sau quá trình nghiên cứu và triển khai, đề tài đã đạt được các kết quả quan trọng sau:
- **Mô hình hóa hệ thống**: Xây dựng thành công mô hình kết nối tài xế và hành khách với đầy đủ các tác nhân và quy trình nghiệp vụ thực tế.
- **Kiến trúc phần mềm**: Tổ chức mã nguồn theo tư duy hướng đối tượng và kiến trúc phân tầng, đảm bảo sự tách biệt rõ ràng giữa các tầng.
- **Quản lý trạng thái**: Hiện thực hóa thành công vòng đời chuyến đi và trạng thái tài xế thông qua `State Pattern`.
- **Vận dụng mẫu thiết kế**: Áp dụng hiệu quả các Design Patterns như `State`, `Factory`, `Repository` và `Observer`.
- **Lưu trữ bền vững**: Triển khai cơ chế Serialization/Deserialization với JSON.

## 4.2. Một số tồn tại {#42-một-số-tồn-tại}

- **Phạm vi vận hành**: Đồ án mới dừng ở mức mô phỏng học thuật, chưa triển khai trong môi trường thực tế với lượng người dùng lớn.
- **Toàn vẹn dữ liệu**: Chưa có cơ chế giao dịch (transaction) tổng quát khi cập nhật đồng thời nhiều tệp JSON.
- **Bảo mật**: Cơ chế xác thực hiện tại xử lý mật khẩu dưới dạng văn bản thuần, cần bổ sung băm mật khẩu (hashing).

## 4.3. Hướng phát triển {#43-hướng-phát-triển}

- **Tăng cường toàn vẹn dữ liệu**: Triển khai cơ chế Unit of Work hoặc giao dịch.
- **Mở rộng nền tảng**: Phát triển Web API và ứng dụng di động để phục vụ đa nền tảng.
- **Hoàn thiện bảo mật**: Áp dụng cơ chế băm mật khẩu an toàn và phân quyền chi tiết.

# **PHỤ LỤC** {#phụ-lục}

**Phụ lục 1: Liên kết Github**  
Link: [https://github.com/phuongtran31241022663/OOP2026](https://github.com/phuongtran31241022663/OOP2026)

**Phụ lục 2: Hướng dẫn cài đặt**  
1. Tải mã nguồn từ Github.
2. Mở file solution `OOP2026.slnx` bằng Visual Studio 2022.
3. Thực hiện `Restore NuGet Packages`.
4. Biên dịch và chạy ứng dụng (F5).

**Phụ lục 3: Phân công công việc**  
- **Trần Yến Phượng**: Chịu trách nhiệm toàn bộ về **Tầng Miền nghiệp vụ (Domain Layer)** và **Tầng Ứng dụng (Application Layer)**. Đây là hai tầng cốt lõi chứa đựng toàn bộ "não bộ" và quy tắc vận hành của hệ thống.
  * **Tầng Miền nghiệp vụ**: Thiết kế và hiện thực toàn bộ cấu trúc thực thể (`Entity.cs`), các đối tượng giá trị (`ValueObject.cs`), hệ thống Interface (`Interface.cs`) và các mẫu thiết kế nền tảng (`Pattern.cs`) như State Pattern cho vòng đời chuyến đi và Factory Pattern.
  * **Tầng Ứng dụng**: Xây dựng toàn bộ các dịch vụ xử lý nghiệp vụ (`Service.cs`) bao gồm: Thuật toán ghép tài xế tối ưu, logic tính toán giá cước, điều phối luồng thanh toán ví điện tử và hệ thống thông báo trạng thái.
  * **Nhiệm vụ trọng tâm**: Đảm bảo tính đúng đắn của logic nghiệp vụ, thiết kế kiến trúc hệ thống và quản lý các tệp mã nguồn cốt lõi trên Git.

- **Lê Tô Nguyệt Minh**: Chịu trách nhiệm toàn bộ về **Tầng Giao diện (Presentation Layer)** và **Tầng Hạ tầng (Infrastructure Layer)**. Đây là hai tầng đảm nhiệm việc tương tác với người dùng và lưu trữ dữ liệu bền vững.
  * **Tầng Giao diện**: Thiết kế và lập trình toàn bộ hệ thống Form (`Form/`), các thành phần điều khiển người dùng (`UserControl/`), xử lý tương tác bản đồ thời gian thực và các tiện ích hỗ trợ giao diện (`UIHelper.cs`).
  * **Tầng Hạ tầng**: Xây dựng cơ chế lưu trữ dữ liệu JSON (`Repository.cs`), xử lý Serialization đa hình, khởi tạo dữ liệu mẫu (`DataSeeder.cs`) và kịch bản mô phỏng vận hành (`Simulation.cs`).
  * **Nhiệm vụ trọng tâm**: Tối ưu hóa trải nghiệm người dùng (UX/UI), quản lý cấu hình dự án (`Program.cs`, `.csproj`), biên tập báo cáo đồ án và vẽ các sơ đồ kỹ thuật.

# **TÀI LIỆU THAM KHẢO** {#tài-liệu-tham-khảo}

1. Đặng Ngọc Hoàng Thành. (2026). *Bài giảng Lập trình hướng đối tượng*. TP. Hồ Chí Minh: Đại học Kinh tế TP. Hồ Chí Minh (UEH).
2. Erich Gamma, Richard Helm, Ralph Johnson, & John Vlissides. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley.
3. Microsoft. (n.d.). *Serialization in .NET*. Retrieved from https://learn.microsoft.com/en-us/dotnet/standard/serialization/