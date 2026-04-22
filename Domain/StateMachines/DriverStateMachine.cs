using Domain.Enums;
using System.Collections.Generic;

namespace Domain.StateMachines
{
    public static class DriverStateMachine
    {
        private static readonly Dictionary<DriverStatus, HashSet<DriverStatus>> ValidTransitions =
            new Dictionary<DriverStatus, HashSet<DriverStatus>>()
        {
            { DriverStatus.Offline, new HashSet<DriverStatus> { DriverStatus.Available } },
            { DriverStatus.Available, new HashSet<DriverStatus> { DriverStatus.OnTrip, DriverStatus.Offline } },
            { DriverStatus.OnTrip, new HashSet<DriverStatus> { DriverStatus.Available } }
        };

        public static bool CanTransition(DriverStatus from, DriverStatus to)
        {
            HashSet<DriverStatus> validTargets;
            return ValidTransitions.TryGetValue(from, out validTargets)
                   && validTargets.Contains(to);
        }
    }
}
