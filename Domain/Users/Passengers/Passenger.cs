using System;

namespace Domain.Users.Passengers
{
    public sealed class Passenger : User
    {
        #region Fields
        private int _totalTrips;
        // public int Version { get; set; } = 1;
        #endregion

        #region Properties
        public int TotalTrips => _totalTrips;
        #endregion

        #region Constructors
        public Passenger(string name, string phone, string password) : base(name, phone, password)
        {
            _totalTrips = 0;
        }

        public Passenger(Guid id, string name, string phone, string hashedPassword) : base(id, name, phone, hashedPassword)
        {
            _totalTrips = 0;
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
