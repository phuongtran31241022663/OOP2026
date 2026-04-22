using Domain.SharedKernel;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Osm_Value { get; set; }
        public string HouseNumber { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // EF Core cần constructor không tham số
        public Address() { }

        public Address(string osm_Value, string houseNumber, string name, string street, string locality, string district, string city, string country)
        {
            Osm_Value = osm_Value;
            HouseNumber = houseNumber;
            Name = name;
            Street = street;
            Locality = locality;
            District = district;
            City = city;
            Country = country;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Street;
            yield return District;
            yield return City;
            yield return Country;
            yield return HouseNumber;
            yield return Osm_Value;
            yield return Locality;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            string fullStreet = string.IsNullOrWhiteSpace(HouseNumber) ? Street : HouseNumber + " " + Street;
            if (!string.IsNullOrWhiteSpace(fullStreet)) parts.Add(fullStreet);
            if (!string.IsNullOrWhiteSpace(District)) parts.Add(District);
            if (!string.IsNullOrWhiteSpace(City)) parts.Add(City);

            string rawAddress = string.Join(", ", parts);
            return string.IsNullOrWhiteSpace(Name) ? rawAddress : Name + " (" + rawAddress + ")";
        }
    }
}
