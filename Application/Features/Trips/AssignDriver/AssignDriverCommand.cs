// Application/Features/Trips/AssignDriver/AssignDriverCommand.cs
using System;

namespace Application.Features.Trips.AssignDriver
{
    public class AssignDriverCommand
    {
        public Guid TripId { get; set; }
        public Guid DriverId { get; set; }
    }
}