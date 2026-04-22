using System;
using Domain.ValueObjects;
using Domain.Enums;

namespace Application.DTOs
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public Guid PassengerId { get; set; }
        public Guid? DriverId { get; set; }
        public string PassengerName { get; set; }
        public string DriverName { get; set; }
        public Location Pickup { get; set; }
        public Location Destination { get; set; }
        public TripStatus Status { get; set; }
        public Money Fare { get; set; }
        public DateTime RequestedAt { get; set; }
        public double? Distance { get; set; }
        public double? Duration { get; set; }
        public VehicleType VehicleType { get; set; }
        public int Version { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRated { get; set; }
    }
}
