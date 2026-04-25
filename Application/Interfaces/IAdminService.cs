using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        // User management
        Task<List<User>> GetAllUsersAsync();
        Task<List<Driver>> GetAllDriversAsync();
        Task<List<Passenger>> GetAllPassengersAsync();

        // Trip management
        Task<List<Trip>> GetAllTripsAsync();
        Task<List<Trip>> GetTripsByStatusAsync(TripStatus status);

        // Fare rules
        Task<List<FareRule>> GetFareRulesAsync();
        Task CreateFareRuleAsync(FareRule rule);
        Task UpdateFareRuleAsync(FareRule rule);
        Task UpdateFareRuleAsync(VehicleType vehicleType, Money baseFare, Money pricePerKm, double commissionRate);

        // Statistics
        Task<decimal> GetTotalGMVAsync();          // Gross Merchandise Value
        Task<decimal> GetTotalNTRAsync();          // Net Retained Revenue
        Task<double> GetCompletionRateAsync();     // Completion rate percentage
        Task<double> GetAverageSatisfactionAsync(); // Average rating
    }
}