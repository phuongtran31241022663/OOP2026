using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class UcActive : UserControl
    {
        private readonly ITripQry _tripQry;

        public UcActive()
        {
            InitializeComponent();
        }

        public UcActive(ITripQry tripQry) : this()
        {
            _tripQry = tripQry;
        }

        public void SetData(int completed, int cancelled, int timeout, int active)
        {
            lblCompletedCount.Text = completed.ToString();
            lblCancelledCount.Text = cancelled.ToString();
            lblTimeoutCount.Text = timeout.ToString();
            lblActiveCount.Text = active.ToString();
        }

        public async Task LoadDataAsync(Guid driverId)
        {
            if (_tripQry == null) return;

            var trips =
                await _tripQry.GetTripsByDriverAsync(driverId);

            SetData(
                trips.Count(t => t.Status == TripStatus.Completed),
                trips.Count(t => t.Status == TripStatus.Cancelled),
                trips.Count(t => t.Status == TripStatus.Timeout),
                trips.Count(t => t.Status == TripStatus.Matched ||
                                 t.Status == TripStatus.Arrived ||
                                 t.Status == TripStatus.Started)
            );
        }
    }
}