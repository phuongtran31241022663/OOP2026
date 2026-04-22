using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SimulationService : ISimulationService
    {
        private ITripService _tripService;

        public void SetTripService(ITripService tripService)
        {
            _tripService = tripService;
        }

        public void SetDriverSimulationService(IDriverSimulationService driverSimulationService)
        {
            // Not used in MVP
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
