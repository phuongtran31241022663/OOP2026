using System;
using Domain.Enums;

namespace Domain.Vehicles
{
    public class Motorbike : Vehicle
    {
        public Motorbike(Guid? id, string plateNumber, string brand, string model, string color)
            : base(id ?? Guid.NewGuid(), plateNumber, brand, model, color, 2)
        {
            Type = VehicleType.Motorbike;
        }
        public override double GetAvgSpeed()
        {
            return 40;
        }

        public override double GetMaxPickupDistance()
        {
            return 5;
        }
    }
}
