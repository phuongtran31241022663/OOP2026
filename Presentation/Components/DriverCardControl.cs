using System;
using System.Drawing;
using System.Windows.Forms;
using Domain.Enums;
using Domain.Users.Drivers;

namespace Presentation.Components
{
    /// <summary>
    /// UserControl hiển thị thông tin tài xế dưới dạng card.
    /// Sử dụng trong danh sách tài xế có sẵn hoặc tìm kiếm.
    /// </summary>
    public partial class DriverCardControl : UserControl
    {


        /// <summary>
        /// Sự kiện khi người dùng click vào driver card
        /// </summary>
        public event Action<DriverCardControl> Clicked;

        /// <summary>
        /// Tài xế được hiển thị
        /// </summary>
        public Driver Driver { get; private set; }

        public DriverCardControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set thông tin tài xế để hiển thị
        /// </summary>
        public void SetDriver(Driver driver, double distanceKm = 0)
        {
            Driver = driver;

            if (driver == null) return;

            _lblName.Text = driver.Name;
            _lblPhone.Text = driver.Phone;
            _lblReview.Text = $"★ {driver.Review:F1}";

            // Status
            UpdateStatus(driver.Status);

            // Vehicle with null check
            if (driver.Vehicle != null)
            {
                _lblVehicle.Text = $"{driver.Vehicle.GetVehicleType()} • {driver.Vehicle.PlateNumber}";
            }
            else
            {
                _lblVehicle.Text = "No vehicle info";
            }

            // Distance
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

        private void UpdateStatus(DriverStatus status)
        {
            switch (status)
            {
                case DriverStatus.Available:
                    _lblStatus.Text = "Có sẵn";
                    _lblStatus.ForeColor = Color.Green;
                    _statusIndicator.BackColor = Color.Green;
                    break;
                case DriverStatus.OnTrip:
                    _lblStatus.Text = "Đang chạy";
                    _lblStatus.ForeColor = Color.Blue;
                    _statusIndicator.BackColor = Color.Blue;
                    break;
                case DriverStatus.Offline:
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
            this.BackColor = Color.LightBlue;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
