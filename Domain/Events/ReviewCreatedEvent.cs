using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class ReviewCreatedEvent : DomainEvent
    {
        public Guid ReviewId { get; }
        public Guid DriverId { get; }
        public Guid RiderId { get; }
        public int Rating { get; }
        public string Comment { get; }

        public ReviewCreatedEvent(Guid reviewId, Guid driverId, Guid riderId, int rating, string comment)
        {
            ReviewId = reviewId;
            DriverId = driverId;
            RiderId = riderId;
            Rating = rating;
            Comment = comment;
        }
    }
}