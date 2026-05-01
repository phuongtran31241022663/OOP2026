using Application.Interfaces;
using Domain.Enums;
using Domain.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
public class SimulationService : ISimulationService
    {
        private Timer _timer;
        private ITripRepository _tripRepository;
        private IDriverRepository _driverRepository;
        private IVehicleRepository _vehicleRepository;
        private readonly HashSet<Guid> _simulatingTrips = new HashSet<Guid>();

        public SimulationService()
        {
        }

        public void SetRepositories(ITripRepository tripRepo, IDriverRepository driverRepo, IVehicleRepository vehicleRepo)
        {
            _tripRepository = tripRepo;
            _driverRepository = driverRepo;
            _vehicleRepository = vehicleRepo;
        }

        public void StartSimulation()
        {
            _timer = new Timer(async _ => await Tick(), null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        public void StopSimulation()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _timer = null;
        }

        public void StartTripSimulation(Guid tripId)
        {
            _simulatingTrips.Add(tripId);
        }

        public bool IsTripSimulating(Guid tripId) => _simulatingTrips.Contains(tripId);

        public async Task Tick()
        {
            if (_tripRepository == null || _driverRepository == null) return;

            try
            {
                var pendingTrips = await _tripRepository.GetAllAsync();
                var searchingTrips = pendingTrips.Where(t => t.IsSearching()).ToList();

                foreach (var trip in searchingTrips)
                {
                    if (_simulatingTrips.Contains(trip.Id)) continue;

                    var availableDrivers = await _driverRepository.GetAvailableDriversAsync();
                    var matchingDrivers = new List<Domain.Entities.Users.Driver>();

                    foreach (var driver in availableDrivers)
                    {
                        var vehicle = await _vehicleRepository.GetByIdAsync(driver.VehicleId);
                        if (vehicle != null && vehicle.Type == trip.TripVehicleType)
                        {
                            matchingDrivers.Add(driver);
                        }
                    }

                    if (matchingDrivers.Any())
                    {
                        var random = new Random();
                        var selectedDriver = matchingDrivers[random.Next(matchingDrivers.Count)];
                        var success = await MatchDriverToTrip(trip.Id, selectedDriver.Id);
                        if (success)
                        {
                            _simulatingTrips.Add(trip.Id);
                            _ = Task.Run(async () => await SimulateTripProgress(trip.Id));
                        }
                    }
                }
            }
            catch
            {
                // Silently handle errors
            }
        }

        private async Task<bool> MatchDriverToTrip(Guid tripId, Guid driverId)
        {
            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null || !trip.IsSearching()) return false;

            var driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null || !driver.IsAvailable()) return false;

            try
            {
                trip.MatchDriver(driverId);
                driver.SetOnTrip();
await _tripRepository.UpdateAsync(trip);
                await _driverRepository.UpdateAsync(driver);
                await _tripRepository.SaveChangesAsync();
                await _driverRepository.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task SimulateTripProgress(Guid tripId)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            try
            {
                var trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsMatched()) return;
                trip.MarkAsArrived();
                await _tripRepository.UpdateAsync(trip);
                await _tripRepository.SaveChangesAsync();
            }
            catch { }

            await Task.Delay(TimeSpan.FromSeconds(3));
            try
            {
                var trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsArrived()) return;
                trip.StartTrip();
                await _tripRepository.UpdateAsync(trip);
                await _tripRepository.SaveChangesAsync();
            }
            catch { }

            await Task.Delay(TimeSpan.FromSeconds(5));
            try
            {
                var trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null || !trip.IsStarted()) return;
                trip.CompleteTrip();
                if (trip.DriverId.HasValue)
                {
                    var driver = await _driverRepository.GetByIdAsync(trip.DriverId.Value);
                    if (driver != null)
                    {
                        driver.SetAvailable();
                        driver.AddTrip();
                        await _driverRepository.UpdateAsync(driver);
                    }
                }
                await _tripRepository.UpdateAsync(trip);
                await _tripRepository.SaveChangesAsync();
                await _driverRepository.SaveChangesAsync();
                _simulatingTrips.Remove(tripId);
            }
            catch { }
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

            await driverRepository.InitializeAsync();
            await passengerRepository.InitializeAsync();
            await tripRepository.InitializeAsync();
            await fareRuleRepository.InitializeAsync();
            await fareRuleRepository.EnsureSeededAsync();
            await reviewRepository.InitializeAsync();
            await vehicleRepository.InitializeAsync();

            IUserService userService = new UserService(driverRepository, passengerRepository);
            IMapService mapService = new MapService();
            FareService fareService = new FareService(fareRuleRepository);
            ITripService tripService = new TripService(tripRepository, driverRepository, passengerRepository, fareService, mapService);
            await fareService.SeedDefaultFareRulesAsync();

            await Infrastructure.Data.DataSeeder.SeedAsync(
                driverRepository,
                passengerRepository,
                tripRepository,
                vehicleRepository);

SimulationService simService = new SimulationService();
            simService.SetRepositories(tripRepository, driverRepository, vehicleRepository);

            return new AppServiceBundle
            {
                UserService = userService,
                TripService = tripService,
                FareService = fareService,
                SimulationService = simService,
                AdminService = new AdminService(driverRepository, passengerRepository, tripRepository, fareRuleRepository, reviewRepository),
                MatchingService = new MatchingService(tripRepository, driverRepository, vehicleRepository),
                ReviewService = new ReviewService(reviewRepository, driverRepository, tripRepository),
                MapService = mapService,
                VehicleRepository = vehicleRepository
            };
        }
    }
}
