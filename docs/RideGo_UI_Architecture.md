# RideGo – Kiến Trúc Giao Diện WinForms (Phiên Bản Cuối)

> **Phạm vi:** WinForms .NET Framework 4.8 · GMap.NET · Dữ liệu JSON  
> **Mục tiêu tài liệu:** Xác định số lượng Form/UserControl, bố cục container, và quy tắc điều hướng Modal vs. Inline cho toàn bộ hệ thống RideGo.

---

## 1. Nguyên Lý Thiết Kế Nền Tảng

Bốn nguyên tắc dưới đây chi phối **mọi** quyết định giao diện. Bất kỳ thành phần nào vi phạm một trong bốn nguyên tắc này đều cần được xem xét lại.

| # | Nguyên tắc | Hệ quả thực tế |
|---|-----------|----------------|
| 1 | **Tối thiểu Form** | Chỉ tạo `Form` mới khi cần ngữ cảnh làm việc hoàn toàn biệt lập. Mọi điều hướng còn lại dùng `UserControl`. |
| 2 | **Single-Form Shell** | Một `FrmMainShell` duy nhất làm khung nền. Chuyển cảnh = hoán đổi `UserControl` bên trong Shell, không đóng/mở cửa sổ. |
| 3 | **Container theo nghiệp vụ** | Chọn loại container xuất phát từ câu hỏi: *"Người dùng tương tác với màn hình này như thế nào?"*, không phải từ sở thích cá nhân. |
| 4 | **Tái sử dụng tối đa** | Các component dùng chung (`MapControl`, `FarePanel`, `DriverCardControl`…) phải được tái sử dụng, không được viết lại với mục đích tương tự. |

---

## 2. Tổng Quan Thành Phần

### 2.1 Sơ đồ phân cấp

```
FrmMainShell  (1 Form duy nhất – khung nền)
├── FrmModal  (Form phụ – hộp thoại tập trung)
├── FrmToast  (Form phụ – thông báo real-time, không cướp focus)
│
└── [UserControl được nạp động vào Shell]
    ├── UcAuth          ← Màn hình đầu tiên (Login / Register)
    ├── UcPassenger     ← Sau khi đăng nhập với vai trò Passenger
    ├── UcDriver        ← Sau khi đăng nhập với vai trò Driver
    └── UcAdmin         ← Sau khi đăng nhập với vai trò Admin

[UserControl phụ – dùng bên trong các UC chính hoặc trong FrmModal]
├── UcRating        ← Đánh giá sau chuyến
├── UcProfile       ← Hồ sơ cá nhân / Nạp ví
└── UcTripDetail    ← Chi tiết một chuyến đi
```

### 2.2 Bảng tổng hợp

| Thành phần | Loại | Actor | Mô tả ngắn |
|-----------|------|-------|------------|
| `FrmMainShell` | Form | Tất cả | Khung nền duy nhất; quản lý nạp/đổi UC, đăng ký event cấp ứng dụng |
| `FrmModal` | Form | Tất cả | Hộp thoại dùng chung cho mọi tác vụ cần sự tập trung (CRUD, đánh giá, hồ sơ) |
| `FrmToast` | Form | Tất cả | Popup thông báo real-time (chuyến mới, tài xế đến…) không chiếm focus |
| `UcAuth` | UserControl | Tất cả | Gộp Login + Register; sau đăng nhập thành công → Shell nạp UC theo vai trò |
| `UcPassenger` | UserControl | Passenger | Toàn bộ vòng đời đặt xe: Đặt → Theo dõi → Thanh toán |
| `UcDriver` | UserControl | Driver | Bật/tắt trạng thái, nhận/từ chối chuyến, cập nhật tiến trình |
| `UcAdmin` | UserControl | Admin | Quản lý Users/Trips/FareRules + Thống kê |
| `UcRating` | UserControl | Passenger | Form đánh giá 5 sao + comment; mở trong `FrmModal` |
| `UcProfile` | UserControl | Passenger, Driver | Xem/sửa hồ sơ, nạp ví; mở trong `FrmModal` |
| `UcTripDetail` | UserControl | Admin, Passenger, Driver | Chi tiết một chuyến đi; dùng trong Tab (Admin) hoặc Modal (các vai trò còn lại) |

---

## 3. Bố Cục Chi Tiết Từng UserControl Chính

### 3.1 `UcAuth` – Xác thực tập trung

**Container chính:** `TableLayoutPanel` (1 cột, 3 hàng: Logo · Form · Footer)

**Lý do:** Cần căn giữa theo cả chiều ngang lẫn dọc khi Shell co giãn. `TableLayoutPanel` với `Anchor = None` và `AutoSize` trên ô giữa cho phép điều này mà không cần tọa độ cứng.

```
UcAuth (Dock Fill)
└── TableLayoutPanel (1 cột × 3 hàng, Row 2 = Fill)
    ├── Row 1 – Logo / Tên ứng dụng (cố định Height)
    ├── Row 2 – Panel trung tâm (Fill, căn giữa ngang)
    │   └── Panel "pnlCenter" (AutoSize, Anchor = None)
    │       ├── pnlLogin  (Label, TextBox phone, TextBox password, Button "Đăng nhập", LinkLabel "→ Đăng ký")
    │       └── pnlRegister (TextBox name/phone/password, ComboBox Role, Button "Tạo tài khoản", LinkLabel "→ Đăng nhập")
    │           [Hai panel ẩn/hiện luân phiên – KHÔNG dùng TabControl]
    └── Row 3 – Footer nhỏ (cố định Height)
```

> **Quyết định thiết kế:** Dùng hai `Panel` ẩn/hiện thay vì `TabControl` vì tab bar của `TabControl` tạo ra affordance "người dùng có thể tự chọn tab", điều không phù hợp với flow xác thực tuyến tính.

---

### 3.2 `UcPassenger` – Vòng đời đặt xe

**Container chính:** `SplitContainer` (Vertical – bản đồ trái, bảng điều khiển phải)

**Lý do:** Bản đồ cần chiếm phần lớn không gian và cho phép người dùng kéo thanh chia; bảng bên phải thay đổi nội dung theo trạng thái chuyến mà không ảnh hưởng bản đồ.

```
UcPassenger (Dock Fill)
└── SplitContainer (Orientation = Vertical, Panel1 ~70%, Panel2 ~30%)
    │
    ├── Panel1 – Bản đồ
    │   └── MapControl (Dock Fill)
    │       [Hiển thị marker Pickup / Destination, mô phỏng vị trí tài xế]
    │
    └── Panel2 – Bảng điều khiển động
        └── TableLayoutPanel (1 cột × 3 hàng)
            ├── Row 1 – Header (cố định, hiển thị tên + Button "Lịch sử" + Button "Hồ sơ")
            ├── Row 2 – pnlActionStage (Fill) ← "sân khấu" thay nội dung theo trạng thái Trip
            │   ├── [Idle / Timeout]     → BookingPanel
            │   │   · LocationPickerControl (Pickup + Destination)
            │   │   · ComboBox (VehicleType: Car / Motorbike)
            │   │   · FarePanel (Base fare, /km, Tổng dự kiến)
            │   │   · Button "Đặt xe" [lớn, full-width]
            │   │
            │   ├── [Searching]          → SearchingPanel
            │   │   · ProgressBar (marquee) + Label "Đang tìm tài xế…"
            │   │   · Button "Hủy yêu cầu"
            │   │
            │   ├── [Matched / Arrived / Started] → TrackingPanel
            │   │   · TripStatusPanel (hiển thị trạng thái hiện tại)
            │   │   · DriverCardControl (tên, sao, loại xe, biển số)
            │   │   · Button "Hủy chuyến" [chỉ Enable khi Status < Started]
            │   │
            │   └── [Completed]          → PaymentPanel
            │       · Label Tổng tiền (lớn, nổi bật)
            │       · Button "Xác nhận thanh toán"
            │       · Button "Đánh giá tài xế" → mở UcRating trong FrmModal
            │
            └── Row 3 – StatusBar nhỏ (cố định, hiển thị trạng thái kết nối / thông báo)
```

> **Cơ chế chuyển Panel:** `TripStatusChanged` event → Shell hoặc `UcPassenger` gọi helper `ShowStage(TripStatus status)` → ẩn tất cả panel con, hiện đúng panel tương ứng. Không dùng `TabControl` vì người dùng không được phép tự chuyển tab.

---

### 3.3 `UcDriver` – Trạm chỉ huy tài xế

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
    │       · Button "Hồ sơ / Nạp ví" → mở UcProfile trong FrmModal
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
                        · TripStatusPanel (thông tin Passenger + điểm đón/đến)
                        · Button "Đã đến điểm đón"  [Height = 80px]
                        · Button "Bắt đầu chuyến"   [Height = 80px]
                        · Button "Hoàn thành"        [Height = 80px]
                        · Button "Hủy chuyến"        [Height = 48px, màu cảnh báo]
                        [Chỉ button phù hợp với trạng thái hiện tại mới Enable = true]
```

> **Quyết định thiết kế:** Các nút hành động cao 80px là bắt buộc – tài xế thao tác trong khi lái xe (hoặc vừa dừng xe), diện tích bấm nhỏ là lỗi nghiêm trọng về UX.

---

### 3.4 `UcAdmin` – Trung tâm quản trị

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
    │       · Thêm Button "Xem chi tiết" → mở UcTripDetail trong FrmModal
    │       · Thêm Button "Hủy chuyến" (chỉ Enable khi Status chưa terminal)
    │
    ├── TabPage "Giá cước"
    │   └── TableLayoutPanel (1 cột × 2 hàng)
    │       ├── Toolbar: Button "Thêm mới" · Button "Sửa" · Button "Xóa"
    │       └── DataGridView "dgvFareRules" (Dock Fill)
    │           Columns: VehicleType · BaseFare · PricePerKm · CommissionRate
    │           [Thêm/Sửa → mở form nhập liệu trong FrmModal]
    │
    └── TabPage "Thống kê"
        └── TableLayoutPanel (2 cột × 2 hàng, padding 24px)
            · Panel "GMV"              – Label số lớn + Label đơn vị
            · Panel "Tỷ lệ hoàn thành" – Label % + ProgressBar minh họa
            · Panel "Điểm hài lòng"    – Label trung bình sao
            · Panel "Tổng chuyến"      – Label số chuyến theo trạng thái
            [Button "Làm mới dữ liệu" ở góc trên phải]
```

---

## 4. Quy Tắc Modal vs. Inline

| Hành động người dùng | Phương thức | Lý do |
|----------------------|-------------|-------|
| Đăng nhập / Đăng ký | **Inline** (UcAuth) | Là điểm khởi đầu duy nhất; không cần cô lập |
| Đặt xe, Theo dõi, Thanh toán | **Inline** (UcPassenger – Panel động) | Cập nhật real-time liên tục; gián đoạn bởi Form mới = mất trạng thái |
| Tìm kiếm tài xế (Searching) | **Inline** | Trạng thái trung gian ngắn; không cần cửa sổ riêng |
| Chấp nhận / Từ chối chuyến | **Inline** (UcDriver) | Phản xạ tức thì, là core workflow của tài xế |
| Cập nhật tiến trình chuyến | **Inline** (UcDriver) | Thao tác liên tiếp; cần giao diện luôn hiển thị |
| Xem lịch sử chuyến đi | **Inline** (Panel ẩn/hiện trong UcPassenger / Tab trong UcDriver) | Không cần cô lập; người dùng có thể cần xem lại khi đặt chuyến mới |
| Đánh giá tài xế | **Modal** (FrmModal + UcRating) | Tác vụ một lần sau chuyến; cần sự tập trung; không liên quan đến màn hình chính |
| Xem/Sửa hồ sơ, Nạp ví | **Modal** (FrmModal + UcProfile) | Tác vụ ít dùng, cô lập; không nên chiếm không gian màn hình làm việc |
| Chi tiết chuyến (Passenger/Driver) | **Modal** (FrmModal + UcTripDetail) | Xem thêm thông tin, không thay thế màn hình chính |
| Chi tiết chuyến (Admin) | **Inline** (trong Tab "Chuyến đi") | Admin xem nhiều chuyến liên tiếp; Modal gây gián đoạn không cần thiết |
| CRUD FareRule (Admin) | **Modal** (FrmModal) | Nhập liệu cô lập; tránh làm nhiễu DataGrid chính |
| Xác nhận hủy chuyến | **Modal** (FrmModal – dialog Yes/No) | Hành động không thể hoàn tác; cần xác nhận rõ ràng |

---

## 5. Cơ Chế Real-time & Sự Kiện

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
    └──→ FrmMainShell.OnTripStatusChanged(status)
            └── FrmToast.Show(message) – thông báo nổi

[Timer polling – dùng System.Windows.Forms.Timer]
· Interval: 1000ms (1 giây) cho Passenger tracking
· Interval: 2000ms (2 giây) cho Driver request check
· Tất cả cập nhật UI phải trên UI thread (Control.Invoke nếu cần)
```

---

## 6. Quy Tắc Layout & Responsive

Mọi control phải tuân theo các quy tắc sau để tránh tọa độ cứng:

| Tình huống | Quy tắc bắt buộc |
|-----------|-----------------|
| Control chiếm toàn bộ container | `Dock = Fill` |
| Control bám theo cạnh container | `Anchor = Top, Left` (mặc định) hoặc cụ thể theo vị trí |
| Control căn giữa động | Đặt trong `TableLayoutPanel` với ô có `AutoSize`; đặt `Anchor = None` trên control |
| Khu vực cố định + khu vực co giãn | `TableLayoutPanel` với `RowStyle`: hàng cố định dùng `Absolute`, hàng co giãn dùng `Percent = 100` |
| Hai khu vực kéo được | `SplitContainer` với `IsSplitterFixed = false` |
| **Cấm tuyệt đối** | Đặt `Location.X`, `Location.Y` trực tiếp trên control trong Designer |

---

## 7. Tổng Kết

| Hạng mục | Số lượng | Danh sách |
|---------|---------|-----------|
| Form | 3 | `FrmMainShell`, `FrmModal`, `FrmToast` |
| UserControl chính | 4 | `UcAuth`, `UcPassenger`, `UcDriver`, `UcAdmin` |
| UserControl phụ | 3 | `UcRating`, `UcProfile`, `UcTripDetail` |
| Component tái sử dụng | 6+ | `MapControl`, `FarePanel`, `DriverCardControl`, `LocationPickerControl`, `TripStatusPanel`, `TripCard` |

**Lợi ích của kiến trúc này:**

- **Mượt mà:** Chuyển từ Login → Màn hình chính → Đặt xe → Theo dõi không có cửa sổ nào đóng/mở.
- **Rõ ràng:** Mỗi actor có không gian làm việc riêng biệt, layout tối ưu cho nghiệp vụ của họ.
- **Mở rộng được:** Thêm chức năng mới = thêm `UserControl` mới và cắm vào đúng vị trí (Panel động, Tab mới, hoặc Modal).
- **Bảo trì được:** Tối đa tái sử dụng component; số lượng file `.Designer.cs` tối thiểu.
