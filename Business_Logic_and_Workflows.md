Nghiên Cứu Hệ Thống Đặt Xe Mô Phỏng (Kiểu Grab/Uber): Kiến Trúc, Chức Năng, Thuật Toán, Vấn Đề Kỹ Thuật & Triển Khai

---

## Giới Thiệu

Hệ thống đặt xe mô phỏng (ride-hailing simulation system) là một chủ đề nghiên cứu và phát triển hấp dẫn, đặc biệt trong bối cảnh các nền tảng như Grab, Uber, Gojek đã thay đổi căn bản ngành vận tải đô thị. Việc xây dựng một hệ thống mô phỏng không chỉ giúp kiểm thử thuật toán, đánh giá hiệu năng mà còn là nền tảng để đào tạo, nghiên cứu và phát triển các giải pháp mới. Báo cáo này sẽ phân tích chi tiết kiến trúc, chức năng, thuật toán, các vấn đề kỹ thuật và giải pháp triển khai cho một hệ thống đặt xe mô phỏng, đáp ứng đầy đủ các yêu cầu thực tế và mô phỏng sát với hệ thống sản xuất.

---

## 1. Tổng Quan Kiến Trúc Hệ Thống Đặt Xe Mô Phỏng

### 1.1. Kiến Trúc Phù Hợp Đồ Án

Để phù hợp với quy mô đồ án và đơn giản hóa triển khai, hệ thống sử dụng kiến trúc Layered Architecture với các tầng rõ ràng:

```
Presentation (WinForms)
        ↓
Application Layer (Services)
        ↓
Infrastructure (File Repository - JSON)
        ↓
Domain Layer (Entities + Enums + Events)
```

- **Presentation Layer**: Giao diện người dùng (WinForms), xử lý input/output và hiển thị.
- **Application Layer**: Các service điều phối logic nghiệp vụ, gọi domain và infrastructure.
- **Infrastructure Layer**: Repository lưu trữ dữ liệu bằng JSON files (Newtonsoft.Json).
- **Domain Layer**: Chứa entities, enums, events, state machines, business rules. **Không phụ thuộc vào bất kỳ layer nào khác.**

> **Nguyên tắc vàng:** Luồng phụ thuộc luôn hướng vào Domain. Domain không biết Infrastructure hay Presentation tồn tại. Điều này cho phép thay thế lớp lưu trữ (JSON → SQL Server) mà không sửa business logic.

### 1.2. Lợi ích của kiến trúc phân tầng

Kiến trúc này giúp hệ thống:

- **Dễ mở rộng**: Mỗi service có thể scale độc lập.
- **Đảm bảo hiệu năng**: Phân tải, cache, xử lý song song.
- **Dễ bảo trì, phát triển**: Tách biệt logic, dễ nâng cấp.
- **Khả năng chịu lỗi cao**: Một service lỗi không ảnh hưởng toàn hệ thống.

---

## 2. Chức Năng Chính Của Hệ Thống Đặt Xe Mô Phỏng

### 2.1. Chức Năng Tìm Kiếm & Đặt Xe

Hành khách nhập điểm đón, điểm đến, loại xe, hệ thống sẽ tìm kiếm tài xế phù hợp gần nhất, tính toán giá cước, hiển thị thông tin và cho phép đặt xe [1](https://taixeviet.vn/tim-tai-xe) [2](https://www.techinterview.org/post/3233465615/lld-ride-sharing/). Quá trình này gồm các bước:

- Nhập thông tin chuyến đi (pickup, dropoff, loại xe).
- Xem giá dự kiến (bao gồm surge nếu có).
- Gửi yêu cầu đặt xe.
- Hệ thống tìm kiếm tài xế phù hợp, gửi request lần lượt.
- Khi tài xế nhận chuyến, xác nhận và hiển thị thông tin tài xế cho hành khách.

### 2.2. Xác Nhận & Hủy Chuyến

- **Xác nhận chuyến**: Khi tài xế chấp nhận, trạng thái chuyển sang Matched/Accepted, hành khách nhận thông báo.
- **Hủy chuyến**: Hành khách hoặc tài xế có thể hủy trước khi chuyến bắt đầu, hệ thống cập nhật trạng thái, tính phí hủy nếu cần [3](https://www.vietnamairlines.com/vn/vi/help-desk/common-topics/Ticket-refund/dieu-kien-hoan-ve-va-muc-phi) [2](https://www.techinterview.org/post/3233465615/lld-ride-sharing/).

### 2.3. Quản Lý Thông Tin Tài Xế & Khách Hàng

- Đăng ký/đăng nhập, cập nhật thông tin cá nhân, phương tiện, giấy tờ, trạng thái hoạt động.
- Quản lý lịch sử chuyến đi, đánh giá, thu nhập (tài xế) [4](https://drivemond.app/admin-panel/).

### 2.4. Lưu Trữ Lịch Sử Chuyến & Audit Log

- Lưu trữ chi tiết từng chuyến: thời gian, trạng thái, vị trí, tài xế, hành khách, giá cước, feedback.
- Audit log cho các thao tác quan trọng (hủy, xác nhận, thanh toán) phục vụ kiểm tra, giải quyết khiếu nại.

### 2.5. Quản Lý Thanh Toán & Tiền Mặt

- Hỗ trợ thanh toán tiền mặt (mô phỏng), lưu trạng thái thanh toán, cập nhật thu nhập tài xế.
- Có thể mở rộng tích hợp ví điện tử, thẻ, cổng thanh toán [5](https://www.ongraph.com/taxi-app-payment-gateway-integration/).

### 2.6. Đánh Giá & Phản Hồi Dịch Vụ

- Hành khách đánh giá tài xế, tài xế đánh giá hành khách sau mỗi chuyến.
- Lưu trữ điểm số, phản hồi, phục vụ cải thiện chất lượng dịch vụ [6](https://www.appicial.com/blog/how-ratings-and-feedback-drive-continuous-improvement-in-taxi-services.html).

---

## 3. Bảng Tổng Hợp Chức Năng Theo Vai Trò

| Chức năng               | Passenger Side | Driver Side            | Admin System           |
| ----------------------- | -------------- | ---------------------- | ---------------------- |
| Đăng ký/Đăng nhập       | ✔️             | ✔️                     | ✔️                     |
| Nhập điểm đón/điểm đến  | ✔️             |                        |                        |
| Chọn loại xe            | ✔️             |                        |                        |
| Xem giá                 | ✔️             |                        |                        |
| Gửi request/chọn chuyến | ✔️             | Nhận request phù hợp   |                        |
| Hủy chuyến              | ✔️             | ✔️                     | ✔️                     |
| Nhận thông báo          | ✔️             | ✔️                     |                        |
| Theo dõi trạng thái     | ✔️             | ✔️                     | ✔️                     |
| Theo dõi vị trí tài xế  | ✔️ (mô phỏng)  |                        | ✔️ (giám sát)          |
| Thanh toán tiền mặt     | ✔️ (mô phỏng)  | ✔️ (ghi nhận thu nhập) | ✔️ (báo cáo)           |
| Đánh giá                | ✔️ (tài xế)    | ✔️ (hành khách)        | ✔️ (quản lý feedback)  |
| Xem lịch sử             | ✔️             | ✔️                     | ✔️                     |
| Bật/tắt trạng thái      |                | ✔️                     | ✔️                     |
| Quản lý tài khoản       |                |                        | CRUD hành khách/tài xế |
| Quản lý chuyến đi       |                |                        | ✔️                     |
| Báo cáo thống kê        |                |                        | ✔️                     |
| Cấu hình giá cước       |                |                        | ✔️                     |

**Phân tích:**  
Bảng trên cho thấy hệ thống phải đáp ứng đầy đủ các luồng nghiệp vụ cho từng vai trò, đảm bảo trải nghiệm thực tế và khả năng kiểm soát, giám sát toàn diện từ phía quản trị viên [4](https://drivemond.app/admin-panel/) [7](https://www.moovrsoft.com/passenger-app).

---

## 4. Quản Lý Trạng Thái Tài Xế & Chuyến Đi

### 4.1. Trạng Thái Tài Xế

| Trạng thái | Mô tả                                             |
| ---------- | ------------------------------------------------- |
| Offline    | Không nhận chuyến, không hiển thị trên bản đồ     |
| Available  | Sẵn sàng nhận chuyến, xuất hiện trong tìm kiếm    |
| OnTrip     | Đang thực hiện chuyến đi, tạm thời không nhận mới |

**Phân tích:**  
Quản lý trạng thái tài xế là yếu tố then chốt để đảm bảo thuật toán ghép tài xế hoạt động chính xác, tránh trường hợp double booking hoặc tài xế nhận nhiều chuyến cùng lúc [2](https://www.techinterview.org/post/3233465615/lld-ride-sharing/) [8](https://www.geeksforgeeks.org/system-design/how-uber-finds-nearby-drivers-at-1-million-requests-per-second/).

### 4.2. Trạng Thái Chuyến Đi

| Trạng thái | Mô tả                                   |
| ---------- | --------------------------------------- |
| Requested  | Hành khách gửi yêu cầu, chưa tìm tài xế |
| Searching  | Hệ thống đang tìm kiếm tài xế phù hợp   |
| Matched    | Đã ghép tài xế, chờ xác nhận            |
| Arrived    | Tài xế đã đến điểm đón                  |
| Started    | Chuyến đi bắt đầu                       |
| Completed  | Chuyến đi hoàn thành                    |
| Cancelled  | Bị hủy bởi hành khách/tài xế/hệ thống   |
| Timeout    | Không có tài xế nhận, tự động hủy       |

**Phân tích:**  
Mô hình trạng thái chuyến đi (state machine) giúp kiểm soát logic nghiệp vụ, đảm bảo các thao tác chuyển trạng thái hợp lệ, hỗ trợ rollback, audit, và xử lý bất đồng bộ hiệu quả [2](https://www.techinterview.org/post/3233465615/lld-ride-sharing/) [9](https://help.sap.com/docs/SAP_S4HANA_ON-PREMISE/f4153779fcb742c094bb6157c0ff1cbe/4d5900260afd3ba4e10000000a42189b.html).

**Lưu ý về trạng thái ban đầu:** Trip được tạo ở trạng thái Requested khi hành khách gửi yêu cầu. Sau khi RequestTrip() được gọi, trạng thái chuyển sang Searching để bắt đầu tìm tài xế.

---

## 5. Thuật Toán Tìm & Ghép Tài Xế (Dispatch Algorithm)

### 5.1. Quy Trình Ghép Tài Xế

Vì không dùng LINQ, hệ thống duyệt `foreach` trên toàn bộ danh sách `Driver` được tải từ `DriverRepository`.

**Tiêu chí lọc (theo thứ tự):**

1. **Lọc theo loại xe**: Chỉ chọn tài xế có `VehicleType` khớp yêu cầu (`Car` / `Motorbike`).
2. **Loại bỏ không đủ điều kiện**: Loại tài xế `Offline`, đang `OnTrip`.
3. **Lọc thô theo địa chỉ hành chính** (xem mục 5.1.1 bên dưới) — tránh gọi route API cho toàn bộ danh sách.
4. **Kiểm tra khoảng cách**: Khoảng cách từ vị trí tài xế đến điểm đón < `MaxPickupDistance` của phương tiện.
5. **Kiểm tra ví**: Số dư `Wallet` đủ để khấu trừ hoa hồng dự kiến.
6. **Gửi request lần lượt**: Theo thứ tự ưu tiên, mỗi tài xế có thời gian xác nhận (timeout).
7. **Retry logic**: Nếu tài xế từ chối hoặc timeout, thử tài xế tiếp theo cho đến hết danh sách.
8. **Fallback**: Nếu hết tài xế → `Trip.MarkTimeout()`.

#### 5.1.1. Lọc Thô Theo Địa Chỉ Hành Chính

**Bài toán:** Không thể gọi route API cho hàng nghìn tài xế cùng lúc — quá tốn tài nguyên.

**Giải pháp:** Lọc bằng cấp địa chỉ trước, sau đó mới tính route cho nhóm nhỏ:

```
Pickup point
    │
    ▼ Reverse Geocoding (GMap.NET Placemark)
Lấy: LocalityName (phường) → SubAdminArea (quận) → AdminArea (thành phố)
    │
    ▼ Lọc Driver (foreach, không LINQ)
Cùng phường? → đủ số lượng? → Tính route cho nhóm nhỏ
    │ Không đủ
    ▼
Mở rộng lên quận → đủ? → Tính route
    │ Không đủ
    ▼
Mở rộng lên thành phố → Tính route
```

Cách tiếp cận này thực tế, không cần công nghệ mới, phù hợp với constraint "không LINQ".

### 5.2. Xử Lý Race Condition

**Vấn đề:** Khi hai tài xế cùng nhấn "Chấp nhận" một chuyến tại cùng một mili giây — cả hai đều đọc `Trip.Status == Searching` và cùng tiến hành ghép.

**Giải pháp chính xác:** Dùng `SemaphoreSlim` để tạo "khóa" tại `MatchDriverAsync`:

```csharp
private static readonly SemaphoreSlim _matchLock = new SemaphoreSlim(1, 1);

public async Task MatchDriverAsync(Guid tripId, Guid driverId)
{
    await _matchLock.WaitAsync();
    try
    {
        Trip trip = await _tripRepository.GetByIdAsync(tripId);
        Driver driver = await _driverRepository.GetByIdAsync(driverId);

        if (trip.Status != TripStatus.Searching)
            throw new InvalidOperationException("Trip đã được nhận bởi tài xế khác.");
        if (driver.Status != DriverStatus.Available)
            throw new InvalidOperationException("Tài xế không còn sẵn sàng.");

        trip.MatchDriver(driverId);
        driver.SetOnTrip();

        _tripRepository.Update(trip);
        _driverRepository.Update(driver);
        await _tripRepository.SaveChangesAsync();
        await _driverRepository.SaveChangesAsync();
    }
    finally
    {
        _matchLock.Release();
    }
}
```

Khi luồng đầu tiên đang xử lý, các luồng khác chờ ở `WaitAsync()`. Sau khi `Trip.Status` chuyển sang `Matched` và được lưu, luồng tiếp theo đọc lại trạng thái và bị từ chối ngay.

### 5.3. Retry Logic & Fallback

- **Retry logic**: Nếu tài xế đầu tiên không phản hồi, hệ thống tự động thử tài xế tiếp theo.
- **Fallback**: Nếu hết tài xế, thông báo cho hành khách, chuyển trạng thái chuyến đi sang Timeout.

---

## 6. Theo Dõi Hành Trình Realtime & Map Animation

### 6.1. Theo Dõi Vị Trí & Animation

- **Driver app** cập nhật vị trí định kỳ.
- **Hệ thống** lưu vị trí tài xế, phục vụ tìm kiếm và cập nhật bản đồ.
- **Passenger app** nhận update qua polling hoặc events, hiển thị vị trí tài xế, animation mượt (interpolation, bearing calculation, auto-zoom).

### 6.2. Location Simulation

- **Mô phỏng vị trí tài xế**: Sinh ngẫu nhiên tọa độ trong khu vực HCM, cập nhật liên tục để mô phỏng di chuyển thực tế.
- **Dữ liệu ảo**: Có thể dùng GTFS, GeoJSON, hoặc random hóa trong bounding box HCM [16](https://enlighteneddata.github.io/EDprofile/TransformVietnam.html).

### 6.3. Map & Simulation Logic

- **Phân tầng overlay**: Hiển thị nhiều lớp (tài xế, hành khách, tuyến đường, vùng surge).
- **Xử lý tọa độ**: Sử dụng thư viện GMap.NET, Google Maps API, Mapbox để chuyển đổi, vẽ tuyến, marker.
- **Animation mượt**: Interpolation giữa các điểm GPS, tính toán bearing, auto-zoom theo trạng thái chuyến đi [17](https://www.youtube.com/watch?v=to16P4N-rqg) [15](https://mapimator.com/).
- **Bất đồng bộ**: Sử dụng async/await, event-driven để đảm bảo UI không bị lag khi nhận update liên tục.

---

## 7. Notification Simulation & Giao Tiếp Realtime

### 7.1. Notification Simulation

- **MessageBox trong WinForms**: Mô phỏng thông báo realtime cho tài xế, hành khách khi có sự kiện (matched, arrived, started, completed, cancelled) [18](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox?view=windowsdesktop-10.0).
- **Push notification**: Có thể mở rộng tích hợp Firebase, OneSignal cho mobile app.

### 7.2. Giao Tiếp Realtime

- **Polling**: Client gửi request định kỳ lấy dữ liệu mới, phù hợp cho mô phỏng nhỏ.

---

## 8. TripService: Orchestrator Vòng Đời Chuyến Đi

### 8.1. Vai Trò Orchestrator

- **TripService** là trung tâm điều phối toàn bộ vòng đời chuyến đi: từ nhận request, tìm tài xế, cập nhật trạng thái, xử lý thanh toán, lưu trữ lịch sử, thống kê [19](https://deepwiki.com/world-cyj/aitrip/2.1-react-orchestrator-and-trip-planning-service).
- **Các bước chính**:
  1. Nhận request đặt xe từ hành khách.
  2. Gọi MatchingService tìm tài xế phù hợp.
  3. Gửi request, nhận phản hồi tài xế.
  4. Cập nhật trạng thái chuyến đi, thông báo cho các bên.
  5. Theo dõi hành trình, cập nhật vị trí.
  6. Xử lý hoàn thành/chấm dứt chuyến đi.
  7. Thanh toán, cập nhật thu nhập, lưu lịch sử.
  8. Sinh báo cáo, thống kê cho admin.

### 8.2. Lưu Trữ & Thống Kê

- **SQL/NoSQL**: Lưu trữ trip data, event log, trạng thái, feedback.
- **Data warehouse**: Lưu trữ dữ liệu lớn phục vụ phân tích, ML.
- **Thống kê**: Số chuyến, doanh thu, tỷ lệ hủy, thời gian chờ, feedback.

---

## 9. Cơ Chế Tự Động Expiry/Timeout

- **Expire searching trips**: Nếu sau X giây/phút không tìm được tài xế, tự động chuyển trạng thái sang Timeout, thông báo cho hành khách.
- **Expire matched trips**: Nếu tài xế đã matched nhưng không đến điểm đón/hành khách không xác nhận sau Y phút, tự động hủy, cập nhật trạng thái, giải phóng tài xế [20](https://www.flyertalk.com/forum/united-airlines-mileageplus/2126795-session-expired-your-united-com-session-timed-out-while-doing-flight-search.html) [2](https://www.techinterview.org/post/3233465615/lld-ride-sharing/).
- **Session timeout**: Đảm bảo bảo mật, tránh giữ trạng thái quá lâu gây lỗi logic.

---

## 10. Quản Lý Dữ Liệu

### 10.1. Lưu Trữ Trip Data

- **JSON/Binary Files**: Lưu trữ dữ liệu người dùng, tài xế, chuyến đi, lịch sử dưới dạng file JSON hoặc binary.
- **Repository Pattern**: Sử dụng repository để quản lý truy cập dữ liệu, hỗ trợ lưu và tải dữ liệu.

---

## 12. Giám Sát, Logging & Observability

- **Prometheus, Grafana**: Thu thập metrics (request rate, latency, error rate), hiển thị dashboard realtime [23](https://github.com/richxcame/ride-hailing/blob/main/monitoring/README.md).
- **ELK Stack (Elasticsearch, Logstash, Kibana)**: Lưu trữ, phân tích log, hỗ trợ tìm kiếm, cảnh báo.
- **Distributed tracing (OpenTelemetry, Tempo)**: Theo dõi luồng request qua các service, phát hiện bottleneck.
- **Alerting**: Thiết lập rule cảnh báo khi có lỗi, downtime, hiệu năng thấp, tỷ lệ hủy cao.

---

## 13. Kiểm Thử & Mô Phỏng Tải

- **Load testing**: Sử dụng công cụ như LoadNinja, JMeter để mô phỏng hàng nghìn request đồng thời từ nhiều vị trí địa lý khác nhau, kiểm tra khả năng chịu tải, độ trễ, tỷ lệ thành công [24](https://support.smartbear.com/loadninja/docs/use-cases/location.html).
- **Trip simulation phases**: Implemented background simulation with phases: ToPickup (3 seconds), Arrived (3 seconds wait), ToDestination (4 seconds), Completed. Timeout pending trips after 60 seconds.
- **Test case**: Kiểm thử các luồng nghiệp vụ chính, xử lý lỗi, timeout, race condition, failover.
- **Replay simulation**: Ghi lại log thực tế, phát lại để kiểm tra logic, debug.

---

## 14. UI/UX: Passenger & Driver

### 14.1. Passenger Side

- Đăng ký/đăng nhập, nhập điểm đón/điểm đến/loại xe.
- Xem giá, gửi request, nhận thông báo trạng thái.
- Theo dõi vị trí tài xế (mô phỏng), animation mượt.
- Thanh toán tiền mặt (mô phỏng), đánh giá tài xế, xem lịch sử chuyến đi [7](https://www.moovrsoft.com/passenger-app).

### 14.2. Driver Side

- Đăng ký/đăng nhập, bật/tắt trạng thái.
- Nhận request phù hợp, accept/reject, cập nhật trạng thái chuyến.
- Xem lộ trình, theo dõi thu nhập, xem lịch sử.
- Giao diện đơn giản, tối ưu thao tác trên di động [25](https://help.optimoroute.com/hc/en-us/articles/32145620519572-Configure-driver-workflow-in-driver-app).

---

## 15. Admin System: Dashboard, CRUD, Báo Cáo

- **Đăng nhập quản trị**: Xác thực, phân quyền.
- **Quản lý hành khách/tài xế**: CRUD, duyệt giấy tờ, khóa/mở tài khoản.
- **Quản lý chuyến đi**: Xem, chỉnh sửa, hủy, giải quyết khiếu nại.
- **Báo cáo thống kê**: Số chuyến, doanh thu, tỷ lệ hủy, feedback.
- **Cấu hình giá cước**: Thay đổi base fare, surge, phí hủy, theo vùng, loại xe [4](https://drivemond.app/admin-panel/).

---

## 16. Tích Hợp Bản Đồ & Routing APIs

- **Google Maps, Mapbox, OSRM**: Tính toán tuyến đường, ETA, hiển thị bản đồ, marker, polyline [26](https://developers.google.com/maps/documentation/routes).
- **GTFS, GeoJSON**: Dữ liệu bản đồ, tuyến xe buýt, metro cho mô phỏng vị trí, hành trình [16](https://enlighteneddata.github.io/EDprofile/TransformVietnam.html).
- **GMap.NET**: Thư viện tích hợp bản đồ vào WinForms, hỗ trợ overlay, marker, animation [17](https://www.youtube.com/watch?v=to16P4N-rqg).

---

## 17. Ví Dụ Mã Nguồn & Dự Án Mô Phỏng Tham Khảo

- **jurajmajerik/rides**: Full-stack simulation, Node.js (simulation), Go (web server), PostgreSQL, React frontend, Prometheus/Grafana monitoring.
- **gowthy18/Real-Time-Ride-Matching-System**: Mô phỏng thuật toán ghép tài xế bằng Dijkstra, UI web, C++/JS, graph-based city map.
- **richxcame/ride-hailing**: Microservice backend, Go, PostgreSQL, Prometheus/Grafana, monitoring/observability mẫu [23](https://github.com/richxcame/ride-hailing/blob/main/monitoring/README.md).

---

## Kết Luận

Hệ thống đặt xe mô phỏng sử dụng kiến trúc Layered Architecture đơn giản, phù hợp với quy mô đồ án, tập trung vào thuật toán ghép tài xế, quản lý trạng thái, và giao tiếp cơ bản. Việc mô phỏng giúp kiểm thử logic nghiệp vụ, đào tạo và phát triển giải pháp vận tải.

Các vấn đề then chốt cần giải quyết bao gồm: thuật toán dispatch với lọc địa chỉ hành chính, xử lý race condition bằng `SemaphoreSlim`, tracking vị trí mô phỏng, lưu trữ dữ liệu bằng Newtonsoft.Json, và xây dựng UI/UX thân thiện cho hành khách, tài xế và admin.

---

**Tóm lại, một hệ thống đặt xe mô phỏng đơn giản cần hội tụ các yếu tố: kiến trúc layered, thuật toán matching, state management, và trải nghiệm người dùng tốt. Đây là nền tảng cho nghiên cứu và phát triển giải pháp vận tải.**

---

## 18. Educational Constraints

Yêu cầu
Mục đích giáo khoa
WinForms
Làm quen với Event-Driven Programming.
Sử dụng các Control như Timer (cho simulation), DataGridView (hiển thị danh sách), và sự kiện MouseClick trên Panel (giả lập bản đồ).
Serialization
Hiểu về Persistence & Data Stream.
Sử dụng **Newtonsoft.Json** (`JsonConvert.SerializeObject` / `JsonConvert.DeserializeObject<List<T>>`) để lưu dữ liệu xuống file `.json`. Cấu hình `TypeNameHandling.All` để bảo toàn tính đa hình khi `List<User>` chứa cả `Driver` lẫn `Passenger`.
Không LINQ
Nắm vững cấu trúc dữ liệu và giải thuật.
Thay vì drivers.Where(...), bạn sẽ viết hàm lọc tài xế bằng vòng lặp foreach kết hợp với cấu trúc if-else truyền thống.
Không Lambda
Hiểu rõ cơ chế Delegate và Method.
Thay vì (x => x.ID), bạn phải khai báo các phương thức tường minh hoặc sử dụng các vòng lặp để truy xuất thuộc tính của đối tượng.
Không var
Rèn luyện tư duy kiểu dữ liệu (Static Typing).
Mọi biến phải được khai báo rõ ràng, ví dụ: Passenger customer = new Passenger(); thay vì var customer = ....

---

## 8. Workflows (End-to-end flow orchestration)

- **Trip Request Workflow**: Passenger UI → `ITripService.RequestTrip(passengerId, pickup, dest, vehicleType)` → `new Trip(...)` → `trip.SetSearching()` → `TripRepository.Add(trip)` → `TripRepository.SaveChangesAsync()` → Return `Trip`.
- **Driver Assignment Workflow**: Driver UI → `ITripService.MatchDriverAsync(tripId, driverId)` → Check `trip.Status == Searching` + `driver.Status == Available` → `trip.MatchDriver(driverId)` + `driver.SetOnTrip()` → Update repositories → Save changes → Emit `TripMatchedEvent`.
- **Trip Completion Workflow**: Driver UI → `ITripService.CompleteTrip(tripId, fareAmount)` → `trip.CompleteTrip()` → `trip.ConfirmPayment()` → `driver.PayCommission(fare)` + `driver.SetAvailable()` + `passenger.AddTrip()` → Update repositories → Save changes → Emit `TripCompletedEvent` + `TripPaidEvent`.
- **Rating Workflow**: Passenger UI → `IReviewService.AddReviewAsync(driverId, passengerId, tripId, rating, comment)` → `new Review(...)` → `driver.UpdateReviews(rating)` → Update repositories → Save changes → Emit `ReviewCreatedEvent`.

## 9. Dependency Mapping (Application → Domain interaction)

> **Lưu ý:** Hệ thống không sử dụng Dependency Injection container. Các dependency được khởi tạo bằng `new` trực tiếp hoặc truyền qua constructor do code tự quản lý (manual composition).

- Application Services nhận Repository interfaces qua constructor (manual pass, không dùng DI container).
- Services điều phối Domain objects: Gọi entity methods, modify aggregates, handle events.
- Domain dependencies: Entities (`Trip`, `Driver`), `FareRule.CalculateFare(double)`, Repository interfaces (`ITripRepository`, `IDriverRepository`).
- Flow: UI Event Handler → Application Service (interface) → Domain Entity / State Machine → Repository (interface) → Infrastructure Implementation (`JsonRepository<T>`).
