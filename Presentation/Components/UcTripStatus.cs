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
                    _statusLabel.Text = "лс yЖu c?u";
                    _progressBar.Value = 20;
                    _stepLabel.Text = "лang tВm tЯi x?...";
                    break;
                case "Matched":
                    _statusLabel.Text = "лс ghжp dЗi";
                    _progressBar.Value = 40;
                    _stepLabel.Text = "TЯi x? dang d?n...";
                    break;
                case "Arrived":
                    _statusLabel.Text = "TЯi x? dс d?n";
                    _progressBar.Value = 60;
                    _stepLabel.Text = "S?n sЯng kh?i hЯnh";
                    break;
                case "Started":
                    _statusLabel.Text = "лang ch?y";
                    _progressBar.Value = 80;
                    _stepLabel.Text = "л?n di?m dьch...";
                    break;
                case "Completed":
                    _statusLabel.Text = "HoЯn thЯnh";
                    _progressBar.Value = 100;
                    _stepLabel.Text = "C?m on dс s? d?ng";
                    break;
                case "Cancelled":
                    _statusLabel.Text = "лс h?y";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Chuy?n di dс h?y";
                    break;
                default:
                    _statusLabel.Text = "KhЗng xрc d?nh";
                    _progressBar.Value = 0;
                    _stepLabel.Text = "Tr?ng thрi khЗng rш";
                    break;
            }
        }
    }
}
