# Core Flows Sequence Diagrams

Tài liệu này mô tả các luồng nghiệp vụ chính của dự án RideGo thông qua các sơ đồ tuần tự (Sequence Diagrams).

## 1. Luồng đặt xe và hoàn thành (Core Flow)

```mermaid
sequenceDiagram
    participant P as Hành khách
    participant S as Hệ thống
    participant D as Tài xế


    P->>S: Tạo yêu cầu đặt xe
    S->>S: Tìm tài xế phù hợp
    S->>D: Thông báo yêu cầu
    D-->>S: Xác nhận nhận chuyến
    S-->>P: Thông báo tài xế đã nhận chuyến
   
    D->>S: Xác nhận đã đến điểm đón
    S-->>P: Thông báo tài xế đã đến điểm đón
   
    D->>S: Xác nhận bắt đầu chuyến
    D->>S: Xác nhận đã đến điểm trả
    D->>S: Xác nhận thanh toán
   
    S->>P: Gửi lời nhắc đánh giá
   
    opt Đánh giá chuyến đi
        P->>S: Gửi đánh giá
    end
```

## 2. Luồng Timeout và Từ chối

```mermaid
sequenceDiagram
    participant P as Hành khách
    participant S as Hệ thống
    participant D as Danh sách Tài xế


    P->>S: Tạo yêu cầu đặt xe
    S->>S: Tìm tài xế phù hợp
   
    Note over S: Trạng thái: Đang tìm tài xế
    S->>D: Thông báo yêu cầu
   
    alt Không có tài xế nào nhận chuyến (Timeout)
        Note over S: Hết thời gian chờ (e.g., 3 phút)
        S->>S: Hủy yêu cầu đặt xe
        S-->>P: Thông báo: "Không tìm thấy tài xế, vui lòng thử lại"
    else Tài xế từ chối lượt gọi
        D-->>S: Từ chối nhận chuyến
        S->>S: Chuyển hướng tìm tài xế tiếp theo
    end
```

## 3. Luồng Quản trị (Admin)

```mermaid
sequenceDiagram
    participant A as Quản trị viên (Admin)
    participant S as Hệ thống
    participant DB as Cơ sở dữ liệu


    A->>S: Đăng nhập vào bảng điều khiển
    S-->>A: Hiển thị danh sách chuyến đi & Thống kê doanh thu
   
    A->>S: Yêu cầu điều chỉnh chính sách giá cước
    S->>S: Kiểm tra tính hợp lệ của tham số cấu hình mới
   
    S->>DB: Lưu thông tin cấu hình giá cước mới
    DB-->>S: Xác nhận lưu trữ thành công
   
    S-->>A: Thông báo: "Cấu hình giá cước đã được cập nhật thành công"
    Note over S, DB: Các chuyến đi tạo từ thời điểm này sẽ áp dụng giá cước mới
```

## 4. Luồng Hủy chuyến

```mermaid
sequenceDiagram
    participant P as Hành khách
    participant S as Hệ thống
    participant D as Tài xế


    P->>S: Nhấn "Hủy chuyến"
   
    alt Trạng thái: Đang tìm tài xế
        S->>S: Dừng tiến trình quét xe trong hàng đợi
        S->>S: Cập nhật trạng thái chuyến đi: [Đã hủy]
        S-->>P: Xác nhận hủy chuyến thành công
       
    else Trạng thái: Đã ghép chuyến (Tài xế ĐANG DI CHUYỂN đến điểm đón)
        S->>S: Cập nhật trạng thái chuyến đi: [Đã hủy]
        S->>S: Chuyển trạng thái Tài xế: [Sẵn sàng]
        S->>D: Thông báo: "Chuyến đi đã bị hủy bởi Hành khách"
        S-->>P: Xác nhận hủy chuyến thành công
    end