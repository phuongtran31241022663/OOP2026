using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Enums;
using Application.Interfaces;

namespace Presentation.ViewModels
{
    internal class DriverViewModel
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly ISimulationService _simulationService;

        // Driver data
        public Driver Driver { get; private set; }
        public Trip CurrentTrip { get; private set; }
        public bool IsActive { get; private set; }

        // Available trips
        public List<Trip> AvailableTrips { get; private set; }

        // Trip history
        public List<Trip> TripHistory { get; private set; }

        // Earnings
        public decimal TodayEarnings { get; private set; }
        public decimal WeeklyEarnings { get; private set; }
        public decimal MonthlyEarnings { get; private set; }

        // Stats
        public int TotalTrips { get; private set; }
        public double AverageReview { get; private set; }
        public TimeSpan TotalDrivingTime { get; private set; }

        public DriverViewModel(Driver driver, IUserService userService,
            INotificationService notificationService, ISimulationService simulationService)
        {
            Driver = driver;
            _userService = userService;
            _notificationService = notificationService;
            _simulationService = simulationService;

            AvailableTrips = new List<Trip>();
            TripHistory = new List<Trip>();
        }

        public async Task LoadDataAsync()
        {
            // Load trip history
            // TODO: Add method to get driver trips
            // TripHistory = await _userService.GetDriverTripsAsync(Driver.Id);

            // Calculate earnings and stats
            CalculateEarnings();
            CalculateStats();
        }

        public async Task ToggleActiveAsync()
        {
            var newStatus = IsActive ? DriverStatus.Offline : DriverStatus.Available;
            await _userService.UpdateDriverStatus(Driver.Id, newStatus);
            
            // Update driver status using proper methods instead of direct property set
            if (newStatus == DriverStatus.Available)
            {
                Driver.SetAvailable();
            }
            else
            {
                Driver.SetOffline();
            }
            
            IsActive = !IsActive;

            if (IsActive)
            {
                // Load available trips
                await LoadAvailableTripsAsync();
            }
            else
            {
                AvailableTrips.Clear();
            }
        }

        public async Task LoadAvailableTripsAsync()
        {
            // TODO: Get available trips for driver
            // AvailableTrips = await _tripService.GetAvailableTripsForDriverAsync(Driver.Id);
        }

        public async Task AcceptTripAsync(Guid tripId)
        {
            // TODO: Accept trip logic
            // await _tripService.AcceptTripAsync(tripId, Driver.Id);
            // Set CurrentTrip
            // Navigate to trip screen
        }

        public async Task UpdateLocationAsync(Location location)
        {
            await _userService.UpdateDriverLocation(Driver.Id, location);
        }

        public async Task StartTripAsync()
        {
            if (CurrentTrip != null)
            {
                // TODO: Start trip
                // await _tripService.StartTripAsync(CurrentTrip.Id);
            }
        }

        public async Task CompleteTripAsync()
        {
            if (CurrentTrip != null)
            {
                // TODO: Complete trip
                // await _tripService.CompleteTripAsync(CurrentTrip.Id);

                // Add to history
                TripHistory.Insert(0, CurrentTrip);

                // Clear current trip
                CurrentTrip = null;

                // Update earnings
                CalculateEarnings();
            }
        }

        private void CalculateEarnings()
        {
            // Calculate earnings from trip history
            var today = DateTime.Today;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            TodayEarnings = TripHistory
                .Where(t => t.CreatedAt.Date == today)
                .Sum(t => t.Fare.Amount * 0.8m); // 80% after commission

            WeeklyEarnings = TripHistory
                .Where(t => t.CreatedAt >= weekStart)
                .Sum(t => t.Fare.Amount * 0.8m);

            MonthlyEarnings = TripHistory
                .Where(t => t.CreatedAt >= monthStart)
                .Sum(t => t.Fare.Amount * 0.8m);
        }

        private void CalculateStats()
        {
            TotalTrips = TripHistory.Count;

            var ratedTrips = TripHistory.Where(t => t.DriverReview.HasValue).ToList();
            AverageReview = ratedTrips.Any()
                ? Convert.ToDouble(ratedTrips.Average(t => t.DriverReview.Value))
                : 0;

            TotalDrivingTime = TimeSpan.FromMinutes(TripHistory.Sum(t => (t.EndedAt - t.StartedAt)?.TotalMinutes ?? 0));
        }

        public DriverStatusViewModel GetStatusViewModel()
        {
            return new DriverStatusViewModel
            {
                DriverId = Driver.Id,
                Name = Driver.Name,
                Phone = Driver.Phone,
                Status = Driver.Status.ToString(),
                IsActive = IsActive,
                CurrentTripId = CurrentTrip?.Id,
                TodayEarnings = TodayEarnings,
                WeeklyEarnings = WeeklyEarnings,
                MonthlyEarnings = MonthlyEarnings,
                TotalTrips = TotalTrips,
                AverageReview = AverageReview,
                Vehicle = Driver.Vehicle
            };
        }
    }

    public class DriverStatusViewModel
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public Guid? CurrentTripId { get; set; }
        public decimal TodayEarnings { get; set; }
        public decimal WeeklyEarnings { get; set; }
        public decimal MonthlyEarnings { get; set; }
        public int TotalTrips { get; set; }
        public double AverageReview { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
