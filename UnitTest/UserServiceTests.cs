using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private MockDriverRepository _driverRepository;
        private MockPassengerRepository _passengerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _driverRepository = new MockDriverRepository();
            _passengerRepository = new MockPassengerRepository();
            _userService = new UserService(_driverRepository, _passengerRepository);
        }

        [TestMethod]
        public async Task LoginAsync_ShouldReturnDriver_WhenDriverCredentialsAreCorrect()
        {
            // Arrange
            var phone = "0123456789";
            var password = "password123";
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver("Test Driver", phone, password, "DL123", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            // Act
            var result = await _userService.LoginAsync(phone, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Driver));
            Assert.AreEqual(phone, result.Phone);
        }

        [TestMethod]
        public async Task LoginAsync_ShouldReturnPassenger_WhenPassengerCredentialsAreCorrect()
        {
            // Arrange
            var phone = "0987654321";
            var password = "password123";
            var passenger = new Passenger("Test Passenger", phone, password);
            await _passengerRepository.AddAsync(passenger);

            // Act
            var result = await _userService.LoginAsync(phone, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Passenger));
            Assert.AreEqual(phone, result.Phone);
        }

        [TestMethod]
        public async Task LoginAsync_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            // Arrange
            var phone = "0123456789";
            var password = "password123";
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver("Test Driver", phone, password, "DL123", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => 
                _userService.LoginAsync(phone, "wrongpassword"));
        }

        [TestMethod]
        public async Task LoginAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var phone = "0000000000";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
                _userService.LoginAsync(phone, "password123"));
        }

        [TestMethod]
        public async Task RegisterDriverAsync_ShouldSucceed_WhenPhoneIsUnique()
        {
            // Arrange
            var name = "New Driver";
            var phone = "0112233445";
            var password = "password123";
            var license = "DL999";
            var vehicleId = Guid.NewGuid();
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));

            // Act
            await _userService.RegisterDriverAsync(name, phone, password, license, vehicleId, location);

            // Assert
            var driver = await _driverRepository.GetByPhoneAsync(phone);
            Assert.IsNotNull(driver);
            Assert.AreEqual(name, driver.Name);
        }

        [TestMethod]
        public async Task RegisterPassengerAsync_ShouldSucceed_WhenPhoneIsUnique()
        {
            // Arrange
            var name = "New Passenger";
            var phone = "0998877665";
            var password = "password123";

            // Act
            await _userService.RegisterPassengerAsync(name, phone, password);

            // Assert
            var passenger = await _passengerRepository.GetByPhoneAsync(phone);
            Assert.IsNotNull(passenger);
            Assert.AreEqual(name, passenger.Name);
        }

        [TestMethod]
        public async Task RegisterPassengerAsync_ShouldThrowException_WhenPhoneExists()
        {
            // Arrange
            var phone = "0987654321";
            var passenger = new Passenger("Existing Passenger", phone, "password123");
            await _passengerRepository.AddAsync(passenger);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
                _userService.RegisterPassengerAsync("New Name", phone, "newpassword123"));
        }
    }
}