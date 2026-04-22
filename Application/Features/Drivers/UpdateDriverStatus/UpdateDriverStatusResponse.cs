using Domain.Users.Drivers;

namespace Application.Features.Drivers.UpdateDriverStatus
{
    public class UpdateDriverStatusResponse
    {
        public Driver Driver { get; set; }
        public string Status { get; set; }
        public string Message { get; set; } = "Driver status updated successfully.";
    }
}
