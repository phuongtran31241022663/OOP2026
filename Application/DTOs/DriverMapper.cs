using Domain.Users.Drivers;

namespace Application.DTOs
{
    public static class DriverMapper
    {
        public static DriverDto ToDto(Driver driver)
        {
            if (driver == null) return null;

            return new DriverDto
            {
                Id = driver.Id,
                Name = driver.Name,
                Phone = driver.Phone,
                Status = driver.Status,
                Latitude = driver.Position?.Lat ?? 0,
                Longitude = driver.Position?.Lng ?? 0,
                LicenseNumber = driver.Vehicle?.LicenseNumber ?? "",
                VehicleType = driver.Vehicle?.Type.ToString() ?? "",
                VehiclePlate = driver.Vehicle?.PlateNumber ?? "",
                VehicleBrand = driver.Vehicle?.Brand ?? "",
                VehicleModel = driver.Vehicle?.Model ?? "",
                VehicleColor = driver.Vehicle?.Color ?? "",
                VehicleCapacity = driver.Vehicle?.Capacity ?? 0,
                Review = driver.AverageReview,
                TotalTrips = driver.TotalTrips,
                WalletAmount = driver.Wallet?.Amount ?? 0,
                Currency = driver.Wallet?.Currency ?? "VND"
            };
        }
    }
}