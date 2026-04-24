using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Repositories;
using Application.Interfaces;

namespace Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Background worker that periodically attempts to match pending trips with available drivers.
    /// </summary>
    public class TripMatchingWorker
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ITripService _tripService;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(10);
        private CancellationTokenSource _cancellationTokenSource;
        private Task _backgroundTask;

        public TripMatchingWorker(
            ITripRepository tripRepository,
            IDriverRepository driverRepository,
            ITripService tripService,
            IVehicleRepository vehicleRepository)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
            _tripService = tripService;
            _vehicleRepository = vehicleRepository;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _backgroundTask = Task.Run(() => RunAsync(_cancellationTokenSource.Token));
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            try { _backgroundTask?.Wait(TimeSpan.FromSeconds(5)); } catch { }
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var searchingTrips = await _tripRepository.GetPendingTripsAsync();
                    foreach (var trip in searchingTrips)
                    {
                        if (trip.Status != TripStatus.Searching) continue;

                        var availableDrivers = await _driverRepository.GetAvailableDriversAsync();
                        foreach (var driver in availableDrivers)
                        {
                            var vehicle = await _vehicleRepository.GetByIdAsync(driver.VehicleId);
                            if (vehicle == null || vehicle.Type != trip.TripVehicleType) continue;

                            await _tripService.MatchDriverAsync(trip.Id, driver.Id);
                            break;
                        }
                    }
                }
                catch
                {
                    // swallow errors
                }

                await Task.Delay(_checkInterval, cancellationToken);
            }
        }
    }
}
