namespace OOP2026
{
    #region Simulation
    public class Simulation
    {
        private readonly IDrvQry _driverQueryService;
        private readonly ITripCmd _tripCmdService;
        private readonly ITripQry _tripQueryService;
        private readonly IMatchSvc _matchingService;
        private readonly IUsrRepo _userRepo;
        private readonly InMemoryDriverGrid _driverGrid;

        public Simulation(
            IDrvQry driverQueryService,
            ITripCmd tripCommandService,
            ITripQry tripQueryService,
            IMatchSvc matchingService,
            IUsrRepo userRepository,
            InMemoryDriverGrid driverGrid)
        {
            _driverQueryService = driverQueryService ?? throw new ArgumentNullException(nameof(driverQueryService));
            _tripCmdService = tripCommandService ?? throw new ArgumentNullException(nameof(tripCommandService));
            _tripQueryService = tripQueryService ?? throw new ArgumentNullException(nameof(tripQueryService));
            _matchingService = matchingService ?? throw new ArgumentNullException(nameof(matchingService));
            _userRepo = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _driverGrid = driverGrid ?? throw new ArgumentNullException(nameof(driverGrid));
        }

        public void Start()
        {
            _tripCmdService.TripStatusChanged += OnTripStatusChanged;
            Console.WriteLine("[Simulation] Started. Waiting for trip requests...");
        }

        public void Stop()
        {
            _tripCmdService.TripStatusChanged -= OnTripStatusChanged;
            Console.WriteLine("[Simulation] Stopped.");
        }

        private async void OnTripStatusChanged(object? sender, TripStatusChangedEventArgs e)
        {
            if (e.NewStatus != TripStatus.Searching) return;
            try
            {
                Trip? trip = await _tripQueryService.GetTripByIdAsync(e.TripId).ConfigureAwait(false);
                if (trip != null)
                    await TeleportDriversToPickup(trip).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Simulation] Error during teleport: {ex.Message}");
            }
        }

        private async Task TeleportDriversToPickup(Trip trip)
        {
            var pickupCoord = trip.TripRoute.Pickup.Coord;

            List<Drv> drivers = await _driverQueryService.GetOnlineDriversAsync().ConfigureAwait(false);
            if (drivers.Count == 0) return;

            const double minRadiusKm = 0.2;
            const double maxRadiusKm = 1.5;
            const double minRadiusDeg = minRadiusKm / 111.0;
            const double maxRadiusDeg = maxRadiusKm / 111.0;
            var random = new Random();

            foreach (var driver in drivers)
            {
                if (driver == null) continue;

                if (!driver.IsOnline()) continue;

                double angle = random.NextDouble() * Math.PI * 2;
                double radiusDeg = minRadiusDeg + Math.Sqrt(random.NextDouble()) * (maxRadiusDeg - minRadiusDeg);
                double newLat = pickupCoord.Latitude + radiusDeg * Math.Cos(angle);
                double newLng = pickupCoord.Longitude + radiusDeg * Math.Sin(angle);

                var currentAddress = driver.Position?.Addr
                    ?? new Addr("Vị trí giả lập lân cận", "", "", "", "Việt Nam");

                var newLocation = new Loc(new Coord(newLat, newLng), currentAddress);

                driver.UpdatePosition(newLocation);
                await _userRepo.UpdateAsync(driver).ConfigureAwait(false);
                _driverGrid.UpdateDriverLocation(driver.Id, newLat, newLng);
            }

            await _matchingService.ProposeDriversForTripAsync(trip.Id).ConfigureAwait(false);
            Console.WriteLine($"[Simulation] Teleported {drivers.Count} drivers near pickup.");
        }
    }
    #endregion

}
