using Domain.Entities;
using Domain.Enums;
using Domain.States;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class TripTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesTrip()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            VehicleType vehicleType = VehicleType.Car;

            // Act
            Trip trip = new Trip(passengerId, route, fare, vehicleType);

            // Assert
            Assert.AreEqual(passengerId, trip.PassengerId);
            Assert.AreEqual(vehicleType, trip.TripVehicleType);
            Assert.IsNotNull(trip.TripRoute);
            Assert.IsNotNull(trip.TripFare);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyPassengerId_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.Empty;
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));

            // Act
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullRoute_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = null;
            Fare fare = new Fare(new Money(50000m), new Money(7500m));

            // Act
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullFare_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = null;

            // Act
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithZeroVehicleType_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            VehicleType vehicleType = 0;

            // Act
            Trip trip = new Trip(passengerId, route, fare, vehicleType);
        }

        #endregion

        #region Initial State Tests

        [TestMethod]
        public void Constructor_InitializesWithRequestedState()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));

            // Act
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);

            // Assert - Trip starts in Requested state
            Assert.AreEqual("Requested", trip.Status);
        }

        #endregion

        #region SetSearching Tests

        [TestMethod]
        public void SetSearching_FromRequested_TransitionsToSearching()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);

            // Act
            trip.SetSearching();

            // Assert
            Assert.IsTrue(trip.IsSearching());
            Assert.AreEqual("Searching", trip.Status);
        }

        [TestMethod]
        public void SetSearching_FromSearching_RemainsInSearching()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching();

            // Act
            trip.SetSearching();

            // Assert - remains in Searching
            Assert.IsTrue(trip.IsSearching());
        }

        #endregion

        #region MatchDriver Tests

        [TestMethod]
        public void MatchDriver_FromSearching_TransitionsToMatched()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching(); // Must transition to Searching first
            Guid driverId = Guid.NewGuid();

            // Act
            trip.MatchDriver(driverId);

            // Assert
            Assert.IsTrue(trip.IsMatched());
            Assert.AreEqual(driverId, trip.DriverId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MatchDriver_FromRequested_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            // Trip is in Requested state - cannot match directly
            
            Guid driverId = Guid.NewGuid();

            // Act
            trip.MatchDriver(driverId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MatchDriver_FromMatched_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching();
            Guid driverId = Guid.NewGuid();
            trip.MatchDriver(driverId);

            // Act - Try to match again
            trip.MatchDriver(Guid.NewGuid());
        }

        #endregion

        #region MarkAsArrived Tests

        [TestMethod]
        public void MarkAsArrived_FromMatched_TransitionsToArrived()
        {
            // Arrange
            Trip trip = CreateMatchedTrip();

            // Act
            trip.MarkAsArrived();

            // Assert
            Assert.IsTrue(trip.IsArrived());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MarkAsArrived_FromSearching_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching(); // Now in Searching state

            // Act
            trip.MarkAsArrived();
        }

        #endregion

        #region StartTrip Tests

        [TestMethod]
        public void StartTrip_FromArrived_TransitionsToStarted()
        {
            // Arrange
            Trip trip = CreateArrivedTrip();

            // Act
            trip.StartTrip();

            // Assert
            Assert.IsTrue(trip.IsStarted());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartTrip_FromSearching_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching();

            // Act
            trip.StartTrip();
        }

        #endregion

        #region CompleteTrip Tests

        [TestMethod]
        public void CompleteTrip_FromStarted_TransitionsToCompleted()
        {
            // Arrange
            Trip trip = CreateStartedTrip();

            // Act
            trip.CompleteTrip();

            // Assert
            Assert.IsTrue(trip.IsCompleted());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CompleteTrip_FromSearching_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);
            trip.SetSearching();

            // Act
            trip.CompleteTrip();
        }

        #endregion

        #region Cancel Tests

        [TestMethod]
        public void Cancel_FromRequested_TransitionsToCancelled()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            // Note: Trip starts in Requested but we need to call SetSearching for full flow

            // Act
            trip.Cancel("User cancelled");

            // Assert
            Assert.IsTrue(trip.IsCancelled());
        }

        [TestMethod]
        public void Cancel_FromSearching_TransitionsToCancelled()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching();

            // Act
            trip.Cancel("User cancelled");

            // Assert
            Assert.IsTrue(trip.IsCancelled());
        }

        [TestMethod]
        public void Cancel_FromMatched_TransitionsToCancelled()
        {
            // Arrange
            Trip trip = CreateMatchedTrip();

            // Act
            trip.Cancel("User cancelled");

            // Assert
            Assert.IsTrue(trip.IsCancelled());
        }

        [TestMethod]
        public void Cancel_FromArrived_TransitionsToCancelled()
        {
            // Arrange
            Trip trip = CreateArrivedTrip();

            // Act
            trip.Cancel("User cancelled");

            // Assert
            Assert.IsTrue(trip.IsCancelled());
        }

[TestMethod]
        public void Cancel_FromStarted_TransitionsToCancelled()
        {
            // Arrange
            Trip trip = CreateStartedTrip();

            // Act
            trip.Cancel("User cancelled");

            // Assert - StartedState actually allows Cancel!
            Assert.IsTrue(trip.IsCancelled());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Cancel_FromCompleted_ThrowsException()
        {
            // Arrange
            Trip trip = CreateCompletedTrip();

            // Act
            trip.Cancel("User cancelled");
        }

        #endregion

        #region MarkTimeout Tests

        [TestMethod]
        public void MarkTimeout_FromSearching_TransitionsToTimeout()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching();

            // Act
            trip.MarkTimeout();

            // Assert
            Assert.IsTrue(trip.IsTimeout());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MarkTimeout_FromRequested_ThrowsException()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);

            // Act
            trip.MarkTimeout();
        }

        #endregion

        #region IsTerminal Tests

        [TestMethod]
        public void IsTerminal_WhenCompleted_ReturnsTrue()
        {
            // Arrange
            Trip trip = CreateCompletedTrip();

            // Assert
            Assert.IsTrue(trip.IsTerminal());
        }

        [TestMethod]
        public void IsTerminal_WhenCancelled_ReturnsTrue()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching();
            trip.Cancel("User cancelled");

            // Assert
            Assert.IsTrue(trip.IsTerminal());
        }

        [TestMethod]
        public void IsTerminal_WhenTimeout_ReturnsTrue()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching();
            trip.MarkTimeout();

            // Assert
            Assert.IsTrue(trip.IsTerminal());
        }

        [TestMethod]
        public void IsTerminal_WhenSearching_ReturnsFalse()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching();

            // Assert
            Assert.IsFalse(trip.IsTerminal());
        }

        #endregion

        #region Payment Tests

        [TestMethod]
        public void ConfirmPayment_WhenNotPaid_SetsPaidToTrue()
        {
            // Arrange
            Trip trip = CreateMatchedTrip();

            // Act
            trip.ConfirmPayment();

            // Assert
            Assert.IsTrue(trip.IsPaid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfirmPayment_WhenAlreadyPaid_ThrowsException()
        {
            // Arrange
            Trip trip = CreateMatchedTrip();
            trip.ConfirmPayment();

            // Act
            trip.ConfirmPayment();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ConfirmPayment_WhenNoDriver_ThrowsException()
        {
            // Arrange
            Trip trip = CreateSearchingTrip();
            trip.SetSearching(); // Now in Searching with no driver

            // Act
            trip.ConfirmPayment();
        }

        #endregion

        #region Distance and Duration Tests

        [TestMethod]
        public void Distance_ReturnsRouteDistance()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);

            // Act & Assert
            Assert.IsNotNull(trip.Distance);
        }

        [TestMethod]
        public void Duration_ReturnsRouteDuration()
        {
            // Arrange
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            Trip trip = new Trip(passengerId, route, fare, VehicleType.Car);

            // Act & Assert
            Assert.IsNotNull(trip.Duration);
        }

        #endregion

        #region Helper Methods

        private Route CreateValidRoute()
        {
            return new Route(
                new Location(
                    new Coordinate(10.7769, 106.7009), 
                    new Address("Pickup Point", "Main St", "District 1", "HCMC", "Vietnam")),
                new Location(
                    new Coordinate(10.7879, 106.7200), 
                    new Address("Destination", "Office St", "District 3", "HCMC", "Vietnam")),
                5.0,
                TimeSpan.FromMinutes(15),
                "");
        }

        private Trip CreateSearchingTrip()
        {
            Guid passengerId = Guid.NewGuid();
            Route route = CreateValidRoute();
            Fare fare = new Fare(new Money(50000m), new Money(7500m));
            return new Trip(passengerId, route, fare, VehicleType.Car);
        }

        private Trip CreateMatchedTrip()
        {
            Trip trip = CreateSearchingTrip();
            trip.SetSearching(); // Must call SetSearching to transition to Searching state first
            Guid driverId = Guid.NewGuid();
            trip.MatchDriver(driverId);
            return trip;
        }

        private Trip CreateArrivedTrip()
        {
            Trip trip = CreateMatchedTrip();
            trip.MarkAsArrived();
            return trip;
        }

        private Trip CreateStartedTrip()
        {
            Trip trip = CreateArrivedTrip();
            trip.StartTrip();
            return trip;
        }

        private Trip CreateCompletedTrip()
        {
            Trip trip = CreateStartedTrip();
            trip.CompleteTrip();
            return trip;
        }

        #endregion
    }
}
