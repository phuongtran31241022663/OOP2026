// Infrastructure/Repositories/TripRepository.cs
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class TripRepository : JsonRepository<Trip>, ITripRepository
    {
        public TripRepository() : base("trips.json") { }

        public async Task<List<Trip>> GetByDriverIdAsync(Guid driverId)
        {
            await Task.CompletedTask;
            return _items.Where(t => t.DriverId == driverId).ToList();
        }

        public async Task<List<Trip>> GetByPassengerIdAsync(Guid passengerId)
        {
            await Task.CompletedTask;
            return _items.Where(t => t.PassengerId == passengerId).ToList();
        }
    }
}