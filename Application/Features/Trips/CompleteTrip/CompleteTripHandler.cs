// Application/Features/Trips/CompleteTrip/CompleteTripHandler.cs
using Application.Interfaces;
using Domain.Trips.Events;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Features.Trips.CompleteTrip
{
    public class TripCompletedEventHandler : IEventHandler<TripCompletedEvent>
    {
        private readonly IDriverRepository _driverRepo;
        private readonly IPassengerRepository _passengerRepo;

        public async Task Handle(TripCompletedEvent @event)
        {
            // Transaction 1: Driver
            var driver = await _driverRepo.GetById(@event.DriverId);
            driver.CompleteTrip();                       // SetAvailable + TotalTrips++
            driver.ConfirmCashPayment(@event.Fare);      // cập nhật Wallet, Income
            await _driverRepo.Save(driver);

            // Transaction 2: Passenger
            var passenger = await _passengerRepo.GetById(@event.PassengerId);
            passenger.AddTrip();                         // TotalTrips++
            await _passengerRepo.Save(passenger);
        }
    }
}
}