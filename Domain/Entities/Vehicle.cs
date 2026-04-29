using System;
using Domain.Enums;
using Domain.SharedKernel;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp trừu tượng cơ sở cho các loại phương tiện trong hệ thống.
    /// </summary>
    /// <remarks>
    /// Đây là một ví dụ về kế thừa (Inheritance), nơi các lớp cụ thể như <c>Car</c> và <c>Motorbike</c>
    /// sẽ kế thừa từ lớp <c>Vehicle</c> này.
    /// </remarks>
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

        /// <summary>
        /// Biển số xe.
        /// </summary>
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

        /// <summary>
        /// Hãng sản xuất xe.
        /// </summary>
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

        /// <summary>
        /// Mẫu xe.
        /// </summary>
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

        /// <summary>
        /// Màu sắc của xe.
        /// </summary>
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

        /// <summary>
        /// Sức chứa của xe (số chỗ ngồi).
        /// </summary>
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

        /// <summary>
        /// Loại phương tiện (ví dụ: Car, Motorbike).
        /// </summary>
        public VehicleType Type { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor để khởi tạo một đối tượng Vehicle.
        /// </summary>
        /// <param name="id">ID của phương tiện.</param>
        /// <param name="plateNumber">Biển số xe.</param>
        /// <param name="brand">Hãng sản xuất.</param>
        /// <param name="model">Mẫu xe.</param>
        /// <param name="color">Màu sắc.</param>
        /// <param name="capacity">Sức chứa.</param>
        protected Vehicle(Guid id, string plateNumber, string brand, string model, string color, int capacity)
            : base(id)
        {
            PlateNumber = plateNumber;
            Brand = brand;
            Model = model;
            Color = color;
            Capacity = capacity;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lấy tốc độ trung bình của loại phương tiện này (km/h).
        /// </summary>
        /// <returns>Tốc độ trung bình.</returns>
        public abstract double GetAvgSpeed();

        #endregion
    }
}