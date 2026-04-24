using Domain.Entities.Users;
using Domain.Entities.Users;
using Domain.Users.Passengers;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly IDriverRepository _driverRepo;
        private readonly IPassengerRepository _passengerRepo;

        public UserService(IDriverRepository driverRepo, IPassengerRepository passengerRepo)
        {
            _driverRepo = driverRepo;
            _passengerRepo = passengerRepo;
        }

        public async Task<User> Login(string phone, string password)
        {
            // Tìm trong driver
            var driver = await _driverRepo.GetByPhoneAsync(phone);
            if (driver != null && driver.VerifyPassword(password))
                return driver;

            // Tìm trong passenger
            var passenger = await _passengerRepo.GetByPhoneAsync(phone);
            if (passenger != null && passenger.VerifyPassword(password))
                return passenger;

            throw new Exception("Sai số điện thoại hoặc mật khẩu.");
        }

        public async Task RegisterDriver(string name, string phone, string password,
            string licenseNumber, Guid vehicleId, Location initialPosition)
        {
            if (await _driverRepo.ExistsByPhoneAsync(phone) || await _passengerRepo.ExistsByPhoneAsync(phone))
                throw new Exception("Số điện thoại đã được đăng ký.");

            var driver = new Driver(name, phone, password, licenseNumber, vehicleId, initialPosition);
            _driverRepo.Add(driver);
            await _driverRepo.SaveChangesAsync();
        }

        public async Task RegisterPassenger(string name, string phone, string password)
        {
            if (await _passengerRepo.ExistsByPhoneAsync(phone) || await _driverRepo.ExistsByPhoneAsync(phone))
                throw new Exception("Số điện thoại đã được đăng ký.");

            var passenger = new Passenger(name, phone, password);
            _passengerRepo.Add(passenger);
            await _passengerRepo.SaveChangesAsync();
        }
    }
}

