namespace OOP2026
{
    partial class ucTripCard
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.lblNetEarnings = new System.Windows.Forms.Label();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDropoff = new System.Windows.Forms.Label();
            this.lblTripParams = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();

            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();

            // ========== tlpMain ==========
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.Controls.Add(this.lblServiceType, 0, 0);
            this.tlpMain.Controls.Add(this.lblNetEarnings, 1, 0);
            this.tlpMain.Controls.Add(this.lblPickup, 0, 1);
            this.tlpMain.Controls.Add(this.lblDropoff, 0, 2);
            this.tlpMain.Controls.Add(this.lblTripParams, 0, 3);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(16, 16);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.Size = new System.Drawing.Size(330, 220);
            this.tlpMain.TabIndex = 0;

            // ========== lblServiceType ==========
            this.lblServiceType.AutoSize = false;
            this.lblServiceType.Size = new System.Drawing.Size(90, 30);
            this.lblServiceType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServiceType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Text = "      Ô tô";
            this.lblServiceType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblServiceType.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            this.lblServiceType.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.lblServiceType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // ========== lblNetEarnings ==========
            this.lblNetEarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetEarnings.Location = new System.Drawing.Point(201, 0);
            this.lblNetEarnings.Name = "lblNetEarnings";
            this.lblNetEarnings.Size = new System.Drawing.Size(126, 50);
            this.lblNetEarnings.Text = "45,000d";
            this.lblNetEarnings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNetEarnings.ForeColor = System.Drawing.Color.FromArgb(255, 126, 0);
            this.lblNetEarnings.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);

            // ========== lblPickup (Chỉ hiển thị Tên Địa Điểm rút gọn) ==========
            this.tlpMain.SetColumnSpan(this.lblPickup, 2);
            this.lblPickup.Dock = System.Windows.Forms.DockStyle.Fill;
            // --- CẬP NHẬT TEXT GIỮ CHỖ NGẮN GỌN ---
            this.lblPickup.Text = "      Đại học Kinh tế UEH - Cơ sở A";
            this.lblPickup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPickup.Font = OOP2026.Typography.Font10Regular;
            this.lblPickup.ForeColor = OOP2026.Colors.Black;

            // ========== lblDropoff (Chỉ hiển thị Tên Địa Điểm rút gọn) ==========
            this.tlpMain.SetColumnSpan(this.lblDropoff, 2);
            this.lblDropoff.Dock = System.Windows.Forms.DockStyle.Fill;
            // --- CẬP NHẬT TEXT GIỮ CHỖ NGẮN GỌN ---
            this.lblDropoff.Text = "      Chợ Bến Thành";
            this.lblDropoff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDropoff.Font = OOP2026.Typography.Font10Regular;
            this.lblDropoff.ForeColor = OOP2026.Colors.Black;

            // ========== lblTripParams ==========
            this.tlpMain.SetColumnSpan(this.lblTripParams, 2);
            this.lblTripParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTripParams.Text = "4.2 km  •  15 phút  •  Cước: 60,000đ";
            this.lblTripParams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTripParams.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblTripParams.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.lblTripParams.Font = OOP2026.Typography.Font8Regular;

            // ========== tlpButtons ==========
            this.tlpButtons.ColumnCount = 2;
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Controls.Add(this.btnReject, 0, 0);
            this.tlpButtons.Controls.Add(this.btnAccept, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 165);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(330, 55);

            // ========== btnReject ==========
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReject.Location = new System.Drawing.Point(2, 2);
            this.btnReject.Margin = new System.Windows.Forms.Padding(2, 2, 10, 2);
            this.btnReject.Name = "btnReject";
            this.btnReject.Text = "Từ chối";
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.BackColor = OOP2026.Colors.White;
            this.btnReject.ForeColor = OOP2026.Colors.Red;
            this.btnReject.Font = OOP2026.Typography.Font10Bold;
            this.btnReject.Click += new System.EventHandler(this.BtnReject_Click);

            // ========== btnAccept ==========
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAccept.Location = new System.Drawing.Point(167, 2);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Text = "Nhận cuốc";
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.BackColor = OOP2026.Colors.Orange;
            this.btnAccept.ForeColor = OOP2026.Colors.White;
            this.btnAccept.Font = OOP2026.Typography.Font10Bold;
            this.btnAccept.Click += new System.EventHandler(this.BtnAccept_Click);

            // ========== ucTripCard ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Font = OOP2026.Typography.Font9Regular;
            this.Name = "ucTripCard";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(362, 252);
            this.BackColor = OOP2026.Colors.White;

            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.Label lblNetEarnings;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.Label lblDropoff;
        private System.Windows.Forms.Label lblTripParams;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAccept;
        #endregion
    }
}
