using Domain.Users.Drivers;

namespace Application.Features.Drivers.RegisterDriver
{
    public class RegisterDriverResponse
    {
        public Driver Driver { get; set; }
        public string Message { get; set; } = "Driver registered successfully.";
    }
}
