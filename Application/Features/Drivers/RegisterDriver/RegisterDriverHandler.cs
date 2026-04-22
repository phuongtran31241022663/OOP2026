using System;
using Domain.Users.Drivers;
using Domain.Users.Drivers.Vehicles;
using Domain.ValueObjects;

namespace Application.Features.Drivers.RegisterDriver
{
    public class RegisterDriverHandler
    {
        private readonly IDriverRepository _repo;

        public RegisterDriverHandler(IDriverRepository repo)
        {
            _repo = repo;
        }

        public Driver Handle(RegisterDriverCommand cmd)
        {
            var vehicle = new Car(null, Guid.NewGuid(), "0000", "Generic", "Model", "White");
            var address = new Address("Unknown", "Unknown", "Unknown", "Unknown");
            var position = new Location("Default", address, 0, 0);

            var driver = new Driver(cmd.Name, cmd.Phone, cmd.Password, vehicle, position);
            _repo.Add(driver);
            return driver;
        }
    }
}
