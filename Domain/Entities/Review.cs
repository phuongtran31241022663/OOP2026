using Domain.Events;
using Domain.SharedKernel;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Đại diện cho một đánh giá của hành khách về tài xế sau một chuyến đi.
    /// </summary>
    /// <remarks>
    /// Lớp này là một Entity, không thể thay đổi Id của tài xế, hành khách và chuyến đi sau khi đã tạo.
    /// </remarks>
    public sealed class Review : Entity
    {
        #region Fields

        private readonly Guid _driverId;
        private readonly Guid _passengerId;
        private readonly Guid _tripId;
        private int _rating;
        private string _comment;
        private readonly DateTime _createdAt;

        #endregion

        #region Properties

        /// <summary>
        /// ID của tài xế được đánh giá.
        /// </summary>
        public Guid DriverId => _driverId;

        /// <summary>
        /// ID của hành khách thực hiện đánh giá.
        /// </summary>
        public Guid PassengerId => _passengerId;

        /// <summary>
        /// ID của chuyến đi liên quan đến đánh giá này.
        /// </summary>
        public Guid TripId => _tripId;

        /// <summary>
        /// Số sao đánh giá (từ 1 đến 5).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra khi số sao nằm ngoài khoảng [1, 5].</exception>
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

        /// <summary>
        /// Nội dung bình luận.
        /// </summary>
        public string Comment
        {
            get => _comment;
            private set => _comment = value ?? string.Empty;
        }

        /// <summary>
        /// Thời điểm đánh giá được tạo.
        /// </summary>
        public DateTime CreatedAt => _createdAt;

        #endregion

        #region Constructors

        /// <summary>
        /// Tạo một đối tượng đánh giá mới.
        /// </summary>
        /// <param name="driverId">ID của tài xế.</param>
        /// <param name="passengerId">ID của hành khách.</param>
        /// <param name="tripId">ID của chuyến đi.</param>
        /// <param name="rating">Số sao đánh giá (1-5).</param>
        /// <param name="comment">Nội dung bình luận.</param>
        /// <exception cref="ArgumentException">Ném ra khi một trong các ID không hợp lệ.</exception>
        public Review(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment) : base(Guid.NewGuid())
        {
            if (driverId == Guid.Empty)
                throw new ArgumentException("Id tài xế không hợp lệ.", nameof(driverId));
            if (passengerId == Guid.Empty)
                throw new ArgumentException("Id hành khách không hợp lệ.", nameof(passengerId));
            if (tripId == Guid.Empty)
                throw new ArgumentException("Id chuyến đi không hợp lệ.", nameof(tripId));

            _driverId = driverId;
            _passengerId = passengerId;
            _tripId = tripId;
            Rating = rating;
            Comment = comment;
            _createdAt = DateTime.UtcNow;
            AddEvent(new ReviewCreatedEvent(Id, _driverId, _passengerId, Rating, Comment));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cập nhật nội dung của đánh giá.
        /// </summary>
        /// <param name="rating">Số sao đánh giá mới (1-5).</param>
        /// <param name="comment">Nội dung bình luận mới.</param>
        public void UpdateReview(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }

        #endregion
    }
}
