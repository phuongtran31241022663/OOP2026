namespace Presentation.UserControls
{
    partial class UcAdmin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabTrips;
        private System.Windows.Forms.TabPage tabFareRules;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel pnlUsersToolbar;
        private System.Windows.Forms.Button btnUnlockUser;
        private System.Windows.Forms.Button btnLockUser;
        private System.Windows.Forms.ComboBox cmbUserRole;
        private System.Windows.Forms.TextBox txtSearchUsers;
        private System.Windows.Forms.DataGridView dgvTrips;
        private System.Windows.Forms.Panel pnlTripsToolbar;
        private System.Windows.Forms.Button btnTripDetail;
        private System.Windows.Forms.Button btnCancelTrip;
        private System.Windows.Forms.TextBox txtSearchTrips;
        private System.Windows.Forms.DataGridView dgvFareRules;
        private System.Windows.Forms.Panel pnlFareToolbar;
        private System.Windows.Forms.Button btnDeleteFare;
        private System.Windows.Forms.Button btnEditFare;
        private System.Windows.Forms.Button btnAddFare;
        private System.Windows.Forms.TableLayoutPanel tblStats;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Label lblCompletionRate;
        private System.Windows.Forms.Label lblAvgRating;
        private System.Windows.Forms.Label lblActiveDrivers;
        private System.Windows.Forms.Label lblTotalTrips;
        private System.Windows.Forms.Label lblGMV;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlAdmin = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.pnlUsersToolbar = new System.Windows.Forms.Panel();
            this.btnUnlockUser = new System.Windows.Forms.Button();
            this.btnLockUser = new System.Windows.Forms.Button();
            this.cmbUserRole = new System.Windows.Forms.ComboBox();
            this.txtSearchUsers = new System.Windows.Forms.TextBox();
            this.tabTrips = new System.Windows.Forms.TabPage();
            this.dgvTrips = new System.Windows.Forms.DataGridView();
            this.pnlTripsToolbar = new System.Windows.Forms.Panel();
            this.btnTripDetail = new System.Windows.Forms.Button();
            this.btnCancelTrip = new System.Windows.Forms.Button();
            this.txtSearchTrips = new System.Windows.Forms.TextBox();
            this.tabFareRules = new System.Windows.Forms.TabPage();
            this.dgvFareRules = new System.Windows.Forms.DataGridView();
            this.pnlFareToolbar = new System.Windows.Forms.Panel();
            this.btnDeleteFare = new System.Windows.Forms.Button();
            this.btnEditFare = new System.Windows.Forms.Button();
            this.btnAddFare = new System.Windows.Forms.Button();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.tblStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblCompletionRate = new System.Windows.Forms.Label();
            this.lblAvgRating = new System.Windows.Forms.Label();
            this.lblActiveDrivers = new System.Windows.Forms.Label();
            this.lblTotalTrips = new System.Windows.Forms.Label();
            this.lblGMV = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.tabControlAdmin.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.pnlUsersToolbar.SuspendLayout();
            this.tabTrips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).BeginInit();
            this.pnlTripsToolbar.SuspendLayout();
            this.tabFareRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFareRules)).BeginInit();
            this.pnlFareToolbar.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.tblStats.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAdmin
            // 
            this.tabControlAdmin.Controls.Add(this.tabUsers);
            this.tabControlAdmin.Controls.Add(this.tabTrips);
            this.tabControlAdmin.Controls.Add(this.tabFareRules);
            this.tabControlAdmin.Controls.Add(this.tabStats);
            this.tabControlAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdmin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControlAdmin.Location = new System.Drawing.Point(0, 48);
            this.tabControlAdmin.Name = "tabControlAdmin";
            this.tabControlAdmin.SelectedIndex = 0;
            this.tabControlAdmin.Size = new System.Drawing.Size(1200, 752);
            this.tabControlAdmin.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.pnlUsersToolbar);
            this.tabUsers.Location = new System.Drawing.Point(4, 32);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(8);
            this.tabUsers.Size = new System.Drawing.Size(1192, 716);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Nguoi dung";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(8, 64);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowTemplate.Height = 28;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(1176, 644);
            this.dgvUsers.TabIndex = 1;
            // 
            // pnlUsersToolbar
            // 
            this.pnlUsersToolbar.Controls.Add(this.btnUnlockUser);
            this.pnlUsersToolbar.Controls.Add(this.btnLockUser);
            this.pnlUsersToolbar.Controls.Add(this.cmbUserRole);
            this.pnlUsersToolbar.Controls.Add(this.txtSearchUsers);
            this.pnlUsersToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUsersToolbar.Location = new System.Drawing.Point(8, 8);
            this.pnlUsersToolbar.Name = "pnlUsersToolbar";
            this.pnlUsersToolbar.Size = new System.Drawing.Size(1176, 56);
            this.pnlUsersToolbar.TabIndex = 0;
            // 
            // btnUnlockUser
            // 
            this.btnUnlockUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlockUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnlockUser.Location = new System.Drawing.Point(1072, 8);
            this.btnUnlockUser.Name = "btnUnlockUser";
            this.btnUnlockUser.Size = new System.Drawing.Size(96, 36);
            this.btnUnlockUser.TabIndex = 3;
            this.btnUnlockUser.Text = "Mo khoa";
            this.btnUnlockUser.UseVisualStyleBackColor = true;
            // 
            // btnLockUser
            // 
            this.btnLockUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLockUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockUser.Location = new System.Drawing.Point(968, 8);
            this.btnLockUser.Name = "btnLockUser";
            this.btnLockUser.Size = new System.Drawing.Size(96, 36);
            this.btnLockUser.TabIndex = 2;
            this.btnLockUser.Text = "Khoa";
            this.btnLockUser.UseVisualStyleBackColor = true;
            // 
            // cmbUserRole
            // 
            this.cmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserRole.FormattingEnabled = true;
            this.cmbUserRole.Items.AddRange(new object[] { "Tat ca", "Passenger", "Driver", "Admin" });
            this.cmbUserRole.Location = new System.Drawing.Point(320, 8);
            this.cmbUserRole.Name = "cmbUserRole";
            this.cmbUserRole.Size = new System.Drawing.Size(160, 31);
            this.cmbUserRole.TabIndex = 1;
            // 
            // txtSearchUsers
            // 
            this.txtSearchUsers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchUsers.Location = new System.Drawing.Point(8, 8);
            this.txtSearchUsers.Name = "txtSearchUsers";
            this.txtSearchUsers.Size = new System.Drawing.Size(300, 30);
            this.txtSearchUsers.TabIndex = 0;
            // 
            // tabTrips
            // 
            this.tabTrips.Controls.Add(this.dgvTrips);
            this.tabTrips.Controls.Add(this.pnlTripsToolbar);
            this.tabTrips.Location = new System.Drawing.Point(4, 32);
            this.tabTrips.Name = "tabTrips";
            this.tabTrips.Padding = new System.Windows.Forms.Padding(8);
            this.tabTrips.Size = new System.Drawing.Size(1192, 716);
            this.tabTrips.TabIndex = 1;
            this.tabTrips.Text = "Chuyen di";
            this.tabTrips.UseVisualStyleBackColor = true;
            // 
            // dgvTrips
            // 
            this.dgvTrips.AllowUserToAddRows = false;
            this.dgvTrips.AllowUserToDeleteRows = false;
            this.dgvTrips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrips.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrips.Location = new System.Drawing.Point(8, 64);
            this.dgvTrips.Name = "dgvTrips";
            this.dgvTrips.ReadOnly = true;
            this.dgvTrips.RowHeadersVisible = false;
            this.dgvTrips.RowTemplate.Height = 28;
            this.dgvTrips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrips.Size = new System.Drawing.Size(1176, 644);
            this.dgvTrips.TabIndex = 1;
            // 
            // pnlTripsToolbar
            // 
            this.pnlTripsToolbar.Controls.Add(this.btnTripDetail);
            this.pnlTripsToolbar.Controls.Add(this.btnCancelTrip);
            this.pnlTripsToolbar.Controls.Add(this.txtSearchTrips);
            this.pnlTripsToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTripsToolbar.Location = new System.Drawing.Point(8, 8);
            this.pnlTripsToolbar.Name = "pnlTripsToolbar";
            this.pnlTripsToolbar.Size = new System.Drawing.Size(1176, 56);
            this.pnlTripsToolbar.TabIndex = 0;
            // 
            // btnTripDetail
            // 
            this.btnTripDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTripDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTripDetail.Location = new System.Drawing.Point(1072, 8);
            this.btnTripDetail.Name = "btnTripDetail";
            this.btnTripDetail.Size = new System.Drawing.Size(96, 36);
            this.btnTripDetail.TabIndex = 2;
            this.btnTripDetail.Text = "Chi tiet";
            this.btnTripDetail.UseVisualStyleBackColor = true;
            // 
            // btnCancelTrip
            // 
            this.btnCancelTrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancelTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTrip.ForeColor = System.Drawing.Color.White;
            this.btnCancelTrip.Location = new System.Drawing.Point(968, 8);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Size = new System.Drawing.Size(96, 36);
            this.btnCancelTrip.TabIndex = 1;
            this.btnCancelTrip.Text = "Huy";
            this.btnCancelTrip.UseVisualStyleBackColor = false;
            // 
            // txtSearchTrips
            // 
            this.txtSearchTrips.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearchTrips.Location = new System.Drawing.Point(8, 8);
            this.txtSearchTrips.Name = "txtSearchTrips";
            this.txtSearchTrips.Size = new System.Drawing.Size(300, 30);
            this.txtSearchTrips.TabIndex = 0;
            // 
            // tabFareRules
            // 
            this.tabFareRules.Controls.Add(this.dgvFareRules);
            this.tabFareRules.Controls.Add(this.pnlFareToolbar);
            this.tabFareRules.Location = new System.Drawing.Point(4, 32);
            this.tabFareRules.Name = "tabFareRules";
            this.tabFareRules.Padding = new System.Windows.Forms.Padding(8);
            this.tabFareRules.Size = new System.Drawing.Size(1192, 716);
            this.tabFareRules.TabIndex = 2;
            this.tabFareRules.Text = "Gia cuoc";
            this.tabFareRules.UseVisualStyleBackColor = true;
            // 
            // dgvFareRules
            // 
            this.dgvFareRules.AllowUserToAddRows = false;
            this.dgvFareRules.AllowUserToDeleteRows = false;
            this.dgvFareRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFareRules.BackgroundColor = System.Drawing.Color.White;
            this.dgvFareRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFareRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFareRules.Location = new System.Drawing.Point(8, 64);
            this.dgvFareRules.Name = "dgvFareRules";
            this.dgvFareRules.ReadOnly = true;
            this.dgvFareRules.RowHeadersVisible = false;
            this.dgvFareRules.RowTemplate.Height = 28;
            this.dgvFareRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFareRules.Size = new System.Drawing.Size(1176, 644);
            this.dgvFareRules.TabIndex = 1;
            // 
            // pnlFareToolbar
            // 
            this.pnlFareToolbar.Controls.Add(this.btnDeleteFare);
            this.pnlFareToolbar.Controls.Add(this.btnEditFare);
            this.pnlFareToolbar.Controls.Add(this.btnAddFare);
            this.pnlFareToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFareToolbar.Location = new System.Drawing.Point(8, 8);
            this.pnlFareToolbar.Name = "pnlFareToolbar";
            this.pnlFareToolbar.Size = new System.Drawing.Size(1176, 56);
            this.pnlFareToolbar.TabIndex = 0;
            // 
            // btnDeleteFare
            // 
            this.btnDeleteFare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnDeleteFare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteFare.ForeColor = System.Drawing.Color.White;
            this.btnDeleteFare.Location = new System.Drawing.Point(1072, 8);
            this.btnDeleteFare.Name = "btnDeleteFare";
            this.btnDeleteFare.Size = new System.Drawing.Size(96, 36);
            this.btnDeleteFare.TabIndex = 2;
            this.btnDeleteFare.Text = "Xoa";
            this.btnDeleteFare.UseVisualStyleBackColor = false;
            // 
            // btnEditFare
            // 
            this.btnEditFare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditFare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditFare.Location = new System.Drawing.Point(968, 8);
            this.btnEditFare.Name = "btnEditFare";
            this.btnEditFare.Size = new System.Drawing.Size(96, 36);
            this.btnEditFare.TabIndex = 1;
            this.btnEditFare.Text = "Sua";
            this.btnEditFare.UseVisualStyleBackColor = true;
            // 
            // btnAddFare
            // 
            this.btnAddFare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnAddFare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddFare.ForeColor = System.Drawing.Color.White;
            this.btnAddFare.Location = new System.Drawing.Point(864, 8);
            this.btnAddFare.Name = "btnAddFare";
            this.btnAddFare.Size = new System.Drawing.Size(96, 36);
            this.btnAddFare.TabIndex = 0;
            this.btnAddFare.Text = "Them";
            this.btnAddFare.UseVisualStyleBackColor = false;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.tblStats);
            this.tabStats.Location = new System.Drawing.Point(4, 32);
            this.tabStats.Name = "tabStats";
            this.tabStats.Padding = new System.Windows.Forms.Padding(24);
            this.tabStats.Size = new System.Drawing.Size(1192, 716);
            this.tabStats.TabIndex = 3;
            this.tabStats.Text = "Thong ke";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // tblStats
            // 
            this.tblStats.ColumnCount = 2;
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblStats.Controls.Add(this.lblCompletionRate, 1, 1);
            this.tblStats.Controls.Add(this.lblAvgRating, 0, 1);
            this.tblStats.Controls.Add(this.lblActiveDrivers, 1, 0);
            this.tblStats.Controls.Add(this.lblTotalTrips, 0, 0);
            this.tblStats.Controls.Add(this.lblGMV, 0, 2);
            this.tblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStats.Location = new System.Drawing.Point(24, 24);
            this.tblStats.Name = "tblStats";
            this.tblStats.RowCount = 3;
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tblStats.Size = new System.Drawing.Size(1144, 668);
            this.tblStats.TabIndex = 0;
            // 
            // lblCompletionRate
            // 
            this.lblCompletionRate.AutoSize = true;
            this.lblCompletionRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompletionRate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCompletionRate.Location = new System.Drawing.Point(575, 222);
            this.lblCompletionRate.Name = "lblCompletionRate";
            this.lblCompletionRate.Size = new System.Drawing.Size(566, 222);
            this.lblCompletionRate.TabIndex = 4;
            this.lblCompletionRate.Text = "Ty le hoan thanh: 0%";
            this.lblCompletionRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvgRating
            // 
            this.lblAvgRating.AutoSize = true;
            this.lblAvgRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvgRating.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAvgRating.Location = new System.Drawing.Point(3, 222);
            this.lblAvgRating.Name = "lblAvgRating";
            this.lblAvgRating.Size = new System.Drawing.Size(566, 222);
            this.lblAvgRating.TabIndex = 3;
            this.lblAvgRating.Text = "Diem hai long: 0.0";
            this.lblAvgRating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActiveDrivers
            // 
            this.lblActiveDrivers.AutoSize = true;
            this.lblActiveDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveDrivers.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblActiveDrivers.Location = new System.Drawing.Point(575, 0);
            this.lblActiveDrivers.Name = "lblActiveDrivers";
            this.lblActiveDrivers.Size = new System.Drawing.Size(566, 222);
            this.lblActiveDrivers.TabIndex = 2;
            this.lblActiveDrivers.Text = "Tai xe hoat dong: 0";
            this.lblActiveDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalTrips
            // 
            this.lblTotalTrips.AutoSize = true;
            this.lblTotalTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalTrips.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalTrips.Location = new System.Drawing.Point(3, 0);
            this.lblTotalTrips.Name = "lblTotalTrips";
            this.lblTotalTrips.Size = new System.Drawing.Size(566, 222);
            this.lblTotalTrips.TabIndex = 1;
            this.lblTotalTrips.Text = "Tong chuyen: 0";
            this.lblTotalTrips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGMV
            // 
            this.lblGMV.AutoSize = true;
            this.lblGMV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGMV.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblGMV.Location = new System.Drawing.Point(3, 444);
            this.lblGMV.Name = "lblGMV";
            this.lblGMV.Size = new System.Drawing.Size(566, 224);
            this.lblGMV.TabIndex = 0;
            this.lblGMV.Text = "GMV: 0 VND";
            this.lblGMV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnLogout);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.lblAdminTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 48);
            this.pnlTop.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(1104, 8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(88, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Dang xuat";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1008, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 32);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Lam moi";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.Location = new System.Drawing.Point(12, 12);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(189, 28);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "Quan tri RideGo";
            // 
            // UcAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlAdmin);
            this.Controls.Add(this.pnlTop);
            this.Name = "UcAdmin";
            this.Size = new System.Drawing.Size(1200, 800);
            this.tabControlAdmin.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.pnlUsersToolbar.ResumeLayout(false);
            this.pnlUsersToolbar.PerformLayout();
            this.tabTrips.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).EndInit();
            this.pnlTripsToolbar.ResumeLayout(false);
            this.pnlTripsToolbar.PerformLayout();
            this.tabFareRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFareRules)).EndInit();
            this.pnlFareToolbar.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.tblStats.ResumeLayout(false);
            this.tblStats.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

