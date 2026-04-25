using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<List<Trip>> GetByDriverIdAsync(Guid driverId);
        Task<List<Trip>> GetByPassengerIdAsync(Guid passengerId);
    }
}