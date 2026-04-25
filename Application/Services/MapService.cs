using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Interfaces; // IGMapService

namespace Infrastructure.ExternalServices
{
    public class MapService : IMapService
    {
        private readonly IGMapService _gMapService;

        public MapService(IGMapService gMapService)
        {
            _gMapService = gMapService;
        }

        public async Task<Route> GetRouteAsync(Location pickup, Location destination)
        {
            return await _gMapService.GetRouteAsync(pickup, destination);
        }

        public async Task<List<Location>> SearchLocationAsync(string query)
        {
            return await _gMapService.SearchLocationAsync(query);
        }

        public async Task<Location> ReverseGeocodeAsync(double latitude, double longitude)
        {
            return await _gMapService.ReverseGeocodeAsync(latitude, longitude);
        }
    }
}