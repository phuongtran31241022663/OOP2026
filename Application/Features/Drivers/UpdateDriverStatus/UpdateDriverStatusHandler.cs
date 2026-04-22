using System;
using Application.Interfaces;
using Domain.Users.Drivers;
using Domain.Enums;

namespace Application.Features.Drivers.UpdateDriverStatus
{
    public class UpdateDriverStatusHandler
    {
        private readonly IDriverRepository _driverRepository;

        public UpdateDriverStatusHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public Driver Handle(UpdateDriverStatusCommand command)
        {
            var driver = _driverRepository.GetById(command.DriverId);
            if (driver == null)
                throw new Exception("Driver not found");

            DriverStatus status;
            if (string.Equals(command.Status, "Available", StringComparison.OrdinalIgnoreCase))
            {
                status = DriverStatus.Available;
            }
            else if (string.Equals(command.Status, "OnTrip", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(command.Status, "On_Trip", StringComparison.OrdinalIgnoreCase))
            {
                status = DriverStatus.OnTrip;
            }
            else if (string.Equals(command.Status, "Offline", StringComparison.OrdinalIgnoreCase))
            {
                status = DriverStatus.Offline;
            }
            else
            {
                throw new ArgumentException($"Invalid status: {command.Status}");
            }

            switch (status)
            {
                case DriverStatus.Available:
                    driver.SetAvailable();
                    break;
                case DriverStatus.OnTrip:
                    driver.SetOnTrip();
                    break;
                case DriverStatus.Offline:
                    driver.SetOffline();
                    break;
            }

            _driverRepository.Update(driver);
            return driver;
        }
    }
}
