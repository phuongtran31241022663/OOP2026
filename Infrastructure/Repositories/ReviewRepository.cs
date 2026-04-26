// Infrastructure/Repositories/ReviewRepository.cs
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : JsonRepository<Review>, IReviewRepository
    {
        public ReviewRepository() : base("reviews.json") { }

        public async Task<List<Review>> GetByDriverIdAsync(Guid driverId)
        {
            await Task.CompletedTask;
            return _items.Where(r => r.DriverId == driverId).ToList();
        }

        public async Task<List<Review>> GetByTripIdAsync(Guid tripId)
        {
            await Task.CompletedTask;
            return _items.Where(r => r.TripId == tripId).ToList();
        }
    }
}