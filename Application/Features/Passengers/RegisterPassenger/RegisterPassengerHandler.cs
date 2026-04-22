using Domain.Users.Passengers;
using System;
using System.Threading.Tasks;

namespace Application.Features.Passengers.RegisterPassenger
{
    public class RegisterPassengerHandler
    {
        private readonly IPassengerRepository _repo;

        public RegisterPassengerHandler(IPassengerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Passenger> Handle(RegisterPassengerCommand cmd)
        {
            // 1. Chỉ kiểm tra quy tắc cần dữ liệu ngoại vi (Database)
            // Đây là Business Rule thực sự của tầng Application
            if (await _repo.ExistsByPhone(cmd.Phone))
            {
                throw new InvalidOperationException("Số điện thoại này đã được đăng ký trong hệ thống.");
            }

            // 2. Khởi tạo đối tượng (Data Integrity check tự chạy trong constructor/setters)
            // Nếu cmd.Password ngắn hơn 6, dòng này sẽ tự bắn lỗi lên UI.
            Passenger passenger = new Passenger(
                cmd.Name,
                cmd.Phone,
                cmd.Password
            );

            // 3. Lưu xuống Infrastructure
            _repo.Add(passenger);
            await _repo.SaveChangesAsync();

            return passenger;
        }
    }
}
