using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Chi tiet mot chuyen di.
    /// Dung trong Tab (Admin) hoac Modal (cac vai tro con lai).
    /// </summary>
    public partial class UcTripDetail : BaseUserControl
    {
        private Trip _trip;

        public UcTripDetail(Trip trip)
        {
            _trip = trip ?? throw new ArgumentNullException(nameof(trip));
            InitializeComponent();
            PopulateInfo();
        }

        private void PopulateInfo()
        {
            if (_trip == null) return;

            lblTripId.Text = $"Chuyen: {_trip.Id.ToString().Substring(0, 8).ToUpper()}";
            lblStatus.Text = $"Trang thai: {_trip.Status}";
            lblPickup.Text = $"Diem don: {FormatLocation(_trip.TripRoute.Pickup)}";
            lblDestination.Text = $"Diem den: {FormatLocation(_trip.TripRoute.Destination)}";
            lblVehicleType.Text = $"Loai xe: {_trip.TripVehicleType}";
            lblFare.Text = $"Cuoc phi: {_trip.TripFare?.TotalAmount.Amount.ToString("N0") + " VND" ?? "N/A"}";
            lblDriverName.Text = $"Tai xe: {(_trip.DriverId != Guid.Empty ? _trip.DriverId.ToString().Substring(0, 8) : "Chua co")}";
            lblPassengerName.Text = $"Hanh khach: {_trip.PassengerId.ToString().Substring(0, 8)}";
            lblRequestTime.Text = $"Thoi gian: {_trip.RequestAt:dd/MM/yyyy HH:mm}";
        }

        private string FormatLocation(Location loc)
        {
            if (loc == null) return "N/A";
            return $"{loc.Address?.Street}, {loc.Address?.District}, {loc.Address?.City}";
        }
    }
}
