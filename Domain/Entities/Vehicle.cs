using System;
using Domain.Enums;
using Domain.SharedKernel;

namespace Domain.Entities
{
    // Inheritance (Is-A)
    public abstract class Vehicle : Entity
    {
        public string PlateNumber { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public int Capacity { get; private set; }

        public VehicleType Type { get; protected set; }

        protected Vehicle(Guid id, string plateNumber, string brand, string model, string color, int capacity) : base(id)
        {
            PlateNumber = plateNumber;
            Brand = brand;
            Model = model;
            Color = color;
            Capacity = capacity;
        }
        public abstract double GetAvgSpeed();
    }
}
