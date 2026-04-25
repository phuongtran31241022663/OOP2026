using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;

namespace Domain.States
{
    public class RequestedState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            trip.TransitionTo(new SearchingState());
            trip.SetStatusInternal(TripStatus.Searching);
            trip.AddEvent(new TripSearchingEvent(trip.Id, 1));
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Cannot match driver in Requested state.");
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Cannot arrive in Requested state.");
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot start in Requested state.");
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot complete in Requested state.");
        }

        public void Cancel(Trip trip, string reason)
        {
            trip.TransitionTo(new CancelledState());
            trip.SetStatusInternal(TripStatus.Cancelled);
            trip.AddEvent(new TripCancelledEvent(trip.Id, reason));
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Timeout not applicable in Requested state.");
        }
    }
}