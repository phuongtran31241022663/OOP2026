using Domain.SharedKernel;
using System;

namespace Domain.Entities
{
    public sealed class Review : Entity
    {
        #region Backing Fields
        private readonly Guid _driverId;
        private readonly Guid _passengerId;
        private readonly Guid _tripId;
        private int _rating;
        private string _comment;
        private readonly DateTime _createdAt;
        #endregion

        #region Properties
        public Guid DriverId => _driverId;
        public Guid PassengerId => _passengerId;
        public Guid TripId => _tripId;
        public int Rating
        {
            get => _rating;
            private set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentOutOfRangeException(nameof(Rating), "Đánh giá phải từ 1 đến 5.");

                _rating = value;
            }
        }

        public string Comment
        {
            get => _comment;
            private set => _comment = value ?? string.Empty;
        }

        public DateTime CreatedAt => _createdAt;
        #endregion

        public Review(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment) : base(Guid.NewGuid())
        {
            _driverId = driverId;
          _passengerId = passengerId;
            _tripId = tripId;
            Rating = rating;
            Comment = comment;
            _createdAt = DateTime.UtcNow;
        }
        public void UpdateReview(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }
    }
}