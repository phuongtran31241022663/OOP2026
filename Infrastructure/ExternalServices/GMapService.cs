using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using Infrastructure.Interfaces;
using Domain.ValueObjects;
using DomainLocation = Domain.ValueObjects.Location;
using DomainRoute = Domain.ValueObjects.Route;


namespace Infrastructure.ExternalServices
{
    internal class GMapService : IGMapService
    {
        private readonly GMapProvider _provider;

        public GMapService()
        {
            _provider = GMapProviders.GoogleMap; // hoặc OpenStreetMap
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
        }

        #region Geocoding
        public async Task<List<DomainLocation>> SearchLocation(string q)
        {
            return new List<DomainLocation>();
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
        #endregion

        #region Routing (GetRoute)
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
        #endregion
    }
}
