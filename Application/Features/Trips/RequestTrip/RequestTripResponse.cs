using Application.DTOs;

namespace Application.Features.Trips.RequestTrip
{
    public class RequestTripResponse
    {
        public TripDto Trip { get; set; }
        public string Message { get; set; } = "Trip requested successfully.";
    }
}
