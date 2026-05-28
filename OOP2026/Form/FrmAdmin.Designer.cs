using OOP2026;

namespace OOP2026
{
    partial class FrmAdmin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblAdminRole = new System.Windows.Forms.Label();
            this.btnCloseAdmin = new System.Windows.Forms.Button();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btnNavStats = new System.Windows.Forms.Button();
            this.btnNavUsers = new System.Windows.Forms.Button();
            this.btnNavTrips = new System.Windows.Forms.Button();
            this.btnNavFees = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.flpCards = new System.Windows.Forms.FlowLayoutPanel();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.tabTrips = new System.Windows.Forms.TabPage();
            this.tabPricing = new System.Windows.Forms.TabPage();
            this.tableLayoutPricing = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.cboVehicleType = new System.Windows.Forms.ComboBox();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.nudBasePrice = new System.Windows.Forms.NumericUpDown();
            this.lblKmPrice = new System.Windows.Forms.Label();
            this.nudKmPrice = new System.Windows.Forms.NumericUpDown();
            this.lblCommission = new System.Windows.Forms.Label();
            this.nudCommission = new System.Windows.Forms.NumericUpDown();
            this.btnSavePolicy = new System.Windows.Forms.Button();
            this.pnlTopBar.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.tabPricing.SuspendLayout();
            this.tableLayoutPricing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBasePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCommission)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(236)))));
            this.pnlTopBar.Controls.Add(this.lblAdminTitle);
            this.pnlTopBar.Controls.Add(this.lblAdminRole);
            this.pnlTopBar.Controls.Add(this.btnCloseAdmin);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1152, 71);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.White;
            this.lblAdminTitle.Location = new System.Drawing.Point(9, 9);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(243, 37);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "Quản trị hệ thống";
            // 
            // lblAdminRole
            // 
            this.lblAdminRole.AutoSize = true;
            this.lblAdminRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAdminRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.lblAdminRole.Location = new System.Drawing.Point(12, 46);
            this.lblAdminRole.Name = "lblAdminRole";
            this.lblAdminRole.Size = new System.Drawing.Size(93, 20);
            this.lblAdminRole.TabIndex = 1;
            this.lblAdminRole.Text = "Quản trị viên";
            // 
            // btnCloseAdmin
            // 
            this.btnCloseAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(236)))));
            this.btnCloseAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseAdmin.FlatAppearance.BorderSize = 0;
            this.btnCloseAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseAdmin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCloseAdmin.ForeColor = System.Drawing.Color.White;
            this.btnCloseAdmin.Location = new System.Drawing.Point(2061, 18);
            this.btnCloseAdmin.Name = "btnCloseAdmin";
            this.btnCloseAdmin.Size = new System.Drawing.Size(34, 35);
            this.btnCloseAdmin.TabIndex = 2;
            this.btnCloseAdmin.Text = "X";
            this.btnCloseAdmin.UseVisualStyleBackColor = false;
            this.btnCloseAdmin.Click += new System.EventHandler(this.BtnCloseAdmin_Click);
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.White;
            this.pnlNav.Controls.Add(this.btnNavStats);
            this.pnlNav.Controls.Add(this.btnNavUsers);
            this.pnlNav.Controls.Add(this.btnNavTrips);
            this.pnlNav.Controls.Add(this.btnNavFees);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNav.Location = new System.Drawing.Point(0, 71);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(1152, 59);
            this.pnlNav.TabIndex = 1;
            // 
            // btnNavStats
            // 
            this.btnNavStats.BackColor = System.Drawing.Color.White;
            this.btnNavStats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavStats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(236)))));
            this.btnNavStats.Location = new System.Drawing.Point(23, 6);
            this.btnNavStats.Name = "btnNavStats";
            this.btnNavStats.Size = new System.Drawing.Size(137, 47);
            this.btnNavStats.TabIndex = 0;
            this.btnNavStats.Text = "Thống kê";
            this.btnNavStats.UseVisualStyleBackColor = false;
            this.btnNavStats.Click += new System.EventHandler(this.BtnNavStats_Click);
            // 
            // btnNavUsers
            // 
            this.btnNavUsers.BackColor = System.Drawing.Color.White;
            this.btnNavUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUsers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.btnNavUsers.Location = new System.Drawing.Point(171, 6);
            this.btnNavUsers.Name = "btnNavUsers";
            this.btnNavUsers.Size = new System.Drawing.Size(137, 47);
            this.btnNavUsers.TabIndex = 1;
            this.btnNavUsers.Text = "Người dùng";
            this.btnNavUsers.UseVisualStyleBackColor = false;
            this.btnNavUsers.Click += new System.EventHandler(this.BtnNavUsers_Click);
            // 
            // btnNavTrips
            // 
            this.btnNavTrips.BackColor = System.Drawing.Color.White;
            this.btnNavTrips.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavTrips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTrips.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavTrips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.btnNavTrips.Location = new System.Drawing.Point(320, 6);
            this.btnNavTrips.Name = "btnNavTrips";
            this.btnNavTrips.Size = new System.Drawing.Size(137, 47);
            this.btnNavTrips.TabIndex = 2;
            this.btnNavTrips.Text = "Chuyến đi";
            this.btnNavTrips.UseVisualStyleBackColor = false;
            this.btnNavTrips.Click += new System.EventHandler(this.BtnNavTrips_Click);
            // 
            // btnNavFees
            // 
            this.btnNavFees.BackColor = System.Drawing.Color.White;
            this.btnNavFees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavFees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.btnNavFees.Location = new System.Drawing.Point(469, 6);
            this.btnNavFees.Name = "btnNavFees";
            this.btnNavFees.Size = new System.Drawing.Size(137, 47);
            this.btnNavFees.TabIndex = 3;
            this.btnNavFees.Text = "Biểu phí";
            this.btnNavFees.UseVisualStyleBackColor = false;
            this.btnNavFees.Click += new System.EventHandler(this.BtnNavFees_Click);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl.Controls.Add(this.tabStatistics);
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Controls.Add(this.tabTrips);
            this.tabControl.Controls.Add(this.tabPricing);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl.Location = new System.Drawing.Point(0, 130);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1152, 624);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 2;
            // 
            // tabStatistics
            // 
            this.tabStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.tabStatistics.Controls.Add(this.flpCards);
            this.tabStatistics.Location = new System.Drawing.Point(4, 4);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStatistics.Size = new System.Drawing.Size(1144, 0);
            this.tabStatistics.TabIndex = 0;
            // 
            // flpCards
            // 
            this.flpCards.AutoScroll = true;
            this.flpCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCards.Location = new System.Drawing.Point(3, 4);
            this.flpCards.Name = "flpCards";
            this.flpCards.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.flpCards.Size = new System.Drawing.Size(1138, 0);
            this.flpCards.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.tabUsers.Location = new System.Drawing.Point(4, 4);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabUsers.Size = new System.Drawing.Size(1144, 0);
            this.tabUsers.TabIndex = 1;
            // 
            // tabTrips
            // 
            this.tabTrips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.tabTrips.Location = new System.Drawing.Point(4, 4);
            this.tabTrips.Name = "tabTrips";
            this.tabTrips.Size = new System.Drawing.Size(1144, 0);
            this.tabTrips.TabIndex = 2;
            // 
            // tabPricing
            // 
            this.tabPricing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.tabPricing.Controls.Add(this.tableLayoutPricing);
            this.tabPricing.Location = new System.Drawing.Point(4, 4);
            this.tabPricing.Name = "tabPricing";
            this.tabPricing.Size = new System.Drawing.Size(1144, 0);
            this.tabPricing.TabIndex = 3;
            // 
            // tableLayoutPricing
            // 
            this.tableLayoutPricing.BackColor = System.Drawing.Color.White;
            this.tableLayoutPricing.ColumnCount = 4;
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.Controls.Add(this.lblVehicleType, 0, 0);
            this.tableLayoutPricing.Controls.Add(this.cboVehicleType, 0, 1);
            this.tableLayoutPricing.Controls.Add(this.lblBasePrice, 1, 0);
            this.tableLayoutPricing.Controls.Add(this.nudBasePrice, 1, 1);
            this.tableLayoutPricing.Controls.Add(this.lblKmPrice, 2, 0);
            this.tableLayoutPricing.Controls.Add(this.nudKmPrice, 2, 1);
            this.tableLayoutPricing.Controls.Add(this.lblCommission, 3, 0);
            this.tableLayoutPricing.Controls.Add(this.nudCommission, 3, 1);
            this.tableLayoutPricing.Controls.Add(this.btnSavePolicy, 0, 2);
            this.tableLayoutPricing.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPricing.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPricing.Name = "tableLayoutPricing";
            this.tableLayoutPricing.Padding = new System.Windows.Forms.Padding(14);
            this.tableLayoutPricing.RowCount = 3;
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPricing.Size = new System.Drawing.Size(1144, 176);
            this.tableLayoutPricing.TabIndex = 0;
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleType.ForeColor = System.Drawing.Color.Black;
            this.lblVehicleType.Location = new System.Drawing.Point(17, 20);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(114, 27);
            this.lblVehicleType.TabIndex = 0;
            this.lblVehicleType.Text = "Loại xe:";
            // 
            // cboVehicleType
            // 
            this.cboVehicleType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboVehicleType.Location = new System.Drawing.Point(17, 52);
            this.cboVehicleType.Name = "cboVehicleType";
            this.cboVehicleType.Size = new System.Drawing.Size(273, 31);
            this.cboVehicleType.TabIndex = 1;
            // 
            // lblBasePrice
            // 
            this.lblBasePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBasePrice.ForeColor = System.Drawing.Color.Black;
            this.lblBasePrice.Location = new System.Drawing.Point(296, 14);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(114, 27);
            this.lblBasePrice.TabIndex = 2;
            this.lblBasePrice.Text = "Giá mở cửa (VND):";
            // 
            // nudBasePrice
            // 
            this.nudBasePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudBasePrice.Location = new System.Drawing.Point(296, 50);
            this.nudBasePrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudBasePrice.Name = "nudBasePrice";
            this.nudBasePrice.Size = new System.Drawing.Size(137, 30);
            this.nudBasePrice.TabIndex = 3;
            this.nudBasePrice.ThousandsSeparator = true;
            // 
            // lblKmPrice
            // 
            this.lblKmPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKmPrice.ForeColor = System.Drawing.Color.Black;
            this.lblKmPrice.Location = new System.Drawing.Point(575, 14);
            this.lblKmPrice.Name = "lblKmPrice";
            this.lblKmPrice.Size = new System.Drawing.Size(114, 27);
            this.lblKmPrice.TabIndex = 4;
            this.lblKmPrice.Text = "Giá mỗi KM (VND):";
            // 
            // nudKmPrice
            // 
            this.nudKmPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudKmPrice.Location = new System.Drawing.Point(575, 50);
            this.nudKmPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudKmPrice.Name = "nudKmPrice";
            this.nudKmPrice.Size = new System.Drawing.Size(137, 30);
            this.nudKmPrice.TabIndex = 5;
            this.nudKmPrice.ThousandsSeparator = true;
            // 
            // lblCommission
            // 
            this.lblCommission.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCommission.ForeColor = System.Drawing.Color.Black;
            this.lblCommission.Location = new System.Drawing.Point(854, 14);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(114, 27);
            this.lblCommission.TabIndex = 6;
            this.lblCommission.Text = "Chiết khấu (%):";
            // 
            // nudCommission
            // 
            this.nudCommission.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCommission.Location = new System.Drawing.Point(854, 50);
            this.nudCommission.Name = "nudCommission";
            this.nudCommission.Size = new System.Drawing.Size(137, 30);
            this.nudCommission.TabIndex = 7;
            // 
            // btnSavePolicy
            // 
            this.btnSavePolicy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSavePolicy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(236)))));
            this.btnSavePolicy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSavePolicy.FlatAppearance.BorderSize = 0;
            this.btnSavePolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePolicy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePolicy.ForeColor = System.Drawing.Color.White;
            this.btnSavePolicy.Location = new System.Drawing.Point(17, 106);
            this.btnSavePolicy.Name = "btnSavePolicy";
            this.btnSavePolicy.Size = new System.Drawing.Size(137, 38);
            this.btnSavePolicy.TabIndex = 8;
            this.btnSavePolicy.Text = "Thêm biểu phí";
            this.btnSavePolicy.UseVisualStyleBackColor = false;
            this.btnSavePolicy.Click += new System.EventHandler(this.BtnSavePolicy_Click);
            // 
            // FrmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1152, 754);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.pnlTopBar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adm Dashboard";
            this.Load += new System.EventHandler(this.FrmAdmin_Load);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlNav.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.tabPricing.ResumeLayout(false);
            this.tableLayoutPricing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBasePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCommission)).EndInit();
            this.ResumeLayout(false);

        }

        #region Controls declaration
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Label lblAdminRole;
        private System.Windows.Forms.Button btnCloseAdmin;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.Button btnNavStats;
        private System.Windows.Forms.Button btnNavUsers;
        private System.Windows.Forms.Button btnNavTrips;
        private System.Windows.Forms.Button btnNavFees;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabTrips;
        private System.Windows.Forms.TabPage tabPricing;
        private System.Windows.Forms.FlowLayoutPanel flpCards;
        private OOP2026.ucPolicyCard ucPolicy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPricing;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.ComboBox cboVehicleType;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.NumericUpDown nudBasePrice;
        private System.Windows.Forms.Label lblKmPrice;
        private System.Windows.Forms.NumericUpDown nudKmPrice;
        private System.Windows.Forms.Label lblCommission;
        private System.Windows.Forms.NumericUpDown nudCommission;
        private System.Windows.Forms.Button btnSavePolicy;
        private OOP2026.ucStatCard statCard;
        private OOP2026.ucUserCount userCount;
        private OOP2026.ucTripStatus statusCard;

        #endregion
    }
}