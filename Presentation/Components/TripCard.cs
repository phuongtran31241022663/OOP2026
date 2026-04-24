// Presentation/Components/TripCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiển thị thông tin chuyến đi với style nhất quán.
    /// Dùng với FlowLayoutPanel để hiển thị danh sách chuyến.
    /// </summary>
    public partial class TripCard : BaseUserControl
    {
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