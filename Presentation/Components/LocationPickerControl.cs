using Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// Location picker control with TextBox + ListBox pattern.
    /// When clicked, shows a TextBox for keyword input and ListBox for search results.
    /// </summary>
    public partial class LocationPickerControl : UserControl
    {
        private Application.Interfaces.IMapService _mapService;
        private System.Windows.Forms.Timer _searchTimer;
        private bool _isSearching = false;
        private string _lastSearchQuery = string.Empty;
        
        // Constants
        private const string RecentLocationsFileName = "recent_locations.json";
        private const int MaxRecentLocations = 10;
        public const double CoordinateTolerance = 0.0001;

        // UI Controls
        private TextBox _txtSearch;
        private ListBox _lstSuggestions;
        private Panel _pnlSuggestions;
        private Label _lblPlaceholder;

// Properties
        public Location SelectedLocation { get; set; }
        public Location CurrentPickup { get; set; }
        public Location CurrentDestination { get; set; }
        public string SlotLabel { get; set; } = string.Empty; // "A" or "B"

        // Recent locations storage (in-memory, keyed by user identifier)
        private static readonly Dictionary<string, List<Location>> _recentByUser = new Dictionary<string, List<Location>>();
        private string _currentUserIdentifier;

        public void SetMapService(Application.Interfaces.IMapService mapService)
        {
            _mapService = mapService;
        }

        // Fixed UEH locations
        private static readonly List<Location> _fixedLocations = new List<Location>
        {
            new Location(
                new Coordinate(10.7826, 106.6954),
                new Address("UEH Cơ sở A", "59C Nguyễn Đình Chiểu", "Quận 3", "Ho Chi Minh", "Vietnam")
            ),
            new Location(
                new Coordinate(10.7679, 106.6707),
                new Address("UEH Cơ sở B", "279 Nguyễn Tri Phương", "Quận 10", "Ho Chi Minh", "Vietnam")
            ),
            new Location(
                new Coordinate(10.7132, 106.6655),
                new Address("UEH Cơ sở N", "Nguyễn Văn Linh", "Bình Chánh", "Ho Chi Minh", "Vietnam")
            )
        };

        public LocationPickerControl()
        {
            InitializeComponent();
            
            // Configure control
            this.Size = new Size(334, 34);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            // Initialize search timer
            _searchTimer = new System.Windows.Forms.Timer { Interval = 800 }; // 800ms debounce
            _searchTimer.Tick += SearchTimer_Tick;

            // Load initial data
            PopulateSuggestions();
        }

        
        private void UpdatePlaceholder()
        {
            if (SelectedLocation != null)
            {
                _lblPlaceholder.Text = FormatLocationForDisplay(SelectedLocation);
                _lblPlaceholder.Visible = true;
                _txtSearch.Visible = false;
            }
            else
            {
                _lblPlaceholder.Text = "Chọn điểm đón...";
                _lblPlaceholder.Visible = true;
                _txtSearch.Visible = true;
            }
        }

        private void LblPlaceholder_Click(object sender, EventArgs e)
        {
            _lblPlaceholder.Visible = false;
            _txtSearch.Visible = true;
            _txtSearch.Focus();
            ShowSuggestionsPanel();
            PopulateSuggestions();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            ShowSuggestionsPanel();
            PopulateSuggestions();
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            // Delay hide to allow click on listbox
            var timer = new System.Windows.Forms.Timer { Interval = 200 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                timer.Dispose();
                if (!_lstSuggestions.ClientRectangle.Contains(_lstSuggestions.PointToClient(Cursor.Position)))
                {
                    HideSuggestionsPanel();
                }
            };
            timer.Start();
        }

        private void ShowSuggestionsPanel()
        {
            if (_pnlSuggestions.Parent != this)
            {
                this.Controls.Add(_pnlSuggestions);
            }
            _pnlSuggestions.BringToFront();
            _pnlSuggestions.Visible = true;
        }

        private void HideSuggestionsPanel()
        {
            _pnlSuggestions.Visible = false;
            UpdatePlaceholder();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            if (_txtSearch.Text.Length >= 3)
            {
                _searchTimer.Start();
            }
            else if (string.IsNullOrWhiteSpace(_txtSearch.Text))
            {
                // Reset to default list when cleared
                PopulateSuggestions();
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                PerformSearch(_txtSearch.Text);
            }
            else if (e.KeyCode == Keys.Down && _lstSuggestions.Visible && _lstSuggestions.Items.Count > 0)
            {
                _lstSuggestions.SelectedIndex = 0;
                _lstSuggestions.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideSuggestionsPanel();
            }
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            await PerformSearch(_txtSearch.Text);
        }

        private async System.Threading.Tasks.Task PerformSearch(string query)
        {
            if (_mapService == null || string.IsNullOrWhiteSpace(query) || query == _lastSearchQuery || _isSearching)
                return;

            if (query.Length < 3) return;

            _isSearching = true;
            _lastSearchQuery = query;

            try
            {
                var results = await _mapService.SearchLocationAsync(query);
                if (results != null && results.Count > 0)
                {
                    UpdateSuggestionsWithSearchResults(results);
                }
            }
            finally
            {
                _isSearching = false;
            }
        }

        private void UpdateSuggestionsWithSearchResults(List<Location> results)
        {
            _lstSuggestions.Items.Clear();
            
            // Add header
            _lstSuggestions.Items.Add(new DropdownItem { IsHeader = true, HeaderText = "KẾT QUẢ TÌM KIẾM" });

            foreach (var loc in results)
            {
                _lstSuggestions.Items.Add(new DropdownItem { IsHeader = false, Location = loc });
            }

            _lstSuggestions.Visible = true;
        }

        private void PopulateSuggestions()
        {
            // Build a list of suggestions from recent + fixed locations
            _lstSuggestions.Items.Clear();

            // Add recent locations header
            var recentLocations = GetRecentLocations(_currentUserIdentifier);
            if (recentLocations.Count > 0)
            {
                _lstSuggestions.Items.Add(new DropdownItem { IsHeader = true, HeaderText = "ĐỊA ĐIỂM GẦN ĐÂY" });
                foreach (var location in recentLocations)
                {
                    _lstSuggestions.Items.Add(new DropdownItem { IsHeader = false, Location = location });
                }
            }

            // Add fixed UEH locations header
            _lstSuggestions.Items.Add(new DropdownItem { IsHeader = true, HeaderText = "CƠ SỞ UEH" });
            foreach (var location in _fixedLocations)
            {
                _lstSuggestions.Items.Add(new DropdownItem { IsHeader = false, Location = location });
            }
        }

        private void LstSuggestions_Click(object sender, EventArgs e)
        {
            HandleSelection();
        }

        private void LstSuggestions_DoubleClick(object sender, EventArgs e)
        {
            HandleSelection();
        }

        private void LstSuggestions_MouseEnter(object sender, EventArgs e)
        {
            // Keep panel visible when mouse enters listbox
        }

        private void HandleSelection()
        {
            if (_lstSuggestions.SelectedItem is DropdownItem item)
            {
                if (item.IsHeader)
                {
                    // Don't allow header selection
                    return;
                }

                SelectedLocation = item.Location;

                // Update display
                if (SelectedLocation != null && SelectedLocation.Address != null)
                {
                    _txtSearch.Text = SelectedLocation.Address.Street + ", " + SelectedLocation.Address.District;
                }

                // Add to recent locations
                AddToRecentLocations(SelectedLocation);

                // Hide suggestions panel and show selected location
                HideSuggestionsPanel();

                // Trigger selection event
                OnSelectedLocationChanged();
            }
        }

        private string FormatLocationForDisplay(Location location)
        {
            if (location == null)
                return string.Empty;

            if (location.Address == null)
                return $"[{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}]";

            var address = location.Address;
            // Format: District, City
            if (!string.IsNullOrWhiteSpace(address.District) && !string.IsNullOrWhiteSpace(address.City))
                return $"{address.District}, {address.City}";
            else if (!string.IsNullOrWhiteSpace(address.City))
                return address.City;
            else if (!string.IsNullOrWhiteSpace(address.District))
                return address.District;
            else
                return $"[{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}]";
        }

        public void PopulateDropdown(string userIdentifier = null)
        {
            _currentUserIdentifier = userIdentifier;
            PopulateSuggestions();
        }

        private List<Location> GetRecentLocations(string userIdentifier)
        {
            if (string.IsNullOrWhiteSpace(userIdentifier))
                return new List<Location>();

            if (_recentByUser.TryGetValue(userIdentifier, out var locations))
                return locations;

            return new List<Location>();
        }

        private void AddToRecentLocations(Location location)
        {
            if (string.IsNullOrWhiteSpace(_currentUserIdentifier) || location == null) return;

            if (!_recentByUser.TryGetValue(_currentUserIdentifier, out var list))
            {
                list = new List<Location>();
                _recentByUser[_currentUserIdentifier] = list;
            }

            // Remove duplicates by coordinates
            list.RemoveAll(l =>
                Math.Abs(l.Coordinate.Latitude - location.Coordinate.Latitude) < CoordinateTolerance &&
                Math.Abs(l.Coordinate.Longitude - location.Coordinate.Longitude) < CoordinateTolerance);

            list.Insert(0, location);

            if (list.Count > MaxRecentLocations)
                list.RemoveAt(list.Count - 1);
        }

        // Event for selection changed
        public event EventHandler<LocationSelectedEventArgs> LocationSelected;
        
        protected virtual void OnSelectedLocationChanged()
        {
            LocationSelected?.Invoke(this, new LocationSelectedEventArgs(SelectedLocation));
        }

        /// <summary>
        /// Loads recent locations from storage.
        /// </summary>
        public static void LoadRecentLocations()
        {
            try
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var rideGoPath = Path.Combine(appDataPath, "RideGo");
                var filePath = Path.Combine(rideGoPath, RecentLocationsFileName);

                if (!File.Exists(filePath))
                    return;

                var json = File.ReadAllText(filePath);
                var recentData = JsonConvert.DeserializeObject<Dictionary<string, List<LocationData>>>(json);

                if (recentData == null)
                    return;

                foreach (var kvp in recentData)
                {
                    var locations = new List<Location>();
                    foreach (var locationData in kvp.Value)
                    {
                        if (locationData?.Coordinate != null && locationData.Address != null)
                        {
                            var location = new Location(
                                new Coordinate(locationData.Coordinate.Latitude, locationData.Coordinate.Longitude),
                                new Address(
                                    locationData.Address.District,
                                    locationData.Address.Street,
                                    locationData.Address.Locality,
                                    locationData.Address.City,
                                    locationData.Address.Country
                                )
                            );
                            locations.Add(location);
                        }
                    }

                    if (locations.Count > 0)
                        _recentByUser[kvp.Key] = locations;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading recent locations: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves recent locations to storage.
        /// </summary>
        public static void SaveRecentLocations()
        {
            try
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var rideGoPath = Path.Combine(appDataPath, "RideGo");
                Directory.CreateDirectory(rideGoPath);

                var filePath = Path.Combine(rideGoPath, RecentLocationsFileName);

                var recentData = new Dictionary<string, List<LocationData>>();
                foreach (var kvp in _recentByUser)
                {
                    var locationDataList = new List<LocationData>();
                    foreach (var location in kvp.Value)
                    {
                        if (location?.Coordinate != null && location.Address != null)
                        {
                            locationDataList.Add(new LocationData
                            {
                                Coordinate = new CoordinateData
                                {
                                    Latitude = location.Coordinate.Latitude,
                                    Longitude = location.Coordinate.Longitude
                                },
                                Address = new AddressData
                                {
                                    District = location.Address.District,
                                    Street = location.Address.Street,
                                    Locality = location.Address.Locality,
                                    City = location.Address.City,
                                    Country = location.Address.Country
                                }
                            });
                        }
                    }

                    if (locationDataList.Count > 0)
                        recentData[kvp.Key] = locationDataList;
                }

                var json = JsonConvert.SerializeObject(recentData, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving recent locations: {ex.Message}");
            }
        }

        // Internal item structure for suggestions list
        private class DropdownItem
        {
            public bool IsHeader { get; set; }
            public string HeaderText { get; set; }
            public Location Location { get; set; }

            public override string ToString()
            {
                if (IsHeader)
                    return HeaderText;
                else if (Location != null)
                {
                    var addr = Location.Address;
                    if (addr != null)
                        return $"{addr.Street}, {addr.District}";
                    return $"[{Location.Coordinate.Latitude:F5}, {Location.Coordinate.Longitude:F5}]";
                }
                return string.Empty;
            }
        }

        // Event args for location selection
        public class LocationSelectedEventArgs : EventArgs
        {
            public Location Location { get; }
            public LocationSelectedEventArgs(Location location) { Location = location; }
        }

        // Helper classes for JSON serialization
        private class LocationData
        {
            public CoordinateData Coordinate { get; set; }
            public AddressData Address { get; set; }
        }

        private class CoordinateData
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        private class AddressData
        {
            public string District { get; set; }
            public string Street { get; set; }
            public string Locality { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
    }
}
