using Domain.Entities.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class PassengerTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesPassenger()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "password123";

            // Act
            Passenger passenger = new Passenger(name, phone, password);

            // Assert
            Assert.AreEqual(name, passenger.Name);
            Assert.AreEqual(phone, passenger.Phone);
            Assert.AreEqual(0, passenger.TotalTrips);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyName_ThrowsException()
        {
            // Arrange
            string name = "";
            string phone = "0912345678";
            string password = "password123";

            // Act
            Passenger passenger = new Passenger(name, phone, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyPhone_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "";
            string password = "password123";

            // Act
            Passenger passenger = new Passenger(name, phone, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithShortPassword_ThrowsException()
        {
            // Arrange
            string name = "John Doe";
            string phone = "0912345678";
            string password = "12345";

            // Act
            Passenger passenger = new Passenger(name, phone, password);
        }

        #endregion

        #region Persistence Constructor Tests

        [TestMethod]
        public void PersistenceConstructor_WithValidParameters_CreatesPassenger()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "Jane Doe";
            string phone = "0987654321";
            string hashedPassword = "hashedpassword";

            // Act
            Passenger passenger = new Passenger(id, name, phone, hashedPassword);

            // Assert
            Assert.AreEqual(id, passenger.Id);
            Assert.AreEqual(name, passenger.Name);
            Assert.AreEqual(phone, passenger.Phone);
            Assert.AreEqual(0, passenger.TotalTrips);
        }

        #endregion

        #region AddTrip Tests

        [TestMethod]
        public void AddTrip_IncrementsTotalTrips()
        {
            // Arrange
            Passenger passenger = new Passenger("John", "0912345678", "password123");
            int initialTrips = passenger.TotalTrips;

            // Act
            passenger.AddTrip();

            // Assert
            Assert.AreEqual(initialTrips + 1, passenger.TotalTrips);
        }

        [TestMethod]
        public void AddTrip_MultipleTimes_IncrementsCorrectly()
        {
            // Arrange
            Passenger passenger = new Passenger("John", "0912345678", "password123");

            // Act
            passenger.AddTrip();
            passenger.AddTrip();
            passenger.AddTrip();

            // Assert
            Assert.AreEqual(3, passenger.TotalTrips);
        }

        #endregion

        #region GetInfo Tests

        [TestMethod]
        public void GetInfo_ReturnsFormattedString()
        {
            // Arrange
            Passenger passenger = new Passenger("John Doe", "0912345678", "password123");
            passenger.AddTrip();
            passenger.AddTrip();

            // Act
            string info = passenger.GetInfo();

            // Assert
            Assert.IsTrue(info.Contains("John Doe"));
            Assert.IsTrue(info.Contains("Hành khách"));
            Assert.IsTrue(info.Contains("Tổng chuyến"));
        }

        #endregion
    }
}
