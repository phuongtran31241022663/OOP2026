using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Users;
using Application.Interfaces;

namespace Presentation.ViewModels
{
    public class AdminViewModel
    {
        private Admin _admin;
        private IAdminService _adminService;

        public AdminViewModel(Admin admin, IAdminService adminService)
        {
            _admin = admin;
            _adminService = adminService;
        }

        public int TotalUsers { get; set; }
        public int ActiveDrivers { get; set; }
        public int OnTripDrivers { get; set; }
        public int OngoingTrips { get; set; }
        public int TotalTrips { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Driver> AllDrivers { get; set; } = new List<Driver>();
        public List<Passenger> AllPassengers { get; set; } = new List<Passenger>();

        public async Task LoadDataAsync()
        {
            // Stub implementation
            await Task.CompletedTask;
        }

        public List<Trip> GetFilteredTrips(string searchTerm)
        {
            return new List<Trip>();
        }

        public string GetTripReport()
        {
            return "Report";
        }
    }
}
