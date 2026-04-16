using System;

namespace Domain.ValueObjects
{
    public class Location
    {
        #region Properties
        public string Name { get; private set; }
        public string Address { get; private set; }
        public double Lat { get; private set; }
        public double Lng { get; private set; }
        #endregion
        #region Constructors
        protected Location() { }

        public Location(string name, string address, double lat, double lng)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên địa điểm không được để trống.");

            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException(nameof(lat), "Vĩ độ phải nằm trong khoảng từ -90 đến 90.");

            if (lng < -180 || lng > 180)
                throw new ArgumentOutOfRangeException(nameof(lng), "Kinh độ phải nằm trong khoảng từ -180 đến 180.");
            Name = name;
            Address = address ?? string.Empty;
            Lat = lat;
            Lng = lng;
        }
        #endregion
        #region Methods
        public override bool Equals(object obj)
        {
            Location other = obj as Location;
            if (other == null) return false;

            const double threshold = 0.0001; // ~10m

            return Math.Abs(Lat - other.Lat) <= threshold
                && Math.Abs(Lng - other.Lng) <= threshold;
        }

        public override int GetHashCode()
        {
            return Lat.GetHashCode() ^ Lng.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name} ({Lat:F5}, {Lng:F5})";
        }
        #endregion
}
}
