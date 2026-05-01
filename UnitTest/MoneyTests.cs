using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class MoneyTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidAmount_CreatesMoney()
        {
            // Arrange & Act
            Money money = new Money(10000m, "VND");

            // Assert
            Assert.AreEqual(10000m, money.Amount);
            Assert.AreEqual("VND", money.Currency);
        }

        [TestMethod]
        public void Constructor_WithDefaultCurrency_UsesVND()
        {
            // Arrange & Act
            Money money = new Money(5000m);

            // Assert
            Assert.AreEqual(5000m, money.Amount);
            Assert.AreEqual("VND", money.Currency);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WithNegativeAmount_ThrowsException()
        {
            // Arrange & Act
            Money money = new Money(-100m, "VND");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyCurrency_ThrowsException()
        {
            // Arrange & Act
            Money money = new Money(100m, "");
        }

        #endregion

        #region Arithmetic Operator Tests

        [TestMethod]
        public void Add_TwoMoneyObjects_ReturnsCorrectSum()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(5000m, "VND");

            // Act
            Money result = money1 + money2;

            // Assert
            Assert.AreEqual(15000m, result.Amount);
        }

        [TestMethod]
        public void Subtract_TwoMoneyObjects_ReturnsCorrectDifference()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(3000m, "VND");

            // Act
            Money result = money1 - money2;

            // Assert
            Assert.AreEqual(7000m, result.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Subtract_NegativeResult_ThrowsException()
        {
            // Arrange
            Money money1 = new Money(3000m, "VND");
            Money money2 = new Money(10000m, "VND");

            // Act
            Money result = money1 - money2;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_DifferentCurrencies_ThrowsException()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(5000m, "USD");

            // Act
            Money result = money1 + money2;
        }

        #endregion

        #region Comparison Operator Tests

        [TestMethod]
        public void LessThan_ReturnsTrueWhenFirstIsSmaller()
        {
            // Arrange
            Money money1 = new Money(5000m, "VND");
            Money money2 = new Money(10000m, "VND");

            // Act & Assert
            Assert.IsTrue(money1 < money2);
        }

        [TestMethod]
        public void GreaterThan_ReturnsTrueWhenFirstIsLarger()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(5000m, "VND");

            // Act & Assert
            Assert.IsTrue(money1 > money2);
        }

        [TestMethod]
        public void LessThanOrEqual_ReturnsTrueWhenEqual()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(10000m, "VND");

            // Act & Assert
            Assert.IsTrue(money1 <= money2);
        }

        [TestMethod]
        public void GreaterThanOrEqual_ReturnsTrueWhenEqual()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(10000m, "VND");

            // Act & Assert
            Assert.IsTrue(money1 >= money2);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameAmountAndCurrency_ReturnsTrue()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(10000m, "VND");

            // Act & Assert
            Assert.AreEqual(money1, money2);
        }

        [TestMethod]
        public void Equals_DifferentAmount_ReturnsFalse()
        {
            // Arrange
            Money money1 = new Money(10000m, "VND");
            Money money2 = new Money(20000m, "VND");

            // Act & Assert
            Assert.AreNotEqual(money1, money2);
        }

        #endregion

        #region ToString Tests

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            Money money = new Money(15000m, "VND");

            // Act
            string result = money.ToString();

            // Assert
            Assert.IsTrue(result.Contains("15,000"));
            Assert.IsTrue(result.Contains("VND"));
        }

        #endregion
    }
}
