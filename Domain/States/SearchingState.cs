using System;
using Domain.Entities;
using Domain.Events;

namespace Domain.States
{
    public class SearchingState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            // Already searching, do nothing or throw? Thường thì không cần.
            // Có thể throw nếu muốn nghiêm ngặt.
            // Tạm thời do nothing.
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            if (driverId == Guid.Empty) throw new ArgumentException("Invalid driverId");
            trip.SetDriverId(driverId);
            trip.TransitionTo(new MatchedState());
            trip.AddEvent(new TripMatchedEvent(trip.Id, driverId));
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Cannot arrive before matching.");
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot start before matching.");
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot complete before starting.");
        }

        public void Cancel(Trip trip, string reason)
        {
            trip.TransitionTo(new CancelledState());
            trip.AddEvent(new TripCancelledEvent(trip.Id, reason));
        }

        public void MarkTimeout(Trip trip)
        {
            trip.TransitionTo(new TimeoutState());
            trip.AddEvent(new TripTimeoutEvent(trip.Id));
        }
    }
}