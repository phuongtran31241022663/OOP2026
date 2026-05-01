using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Events;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IReviewService for UI testing.
    /// </summary>
    public class MockReviewService : IReviewService
    {
        private bool _createReviewCalled;
        private bool _getReviewsCalled;

        public bool CreateReviewCalled => _createReviewCalled;
        public bool GetReviewsCalled => _getReviewsCalled;

        public event EventHandler<ReviewCreatedEvent> ReviewCreated;

        public Task AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment)
        {
            _createReviewCalled = true;
            return Task.CompletedTask;
        }

        public Task<double> GetAverageRatingAsync(Guid userId)
        {
            return Task.FromResult(4.5);
        }

        public Task<List<Review>> GetReviewsForDriverAsync(Guid driverId)
        {
            _getReviewsCalled = true;
            return Task.FromResult(new List<Review>());
        }
    }
}
