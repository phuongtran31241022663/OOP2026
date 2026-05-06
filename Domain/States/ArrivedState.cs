using System;
using Domain.Entities;
using Domain.Events;

namespace Domain.States
{
    public class ArrivedState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            throw new InvalidOperationException("Cannot go back to searching.");
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Already matched.");
        }

        public void MarkAsArrived(Trip trip)
        {
            // Already arrived
        }

        public void StartTrip(Trip trip)
        {
            trip.TransitionTo(new StartedState());
            trip.AddEvent(new TripStartedEvent(trip.Id));
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot complete before start.");
        }

        public void Cancel(Trip trip, string reason)
        {
            trip.TransitionTo(new CancelledState());
            trip.AddEvent(new TripCancelledEvent(trip.Id, reason));
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Timeout not applicable.");
        }
    }
}