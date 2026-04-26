using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using Presentation.Screens.Auth;
using Presentation.Shells;

namespace Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            try
            {
                InitializeStorage(provider);

                var simulationService = provider.GetRequiredService<ISimulationService>();
                var tripTimeoutWorker = provider.GetRequiredService<TripTimeoutWorker>();
                var tripMatchingWorker = provider.GetRequiredService<TripMatchingWorker>();

                simulationService.StartSimulation();
                tripTimeoutWorker.Start();
                tripMatchingWorker.Start();

                Application.Run(CreateMainShell(provider));

                simulationService.StopSimulation();
                tripTimeoutWorker.Stop();
                tripMatchingWorker.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fatal error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // Storage
            services.AddSingleton(new JsonStorage<User>("data/users.json"));
            services.AddSingleton(new JsonStorage<Trip>("data/trips.json"));
            services.AddSingleton(new JsonStorage<Driver>("data/drivers.json"));
            services.AddSingleton(new JsonStorage<Passenger>("data/passengers.json"));

            // Repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITripRepository, TripRepository>();
            services.AddSingleton<IDriverRepository, DriverRepository>();
            services.AddSingleton<IPassengerRepository, PassengerRepository>();

            // HttpClient (IMPORTANT FIX)
            services.AddHttpClient<IMapService, MapApiService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Add("User-Agent", "RideHailingApp/1.0");
            });

            // Application services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITripService, TripService>();
            services.AddSingleton<IRouteService, MapService>();
            services.AddSingleton<IFareService, FareService>();
            services.AddSingleton<ISimulationService, SimulationService>();
            services.AddSingleton<IDriverSimulationService, DriverSimulationService>();

            // Background workers
            services.AddSingleton<TripTimeoutWorker>();
            services.AddSingleton<TripMatchingWorker>();
        }

        private static void InitializeStorage(IServiceProvider provider)
        {
            try
            {
                var userStorage = provider.GetRequiredService<JsonStorage<User>>();
                var tripStorage = provider.GetRequiredService<JsonStorage<Trip>>();
                var driverStorage = provider.GetRequiredService<JsonStorage<Driver>>();
                var passengerStorage = provider.GetRequiredService<JsonStorage<Passenger>>();

                userStorage.InitializeAsync(u => u.Id).GetAwaiter().GetResult();
                tripStorage.InitializeAsync(t => t.Id).GetAwaiter().GetResult();
                driverStorage.InitializeAsync(d => d.Id).GetAwaiter().GetResult();
                passengerStorage.InitializeAsync(p => p.Id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Storage initialization failed: {ex.Message}", ex);
            }
        }

        private static MainShell CreateMainShell(IServiceProvider provider)
        {
            return new MainShell(
                () => new LoginForm(provider.GetRequiredService<IUserService>()),
                () => new RegisterForm(provider.GetRequiredService<IUserService>()),
                () => new BookTripForm(
                    provider.GetRequiredService<ITripService>(),
                    provider.GetRequiredService<IUserService>(),
                    provider.GetRequiredService<IRouteService>(),
                    provider.GetRequiredService<IFareService>(),
                    provider.GetRequiredService<IMapService>(),
                    new PassengerShell(
                        provider.GetRequiredService<IUserService>(),
                        provider.GetRequiredService<ITripService>(),
                        provider.GetRequiredService<IDriverSimulationService>()
                    )
                ),
                () => new DriverDashboardForm(),
                provider.GetRequiredService<IUserService>()
            );
        }
    }
}
