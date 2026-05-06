using Domain.Entities;
using Domain.ValueObjects;
using Presentation.Base;
using System;
using System.Windows.Forms;

namespace Presentation.Components
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

            lblTripId.Text = $"Chuyến: {_trip.Id.ToString().Substring(0, 8).ToUpper()}";
            lblStatus.Text = $"Trạng thái: {_trip.Status}";
            lblPickup.Text = $"Điểm đón: {FormatLocation(_trip.TripRoute.Pickup)}";
            lblDestination.Text = $"Điểm đến: {FormatLocation(_trip.TripRoute.Destination)}";
            lblVehicleType.Text = $"Loại xe: {_trip.TripVehicleType}";
            lblFare.Text = $"Cước phí: {(_trip.TripFare?.TotalAmount.Amount.ToString("N0") ?? "Chưa có") + "đ"}";
            lblDriverName.Text = $"Tài xế: {(_trip.DriverId.HasValue && _trip.DriverId.Value != Guid.Empty ? _trip.DriverId.Value.ToString().Substring(0, 8) : "Chưa có")}";
            lblPassengerName.Text = $"Hành khách: {_trip.PassengerId.ToString().Substring(0, 8)}";
            lblRequestTime.Text = $"Thời gian: {_trip.RequestAt:dd/MM/yyyy HH:mm}";
        }

        private string FormatLocation(Location loc)
        {
            if (loc == null) return "N/A";
            return $"{loc.Address?.Street}, {loc.Address?.District}, {loc.Address?.City}";
        }
    }
}
