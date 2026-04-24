using Application.Interfaces;
using Application.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation;

namespace Presentation.Screens.Passenger
{
    public partial class ReviewForm : BaseForm
    {
        // Dependencies
        private readonly Guid _userId;
        private readonly IReviewService _ReviewService;
        private readonly ITripService _tripService;

        // State
        private List<TripDto> _pendingTrips = new List<TripDto>();
        private TripDto _selectedTrip;
        private int _score = 5;

        public ReviewForm(Guid userId, IReviewService ReviewService, ITripService tripService)
        {
            _userId = userId;
            _ReviewService = ReviewService ?? throw new ArgumentNullException(nameof(ReviewService));
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));

            InitializeComponent();
        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            LoadPending();
        }

        private void OnDrawTripItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _tripList.Items.Count)
            {
                return;
            }

            var trip = (TripDto)_tripList.Items[e.Index];
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(selected ? SystemBrushes.Highlight : SystemBrushes.Window, e.Bounds);

            using (var divPen = new Pen(Color.FromArgb(224, 224, 224)))
            {
                e.Graphics.DrawLine(divPen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
            }

            var fg = selected ? SystemColors.HighlightText : SystemColors.ControlText;
            var fgMuted = selected ? SystemColors.HighlightText : SystemColors.GrayText;

            int x = e.Bounds.X + 10;
            int y = e.Bounds.Y + 6;

            TextRenderer.DrawText(
                e.Graphics,
                trip.CreatedAt.ToString("dd/MM/yyyy  HH:mm"),
                new Font("Segoe UI", 7, FontStyle.Bold),
                new Point(x, y),
                fg);

            string route = $"{trip.Pickup?.Address ?? "--"}  ->  {trip.Destination?.Address ?? "--"}";
            TextRenderer.DrawText(
                e.Graphics,
                Truncate(route, 48),
                new Font("Segoe UI", 8),
                new Point(x, y + 18),
                fgMuted);

            TextRenderer.DrawText(
                e.Graphics,
                $"{trip.Fare?.Amount:N0} đ",
                new Font("Segoe UI", 8, FontStyle.Bold),
                new Point(x, y + 36),
                fg);

            e.DrawFocusRectangle();
        }

        private async void LoadPending()
        {
            _refreshBtn.Enabled = false;
            _refreshBtn.Text = "Đang tải...";
            _statusLabel.Text = "Đang tải...";

            try
            {
                var all = _tripService.GetTripsByPassenger(_userId);

                _pendingTrips = all
                    .Where(t => t.Status == TripStatus.Completed)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToList();

                UpdateTripList();
                ResetSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi tải danh sách: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _refreshBtn.Enabled = true;
                _refreshBtn.Text = "Làm mới";
                _statusLabel.Text = _pendingTrips.Count == 0
                    ? "Không còn chuyến nào cần đánh giá"
                    : $"{_pendingTrips.Count} chuyến chờ đánh giá";
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e) => LoadPending();

        private void UpdateTripList()
        {
            _tripList.Items.Clear();
            foreach (var t in _pendingTrips)
            {
                _tripList.Items.Add(t);
            }

            _listHeader.Text = _pendingTrips.Count == 0
                ? "Không có chuyến nào"
                : $"Chuyến chờ đánh giá ({_pendingTrips.Count})";
        }

        private void ResetSelection()
        {
            _selectedTrip = null;
            _score = 5;

            _tripList.SelectedIndex = -1;
            _promptLabel.Visible = true;
            _ReviewPanel.Visible = false;
            _submitBtn.Enabled = true;
            _successLabel.Visible = false;
            _commentBox.Text = string.Empty;
        }

        // Event handlers
        private void OnTripSelected(object sender, EventArgs e)
        {
            var trip = _tripList.SelectedItem as TripDto;
            if (trip == null)
            {
                return;
            }

            _selectedTrip = trip;

            _tripInfoLabel.Text =
                $"{trip.Pickup?.Address ?? "--"}  ->  {trip.Destination?.Address ?? "--"}\n" +
                $"Thời gian: {trip.CreatedAt:dd/MM/yyyy  HH:mm}      Giá: {trip.Fare?.Amount:N0} đ";

            _score = 5;
            RefreshStars();
            _commentBox.Text = string.Empty;
            _successLabel.Visible = false;
            _submitBtn.Enabled = true;

            _promptLabel.Visible = false;
            _ReviewPanel.Visible = true;
        }

        private void OnStarClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int value)
            {
                _score = value;
                RefreshStars();
            }
        }

        private void RefreshStars()
        {
            for (int i = 0; i < _starBtns.Length; i++)
            {
                _starBtns[i].ForeColor = (i + 1) <= _score
                    ? Color.FromArgb(200, 160, 0)
                    : Color.Silver;
            }

            _scoreHint.Text = $"{_score} / 5 sao";
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (_selectedTrip == null)
            {
                return;
            }

            if (_score < 3 && string.IsNullOrWhiteSpace(_commentBox.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập nhận xét khi đánh giá dưới 3 sao.",
                    "Cần nhận xét",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                _commentBox.Focus();
                return;
            }

            _submitBtn.Enabled = false;

            try
            {
                await _ReviewService.SubmitReviewAsync(
                    _selectedTrip.Id,
                    _score,
                    _commentBox.Text.Trim());

                _successLabel.Visible = true;
                _statusLabel.Text = "Đã gửi đánh giá thành công";

                await Task.Delay(1500);

                ResetSelection();
                LoadPending();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi gửi đánh giá: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                _submitBtn.Enabled = true;
            }
        }

        private static string Truncate(string value, int maxLen)
            => value.Length <= maxLen ? value : value.Substring(0, maxLen) + "...";

        public void RefreshData() => LoadPending();
    }
}
