using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class DriverStatusChangedEvent : DomainEvent
    {
        public Guid DriverId { get; }
        public string OldStatus { get; }
        public string NewStatus { get; }

        public DriverStatusChangedEvent(Guid driverId, string oldStatus, string newStatus)
        {
            DriverId = driverId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
}
