using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Infrastructure.Interfaces
{
    public interface IGMapService
    {
        /// <summary>
        /// Tìm kiếm địa điểm theo chuỗi truy vấn (ví dụ: "Quận 1, TP HCM")
        /// </summary>
        /// <param name="query">Chuỗi tìm kiếm</param>
        /// <returns>Danh sách các Location tìm được</returns>
        Task<List<Location>> SearchLocationAsync(string query);

        /// <summary>
        /// Reverse geocoding: từ tọa độ (lat, lon) -> địa chỉ (Location)
        /// </summary>
        Task<Location> ReverseGeocodeAsync(double latitude, double longitude);

        /// <summary>
        /// Lấy route (lộ trình) giữa hai điểm, bao gồm khoảng cách, thời gian ước tính, polyline (nếu có)
        /// </summary>
        Task<Route> GetRouteAsync(Location start, Location end);
    }
}