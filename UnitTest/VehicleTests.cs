using Domain.Entities;
using Domain.Entities.Vehicles;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class VehicleTests
    {
        #region Car Constructor Tests

        [TestMethod]
        public void Car_Constructor_WithValidParameters_CreatesCar()
        {
            // Arrange & Act
            Car car = new Car(null, "51A-12345", "Toyota", "Vios", "White", 4);

            // Assert
            Assert.AreEqual("51A-12345", car.PlateNumber);
            Assert.AreEqual("Toyota", car.Brand);
            Assert.AreEqual("Vios", car.Model);
            Assert.AreEqual("White", car.Color);
            Assert.AreEqual(4, car.Capacity);
            Assert.AreEqual(VehicleType.Car, car.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithEmptyPlateNumber_ThrowsException()
        {
            // Act
            Car car = new Car(null, "", "Toyota", "Vios", "White", 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithEmptyBrand_ThrowsException()
        {
            // Act
            Car car = new Car(null, "51A-12345", "", "Vios", "White", 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithEmptyModel_ThrowsException()
        {
            // Act
            Car car = new Car(null, "51A-12345", "Toyota", "", "White", 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithEmptyColor_ThrowsException()
        {
            // Act
            Car car = new Car(null, "51A-12345", "Toyota", "Vios", "", 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithZeroCapacity_ThrowsException()
        {
            // Act
            Car car = new Car(null, "51A-12345", "Toyota", "Vios", "White", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Car_Constructor_WithNegativeCapacity_ThrowsException()
        {
            // Act
            Car car = new Car(null, "51A-12345", "Toyota", "Vios", "White", -1);
        }

        #endregion

        #region Car GetAvgSpeed Tests

        [TestMethod]
        public void Car_GetAvgSpeed_Returns60()
        {
            // Arrange
            Car car = new Car(null, "51A-12345", "Toyota", "Vios", "White", 4);

            // Act
            double speed = car.GetAvgSpeed();

            // Assert
            Assert.AreEqual(60, speed);
        }

        #endregion
    }
}
