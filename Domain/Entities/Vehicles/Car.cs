using System;
using Domain.Enums;

namespace Domain.Entities.Vehicles
{
    /// <summary>
    /// Đại diện cho một phương tiện loại Xe hơi.
    /// </summary>
    /// <remarks>
    /// Lớp này kế thừa từ <see cref="Vehicle"/> và cài đặt các thuộc tính cụ thể cho xe hơi.
    /// </remarks>
    public class Car : Vehicle
    {
        #region Constructors

        /// <summary>
        /// Khởi tạo một đối tượng Xe hơi mới.
        /// </summary>
        /// <param name="id">ID của xe. Nếu là null, một ID mới sẽ được tạo.</param>
        /// <param name="plateNumber">Biển số xe.</param>
        /// <param name="brand">Hãng sản xuất.</param>
        /// <param name="model">Mẫu xe.</param>
        /// <param name="color">Màu sắc.</param>
        /// <param name="capacity">Sức chứa (số chỗ ngồi).</param>
        public Car(Guid? id, string plateNumber, string brand, string model, string color, int capacity)
            : base(id ?? Guid.NewGuid(), plateNumber, brand, model, color, capacity)
        {
            Type = VehicleType.Car;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lấy tốc độ trung bình giả định cho xe hơi.
        /// </summary>
        /// <returns>Tốc độ trung bình là 60 km/h.</returns>
        public override double GetAvgSpeed()
        {
            return 60;
        }

        #endregion
    }
}
