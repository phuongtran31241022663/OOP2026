# Core Flows Class Diagram (Chi tiết theo Code)

Tài liệu này mô tả chi tiết cấu trúc lớp của hệ thống RideGo, phản ánh chính xác các interface, mẫu thiết kế và thuộc tính trong mã nguồn.

## 1. Sơ đồ Domain Model (Thực thể & Đối tượng Giá trị)

```mermaid
classDiagram
    direction LR
    
    %% Value Objects
    class Coord {
        +double Latitude
        +double Longitude
    }
    class Addr {
        +string Name
        +string Street
        +string District
        +string City
        +string Country
        +string HouseNumber
    }
    class Loc {
        +Coord Coord
        +Addr Addr
    }
    class Route {
        +Loc Pickup
        +Loc Dropoff
        +decimal Distance
        +TimeSpan Duration
        +string Polyline
    }
    class Fare {
        +decimal TotalAmount
        +decimal Commission
        +decimal DriverIncome
    }

    %% Domain Entities
    class Usr {
        <<abstract>>
        +Guid Id
        +string Name
        +string Phone
        +string Pwd
        +ChangePassword()
        +UpdateProfile()
        +VerifyPassword()
    }
    class Psg {
        +int TotTrip
        +IncrementTotalTrips()
    }
    class Drv {
        +string LicNo
        +Guid VehId
        +Loc Position
        +decimal Wallet
        +decimal Income
        +int TotTrip
        +int RatingSum
        +int TotalReviews
        +decimal AvgRat
        +DriverStatus Status
        +PayCommission()
        +UpdatePosition()
        +Deposit()
        +AddIncome()
        +AddReview()
    }
    class Adm
    class Veh {
        <<abstract>>
        +Guid Id
        +Guid DriverId
        +string Plate
        +string Brand
        +string Model
        +string Color
        +int Capacity
        +VehicleType Type
        +LinkDriver()
        +UnlinkDriver()
    }
    class Car
    class Moto
    class Trip {
        +Guid Id
        +Guid PassengerId
        +VehicleType TripVehicleType
        +Fare TripFare
        +Route TripRoute
        +DateTime RequestAt
        +Guid? DriverId
        +bool IsPaid
        +bool IsReviewed
        +TripStatus Status
        +int PendingCount
        +StartSearching()
        +AssignDriver()
        +DriverArrived()
        +BeginTrip()
        +FinishTrip()
        +Cancel()
        +Timeout()
        +ConfirmPayment()
    }
    class Pol {
        +Guid Id
        +VehicleType VehicleType
        +decimal Base
        +decimal PriceKm
        +decimal CommRate
        +DateTime CreatedAt
        +CalculateFare()
    }
    class Rev {
        +Guid Id
        +Guid DriverId
        +Guid PassengerId
        +Guid TripId
        +int Star
        +string Comment
        +DateTime CreatedAt
        +UpdateReview()
    }

    %% Relationships
    Usr <|-- Psg
    Usr <|-- Drv
    Usr <|-- Adm
    Veh <|-- Car
    Veh <|-- Moto
    
    Loc *-- Coord
    Loc *-- Addr
    Route *-- Loc : Pickup/Dropoff
    Trip *-- Route : tripRoute
    Trip *-- Fare : tripFare
    
    Trip --> Psg : passengerId
    Trip --> Drv : driverId
    Drv --> Veh : vehicleId
    Drv --> Loc : position
    Rev --> Drv : driverId
    Rev --> Psg : passengerId
    Rev --> Trip : tripId

    %% Styles
    style Usr fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Psg fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Drv fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Adm fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Veh fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Car fill:#fff3cd,stroke:#ffc107,stroke-width:1px
    style Moto fill:#fff3cd,stroke:#ffc107,stroke-width:1px
    style Trip fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Pol fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    style Rev fill:#fff3cd,stroke:#ffc107,stroke-width:2px
    
    style Coord fill:#e2e3e5,stroke:#6c757d,stroke-width:1px
    style Addr fill:#e2e3e5,stroke:#6c757d,stroke-width:1px
    style Loc fill:#e2e3e5,stroke:#6c757d,stroke-width:1px
    style Route fill:#e2e3e5,stroke:#6c757d,stroke-width:1px
    style Fare fill:#e2e3e5,stroke:#6c757d,stroke-width:1px
```

## 2. Sơ đồ Application Services & Repositories

```mermaid
classDiagram
    direction LR

    %% Repository Interfaces
    class IJsonRepository~T~ {
        <<interface>>
        +ReadAsync()
        +GetByIdAsync()
        +CreateAsync()
        +UpdateAsync()
        +DeleteAsync()
    }
    class IUsrRepo { <<interface>> +GetByPhoneAsync() }
    class ITripRepo { <<interface>> +GetByDriverIdAsync() }
    class IVehRepo { <<interface>> +GetByTypeAsync() }
    class IPolRepo { <<interface>> +GetLatestByVehicleTypeAsync() }
    class IRevRepo { <<interface>> +GetByDriverIdAsync() }

    %% Service Interfaces
    class IUsrSvc { <<interface>> +LoginAsync() +RegisterPassengerAsync() }
    class ITripCmd { <<interface>> +CreateTripAsync() +AssignDriverAsync() }
    class ITripQry { <<interface>> +GetTripByIdAsync() +GetActiveTripForDriverAsync() }
    class IDrvCmd { <<interface>> +GoOnlineAsync() +AcceptTripAsync() }
    class IDrvQry { <<interface>> +GetOnlineDriversAsync() +GetNearbyDriversAsync() }
    class IMatchSvc { <<interface>> +FindBestDriversAsync() +TryAssignDriverAsync() }
    class IMapSvc { <<interface>> +SearchAsync() +GetDirectionsAsync() }
    class IFareSvc { <<interface>> +CalculateFareAsync() }
    class IAdmSvc { <<interface>> +CreatePolicyAsync() +GetTripStatisticsAsync() }

    %% Implementations
    class UsrSvc
    class TripCmd
    class TripQry
    class DrvCmd
    class DrvQry
    class MatchSvc
    class MapSvc
    class FareSvc
    class AdmSvc

    %% Relationships
    IJsonRepository <|-- IUsrRepo
    IJsonRepository <|-- ITripRepo
    IJsonRepository <|-- IVehRepo
    IJsonRepository <|-- IPolRepo
    IJsonRepository <|-- IRevRepo
    
    IUsrSvc <|.. UsrSvc
    ITripCmd <|.. TripCmd
    ITripQry <|.. TripQry
    IDrvCmd <|.. DrvCmd
    IDrvQry <|.. DrvQry
    IMatchSvc <|.. MatchSvc
    IMapSvc <|.. MapSvc
    IFareSvc <|.. FareSvc
    IAdmSvc <|.. AdmSvc

    UsrSvc --> IUsrRepo
    TripCmd --> ITripRepo
    TripCmd --> IMatchSvc
    TripCmd --> IMapSvc
    TripCmd --> IFareSvc
    DrvCmd --> IMatchSvc
    AdmSvc --> IPolRepo

    %% Styles
    style IJsonRepository fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IUsrRepo fill:#d1ecf1,stroke:#17a2b8,stroke-width:1px
    style ITripRepo fill:#d1ecf1,stroke:#17a2b8,stroke-width:1px
    style IVehRepo fill:#d1ecf1,stroke:#17a2b8,stroke-width:1px
    style IPolRepo fill:#d1ecf1,stroke:#17a2b8,stroke-width:1px
    style IRevRepo fill:#d1ecf1,stroke:#17a2b8,stroke-width:1px
    
    style IUsrSvc fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style ITripCmd fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style ITripQry fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IDrvCmd fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IDrvQry fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IMatchSvc fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IMapSvc fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IFareSvc fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
    style IAdmSvc fill:#d1ecf1,stroke:#17a2b8,stroke-width:2px
```

## 3. Sơ đồ Mẫu thiết kế (State, Factory, Observer)

```mermaid
classDiagram
    direction LR

    %% State Pattern
    class IDriverState { <<interface>> +Status +SetOnline() +SetOnTrip() +SetOffline() }
    class ITripState { <<interface>> +Status +StartSearching() +AssignDriver() +BeginTrip() +FinishTrip() +Cancel() }
    class DriverStateFactory { +Create(status) IDriverState }
    class TripStateFactory { +Create(status) ITripState }
    
    Drv *-- IDriverState : currentState
    Trip *-- ITripState : currentState
    DriverStateFactory ..> IDriverState : creates
    TripStateFactory ..> ITripState : creates

    %% Factory Pattern
    class UserFactory { +CreateAdmin() +CreatePassenger() +CreateDriver() }
    class VehicleFactory { +CreateVehicle() }
    UserFactory ..> Usr : creates
    VehicleFactory ..> Veh : creates

    %% Observer Pattern
    class StatusMonitorBase~T~ { <<abstract>> +OnNext(T value) +Unsubscribe() }
    class DriverStatusMonitor { +Subscribe(Drv driver) }
    class TripStatusMonitor { +Subscribe(Trip trip) }
    class EventObservable~T~ { +Subscribe(IObserver observer) }
    
    StatusMonitorBase <|-- DriverStatusMonitor
    StatusMonitorBase <|-- TripStatusMonitor
    DriverStatusMonitor ..> EventObservable : uses
    TripStatusMonitor ..> EventObservable : uses
    
    Drv ..> DriverStatusChangedEventArgs : raises
    Trip ..> TripStatusChangedEventArgs : raises
    DriverStatusMonitor ..> Drv : observes
    TripStatusMonitor ..> Trip : observes

    %% Styles
    style IDriverState fill:#f8d7da,stroke:#dc3545,stroke-width:2px
    style ITripState fill:#f8d7da,stroke:#dc3545,stroke-width:2px
    style DriverStateFactory fill:#f8d7da,stroke:#dc3545,stroke-width:1px
    style TripStateFactory fill:#f8d7da,stroke:#dc3545,stroke-width:1px
    style UserFactory fill:#f8d7da,stroke:#dc3545,stroke-width:1px
    style VehicleFactory fill:#f8d7da,stroke:#dc3545,stroke-width:1px
    style StatusMonitorBase fill:#f8d7da,stroke:#dc3545,stroke-width:2px
    style DriverStatusMonitor fill:#f8d7da,stroke:#dc3545,stroke-width:1px
    style TripStatusMonitor fill:#f8d7da,stroke:#dc3545,stroke-width:1px