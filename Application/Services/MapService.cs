using Domain.ValueObjects;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLocation = Domain.ValueObjects.Location;
using DomainRoute = Domain.ValueObjects.Route;
using Application.Interfaces;

namespace Application.Services
{
    public class MapService : IMapService
    {
        private readonly GMapProvider _provider;

        // Static locations for demonstration/fallback
        private static readonly List<DomainLocation> _predefinedLocations = new List<DomainLocation>
        {
            // Ho Chi Minh
            new DomainLocation(new Coordinate(10.7769, 106.7009), new Address("Bến Nghé", "Lê Lợi", "Quận 1", "Hồ Chí Minh", "Vietnam")),
            new DomainLocation(new Coordinate(10.7826, 106.6954), new Address("Phường 6", "59C Nguyễn Đình Chiểu", "Quận 3", "Hồ Chí Minh", "Vietnam")),
            
            // Ha Noi
            new DomainLocation(new Coordinate(21.0285, 105.8542), new Address("Hàng Trống", "Hoàn Kiếm", "Hoàn Kiếm", "Hà Nội", "Vietnam")),
            new DomainLocation(new Coordinate(21.0367, 105.8347), new Address("Quán Thánh", "Hùng Vương", "Ba Đình", "Hà Nội", "Vietnam")),

            // Da Nang
            new DomainLocation(new Coordinate(16.0678, 108.2235), new Address("Phước Ninh", "Trần Hưng Đạo", "Hải Châu", "Đà Nẵng", "Vietnam")),
            new DomainLocation(new Coordinate(16.0748, 108.2435), new Address("Phước Mỹ", "Võ Nguyên Giáp", "Sơn Trà", "Đà Nẵng", "Vietnam")),

            // Can Tho
            new DomainLocation(new Coordinate(10.0333, 105.7833), new Address("Tân An", "Hai Bà Trưng", "Ninh Kiều", "Cần Thơ", "Vietnam"))
        };

        public MapService()
        {
            _provider = GMapProviders.GoogleMap; 
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
        }

        public Task<DomainLocation> ReverseGeocodeAsync(double lat, double lon)
        {
            // Find closest predefined location if within range, else return generic
            var closest = _predefinedLocations
                .OrderBy(l => Math.Pow(l.Coordinate.Latitude - lat, 2) + Math.Pow(l.Coordinate.Longitude - lon, 2))
                .FirstOrDefault();

            if (closest != null)
            {
                double dist = Math.Sqrt(Math.Pow(closest.Coordinate.Latitude - lat, 2) + Math.Pow(closest.Coordinate.Longitude - lon, 2));
                if (dist < 0.01) return Task.FromResult(closest);
            }

            return Task.FromResult(
                new DomainLocation(
                    new Coordinate(lat, lon),
                    new Address("Unknown", "", "", "", "", "", "", "Việt Nam")
                )
            );
        }

        public async Task<List<DomainLocation>> SearchLocationAsync(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return new List<DomainLocation>();

            // First, try to match predefined locations (useful for offline/demo)
            var query = q.ToLower();
            var matches = _predefinedLocations
                .Where(l => 
                    l.Address.City.ToLower().Contains(query) || 
                    l.Address.Street.ToLower().Contains(query) ||
                    l.Address.District.ToLower().Contains(query))
                .ToList();

            if (matches.Any()) return matches;

            // Fallback to real GMap Geocoding if available
            var geoProvider = _provider as GeocodingProvider;
            if (geoProvider == null) return matches;

            try
            {
                var status = geoProvider.GetPoints(q, out List<PointLatLng> points);
                if (status == GeoCoderStatusCode.OK && points != null)
                {
                    var results = new List<DomainLocation>();
                    foreach (var point in points)
                    {
                        var coord = new Coordinate(point.Lat, point.Lng);
                        var address = await GetAddressFromPoint(point);
                        results.Add(new DomainLocation(coord, address));
                    }
                    return results;
                }
            }
            catch { /* Ignore geocoding errors in demo */ }

            return matches;
        }

        private async Task<Address> GetAddressFromPoint(PointLatLng point)
        {
            // Simple city detection based on coordinates for demo
            if (point.Lat > 20) return new Address("", "", "", "Hà Nội", "Vietnam");
            if (point.Lat > 15) return new Address("", "", "", "Đà Nẵng", "Vietnam");
            return new Address("", "", "", "Hồ Chí Minh", "Vietnam");
        }

        public async Task<DomainRoute> GetRouteAsync(DomainLocation start, DomainLocation end)
        {
            var p1 = new PointLatLng(start.Coordinate.Latitude, start.Coordinate.Longitude);
            var p2 = new PointLatLng(end.Coordinate.Latitude, end.Coordinate.Longitude);

            var routingProvider = _provider as RoutingProvider;
            if (routingProvider == null)
            {
                // Fallback: simple Haversine distance and estimated time
                double d = GetDistance(start.Coordinate, end.Coordinate);
                return new DomainRoute(start, end, d, TimeSpan.FromHours(d / 40.0), null);
            }

            var mapRoute = routingProvider.GetRoute(p1, p2, false, false, 15);
            if (mapRoute == null || mapRoute.Points == null || mapRoute.Points.Count == 0)
            {
                double d = GetDistance(start.Coordinate, end.Coordinate);
                return new DomainRoute(start, end, d, TimeSpan.FromHours(d / 40.0), null);
            }

            double distanceKm = mapRoute.Distance;
            TimeSpan duration;
            if (!string.IsNullOrEmpty(mapRoute.Duration))
            {
                if (!TimeSpan.TryParse(mapRoute.Duration, out duration))
                    duration = TimeSpan.FromHours(distanceKm / 40.0);
            }
            else
            {
                duration = TimeSpan.FromHours(distanceKm / 40.0);
            }

            return new DomainRoute(start, end, distanceKm, duration, null);
        }

        private double GetDistance(Coordinate c1, Coordinate c2)
        {
            var d1 = c1.Latitude * (Math.PI / 180.0);
            var num1 = c1.Longitude * (Math.PI / 180.0);
            var d2 = c2.Latitude * (Math.PI / 180.0);
            var num2 = c2.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))) / 1000.0;
        }
    }
}