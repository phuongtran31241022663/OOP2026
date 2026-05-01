using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IVehicleRepository for UI testing.
    /// </summary>
    public class MockVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles = new List<Vehicle>();
        private Guid _lastAddedVehicleId;

        public Guid LastAddedVehicleId => _lastAddedVehicleId;
        public int AddAsyncCallCount { get; private set; }
        public int SaveChangesAsyncCallCount { get; private set; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task AddAsync(Vehicle vehicle)
        {
            AddAsyncCallCount++;
            if (vehicle != null)
            {
                // Set the Id directly through the property since SetId doesn't exist
                var idProperty = typeof(Entity).BaseType?.GetProperty("Id");
                if (idProperty != null && idProperty.CanWrite)
                {
                    idProperty.SetValue(vehicle, Guid.NewGuid());
                }
                _lastAddedVehicleId = vehicle.Id;
                _vehicles.Add(vehicle);
            }
            return Task.CompletedTask;
        }

        public Task<Vehicle> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));
        }

        public Task<List<Vehicle>> GetAllAsync()
        {
            return Task.FromResult(_vehicles.ToList());
        }

        public Task<List<Vehicle>> GetByTypeAsync(VehicleType type)
        {
            return Task.FromResult(_vehicles.Where(v => v.Type == type).ToList());
        }

        public Task UpdateAsync(Vehicle entity)
        {
            var existing = _vehicles.FirstOrDefault(v => v.Id == entity.Id);
            if (existing != null)
            {
                _vehicles.Remove(existing);
                _vehicles.Add(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle != null)
            {
                _vehicles.Remove(vehicle);
            }
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            SaveChangesAsyncCallCount++;
            return Task.CompletedTask;
        }
    }
}
