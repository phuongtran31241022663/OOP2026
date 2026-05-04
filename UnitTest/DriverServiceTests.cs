using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestClass]
    public class DriverServiceTests
    {
        private DriverService _driverService;
        private MockDriverRepository _driverRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _driverRepository = new MockDriverRepository();
            _driverService = new DriverService(_driverRepository);
        }

        [TestMethod]
        public async Task GetDriverAsync_ShouldReturnDriver_WhenExists()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "pass", "DL", Guid.NewGuid(), location);
            await _driverRepository.AddAsync(driver);

            // Act
            var result = await _driverService.GetDriverAsync(driverId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(driverId, result.Id);
        }

        [TestMethod]
        public async Task UpdateLocationAsync_ShouldUpdate_WhenDriverExists()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var oldLocation = new Location(new Coordinate(0, 0), new Address("Old", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "pass", "DL", Guid.NewGuid(), oldLocation);
            await _driverRepository.AddAsync(driver);

            var newLocation = new Location(new Coordinate(1, 1), new Address("New", "456", "Dist", "City", "Country"));

            // Act
            await _driverService.UpdateLocationAsync(driverId, newLocation);

            // Assert
            var updatedDriver = await _driverRepository.GetByIdAsync(driverId);
            Assert.AreEqual(1, updatedDriver.Position.Coordinate.Latitude);
        }

        [TestMethod]
        public async Task SetAvailableAsync_ShouldUpdateStatus()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver = new Driver(driverId, "Driver", "0123456789", "pass", "DL", Guid.NewGuid(), location);
            driver.SetOffline();
            await _driverRepository.AddAsync(driver);

            // Act
            await _driverService.SetAvailableAsync(driverId);

            // Assert
            var updatedDriver = await _driverRepository.GetByIdAsync(driverId);
            Assert.IsTrue(updatedDriver.IsAvailable());
        }

        [TestMethod]
        public async Task GetAvailableDriversAsync_ShouldReturnList()
        {
            // Arrange
            var location = new Location(new Coordinate(0, 0), new Address("Street", "123", "Dist", "City", "Country"));
            var driver1 = new Driver(Guid.NewGuid(), "D1", "0123456781", "p", "L1", Guid.NewGuid(), location);
            driver1.SetAvailable();
            var driver2 = new Driver(Guid.NewGuid(), "D2", "0123456782", "p", "L2", Guid.NewGuid(), location);
            driver2.SetOffline();

            await _driverRepository.AddAsync(driver1);
            await _driverRepository.AddAsync(driver2);

            // Act
            var result = await _driverService.GetAvailableDriversAsync();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("D1", result[0].Name);
        }
    }
}