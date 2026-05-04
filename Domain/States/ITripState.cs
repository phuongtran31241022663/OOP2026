﻿using Domain.Entities;
using System;

namespace Domain.States
{
    /// <summary>
    /// Interface for Trip state machine.
    /// Each concrete state encapsulates the behavior and state transitions for a specific Trip state.
    /// For serialization, use TypeNameHandling.Auto in JsonSerializerSettings.
    /// </summary>
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
