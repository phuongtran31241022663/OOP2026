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
    public class AdminService : IAdminService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IFareRuleRepository _fareRuleRepository;
        private readonly IReviewRepository _reviewRepository;

        public AdminService(
            IDriverRepository driverRepository,
            IPassengerRepository passengerRepository,
            ITripRepository tripRepository,
            IFareRuleRepository fareRuleRepository,
            IReviewRepository reviewRepository)
        {
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
            _tripRepository = tripRepository;
            _fareRuleRepository = fareRuleRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            List<Driver> drivers = await _driverRepository.GetAllAsync();
            for (int i = 0; i < drivers.Count; i++)
                users.Add(drivers[i]);

            List<Passenger> passengers = await _passengerRepository.GetAllAsync();
            for (int i = 0; i < passengers.Count; i++)
                users.Add(passengers[i]);

            return users;
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            return await _driverRepository.GetAllAsync();
        }

        public async Task<List<Passenger>> GetAllPassengersAsync()
        {
            return await _passengerRepository.GetAllAsync();
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _tripRepository.GetAllAsync();
        }

        public async Task<List<Trip>> GetTripsByStatusAsync(string status)
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            List<Trip> result = new List<Trip>();
            for (int i = 0; i < allTrips.Count; i++)
            {
                if (allTrips[i].Status == status)
                    result.Add(allTrips[i]);
            }
            return result;
        }

        public async Task<List<FareRule>> GetFareRulesAsync()
        {
            return await _fareRuleRepository.GetAllAsync();
        }

        public async Task CreateFareRuleAsync(FareRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            await _fareRuleRepository.AddAsync(rule);
            await _fareRuleRepository.SaveChangesAsync();
        }

        public async Task UpdateFareRuleAsync(FareRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            await _fareRuleRepository.UpdateAsync(rule);
            await _fareRuleRepository.SaveChangesAsync();
        }

        public async Task UpdateFareRuleAsync(VehicleType vehicleType, Money baseFare, Money pricePerKm, double commissionRate)
        {
            List<FareRule> allRules = await _fareRuleRepository.GetAllAsync();
            FareRule existingRule = null;
            for (int i = 0; i < allRules.Count; i++)
            {
                if (allRules[i].VehicleType == vehicleType)
                {
                    existingRule = allRules[i];
                    break;
                }
            }
            if (existingRule == null)
            {
                existingRule = new FareRule(vehicleType, baseFare, pricePerKm, (decimal)commissionRate);
                await _fareRuleRepository.AddAsync(existingRule);
            }
            else
            {
                existingRule.UpdateRule(vehicleType, baseFare, pricePerKm, (decimal)commissionRate);
                await _fareRuleRepository.UpdateAsync(existingRule);
            }
            await _fareRuleRepository.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalGMVAsync()
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            decimal total = 0m;
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.IsCompleted() && trip.TripFare != null)
                {
                    total += trip.TripFare.TotalAmount.Amount;
                }
            }
            return total;
        }

        public async Task<decimal> GetTotalNTRAsync()
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            decimal total = 0m;
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.IsCompleted() && trip.TripFare != null)
                {
                    total += trip.TripFare.Commission.Amount;
                }
            }
            return total;
        }

        public async Task<double> GetCompletionRateAsync()
        {
            List<Trip> allTrips = await _tripRepository.GetAllAsync();
            int completed = 0;
            int terminal = 0;
            for (int i = 0; i < allTrips.Count; i++)
            {
                Trip trip = allTrips[i];
                if (trip.IsCompleted())
                {
                    completed++;
                    terminal++;
                }
                else if (trip.IsCancelled() || trip.IsTimeout())
                {
                    terminal++;
                }
            }
            if (terminal == 0) return 0.0;
            return (double)completed / terminal * 100.0;
        }

        public async Task<double> GetAverageSatisfactionAsync()
        {
            List<Review> reviews = await _reviewRepository.GetAllAsync();
            if (reviews.Count == 0) return 0.0;
            int sum = 0;
            for (int i = 0; i < reviews.Count; i++)
            {
                sum += reviews[i].Rating;
            }
            return (double)sum / reviews.Count;
        }
    }
}
