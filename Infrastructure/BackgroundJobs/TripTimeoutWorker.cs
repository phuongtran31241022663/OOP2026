using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Repositories;
using Application.Interfaces;

namespace Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Background worker that cancels trips after a timeout while in Searching state.
    /// </summary>
    public class TripTimeoutWorker
    {
        private readonly ITripRepository _tripRepository;
        private readonly ITripService _tripService;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);
        private CancellationTokenSource _cancellationTokenSource;
        private Task _backgroundTask;

        public TripTimeoutWorker(ITripRepository tripRepository, ITripService tripService)
        {
            _tripRepository = tripRepository;
            _tripService = tripService;
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
                    var pendingTrips = await _tripRepository.GetPendingTripsAsync();
                    var now = DateTime.UtcNow;

                    foreach (var trip in pendingTrips)
                    {
                        if (trip.Status != TripStatus.Searching) continue;

                        var age = now - trip.RequestAt;
                        if (age.TotalSeconds > 60) // 60s timeout
                        {
                            trip.MarkTimeout();
                            await _tripRepository.UpdateAsync(trip);
                            await _tripRepository.SaveChangesAsync();
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
