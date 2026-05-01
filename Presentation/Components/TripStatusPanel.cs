using System.Windows.Forms;

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
                    _statusLabel.Text = "Ðã yêu c?u";
                    _progressBar.Value = 20;
                    _stepLabel.Text = "Ðang tìm tài x?...";
                    break;
                case "Matched":
                    _statusLabel.Text = "Ðã ghép dôi";
                    _progressBar.Value = 40;
                    _stepLabel.Text = "Tài x? dang d?n...";
                    break;
                case "Arrived":
                    _statusLabel.Text = "Tài x? dã d?n";
                    _progressBar.Value = 60;
                    _stepLabel.Text = "S?n sàng kh?i hành";
                    break;
                case "Started":
                    _statusLabel.Text = "Ðang ch?y";
                    _progressBar.Value = 80;
                    _stepLabel.Text = "Ð?n di?m dích...";
                    break;
                case "Completed":
                    _statusLabel.Text = "Hoàn thành";
                    _progressBar.Value = 100;
                    _stepLabel.Text = "C?m on dã s? d?ng";
                    break;
                case "Cancelled":
                    _statusLabel.Text = "Ðã h?y";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Chuy?n di dã h?y";
                    break;
                default:
                    _statusLabel.Text = "Không xác d?nh";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Tr?ng thái không rõ";
                    break;
            }
        }
    }
}
