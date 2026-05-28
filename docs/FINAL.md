# Tài liệu chung nhất

> **Phạm vi:** WinForms .NET Framework 4.8 · GMap.NET · Dữ liệu JSON
> **Single source of truth:** File này là tài liệu chính. `docs/FINAL.md` chỉ là bản dẫn hướng rút gọn và phải trỏ về file này khi có khác biệt.

## Mục lục

- [1. Nguyên lý thiết kế giao diện](#1-nguyên-lý-thiết-kế-giao-diện)
- [2. Tổng quan kiến trúc hệ thống](#2-tổng-quan-kiến-trúc-hệ-thống)
- [3. Kiến Trúc Domain (Behavioral & Structural)](#3-kiến-trúc-domain-behavioral--structural)
- [4. Kiến Trúc Application (Orchestration)](#4-kiến-trúc-application-orchestration)
- [5. Kiến Trúc Infrastructure (Data & External)](#5-kiến-trúc-infrastructure-data--external)
- [6. Kiến Trúc Presentation Layer](#6-kiến-trúc-presentation-layer)
- [7. Quy Tắc Modal vs. Inline](#7-quy-tắc-modal-vs-inline)
- [8. Cơ Chế Real-time & Sự Kiện](#8-cơ-chế-real-time--sự-kiện)
- [9. Quy Tắc Layout & Responsive](#9-quy-tắc-layout--responsive)
- [10. Luồng Nghiệp Vụ Chi Tiết](#10-luồng-nghiệp-vụ-chi-tiết)
- [11. Bài Học Từ DEVLOG](#11-bài-học-từ-devlog)
- [12. Use Cases](#12-use-cases)
- [13. Ràng Buộc Kỹ Thuật](#13-ràng-buộc-kỹ-thuật)
- [14. Tổng Kết](#14-tổng-kết)
- [15. Tham Khảo](#15-tham-khảo)

---

## 1. Nguyên lý thiết kế giao diện


Bốn nguyên tắc dưới đây chi phối **mọi** quyết định giao diện. Bất kỳ thành phần nào vi phạm một trong bốn nguyên tắc này đều cần được xem xét lại.

| #   | Nguyên tắc                   | Hệ quả thực tế                                                                                                                                                                     |
| --- | ---------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1   | **Tối thiểu Form**           | Chỉ tạo `Form` mới khi cần ngữ cảnh làm việc hoàn toàn biệt lập. Mọi điều hướng còn lại dùng `UserControl`.                                                                        |
| 2   | **Single-Form Shell**        | Một `FrmMultiRole` duy nhất làm khung nền. Chuyển cảnh = hoán đổi `UserControl` bên trong Shell, không đóng/mở cửa sổ.                                                                  |
| 3   | **Container theo nghiệp vụ** | Chọn loại container xuất phát từ câu hỏi: _"Người dùng tương tác với màn hình này như thế nào?"_, không phải từ sở thích cá nhân.                                                  |
| 4   | **Tái sử dụng tối đa**       | Các component dùng chung (`ucMap`, `ucLocationPicker`, `ucTripStatus`, `ucDriverCard`, `ucFareSelector`…) phải được tái sử dụng, không được viết lại với mục đích tương tự. |

---

## 2. Tổng quan kiến trúc hệ thống


### 2.1 Sơ đồ layer


Hệ thống OOP sử dụng **Single Project Architecture** với code được tổ chức theo nguyên tắc phân tầng logic bên trong một project duy nhất.

```
Entity.cs + ValueObject.cs + Pattern.cs → Interface.cs → Service.cs → Repository.cs → Form/*.cs + UserControl/*.cs
```

| Layer          | File chính                                      | Vai trò                                  |
| -------------- | ----------------------------------------------- | ---------------------------------------- |
| Domain         | `Entity.cs`, `ValueObject.cs`, `Pattern.cs`     | Entities, Value Objects, State Pattern  |
| Application    | `Interface.cs`, `Service.cs`                    | Service Interfaces và Implementations   |
| Infrastructure | `Repository.cs`, `DataSeeder.cs`, `Service.cs`  | JSON persistence, seed data, Map API    |
| Presentation   | `Form/*.cs`, `UserControl/*.cs`, `UIHelper.cs`  | WinForms UI, Forms, UserControls        |

> **Ghi chú:** Đây là cấu trúc đơn giản hóa phù hợp với đồ án học phần OOP. Các layer được phân tách theo file chứ không theo project.

### 2.2 Sơ đồ phân cấp UI


```
Program.Main
└── FrmMultiRole  (shell thực tế cho passenger/driver/admin trong phiên demo)
    ├── ucPassengerHome  ← khu vực passenger
    │   ├── ucBooking
    │   ├── ucMap
    │   ├── ucHistory / ucHistoryCard
    │   ├── ucReview
    │   └── ucProfile
    ├── ucDriverHome     ← khu vực driver
    │   ├── ucRequest
    │   ├── ucTripStatus
    │   ├── ucDriverStatus
    │   ├── ucWallet
    │   └── ucTripCard
    └── FrmAdmin         ← form quản trị mở từ shell

[Auth/Form phụ]
├── FrmAuth / FrmPassengerAuth / FrmDriverAuth
├── FrmPassenger / FrmDriver
└── Form1 là form mặc định còn lại, không phải shell chính
```


| Thành phần thực tế      | Loại        | Actor                    | Mô tả ngắn                                                                      |
| ----------------------- | ----------- | ------------------------ | ------------------------------------------------------------------------------- |
| `FrmMultiRole`          | Form        | Driver, Passenger, Admin | Shell demo chính; chứa passenger/driver panels và mở `FrmAdmin`                 |
| `FrmAuth`               | Form        | Tất cả                   | Form đăng nhập/đăng ký; tách thêm `FrmPassengerAuth` và `FrmDriverAuth`         |
| `FrmAdmin`              | Form        | Admin                    | Quản lý Users/Trips/Policy + Thống kê                                           |
| `ucPassengerHome`       | UserControl | Passenger                | Khu vực đặt chuyến, bản đồ, lịch sử, hồ sơ                                      |
| `ucDriverHome`          | UserControl | Driver                   | Bật/tắt trạng thái, nhận/từ chối chuyến, cập nhật tiến trình                    |
| `ucBooking`             | UserControl | Passenger                | Nhập pickup/dropoff, chọn xe, gọi map/fare/request trip                         |
| `ucReview`              | UserControl | Passenger                | Form đánh giá sao + comment                                                     |
| `ucProfile`             | UserControl | Passenger, Driver        | Xem/sửa hồ sơ                                                                   |
| `ucTrip`                | UserControl | Admin, Passenger, Driver | Chi tiết một chuyến đi                                                          |
| `ucMap`                 | UserControl | Passenger, Driver        | Hiển thị bản đồ GMap.NET với markers và routes                                  |
| `ucLocationPicker`      | UserControl | Passenger                | Cho phép chọn điểm đón/đến trên bản đồ                                          |
| `ucTripStatus`          | UserControl | Passenger, Driver        | Hiển thị trạng thái hiện tại của chuyến                                         |
| `ucTripCard`            | UserControl | Passenger, Driver        | Card hiển thị thông tin chuyến trong danh sách                                  |
| `ucDriverCard`          | UserControl | Passenger                | Card hiển thị thông tin tài xế                                                  |
| `ucFareSelector`        | UserControl | Passenger                | Chọn loại dịch vụ (Car/Motorbike) và xem giá dự kiến                            |

---

## 3. Kiến Trúc Domain (Behavioral & Structural)

### 3.1 Vòng Đời Chuyến Đi (Trip Lifecycle & State Machine)

Chuyến đi (`Trip`) là trung tâm của hệ thống, được quản lý chặt chẽ bởi **State Pattern**. Mỗi trạng thái là một lớp kế thừa `ITripState`, đảm bảo chỉ các chuyển đổi hợp lệ mới được thực thi.

#### 3.1.1 Các Trạng Thái (State Machine)
- **TripPendingState**: Hành khách vừa tạo yêu cầu. Cho phép: `StartSearching`, `Cancel`.
- **TripSearchingState**: Hệ thống đang tìm tài xế. Cho phép: `AssignDriver`, `Cancel`, `Timeout`.
- **TripMatchedState**: Đã tìm được tài xế. Tài xế đang di chuyển đến điểm đón. Cho phép: `DriverArrived`, `Cancel`.
- **TripArrivedState**: Tài xế đã đến điểm đón. Cho phép: `StartTrip`, `Cancel`.
- **TripStartedState**: Chuyến đi đang diễn ra. Cho phép: `CompleteTrip`, `Cancel`.
- **TripDropOffState**: Đã đến điểm trả khách. Cho phép: `ConfirmPayment`.
- **TripCompletedState** (Terminal): Chuyến đi kết thúc thành công.
- **TripCancelledState** (Terminal): Chuyến đi bị hủy.
- **TripTimeoutState** (Terminal): Không tìm thấy tài xế sau một khoảng thời gian.

#### 3.1.2 Quy tắc Chuyển Trạng Thái (Guardrails)
- Không thể `StartTrip` nếu chưa `Arrived`.
- Không thể `CompleteTrip` nếu chưa `Started`.
- Thanh toán (`ConfirmPayment`) chỉ được thực hiện ở trạng thái `DropOff`.
- Mọi chuyển đổi trạng thái đều phát ra **Domain Event** tương ứng (ví dụ: `TripStartedEvent`).

### 3.2 Cơ Chế Ghép Tài Xế (Matching Logic)

Nằm trong `MatchingService`, luồng ghép tài xế hoạt động như sau:

1. **Spatial Indexing**: Sử dụng `InMemoryDriverGrid` để phân vùng bản đồ thành các ô lưới (~1km).
2. **Lọc Tài Xế**:
   - Vị trí: Chỉ lấy tài xế trong ô lưới hiện tại và 8 ô xung quanh của điểm đón.
   - Trạng thái: Phải là `Online`.
   - Phương tiện: Loại xe của tài xế phải khớp với yêu cầu (`VehicleType`).
   - Tài chính: `Driver.PayCommission(Fare)` có guard `Wallet >= Commission`, nhưng `MatchingService` hiện **chưa** lọc điều kiện này trước khi propose/assign.
3. **Đề xuất (Propose)**: Gửi thông báo đến tối đa 3 tài xế gần nhất.
4. **Chấp nhận (Accept)**: Tài xế đầu tiên chấp nhận sẽ được gán vào chuyến đi. Trạng thái Trip chuyển sang `Matched`, Driver chuyển sang `OnTrip`.

### 3.3 Tính Giá Cước (Fare Calculation)

Nằm trong `FareService` và `Policy`, giá cước được tính khi Hành khách tạo yêu cầu:

1. **Tra cứu Policy**: Lấy quy tắc giá mới nhất cho loại xe (`Car`/`Motorbike`).
2. **Công thức**:
   - `TotalAmount = BaseFare + (PricePerKm * DistanceKm)`
   - `Commission = TotalAmount * CommissionRate`
   - `DriverIncome = TotalAmount - Commission`
3. **Lưu trữ**: Giá cước được đóng gói vào đối tượng `Fare` (Value Object) và gắn vào `Trip`.

### 3.4 Thực Thể (Entities) & Đối Tượng Giá Trị (Value Objects)

#### 3.4.1 Driver (Tài xế)
- Quản lý trạng thái: `Offline`, `Online`, `OnTrip` (State Pattern).
- Tài chính: `Wallet` (tiền nạp), `Income` (tổng thu nhập từ chuyến đi).
- Đánh giá: Tự động cập nhật `AverageRating` mỗi khi có `Review` mới.

#### 3.4.2 User Hierarchy
- `Usr` (Abstract): Chứa thông tin cơ bản (Name, Phone, Password hash).
- `Psg`, `Drv`, `Adm`: Các vai trò cụ thể với hành vi riêng.

#### 3.4.3 Value Objects (Bất biến)
- `Loc`: Gồm tọa độ (`Coord`) và địa chỉ (`Addr`).
- `Route`: Gồm điểm đón, điểm trả, quãng đường, thời gian dự kiến và chuỗi Polyline để vẽ bản đồ.

---

## 4. Kiến Trúc Application (Orchestration)

### 4.1 Dependency Injection & Khởi Tạo
Hệ thống sử dụng **Manual Dependency Injection** tập trung tại `AppServiceBundle`. Khi ứng dụng khởi động:
1. Khởi tạo các Repositories (đọc dữ liệu từ JSON).
2. Khởi tạo các Services (truyền Repository vào constructor).
3. Đăng ký các sự kiện chéo giữa các Service (ví dụ: `TripService` lắng nghe `MapService`).

### 4.2 Xử lý Real-time (Event-driven UI)
- UI không chủ động hỏi trạng thái (polling).
- Khi một Service thay đổi trạng thái Entity (ví dụ: `Trip.Status`), nó phát ra một sự kiện C# (`EventHandler`).
- Các `UserControl` đăng ký sự kiện này và tự cập nhật giao diện (ví dụ: `UcTripStatus` tự đổi text khi trạng thái Trip thay đổi).

### 4.3 Thread Safety (An toàn đa luồng)
Vì ứng dụng chạy trên WinForms (Single Threaded UI) nhưng các tác vụ I/O và Simulation chạy trên luồng phụ:
- `SemaphoreSlim`: Dùng trong `TripService`, `MatchingService` để tránh race condition khi 2 tài xế cùng nhận 1 chuyến.
- `Control.Invoke`: Dùng trong Presentation layer để đảm bảo cập nhật UI luôn diễn ra trên UI thread.
- `JsonRepository`: Sử dụng khóa tệp (file lock) để đảm bảo không có 2 luồng cùng ghi vào 1 file JSON cùng lúc.

---

## 5. Kiến Trúc Infrastructure (Data & External)


### 5.1 Repositories

#### 5.1.1 JsonRepository<T>

**File:** `OOP2026/Repository.cs`

**Mục đích:** Generic JSON file-based repository với thread-safe file mutex (`SemaphoreSlim`)

**File:** `Data/{filename}.json`

**Phương thức:** ReadAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync

**Phụ thuộc:** System.Text.Json

#### 5.1.2 Concrete Repositories (Tất cả định nghĩa trong `OOP2026/Repository.cs`)

| Class               | Base Class                | Extra                                     |
| ------------------- | ------------------------- | ----------------------------------------- |
| TripRepo            | JsonRepository<Trip>      | GetByDriverIdAsync, GetByPassengerIdAsync |
| UsrRepo             | JsonRepository<Usr>       | GetByPhoneAsync, GetPassengerByIdAsync, GetDriverByIdAsync |
| PolRepo             | JsonRepository<Pol>       | GetLatestByVehicleTypeAsync               |
| RevRepo             | JsonRepository<Rev>       | GetByDriverIdAsync, GetByTripIdAsync      |
| VehRepo             | JsonRepository<Veh>       | GetByTypeAsync                            |

#### 5.1.4 FileStorage (static)

**File:** `OOP2026/Repository.cs`

**Phương thức:** LoadAsync<T>, SaveAsync<T>

### 5.2 External Services

#### 5.2.1 GMapService

**File:** `OOP2026/Service.cs`

**Mục đích:** Map rendering (GMap.NET)

**Phương thức:** SearchLocationAsync (mock empty), ReverseGeocodeAsync, GetRouteAsync

**Phụ thuộc:** GMap.NET, IGMapService

#### 5.2.2 MapService

**File:** `OOP2026/Service.cs`

**Mục đích:** HTTP-based routing/geocoding (Photon + OSRM)

**Phương thức:** SearchAsync, GetAddressAsync, GetDirectionsAsync, EnsureLocationAsync

**Phụ thuộc:** HttpClient, System.Text.Json

---

## 6. Kiến Trúc Presentation Layer

### 6.1 Forms Chính

#### 6.1.1 FrmMultiRole

**File:** `OOP2026/Form/FrmMultiRole.cs`

**Mục đích:** Shell chính cho hành khách/tài xế/admin trong phiên demo.

#### 6.1.2 FrmAuth

**File:** `OOP2026/Form/FrmAuth.cs`

**Mục đích:** Shell cho hành khách/tài xế/admin

**Phụ thuộc:** IUserService, ITripService, ISimulationService

**Screens:** BookTripForm, TripTrackingForm, TripHistoryForm

**Navigation:** RegisterScreens → NavigateTo(key)

### 6.2 UserControls Chính

#### 6.2.1 ucPassengerHome – Vòng Đời Đặt Xe

**Container chính:** `SplitContainer` (Vertical – bản đồ trái, bảng điều khiển phải)

**Lý do:** Bản đồ cần chiếm phần lớn không gian và cho phép người dùng kéo thanh chia; bảng bên phải thay đổi nội dung theo trạng thái chuyến mà không ảnh hưởng bản đồ.

```
UcPassenger (Dock Fill)
└── SplitContainer (Orientation = Vertical, Panel1 ~70%, Panel2 ~30%)
    │
    ├── Panel1 – Bản đồ
    │   └── UcMap (Dock Fill)
    │       [Hiển thị marker Pickup / Destination, mô phỏng vị trí tài xế]
    │
    └── Panel2 – Bảng điều khiển động
        └── TableLayoutPanel (1 cột × 3 hàng)
            ├── Row 1 – Header (cố định, hiển thị tên + Button "Lịch sử" + Button "Hồ sơ")
            ├── Row 2 – pnlActionStage (Fill) ← "sân khấu" thay nội dung theo trạng thái Trip
            │   ├── [Idle / Timeout]     → BookingPanel
            │   │   · UcLocationPicker (Pickup + Destination)
            │   │   · UcVehicleFareSelector (VehicleType: Car / Motorbike)
            │   │   · FarePanel (Base fare, /km, Tổng dự kiến)
            │   │   · Button "Đặt xe" [lớn, full-width]
            │   │
            │   ├── [Searching]          → SearchingPanel
            │   │   · ProgressBar (marquee) + Label "Đang tìm tài xế…"
            │   │   · Button "Hủy yêu cầu"
            │   │
            │   ├── [Matched / Arrived / Started] → TrackingPanel
            │   │   · UcTripStatus (hiển thị trạng thái hiện tại)
            │   │   · UcDriverCard (tên, sao, loại xe, biển số)
            │   │   · Button "Hủy chuyến" [chỉ Enable khi Status < Started]
            │   │
            │   └── [Completed]          → PaymentPanel
            │       · Label Tổng tiền (lớn, nổi bật)
            │       · Button "Xác nhận thanh toán"
            │       · Button "Đánh giá tài xế" → mở UcReview trong FrmMultiRole
            │
            └── Row 3 – StatusBar nhỏ (cố định, hiển thị trạng thái kết nối / thông báo)
```

> **Cơ chế chuyển Panel:** `TripStatusChanged` event → Shell hoặc `UcPassenger` gọi helper `ShowStage(TripStatus status)` → ẩn tất cả panel con, hiện đúng panel tương ứng. Không dùng `TabControl` vì người dùng không được phép tự chuyển tab.

#### 6.2.2 ucDriverHome – Trạm Chỉ Huy Tài Xế

**Container chính:** `TableLayoutPanel` (ngoài) + `SplitContainer` (trong)

**Lý do:** Tài xế cần đồng thời thấy danh sách request mới (trái) **và** nút hành động chuyến hiện tại (phải). `SplitContainer` phản ánh đúng đặc thù làm việc song song này.

```
UcDriver (Dock Fill)
└── TableLayoutPanel (1 cột × 2 hàng)
    │
    ├── Row 1 – TopBar (cố định Height = 64px)
    │   └── FlowLayoutPanel (ngang)
    │       · ToggleButton "Online / Offline" [lớn, đổi màu theo trạng thái]
    │       · Label "Ví: {amount} VNĐ"
    │       · Label "Sao: {rating} ★"
    │       · Button "Hồ sơ / Nạp ví" → mở UcProfile trong FrmMultiRole
    │
    └── Row 2 – Nội dung chính (Fill)
        └── SplitContainer (Orientation = Vertical, Panel1 ~35%, Panel2 ~65%)
            │
            ├── Panel1 – Danh sách yêu cầu chờ
            │   └── TableLayoutPanel (1 cột × 2 hàng)
            │       ├── Label "Yêu cầu mới" (cố định)
            │       └── DataGridView "dgvRequests" (Dock Fill, ReadOnly = true)
            │           Columns: Pickup · Destination · VehicleType · Fare
            │           [Double-click hoặc nút bên dưới để Chấp nhận / Từ chối]
            │           · Button "Chấp nhận" · Button "Từ chối"
            │
            └── Panel2 – Xử lý chuyến hiện tại
                └── pnlCurrentTrip (Dock Fill)
                    ├── [Không có chuyến] → Label căn giữa "Đang rảnh, chờ yêu cầu…"
                    └── [Có chuyến] → TableLayoutPanel dọc
                        · UcTripStatus (thông tin Passenger + điểm đón/đến)
                        · Button "Đã đến điểm đón"  [Height = 80px]
                        · Button "Bắt đầu chuyến"   [Height = 80px]
                        · Button "Hoàn thành"        [Height = 80px]
                        · Button "Hủy chuyến"        [Height = 48px, màu cảnh báo]
                        [Chỉ button phù hợp với trạng thái hiện tại mới Enable = true]
```

> **Quyết định thiết kế:** Các nút hành động cao 80px là bắt buộc – tài xế thao tác trong khi lái xe (hoặc vừa dừng xe), diện tích bấm nhỏ là lỗi nghiêm trọng về UX.

#### 6.2.3 FrmAdmin – Trung Tâm Quản Trị

**Container chính:** `TabControl` (Dock Fill)

**Lý do:** Admin làm việc theo module chức năng độc lập (Users, Trips, Fares, Stats). `TabControl` là chuẩn mực back-office: mỗi tab = một không gian làm việc không ảnh hưởng lẫn nhau.

```
UcAdmin (Dock Fill)
└── TabControl (Dock Fill)
    │
    ├── TabPage "Người dùng"
    │   └── TableLayoutPanel (1 cột × 2 hàng)
    │       ├── Toolbar (cố định Height = 48px)
    │       │   · TextBox tìm kiếm · ComboBox lọc Role
    │       │   · Button "Khóa tài khoản" · Button "Mở khóa"
    │       └── DataGridView "dgvUsers" (Dock Fill)
    │           Columns: ID · Tên · Phone · Role · Trạng thái · Ngày tạo
    │
    ├── TabPage "Chuyến đi"
    │   └── Cấu trúc tương tự Tab "Người dùng"
    │       · Thêm Button "Xem chi tiết" → mở UcTripDetail trong FrmMultiRole
    │       · Thêm Button "Hủy chuyến" (chỉ Enable khi Status chưa terminal)
    │
    ├── TabPage "Giá cước"
    │   └── TableLayoutPanel (1 cột × 2 hàng)
    │       ├── Toolbar: Button "Thêm mới" · Button "Sửa" · Button "Xóa"
    │       └── DataGridView "dgvFareRules" (Dock Fill)
    │           Columns: VehicleType · BaseFare · PricePerKm · CommissionRate
    │           [Thêm/Sửa → mở form nhập liệu trong FrmMultiRole]
    │
    └── TabPage "Thống kê"
        └── TableLayoutPanel (2 cột × 2 hàng, padding 24px)
            · Panel "GMV"              – Label số lớn + Label đơn vị
            · Panel "Tỷ lệ hoàn thành" – Label % + ProgressBar minh họa
            · Panel "Điểm hài lòng"    – Label trung bình sao
            · Panel "Tổng chuyến"      – Label số chuyến theo trạng thái
            [Button "Làm mới dữ liệu" ở góc trên phải]
```

### 6.3 UserControls Phụ

#### 6.4.1 UcMap – Bản Đồ

**File:** `OOP2026/UserControl/ucMap.cs`

**Mục đích:** Hiển thị bản đồ GMap.NET với markers và routes

**Features:**

- Hiển thị marker Pickup/Destination
- Vẽ route từ OSRM response (polyline decoding)
- Cập nhật vị trí tài xế mô phỏng

**Phụ thuộc:** GMap.NET, IGMapService

#### 6.4.2 UcLocationPicker – Chọn Địa Điểm

**File:** `OOP2026/UserControl/ucLocationPicker.cs`

**Mục đích:** Cho phép người dùng chọn điểm đón/đến trên bản đồ hoặc nhập địa chỉ

**Features:**

- TextBox nhập địa chỉ
- Nút chọn trên bản đồ
- Autocomplete từ Photon API

#### 6.4.3 UcTripStatus – Trạng Thái Chuyến

**File:** `OOP2026/UserControl/ucTripStatus.cs`

**Mục đích:** Hiển thị trạng thái hiện tại của chuyến đi

**States hiển thị:**

- Pending: "Đang chờ xử lý..."
- Searching: "Đang tìm tài xế..."
- Matched: "Đã tìm được tài xế"
- Arrived: "Tài xế đã đến điểm đón"
- Started: "Đang di chuyển..."
- DropOff: "Đã đến điểm trả khách"
- Completed: "Chuyến đi hoàn thành"
- Cancelled: "Chuyến đi đã hủy"
- Timeout: "Hết thời gian chờ"

#### 6.4.4 UcDriverCard – Thông Tin Tài Xế

**File:** `OOP2026/UserControl/ucDriverCard.cs`

**Mục đích:** Hiển thị thông tin tài xế (tên, sao, loại xe, biển số)

**Layout:**

- Avatar/Icon
- Tên tài xế
- Rating (sao)
- Loại xe + biển số
- Nút gọi (nếu cần)

#### 6.4.5 UcTripCard – Card Chuyến Đi

**File:** `OOP2026/UserControl/ucTripCard.cs`

**Mục đích:** Hiển thị thông tin chuyến trong danh sách

**Layout:**

- Thời gian yêu cầu
- Điểm đón → Điểm đến
- Loại xe
- Trạng thái (màu sắc theo trạng thái)
- Giá tiền

#### 6.4.6 UcFareSelector – Chọn Dịch Vụ Và Giá

**File:** `OOP2026/UserControl/ucFareSelector.cs`

**Mục đích:** Chọn loại dịch vụ (Car/Motorbike) và xem giá dự kiến

**Features:**

- RadioButton/ComboBox chọn Car/Motorbike
- Hiển thị giá cơ bản (BaseFare)
- Hiển thị giá theo km (PricePerKm)
- Tính và hiển thị tổng giá dự kiến

#### 6.4.7 UcReview – Đánh Giá

**File:** `OOP2026/UserControl/ucReview.cs`

**Mục đích:** Form đánh giá 5 sao + comment sau chuyến

**Layout:**

- 5 RadioButton/Star cho rating (1-5 sao)
- TextBox cho comment
- Button "Gửi đánh giá"
- Button "Bỏ qua"

#### 6.4.8 UcProfile – Hồ Sơ Cá Nhân

**File:** `OOP2026/UserControl/ucProfile.cs`

**Mục đích:** Xem/sửa hồ sơ, nạp ví

**Layout:**

- Thông tin cá nhân (tên, số điện thoại)
- Button sửa thông tin
- Số dư ví
- Button nạp tiền
- Lịch sử giao dịch (nếu cần)

#### 6.4.9 UcTrip – Chi Tiết Chuyến

**File:** `OOP2026/UserControl/ucTrip.cs`

**Mục đích:** Xem chi tiết một chuyến đi

**Layout:**

- Thông tin chuyến (ID, thời gian, trạng thái)
- Thông tin hành khách
- Thông tin tài xế (nếu đã matched)
- Điểm đón → Điểm đến
- Quãng đường + Thời gian ước tính
- Giá tiền + Trạng thái thanh toán

---

## 7. Quy Tắc Modal vs. Inline

| Hành động người dùng               | Phương thức                                                       | Lý do                                                                           |
| ---------------------------------- | ----------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| Đăng nhập / Đăng ký                | **Inline** (UcAuth)                                               | Là điểm khởi đầu duy nhất; không cần cô lập                                     |
| Đặt xe, Theo dõi, Thanh toán       | **Inline** (UcPassenger – Panel động)                             | Cập nhật real-time liên tục; gián đoạn bởi Form mới = mất trạng thái            |
| Tìm kiếm tài xế (Searching)        | **Inline**                                                        | Trạng thái trung gian ngắn; không cần cửa sổ riêng                              |
| Chấp nhận / Từ chối chuyến         | **Inline** (UcDriver)                                             | Phản xạ tức thì, là core workflow của tài xế                                    |
| Cập nhật tiến trình chuyến         | **Inline** (UcDriver)                                             | Thao tác liên tiếp; cần giao diện luôn hiển thị                                 |
| Xem lịch sử chuyến đi              | **Inline** (Panel ẩn/hiện trong UcPassenger / Tab trong UcDriver) | Không cần cô lập; người dùng có thể cần xem lại khi đặt chuyến mới              |
| Đánh giá tài xế                    | **Modal** (FrmMultiRole + UcReview)                               | Tác vụ một lần sau chuyến; cần sự tập trung; không liên quan đến màn hình chính |
| Xem/Sửa hồ sơ, Nạp ví              | **Modal** (FrmMultiRole + UcProfile)                              | Tác vụ ít dùng, cô lập; không nên chiếm không gian màn hình làm việc            |
| Chi tiết chuyến (Passenger/Driver) | **Modal** (FrmMultiRole + UcTripDetail)                           | Xem thêm thông tin, không thay thế màn hình chính                               |
| Chi tiết chuyến (Admin)            | **Inline** (trong Tab "Chuyến đi")                                | Admin xem nhiều chuyến liên tiếp; Modal gây gián đoạn không cần thiết           |
| CRUD FareRule (Admin)              | **Modal** (FrmMultiRole)                                          | Nhập liệu cô lập; tránh làm nhiễu DataGrid chính                                |
| Xác nhận hủy chuyến                | **Modal** (FrmMultiRole – dialog Yes/No)                          | Hành động không thể hoàn tác; cần xác nhận rõ ràng                              |

---

## 8. Cơ Chế Real-time & Sự Kiện

Ứng dụng không dùng cơ sở dữ liệu quan hệ; cập nhật trạng thái dựa hoàn toàn vào **event-driven model**.

```
[Luồng sự kiện chính]

TripService.RaiseTripStatusChanged(trip)
    │
    ├──→ UcPassenger.OnTripStatusChanged(status)
    │       └── ShowStage(status) – chuyển panel phù hợp
    │
    ├──→ UcDriver.OnTripStatusChanged(status)
    │       └── Cập nhật nút hành động Enable/Disable
    │
    └──→ FrmMain.OnTripStatusChanged(status)
            └── FrmToast.Show(message) – thông báo nổi

[Timer polling – dùng System.Windows.Forms.Timer]
· Interval: 1000ms (1 giây) cho Passenger tracking
· Interval: 2000ms (2 giây) cho Driver request check
· Tất cả cập nhật UI phải trên UI thread (Control.Invoke nếu cần)
```

---

## 9. Quy Tắc Layout & Responsive

Mọi control phải tuân theo các quy tắc sau để tránh tọa độ cứng:

| Tình huống                        | Quy tắc bắt buộc                                                                                   |
| --------------------------------- | -------------------------------------------------------------------------------------------------- |
| Control chiếm toàn bộ container   | `Dock = Fill`                                                                                      |
| Control bám theo cạnh container   | `Anchor = Top, Left` (mặc định) hoặc cụ thể theo vị trí                                            |
| Control căn giữa động             | Đặt trong `TableLayoutPanel` với ô có `AutoSize`; đặt `Anchor = None` trên control                 |
| Khu vực cố định + khu vực co giãn | `TableLayoutPanel` với `RowStyle`: hàng cố định dùng `Absolute`, hàng co giãn dùng `Percent = 100` |
| Hai khu vực kéo được              | `SplitContainer` với `IsSplitterFixed = false`                                                     |
| **Cấm tuyệt đối**                 | Đặt `Location.X`, `Location.Y` trực tiếp trên control trong Designer                               |

---

## 10. Luồng Nghiệp Vụ Chi Tiết

### 10.1 Luồng Đặt Chuyến

```
[Passenger] Nhập pickup, dest, vehicleType
    → TripService.RequestTrip(passengerId, pickup, dest, vehicleType)
        → new Trip(passengerId, pickup, dest, vehicleType)  // emit TripRequestedEvent
        → trip.SetSearching()                              // emit TripSearchingEvent
        → TripRepository.AddAsync(trip)
        → return trip

[System - TripMatchingWorker] Poll trips đang Searching (dùng trip.IsSearching())
    → MatchingService.MatchDriverToTripAsync(tripId, driverId)
        → lọc driver: Status == Online AND VehicleType match AND Wallet >= Commission
        → trip.MatchDriver(driverId)                       // emit TripMatchedEvent
        → driver.SetOnTrip()                               // emit DriverStatusChangedEvent
        → save Trip + Driver

[Driver] Đến điểm đón
    → TripService.ArriveAtPickup(tripId)
        → trip.MarkAsArrived()                             // emit TripArrivedEvent

[Driver] Bắt đầu chuyến
    → TripService.StartTrip(tripId)
        → trip.StartTrip()                                 // emit TripStartedEvent

[Driver] Hoàn thành chuyến
    → TripService.CompleteTrip(tripId, distanceKm)
        → fare = FareService.CalculateFare(vehicleType, distanceKm)
        → trip.CompleteTrip()                              // emit TripCompletedEvent
        → trip.ConfirmPayment()                            // emit TripPaidEvent
        → driver.PayCommission(fare)
        → driver.SetOnline()
        → passenger.AddTrip()
        → save all

[Passenger] Đánh giá
    → ReviewService.AddReviewAsync(driverId, passengerId, tripId, rating, comment)
        → new Review(...)                                  // emit ReviewCreatedEvent
        → driver.UpdateReviews(rating)
        → save Review + Driver
```

### 10.2 Luồng Timeout

```
[System - TripTimeoutWorker] Poll trips đang Searching (dùng trip.IsSearching())
    → trip.MarkTimeout()                                   // emit TripTimeoutEvent
    → save Trip
```

### 10.3 Tính Giá Cước

```
FareRule.CalculateFare(distanceKm):
    totalAmount = BaseFare + (PerKmRate × distanceKm)
    commission = totalAmount × CommissionRate
    driverIncome = totalAmount - commission
    return Fare(totalAmount, commission, driverIncome)
```

---

## 11. Bài Học Từ DEVLOG

### 11.1 Problems Đã Giải Quyết

| #   | Ngày       | Ngữ cảnh     | Vấn đề                                                                       | Nguyên nhân                                                                                                                  | Hướng nghĩ                                                                                                                                                              |
| --- | ---------- | ------------ | ---------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| P01 | 2026-04-24 | Matching     | Race condition: hai trip Searching có thể cùng nhận một driver               | `MatchDriverAsync` không có lock — không có `SemaphoreSlim`                                                                  | Đã thêm `SemaphoreSlim(1,1)` trong `MatchingService` và `TripService` để tránh double-assignment                                                                        |
| P02 | 2026-04-24 | Matching     | Thuật toán lọc driver chưa đầy đủ                                            | Chỉ lọc `Status == Online` + `VehicleType` match; chưa lọc: địa chỉ hành chính (phường→quận→thành phố), số dư `Wallet` đủ | Đã thêm kiểm tra `Wallet >= Commission` trong `MatchingService`; bỏ `MaxPickupDistance` khỏi Vehicle hierarchy (dùng `SimulationConstants.MaxPickupDistanceKm` nếu cần) |
| P03 | 2026-04-24 | Architecture | Presentation phụ thuộc trực tiếp vào Domain và Infrastructure                | Vi phạm Clean Architecture — Presentation nên chỉ phụ thuộc Application                                                      | Về lâu dài: tách DTO layer, chỉ expose Application interfaces ra Presentation. Trước mắt: ghi nhận, không block MVP                                                     |
| P04 | 2026-04-24 | Async        | `TripService` mix sync và async (`.Result` usage) trong một số call chain    | `RequestTrip()` là sync nhưng gọi async internals — `.Result` có thể gây deadlock trong WinForms message loop                | Tách hẳn sync và async path; hoặc dùng `Task.Run()` bọc ở Presentation, không gọi `.Result` trực tiếp                                                                   |
| P05 | 2026-04-24 | Map          | Polyline decoding chưa hoàn chỉnh — route không hiển thị trên MapControl     | OSRM trả về encoded polyline string; GMapRoute cần list Coordinate; chưa implement decoder                                   | Đã thêm `DecodePolyline` trong `MapControl`; chưa test tích hợp đầy đủ                                                                                                  |
| P06 | 2026-04-24 | Simulation   | `SimulationService` là stub — không có timer, không tự động di chuyển driver | `IDriverSimulationService` interface thiếu định nghĩa; class là no-op                                                        | Đã implement `SimulationService` với `System.Threading.Timer`, `StartSimulation/StopSimulation`, `CreateDefaultAsync()` factory                                         |

### 11.2 Bugs Đã Sửa

| #   | Ngày       | Module                                        | Lỗi                                                                        | Nguyên nhân                                                                                                       | Cách fix                                                                                                                                                  |
| --- | ---------- | --------------------------------------------- | -------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------- |
| B01 | 2026-04-24 | `Presentation/Program.cs`                     | Compile error: `RouteService` không tìm thấy                               | `new RouteService(...)` — `RouteService` class chưa tồn tại trong codebase                                        | Xoá dòng khởi tạo này hoặc tạo class `SimpleRouteService : IRouteService` wrapper `MapService`; cập nhật `IRouteService` → `IMapService` trong Program.cs |
| B02 | 2026-04-24 | `Presentation/Program.cs`                     | Compile error: `IDriverSimulationService` undefined                        | Interface được reference trong Program.cs (manual composition) nhưng chưa khai báo trong `Application.Interfaces` | Tạo `IDriverSimulationService` interface trong `Application/Interfaces/`; tạo stub impl `DriverSimulationService`                                         |
| B03 | 2026-04-24 | `Infrastructure/Repositories`                 | `JsonStorage` không tìm thấy từ `Program.cs`                               | Missing `using` statement cho Infrastructure namespace trong Presentation                                         | Thêm `using Infrastructure.Repositories;` hoặc dùng fully-qualified name                                                                                  |
| B04 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Reference `Domain.Interfaces.IDriverRepository` (namespace không tồn tại)  | Namespace sai — đúng là `Domain.Repositories`                                                                     | Sửa `using Domain.Interfaces` → `using Domain.Repositories`                                                                                               |
| B05 | 2026-04-24 | `Application/Handlers/AssignDriverHandler.cs` | Gọi `ITripService.TryAssignDriver()` — method không tồn tại trên interface | Method name sai — interface expose `MatchDriverAsync(Guid, Guid)`                                                 | Đổi call → `MatchDriverAsync(tripId, driverId)`                                                                                                           |
| B06 | 2026-04-24 | `Presentation/ViewModels/DriverViewModel.cs`  | Missing using cho `Driver`, `Trip`, `Vehicle`, `Location`                  | Không có `using Domain.Entities;` và `using Domain.ValueObjects;`                                                 | Thêm đúng using statements                                                                                                                                |
| B07 | 2026-04-24 | `Infrastructure/Repositories/JsonStorage`     | Thread-safety chưa xác minh đầy đủ                                         | `ReaderWriterLockSlim` khai báo nhưng chưa chắc bao phủ hết read path                                             | Audit lại toàn bộ public methods của `JsonStorage` — đảm bảo mọi read dùng `EnterReadLock`, mọi write dùng `EnterWriteLock`                               |

### 11.3 Design Decisions Quan Trọng

| #   | Ngày       | Phạm vi                | Quyết định                                                                                            | Lý do                                                                                                                                                                                   | Ảnh hưởng                                                                                                       |
| --- | ---------- | ---------------------- | ----------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| D02 | 2026-04-24 | Domain / Trip          | Dùng **State Pattern** (`ITripState` + 8 state classes) thay vì switch-case trên `TripStatus` enum    | Đảm bảo chỉ chuyển trạng thái hợp lệ; dễ thêm state mới không sửa Trip class; tách biệt behavior theo trạng thái                                                                        | Mọi transition phải đi qua state object → không thể bypass business rule từ bên ngoài.                          |
| D03 | 2026-05-01 | Domain / Driver        | Chuyển từ `DriverStateMachine` sang **State Pattern** (`IDriverState` + 3 states)                     | Đồng bộ kiến trúc với Trip; State Pattern giúp quản lý behavior (SetOnline, SetOnTrip, SetOffline) sạch hơn static dictionary; hỗ trợ tốt hơn cho persistence                        | Loại bỏ `DriverStateMachine` hoàn toàn; Driver class giờ chỉ delegate hành vi sang `_currentState`.             |
| D04 | 2026-04-24 | Domain / Persistence   | Driver tham chiếu `VehicleId` (Guid) thay vì embed `Vehicle` object                                   | Giữ Vehicle là Aggregate riêng, tránh circular dependency; Vehicle có vòng đời độc lập (có thể đổi xe)                                                                                  | Query thông tin xe phải join qua VehicleRepository — 2 query thay vì 1                                          |
| D05 | 2026-04-24 | Infrastructure         | JSON file storage với `JsonDerivedType` (System.Text.Json)                                            | Ràng buộc giáo khoa không dùng SQL; `JsonDerivedType` cần thiết để deserialized `List<Usr>` giữ đúng subtype (Drv / Psg / Adm)                                                         | Dữ liệu JSON lưu thêm `$type` field — chuẩn hóa theo System.Text.Json; dễ dàng bảo trì và tương thích.          |
| D06 | 2026-04-24 | Application / Event    | `ITripService.TripStatusChanged` là `EventHandler<TripStatusChangedEventArgs>` declare trên interface | Cho phép UI subscribe qua interface mà không cần cast sang `TripService` concrete — decoupling giữa Presentation và Application                                                         | UI Forms phải unsubscribe khi close để tránh memory leak                                                        |
| D07 | 2026-04-24 | Application / Matching | Không implement `InMemoryDriverCache`, không có `Policies/` (EligibilityPolicy, AssignmentPolicy)     | MVP scope — inline logic trong `MatchingService` đủ; cache và policy object là over-engineering cho project đơn luồng                                                                   | Khi scale lên (nhiều concurrent request), cần extract policy và cache                                           |
| D08 | 2026-04-24 | Infrastructure / Map   | Dùng Photon (geocoding) + OSRM (routing) thay vì Google Maps API                                      | Google Maps yêu cầu API key có phí; Photon + OSRM là open-source, không cần key                                                                                                         | OSRM trả về encoded polyline — đã thêm decoder trong `MapControl` (chưa test đầy đủ)                            |
| D10 | 2026-05-01 | Domain / Persistence   | Sử dụng `[JsonConstructor]` và private state setters trong Domain Entities                            | Cho phép tái tạo (reconstitute) trạng thái nội bộ của object (như `_currentState`, `_isPaid`) từ JSON mà không cần public setters hay constructor tham số rỗng (Always Valid principle) | Khắc phục lỗi dữ liệu bị null/0 khi load từ file; đảm bảo tính đóng gói (encapsulation).                        |
| D11 | 2026-05-01 | Documentation          | Đồng bộ hóa toàn bộ tài liệu (README, SOURCE_MAP, docs/) với code thực tế                             | Đảm bảo tài liệu phản ánh chính xác các thay đổi về State Pattern, Persistence và Project Structure                                                                                     | Tăng tính tin cậy của tài liệu cho Agent và Developer.                                                          |

### 11.4 Lessons Learned

- [2026-05-04] [Doc] Đã cấu trúc lại hệ thống tài liệu: PSPEC (nghiệp vụ + limit), TECH_SPEC (kiến trúc), KNOWLEDGE (tri thức), SOURCE_MAP (codebase). Ưu tiên đọc theo thứ tự này giúp Agent nắm context nhanh mà không cần đọc lại toàn bộ code.
- [2026-05-04] [Agent] Tích hợp Learning Loop: Mỗi task xong phải ghi kết luận/bài học vào DEVLOG để Agent sau kế thừa, tránh lặp lại phân tích tốn token.
- [2026-05-06] [Doc] Kiểm tra & cập nhật tài liệu theo hướng "code-first": đối chiếu SOURCE_MAP/TECH_SPEC/Business_and_UI_Guide với contracts & flow trong codebase, giảm drift giữa tài liệu và implementation.
- [2026-05-06] [UI] Modernize UI/UX: Vehicle card selection, Driver status pills, and Passenger layout polishing.

---

## 12. Use Cases

| ID   | Tên                      | Tác nhân         | Mapping code thật                                                                                                  | Trạng thái |
| ---- | ------------------------ | ---------------- | ------------------------------------------------------------------------------------------------------------------- | ---------- |
| UC1  | Đăng nhập                | User             | `UserService.LoginAsync()` trong `OOP2026/Service.cs`; UI: `FrmAuth`                                                | ✅         |
| UC2  | Đăng ký tài xế           | Driver           | `UserService.RegisterDriverAsync()` + `VehicleFactory.CreateVehicle()`                                              | ✅         |
| UC3  | Đăng ký hành khách       | Passenger        | `UserService.RegisterPassengerAsync()`                                                                              | ✅         |
| UC4  | Đặt chuyến               | Passenger        | `ucBooking` → `PassengerService.RequestTripAsync()` → `TripCommandService.CreateTripAsync()`                        | ✅         |
| UC5  | Ghép tài xế              | System           | `MatchingService.FindBestDriversAsync()` → `ProposeDriversForTripAsync()` → `TryAssignDriverAsync()`               | ⚠️ wallet filter chưa có trong matching |
| UC6  | Đến điểm đón             | Driver           | `TripCommandService.DriverArrivedAtPickupAsync()` → `Trip.DriverArrived()`                                          | ✅         |
| UC7  | Bắt đầu chuyến           | Driver           | `TripCommandService.StartTripAsync()` → `Trip.BeginTrip()`                                                          | ✅         |
| UC8  | Hoàn thành chuyến        | Driver           | `TripCommandService.DriverArrivedAtDropoffAsync()` → `CompleteTripAsync()` → `Trip.ConfirmPayment()`                | ✅         |
| UC9  | Đánh giá tài xế          | Passenger        | `ReviewService.CreateReviewAsync()`; UI: `ucReview`                                                                | ✅         |
| UC10 | Hủy chuyến               | Passenger/Driver | `PassengerService.CancelTripAsync()` hoặc `TripCommandService.CancelTripAsync()`                                    | ✅         |
| UC11 | Lịch sử chuyến           | Passenger/Driver | `TripRepository.GetByPassengerIdAsync()` / `GetByDriverIdAsync()`; UI: `ucHistory`, `ucHistoryCard`, `ucTripCard`  | ✅         |
| UC12 | Thông tin tài xế matched | Passenger        | `Trip.DriverId` + `UserRepository.GetDriverByIdAsync()` + `VehicleRepository.GetByIdAsync()`                       | ✅         |
| UC13 | Bật/tắt trạng thái       | Driver           | `DriverCommandService.GoOnlineAsync()` / `GoOfflineAsync()`                                                        | ✅         |
| UC14 | Nhận thông tin chuyến    | Driver           | `TripQueryService.GetTripByIdAsync()` / `GetActiveTripForDriverAsync()`                                            | ✅         |
| UC15 | Chấp nhận/Từ chối chuyến | Driver           | `DriverCommandService.AcceptTripAsync()` / `RejectTripAsync()`; UI: `ucRequest`, `ucDriverHome`                    | ✅         |
| UC16 | Admin theo dõi dữ liệu   | Admin            | `FrmAdmin` + `AdminService.GetAllUsersAsync/GetAllTripsAsync/GetAllPoliciesAsync/GetAllReviewsAsync()`             | ✅         |
| UC17 | Cấu hình Policy giá      | Admin            | `AdminService.CreatePolicyAsync()`; repo: `PolicyRepository`                                                       | ✅ create; update/delete chưa có contract |
| UC18 | Driver Radar proximity   | Passenger        | `DriverQueryService.GetNearbyDriversAsync()` tồn tại; passenger radar UI chuyên biệt chưa có                       | ⚠️         |
| UC19 | Thu nhập tài xế          | Driver           | `DriverWalletService.GetIncomeAsync()` + `Driver.AddIncome()`                                                      | ✅         |
| UC20 | Báo cáo thống kê         | Admin            | `AdminService.GetTripStatisticsAsync()`, `GetTotalRevenueAsync()`, `GetTotalCommissionAsync()`                     | ✅         |
| UC21 | Dẫn đường (routing)      | Driver/Passenger | `MapService.GetDirectionsAsync()` + `Route.Polyline`; UI: `ucMap`                                                  | ✅ cơ bản  |
| UC22 | Sửa thông tin cá nhân    | User             | `UserService.UpdateProfileAsync()`; UI: `ucProfile`                                                                | ⚠️ UI phụ thuộc flow |

---

## 13. Ràng Buộc Kỹ Thuật

> **Áp dụng cho tất cả code, tài liệu, và câu trả lời từ AI.**

- **Môi trường:** .NET Framework 4.8, C# Latest, WinForms (Windows).
- **Tính năng ngôn ngữ:** Sử dụng `Nullable reference types`, `var`, `LINQ`, `switch expression`.
- **Quản lý Dependency:**
  - **Manual Dependency Injection:** Không dùng container (`Microsoft.Extensions.DependencyInjection`, v.v.). Tự quản lý dependency (truyền qua constructor hoặc `new` trực tiếp).
- **Ràng buộc khác:** Không tự ý sửa `.csproj`/`.sln` (NuGet, LangVersion) khi chưa duyệt.

---

## 14. Tổng Kết

### 14.1 Số Lượng Thành Phần

| Hạng mục             | Số lượng | Danh sách                                                                                                                                   |
| -------------------- | -------- | ------------------------------------------------------------------------------------------------------------------------------------------- |
| Form                 | 8        | `FrmMultiRole`, `FrmAdmin`, `FrmAuth`, `FrmDriver`, `FrmDriverAuth`, `FrmPassenger`, `FrmPassengerAuth`, `Form1`                               |
| UserControl chính    | 2        | `ucPassengerHome`, `ucDriverHome`                                                                                                           |
| UserControl phụ      | 12       | `ucBooking`, `ucMap`, `ucHistory`, `ucHistoryCard`, `ucReview`, `ucProfile`, `ucTrip`, `ucLocationPicker`, `ucTripStatus`, `ucTripCard`, `ucDriverCard`, `ucFareSelector` |
| Domain Entities      | 10       | `Trip`, `Usr`, `Drv`, `Psg`, `Adm`, `Pol`, `Veh`, `Moto`, `Car`, `Rev`                                                                        |
| Value Objects        | 5        | `Loc`, `Coord`, `Addr`, `Route`, `Fare`                                                                                                         |
| Trip States          | 9        | `TripPendingState`, `TripSearchingState`, `TripMatchedState`, `TripArrivedState`, `TripStartedState`, `TripDropOffState`, `TripCompletedState`, `TripCancelledState`, `TripTimeoutState` |
| Driver States        | 3        | `DriverOfflineState`, `DriverOnlineState`, `DriverOnTripState`                                                                           |
| Domain Events        | 12       | Trip (9) + Driver (2) + Review (1)                                                                                                          |
| Application Services | 11       | Trip, Driver, Passenger, User, Admin, Fare, Map, Matching, Review, Simulation, AppServiceBundle                                             |
| Repositories         | 7        | Trip, Driver, Passenger, User, FareRule, Review, Vehicle                                                                                    |

### 14.2 Lợi Ích Của Kiến Trúc Này

- **Mượt mà:** Chuyển từ Login → Màn hình chính → Đặt xe → Theo dõi không có cửa sổ nào đóng/mở.
- **Rõ ràng:** Mỗi actor có không gian làm việc riêng biệt, layout tối ưu cho nghiệp vụ của họ.
- **Mở rộng được:** Thêm chức năng mới = thêm `UserControl` mới và cắm vào đúng vị trí (Panel động, Tab mới, hoặc Modal).
- **Bảo trì được:** Tối đa tái sử dụng component; số lượng file `.Designer.cs` tối thiểu.
- **State Pattern:** Đảm bảo tính hợp lệ của mọi state transition, không thể bypass business rules.
- **Event-driven:** UI luôn cập nhật theo trạng thái thực tế, không cần polling.

### 14.3 Công Nghệ Sử Dụng

- **Backend (Logic):** C# .NET Framework 4.8
- **Frontend:** Windows Forms (WinForms)
- **Database:** JSON file storage (`Data/` folder)
- **Map:** GMap.NET 2.1.7 (`GMap.NET.WinForms` ở Presentation, `GMap.NET.Core` ở Infrastructure); Provider: Photon (geocoding) + OSRM (routing)
- **Serialization:** System.Text.Json
- **Service Composition:** Manual — khởi tạo bằng `new` trong `Program.cs` (không dùng DI container)

---

## 15. Tham Khảo

| #   | Tên                                      | Link                                                                               | Dùng cho                                                                             |
| --- | ---------------------------------------- | ---------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| R01 | OSRM HTTP API                            | http://router.project-osrm.org                                                     | Tính tuyến đường, ETA, trả về encoded polyline                                       |
| R02 | Photon Geocoding API                     | https://photon.komoot.io                                                           | Geocoding địa chỉ tiếng Việt → Coordinate                                            |
| R03 | GMap.NET GitHub                          | https://github.com/judero01pol/GMap.NET                                            | Tích hợp bản đồ vào WinForms, overlay, marker, route                                 |
| R04 | Google Encoded Polyline Algorithm        | https://developers.google.com/maps/documentation/utilities/polylinealgorithm       | Decode polyline string từ OSRM response → List\<PointLatLng\>                        |
| R05 | System.Text.Json Polymorphism            | https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism | Serialize/Deserialize đa hình (List\<Usr\> giữ Drv / Psg / Adm subtype)              |
| R06 | Microsoft.Extensions.DependencyInjection | https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection      | DI container cho .NET Framework 4.8                                                  |
| R07 | Haversine Formula                        | https://en.wikipedia.org/wiki/Haversine_formula                                    | Tính khoảng cách giữa 2 Coordinate (dùng trong TripService + MatchingService filter) |
| R08 | ReaderWriterLockSlim (.NET)              | https://learn.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlockslim | Thread-safe read/write cho JsonStorage                                               |
| R09 | State Pattern (GoF)                      | https://refactoring.guru/design-patterns/state                                     | Tham chiếu implement ITripState + 8 state classes                                    |
| R10 | SemaphoreSlim (.NET)                     | https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim        | Giải race condition trong MatchingService                                            |
| R11 | Nominatim API                            | https://nominatim.org/release-docs/latest/api/Overview/                            | Geocoding địa chỉ (dùng trong MapService)                                            |

---

> **Tài liệu được cập nhật lần cuối:** 2026-05-26
> **Người cập nhật:** Agent
