using System;

namespace Application.Features.Drivers.UpdateDriverStatus
{
    public class UpdateDriverStatusCommand
    {
        public Guid DriverId { get; set; }
        public string Status { get; set; }
    }
}
