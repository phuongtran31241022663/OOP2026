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

public static class Layout
        {
            public const int FormPadding = 10;
            public const int ControlSpacing = 8;
            public const int BorderRadius = 4;
            public const int PasswordToggleButtonSize = 28;
            public static readonly Point ZeroPoint = new Point(0, 0);
            public const float FullPercent = 100F;
            public const int PasswordToggleColumnWidth = 45;
            public const float LabelColumnPercent = 35F;
            public const float InputColumnPercent = 65F;
            public static readonly Padding VehicleInfoPanelMargin = new Padding(3, 10, 3, 3);
        }

        public static class Typography
        {
            public static readonly Font Logo = new Font("Segoe UI", 22F, FontStyle.Bold);
            public static readonly Font Header = new Font("Segoe UI", 14F, FontStyle.Bold);
            public static readonly Font Body = new Font("Segoe UI", 12F, FontStyle.Regular);
            public static readonly Font BodyBold = new Font("Segoe UI", 12F, FontStyle.Bold);
            public static readonly Font Small = new Font("Segoe UI", 10, FontStyle.Regular);
            public static readonly Font Tiny = new Font("Segoe UI", 8F, FontStyle.Regular);
        }

        public static class Sizes
        {
            public static readonly Size DefaultForm = new Size(900, 600);
            public static readonly Size PasswordToggleButton = new Size(39, 34);
            public const int MinContentPanelWidth = 500;
        }

        public static class Heights
        {
            public const int HeaderRow = 100;
            public const int FooterRow = 40;
            public const int TitleRow = 50;
            public const int LabelRow = 30;
            public const int InputControlRow = 40;
            public const int ActionButtonRow = 70;
            public const int LinkRow = 40;
            public const int VehicleInfoRow = 40; // For each row in pnlVehicleInfo
        }
    }
}