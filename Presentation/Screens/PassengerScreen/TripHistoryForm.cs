using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using Application.Interfaces;

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Presentation;

namespace Presentation.Screens.PassengerScreen
{
    public partial class TripHistoryForm : BaseForm
    {
        // Dependencies
        private readonly Guid _userId;
        private readonly ITripService _tripService;

        // State
        private List<Trip> _trips = new List<Trip>();

        public TripHistoryForm()
        {
            InitializeComponent();
        }

        public TripHistoryForm(Guid userId, ITripService tripService)
        {
            _userId = userId;
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));

            InitializeComponent();
        }

        private void TripHistoryForm_Load(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private async void LoadTrips()
        {
            SetLoading(true);
            try
            {
                _trips = _tripService.GetTripsByPassenger(_userId).ToList();

                UpdateSummary();
                RenderGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lá»—i táº£i lá»‹ch sá»­: {ex.Message}",
                    "Lá»—i",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void SetLoading(bool loading)
        {
            _refreshBtn.Enabled = !loading;
            _refreshBtn.Text = loading ? "Äang táº£i..." : "LÃ m má»›i";
            _statusLabel.Text = loading ? "Äang táº£i..." : BuildStatusText();
        }

        private void UpdateSummary()
        {
            var completed = _trips.Where(t => t.Status == TripStatus.Completed).ToList();
            var cancelled = _trips.Where(t => t.Status == TripStatus.Cancelled).ToList();
            decimal spent = completed.Sum(t => t.Fare?.Amount ?? 0);

            _totalLabel.Text = $"Tá»•ng chuyáº¿n: {_trips.Count}";
            _spentLabel.Text = $"Chi tiÃªu: {spent:N0} Ä‘";
            _completedLabel.Text = $"HoÃ n thÃ nh: {completed.Count}";
            _cancelledLabel.Text = $"ÄÃ£ há»§y: {cancelled.Count}";
        }

        private void RenderGrid()
        {
            _grid.Rows.Clear();

            if (!_trips.Any())
            {
                _grid.Visible = false;
                _emptyLabel.Visible = true;
                _statusLabel.Text = "KhÃ´ng cÃ³ dá»¯ liá»‡u";
                return;
            }

            _grid.Visible = true;
            _emptyLabel.Visible = false;

            foreach (var trip in _trips.OrderByDescending(t => t.RequestedAt))
            {
                int row = _grid.Rows.Add(
                    trip.Pickup?.Address ?? "--",
                    trip.Destination?.Address ?? "--",
                    trip.DriverName ?? "--",
                    $"{trip.Fare?.Amount:N0} Ä‘",
                    StatusText(trip.Status),
                    trip.RequestedAt.ToString("dd/MM HH:mm"));
                _grid.Rows[row].Tag = trip.Status;
            }

            _statusLabel.Text = BuildStatusText();
        }

        private string BuildStatusText()
        {
            int total = _trips.Count;
            return total == 0
                ? "KhÃ´ng cÃ³ chuyáº¿n Ä‘i"
                : $"{total} chuyáº¿n Ä‘i  Â·  trang 1/1";
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_grid.Columns[e.ColumnIndex].Name != "Status")
            {
                return;
            }

            if (!(_grid.Rows[e.RowIndex].Tag is TripStatus))
            {
                return;
            }

            var status = (TripStatus)_grid.Rows[e.RowIndex].Tag;

            switch (status)
            {
                case TripStatus.Completed:
                    e.CellStyle.BackColor = Color.FromArgb(200, 240, 200);
                    e.CellStyle.ForeColor = Color.FromArgb(0, 80, 0);
                    break;
                case TripStatus.Cancelled:
                    e.CellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    e.CellStyle.ForeColor = Color.FromArgb(120, 0, 0);
                    break;
                case TripStatus.Started:
                    e.CellStyle.BackColor = Color.FromArgb(255, 240, 190);
                    e.CellStyle.ForeColor = Color.FromArgb(96, 64, 0);
                    break;
                default:
                    e.CellStyle.BackColor = SystemColors.Window;
                    e.CellStyle.ForeColor = SystemColors.ControlText;
                    break;
            }
        }

        private static string StatusText(TripStatus status)
        {
            switch (status)
            {
                case TripStatus.Completed:
                    return "HoÃ n thÃ nh";
                case TripStatus.Cancelled:
                    return "ÄÃ£ há»§y";
                case TripStatus.Started:
                    return "Äang cháº¡y";
                case TripStatus.Matched:
                    return "ÄÃ£ ghÃ©p Ä‘Ã´i";
                case TripStatus.Searching:
                    return "Äang tÃ¬m";
                case TripStatus.Requested:
                    return "ÄÃ£ yÃªu cáº§u";
                default:
                    return "--";
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e) => LoadTrips();

        public void RefreshData() => LoadTrips();
    }
}

