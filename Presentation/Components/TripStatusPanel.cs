﻿using System.Windows.Forms;

namespace Presentation.Components
{
    public partial class TripStatusPanel : BaseUserControl
    {
        public TripStatusPanel()
        {
            InitializeComponent();
        }

        public void UpdateTripStatus(string status)
        {
            switch (status)
            {
                case "Requested":
                    _statusLabel.Text = "Đã yêu cầu";
                    _progressBar.Value = 20;
                    _stepLabel.Text = "Đang tìm tài xế...";
                    break;
                case "Matched":
                    _statusLabel.Text = "Đã ghép đôi";
                    _progressBar.Value = 40;
                    _stepLabel.Text = "Tài xế đang đến...";
                    break;
                case "Arrived":
                    _statusLabel.Text = "Tài xế đã đến";
                    _progressBar.Value = 60;
                    _stepLabel.Text = "Sẵn sàng khởi hành";
                    break;
                case "Started":
                    _statusLabel.Text = "Đang chạy";
                    _progressBar.Value = 80;
                    _stepLabel.Text = "Đến điểm đích...";
                    break;
                case "Completed":
                    _statusLabel.Text = "Hoàn thành";
                    _progressBar.Value = 100;
                    _stepLabel.Text = "Cảm ơn đã sử dụng";
                    break;
                case "Cancelled":
                    _statusLabel.Text = "Đã hủy";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Chuyến đi đã hủy";
                    break;
                default:
                    _statusLabel.Text = "Không xác định";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Trạng thái không rõ";
                    break;
            }
        }
    }
}
