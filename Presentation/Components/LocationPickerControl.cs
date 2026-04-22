// Presentation/Components/LocationPickerControl.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// Hiển thị điểm đón (trên) và điểm đến (dưới) theo kiểu card — thay thế TextBox thô.
    /// Mỗi slot gồm icon màu + tên địa danh (bold) + địa chỉ chi tiết (muted).
    /// Có đường nối dọc giữa hai điểm như giao diện Grab/Uber.
    /// </summary>
    public partial class LocationPickerControl : UserControl
    {
        // ── State ────────────────────────────────────────────────────────────
        private object _pickup;
        private object _destination;

        // ── Events ───────────────────────────────────────────────────────────
        /// <summary>Fired khi người dùng click vào slot để chọn địa điểm.</summary>
        public event EventHandler PickupClicked;
        public event EventHandler DestinationClicked;

        public event Action<LocationPickerControl, Domain.ValueObjects.Location> LocationSelected;

        public string Placeholder { get; set; }

        // ── Layout constants ─────────────────────────────────────────────────
        private const int SlotHeight = 64;
        private const int IconCol = 20;   // x-center of icon column
        private const int IconSize = 12;
        private const int TextLeft = 48;
        private const int ConnectorX = IconCol;
        private const int CardRadius = 12;
        private const int PaddingV = 12;

        // ── Colors ───────────────────────────────────────────────────────────
        private static readonly Color PickupColor = Color.FromArgb(16, 140, 92);   // green
        private static readonly Color DestinationColor = Color.FromArgb(210, 64, 70);   // red
        private static readonly Color ConnectorColor = Color.FromArgb(200, 204, 210);
        private static readonly Color SlotHover = Color.FromArgb(245, 247, 252);
        private static readonly Color PlaceholderColor = Color.FromArgb(160, 166, 176);

        // ── Fonts ────────────────────────────────────────────────────────────
        private readonly Font _nameFont = new Font("Segoe UI", 10f, FontStyle.Bold);
        private readonly Font _addressFont = new Font("Segoe UI", 9f);
        private readonly Font _placeholderFont = new Font("Segoe UI", 10f, FontStyle.Italic);

        // ── Hover tracking ───────────────────────────────────────────────────
        private bool _pickupHover;
        private bool _destHover;

        // ────────────────────────────────────────────────────────────────────
        public LocationPickerControl()
        {
            SetStyle(ControlStyles.UserPaint
                   | ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.ResizeRedraw, true);

            Height = PaddingV + SlotHeight + 1 + SlotHeight + PaddingV;
            Cursor = Cursors.Hand;
        }

        // ── Public API ───────────────────────────────────────────────────────
        public void SetPickup(object location)
        {
            _pickup = location;
            Invalidate();
        }

        public void SetDestination(object location)
        {
            _destination = location;
            Invalidate();
        }

        public object Pickup => _pickup;
        public object Destination => _destination;
        public bool IsReady => _pickup != null && _destination != null;

        // ── Hit testing ──────────────────────────────────────────────────────
        private Rectangle PickupRect => new Rectangle(0, PaddingV, Width, SlotHeight);
        private Rectangle DestinationRect => new Rectangle(0, PaddingV + SlotHeight + 1, Width, SlotHeight);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool ph = PickupRect.Contains(e.Location);
            bool dh = DestinationRect.Contains(e.Location);
            if (ph != _pickupHover || dh != _destHover)
            {
                _pickupHover = ph;
                _destHover = dh;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _pickupHover = _destHover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            var pt = PointToClient(Cursor.Position);
            if (PickupRect.Contains(pt))
                PickupClicked?.Invoke(this, EventArgs.Empty);
            else if (DestinationRect.Contains(pt))
                DestinationClicked?.Invoke(this, EventArgs.Empty);
            base.OnClick(e);
        }

        // ── Painting ─────────────────────────────────────────────────────────
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Card background with rounded corners
            using (GraphicsPath cardPath = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), CardRadius))
            using (var cardBrush = new SolidBrush(Color.White))
            {
                g.FillPath(cardBrush, cardPath);
                using (var borderPen = new Pen(Color.FromArgb(220, 224, 230)))
                    g.DrawPath(borderPen, cardPath);
            }

            // Divider line between pickup and destination
            int divY = PaddingV + SlotHeight;
            g.DrawLine(new Pen(Color.FromArgb(235, 238, 243)), TextLeft, divY, Width - 16, divY);

            // Vertical connector between icons
            int iconTop = PaddingV + SlotHeight / 2;
            int iconBottom = PaddingV + SlotHeight + SlotHeight / 2;
            using (var connPen = new Pen(ConnectorColor, 2) { DashStyle = DashStyle.Dot })
                g.DrawLine(connPen, ConnectorX, iconTop + IconSize, ConnectorX, iconBottom - IconSize);

            // Draw each slot
            DrawSlot(g, PickupRect, _pickup, PickupColor, _pickupHover,
                        "Chọn điểm đón...", isPickup: true);
            DrawSlot(g, DestinationRect, _destination, DestinationColor, _destHover,
                        "Chọn điểm đến...", isPickup: false);
        }

        private void DrawSlot(
            Graphics g,
            Rectangle rect,
            object loc,
            Color iconColor,
            bool hover,
            string placeholder,
            bool isPickup)
        {
            // Hover background
            if (hover)
            {
                using (SolidBrush hoverBrush = new SolidBrush(SlotHover))
                {
                    // Only round the top or bottom corners depending on slot
                    Rectangle hRect = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2,
                                                     rect.Height - (isPickup ? 0 : 1));
                    g.FillRectangle(hoverBrush, hRect);
                }
            }

            // Icon (circle with inner dot)
            int cy = rect.Y + rect.Height / 2;
            int cx = IconCol;
            Rectangle iconRect = new Rectangle(cx - IconSize / 2, cy - IconSize / 2, IconSize, IconSize);

            using (SolidBrush iconFill = new SolidBrush(Color.White))
            using (Pen iconPen = new Pen(iconColor, 2.5f))
            {
                g.FillEllipse(iconFill, iconRect);
                g.DrawEllipse(iconPen, iconRect);
            }

            // Inner dot for destination diamond feel
            int dotSize = isPickup ? 5 : 6;
            using (SolidBrush dotBrush = new SolidBrush(iconColor))
                g.FillEllipse(dotBrush,
                    cx - dotSize / 2, cy - dotSize / 2, dotSize, dotSize);

            // Text area
            int textRight = Width - 40;
            int textWidth = textRight - TextLeft;

            if (loc != null && loc is Domain.ValueObjects.Location location)
            {
                string name = location.Name;
                string addr = location.Address;
                using (var nameBrush = new SolidBrush(Color.FromArgb(28, 32, 38)))
                    g.DrawString(name, _nameFont, nameBrush,
                        new RectangleF(TextLeft, rect.Y + 10, textWidth, 22));

                using (var addrBrush = new SolidBrush(Color.FromArgb(110, 118, 129)))
                    g.DrawString(addr, _addressFont, addrBrush,
                        new RectangleF(TextLeft, rect.Y + 34, textWidth, 18));
            }
            else
            {
                // Placeholder
                using (SolidBrush phBrush = new SolidBrush(PlaceholderColor))
                    g.DrawString(placeholder, _placeholderFont, phBrush,
                        new RectangleF(TextLeft, rect.Y + 20, textWidth, 24));
            }

            // Chevron hint on right side
            DrawChevron(g, Width - 24, cy);
        }

        private static void DrawChevron(Graphics g, int x, int cy)
        {
            using (Pen pen = new Pen(Color.FromArgb(190, 196, 204), 1.5f))
            {
                pen.EndCap = LineCap.Round;
                pen.StartCap = LineCap.Round;
                g.DrawLines(pen, new[]
                {
                    new Point(x, cy - 4),
                    new Point(x + 5, cy),
                    new Point(x, cy + 4)
                });
            }
        }

        private static string TruncateText(string text, Font font, Graphics g, int maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return text;
            if (g.MeasureString(text, font).Width <= maxWidth) return text;

            string ellipsis = "...";
            while (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
                if (g.MeasureString(text + ellipsis, font).Width <= maxWidth)
                    return text + ellipsis;
            }
            return ellipsis;
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

      
    }
}