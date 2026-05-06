using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using Presentation.Base;
using System;
using System.Drawing;
using System.Windows.Forms;
// Presentation/Components/UcTripCard.cs

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiển thị thông tin chuyến đi với style nhất quán.
    /// Dùng với FlowLayoutPanel để hiển thị danh sách chuyến.
    /// </summary>
    public partial class UcTripCard : BaseUserControl
    {
        /// <summary>
        /// Sự kiện khi người dùng click vào trip card
        /// </summary>
        public event Action<UcTripCard> Clicked;


        /// <summary>
        /// Chuyến đi được hiển thị
        /// </summary>
        public Trip DisplayedTrip { get; private set; }

        public UcTripCard()
        {
            InitializeComponent();

            // Enable click events
            this.Cursor = Cursors.Hand;
            this.Click += OnCardClick;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += OnCardClick;
                ctrl.Cursor = Cursors.Hand;
            }

            // Hover effect
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

        public void SetTrip(Trip trip)
        {
            DisplayedTrip = trip;
            _lblStatus.Text = trip.Status;
            _statusIndicator.BackColor = GetStatusColor(trip.Status);
            _lblRoute.Text = $"{trip.TripRoute?.Pickup?.Address?.Street ?? "---"} -> {trip.TripRoute?.Destination?.Address?.Street ?? "---"}";
            _lblInfo.Text = $"{trip.TripVehicleType} - {trip.TripFare?.TotalAmount.Amount:N0} đ";
            _lblTime.Text = trip.RequestAt.ToString("HH:mm dd/MM");
        }

        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "Requested":
                case "Searching": return Color.Orange;
                case "Matched":
                case "Arrived": return Color.Blue;
                case "Started": return Color.Green;
                case "Completed": return Color.Gray;
                case "Cancelled":
                case "Timeout": return Color.Red;
                default: return Color.Black;
            }
        }

        private void OnCardClick(object sender, EventArgs e)
        {
            Clicked?.Invoke(this);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
