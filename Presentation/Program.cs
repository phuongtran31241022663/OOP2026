using Application.Interfaces;
using Application.Services;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Presentation.Shells;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            AppServiceBundle services = await AppServiceBundle.CreateDefaultAsync();
            IUserService userService = services.UserService;
            ITripService tripService = services.TripService;
            IFareService fareService = services.FareService;
            ISimulationService simulationService = services.SimulationService;
            IAdminService adminService = services.AdminService;
            IMatchingService matchingService = services.MatchingService;
            IReviewService reviewService = services.ReviewService;
            IMapService mapService = services.MapService;

            FrmMainShell shell = new FrmMainShell(
                userService,
                tripService,
                fareService,
                mapService,
                simulationService,
                adminService,
                matchingService,
                reviewService);

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

