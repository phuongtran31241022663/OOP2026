using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Reviews
{
    public interface IReviewRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        Task<Review> GetByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetAllAsync();

        Task<IEnumerable<Review>> GetByDriverIdAsync(Guid driverId);
        Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId);

        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Guid id);
    }
}