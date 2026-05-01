using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of ISimulationService for UI testing.
    /// </summary>
    public class MockSimulationService : ISimulationService
    {
        private bool _startSimulationCalled;
        private bool _stopSimulationCalled;
        private bool _updateDriverLocationCalled;

        public bool StartSimulationCalled => _startSimulationCalled;
        public bool StopSimulationCalled => _stopSimulationCalled;
        public bool UpdateDriverLocationCalled => _updateDriverLocationCalled;

        public event EventHandler<Domain.Events.DriverLocationUpdatedEvent> DriverLocationUpdated;

        public void StartSimulation()
        {
            _startSimulationCalled = true;
        }

        public void StopSimulation()
        {
            _stopSimulationCalled = true;
        }

        public void StartTripSimulation(Guid tripId)
        {
            _startSimulationCalled = true;
        }

        public bool IsTripSimulating(Guid tripId)
        {
            return false;
        }

        public Task Tick()
        {
            return Task.CompletedTask;
        }

        public Task SimulateTripProgress(Guid tripId)
        {
            return Task.CompletedTask;
        }
    }
}
