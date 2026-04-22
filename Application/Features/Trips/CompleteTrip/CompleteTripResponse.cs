using Application.DTOs;
using Domain.ValueObjects;

namespace Application.Features.Trips.CompleteTrip
{
    public class CompleteTripResponse
    {
        public TripDto Trip { get; set; }
        public Money TotalFare { get; set; }
        public Money Commission { get; set; }
        public string Message { get; set; } = "Trip completed successfully.";
    }
}
