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
        public int TotalTrips
        {
            get => _totalTrips;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalTrips), "Tổng số chuyến không được âm.");
                _totalTrips = value;
            }
        }
        #endregion

        #region Constructors
        // Removed parameterless constructor to prevent empty objects
        
        /// <summary>
        /// Constructor để tạo mới một hành khách.
        /// </summary>
        public Passenger(string name, string phone, string password) : base(name, phone, password)
        {
            TotalTrips = 0;
        }

        /// <summary>
        /// Constructor để tái tạo đối tượng hành khách từ cơ sở dữ liệu.
        /// </summary>
        [Newtonsoft.Json.JsonConstructor]
        public Passenger(Guid id, string name, string phone, string password, int totalTrips) 
            : base(id, name, phone, password)
        {
            _totalTrips = totalTrips;
        }

        // Backward compatibility
        public Passenger(Guid id, string name, string phone, string password) 
            : this(id, name, phone, password, 0)
        {
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
