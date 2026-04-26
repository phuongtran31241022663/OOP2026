using System;
using System.Threading.Tasks;
using Application.Interfaces;

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
}