using System.Drawing;

namespace Presentation.Constants
{
    /// <summary>
    /// UI color constants for consistent theming across the application.
    /// </summary>
    public static class UiConstants
    {
        // Primary colors (teal theme)
        public static readonly Color PrimaryNormal = Color.FromArgb(0, 150, 136);
        public static readonly Color PrimaryHover = Color.FromArgb(0, 120, 109);
        public static readonly Color PrimaryPressed = Color.FromArgb(0, 90, 85);

        // Success colors (green)
        public static readonly Color SuccessNormal = Color.FromArgb(0, 200, 0);
        public static readonly Color SuccessHover = Color.FromArgb(0, 153, 0);
        public static readonly Color SuccessPressed = Color.FromArgb(0, 102, 0);

        // Danger colors (red)
        public static readonly Color DangerNormal = Color.FromArgb(220, 50, 50);
        public static readonly Color DangerHover = Color.FromArgb(200, 0, 0);
        public static readonly Color DangerPressed = Color.FromArgb(153, 0, 0);

        // Error colors
        public static readonly Color ErrorRed = Color.Red;

        // Background colors
        public static readonly Color BackgroundWhite = Color.White;
        public static readonly Color BackgroundLightGray = Color.FromArgb(245, 245, 245);
        public static readonly Color BackgroundVeryLightGray = Color.FromArgb(250, 250, 250);

        // Text colors
        public static readonly Color TextBlack = Color.Black;
        public static readonly Color TextGray = Color.Gray;
        public static readonly Color TextDarkGray = Color.FromArgb(64, 64, 64);
        public static readonly Color TextWhite = Color.White;

        // Border colors
        public static readonly Color BorderLightGray = Color.FromArgb(224, 224, 224);
        public static readonly Color BorderGray = Color.FromArgb(189, 189, 189);

        // Sizes
        public static readonly int PasswordToggleButtonSize = 28;
        public static readonly int FormPadding = 10;
        public static readonly int ControlSpacing = 8;
        public static readonly int BorderRadius = 4;
    }
}