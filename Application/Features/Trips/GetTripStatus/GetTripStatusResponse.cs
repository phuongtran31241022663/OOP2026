using Application.DTOs;

namespace Application.Features.Trips.GetTripStatus
{
    public class GetTripStatusResponse
    {
        public TripDto Trip { get; set; }
        public string Status { get; set; }
    }
}
