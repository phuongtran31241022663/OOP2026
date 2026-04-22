// Presentation/Components/TripCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// UserControl hiển thị thông tin chuyến đi với style nhất quán.
    /// Dùng với FlowLayoutPanel để hiển thị danh sách chuyến.
    /// </summary>
    public partial class TripCard : UserControl
    {
        private readonly Label _lblStatus;
        private readonly Label _lblRoute;
        private readonly Label _lblInfo;
        private readonly Label _lblTime;
        private readonly Panel _statusIndicator;

        /// <summary>
        /// Sự kiện khi người dùng click vào trip card
        /// </summary>
        public event Action<TripCard> Clicked;

        /// <summary>
        /// Trip được hiển thị
        /// </summary>
        public object Trip { get; private set; }

        public TripCard()
        {
            // Status indicator (colored bar)
            _statusIndicator = new Panel
            {
                Width = 4,
                Height = 70,
                Location = new Point(0, 0),
                BackColor = Color.Blue
            };

            // Status label
            _lblStatus = new Label
            {
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(16, 8),
                AutoSize = true,
                Text = "Chờ xử lý"
            };

            // Route label
            _lblRoute = new Label
            {
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Black,
                Location = new Point(16, 30),
                AutoSize = false,
                Width = 320,
                Height = 18,
                Text = "Điểm đón → Điểm đến"
            };

            // Info label (fare, vehicle type)
            _lblInfo = new Label
            {
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(16, 52),
                AutoSize = true,
                Text = "15.000 VNĐ • Xe máy"
            };

            // Time label
            _lblTime = new Label
            {
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DarkGray,
                Location = new Point(250, 52),
                AutoSize = true,
                Text = "10:30"
            };

            // Main container
            this.Size = new Size(360, 70);
            this.BackColor = Color.White;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0, 0, 0, 5);

            this.Controls.Add(_statusIndicator);
            this.Controls.Add(_lblStatus);
            this.Controls.Add(_lblRoute);
            this.Controls.Add(_lblInfo);
            this.Controls.Add(_lblTime);

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
        /// Set thông tin trip từ các tham số
        /// </summary>
        public void SetTrip(string status, string route, string info, string time, Color statusColor)
        {
            Trip = new { Status = status, Route = route, Info = info, Time = time };
            _lblStatus.Text = status;
            _statusIndicator.BackColor = statusColor;
            _lblRoute.Text = route;
            _lblInfo.Text = info;
            _lblTime.Text = time;
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