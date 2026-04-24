using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IMapService
    {
        Task<(double distance, double duration)> GetDistanceAsync(Location origin, Location destination);
        Task<Route> GetRouteAsync(Location start, Location end);
        Task<List<Location>> SearchLocation(string query);
        Task<Location> ReverseGeocodeAsync(double latitude, double longitude);
    }
}