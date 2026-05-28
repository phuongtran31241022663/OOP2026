using System.Text.Json.Serialization;

namespace OOP2026
{
    #region Base Value Object
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
                return false;

            ValueObject other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var component in GetEqualityComponents())
                {
                    hash = hash * 23 + (component != null ? component.GetHashCode() : 0);
                }
                return hash;
            }
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
    #endregion

    #region Value Objects
    public sealed class Coord : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coord()
        {
            Latitude = 0;
            Longitude = 0;
        }

        [JsonConstructor]
        public Coord(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Vĩ độ phải nằm trong khoảng từ -90 đến 90.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Kinh độ phải nằm trong khoảng từ -180 đến 180.");

            Latitude = latitude;
            Longitude = longitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }

    public sealed class Addr : ValueObject
    {
        public string Name { get; private set; }
        public string Street { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string? OsmValue { get; private set; }
        public string? HouseNumber { get; private set; }
        public string? Locality { get; private set; }

        public Addr()
        {
            Name = "Unknown Location";
            Street = string.Empty;
            District = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            OsmValue = null;
            HouseNumber = null;
            Locality = null;
        }

        [JsonConstructor]
        public Addr(string name, string street, string district, string city, string country,
                       string? houseNumber = null, string? osmValue = null, string? locality = null)
        {
            // Fallback for empty name to prevent crashes
            Name = string.IsNullOrWhiteSpace(name) ? (string.IsNullOrWhiteSpace(street) ? "Vị trí không xác định" : street) : name;
            Street = street;
            District = district;
            City = city;
            Country = country;
            HouseNumber = houseNumber;
            OsmValue = osmValue;
            Locality = locality;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Street;
            yield return District;
            yield return City;
            yield return Country;
            yield return HouseNumber ?? string.Empty;
            yield return OsmValue ?? string.Empty;
            yield return Locality ?? string.Empty;
        }

        public override string ToString()
        {
            List<string> parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name) && Name != "Unknown" && Name != "Vị trí không xác định")
                parts.Add(Name);

            if (!string.IsNullOrWhiteSpace(HouseNumber))
                parts.Add(HouseNumber ?? string.Empty);
            if (!string.IsNullOrWhiteSpace(Street))
                parts.Add(Street ?? string.Empty);
            if (!string.IsNullOrWhiteSpace(District))
                parts.Add(District);
            if (!string.IsNullOrWhiteSpace(City))
                parts.Add(City);
            if (!string.IsNullOrWhiteSpace(Country))
                parts.Add(Country);

            if (parts.Count == 0)
                return Name;
            else
                return string.Join(", ", parts);
        }
    }

    public sealed class Loc : ValueObject
    {
        public Coord Coord { get; private set; }
        public Addr Addr { get; private set; }

        public Loc()
        {
            Coord = new Coord();
            Addr = new Addr();
        }

        [JsonConstructor]
        public Loc(Coord coord, Addr addr)
        {
            Coord = coord ?? throw new ArgumentNullException(nameof(coord));
            Addr = addr ?? throw new ArgumentNullException(nameof(addr));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Coord;
            yield return Addr;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1:F6}, {2:F6})", Addr, Coord.Latitude, Coord.Longitude);
        }
    }

    public sealed class Route : ValueObject
    {
        public Loc Pickup { get; private set; }
        public Loc Dropoff { get; private set; }
        public decimal Distance { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Polyline { get; private set; }

        public Route()
        {
            Pickup = new Loc();
            Dropoff = new Loc();
            Distance = 0;
            Duration = TimeSpan.Zero;
            Polyline = string.Empty;
        }

        [JsonConstructor]
        public Route(Loc pickup, Loc dropoff, decimal distance, TimeSpan duration,
                     string? polyline = null)
        {
            Pickup = pickup ?? throw new ArgumentNullException(nameof(pickup));
            Dropoff = dropoff ?? throw new ArgumentNullException(nameof(dropoff));

            if (distance <= 0)
                throw new ArgumentOutOfRangeException(nameof(distance), "Khoảng cách phải lớn hơn 0.");
            if (duration.TotalSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(duration), "Thời gian phải lớn hơn hoặc bằng 0.");

            Distance = distance;
            Duration = duration;
            Polyline = polyline ?? string.Empty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Pickup;
            yield return Dropoff;
            yield return Distance;
            yield return Duration;
            yield return Polyline;
        }
    }

    public sealed class Fare : ValueObject
    {
        public decimal TotalAmount { get; private set; }
        public decimal Commission { get; private set; }
        public decimal DriverIncome { get; private set; }

        public Fare()
        {
            TotalAmount = 0;
            Commission = 0;
            DriverIncome = 0;
        }

        [JsonConstructor]
        public Fare(decimal totalAmount, decimal commission)
        {
            if (totalAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalAmount));
            if (commission < 0)
                throw new ArgumentOutOfRangeException(nameof(commission));
            if (commission > totalAmount)
                throw new ArgumentException("Hoa hồng không thể lớn hơn tổng cước.", nameof(commission));

            TotalAmount = totalAmount;
            Commission = commission;
            DriverIncome = totalAmount - commission;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalAmount;
            yield return Commission;
        }

        public static Fare operator +(Fare a, Fare b)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (b is null) throw new ArgumentNullException(nameof(b));
            return new Fare(a.TotalAmount + b.TotalAmount, a.Commission + b.Commission);
        }
    }
    #endregion
}
