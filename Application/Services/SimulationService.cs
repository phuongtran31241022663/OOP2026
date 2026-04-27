using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.ExternalServices;
using Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SimulationService : ISimulationService
    {
        private Timer _timer;

        public SimulationService()
        {
        }

        public void StartSimulation()
        {
            _timer = new Timer(async _ => await Tick(), null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }

        public void StopSimulation()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _timer = null;
        }

        public void StartTripSimulation(Guid tripId) { }
        public bool IsTripSimulating(Guid tripId) => false;

        public async Task Tick()
        {
            await Task.CompletedTask;
        }

        public async Task SimulateTripProgress(Guid tripId)
        {
            await Task.CompletedTask;
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
            ITripService tripService = new TripService(tripRepository, driverRepository, passengerRepository);
            FareService fareService = new FareService(fareRuleRepository);
            await fareService.SeedDefaultFareRulesAsync();

            return new AppServiceBundle
            {
                UserService = userService,
                TripService = tripService,
                FareService = fareService,
                SimulationService = new SimulationService(),
                AdminService = new AdminService(driverRepository, passengerRepository, tripRepository, fareRuleRepository, reviewRepository),
                MatchingService = new MatchingService(tripRepository, driverRepository, vehicleRepository),
                ReviewService = new ReviewService(reviewRepository, driverRepository, tripRepository),
                MapService = new MapService(null) // GMapService wrapper stub
            };
        }
    }
}
