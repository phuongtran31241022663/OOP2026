using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IUserService for UI testing.
    /// Allows configuring expected behaviors and verifying interactions.
    /// </summary>
public class MockUserService : IUserService
    {
        private Func<string, string, User> _loginHandler;
        private Func<string, string, string, Task<User>> _registerPassengerHandler;
        private Func<string, string, string, string, Guid, Location, Task<User>> _registerDriverHandler;

        private bool _loginCalled;
        private bool _registerPassengerCalled;
        private bool _registerDriverCalled;
        private string _lastLoginPhone;
        private string _lastLoginPassword;

        public bool LoginCalled => _loginCalled;
        public bool RegisterPassengerCalled => _registerPassengerCalled;
        public bool RegisterDriverCalled => _registerDriverCalled;
        public string LastLoginPhone => _lastLoginPhone;
        public string LastLoginPassword => _lastLoginPassword;

        public MockUserService()
        {
            // Default behavior - return null (simulates invalid credentials)
            _loginHandler = (phone, password) => null;
            _registerPassengerHandler = async (name, phone, password) =>
                await Task.FromResult<User>(null);
            _registerDriverHandler = async (name, phone, password, license, vehicleId, location) =>
                await Task.FromResult<User>(null);
        }

        public MockUserService(Func<string, string, User> loginHandler)
        {
            _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
            _registerPassengerHandler = async (name, phone, password) =>
                await Task.FromResult<User>(null);
            _registerDriverHandler = async (name, phone, password, license, vehicleId, location) =>
                await Task.FromResult<User>(null);
        }

        public void SetupLoginSuccess(User user)
        {
            _loginHandler = (phone, password) => user;
        }

        public void SetupLoginFailure(string errorMessage)
        {
            _loginHandler = (phone, password) =>
                throw new InvalidOperationException(errorMessage);
        }

        public void SetupRegisterSuccess(User user)
        {
            _registerPassengerHandler = async (name, phone, password) =>
                await Task.FromResult(user);
        }

        public void SetupRegisterDriverSuccess(Driver driver)
        {
            _registerDriverHandler = async (name, phone, password, license, vehicleId, location) =>
                await Task.FromResult<User>(driver);
        }

        public Task<User> LoginAsync(string phone, string password)
        {
            _loginCalled = true;
            _lastLoginPhone = phone;
            _lastLoginPassword = password;
            return Task.FromResult(_loginHandler(phone, password));
        }

        public Task RegisterDriverAsync(string name, string phone, string password, string licenseNumber, Guid vehicleId, Location initialPosition)
        {
            _registerDriverCalled = true;
            return Task.CompletedTask;
        }

        public Task RegisterPassengerAsync(string name, string phone, string password)
        {
            _registerPassengerCalled = true;
            return Task.CompletedTask;
        }

        public Task<Driver> GetDriverByIdAsync(Guid driverId)
        {
            return Task.FromResult<Driver>(null);
        }

        public Task<Passenger> GetPassengerByIdAsync(Guid passengerId)
        {
            return Task.FromResult<Passenger>(null);
        }

        public Task UpdateDriverStatusAsync(Guid driverId, string newStatus)
        {
            return Task.CompletedTask;
        }

        public Task UpdateDriverLocationAsync(Guid driverId, Location location)
        {
            return Task.CompletedTask;
        }

        public Task<List<Driver>> GetAvailableDriversAsync()
        {
            return Task.FromResult(new List<Driver>());
        }

        public Task<bool> DriverExistsAsync(Guid driverId)
        {
            return Task.FromResult(false);
        }

        public Task UpdateUserAsync(User user)
        {
            return Task.CompletedTask;
        }
    }
}
