using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucReview : UserControl
    {
        private Guid _driverId;
        private Guid _passengerId;
        private Guid _tripId;
        private IRevSvc _reviewService;

        private int _selectedStars = 5;                // mặc định 5 sao
        private Label[] _starLabels;                  // mảng tham chiếu tới các Label sao tĩnh

        public event EventHandler ReviewSubmitted;
        public event EventHandler ReviewCancelled;

        public int SelectedStars => _selectedStars;
        public string Comment => txtComment.Text;

        public ucReview()
        {
            InitializeComponent();

            // Gán mảng sao từ Designer
            _starLabels = new[] { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };
            UpdateStarsUI();   // hiển thị 5 sao vàng ban đầu

            this.Disposed += UcReview_Disposed;
        }

        public void Initialize(Guid driverId, Guid passengerId, Guid tripId, IRevSvc reviewService)
        {
            _driverId = driverId;
            _passengerId = passengerId;
            _tripId = tripId;
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        // ──────────────────────── Star handlers ────────────────────────
        private void Star_Click(object sender, EventArgs e)
        {
            Label clickedStar = sender as Label;
            if (clickedStar == null) return;

            // Xác định chỉ số sao (1‑5)
            int starIndex = Array.IndexOf(_starLabels, clickedStar) + 1;
            if (starIndex > 0)
            {
                _selectedStars = starIndex;
                UpdateStarsUI();
            }
        }

        private void Star_MouseEnter(object sender, EventArgs e)
        {
            // Chỉ hiệu ứng hover khi chưa có sao nào được chọn
            if (_selectedStars > 0) return;

            Label hoveredStar = sender as Label;
            if (hoveredStar == null) return;

            int hoverIndex = Array.IndexOf(_starLabels, hoveredStar) + 1;
            for (int i = 0; i < _starLabels.Length; i++)
            {
                _starLabels[i].ForeColor = (i < hoverIndex)
                    ? Color.FromArgb(250, 204, 21)   // vàng
                    : Color.FromArgb(209, 213, 219); // xám
            }
        }

        private void Star_MouseLeave(object sender, EventArgs e)
        {
            // Trả về trạng thái thực tế (0 sao -> tất cả xám)
            UpdateStarsUI();
        }

        private void UpdateStarsUI()
        {
            Color gold = Color.FromArgb(250, 204, 21);
            Color gray = Color.FromArgb(209, 213, 219);

            for (int i = 0; i < _starLabels.Length; i++)
            {
                _starLabels[i].ForeColor = (i < _selectedStars) ? gold : gray;
                // Giữ nguyên text "★" không cần thay đổi
            }
        }

        // ──────────────────────── Button handlers ──────────────────────
        private void btnSkip_Click(object sender, EventArgs e)
        {
            ReviewCancelled?.Invoke(this, EventArgs.Empty);
            // Ẩn control (tuỳ ngữ cảnh, có thể xoá khỏi parent)
            this.Parent?.Controls.Remove(this);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_reviewService == null)
            {
                MessageBox.Show("Dịch vụ đánh giá chưa được liên kết.", "Lỗi kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string commentText = txtComment.Text.Trim();

            if (string.IsNullOrWhiteSpace(commentText))
            {
                var result = MessageBox.Show(
                    "Bạn có muốn gửi đánh giá mà không kèm theo lời nhận xét không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
            }

            btnSubmit.Enabled = false;
            string originalText = btnSubmit.Text;
            btnSubmit.Text = "⌛ Đang gửi đánh giá...";

            try
            {
                await _reviewService.CreateReviewAsync(_driverId, _passengerId, _tripId,
                                                       _selectedStars, commentText);

                MessageBox.Show(
                    "Cảm ơn bạn đã đóng góp ý kiến giúp cải thiện dịch vụ!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ReviewSubmitted?.Invoke(this, EventArgs.Empty);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể gửi đánh giá vào lúc này: {ex.Message}",
                    "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmit.Text = originalText;
                btnSubmit.Enabled = true;
            }
        }

        public void ClearForm()
        {
            _selectedStars = 5;
            UpdateStarsUI();
            txtComment.Clear();
        }

        private void UcReview_Disposed(object sender, EventArgs e)
        {
            // Không cần dọn dẹp sao vì chúng là thành phần tĩnh
        }
    }
}