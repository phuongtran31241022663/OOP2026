using System;

namespace Domain.Entities.Users
{
    /// <summary>
    /// Đại diện cho người dùng có vai trò Hành khách.
    /// </summary>
    public class Passenger : User
    {
        #region Fields
        private int _totalTrips;
        #endregion

        #region Properties
        /// <summary>
        /// Tổng số chuyến đi đã thực hiện.
        /// </summary>
        public int TotalTrips { get => _totalTrips; private set => _totalTrips = value; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor private cho mục đích deserialization.
        /// </summary>
        private Passenger()
        {
        }

        /// <summary>
        /// Constructor để tạo mới một hành khách.
        /// </summary>
        /// <param name="name">Tên hành khách.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="password">Mật khẩu (dạng thô).</param>
        public Passenger(string name, string phone, string password) : base(name, phone, password)

        {
            TotalTrips = 0;
        }

        /// <summary>
        /// Constructor để tái tạo đối tượng hành khách từ cơ sở dữ liệu.
        /// </summary>
        /// <param name="id">ID đã tồn tại.</param>
        /// <param name="name">Tên hành khách.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="hashedPassword">Mật khẩu đã được hash.</param>
        public Passenger(Guid id, string name, string phone, string hashedPassword) : base(id, name, phone, hashedPassword)
        {
            TotalTrips = 0;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Ghi nhận một chuyến đi mới cho hành khách.
        /// </summary>
        public void AddTrip()
        {
            _totalTrips++;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của hành khách.
        /// </summary>
        /// <returns>Chuỗi thông tin bao gồm vai trò, thông tin cơ bản và tổng số chuyến đi.</returns>
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | [Hành khách] | Tổng chuyến: {TotalTrips}";
        }
        #endregion
    }
}
