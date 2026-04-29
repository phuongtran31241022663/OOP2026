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

            if (trip.DriverId != driverId) throw new InvalidOperationException("Tài xế không khớp với chuyến đi.");

            if (trip.PassengerId != passengerId) throw new InvalidOperationException("Hành khách không khớp với chuyến đi.");


            Review review = new Review(driverId, passengerId, tripId, rating, comment);
            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();

            // Update driver stats
            Driver driver = await _driverRepo.GetByIdAsync(driverId);
            if (driver != null)
            {
                driver.UpdateReviews(rating);
                await _driverRepo.UpdateAsync(driver);
                await _driverRepo.SaveChangesAsync();
            }
        }

        public async Task<List<Review>> GetReviewsForDriverAsync(Guid driverId)
        {
            return await _reviewRepo.GetByDriverIdAsync(driverId);
        }
    }
}
