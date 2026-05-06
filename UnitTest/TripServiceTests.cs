using Application.Events;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Entities.Vehicles;
using Domain.Repositories;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Mocks;

// Mock services for testing
public class MockMapService : IMapService
{
    private Route _routeToReturn;

    public void SetRouteToReturn(Route route)
    {
        _routeToReturn = route;
    }

    public Task<Route> GetRouteAsync(Location start, Location end)
    {
        return Task.FromResult(_routeToReturn);
    }

    public Task<List<Location>> SearchLocationAsync(string query)
    {
        return Task.FromResult(new List<Location>());
    }

    public Task<Location> ReverseGeocodeAsync(double latitude, double longitude)
    {
        return Task.FromResult(new Location(new Coordinate(latitude, longitude), new Address("Mock", "Address", "District", "City", "Country")));
    }

    public Task<List<Location>> GetPOIsAsync(double minLat, double minLon, double maxLat, double maxLon)
    {
        return Task.FromResult(new List<Location>());
    }

    public Task<Location> GetIpLocationAsync()
    {
        return Task.FromResult(new Location(new Coordinate(10.7769, 106.7009), new Address("Mock", "IP Address", "District", "HCMC", "Vietnam")));
    }

    public Task<Location> GeocodeNominatimAsync(string query)
    {
        return Task.FromResult(new Location(new Coordinate(10.7769, 106.7009), new Address("Mock", query, "District", "HCMC", "Vietnam")));
    }
}

public class MockFareService : IFareService
{
    private Fare _fareToReturn;

    public void SetFareToReturn(Fare fare)
    {
        _fareToReturn = fare;
    }

    public Task<Fare> CalculateFareAsync(string vehicleType, double distance)
    {
        return Task.FromResult(_fareToReturn);
    }
}

namespace UnitTest
{
    [TestClass]
    public class TripServiceTests
    {
        private TripService _tripService;
        private MockDriverRepository _driverRepository;
        private MockPassengerRepository _passengerRepository;
        private MockTripRepository _tripRepository;
        private MockReviewRepository _reviewRepository;
        private MockFareRuleRepository _fareRuleRepository;
        private MockVehicleRepository _vehicleRepository;
        private MockMapService _mapServiceMock;
        private MockFareService _fareServiceMock;

        private Driver _testDriver;
        private Passenger _testPassenger;
        private Car _testCar;
        private FareRule _testFareRule;
        private Fare _testFare;
        private Route _testRoute;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize repositories
            _driverRepository = new MockDriverRepository();
            _passengerRepository = new MockPassengerRepository();
            _tripRepository = new MockTripRepository();
            _reviewRepository = new MockReviewRepository();
            _fareRuleRepository = new MockFareRuleRepository();
            _vehicleRepository = new MockVehicleRepository();

            // Initialize mock services
            _mapServiceMock = new MockMapService();
            _fareServiceMock = new MockFareService();
            var matchingServiceMock = new Mocks.MockMatchingService();

            // Create test service
            _tripService = new TripService(
                _tripRepository,
                _driverRepository,
                _passengerRepository,
                _vehicleRepository,
                _fareServiceMock,
                _mapServiceMock,
                matchingServiceMock
            );

            // Setup test data
            _testPassenger = new Passenger("Test Passenger", "0987654321", "password123");
            _passengerRepository.AddAsync(_testPassenger).Wait();

            _testCar = new Car(null, "ABC123", "Toyota", "Vios", "Red", 4);
            _vehicleRepository.AddAsync(_testCar).Wait();

            var testPosition = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Test St", "District 1", "HCMC", "Vietnam"));
            _testDriver = new Driver("Test Driver", "0123456789", "password123", "DL123456", _testCar.Id, testPosition);
            _testDriver.DepositToWallet(new Money(100000));
            _driverRepository.AddAsync(_testDriver).Wait();

            _testFareRule = new FareRule("Car", new Money(10000), new Money(5000), 0.1m);
            _fareRuleRepository.AddAsync(_testFareRule).Wait();

            _testFare = new Fare(new Money(50000), new Money(5000));
            _testRoute = new Route(
                new Location(new Coordinate(10.7769, 106.7009), new Address("Home", "Pickup St", "District 1", "HCMC", "Vietnam")),
                new Location(new Coordinate(10.7869, 106.7109), new Address("Home", "Dest St", "District 2", "HCMC", "Vietnam")),
                5.0,
                TimeSpan.FromMinutes(15),
                "Test Route");

            // Setup mock behaviors
            _mapServiceMock.SetRouteToReturn(_testRoute);
            _fareServiceMock.SetFareToReturn(_testFare);
        }

        [TestMethod]
        public async Task RequestTripAsync_ShouldCreateTrip_WhenPassengerExists()
        {
            // Arrange
            var pickupLocation = new Location(new Coordinate(10.7769, 106.7009), new Address("Home", "Pickup St", "District 1", "HCMC", "Vietnam"));
            var destinationLocation = new Location(new Coordinate(10.7869, 106.7109), new Address("Home", "Dest St", "District 2", "HCMC", "Vietnam"));
            var vehicleType = "Car";

            // Act
            var trip = await _tripService.RequestTripAsync(_testPassenger.Id, pickupLocation, destinationLocation, vehicleType);

            // Assert
            Assert.IsNotNull(trip);
            Assert.AreEqual(_testPassenger.Id, trip.PassengerId);
            Assert.AreEqual("Searching", trip.Status);
            Assert.IsTrue(trip.IsSearching());

            var savedTrip = await _tripRepository.GetByIdAsync(trip.Id);
            Assert.IsNotNull(savedTrip);
            Assert.AreEqual(trip.Id, savedTrip.Id);
        }

        [TestMethod]
        public async Task RequestTripAsync_ShouldThrowException_WhenPassengerDoesNotExist()
        {
            // Arrange
            var nonExistentPassengerId = Guid.NewGuid();
            var pickupLocation = new Location(new Coordinate(10.7769, 106.7009), new Address("Home", "Pickup St", "District 1", "HCMC", "Vietnam"));
            var destinationLocation = new Location(new Coordinate(10.7869, 106.7109), new Address("Home", "Dest St", "District 2", "HCMC", "Vietnam"));
            var vehicleType = "Car";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _tripService.RequestTripAsync(nonExistentPassengerId, pickupLocation, destinationLocation, vehicleType));
        }

        [TestMethod]
        public async Task RequestTripAsync_ShouldThrowException_WhenRouteNotFound()
        {
            // Arrange
            var pickupLocation = new Location(new Coordinate(10.7769, 106.7009), new Address("Home", "Pickup St", "District 1", "HCMC", "Vietnam"));
            var destinationLocation = new Location(new Coordinate(10.7869, 106.7109), new Address("Home", "Dest St", "District 2", "HCMC", "Vietnam"));
            var vehicleType = "Car";

            _mapServiceMock.SetRouteToReturn(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _tripService.RequestTripAsync(_testPassenger.Id, pickupLocation, destinationLocation, vehicleType));
        }

        [TestMethod]
        public async Task MatchDriverAsync_ShouldMatchDriver_WhenDriverAndTripAreValid()
        {
            // Arrange
            var trip = await _tripService.RequestTripAsync(_testPassenger.Id,
                new Location(new Coordinate(10.7769, 106.7009), new Address("Home", "Pickup St", "District 1", "HCMC", "Vietnam")),
                new Location(new Coordinate(10.7869, 106.7109), new Address("Home", "Dest St", "District 2", "HCMC", "Vietnam")),
                "Car");
            _testDriver.SetAvailable();
            await _driverRepository.UpdateAsync(_testDriver);

            // Act
            await _tripService.MatchDriverAsync(trip.Id, _testDriver.Id);

            // Assert
            var updatedTrip = await _tripRepository.GetByIdAsync(trip.Id);
            var updatedDriver = await _driverRepository.GetByIdAsync(_testDriver.Id);

            Assert.IsNotNull(updatedTrip);
            Assert.AreEqual("Matched", updatedTrip.Status);
            Assert.AreEqual(_testDriver.Id, updatedTrip.DriverId);
            Assert.IsTrue(updatedTrip.IsMatched());

            Assert.IsNotNull(updatedDriver);
            Assert.AreEqual("DriverOnTrip", updatedDriver.Status.ToString());
            Assert.IsTrue(updatedDriver.IsOnTrip());
        }
    }
}