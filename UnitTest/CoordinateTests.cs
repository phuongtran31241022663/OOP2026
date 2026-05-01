using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class CoordinateTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidCoordinates_CreatesCoordinate()
        {
            // Arrange
            double lat = 10.7769;
            double lng = 106.7009;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);

            // Assert
            Assert.AreEqual(lat, coordinate.Latitude);
            Assert.AreEqual(lng, coordinate.Longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithLatitudeTooLow_ThrowsException()
        {
            // Arrange
            double lat = -91; // Less than -90
            double lng = 106.7009;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithLatitudeTooHigh_ThrowsException()
        {
            // Arrange
            double lat = 91; // Greater than 90
            double lng = 106.7009;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithLongitudeTooLow_ThrowsException()
        {
            // Arrange
            double lat = 10.7769;
            double lng = -181; // Less than -180

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithLongitudeTooHigh_ThrowsException()
        {
            // Arrange
            double lat = 10.7769;
            double lng = 181; // Greater than 180

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);
        }

        #endregion

        #region Boundary Tests

        [TestMethod]
        public void Constructor_WithMinLatitude_CreatesCoordinate()
        {
            // Arrange
            double lat = -90;
            double lng = 0;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);

            // Assert
            Assert.AreEqual(-90, coordinate.Latitude);
        }

        [TestMethod]
        public void Constructor_WithMaxLatitude_CreatesCoordinate()
        {
            // Arrange
            double lat = 90;
            double lng = 0;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);

            // Assert
            Assert.AreEqual(90, coordinate.Latitude);
        }

        [TestMethod]
        public void Constructor_WithMinLongitude_CreatesCoordinate()
        {
            // Arrange
            double lat = 0;
            double lng = -180;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);

            // Assert
            Assert.AreEqual(-180, coordinate.Longitude);
        }

        [TestMethod]
        public void Constructor_WithMaxLongitude_CreatesCoordinate()
        {
            // Arrange
            double lat = 0;
            double lng = 180;

            // Act
            Coordinate coordinate = new Coordinate(lat, lng);

            // Assert
            Assert.AreEqual(180, coordinate.Longitude);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameCoordinates_ReturnsTrue()
        {
            // Arrange
            Coordinate coord1 = new Coordinate(10.7769, 106.7009);
            Coordinate coord2 = new Coordinate(10.7769, 106.7009);

            // Act & Assert
            Assert.AreEqual(coord1, coord2);
        }

        [TestMethod]
        public void Equals_DifferentCoordinates_ReturnsFalse()
        {
            // Arrange
            Coordinate coord1 = new Coordinate(10.7769, 106.7009);
            Coordinate coord2 = new Coordinate(10.8000, 106.7009);

            // Act & Assert
            Assert.AreNotEqual(coord1, coord2);
        }

        #endregion
    }
}
