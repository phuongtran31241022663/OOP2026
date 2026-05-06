using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Constants
{
    /// <summary>
    /// UI constants for consistent theming and layout across the application.
    /// </summary>
    public static class UiConstants
    {
        public static class Colors
        {
            // Brand / Primary (Teal)
            public static readonly Color Primary = Color.FromArgb(0, 150, 136);
            public static readonly Color PrimaryHover = Color.FromArgb(0, 120, 109);
            public static readonly Color PrimaryPressed = Color.FromArgb(0, 90, 85);

            // Feedback States
            public static readonly Color Success = Color.FromArgb(0, 200, 0);
            public static readonly Color SuccessHover = Color.FromArgb(0, 153, 0);
            public static readonly Color Danger = Color.FromArgb(220, 50, 50);
            public static readonly Color DangerHover = Color.FromArgb(200, 0, 0);
            public static readonly Color Error = Color.Red;
            public static readonly Color Warning = Color.Orange;
            public static readonly Color Info = Color.FromArgb(33, 150, 243);

            // Backgrounds / Surfaces
            public static readonly Color SurfaceWhite = Color.White;
            public static readonly Color SurfaceLight = Color.FromArgb(245, 245, 245);
            public static readonly Color SurfaceVariant = Color.FromArgb(250, 250, 250);

            // Typography
            public static readonly Color TextPrimary = Color.FromArgb(33, 33, 33);
            public static readonly Color TextSecondary = Color.FromArgb(64, 64, 64);
            public static readonly Color TextMuted = Color.Gray;
            public static readonly Color TextOnKey = Color.White;

            // Borders
            public static readonly Color BorderSubtle = Color.FromArgb(224, 224, 224);
            public static readonly Color BorderDefault = Color.FromArgb(189, 189, 189);
        }

        public static class Typography
        {
            public static readonly Font Logo = new Font("Segoe UI", 22F, FontStyle.Bold);
            public static readonly Font Header = new Font("Segoe UI", 14F, FontStyle.Bold);
            public static readonly Font Body = new Font("Segoe UI", 12F, FontStyle.Regular);
            public static readonly Font BodyBold = new Font("Segoe UI", 12F, FontStyle.Bold);
            public static readonly Font Default = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            public static readonly Font Small = new Font("Segoe UI", 10, FontStyle.Regular);
            public static readonly Font Tiny = new Font("Segoe UI", 8F, FontStyle.Regular);
        }

        public static class ButtonSizes
        {
            public static readonly Size Default = new Size(100, 32);
            public static readonly Size Compact = new Size(80, 26);
            public static readonly Size Action = new Size(150, 40);
            public static readonly Size Large = new Size(200, 48);

        }

        public static class Spacing
        {
            public static readonly Padding PanelPadding = new Padding(12);
            public static readonly Padding ButtonPadding = new Padding(8, 6, 8, 6);
            public static readonly Padding DefaultMargin = new Padding(0, 0, 0, 8);
            public static readonly Padding TightMargin = new Padding(0, 0, 0, 4);
        }

        public static class Cards
        {
            public const int DefaultWidth = 360;
            public const int StandardHeight = 80;
            public const int CompactHeight = 64;
            public const int IndicatorWidth = 4;
            public const int IconSize = 40;
            public const int SmallIconSize = 32;

            public static class Fonts
            {
                public static readonly Font Title = new Font("Segoe UI", 11F, FontStyle.Bold);
                public static readonly Font Body = new Font("Segoe UI", 9F, FontStyle.Regular);
                public static readonly Font BodyBold = new Font("Segoe UI", 9F, FontStyle.Bold);
                public static readonly Font Info = new Font("Segoe UI", 8F, FontStyle.Regular);
            }
        }

    }
}
