using System;
using System.Drawing;
using Domain.Entities.Users;
// file này bị lỗi unicode nhiều
// cái này là thông tin tài xế trên giao diện à, với cho passenger thấy à??

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hi?n th? thông tin tài x? du?i d?ng card.
    /// S? d?ng trong danh sách tài x? có s?n ho?c tìm ki?m.
    /// </summary>
    public partial class UcDriverCard : BaseUserControl
    {
        /// <summary>
        /// S? ki?n khi ngu?i dùng click vào driver card
        /// </summary>
        public event Action<UcDriverCard> Clicked;

        /// <summary>
        /// Tài x? du?c hi?n th?
        /// </summary>
        public Driver Driver { get; private set; }

        public UcDriverCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set thông tin tài x? d? hi?n th?
        /// </summary>
        public void SetDriver(Driver driver, string vehicleDisplayText = null, double distanceKm = 0)
        {
            Driver = driver;
            if (driver == null) return;

            _lblName.Text = driver.Name;
            _lblPhone.Text = driver.Phone;
            _lblReview.Text = $"? {driver.AverageRating:F1}";

            UpdateStatus(driver.Status);

            if (!string.IsNullOrEmpty(vehicleDisplayText))
            {
                _lblVehicle.Text = vehicleDisplayText;
                _lblVehicle.Visible = true;
            }
            else
            {
                _lblVehicle.Visible = false;
                _lblVehicle.Text = "";
            }

            if (distanceKm > 0)
            {
                _lblDistance.Text = $"{distanceKm:F1} km";
                _lblDistance.Visible = true;
            }
            else
            {
                _lblDistance.Visible = false;
            }
        }

        private void UpdateStatus(string status)
        {
            switch (status)
            {
                case "Available":
                    _lblStatus.Text = "Có s?n";
                    _lblStatus.ForeColor = Color.Green;
                    _statusIndicator.BackColor = Color.Green;
                    break;
                case "OnTrip":
                    _lblStatus.Text = "Ðang ch?y";
                    _lblStatus.ForeColor = Color.Blue;
                    _statusIndicator.BackColor = Color.Blue;
                    break;
                case "Offline":
                    _lblStatus.Text = "Offline";
                    _lblStatus.ForeColor = Color.Gray;
                    _statusIndicator.BackColor = Color.Gray;
                    break;
                default:
                    _lblStatus.Text = "Unknown";
                    _lblStatus.ForeColor = Color.Orange;
                    _statusIndicator.BackColor = Color.Orange;
                    break;
            }
        }

        private void OnCardClick(object sender, EventArgs e)
        {
            if (Clicked != null) Clicked(this);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.LightBlue;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
          BackColor = Color.White;
        }
    }
}


