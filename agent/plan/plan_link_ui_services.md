# Kế hoạch cho Liên kết UI và Service

**Ngày:** 2026-05-26
**Người dùng:** PC
**Mục tiêu:** Liên kết các UserControl với Service và refactor ucTripCard để hoàn thiện luồng nghiệp vụ.

## Các bước

- [ ] Bước 1: Implement logic cho [`ucMap.cs`](OOP2026/UserControl/ucMap.cs) để hỗ trợ sự kiện `LocationSelected` và hiển thị marker – file: [`OOP2026/UserControl/ucMap.cs`](OOP2026/UserControl/ucMap.cs) – hành động: ghi
- [ ] Bước 2: Refactor [`ucTripCard.cs`](OOP2026/UserControl/ucTripCard.cs) để hỗ trợ 3 chế độ hiển thị (Passenger, DriverRequest, DriverActive) – file: [`OOP2026/UserControl/ucTripCard.cs`](OOP2026/UserControl/ucTripCard.cs) – hành động: ghi
- [ ] Bước 3: Cập nhật [`ucBooking.cs`](OOP2026/UserControl/ucBooking.cs) để đồng bộ hóa việc chọn địa điểm giữa [`ucLocationPicker`](OOP2026/UserControl/ucLocationPicker.cs) và [`ucMap`](OOP2026/UserControl/ucMap.cs) – file: [`OOP2026/UserControl/ucBooking.cs`](OOP2026/UserControl/ucBooking.cs) – hành động: ghi
- [ ] Bước 4: Tích hợp [`ucTripStatus.cs`](OOP2026/UserControl/ucTripStatus.cs) với [`ITripQueryService`](OOP2026/Interface.cs) để tự động cập nhật trạng thái chuyến đi – file: [`OOP2026/UserControl/ucTripStatus.cs`](OOP2026/UserControl/ucTripStatus.cs) – hành động: ghi
- [ ] Bước 5: Tích hợp [`ucRequest.cs`](OOP2026/UserControl/ucRequest.cs) với [`ITripCommandService`](OOP2026/Interface.cs) và [`ITripQueryService`](OOP2026/Interface.cs) để xử lý nhận/từ chối chuyến – file: [`OOP2026/UserControl/ucRequest.cs`](OOP2026/UserControl/ucRequest.cs) – hành động: ghi
- [ ] Bước 6: Kiểm tra và hoàn thiện liên kết cho [`ucWallet.cs`](OOP2026/UserControl/ucWallet.cs) và [`ucProfile.cs`](OOP2026/UserControl/ucProfile.cs) – file: [`OOP2026/UserControl/ucWallet.cs`](OOP2026/UserControl/ucWallet.cs), [`OOP2026/UserControl/ucProfile.cs`](OOP2026/UserControl/ucProfile.cs) – hành động: ghi
- [ ] Bước 7: Liên kết [`ucDriverCard.cs`](OOP2026/UserControl/ucDriverCard.cs) để hiển thị thông tin tài xế đang thực hiện chuyến cho hành khách – file: [`OOP2026/UserControl/ucDriverCard.cs`](OOP2026/UserControl/ucDriverCard.cs) – hành động: ghi

## Kiểm chứng

- Test: Chạy ứng dụng, thực hiện luồng đặt xe từ phía khách và nhận xe từ phía tài xế.
- Lint: Không.
- Build: Có, sử dụng `dotnet build`.

## Rủi ro

- Xung đột sự kiện (Event Race Condition) khi nhiều UserControl cùng lắng nghe một Service (Thấp).
- Lỗi hiển thị trên bản đồ khi tọa độ không hợp lệ (Trung bình).

## Nguồn tham khảo

- Mã nguồn hiện tại của dự án.
- Thư viện GMap.NET.
