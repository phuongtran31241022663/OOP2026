namespace OOP2026
{
    partial class FrmMultiRole
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this._passengerPanel = new OOP2026.ucPassengerHome();
            this._map = new OOP2026.ucMap();
            this._driverHome = new OOP2026.ucDriverHome();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.tlpTopBar = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblDemo = new System.Windows.Forms.Label();
            this.lblSearchingStatus = new System.Windows.Forms.Label();
            this.pnlPassengerInfo = new System.Windows.Forms.Panel();
            this.flpPassenger = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPassengerIcon = new System.Windows.Forms.Label();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.lnkPassengerChange = new System.Windows.Forms.LinkLabel();
            this.pnlDriverInfo = new System.Windows.Forms.Panel();
            this.flpDriver = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.pnlOnlineDot = new System.Windows.Forms.Panel();
            this.lblOnline = new System.Windows.Forms.Label();
            this.lnkDriverChange = new System.Windows.Forms.LinkLabel();
            this.pnlRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.lblDriverCount = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.tlpTopBar.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlPassengerInfo.SuspendLayout();
            this.flpPassenger.SuspendLayout();
            this.pnlDriverInfo.SuspendLayout();
            this.flpDriver.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpMain.Controls.Add(this._passengerPanel, 0, 0);
            this.tlpMain.Controls.Add(this._map, 1, 0);
            this.tlpMain.Controls.Add(this._driverHome, 2, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 50);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1280, 670);
            this.tlpMain.TabIndex = 0;
            // 
            // _passengerPanel
            // 
            this._passengerPanel.BackColor = System.Drawing.Color.White;
            this._passengerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._passengerPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._passengerPanel.Location = new System.Drawing.Point(3, 3);
            this._passengerPanel.MinimumSize = new System.Drawing.Size(300, 480);
            this._passengerPanel.Name = "_passengerPanel";
            this._passengerPanel.Size = new System.Drawing.Size(300, 664);
            this._passengerPanel.TabIndex = 0;
            // 
            // _map
            // 
            this._map.Dock = System.Windows.Forms.DockStyle.Fill;
            this._map.Location = new System.Drawing.Point(291, 3);
            this._map.Name = "_map";
            this._map.Size = new System.Drawing.Size(698, 664);
            this._map.TabIndex = 1;
            // 
            // _driverHome
            // 
            this._driverHome.BackColor = System.Drawing.Color.White;
            this._driverHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this._driverHome.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._driverHome.Location = new System.Drawing.Point(995, 3);
            this._driverHome.Name = "_driverHome";
            this._driverHome.Size = new System.Drawing.Size(282, 664);
            this._driverHome.TabIndex = 2;
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlTopBar.Controls.Add(this.tlpTopBar);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1280, 50);
            this.pnlTopBar.TabIndex = 1;
            // 
            // tlpTopBar
            // 
            this.tlpTopBar.ColumnCount = 2;
            this.tlpTopBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpTopBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpTopBar.Controls.Add(this.pnlLeft, 0, 0);
            this.tlpTopBar.Controls.Add(this.pnlRight, 1, 0);
            this.tlpTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopBar.Location = new System.Drawing.Point(0, 0);
            this.tlpTopBar.Name = "tlpTopBar";
            this.tlpTopBar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tlpTopBar.RowCount = 1;
            this.tlpTopBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopBar.Size = new System.Drawing.Size(1280, 50);
            this.tlpTopBar.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblLogo);
            this.pnlLeft.Controls.Add(this.lblDemo);
            this.pnlLeft.Controls.Add(this.lblSearchingStatus);
            this.pnlLeft.Controls.Add(this.pnlPassengerInfo);
            this.pnlLeft.Controls.Add(this.pnlDriverInfo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(10, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(756, 50);
            this.pnlLeft.TabIndex = 0;
            this.pnlLeft.WrapContents = false;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(3, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(195, 37);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "RideGo Demo";
            // 
            // lblDemo
            // 
            this.lblDemo.Location = new System.Drawing.Point(204, 0);
            this.lblDemo.Name = "lblDemo";
            this.lblDemo.Size = new System.Drawing.Size(100, 23);
            this.lblDemo.TabIndex = 1;
            this.lblDemo.Visible = false;
            // 
            // lblSearchingStatus
            // 
            this.lblSearchingStatus.AutoSize = true;
            this.lblSearchingStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearchingStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(190)))), ((int)(((byte)(123)))));
            this.lblSearchingStatus.Location = new System.Drawing.Point(307, 18);
            this.lblSearchingStatus.Margin = new System.Windows.Forms.Padding(0, 18, 20, 0);
            this.lblSearchingStatus.Name = "lblSearchingStatus";
            this.lblSearchingStatus.Size = new System.Drawing.Size(150, 23);
            this.lblSearchingStatus.TabIndex = 2;
            this.lblSearchingStatus.Text = "Đang tìm tài xế...";
            this.lblSearchingStatus.Visible = false;
            // 
            // pnlPassengerInfo
            // 
            this.pnlPassengerInfo.AutoSize = true;
            this.pnlPassengerInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(190)))), ((int)(((byte)(123)))));
            this.pnlPassengerInfo.Controls.Add(this.flpPassenger);
            this.pnlPassengerInfo.Location = new System.Drawing.Point(477, 5);
            this.pnlPassengerInfo.Margin = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.pnlPassengerInfo.Name = "pnlPassengerInfo";
            this.pnlPassengerInfo.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.pnlPassengerInfo.Size = new System.Drawing.Size(144, 35);
            this.pnlPassengerInfo.TabIndex = 3;
            // 
            // flpPassenger
            // 
            this.flpPassenger.AutoSize = true;
            this.flpPassenger.Controls.Add(this.lblPassengerIcon);
            this.flpPassenger.Controls.Add(this.lblPassengerName);
            this.flpPassenger.Controls.Add(this.lnkPassengerChange);
            this.flpPassenger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPassenger.Location = new System.Drawing.Point(10, 5);
            this.flpPassenger.Name = "flpPassenger";
            this.flpPassenger.Size = new System.Drawing.Size(124, 25);
            this.flpPassenger.TabIndex = 0;
            this.flpPassenger.WrapContents = false;
            // 
            // lblPassengerIcon
            // 
            this.lblPassengerIcon.AutoSize = true;
            this.lblPassengerIcon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPassengerIcon.ForeColor = System.Drawing.Color.White;
            this.lblPassengerIcon.Location = new System.Drawing.Point(0, 2);
            this.lblPassengerIcon.Margin = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.lblPassengerIcon.Name = "lblPassengerIcon";
            this.lblPassengerIcon.Size = new System.Drawing.Size(33, 23);
            this.lblPassengerIcon.TabIndex = 0;
            this.lblPassengerIcon.Text = "👤";
            // 
            // lblPassengerName
            // 
            this.lblPassengerName.AutoSize = true;
            this.lblPassengerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPassengerName.ForeColor = System.Drawing.Color.White;
            this.lblPassengerName.Location = new System.Drawing.Point(41, 0);
            this.lblPassengerName.Name = "lblPassengerName";
            this.lblPassengerName.Size = new System.Drawing.Size(37, 23);
            this.lblPassengerName.TabIndex = 1;
            this.lblPassengerName.Text = "Tên";
            // 
            // lnkPassengerChange
            // 
            this.lnkPassengerChange.AutoSize = true;
            this.lnkPassengerChange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkPassengerChange.LinkColor = System.Drawing.Color.White;
            this.lnkPassengerChange.Location = new System.Drawing.Point(91, 0);
            this.lnkPassengerChange.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lnkPassengerChange.Name = "lnkPassengerChange";
            this.lnkPassengerChange.Size = new System.Drawing.Size(33, 20);
            this.lnkPassengerChange.TabIndex = 2;
            this.lnkPassengerChange.TabStop = true;
            this.lnkPassengerChange.Text = "Đổi";
            this.lnkPassengerChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSwitchAcc_LinkClicked);
            // 
            // pnlDriverInfo
            // 
            this.pnlDriverInfo.AutoSize = true;
            this.pnlDriverInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(28)))));
            this.pnlDriverInfo.Controls.Add(this.flpDriver);
            this.pnlDriverInfo.Location = new System.Drawing.Point(631, 5);
            this.pnlDriverInfo.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlDriverInfo.Name = "pnlDriverInfo";
            this.pnlDriverInfo.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.pnlDriverInfo.Size = new System.Drawing.Size(199, 34);
            this.pnlDriverInfo.TabIndex = 4;
            // 
            // flpDriver
            // 
            this.flpDriver.AutoSize = true;
            this.flpDriver.Controls.Add(this.lblDriverName);
            this.flpDriver.Controls.Add(this.pnlOnlineDot);
            this.flpDriver.Controls.Add(this.lblOnline);
            this.flpDriver.Controls.Add(this.lnkDriverChange);
            this.flpDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDriver.Location = new System.Drawing.Point(10, 5);
            this.flpDriver.Name = "flpDriver";
            this.flpDriver.Size = new System.Drawing.Size(179, 24);
            this.flpDriver.TabIndex = 0;
            this.flpDriver.WrapContents = false;
            // 
            // lblDriverName
            // 
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDriverName.ForeColor = System.Drawing.Color.White;
            this.lblDriverName.Location = new System.Drawing.Point(3, 0);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(37, 23);
            this.lblDriverName.TabIndex = 0;
            this.lblDriverName.Text = "Tên";
            // 
            // pnlOnlineDot
            // 
            this.pnlOnlineDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(190)))), ((int)(((byte)(123)))));
            this.pnlOnlineDot.Location = new System.Drawing.Point(48, 8);
            this.pnlOnlineDot.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.pnlOnlineDot.Name = "pnlOnlineDot";
            this.pnlOnlineDot.Size = new System.Drawing.Size(8, 8);
            this.pnlOnlineDot.TabIndex = 1;
            // 
            // lblOnline
            // 
            this.lblOnline.AutoSize = true;
            this.lblOnline.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblOnline.ForeColor = System.Drawing.Color.White;
            this.lblOnline.Location = new System.Drawing.Point(61, 5);
            this.lblOnline.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(75, 19);
            this.lblOnline.TabIndex = 2;
            this.lblOnline.Text = "Hoạt động";
            // 
            // lnkDriverChange
            // 
            this.lnkDriverChange.AutoSize = true;
            this.lnkDriverChange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkDriverChange.LinkColor = System.Drawing.Color.White;
            this.lnkDriverChange.Location = new System.Drawing.Point(146, 0);
            this.lnkDriverChange.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lnkDriverChange.Name = "lnkDriverChange";
            this.lnkDriverChange.Size = new System.Drawing.Size(33, 20);
            this.lnkDriverChange.TabIndex = 3;
            this.lnkDriverChange.TabStop = true;
            this.lnkDriverChange.Text = "Đổi";
            this.lnkDriverChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSwitchAcc_LinkClicked);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnAdmin);
            this.pnlRight.Controls.Add(this.lblDriverCount);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlRight.Location = new System.Drawing.Point(766, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(504, 50);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.WrapContents = false;
            // 
            // btnAdmin
            // 
            this.btnAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(59)))), ((int)(((byte)(236)))));
            this.btnAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdmin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdmin.ForeColor = System.Drawing.Color.White;
            this.btnAdmin.Location = new System.Drawing.Point(411, 3);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(90, 30);
            this.btnAdmin.TabIndex = 0;
            this.btnAdmin.Text = "Quản trị Adm";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.BtnAdmin_Click);
            // 
            // lblDriverCount
            // 
            this.lblDriverCount.AutoSize = true;
            this.lblDriverCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblDriverCount.ForeColor = System.Drawing.Color.White;
            this.lblDriverCount.Location = new System.Drawing.Point(291, 10);
            this.lblDriverCount.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.lblDriverCount.Name = "lblDriverCount";
            this.lblDriverCount.Size = new System.Drawing.Size(107, 23);
            this.lblDriverCount.TabIndex = 1;
            this.lblDriverCount.Text = "Tài xế Online";
            // 
            // FrmMultiRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.pnlTopBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmMultiRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP2026 Demo";
            this.tlpMain.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.tlpTopBar.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlPassengerInfo.ResumeLayout(false);
            this.pnlPassengerInfo.PerformLayout();
            this.flpPassenger.ResumeLayout(false);
            this.flpPassenger.PerformLayout();
            this.pnlDriverInfo.ResumeLayout(false);
            this.pnlDriverInfo.PerformLayout();
            this.flpDriver.ResumeLayout(false);
            this.flpDriver.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ucMap _map;
        private ucPassengerHome _passengerPanel;
        private ucDriverHome _driverHome;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.TableLayoutPanel tlpTopBar;
        private System.Windows.Forms.FlowLayoutPanel pnlLeft;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblDemo;
        private System.Windows.Forms.Label lblSearchingStatus;
        private System.Windows.Forms.Panel pnlPassengerInfo;
        private System.Windows.Forms.FlowLayoutPanel flpPassenger;
        private System.Windows.Forms.Label lblPassengerIcon;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.LinkLabel lnkPassengerChange;
        private System.Windows.Forms.Panel pnlDriverInfo;
        private System.Windows.Forms.FlowLayoutPanel flpDriver;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Panel pnlOnlineDot;
        private System.Windows.Forms.Label lblOnline;
        private System.Windows.Forms.LinkLabel lnkDriverChange;
        private System.Windows.Forms.FlowLayoutPanel pnlRight;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Label lblDriverCount;
    }
}
