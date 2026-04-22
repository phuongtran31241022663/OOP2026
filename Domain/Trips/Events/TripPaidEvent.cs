using Domain.SharedKernel;
using Domain.ValueObjects;
using System;

namespace Domain.Trips.Events
{
    public class TripPaidEvent : DomainEvent
    {
        public Guid TripId { get; }
        public Guid PassengerId { get; }
        public Guid DriverId { get; }
        public Money TotalAmount { get; } // Tổng tiền khách trả
        public DateTime PaidAt { get; }

        public TripPaidEvent(Guid tripId, Guid passengerId, Guid driverId, Money totalAmount)
        {
            TripId = tripId;
            PassengerId = passengerId;
            DriverId = driverId;
            TotalAmount = totalAmount;
            PaidAt = DateTime.UtcNow;
        }
    }
}