using Domain.Entities.Vehicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class MotorbikeTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesMotorbike()
        {
            // Arrange & Act
            Motorbike motorbike = new Motorbike(null, "51A-12345", "Honda", "SH", "Black");

            // Assert
            Assert.AreEqual("51A-12345", motorbike.PlateNumber);
            Assert.AreEqual("Honda", motorbike.Brand);
            Assert.AreEqual("SH", motorbike.Model);
            Assert.AreEqual("Black", motorbike.Color);
            Assert.AreEqual(VehicleType.Motorbike, motorbike.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyPlateNumber_ThrowsException()
        {
            // Act
            Motorbike motorbike = new Motorbike(null, "", "Honda", "SH", "Black");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyBrand_ThrowsException()
        {
            // Act
            Motorbike motorbike = new Motorbike(null, "51A-12345", "", "SH", "Black");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyModel_ThrowsException()
        {
            // Act
            Motorbike motorbike = new Motorbike(null, "51A-12345", "Honda", "", "Black");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyColor_ThrowsException()
        {
            // Act
            Motorbike motorbike = new Motorbike(null, "51A-12345", "Honda", "SH", "");
        }

        #endregion

        #region GetAvgSpeed Tests

        [TestMethod]
        public void GetAvgSpeed_Returns40()
        {
            // Arrange
            Motorbike motorbike = new Motorbike(null, "51A-12345", "Honda", "SH", "Black");

            // Act
            double speed = motorbike.GetAvgSpeed();

            // Assert
            Assert.AreEqual(40, speed);
        }

        #endregion

        #region Capacity Tests

        [TestMethod]
        public void Constructor_SetsCapacityTo2()
        {
            // Arrange
            Motorbike motorbike = new Motorbike(null, "51A-12345", "Honda", "SH", "Black");

            // Act & Assert
            Assert.AreEqual(2, motorbike.Capacity);
        }

        #endregion
    }
}
