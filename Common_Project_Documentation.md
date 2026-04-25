# Common Project Technical Documentation

## 1. Purpose of Common Project

The Common project serves as a cross-cutting shared utilities library used across all layers of the Ride-Hailing System (Domain, Application, Infrastructure, Presentation). It provides foundational, non-domain-specific functionality including constants, extension methods, exception base classes, and helper utilities. This project ensures consistency, reusability, and centralized maintenance of shared code.

**Key Principles:**
- **Stateless and thread-safe** classes
- **Technology-agnostic** utilities (no external framework dependencies)
- **Layer-neutral** - no references to Domain, Application, Infrastructure, or Presentation
- **Generic and reusable** across different application contexts

## 2. Layer Mapping

| Layer Type | Cross-cutting / Shared Infrastructure |
|------------|----------------------------------------|
| **Purpose** | Provide common utilities, constants, extensions, and base classes for entire solution |
| **Dependencies** | None (standalone utility library) |
| **Consumed By** | Domain, Application, Infrastructure, Presentation (all layers) |
| **Deployment** | Compiled as DLL, referenced by all other projects |

## 3. Folder / Namespace Structure

```
Common/
├── Constants/           # Application-wide constants (FareConstants, SimulationConstants)
├── Extensions/          # .NET type extension methods (StringExtensions, DecimalExtensions)
├── Helpers/             # Utility helper classes (PasswordHasher)
└── Utilities/           # Additional utility location (PasswordHasher — duplicate of Helpers)
```

**Namespaces:**
- `Common.Constants`
- `Common.Extensions`
- `Common.Helpers`
- `Common.Utilities`

## 4. Components Overview

### 4.1 Constants

**Purpose:** Centralize magic numbers, configuration values, and business rule constants.

#### FareConstants (`Common/Constants/FareConstants.cs`)
Static class containing fare calculation parameters:

| Constant | Value | Description |
|----------|-------|-------------|
| `CommissionRate` | 0.20m (20%) | Platform commission percentage |
| `MinimumFare` | 12,000 VND | Minimum trip fare |
| `WaitingFeePerMinute` | 1,000 VND/min | Waiting charge |

**Nested classes:**
- `Motorbike`: `BaseFare = 12,000 VND` (first 2km), `PricePerKm = 4,000 VND/km`
- `Car`: `BaseFare = 25,000 VND` (first 2km), `PricePerKm = 12,000 VND/km`

**Usage Example:**
```csharp
decimal baseFare = FareConstants.Motorbike.BaseFare;
decimal commission = fare * FareConstants.CommissionRate;
```

#### SimulationConstants (`Common/Constants/SimulationConstants.cs`)
Static class for simulation engine parameters:

| Constant | Value | Description |
|----------|-------|-------------|
| `TickIntervalMs` | 2000 ms | Simulation update frequency |
| `InterpolationSteps` | 20 | Route interpolation smoothness |
| `DefaultSpeedKmh` | 40.0 km/h | Average vehicle speed |
| `MaxPickupDistanceKm` | 5.0 km | Maximum driver search radius |

**Usage:** Driver location updates, trip progress simulation, ETA calculations.

---

### 4.2 Extensions

**Purpose:** Provide reusable extension methods for .NET types without modifying original types.

#### StringExtensions (`Common/Extensions/StringExtensions.cs`)

**Validation Methods:**

| Method | Purpose | Example |
|--------|---------|---------|
| `IsValidPhone()` | Validate Vietnam phone format (0 + 9 digits) | `"0912345678".IsValidPhone()` → `true` |
| `IsValidEmail()` | Basic email format validation | `"user@domain.com".IsValidEmail()` → `true` |

**Sanitization & Transformation:**

| Method | Purpose | Example |
|--------|---------|---------|
| `ToTitleCase()` | Capitalize first letter of each word | `"nguyễn văn a"` → `"Nguyễn Văn A"` |
| `Truncate(int maxLength)` | Shorten string with ellipsis | `"Hello World".Truncate(5)` → `"He..."` |

**Implementation Notes:**
- Uses `Regex.IsMatch` for phone/email validation
- `ToTitleCase()` converts to lowercase first, then capitalizes each word
- `Truncate()` preserves original string if length ≤ maxLength

---

#### DecimalExtensions (`Common/Extensions/DecimalExtensions.cs`)

**Financial Methods:**

| Method | Purpose | Example |
|--------|---------|---------|
| `RoundToVnd()` | Round to nearest thousand VND | `12300m.RoundToVnd()` → `12000` |
| `SubtractPercentage(decimal)` | Deduct percentage (e.g., commission) | `100000m.SubtractPercentage(0.2m)` → `80000` |

**Formatting Methods:**

| Method | Purpose | Example |
|--------|---------|---------|
| `ToVndCurrency()` | Format as "100,000đ" (comma→dot) | `100000m.ToVndCurrency()` → `"100.000đ"` |
| `ToDistanceString()` | Format as "1.2 km" | `1.234m.ToDistanceString()` → `"1.2 km"` |

**Implementation Notes:**
- `RoundToVnd()` uses `MidpointRounding.AwayFromZero` for proper rounding
- `ToVndCurrency()` replaces comma with dot for Vietnamese locale format

---

### 4.3 Helpers

#### PasswordHasher (`Common/Helpers/PasswordHasher.cs`)

Thread-safe static utility for secure password hashing and verification.

**Security Features:**
- **Algorithm:** PBKDF2 with SHA-256 (`Rfc2898DeriveBytes`)
- **Iterations:** 100,000 (current) with fallback to 10,000 (legacy)
- **Salt Size:** 16 bytes
- **Hash Size:** 32 bytes
- **Storage Format:** `pbkdf2-sha256$100000$<base64salt>$<base64hash>`

**Public Methods:**

| Method | Purpose | Returns |
|--------|---------|---------|
| `HashPassword(string)` | Generate secure hash | Format: `algorithm$iterations$salt$hash` |
| `VerifyPassword(string, string)` | Validate password against stored hash | `true` if match |
| `NeedsRehash(string)` | Check if hash uses outdated parameters | `true` if rehash recommended |

**Legacy Format Support:**
- Current: `pbkdf2-sha256$100000$...$...`
- Legacy PBKDF2-SHA1: `10000:base64salt:base64hash` (10k iterations)
- Legacy Plain Text: `plain$<password>` (unsafe, triggers warning)

**Security Notes:**
- Uses constant-time comparison (`FixedTimeEquals`) to prevent timing attacks
- Migrates legacy passwords on next login (detected via `NeedsRehash()`)
- Comprehensive exception handling with trace warnings

**Usage Example:**
```csharp
// Registration
string hash = PasswordHasher.HashPassword(password);
user.PasswordHash = hash;

// Login
bool isValid = PasswordHasher.VerifyPassword(inputPassword, user.PasswordHash);

// Migration check
if (PasswordHasher.NeedsRehash(user.PasswordHash))
{
    user.PasswordHash = PasswordHasher.HashPassword(inputPassword);
}
```

---

## 5. Missing or Unimplemented Items

**Documentation Notes vs. Actual Implementation:**

| Documented Item | Status | Notes |
|-----------------|--------|-------|
| `Constants/` | ✅ Implemented | FareConstants, SimulationConstants |
| `Extensions/` | ✅ Implemented | StringExtensions, DecimalExtensions |
| `Exceptions/` | ❌ Not present | No exception base class exists in Common |
| `Helpers/` | ✅ Implemented | PasswordHasher only; missing validation helpers |
| `Utilities/` | ✅ Implemented | Duplicate PasswordHasher (same as Helpers) |
| DataMapper helper | ❌ Missing | Not present in Common (in Presentation instead) |
| MapHelper helper | ❌ Missing | Not present in Common (in Presentation instead) |
| UIHelper helper | ❌ Missing | Not present in Common (in Presentation instead) |
| AlertHelper helper | ❌ Missing | Not present in Common (in Presentation instead) |

**Gap Analysis:**
- Common project implementation is **minimal** compared to typical shared library
- Presentation-specific helpers (DataMapper, UIHelper, MapHelper) incorrectly placed in `Presentation/Helpers/` instead of `Common/Helpers/`
- No generic collections helpers, validation helpers, or date/time extensions that typically belong in Common
- No exception base class exists in Common; all layers use standard `System.Exception`

---

## 6. Usage Guidelines

**Design Principles:**
- Stateless: All classes are static or immutable
- Thread-safe: No mutable static state
- Pure functions: Deterministic outputs without side effects
- No external dependencies: Avoid referencing other projects (Domain, Application, etc.)
- Minimal allocations: Avoid allocations in hot paths

**Best Practices:**
1. **Extension Methods:** Use sparingly; only for ubiquitous operations
2. **Constants:** Group related constants in static classes (no public fields mixed with methods)
3. **Helpers:** Keep helper methods small, focused, well-documented
4. **Utilities:** Avoid duplicating helpers across `Helpers/` and `Utilities/` folders

**Thread Safety:**
All Common classes are inherently thread-safe:
- Static readonly constants
- No instance state
- No mutable static fields

---

## 7. Cross-Layer Integration

**Dependency Flow:**
```
Presentation (UI)
    ↓ uses
Application (Services/Handlers)
    ↓ uses
Domain (Entities/Services)
    ↓ uses
Infrastructure (Repositories/External)
    ↓ uses
Common (Constants/Extensions/Helpers/Utilities)
```

**Example Integration Points:**

| Common Component | Used By | Purpose |
|------------------|---------|---------|
| `FareConstants` | Application.FareService | Base fare lookup |
| `SimulationConstants` | Application.SimulationService, Presentation timers | Simulation tick rate |
| `StringExtensions.IsValidPhone()` | Application.UserService | Input validation |
| `DecimalExtensions.ToVndCurrency()` | Presentation UI forms | Fare display formatting |
| `PasswordHasher` | Domain.User | User authentication |

---

## 8. File Reference

| File | Path | Responsibility |
|------|------|----------------|
| `FareConstants.cs` | `Common/Constants/FareConstants.cs` | Fare business constants |
| `SimulationConstants.cs` | `Common/Constants/SimulationConstants.cs` | Simulation parameters |
| `StringExtensions.cs` | `Common/Extensions/StringExtensions.cs` | String utilities |
| `DecimalExtensions.cs` | `Common/Extensions/DecimalExtensions.cs` | Decimal/financial utilities |
| `PasswordHasher.cs` | `Common/Helpers/PasswordHasher.cs` | Password security utility |
| `PasswordHasher.cs` | `Common/Utilities/PasswordHasher.cs` | Duplicate of Helpers version |

---

## 9. Future Enhancements

Based on codebase analysis, recommended additions:

1. **Date/Time Extensions:** `ToRelativeTime()`, `ToShortDateString()`
2. **Validation Helpers:** `ValidateRequired()`, `ValidateRange()` in a new `ValidationHelper.cs`
3. **Collection Extensions:** `ToReadOnlyList()`, `Batch()` for performance
4. **Exception Base Class:** Introduce a shared exception base (e.g., `AppException`) in Common for consistent error handling across layers
5. **Result Pattern:** `Result<T>` type for functional error handling
6. **Guard Clauses:** `Guard.Against.Null()`, `Guard.Against.OutOfRange()` static methods

These would reduce duplication and improve consistency across layers.

---

## 10. Summary

The Common project provides essential shared infrastructure for the Ride-Hailing System. While functionally adequate for the current scope, it is **undersized** relative to typical cross-cutting concerns in a full application. The implementation covers core needs (constants, basic extensions, password hashing), but several presentation-specific helpers are misplaced in the Presentation layer rather than Common.

**Strengths:**
- Clean separation of concerns
- Stateless, thread-safe design
- Comprehensive password hashing with legacy support

**Weaknesses:**
- Limited set of utilities (could be expanded)
- No shared exception base class exists in Common
- Duplicate PasswordHasher in both `Helpers/` and `Utilities/`
- Helpers scattered across Presentation layer

**Recommendation:** Consolidate all generic UI helpers (DataMapper, UIHelper, AlertHelper, MapHelper) into Common/Helpers, and expand exception types to create a complete application foundation.
