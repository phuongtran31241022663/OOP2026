using Domain.Entities;
using System;

namespace Domain.States
{
    public interface ITripState
    {
        void SetSearching(Trip trip);
        void MatchDriver(Trip trip, Guid driverId);
        void MarkAsArrived(Trip trip);
        void StartTrip(Trip trip);
        void CompleteTrip(Trip trip);
        void Cancel(Trip trip, string reason);
        void MarkTimeout(Trip trip);
    }
}