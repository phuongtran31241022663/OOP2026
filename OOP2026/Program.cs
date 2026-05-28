using System.Net.Http;

namespace OOP2026
{
    #region Enums
    public enum DriverStatus
    {
        Offline = 0,
        Online = 1,
        OnTrip = 2
    }
    public enum TripStatus
    {
        Pending = 0,
        Searching = 1,
        Matched = 2,
        Arrived = 3,
        Started = 4,
        DropOff = 5,
        Completed = 6,
        Cancelled = 7,
        Timeout = 8
    }
    public enum VehicleType
    {
        Moto = 1,
        Car = 2,
    }
    #endregion

    #region Photon API Classes
    public class PhotonResponse
    {
        public List<Feature> Features { get; set; } = new();
    }

    public class Feature
    {
        public Geometry Geometry { get; set; } = new();
        public Properties Properties { get; set; } = new();
    }

    public class Geometry
    {
        public List<double> Coordinates { get; set; } = new(); // [Longitude, Latitude]
    }

    public class Properties
    {
        public string? OsmValue { get; set; }
        public string? HouseNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Street { get; set; }
        public string? Locality { get; set; }
        public string? District { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    #endregion

    #region OSRM API Classes
    public class OsrmResponse
    {
        public List<OsrmRoute> Routes { get; set; } = new();
    }

    public class OsrmRoute
    {
        public double Distance { get; set; }
        public double Duration { get; set; }
        public string Geometry { get; set; } = string.Empty;
    }

    #endregion

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- 1. Khởi tạo Infrastructure (Hệ thống) ---
            HttpClient httpClient = new HttpClient();
            InMemoryDriverGrid driverGrid = new InMemoryDriverGrid(); // Giả định cell size phù hợp

            // --- 2. Khởi tạo Repositories (Kho dữ liệu) ---
            UsrRepo userRepo = new UsrRepo();
            TripRepo tripRepo = new TripRepo();
            VehRepo vehicleRepo = new VehRepo();
            PolRepo policyRepo = new PolRepo();
            RevRepo reviewRepo = new RevRepo();

            // --- 3. Khởi tạo Services (Theo đúng thứ tự phụ thuộc) ---
            // Map & Fare
            MapSvc mapService = new MapSvc(httpClient);
            FareSvc fareService = new FareSvc(policyRepo);

            // Matching (Cần thiết cho TripCommand và DriverCommand)
            MatchSvc matchingService = new MatchSvc(driverGrid, tripRepo, userRepo, vehicleRepo);

            // Trip Services
            TripQry tripQuery = new TripQry(tripRepo);
            TripCmd tripCmd = new TripCmd(
                tripRepo, userRepo, vehicleRepo, fareService, mapService, matchingService
            );

            // Drv & Usr Services
            DrvCmd driverCmd = new DrvCmd(userRepo, tripRepo, matchingService, driverGrid);
            DrvQry driverQuery = new DrvQry(userRepo, vehicleRepo);
            UsrSvc userService = new UsrSvc(userRepo, vehicleRepo);
            WalletSvc walletService = new WalletSvc(userRepo, tripQuery);
            RevSvc reviewService = new RevSvc(reviewRepo, userRepo, tripRepo);
            PsgSvc passengerService = new PsgSvc(userRepo, tripCmd, tripQuery, reviewService, mapService, fareService);
            INotificationSvc notificationSvc = new NotificationSvc();
            AdmSvc adminService = new AdmSvc(userRepo, tripRepo, policyRepo, reviewRepo, vehicleRepo);

            // --- 4. Seed tài khoản demo từ DataSeeder để đồng bộ với DB/file JSON ---
            // Mục tiêu: không tạo user/driver/passenger rời rạc trong Program.cs
            DataSeeder.SeedAsync(userRepo, vehicleRepo, tripRepo, policyRepo).GetAwaiter().GetResult();

            // --- 5. Chạy ứng dụng ---

            // LỰA CHỌN CHẾ ĐỘ CHẠY:

            // CHẾ ĐỘ 1: Chạy màn hình Đăng nhập (Mặc định)
            // Application.Run(new FrmAuth(userService, vehicleRepo, userRepo, tripRepo, policyRepo, reviewRepo, tripCmd, tripQuery, driverCmd, driverQuery, mapService, fareService, reviewService, walletService, passengerService, adminService, notificationSvc));

            // CHẾ ĐỘ 2: Chạy thẳng vào Giao diện Mô phỏng Đa vai trò (Dùng để Test/Demo nhanh)
            Application.Run(new FrmMultiRole(
                userService, vehicleRepo, userRepo, tripRepo, policyRepo, reviewRepo, 
                tripCmd, tripQuery, driverCmd, driverQuery, mapService, fareService, reviewService, 
                walletService, passengerService, adminService, notificationSvc));
        }
    }
}
