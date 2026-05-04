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
    public class PassengerServiceTests
    {
        private PassengerService _passengerService;
        private MockPassengerRepository _passengerRepository;
        private MockTripService _tripServiceMock;
        private MockReviewService _reviewServiceMock;
        private MockMapService _mapServiceMock;
        private MockFareService _fareServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _passengerRepository = new MockPassengerRepository();
            _tripServiceMock = new MockTripService();
            _reviewServiceMock = new MockReviewService();
            _mapServiceMock = new MockMapService();
            _fareServiceMock = new MockFareService();

            _passengerService = new PassengerService(
                _passengerRepository,
                _tripServiceMock,
                _reviewServiceMock,
                _mapServiceMock,
                _fareServiceMock);
        }

        [TestMethod]
        public async Task RequestTripAsync_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var passenger = new Passenger(passengerId, "P1", "01", "p");
            await _passengerRepository.AddAsync(passenger);

            var pickup = new Location(new Coordinate(0, 0), new Address("P", "1", "D", "C", "C"));
            var dest = new Location(new Coordinate(1, 1), new Address("D", "2", "D", "C", "C"));
            var route = new Route(pickup, dest, 1.0, TimeSpan.FromMinutes(1), "R");
            _mapServiceMock.SetRouteToReturn(route);

            var fare = new Fare(new Money(10000), new Money(1000));
            _fareServiceMock.SetFareToReturn(fare);

            var trip = new Trip(Guid.NewGuid(), passengerId, null, route, fare, VehicleType.Car, DateTime.UtcNow, "Searching", false);
            _tripServiceMock.SetTripToReturn(trip);

            // Act
            var result = await _passengerService.RequestTripAsync(passengerId, pickup, dest, VehicleType.Car);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(passengerId, result.PassengerId);
        }

        [TestMethod]
        public async Task CancelTripAsync_ShouldSucceed_WhenOwner()
        {
            // Arrange
            var passengerId = Guid.NewGuid();
            var tripId = Guid.NewGuid();
            var pickup = new Location(new Coordinate(10, 10), new Address("P", "1", "D", "C", "C"));
            var dest = new Location(new Coordinate(11, 11), new Address("D", "2", "D", "C", "C"));
            var route = new Route(pickup, dest, 1.5, TimeSpan.FromMinutes(5), "Route");
            var fare = new Fare(new Money(20000), new Money(2000));
            var trip = new Trip(tripId, passengerId, null, route, fare, VehicleType.Car, DateTime.UtcNow, "Searching", false);

            _tripServiceMock.SetTripToReturn(trip);
            _tripServiceMock.SetCanCancel(true);

            // Act
            await _passengerService.CancelTripAsync(passengerId, tripId, "Reason");

            // Assert
            // Verify cancel was called on trip service
            Assert.IsTrue(_tripServiceMock.CancelTripCalled);
        }
    }
}