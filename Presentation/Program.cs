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
                Infrastructure.Repositories.DriverRepository driverRepository = new Infrastructure.Repositories.DriverRepository();
                Infrastructure.Repositories.PassengerRepository passengerRepository = new Infrastructure.Repositories.PassengerRepository();

                // Start simulation service for auto-matching
                simulationService.StartSimulation();

                FrmMain shell = new FrmMain(
                    userService,
                    tripService,
                    fareService,
                    mapService,
                    simulationService,
                    adminService,
                    matchingService,
                    reviewService,
                    vehicleRepository,
                    driverRepository,
                    passengerRepository);
                System.Windows.Forms.Application.Run(shell);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    "Khởi động ứng dụng thất bại vì trạng thái hệ thống chưa sẵn sàng.\nChi tiết: " + ex.Message,
                    "Lỗi khởi động",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    "Khởi động ứng dụng thất bại do dữ liệu cấu hình không đúng định dạng.\nChi tiết: " + ex.Message,
                    "Lỗi định dạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Khởi động ứng dụng thất bại do lỗi không mong muốn.\nChi tiết: " + ex.Message,
                    "Lỗi hệ thống",
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
                    "Thao tác hiện tại không hợp lệ. Vui lòng thử lại.",
                    "Lỗi thao tác",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (e.Exception is FormatException)
            {
                MessageBox.Show(
                    "Dữ liệu nhập vào không đúng định dạng. Vui lòng kiểm tra lại.",
                    "Lỗi định dạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Ứng dụng gặp lỗi không mong muốn trên giao diện.\nChi tiết: " + e.Exception.Message,
                "Lỗi hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e?.ExceptionObject as Exception;
            string detail = ex != null ? ex.Message : "Không xác định được chi tiết lỗi.";

            MessageBox.Show(
                "Ứng dụng vừa gặp lỗi nghiêm trọng.\nChi tiết: " + detail,
                "Lỗi nghiêm trọng",
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
                "Tác vụ nền gặp lỗi.\nChi tiết: " + ex.GetBaseException().Message,
                "Lỗi tác vụ nền",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            e.SetObserved();
        }
    }
}