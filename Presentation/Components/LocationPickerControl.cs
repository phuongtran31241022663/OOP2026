using Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Presentation.Components
{
    public partial class LocationPickerControl : ComboBox
    {
        // Constants
        private const string RecentLocationsFileName = "recent_locations.json";
        private const int MaxRecentLocations = 10;
        public const double CoordinateTolerance = 0.0001;

        // Properties
        public Location SelectedLocation { get; set; }
        public Location CurrentPickup { get; set; }
        public Location CurrentDestination { get; set; }
        public string SlotLabel { get; set; } = string.Empty; // "A" or "B"

        // Recent locations storage (in-memory, keyed by user identifier)
        private static readonly Dictionary<string, List<Location>> _recentByUser = new Dictionary<string, List<Location>>();

        // Fixed UEH locations
        private static readonly List<Location> _fixedLocations = new List<Location>
        {
            new Location(
                new Coordinate(10.7769, 106.7009),
                new Address("District 1", "N/A", "District 1", "Ho Chi Minh", "Vietnam")
            ),
            new Location(
                new Coordinate(10.8756, 106.8002),
                new Address("Thu Duc", "N/A", "Thu Duc", "Ho Chi Minh", "Vietnam")
            ),
            new Location(
                new Coordinate(10.0167, 105.0833),
                new Address("Chau Phong", "N/A", "An Giang", "An Giang", "Vietnam")
            )
        };



        // Internal item structure for dropdown
        private class DropdownItem
        {
            public bool IsHeader { get; set; }
            public string HeaderText { get; set; }
            public Location Location { get; set; }
        }

        private readonly List<DropdownItem> _dropdownItems = new List<DropdownItem>();

        public LocationPickerControl()
        {
            // Configure ComboBox
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 28;
            DropDownWidth = 300;
            Cursor = Cursors.Hand;

            // Event handlers
            DrawItem += LocationPickerControl_DrawItem;
            SelectedIndexChanged += LocationPickerControl_SelectedIndexChanged;
            Click += LocationPickerControl_Click;
        }

        private void LocationPickerControl_Click(object sender, EventArgs e)
        {
            // When clicking the control, open dropdown if not already dropped down
            if (!DroppedDown)
            {
                DroppedDown = true;
            }
        }

        private void LocationPickerControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex < 0 || SelectedIndex >= _dropdownItems.Count)
            {
                SelectedLocation = null;
                return;
            }

            var item = _dropdownItems[SelectedIndex];
            if (item.IsHeader)
            {
                // If header is selected, clear selection (don't allow header selection)
                SelectedIndex = -1;
                SelectedLocation = null;
                return;
            }

            SelectedLocation = item.Location;

            // Add to recent locations if we have a user identifier
            AddToRecentLocations(SelectedLocation);
        }

        private void LocationPickerControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            if (e.Index >= _dropdownItems.Count)
                return;

            var item = _dropdownItems[e.Index];
            var bounds = e.Bounds;

            if (item.IsHeader)
            {
                // Draw header: bold, light gray background
                using (var font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(245, 245, 245)))
                {
                    e.Graphics.FillRectangle(brush, bounds);
                    TextRenderer.DrawText(
                        e.Graphics,
                        item.HeaderText,
                        font,
                        bounds,
                        SystemColors.GrayText,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
            }
            else
            {
                // Draw location item: normal text
                if (item.Location != null)
                {
                    var text = FormatLocationForDisplay(item.Location);
                    TextRenderer.DrawText(
                        e.Graphics,
                        text,
                        Font,
                        bounds,
                        SystemColors.ControlText,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                    );
                }
            }

            e.DrawFocusRectangle();
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


        /// <summary>
        /// Populates the dropdown with headers and locations.
        /// Should be called when the control needs to refresh its data.
        /// </summary>
        public void PopulateDropdown(string userIdentifier = null)
        {
            _dropdownItems.Clear();
            Items.Clear();

            // Add recent locations header and items
            _dropdownItems.Add(new DropdownItem
            {
                IsHeader = true,
                HeaderText = "ĐỊA ĐIỂM GẦN ĐÂY"
            });

            var recentLocations = GetRecentLocations(userIdentifier);
            foreach (var location in recentLocations)
            {
                _dropdownItems.Add(new DropdownItem
                {
                    IsHeader = false,
                    Location = location
                });
                Items.Add(FormatLocationForDisplay(location));
            }

            // Add fixed locations header and items
            _dropdownItems.Add(new DropdownItem
            {
                IsHeader = true,
                HeaderText = "CƠ SỞ UEH"
            });

            foreach (var location in _fixedLocations)
            {
                _dropdownItems.Add(new DropdownItem
                {
                    IsHeader = false,
                    Location = location
                });
                Items.Add(FormatLocationForDisplay(location));
            }

            // Select first item by default (which will be the first recent location or first fixed location)
            if (_dropdownItems.Count > 0)
            {
                // Find first non-header item
                for (int i = 0; i < _dropdownItems.Count; i++)
                {
                    if (!_dropdownItems[i].IsHeader)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets recent locations for a user identifier.
        /// </summary>
        private List<Location> GetRecentLocations(string userIdentifier)
        {
            if (string.IsNullOrWhiteSpace(userIdentifier))
                return new List<Location>();

            if (_recentByUser.TryGetValue(userIdentifier, out var locations))
                return locations;

            return new List<Location>();
        }

        /// <summary>
        /// Adds a location to the recent locations list for a user.
        /// </summary>
        private void AddToRecentLocations(Location location)
        {
            // We need a user identifier - in a real app, this would come from the passenger
            // For now, we'll use a placeholder or skip if not available
            // This method will be called from UcPassenger when a location is selected
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
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    "Khong the tai lich su dia diem.\nChi tiet: " + ex.Message,
                    "Loi tai du lieu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    "Du lieu lich su dia diem khong dung dinh dang.\nChi tiet: " + ex.Message,
                    "Loi dinh dang",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    "Khong the doc tep lich su dia diem.\nChi tiet: " + ex.Message,
                    "Loi doc tep",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Tai lich su dia diem that bai.\nChi tiet: " + ex.Message,
                    "Loi he thong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    "Khong the luu lich su dia diem.\nChi tiet: " + ex.Message,
                    "Loi luu du lieu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    "Du lieu lich su dia diem khong hop le khi luu.\nChi tiet: " + ex.Message,
                    "Loi dinh dang",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    "Khong the ghi tep lich su dia diem.\nChi tiet: " + ex.Message,
                    "Loi ghi tep",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(
                    "Khong du quyen luu lich su dia diem.\nChi tiet: " + ex.Message,
                    "Khong du quyen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Luu lich su dia diem that bai.\nChi tiet: " + ex.Message,
                    "Loi he thong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
