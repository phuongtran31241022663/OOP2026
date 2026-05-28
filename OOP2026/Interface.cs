namespace OOP2026;

public interface IReadOnlyJsonRepository<T> where T : class
{
    Task<List<T>> ReadAsync();
    Task<T?> GetByIdAsync(Guid id);
}
public interface IJsonRepository<T> : IReadOnlyJsonRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
public interface IPolRepo : IReadOnlyJsonRepository<Pol>
{
    Task<Pol?> GetLatestByVehicleTypeAsync(VehicleType vehicleType);
    Task CreateAsync(Pol entity);
}
public interface IRevRepo : IJsonRepository<Rev>
{
    Task<List<Rev>> GetByDriverIdAsync(Guid driverId);
    Task<List<Rev>> GetByTripIdAsync(Guid tripId);
    Task<List<Rev>> GetByPassengerIdAsync(Guid passengerId);
}
public interface ITripRepo : IJsonRepository<Trip>
{
    Task<List<Trip>> GetByDriverIdAsync(Guid driverId);
    Task<List<Trip>> GetByPassengerIdAsync(Guid passengerId);
}
public interface IUsrRepo : IJsonRepository<Usr>
{
    Task<Usr?> GetByPhoneAsync(string phone);
    Task<Psg?> GetPassengerByIdAsync(Guid id);
    Task<Drv?> GetDriverByIdAsync(Guid id);
}
public interface IVehRepo : IJsonRepository<Veh>
{
    Task<List<Veh>> GetByTypeAsync(VehicleType type);
}
// Service Interfaces
public interface IUsrSvc
{
    Task<Usr> LoginAsync(string phone, string password);
    Task<Psg> RegisterPassengerAsync(string name, string phone, string password);
    Task<Drv> RegisterDriverAsync(string name, string phone, string password, string licenseNumber, string plate, string brand, string model, string color, int capacity, VehicleType vehicleType, Loc initialPosition);
    Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    Task UpdateProfileAsync(Guid userId, string name, string phone);
    Task<Usr?> GetUserByIdAsync(Guid userId);
    Task<Usr> RefreshUserAsync(Guid userId);
    Task<string> GetDriverVehicleSummaryAsync(Guid driverId);
}
public interface ITripCmd
{
    event EventHandler<TripStatusChangedEventArgs>? TripStatusChanged;
    Task<Trip> CreateTripAsync(Guid passengerId, Route route, Fare fare, VehicleType vehicleType);
    Task AssignDriverAsync(Guid tripId, Guid driverId);
    Task DriverArrivedAtPickupAsync(Guid tripId);
    Task StartTripAsync(Guid tripId);
    Task DriverArrivedAtDropoffAsync(Guid tripId);
    Task CompleteTripAsync(Guid tripId);
    Task CancelTripAsync(Guid tripId, string reason);
}
public interface ITripQry
{
    Task<Trip?> GetTripByIdAsync(Guid tripId);
    Task<List<Trip>> GetTripsByPassengerAsync(Guid passengerId);
    Task<List<Trip>> GetTripsByDriverAsync(Guid driverId);
    Task<List<Trip>> GetPendingTripsAsync();
    Task<List<Trip>> GetTripsByStatusAsync(TripStatus status);
    Task<List<Trip>> GetTripsWithPendingDriverAsync(Guid driverId);
    Task<Trip?> GetActiveTripForDriverAsync(Guid driverId);
    Task<Trip?> GetActiveTripForPassengerAsync(Guid passengerId);
    Task<int> GetTotalTripsForDriverAsync(Guid driverId);
}
public interface IDrvCmd
{
    event EventHandler<DriverStatusChangedEventArgs>? DriverStatusChanged;

    Task GoOnlineAsync(Guid driverId);
    Task GoOfflineAsync(Guid driverId);
    Task AcceptTripAsync(Guid driverId, Guid tripId);
    Task RejectTripAsync(Guid driverId, Guid tripId);
    Task UpdateLocationAsync(Guid driverId, Loc newLocation);
}
public interface IDrvQry
{
    Task<List<Drv>> GetOnlineDriversAsync();
    Task<List<Drv>> GetNearbyDriversAsync(Loc passengerLocation, double radiusKm = 5);
    Task<Drv> GetDriverByIdAsync(Guid driverId);
    Task<Veh?> GetVehicleByIdAsync(Guid vehicleId);
    //Task<decimal> GetIncomeAsync(Guid driverId);
}
public interface IWalletSvc
{
    Task<decimal> GetWalletAsync(Guid driverId);
    Task<decimal> GetIncomeAsync(Guid driverId);
    Task DepositAsync(Guid driverId, decimal amount);
}
public interface IVehicleService
{
    Task<Veh> CreateVehicleAsync(Guid driverId, VehicleType type, string plateNumber, string brand, string model, string color, int capacity);
    Task<List<Veh>> GetVehiclesByDriverAsync(Guid driverId);
}
public interface IAdmSvc
{
    Task<Pol> CreatePolicyAsync(VehicleType vehicleType, decimal baseFare, decimal pricePerKm, decimal commissionRate);
    Task<(int Total, int Completed, int Cancelled, double CompletionRate)> GetTripStatisticsAsync();

    Task<decimal> GetTotalRevenueAsync();
    Task<decimal> GetTotalCommissionAsync();
    Task<List<Usr>> GetAllUsersAsync();
    Task<List<Trip>> GetAllTripsAsync();
}
public interface IFareSvc
{
    Task<Fare> CalculateFareAsync(VehicleType vehicleType, decimal distanceKm);
}
public interface IMapSvc
{
    Task<List<Loc>> SearchAsync(string query);
    Task<Loc?> GetAddressAsync(double latitude, double longitude);
    Task<Route?> GetDirectionsAsync(Loc origin, Loc Dropoff);
    Task<Loc> EnsureLocationAsync(string address, double? lat = null, double? lng = null);
}
public interface IPsgSvc
{
    Task<Trip> RequestTripAsync(Guid passengerId, Loc pickup, Loc Dropoff, VehicleType vehicleType);
    Task CancelTripAsync(Guid passengerId, Guid tripId, string reason);
}
public interface IMatchSvc
{
    Task<List<Drv>> FindBestDriversAsync(Guid tripId);
    Task ProposeDriversForTripAsync(Guid tripId, int maxDrivers = 3);
    Task<bool> TryAssignDriverAsync(Guid tripId, Guid driverId);
}
public interface IRevSvc
{
    Task CreateReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment);
    Task UpdateReviewAsync(Guid reviewId, int rating, string comment);
    Task DeleteReviewAsync(Guid reviewId);
}

public interface INotificationSvc
{
    string GetDriverNotificationMessage(DriverStatusChangedEventArgs e);
    string GetPassengerNotificationMessage(TripStatusChangedEventArgs e);
    string GetDriverAcceptedMessage();
    string GetDriverArrivedPickupMessage();
    string GetDriverStartTripMessage();
    string GetDriverCompleteTripMessage();
    string GetPassengerRequestSentMessage();
    string GetPassengerDriverFoundMessage();
    string GetPassengerDriverArrivedMessage();
    string GetPassengerTripStartedMessage();
    string GetPassengerTripCompletedMessage();
}
