using Application.Interfaces;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    /// <summary>
    /// Mock implementation of IMapService for UI testing.
    /// </summary>
    public class MockMapService : IMapService
    {
        private bool _searchLocationCalled;
        private bool _reverseGeocodeCalled;
        private bool _getRouteCalled;
        private List<Location> _searchLocations;
        private Location _reverseGeocodeLocation;
        private Route _route;

        public bool SearchLocationCalled => _searchLocationCalled;
        public bool ReverseGeocodeCalled => _reverseGeocodeCalled;
        public bool GetRouteCalled => _getRouteCalled;
        public List<Location> LastSearchLocations => _searchLocations;
        public Location LastReverseGeocodeLocation => _reverseGeocodeLocation;
        public Route LastRoute => _route;

        public Task<List<Location>> SearchLocationAsync(string query)
        {
            _searchLocationCalled = true;
            return Task.FromResult(_searchLocations ?? new List<Location>());
        }

        public Task<Location> ReverseGeocodeAsync(double latitude, double longitude)
        {
            _reverseGeocodeCalled = true;
            return Task.FromResult(_reverseGeocodeLocation);
        }

        public Task<Route> GetRouteAsync(Location start, Location end)
        {
            _getRouteCalled = true;
            return Task.FromResult(_route);
        }

        // Setup methods for configuring mock behavior
        public void SetupSearchLocation(List<Location> locations)
        {
            _searchLocations = locations;
        }

        public void SetupReverseGeocode(Location location)
        {
            _reverseGeocodeLocation = location;
        }

        public void SetupGetRoute(Route route)
        {
            _route = route;
        }
    }
}
