using System;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface ISimulationService
    {
        void SetTripService(ITripService tripService);
        void SetDriverSimulationService(IDriverSimulationService driverSimulationService);
        void StartSimulation();
        void StopSimulation();
        void StartTripSimulation(Guid tripId);
        bool IsTripSimulating(Guid tripId);
        Task Tick();
        Task SimulateTripProgress(Guid tripId);
    }
}
