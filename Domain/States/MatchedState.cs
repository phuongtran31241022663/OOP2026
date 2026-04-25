using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;

namespace Domain.States
{
    public class MatchedState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            // Có thể cho phép quay lại Searching? Trong Transition có Searching.
            // Nếu không muốn thì throw. Ở đây làm theo yêu cầu: có thể.
            trip.TransitionTo(new SearchingState());
            trip.SetStatusInternal(TripStatus.Searching);
            // Có thể thêm event?
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Already matched with a driver.");
        }

        public void MarkAsArrived(Trip trip)
        {
            trip.TransitionTo(new ArrivedState());
            trip.SetStatusInternal(TripStatus.Arrived);
            trip.AddEvent(new TripArrivedEvent(trip.Id));
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Driver must arrive first.");
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot complete before starting.");
        }

        public void Cancel(Trip trip, string reason)
        {
            trip.TransitionTo(new CancelledState());
            trip.SetStatusInternal(TripStatus.Cancelled);
            trip.AddEvent(new TripCancelledEvent(trip.Id, reason));
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Timeout not applicable in Matched state.");
        }
    }
}