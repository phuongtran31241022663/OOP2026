using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SimulationService : ISimulationService
    {
        public SimulationService()
        {
        }

        public void StartSimulation() { }
        public void StopSimulation() { }
        public void StartTripSimulation(Guid tripId) { }
        public bool IsTripSimulating(Guid tripId) => false;
        public Task Tick() => Task.CompletedTask;
        public Task SimulateTripProgress(Guid tripId) => Task.CompletedTask;
    }

    public sealed class AppServiceBundle
    {
        public IUserService UserService { get; private set; }
        public ITripService TripService { get; private set; }
        public IFareService FareService { get; private set; }
        public ISimulationService SimulationService { get; private set; }

        public static AppServiceBundle CreateDefault()
        {
            IDriverRepository driverRepository = new DriverRepository();
            IPassengerRepository passengerRepository = new PassengerRepository();
            ITripRepository tripRepository = new TripRepository();
            IFareRuleRepository fareRuleRepository = new FareRuleRepository();

            driverRepository.InitializeAsync().GetAwaiter().GetResult();
            passengerRepository.InitializeAsync().GetAwaiter().GetResult();
            tripRepository.InitializeAsync().GetAwaiter().GetResult();
            fareRuleRepository.InitializeAsync().GetAwaiter().GetResult();
            fareRuleRepository.EnsureSeededAsync().GetAwaiter().GetResult();

            IUserService userService = new UserService(driverRepository, passengerRepository);
            ITripService tripService = new TripService(tripRepository, driverRepository, passengerRepository);
            FareService fareService = new FareService(fareRuleRepository);
            fareService.SeedDefaultFareRulesAsync().GetAwaiter().GetResult();

            return new AppServiceBundle
            {
                UserService = userService,
                TripService = tripService,
                FareService = fareService,
                SimulationService = new SimulationService()
            };
        }
    }
}
