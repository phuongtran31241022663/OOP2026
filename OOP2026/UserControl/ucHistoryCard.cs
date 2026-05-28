using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucHistoryCard : UserControl
    {
        private Trip _trip;
        private IRevSvc _reviewService;
        private Guid _driverId;
        private Guid _passengerId;
        private bool _canReview;

        // Lưu trữ đối tượng review hiện tại để kiểm soát vòng đời dọn dẹp RAM
        private ucReview _activeReviewControl;

        public ucHistoryCard()
        {
            InitializeComponent();
            btnReview.Visible = false;
            pnlReview.Visible = false;

            // Đăng ký sự kiện hủy của hệ thống để dự phòng về rò rỉ bộ nhớ
            this.Disposed += UcHistoryCard_Disposed;
        }

        public void Bind(Trip trip, IRevSvc reviewService, Guid driverId, Guid passengerId, bool canReview)
        {
            _trip = trip;
            _reviewService = reviewService;
            _driverId = driverId;
            _passengerId = passengerId;
            _canReview = canReview;

            RenderTrip();

            btnReview.Visible = _canReview && _reviewService != null;

            // Dọn dẹp an toàn Panel review trước khi nạp dữ liệu mới
            ClearAndDisposeReviewPanel();
            pnlReview.Visible = false;
        }

        /// <summary>
        /// ĐỒNG BỘ KIẾN TRÚC: Đối khớp chuẩn cấu trúc Domain (TripRoute, TripFare) để tránh lỗi biên dịch
        /// </summary>
        private void RenderTrip()
        {
            if (_trip == null) return;

            lblDate.Text = _trip.CreatedAt.ToString("dd/MM/yyyy HH:mm");

            // Khớp nối chính xác với cấu trúc Value Object của thực thể Trip
            string pickup = "Điểm đón";
            string dropoff = "Điểm trả";

            if (_trip.TripRoute != null)
            {
                pickup = GetShortAddress(_trip.TripRoute.Pickup?.Addr);
                dropoff = GetShortAddress(_trip.TripRoute.Dropoff?.Addr);
            }
            lblRoute.Text = $"{pickup} • {dropoff}";

            lblStatus.Text = GetStatusText(_trip.Status);

            // Khớp nối chính xác tổng tính toán giá tiền TripFare
            decimal amount = _trip.TripFare != null ? _trip.TripFare.TotalAmount : 0m;
            lblFare.Text = $"{amount:N0}đ";

            // Định dạng màu sắc trực quan theo trạng thái chuyến đi
            switch (_trip.Status)
            {
                case TripStatus.Completed:
                    lblFare.ForeColor = Colors.Green;
                    break;
                case TripStatus.Cancelled:
                case TripStatus.Timeout:
                    lblFare.ForeColor = Colors.Red;
                    break;
                default:
                    lblFare.ForeColor = Colors.Gray;
                    break;
            }
        }

        private void BtnReview_Click(object sender, EventArgs e)
        {
            if (_trip == null || _reviewService == null) return;

            // Khóa vẽ giao diện tạm thời để thực hiện thay đổi kích thước lớn
            this.FindForm()?.SuspendLayout();

            ClearAndDisposeReviewPanel();

            _activeReviewControl = new ucReview();
            _activeReviewControl.Dock = DockStyle.Top;
            _activeReviewControl.Initialize(_driverId, _passengerId, _trip.Id, _reviewService);

            _activeReviewControl.ReviewSubmitted += OnReviewSubmittedSuccess;

            pnlReview.Controls.Add(_activeReviewControl);
            pnlReview.Visible = true;
            _activeReviewControl.BringToFront();

            // Mở khóa và ép vẽ lại layout
            this.FindForm()?.ResumeLayout(true);
        }

        private void OnReviewSubmittedSuccess(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnReviewSubmittedSuccess(sender, e)));
                return;
            }

            this.FindForm()?.SuspendLayout();

            pnlReview.Visible = false;
            btnReview.Visible = false;

            ClearAndDisposeReviewPanel();

            this.FindForm()?.ResumeLayout(true);
        }
        private string GetShortAddress(Addr addr)
        {
            if (addr == null) return "Địa điểm";
            if (!string.IsNullOrWhiteSpace(addr.Name) && addr.Name != "Unknown")
                return addr.Name;
            if (!string.IsNullOrWhiteSpace(addr.Street))
                return addr.Street;
            if (!string.IsNullOrWhiteSpace(addr.District) || !string.IsNullOrWhiteSpace(addr.City))
                return $"{addr.District}, {addr.City}";
            return "Địa điểm";
        }

        private string GetStatusText(TripStatus status)
        {
            return status switch
            {
                TripStatus.Completed => "✅ Hoàn thành",
                TripStatus.Cancelled => "❌ Đã hủy",
                TripStatus.Timeout => "⌛ Hết hạn",
                TripStatus.Searching => "🔍 Đang tìm tài xế",
                TripStatus.Matched => "🤝 Đã nhận chuyến",
                TripStatus.Arrived => "📍 Tài xế đã đến",
                TripStatus.Started => "🚗 Đang di chuyển",
                TripStatus.DropOff => "🏁 Đã đến nơi",
                _ => status.ToString()
            };
        }

        // ========== BỘ DỌN DẸP VÙNG NHỚ GDI OBJECTS CHỦ ĐỘNG ==========

        private void ClearAndDisposeReviewPanel()
        {
            if (_activeReviewControl != null)
            {
                // Tháo gỡ sự kiện kết nối để giải phóng liên kết con trỏ
                _activeReviewControl.ReviewSubmitted -= OnReviewSubmittedSuccess;

                if (pnlReview.Controls.Contains(_activeReviewControl))
                {
                    pnlReview.Controls.Remove(_activeReviewControl);
                }

                _activeReviewControl.Dispose(); // Ép hệ điều hành tiêu hủy Handle tài nguyên đồ họa
                _activeReviewControl = null;
            }
            pnlReview.Controls.Clear();
        }

        private void UcHistoryCard_Disposed(object sender, EventArgs e)
        {
            ClearAndDisposeReviewPanel();
        }
    }
}
