using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using Application.Interfaces;

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Presentation;

namespace Presentation.Screens.PassengerScreen
{
    public partial class ReviewForm : BaseForm
    {
        // Dependencies
        private readonly Guid _userId;
        private readonly IReviewService _ReviewService;
        private readonly ITripService _tripService;

        // State
        private List<Trip> _pendingTrips = new List<Trip>();
        private Trip _selectedTrip;
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

            var trip = (Trip)_tripList.Items[e.Index];
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
                $"{trip.Fare?.Amount:N0} Ä‘",
                new Font("Segoe UI", 8, FontStyle.Bold),
                new Point(x, y + 36),
                fg);

            e.DrawFocusRectangle();
        }

        private async void LoadPending()
        {
            _refreshBtn.Enabled = false;
            _refreshBtn.Text = "Äang táº£i...";
            _statusLabel.Text = "Äang táº£i...";

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
                    $"Lá»—i táº£i danh sÃ¡ch: {ex.Message}",
                    "Lá»—i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _refreshBtn.Enabled = true;
                _refreshBtn.Text = "LÃ m má»›i";
                _statusLabel.Text = _pendingTrips.Count == 0
                    ? "KhÃ´ng cÃ²n chuyáº¿n nÃ o cáº§n Ä‘Ã¡nh giÃ¡"
                    : $"{_pendingTrips.Count} chuyáº¿n chá» Ä‘Ã¡nh giÃ¡";
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
                ? "KhÃ´ng cÃ³ chuyáº¿n nÃ o"
                : $"Chuyáº¿n chá» Ä‘Ã¡nh giÃ¡ ({_pendingTrips.Count})";
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
            var trip = _tripList.SelectedItem as Trip;
            if (trip == null)
            {
                return;
            }

            _selectedTrip = trip;

            _tripInfoLabel.Text =
                $"{trip.Pickup?.Address ?? "--"}  ->  {trip.Destination?.Address ?? "--"}\n" +
                $"Thá»i gian: {trip.CreatedAt:dd/MM/yyyy  HH:mm}      GiÃ¡: {trip.Fare?.Amount:N0} Ä‘";

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
                    "Vui lÃ²ng nháº­p nháº­n xÃ©t khi Ä‘Ã¡nh giÃ¡ dÆ°á»›i 3 sao.",
                    "Cáº§n nháº­n xÃ©t",
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
                _statusLabel.Text = "ÄÃ£ gá»­i Ä‘Ã¡nh giÃ¡ thÃ nh cÃ´ng";

                await Task.Delay(1500);

                ResetSelection();
                LoadPending();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lá»—i gá»­i Ä‘Ã¡nh giÃ¡: {ex.Message}",
                    "Lá»—i",
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

