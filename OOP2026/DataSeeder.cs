namespace OOP2026;

#region DataSeeder
public static class DataSeeder
{
    private static readonly Random _sharedRandom = new Random(42);
    private static readonly object _randomLock = new object();

    private static int NextInt(int maxValue)
    {
        lock (_randomLock) { return _sharedRandom.Next(maxValue); }
    }
    private static int NextInt(int minValue, int maxValue)
    {
        lock (_randomLock) { return _sharedRandom.Next(minValue, maxValue); }
    }
    private static double NextDouble()
    {
        lock (_randomLock) { return _sharedRandom.NextDouble(); }
    }

    private static readonly string[] LastNames = {
            "Nguyễn", "Trần", "Lê", "Phạm", "Huỳnh", "Hoàng", "Phan", "Vũ", "Đặng",
            "Bùi", "Đỗ", "Hồ", "Ngô", "Dương", "Lý", "Trịnh", "Đoàn", "Tô", "Mai", "Kim",
            "Lưu", "Từ", "Thái", "Cao", "Nghiêm", "Hà", "Đinh", "Vương", "Tống", "Lâm"
        };

    private static readonly string[] MiddleNames = {
            "Văn", "Thị", "Minh", "Thanh", "Đức", "Ngọc", "Quang", "Hữu", "Xuân", "Thế",
            "Anh", "Hồng", "Bích", "Thủy", "Diệu", "Mỹ", "Kim", "Bảo", "Trung", "Tiến"
        };

    private static readonly string[] FirstNames = {
            "Tài", "Lộc", "Phát", "Hùng", "Dũng", "Anh", "Bình", "Cường", "Đạt", "Giang",
            "Hải", "Hiếu", "Khánh", "Sơn", "Trúc", "Tuấn", "Thành", "Nhân", "Nghĩa", "Phúc",
            "Hoa", "Lan", "Mai", "Nga", "Phương", "Quỳnh", "Thảo", "Trang", "Vân", "Yến",
            "Linh", "Hương", "Hồng", "Nguyệt", "Tuyết", "Chi", "Loan", "Huệ", "Cúc", "Trúc"
        };

    private static readonly string[] BrandsCar = {
            "Toyota", "Hyundai", "Kia", "Mazda", "Honda", "Ford", "Mitsubishi",
            "Suzuki", "VinFast", "Mercedes", "BMW", "Lexus"
        };

    private static readonly string[] ModelsCar = {
            "Vios", "Elantra", "Morning", "Mazda 3", "Civic", "Ranger", "Xpander",
            "Swift", "Lux A2.0", "Fadil", "Camry", "Santa Fe", "Tucson", "C-Class"
        };

    private static readonly string[] BrandsMoto = {
            "Honda", "Yamaha", "Suzuki", "SYM", "Piaggio", "VinFast"
        };

    private static readonly string[] ModelsMoto = {
            "Wave", "Vision", "Air Blade", "Lead", "SH", "Exciter", "Sirius", "Liberty", "Vespa", "Klara"
        };

    private static readonly string[] Colors = {
            "Trắng", "Đen", "Bạc", "Xám", "Đỏ", "Xanh dương", "Xanh lá", "Vàng", "Nâu", "Cam"
        };

    private static readonly string[] PlatePrefixes = {
            "51", "52", "53", "54", "55", "56", "57", "58", "59"
        };

    private static readonly (double Lat, double Lng, string District)[] HcmLocations = {
            (10.7757, 106.7004, "Quận 1"), (10.7844, 106.6862, "Quận 1"),
            (10.7915, 106.7042, "Quận 3"), (10.7810, 106.6964, "Quận 3"),
            (10.7564, 106.6697, "Quận 5"), (10.7619, 106.6820, "Quận 5"),
            (10.7342, 106.7218, "Quận 7"), (10.7282, 106.7367, "Quận 7"),
            (10.7750, 106.6663, "Quận 10"), (10.7702, 106.6735, "Quận 10"),
            (10.8040, 106.7120, "Bình Thạnh"), (10.8105, 106.6950, "Bình Thạnh"),
            (10.7938, 106.6751, "Phú Nhuận"), (10.7990, 106.6805, "Phú Nhuận"),
            (10.8010, 106.6540, "Tân Bình"), (10.8065, 106.6645, "Tân Bình"),
            (10.8385, 106.6650, "Gò Vấp"), (10.8300, 106.6880, "Gò Vấp"),
            (10.8230, 106.6300, "Tân Phú"), (10.8150, 106.6200, "Tân Phú")
        };

    public static async Task SeedAsync(
        IUsrRepo userRepo,
        IVehRepo vehicleRepo,
        ITripRepo tripRepo,
        IPolRepo policyRepo)
    {
        List<Pol> policies = await policyRepo.ReadAsync();
        if (policies.Count == 0)
        {
            await policyRepo.CreateAsync(new Pol(VehicleType.Car, 20000, 12000, 0.2m));
            await policyRepo.CreateAsync(new Pol(VehicleType.Moto, 10000, 8000, 0.15m));
            System.Diagnostics.Debug.WriteLine("? Created default policies");
        }

        // Adm mặc định
        const string adminPhone = "0999999999";
        if (await userRepo.GetByPhoneAsync(adminPhone) == null)
        {
            await userRepo.CreateAsync(new Adm("Quản trị viên", adminPhone, "admin123"));
            Console.WriteLine("? Created admin");
        }

        // Tài xế test cố định
        const string testDriverPhone = "0900000000";
        if (await userRepo.GetByPhoneAsync(testDriverPhone) == null)
        {
            var vehicle = new Car("51A-12345", "Toyota", "Camry", "Trắng", 4);
            await vehicleRepo.CreateAsync(vehicle);
            var driver = new Drv("Tài xế Test", testDriverPhone, "123456", "GPLX-000000", vehicle.Id, GenerateLocation(999));
            driver.Deposit(100000);
            driver.SetOnline();
            await userRepo.CreateAsync(driver);
            Console.WriteLine("? Created test driver");
        }

        // Hành khách test cố định
        const string testPassengerPhone = "0911111111";
        if (await userRepo.GetByPhoneAsync(testPassengerPhone) == null)
        {
            await userRepo.CreateAsync(new Psg("Hành khách Test", testPassengerPhone, "123456"));
            Console.WriteLine("? Created test passenger");
        }

        List<Usr> allUsers = await userRepo.ReadAsync();

        int existingDrivers = 0;
        foreach (var user in allUsers)
        {
            if (user is Drv) existingDrivers++;
        }

        if (existingDrivers <= 2)
        {
            for (int i = 0; i < 10; i++)
            {
                bool isCar = i < 3;
                var vehicle = CreateVehicle(i, isCar);
                await vehicleRepo.CreateAsync(vehicle);

                var driver = (Drv)UserFactory.CreateDriver(
                    GenerateName(i),
                    GeneratePhone(i),
                    "123456",
                    "GPLX-" + (100000 + i).ToString(),
                    vehicle.Id,
                    GenerateLocation(i));

                driver.Deposit(50000);
                driver.SetOnline();
                await userRepo.CreateAsync(driver);
            }
            Console.WriteLine("? Created 10 random drivers");
        }

        int existingPassengers = 0;
        foreach (var user in allUsers)
        {
            if (user is Psg) existingPassengers++;
        }

        if (existingPassengers <= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                var passenger = (Psg)UserFactory.CreatePassenger(
                    GenerateName(i + 100),
                    GeneratePhone(i + 100),
                    "123456");
                await userRepo.CreateAsync(passenger);
            }
            Console.WriteLine("? Created 3 random passengers");
        }

        Console.WriteLine("Data seeding completed.");
    }

    private static Veh CreateVehicle(int index, bool isCar)
    {
        string plate = PlatePrefixes[NextInt(PlatePrefixes.Length)] + "-"
                     + (10000 + NextInt(90000)).ToString();
        string color = Colors[NextInt(Colors.Length)];

        if (isCar)
        {
            string brand = BrandsCar[NextInt(BrandsCar.Length)];
            string model = ModelsCar[NextInt(ModelsCar.Length)];
            int capacity = NextInt(4, 8);
            return VehicleFactory.CreateVehicle(VehicleType.Car, plate, brand, model, color, capacity);
        }
        else
        {
            string brand = BrandsMoto[NextInt(BrandsMoto.Length)];
            string model = ModelsMoto[NextInt(ModelsMoto.Length)];
            return VehicleFactory.CreateVehicle(VehicleType.Moto, plate, brand, model, color, 2);
        }
    }

    private static string GenerateName(int seedOffset)
    {
        var rng = new Random(2000 + seedOffset);
        return LastNames[rng.Next(LastNames.Length)] + " "
             + MiddleNames[rng.Next(MiddleNames.Length)] + " "
             + FirstNames[rng.Next(FirstNames.Length)];
    }

    private static string GeneratePhone(int index)
    {
        var rng = new Random(1000 + index);
        string[] prefixes = {
                "090","091","093","094","095","096","097","098","099",
                "089","088","087","086","085","084","083","082","081","080"
            };
        string prefix = prefixes[rng.Next(prefixes.Length)];
        string suffix = rng.Next(10000000).ToString("D7");
        return prefix + suffix;
    }

    private static Loc GenerateLocation(int seedOffset)
    {
        var rng = new Random(3000 + seedOffset);
        var loc = HcmLocations[rng.Next(HcmLocations.Length)];
        double lat = loc.Lat + (rng.NextDouble() - 0.5) * 0.01;
        double lng = loc.Lng + (rng.NextDouble() - 0.5) * 0.01;
        return new Loc(
            new Coord(lat, lng),
            new Addr(loc.District, "Đường chính", loc.District, "Thành phố Hồ Chí Minh", "Việt Nam"));
    }
}
#endregion
