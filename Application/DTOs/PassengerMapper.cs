using Domain.Users.Passengers;

namespace Application.DTOs
{
    public static class PassengerMapper
    {
        public static PassengerDto ToDto(Passenger passenger)
        {
            if (passenger == null) return null;

            return new PassengerDto
            {
                Id = passenger.Id,
                Name = passenger.Name,
                Phone = passenger.Phone,
                IsActive = passenger.IsActive,
                TotalTrips = passenger.TotalTrips
            };
        }
    }
}