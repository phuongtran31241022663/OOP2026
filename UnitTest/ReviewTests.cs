using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class ReviewTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesReview()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();
            int rating = 5;
            string comment = "Great service!";

            // Act
            Review review = new Review(driverId, passengerId, tripId, rating, comment);

            // Assert
            Assert.AreEqual(driverId, review.DriverId);
            Assert.AreEqual(passengerId, review.PassengerId);
            Assert.AreEqual(tripId, review.TripId);
            Assert.AreEqual(rating, review.Rating);
            Assert.AreEqual(comment, review.Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyDriverId_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.Empty;
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 5, "Good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyPassengerId_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.Empty;
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 5, "Good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyTripId_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.Empty;

            // Act
            Review review = new Review(driverId, passengerId, tripId, 5, "Good");
        }

        #endregion

        #region Rating Validation Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithRatingZero_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 0, "Good");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithRatingGreaterThan5_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 6, "Great");
        }

        [TestMethod]
        public void Constructor_WithRating1_CreatesReview()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 1, "Bad service");

            // Assert
            Assert.AreEqual(1, review.Rating);
        }

        [TestMethod]
        public void Constructor_WithRating5_CreatesReview()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 5, "Excellent!");

            // Assert
            Assert.AreEqual(5, review.Rating);
        }

        #endregion

        #region Comment Tests

        [TestMethod]
        public void Constructor_WithNullComment_SetsEmptyString()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 4, null);

            // Assert
            Assert.AreEqual(string.Empty, review.Comment);
        }

        [TestMethod]
        public void Constructor_WithEmptyComment_SetsEmptyString()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();

            // Act
            Review review = new Review(driverId, passengerId, tripId, 4, "");

            // Assert
            Assert.AreEqual(string.Empty, review.Comment);
        }

        #endregion

        #region UpdateReview Tests

        [TestMethod]
        public void UpdateReview_WithValidRating_UpdatesSuccessfully()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();
            Review review = new Review(driverId, passengerId, tripId, 3, "Average");

            // Act
            review.UpdateReview(5, "Much better!");

            // Assert
            Assert.AreEqual(5, review.Rating);
            Assert.AreEqual("Much better!", review.Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateReview_WithInvalidRating_ThrowsException()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();
            Review review = new Review(driverId, passengerId, tripId, 3, "Average");

            // Act
            review.UpdateReview(0, "Bad");
        }

        #endregion

        #region CreatedAt Tests

        [TestMethod]
        public void Constructor_SetsCreatedAt_ToCurrentUtcTime()
        {
            // Arrange
            Guid driverId = Guid.NewGuid();
            Guid passengerId = Guid.NewGuid();
            Guid tripId = Guid.NewGuid();
            DateTime before = DateTime.UtcNow;

            // Act
            Review review = new Review(driverId, passengerId, tripId, 4, "Good");
            DateTime after = DateTime.UtcNow;

            // Assert
            Assert.IsTrue(review.CreatedAt >= before && review.CreatedAt <= after);
        }

        #endregion
    }
}
