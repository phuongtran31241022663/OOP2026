using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucTripCard : UserControl
    {
        public event EventHandler? TripAccepted;
        public event EventHandler? TripRejected;

        // Khởi tạo sẵn Font dùng chung một lần duy nhất để tránh leak RAM trong sự kiện Paint
        private static readonly Font EmojiFont = new Font("Segoe UI Emoji", 8F);
        private static readonly Font IconFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        private Trip? _trip; // Cho phép null ban đầu để tránh crash

        public Trip Trip
        {
            get => _trip!;
            set
            {
                _trip = value;
                if (_trip != null)
                {
                    bool isCar = _trip.TripVehicleType == VehicleType.Car;
                    lblServiceType.Text = isCar ? "      Ô tô" : "      Xe máy";
                    lblNetEarnings.Text = $"{_trip.TripFare.DriverIncome:N0}đ";
                    lblPickup.Text = "      " + (_trip.TripRoute.Pickup.Addr?.Name ?? "Điểm đón");
                    lblDropoff.Text = "      " + (_trip.TripRoute.Dropoff.Addr?.Name ?? "Điểm đến");
                    lblTripParams.Text = $"{_trip.TripRoute.Distance:F1} km  •  {Math.Ceiling(_trip.TripRoute.Duration.TotalMinutes)} phút  •  Cước: {_trip.TripFare.TotalAmount:N0}đ";
                }
                this.Invalidate(true); // Yêu cầu vẽ lại toàn bộ giao diện card và con của nó
            }
        }

        public ucTripCard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // ========== PHÁT ĐI SỰ KIỆN KHI NGƯỜI DÙNG CLICK ==========

        private void BtnReject_Click(object sender, EventArgs e)
        {
            TripRejected?.Invoke(this, EventArgs.Empty);
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            TripAccepted?.Invoke(this, EventArgs.Empty);
        }
    }
}
