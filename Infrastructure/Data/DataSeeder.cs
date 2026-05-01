using Domain.Entities;
using Domain.Entities.Users;
using Domain.Entities.Vehicles;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DataSeeder
    {
        private static readonly Random _random = new Random(42);

        private static readonly string[] LastNames = {
            "Nguyễn", "Trần", "Lê", "Phạm", "Huỳnh", "Hoàng", "Phan", "Vũ", "Đặng",
            "Bùi", "Đỗ", "Hồ", "Ngô", "Dương", "Lý", "Trịnh", "Đoàn", "Tô", "Mai", "Kim",
            "Lưu", "Tạ", "Thái", "Cao", "Nghiêm", "Hà", "Đinh", "Vương", "Tống", "Lâm"
        };

        private static readonly string[] MiddleNames = {
            "Văn", "Thị", "Minh", "Thanh", "Đức", "Ngọc", "Quang", "Hữu", "Xuân", "Thế",
            "Anh", "Hồng", "Bích", "Thúy", "Diệu", "Mỹ", "Kim", "Bảo", "Trung", "Tiến"
        };

        private static readonly string[] FirstNames = {
            "Tài", "Lộc", "Phát", "Hùng", "Dũng", "Anh", "Bình", "Cường", "Đạt", "Giang",
            "Hải", "Hiếu", "Khánh", "Sơn", "Trí", "Tuấn", "Thành", "Nhân", "Nghĩa", "Phúc",
            "Hoa", "Lan", "Mai", "Nga", "Phương", "Quỳnh", "Thảo", "Trang", "Vân", "Yến",
            "Linh", "Hương", "Hằng", "Nguyệt", "Tuyết", "Chi", "Loan", "Huệ", "Cúc", "Trúc"
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
            (10.7757, 106.7004, "Quận 1"),
            (10.7844, 106.6862, "Quận 1"),
            (10.7915, 106.7042, "Quận 3"),
            (10.7810, 106.6964, "Quận 3"),
            (10.7564, 106.6697, "Quận 5"),
            (10.7619, 106.6820, "Quận 5"),
            (10.7342, 106.7218, "Quận 7"),
            (10.7282, 106.7367, "Quận 7"),
            (10.7750, 106.6663, "Quận 10"),
            (10.7702, 106.6735, "Quận 10"),
            (10.8040, 106.7120, "Bình Thạnh"),
            (10.8105, 106.6950, "Bình Thạnh"),
            (10.7938, 106.6751, "Phú Nhuận"),
            (10.7990, 106.6805, "Phú Nhuận"),
            (10.8010, 106.6540, "Tân Bình"),
            (10.8065, 106.6645, "Tân Bình"),
            (10.8385, 106.6650, "Gò Vấp"),
            (10.8300, 106.6880, "Gò Vấp"),
            (10.8230, 106.6300, "Tân Phú"),
            (10.8150, 106.6200, "Tân Phú")
        };

        public static async Task SeedAsync(
            IDriverRepository driverRepo,
            IPassengerRepository passengerRepo,
            ITripRepository tripRepo,
            IVehicleRepository vehicleRepo)
        {
            if (driverRepo == null)
                throw new ArgumentNullException(nameof(driverRepo));
            if (passengerRepo == null)
                throw new ArgumentNullException(nameof(passengerRepo));
            if (tripRepo == null)
                throw new ArgumentNullException(nameof(tripRepo));
            if (vehicleRepo == null)
                throw new ArgumentNullException(nameof(vehicleRepo));

            // 1. Luôn đảm bảo có tài khoản test cố định
            if (!await driverRepo.ExistsByPhoneAsync("0900000000"))
            {
                var fixedVehicle = new Car(null, "51A-12345", "Toyota", "Camry", "Trắng", 4);
                await vehicleRepo.AddAsync(fixedVehicle);
                var fixedDriver = new Driver("Tài xế Test", "0900000000", "123456", "GPLX-000000", fixedVehicle.Id, GenerateLocation(999));
                fixedDriver.DepositToWallet(new Money(100000, "VND"));
                fixedDriver.SetAvailable();
                await driverRepo.AddAsync(fixedDriver);
                await driverRepo.SaveChangesAsync();
                await vehicleRepo.SaveChangesAsync();
            }

            if (!await passengerRepo.ExistsByPhoneAsync("0911111111"))
            {
                var fixedPassenger = new Passenger("Hành khách Test", "0911111111", "123456");
                await passengerRepo.AddAsync(fixedPassenger);
                await passengerRepo.SaveChangesAsync();
            }

            // 2. Chỉ seed dữ liệu mẫu ngẫu nhiên nếu chưa có gì
            var existingDrivers = await driverRepo.GetAllAsync();
            if (existingDrivers.Count > 1) return; // Đã có dữ liệu (ít nhất là tài khoản test)

            var drivers = new List<Driver>();
            var vehicles = new List<Vehicle>();
            var passengers = new List<Passenger>();

            // Lấy lại driver test để dùng cho việc tạo trip mẫu
            var driverTest = await driverRepo.GetByPhoneAsync("0900000000");
            if (driverTest != null) drivers.Add(driverTest);
            var passengerTest = await passengerRepo.GetByPhoneAsync("0911111111");
            if (passengerTest != null) passengers.Add(passengerTest);

            for (int i = 0; i < 20; i++) // Giảm số lượng xuống để nhanh hơn
            {
                bool isCar = i < 8;
                var vehicle = CreateVehicle(i, isCar);
                vehicles.Add(vehicle);
                await vehicleRepo.AddAsync(vehicle);

                var driver = CreateDriver(i, vehicle.Id);
                driver.DepositToWallet(new Money(50000, "VND"));
                driver.SetAvailable();
                drivers.Add(driver);
                await driverRepo.AddAsync(driver);
            }

            await driverRepo.SaveChangesAsync();
            await vehicleRepo.SaveChangesAsync();

            for (int i = 0; i < 5; i++)
            {
                var passenger = CreatePassenger(i);
                passengers.Add(passenger);
                await passengerRepo.AddAsync(passenger);
            }
            await passengerRepo.SaveChangesAsync();

            int tripCount = _random.Next(5, 11);
            for (int i = 0; i < tripCount; i++)
            {
                var passenger = passengers[_random.Next(passengers.Count)];
                var driver = drivers[_random.Next(drivers.Count)];
                var trip = CreateCompletedTrip(passenger.Id, driver.Id, i);
                await tripRepo.AddAsync(trip);

                driver.SetAvailable();
                driver.AddTrip();
                await driverRepo.UpdateAsync(driver);

                passenger.AddTrip();
                await passengerRepo.UpdateAsync(passenger);
            }

            await tripRepo.SaveChangesAsync();
            await driverRepo.SaveChangesAsync();
            await passengerRepo.SaveChangesAsync();
        }

        private static Vehicle CreateVehicle(int index, bool isCar)
        {
            string plate = PlatePrefixes[_random.Next(PlatePrefixes.Length)] + "-" + (10000 + _random.Next(90000)).ToString();
            string color = Colors[_random.Next(Colors.Length)];

            if (isCar)
            {
                string brand = BrandsCar[_random.Next(BrandsCar.Length)];
                string model = ModelsCar[_random.Next(ModelsCar.Length)];
                int capacity = _random.Next(4, 8);
                return new Car(null, plate, brand, model, color, capacity);
            }
            else
            {
                string brand = BrandsMoto[_random.Next(BrandsMoto.Length)];
                string model = ModelsMoto[_random.Next(ModelsMoto.Length)];
                return new Motorbike(null, plate, brand, model, color);
            }
        }

        private static Driver CreateDriver(int index, Guid vehicleId)
        {
            string name = GenerateName(index);
            string phone = GeneratePhone(index);
            string license = "GPLX-" + (100000 + index).ToString();
            var location = GenerateLocation(index);
            return new Driver(name, phone, "123456", license, vehicleId, location);
        }

        private static Passenger CreatePassenger(int index)
        {
            string name = GenerateName(index + 100);
            string phone = GeneratePhone(index + 100);
            return new Passenger(name, phone, "123456");
        }

        private static Trip CreateCompletedTrip(Guid passengerId, Guid driverId, int index)
        {
            var pickupLoc = GenerateLocation(index + 200);
            var destLoc = GenerateLocation(index + 300);
            double distance = 2.0 + _random.NextDouble() * 10.0;
            var duration = TimeSpan.FromMinutes(distance * 3 + _random.Next(-5, 10));
            var route = new Route(pickupLoc, destLoc, distance, duration, "");
            var fare = new Fare(new Money((decimal)(distance * 10000 + 15000), "VND"), new Money((decimal)(distance * 2000), "VND"));
            var vehicleType = _random.Next(2) == 0 ? VehicleType.Motorbike : VehicleType.Car;

            var trip = new Trip(passengerId, route, fare, vehicleType);
            trip.SetSearching();
            trip.MatchDriver(driverId);
            trip.MarkAsArrived();
            trip.StartTrip();
            trip.CompleteTrip();
            trip.ConfirmPayment();
            return trip;
        }

        private static string GenerateName(int seedOffset)
        {
            var rng = new Random(2000 + seedOffset);
            string last = LastNames[rng.Next(LastNames.Length)];
            string middle = MiddleNames[rng.Next(MiddleNames.Length)];
            string first = FirstNames[rng.Next(FirstNames.Length)];
            return last + " " + middle + " " + first;
        }

        private static string GeneratePhone(int index)
        {
            var rng = new Random(1000 + index);
            string[] prefixes = { "090", "091", "093", "094", "095", "096", "097", "098", "099", "089", "088", "087", "086", "085", "084", "083", "082", "081", "080" };
            string prefix = prefixes[rng.Next(prefixes.Length)];
            string suffix = rng.Next(10000000).ToString("D7");
            return prefix + suffix;
        }

        private static Location GenerateLocation(int seedOffset)
        {
            var rng = new Random(3000 + seedOffset);
            var loc = HcmLocations[rng.Next(HcmLocations.Length)];
            double lat = loc.Lat + (rng.NextDouble() - 0.5) * 0.01;
            double lng = loc.Lng + (rng.NextDouble() - 0.5) * 0.01;
            var coordinate = new Coordinate(lat, lng);
            var address = new Address(loc.District, "Đường chính", loc.District, "Thành phố Hồ Chí Minh", "Việt Nam");

            return new Location(coordinate, address);
        }
    }
}
