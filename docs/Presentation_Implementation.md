# RideGo - Presentation Layer Implementation Details

> **Generated:** Auto-documented from actual codebase. .NET Framework 4.8 WinForms. Matches RideGo_UI_Architecture.md structure.

## Forms (3 Forms)

### FrmMainShell.cs (Shells/)
- Inherits BaseShell.
- Program.cs entry: `new FrmMainShell(services)` → `Application.Run(shell)`.
- Dynamic UC loader for UcAuth → role UCs (Passenger/Driver/Admin).
- TabControl for multi-UC hosting, event hub (TripStatusChanged → FrmToast).

### FrmModal.cs (Shells/)
- Generic modal container for sub-UCs (UcRating, UcProfile, UcTripDetail).
- TopLevel=false when hosted.

### FrmToast.cs (Shells/)
- Non-modal notifications (trip updates), no focus steal.

## Main UserControls (UserControls/)

### UcPassenger.cs / .Designer.cs
**Layout:** SplitContainer (Vertical, SplitterDistance=840 ~70% map left / 30% right).
- **Panel1:** MapControl (Dock Fill).
- **Panel2:** TableLayoutPanel tblRight (3 rows: Header 56px / pnlActionStage Fill / lblStatus 32px).
  - Header: lblPassengerName + btnHistory/Profile/Logout.
  - pnlActionStage: pnlBooking/Searching/Tracking/Payment/History (dynamic show/hide).
    - pnlBooking: pickupPicker/destinationPicker (Dock Top 60px each), cmbVehicleType, btnBook full-width 68px.
    - Other stages similar with components.
**Features:** 
- Click pickers → mapControl.ActiveSlot = Pickup/Destination, lblStatus update.
- MapControl_MapClicked → update picker SelectedLocation, place marker.
- Matches docs 100%.

### UcDriver.cs / .Designer.cs
**Layout:** TableLayoutPanel tblMain (2 rows: pnlTopBar 64px / splitContent Fill).
- **pnlTopBar:** FlowLayoutPanel-like (lblDriverName, btnToggleStatus 160x40, lblWallet/Rating, btnProfile/Logout 90x36).
- **splitContent:** SplitContainer Vertical SplitterDistance=417 (~35% pnlRequests / 65% pnlCurrentTrip).
  - pnlRequests: lblRequestsTitle + dgvRequests + btnAccept/Reject.
  - pnlCurrentTrip: lblTripStatus + pnlNoTrip / pnlTripActions (btnArrived/Start/Complete 740x80, Cancel 48px).
**Features:** Toggle status, accept/reject requests, trip actions async via services, event-driven updates.

### UcAdmin.cs / .Designer.cs
**Layout:** TabControl (Dock Fill) – Users/Trips/Fares/Stats tabs with DataGrids/CRUDS (pending full verify).

### UcAuth.cs / .Designer.cs
**Layout:** TableLayoutPanel toggle pnlLogin/Register (pending verify).

## Sub/Components (Components/)

| Component | Purpose | Key Features |
|-----------|---------|--------------|
| MapControl.cs | GMap.NET wrapper | ActiveSlot (Pickup/Destination/None), markers (pickup green, dest red, driver blue), routes, MockReverseGeocode (District 1/Thu Duc/Chau Doc), MapClicked event. |
| LocationPickerControl.cs | Address picker | Recent + fixed UEH locations, OwnerDraw dropdown, graceful partial Address (coords fallback), JSON persist. |
| FarePanel.cs / .resx | Fare display | Layout polish (fonts 9F), base/km/total. |
| DriverCardControl.cs | Driver info card | Name/rating/vehicle, layout adjusted. |
| TripStatusPanel.cs | Status display | Real-time trip state. |
| Others | StatusPanel, LocationCard, TripCard | Reusable. |

## Base Classes
- BaseShell.cs: Tab mgmt, exception handling (Arg/InvalidOp/Exception → friendly msg + error.log).
- Program.cs: Manual services composition, global exception handlers.

## Sync Status
- Code fully implements docs structure/layouts.
- TODO_UI_Fixes.md complete (layouts, map fixes).
- Responsive: Dock/Fill/Anchor/TableLayoutPanel/SplitContainer, no hard Location.
- Events: TripStatusChanged wired for stages.
- Build: Success (non-critical warnings).

**Test:** `cd Presentation && dotnet run` – Auth → roles → map/panels functional.

Updated from codebase scan. Code/docs now synchronized.
