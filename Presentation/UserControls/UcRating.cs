using Application.Interfaces;
using Domain.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.UserControls
{
    /// <summary>
    /// Danh gia 5 sao + comment sau chuyen.
    /// Mo trong FrmModal.
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
        }

        private void SetupStarButtons()
        {
            btnStar1.Click += (s, e) => SetRating(1);
            btnStar2.Click += (s, e) => SetRating(2);
            btnStar3.Click += (s, e) => SetRating(3);
            btnStar4.Click += (s, e) => SetRating(4);
            btnStar5.Click += (s, e) => SetRating(5);

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
                ShowWarning("Vui long chon so sao danh gia.");
                return;
            }

            try
            {
                if (_reviewService != null && _trip != null)
                {
                    await _reviewService.AddReviewAsync(_trip.DriverId ?? Guid.Empty, _trip.PassengerId, _trip.Id, _selectedRating, txtComment.Text);
                }
                ShowInfo("Danh gia thanh cong!");
                var parent = this.ParentForm;
                if (parent != null) parent.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ShowError("Danh gia that bai: " + ex.Message);
            }
        }
    }
}

