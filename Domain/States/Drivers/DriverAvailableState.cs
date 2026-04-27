using System;
using Domain.Entities.Users;
using Domain.Events;

namespace Domain.States.Drivers
{
    public class DriverAvailableState : IDriverState
    {
        public void SetAvailable(Driver driver)
        {
            // Already available
        }

        public void SetOnTrip(Driver driver)
        {
            driver.TransitionTo(new DriverOnTripState());
            driver.AddEvent(new DriverStatusChangedEvent(driver.Id, "Available", "OnTrip"));
        }

        public void SetOffline(Driver driver)
        {
            driver.TransitionTo(new DriverOfflineState());
            driver.AddEvent(new DriverStatusChangedEvent(driver.Id, "Available", "Offline"));
        }
    }
}

