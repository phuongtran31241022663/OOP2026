
using System;

namespace Application.Features.Drivers.AcceptTrip
{
    public class AcceptTripCommand
    {
        public Guid TripId { get; set; }
        public Guid DriverId { get; set; }
    }
}
