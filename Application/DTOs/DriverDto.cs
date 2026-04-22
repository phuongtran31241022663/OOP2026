using System;
using Domain.Enums;

namespace Application.DTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DriverStatus Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LicenseNumber { get; set; }

        public string VehicleType { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public int VehicleCapacity { get; set; }

        public decimal Review { get; set; }
        public int TotalTrips { get; set; }
        public decimal WalletAmount { get; set; }
        public string Currency { get; set; } = "VND";
    }
}