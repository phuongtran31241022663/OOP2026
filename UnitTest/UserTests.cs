using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UserTests
    {
        #region User Validation Tests (using Driver as concrete implementation)

        [TestMethod]
        public void Driver_Constructor_WithValidData_CreatesDriver()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "password123";
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.NewGuid();
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);

            // Assert
            Assert.AreEqual(name, driver.Name);
            Assert.AreEqual(phone, driver.Phone);
            Assert.AreEqual(licenseNumber, driver.LicenseNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Driver_Constructor_WithEmptyName_ThrowsException()
        {
            // Arrange
            string name = "";
            string phone = "0912345678";
            string password = "password123";
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.NewGuid();
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Driver_Constructor_WithEmptyPhone_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "";
            string password = "password123";
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.NewGuid();
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Driver_Constructor_WithShortPassword_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "12345"; // Less than 6 characters
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.NewGuid();
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Driver_Constructor_WithEmptyLicenseNumber_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "password123";
            string licenseNumber = "";
            Guid vehicleId = Guid.NewGuid();
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Driver_Constructor_WithEmptyVehicleId_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "password123";
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.Empty;
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Driver_Constructor_WithNullPosition_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "password123";
            string licenseNumber = "DL123456";
            Guid vehicleId = Guid.NewGuid();
            Location position = null;

            // Act
            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, position);
        }

        #endregion

        #region UpdateName Tests

        [TestMethod]
        public void UpdateName_WithValidName_UpdatesSuccessfully()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.UpdateName("Jane");

            // Assert
            Assert.AreEqual("Jane", driver.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateName_WithEmptyName_ThrowsException()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.UpdateName("");
        }

        #endregion

        #region UpdatePhone Tests

        [TestMethod]
        public void UpdatePhone_WithValidPhone_UpdatesSuccessfully()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.UpdatePhone("0987654321");

            // Assert
            Assert.AreEqual("0987654321", driver.Phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdatePhone_WithEmptyPhone_ThrowsException()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.UpdatePhone("");
        }

        #endregion

        #region Password Tests

        [TestMethod]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            bool result = driver.VerifyPassword("password123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            bool result = driver.VerifyPassword("wrongpassword");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_WithCorrectOldPassword_ChangesSuccessfully()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.ChangePassword("password123", "newpassword456");

            // Assert
            Assert.IsTrue(driver.VerifyPassword("newpassword456"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangePassword_WithWrongOldPassword_ThrowsException()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.ChangePassword("wrongpassword", "newpassword456");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangePassword_WithSameAsOldPassword_ThrowsException()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            driver.ChangePassword("password123", "password123");
        }

        #endregion

        #region GetInfo Tests

        [TestMethod]
        public void GetInfo_ReturnsFormattedString()
        {
            // Arrange
            var position = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Home", "123 Main St", "District 1", "HCMC", "Vietnam"));
            Driver driver = new Driver("John Doe", "0912345678", "password123", "DL123456", Guid.NewGuid(), position);

            // Act
            string info = driver.GetInfo();

            // Assert
            Assert.IsTrue(info.Contains("John Doe"));
        }

        #endregion
    }
}
