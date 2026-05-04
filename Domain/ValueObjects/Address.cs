﻿using System;
using Domain.SharedKernel;
using System.Collections.Generic;


namespace Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        #region Backing Fields
        private readonly string _osm_Value;
        private readonly string _houseNumber; // có thể null nếu không có số nhà
        private readonly string _name;
        private readonly string _street;
        private readonly string _locality;
        private readonly string _district;
        private readonly string _city;
        private readonly string _country;
        #endregion
        #region Properties
        public string Osm_Value => _osm_Value;
        public string HouseNumber => _houseNumber;
        public string Name => _name;
        public string Street => _street;
        public string Locality => _locality;
        public string District => _district;
        public string City => _city;
        public string Country => _country;
        #endregion
        #region Constructors
        [Newtonsoft.Json.JsonConstructor]
        public Address(string name, string street, string district, string city, string country, string houseNumber = null, string osm_Value = null, string locality = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên địa điểm không được để trống.", nameof(name));
            
            _name = name;
            _street = street;
            _district = district;
            _city = city;
            _country = country;
            _houseNumber = houseNumber;
            _osm_Value = osm_Value;
            _locality = locality;
        }

        #endregion
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
            List<string> parts = new List<string>();
            string fullStreet = string.IsNullOrWhiteSpace(HouseNumber) ? Street : HouseNumber + " " + Street;
            if (!string.IsNullOrWhiteSpace(fullStreet)) parts.Add(fullStreet);
            if (!string.IsNullOrWhiteSpace(District)) parts.Add(District);
            if (!string.IsNullOrWhiteSpace(City)) parts.Add(City);

            string rawAddress = string.Join(", ", parts);
            return string.IsNullOrWhiteSpace(Name) ? rawAddress : Name + " (" + rawAddress + ")";
        }
    }
}
