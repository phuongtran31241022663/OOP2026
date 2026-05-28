namespace OOP2026
{
    partial class ucDriverHome
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.flpTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnRequests = new System.Windows.Forms.Button();
            this.btnWallet = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tlpDriverContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTop.SuspendLayout();
            this.flpTabs.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = OOP2026.Colors.Orange;
            this.pnlTop.Controls.Add(this.lblStatus);
            this.pnlTop.Controls.Add(this.lblStats);
            this.pnlTop.Controls.Add(this.lblPhone);
            this.pnlTop.Controls.Add(this.lblName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(12);
            this.pnlTop.Size = new System.Drawing.Size(300, 120);
            this.pnlTop.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = OOP2026.Colors.Orange;
            this.lblStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStatus.Font = OOP2026.Typography.Font9Regular;
            this.lblStatus.ForeColor = OOP2026.Colors.White;
            this.lblStatus.Location = new System.Drawing.Point(200, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ngoại tuyến";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Font = OOP2026.Typography.Font9Regular;
            this.lblStats.ForeColor = OOP2026.Colors.White;
            this.lblStats.Location = new System.Drawing.Point(17, 65);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(41, 20);
            this.lblStats.TabIndex = 2;
            this.lblStats.Text = "Thống kê";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = OOP2026.Typography.Font9Regular;
            this.lblPhone.ForeColor = OOP2026.Colors.White;
            this.lblPhone.Location = new System.Drawing.Point(17, 45);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(50, 20);
            this.lblPhone.TabIndex = 1;
            this.lblPhone.Text = "Điện thoại";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = OOP2026.Typography.Font14Bold;
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(15, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(159, 32);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên tài xế";
            // 
            // flpTabs
            // 
            this.flpTabs.BackColor = OOP2026.Colors.White;
            this.flpTabs.Controls.Add(this.btnStatus);
            this.flpTabs.Controls.Add(this.btnRequests);
            this.flpTabs.Controls.Add(this.btnWallet);
            this.flpTabs.Controls.Add(this.btnHistory);
            this.flpTabs.Controls.Add(this.btnProfile);
            this.flpTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpTabs.Location = new System.Drawing.Point(0, 120);
            this.flpTabs.Name = "flpTabs";
            this.flpTabs.Size = new System.Drawing.Size(300, 50);
            this.flpTabs.TabIndex = 1;
            this.flpTabs.WrapContents = false;
            // 
            // btnStatus
            // 
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.Font = OOP2026.Typography.Font10Regular;
            this.btnStatus.Location = new System.Drawing.Point(0, 0);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(0);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(60, 50);
            this.btnStatus.TabIndex = 0;
            this.btnStatus.Tag = "Status";
            this.btnStatus.Text = "Trạng thái";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnRequests
            // 
            this.btnRequests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequests.Font = OOP2026.Typography.Font10Regular;
            this.btnRequests.Location = new System.Drawing.Point(60, 0);
            this.btnRequests.Margin = new System.Windows.Forms.Padding(0);
            this.btnRequests.Name = "btnRequests";
            this.btnRequests.Size = new System.Drawing.Size(60, 50);
            this.btnRequests.TabIndex = 1;
            this.btnRequests.Tag = "Requests";
            this.btnRequests.Text = "Cuốc";
            this.btnRequests.UseVisualStyleBackColor = true;
            this.btnRequests.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnWallet
            // 
            this.btnWallet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWallet.Font = OOP2026.Typography.Font10Regular;
            this.btnWallet.Location = new System.Drawing.Point(120, 0);
            this.btnWallet.Margin = new System.Windows.Forms.Padding(0);
            this.btnWallet.Name = "btnWallet";
            this.btnWallet.Size = new System.Drawing.Size(60, 50);
            this.btnWallet.TabIndex = 2;
            this.btnWallet.Tag = "Wallet";
            this.btnWallet.Text = "Ví";
            this.btnWallet.UseVisualStyleBackColor = true;
            this.btnWallet.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = OOP2026.Typography.Font10Regular;
            this.btnHistory.Location = new System.Drawing.Point(180, 0);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(0);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(60, 50);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Tag = "History";
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = OOP2026.Typography.Font10Regular;
            this.btnProfile.Location = new System.Drawing.Point(240, 0);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(0);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(60, 50);
            this.btnProfile.TabIndex = 4;
            this.btnProfile.Tag = "Profile";
            this.btnProfile.Text = "Hồ sơ";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.Tab_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tlpDriverContent);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 170);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12);
            this.pnlContent.Size = new System.Drawing.Size(300, 430);
            this.pnlContent.TabIndex = 2;
            // 
            // tlpDriverContent
            // 
            this.tlpDriverContent.ColumnCount = 1;
            this.tlpDriverContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDriverContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDriverContent.Location = new System.Drawing.Point(12, 12);
            this.tlpDriverContent.Name = "tlpDriverContent";
            this.tlpDriverContent.RowCount = 2;
            this.tlpDriverContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDriverContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDriverContent.Size = new System.Drawing.Size(276, 406);
            this.tlpDriverContent.TabIndex = 0;
            // 
            // ucDriverHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.flpTabs);
            this.Controls.Add(this.pnlTop);
            this.Font = OOP2026.Typography.Font10Regular;
            this.Name = "ucDriverHome";
            this.Size = new System.Drawing.Size(300, 600);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.flpTabs.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region Controls declaration
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flpTabs;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnRequests;
        private System.Windows.Forms.Button btnWallet;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tlpDriverContent;
        #endregion
    }
}
