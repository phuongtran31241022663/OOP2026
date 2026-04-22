using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Common.Constants;
using Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces;

namespace Presentation.ViewModels
{
    internal class TripViewModel
    {
        // Trip data
        public TripDto Trip { get; private set; }
        private readonly ITripService _tripService;

        // Participant info
        public string PassengerName { get; private set; }
        public string DriverName { get; private set; }

        // Route info
        public List<Location> RouteDetails { get; private set; }
        public double Distance { get; private set; }
        public TimeSpan Duration { get; private set; }

        // Status and progress
        public TripStatus Status { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public TimeSpan ElapsedTime => StartedAt.HasValue ?
            (EndedAt ?? DateTime.Now) - StartedAt.Value : TimeSpan.Zero;

        // Financial info
        public decimal BaseFare { get; private set; }
        public decimal DistanceFare { get; private set; }
        public decimal Commission { get; private set; }
        public decimal DriverEarnings { get; private set; }
        public decimal TotalFare { get; private set; }

        // Reviews
        public int? PassengerReview { get; private set; }
        public int? DriverReview { get; private set; }
        public string PassengerComment { get; private set; }
        public string DriverComment { get; private set; }

        public TripViewModel(TripDto trip, ITripService tripService)
        {
            Trip = trip;
            _tripService = tripService;
            UpdateFromDto();
        }

        public void UpdateFromDto()
        {
            Status = Trip.Status;
            StartedAt = Trip.StartedAt;
            EndedAt = Trip.EndedAt;
            PassengerName = Trip.PassengerName;
            DriverName = Trip.DriverName;
            // Reviews are separate entities, not on Trip
            PassengerReview = null;
            DriverReview = null;
            PassengerComment = null;
            DriverComment = null;
            TotalFare = Trip.Fare?.Amount ?? 0;
            Distance = Trip.Distance ?? 0;
            Duration = TimeSpan.FromMinutes(Trip.Duration ?? 0);

            // Calculate financial breakdown using the same fare rule assumptions as the booking/service layer
            BaseFare = Trip.VehicleType == VehicleType.Car ? FareConstants.Car.BaseFare : FareConstants.Motorbike.BaseFare;
            var perKm = Trip.VehicleType == VehicleType.Car ? FareConstants.Car.PricePerKm : FareConstants.Motorbike.PricePerKm;
            DistanceFare = (decimal)Math.Max(0, Distance) * perKm;
            Commission = TotalFare * FareConstants.CommissionRate;
            DriverEarnings = TotalFare - Commission;
        }

        // Participant info is already in TripDto

        public async Task LoadRouteInfoAsync(IRouteService routeService)
        {
            if (Trip.Pickup != null && Trip.Destination != null)
            {
                // Assuming RouteDetails is List<Location> for points
                RouteDetails = (await routeService.GetRoutePointsAsync(Trip.Pickup, Trip.Destination)).ToList();

                if (RouteDetails.Any())
                {
                    // Route points loaded; do not overwrite existing trip distance/duration values.
                }
            }
        }

        public bool CanCancel()
        {
            return _tripService.CanTripBeCancelled(Trip.Id);
        }

        public bool CanStart()
        {
            return Status == TripStatus.Matched;
        }

        public bool CanComplete()
        {
            return Status == TripStatus.Started;
        }

        public bool CanRate()
        {
            return Status == TripStatus.Completed && !PassengerReview.HasValue;
        }

        public TripDisplayViewModel GetDisplayViewModel()
        {
            return new TripDisplayViewModel
            {
                TripId = Trip.Id,
                PassengerName = Trip.PassengerName,
                DriverName = Trip.DriverName,
                Pickup = Trip.Pickup?.Address ?? "Unknown",
                Destination = Trip.Destination?.Address ?? "Unknown",
                Status = Status.ToString(),
                Distance = Distance,
                Duration = Duration,
                ElapsedTime = ElapsedTime,
                TotalFare = TotalFare,
                DriverEarnings = DriverEarnings,
                Commission = Commission,
                PassengerReview = PassengerReview,
                DriverReview = DriverReview,
                CreatedAt = Trip.CreatedAt,
                StartedAt = StartedAt,
                EndedAt = EndedAt
            };
        }
    }

    public class TripDisplayViewModel
    {
        public Guid TripId { get; set; }
        public string PassengerName { get; set; }
        public string DriverName { get; set; }
        public string Pickup { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public double Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public decimal TotalFare { get; set; }
        public decimal DriverEarnings { get; set; }
        public decimal Commission { get; set; }
        public int? PassengerReview { get; set; }
        public int? DriverReview { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
