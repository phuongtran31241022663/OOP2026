using System;
using Domain.Entities;

namespace Domain.States
{
    public class CancelledState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }

        public void Cancel(Trip trip, string reason)
        {
            // Already cancelled
        }

        public void MarkTimeout(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify cancelled trip.");
        }
    }
}