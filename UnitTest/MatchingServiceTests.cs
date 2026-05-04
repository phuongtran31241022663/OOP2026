using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Entities.Vehicles;
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
    public class MatchingServiceTests
    {
        private MatchingService _matchingService;
        private MockTripRepository _tripRepository;
        private MockDriverRepository _driverRepository;
        private MockVehicleRepository _vehicleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _tripRepository = new MockTripRepository();
            _driverRepository = new MockDriverRepository();
            _vehicleRepository = new MockVehicleRepository();
            _matchingService = new MatchingService(_tripRepository, _driverRepository, _vehicleRepository);
        }

        [TestMethod]
        public async Task MatchDriverToTripAsync_ShouldSucceed_WhenAllConditionsMet()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            
            var location = new Location(new Coordinate(0, 0), new Address("S", "1", "D", "C", "C"));
            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "R");
            var fare = new Fare(new Money(50000), new Money(5000));
            var trip = new Trip(Guid.NewGuid(), passengerId, null, route, fare, VehicleType.Car, DateTime.UtcNow, "Searching", false);
            await _tripRepository.AddAsync(trip);

            var car = new Car(null, "ABC", "T", "V", "R", 4);
            await _vehicleRepository.AddAsync(car);
            var vehicleId = car.Id;

            var driver = new Driver(driverId, "D", "0123456789", "p", "L", vehicleId, location);
            driver.SetAvailable();
            driver.DepositToWallet(new Money(10000));
            await _driverRepository.AddAsync(driver);

            // Act
            var result = await _matchingService.MatchDriverToTripAsync(trip.Id, driverId);

            // Assert
            Assert.IsTrue(result);
            var updatedTrip = await _tripRepository.GetByIdAsync(trip.Id);
            Assert.AreEqual(driverId, updatedTrip.DriverId);
            Assert.IsTrue(updatedTrip.IsMatched());

            var updatedDriver = await _driverRepository.GetByIdAsync(driverId);
            Assert.IsTrue(updatedDriver.IsOnTrip());
        }

        [TestMethod]
        public async Task MatchDriverToTripAsync_ShouldFail_WhenWalletInsufficient()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            
            var location = new Location(new Coordinate(0, 0), new Address("S", "1", "D", "C", "C"));
            var route = new Route(location, location, 1.0, TimeSpan.FromMinutes(1), "R");
            var fare = new Fare(new Money(50000), new Money(5000));
            var trip = new Trip(Guid.NewGuid(), passengerId, null, route, fare, VehicleType.Car, DateTime.UtcNow, "Searching", false);
            await _tripRepository.AddAsync(trip);

            var car = new Car(null, "ABC", "T", "V", "R", 4);
            await _vehicleRepository.AddAsync(car);
            var vehicleId = car.Id;

            var driver = new Driver(driverId, "D", "0123456789", "p", "L", vehicleId, location);
            driver.SetAvailable();
            // Wallet is 0, commission is 5000
            await _driverRepository.AddAsync(driver);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
                _matchingService.MatchDriverToTripAsync(trip.Id, driverId));
        }
    }
}