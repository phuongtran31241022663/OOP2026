using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    // định làm mô phỏng cho 1 điểm chấm trượt theo route, xóa route đoạn chạy qua
    public class SimulationService : ISimulationService
    {
        private Timer _timer;
        private ITripService _tripService;
        private ITripRepository _tripRepository;
        private IDriverRepository _driverRepository;
        private IVehicleRepository _vehicleRepository;
        private readonly HashSet<Guid> _simulatingTrips = new HashSet<Guid>();
        private readonly object _simulatingTripsLock = new object();
        private readonly SemaphoreSlim _tickLock = new SemaphoreSlim(1, 1);
        private static readonly Random _random = new Random();
        private static readonly object _randomLock = new object();
        private readonly IMapService _mapService;

        public SimulationService(ITripService tripService, IDriverRepository driverRepository, IPassengerRepository passengerRepository, IMapService mapService)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _mapService = mapService ?? throw new ArgumentNullException(nameof(mapService));
        }

        public void SetRepositories(ITripRepository tripRepo, IDriverRepository driverRepo, IVehicleRepository vehicleRepo)
        {
            _tripRepository = tripRepo;
            _driverRepository = driverRepo;
            _vehicleRepository = vehicleRepo;
        }

        public void StartSimulation()
        {
            if (_timer != null)
            {
                return;
            }

            _timer = new Timer(OnTimerTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        public void StopSimulation()
        {
            Timer timer = _timer;
            _timer = null;

            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
            }
        }

        public void StartTripSimulation(Guid tripId)
        {
            AddSimulatingTrip(tripId);
        }

        public bool IsTripSimulating(Guid tripId)
        {
            return ContainsSimulatingTrip(tripId);
        }

        private void OnTimerTick(object state)
        {
            Tick().ContinueWith(
                task =>
                {
                    if (task.Exception != null)
                    {
                        System.Diagnostics.Debug.WriteLine("[SimulationService] Tick failed: " + task.Exception.GetBaseException().Message);
                    }
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        public async Task Tick()
        {
            if (_tripRepository == null || _driverRepository == null || _vehicleRepository == null)
            {
                return;
            }

            if (!await _tickLock.WaitAsync(0))
            {
                return;
            }

            try
            {
                List<Domain.Entities.Trip> allTrips = await _tripRepository.GetAllAsync();
                List<Domain.Entities.Trip> searchingTrips = new List<Domain.Entities.Trip>();

                for (int i = 0; i < allTrips.Count; i++)
                {
                    Domain.Entities.Trip trip = allTrips[i];
                    if (trip.IsSearching())
                    {
                        searchingTrips.Add(trip);
                    }
                }

                for (int i = 0; i < searchingTrips.Count; i++)
                {
                    Domain.Entities.Trip trip = searchingTrips[i];

                    if (ContainsSimulatingTrip(trip.Id))
                    {
                        continue;
                    }

                    List<Domain.Entities.Users.Driver> availableDrivers = await _driverRepository.GetAvailableDriversAsync();
                    List<Domain.Entities.Users.Driver> matchingDrivers = new List<Domain.Entities.Users.Driver>();

                    for (int j = 0; j < availableDrivers.Count; j++)
                    {
                        Domain.Entities.Users.Driver driver = availableDrivers[j];
                        Domain.Entities.Vehicle vehicle = await _vehicleRepository.GetByIdAsync(driver.VehicleId);

                        if (vehicle != null && string.Equals(vehicle.TypeName, trip.TripVehicleType, StringComparison.OrdinalIgnoreCase))
                        {
                            matchingDrivers.Add(driver);
                        }

                    }

                    if (matchingDrivers.Count > 0)
                    {
                        int selectedIndex;
                        lock (_randomLock)
                        {
                            selectedIndex = _random.Next(matchingDrivers.Count);
                        }

                        Domain.Entities.Users.Driver selectedDriver = matchingDrivers[selectedIndex];
                        bool success = await MatchDriverToTrip(trip.Id, selectedDriver.Id);

                        if (success)
                        {
                            AddSimulatingTrip(trip.Id);
                            SimulateTripProgress(trip.Id).ContinueWith(
                                task =>
                                {
                                    if (task.Exception != null)
                                    {
                                        System.Diagnostics.Debug.WriteLine("[SimulationService] SimulateTripProgress failed: " + task.Exception.GetBaseException().Message);
                                    }
                                },
                                TaskContinuationOptions.OnlyOnFaulted);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[SimulationService] Tick error: " + ex.Message);
            }
            finally
            {
                _tickLock.Release();
            }
        }

        private async Task<bool> MatchDriverToTrip(Guid tripId, Guid driverId)
        {
            Domain.Entities.Trip trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null || !trip.IsSearching())
            {
                return false;
            }

            Domain.Entities.Users.Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null || !driver.IsAvailable())
            {
                return false;
            }

            try
            {
                Domain.Entities.Vehicle vehicle = await _vehicleRepository.GetByIdAsync(driver.VehicleId);
                if (vehicle == null)
                {
                    return false;
                }

                if (!string.Equals(vehicle.TypeName, trip.TripVehicleType, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }


                if (driver.Wallet.Amount < trip.TripFare.Commission.Amount)
                {
                    return false;
                }

                // Use TripService to ensure events are fired properly
                await _tripService.MatchDriverAsync(tripId, driverId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[SimulationService] MatchDriverToTrip error: " + ex.Message);
                return false;
            }
        }

        public async Task SimulateTripProgress(Guid tripId)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(3));

                Domain.Entities.Trip trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsMatched())
                {
                    RemoveSimulatingTrip(tripId);
                    return;
                }

                // Use TripService to ensure events are fired properly
                await _tripService.MarkAsArrivedAsync(tripId);

                await Task.Delay(TimeSpan.FromSeconds(3));

                trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsArrived())
                {
                    RemoveSimulatingTrip(tripId);
                    return;
                }

                // Use TripService to ensure events are fired properly
                await _tripService.StartTripAsync(tripId);

                await Task.Delay(TimeSpan.FromSeconds(5));

                trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsStarted())
                {
                    RemoveSimulatingTrip(tripId);
                    return;
                }

                // Use TripService to ensure events are fired properly
                await _tripService.CompleteTripAsync(tripId);
                RemoveSimulatingTrip(tripId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[SimulationService] SimulateTripProgress error: " + ex.Message);
                RemoveSimulatingTrip(tripId);
            }
        }

        private void AddSimulatingTrip(Guid tripId)
        {
            lock (_simulatingTripsLock)
            {
                _simulatingTrips.Add(tripId);
            }
        }

        private bool ContainsSimulatingTrip(Guid tripId)
        {
            lock (_simulatingTripsLock)
            {
                return _simulatingTrips.Contains(tripId);
            }
        }

        private void RemoveSimulatingTrip(Guid tripId)
        {
            lock (_simulatingTripsLock)
            {
                _simulatingTrips.Remove(tripId);
            }
        }
    }

    public sealed class AppServiceBundle
    {
        public IUserService UserService { get; private set; }
        public ITripService TripService { get; private set; }
        public IFareService FareService { get; private set; }
        public ISimulationService SimulationService { get; private set; }
        public IAdminService AdminService { get; private set; }
        public IMatchingService MatchingService { get; private set; }
        public IReviewService ReviewService { get; private set; }
        public IMapService MapService { get; private set; }

        public IVehicleRepository VehicleRepository { get; private set; }

        public static async Task<AppServiceBundle> CreateDefaultAsync()
        {
            IDriverRepository driverRepository = new DriverRepository();
            IPassengerRepository passengerRepository = new PassengerRepository();
            ITripRepository tripRepository = new TripRepository();
            IFareRuleRepository fareRuleRepository = new FareRuleRepository();
            IReviewRepository reviewRepository = new ReviewRepository();
            IVehicleRepository vehicleRepository = new VehicleRepository();
            IUserRepository userRepository = new UserRepository();

            await driverRepository.InitializeAsync();
            await passengerRepository.InitializeAsync();
            await tripRepository.InitializeAsync();
            await fareRuleRepository.InitializeAsync();
            await fareRuleRepository.EnsureSeededAsync();
            await reviewRepository.InitializeAsync();
            await vehicleRepository.InitializeAsync();
            await userRepository.InitializeAsync();

            // Seed initial data
            await Infrastructure.Data.DataSeeder.SeedAsync(
                driverRepository,
                passengerRepository,
                tripRepository,
                vehicleRepository,
                userRepository);

            IUserService userService = new UserService(driverRepository, passengerRepository);
            var httpClient = new HttpClient();
            IMapService mapService = new MapService(httpClient);
            FareService fareService = new FareService(fareRuleRepository);
            ITripService tripService = new TripService(
                tripRepository,
                driverRepository,
                passengerRepository,
                vehicleRepository,
                fareService,
                mapService);

            IAdminService adminService = new AdminService(driverRepository, passengerRepository, tripRepository, fareRuleRepository, reviewRepository);
            IMatchingService matchingService = new MatchingService(tripRepository, driverRepository, vehicleRepository);
            IReviewService reviewService = new ReviewService(reviewRepository, driverRepository, tripRepository);
            ISimulationService simulationService = new SimulationService(tripService, driverRepository, passengerRepository, mapService);

            return new AppServiceBundle
            {
                UserService = userService,
                TripService = tripService,
                FareService = fareService,
                SimulationService = simulationService,
                AdminService = adminService,
                MatchingService = matchingService,
                ReviewService = reviewService,
                MapService = mapService,
                VehicleRepository = vehicleRepository
            };
        }
    }
}