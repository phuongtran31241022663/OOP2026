# Plan for RideGo WinForms UI Refactor

**Date:** 2026-05-26
**Goal:** Refactor the WinForms UI to match the provided high-fidelity screenshots, ensuring a multi-role experience (Passenger, Driver, Admin) while adhering to strict UI coding standards.

## 🛠 UI Coding Standards (Mandatory)
- **`.Designer.cs` Purity**: No business logic or event handlers. Only `InitializeComponent`.
- **Centralized Styling**: Use `OOP2026.Colors` and `OOP2026.Typography` from `UIHelper.cs`.
- **Dynamic Layout**: Use `TableLayoutPanel`, `FlowLayoutPanel`, `Dock`, and `Anchor`.
- **Naming**: `btn` (Button), `lbl` (Label), `txt` (TextBox), `pnl` (Panel), `uc` (UserControl), `frm` (Form).

---

## 📋 Implementation Phases

### Phase 1: Main Shell (`FrmMultiRole`)
**Goal:** Establish the primary layout and navigation.
- [ ] **Header Bar**: 
    - Implement a dark top bar with:
        - Role Switcher cards (Passenger/Driver) with "Đổi" (Change) links.
        - Online driver count label (`lblDriverCount`).
        - Admin button (`btnAdmin`) with a shield icon.
- [ ] **Three-Column Layout**:
    - Left Column: Role-specific home control (`ucPassengerHome` or `ucDriverHome`).
    - Center Column: Map control (`ucMap`).
    - Right Column: Contextual details (Driver info for passenger, Passenger info for driver).
- [ ] **Role Switching Logic**: Ensure `btnSwitchRole` triggers `FrmAuth` and refreshes the shell.

### Phase 2: Authentication Flow (`FrmAuth`)
**Goal:** Implement role-specific login and registration.
- [ ] **Auth Shell**:
    - Role-colored header (Green for Passenger, Orange for Driver).
    - Login/Register toggle tabs.
    - Close button (X) in the top right.
- [ ] **Passenger Auth**:
    - Login: Phone, Password.
    - Register: Full Name, Phone, Password.
    - Demo account shortcut.
- [ ] **Driver Auth**:
    - Login: Phone, Password.
    - Register: Full Name, Phone, Password, License Number, Vehicle Type (Car/Bike), Plate, Brand, Model, Color.
    - Demo account shortcut.

### Phase 3: Passenger Experience (`ucPassengerHome`)
**Goal:** Implement the passenger's journey from booking to history.
- [ ] **Home Shell**:
    - User header (Name, Phone, Total Trips).
    - Tab Navigation: "Đặt xe", "Chuyến", "Lịch sử", "Hồ sơ" with active underline.
- [ ] **Booking Tab (`ucBooking`)**:
    - Pickup/Dropoff input fields with location icons.
    - Vehicle selector (Car vs Bike) with price and capacity.
    - Estimated distance/time display.
    - "Đặt chuyến" button.
- [ ] **Trip Status Tab (`ucTripStatus`)**:
    - Progress bar for trip stages (Searching -> Matched -> Arrived -> Started -> Dropped Off).
    - Trip summary card (Route, Fare, Driver info).
    - "Hủy chuyến" (Cancel) button.
- [ ] **History Tab (`ucHistory`)**:
    - List of trip cards (Status, Date, Route, Fare).
    - Rating popup for completed trips (Stars + Comment).
- [ ] **Profile Tab (`ucProfile`)**:
    - User details display.
    - Password change section (Old Password, New Password).

### Phase 4: Driver Experience (`ucDriverHome`)
**Goal:** Implement the driver's operational interface.
- [ ] **Home Shell**:
    - Driver header (Avatar, Name, Phone, Rating, Total Trips, Vehicle).
    - Online/Offline toggle switch.
    - Tab Navigation: "Trạng thái", "Cuốc", "Ví", "Lịch sử", "Hồ sơ".
- [ ] **Status Tab (`ucDriverStatus`)**:
    - Wallet balance display.
    - KPI cards: Total Income, Total Trips.
- [ ] **Trips Tab (`ucRequest`)**:
    - Suggested trip cards (Pickup/Dropoff, Payout, Distance/Time).
    - "Từ chối" (Reject) and "Nhận cuốc" (Accept) buttons.
- [ ] **Wallet Tab (`ucWallet`)**:
    - Top-up interface with quick amount buttons (50k, 100k, 200k).
    - Manual amount input and "Nạp tiền" button.
- [ ] **Profile Tab**: Driver-specific details and password change.

### Phase 5: Admin Interface (`FrmAdmin`)
**Goal:** Implement the management dashboard.
- [ ] **Admin Shell**:
    - Blue header with close button.
    - Tab Navigation: "Thống kê", "Người dùng", "Chuyến đi", "Giá cước".
- [ ] **Statistics Tab**: 4 KPI cards (Total Users, Total Drivers, Total Trips, Total Revenue).
- [ ] **Users Tab**: Expandable list of users with role-specific details.
- [ ] **Trips Tab**: Expandable list of all trips with full audit details.
- [ ] **Pricing Tab**: Policy management for Car/Bike (Base fare, per-km, commission).

---

## ✅ Verification & Audit
- [ ] **Visual Audit**: Compare every screen against screenshots.
- [ ] **Functional Audit**: 
    - Test full Passenger flow: Book -> Match -> Trip -> Rate.
    - Test full Driver flow: Online -> Accept -> Complete -> Wallet.
    - Test Admin: Update pricing -> Verify in booking.
- [ ] **Code Audit**: 
    - Search for `Color.FromArgb` (should be 0 outside `UIHelper`).
    - Search for `new Font` (should be 0 outside `UIHelper`).
    - Verify `.Designer.cs` files are clean of logic.
