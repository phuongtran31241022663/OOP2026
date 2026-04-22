using Application.Interfaces;
using Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices
{
    internal class MapService : IMapService
    {
        private readonly HttpClient _httpClient;
        private const string PhotonUrl = "https://photon.komoot.io/api/";
        private const string OsrmUrl = "http://router.project-osrm.org/route/v1/driving/";
        public MapService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "RideHailingApp/1.0");
            }
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }
        #region Geocoding (Photon API)
        public async Task<List<Location>> SearchLocation(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return new List<Location>();

            try
            {
                var url = $"{PhotonUrl}?q={Uri.EscapeDataString(q)}&limit=7";
                using (var response = await _httpClient.GetAsync(url))
                {
                    if (!response.IsSuccessStatusCode)
                        return new List<Location>();

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PhotonResponse>(json);

                    if (result?.Features == null)
                        return new List<Location>();

                    var locations = new List<Location>();
                    foreach (var f in result.Features)
                    {
                        var p = f.Properties;

                        // Khởi tạo Address theo đúng thứ tự constructor: 
                        var addr = new Address(
      p.Osm_Value ?? "",      // osm_Value
      p.HouseNumber ?? "",    // houseNumber
      p.Name ?? "",           // name
      p.Street ?? "",         // street
      p.Locality ?? "",       // locality
      p.District ?? "",       // district
      p.City ?? "",// city 
      p.Country ?? "Việt Nam" // country
  );

                        var coord = new Coordinate { Latitude = f.Geometry.Coordinates[1], Longitude = f.Geometry.Coordinates[0] };

                        var loc = new Location(coord, addr);

                        locations.Add(loc);
                    }
                    return locations;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Photon geocoding error: {ex.Message}");
                return new List<Location>();
            }
        }
        public async Task<Location> ReverseGeocodeAsync(double latitude, double longitude)
        {
            try
            {
                // 1. Photon yêu cầu lat/lon cho reverse
                var url = $"{PhotonUrl}reverse?lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}";

                using (var response = await ExecuteWithRetryAsync(url))
                {
                    if (!response.IsSuccessStatusCode)
                        return CreateUnknownLocation(latitude, longitude);

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PhotonResponse>(json);
                    var feature = result?.Features?.FirstOrDefault();

                    if (feature == null)
                        return CreateUnknownLocation(latitude, longitude);

                    var p = feature.Properties;
                    // 2. Tạo Address
                    var addr = new Address(
                     p.Osm_Value ?? "",
                     p.HouseNumber ?? "",
                     p.Name ?? "",
                     p.Street ?? "",
                     p.Locality ?? "",
                     p.District ?? "",
                     p.City ?? "",
                     p.Country ?? "Việt Nam"
                 );

                    // 4. Tạo Location và Coordinate
                    var coord = new Coordinate { Latitude = latitude, Longitude = longitude };
                    return new Location(coord, addr);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Photon reverse error: {ex.Message}");
                return CreateUnknownLocation(latitude, longitude);
            }
        }
        // Hàm phụ để tạo Location mặc định khi không tìm thấy địa chỉ
        private Location CreateUnknownLocation(double lat, double lon)
        {
            return new Location(
     new Coordinate { Latitude = lat, Longitude = lon },
     new Address("Địa điểm chưa xác định", "", "", "", "", "", "", "Việt Nam")
 );
        }
        #endregion

        #region Routing (OSRM API)
        public async Task<(double distance, double duration)> GetDistanceAsync(Location start, Location end)
        {
            try
            {
                // OSRM: lng,lat;lng,lat
                var startCoord = start.Coordinate;
                var endCoord = end.Coordinate;
                var url = $"{OsrmUrl}{startCoord.Longitude},{startCoord.Latitude};{endCoord.Longitude},{endCoord.Latitude}?overview=false";

                using (var response = await ExecuteWithRetryAsync(url))
                {
                    if (!response.IsSuccessStatusCode) return (0, 0);

                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<OsrmResponse>(json);
                    var route = data?.Routes?.FirstOrDefault();

                    return route != null ? (route.Distance, route.Duration) : (0, 0);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw; // hoặc return Result object
            }
        }
        #endregion

        private async Task<HttpResponseMessage> ExecuteWithRetryAsync(string url, int maxRetries = 3)
        {
            int delay = 1000;
            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode) return response;

                    if ((int)response.StatusCode >= 500 || (int)response.StatusCode == 429)
                    {
                        if (attempt < maxRetries - 1)
                        {
                            await Task.Delay(delay);
                            delay *= 2;
                            continue;
                        }
                    }
                    return response;
                }
                catch (HttpRequestException) when (attempt < maxRetries - 1)
                {
                    await Task.Delay(delay);
                    delay *= 2;
                }
            }
            throw new HttpRequestException("Max retries exceeded");
        }
        public async Task<Route> GetRouteAsync(Location start, Location end)
        {
            try
            {
                // geometries=polyline giúp lấy chuỗi tọa độ mã hóa
                var url = $"{OsrmUrl}{start.Coordinate.Longitude},{start.Coordinate.Latitude};" +
                   $"{end.Coordinate.Longitude},{end.Coordinate.Latitude}" +
                   $"?overview=full&geometries=polyline";

                using (var response = await _httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<OsrmResponse>(json);
                    var osrmRoute = data?.Routes?.FirstOrDefault();

                    if (osrmRoute == null) return null;

                    // Giả sử class Route của bạn có thêm thuộc tính Polyline (string)
                    return new Route(
                        start,
                        end,
                        osrmRoute.Distance / 1000, // Đổi sang km
                        TimeSpan.FromSeconds(osrmRoute.Duration),
                        osrmRoute.Geometry // Chuỗi polyline mã hóa
                    );
                }
            }

            catch (Exception ex)
            {
                Trace.TraceError($"OSRM Routing error: {ex.Message}");
                return null;
            }
        }
        List<Coordinate> DecodePolyline(string polyline)
        {
            // thuật toán Google polyline decoding
            return new List<Coordinate>(); // Implement decoding logic here
        }
        //public string GetMapTileUrl(Coordinate c, int zoom)
        //{
        //    // Mock OpenStreetMap tile URL
        //    int x = (int)((c.Longitude + 180) / 360 * (1 << zoom));
        //    int y = (int)((1 - Math.Log(Math.Tan(c.Latitude * Math.PI / 180) + 1 / Math.Cos(c.Latitude * Math.PI / 180)) / Math.PI) / 2 * (1 << zoom));
        //    return $"https://tile.openstreetmap.org/{zoom}/{x}/{y}.png";
        //}
        #region Photon DTOs
        public class PhotonResponse
        {
            public List<Feature> Features { get; set; }
        }
        public class Feature
        {
            public Geometry Geometry { get; set; }
            public Properties Properties { get; set; }
        }
        public class Geometry
        {
            public List<double> Coordinates { get; set; } // [lng, lat]
        }
        public class Properties
        {
            public string Osm_Value { get; set; }
            public string HouseNumber { get; set; }
            public string Name { get; set; }
            public string Street { get; set; }
            public string Locality { get; set; }
            public string District { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
        #endregion
        #region OSRM DTOs
        public class OsrmResponse
        {
            [JsonProperty("routes")]
            public List<OsrmRoute> Routes { get; set; }
        }

        public class OsrmRoute
        {
            [JsonProperty("distance")]
            public double Distance { get; set; }

            [JsonProperty("duration")]
            public double Duration { get; set; }

            [JsonProperty("geometry")]
            public string Geometry { get; set; } // Đây là chuỗi Polyline
        }
        #endregion
    }
}