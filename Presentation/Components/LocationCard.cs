// Presentation/Components/LocationCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiển thị thông tin địa điểm với style nhất quán.
    /// Thay thế việc dùng TextBox để hiển thị thông tin địa điểm.
    /// </summary>
    public partial class LocationCard : BaseUserControl
    {
        /// <summary>
        /// Sự kiện khi người dùng click vào location card
        /// </summary>
        public event Action<LocationCard> Clicked;

        /// <summary>
        /// Địa điểm được hiển thị
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
        /// Set thông tin địa điểm từ các tham số riêng
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