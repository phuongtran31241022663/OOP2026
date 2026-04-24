using Domain.Users.Passengers;
using Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class PassengerService
    {
        private readonly PassengerRepository _passengerRepo;

        public PassengerService(PassengerRepository passengerRepo)
        {
            _passengerRepo = passengerRepo;
        }

        public async Task<Passenger> GetPassengerAsync(Guid passengerId)
            => await _passengerRepo.GetByIdAsync(passengerId);

        // Các thao tác khác nếu cần
    }
}