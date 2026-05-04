using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestClass]
    public class ReviewServiceTests
    {
        private ReviewService _reviewService;
        private MockReviewRepository _reviewRepository;
        private MockDriverRepository _driverRepository;
        private MockTripRepository _tripRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _reviewRepository = new MockReviewRepository();
            _driverRepository = new MockDriverRepository();
            _tripRepository = new MockTripRepository();
            _reviewService = new ReviewService(_reviewRepository, _driverRepository, _tripRepository);
        }

        [TestMethod]
        public async Task AddReviewAsync_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var passengerId = Guid.NewGuid();
            var tripId = Guid.NewGuid();
            var rating = 5;
            var comment = "Great service";

            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "pass", "DL", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "Route");
            var fare = new Fare(new Money(10000), new Money(1000));
            var trip = new Trip(tripId, passengerId, driverId, route, fare, VehicleType.Car, DateTime.UtcNow, "Completed", true);
            await _tripRepository.AddAsync(trip);

            // Act
            await _reviewService.AddReviewAsync(driverId, passengerId, tripId, rating, comment);

            // Assert
            var reviews = await _reviewRepository.GetByDriverIdAsync(driverId);
            Assert.AreEqual(1, reviews.Count);
            Assert.AreEqual(comment, reviews[0].Comment);
            Assert.AreEqual(rating, reviews[0].Rating);

            var updatedDriver = await _driverRepository.GetByIdAsync(driverId);
            Assert.AreEqual(1, updatedDriver.TotalReviews);
            Assert.AreEqual(5, updatedDriver.RatingSum);
        }

        [TestMethod]
        public async Task AddReviewAsync_ShouldThrowException_WhenTripNotCompleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var passengerId = Guid.NewGuid();
            var tripId = Guid.NewGuid();

            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "pass", "DL", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "Route");
            var fare = new Fare(new Money(10000), new Money(1000));
            var trip = new Trip(tripId, passengerId, driverId, route, fare, VehicleType.Car, DateTime.UtcNow, "Started", false);
            await _tripRepository.AddAsync(trip);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
                _reviewService.AddReviewAsync(driverId, passengerId, tripId, 5, "Comment"));
        }

        [TestMethod]
        public async Task AddReviewAsync_ShouldThrowException_WhenDriverMismatch()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var wrongDriverId = Guid.NewGuid();
            var passengerId = Guid.NewGuid();
            var tripId = Guid.NewGuid();

            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "password123", "DL", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "Route");
            var fare = new Fare(new Money(10000), new Money(1000));
            var trip = new Trip(tripId, passengerId, driverId, route, fare, VehicleType.Car, DateTime.UtcNow, "Completed", true);
            await _tripRepository.AddAsync(trip);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
                _reviewService.AddReviewAsync(wrongDriverId, passengerId, tripId, 5, "Comment"));
        }

        [TestMethod]
        public async Task GetReviewsForDriverAsync_ShouldReturnList()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var passengerId = Guid.NewGuid();
            var tripId = Guid.NewGuid();
            var review = new Review(driverId, passengerId, tripId, 5, "Good");
            await _reviewRepository.AddAsync(review);

            // Act
            var result = await _reviewService.GetReviewsForDriverAsync(driverId);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Good", result[0].Comment);
        }
    }
}