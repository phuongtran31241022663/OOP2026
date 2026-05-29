using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucTripStatus : UserControl
    {
        private TripStatus _currentStatus = TripStatus.Pending;

        public event EventHandler CancelClicked;
        public event EventHandler RateClicked;
        public event EventHandler RetryClicked;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TripStatus CurrentStatus
        {
            get => _currentStatus;
            set
            {
                _currentStatus = value;
            }
        }

        public ucTripStatus()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // ─────────────────────────────────────────────────────
        //  BUTTON EVENTS — được wire trong Designer
        // ─────────────────────────────────────────────────────

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            RateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            RetryClicked?.Invoke(this, EventArgs.Empty);
        }

        // ─────────────────────────────────────────────────────
        //  STATE MACHINE → UI
        // ─────────────────────────────────────────────────────

        private void SetIcon(string icon, Color bgColor, Color fgColor)
        {
            lblIcon.Text = icon;
            pnlStatusIcon.BackColor = bgColor;
            lblIcon.ForeColor = fgColor;
        }
    }
}