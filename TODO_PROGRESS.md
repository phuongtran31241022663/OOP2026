# TODO Progress - TripStatus Cleanup + Driver State Pattern

## Phase 1: Dọn dẹp TripStatus enum
- [ ] 1.1 `Presentation/Components/TripStatusPanel.cs` — Đổi `UpdateTripStatus(TripStatus)` → `UpdateTripStatus(string)`
- [ ] 1.2 `Domain/Enums/TripStatus.cs` — Xóa file
- [ ] 1.3 `Domain/States/*.cs` (8 files) — Xóa `using Domain.Enums;` thừa
- [ ] 1.4 `Application/Events/TripStatusChangedEventArgs.cs` — Xóa `using Domain.Enums;` thừa
- [ ] 1.5 Các file khác còn `using Domain.Enums;` chỉ vì TripStatus — dọn dẹp

## Phase 2: Chuyển Driver sang State Pattern
- [ ] 2.1 `Domain/States/IDriverState.cs` — Tạo mới
- [ ] 2.2 `Domain/States/Drivers/DriverOfflineState.cs` — Tạo mới
- [ ] 2.3 `Domain/States/Drivers/DriverAvailableState.cs` — Tạo mới
- [ ] 2.4 `Domain/States/Drivers/DriverOnTripState.cs` — Tạo mới
- [ ] 2.5 `Domain/Entities/Users/Driver.cs` — Refactor sang State Pattern
- [ ] 2.6 `Domain/Events/DriverStatusChangedEvent.cs` — Đổi DriverStatus → string
- [ ] 2.7 `Domain/StateMachines/DriverStateMachine.cs` — Xóa file
- [ ] 2.8 `Application/Interfaces/IUserService.cs` — Đổi signature UpdateDriverStatusAsync
- [ ] 2.9 `Application/Services/UserService.cs` — Đổi switch sang string
- [ ] 2.10 `Presentation/Shells/DriverShell.cs` — Dùng string status
- [ ] 2.11 `Presentation/Components/DriverCardControl.cs` — Dùng string status
- [ ] 2.12 `Presentation/Shells/AdminShell.cs` — Dùng string status / IsXxx()

## Phase 3: Build & Verify
- [ ] 3.1 Build solution OOP2026.slnx
- [ ] 3.2 Fix compile errors

## Phase 4: Cập nhật tài liệu
- [ ] 4.1 `TODO.md` — Đánh dấu hoàn thành
- [ ] 4.2 `SOURCE_MAP.md` — Cập nhật Driver mô tả

