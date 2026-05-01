using Application.Interfaces;
using Domain.Entities.Users;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Entities;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IMatchingService for UI testing.
    /// </summary>
    public class MockMatchingService : IMatchingService
    {
        private bool _matchCalled;
        private bool _cancelMatchingCalled;

        public bool MatchCalled => _matchCalled;
        public bool CancelMatchingCalled => _cancelMatchingCalled;

        public event EventHandler<TripMatchedEvent> TripMatched;

        public Task<Driver> FindNearbyDriverAsync(Location passengerLocation, Domain.Enums.VehicleType vehicleType)
        {
            _matchCalled = true;
            return Task.FromResult<Driver>(null);
        }

        public Task MatchTripAsync(Trip trip)
        {
            _matchCalled = true;
            return Task.CompletedTask;
        }

public Task CancelMatchingAsync(Guid tripId)
        {
            _cancelMatchingCalled = true;
            return Task.CompletedTask;
        }

        public Task<bool> MatchDriverToTripAsync(Guid tripId, Guid driverId)
        {
            _matchCalled = true;
            return Task.FromResult(true);
        }
    }
}
