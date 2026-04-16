using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Passengers.RegisterPassenger
{
    public class RegisterPassengerCommand
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
