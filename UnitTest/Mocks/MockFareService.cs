using Application.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IFareService for UI testing.
    /// </summary>
    public class MockFareService : IFareService
    {
        private bool _calculateFareCalled;

        public bool CalculateFareCalled => _calculateFareCalled;

        public Task<Fare> CalculateFareAsync(VehicleType vehicleType, double distanceKM)
        {
            _calculateFareCalled = true;
            return Task.FromResult(new Fare(new Money(50000m), new Money(7500m)));
        }
    }
}
