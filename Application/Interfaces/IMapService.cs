using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMapService
    {
        // Lấy route (lộ trình) giữa hai điểm
        Task<Route> GetRouteAsync(Location pickup, Location destination);

        // Tìm kiếm địa điểm
        Task<List<Location>> SearchLocationAsync(string query);

        // Reverse geocoding: tọa độ -> địa chỉ
        Task<Location> ReverseGeocodeAsync(double latitude, double longitude);
    }
}