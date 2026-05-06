using System;
using System.Drawing;
using Domain.Entities.Users;
using Presentation.Base;
using Presentation.Constants;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiển thị thông tin tài xế dưới dạng card.
    /// Sử dụng trong danh sách tài xế có sẵn hoặc tìm kiếm.
    /// </summary>
    public partial class UcDriverCard : BaseUserControl
    {
        /// <summary>
        /// Sự kiện khi người dùng click vào driver card
        /// </summary>
        public event Action<UcDriverCard> Clicked;

        /// <summary>
        /// Tài xế được hiển thị
        /// </summary>
        public Driver Driver { get; private set; }

        public UcDriverCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set thông tin tài xế để hiển thị
        /// </summary>
        public void SetDriver(Driver driver, string vehicleDisplayText = null, double distanceKm = 0)
        {
            Driver = driver;
            if (driver == null) return;

            _lblName.Text = driver.Name;
            _lblPhone.Text = driver.Phone;
            _lblReview.Text = $"★ {driver.AverageRating:F1}";

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
                    _lblStatus.Text = "Có sẵn";
                    _lblStatus.ForeColor = UiConstants.Colors.Success;
                    _statusIndicator.BackColor = UiConstants.Colors.Success;
                    break;
                case "OnTrip":
                    _lblStatus.Text = "Đang chạy";
                    _lblStatus.ForeColor = UiConstants.Colors.Info;
                    _statusIndicator.BackColor = UiConstants.Colors.Info;
                    break;
                case "Offline":
                    _lblStatus.Text = "Offline";
                    _lblStatus.ForeColor = UiConstants.Colors.TextMuted;
                    _statusIndicator.BackColor = UiConstants.Colors.TextMuted;
                    break;
                default:
                    _lblStatus.Text = "Unknown";
                    _lblStatus.ForeColor = UiConstants.Colors.Warning;
                    _statusIndicator.BackColor = UiConstants.Colors.Warning;
                    break;
            }
        }

        private void OnCardClick(object sender, EventArgs e)
        {
            if (Clicked != null) Clicked(this);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = UiConstants.Colors.SurfaceLight;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
          BackColor = UiConstants.Colors.SurfaceWhite;
        }
    }
}


