using System;
using System.Collections.Generic;
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

        private int _selectedStars = 5;
        private readonly List<Label> _starLabels = new List<Label>();

        public event EventHandler ReviewSubmitted;
        public event EventHandler ReviewCancelled;

        public int SelectedStars => _selectedStars;
        public string Comment => txtComment.Text;

        public ucReview()
        {
            InitializeComponent();
            this.Disposed += UcReview_Disposed;
        }

        public void Initialize(Guid driverId, Guid passengerId, Guid tripId, IRevSvc reviewService)
        {
            _driverId = driverId;
            _passengerId = passengerId;
            _tripId = tripId;
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        // ─────────────────────────────────────────────────────
        //  SỬA: Render star sau khi Layout hoàn tất để pnlStars.Width > 0
        // ─────────────────────────────────────────────────────
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (_starLabels.Count == 0 && pnlStars.Width > 0)
                RenderStars();
        }

        private void RenderStars()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(RenderStars));
                return;
            }

            ClearAndDisposeStars();

            int starSize = 36;
            int startX = Math.Max(0, (pnlStars.Width - 5 * starSize) / 2);

            for (int i = 0; i < 5; i++)
            {
                var lbl = new Label
                {
                    Text = "⭐",
                    Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular),
                    Size = new Size(starSize, starSize),
                    Location = new Point(startX + i * starSize, (pnlStars.Height - starSize) / 2),
                    Cursor = Cursors.Hand,
                    Tag = i + 1,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                lbl.Click += Star_Click;
                _starLabels.Add(lbl);
                pnlStars.Controls.Add(lbl);
            }
            UpdateStarsUI();
        }

        private void Star_Click(object sender, EventArgs e)
        {
            if (sender is Label lbl && lbl.Tag is int stars)
            {
                _selectedStars = stars;
                UpdateStarsUI();
            }
        }

        private void UpdateStarsUI()
        {
            for (int i = 0; i < _starLabels.Count; i++)
            {
                bool lit = (i + 1) <= _selectedStars;
                _starLabels[i].ForeColor = lit ? Colors.Orange : Color.FromArgb(209, 213, 219);
            }
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

                // ĐÃ SỬA: MessageBox.Show(text, caption) — đúng thứ tự tham số
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

        private void ClearAndDisposeStars()
        {
            for (int i = _starLabels.Count - 1; i >= 0; i--)
            {
                var lbl = _starLabels[i];
                lbl.Click -= Star_Click;
                if (pnlStars.Controls.Contains(lbl))
                    pnlStars.Controls.Remove(lbl);
                lbl.Dispose();
            }
            _starLabels.Clear();
        }

        private void UcReview_Disposed(object sender, EventArgs e)
        {
            ClearAndDisposeStars();
        }
    }
}