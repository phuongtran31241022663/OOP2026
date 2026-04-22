using Domain.ValueObjects;
using Domain.Enums;

namespace Application.Features.Drivers.RegisterDriver
{
    public class RegisterDriverCommand
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public VehicleType VehicleType { get; set; }
        public Location Position { get; set; }
        public string LicenseNumber { get; set; }
    }
}