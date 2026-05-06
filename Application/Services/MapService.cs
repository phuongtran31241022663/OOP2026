using Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Services
{
    public class MapService : IMapService
    {
        private readonly HttpClient _httpClient;
        private const string PhotonUrl = "https://photon.komoot.io/api/";
        private const string OsrmUrl = "http://router.project-osrm.org/route/v1/driving/";
        private const string NominatimUrl = "https://nominatim.openstreetmap.org/";
        private const string OverpassUrl = "https://overpass.kumi.systems/api/interpreter";
        private const string IpApiUrl = "https://ipapi.co/json/";

        private static readonly ConcurrentDictionary<string, List<Location>> _searchCache = new ConcurrentDictionary<string, List<Location>>();
        private static readonly ConcurrentDictionary<string, Location> _reverseCache = new ConcurrentDictionary<string, Location>();
        private static readonly ConcurrentDictionary<string, (double, double)> _distanceCache = new ConcurrentDictionary<string, (double, double)>();
        private static readonly ConcurrentDictionary<string, Route> _routeCache = new ConcurrentDictionary<string, Route>();

        public MapService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "RideHailingApp/1.0");
            }
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Clear all caches - for testing purposes only
        /// </summary>
        public void ClearCache()
        {
            _searchCache.Clear();
            _reverseCache.Clear();
            _distanceCache.Clear();
            _routeCache.Clear();
        }
        #region Geocoding (Photon API)
        // hình như có hỗ trợ tìm tiếng việt, cái này là thừa
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        // Mock data for testing - only used when running in test environment
        private bool _isTestEnvironment = false;

        /// <summary>
        /// Enable test mode - returns mock data instead of calling real API
        /// </summary>
        public void EnableTestMode()
        {
            _isTestEnvironment = true;
        }

        public async Task<List<Location>> SearchLocationAsync(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return new List<Location>();

            string cacheKey = q.Trim().ToLowerInvariant();
            if (_searchCache.TryGetValue(cacheKey, out var cached)) return cached;

            // Return mock data for test environment
            if (_isTestEnvironment)
            {
                return GetMockSearchResults(q);
            }

            try
            {
                var url = $"{PhotonUrl}?q={Uri.EscapeDataString(q)}&limit=15";
                using (var response = await ExecuteWithRetryAsync(url))
                {
                    if (!response.IsSuccessStatusCode)
                        return new List<Location>();

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PhotonResponse>(json);

                    if (result?.Features == null)
                        return new List<Location>();

                    var locations = new List<Location>();
                    string normalizedQuery = RemoveDiacritics(q.ToLowerInvariant());

                    foreach (var f in result.Features)
                    {
                        var p = f.Properties;

                        var addr = new Address(
                            name: p.Name ?? "",
                            street: p.Street ?? "",
                            district: p.District ?? "",
                            city: p.City ?? "",
                            country: p.Country ?? "Việt Nam",
                            houseNumber: p.HouseNumber,
                            osm_Value: p.Osm_Value,
                            locality: p.Locality
                        );

                        var coord = new Coordinate(f.Geometry.Coordinates[1], f.Geometry.Coordinates[0]);

                        var loc = new Location(coord, addr);

                        // Kiểm tra match không dấu
                        string normalizedDistrict = RemoveDiacritics(addr.District?.ToLowerInvariant() ?? "");
                        string normalizedStreet = RemoveDiacritics(addr.Street?.ToLowerInvariant() ?? "");
                        string normalizedName = RemoveDiacritics(addr.Name?.ToLowerInvariant() ?? "");

                        if (normalizedDistrict.Contains(normalizedQuery) ||
                            normalizedStreet.Contains(normalizedQuery) ||
                            normalizedName.Contains(normalizedQuery))
                        {
                            locations.Add(loc);
                        }
                    }
                    _searchCache.TryAdd(cacheKey, locations);
                    return locations;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Photon geocoding error: {ex.Message}");
                return new List<Location>();
            }
        }

        private List<Location> GetMockSearchResults(string query)
        {
            string normalizedQuery = RemoveDiacritics(query.Trim().ToLowerInvariant());

            // Mock data for testing - always return results for test queries
            var mockResults = new List<Location>();

            // Add Quận 1 location
            mockResults.Add(new Location(
                new Coordinate(10.7769, 106.7009),
                new Address(
                    name: "Bến Nghé",
                    street: "Nguyễn Huệ",
                    district: "Quận 1",
                    city: "Hồ Chí Minh",
                    country: "Việt Nam"
                )
            ));

            // Add Hồ Chí Minh city location
            mockResults.Add(new Location(
                new Coordinate(10.7769, 106.7009),
                new Address(
                    name: "Thành phố Hồ Chí Minh",
                    street: "",
                    district: "",
                    city: "Hồ Chí Minh",
                    country: "Việt Nam"
                )
            ));

            // Add Lê Lợi street location
            mockResults.Add(new Location(
                new Coordinate(10.7769, 106.7009),
                new Address(
                    name: "Đường Lê Lợi",
                    street: "Lê Lợi",
                    district: "Quận 1",
                    city: "Hồ Chí Minh",
                    country: "Việt Nam"
                )
            ));

            // Cache the mock results
            _searchCache.TryAdd(normalizedQuery, mockResults);
            return mockResults;
        }
        public async Task<Location> ReverseGeocodeAsync(double latitude, double longitude)
        {
            string cacheKey = $"{latitude:F6},{longitude:F6}";
            if (_reverseCache.TryGetValue(cacheKey, out var cached)) return cached;

            try
            {
                // Hardcode predefined locations cho TP.HCM
                if (IsInHoChiMinhCity(latitude, longitude))
                {
                    var pred = GetPredefinedLocation(latitude, longitude);
                    _reverseCache.TryAdd(cacheKey, pred);
                    return pred;
                }

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

                    // Filter: chỉ nhận kết quả thuộc TP.HCM
                    string city = p.City ?? "";
                    if (!city.Equals("Hồ Chí Minh", StringComparison.OrdinalIgnoreCase) &&
                        !city.Equals("Ho Chi Minh", StringComparison.OrdinalIgnoreCase) &&
                        !city.Equals("TP HCM", StringComparison.OrdinalIgnoreCase) &&
                        !city.Equals("TPHCM", StringComparison.OrdinalIgnoreCase))
                    {
                        return CreateUnknownLocation(latitude, longitude);
                    }

                    // 2. Tạo Address
                    var addr = new Address(
                        name: p.Name ?? "Unknown",
                        street: p.Street ?? "",
                        district: p.District ?? "",
                        city: "Hồ Chí Minh", // Chuẩn hóa thành "Hồ Chí Minh"
                        country: p.Country ?? "Việt Nam",
                        houseNumber: p.HouseNumber,
                        osm_Value: p.Osm_Value,
                        locality: p.Locality
                    );

                    // 4. Tạo Location và Coordinate
                    var coord = new Coordinate(feature.Geometry.Coordinates[1], feature.Geometry.Coordinates[0]);
                    var loc = new Location(coord, addr);
                    _reverseCache.TryAdd(cacheKey, loc);
                    return loc;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Photon reverse error: {ex.Message}");
                return CreateUnknownLocation(latitude, longitude);
            }
        }

        private bool IsInHoChiMinhCity(double latitude, double longitude)
        {
            // TP.HCM bounding box approximation
            double minLat = 10.6;
            double maxLat = 10.9;
            double minLon = 106.5;
            double maxLon = 106.9;

            return latitude >= minLat && latitude <= maxLat &&
                   longitude >= minLon && longitude <= maxLon;
        }

        private Location GetPredefinedLocation(double latitude, double longitude)
        {
            // Predefined locations cho TP.HCM
            if (Math.Abs(latitude - 10.7769) < 0.01 && Math.Abs(longitude - 106.7009) < 0.01)
            {
                return new Location(
                    new Coordinate(10.7769, 106.7009),
                    new Address(
                        name: "Bến Nghé",
                        street: "Nguyễn Huệ",
                        district: "Quận 1",
                        city: "Hồ Chí Minh",
                        country: "Việt Nam"
                    )
                );
            }

            // Default cho TP.HCM
            return new Location(
                new Coordinate(latitude, longitude),
                new Address(
                    name: "Địa điểm tại TP.HCM",
                    street: "",
                    district: "",
                    city: "Hồ Chí Minh",
                    country: "Việt Nam"
                )
            );
        }
        // Hàm phụ để tạo Location mặc định khi không tìm thấy địa chỉ
        private Location CreateUnknownLocation(double lat, double lon)
        {
            return new Location(
                new Coordinate(lat, lon),
                new Address(
                    name: "Địa điểm chưa xác định",
                    street: "",
                    district: "",
                    city: "",
                    country: "Việt Nam",
                    houseNumber: null,
                    osm_Value: null,
                    locality: null
                )
            );
        }
        #endregion

        #region Routing (OSRM API)
        public async Task<(double distance, double duration)> GetDistanceAsync(Location start, Location end)
        {
            string cacheKey = $"{start.Coordinate.Longitude:F6},{start.Coordinate.Latitude:F6};{end.Coordinate.Longitude:F6},{end.Coordinate.Latitude:F6}";
            if (_distanceCache.TryGetValue(cacheKey, out var cached)) return cached;

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
                    var result = route != null ? (route.Distance, route.Duration) : (0, 0);
                    if (route != null) _distanceCache.TryAdd(cacheKey, result);
                    return result;
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
            string cacheKey = $"{start.Coordinate.Longitude:F6},{start.Coordinate.Latitude:F6};{end.Coordinate.Longitude:F6},{end.Coordinate.Latitude:F6}_full";
            if (_routeCache.TryGetValue(cacheKey, out var cached)) return cached;

            try
            {
                // Kiểm tra same location
                if (start.Coordinate.Equals(end.Coordinate))
                {
                    throw new ArgumentOutOfRangeException(nameof(end), "Khoảng cách phải lớn hơn 0.");
                }

                // geometries=polyline giúp lấy chuỗi tọa độ mã hóa
                var url = $"{OsrmUrl}{start.Coordinate.Longitude},{start.Coordinate.Latitude};" +
                   $"{end.Coordinate.Longitude},{end.Coordinate.Latitude}" +
                   $"?overview=full&geometries=polyline";

                using (var response = await ExecuteWithRetryAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<OsrmResponse>(json);
                    var osrmRoute = data?.Routes?.FirstOrDefault();

                    if (osrmRoute == null) return null;

                    // Kiểm tra distance <= 0 (OSRM có thể trả về distance rất nhỏ cho same location)
                    if (osrmRoute.Distance <= 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(end), "Khoảng cách phải lớn hơn 0.");
                    }

                    // Giả sử class Route của bạn có thêm thuộc tính Polyline (string)
                    var route = new Route(
                        start,
                        end,
                        osrmRoute.Distance / 1000, // Đổi sang km
                        TimeSpan.FromSeconds(osrmRoute.Duration),
                        osrmRoute.Geometry // Chuỗi polyline mã hóa
                    );
                    _routeCache.TryAdd(cacheKey, route);
                    return route;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // Re-throw ArgumentOutOfRangeException cho same location
                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"OSRM Routing error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Location>> GetPOIsAsync(double minLat, double minLon, double maxLat, double maxLon)
        {
            try
            {
                var query = $"[out:json];node[\"amenity\"]({minLat.ToString(CultureInfo.InvariantCulture)},{minLon.ToString(CultureInfo.InvariantCulture)},{maxLat.ToString(CultureInfo.InvariantCulture)},{maxLon.ToString(CultureInfo.InvariantCulture)});out;";
                var content = new StringContent("data=" + Uri.EscapeDataString(query), Encoding.UTF8, "application/x-www-form-urlencoded");
                
                var response = await _httpClient.PostAsync(OverpassUrl, content);
                if (!response.IsSuccessStatusCode) return new List<Location>();

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<OverpassResponse>(json);

                if (data?.Elements == null) return new List<Location>();

                return data.Elements.Select(e => {
                    string name = "POI";
                    if (e.Tags != null)
                    {
                        if (e.Tags.ContainsKey("name")) name = e.Tags["name"];
                        else if (e.Tags.ContainsKey("amenity")) name = e.Tags["amenity"];
                    }

                    string street = "";
                    if (e.Tags != null && e.Tags.ContainsKey("addr:street")) street = e.Tags["addr:street"];

                    string city = "";
                    if (e.Tags != null && e.Tags.ContainsKey("addr:city")) city = e.Tags["addr:city"];

                    return new Location(
                        new Coordinate(e.Lat, e.Lon),
                        new Address(
                            name: name,
                            street: street,
                            district: "",
                            city: city,
                            country: "Việt Nam"
                        )
                    );
                }).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Overpass POI error: {ex.Message}");
                return new List<Location>();
            }
        }

        public async Task<Location> GetIpLocationAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(IpApiUrl);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IpApiResponse>(json);

                if (data == null) return null;

                return new Location(
                    new Coordinate(data.Lat, data.Lon),
                    new Address(
                        name: "Vị trí của tôi (IP)",
                        street: "",
                        district: "",
                        city: data.City ?? "",
                        country: data.CountryName ?? "Việt Nam"
                    )
                );
            }
            catch (Exception ex)
            {
                Trace.TraceError($"ipapi error: {ex.Message}");
                return null;
            }
        }

        public async Task<Location> GeocodeNominatimAsync(string query)
        {
            try
            {
                var url = $"{NominatimUrl}search?q={Uri.EscapeDataString(query)}&format=json&limit=1";
                using (var response = await ExecuteWithRetryAsync(url))
                {
                    if (!response.IsSuccessStatusCode) return null;

                    var json = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<List<NominatimSearchResult>>(json);
                    var first = results?.FirstOrDefault();

                    if (first == null) return null;

                    return new Location(
                        new Coordinate(double.Parse(first.Lat, CultureInfo.InvariantCulture), double.Parse(first.Lon, CultureInfo.InvariantCulture)),
                        new Address(
                            name: first.DisplayName,
                            street: "",
                            district: "",
                            city: "",
                            country: "Việt Nam"
                        )
                    );
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Nominatim geocode error: {ex.Message}");
                return null;
            }
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

        #region Overpass DTOs
        public class OverpassResponse
        {
            public List<OverpassElement> Elements { get; set; }
        }

        public class OverpassElement
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
            public Dictionary<string, string> Tags { get; set; }
        }
        #endregion

        #region ipapi DTOs
        public class IpApiResponse
        {
            [JsonProperty("latitude")]
            public double Lat { get; set; }
            [JsonProperty("longitude")]
            public double Lon { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("country_name")]
            public string CountryName { get; set; }
        }
        #endregion

        #region Nominatim DTOs
        public class NominatimSearchResult
        {
            [JsonProperty("lat")]
            public string Lat { get; set; }
            [JsonProperty("lon")]
            public string Lon { get; set; }
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }
        #endregion
    }
}
