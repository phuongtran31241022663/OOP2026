using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;

namespace OOP2026
{
    public partial class ucLocationPicker : UserControl
    {
        private CancellationTokenSource _cts;
        private IMapSvc _mapService;
        private bool _isUpdatingProgrammatically;

        public event EventHandler<LocationSelectedEventArgs> AddressSelected;
        public new event EventHandler TextChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedAddress
        {
            get => txtAddress.Text;
            set
            {
                _isUpdatingProgrammatically = true;
                txtAddress.Text = value;
                _isUpdatingProgrammatically = false;
            }
        }

        public PointLatLng? SelectedLocation { get; private set; }

        public void SetMapService(IMapSvc mapService)
        {
            _mapService = mapService;
        }

        public ucLocationPicker()
        {
            InitializeComponent();
            this.Disposed += UcLocationPicker_Disposed;
        }

        private void UcLocationPicker_Disposed(object sender, EventArgs e)
        {
            searchTimer.Stop();
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void TxtAddress_TextChanged(object sender, EventArgs e)
        {
            if (!_isUpdatingProgrammatically)
                TextChanged?.Invoke(this, EventArgs.Empty);

            searchTimer.Stop();

            // Trigger search when user types at least 1 character
            if (txtAddress.Text.Trim().Length >= 1 && txtAddress.Focused)
            {
                searchTimer.Start();
            }
            else
            {
                HideSuggestions();
            }
        }

        private async void LstSuggestions_Click(object sender, EventArgs e)
        {
            if (lstSuggestions.SelectedItem == null) return;

            string address = lstSuggestions.SelectedItem.ToString();
            if (address == "No locations found") return;

            SelectedAddress = address;
            HideSuggestions();

            // Convert address string to GPS coordinates
            PointLatLng? point = await GeocodeAddress(address);
            SelectedLocation = point;
            AddressSelected?.Invoke(this, new LocationSelectedEventArgs(address, point));
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            // Khá»­ luá»“ng cÅ© Ä‘ang cháº¡y Ä‘á»ƒ trÃ¡nh hiá»‡n tÆ°á»£ng Race Condition (Dá»¯ liá»‡u cÅ© Ä‘Ã¨ dá»¯ liá»‡u má»›i)
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _cts = new CancellationTokenSource();

            try
            {
                string query = txtAddress.Text.Trim();
                if (string.IsNullOrWhiteSpace(query) || query.Length < 1 || _mapService == null) return;

                var locations = await _mapService.SearchAsync(query);

                // Check if search was cancelled
                if (_cts.Token.IsCancellationRequested) return;
                if (locations == null) { HideSuggestions(); return; }

                var items = new List<string>();
                int maxCount = locations.Count < 5 ? locations.Count : 5; // Take top 5 results

                for (int i = 0; i < maxCount; i++)
                {
                    var loc = locations[i];
                    if (loc?.Addr != null)
                    {
                        items.Add(loc.Addr.ToString());
                    }
                }

                lstSuggestions.Items.Clear();
                if (items.Count > 0)
                {
                    lstSuggestions.Items.AddRange(items.ToArray());
                    ShowSuggestions();
                }
                else
                {
                    lstSuggestions.Items.Add("No locations found");
                    ShowSuggestions();
                }
            }
            catch
            {
                HideSuggestions();
            }
        }

        private void ShowSuggestions()
        {
            lstSuggestions.Visible = true;
            // Expand control height to show suggestions
            this.Height = txtAddress.Height + lstSuggestions.Height + 4;
            lstSuggestions.BringToFront();
        }

        public void HideSuggestions()
        {
            lstSuggestions.Visible = false;
            // Reset control height to textbox height
            this.Height = txtAddress.Height;
        }

        private async Task<PointLatLng?> GeocodeAddress(string address)
        {
            try
            {
                if (_mapService == null) return null;
                var locations = await _mapService.SearchAsync(address);

                if (locations != null && locations.Count > 0)
                {
                    var first = locations[0];
                    if (first != null)
                    {
                        return new PointLatLng(first.Coord.Latitude, first.Coord.Longitude);
                    }
                }
                return null;
            }
            catch { return null; }
        }

        public async Task SetLocationFromCoordinates(PointLatLng point, IMapSvc mapService = null)
        {
            SelectedLocation = point;
            string address = await ReverseGeocode(point, mapService);
            if (!string.IsNullOrEmpty(address))
            {
                _isUpdatingProgrammatically = true;
                txtAddress.Text = address;
                _isUpdatingProgrammatically = false;
                AddressSelected?.Invoke(this, new LocationSelectedEventArgs(address, point));
            }
        }

        private async Task<string> ReverseGeocode(PointLatLng point, IMapSvc mapService)
        {
            var service = mapService ?? _mapService;
            if (service != null)
            {
                var location = await service.GetAddressAsync(point.Lat, point.Lng);
                if (location?.Addr != null)
                    return location.Addr.ToString();
            }
            return "ÄÃ£ chá»n vá»‹ trÃ­ trÃªn báº£n Ä‘á»“";
        }

        public void Clear()
        {
            SelectedAddress = "";
            SelectedLocation = null;
            HideSuggestions();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Support Enter key to select the current or first suggestion
            if (keyData == Keys.Enter && lstSuggestions.Visible && lstSuggestions.Items.Count > 0)
            {
                if (lstSuggestions.SelectedIndex < 0)
                    lstSuggestions.SelectedIndex = 0;
                
                LstSuggestions_Click(lstSuggestions, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class LocationSelectedEventArgs : EventArgs
    {
        public string Addr { get; }
        public PointLatLng? Loc { get; }

        public LocationSelectedEventArgs(string address, PointLatLng? location)
        {
            Addr = address;
            Loc = location;
        }
    }
}

