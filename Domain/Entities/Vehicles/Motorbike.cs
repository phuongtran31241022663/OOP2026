using System;
using Domain.Enums;

namespace Domain.Entities.Vehicles
{
    /// <summary>
    /// Đại diện cho một phương tiện loại Xe máy.
    /// </summary>
    /// <remarks>
    /// Lớp này kế thừa từ <see cref="Vehicle"/> và cài đặt các thuộc tính cụ thể cho xe máy.
    /// Sức chứa mặc định là 2.
    /// </remarks>
    public class Motorbike : Vehicle
    {
        #region Constructors

        /// <summary>
        /// Khởi tạo một đối tượng Xe máy mới.
        /// </summary>
        /// <param name="id">ID của xe. Nếu là null, một ID mới sẽ được tạo.</param>
        /// <param name="plateNumber">Biển số xe.</param>
        /// <param name="brand">Hãng sản xuất.</param>
        /// <param name="model">Mẫu xe.</param>
        /// <param name="color">Màu sắc.</param>
        public Motorbike(Guid? id, string plateNumber, string brand, string model, string color)
            : base(id ?? Guid.NewGuid(), plateNumber, brand, model, color, 2) // Sức chứa cố định là 2
        {
            Type = VehicleType.Motorbike;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lấy tốc độ trung bình giả định cho xe máy.
        /// </summary>
        /// <returns>Tốc độ trung bình là 40 km/h.</returns>
        public override double GetAvgSpeed()
        {
            return 40;
        }

        #endregion
    }
}
