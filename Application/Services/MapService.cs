using Domain.ValueObjects;
using GMap.NET; // GMap.NET
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLocation = Domain.ValueObjects.Location;
using DomainRoute = Domain.ValueObjects.Route;
using Application.Interfaces;

namespace Application.Services
{
    public class MapService : IMapService
    {
        private readonly GMapProvider _provider;

        public MapService()
        {
            _provider = GMapProviders.GoogleMap; 
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
        }
        public Task<DomainLocation> ReverseGeocodeAsync(double lat, double lon)
        {
            return Task.FromResult(
                new DomainLocation(
                    new Coordinate(lat, lon),
                    new Address("Unknown", "", "", "", "", "", "", "Việt Nam")
                )
            );
        }

        public async Task<List<DomainLocation>> SearchLocationAsync(string q)
        {
            var geoProvider = _provider as GeocodingProvider;
            if (geoProvider == null)
                return new List<DomainLocation>();

            // Gọi đúng phương thức: GetPoints(string keywords, out List<PointLatLng> pointList)
            var status = geoProvider.GetPoints(q, out List<PointLatLng> points);
            if (status != GeoCoderStatusCode.OK || points == null)
                return new List<DomainLocation>();

            var results = new List<DomainLocation>();
            foreach (var point in points)
            {
                // GMap.NET không trả sẵn địa chỉ, bạn cần tự reverse geocode từng điểm
                // hoặc dùng thông tin có sẵn (thường là không có)
                var coord = new Coordinate(point.Lat, point.Lng);
                var address = await GetAddressFromPoint(point); // viết hàm helper
                results.Add(new DomainLocation(coord, address));
            }
            return results;
        }

        private async Task<Address> GetAddressFromPoint(PointLatLng point)
        {
            // Gọi ReverseGeocodeAsync từ GMap.NET hoặc từ chính MapService
            // Hoặc dùng thông tin có sẵn trong GeoCoderStatusCode
            // Nếu không có ReverseGeocode, trả về Address rỗng
            return new Address("", "", "", "", "", "", "", "Việt Nam");
        }

        public async Task<DomainRoute> GetRouteAsync(DomainLocation start, DomainLocation end)
        {
            var p1 = new PointLatLng(start.Coordinate.Latitude, start.Coordinate.Longitude);
            var p2 = new PointLatLng(end.Coordinate.Latitude, end.Coordinate.Longitude);

            var routingProvider = _provider as RoutingProvider;
            if (routingProvider == null)
                return null;

            var mapRoute = routingProvider.GetRoute(p1, p2, false, false, 15);
            if (mapRoute == null || mapRoute.Points == null || mapRoute.Points.Count == 0)
                return null;

            // Dùng distance có sẵn từ GMap (km)
            double distanceKm = mapRoute.Distance;
            // Thời gian: nếu có property Duration (string) thử parse, nếu không thì ước lượng
            TimeSpan duration;
            if (!string.IsNullOrEmpty(mapRoute.Duration))
            {
                // Định dạng thường là "HH:MM:SS" hoặc "MM:SS"
                if (!TimeSpan.TryParse(mapRoute.Duration, out duration))
                    duration = TimeSpan.FromHours(distanceKm / 40.0); // fallback
            }
            else
            {
                duration = TimeSpan.FromHours(distanceKm / 40.0);
            }

            // Polyline: GMap không cung cấp trực tiếp, có thể lấy từ Tag, bỏ qua
            string polyline = null;

            var route = new DomainRoute(start, end, distanceKm, duration, polyline);
            return route;
        }
    }
}