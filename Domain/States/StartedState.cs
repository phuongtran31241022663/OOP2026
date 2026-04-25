using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;

namespace Domain.States
{
    public class StartedState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            throw new InvalidOperationException("Cannot search after started.");
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Already matched.");
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Already arrived and started.");
        }

        public void StartTrip(Trip trip)
        {
            // Already started
        }

        public void CompleteTrip(Trip trip)
        {
            trip.TransitionTo(new CompletedState());
            trip.SetStatusInternal(TripStatus.Completed);
            trip.AddEvent(new TripCompletedEvent(trip.Id, trip.PassengerId, trip.DriverId.Value, trip.TripFare));
        }

        public void Cancel(Trip trip, string reason)
        {
            trip.TransitionTo(new CancelledState());
            trip.SetStatusInternal(TripStatus.Cancelled);
            trip.AddEvent(new TripCancelledEvent(trip.Id, reason));
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Timeout not applicable.");
        }
    }
}