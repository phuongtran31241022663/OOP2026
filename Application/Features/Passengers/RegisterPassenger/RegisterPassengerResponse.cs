using Domain.Users.Passengers;

namespace Application.Features.Passengers.RegisterPassenger
{
    public class RegisterPassengerResponse
    {
        public Passenger Passenger { get; set; }
        public string Message { get; set; } = "Passenger registered successfully.";
    }
}