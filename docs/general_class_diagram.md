# RideGo Design Class Diagram (DCD)

Tài liệu này mô tả sơ đồ lớp mức thiết kế (Design Class Diagram - DCD) của RideGo theo UML. Sơ đồ tập trung vào các lớp thật trong code, trách nhiệm nghiệp vụ, mẫu thiết kế và quan hệ dẫn hướng giữa các lớp. Getter/setter, helper nhỏ và chi tiết Designer WinForms được lược bỏ để giữ đúng mức thiết kế.

## Quy ước đọc sơ đồ

- `+`: public
- `-`: private
- `#`: protected
- `<<interface>>`: interface
- `<<abstract>>`: lớp trừu tượng
- `*--`: composition
- `o--`: aggregation
- `-->`: association có dẫn hướng
- `..>`: dependency
- Multiplicity được ghi ở hai đầu quan hệ khi quan trọng cho thiết kế.

## Sơ đồ lớp mức thiết kế

```mermaid
classDiagram
    direction LR

    %% =========================
    %% Domain Entities
    %% =========================
    namespace Domain_Entities {
        class Usr {
            <<abstract>>
            -Guid _id
            -string _name
            -string _phone
            -string _password
            +Guid Id
            +string Name
            +string Phone
            +string Pwd
            +ChangePassword(oldRaw: string, newRaw: string) void
            +UpdateProfile(name: string, phone: string) void
            +VerifyPassword(rawInput: string) bool
        }

        class Drv {
            -IDriverState _currentState
            -DriverStatus _status
            -string _licenseNumber
            -Guid _vehicleId
            -Loc _position
            -decimal _wallet
            -decimal _income
            -int _totalTrips
            -int _ratingSum
            -int _totalReviews
            +DriverStatus Status
            +decimal Wallet
            +decimal Income
            +decimal AvgRat
            +SetOnline() void
            +SetOnTrip() void
            +SetOffline() void
            +PayCommission(fare: Fare) void
            +UpdatePosition(newPosition: Loc) void
            +Deposit(amount: decimal) void
            +AddIncome(amount: decimal) void
            +AddReview(rating: int) void
            +IncrementTotalTrips() void
            ~TransitionTo(newState: IDriverState) void
        }

        class Psg {
            -int _totalTrips
            +int TotTrip
            +IncrementTotalTrips() void
        }

        class Adm {
        }

        class Veh {
            <<abstract>>
            -Guid _id
            -Guid _driverId
            -string _plateNumber
            -string _brand
            -string _model
            -string _color
            -int _capacity
            +Guid Id
            +Guid DriverId
            +string Plate
            +string Brand
            +string Model
            +string Color
            +int Capacity
            +VehicleType Type
            +LinkDriver(driverId: Guid) void
            +UnlinkDriver() void
        }

        class Car {
            +VehicleType Type
        }

        class Moto {
            +VehicleType Type
        }

        class Trip {
            -Guid _id
            -Guid _passengerId
            -VehicleType _tripVehicleType
            -Fare _tripFare
            -Route _tripRoute
            -DateTime _requestAt
            -List~Guid~ _pendingDriverIds
            -Dictionary~Guid, DateTime~ _pendingExpiry
            -Guid? _driverId
            -bool _isPaid
            -bool _isReviewed
            -ITripState _currentState
            -TripStatus _status
            +Guid Id
            +Guid PassengerId
            +Guid? DriverId
            +TripStatus Status
            +int PendingCount
            +StartSearching() void
            +AssignDriver(driverId: Guid) void
            +DriverArrived() void
            +BeginTrip() void
            +FinishTrip() void
            +Cancel(reason: string) void
            +Timeout() void
            +ConfirmPayment() void
            +ProposeDrivers(driverIds: IEnumerable~Guid~, expirySeconds: int) void
            +TryAssignFromPending(driverId: Guid) bool
            +RemovePendingDriver(driverId: Guid) bool
            +GetValidPendingDrivers() IReadOnlyList~Guid~
            +MarkAsReviewed() void
            ~SetDriverId(driverId: Guid) void
            ~MarkPaymentAsSettled() void
            ~TransitionTo(newState: ITripState) void
        }

        class Pol {
            -Guid _id
            -VehicleType _vehicleType
            -decimal _baseFare
            -decimal _pricePerKm
            -decimal _commissionRate
            -DateTime _createdAt
            +Guid Id
            +VehicleType VehicleType
            +decimal Base
            +decimal PriceKm
            +decimal CommRate
            +DateTime CreatedAt
            +CalculateFare(distance: decimal) Fare
        }

        class Rev {
            -Guid _id
            -Guid _driverId
            -Guid _passengerId
            -Guid _tripId
            -int _star
            -string _comment
            -DateTime _createdAt
            +Guid Id
            +Guid DriverId
            +Guid PassengerId
            +Guid TripId
            +int Star
            +string Comment
            +DateTime CreatedAt
            +UpdateReview(star: int, comment: string) void
        }
    }

    Usr <|-- Drv
    Usr <|-- Psg
    Usr <|-- Adm
    Veh <|-- Car
    Veh <|-- Moto

    %% =========================
    %% Value Objects
    %% =========================
    namespace Value_Objects {
        class ValueObject {
            <<abstract>>
            #GetEqualityComponents() IEnumerable~object~
            +Equals(obj: object) bool
            +GetHashCode() int
        }

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
            +string? OsmValue
            +string? HouseNumber
            +string? Locality
            +ToString() string
        }

        class Loc {
            +Coord Coord
            +Addr Addr
            +ToString() string
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
    }

    ValueObject <|-- Coord
    ValueObject <|-- Addr
    ValueObject <|-- Loc
    ValueObject <|-- Route
    ValueObject <|-- Fare

    Loc "1" *-- "1" Coord : coord
    Loc "1" *-- "1" Addr : addr
    Route "1" *-- "1" Loc : pickup
    Route "1" *-- "1" Loc : dropoff
    Trip "1" *-- "1" Route : tripRoute
    Trip "1" *-- "1" Fare : tripFare
    Drv "1" --> "1" Loc : position
    Trip "1" --> "0..1" Drv : assignedDriverId
    Trip "1" --> "1" Psg : passengerId
    Drv "1" --> "1" Veh : vehicleId
    Rev "0..*" --> "1" Drv : driverId
    Rev "0..*" --> "1" Psg : passengerId
    Rev "0..*" --> "1" Trip : tripId

    %% =========================
    %% State Pattern
    %% =========================
    namespace State_Pattern {
        class IDriverState {
            <<interface>>
            +DriverStatus Status
            +SetOnline(driver: Drv) void
            +SetOnTrip(driver: Drv) void
            +SetOffline(driver: Drv) void
        }

        class DriverOfflineState {
            +DriverStatus Status
            +SetOnline(driver: Drv) void
            +SetOnTrip(driver: Drv) void
            +SetOffline(driver: Drv) void
        }

        class DriverOnlineState {
            +DriverStatus Status
            +SetOnline(driver: Drv) void
            +SetOnTrip(driver: Drv) void
            +SetOffline(driver: Drv) void
        }

        class DriverOnTripState {
            +DriverStatus Status
            +SetOnline(driver: Drv) void
            +SetOnTrip(driver: Drv) void
            +SetOffline(driver: Drv) void
        }

        class ITripState {
            <<interface>>
            +TripStatus Status
            +StartSearching(trip: Trip) void
            +AssignDriver(trip: Trip, driverId: Guid) void
            +DriverArrived(trip: Trip) void
            +BeginTrip(trip: Trip) void
            +FinishTrip(trip: Trip) void
            +ConfirmPayment(trip: Trip) void
            +Cancel(trip: Trip, reason: string) void
            +Timeout(trip: Trip) void
        }

        class AbstractTripState {
            <<abstract>>
            +TripStatus Status
            #ChangeState(trip: Trip, newState: ITripState) void
        }

        class TripPendingState
        class TripSearchingState
        class TripMatchedState
        class TripArrivedState
        class TripStartedState
        class TripDropOffState
        class TripCompletedState
        class TripCancelledState
        class TripTimeoutState
    }

    IDriverState <|.. DriverOfflineState
    IDriverState <|.. DriverOnlineState
    IDriverState <|.. DriverOnTripState
    Drv "1" *-- "1" IDriverState : currentState

    ITripState <|.. AbstractTripState
    AbstractTripState <|-- TripPendingState
    AbstractTripState <|-- TripSearchingState
    AbstractTripState <|-- TripMatchedState
    AbstractTripState <|-- TripArrivedState
    AbstractTripState <|-- TripStartedState
    AbstractTripState <|-- TripDropOffState
    AbstractTripState <|-- TripCompletedState
    AbstractTripState <|-- TripCancelledState
    AbstractTripState <|-- TripTimeoutState
    Trip "1" *-- "1" ITripState : currentState

    %% =========================
    %% Repository Pattern
    %% =========================
    namespace Repository_Pattern {
        class IReadOnlyJsonRepository~T~ {
            <<interface>>
            +ReadAsync() Task~List~T~~
            +GetByIdAsync(id: Guid) Task~T?~
        }

        class IJsonRepository~T~ {
            <<interface>>
            +CreateAsync(entity: T) Task
            +UpdateAsync(entity: T) Task
            +DeleteAsync(id: Guid) Task
        }

        class JsonRepository~T~ {
            -string _filePath
            -SemaphoreSlim _fileLock
            -List~T~ _items
            #EnsureLoadedAsync() Task
            -SaveInternalAsync() Task
            +ReadAsync() Task~List~T~~
            +GetByIdAsync(id: Guid) Task~T?~
            +CreateAsync(entity: T) Task
            +UpdateAsync(entity: T) Task
            +DeleteAsync(id: Guid) Task
            +Dispose() void
        }

        class IUsrRepo {
            <<interface>>
            +GetByPhoneAsync(phone: string) Task~Usr?~
            +GetPassengerByIdAsync(id: Guid) Task~Psg?~
            +GetDriverByIdAsync(id: Guid) Task~Drv?~
        }

        class ITripRepo {
            <<interface>>
            +GetByDriverIdAsync(driverId: Guid) Task~List~Trip~~
            +GetByPassengerIdAsync(passengerId: Guid) Task~List~Trip~~
        }

        class IVehRepo {
            <<interface>>
            +GetByTypeAsync(type: VehicleType) Task~List~Veh~~
        }

        class IPolRepo {
            <<interface>>
            +GetLatestByVehicleTypeAsync(vehicleType: VehicleType) Task~Pol?~
            +CreateAsync(entity: Pol) Task
        }

        class IRevRepo {
            <<interface>>
            +GetByDriverIdAsync(driverId: Guid) Task~List~Rev~~
            +GetByTripIdAsync(tripId: Guid) Task~List~Rev~~
            +GetByPassengerIdAsync(passengerId: Guid) Task~List~Rev~~
        }

        class UsrRepo
        class TripRepo
        class VehRepo
        class PolRepo
        class RevRepo
    }

    IReadOnlyJsonRepository <|-- IJsonRepository
    IJsonRepository <|.. JsonRepository
    IJsonRepository <|-- IUsrRepo
    IJsonRepository <|-- ITripRepo
    IJsonRepository <|-- IVehRepo
    IReadOnlyJsonRepository <|-- IPolRepo
    IJsonRepository <|-- IRevRepo

    JsonRepository <|-- UsrRepo
    JsonRepository <|-- TripRepo
    JsonRepository <|-- RevRepo
    UsrRepo ..|> IUsrRepo
    TripRepo ..|> ITripRepo
    VehRepo ..|> IVehRepo
    PolRepo ..|> IPolRepo
    RevRepo ..|> IRevRepo
    VehRepo "1" *-- "1" JsonRepository : inner
    PolRepo "1" *-- "1" JsonRepository : inner

    %% =========================
    %% Application Services
    %% =========================
    namespace Application_Services {
        class IUsrSvc {
            <<interface>>
            +LoginAsync(phone: string, password: string) Task~Usr~
            +RegisterPassengerAsync(name: string, phone: string, password: string) Task~Psg~
            +RegisterDriverAsync(...) Task~Drv~
            +ChangePasswordAsync(userId: Guid, oldPassword: string, newPassword: string) Task
            +UpdateProfileAsync(userId: Guid, name: string, phone: string) Task
        }

        class UsrSvc {
            -IUsrRepo _userRepo
            -IVehRepo _vehicleRepo
            +LoginAsync(phone: string, password: string) Task~Usr~
            +RegisterPassengerAsync(name: string, phone: string, password: string) Task~Psg~
            +RegisterDriverAsync(...) Task~Drv~
            +ChangePasswordAsync(userId: Guid, oldPassword: string, newPassword: string) Task
            +UpdateProfileAsync(userId: Guid, name: string, phone: string) Task
        }

        class IAdmSvc {
            <<interface>>
            +CreatePolicyAsync(type: VehicleType, baseFare: decimal, pricePerKm: decimal, commissionRate: decimal) Task~Pol~
            +GetTripStatisticsAsync() Task
            +GetTotalRevenueAsync() Task~decimal~
            +GetTotalCommissionAsync() Task~decimal~
        }

        class AdmSvc {
            -IUsrRepo _userRepo
            -ITripRepo _tripRepository
            -IPolRepo _policyRepository
            -IRevRepo _reviewRepository
            -IVehRepo _vehicleRepo
            +CreatePolicyAsync(...) Task~Pol~
            +GetAllUsersAsync() Task~List~Usr~~
            +GetAllTripsAsync() Task~List~Trip~~
            +GetTripStatisticsAsync() Task
            +GetTotalRevenueAsync() Task~decimal~
            +GetTotalCommissionAsync() Task~decimal~
        }

        class IFareSvc {
            <<interface>>
            +CalculateFareAsync(vehicleType: VehicleType, distanceKm: decimal) Task~Fare~
        }

        class FareSvc {
            -IPolRepo _policyRepo
            +CalculateFareAsync(vehicleType: VehicleType, distanceKm: decimal) Task~Fare~
        }

        class IPsgSvc {
            <<interface>>
            +RequestTripAsync(passengerId: Guid, pickup: Loc, dropoff: Loc, vehicleType: VehicleType) Task~Trip~
            +CancelTripAsync(passengerId: Guid, tripId: Guid, reason: string) Task
        }

        class PsgSvc {
            -IUsrRepo _userRepo
            -ITripCmd _tripCmdService
            -ITripQry _tripQueryService
            -IRevSvc _reviewService
            -IMapSvc _mapService
            -IFareSvc _fareService
            +RequestTripAsync(passengerId: Guid, pickup: Loc, dropoff: Loc, vehicleType: VehicleType) Task~Trip~
            +CancelTripAsync(passengerId: Guid, tripId: Guid, reason: string) Task
        }

        class ITripCmd {
            <<interface>>
            +CreateTripAsync(passengerId: Guid, route: Route, fare: Fare, vehicleType: VehicleType) Task~Trip~
            +AssignDriverAsync(tripId: Guid, driverId: Guid) Task
            +DriverArrivedAtPickupAsync(tripId: Guid) Task
            +StartTripAsync(tripId: Guid) Task
            +DriverArrivedAtDropoffAsync(tripId: Guid) Task
            +CompleteTripAsync(tripId: Guid) Task
            +CancelTripAsync(tripId: Guid, reason: string) Task
        }

        class TripCmd {
            -ITripRepo _tripRepository
            -IUsrRepo _userRepo
            -IVehRepo _vehicleRepo
            -IFareSvc _fareService
            -IMapSvc _mapService
            -IMatchSvc _matchingService
            -Stack~Trip~ _recentTrips
            +CreateTripAsync(passengerId: Guid, route: Route, fare: Fare, vehicleType: VehicleType) Task~Trip~
            +AssignDriverAsync(tripId: Guid, driverId: Guid) Task
            +DriverArrivedAtPickupAsync(tripId: Guid) Task
            +StartTripAsync(tripId: Guid) Task
            +DriverArrivedAtDropoffAsync(tripId: Guid) Task
            +CompleteTripAsync(tripId: Guid) Task
            +CancelTripAsync(tripId: Guid, reason: string) Task
        }

        class ITripQry {
            <<interface>>
            +GetTripByIdAsync(tripId: Guid) Task~Trip?~
            +GetTripsByPassengerAsync(passengerId: Guid) Task~List~Trip~~
            +GetTripsByDriverAsync(driverId: Guid) Task~List~Trip~~
            +GetPendingTripsAsync() Task~List~Trip~~
            +GetActiveTripForDriverAsync(driverId: Guid) Task~Trip?~
            +GetActiveTripForPassengerAsync(passengerId: Guid) Task~Trip?~
        }

        class TripQry {
            -ITripRepo _tripRepository
            +GetTripByIdAsync(tripId: Guid) Task~Trip?~
            +GetTripsByPassengerAsync(passengerId: Guid) Task~List~Trip~~
            +GetTripsByDriverAsync(driverId: Guid) Task~List~Trip~~
            +GetPendingTripsAsync() Task~List~Trip~~
            +GetActiveTripForDriverAsync(driverId: Guid) Task~Trip?~
            +GetActiveTripForPassengerAsync(passengerId: Guid) Task~Trip?~
        }

        class IDrvCmd {
            <<interface>>
            +GoOnlineAsync(driverId: Guid) Task
            +GoOfflineAsync(driverId: Guid) Task
            +AcceptTripAsync(driverId: Guid, tripId: Guid) Task
            +RejectTripAsync(driverId: Guid, tripId: Guid) Task
            +UpdateLocationAsync(driverId: Guid, newLocation: Loc) Task
        }

        class DrvCmd {
            -IUsrRepo _userRepo
            -ITripRepo _tripRepository
            -IMatchSvc _matchingService
            -InMemoryDriverGrid _driverGrid
            +GoOnlineAsync(driverId: Guid) Task
            +GoOfflineAsync(driverId: Guid) Task
            +AcceptTripAsync(driverId: Guid, tripId: Guid) Task
            +RejectTripAsync(driverId: Guid, tripId: Guid) Task
            +UpdateLocationAsync(driverId: Guid, newLocation: Loc) Task
        }

        class IDrvQry {
            <<interface>>
            +GetOnlineDriversAsync() Task~List~Drv~~
            +GetNearbyDriversAsync(passengerLocation: Loc, radiusKm: double) Task~List~Drv~~
            +GetDriverByIdAsync(driverId: Guid) Task~Drv~
            +GetVehicleByIdAsync(vehicleId: Guid) Task~Veh?~
        }

        class DrvQry {
            -IUsrRepo _userRepo
            -IVehRepo _vehicleRepo
            +GetOnlineDriversAsync() Task~List~Drv~~
            +GetNearbyDriversAsync(passengerLocation: Loc, radiusKm: double) Task~List~Drv~~
            +GetDriverByIdAsync(driverId: Guid) Task~Drv~
            +GetVehicleByIdAsync(vehicleId: Guid) Task~Veh?~
        }

        class IWalletSvc {
            <<interface>>
            +GetWalletAsync(driverId: Guid) Task~decimal~
            +GetIncomeAsync(driverId: Guid) Task~decimal~
            +DepositAsync(driverId: Guid, amount: decimal) Task
        }

        class WalletSvc {
            -IUsrRepo _userRepo
            +GetWalletAsync(driverId: Guid) Task~decimal~
            +GetIncomeAsync(driverId: Guid) Task~decimal~
            +DepositAsync(driverId: Guid, amount: decimal) Task
        }

        class IMapSvc {
            <<interface>>
            +SearchAsync(query: string) Task~List~Loc~~
            +GetAddressAsync(latitude: double, longitude: double) Task~Loc?~
            +GetDirectionsAsync(origin: Loc, dropoff: Loc) Task~Route?~
            +EnsureLocationAsync(address: string, lat: double?, lng: double?) Task~Loc~
        }

        class MapSvc {
            -HttpClient _httpClient
            +SearchAsync(query: string) Task~List~Loc~~
            +GetAddressAsync(latitude: double, longitude: double) Task~Loc?~
            +GetDirectionsAsync(origin: Loc, destination: Loc) Task~Route?~
            +EnsureLocationAsync(address: string, lat: double?, lng: double?) Task~Loc~
        }

        class IMatchSvc {
            <<interface>>
            +FindBestDriversAsync(tripId: Guid) Task~List~Drv~~
            +ProposeDriversForTripAsync(tripId: Guid, maxDrivers: int) Task
            +TryAssignDriverAsync(tripId: Guid, driverId: Guid) Task~bool~
        }

        class MatchSvc {
            -InMemoryDriverGrid _driverGrid
            -ITripRepo _tripRepository
            -IUsrRepo _userRepo
            -IVehRepo _vehicleRepo
            -SemaphoreSlim _matchLock
            +FindBestDriversAsync(tripId: Guid) Task~List~Drv~~
            +ProposeDriversForTripAsync(tripId: Guid, maxDrivers: int) Task
            +TryAssignDriverAsync(tripId: Guid, driverId: Guid) Task~bool~
        }

        class IRevSvc {
            <<interface>>
            +CreateReviewAsync(driverId: Guid, passengerId: Guid, tripId: Guid, rating: int, comment: string) Task
            +UpdateReviewAsync(reviewId: Guid, rating: int, comment: string) Task
            +DeleteReviewAsync(reviewId: Guid) Task
        }

        class RevSvc {
            -IRevRepo _reviewRepo
            -IUsrRepo _userRepo
            -ITripRepo _tripRepo
            +CreateReviewAsync(driverId: Guid, passengerId: Guid, tripId: Guid, rating: int, comment: string) Task
            +UpdateReviewAsync(reviewId: Guid, rating: int, comment: string) Task
            +DeleteReviewAsync(reviewId: Guid) Task
        }

        class InMemoryDriverGrid {
            -ConcurrentDictionary~string, HashSet~Guid~~ _grid
            -ConcurrentDictionary~Guid, Cell~ _driverCells
            +UpdateDriverLocation(driverId: Guid, lat: double, lon: double) void
            +GetDriversInCellAndNeighbors(lat: double, lon: double, radiusCells: int) List~Guid~
            +RemoveDriver(driverId: Guid) void
        }
    }

    IUsrSvc <|.. UsrSvc
    IAdmSvc <|.. AdmSvc
    IFareSvc <|.. FareSvc
    IPsgSvc <|.. PsgSvc
    ITripCmd <|.. TripCmd
    ITripQry <|.. TripQry
    IDrvCmd <|.. DrvCmd
    IDrvQry <|.. DrvQry
    IWalletSvc <|.. WalletSvc
    IMapSvc <|.. MapSvc
    IMatchSvc <|.. MatchSvc
    IRevSvc <|.. RevSvc

    UsrSvc --> IUsrRepo
    UsrSvc --> IVehRepo
    AdmSvc --> IUsrRepo
    AdmSvc --> ITripRepo
    AdmSvc --> IPolRepo
    AdmSvc --> IRevRepo
    AdmSvc --> IVehRepo
    FareSvc --> IPolRepo
    PsgSvc --> IUsrRepo
    PsgSvc --> ITripCmd
    PsgSvc --> ITripQry
    PsgSvc --> IRevSvc
    PsgSvc --> IMapSvc
    PsgSvc --> IFareSvc
    TripCmd --> ITripRepo
    TripCmd --> IUsrRepo
    TripCmd --> IVehRepo
    TripCmd --> IFareSvc
    TripCmd --> IMapSvc
    TripCmd --> IMatchSvc
    TripQry --> ITripRepo
    DrvCmd --> IUsrRepo
    DrvCmd --> ITripRepo
    DrvCmd --> IMatchSvc
    DrvCmd --> InMemoryDriverGrid
    DrvQry --> IUsrRepo
    DrvQry --> IVehRepo
    WalletSvc --> IUsrRepo
    MatchSvc --> InMemoryDriverGrid
    MatchSvc --> ITripRepo
    MatchSvc --> IUsrRepo
    MatchSvc --> IVehRepo
    RevSvc --> IRevRepo
    RevSvc --> IUsrRepo
    RevSvc --> ITripRepo

    %% =========================
    %% Factories
    %% =========================
    namespace Factories {
        class UserFactory {
            +CreateAdmin(name: string, phone: string, password: string) Usr
            +CreatePassenger(name: string, phone: string, password: string) Usr
            +CreateDriver(name: string, phone: string, password: string, licenseNumber: string, vehicleId: Guid, position: Loc) Usr
        }

        class VehicleFactory {
            +CreateVehicle(type: VehicleType, plateNumber: string, brand: string, model: string, color: string, capacity: int) Veh
        }

        class DriverStateFactory {
            +Create(status: DriverStatus) IDriverState
        }

        class TripStateFactory {
            +Create(status: TripStatus) ITripState
        }
    }

    UserFactory ..> Usr : creates
    UserFactory ..> Adm : creates
    UserFactory ..> Psg : creates
    UserFactory ..> Drv : creates
    VehicleFactory ..> Veh : creates
    VehicleFactory ..> Car : creates
    VehicleFactory ..> Moto : creates
    DriverStateFactory ..> IDriverState : creates
    TripStateFactory ..> ITripState : creates

    %% =========================
    %% Presentation Layer (WinForms)
    %% =========================
    namespace Presentation_WinForms {
        class FrmMultiRole {
            +ShowPassenger() void
            +ShowDriver() void
            +OpenAdmin() void
        }

        class FrmAuth
        class FrmAdmin
        class FrmPassenger
        class FrmDriver
        class FrmPassengerAuth
        class FrmDriverAuth
        class Form1

        class ucPassengerHome
        class ucDriverHome
        class ucBooking
        class ucMap
        class ucHistory
        class ucHistoryCard
        class ucReview
        class ucProfile
        class ucTrip
        class ucTripCard
        class ucTripStatus
        class ucDriverCard
        class ucDriverStatus
        class ucFareSelector
        class ucLocationPicker
        class ucRequest
        class ucWallet
        class ucPolicyCard
        class ucStatCard
        class ucUserCount
    }

    FrmMultiRole *-- ucPassengerHome
    FrmMultiRole *-- ucDriverHome
    FrmMultiRole ..> FrmAdmin
    FrmMultiRole ..> FrmAuth
    FrmAuth ..> IUsrSvc
    FrmAdmin ..> IAdmSvc
    ucPassengerHome *-- ucBooking
    ucPassengerHome *-- ucMap
    ucPassengerHome *-- ucHistory
    ucPassengerHome *-- ucProfile
    ucDriverHome *-- ucRequest
    ucDriverHome *-- ucDriverStatus
    ucDriverHome *-- ucWallet
    ucDriverHome *-- ucTripCard
    ucBooking ..> IPsgSvc
    ucBooking ..> IMapSvc
    ucBooking ..> IFareSvc
    ucReview ..> IRevSvc
    ucRequest ..> IDrvCmd
    ucWallet ..> IWalletSvc

    %% =========================
    %% Events / Observer Support
    %% =========================
    namespace Observer_Support {
        class DriverStatusChangedEventArgs {
            +Guid DriverId
            +DriverStatus OldStatus
            +DriverStatus NewStatus
        }

        class TripStatusChangedEventArgs {
            +Guid TripId
            +TripStatus OldStatus
            +TripStatus NewStatus
        }

        class StatusMonitorBase~TEventArgs~ {
            <<abstract>>
            -List~IDisposable~ _unsubscribers
            -List~string~ _log
            +Unsubscribe() void
            +OnCompleted() void
            +OnError(error: Exception) void
            +OnNext(value: TEventArgs) void
            #FormatMessage(args: TEventArgs) string
        }

        class DriverStatusMonitor
        class TripStatusMonitor
        class EventObservable~T~
    }

    StatusMonitorBase <|-- DriverStatusMonitor
    StatusMonitorBase <|-- TripStatusMonitor
    Drv ..> DriverStatusChangedEventArgs : raises
    Trip ..> TripStatusChangedEventArgs : raises
    DriverStatusMonitor ..> Drv : subscribes
    TripStatusMonitor ..> Trip : subscribes
```

## Ghi chú thiết kế

- `Usr`, `Veh`, `ValueObject`, `IDriverState`, `ITripState` là các abstraction trung tâm của mô hình.
- `Trip` và `Drv` là Context trong State Pattern; state hiện tại được giữ bằng composition với `ITripState` hoặc `IDriverState`.
- `JsonRepository<T>` là generic repository dùng chung; các repository cụ thể bổ sung truy vấn nghiệp vụ.
- Các service phụ thuộc vào interface repository/service theo hướng một chiều, đúng với manual dependency injection trong `Program.cs`.
- UI WinForms được đưa vào sơ đồ ở mức thiết kế: chỉ thể hiện form/control chính và dependency đến service, không liệt kê thành phần Designer chi tiết.
- Sơ đồ ưu tiên class và phương thức nghiệp vụ thật trong code, không đưa getter/setter cơ bản vào DCD.
