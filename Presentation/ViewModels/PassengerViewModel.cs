using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using Domain.Trips;
using Application.Interfaces;

namespace Presentation.ViewModels
{
    internal class PassengerViewModel
    {
        private readonly IUserService _userService;
        private readonly IReviewService _ReviewService;
        private readonly IRouteService _routeService;

        // Passenger data
        public Passenger Passenger { get; private set; }
        public Trip CurrentTrip { get; private set; }

        // Trip history
        public List<Trip> TripHistory { get; private set; }

        // Available drivers
        public List<Driver> AvailableDrivers { get; private set; }

        // Booking data
        public Location Pickup { get; set; }
        public Location Destination { get; set; }
        public decimal Fare { get; private set; }
        public List<RouteInfo> AvailableRoutes { get; private set; }

        // Stats
        public int TotalTrips { get; private set; }
        public decimal TotalSpent { get; private set; }
        public double AverageReview { get; private set; }

        public PassengerViewModel(Passenger passenger, IUserService userService,
            IReviewService ReviewService, IRouteService routeService)
        {
            Passenger = passenger;
            _userService = userService;
            _ReviewService = ReviewService;
            _routeService = routeService;

            TripHistory = new List<Trip>();
            AvailableDrivers = new List<Driver>();
            AvailableRoutes = new List<RouteInfo>();
        }

        public async Task LoadDataAsync()
        {
            // Load trip history
            // TODO: Add method to get passenger trips
            // TripHistory = await _userService.GetPassengerTripsAsync(Passenger.Id);

            // Calculate stats
            CalculateStats();
        }

        public async Task RequestTripAsync(Location pickup, Location destination)
        {
            Pickup = pickup;
            Destination = destination;

            // Calculate  fare
            // TODO: Fare = await _fareService.CalculateFareAsync(pickup, destination);

            // Get available drivers
            await LoadAvailableDriversAsync();

            // Get route info
            await LoadRoutesAsync();
        }

        public async Task BookTripAsync()
        {
            if (Pickup == null || Destination == null)
                return;

            // TODO: Create trip request
            // var tripRequest = new TripRequest
            // {
            //     PassengerId = Passenger.Id,
            //     Pickup = Pickup,
            //     Destination = Destination,
            //     Fare = Fare
            // };

            // CurrentTrip = await _tripService.RequestTripAsync(tripRequest);
        }

        public async Task CancelTripAsync()
        {
            if (CurrentTrip != null)
            {
                // TODO: Cancel trip
                // await _tripService.CancelTripAsync(CurrentTrip.Id);
                CurrentTrip = null;
            }
        }

        public async Task RateDriverAsync(Guid tripId, int Review, string comment)
        {
            await _ReviewService.SubmitReviewAsync(tripId, Review, comment);

            var trip = TripHistory.FirstOrDefault(t => t.Id == tripId);
            if (trip != null)
            {
                trip.RateByPassenger(Review, comment);
            }
        }

        private async Task LoadAvailableDriversAsync()
        {
            // TODO: Get available drivers near pickup location
            // AvailableDrivers = await _driverService.GetAvailableDriversAsync(Pickup);
        }

        private async Task LoadRoutesAsync()
        {
            if (Pickup != null && Destination != null)
            {
                var domainRoutes = await _routeService.GetRoutesAsync(Pickup, Destination);

                AvailableRoutes = domainRoutes.Select(r => new RouteInfo
                {
                    RouteId = Guid.NewGuid().ToString(),
                    Description = $"From {r.Start.Address} to {r.End.Address}",

                    Distance = r.Distance,
                    Time = r.Duration,
                    Waypoints = r.Points.ToList()
                }).ToList();
            }
        }


        private void CalculateStats()
        {
            TotalTrips = TripHistory.Count;
            TotalSpent = TripHistory.Sum(t => t.Fare.Amount);

            var ratedTrips = TripHistory.Where(t => t.PassengerReview.HasValue).ToList();
            AverageReview = ratedTrips.Any()
                ? Convert.ToDouble(ratedTrips.Average(t => t.PassengerReview.Value))
                : 0.0;
        }

        public PassengerStatusViewModel GetStatusViewModel()
        {
            return new PassengerStatusViewModel
            {
                PassengerId = Passenger.Id,
                Name = Passenger.Name,
                Phone = Passenger.Phone,
                CurrentTripId = CurrentTrip?.Id,
                TotalTrips = TotalTrips,
                TotalSpent = TotalSpent,
                AverageReview = AverageReview,
                HasActiveTrip = CurrentTrip != null
            };
        }

        public TripBookingViewModel GetBookingViewModel()
        {
            return new TripBookingViewModel
            {
                Pickup = Pickup,
                Destination = Destination,
                Fare = Fare,
                AvailableDrivers = AvailableDrivers.Select(d => new DriverInfo
                {
                    Id = d.Id,
                    Name = d.Name,
                    Review = (double)d.Review,
                    Vehicle = d.Vehicle
                }).ToList(),
                AvailableRoutes = AvailableRoutes
            };
        }
    }

    public class PassengerStatusViewModel
    {
        public Guid PassengerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid? CurrentTripId { get; set; }
        public int TotalTrips { get; set; }
        public decimal TotalSpent { get; set; }
        public double AverageReview { get; set; }
        public bool HasActiveTrip { get; set; }
    }

    public class TripBookingViewModel
    {
        public Location Pickup { get; set; }
        public Location Destination { get; set; }
        public decimal Fare { get; set; }
        public List<DriverInfo> AvailableDrivers { get; set; }
        public List<RouteInfo> AvailableRoutes { get; set; }
    }

    public class DriverInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? Review { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public class RouteInfo
    {
        public string RouteId { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public List<Location> Waypoints { get; set; }
    }
}
