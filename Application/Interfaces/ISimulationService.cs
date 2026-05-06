using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISimulationService
    {
        void StartSimulation();
        void StopSimulation();
        void StartTripSimulation(Guid tripId);
        bool IsTripSimulating(Guid tripId);
        Task Tick();
        Task SimulateTripProgress(Guid tripId);
    }
}