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

        public static class ButtonSizes
        {
            public static readonly Size Default = new Size(100, 32);
            public static readonly Size Compact = new Size(80, 26);
            public static readonly Size Action = new Size(150, 40);
            public static readonly Size Large = new Size(200, 48);

            public const int StandardHeight = 32;
            public const int CompactHeight = 26;
            public const int ActionHeight = 40;
            public const int LargeHeight = 48;
        }

        public static class ControlHeights
        {
            public const int TextBoxHeight = 23;
            public const int ModernInputHeight = 32;
        }

        public static class GridSettings
        {
            public const int HeaderHeight = 35;
            public const int RowHeight = 30;
            public static readonly Color GridLineColor = Color.FromArgb(240, 240, 240);
            public const int IdColumnWidth = 50;
            public const int ActionColumnWidth = 100;
            public const int DateColumnWidth = 120;
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
            public const int VehicleInfoRow = 40;
            public const int StandardRow = 32;
            public const int DataGridViewRow = 28;
            public const int TabControlHeight = 30;
        }

        public static class ColumnWidths
        {
            public const int LabelDefault = 120;
            public const int InputDefault = 200;
            public const int SmallLabel = 80;
            public const int LargeInput = 300;
        }

        public static class DialogSizes
        {
            public static readonly Size LoginForm = new Size(400, 500);
            public static readonly Size RegisterForm = new Size(500, 700);
            public static readonly Size ConfirmationDialog = new Size(350, 200);
            public static readonly Size FullScreenDialog = new Size(1024, 768);
        }

        public static class IconSizes
        {
            public static readonly Size Small = new Size(16, 16);
            public static readonly Size Medium = new Size(24, 24);
            public static readonly Size Large = new Size(32, 32);
            public static readonly Size ExtraLarge = new Size(48, 48);
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

            public static class Typography
            {
                public static readonly Font Title = new Font("Segoe UI", 11F, FontStyle.Bold);
                public static readonly Font Body = new Font("Segoe UI", 9F, FontStyle.Regular);
                public static readonly Font BodyBold = new Font("Segoe UI", 9F, FontStyle.Bold);
                public static readonly Font Info = new Font("Segoe UI", 8F, FontStyle.Regular);
            }
        }

        public static class Separators
        {
            public const int DefaultHeight = 2;
            public static readonly Color Color = Color.LightGray;
        }

        public static class Shadows
        {
            public static readonly Color LightShadow = Color.FromArgb(20, 0, 0, 0);
            public static readonly Color AmbientShadow = Color.FromArgb(10, 0, 0, 0);
        }
    }
}