using Application.DTOs;

namespace Application.Features.Trips.AssignDriver
{
    public class AssignDriverResponse
    {
        public TripDto Trip { get; set; }
        public DriverDto Driver { get; set; }
        public string Message { get; set; } = "Driver assigned successfully.";
    }
}
