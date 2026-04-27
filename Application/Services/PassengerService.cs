using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly ITripService _tripService;
        private readonly IReviewService _reviewService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;

        public PassengerService(
            IPassengerRepository passengerRepository,
            ITripService tripService,
            IReviewService reviewService,
            IMapService mapService,
            IFareService fareService)
        {
            _passengerRepository = passengerRepository;
            _tripService = tripService;
            _reviewService = reviewService;
            _mapService = mapService;
            _fareService = fareService;
        }

        public async Task<Trip> RequestTripAsync(Guid passengerId, Location pickup, Location destination, VehicleType vehicleType)
        {
            // Kiểm tra hành khách tồn tại
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new Exception("Hành khách không tồn tại.");

            // Tính route
            Route route = await _mapService.GetRouteAsync(pickup, destination);
            if (route == null)
                throw new Exception("Không thể tính đường đi.");

            // Tính giá cước
            Fare fare = await _fareService.CalculateFareAsync(vehicleType, route.Distance);

            // Tạo chuyến
            Trip trip = await _tripService.CreateTripAsync(passengerId, route, fare, vehicleType);
            return trip;
        }

        public async Task CancelTripAsync(Guid passengerId, Guid tripId, string reason)
        {
            Trip trip = await _tripService.GetTripAsync(tripId);
            if (trip == null)
                throw new Exception("Chuyến không tồn tại.");

            if (trip.PassengerId != passengerId)
                throw new Exception("Bạn không phải chủ của chuyến này.");

            bool canCancel = await _tripService.CanTripBeCancelledAsync(tripId);
            if (!canCancel)
                throw new Exception("Không thể hủy chuyến ở trạng thái hiện tại.");

            await _tripService.CancelTripAsync(tripId, reason);
        }

        public async Task<List<Trip>> GetTripHistoryAsync(Guid passengerId)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new Exception("Hành khách không tồn tại.");

            return await _tripService.GetTripsByPassengerAsync(passengerId);
        }

        public async Task<Trip> GetActiveTripAsync(Guid passengerId)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new Exception("Hành khách không tồn tại.");

            return await _tripService.GetActiveTripForPassengerAsync(passengerId);
        }

        public async Task RateDriverAsync(Guid passengerId, Guid tripId, int rating, string comment)
        {
            Trip trip = await _tripService.GetTripAsync(tripId);
            if (trip == null)
                throw new Exception("Chuyến không tồn tại.");

            if (trip.PassengerId != passengerId)
                throw new Exception("Bạn không phải hành khách của chuyến này.");

            if (!trip.IsCompleted())
                throw new Exception("Chỉ có thể đánh giá sau khi chuyến hoàn thành.");

            if (!trip.DriverId.HasValue)
                throw new Exception("Chuyến không có tài xế.");

            await _reviewService.AddReviewAsync(trip.DriverId.Value, passengerId, tripId, rating, comment);
        }

        public async Task<Passenger> GetPassengerInfoAsync(Guid passengerId)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null)
                throw new Exception("Hành khách không tồn tại.");
            return passenger;
        }
    }
}
