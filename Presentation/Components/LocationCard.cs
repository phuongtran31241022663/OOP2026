// Presentation/Components/LocationCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// UserControl hiển thị thông tin địa điểm với style nhất quán.
    /// Thay thế việc dùng TextBox để hiển thị thông tin địa điểm.
    /// </summary>
    public partial class LocationCard : UserControl
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
            // Icon panel
            _iconPanel = new Panel
            {
                Width = 40,
                Height = 40,
                BackColor = Color.Green,
                Location = new Point(12, 12)
            };
            var iconLabel = new Label
            {
                Text = "📍",
                Font = new Font("Segoe UI", 16),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            _iconPanel.Controls.Add(iconLabel);

            // Name label
            _lblName = new Label
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = false,
                Location = new Point(60, 10),
                Width = 200,
                Height = 20,
                Text = "Tên địa điểm"
            };

            // Address label
            _lblAddress = new Label
            {
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Location = new Point(60, 32),
                Width = 280,
                Height = 16,
                Text = "Địa chỉ chi tiết"
            };

            // Coordinates label
            _lblCoords = new Label
            {
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DarkGray,
                AutoSize = false,
                Location = new Point(60, 50),
                Width = 100,
                Height = 14,
                Text = ""
            };

            // Main container
            this.Size = new Size(360, 64);
            this.BackColor = Color.White;
            this.Controls.Add(_iconPanel);
            this.Controls.Add(_lblName);
            this.Controls.Add(_lblAddress);
            this.Controls.Add(_lblCoords);

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