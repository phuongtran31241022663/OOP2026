using Application.Interfaces;
using Domain.Trips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ITripService _tripService;
        private readonly IDriverSimulationService _driverSimulationService;

        public SimulationService(ITripService tripService, IDriverSimulationService driverSimulationService)
        {
            _tripService = tripService;
            _driverSimulationService = driverSimulationService;
        }

        public void StartSimulation()
        {
            // Not used in MVP
        }

        public void StopSimulation()
        {
            // Not used in MVP
        }

        public Task Tick()
        {
            return Task.CompletedTask;
        }

        public void StartTripSimulation(Guid tripId)
        {
            // No-op for MVP – simulation not required
        }

        public bool IsTripSimulating(Guid tripId) => false;

        public Task SimulateTripProgress(Guid tripId) => Task.CompletedTask;
    }
}

