using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetByDriverIdAsync(Guid driverId);   
        Task<List<Review>> GetByTripIdAsync(Guid tripId);      
    }
}