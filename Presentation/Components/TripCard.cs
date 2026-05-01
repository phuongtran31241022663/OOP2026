using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
// Presentation/Components/TripCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiá»ƒn thá»‹ thÃ´ng tin chuyáº¿n Ä‘i vá»›i style nháº¥t quÃ¡n.
    /// DÃ¹ng vá»›i FlowLayoutPanel Ä‘á»ƒ hiá»ƒn thá»‹ danh sÃ¡ch chuyáº¿n.
    /// </summary>
    public partial class TripCard : BaseUserControl
    {
        /// <summary>
        /// Sá»± kiá»‡n khi ngÆ°á»i dÃ¹ng click vÃ o trip card
        /// </summary>
        public event Action<TripCard> Clicked;

        /// <summary>
        /// Chuyến đi được hiển thị
        /// </summary>
        public Trip DisplayedTrip { get; private set; }

        public TripCard()
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
            if (Clicked != null) Clicked(this);
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
