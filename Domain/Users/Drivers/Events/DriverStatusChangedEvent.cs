using System;
using Domain.Enums;
using Domain.SharedKernel;

namespace Domain.Users.Drivers.Events
{
    public class DriverStatusChangedEvent : DomainEvent
    {
        public Guid DriverId { get; }
        public DriverStatus OldStatus { get; }
        public DriverStatus NewStatus { get; }

        public DriverStatusChangedEvent(Guid driverId, DriverStatus oldStatus, DriverStatus newStatus)
        {
            DriverId = driverId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
}
