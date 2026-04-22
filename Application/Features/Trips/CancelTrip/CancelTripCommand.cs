
using System;

namespace Application.Features.Trips.CancelTrip
{
    public class CancelTripCommand
    {
        public Guid TripId { get; set; }
        public string Reason { get; set; }
    }
}
