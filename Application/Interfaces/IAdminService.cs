using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<Driver>> GetAllDriversAsync();
        Task<List<Passenger>> GetAllPassengersAsync();

        Task<List<Trip>> GetAllTripsAsync();
        Task<List<Trip>> GetTripsByStatusAsync(string status);

        Task<List<FareRule>> GetFareRulesAsync();
        Task CreateFareRuleAsync(FareRule rule);
        Task UpdateFareRuleAsync(FareRule rule);
        Task UpdateFareRuleAsync(VehicleType vehicleType, Money baseFare, Money pricePerKm, double commissionRate);

        Task<decimal> GetTotalGMVAsync();
        Task<decimal> GetTotalNTRAsync();
        Task<double> GetCompletionRateAsync();
        Task<double> GetAverageSatisfactionAsync();
    }
}
