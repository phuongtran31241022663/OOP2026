using System;
using Domain.Users.Drivers;

namespace Application.Features.Drivers.AcceptTrip
{
    public class AcceptTripResponse
    {
        public Driver Driver { get; set; }
        public Guid TripId { get; set; }
        public string Message { get; set; } = "Trip accepted successfully.";
        public bool Accepted { get; set; }
    }
}
