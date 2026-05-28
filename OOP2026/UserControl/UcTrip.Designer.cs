namespace OOP2026
{
    partial class ucTrip
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTripInfo = new System.Windows.Forms.Label();
            this.pnlNoTrip = new System.Windows.Forms.Panel();
            this.lblNoTripMsg = new System.Windows.Forms.Label();
            this.pnlTripDetails = new System.Windows.Forms.Panel();
            this.tlpInfoGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDropoff = new System.Windows.Forms.Label();
            this.lblFare = new System.Windows.Forms.Label();
            this.lblDriverInfo = new System.Windows.Forms.Label();
            this.ucTripStatus = new OOP2026.ucTripStatus();

            this.pnlNoTrip.SuspendLayout();
            this.pnlTripDetails.SuspendLayout();
            this.tlpInfoGrid.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTripInfo
            // 
            this.lblTripInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTripInfo.Location = new System.Drawing.Point(16, 16);
            this.lblTripInfo.Name = "lblTripInfo";
            this.lblTripInfo.Size = new System.Drawing.Size(368, 40);
            this.lblTripInfo.TabIndex = 0;
            this.lblTripInfo.Text = "Chi tiết hành trình";
            this.lblTripInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // pnlNoTrip (Màn hình trống khi chưa đặt xe)
            // 
            this.pnlNoTrip.Controls.Add(this.lblNoTripMsg);
            this.pnlNoTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoTrip.Location = new System.Drawing.Point(16, 56);
            this.pnlNoTrip.Name = "pnlNoTrip";
            this.pnlNoTrip.Size = new System.Drawing.Size(368, 428);
            this.pnlNoTrip.TabIndex = 1;
            this.pnlNoTrip.Visible = false;

            // 
            // lblNoTripMsg
            // 
            this.lblNoTripMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoTripMsg.Font = OOP2026.Typography.Font10Regular;
            this.lblNoTripMsg.ForeColor = System.Drawing.Color.DarkGray;
            this.lblNoTripMsg.Location = new System.Drawing.Point(0, 0);
            this.lblNoTripMsg.Name = "lblNoTripMsg";
            this.lblNoTripMsg.Size = new System.Drawing.Size(368, 428);
            this.lblNoTripMsg.TabIndex = 0;
            this.lblNoTripMsg.Text = "Bạn hiện không có chuyến đi nào đang diễn ra.";
            this.lblNoTripMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // pnlTripDetails (Màn hình chứa thông tin chuyến đi thực tế)
            // 
            this.pnlTripDetails.Controls.Add(this.tlpInfoGrid);
            this.pnlTripDetails.Controls.Add(this.ucTripStatus);
            this.pnlTripDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTripDetails.Location = new System.Drawing.Point(16, 56);
            this.pnlTripDetails.Name = "pnlTripDetails";
            this.pnlTripDetails.Size = new System.Drawing.Size(368, 428);
            this.pnlTripDetails.TabIndex = 2;

            // 
            // tlpInfoGrid
            // 
            this.tlpInfoGrid.ColumnCount = 1;
            this.tlpInfoGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfoGrid.Controls.Add(this.lblPickup, 0, 0);
            this.tlpInfoGrid.Controls.Add(this.lblDropoff, 0, 1);
            this.tlpInfoGrid.Controls.Add(this.lblFare, 0, 2);
            this.tlpInfoGrid.Controls.Add(this.lblDriverInfo, 0, 3);
            this.tlpInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfoGrid.Location = new System.Drawing.Point(0, 0);
            this.tlpInfoGrid.Name = "tlpInfoGrid";
            this.tlpInfoGrid.RowCount = 4;
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfoGrid.Size = new System.Drawing.Size(368, 248);
            this.tlpInfoGrid.TabIndex = 0;

            // 
            // lblPickup (Tên địa điểm rút gọn giữ chỗ)
            // 
            this.lblPickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPickup.Font = OOP2026.Typography.Font10Regular;
            this.lblPickup.ForeColor = OOP2026.Colors.Black;
            this.lblPickup.Location = new System.Drawing.Point(3, 0);
            this.lblPickup.Name = "lblPickup";
            this.lblPickup.Text = "      Từ: Đại học Kinh tế UEH - Cơ sở B";
            this.lblPickup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblDropoff (Tên địa điểm rút gọn giữ chỗ)
            // 
            this.lblDropoff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropoff.Font = OOP2026.Typography.Font10Regular;
            this.lblDropoff.ForeColor = OOP2026.Colors.Black;
            this.lblDropoff.Location = new System.Drawing.Point(3, 40);
            this.lblDropoff.Name = "lblDropoff";
            this.lblDropoff.Text = "      Đến: Landmark 81";
            this.lblDropoff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblFare
            // 
            this.lblFare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFare.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFare.ForeColor = System.Drawing.Color.FromArgb(255, 126, 0); // Màu cam thương hiệu
            this.lblFare.Location = new System.Drawing.Point(3, 80);
            this.lblFare.Name = "lblFare";
            this.lblFare.Text = "Giá: 65,000đ";
            this.lblFare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblDriverInfo
            // 
            this.lblDriverInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDriverInfo.Font = OOP2026.Typography.Font9Regular;
            this.lblDriverInfo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128); // Muted Gray
            this.lblDriverInfo.Location = new System.Drawing.Point(3, 125);
            this.lblDriverInfo.Name = "lblDriverInfo";
            this.lblDriverInfo.Text = "⌛ Hệ thống đang điều phối tài xế gần nhất...";
            this.lblDriverInfo.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);

            // 
            // ucTripStatus (Đẩy xuống đây Panel chi tiết)
            // 
            this.ucTripStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucTripStatus.Location = new System.Drawing.Point(0, 248);
            this.ucTripStatus.Name = "ucTripStatus";
            this.ucTripStatus.Size = new System.Drawing.Size(368, 180);
            this.ucTripStatus.TabIndex = 1;

            // 
            // ucTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTripDetails);
            this.Controls.Add(this.pnlNoTrip);
            this.Controls.Add(this.lblTripInfo);
            this.Name = "ucTrip";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(400, 500);

            this.pnlNoTrip.ResumeLayout(false);
            this.pnlTripDetails.ResumeLayout(false);
            this.tlpInfoGrid.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTripInfo;
        private System.Windows.Forms.Panel pnlNoTrip;
        private System.Windows.Forms.Label lblNoTripMsg;
        private System.Windows.Forms.Panel pnlTripDetails;
        private System.Windows.Forms.TableLayoutPanel tlpInfoGrid;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.Label lblDropoff;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Label lblDriverInfo;
        private OOP2026.ucTripStatus ucTripStatus;
    }
}
