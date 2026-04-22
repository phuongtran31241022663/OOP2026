using System;
using Domain.Enums;

namespace Domain.Vehicles
{
    public class Car : Vehicle
    {
        public Car(Guid? id, string plateNumber, string brand, string model, string color, int capacity)
            : base(id ?? Guid.NewGuid(), plateNumber, brand, model, color, capacity)
        {
            Type = VehicleType.Car;
        }
        public override double GetAvgSpeed()
        {
            return 60;
        }

        public override double GetMaxPickupDistance()
        {
            return 7;
        }
    }
}
