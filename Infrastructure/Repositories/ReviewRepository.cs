using Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : JsonRepository<Review>, IReviewRepository
    {
        public ReviewRepository() : base("reviews.json") { }

        public async Task<IEnumerable<Review>> GetByDriverIdAsync(Guid driverId)
        {
            await Task.CompletedTask;
            return _items.Where(r => r.DriverId == driverId);
        }

        public async Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId)
        {
            await Task.CompletedTask;
            return _items.Where(r => r.TripId == tripId);
        }

        public async Task AddAsync(Review review)
        {
            Add(review);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Review review)
        {
            Update(review);
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