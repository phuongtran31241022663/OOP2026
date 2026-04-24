using Domain.SharedKernel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetByDriverIdAsync(Guid driverId);
        Task<IEnumerable<Trip>> GetByPassengerIdAsync(Guid passengerId);
        Task<IEnumerable<Trip>> GetPendingTripsAsync();
    }
}