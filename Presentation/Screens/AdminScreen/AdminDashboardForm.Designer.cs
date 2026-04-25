namespace Presentation.Screens.AdminScreen
{
    partial class AdminDashboardForm : BaseForm
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
            this.Text = "AdminDashboardForm : BaseForm";
            
        }
 // ── DataGridViews ─────────────────────────────────────────────────────
        private DataGridView _dgvUsers     = null!;
        private DataGridView _dgvDrivers   = null!;
        private DataGridView _dgvPassengers = null!;
        private DataGridView _dgvTrips     = null!;
        private DataGridView _dgvFareRules = null!;

        // ── Stats labels ──────────────────────────────────────────────────────
        private Label _lblStatsTotalUsers    = null!;
        private Label _lblStatsActiveDrivers = null!;
        private Label _lblStatsOnTripDrivers = null!;
        private Label _lblStatsOngoingTrips  = null!;

        // ── Report labels (Trips page) ────────────────────────────────────────
        private Label _lblTotalTrips   = null!;
        private Label _lblTotalRevenue = null!;
        private Label _lblDriverIncome = null!;
        private Label _lblCommission   = null!;

        // ── Search boxes ──────────────────────────────────────────────────────
        private TextBox _txtSearchUsers      = null!;
        private TextBox _txtSearchDrivers    = null!;
        private TextBox _txtSearchPassengers = null!;
        private TextBox _txtSearchTrips      = null!;

        // ── Navigation (Panel-based — NO TabControl) ──────────────────────────
        private readonly Dictionary<int, Panel>  _contentPanels  = new();
        private readonly Dictionary<int, Button> _navButtons     = new();
        private int _currentSection = TAB_DASHBOARD;
        private Label _lblSectionTitle = null!;

        #endregion
    }
}