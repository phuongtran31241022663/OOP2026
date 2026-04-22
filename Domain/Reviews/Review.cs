using Domain.SharedKernel;
using System;

namespace Domain.Reviews
{
    
    public sealed class Review : Entity
    {
        private Guid _driverId;
        private Guid _riderId;
        private Guid _tripId;
        public int Rating { get; }
        public string Comment { get; }
        public DateTime CreatedAt { get; private set; }
        public Guid DriverId { get => _driverId; set => _driverId = value; }
        public Guid RiderId { get => _riderId; set => _riderId = value; }
        public Guid TripId { get => _tripId; set => _tripId = value; }

        public Review(Guid id, Guid driverId, Guid riderId, Guid tripId, int rating, string comment) : base(id)
        {
            if (rating < 1 || rating > 5)
                throw new Exception(nameof(rating), new Exception("Review phải nằm trong khoảng từ 1 đến 5."));
            DriverId = driverId;
            RiderId = riderId;
            TripId = tripId;
            Rating = rating;
            Comment = comment ?? string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
