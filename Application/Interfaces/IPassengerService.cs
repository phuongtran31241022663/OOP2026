using Domain.Entities;
using Domain.Entities.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPassengerService
    {
        // Đặt chuyến
        Task<Trip> RequestTripAsync(Guid passengerId, Location pickup, Location destination, string vehicleType);

        // Hủy chuyến
        Task CancelTripAsync(Guid passengerId, Guid tripId, string reason);

        // Xem lịch sử chuyến đi của hành khách
        Task<List<Trip>> GetTripHistoryAsync(Guid passengerId);

        // Xem chuyến đang hoạt động (đang tìm xe, đã ghép, đang đi)
        Task<Trip> GetActiveTripAsync(Guid passengerId);

        // Đánh giá tài xế sau chuyến đi
        Task RateDriverAsync(Guid passengerId, Guid tripId, int rating, string comment);

        // Xem thông tin hành khách
        Task<Passenger> GetPassengerInfoAsync(Guid passengerId);
    }
}

