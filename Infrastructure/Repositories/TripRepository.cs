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

        public async Task<Trip> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(t => t.Id == id));
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<IEnumerable<Trip>> GetByDriverIdAsync(Guid driverId)
        {
            return await Task.FromResult(
                _entities.Where(t => t.DriverId == driverId)
            );
        }

        public async Task<IEnumerable<Trip>> GetByPassengerIdAsync(Guid passengerId)
        {
            return await Task.FromResult(
                _entities.Where(t => t.PassengerId == passengerId)
            );
        }

        public async Task<IEnumerable<Trip>> GetPendingTripsAsync()
        {
            return await Task.FromResult(
                _entities.Where(t => t.Status == TripStatus.Searching)
            );
        }

        public async Task AddAsync(Trip trip)
        {
            _entities.Add(trip);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Trip trip)
        {
            var index = _entities.FindIndex(t => t.Id == trip.Id);
            if (index == -1)
                throw new ArgumentException("Trip not found");

            _entities[index] = trip;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(t => t.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }
    }
}