using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
namespace Presentation.Screens.AdminScreen
{
    partial class AdminDashboardForm : BaseForm
    {
        // ── Tab index constants ───────────────────────────────────────────────
        private const int TAB_DASHBOARD = 0;
        private const int TAB_USERS = 1;
        private const int TAB_DRIVERS = 2;
        private const int TAB_PASSENGERS = 3;
        private const int TAB_TRIPS = 4;
        private const int TAB_FARE_RULES = 5;
        private const int TAB_REPORTS = 6;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 768);
            this.Text = "Quản trị hệ thống";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        // ── DataGridViews ─────────────────────────────────────────────────────
        private System.Windows.Forms.DataGridView _dgvUsers;
        private System.Windows.Forms.DataGridView _dgvDrivers;
        private System.Windows.Forms.DataGridView _dgvPassengers;
        private System.Windows.Forms.DataGridView _dgvTrips;
        private System.Windows.Forms.DataGridView _dgvFareRules;

        // ── Stats labels (header stats bar) ───────────────────────────────────
        private System.Windows.Forms.Label _lblStatsTotalUsers;
        private System.Windows.Forms.Label _lblStatsActiveDrivers;
        private System.Windows.Forms.Label _lblStatsOnTripDrivers;
        private System.Windows.Forms.Label _lblStatsOngoingTrips;

        // ── Trip report strip labels ──────────────────────────────────────────
        private System.Windows.Forms.Label _lblTotalTrips;
        private System.Windows.Forms.Label _lblTotalRevenue;
        private System.Windows.Forms.Label _lblDriverIncome;
        private System.Windows.Forms.Label _lblCommission;

        // ── Search boxes ──────────────────────────────────────────────────────
        private System.Windows.Forms.TextBox _txtSearchUsers;
        private System.Windows.Forms.TextBox _txtSearchDrivers;
        private System.Windows.Forms.TextBox _txtSearchPassengers;
        private System.Windows.Forms.TextBox _txtSearchTrips;

        // ── Navigation state ──────────────────────────────────────────────────
        private readonly System.Collections.Generic.Dictionary<int, System.Windows.Forms.Panel>
            _contentPanels = new System.Collections.Generic.Dictionary<int, System.Windows.Forms.Panel>();

        private readonly System.Collections.Generic.Dictionary<int, System.Windows.Forms.Button>
            _navButtons = new System.Collections.Generic.Dictionary<int, System.Windows.Forms.Button>();

        private int _currentSection = TAB_DASHBOARD;

        private System.Windows.Forms.Label _lblSectionTitle;
    }
}
