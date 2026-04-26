using Application.Interfaces;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.ValueObjects;
using Presentation.Components;
using Presentation.Shells;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    public partial class BookTripForm : BaseForm
    {
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IFareService _fareService;
        private readonly HttpClient _httpClient;
        private readonly PassengerShell _parentShell;

        private VehicleType _selectedVehicle;
        private Location _pickup;
        private Location _destination;
        private bool _isPickupSlotActive;

        public BookTripForm(
            ITripService tripService,
            IUserService userService,
            IMapService mapService,
            IFareService fareService,
            HttpClient httpClient,
            PassengerShell parentShell)
        {
            _tripService = tripService;
            _userService = userService;
            _mapService = mapService;
            _fareService = fareService;
            _httpClient = httpClient;
            _parentShell = parentShell;
            _selectedVehicle = VehicleType.Motorbike;

            InitializeComponent();
        }

        private void BookTripForm_Load(object sender, EventArgs e)
        {
            _statusLabel.Text = "San sang";
        }

        private void OnMapClicked(MapControl sender, Location location)
        {
            if (location == null)
            {
                return;
            }

            if (_isPickupSlotActive || _pickup == null)
            {
                _pickup = location;
                _pickupPicker.SetPickup(location);
                _isPickupSlotActive = false;
            }
            else
            {
                _destination = location;
                _destinationPicker.SetDestination(location);
            }
        }

        private void OnPickupSelected(LocationPickerControl sender, Location location)
        {
            _pickup = location;
            _pickupPicker.SetPickup(location);
            _isPickupSlotActive = false;
        }

        private void OnDestinationLocationSelected(LocationPickerControl sender, Location location)
        {
            _destination = location;
            _destinationPicker.SetDestination(location);
        }

        private void OnVehicleChanged(object sender, EventArgs e)
        {
            _selectedVehicle = _vehicleCombo.SelectedIndex == 1
                ? VehicleType.Car
                : VehicleType.Motorbike;
        }

        private void OnRequestClicked(object sender, EventArgs e)
        {
            if (_pickup == null || _destination == null)
            {
                MessageBox.Show(
                    "Vui long chon diem don va diem den.",
                    "Thieu thong tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            _statusLabel.Text = "Chuc nang dat xe tam thoi duoc vo hieu hoa de on dinh build.";
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            _statusLabel.Text = "Da huy thao tac.";
        }

        private void OnDrawSuggestionItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();
            string text = _suggestions.Items[e.Index] == null
                ? string.Empty
                : _suggestions.Items[e.Index].ToString();
            TextRenderer.DrawText(
                e.Graphics,
                text,
                e.Font,
                e.Bounds,
                SystemColors.ControlText,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            e.DrawFocusRectangle();
        }

        private void OnSuggestionSelected(object sender, EventArgs e)
        {
            _suggestions.Visible = false;
        }

        public void OnTripFinished()
        {
            _statusLabel.Text = "Khong co chuyen dang hoat dong.";
        }

        public void UpdateDriverInfo(Driver driver)
        {
            if (driver == null)
            {
                return;
            }

            _driverNameLabel.Text = "Tai xe: " + driver.Name;
            _driverPhoneLabel.Text = "SDT: " + driver.Phone;
            _driverReviewLabel.Text = "Danh gia: " + driver.AverageRating.ToString("F1");
            _driverCard.Visible = true;
        }
    }
}
