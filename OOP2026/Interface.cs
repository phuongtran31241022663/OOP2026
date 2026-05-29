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
}
// Service Interfaces
public interface IUsrSvc
{
    Task<Usr> LoginAsync(string phone, string password);
    Task<Psg> RegisterPassengerAsync(string name, string phone, string password);
    Task<Drv> RegisterDriverAsync(string name, string phone, string password, string licenseNumber,
        VehicleType vehicleType, string plate, string brand, string model, string color, int capacity,
        Loc initialPosition);
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
    Task ArrivedPickupAtPickupAsync(Guid tripId);
    Task StartTripAsync(Guid tripId);
    Task ArrivedPickupAtDropoffAsync(Guid tripId);
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
public class WalletChangedEventArgs : EventArgs
{
    public Guid DriverId { get; }
    public decimal NewBalance { get; }
    public WalletChangedEventArgs(Guid driverId, decimal newBalance)
    {
        DriverId = driverId;
        NewBalance = newBalance;
    }
}

public interface IWalletSvc
{
    event EventHandler<WalletChangedEventArgs>? WalletChanged;
    Task<decimal> GetWalletAsync(Guid driverId);
    Task<decimal> GetIncomeAsync(Guid driverId);
    Task DepositAsync(Guid driverId, decimal amount);
}
public interface IVehSvc
{
    Task<Veh> CreateVehicleAsync(Guid driverId, VehicleType type, string plateNumber, string brand, string model, string color, int capacity);
    Task<List<Veh>> GetVehiclesByDriverAsync(Guid driverId);
}
public interface IAdmSvc
{
    Task<Pol> CreatePolicyAsync(VehicleType vehicleType, decimal baseFare, decimal pricePerKm, decimal commissionRate);
    Task<(int Total, int Completed, int Cancelled, int Timeout, int Active, double CompletionRate)> GetTripStatisticsAsync();

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
    bool IsPendingDriver(Guid tripId, Guid driverId);
    int GetPendingCount(Guid tripId);
    void ClearPendingDrivers(Guid tripId);
    void RemovePendingDriver(Guid tripId, Guid driverId);
}
public class TripReviewedEventArgs : EventArgs
{
    public Guid TripId { get; }
    public Guid DriverId { get; }
    public int Rating { get; }
    public TripReviewedEventArgs(Guid tripId, Guid driverId, int rating)
    {
        TripId = tripId;
        DriverId = driverId;
        Rating = rating;
    }
}

public interface IRevSvc
{
    event EventHandler<TripReviewedEventArgs>? TripReviewed;
    Task CreateReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment);
    Task UpdateReviewAsync(Guid reviewId, int rating, string comment);
    Task DeleteReviewAsync(Guid reviewId);
}

public interface INotiSvc
{
    string GetDriverNotificationMessage(DriverStatusChangedEventArgs e);
    string GetPassengerNotificationMessage(TripStatusChangedEventArgs e);
    string GetDriverAcceptedMessage();
    string GetDriverCancelledMessage();
    string GetArrivedPickupPickupMessage();
    string GetDriverStartTripMessage();
    string GetDriverCompleteTripMessage();
    string GetPassengerRequestSentMessage();
    string GetPassengerDriverFoundMessage();
    string GetPassengerArrivedPickupMessage();
    string GetPassengerTripStartedMessage();
    string GetPassengerTripCompletedMessage();
}
