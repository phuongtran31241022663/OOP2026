using Domain.Enums;
using Domain.FareRules;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        // User management
        Task<IEnumerable<User>> GetAllUsers();
        Task ActivateAccountUser(Guid userId);
        Task DeActivateAccountUser(Guid userId, Guid actorId);

        // Trip management
        Task<IEnumerable<Domain.Trips.Trip>> GetAllTrips();
        Task<TripReportDto> GetTripReport();

        // Fare rules
        Task<IEnumerable<FareRule>> GetFareRules();
        Task CreateFareRule(FareRule rule);
        Task UpdateFareRule(FareRule rule);
    }

    public class TripReportDto
    {
        public int TotalTrips { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalDriverIncome { get; set; }
        public decimal TotalCommission { get; set; }
        public double CancellationRate { get; set; }
    }
}