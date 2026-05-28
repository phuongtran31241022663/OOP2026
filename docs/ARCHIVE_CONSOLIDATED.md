# RideGo Archive Consolidated Documentation

This file contains consolidated information from archived audit reports and UI specifications.

---

# FINAL_DOC (gộp) — RideGo Architecture & Technical Notes

> File này gộp nội dung từ 3 tài liệu audit/quality: 
> - `docs/ARCHITECTURAL_CRITIQUE.md`
> - `docs/CODE_QUALITY_REPORT.md`
> - `docs/RideGo_ArchitectureAudit.md`
>
> `OOP2026/REPORT.md` vẫn là tài liệu chính báo cáo đồ án.

---

## 1) Architecture Critique (Boundary / State / Transactions)

### 1.1 Application layer phụ thuộc Infrastructure
- **Evidence:** `Application.csproj` có reference trực tiếp sang `Infrastructure`.
- **Risk:** Vi phạm Dependency Inversion; khó thay persistence JSON bằng storage khác (SQL/Mongo) mà không phải sửa service nghiệp vụ.

### 1.2 Domain layer bị rò rỉ quan tâm persistence
- **Evidence:** Domain entities implement `ISerializable` / có `[Serializable]`.
- **Risk:** Model nghiệp vụ bị trộn với concerns serialize/deserialize; thay đổi format lưu trữ có thể buộc thay đổi Domain.

### 1.3 Dual state machine & đồng bộ vòng đời
- **Evidence:** `Trip` và `Driver` có state machine riêng.
- **Risk:** Nếu crash xảy ra giữa các bước chuyển state (ví dụ Trip đã chuyển `Started` nhưng Driver chưa chuyển `OnTrip`) có thể sinh trạng thái không đồng nhất và cho phép assign nhầm.

### 1.4 Transaction ambiguity & concurrency rủi ro
- **Evidence:** Mỗi repository có file lock độc lập.
- **Risk:** Hoàn tất trip cần cập nhật nhiều file (ví dụ `trips.json` + `users.json`) nhưng không có transaction toàn cục; crash ở giữa có thể để dữ liệu “dở”.

### 1.5 Security: mật khẩu lưu plain-text
- **Evidence:** So sánh chuỗi trực tiếp trong `User.VerifyPassword`.
- **Risk:** Nếu `users.json` bị lộ/đọc trái phép thì lộ toàn bộ credentials.

---

## 2) RideGo Architecture Audit (OOP Map / Patterns)

### 2.1 Bản đồ quan hệ OOP (theo thực tế triển khai)
- **Inheritance:** `User` (abstract) ← `Passenger`, `Driver`, `Admin`; `Vehicle` (abstract) ← `Car`, `Motorbike`.
- **Composition:** `Trip` sở hữu `Route` và `Fare`; `Location` sở hữu `Coordinate` và `Address`.
- **Aggregation:** `Driver` liên kết `Vehicle` qua `VehicleId`; `Trip` liên kết passenger/driver qua ID.

### 2.2 Mẫu thiết kế (Design Patterns)
- **State Pattern:**
  - `Trip` qua `ITripState`.
  - `Driver` qua `IDriverState`.
- **Factory Pattern:** `UserFactory`, `VehicleFactory` để chuẩn hóa việc tạo đối tượng.
- **Repository Pattern:** `JsonRepository<T>` làm nền persistence file; repository cụ thể hỗ trợ truy vấn theo nghiệp vụ.

### 2.3 Vấn đề nổi bật
- **Phụ thuộc chéo:** Presentation có thể gọi trực tiếp repository thay vì đi qua service.
- **Hiệu năng:** lưu JSON toàn bộ danh sách mỗi lần cập nhật có thể giảm hiệu năng khi dữ liệu lớn.

---

## 3) Code Quality Report (Technical Debt & Recommendations)

### 3.1 Tổng quan
- Nền tảng có cấu trúc rõ ràng, áp dụng tốt các nguyên lý/mẫu OOP.
- Tuy nhiên mức chất lượng tổng thể chỉ ở mức trung bình–khá; còn nhiều technical debt ảnh hưởng khả năng bảo trì và mở rộng.

### 3.2 Technical Debt (liệt kê các điểm chính)
1. **Bảo mật:** mật khẩu plain-text.
2. **Giao dịch/transaction:** thiếu Unit of Work hoặc cơ chế transaction liên repository.
3. **Dispose không chuẩn:** cần rà soát vòng đời dispose cho lớp giao diện/tài nguyên.
4. **Wrapper trong Service:** có một số phương thức chủ yếu chuyển tiếp lời gọi.
5. **Presentation coupling:** UI có thể truy cập repository/infrastructure trực tiếp hoặc tạo logic trùng.
6. **Timer/Event risk:** nguy cơ rò rỉ do event subscription khi control bị dispose không đúng.
7. **Exception handling chưa nhất quán:** bắt lỗi quá rộng/nuốt lỗi/hiển thị chung chung.
8. **Quản lý tài nguyên:** cần rà soát tài nguyên control động, repository I/O, thành phần bản đồ, async components.

### 3.3 Kết luận
- Hệ thống đủ tốt để minh họa luồng nghiệp vụ trong phạm vi học phần.
- Với yêu cầu gần thực tế, cần ưu tiên các hạng mục: bảo mật, transaction, event lifecycle/Dispose, giảm coupling giữa Presentation–Application và chuẩn hóa exception handling.

---

# ĐẶC TẢ GIAO DIỆN ỨNG DỤNG OOP DEMO

## 1. TỔNG QUAN HỆ THỐNG & BẢNG MÀU VAI TRÒ

Hệ thống mô phỏng nền tảng đặt xe, hỗ trợ đa vai trò: **Hành khách**, **Tài xế** và **Quản trị viên**.  
Toàn bộ giao diện được đặt trên nền bản đồ trung tâm, chia thành các cột/hộp thoại tương ứng với từng phiên làm việc.

| Vai trò               | Màu chủ đạo | Màu nhấn phụ    |
| --------------------- | ----------- | --------------- |
| Hành khách            | Xanh lá cây | Xanh lá nhạt    |
| Tài xế                | Cam         | Cam nhạt        |
| Quản trị viên (Admin) | Xanh dương  | Tím             |
| Xe ô tô (dịch vụ)     | Xanh dương  | Viền xanh dương |
| Xe máy (dịch vụ)      | Cam         | Viền cam        |

Các màu trạng thái cố định:

- **Điểm đón**: Xanh lá cây (vòng tròn).
- **Điểm đến**: Đỏ (vòng tròn).
- **Trạng thái hoàn thành**: Xanh lá nhạt, chữ xanh lá.
- **Trạng thái hủy**: Đỏ nhạt, chữ đỏ.
- **Nút chính hành động**: Xanh lá (với khách), Cam (với tài xế), Tím (với admin).

---

## 2. THANH TIÊU ĐỀ CHUNG (HEADER BAR)

Thanh ngang trên cùng toàn ứng dụng, nền **xanh đen đậm**.

- **Bên trái**: Tên ứng dụng **OOP Demo**.
- **Trình chuyển đổi vai trò (Role Switcher)**:
  - Hai thẻ đại diện: **Nguyễn Thị Lan** (Hành khách, tag xanh lá) và **Trần Văn Tài** (Tài xế, tag vàng cam).
  - Nút **Đổi nhanh** cho phép chuyển đổi tài khoản đang hoạt động.
  - Trạng thái kết nối hiển thị bên dưới tên tài xế hoặc dòng trạng thái nhỏ: _“Chuyến đi đang chạy: matched”_ hoặc _“Trần Văn Tài - Đang chạy”_ khi có chuyến.
- **Góc phải**:
  - Hiển thị số lượng **tài xế online** (dạng số).
  - Nút **Admin** nổi bật: nền tím, bo tròn dạng viên thuốc, bên trong chứa icon khiên 🛡️ và chữ **Admin** màu trắng. Bấm vào mở cửa sổ quản trị.

---

## 3. CỬA SỔ ĐĂNG NHẬP & ĐĂNG KÝ

Khi bấm vào nút đổi tài khoản hoặc truy cập lần đầu, cửa sổ pop-up hiện ra cho phép đăng nhập hoặc đăng ký. Có hai giao diện riêng biệt cho Hành khách và Tài xế.

### 3.1. Hành khách

**Đăng nhập (Hành khách)**:

- **Header**: Nền xanh lá đậm, icon người màu tím bên trái. Dòng “Hành khách — Đăng nhập” (trắng to), dưới là “Demo System” (trắng nhỏ). Góc phải có dấu **X** trắng để đóng.
- **Tab chuyển đổi**: “Đăng nhập” (viền xanh lá) | “Đăng ký” (chữ xám).
- **Khung tài khoản demo**: Nền xanh lá nhạt, chữ “Tài khoản demo:”. Bên trong hiển thị “Hành khách demo” cùng SĐT **0911111111 / 123456** và dấu mũi tên > để điền nhanh.
- **Form nhập**:
  - _Số điện thoại_: icon điện thoại, placeholder 09xxxxxxxx.
  - _Mật khẩu_: icon ổ khóa, hiển thị dấu chấm.
- **Nút “Đăng nhập”**: Hình chữ nhật bo góc lớn, màu xanh lá đậm, chữ trắng.

**Đăng ký (Hành khách)**:

- **Header**: “Hành khách — Đăng ký” (nền xanh lá).
- **Tab**: “Đăng ký” được chọn (viền xanh lá).
- **Form nhập**:
  - _Họ và tên_ (icon người, placeholder Nguyễn Văn A)
  - _Số điện thoại_ (icon điện thoại)
  - _Mật khẩu_ (icon ổ khóa)
- **Nút “Đăng ký”**: Màu xanh lá đậm toàn bộ, chữ trắng.

### 3.2. Tài xế

**Đăng nhập (Tài xế)**:

- **Header**: Nền cam. Biểu tượng ô tô nhỏ đỏ góc trái. Dòng “Tài xế — Đăng nhập” và “Demo System” (trắng). Dấu X trắng.
- **Tab**: “Đăng nhập” (viền cam) | “Đăng ký” (xám).
- **Khung demo**: Nền vàng nhạt, “Tài khoản demo:” – “Tài xế demo” **0900000000 / 123456** và nút >.
- **Form**: Số điện thoại, Mật khẩu giống khách.
- **Nút “Đăng nhập”**: Cam đậm, chữ trắng.

**Đăng ký (Tài xế)**:

- **Header**: Nền cam, “Tài xế — Đăng ký”.
- **Tab**: “Đăng ký” được chọn (viền cam).
- **Form**:
  - Họ tên, Số điện thoại, Mật khẩu.
  - **Số GPLX**: icon tờ giấy, placeholder GPLX-XXXXXX.
  - **Loại xe**: Hai nút chọn Ô tô (icon xe hơi đỏ) và Xe máy (icon xe máy). Nút đang chọn có viền cam, nền vàng nhạt.
  - _Biển số_ (placeholder 51A-12345)
  - _Hãng xe_ (Honda)
  - _Mẫu xe_ (Wave)
  - _Màu xe_: dropdown, mặc định “Trắng”.
- **Nút “Đăng ký”**: Cam đậm to, chữ trắng.

---

## 4. GIAO DIỆN HÀNH KHÁCH

Sau khi đăng nhập, giao diện chính của Hành khách bao gồm:

### 4.1. Thanh tiêu đề (Hành khách)

- Nền trắng / trong suốt phía trên.
- **Tên tài khoản**: chữ trắng (trên nền tối của header chung), cỡ chữ lớn.
- **Số điện thoại**: hiển thị ngay dưới tên.
- **Số chuyến đã đi**: hiển thị phía dưới số điện thoại.

### 4.2. Thanh menu điều hướng (4 tab)

Nằm ngang, các tab có icon và chữ:

- **Tab 1** – _Đặt xe_: icon ô tô.
- **Tab 2** – _Chuyến đi_: (trạng thái chuyến hiện tại).
- **Tab 3** – _Lịch sử_: (không mô tả chi tiết).
- **Tab 4** – _Hồ sơ_: icon người.
- Tab được chọn có đường gạch chân màu xanh lá.

### 4.3. Tab 1 – Đặt xe (trạng thái bình thường)

**Khung nhập lộ trình**:

- **Điểm đón**:
  - Vòng tròn màu xanh lá đầu dòng.
  - Địa chỉ hiển thị (hoặc placeholder).
  - Bên phải: nếu có địa chỉ thì hiện dấu **X** để xóa; nếu trống hiện biểu tượng **định vị mục tiêu**.
- **Điểm đến**:
  - Vòng tròn màu đỏ.
  - Địa chỉ hiển thị.
  - Bên phải: dấu X và biểu tượng mũi tên (tương tự).
- **Thời gian di chuyển dự kiến**: biểu tượng đồng hồ kèm chữ “X phút” (dựa trên quãng đường ước tính).

**Khung lựa chọn phương tiện**:

Hai ô hình chữ nhật bo góc, nằm song song:

- **Xe ô tô** (bên trái):
  - Trạng thái chưa chọn → không viền đậm.
  - Icon ô tô, chữ “Xe ô tô”.
  - Giá: **55.000đ** chữ xanh lá.
- **Xe máy** (bên phải):
  - Được chọn mặc định → viền xanh lá đậm, nền xanh lá nhạt.
  - Icon xe máy, chữ “Xe máy”.
  - Giá: **33.000đ** chữ xanh lá.

**Nút hành động**:

- Nút lớn hình chữ nhật bo góc dài, nền **xanh lá cây** toàn bộ.
- Ở giữa: icon ô tô nhỏ màu vàng và chữ trắng **Đặt chuyến**.

### 4.4. Tab 1 – Khi đã có chuyến hoạt động

- Xuất hiện ô chữ nhật bo góc, viền và nền **vàng nhạt**.
- Cảnh báo: _“Bạn đang có chuyến đang hoạt động. Hoàn thành hoặc hủy trước khi đặt chuyến mới.”_
- Nút **Đặt chuyến** chuyển thành màu **xám đục**, không bấm được (trạng thái khóa).

### 4.5. Tab 2 – Trạng thái tìm tài xế (khi vừa đặt chuyến)

- **Thanh thông báo trạng thái trên cùng**: Biểu tượng chữ “i” trong vòng tròn xanh lam. Nội dung: _“Chuyến đang chạy: searching · 1 cuốc đang đề nghị cho tài xế”_.
- **Tiến trình tìm kiếm**:
  - Dòng chữ đậm màu xanh đậm: **“Đang tìm tài xế...”**.
  - Bên dưới là thanh tiến trình dạng vạch chạy (animation).
  - Góc phải hiển thị dịch vụ đã chọn (Ô tô / Xe máy).
- **Chi tiết lộ trình**:
  - Điểm đón (vòng xanh lá) – Tên địa điểm.
  - Điểm đến (vòng đỏ) – Tên địa điểm.
- **Thông tin cước phí**:
  - Cước phí ước tính: chữ lớn, in đậm màu xanh đen.
  - Quãng đường (km).
  - Thời gian dự kiến (phút).
- **Nút “Hủy chuyến”**: Nền trắng, viền đỏ, chữ đỏ. Nằm dưới cùng.

### 4.6. Tab 4 – Hồ sơ (Hành khách)

- Hiển thị **tên**, **số điện thoại**, **tổng số chuyến đã đi**.
- Nút **Chỉnh sửa thông tin** (icon bút chì).
- **Form đổi mật khẩu**:
  - Ô nhập _Mật khẩu cũ_.
  - Ô nhập _Mật khẩu mới_.
  - Nút **Đổi mật khẩu** lớn, màu xanh đen.

---

## 5. GIAO DIỆN TÀI XẾ

Sau khi đăng nhập tài xế, giao diện chia làm hai phần chính: **Phần thông tin tài xế** (cố định) và **Thanh tab chức năng**.

### 5.1. Phần thông tin tài xế (Khung màu cam trên cùng)

- Nền cam, bo góc dưới nhẹ.
- **Ảnh đại diện**: icon người trắng trong vòng tròn cam nhạt.
- **Họ và tên** (trắng, lớn).
- **Số điện thoại**.
- **Trạng thái Online**: nút bo góc nhỏ, nền trắng chữ xanh/cam?, có icon nút nguồn.
- **Đánh giá**: 4.8 ⭐ (sao vàng).
- **Số chuyến đã chạy**.
- **Dòng xe đăng ký** (VD: Honda Wave).

### 5.2. Thanh tab điều hướng (Tài xế)

Gồm 5 tab, nằm ngang, dưới phần thông tin:

- **Tab 1 – Trạng thái**: icon nút nguồn.
- **Tab 2 – Cuốc**: icon vô lăng ô tô.
- **Tab 3 – Ví**: icon chiếc ví.
- **Tab 4 – Lịch sử**: icon đồng hồ ngược.
- **Tab 5 – Hồ sơ**: icon hình người.
- Tab đang chọn có gạch chân màu cam.

### 5.3. Tab 1 – Trạng thái & Số dư

- Khu vực nền trắng, bên dưới header cam.
- **Số dư ví**: Khối bo góc màu cam lớn, số tiền chữ trắng, cỡ chữ lớn.
- Hai thông số nhỏ bên dưới, nằm ngang:
  - **Tổng thu nhập** (bên trái).
  - **Tổng chuyến** (bên phải).

### 5.4. Tab 3 – Ví (Nạp tiền)

- Tiêu đề: **+ Nạp tiền vào ví**.
- **Các mức gợi ý nhanh**: 3 nút bấm:
  - 50.000đ (khung xám)
  - 100.000đ (khung xám)
  - 200.000đ (đang chọn → viền cam, nền vàng nhạt)
- Ô nhập số tiền thủ công (placeholder).
- Nút **Nạp tiền** lớn, màu cam, chữ trắng ở đáy màn hình.

### 5.5. Tab 5 – Hồ sơ & Nhận chuyến

Tab này gồm hai vùng chính:

#### a) Thông tin tài xế

- Họ tên, số điện thoại, dòng xe...
- Nút **Chỉnh sửa thông tin** (icon bút chì).
- Form **Đổi mật khẩu** tương tự khách hàng.

#### b) Danh sách chuyến được đề nghị

- Tiêu đề: **Chuyến được đề nghị ({số lượng})**.
- Mỗi đơn hàng hiển thị trong một khung bo góc riêng biệt:
  - **Dịch vụ**: biểu tượng xe kèm chữ trong khung xanh dương nhỏ (VD: “Xe ô tô”).
  - **Số tiền tài xế nhận**: góc phải, chữ to màu cam đậm, VD: **45.000đ** (sau chiết khấu).
  - **Lộ trình**:
    - Điểm đón: vòng xanh lá – tên địa điểm.
    - Điểm đến: vòng đỏ – tên địa điểm.
  - Thông số chuyến đi: quãng đường (km), thời gian dự kiến.
  - **Tổng cước khách trả**: chữ xám nhỏ hơn, VD: _Cước: 55.000đ_.
- **Cặp nút hành động** dưới mỗi đơn:
  - Trái: **Từ chối** – chữ đỏ, biểu tượng dấu gạch chéo đỏ.
  - Phải: **Nhận cuốc** – nền cam, chữ trắng, biểu tượng dấu tích.

---

## 6. GIAO DIỆN QUẢN TRỊ (ADMIN)

Cửa sổ pop-up (chỉ có nút X, không phóng to/thu nhỏ). Nền chung trắng, các thành phần màu tím chủ đạo.

### 6.1. Thanh tiêu đề & Menu

- **Header**: Nền xanh dương đậm? (Mô tả gốc: “thanh tiêu đề có màu xanh dương”). Góc trái: “Quản trị hệ thống”, dòng nhỏ “Quản trị viên”. Góc phải: dấu X.
- **Menu điều hướng** (4 tab ngang), tab đang chọn có chữ highlight và gạch dưới màu tím:
  - **Tab 1**: icon biểu đồ – _Thống kê_
  - **Tab 2**: icon hai người – _Người dùng_
  - **Tab 3**: icon ô tô – _Chuyến đi_
  - **Tab 4**: icon bánh răng – _Biểu phí_

### 6.2. Tab 1 – Thống kê

- **4 thẻ chỉ số** nằm ngang, mỗi thẻ nền viền nhạt, số liệu in đậm:
  - _Tổng doanh thu_ (nền xanh lá nhạt, chữ xanh lá) – kèm chú thích “từ chuyến hoàn thành”.
  - _Hoa hồng_ (nền xanh dương nhạt, chữ xanh dương) – “doanh thu Hệ thống”.
  - _Tổng chuyến_ (nền xanh biển nhạt, chữ xanh biển) – “số lượng hoàn thành · số lượng hủy”.
  - _Tỷ lệ hoàn thành_ (nền vàng nhạt, chữ cam) – “X/X chuyến”.
- **Hai cột bên dưới**:
  - Cột trái – _Người dùng_: tổng số tài xế (kèm số online), tổng hành khách, tổng admin.
  - Cột phải – _Trạng thái chuyến_: liệt kê số lượng “Hoàn thành”, “Hủy”, ...

### 6.3. Tab 2 – Người dùng

- Tiêu đề: **Tất cả người dùng ({Số lượng})**.
- Danh sách thẻ người dùng: avatar chữ cái đầu, tên, số điện thoại, vai trò (tag). Với tài xế, có thêm trạng thái _Online/Offline_.
- Khi bấm mở rộng (dấu mũi tên) chi tiết một tài xế, hiển thị:
  - ID tài xế.
  - Ngày đăng ký.
  - Số GPLX.
  - Số dư ví.
  - Tổng thu nhập tích lũy.
  - Số chuyến đã thực hiện.
  - Điểm đánh giá (⭐ 4.8).

### 6.4. Tab 3 – Chuyến đi

- Tiêu đề: **Tất cả chuyến đi ({Số lượng})**.
- Mỗi thẻ chuyến đi gồm:
  - _Hàng trên_: biểu tượng ô tô tròn xanh lá, lộ trình in đậm (VD: Quận 10 → Quận 5), tên khách, ngày tháng. Góc phải: tổng tiền (55.000đ) và nhãn trạng thái nền xanh lá nhạt (Hoàn thành).
  - _Khi click mở rộng_:
    - ID chuyến.
    - Hành khách (tên + SĐT).
    - Tài xế (tên + SĐT).
    - Khoảng cách, thời gian.
    - Chi tiết giá: Phí gốc, Phí km.
    - Phân bổ dòng tiền: Thu nhập tài xế, Hoa hồng hệ thống.

### 6.5. Tab 4 – Biểu phí

- **Hai thẻ hiển thị biểu phí hiện tại** (load từ server):
  - **Xe ô tô**: nền xanh dương nhạt, viền xanh dương. Hiển thị: Phí mở cửa, Giá/km, Hoa hồng %.
  - **Xe máy**: nền vàng nhạt, viền vàng. Thông tin tương tự.
- **Form cập nhật biểu phí**:
  - Dropdown chọn _Loại xe_ (Ô tô / Xe máy).
  - Các ô nhập: _Phí mở cửa (đ)_, _Giá/km (đ)_, _Hoa hồng (%)_.
  - Nút **Thêm biểu phí** lớn, màu tím, chữ trắng.
