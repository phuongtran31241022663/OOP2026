using Presentation.Base;
using System.Windows.Forms;

namespace Presentation.Components
{
    public partial class UcTripStatus : BaseUserControl
    {
        public UcTripStatus()
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
                    _statusLabel.Text = "Kh\u00f4ng x\u00e1c \u0111\u1ecbnh";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Tr\u1ea1ng th\u00e1i kh\u00f4ng r\u00f5";
                    break;
            }
        }
    }
}