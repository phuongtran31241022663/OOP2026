using Domain.Enums;
using System.Collections.Generic;

namespace Domain.StateMachines
{
    public class TripStateMachine
    {
        private static readonly Dictionary<TripStatus, HashSet<TripStatus>> ValidTransitions
    = new Dictionary<TripStatus, HashSet<TripStatus>>()
    {
        [TripStatus.Requested] = new HashSet<TripStatus>
    {
        TripStatus.Searching,
        TripStatus.Cancelled
    },

        [TripStatus.Searching] = new HashSet<TripStatus>
    {
        TripStatus.Matched,
        TripStatus.Cancelled,
        TripStatus.Timeout
    },

        [TripStatus.Matched] = new HashSet<TripStatus>
    {
        TripStatus.Arrived,
        TripStatus.Searching,
        TripStatus.Cancelled
    },

        [TripStatus.Arrived] = new HashSet<TripStatus>
    {
        TripStatus.Started,
        TripStatus.Cancelled
    },

        [TripStatus.Started] = new HashSet<TripStatus>
    {
        TripStatus.Completed,
        TripStatus.Cancelled
    },

        [TripStatus.Completed] = new HashSet<TripStatus>(),
        [TripStatus.Cancelled] = new HashSet<TripStatus>(),
        [TripStatus.Timeout] = new HashSet<TripStatus>()
    };

    public static bool CanTransition(TripStatus from, TripStatus to)
    {
        HashSet<TripStatus> validTargets;
        return ValidTransitions.TryGetValue(from, out validTargets) && validTargets.Contains(to);
    }
        public static bool CanBeCancelled(TripStatus status)
        {
            return status == TripStatus.Requested ||
                   status == TripStatus.Searching ||
                   status == TripStatus.Matched ||
                   status == TripStatus.Arrived;
        }
    }
}
