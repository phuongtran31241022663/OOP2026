using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Trips
{
    public interface ITripRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        Task<Trip> GetByIdAsync(Guid id);
        Task<IEnumerable<Trip>> GetAllAsync();

        Task<IEnumerable<Trip>> GetByDriverIdAsync(Guid driverId);
        Task<IEnumerable<Trip>> GetByPassengerIdAsync(Guid passengerId);
        Task<IEnumerable<Trip>> GetPendingTripsAsync();

        Task AddAsync(Trip trip);
        Task UpdateAsync(Trip trip);
        Task DeleteAsync(Guid id);
    }
}