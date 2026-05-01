using Application.Interfaces;
using Application.Services;
using Presentation.Shells;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            System.Windows.Forms.Application.ThreadException += OnUiThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                AppServiceBundle services = Task.Run(async () => await AppServiceBundle.CreateDefaultAsync())
                                                  .GetAwaiter().GetResult();

                IUserService userService = services.UserService;
                ITripService tripService = services.TripService;
                IFareService fareService = services.FareService;
                ISimulationService simulationService = services.SimulationService;
                IAdminService adminService = services.AdminService;
                IMatchingService matchingService = services.MatchingService;
                IReviewService reviewService = services.ReviewService;
                IMapService mapService = services.MapService;
                Domain.Repositories.IVehicleRepository vehicleRepository = services.VehicleRepository;

                FrmMain shell = new FrmMain(
                    userService,
                    tripService,
                    fareService,
                    mapService,
                    simulationService,
                    adminService,
                    matchingService,
                    reviewService,
                    vehicleRepository);

                System.Windows.Forms.Application.Run(shell);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    "Khoi dong ung dung that bai vi trang thai he thong chua san sang.\nChi tiet: " + ex.Message,
                    "Loi khoi dong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    "Khoi dong ung dung that bai do du lieu cau hinh khong dung dinh dang.\nChi tiet: " + ex.Message,
                    "Loi dinh dang",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Khoi dong ung dung that bai do loi khong mong muon.\nChi tiet: " + ex.Message,
                    "Loi he thong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                System.Windows.Forms.Application.ThreadException -= OnUiThreadException;
                AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
                TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
            }
        }

        private static void OnUiThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e == null || e.Exception == null)
            {
                return;
            }

            if (e.Exception is InvalidOperationException)
            {
                MessageBox.Show(
                    "Thao tac hien tai khong hop le. Vui long thu lai.",
                    "Loi thao tac",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (e.Exception is FormatException)
            {
                MessageBox.Show(
                    "Du lieu nhap vao khong dung dinh dang. Vui long kiem tra lai.",
                    "Loi dinh dang",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Ung dung gap loi khong mong muon tren giao dien.\nChi tiet: " + e.Exception.Message,
                "Loi he thong",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e?.ExceptionObject as Exception;
            string detail = ex != null ? ex.Message : "Khong xac dinh duoc chi tiet loi.";

            MessageBox.Show(
                "Ung dung vua gap loi nghiem trong.\nChi tiet: " + detail,
                "Loi nghiem trong",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e?.Exception;
            if (ex == null)
            {
                return;
            }

            MessageBox.Show(
                "Tac vu nen gap loi.\nChi tiet: " + ex.GetBaseException().Message,
                "Loi tac vu nen",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            e.SetObserved();
        }
    }
}
