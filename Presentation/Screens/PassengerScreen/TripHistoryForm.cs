using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    public partial class TripHistoryForm : BaseForm
    {
        private readonly Guid _userId;
        private readonly ITripService _tripService;
        private readonly List<Trip> _trips = new List<Trip>();

        public TripHistoryForm()
        {
            InitializeComponent();
        }

        public TripHistoryForm(Guid userId, ITripService tripService)
            : this()
        {
            _userId = userId;
            _tripService = tripService;
        }

        private void TripHistoryForm_Load(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private async void LoadTrips()
        {
            _grid.Rows.Clear();
            _statusLabel.Text = "Dang tai...";

            if (_tripService != null)
            {
                List<Trip> trips = await _tripService.GetTripsByPassengerAsync(_userId);
                _trips.Clear();
                if (trips != null)
                {
                    _trips.AddRange(trips);
                }
            }

            for (int i = 0; i < _trips.Count; i++)
            {
                Trip trip = _trips[i];
                _grid.Rows.Add(
                    trip.TripRoute != null ? trip.TripRoute.Pickup.ToString() : "--",
                    trip.TripRoute != null ? trip.TripRoute.Destination.ToString() : "--",
                    trip.Distance.HasValue ? trip.Distance.Value.ToString("F1") + " km" : "--",
                    trip.TripFare != null ? trip.TripFare.TotalAmount.Amount.ToString("N0") + " d" : "--",
                    trip.Status.ToString(),
                    trip.RequestAt.ToString("dd/MM HH:mm"));
            }

            _emptyLabel.Visible = _trips.Count == 0;
            _grid.Visible = _trips.Count > 0;
            _statusLabel.Text = string.Format("{0} chuyen", _trips.Count);
            _totalLabel.Text = string.Format("Tong chuyen: {0}", _trips.Count);
            _completedLabel.Text = "Hoan thanh: --";
            _cancelledLabel.Text = "Da huy: --";
            _spentLabel.Text = "Tong chi tieu: --";
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (_grid.Columns[e.ColumnIndex].Name != "Status")
            {
                return;
            }

            string status = e.Value == null ? string.Empty : e.Value.ToString();
            if (status == "Completed")
            {
                e.CellStyle.BackColor = Color.Honeydew;
            }
            else if (status == "Cancelled")
            {
                e.CellStyle.BackColor = Color.MistyRose;
            }
        }

        public void RefreshData()
        {
            LoadTrips();
        }
    }
}
