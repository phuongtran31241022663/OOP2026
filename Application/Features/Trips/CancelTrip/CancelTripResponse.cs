using Application.DTOs;

namespace Application.Features.Trips.CancelTrip
{
    public class CancelTripResponse
    {
        public TripDto Trip { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public CancelTripResponse(string message, bool success = true)
        {
            Message = message;
            Success = success;
        }
    }
}
