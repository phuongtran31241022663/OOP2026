using Application.DTOs;
using Application.Interfaces;
using System;

namespace Application.Features.Trips.GetTripStatus
{
    public class GetTripStatusHandler
    {
        private readonly ITripService _tripService;

        public GetTripStatusHandler(ITripService tripService)
        {
            _tripService = tripService;
        }

        public GetTripStatusResponse Handle(GetTripStatusQuery query)
        {
            var trip = _tripService.GetTripDto(query.TripId);
            if (trip == null)
                throw new Exception("Trip not found");

            return new GetTripStatusResponse
            {
                Trip = trip,
                Status = trip.Status.ToString()
            };
        }
    }
}
