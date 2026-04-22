using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Coordinate
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Coordinate() { }
        public Coordinate(double longitude, double latitude) {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
