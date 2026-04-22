using Application.DTOs;
using Application.Interfaces;
using System.Threading.Tasks;

namespace Application.Features.Trips.RequestTrip
{
    public class RequestTripHandler
    {
        private readonly ITripService _tripService;

        public RequestTripHandler(ITripService tripService)
        {
            _tripService = tripService;
        }

        public async Task<RequestTripResponse> HandleAsync(RequestTripCommand command)
        {
            var tripDto = await Task.FromResult(_tripService.RequestTrip(
                command.PassengerId,
                command.Pickup,
                command.Destination,
                command.VehicleType));

            return new RequestTripResponse
            {
                Trip = tripDto,
                Message = "Trip requested successfully."
            };
        }
    }
}
