using System;
using Domain.Enums;
using Domain.SharedKernel;

namespace Domain.Entities
{
    // Inheritance (Is-A)
    public abstract class Vehicle : Entity
    {
        #region Fields
        private string _plateNumber;
        private string _brand;
        private string _model;
        private string _color;
        private int _capacity;
        #endregion

        #region Properties
        public string PlateNumber
        {
            get => _plateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Biển số xe không được để trống.", nameof(PlateNumber));
                _plateNumber = value;
            }
        }

        public string Brand
        {
            get => _brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Hãng xe không được để trống.", nameof(Brand));
                _brand = value;
            }
        }

        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mẫu xe không được để trống.", nameof(Model));
                _model = value;
            }
        }

        public string Color
        {
            get => _color;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Màu sắc xe không được để trống.", nameof(Color));
                _color = value;
            }
        }

        public int Capacity
        {
            get => _capacity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Sức chứa phải lớn hơn 0.", nameof(Capacity));
                _capacity = value;
            }
        }

        public VehicleType Type { get; protected set; }
        #endregion

        protected Vehicle(Guid id, string plateNumber, string brand, string model, string color, int capacity)
            : base(id)
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