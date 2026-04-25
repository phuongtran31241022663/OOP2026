using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMatchingService
    {
        /// <summary>
        /// Ghép một tài xế với chuyến đi.
        /// </summary>
        /// <param name="tripId">ID của chuyến đi</param>
        /// <param name="driverId">ID của tài xế</param>
        /// <returns>True nếu ghép thành công, false nếu không thể ghép</returns>
        Task<bool> MatchDriverToTripAsync(Guid tripId, Guid driverId);
    }
}