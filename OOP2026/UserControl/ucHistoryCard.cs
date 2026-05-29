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

        private ucReview _activeReviewControl;

        public ucHistoryCard()
        {
            InitializeComponent();
            btnReview.Visible = false;
            pnlReview.Visible = false;
            this.Disposed += UcHistoryCard_Disposed;
        }

        public void Bind(Trip trip, IRevSvc reviewService,
                         Guid driverId, Guid passengerId, bool canReview)
        {
            _trip = trip;
            _reviewService = reviewService;
            _driverId = driverId;
            _passengerId = passengerId;
            _canReview = canReview;

            RenderTrip();
            btnReview.Visible = _canReview && _reviewService != null;

            ClearAndDisposeReviewPanel();
            pnlReview.Visible = false;
        }

        private void RenderTrip()
        {
            if (_trip == null) return;

            lblDate.Text = _trip.CreatedAt.ToString("dd/MM/yyyy HH:mm");

            string pickup = "Điểm đón";
            string dropoff = "Điểm trả";
            if (_trip.TripRoute != null)
            {
                pickup = GetShortAddress(_trip.TripRoute.Pickup?.Addr);
                dropoff = GetShortAddress(_trip.TripRoute.Dropoff?.Addr);
            }
            lblRoute.Text = pickup + " • " + dropoff;

            lblStatus.Text = GetStatusText(_trip.Status);

            decimal amount = _trip.TripFare != null ? _trip.TripFare.TotalAmount : 0m;
            lblFare.Text = amount.ToString("N0") + "đ";

            // Màu theo trạng thái
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

            this.FindForm()?.SuspendLayout();
            ClearAndDisposeReviewPanel();

            _activeReviewControl = new ucReview();
            _activeReviewControl.Dock = DockStyle.Top;
            _activeReviewControl.Initialize(_driverId, _passengerId, _trip.Id, _reviewService);
            _activeReviewControl.ReviewSubmitted += OnReviewSubmittedSuccess;

            pnlReview.Controls.Add(_activeReviewControl);
            pnlReview.Visible = true;
            _activeReviewControl.BringToFront();
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
            if (!string.IsNullOrWhiteSpace(addr.Name) && addr.Name != "Unknown") return addr.Name;
            if (!string.IsNullOrWhiteSpace(addr.Street)) return addr.Street;
            if (!string.IsNullOrWhiteSpace(addr.District) || !string.IsNullOrWhiteSpace(addr.City))
                return addr.District + ", " + addr.City;
            return "Địa điểm";
        }

        // ĐÃ SỬA: switch expression (C# 8) → switch statement (C# 7.3)
        private string GetStatusText(TripStatus status)
        {
            switch (status)
            {
                case TripStatus.Completed: return "✅ Hoàn thành";
                case TripStatus.Cancelled: return "❌ Đã hủy";
                case TripStatus.Timeout: return "⌛ Hết hạn";
                case TripStatus.Searching: return "🔍 Đang tìm tài xế";
                case TripStatus.Matched: return "🤝 Đã nhận chuyến";
                case TripStatus.Arrived: return "📍 Tài xế đã đến";
                case TripStatus.Started: return "🚗 Đang di chuyển";
                case TripStatus.DropOff: return "🏁 Đã đến nơi";
                default: return status.ToString();
            }
        }

        private void ClearAndDisposeReviewPanel()
        {
            if (_activeReviewControl != null)
            {
                _activeReviewControl.ReviewSubmitted -= OnReviewSubmittedSuccess;
                if (pnlReview.Controls.Contains(_activeReviewControl))
                    pnlReview.Controls.Remove(_activeReviewControl);
                _activeReviewControl.Dispose();
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