using Domain.Reviews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(Guid driverId, Guid passengerId, Guid tripId, int rating, string comment);
        Task<List<Review>> GetReviewsForDriverAsync(Guid driverId);
    }
}
