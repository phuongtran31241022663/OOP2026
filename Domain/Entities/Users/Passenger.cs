using System;

namespace Domain.Entities.Users
{
    public class Passenger : User
    {
        #region Fields
        private int _totalTrips;
        #endregion

        #region Properties
        public int TotalTrips { get => _totalTrips; private set => _totalTrips = value; }

        #endregion

        #region Constructors
        // Constructor dành cho JSON deserialization
        private Passenger()
        {
        }

        public Passenger(string name, string phone, string password) : base(name, phone, password)

        {
            TotalTrips = 0;
        }

        public Passenger(Guid id, string name, string phone, string hashedPassword) : base(id, name, phone, hashedPassword)
        {
            TotalTrips = 0;
        }

        #endregion

        #region Public Methods
        public void AddTrip()
        {
            _totalTrips++;
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} | [Hành khách] | Tổng chuyến: {TotalTrips}";
        }
        #endregion
    }
}
