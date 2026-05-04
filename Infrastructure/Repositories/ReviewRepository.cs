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
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(r => r.DriverId == driverId).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<List<Review>> GetByTripIdAsync(Guid tripId)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(r => r.TripId == tripId).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}