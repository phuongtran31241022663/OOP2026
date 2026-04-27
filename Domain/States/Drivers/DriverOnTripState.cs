using System;
using Domain.Entities.Users;
using Domain.Events;

namespace Domain.States.Drivers
{
    public class DriverOnTripState : IDriverState
    {
        public void SetAvailable(Driver driver)
        {
            driver.TransitionTo(new DriverAvailableState());
            driver.AddEvent(new DriverStatusChangedEvent(driver.Id, "OnTrip", "Available"));
        }

        public void SetOnTrip(Driver driver)
        {
            // Already on trip
        }

        public void SetOffline(Driver driver)
        {
            throw new InvalidOperationException("Quy tắc nghiệp vụ: Không thể ngắt kết nối khi đang chạy chuyến.");
        }
    }
}

