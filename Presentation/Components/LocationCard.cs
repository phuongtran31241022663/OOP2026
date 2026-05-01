using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
// Presentation/Components/LocationCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiá»ƒn thá»‹ thÃ´ng tin Ä‘á»‹a Ä‘iá»ƒm vá»›i style nháº¥t quÃ¡n.
    /// Thay tháº¿ viá»‡c dÃ¹ng TextBox Ä‘á»ƒ hiá»ƒn thá»‹ thÃ´ng tin Ä‘á»‹a Ä‘iá»ƒm.
    /// </summary>
    public partial class LocationCard : BaseUserControl
    {
        /// <summary>
        /// Sá»± kiá»‡n khi ngÆ°á»i dÃ¹ng click vÃ o location card
        /// </summary>
        public event Action<LocationCard> Clicked;

        /// <summary>
        /// Địa điểm được hiển thị
        /// </summary>
        public Location SelectedLocation { get; private set; }
        private Color _originalBackColor;

        public LocationCard()
        {
            InitializeComponent();
            _originalBackColor = this.BackColor;

            // Enable click events
            this.Cursor = Cursors.Hand;
            this.Click += OnCardClick;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += OnCardClick;
                ctrl.Cursor = Cursors.Hand;
            }

            // Hover effect
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

        /// <summary>
        /// Set thông tin địa điểm từ Location object
        /// </summary>
        public void SetLocation(Location location)
        {
            SelectedLocation = location;
            _lblName.Text = location.Address?.Street ?? "Không rõ tên";
            _lblAddress.Text = $"{location.Address?.District}, {location.Address?.City}";
            _lblCoords.Text = $"{location.Coordinate.Latitude:F5}, {location.Coordinate.Longitude:F5}";
        }

        /// <summary>
        /// Set icon cho card (pickup/destination marker)
        /// </summary>
        public void SetIcon(string emoji)
        {
            if (_iconPanel.Controls.Count > 0 && _iconPanel.Controls[0] is Label iconLabel)
            {
                iconLabel.Text = emoji;
            }
        }

        private void OnCardClick(object sender, EventArgs e)
        {
            if (Clicked != null) Clicked(this);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _originalBackColor;
        }
    }
}
