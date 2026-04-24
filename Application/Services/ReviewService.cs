using Application.Interfaces;
using Domain.Repositories;
using Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Handles review submission and driver rating updates.
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IDriverRepository _driverRepository;
        private readonly ITripRepository _tripRepository;

        public ReviewService(IReviewRepository reviewRepo, IDriverRepository driverRepository, ITripRepository tripRepository)
        {
            _reviewRepo = reviewRepo;
            _driverRepository = driverRepository;
            _tripRepository = tripRepository;
        }

        public async Task AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Rating must be between 1 and 5");

            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null) throw new Exception("Trip not found");
            if (trip.DriverId != driverId) throw new Exception("Driver does not match trip");
            if (trip.PassengerId != passengerId) throw new Exception("Passenger does not match trip");

            var review = new Review(driverId, passengerId, tripId, rating, comment);
            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();

            // Update driver stats
            var driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver != null)
            {
                driver.UpdateReviews(rating);
                await _driverRepository.UpdateAsync(driver);
                await _driverRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Review>> GetByDriverAsync(Guid driverId)
        {
            return _reviewRepo.GetByDriverIdAsync(driverId);
        }

        public Task<IEnumerable<Review>> GetByTripAsync(Guid tripId)
        {
            return _reviewRepo.GetByTripIdAsync(tripId);
        }
    }
}
