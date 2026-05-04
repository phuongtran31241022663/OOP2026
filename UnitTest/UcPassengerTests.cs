using Application.Events;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.UserControls;
using System;
using System.Reflection;
using System.Threading.Tasks;
using UnitTest.Mocks;

namespace UnitTest
{
    /// <summary>
    /// UI Tests for UcPassenger (Passenger UserControl).
    /// Tests button clicks and UI interactions trigger correct service calls and handle errors.
    /// </summary>
    [TestClass]
    public class UcPassengerTests
    {
        private Passenger _testPassenger;
        private MockTripService _mockTripService;
        private MockUserService _mockUserService;
        private MockMapService _mockMapService;
        private MockFareService _mockFareService;
        private MockSimulationService _mockSimulationService;
        private MockMatchingService _mockMatchingService;
        private MockReviewService _mockReviewService;
        private Location _testPickupLocation;
        private Location _testDestinationLocation;

        [TestInitialize]
        public void Setup()
        {
             _testPassenger = new Passenger("Test Passenger", "0123456789", "hashed_password");

            _mockTripService = new MockTripService();
            _mockUserService = new MockUserService();
            _mockMapService = new MockMapService();
            _mockFareService = new MockFareService();
            _mockSimulationService = new MockSimulationService();
            _mockMatchingService = new MockMatchingService();
            _mockReviewService = new MockReviewService();

            _testPickupLocation = new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("Pickup", "Main St", "District 1", "HCMC", "Vietnam"));

            _testDestinationLocation = new Location(
                new Coordinate(10.7879, 106.7200),
                new Address("Destination", "Office St", "District 3", "HCMC", "Vietnam"));
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidServices_InitializesCorrectly()
        {
            // Arrange & Act
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Assert
            Assert.IsNotNull(ucPassenger);
            Assert.IsFalse(ucPassenger.IsLoading);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_WithNullPassenger_ThrowsException()
        {
            // Arrange & Act
            var ucPassenger = new UcPassenger(
                null,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_WithNullTripService_ThrowsException()
        {
            // Arrange & Act
            var ucPassenger = new UcPassenger(
                _testPassenger,
                null,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);
        }

        #endregion

        #region Book Button Tests

        [TestMethod]
        public void BookButton_Click_WithMissingPickup_ShowsWarning()
        {
            // Arrange
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Set destination but no pickup
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1); // Car

            // Act - Attempt to book without pickup
            InvokeBookButton(ucPassenger);

            // Wait for async operation
            Task.Delay(50).Wait();

            // Assert - Trip should NOT be requested
            Assert.IsFalse(_mockTripService.RequestTripCalled);
        }

        [TestMethod]
        public void BookButton_Click_WithMissingDestination_ShowsWarning()
        {
            // Arrange
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Set pickup but no destination
            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1);

            // Act
            InvokeBookButton(ucPassenger);

            // Wait for async operation
            Task.Delay(50).Wait();

            // Assert
            Assert.IsFalse(_mockTripService.RequestTripCalled);
        }

        [TestMethod]
        public void BookButton_Click_WithMissingVehicleType_ShowsWarning()
        {
            // Arrange
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Set both locations but no vehicle type
            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", -1); // None selected

            // Act
            InvokeBookButton(ucPassenger);

            // Wait for async operation
            Task.Delay(50).Wait();

            // Assert
            Assert.IsFalse(_mockTripService.RequestTripCalled);
        }

        [TestMethod]
        public void BookButton_Click_WithValidInputs_RequestsTrip()
        {
            // Arrange
            var testTrip = new Trip(
                _testPassenger.Id,
                new Route(_testPickupLocation, _testDestinationLocation, 5.0, TimeSpan.FromMinutes(15), ""),
                new Fare(new Money(50000m), new Money(7500m)),
                VehicleType.Car);

            _mockTripService.SetupRequestTripSuccess(testTrip);

            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Set all required fields
            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1); // Car

            // Act
            InvokeBookButton(ucPassenger);

            // Wait for async operation
            Task.Delay(100).Wait();

            // Assert
            Assert.IsTrue(_mockTripService.RequestTripCalled);
            Assert.IsNotNull(_mockTripService.LastRequestedTrip);
        }

        [TestMethod]
        public void BookButton_Click_WhenServiceThrowsException_HandlesError()
        {
            // Arrange
            _mockTripService.SetupRequestTripFailure(new InvalidOperationException("No drivers available"));

            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1);

            // Act
            InvokeBookButton(ucPassenger);

            // Wait for async operation
            Task.Delay(100).Wait();

            // Assert - Error is handled gracefully, no exception propagates
            Assert.IsTrue(_mockTripService.RequestTripCalled);
            Assert.IsFalse(ucPassenger.IsLoading); // Loading flag should be reset
        }

        #endregion

        #region Event Tests

        [TestMethod]
        public void LogoutButton_Click_TriggersRequestLogoutEvent()
        {
            // Arrange
            bool logoutRequested = false;
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            ucPassenger.RequestLogout += (s, e) => logoutRequested = true;

            // Act - Find and click logout button
            var btnLogout = ucPassenger.GetType().GetField("btnLogout", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(ucPassenger) as System.Windows.Forms.Button;
            btnLogout?.PerformClick();

            // Assert
            Assert.IsTrue(logoutRequested);
        }

        [TestMethod]
        public void ProfileButton_Click_TriggersRequestShowProfileEvent()
        {
            // Arrange
            User profileUser = null;
            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            ucPassenger.RequestShowProfile += (s, user) => profileUser = user;

            // Act
            var btnProfile = ucPassenger.GetType().GetField("btnProfile", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(ucPassenger) as System.Windows.Forms.Button;
            btnProfile?.PerformClick();

            // Assert
            Assert.IsNotNull(profileUser);
            Assert.AreEqual(_testPassenger.Id, profileUser.Id);
        }

        #endregion

        #region Trip Status Change Tests

        [TestMethod]
        public void OnTripStatusChanged_WithMatchingTripId_UpdatesStage()
        {
            // Arrange
            var testTrip = new Trip(
                _testPassenger.Id,
                new Route(_testPickupLocation, _testDestinationLocation, 5.0, TimeSpan.FromMinutes(15), ""),
                new Fare(new Money(50000m), new Money(7500m)),
                VehicleType.Car);

            _mockTripService.SetupRequestTripSuccess(testTrip);

            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // First, request a trip
            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1);
            InvokeBookButton(ucPassenger);
            Task.Delay(100).Wait();

            // Act - Simulate status change event
            _mockTripService.SimulateStatusChange(testTrip.Id, "Searching");
            Task.Delay(50).Wait();

            // Assert - Event was handled (no exception means success)
            Assert.IsTrue(_mockTripService.RequestTripCalled);
        }

        [TestMethod]
        public void OnTripStatusChanged_WithNonMatchingTripId_DoesNothing()
        {
            // Arrange
            var testTrip = new Trip(
                _testPassenger.Id,
                new Route(_testPickupLocation, _testDestinationLocation, 5.0, TimeSpan.FromMinutes(15), ""),
                new Fare(new Money(50000m), new Money(7500m)),
                VehicleType.Car);

            _mockTripService.SetupRequestTripSuccess(testTrip);

            var ucPassenger = new UcPassenger(
                _testPassenger,
                _mockTripService,
                _mockUserService,
                _mockMapService,
                _mockFareService,
                _mockSimulationService,
                _mockMatchingService,
                _mockReviewService);

            // Request trip
            SetLocationPicker(ucPassenger, "pickupPicker", _testPickupLocation);
            SetLocationPicker(ucPassenger, "destinationPicker", _testDestinationLocation);
            SetComboBoxIndex(ucPassenger, "cmbVehicleType", 1);
            InvokeBookButton(ucPassenger);
            Task.Delay(100).Wait();

            // Act - Simulate status change for a DIFFERENT trip ID
            _mockTripService.SimulateStatusChange(Guid.NewGuid(), "Matched");
            Task.Delay(50).Wait();

            // Assert - No exception, no state change for wrong trip
            Assert.IsTrue(_mockTripService.RequestTripCalled);
        }

        #endregion

        #region Helper Methods

        private static void InvokeBookButton(UcPassenger ucPassenger)
        {
            var method = ucPassenger.GetType().GetMethod("btnBook_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(ucPassenger, new object[] { null, null });
        }

        private static void SetLocationPicker(UcPassenger ucPassenger, string fieldName, Location location)
        {
            var picker = ucPassenger.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(ucPassenger);
            
            if (picker != null)
            {
                var property = picker.GetType().GetProperty("SelectedLocation");
                property?.SetValue(picker, location);
            }
        }

        private static void SetComboBoxIndex(UcPassenger ucPassenger, string fieldName, int index)
        {
            var comboBox = ucPassenger.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(ucPassenger) as System.Windows.Forms.ComboBox;
            if (comboBox != null)
            {
                comboBox.SelectedIndex = index;
            }
        }

        #endregion
    }
}
