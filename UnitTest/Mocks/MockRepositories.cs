using Domain.Entities;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    public class MockBaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> _items = new List<T>();

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult<T>(null);
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(new List<T>(_items));
        }

        public virtual Task AddAsync(T entity)
        {
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(T entity)
        {
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(Guid id)
        {
            return Task.CompletedTask;
        }

        public virtual Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class MockDriverRepository : MockBaseRepository<Driver>, IDriverRepository
    {
        public override Task<Driver> GetByIdAsync(Guid id)
        {
            foreach (var driver in _items)
            {
                if (driver.Id == id) return Task.FromResult(driver);
            }
            return Task.FromResult<Driver>(null);
        }

        public Task<Driver> GetByPhoneAsync(string phone)
        {
            foreach (var driver in _items)
            {
                if (driver.Phone == phone) return Task.FromResult(driver);
            }
            return Task.FromResult<Driver>(null);
        }

        public Task<bool> ExistsByPhoneAsync(string phone)
        {
            foreach (var driver in _items)
            {
                if (driver.Phone == phone) return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<Driver>> GetAvailableDriversAsync()
        {
            List<Driver> result = new List<Driver>();
            foreach (var driver in _items)
            {
                if (driver.IsAvailable()) result.Add(driver);
            }
            return Task.FromResult(result);
        }

        public Task<List<Driver>> GetByStatusAsync(string status)
        {
            List<Driver> result = new List<Driver>();
            foreach (var driver in _items)
            {
                if (driver.Status == status) result.Add(driver);
            }
            return Task.FromResult(result);
        }
    }

    public class MockPassengerRepository : MockBaseRepository<Passenger>, IPassengerRepository
    {
        public override Task<Passenger> GetByIdAsync(Guid id)
        {
            foreach (var p in _items)
            {
                if (p.Id == id) return Task.FromResult(p);
            }
            return Task.FromResult<Passenger>(null);
        }

        public Task<Passenger> GetByPhoneAsync(string phone)
        {
            foreach (var p in _items)
            {
                if (p.Phone == phone) return Task.FromResult(p);
            }
            return Task.FromResult<Passenger>(null);
        }

        public Task<bool> ExistsByPhoneAsync(string phone)
        {
            foreach (var p in _items)
            {
                if (p.Phone == phone) return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }

    public class MockTripRepository : MockBaseRepository<Trip>, ITripRepository
    {
        public override Task<Trip> GetByIdAsync(Guid id)
        {
            foreach (var t in _items)
            {
                if (t.Id == id) return Task.FromResult(t);
            }
            return Task.FromResult<Trip>(null);
        }

        public Task<List<Trip>> GetByPassengerIdAsync(Guid passengerId)
        {
            List<Trip> result = new List<Trip>();
            foreach (var t in _items)
            {
                if (t.PassengerId == passengerId) result.Add(t);
            }
            return Task.FromResult(result);
        }

        public Task<List<Trip>> GetByDriverIdAsync(Guid driverId)
        {
            List<Trip> result = new List<Trip>();
            foreach (var t in _items)
            {
                if (t.DriverId == driverId) result.Add(t);
            }
            return Task.FromResult(result);
        }

        public Task<List<Trip>> GetActiveTripsAsync()
        {
            List<Trip> result = new List<Trip>();
            foreach (var t in _items)
            {
                if (!t.IsTerminal()) result.Add(t);
            }
            return Task.FromResult(result);
        }
    }

    public class MockReviewRepository : MockBaseRepository<Review>, IReviewRepository
    {
        public override Task<Review> GetByIdAsync(Guid id)
        {
            foreach (var r in _items)
            {
                if (r.Id == id) return Task.FromResult(r);
            }
            return Task.FromResult<Review>(null);
        }

        public Task<List<Review>> GetByDriverIdAsync(Guid driverId)
        {
            List<Review> result = new List<Review>();
            foreach (var r in _items)
            {
                if (r.DriverId == driverId) result.Add(r);
            }
            return Task.FromResult(result);
        }

        public Task<List<Review>> GetByTripIdAsync(Guid tripId)
        {
            List<Review> result = new List<Review>();
            foreach (var r in _items)
            {
                if (r.TripId == tripId) result.Add(r);
            }
            return Task.FromResult(result);
        }
    }

    public class MockFareRuleRepository : IFareRuleRepository
    {
        private List<FareRule> _rules = new List<FareRule>();

        public Task<FareRule> GetByVehicleTypeAsync(string vehicleType)
        {
            foreach (var rule in _rules)
            {
                if (rule.VehicleType == vehicleType) return Task.FromResult(rule);
            }
            return Task.FromResult<FareRule>(null);
        }

        public Task AddAsync(FareRule rule)
        {
            _rules.Add(rule);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(FareRule rule) => Task.CompletedTask;

        public Task EnsureSeededAsync() => Task.CompletedTask;

        public Task InitializeAsync() => Task.CompletedTask;

        public Task<List<FareRule>> GetAllAsync() => Task.FromResult(new List<FareRule>(_rules));

        public Task<FareRule> GetByIdAsync(Guid id)
        {
            foreach (var rule in _rules)
            {
                if (rule.Id == id) return Task.FromResult(rule);
            }
            return Task.FromResult<FareRule>(null);
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
