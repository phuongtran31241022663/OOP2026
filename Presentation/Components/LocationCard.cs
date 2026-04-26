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
        /// Äá»‹a Ä‘iá»ƒm Ä‘Æ°á»£c hiá»ƒn thá»‹
        /// </summary>
        public object GeoLocation { get; private set; }

        public LocationCard()
        {
            InitializeComponent();

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
        /// Set thÃ´ng tin Ä‘á»‹a Ä‘iá»ƒm tá»« cÃ¡c tham sá»‘ riÃªng
        /// </summary>
        public void SetLocation(string name, string address, double lat, double lng)
        {
            GeoLocation = new { Name = name, Address = address, Lat = lat, Lng = lng };
            _lblName.Text = name;
            _lblAddress.Text = address;
            _lblCoords.Text = $"{lat:F5}, {lng:F5}";
        }

        /// <summary>
        /// Set icon cho card (pickup/destination marker)
        /// </summary>
        public void SetIcon(string emoji)
        {
            if (_iconPanel.Controls[0] is Label iconLabel)
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
            this.BackColor = Color.White;
        }
    }
}
