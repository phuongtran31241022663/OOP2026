using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class RouteTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesRoute()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            double distance = 5.0;
            TimeSpan duration = TimeSpan.FromMinutes(15);
            string polyline = "test_polyline";

            // Act
            Route route = new Route(pickup, destination, distance, duration, polyline);

            // Assert
            Assert.AreEqual(pickup, route.Pickup);
            Assert.AreEqual(destination, route.Destination);
            Assert.AreEqual(distance, route.Distance);
            Assert.AreEqual(duration, route.Duration);
            Assert.AreEqual(polyline, route.Polyline);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullPickup_ThrowsException()
        {
            // Arrange
            Location pickup = null;
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            double distance = 5.0;
            TimeSpan duration = TimeSpan.FromMinutes(15);

            // Act
            Route route = new Route(pickup, destination, distance, duration, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullDestination_ThrowsException()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = null;
            double distance = 5.0;
            TimeSpan duration = TimeSpan.FromMinutes(15);

            // Act
            Route route = new Route(pickup, destination, distance, duration, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithZeroDistance_ThrowsException()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            double distance = 0;
            TimeSpan duration = TimeSpan.FromMinutes(15);

            // Act
            Route route = new Route(pickup, destination, distance, duration, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativeDistance_ThrowsException()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            double distance = -5.0;
            TimeSpan duration = TimeSpan.FromMinutes(15);

            // Act
            Route route = new Route(pickup, destination, distance, duration, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativeDuration_ThrowsException()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            double distance = 5.0;
            TimeSpan duration = TimeSpan.FromMinutes(-15);

            // Act
            Route route = new Route(pickup, destination, distance, duration, "");
        }

        #endregion

        #region Optional Parameters Tests

        [TestMethod]
        public void Constructor_WithNullPolyline_SetsEmptyString()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");

            // Act
            Route route = new Route(pickup, destination, 5.0, TimeSpan.FromMinutes(15), null);

            // Assert
            Assert.AreEqual(string.Empty, route.Polyline);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameRoute_ReturnsTrue()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            Route route1 = new Route(pickup, destination, 5.0, TimeSpan.FromMinutes(15), "");
            Route route2 = new Route(pickup, destination, 5.0, TimeSpan.FromMinutes(15), "");

            // Act & Assert
            Assert.AreEqual(route1, route2);
        }

        [TestMethod]
        public void Equals_DifferentDistance_ReturnsFalse()
        {
            // Arrange
            Location pickup = CreateLocation(10.7769, 106.7009, "Pickup");
            Location destination = CreateLocation(10.7879, 106.7200, "Destination");
            Route route1 = new Route(pickup, destination, 5.0, TimeSpan.FromMinutes(15), "");
            Route route2 = new Route(pickup, destination, 10.0, TimeSpan.FromMinutes(15), "");

            // Act & Assert
            Assert.AreNotEqual(route1, route2);
        }

        #endregion

        #region Helper Methods

        private Location CreateLocation(double lat, double lng, string name)
        {
            return new Location(
                new Coordinate(lat, lng),
                new Address(name, "Main St", "District 1", "HCMC", "Vietnam"));
        }

        #endregion
    }
}
