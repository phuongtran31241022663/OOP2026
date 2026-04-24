using Domain.Enums;
using Domain.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TripRepository : JsonRepository<Trip>, ITripRepository
    {
        public TripRepository() : base("trips.json") { }

        public async Task<IEnumerable<Trip>> GetByDriverIdAsync(Guid driverId)
        {
            await Task.CompletedTask;
            return _items.Where(t => t.DriverId == driverId);
        }

        public async Task<IEnumerable<Trip>> GetByPassengerIdAsync(Guid passengerId)
        {
            await Task.CompletedTask;
            return _items.Where(t => t.PassengerId == passengerId);
        }

        public async Task<IEnumerable<Trip>> GetPendingTripsAsync()
        {
            await Task.CompletedTask;
            return _items.Where(t => t.Status == TripStatus.Searching);
        }

        public async Task AddAsync(Trip trip)
        {
            Add(trip);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Trip trip)
        {
            Update(trip);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
            await Task.CompletedTask;
        }
    }
}