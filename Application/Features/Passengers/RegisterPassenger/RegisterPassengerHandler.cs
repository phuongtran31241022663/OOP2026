using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Passengers.RegisterPassenger
{
    public class RegisterPassengerHandler
    {
        private readonly IUserRepository _repo;

        public async Task<Passenger> Handle(RegisterPassengerCommand cmd)
        {
            string phone = DomainValidators.UserValidator.NormalizePhone(cmd.Phone);
            DomainValidators.UserValidator.ValidatePassword(cmd.Password);

            if (await _repo.ExistsByPhone(phone))
                throw new InvalidOperationException("Số điện thoại đã tồn tại.");

            Passenger passenger = new Passenger(
                Guid.NewGuid(),
                cmd.Name,
                phone,
                cmd.Password,
                true
            );

            await _repo.Add(passenger);
            return passenger;
        }
    }
}
