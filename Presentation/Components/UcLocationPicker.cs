using Application.Interfaces;
using Domain.ValueObjects;
using Presentation.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// Location picker control with TextBox + ListBox pattern.
    /// When clicked, shows a TextBox for keyword input and ListBox for search results.
    /// </summary>
    public partial class UcLocationPicker : UserControl
    {
        private IMapService _mapService; // dịch vụ bản đồ
        private bool _isSearching = false;
        private string _lastSearchQuery = string.Empty;
        private System.Threading.Timer _debounceTimer;
        private const int DebounceDelayMs = 300;
        private bool _pendingSearch = false;

        private const int MaxRecentLocations = 10;
        private Timer _hideTimer;
        public const double CoordinateTolerance = 0.0001;
        // controls mà ở bên .cs là sai rồi
        // UI Controls
        private TextBox _txtSearch;
        private ListBox _lstSuggestions;
        private Panel _pnlSuggestions;
        private ToolTip _displayTooltip = new ToolTip();

        // Properties
        public Location SelectedLocation { get; set; }
        public Location CurrentPickup { get; set; }
        public Location CurrentDestination { get; set; }
        public string SlotLabel { get; set; } = string.Empty; // "A" or "B"

        // Recent locations storage (in-memory, keyed by user identifier)
        private static readonly Dictionary<string, List<Location>> _recentByUser = new Dictionary<string, List<Location>>();
        private string _currentUserIdentifier;

        public void SetMapService(IMapService mapService)
        {
            _mapService = mapService;
        }

        /// <summary>
        /// Cập nhật danh sách địa điểm gần đây từ bên ngoài (UcPassenger)
        /// </summary>
        public void SetRecentLocations(List<Location> locations)
        {
            if (string.IsNullOrEmpty(_currentUserIdentifier)) return;

            if (_recentByUser.ContainsKey(_currentUserIdentifier))
            {
                _recentByUser[_currentUserIdentifier] = locations;
            }
            else
            {
                _recentByUser.Add(_currentUserIdentifier, new List<Location>(locations));
            }
        }

        public UcLocationPicker()
        {
            InitializeComponent();

            // Configure control
            Size = new Size(334, 34);
            BackColor = UiConstants.Colors.SurfaceWhite;
            ForeColor = UiConstants.Colors.TextPrimary;

            // Load initial data
            PopulateSuggestions();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _debounceTimer?.Dispose();
                _hideTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (_lstSuggestions.Items.Count == 0)
            {
                PopulateSuggestions();
            }

            ShowSuggestionsPanel();
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            // Delay hide to allow click on listbox
            _hideTimer = new Timer { Interval = 200 };
            _hideTimer.Tick += HideTimer_Tick;
            _hideTimer.Start();
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            _hideTimer.Stop();
            _hideTimer.Dispose();
            _hideTimer = null;
            
            // Check if control is disposed or being disposed
            if (IsDisposed || _lstSuggestions == null || _lstSuggestions.IsDisposed)
                return;
            if (!_lstSuggestions.Visible)
                return;
            if (!_lstSuggestions.ClientRectangle.Contains(_lstSuggestions.PointToClient(Cursor.Position)))
            {
                HideSuggestionsPanel();
            }
        }

        private void ShowSuggestionsPanel()
        {
            // Gắn panel vào Form để tránh bị cắt bởi panel cha
            Form parentForm = this.FindForm();
            if (parentForm == null) return;

            if (_pnlSuggestions.Parent != parentForm)
            {
                parentForm.Controls.Add(_pnlSuggestions);
                _pnlSuggestions.BringToFront();
            }

            // Tính vị trí tuyệt đối của textbox trên màn hình, rồi chuyển về Form client coords
            Point screenPos = _txtSearch.PointToScreen(Point.Empty);
            Point formPos = parentForm.PointToClient(screenPos);

            // Đặt panel ngay dưới textbox
            _pnlSuggestions.Location = new Point(formPos.X, formPos.Y + _txtSearch.Height);
            _pnlSuggestions.Width = _txtSearch.Width;
            _pnlSuggestions.Height = 200;
            _pnlSuggestions.Visible = true;
            _pnlSuggestions.BringToFront();

            // Chỉ focus nếu txtSearch chưa có focus, tránh kích hoạt Enter/Leave loop
            if (!_txtSearch.Focused)
            {
                _txtSearch.Focus();
            }
        }

        /// <summary>
        /// Ẩn panel và đưa nó về lại control
        /// </summary>
        private void HideSuggestionsPanel()
        {
            _pnlSuggestions.Visible = false;
            // Đưa panel về lại control để quản lý dễ hơn
            if (_pnlSuggestions.Parent != this)
            {
                this.Controls.Add(_pnlSuggestions);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Debounce: hủy timer cũ, tạo timer mới
            if (_debounceTimer != null)
            {
                _debounceTimer.Dispose();
            }
            _pendingSearch = true;

            _debounceTimer = new System.Threading.Timer(new System.Threading.TimerCallback(OnDebounceTimerTick), null, DebounceDelayMs, System.Threading.Timeout.Infinite);
        }

        private void OnDebounceTimerTick(object state)
        {
            if (!IsDisposed && _txtSearch != null && !_txtSearch.IsDisposed)
            {
                _txtSearch.Invoke(new MethodInvoker(ProcessPendingSearch));
            }
        }

        private void ProcessPendingSearch()
        {
            if (_pendingSearch)
            {
                _pendingSearch = false;
                if (_txtSearch.Text.Length >= 2)
                {
                    ShowSuggestionsPanel();
                    _ = PerformSearch(_txtSearch.Text);
                }
                else if (string.IsNullOrWhiteSpace(_txtSearch.Text))
                {
                    PopulateSuggestions();
                    ShowSuggestionsPanel();
                }
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                // Search ngay lập tức khi nhấn Enter
                _ = PerformSearch(_txtSearch.Text);
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

        private async Task PerformSearch(string query)
        {
            if (query == _lastSearchQuery || _isSearching)
                return;

            _lastSearchQuery = query;

            if (string.IsNullOrWhiteSpace(query))
            {
                PopulateSuggestions();
                ShowSuggestionsPanel();
                return;
            }

            _isSearching = true;
            try
            {
                List<Location> results = FindLocalMatches(query);

                // Nếu local không có đủ kết quả (ví dụ < 3), tìm thêm từ MapService
                if (results.Count < 3 && _mapService != null)
                {
                    List<Location> remoteResults = await _mapService.SearchLocationAsync(query);
                    if (remoteResults != null)
                    {
                        foreach (Location loc in remoteResults)
                        {
                            if (loc == null || loc.Coordinate == null) continue;

                            // Tránh trùng với kết quả local đã tìm thấy
                            if (!ContainsLocation(results, loc))
                            {
                                results.Add(loc);
                            }
                        }
                    }
                }

                if (results.Count == 0)
                {
                    PopulateSuggestions();
                    ShowSuggestionsPanel();
                    return;
                }

                UpdateSuggestionsWithSearchResults(results);
            }
            finally
            {
                _isSearching = false;
            }
        }

        private bool ContainsLocation(List<Location> list, Location loc)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Location l = list[i];
                if (Math.Abs(l.Coordinate.Latitude - loc.Coordinate.Latitude) < CoordinateTolerance &&
                    Math.Abs(l.Coordinate.Longitude - loc.Coordinate.Longitude) < CoordinateTolerance)
                {
                    return true;
                }
            }
            return false;
        }

        private List<Location> FindLocalMatches(string query)
        {
            string normalizedQuery = query.Trim().ToLowerInvariant();
            List<Location> matches = new List<Location>();
            List<Location> candidates = new List<Location>();

            candidates.AddRange(GetRecentLocations(_currentUserIdentifier));

            foreach (Location location in candidates)
            {
                if (location == null) continue;

                string displayText = FormatLocationForDisplay(location);
                string addressText = location.Address != null ? location.Address.ToString() : string.Empty;

                bool isMatch =
                    (!string.IsNullOrWhiteSpace(displayText) && displayText.ToLowerInvariant().Contains(normalizedQuery)) ||
                    (!string.IsNullOrWhiteSpace(addressText) && addressText.ToLowerInvariant().Contains(normalizedQuery));

                if (!isMatch) continue;

                bool isDuplicate = ContainsLocation(matches, location);

                if (!isDuplicate)
                {
                    matches.Add(location);
                }
            }

            return matches;
        }

        private void UpdateSuggestionsWithSearchResults(List<Location> results)
        {
            _lstSuggestions.Items.Clear();

            foreach (Location loc in results)
            {
                _lstSuggestions.Items.Add(new DropdownItem { IsHeader = false, Location = loc });
            }

            ShowSuggestionsPanel();
        }

        private void PopulateSuggestions()
        {
            _lstSuggestions.Items.Clear();

            List<Location> recentLocations = GetRecentLocations(_currentUserIdentifier);
            if (recentLocations.Count > 0)
            {
                foreach (Location location in recentLocations)
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
                if (item.IsHeader) return;

                SelectedLocation = item.Location;

                // Add to recent locations
                AddToRecentLocations(SelectedLocation);

                // Reset search state
                _lastSearchQuery = string.Empty;

                // Hide suggestions panel and show selected location
                HideSuggestionsPanel();

                // Trigger selection event
                OnSelectedLocationChanged();
            }
        }

        private string FormatLocationForDisplay(Location location)
        {
            if (location == null) return string.Empty;

            if (location.Address == null)
                return $"{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";

            Address addr = location.Address;

            // Hiển thị Name + Street + District để người dùng nhận ra địa điểm
            List<string> parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(addr.Name) && addr.Name != "Unknown")
                parts.Add(addr.Name);
            if (!string.IsNullOrWhiteSpace(addr.Street))   parts.Add(addr.Street);
            if (!string.IsNullOrWhiteSpace(addr.District)) parts.Add(addr.District);
            if (!string.IsNullOrWhiteSpace(addr.City))     parts.Add(addr.City);

            return parts.Count > 0
                ? string.Join(", ", parts)
                : $"{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
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

            List<Location> locations;
            if (_recentByUser.TryGetValue(userIdentifier, out locations))
                return locations;

            return new List<Location>();
        }

        private void AddToRecentLocations(Location location)
        {
            if (string.IsNullOrWhiteSpace(_currentUserIdentifier) || location == null) return;

            List<Location> list;
            if (!_recentByUser.TryGetValue(_currentUserIdentifier, out list))
            {
                list = new List<Location>();
                _recentByUser[_currentUserIdentifier] = list;
            }

            // Remove duplicates by coordinates
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Location l = list[i];
                if (Math.Abs(l.Coordinate.Latitude - location.Coordinate.Latitude) < CoordinateTolerance &&
                    Math.Abs(l.Coordinate.Longitude - location.Coordinate.Longitude) < CoordinateTolerance)
                {
                    list.RemoveAt(i);
                }
            }

            list.Insert(0, location);

            if (list.Count > MaxRecentLocations)
                list.RemoveAt(list.Count - 1);
        }

        /// <summary>
        /// Set location programmatically (e.g., from map click)
        /// </summary>
        public void SetSelectedLocation(Location location)
        {
            SelectedLocation = location;
            _pnlSuggestions.Visible = false;
        }

        // Event for selection changed
        public event EventHandler<LocationSelectedEventArgs> LocationSelected;

        protected virtual void OnSelectedLocationChanged()
        {
            LocationSelected?.Invoke(this, new LocationSelectedEventArgs(SelectedLocation));
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
                    Address addr = Location.Address;
                    if (addr != null)
                    {
                        // Hiển thị Name nếu có, kèm Street + District
                        string name = !string.IsNullOrWhiteSpace(addr.Name) && addr.Name != "Unknown"
                            ? addr.Name + " - "
                            : string.Empty;
                        return $"{name}{addr.Street}, {addr.District}";
                    }
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
    }
}