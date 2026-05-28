# WinForms UI Rules Refactor Plan

**Date:** 2026-05-26
**Goal:** Enforce strict WinForms UI coding standards across the project to improve maintainability, consistency, and performance.

## 🛠 UI Rules to Enforce
1. **`.Designer.cs` Purity**: No business logic, event handlers, or `OnPaint` overrides. Only auto-generated initialization.
2. **`.cs` Logic**: All event handling, service interaction, and custom painting logic must reside here.
3. **No Self-Rounding**: UserControls must not call `SetRoundedRegion` on themselves in `OnLoad`/`OnPaint`.
4. **Centralized Colors**: Use `OOP2026.Colors` from `UIHelper.cs`. No `Color.FromArgb(...)`.
5. **Centralized Typography**: Use `OOP2026.Typography` from `UIHelper.cs`. No `new Font(...)`.
6. **Dynamic Layout**: Prefer `TableLayoutPanel`, `FlowLayoutPanel`, `Dock`, and `Anchor`. Minimize absolute `Point(x, y)` coordinates.
7. **Naming Convention**:
    - `btn`: Button
    - `lbl`: Label
    - `txt`: TextBox / RoundTextBox
    - `tlp`: TableLayoutPanel
    - `flp`: FlowLayoutPanel
    - `pnl`: Panel
    - `cbo`: ComboBox
    - `nud`: NumericUpDown
    - `dgv`: DataGridView
    - `uc`: UserControl
    - `frm`: Form

---

## 📋 Refactor Steps

### Phase 1: Colors & Typography (High Priority)
*Goal: Eliminate hardcoded visual styles.*
- [ ] **Audit & Replace Colors**: Search for `Color.FromArgb` and `Color.White/Black/etc` (where not using `Colors` class) in all `.cs` and `.Designer.cs` files.
    - Target files: `FrmAdmin.Designer.cs`, `FrmDriver.Designer.cs`, `ucPassengerHome.Designer.cs`, `ucPassengerHome.cs`, `UcProfile.Designer.cs`, `ucReview.cs`, `UcStatCard.Designer.cs`, `UcTrip.Designer.cs`, `ucTripCard.cs`, `UcTripCard.Designer.cs`, `UcTripStatus.Designer.cs`, `UcWallet.Designer.cs`.
- [ ] **Audit & Replace Fonts**: Search for `new Font(...)` and replace with `Typography` constants.
    - Target files: `FrmAdmin.Designer.cs`, `UcProfile.Designer.cs`, `ucReview.cs`, `UcTripCard.cs`, `UcTripStatus.Designer.cs`.

### Phase 2: Logic Separation (Medium Priority)
*Goal: Clean up `.Designer.cs` files.*
- [ ] **Move Paint Logic**: Move `PnlPickupDot_Paint` and `PnlDropoffDot_Paint` from `UcBooking.Designer.cs` to `ucBooking.cs`.
- [ ] **Move Event Handlers**: Ensure no `Click` or `TextChanged` logic is defined inside `InitializeComponent` (only the subscription `+=` is allowed).

### Phase 3: Layout & Naming (Medium Priority)
*Goal: Improve responsiveness and readability.*
- [ ] **Rename Controls**:
    - `FrmDriverAuth.Designer.cs`: `tblMain` $\rightarrow$ `tlpMain`.
    - `FrmPassengerAuth.Designer.cs`: `tblMain` $\rightarrow$ `tlpMain`.
    - `FrmMultiRole.Designer.cs`: `_map` $\rightarrow$ `ucMap`, `_passengerPanel` $\rightarrow$ `ucPassengerHome`, `_driverHome` $\rightarrow$ `ucDriverHome`.
    - `Form1.Designer.cs`: Rename generic `btn`, `lbl`, `txt` to descriptive names (e.g., `btnSubmit`, `lblTitle`).
- [ ] **Refactor Absolute Layouts**:
    - `FrmAdmin.Designer.cs`: Convert top bar and nav panel to use `Dock` and `Anchor` more effectively.
    - `UcProfile.Designer.cs`: Review `tlpMain` children to ensure they don't rely on absolute `Location` where `Dock/Anchor` suffices.

### Phase 4: User Control Audit (Low Priority)
*Goal: Prevent UI glitches and memory leaks.*
- [ ] **Verify Rounding**: Confirm no `UserControl` calls `SetRoundedRegion(this, ...)` in its own `OnLoad` or `OnPaint`.
- [ ] **Check `UcDriverStatus`**: Ensure `SetRoundedRegion` calls on internal panels are moved to the `.cs` file if they are considered "logic" rather than "property initialization".

---

## ✅ Verification Checklist
- [ ] No `Color.FromArgb` found in project (except `UIHelper.cs`).
- [ ] No `new Font` found in project (except `UIHelper.cs`).
- [ ] `.Designer.cs` files contain no method bodies other than `InitializeComponent` and `Dispose`.
- [ ] All controls follow the `prefixName` naming convention.
- [ ] UI remains visually identical after refactoring.

## ⚠️ Risks
- **Designer Corruption**: Manual edits to `.Designer.cs` can sometimes be overwritten by the Visual Studio Designer. Changes should be verified after opening the form in the designer.
- **Layout Shift**: Changing absolute positions to `Dock/Anchor` might shift elements if the container constraints are not set correctly.
