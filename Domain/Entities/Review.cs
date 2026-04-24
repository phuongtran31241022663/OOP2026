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
        public Guid DriverId
        {
            get => _driverId;
            init
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("DriverId không được để trống.", nameof(DriverId));
                _driverId = value;
            }
        }
        public Guid PassengerId
        {
            get => _passengerId;
            init
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("PassengerId không được để trống.", nameof(PassengerId));
                _passengerId = value;
            }
        }

        public Guid TripId
        {
            get => _tripId;
            init
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("TripId không được để trống.", nameof(TripId));
                _tripId = value;
            }
        }

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
            DriverId = driverId;
            PassengerId = passengerId;
            TripId = tripId;
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