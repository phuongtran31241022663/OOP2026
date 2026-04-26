using Application.Interfaces;
using Application.Services;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Presentation.Screens.Auth;
using Presentation.Shells;
using System;
using System.Windows.Forms;

namespace Presentation
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            AppServiceBundle services = AppServiceBundle.CreateDefault();
            IUserService userService = services.UserService;
            ITripService tripService = services.TripService;
            IFareService fareService = services.FareService;
            ISimulationService simulationService = services.SimulationService;

            MainShell shell = new MainShell(
                () => new LoginForm(userService),
                () => new RegisterForm(userService),
                () => new PassengerShell(userService, tripService, simulationService),
                () => new DriverShell(
                    userService,
                    tripService,
                    simulationService,
                    fareService,
                    CreateDemoDriver()),
                userService);

            System.Windows.Forms.Application.Run(shell);
        }

        private static Driver CreateDemoDriver()
        {
            return new Driver(
                "Driver Demo",
                "0900000000",
                "123456",
                "GPLX-DEMO",
                Guid.NewGuid(),
                CreateDefaultLocation());
        }

        private static Location CreateDefaultLocation()
        {
            Coordinate coordinate = new Coordinate(10.7769, 106.7009);
            Address address = new Address("District 1", "", "District 1", "Ho Chi Minh", "Vietnam");
            return new Location(coordinate, address);
        }
    }
}
