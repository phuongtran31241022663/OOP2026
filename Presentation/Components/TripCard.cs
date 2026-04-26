using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
// Presentation/Components/TripCard.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// BaseUserControl hiá»ƒn thá»‹ thÃ´ng tin chuyáº¿n Ä‘i vá»›i style nháº¥t quÃ¡n.
    /// DÃ¹ng vá»›i FlowLayoutPanel Ä‘á»ƒ hiá»ƒn thá»‹ danh sÃ¡ch chuyáº¿n.
    /// </summary>
    public partial class TripCard : BaseUserControl
    {
        /// <summary>
        /// Sá»± kiá»‡n khi ngÆ°á»i dÃ¹ng click vÃ o trip card
        /// </summary>
        public event Action<TripCard> Clicked;

        /// <summary>
        /// Trip Ä‘Æ°á»£c hiá»ƒn thá»‹
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
        /// Set thÃ´ng tin trip tá»« cÃ¡c tham sá»‘
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
