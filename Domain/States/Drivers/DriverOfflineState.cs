using System;
using Domain.Entities.Users;
using Domain.Events;

namespace Domain.States.Drivers
{
    public class DriverOfflineState : IDriverState
    {
        public void SetAvailable(Driver driver)
        {
            driver.TransitionTo(new DriverAvailableState());
            driver.AddEvent(new DriverStatusChangedEvent(driver.Id, "Offline", "Available"));
        }

        public void SetOnTrip(Driver driver)
        {
            throw new InvalidOperationException("Không thể bắt đầu chuyến khi đang Offline.");
        }

        public void SetOffline(Driver driver)
        {
            // Already offline
        }
    }
}

