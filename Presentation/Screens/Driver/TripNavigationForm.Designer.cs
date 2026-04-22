namespace Presentation.Screens.Driver
{
    partial class TripNavigationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "TripNavigationForm";
        }
// UI components
        private Panel _topBar;
        private Label _titleLabel;
        private Button _refreshButton;

        private Panel _statsStrip;
        private Label _ReviewLabel;
        private Label _totalTripsLabel;
        private Label _incomeLabel;
        private Label _walletLabel;
        private Label _revenueTodayLabel;

        private Panel _contentArea;
        private Panel _emptyPanel;
        private Label _emptyMessageLabel;

        private Panel _requestPanel;
        private Label _requestInfoLabel;
        private Button _acceptButton;
        private Button _rejectButton;

        private Panel _activeTripPanel;
        private Label _routeInfoLabel;
        private Panel _stepBar;
        private PictureBox[] _stepDots;
        private Label[] _stepLabels;
        private Button _actionButton;
        private ListBox _logListBox;
        #endregion
    }
}