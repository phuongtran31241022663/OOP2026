using Application.Interfaces;
using Application.Services;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MapServiceTests
    {
        private MapService _mapService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mapService = new MapService(new HttpClient());
            // Clear cache before each test to ensure test isolation
            _mapService.ClearCache();
            // Enable test mode to use mock data instead of real API
            _mapService.EnableTestMode();
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldReturnEmptyList_WhenQueryIsEmpty()
        {
            // Arrange
            var query = "";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldReturnEmptyList_WhenQueryIsWhitespace()
        {
            // Arrange
            var query = "   ";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldReturnEmptyList_WhenQueryIsNull()
        {
            // Arrange
            string query = null;

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldReturnResults_WhenQueryMatchesPredefinedLocation()
        {
            // Arrange
            var query = "Quận 1";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            // Verify at least one result contains "Quận 1"
            bool found = false;
            foreach (var location in result)
            {
                if (location.Address.District.Contains("Quận 1"))
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, "Expected at least one location with Quận 1");

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.District, cachedResult[0].Address.District);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldSearchByCity_WhenQueryMatchesCity()
        {
            // Arrange
            var query = "Hồ Chí Minh";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Exists(l => l.Address.City.Contains("Hồ Chí Minh")));

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.City, cachedResult[0].Address.City);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldSearchByStreet_WhenQueryMatchesStreet()
        {
            // Arrange
            var query = "Lê Lợi";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Exists(l => l.Address.Street.Contains("Lê Lợi")));

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.Street, cachedResult[0].Address.Street);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldSearchByCity_WhenQueryHasNoDiacritics()
        {
            // Arrange
            var query = "Ho Chi Minh";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Exists(location => location.Address.City.Contains("Hồ Chí Minh")));

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.City, cachedResult[0].Address.City);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldSearchByDistrict_WhenQueryHasNoDiacritics()
        {
            // Arrange
            var query = "Quan 1";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Exists(location => location.Address.District.Contains("Quận 1")));

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.District, cachedResult[0].Address.District);
        }

        [TestMethod]
        public async Task SearchLocationAsync_ShouldSearchByStreet_WhenQueryHasNoDiacritics()
        {
            // Arrange
            var query = "Le Loi";

            // Act
            var result = await _mapService.SearchLocationAsync(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Exists(location => location.Address.Street.Contains("Lê Lợi")));

            // Verify caching works
            var cachedResult = await _mapService.SearchLocationAsync(query);
            Assert.AreEqual(result.Count, cachedResult.Count);
            Assert.AreEqual(result[0].Address.Street, cachedResult[0].Address.Street);
        }

        [TestMethod]
        public async Task ReverseGeocodeAsync_ShouldReturnUnknownAddress_WhenCoordinatesFarFromPredefined()
        {
            // Arrange - coordinates far from any predefined location
            var lat = 0.0;
            var lon = 0.0;

            // Act
            var result = await _mapService.ReverseGeocodeAsync(lat, lon);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Coordinate);
            Assert.AreEqual(lat, result.Coordinate.Latitude);
            Assert.AreEqual(lon, result.Coordinate.Longitude);
        }

        [TestMethod]
        public async Task ReverseGeocodeAsync_ShouldReturnPredefinedLocation_WhenCoordinatesClose()
        {
            // Arrange - coordinates very close to predefined location (Ho Chi Minh)
            var lat = 10.7769;
            var lon = 106.7009;

            // Act
            var result = await _mapService.ReverseGeocodeAsync(lat, lon);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            // Should return the predefined location
            Assert.AreEqual("Bến Nghé", result.Address.Name);
        }

        [TestMethod]
        public async Task GetRouteAsync_ShouldReturnRoute_WhenValidLocations()
        {
            // Arrange
            var start = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Start", "Street1", "District1", "Hồ Chí Minh", "Vietnam"));
            var end = new Location(
                new Coordinate(10.7826, 106.6954),
                new Address("End", "Street2", "District2", "Hồ Chí Minh", "Vietnam"));

            // Act
            var result = await _mapService.GetRouteAsync(start, end);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Pickup);
            Assert.IsNotNull(result.Destination);
            Assert.IsTrue(result.Distance >= 0);
            Assert.IsTrue(result.Duration >= TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetRouteAsync_ShouldThrowException_WhenSameLocation()
        {
            // Arrange
            var location = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Test", "Street", "District", "Hồ Chí Minh", "Vietnam"));

            // Act
            await _mapService.GetRouteAsync(location, location);
        }

        [TestMethod]
        public async Task GetRouteAsync_ShouldReturnRouteBetweenDifferentCities()
        {
            // Arrange
            var hcm = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("HCM", "Street", "District", "Hồ Chí Minh", "Vietnam"));
            var hn = new Location(
                new Coordinate(21.0285, 105.8542),
                new Address("HN", "Street", "District", "Hà Nội", "Vietnam"));

            // Act
            var result = await _mapService.GetRouteAsync(hcm, hn);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Distance > 0);
            // Distance between HCM and HN should be significant
            Assert.IsTrue(result.Distance > 1000, "Expected distance > 1000km between HCM and HN");
        }
    }
}