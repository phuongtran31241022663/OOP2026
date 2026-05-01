using Application.Interfaces;
using Domain.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Danh gia 5 sao + comment sau chuyen.
    /// Hiển thị thông tin đánh giá.
    /// </summary>
    public partial class UcRating : BaseUserControl
    {
        private readonly IReviewService _reviewService;
        private readonly Trip _trip;
        private int _selectedRating = 0;

        public UcRating(IReviewService reviewService, Trip trip)
        {
            _reviewService = reviewService;
            _trip = trip;
            InitializeComponent();
            SetupStarButtons();
            SetupSubmitButtonEffects();
        }

        private void SetupSubmitButtonEffects()
        {
            btnSubmit.MouseEnter += (s, e) => btnSubmit.BackColor = Color.FromArgb(25, 118, 210);
            btnSubmit.MouseLeave += (s, e) => btnSubmit.BackColor = Color.FromArgb(33, 150, 243);
        }

        private void SetupStarButtons()
        {
            btnStar1.Click += (s, e) => SetRating(1);
            btnStar2.Click += (s, e) => SetRating(2);
            btnStar3.Click += (s, e) => SetRating(3);
            btnStar4.Click += (s, e) => SetRating(4);
            btnStar5.Click += (s, e) => SetRating(5);

            // Hover effects for stars
            Button[] stars = { btnStar1, btnStar2, btnStar3, btnStar4, btnStar5 };
            foreach (var star in stars)
            {
                star.MouseEnter += (s, e) => star.Cursor = Cursors.Hand;
            }

            btnSubmit.Click += async (s, e) => await OnSubmitClicked();
        }

        private void SetRating(int rating)
        {
            _selectedRating = rating;
            UpdateStarDisplay();
        }

        private void UpdateStarDisplay()
        {
            Button[] stars = { btnStar1, btnStar2, btnStar3, btnStar4, btnStar5 };
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Text = i < _selectedRating ? "\u2605" : "\u2606";
                stars[i].ForeColor = i < _selectedRating ? Color.Gold : Color.Gray;
            }
        }

        private async System.Threading.Tasks.Task OnSubmitClicked()
        {
            if (_selectedRating == 0)
            {
                ShowWarning("Vui lòng chọn số sao để đánh giá.");
                return;
            }

            IsLoading = true;
            await ExecuteWithHandlingAsync("Gửi đánh giá tài xế", async () =>
            {
                if (_reviewService != null && _trip != null)
                {
                    await _reviewService.AddReviewAsync(_trip.DriverId ?? Guid.Empty, _trip.PassengerId, _trip.Id, _selectedRating, txtComment.Text);
                }
                else
                {
                    throw new InvalidOperationException("Không tìm thấy dữ liệu chuyến đi để đánh giá.");
                }

                ShowInfo("Đánh giá thành công!");
                var parent = this.ParentForm;
                if (parent != null) parent.DialogResult = DialogResult.OK;
            }, () => IsLoading = false);
        }
    }
}

