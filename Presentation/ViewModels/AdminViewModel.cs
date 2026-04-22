using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;
using System.Linq;
using Application.DTOs;

namespace Presentation.ViewModels
{
    internal class AdminViewModel
    {
        private readonly IAdminService _adminService;

        // Admin data
        public Admin Admin { get; private set; }

        // Stats data
        public int TotalUsers { get; private set; }
        public int TotalDrivers { get; private set; }
        public int TotalPassengers { get; private set; }
        public int ActiveDrivers { get; private set; }
        public int OnTripDrivers { get; private set; }
        public int OngoingTrips { get; private set; }
        public int TotalTrips { get; private set; }
        public decimal TotalRevenue { get; private set; }

        // Cached data
        public List<User> AllUsers { get; private set; }
        public List<Driver> AllDrivers { get; private set; }
        public List<Passenger> AllPassengers { get; private set; }
        public List<Trip> AllTrips { get; private set; }
        public List<FareRule> AllFareRules { get; private set; }

        public AdminViewModel(Admin admin, IAdminService adminService)
        {
            Admin = admin;
            _adminService = adminService;
        }

        public async Task LoadDataAsync()
        {
            // Load users, trips, and fare rules in parallel
            var usersTask = Task.Run(() => _adminService.GetAllUsers());
            var tripsTask = Task.Run(() => _adminService.GetAllTrips());
            var fareRulesTask = Task.Run(() => _adminService.GetAllFareRules());

            await Task.WhenAll(usersTask, tripsTask, fareRulesTask);

            AllUsers = usersTask.Result.ToList();
            AllTrips = tripsTask.Result.ToList();
            AllFareRules = fareRulesTask.Result.ToList();

            // Separate by type
            AllDrivers = AllUsers.OfType<Driver>().ToList();
            AllPassengers = AllUsers.OfType<Passenger>().ToList();

            // Calculate stats
            CalculateStats();
        }

        private void CalculateStats()
        {
            TotalUsers = AllUsers.Count;
            TotalDrivers = AllDrivers.Count;
            TotalPassengers = AllPassengers.Count;
            ActiveDrivers = AllDrivers.Count(d => d.Status == DriverStatus.Available);
            OnTripDrivers = AllDrivers.Count(d => d.Status == DriverStatus.OnTrip);
            OngoingTrips = AllTrips.Count(t => t.Status == TripStatus.Started || t.Status == TripStatus.Arrived);
            TotalTrips = AllTrips.Count;
            TotalRevenue = AllTrips.Sum(t => t.Fare.Amount);
        }

        public async Task ActivateUserAsync(Guid userId)
        {
            _adminService.ActivateAccountUser(userId);
            await LoadDataAsync(); // Refresh data
        }

        public async Task DeactivateUserAsync(Guid userId)
        {
            _adminService.DeActivateAccountUser(userId, Admin.Id);
            await LoadDataAsync(); // Refresh data
        }

        public TripReportDto GetTripReport()
        {
            return _adminService.GetTripReport();
        }

        public List<UserDto> GetFilteredUsers(string searchTerm)
        {
            var passengerIds = AllTrips
                .Where(t =>
                    string.IsNullOrEmpty(searchTerm) ||
                    t.Pickup.Address.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.Destination.Address.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(t => t.PassengerId)
                .Distinct();

            var users = AllUsers
                .Where(u => passengerIds.Contains(u.Id));

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Role = u is Driver ? "Driver" : u is Passenger ? "Passenger" : "Admin",
                IsActive = u.IsActive,
                Status = u is Driver d ? d.Status.ToString() : "N/A"
            }).ToList();
        }

        public List<TripDto> GetFilteredTrips(string searchTerm)
        {
            var filtered = AllTrips.Where(t =>
    string.IsNullOrEmpty(searchTerm) ||
    t.Pickup.Address.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
    t.Destination.Address.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

            // Create name mappings
            var userNames = AllUsers.ToDictionary(u => u.Id, u => u.Name);

            return filtered.Select(t => new TripDto
            {
                Id = t.Id,
                PassengerName = userNames.ContainsKey(t.PassengerId) ? userNames[t.PassengerId] : "Unknown",
                DriverName = t.DriverId.HasValue && userNames.ContainsKey(t.DriverId.Value) ? userNames[t.DriverId.Value] : "Not Assigned",
                Pickup = t.Pickup,
                Destination = t.Destination,
                Status = t.Status,
                Fare = t.Fare,
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }

    // DTOs for UI binding
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
}
