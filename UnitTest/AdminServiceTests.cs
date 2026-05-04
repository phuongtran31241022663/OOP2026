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
    public class AdminServiceTests
    {
        private AdminService _adminService;
        private MockDriverRepository _driverRepository;
        private MockPassengerRepository _passengerRepository;
        private MockTripRepository _tripRepository;
        private MockFareRuleRepository _fareRuleRepository;
        private MockReviewRepository _reviewRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _driverRepository = new MockDriverRepository();
            _passengerRepository = new MockPassengerRepository();
            _tripRepository = new MockTripRepository();
            _fareRuleRepository = new MockFareRuleRepository();
            _reviewRepository = new MockReviewRepository();

            _adminService = new AdminService(
                _driverRepository,
                _passengerRepository,
                _tripRepository,
                _fareRuleRepository,
                _reviewRepository);
        }

        [TestMethod]
        public async Task GetAllDriversAsync_ShouldReturnAll()
        {
            // Arrange
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            await _driverRepository.AddAsync(new Driver("D1", "0123456781", "password1", "L1", Guid.NewGuid(), location));
            await _driverRepository.AddAsync(new Driver("D2", "0123456782", "password2", "L2", Guid.NewGuid(), location));

            // Act
            var result = await _adminService.GetAllDriversAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetTotalGMVAsync_ShouldCalculateCorrectly()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "Route");
            
            var fare1 = new Fare(new Money(50000), new Money(5000));
            var trip1 = new Trip(Guid.NewGuid(), passengerId, driverId, route, fare1, VehicleType.Car, DateTime.UtcNow, "Completed", true);
            
            var fare2 = new Fare(new Money(30000), new Money(3000));
            var trip2 = new Trip(Guid.NewGuid(), passengerId, driverId, route, fare2, VehicleType.Car, DateTime.UtcNow, "Completed", true);

            await _tripRepository.AddAsync(trip1);
            await _tripRepository.AddAsync(trip2);

            // Act
            var totalGMV = await _adminService.GetTotalGMVAsync();

            // Assert
            Assert.AreEqual(80000, totalGMV);
        }

        [TestMethod]
        public async Task GetTotalNTRAsync_ShouldCalculateCorrectly()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "Route");
            
            var fare1 = new Fare(new Money(50000), new Money(5000));
            var trip1 = new Trip(Guid.NewGuid(), passengerId, driverId, route, fare1, VehicleType.Car, DateTime.UtcNow, "Completed", true);
            
            var fare2 = new Fare(new Money(30000), new Money(3000));
            var trip2 = new Trip(Guid.NewGuid(), passengerId, driverId, route, fare2, VehicleType.Car, DateTime.UtcNow, "Completed", true);

            await _tripRepository.AddAsync(trip1);
            await _tripRepository.AddAsync(trip2);

            // Act
            var totalNTR = await _adminService.GetTotalNTRAsync();

            // Assert
            Assert.AreEqual(8000, totalNTR);
        }
    }
}