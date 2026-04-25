using System;
using Domain.Entities;

namespace Domain.States
{
    public class TimeoutState : ITripState
    {
        public void SetSearching(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void MatchDriver(Trip trip, Guid driverId)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void MarkAsArrived(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void StartTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void CompleteTrip(Trip trip)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void Cancel(Trip trip, string reason)
        {
            throw new InvalidOperationException("Cannot modify timed out trip.");
        }

        public void MarkTimeout(Trip trip)
        {
            // Already timeout
        }
    }
}