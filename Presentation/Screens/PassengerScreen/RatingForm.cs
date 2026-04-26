using Application.Interfaces;
using Domain.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    public partial class ReviewForm : BaseForm
    {
        private readonly Guid _userId;
        private readonly IReviewService _reviewService;
        private readonly ITripService _tripService;
        private int _score = 5;

        public ReviewForm()
        {
            InitializeComponent();
        }

        public ReviewForm(Guid userId, IReviewService reviewService, ITripService tripService)
            : this()
        {
            _userId = userId;
            _reviewService = reviewService;
            _tripService = tripService;
        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            _statusLabel.Text = "Chon mot chuyen de danh gia.";
            _ReviewPanel.Visible = false;
            _promptLabel.Visible = true;
            RefreshStars();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            _statusLabel.Text = "Da lam moi.";
        }

        private void OnDrawTripItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();
            object raw = _tripList.Items[e.Index];
            string text = raw == null ? string.Empty : raw.ToString();
            TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds, SystemColors.ControlText);
            e.DrawFocusRectangle();
        }

        private void OnTripSelected(object sender, EventArgs e)
        {
            _promptLabel.Visible = false;
            _ReviewPanel.Visible = true;
            _tripInfoLabel.Text = "Thong tin chuyen di";
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            _successLabel.Visible = true;
            _statusLabel.Text = "Da gui danh gia thanh cong.";
        }

        private void OnStarClicked(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked == null || clicked.Tag == null)
            {
                return;
            }

            int value;
            if (!int.TryParse(clicked.Tag.ToString(), out value))
            {
                return;
            }

            _score = value;
            RefreshStars();
        }

        private void RefreshStars()
        {
            if (_starBtns == null)
            {
                return;
            }

            for (int i = 0; i < _starBtns.Length; i++)
            {
                _starBtns[i].ForeColor = (i + 1) <= _score ? Color.Goldenrod : Color.Silver;
            }

            _scoreHint.Text = string.Format("{0} / 5 sao", _score);
        }

        public void RefreshData()
        {
            _statusLabel.Text = "Da cap nhat du lieu.";
        }
    }
}
