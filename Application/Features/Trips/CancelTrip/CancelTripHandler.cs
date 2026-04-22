using Application.Services;
using Application.DTOs;
using System;

namespace Application.Features.Trips.CancelTrip
{
    public class CancelTripHandler
    {
        private readonly TripService _tripService;

        public CancelTripHandler(TripService tripService)
        {
            _tripService = tripService;
        }

        public CancelTripResponse Handle(CancelTripCommand command)
        {
            var tripBefore = _tripService.GetTripDto(command.TripId);
            _tripService.CancelTrip(command.TripId, command.Reason ?? "Cancelled by user.");
            var tripAfter = _tripService.GetTripDto(command.TripId);

            return new CancelTripResponse($"Trip cancelled. Reason: {command.Reason}")
            {
                Trip = tripAfter,
                Success = true
            };
        }
    }
}
