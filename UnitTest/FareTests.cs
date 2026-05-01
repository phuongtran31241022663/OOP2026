using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class FareTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesFare()
        {
            // Arrange
            Money totalAmount = new Money(50000m);
            Money commission = new Money(7500m);

            // Act
            Fare fare = new Fare(totalAmount, commission);

            // Assert
            Assert.AreEqual(totalAmount, fare.TotalAmount);
            Assert.AreEqual(commission, fare.Commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullTotalAmount_ThrowsException()
        {
            // Arrange
            Money totalAmount = null;
            Money commission = new Money(7500m);

            // Act
            Fare fare = new Fare(totalAmount, commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullCommission_ThrowsException()
        {
            // Arrange
            Money totalAmount = new Money(50000m);
            Money commission = null;

            // Act
            Fare fare = new Fare(totalAmount, commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithCommissionGreaterThanTotalAmount_ThrowsException()
        {
            // Arrange
            Money totalAmount = new Money(10000m);
            Money commission = new Money(15000m);

            // Act
            Fare fare = new Fare(totalAmount, commission);
        }

        #endregion

        #region DriverIncome Tests

        [TestMethod]
        public void Constructor_SetsDriverIncomeCorrectly()
        {
            // Arrange
            Money totalAmount = new Money(50000m);
            Money commission = new Money(7500m);

            // Act
            Fare fare = new Fare(totalAmount, commission);

            // Assert
            Assert.AreEqual(42500m, fare.DriverIncome.Amount);
        }

        [TestMethod]
        public void DriverIncome_WithZeroCommission_ReturnsFullAmount()
        {
            // Arrange
            Money totalAmount = new Money(50000m);
            Money commission = new Money(0m);

            // Act
            Fare fare = new Fare(totalAmount, commission);

            // Assert
            Assert.AreEqual(50000m, fare.DriverIncome.Amount);
        }

        [TestMethod]
        public void DriverIncome_WithCommissionEqualToTotal_ReturnsZero()
        {
            // Arrange
            Money totalAmount = new Money(10000m);
            Money commission = new Money(10000m);

            // Act
            Fare fare = new Fare(totalAmount, commission);

            // Assert
            Assert.AreEqual(0m, fare.DriverIncome.Amount);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameFare_ReturnsTrue()
        {
            // Arrange
            Fare fare1 = new Fare(new Money(50000m), new Money(7500m));
            Fare fare2 = new Fare(new Money(50000m), new Money(7500m));

            // Act & Assert
            Assert.AreEqual(fare1, fare2);
        }

        [TestMethod]
        public void Equals_DifferentTotalAmount_ReturnsFalse()
        {
            // Arrange
            Fare fare1 = new Fare(new Money(50000m), new Money(7500m));
            Fare fare2 = new Fare(new Money(60000m), new Money(7500m));

            // Act & Assert
            Assert.AreNotEqual(fare1, fare2);
        }

        [TestMethod]
        public void Equals_DifferentCommission_ReturnsFalse()
        {
            // Arrange
            Fare fare1 = new Fare(new Money(50000m), new Money(7500m));
            Fare fare2 = new Fare(new Money(50000m), new Money(8000m));

            // Act & Assert
            Assert.AreNotEqual(fare1, fare2);
        }

        #endregion
    }
}
