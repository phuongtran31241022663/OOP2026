using Application.Interfaces;
using Application.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.Screens.Passenger
{
    public partial class TripHistoryForm : Form
    {
        // Dependencies
        private readonly Guid _userId;
        private readonly ITripService _tripService;

        // State
        private List<TripDto> _trips = new List<TripDto>();

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
                    $"Lỗi tải lịch sử: {ex.Message}",
                    "Lỗi",
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
            _refreshBtn.Text = loading ? "Đang tải..." : "Làm mới";
            _statusLabel.Text = loading ? "Đang tải..." : BuildStatusText();
        }

        private void UpdateSummary()
        {
            var completed = _trips.Where(t => t.Status == TripStatus.Completed).ToList();
            var cancelled = _trips.Where(t => t.Status == TripStatus.Cancelled).ToList();
            decimal spent = completed.Sum(t => t.Fare?.Amount ?? 0);

            _totalLabel.Text = $"Tổng chuyến: {_trips.Count}";
            _spentLabel.Text = $"Chi tiêu: {spent:N0} đ";
            _completedLabel.Text = $"Hoàn thành: {completed.Count}";
            _cancelledLabel.Text = $"Đã hủy: {cancelled.Count}";
        }

        private void RenderGrid()
        {
            _grid.Rows.Clear();

            if (!_trips.Any())
            {
                _grid.Visible = false;
                _emptyLabel.Visible = true;
                _statusLabel.Text = "Không có dữ liệu";
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
                    $"{trip.Fare?.Amount:N0} đ",
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
                ? "Không có chuyến đi"
                : $"{total} chuyến đi  ·  trang 1/1";
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
                    return "Hoàn thành";
                case TripStatus.Cancelled:
                    return "Đã hủy";
                case TripStatus.Started:
                    return "Đang chạy";
                case TripStatus.Matched:
                    return "Đã ghép đôi";
                case TripStatus.Searching:
                    return "Đang tìm";
                case TripStatus.Requested:
                    return "Đã yêu cầu";
                default:
                    return "--";
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e) => LoadTrips();

        public void RefreshData() => LoadTrips();
    }
}
