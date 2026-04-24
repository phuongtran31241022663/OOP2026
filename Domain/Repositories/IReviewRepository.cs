using Domain.Entities;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByDriverIdAsync(Guid driverId);
        Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId);
    }
}