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

        public async Task<Review> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(r => r.Id == id));
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<IEnumerable<Review>> GetByDriverIdAsync(Guid driverId)
        {
            return await Task.FromResult(_entities.Where(r => r.DriverId == driverId));
        }

        public async Task<IEnumerable<Review>> GetByTripIdAsync(Guid tripId)
        {
            return await Task.FromResult(_entities.Where(r => r.TripId == tripId));
        }

        public async Task AddAsync(Review review)
        {
            _entities.Add(review);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Review review)
        {
            var index = _entities.FindIndex(r => r.Id == review.Id);
            if (index != -1)
                _entities[index] = review;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(r => r.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }
    }
}