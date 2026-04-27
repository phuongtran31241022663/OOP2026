using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MatchingService : IMatchingService
    {
        private static readonly SemaphoreSlim _matchLock = new SemaphoreSlim(1, 1);

        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public MatchingService(ITripRepository tripRepository, IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }

        public async Task<bool> MatchDriverToTripAsync(Guid tripId, Guid driverId)
        {
            await _matchLock.WaitAsync();
            try
            {
                Trip trip = await _tripRepository.GetByIdAsync(tripId);
                if (trip == null)
                {
                    return false;
                }

                // Chỉ ghép khi chuyến đang ở trạng thái tìm tài xế
                if (!trip.IsSearching())
                {
                    return false;
                }

                Driver driver = await _driverRepository.GetByIdAsync(driverId);
                if (driver == null)
                {
                    return false;
                }

                if (driver.Status != DriverStatus.Available)
                {
                    return false;
                }

                Vehicle vehicle = await _vehicleRepository.GetByIdAsync(driver.VehicleId);
                if (vehicle == null)
                {
                    return false;
                }

                // Kiểm tra loại xe khớp với yêu cầu của chuyến
                if (vehicle.Type != trip.TripVehicleType)
                {
                    return false;
                }

                // Kiểm tra ví tài xế có đủ tiền trả hoa hồng không
                if (driver.Wallet.Amount < trip.TripFare.Commission.Amount)
                {
                    throw new InvalidOperationException("Số dư ví không đủ để trả hoa hồng.");
                }

                trip.MatchDriver(driver.Id);
                driver.SetOnTrip();

                await _tripRepository.UpdateAsync(trip);
                await _driverRepository.UpdateAsync(driver);

                await _tripRepository.SaveChangesAsync();
                await _driverRepository.SaveChangesAsync();

                return true;
            }
            finally
            {
                _matchLock.Release();
            }
        }
    }
}
