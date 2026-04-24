using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Interfaces;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using DomainLocation = Domain.ValueObjects.Location;
using DomainRoute = Domain.ValueObjects.Route;

namespace Infrastructure.ExternalServices
{
    internal class GMapService : IMapService
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
        public Task<DomainRoute> GetRouteAsync(DomainLocation start, DomainLocation end)
        {
            var p1 = new PointLatLng(start.Coordinate.Latitude, start.Coordinate.Longitude);
            var p2 = new PointLatLng(end.Coordinate.Latitude, end.Coordinate.Longitude);

            var routingProvider = _provider as RoutingProvider;

            if (routingProvider == null)
                return Task.FromResult<DomainRoute>(null);

            var mapRoute = routingProvider.GetRoute(p1, p2, false, false, 15);

            if (mapRoute == null || mapRoute.Points == null || mapRoute.Points.Count == 0)
                return Task.FromResult<DomainRoute>(null);

            var coords = mapRoute.Points
                .Select(p => new Coordinate(p.Lat, p.Lng))
                .ToList();

            double distance = CalculateDistance(coords);
            var duration = TimeSpan.FromHours(distance / 40.0);

            return Task.FromResult(new DomainRoute(
                start,
                end,
                distance,
                duration,
                null
            ));
        }

        public async Task<(double distance, double duration)> GetDistanceAsync(DomainLocation start, DomainLocation end)
        {
            var route = await GetRouteAsync(start, end);

            if (route == null)
                return (0, 0);

            return (route.Distance * 1000, route.Duration.TotalSeconds);
        }
        #endregion

        #region Helpers
        private double CalculateDistance(List<Coordinate> coords)
        {
            double total = 0;

            for (int i = 1; i < coords.Count; i++)
            {
                total += Haversine(
                    coords[i - 1].Latitude,
                    coords[i - 1].Longitude,
                    coords[i].Latitude,
                    coords[i].Longitude
                );
            }

            return total;
        }

        private double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;

            var dLat = ToRad(lat2 - lat1);
            var dLon = ToRad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRad(double deg) => deg * Math.PI / 180;
        #endregion
    }
}
