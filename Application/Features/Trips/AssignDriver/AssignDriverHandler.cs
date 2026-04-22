// Application/Features/Trips/AssignDriver/AssignDriverHandler.cs
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Users.Drivers;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Features.Trips.AssignDriver
{
    public class AssignDriverHandler
    {
        private readonly ITripService _tripService;
        private readonly IDriverRepository _driverRepository;

        public AssignDriverHandler(ITripService tripService, IDriverRepository driverRepository)
        {
            _tripService = tripService;
            _driverRepository = driverRepository;
        }

        public AssignDriverResponse Handle(AssignDriverCommand cmd)
        {
            var success = _tripService.TryAssignDriver(cmd.TripId, cmd.DriverId);
            if (!success)
                throw new Exception("Failed to assign driver.");

            var trip = _tripService.GetTripDto(cmd.TripId);
            var driver = _driverRepository.GetById(cmd.DriverId);

            return new AssignDriverResponse
            {
                Trip = trip,
                Driver = driver != null ? new DriverDto
                {
                    Id = driver.Id,
                    Name = driver.Name,
                    Phone = driver.Phone,
                    Status = driver.Status,
                    Latitude = driver.Position.Lat,
                    Longitude = driver.Position.Lng,
                    LicenseNumber = string.Empty, // No license in simplified MVP
                    VehicleType = driver.Vehicle.Type.ToString(),
                    VehiclePlate = driver.Vehicle.PlateNumber,
                    VehicleBrand = driver.Vehicle.Brand,
                    VehicleModel = driver.Vehicle.Model,
                    VehicleColor = driver.Vehicle.Color,
                    VehicleCapacity = driver.Vehicle.Capacity,
                    Review = driver.AverageReview,
                    TotalTrips = driver.TotalTrips,
                    WalletAmount = driver.Wallet.Amount,
                    Currency = driver.Wallet.Currency
                } : null,
                Message = "Driver assigned successfully."
            };
        }
    }
}