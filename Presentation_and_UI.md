# Presentation Layer Technical Documentation

## 1. Purpose of Presentation Layer

The Presentation Layer provides the user interface for the Ride-Hailing System using WinForms. It handles user interactions, displays data from the Application Layer, and sends commands/queries to Application services. This layer focuses on UI logic, event handling, and data binding without containing business rules or infrastructure concerns.

## 2. Folder / Namespace Structure

```
Presentation/
├── Components/          # Reusable UI controls (MapControl, FarePanel, DriverCardControl, etc.)
├── Helpers/             # Utility classes (DataMapper, UIHelper, AlertHelper, MapHelper, etc.)
├── Screens/             # Specific forms/pages
│   ├── Auth/            # Authentication screens (LoginForm, RegisterForm)
│   ├── Passenger/       # Passenger-specific screens (BookTripForm, TripTrackingForm, etc.)
│   ├── Driver/          # Driver-specific screens (DriverDashboardForm, EarningsForm, etc.)
│   └── Admin/           # Admin screens (AdminDashboardForm, UserManagementForm, etc.)
├── Shells/              # Main container forms (MainShell, PassengerShell, DriverShell, AdminShell)
└── ViewModels/          # UI state management classes (PassengerViewModel, DriverViewModel, etc.)
```

## 3. UI Structure Overview

- **Screens / Forms / Pages**: WinForms Forms serving as screens.
  - MainShell: Entry point with login/register navigation.
  - PassengerShell: Tab-based dashboard for passengers (Book Trip, Track Trip, History, Rating).
  - DriverShell: Header with status toggle, bottom navigation (Dashboard, Trips, Earnings).
  - AdminShell: TabControl for Users, Drivers, Trips, FareRules, Reports.
  - Specific Screens: LoginForm, RegisterForm, BookTripForm, TripTrackingForm, RatingForm, DriverDashboardForm, etc.
- **Components / Controls**: UserControls for reusable UI parts.
  - MapControl: GMap.NET-based map display with markers/routes.
  - FarePanel: Displays fare estimates.
  - DriverCardControl: Shows driver info cards.
  - LocationPickerControl: Pickup/destination selection.
  - TripStatusPanel: Real-time trip status display.
  - StatusPanel: General status/error display.

## 4. ViewModels / UI Models (if applicable)

- **ViewModels**: Classes managing UI state and data binding.
  - PassengerViewModel: Handles passenger data, trip booking, history, stats (TotalTrips, TotalSpent, AverageRating).
  - DriverViewModel: Manages driver status, earnings, trip execution.
  - AdminViewModel: Admin operations data.
  - TripViewModel: Shared trip-related UI logic.
- **UI Models**: Nested classes in ViewModels for data binding (e.g., PassengerStatusViewModel, TripBookingViewModel, DriverInfo, RouteInfo).

## 5. Event Handling Flow

- **User actions → handlers → application calls**:
  - Button clicks (e.g., Book Trip button in BookTripForm) → Event handler (btnBookTrip_Click) → Validates input → Calls Application service (e.g., \_tripService.CreateTripAsync()) → Updates UI state/ViewModel.
  - Map clicks (MapControl.MapClicked event) → Sets Pickup/Destination in ViewModel → Updates map markers.
  - Timer ticks (e.g., \_pollTimer in PassengerShell) → Polls Application for trip status → Updates TripStatusPanel.
  - Form loads (e.g., PassengerShell_Load) → Initializes ViewModel → Loads data via Application queries.
  - Navigation (TabControl selection) → Switches screens/forms in Shell → Refreshes ViewModel data.

## 6. Navigation Flow (if applicable)

- **Shell-based Navigation**: MainShell → Auth forms → Role-specific Shell (PassengerShell/DriverShell/AdminShell).
- **Tab-based**: PassengerShell uses TabControl for sub-screens (BookTripForm, TripTrackingForm, TripHistoryForm, RatingForm).
- **Modal/Dialog**: RatingForm, LoginForm opened as dialogs.
- **State-driven**: Navigation based on user role and trip status (e.g., active trip enables tracking tab).

## 7. Interaction with Application Layer

- **Commands / Queries usage**: UI calls Application interfaces directly.
  - Commands: `CreateTripAsync` (via `ITripService`), `MatchDriverAsync` (via `ITripService`), `CancelTripAsync` (via `ITripService`).
  - Queries: `GetTripAsync` (via `ITripService`), `GetActiveTripForPassengerAsync` (via `ITripService`), `GetAvailableDriversAsync` (via `IUserService` / `IDriverService`).
  - Direct service call pattern, inspired by CQRS concept, implemented via direct async service calls.
  - Domain entities are passed directly between layers (no DTO layer exists yet); UI must not modify Domain entities directly to maintain layer boundaries.

## 8. State Management (if any)

- **In-Form State**: Forms hold local state (e.g., \_currentTrip in PassengerShell, \_selectedVehicle in BookTripForm).
- **ViewModel State**: ViewModels maintain UI state (e.g., PassengerViewModel.CurrentTrip, AvailableDrivers list).
- **Shell State**: Shells manage global state per user (e.g., PassengerShell.\_passenger, \_currentTrip).
- **Polling**: Timers poll Application for real-time updates (e.g., trip status every 5 seconds via `GetTripAsync`). Interval balances UI responsiveness with server load; impacts performance especially under high concurrency.
- **No centralized state**: Relies on in-memory state and Application services; no Redux-like store.

## 9. Error Handling & User Feedback

- **Exception Handling**: Try-catch in event handlers; displays MessageBox for errors.
- **Validation**: Input validation via Application validators; shows error labels/tooltips.
- **User Feedback**: AlertHelper for success/error messages (likely MessageBox or status labels).
- **Status Updates**: StatusPanel/StatusBar for real-time feedback (e.g., "Trip requested", "Driver assigned").
- **Async Handling**: Loading indicators during async calls (e.g., disable buttons, show progress).

## 10. Design Issues / Inconsistencies (if any)

- **Direct Service Injection**: Forms inject Application services directly, mixing UI and Application concerns; should use ViewModels as mediators.
- **Mock Mappings**: DataMapper uses mocks ("mock" strings); incomplete implementation.
- **State Duplication**: State in Forms and ViewModels overlaps (e.g., \_currentTrip in both PassengerShell and PassengerViewModel). Resolution: ViewModel should be the single source of truth; Forms read state from ViewModels only.
- **Incomplete ViewModels**: Many TODOs in ViewModels (e.g., commented service calls); not fully implemented.

## 11. User Flows

### 11.1 Passenger

- **Login/Register**
- **Select Pickup/Destination** (via map click or search)
- **Choose Vehicle Type**
- **View Fare Estimate**
- **Request Ride**
- **Track Driver Location (simulation)**
- **Cancel Trip (before arrival)**
- **Pay & Rate Driver**
- **View Trip History**

### 11.2 Driver

- **Login/Register**
- **Toggle Availability**
- **Receive Trip Requests**
- **Accept/Reject Trip**
- **Update Trip Status (Arrived, Started, Completed)**
- **Track Earnings**
- **View Trip History**

### 11.3 Admin

- **Login**
- **Manage Users (CRUD)**
- **View Trips**
- **Configure Fare Rules**
- **Generate Reports**

## 12. GMap.NET — Phân Tách Theo Kiến Trúc

| Thành phần | Layer | Package cài đặt |
|---|---|---|
| `GMapControl` (UI widget, bản đồ hiển thị) | **Presentation** | `GMap.NET.WinForms` |
| `GMapProviders` (Routing, Geocoding API) | **Infrastructure** | `GMap.NET.Core` |

- **Presentation** cài `GMap.NET.WinForms` (bao gồm Core).
- **Infrastructure** chỉ cần `GMap.NET.Core` — không kéo theo WinForms dependency, phù hợp với Clean Architecture.
- `MapControl` (UserControl trong Presentation) wraps `GMapControl`; cấu hình `AccessMode.ServerAndCache` để cache tile giảm băng thông.

## 13. Map Interaction Design

- **Layers in GMap.NET**:
  - **Static Overlay**: Pickup (green), Destination (red).
  - **Route Overlay**: Trip path (GMapRoute từ danh sách PointLatLng sau khi giải mã Encoded Polyline).
  - **Dynamic Overlay**: Driver marker với smooth animation (Timer 500–1000ms, interpolation).
  - **Nearby Overlay**: Idle drivers for realism (UC18 Driver Radar).
- **Camera Control**: Auto-zoom to fit trip bounds, tránh flicker bằng Double Buffering.
- **Simulation**: Timer-driven interpolation — cập nhật vị trí điểm N → N+1, gọi `gMapControl.Invalidate()`.

## 14. Real-Time Updates in WinForms

- **Timer Control**: Updates driver positions every 500–1000ms (simulation tick).
- **DataGridView**: Displays trip lists, driver statuses.
- **Observer Pattern**: `ITripService.TripStatusChanged` event (`EventHandler<TripStatusChangedEventArgs>`) — Forms subscribe, không polling.
- **Async Tasks**: Prevent UI blocking during route calculation or file I/O.

## 7. Best Practices

- **Encapsulation**: Keep business logic out of UI forms.
- **Consistency**: Use tab-based navigation (PassengerShell, DriverShell, AdminShell).
- **Feedback**: Clear status indicators (trip states, driver availability).
- **Accessibility**: Large buttons, readable fonts, color-coded markers.
- **Error Handling**: Inform users when persistence fails (JSON read/write).

## 8. Example UI Components

| Component             | Purpose                          |
| --------------------- | -------------------------------- |
| **MapControl**        | Visualize trips, drivers, routes |
| **TripStatusPanel**   | Show trip progress               |
| **FarePanel**         | Display fare estimate            |
| **DriverCardControl** | Show driver info                 |
| **Admin Dashboard**   | Manage users, trips, fare rules  |

## 9. Maintaining Responsiveness

- **Async/Await** for route fetching.
- **BackgroundWorker** for simulation updates.
- **Optimistic Concurrency** for trip assignment.
- **Atomic JSON writes** to avoid corruption.
