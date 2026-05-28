# FINAL REPORT

## 1. CHƯƠNG 1. PHẦN MỞ ĐẦU

### 1.1. Tính cấp thiết của đề tài

The OOP system addresses the growing demand for efficient and convenient ride-hailing services in urban environments. It aims to provide a seamless experience for passengers (quick booking, transparent pricing, route tracking, service ratings) and drivers (easy trip acceptance, status management, clear income tracking). This project simulates a core intermediary platform in the transportation sector, offering economic and social benefits by optimizing vehicle usage, reducing passenger wait times, and creating flexible work opportunities. Academically, it serves as a practical application of OOP principles, software design, and system architecture.

### 1.2. Giới thiệu về Kĩ thuật lập trình hướng đối tượng (LTHĐT)

Object-Oriented Programming (OOP) is central to OOP, modeling the system as a collection of interacting objects (e.g., `Passenger`, `Driver`, `Trip`). Key OOP principles are applied:

- **Encapsulation**: Data within objects is protected and accessed via methods (e.g., `TripStatus` is managed through defined behaviors).
- **Inheritance**: Common properties are shared among related classes (e.g., `Driver` and `Passenger` inherit from an abstract `User` class).
- **Polymorphism**: Different objects respond to the same method call in their own way, allowing for flexible system behavior.
- **Abstraction**: Complex implementation details are hidden, exposing only necessary functionalities through interfaces and abstract classes.

### 1.3. Tầm quan trọng của LTHĐT trong Kĩ thuật phần mềm

OOP facilitates the development of complex software by breaking systems into manageable, reusable components. This approach enhances code maintainability, scalability, and reduces development time. In OOP, OOP principles contribute to a well-organized codebase, making it easier to manage different user roles, trip lifecycles, and system states.

## 2. CHƯƠNG 2. PHÂN TÍCH VÀ THIẾT KẾ LỚP

### 2.1. Phân tích bài toán

The OOP system simulates the interaction between passengers and drivers. Key actors include Passengers (booking, tracking, rating), Drivers (accepting trips, managing status, viewing earnings), and Admins (system management, pricing configuration). Core entities include `User`, `Trip`, `Vehicle`, `Location`, `Fare`, `Review`, and `Policy`. The system manages the lifecycle of a `Trip` through various statuses (`Pending`, `Searching`, `Matched`, `Arrived`, `Started`, `DropOff`, `Completed`, `Cancelled`, `Timeout`).

### 2.2. Thiết kế lớp và sơ đồ lớp

The system is designed with a layered architecture:

- **Domain Models** (`Entity.cs`, `ValueObject.cs`): `User`, `Driver`, `Passenger`, `Trip`, `Vehicle`, `Policy`, `Review`, `Location`, `Coordinate`, `Address`, `Route`, `Fare`.
- **State Pattern** (`Pattern.cs`): `IDriverState`, `ITripState` and concrete implementations.
- **Factories** (`Pattern.cs`): `UserFactory`, `VehicleFactory`, `DriverStateFactory`, `TripStateFactory`.
- **Services** (`Service.cs`): `TripCommandService`, `TripQueryService`, `UserService`, `DriverCommandService`, `DriverQueryService`, `DriverWalletService`, `PassengerService`, `AdminService`, `FareService`, `MapService`, `MatchingService`, `ReviewService`.
- **Repositories** (`Repositories.cs`): `JsonRepository<T>`, `TripRepository`, `UserRepository`, `VehicleRepository`, `PolicyRepository`, `ReviewRepository`.
- **Presentation** (`*Form.cs`, `Uc*.cs`): Windows Forms UI components.

Relationships include Inheritance (e.g., `Car`, `Motorbike` from `Vehicle`), Association (`Trip` with `Passenger`, `Driver`), Aggregation (Services using Repositories), and Dependency (UI depending on Services).

### 2.3. Cài đặt các lớp chức năng

The implementation follows the layered architecture:

- **Domain Layer** (`Entity.cs`, `ValueObject.cs`, `Pattern.cs`): Core entities and business rules.
- **Application Layer** (`Service.cs`): Orchestrates business logic via services.
- **Infrastructure Layer** (`Repositories.cs`, `Map.cs`): Handles data persistence (JSON files via `JsonRepository`) and external integrations (`MapService`).
- **Presentation Layer** (`*Form.cs`, `Uc*.cs`): Windows Forms UI for user interaction.

## 3. CHƯƠNG 3. XÂY DỰNG ỨNG DỤNG

### 3.1. Thiết kế giao diện chương trình

The Windows Forms UI is designed for clarity and role-specific usability. It features a main window with dynamic functional areas, consistent navigation, and clear visual cues. Color themes and typography are managed via `UIHelper.cs` and described in `UI.md`.

### 3.2. Phát triển các chức năng của ứng dụng

Core functionalities include user management, trip booking, driver matching, status updates, fare calculation, and reviews. The application uses C#/.NET, with code organized into projects per layer. OOP principles and design patterns are applied throughout. Data is serialized/deserialized using JSON for persistence.

### 3.3. Các kịch bản thực thi ứng dụng

Key scenarios tested include:

1.  Greenful passenger trip booking.
2.  Driver status changes (Online, OnTrip, Offline).
3.  Admin monitoring and configuration.

## 4. CHƯƠNG 4. THẢO LUẬN VÀ ĐÁNH GIÁ

### 4.1. Các kết quả nhận được

- A functional ride-hailing system simulation.
- Code organized using OOP principles and layered architecture.
- Implementation of core features and design patterns.
- A foundation for future expansion.

### 4.2. Đánh giá kiến trúc và chất lượng mã nguồn

The layered architecture (Domain, Application, Infrastructure, Presentation) provides good separation of concerns. The use of State Pattern for trip and driver lifecycles, and Repository Pattern for data access, are effective. However, architectural boundaries are not perfectly clean, with some dependencies potentially violating inversion principles. Domain models are coupled with JSON serialization, and the lack of transaction management for multi-repository operations poses a risk of data inconsistency.

### 4.3. Các tồn tại và khoản nợ kỹ thuật

- **Architectural Boundaries**: Dependencies between layers could be improved.
- **Domain-Serialization Coupling**: Domain models are tied to JSON persistence.
- **Transaction Management**: Missing for operations involving multiple repositories.
- **State Synchronization**: Potential inconsistencies between trip and driver states.
- **Security**: Basic login security (plain text passwords) is insufficient for production.
- **Other Limitations**: Scalability of JSON storage, lack of automated tests, potential issues with UI event/timer lifecycles, inconsistent exception handling.

### 4.4. Hướng phát triển và cơ hội tái cấu trúc

1.  Migrate data storage from JSON to a database.
2.  Decouple domain models from persistence using DTOs or separate persistence models.
3.  Strengthen architectural boundaries using Dependency Injection and composition roots.
4.  Implement Unit of Work or transaction patterns.
5.  Standardize state synchronization using domain events or aggregates.
6.  Implement secure password hashing.
7.  Develop real-time features (Web API, mobile app).
8.  Integrate advanced mapping and navigation.
9.  Add online payment and detailed reporting.
10. Implement comprehensive automated testing.

## PHỤ LỤC

### Phụ lục 1. Liên kết Github

The source code is available at: `https://github.com/<ten-to-chuc-hoac-tai-khoan>/<ten-repository>` (Placeholder, needs actual URL).

### Phụ lục 2. Hướng dẫn cài đặt để chạy chương trình

**Requirements:** Windows 10/11, Visual Studio 2022, .NET Framework 4.8 SDK.
**Steps:**

1.  Clone the repository.
2.  Open the solution file (`OOP2026.slnx`) in Visual Studio.
3.  Restore NuGet packages.
4.  Set `Presentation` project as startup.
5.  Build the solution.
6.  Run the application (F5).
    The `Program.Main` method initializes services, creates sample data for Passenger and Driver, and runs the `MultiRoleForm`.

### Phụ lục 3. Phân công công việc

- **NGUYỄN VĂN A**: Requirements analysis, system architecture, Domain Layer implementation (User, Trip, State), technical documentation.
- **NGUYỄN VĂN B**: UI design (WinForms), map integration, Infrastructure Layer (JSON persistence, Repositories), user guide, report preparation.

### Phụ lục 4. Ghi chú áp dụng kiến thức OOP

OOP concepts and patterns were applied throughout the project:

- **Abstraction**: Used via abstract classes and interfaces (`User`, `Vehicle`, `Repository`, `Service`).
- **Encapsulation**: Data protected within objects via methods.
- **Inheritance**: Used for `User` and `Vehicle` hierarchies.
- **Polymorphism**: Enabled by interfaces and abstract classes for flexible object handling.
- **Object Relationships**: Demonstrated inheritance, association, aggregation, composition, and dependency.
- **Design Patterns**: State, Factory, Repository patterns implemented.
- **Serialization**: JSON used for data persistence, illustrating object-to-file conversion.

### Tài liệu tham khảo

1.  Đặng Ngọc Hoàng Thành, _Bài giảng Lập trình hướng đối tượng_, UEH, 2026.
2.  Erich Gamma, et al., _Design Patterns: Elements of Reusable Object-Oriented Software_, 1994.
3.  Robert C. Martin, _Clean Architecture_, 2017.
4.  Microsoft Docs, _Serialization in .NET_.
