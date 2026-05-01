using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class AddressTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesAddress()
        {
            // Arrange
            string name = "Home";
            string street = "123 Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";

            // Act
            Address address = new Address(name, street, district, city, country);

            // Assert
            Assert.AreEqual(name, address.Name);
            Assert.AreEqual(street, address.Street);
            Assert.AreEqual(district, address.District);
            Assert.AreEqual(city, address.City);
            Assert.AreEqual(country, address.Country);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyName_ThrowsException()
        {
            // Arrange
            string name = "";
            string street = "123 Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";

            // Act
            Address address = new Address(name, street, district, city, country);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithWhiteSpaceName_ThrowsException()
        {
            // Arrange
            string name = "   ";
            string street = "123 Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";

            // Act
            Address address = new Address(name, street, district, city, country);
        }

        #endregion

        #region Optional Parameters Tests

        [TestMethod]
        public void Constructor_WithHouseNumber_SetsHouseNumber()
        {
            // Arrange
            string name = "Home";
            string street = "123 Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";
            string houseNumber = "123";

            // Act
            Address address = new Address(name, street, district, city, country, houseNumber);

            // Assert
            Assert.AreEqual(houseNumber, address.HouseNumber);
        }

        [TestMethod]
        public void Constructor_WithOsmValue_SetsOsmValue()
        {
            // Arrange
            string name = "Home";
            string street = "Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";
            string osmValue = "node:12345";

            // Act
            Address address = new Address(name, street, district, city, country, null, osmValue);

            // Assert
            Assert.AreEqual(osmValue, address.Osm_Value);
        }

        [TestMethod]
        public void Constructor_WithLocality_SetsLocality()
        {
            // Arrange
            string name = "Home";
            string street = "Main St";
            string district = "District 1";
            string city = "HCMC";
            string country = "Vietnam";
            string locality = "Area 1";

            // Act
            Address address = new Address(name, street, district, city, country, null, null, locality);

            // Assert
            Assert.AreEqual(locality, address.Locality);
        }

        #endregion

        #region Value Equality Tests

        [TestMethod]
        public void Equals_SameAddress_ReturnsTrue()
        {
            // Arrange
            Address addr1 = new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam");
            Address addr2 = new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam");

            // Act & Assert
            Assert.AreEqual(addr1, addr2);
        }

        [TestMethod]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // Arrange
            Address addr1 = new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam");
            Address addr2 = new Address("Office", "123 Main St", "District 1", "HCMC", "Vietnam");

            // Act & Assert
            Assert.AreNotEqual(addr1, addr2);
        }

        #endregion

        #region ToString Tests

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            Address address = new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam");

            // Act
            string result = address.ToString();

            // Assert
            Assert.IsTrue(result.Contains("123 Main St"));
            Assert.IsTrue(result.Contains("District 1"));
            Assert.IsTrue(result.Contains("HCMC"));
        }

        [TestMethod]
        public void ToString_WithName_ReturnsNameWithAddress()
        {
            // Arrange
            Address address = new Address("My Home", "123 Main St", "District 1", "HCMC", "Vietnam");

            // Act
            string result = address.ToString();

            // Assert
            Assert.IsTrue(result.Contains("My Home"));
            Assert.IsTrue(result.Contains("123 Main St"));
        }

        [TestMethod]
        public void ToString_WithoutHouseNumber_UsesStreet()
        {
            // Arrange
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam");

            // Act
            string result = address.ToString();

            // Assert
            Assert.IsTrue(result.Contains("Main St"));
        }

        [TestMethod]
        public void ToString_WithHouseNumber_CombinesWithStreet()
        {
            // Arrange
            Address address = new Address("Home", "Main St", "District 1", "HCMC", "Vietnam", "123");

            // Act
            string result = address.ToString();

            // Assert
            Assert.IsTrue(result.Contains("123 Main St"));
        }

        #endregion
    }
}
