using Domain.Entities.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class AdminTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesAdmin()
        {
            // Arrange
            string name = "Admin User";
            string phone = "0912345678";
            string password = "password123";

            // Act
            Admin admin = new Admin(name, phone, password);

            // Assert
            Assert.AreEqual(name, admin.Name);
            Assert.AreEqual(phone, admin.Phone);
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
            Admin admin = new Admin(name, phone, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithEmptyPhone_ThrowsException()
        {
            // Arrange
            string name = "Admin User";
            string phone = "";
            string password = "password123";

            // Act
            Admin admin = new Admin(name, phone, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithShortPassword_ThrowsException()
        {
            // Arrange
            string name = "Admin User";
            string phone = "0912345678";
            string password = "12345"; // Less than 6 characters

            // Act
            Admin admin = new Admin(name, phone, password);
        }

        #endregion

        #region Persistence Constructor Tests

        [TestMethod]
        public void PersistenceConstructor_WithValidParameters_CreatesAdmin()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "Jane Admin";
            string phone = "0987654321";
            string hashedPassword = "hashedpassword";

            // Act
            Admin admin = new Admin(id, name, phone, hashedPassword);

            // Assert
            Assert.AreEqual(id, admin.Id);
            Assert.AreEqual(name, admin.Name);
            Assert.AreEqual(phone, admin.Phone);
        }

        #endregion

        #region GetInfo Tests

        [TestMethod]
        public void GetInfo_ReturnsFormattedStringWithAdminPrefix()
        {
            // Arrange
            Admin admin = new Admin("Admin User", "0912345678", "password123");

            // Act
            string info = admin.GetInfo();

            // Assert
            Assert.IsTrue(info.Contains("Admin User"));
            Assert.IsTrue(info.Contains("TÀI KHOẢN QUẢN TRỊ VIÊN"));
        }

        #endregion
    }
}
