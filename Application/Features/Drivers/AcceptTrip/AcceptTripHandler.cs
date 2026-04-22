using Domain.Trips;
using Domain.Users.Drivers;
using Application.DTOs;
using Domain.Interfaces;
using System;

namespace Application.Features.Drivers.AcceptTrip
{
    public class AcceptTripHandler
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;

        public AcceptTripHandler(
            ITripRepository tripRepository,
            IDriverRepository driverRepository)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
        }

        public AcceptTripResponse Handle(AcceptTripCommand command)
        {
            var trip = _tripRepository.GetById(command.TripId);
            var driver = _driverRepository.GetById(command.DriverId);

            if (trip == null || driver == null)
                return new AcceptTripResponse
                {
                    Accepted = false,
                    Message = "Trip or Driver not found."
                };

            if (trip.DriverId.HasValue)
                return new AcceptTripResponse
                {
                    Accepted = false,
                    Message = "Trip already assigned to another driver."
                };

            trip.MatchDriver(driver.Id);
            driver.SetOnTrip();

            _tripRepository.Update(trip);
            _driverRepository.Update(driver);

            return new AcceptTripResponse
            {
                Accepted = true,
                TripId = trip.Id,
                Driver = driver,
                Message = "Trip accepted successfully."
            };
        }
    }
}
