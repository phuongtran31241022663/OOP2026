using System;
using Domain.Entities;

namespace Domain.States
{
    public class CompletedState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify completed trip.");
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Cannot modify completed trip.");
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify completed trip.");
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify completed trip.");
        }

        public void CompleteTrip(Trip trip)
        {
            // Already completed
        }

        public void Cancel(Trip trip, string reason)
        {
            throw new InvalidOperationException("Cannot cancel completed trip.");
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Cannot timeout completed trip.");
        }
    }
}