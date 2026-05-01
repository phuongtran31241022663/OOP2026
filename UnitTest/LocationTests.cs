using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class LocationTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesLocation()
        {
            // Arrange
            Coordinate coordinate = new Coordinate(10.7769, 106.7009);
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");

            // Act
            Location location = new Location(coordinate, address);

            // Assert
            Assert.AreEqual(coordinate, location.Coordinate);
            Assert.AreEqual(address, location.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullCoordinate_ThrowsException()
        {
            // Arrange
            Coordinate coordinate = null;
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");

            // Act
            Location location = new Location(coordinate, address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullAddress_ThrowsException()
        {
            // Arrange
            Coordinate coordinate = new Coordinate(10.7769, 106.7009);
            Address address = null;

            // Act
            Location location = new Location(coordinate, address);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameLocation_ReturnsTrue()
        {
            // Arrange
            Coordinate coordinate = new Coordinate(10.7769, 106.7009);
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");
            Location location1 = new Location(coordinate, address);
            Location location2 = new Location(coordinate, address);

            // Act & Assert
            Assert.AreEqual(location1, location2);
        }

        [TestMethod]
        public void Equals_DifferentCoordinate_ReturnsFalse()
        {
            // Arrange
            Coordinate coordinate1 = new Coordinate(10.7769, 106.7009);
            Coordinate coordinate2 = new Coordinate(10.8000, 106.7009);
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");
            Location location1 = new Location(coordinate1, address);
            Location location2 = new Location(coordinate2, address);

            // Act & Assert
            Assert.AreNotEqual(location1, location2);
        }

        [TestMethod]
        public void Equals_DifferentAddress_ReturnsFalse()
        {
            // Arrange
            Coordinate coordinate = new Coordinate(10.7769, 106.7009);
            Address address1 = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");
            Address address2 = new Address("Office", "Office St", "District 3", "HCMC", "Vietnam");
            Location location1 = new Location(coordinate, address1);
            Location location2 = new Location(coordinate, address2);

            // Act & Assert
            Assert.AreNotEqual(location1, location2);
        }

        #endregion
    }
}
