using Application.Events;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of ITripService for UI testing.
    /// Allows configurable trip request behavior and status change simulation.
    /// </summary>
    public class MockTripService : ITripService
    {
        private Func<Guid, Location, Location, VehicleType, Trip> _requestTripHandler;
        private Action<TripStatusChangedEventArgs> _statusChangedHandler;
        
        private Trip _lastRequestedTrip;
        private bool _requestTripCalled;
        private string _lastTripStatus;

        public event EventHandler<TripStatusChangedEventArgs> TripStatusChanged;

        public Trip LastRequestedTrip => _lastRequestedTrip;
        public bool RequestTripCalled => _requestTripCalled;
        public string LastTripStatus => _lastTripStatus;
        public int RequestTripCallCount { get; private set; }
        public bool CancelTripCalled { get; private set; }
        public bool CancelTripAsyncCalled { get; private set; }
        public int CanCancelCallCount { get; private set; }
        public bool CanCancelReturnValue { get; private set; } = true;
        private Trip _tripToReturn;

        public MockTripService()
        {
            _requestTripHandler = (passengerId, pickup, destination, vehicleType) => null;
        }

        public void SetupRequestTripSuccess(Trip trip)
        {
            _requestTripHandler = (passengerId, pickup, destination, vehicleType) => trip;
        }

        public void SetupRequestTripFailure(Exception ex)
        {
            _requestTripHandler = (passengerId, pickup, destination, vehicleType) =>
                throw ex;
        }

        public void SetTripToReturn(Trip trip)
        {
            _tripToReturn = trip;
            SetupRequestTripSuccess(trip);
        }

        public void SetCanCancel(bool canCancel)
        {
            CanCancelReturnValue = canCancel;
        }

        public Task<Trip> CreateTripAsync(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
        {
            return Task.FromResult(_tripToReturn);
        }

        public Task MatchDriverAsync(Guid tripId, Guid driverId)
        {
            return Task.CompletedTask;
        }

        public Task MarkAsArrivedAsync(Guid tripId)
        {
            return Task.CompletedTask;
        }

        public Task StartTripAsync(Guid tripId)
        {
            return Task.CompletedTask;
        }

        public Task CompleteTripAsync(Guid tripId)
        {
            return Task.CompletedTask;
        }

        public Task CancelTripAsync(Guid tripId, string reason)
        {
            CancelTripCalled = true;
            CancelTripAsyncCalled = true;
            return Task.CompletedTask;
        }

        public Task<Trip> RequestTripAsync(Guid passengerId, Location pickupLocation, Location destinationLocation, VehicleType vehicleType)
        {
            RequestTripCallCount++;
            _requestTripCalled = true;
            _lastRequestedTrip = _requestTripHandler(passengerId, pickupLocation, destinationLocation, vehicleType);
            
            if (_lastRequestedTrip != null)
            {
                _lastTripStatus = _lastRequestedTrip.Status;
            }
            
            return Task.FromResult(_lastRequestedTrip);
        }

        public Task<Trip> GetTripAsync(Guid tripId)
        {
            return Task.FromResult(_tripToReturn);
        }

        public Task<Trip> GetTripByIdAsync(Guid id)
        {
            return Task.FromResult<Trip>(null);
        }

        public Task<Trip> GetActiveTripForDriverAsync(Guid driverId)
        {
            return Task.FromResult<Trip>(null);
        }

        public Task<Trip> GetActiveTripForPassengerAsync(Guid passengerId)
        {
            return Task.FromResult<Trip>(null);
        }

        public Task<List<Trip>> GetPendingTripsAsync()
        {
            return Task.FromResult(new List<Trip>());
        }

        public Task<List<Trip>> GetTripsByDriverAsync(Guid driverId)
        {
            return Task.FromResult(new List<Trip>());
        }

        public Task<List<Trip>> GetTripsByPassengerAsync(Guid passengerId)
        {
            return Task.FromResult(new List<Trip>());
        }

        public Task<bool> CanTripBeCancelledAsync(Guid tripId)
        {
            CanCancelCallCount++;
            return Task.FromResult(CanCancelReturnValue);
        }

        public Task ConfirmPaymentAsync(Guid tripId)
        {
            return Task.CompletedTask;
        }

        public Task UpdateDriverLocationAsync(Guid tripId, Location location)
        {
            return Task.CompletedTask;
        }

/// <summary>
        /// Simulates a trip status change event for testing.
        /// </summary>
        public void SimulateStatusChange(Guid tripId, string newStatus)
        {
            var eventArgs = new TripStatusChangedEventArgs(tripId, newStatus, null);
            _lastTripStatus = newStatus;
            TripStatusChanged?.Invoke(this, eventArgs);
        }
    }
}
