using Domain.ValueObjects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    public partial class LocationPickerControl : BaseUserControl
    {
        private Location _pickup;
        private Location _destination;

        public event EventHandler PickupClicked;
        public event EventHandler DestinationClicked;
        public event Action<LocationPickerControl, Location> LocationSelected;

        public Location Pickup
        {
            get { return _pickup; }
        }

        public Location Destination
        {
            get { return _destination; }
        }

        public string Placeholder { get; set; }

        public bool IsReady
        {
            get { return _pickup != null && _destination != null; }
        }

        public LocationPickerControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Height = 28;
            Placeholder = "Chon dia diem";
        }

        public void SetPickup(Location location)
        {
            _pickup = location;
            Invalidate();
            LocationSelected?.Invoke(this, location);
        }

        public void SetDestination(Location location)
        {
            _destination = location;
            Invalidate();
            LocationSelected?.Invoke(this, location);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_pickup == null)
            {
                PickupClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                DestinationClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            string pickupText = _pickup == null ? "A: " + Placeholder : "A: " + _pickup.ToString();
            string destinationText = _destination == null ? "B: " + Placeholder : "B: " + _destination.ToString();

            TextRenderer.DrawText(
                e.Graphics,
                pickupText,
                Font,
                new Point(4, 2),
                SystemColors.ControlText);

            TextRenderer.DrawText(
                e.Graphics,
                destinationText,
                Font,
                new Point(4, 14),
                SystemColors.GrayText);
        }
    }
}
