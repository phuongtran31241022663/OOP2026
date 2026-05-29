using System.Drawing;
using System.Windows.Forms;

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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblAdminRole = new System.Windows.Forms.Label();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.tlpNavGrid = new System.Windows.Forms.TableLayoutPanel();
            this.btnNavStats = new System.Windows.Forms.Button();
            this.btnNavUsers = new System.Windows.Forms.Button();
            this.btnNavTrips = new System.Windows.Forms.Button();
            this.btnNavFees = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.tlpStatsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStatCardsRow = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSystemRow = new System.Windows.Forms.TableLayoutPanel();
            this.ucAdminUser = new OOP2026.UcAdminUser();
            this.ucActive = new OOP2026.UcActive();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.flpUsers = new System.Windows.Forms.FlowLayoutPanel();
            this.tabTrips = new System.Windows.Forms.TabPage();
            this.flpTrips = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPricing = new System.Windows.Forms.TabPage();
            this.tableLayoutPricing = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.lblKmPrice = new System.Windows.Forms.Label();
            this.lblCommission = new System.Windows.Forms.Label();
            this.cboVehicleType = new System.Windows.Forms.ComboBox();
            this.nudBasePrice = new System.Windows.Forms.NumericUpDown();
            this.nudKmPrice = new System.Windows.Forms.NumericUpDown();
            this.nudCommission = new System.Windows.Forms.NumericUpDown();
            this.btnSavePolicy = new System.Windows.Forms.Button();
            this.flpPolicies = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTopBar.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.tlpNavGrid.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.tlpStatsLayout.SuspendLayout();
            this.pnlSystemRow.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabTrips.SuspendLayout();
            this.tabPricing.SuspendLayout();
            this.tableLayoutPricing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBasePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCommission)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.pnlTopBar.Controls.Add(this.lblAdminTitle);
            this.pnlTopBar.Controls.Add(this.lblAdminRole);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1152, 75);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.White;
            this.lblAdminTitle.Location = new System.Drawing.Point(18, 12);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(248, 37);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "Quản trị Hệ thống";
            // 
            // lblAdminRole
            // 
            this.lblAdminRole.AutoSize = true;
            this.lblAdminRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblAdminRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblAdminRole.Location = new System.Drawing.Point(20, 48);
            this.lblAdminRole.Name = "lblAdminRole";
            this.lblAdminRole.Size = new System.Drawing.Size(134, 20);
            this.lblAdminRole.TabIndex = 1;
            this.lblAdminRole.Text = "Hệ thống chỉnh sửa";
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.White;
            this.pnlNav.Controls.Add(this.tlpNavGrid);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNav.Location = new System.Drawing.Point(0, 75);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(1152, 52);
            this.pnlNav.TabIndex = 1;
            // 
            // tlpNavGrid
            // 
            this.tlpNavGrid.ColumnCount = 4;
            this.tlpNavGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNavGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNavGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNavGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNavGrid.Controls.Add(this.btnNavStats, 0, 0);
            this.tlpNavGrid.Controls.Add(this.btnNavUsers, 1, 0);
            this.tlpNavGrid.Controls.Add(this.btnNavTrips, 2, 0);
            this.tlpNavGrid.Controls.Add(this.btnNavFees, 3, 0);
            this.tlpNavGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNavGrid.Location = new System.Drawing.Point(0, 0);
            this.tlpNavGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNavGrid.Name = "tlpNavGrid";
            this.tlpNavGrid.RowCount = 1;
            this.tlpNavGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNavGrid.Size = new System.Drawing.Size(1152, 52);
            this.tlpNavGrid.TabIndex = 0;
            // 
            // btnNavStats
            // 
            this.btnNavStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnNavStats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNavStats.FlatAppearance.BorderSize = 0;
            this.btnNavStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavStats.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnNavStats.Location = new System.Drawing.Point(0, 0);
            this.btnNavStats.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavStats.Name = "btnNavStats";
            this.btnNavStats.Size = new System.Drawing.Size(288, 52);
            this.btnNavStats.TabIndex = 0;
            this.btnNavStats.Text = "📈 Thống kê";
            this.btnNavStats.UseVisualStyleBackColor = false;
            this.btnNavStats.Click += new System.EventHandler(this.BtnNavStats_Click);
            // 
            // btnNavUsers
            // 
            this.btnNavUsers.BackColor = System.Drawing.Color.White;
            this.btnNavUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNavUsers.FlatAppearance.BorderSize = 0;
            this.btnNavUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavUsers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavUsers.Location = new System.Drawing.Point(288, 0);
            this.btnNavUsers.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavUsers.Name = "btnNavUsers";
            this.btnNavUsers.Size = new System.Drawing.Size(288, 52);
            this.btnNavUsers.TabIndex = 1;
            this.btnNavUsers.Text = "👥 Người dùng";
            this.btnNavUsers.UseVisualStyleBackColor = false;
            this.btnNavUsers.Click += new System.EventHandler(this.BtnNavUsers_Click);
            // 
            // btnNavTrips
            // 
            this.btnNavTrips.BackColor = System.Drawing.Color.White;
            this.btnNavTrips.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNavTrips.FlatAppearance.BorderSize = 0;
            this.btnNavTrips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTrips.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavTrips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavTrips.Location = new System.Drawing.Point(576, 0);
            this.btnNavTrips.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavTrips.Name = "btnNavTrips";
            this.btnNavTrips.Size = new System.Drawing.Size(288, 52);
            this.btnNavTrips.TabIndex = 2;
            this.btnNavTrips.Text = "🗺️ Chuyến đi";
            this.btnNavTrips.UseVisualStyleBackColor = false;
            this.btnNavTrips.Click += new System.EventHandler(this.BtnNavTrips_Click);
            // 
            // btnNavFees
            // 
            this.btnNavFees.BackColor = System.Drawing.Color.White;
            this.btnNavFees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNavFees.FlatAppearance.BorderSize = 0;
            this.btnNavFees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNavFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnNavFees.Location = new System.Drawing.Point(864, 0);
            this.btnNavFees.Margin = new System.Windows.Forms.Padding(0);
            this.btnNavFees.Name = "btnNavFees";
            this.btnNavFees.Size = new System.Drawing.Size(288, 52);
            this.btnNavFees.TabIndex = 3;
            this.btnNavFees.Text = "💳 Biểu phí";
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
            this.tabControl.Location = new System.Drawing.Point(0, 127);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1152, 627);
            this.tabControl.TabIndex = 2;
            // 
            // tabStatistics
            // 
            this.tabStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabStatistics.Controls.Add(this.tlpStatsLayout);
            this.tabStatistics.Location = new System.Drawing.Point(4, 4);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(16);
            this.tabStatistics.Size = new System.Drawing.Size(1144, 622);
            this.tabStatistics.TabIndex = 0;
            // 
            // tlpStatsLayout
            // 
            this.tlpStatsLayout.ColumnCount = 1;
            this.tlpStatsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatsLayout.Controls.Add(this.pnlStatCardsRow, 0, 0);
            this.tlpStatsLayout.Controls.Add(this.pnlSystemRow, 0, 1);
            this.tlpStatsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatsLayout.Location = new System.Drawing.Point(16, 16);
            this.tlpStatsLayout.Name = "tlpStatsLayout";
            this.tlpStatsLayout.RowCount = 2;
            this.tlpStatsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpStatsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatsLayout.Size = new System.Drawing.Size(1112, 590);
            this.tlpStatsLayout.TabIndex = 0;
            // 
            // pnlStatCardsRow
            // 
            this.pnlStatCardsRow.ColumnCount = 4;
            this.pnlStatCardsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlStatCardsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlStatCardsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlStatCardsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlStatCardsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatCardsRow.Location = new System.Drawing.Point(0, 0);
            this.pnlStatCardsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.pnlStatCardsRow.Name = "pnlStatCardsRow";
            this.pnlStatCardsRow.RowCount = 1;
            this.pnlStatCardsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlStatCardsRow.Size = new System.Drawing.Size(1112, 128);
            this.pnlStatCardsRow.TabIndex = 0;
            // 
            // pnlSystemRow
            // 
            this.pnlSystemRow.ColumnCount = 2;
            this.pnlSystemRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlSystemRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlSystemRow.Controls.Add(this.ucAdminUser, 0, 0);
            this.pnlSystemRow.Controls.Add(this.ucActive, 1, 0);
            this.pnlSystemRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSystemRow.Location = new System.Drawing.Point(0, 140);
            this.pnlSystemRow.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlSystemRow.Name = "pnlSystemRow";
            this.pnlSystemRow.RowCount = 1;
            this.pnlSystemRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlSystemRow.Size = new System.Drawing.Size(1112, 442);
            this.pnlSystemRow.TabIndex = 2;
            // 
            // ucAdminUser
            // 
            this.ucAdminUser.BackColor = System.Drawing.Color.White;
            this.ucAdminUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAdminUser.Location = new System.Drawing.Point(3, 3);
            this.ucAdminUser.Name = "ucAdminUser";
            this.ucAdminUser.Size = new System.Drawing.Size(550, 436);
            this.ucAdminUser.TabIndex = 0;
            // 
            // ucActive
            // 
            this.ucActive.BackColor = System.Drawing.Color.White;
            this.ucActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucActive.Location = new System.Drawing.Point(559, 3);
            this.ucActive.Name = "ucActive";
            this.ucActive.Size = new System.Drawing.Size(550, 436);
            this.ucActive.TabIndex = 1;
            // 
            // tabUsers
            // 
            this.tabUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabUsers.Controls.Add(this.flpUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 4);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(16);
            this.tabUsers.Size = new System.Drawing.Size(1144, 622);
            this.tabUsers.TabIndex = 1;
            // 
            // flpUsers
            // 
            this.flpUsers.AutoScroll = true;
            this.flpUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpUsers.Location = new System.Drawing.Point(16, 16);
            this.flpUsers.Name = "flpUsers";
            this.flpUsers.Size = new System.Drawing.Size(1112, 590);
            this.flpUsers.TabIndex = 0;
            // 
            // tabTrips
            // 
            this.tabTrips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabTrips.Controls.Add(this.flpTrips);
            this.tabTrips.Location = new System.Drawing.Point(4, 4);
            this.tabTrips.Name = "tabTrips";
            this.tabTrips.Padding = new System.Windows.Forms.Padding(16);
            this.tabTrips.Size = new System.Drawing.Size(1144, 622);
            this.tabTrips.TabIndex = 2;
            // 
            // flpTrips
            // 
            this.flpTrips.AutoScroll = true;
            this.flpTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTrips.Location = new System.Drawing.Point(16, 16);
            this.flpTrips.Name = "flpTrips";
            this.flpTrips.Size = new System.Drawing.Size(1112, 590);
            this.flpTrips.TabIndex = 0;
            // 
            // tabPricing
            // 
            this.tabPricing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabPricing.Controls.Add(this.tableLayoutPricing);
            this.tabPricing.Controls.Add(this.flpPolicies);
            this.tabPricing.Location = new System.Drawing.Point(4, 4);
            this.tabPricing.Name = "tabPricing";
            this.tabPricing.Padding = new System.Windows.Forms.Padding(20);
            this.tabPricing.Size = new System.Drawing.Size(1144, 622);
            this.tabPricing.TabIndex = 3;
            // 
            // tableLayoutPricing
            // 
            this.tableLayoutPricing.BackColor = System.Drawing.Color.White;
            this.tableLayoutPricing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPricing.ColumnCount = 4;
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPricing.Controls.Add(this.lblVehicleType, 0, 0);
            this.tableLayoutPricing.Controls.Add(this.lblBasePrice, 1, 0);
            this.tableLayoutPricing.Controls.Add(this.lblKmPrice, 2, 0);
            this.tableLayoutPricing.Controls.Add(this.lblCommission, 3, 0);
            this.tableLayoutPricing.Controls.Add(this.cboVehicleType, 0, 1);
            this.tableLayoutPricing.Controls.Add(this.nudBasePrice, 1, 1);
            this.tableLayoutPricing.Controls.Add(this.nudKmPrice, 2, 1);
            this.tableLayoutPricing.Controls.Add(this.nudCommission, 3, 1);
            this.tableLayoutPricing.Controls.Add(this.btnSavePolicy, 0, 2);
            this.tableLayoutPricing.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPricing.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPricing.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPricing.Name = "tableLayoutPricing";
            this.tableLayoutPricing.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPricing.RowCount = 3;
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPricing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPricing.Size = new System.Drawing.Size(1104, 185);
            this.tableLayoutPricing.TabIndex = 0;
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblVehicleType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVehicleType.Location = new System.Drawing.Point(16, 18);
            this.lblVehicleType.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(267, 21);
            this.lblVehicleType.TabIndex = 0;
            this.lblVehicleType.Text = "Loại phương tiện:";
            this.lblVehicleType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblBasePrice
            // 
            this.lblBasePrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBasePrice.AutoSize = true;
            this.lblBasePrice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBasePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBasePrice.Location = new System.Drawing.Point(283, 18);
            this.lblBasePrice.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(267, 21);
            this.lblBasePrice.TabIndex = 1;
            this.lblBasePrice.Text = "Giá mở cửa cơ bản (VNĐ):";
            this.lblBasePrice.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblKmPrice
            // 
            this.lblKmPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKmPrice.AutoSize = true;
            this.lblKmPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKmPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblKmPrice.Location = new System.Drawing.Point(550, 18);
            this.lblKmPrice.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblKmPrice.Name = "lblKmPrice";
            this.lblKmPrice.Size = new System.Drawing.Size(267, 21);
            this.lblKmPrice.TabIndex = 2;
            this.lblKmPrice.Text = "Cước phí mỗi km (VNĐ):";
            this.lblKmPrice.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblCommission
            // 
            this.lblCommission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommission.AutoSize = true;
            this.lblCommission.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCommission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCommission.Location = new System.Drawing.Point(817, 18);
            this.lblCommission.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(269, 21);
            this.lblCommission.TabIndex = 3;
            this.lblCommission.Text = "Chiết khấu hệ thống (%):";
            this.lblCommission.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboVehicleType
            // 
            this.cboVehicleType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboVehicleType.Location = new System.Drawing.Point(19, 53);
            this.cboVehicleType.Name = "cboVehicleType";
            this.cboVehicleType.Size = new System.Drawing.Size(261, 31);
            this.cboVehicleType.TabIndex = 4;
            // 
            // nudBasePrice
            // 
            this.nudBasePrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBasePrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudBasePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudBasePrice.Location = new System.Drawing.Point(286, 53);
            this.nudBasePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudBasePrice.Name = "nudBasePrice";
            this.nudBasePrice.Size = new System.Drawing.Size(261, 30);
            this.nudBasePrice.TabIndex = 5;
            this.nudBasePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudBasePrice.ThousandsSeparator = true;
            // 
            // nudKmPrice
            // 
            this.nudKmPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudKmPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudKmPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudKmPrice.Location = new System.Drawing.Point(553, 53);
            this.nudKmPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudKmPrice.Name = "nudKmPrice";
            this.nudKmPrice.Size = new System.Drawing.Size(261, 30);
            this.nudKmPrice.TabIndex = 6;
            this.nudKmPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudKmPrice.ThousandsSeparator = true;
            // 
            // nudCommission
            // 
            this.nudCommission.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudCommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudCommission.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCommission.Location = new System.Drawing.Point(820, 53);
            this.nudCommission.Name = "nudCommission";
            this.nudCommission.Size = new System.Drawing.Size(265, 30);
            this.nudCommission.TabIndex = 7;
            this.nudCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSavePolicy
            // 
            this.btnSavePolicy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.tableLayoutPricing.SetColumnSpan(this.btnSavePolicy, 4);
            this.btnSavePolicy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSavePolicy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSavePolicy.FlatAppearance.BorderSize = 0;
            this.btnSavePolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePolicy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePolicy.ForeColor = System.Drawing.Color.White;
            this.btnSavePolicy.Location = new System.Drawing.Point(19, 114);
            this.btnSavePolicy.Name = "btnSavePolicy";
            this.btnSavePolicy.Size = new System.Drawing.Size(1066, 49);
            this.btnSavePolicy.TabIndex = 8;
            this.btnSavePolicy.Text = "Cập nhật Biểu phí";
            this.btnSavePolicy.UseVisualStyleBackColor = false;
            this.btnSavePolicy.Click += new System.EventHandler(this.BtnSavePolicy_Click);
            // 
            // flpPolicies
            // 
            this.flpPolicies.AutoScroll = true;
            this.flpPolicies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPolicies.Location = new System.Drawing.Point(20, 205);
            this.flpPolicies.Name = "flpPolicies";
            this.flpPolicies.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flpPolicies.Size = new System.Drawing.Size(1104, 397);
            this.flpPolicies.TabIndex = 1;
            // 
            // FrmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 754);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.pnlTopBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RideGo Admin Console";
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlNav.ResumeLayout(false);
            this.tlpNavGrid.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.tlpStatsLayout.ResumeLayout(false);
            this.pnlSystemRow.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabTrips.ResumeLayout(false);
            this.tabPricing.ResumeLayout(false);
            this.tableLayoutPricing.ResumeLayout(false);
            this.tableLayoutPricing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBasePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKmPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCommission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Label lblAdminRole;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.TableLayoutPanel tlpNavGrid;
        private System.Windows.Forms.Button btnNavStats;
        private System.Windows.Forms.Button btnNavUsers;
        private System.Windows.Forms.Button btnNavTrips;
        private System.Windows.Forms.Button btnNavFees;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabTrips;
        private System.Windows.Forms.TabPage tabPricing;
        private System.Windows.Forms.TableLayoutPanel tlpStatsLayout;
        private System.Windows.Forms.TableLayoutPanel pnlStatCardsRow;
        private System.Windows.Forms.TableLayoutPanel pnlSystemRow;
        private UcAdminUser ucAdminUser;
        private UcActive ucActive;
        private System.Windows.Forms.FlowLayoutPanel flpUsers;
        private System.Windows.Forms.FlowLayoutPanel flpTrips;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPricing;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.Label lblKmPrice;
        private System.Windows.Forms.Label lblCommission;
        private System.Windows.Forms.ComboBox cboVehicleType;
        private System.Windows.Forms.NumericUpDown nudBasePrice;
        private System.Windows.Forms.NumericUpDown nudKmPrice;
        private System.Windows.Forms.NumericUpDown nudCommission;
        private System.Windows.Forms.Button btnSavePolicy;
        private System.Windows.Forms.FlowLayoutPanel flpPolicies;
    }
}
