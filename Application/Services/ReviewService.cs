using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Handles review submission and driver rating updates.
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IDriverRepository _driverRepo;
        private readonly ITripRepository _tripRepo;

        public ReviewService(IReviewRepository reviewRepo, IDriverRepository driverRepository, ITripRepository tripRepository)
        {
            _reviewRepo = reviewRepo;
            _driverRepo = driverRepository;
            _tripRepo = tripRepository;
        }

        public async Task AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Đánh giá phải từ 1 đến 5.", nameof(rating));


            Trip trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null) throw new InvalidOperationException("Không tìm thấy chuyến.");

            if (!trip.IsCompleted())
                throw new InvalidOperationException("Chỉ có thể đánh giá chuyến đi đã hoàn thành.");

            if (trip.DriverId != driverId) throw new InvalidOperationException("Tài xế không khớp với chuyến đi.");

            if (trip.PassengerId != passengerId) throw new InvalidOperationException("Hành khách không khớp với chuyến đi.");

            // Check if already rated
            var existingReviews = await _reviewRepo.GetByTripIdAsync(tripId);
            if (existingReviews != null && existingReviews.Count > 0)
                throw new InvalidOperationException("Chuyến đi này đã được đánh giá trước đó.");

            Review review = new Review(driverId, passengerId, tripId, rating, comment);
            await _reviewRepo.AddAsync(review);
            // _reviewRepo.AddAsync already calls SaveChangesAsync in JsonRepository

            // Update driver stats
            Driver driver = await _driverRepo.GetByIdAsync(driverId);
            if (driver != null)
            {
                driver.UpdateReviews(rating);
                await _driverRepo.UpdateAsync(driver);
                // _driverRepo.UpdateAsync already calls SaveChangesAsync in JsonRepository
            }
        }

        public async Task<List<Review>> GetReviewsForDriverAsync(Guid driverId)
        {
            return await _reviewRepo.GetByDriverIdAsync(driverId);
        }
    }
}
