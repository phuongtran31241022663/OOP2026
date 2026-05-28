using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucTripStatus : UserControl
    {
        private TripStatus _currentStatus = TripStatus.Pending;

        public event EventHandler CancelClicked;
        public event EventHandler RateClicked;
        public event EventHandler RetryClicked;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TripStatus CurrentStatus
        {
            get => _currentStatus;
            set
            {
                _currentStatus = value;
                UpdateStatusDisplay();
            }
        }

        public ucTripStatus()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            UpdateStatusDisplay();
        }

        // ─────────────────────────────────────────────────────
        //  BUTTON EVENTS — được wire trong Designer
        // ─────────────────────────────────────────────────────

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            RateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            RetryClicked?.Invoke(this, EventArgs.Empty);
        }

        // ─────────────────────────────────────────────────────
        //  STATE MACHINE → UI
        // ─────────────────────────────────────────────────────

        private void UpdateStatusDisplay()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateStatusDisplay));
                return;
            }

            // Đặt lại trạng thái nút về ẩn trước
            btnCancel.Visible = false;
            btnRate.Visible = false;
            btnRetry.Visible = false;

            switch (_currentStatus)
            {
                case TripStatus.Pending:
                case TripStatus.Searching:
                    SetIcon("🔍", Colors.LightBlue, Colors.Adm);
                    lblTitle.Text = "Đang tìm tài xế...";
                    lblDescription.Text = "Hệ thống đang kết nối tài xế gần bạn";
                    btnCancel.Visible = true; // Cho phép hủy khi đang tìm
                    break;

                case TripStatus.Matched:
                    SetIcon("🤝", Colors.LightGreen, Colors.Green);
                    lblTitle.Text = "Đã ghép tài xế";
                    lblDescription.Text = "Tài xế đang đến điểm đón";
                    btnCancel.Visible = true; // Vẫn cho phép hủy khi tài xế chưa đến
                    break;

                case TripStatus.Arrived:
                    SetIcon("📍", Colors.LightGreen, Colors.Green);
                    lblTitle.Text = "Tài xế đã đến";
                    lblDescription.Text = "Tài xế đang chờ bạn tại điểm hẹn";
                    break;

                case TripStatus.Started:
                    SetIcon("🚗", Colors.LightGreen, Colors.Green);
                    lblTitle.Text = "Đang di chuyển";
                    lblDescription.Text = "Bạn đang trên đường đến điểm đến";
                    break;

                case TripStatus.DropOff:
                    SetIcon("🏁", Colors.LightGreen, Colors.Green);
                    lblTitle.Text = "Đã đến điểm đến";
                    lblDescription.Text = "Cảm ơn bạn đã sử dụng dịch vụ";
                    break;

                case TripStatus.Completed:
                    SetIcon("✅", Colors.LightGreen, Colors.Green);
                    lblTitle.Text = "Chuyến đi hoàn thành";
                    lblDescription.Text = "Vui lòng để lại đánh giá cho tài xế";
                    btnRate.Visible = true; // Chỉ hiện nút đánh giá sau khi hoàn thành
                    break;

                case TripStatus.Cancelled:
                    SetIcon("❌", Colors.LightRed, Colors.Red);
                    lblTitle.Text = "Chuyến đi đã hủy";
                    lblDescription.Text = "Cuốc xe đã được xác nhận hủy bỏ";
                    btnRetry.Visible = true;
                    break;

                case TripStatus.Timeout:
                    SetIcon("⏰", Colors.LightRed, Colors.Red);
                    lblTitle.Text = "Hết thời gian chờ";
                    lblDescription.Text = "Không tìm được tài xế, vui lòng thử lại";
                    btnRetry.Visible = true;
                    break;

                default:
                    SetIcon("⏳", Colors.LightBlue, Colors.Adm);
                    lblTitle.Text = "Đang xử lý";
                    lblDescription.Text = "Vui lòng đợi trong giây lát";
                    break;
            }
        }

        private void SetIcon(string icon, Color bgColor, Color fgColor)
        {
            lblIcon.Text = icon;
            pnlStatusIcon.BackColor = bgColor;
            lblIcon.ForeColor = fgColor;
        }
    }
}