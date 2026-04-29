# Business Logic & UI Implementation Guide — RideGo

> **Platform:** .NET Framework 4.8 · C# 7.3 · WinForms
> **Scope:** Business workflows, UI specification, common utilities

---

## Table of Contents

1. [System Overview](#1-system-overview)
2. [Core Features](#2-core-features)
3. [Feature Matrix by Role](#3-feature-matrix-by-role)
4. [State Management](#4-state-management)
5. [Dispatch Algorithm](#5-dispatch-algorithm)
6. [Business Logic Details](#6-business-logic-details)
7. [UI Structure](#7-ui-structure)
8. [UI Flows](#8-ui-flows)
9. [Map Integration](#9-map-integration)
10. [Real-Time Updates](#10-real-time-updates)
11. [Common Utilities](#11-common-utilities)
12. [Educational Constraints](#12-educational-constraints)

---

## 1. System Overview

RideGo is a ride-hailing simulation system (Grab/Uber-style) built for educational purposes. It demonstrates:

- Layered architecture with clean separation
- State machines for trip lifecycle management
- Manual service composition (no DI container)
- Event-driven UI updates
- JSON file-based persistence

---

## 2. Core Features

### 2.1 Search & Book Trip

Passenger enters pickup, destination, vehicle type. System finds nearest matching driver, calculates fare, displays info, allows booking.

Steps:
1. Enter trip info (pickup, dropoff, vehicle type)
2. View estimated fare (including surge if applicable)
3. Send booking request
4. System searches for matching driver, sends request sequentially
5. When driver accepts, confirm and display driver info to passenger

### 2.2 Confirm & Cancel Trip

- **Confirm:** Driver accepts → status becomes Matched/Accepted → passenger receives notification
- **Cancel:** Passenger or driver can cancel before trip starts → system updates status, applies cancellation fee if needed

### 2.3 User Management

- Register/login, update personal info, vehicle, documents, work status
- Manage trip history, ratings, earnings (driver)

### 2.4 History & Audit

- Store trip details: time, status, locations, driver, passenger, fare, feedback
- Audit log for important operations (cancel, confirm, payment)

### 2.5 Payment (Simulation)

- Cash payment (simulated), store payment status, update driver earnings
- Extensible to e-wallet, card, payment gateway

### 2.6 Rating & Feedback

- Passenger rates driver, driver rates passenger after each trip
- Store scores, feedback for service quality improvement

---

## 3. Feature Matrix by Role

| Feature | Passenger | Driver | Admin |
|---------|-----------|--------|-------|
| Register/Login | ✔️ | ✔️ | ✔️ |
| Enter pickup/destination | ✔️ | | |
| Choose vehicle type | ✔️ | | |
| View fare | ✔️ | | |
| Send/Receive request | ✔️ | ✔️ | |
| Cancel trip | ✔️ | ✔️ | ✔️ |
| Receive notifications | ✔️ | ✔️ | |
| Track status | ✔️ | ✔️ | ✔️ |
| Track driver location | ✔️ (simulated) | | ✔️ (monitor) |
| Cash payment | ✔️ | ✔️ (record income) | ✔️ (report) |
| Rating | ✔️ (rate driver) | ✔️ (rate passenger) | ✔️ (manage) |
| View history | ✔️ | ✔️ | ✔️ |
| Toggle work status | | ✔️ | ✔️ |
| Manage accounts | | | CRUD users/drivers |
| Manage trips | | | ✔️ |
| Reports & statistics | | | ✔️ |
| Configure fare rules | | | ✔️ |

---

## 4. State Management

### 4.1 Driver States

| Status | Description |
|--------|-------------|
| Offline | Not accepting trips, not on map |
| Available | Ready to accept, visible in search |
| OnTrip | Currently on trip, temporarily unavailable |

### 4.2 Trip States

| Status | Description |
|--------|-------------|
| Requested | Passenger sent request, not yet searching |
| Searching | System finding driver |
| Matched | Driver assigned, waiting confirmation |
| Arrived | Driver arrived at pickup |
| Started | Trip in progress |
| Completed | Trip finished |
| Cancelled | Cancelled by passenger/driver/system |
| Timeout | No driver found, auto-cancelled |

**Note:** Trip is created at Requested status. After `RequestTrip()`, status changes to Searching to begin driver search.

---

## 5. Dispatch Algorithm

### 5.1 Driver Matching Process

System iterates `foreach` over all drivers from `DriverRepository` (no LINQ).

**Filter criteria (in order):**
1. **Vehicle type match:** Driver's `VehicleType` matches request (`Car` / `Motorbike`)
2. **Availability:** Exclude `Offline` and `OnTrip` drivers
3. **Administrative address filter** (see 5.1.1) — avoid route API for all drivers
4. **Wallet check:** Driver `Wallet` balance covers expected commission (hoa hồng)
5. **Sequential request:** Send to each driver in priority order with timeout
6. **Retry logic:** If rejected or timeout, try next driver
7. **Fallback:** If no drivers → `Trip.MarkTimeout()`

#### 5.1.1 Administrative Address Filter

**Problem:** Cannot call route API for thousands of drivers simultaneously.

**Solution:** Filter by address level first, then calculate route for small group:

```
Pickup point
    │
    ▼ Reverse Geocoding (GMap.NET Placemark)
Extract: LocalityName (phường) → SubAdminArea (quận) → AdminArea (thành phố)
    │
    ▼ Filter Driver (foreach, no LINQ)
Same phường? → enough? → Calculate route for small group
    │ No
    ▼
Expand to district → enough? → Calculate route
    │ No
    ▼
Expand to city → Calculate route
```

### 5.2 Race Condition Handling

**Problem:** Two drivers click "Accept" same trip at same millisecond — both read `Trip.Status == Searching`.

**Solution:** `SemaphoreSlim` lock in `MatchDriverAsync`:

```csharp
private static readonly SemaphoreSlim _matchLock = new SemaphoreSlim(1, 1);

public async Task MatchDriverAsync(Guid tripId, Guid driverId)
{
    await _matchLock.WaitAsync();
    try
    {
        Trip trip = await _tripRepository.GetByIdAsync(tripId);
        Driver driver = await _driverRepository.GetByIdAsync(driverId);

        if (!trip.IsSearching())
            throw new InvalidOperationException("Trip already assigned.");
        if (driver.Status != DriverStatus.Available)
            throw new InvalidOperationException("Driver no longer available.");

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

### 5.3 Retry & Fallback

- **Retry:** If first driver doesn't respond, automatically try next
- **Fallback:** If all drivers exhausted, notify passenger and set Timeout

---

## 6. Business Logic Details

### 6.1 Fare Calculation

```
TotalFare = BaseFare + Distance × PricePerKm
Commission = TotalFare × CommissionRate
DriverIncome = TotalFare − Commission
```

`FareRule` stores parameters by `VehicleType`. `FareRule.CalculateFare(distance)` returns `Fare` value object.

### 6.2 Workflows

**Trip Request Flow:**
1. Passenger UI → `ITripService.RequestTrip(passengerId, pickup, dest, vehicleType)`
2. `new Trip(...)` → `trip.SetSearching()`
3. `TripRepository.Add(trip)` → `SaveChangesAsync()`
4. Return `Trip`

**Driver Assignment Flow:**
1. Driver UI → `ITripService.MatchDriverAsync(tripId, driverId)`
2. Check `trip.IsSearching()` + `driver.Status == Available`
3. `trip.MatchDriver(driverId)` + `driver.SetOnTrip()`
4. Update repositories → Save changes → Emit `TripMatchedEvent`

**Trip Completion Flow:**
1. Driver UI → `ITripService.CompleteTrip(tripId, fareAmount)`
2. `trip.CompleteTrip()` → `trip.ConfirmPayment()`
3. `driver.PayCommission(fare)` + `driver.SetAvailable()` + `passenger.AddTrip()`
4. Update repositories → Save changes → Emit `TripCompletedEvent` + `TripPaidEvent`

**Rating Flow:**
1. Passenger UI → `IReviewService.AddReviewAsync(...)`
2. `new Review(...)` → `driver.UpdateReviews(rating)`
3. Update repositories → Save changes → Emit `ReviewCreatedEvent`

---

## 7. UI Structure

### Folder Structure (Updated from Codebase)

```
Presentation/
├── Components/          # Reusable UserControls
│   ├── MapControl.cs          # GMap.NET + ActiveSlot/MapClicked/MockReverseGeocode
│   ├── FarePanel.cs/.resx     # Fare breakdown (fonts polished)
│   ├── DriverCardControl.cs   # Driver info
│   ├── LocationPickerControl.cs # Dropdown recent/fixed, partial Address OK
│   ├── TripStatusPanel.cs     # Status display
│   ├── StatusPanel.cs         # General alerts
│   ├── LocationCard.cs
│   └── TripCard.cs
├── Shells/              # Forms
│   ├── FrmMainShell.cs        # Single shell, UC loader, event hub
│   ├── FrmModal.cs            # Sub-UC host
│   └── FrmToast.cs            # Notifications
├── UserControls/        # Main UCs (actual)
│   ├── UcPassenger.cs         # SplitContainer 70/30 map/dynamic stages
│   ├── UcDriver.cs            # tblMain topbar + split 35/65
│   ├── UcAdmin.cs             # TabControl CRUD
│   ├── UcAuth.cs              # Toggle login/register
│   ├── UcProfile.cs
│   ├── UcRating.cs
│   └── UcTripDetail.cs
├── BaseShell.cs, BaseUserControl.cs # Exception handling
└── Program.cs           # Services composition root
```

### Shell Navigation (Implemented)

- FrmMainShell loads UcAuth → post-auth role UC (UcPassenger/Driver/Admin) dynamically.
- No separate PassengerShell/DriverShell/AdminShell (uses UCs directly in MainShell).
- Event-driven: TripStatusChanged → stages/Toast.

---

## 8. UI Flows

### 8.1 Passenger Flow

1. Login/Register
2. Select pickup/destination (map click or search)
3. Choose vehicle type
4. View fare estimate
5. Request ride
6. Track driver location (simulation)
7. Cancel trip (before arrival)
8. Pay & rate driver
9. View trip history

### 8.2 Driver Flow

1. Login/Register
2. Toggle availability (Online/Offline)
3. Receive trip requests
4. Accept/Reject trip
5. Update trip status (Arrived → Started → Completed)
6. Track earnings
7. View trip history

### 8.3 Admin Flow

1. Login
2. Manage users (CRUD)
3. View trips
4. Configure fare rules
5. Generate reports

---

## 9. Map Integration

### GMap.NET Architecture

| Component | Layer | Package |
|-----------|-------|---------|
| `GMapControl` (UI widget) | **Presentation** | `GMap.NET.WinForms` |
| `GMapProviders` (Routing, Geocoding) | **Infrastructure** | `GMap.NET.Core` |

### MapControl Configuration

```csharp
gMapControl.MapProvider = GMapProviders.GoogleMap;
gMapControl.AccessMode = AccessMode.ServerAndCache;
gMapControl.ShowCenter = false;
gMapControl.DragButton = MouseButtons.Left;
gMapControl.MouseWheelZoomEnabled = true;
```

### Map Layers

- **Static Overlay:** Pickup (green), Destination (red)
- **Route Overlay:** Trip path (GMapRoute from decoded polyline)
- **Dynamic Overlay:** Driver marker with smooth animation
- **Nearby Overlay:** Available drivers for realism

### Camera & Animation

- Auto-zoom to fit trip bounds
- Double buffering to prevent flicker
- Timer-driven interpolation (500–1000ms) for driver movement
- Update position point N → N+1, call `gMapControl.Invalidate()`

---

## 10. Real-Time Updates

### Observer Pattern

`ITripService.TripStatusChanged` event (`EventHandler<TripStatusChangedEventArgs>`) — Forms subscribe without polling:

```csharp
// In Form constructor or Load
_tripService.TripStatusChanged += OnTripStatusChanged;

// Handler
void OnTripStatusChanged(object sender, TripStatusChangedEventArgs e)
{
    // Update UI on main thread
    if (InvokeRequired) { Invoke(...); return; }
    UpdateStatusDisplay(e.NewStatus);
}
```

**Important:** Unsubscribe in `Form_FormClosed` to prevent memory leaks.

### Polling (Alternative)

Timer-based polling every 5 seconds for trip status updates. Used when event subscription not available.

---

## 11. Common Utilities

### 11.1 Folder Structure

```
Common/
├── Constants/           # Application-wide constants
│   ├── FareConstants.cs
│   └── SimulationConstants.cs
├── Extensions/          # Extension methods
│   ├── StringExtensions.cs
│   └── DecimalExtensions.cs
├── Helpers/             # Utility classes
│   └── PasswordHasher.cs
└── Utilities/           # Duplicate location
    └── PasswordHasher.cs
```

### 11.2 Constants

**FareConstants:**
| Constant | Value | Description |
|----------|-------|-------------|
| `CommissionRate` | 0.20m (20%) | Platform commission |
| `MinimumFare` | 12,000 VND | Minimum trip fare |

**Vehicle-specific:**
- Motorbike: `BaseFare = 12,000 VND` (first 2km), `PricePerKm = 4,000 VND`
- Car: `BaseFare = 25,000 VND` (first 2km), `PricePerKm = 12,000 VND`

**SimulationConstants:**
| Constant | Value | Description |
|----------|-------|-------------|
| `TickIntervalMs` | 2000 ms | Simulation update frequency |
| `InterpolationSteps` | 20 | Route smoothness |
| `DefaultSpeedKmh` | 40.0 km/h | Average speed |
| `MaxPickupDistanceKm` | 5.0 km | Maximum search radius |

### 11.3 StringExtensions

| Method | Purpose | Example |
|--------|---------|---------|
| `IsValidPhone()` | Validate Vietnam phone | `"0912345678".IsValidPhone()` → `true` |
| `IsValidEmail()` | Basic email validation | `"user@domain.com".IsValidEmail()` → `true` |
| `ToTitleCase()` | Capitalize words | `"nguyễn văn a"` → `"Nguyễn Văn A"` |
| `Truncate(int)` | Shorten with ellipsis | `"Hello".Truncate(3)` → `"Hel..."` |

### 11.4 DecimalExtensions

| Method | Purpose | Example |
|--------|---------|---------|
| `RoundToVnd()` | Round to nearest thousand | `12300m.RoundToVnd()` → `12000` |
| `SubtractPercentage(decimal)` | Deduct percentage | `100000m.SubtractPercentage(0.2m)` → `80000` |
| `ToVndCurrency()` | Format as "100.000đ" | `100000m.ToVndCurrency()` → `"100.000đ"` |
| `ToDistanceString()` | Format as "1.2 km" | `1.234m.ToDistanceString()` → `"1.2 km"` |

### 11.5 PasswordHasher

Thread-safe static utility using PBKDF2 with SHA-256:

| Method | Purpose |
|--------|---------|
| `HashPassword(string)` | Generate secure hash |
| `VerifyPassword(string, string)` | Validate against stored hash |
| `NeedsRehash(string)` | Check if outdated parameters |

**Format:** `pbkdf2-sha256$100000$<salt>$<hash>`

---

## 12. Educational Constraints

| Constraint | Purpose | Implementation |
|------------|---------|----------------|
| **WinForms** | Event-Driven Programming | Timer, DataGridView, MouseClick on Panel |
| **Newtonsoft.Json** | Persistence & Data Stream | `JsonConvert.SerializeObject` / `DeserializeObject<List<T>>` with `TypeNameHandling.All` for polymorphism |
| **No LINQ** | Data structures & algorithms | `foreach` + `if-else` instead of `Where`, `Select`, etc. |
| **No Lambda** | Delegate & Method understanding | Explicit methods instead of `x => x.Id` |
| **No `var`** | Static Typing mindset | `Passenger p = new Passenger();` |

---

## 13. Error Handling & User Feedback

| Scenario | Solution |
|----------|----------|
| Internet disconnect | Switch GMap.NET to `AccessMode.CacheOnly` |
| App crash | Save after each important operation |
| ID conflict | Use `Guid` as primary key |
| UI freeze when drawing map | Double buffering, invalidate changed markers only |

---

*Document version: 2.1 — Updated: removed MaxPickupDistance, trip.Status uses IsSearching(), clarified State Pattern vs State Machine.*
