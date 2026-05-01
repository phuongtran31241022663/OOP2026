using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class FareRuleTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesFareRule()
        {
            // Arrange
            VehicleType vehicleType = VehicleType.Car;
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            decimal commissionRate = 0.15m;

            // Act
            FareRule fareRule = new FareRule(vehicleType, baseFare, pricePerKm, commissionRate);

            // Assert
            Assert.AreEqual(vehicleType, fareRule.VehicleType);
            Assert.AreEqual(baseFare, fareRule.BaseFare);
            Assert.AreEqual(pricePerKm, fareRule.PricePerKm);
            Assert.AreEqual(commissionRate, fareRule.CommissionRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativeBaseFare_ThrowsException()
        {
            // Arrange
            Money baseFare = new Money(-1000m);
            Money pricePerKm = new Money(5000m);

            // Act
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativePricePerKm_ThrowsException()
        {
            // Arrange
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(-5000m);

            // Act
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativeCommissionRate_ThrowsException()
        {
            // Arrange
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            decimal commissionRate = -0.1m;

            // Act
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, commissionRate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithCommissionRateGreaterThan1_ThrowsException()
        {
            // Arrange
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            decimal commissionRate = 1.5m;

            // Act
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, commissionRate);
        }

        #endregion

        #region CalculateFare Tests

        [TestMethod]
        public void CalculateFare_WithZeroDistance_ReturnsBaseFare()
        {
            // Arrange
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);

            // Act
            Fare fare = fareRule.CalculateFare(0);

            // Assert
            Assert.AreEqual(baseFare.Amount, fare.TotalAmount.Amount);
        }

        [TestMethod]
        public void CalculateFare_WithNormalDistance_ReturnsCorrectFare()
        {
            // Arrange
            // Base fare: 12000 VND
            // Per km: 5000 VND
            // Distance: 5 km
            // Expected: 12000 + (5000 * 5) = 37000 VND
            // Commission (15%): 37000 * 0.15 = 5550 VND
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);

            // Act
            Fare fare = fareRule.CalculateFare(5.0);

            // Assert
            Assert.AreEqual(37000m, fare.TotalAmount.Amount);
            Assert.AreEqual(5550m, fare.Commission.Amount);
        }

        [TestMethod]
        public void CalculateFare_WithLongDistance_ReturnsCorrectFare()
        {
            // Arrange
            // Base fare: 12000 VND
            // Per km: 5000 VND
            // Distance: 20 km
            // Expected: 12000 + (5000 * 20) = 112000 VND
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);

            // Act
            Fare fare = fareRule.CalculateFare(20.0);

            // Assert
            Assert.AreEqual(112000m, fare.TotalAmount.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CalculateFare_WithNegativeDistance_ThrowsException()
        {
            // Arrange
            Money baseFare = new Money(12000m);
            Money pricePerKm = new Money(5000m);
            FareRule fareRule = new FareRule(VehicleType.Car, baseFare, pricePerKm, 0.15m);

            // Act
            Fare fare = fareRule.CalculateFare(-5.0);
        }

        [TestMethod]
        public void CalculateFare_DifferentVehicleTypes_ReturnsCorrectFare()
        {
            // Arrange - Motorbike typically cheaper
            Money baseFare = new Money(8000m);
            Money pricePerKm = new Money(3000m);
            FareRule fareRule = new FareRule(VehicleType.Motorbike, baseFare, pricePerKm, 0.10m);

            // Act
            Fare fare = fareRule.CalculateFare(10.0);

            // Assert
            // 8000 + (3000 * 10) = 38000 VND
            Assert.AreEqual(38000m, fare.TotalAmount.Amount);
            // 38000 * 0.10 = 3800 VND
            Assert.AreEqual(3800m, fare.Commission.Amount);
        }

        #endregion
    }
}
