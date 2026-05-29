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
            this.tlpDetailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlpInfoGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDropoff = new System.Windows.Forms.Label();
            this.tlpFareDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblFareTitle = new System.Windows.Forms.Label();
            this.lblFarePrice = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDriverInfo = new System.Windows.Forms.Label();
            this.ucTripStatus = new OOP2026.ucTripStatus();
            this.btnCancelTrip = new System.Windows.Forms.Button();

            this.pnlNoTrip.SuspendLayout();
            this.pnlTripDetails.SuspendLayout();
            this.tlpDetailsLayout.SuspendLayout();
            this.tlpInfoGrid.SuspendLayout();
            this.tlpFareDetails.SuspendLayout();
            this.SuspendLayout();

            // ── lblTripInfo (Tiêu đề) ────────────────────────────────
            this.lblTripInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTripInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTripInfo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTripInfo.Location = new System.Drawing.Point(16, 16);
            this.lblTripInfo.Name = "lblTripInfo";
            this.lblTripInfo.Size = new System.Drawing.Size(368, 40);
            this.lblTripInfo.TabIndex = 0;
            this.lblTripInfo.Text = "Chi tiết hành trình";
            this.lblTripInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── pnlNoTrip ───────────────────────────────────────────
            this.pnlNoTrip.Controls.Add(this.lblNoTripMsg);
            this.pnlNoTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoTrip.Location = new System.Drawing.Point(16, 56);
            this.pnlNoTrip.Name = "pnlNoTrip";
            this.pnlNoTrip.Size = new System.Drawing.Size(368, 428);
            this.pnlNoTrip.TabIndex = 1;
            this.pnlNoTrip.Visible = false;

            // ── lblNoTripMsg ────────────────────────────────────────
            this.lblNoTripMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoTripMsg.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblNoTripMsg.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblNoTripMsg.Location = new System.Drawing.Point(0, 0);
            this.lblNoTripMsg.Name = "lblNoTripMsg";
            this.lblNoTripMsg.Size = new System.Drawing.Size(368, 428);
            this.lblNoTripMsg.TabIndex = 0;
            this.lblNoTripMsg.Text = "🗺️\r\n\r\nBạn hiện không có chuyến đi nào đang diễn ra.\r\nHãy đặt chuyến xe mới để bắt đầu hành trình nhé!";
            this.lblNoTripMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlTripDetails ──────────────────────────────────────
            this.pnlTripDetails.Controls.Add(this.tlpDetailsLayout);
            this.pnlTripDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTripDetails.Location = new System.Drawing.Point(16, 56);
            this.pnlTripDetails.Name = "pnlTripDetails";
            this.pnlTripDetails.Size = new System.Drawing.Size(368, 428);
            this.pnlTripDetails.TabIndex = 2;

            // ── tlpDetailsLayout (bố cục chính 3 hàng) ──────────────
            this.tlpDetailsLayout.ColumnCount = 1;
            this.tlpDetailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetailsLayout.Controls.Add(this.tlpInfoGrid, 0, 0);
            this.tlpDetailsLayout.Controls.Add(this.ucTripStatus, 0, 1);
            this.tlpDetailsLayout.Controls.Add(this.btnCancelTrip, 0, 2);
            this.tlpDetailsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetailsLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpDetailsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDetailsLayout.Name = "tlpDetailsLayout";
            this.tlpDetailsLayout.RowCount = 3;
            this.tlpDetailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Grid thông tin
            this.tlpDetailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F)); // ucTripStatus
            this.tlpDetailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));  // Nút hủy
            this.tlpDetailsLayout.Size = new System.Drawing.Size(368, 428);
            this.tlpDetailsLayout.TabIndex = 0;

            // ── tlpInfoGrid ─────────────────────────────────────────
            this.tlpInfoGrid.ColumnCount = 1;
            this.tlpInfoGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfoGrid.Controls.Add(this.lblPickup, 0, 0);
            this.tlpInfoGrid.Controls.Add(this.lblDropoff, 0, 1);
            this.tlpInfoGrid.Controls.Add(this.tlpFareDetails, 0, 2);
            this.tlpInfoGrid.Controls.Add(this.lblDriverInfo, 0, 3);
            this.tlpInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfoGrid.Location = new System.Drawing.Point(0, 0);
            this.tlpInfoGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlpInfoGrid.Name = "tlpInfoGrid";
            this.tlpInfoGrid.RowCount = 4;
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F)); // Chiều cao vùng cước phí mới
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfoGrid.Size = new System.Drawing.Size(368, 256);
            this.tlpInfoGrid.TabIndex = 0;

            // ── lblPickup ───────────────────────────────────────────
            this.lblPickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPickup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPickup.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblPickup.Location = new System.Drawing.Point(3, 6);
            this.lblPickup.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblPickup.Padding = new System.Windows.Forms.Padding(24, 2, 2, 2);
            this.lblPickup.Text = "Từ: Đại học Kinh tế UEH - Cơ sở B";
            this.lblPickup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblDropoff ──────────────────────────────────────────
            this.lblDropoff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropoff.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDropoff.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblDropoff.Location = new System.Drawing.Point(3, 12);
            this.lblDropoff.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblDropoff.Padding = new System.Windows.Forms.Padding(24, 2, 2, 2);
            this.lblDropoff.Text = "Đến: Landmark 81";
            this.lblDropoff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── tlpFareDetails (bảng con 2x2) ──────────────────────
            this.tlpFareDetails.ColumnCount = 2;
            this.tlpFareDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpFareDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpFareDetails.Controls.Add(this.lblFareTitle, 0, 0);
            this.tlpFareDetails.Controls.Add(this.lblFarePrice, 0, 1);
            this.tlpFareDetails.Controls.Add(this.lblDistance, 1, 0);
            this.tlpFareDetails.Controls.Add(this.lblDuration, 1, 1);
            this.tlpFareDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFareDetails.Location = new System.Drawing.Point(3, 24);
            this.tlpFareDetails.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.tlpFareDetails.Name = "tlpFareDetails";
            this.tlpFareDetails.RowCount = 2;
            this.tlpFareDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpFareDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpFareDetails.Size = new System.Drawing.Size(362, 73);
            this.tlpFareDetails.TabIndex = 10;

            // lblFareTitle
            this.lblFareTitle.AutoSize = false;
            this.lblFareTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFareTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFareTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblFareTitle.Location = new System.Drawing.Point(3, 0);
            this.lblFareTitle.Name = "lblFareTitle";
            this.lblFareTitle.Size = new System.Drawing.Size(211, 25);
            this.lblFareTitle.TabIndex = 0;
            this.lblFareTitle.Text = "Cước phí";
            this.lblFareTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // lblFarePrice (giá tiền to, xanh lá)
            this.lblFarePrice.AutoSize = false;
            this.lblFarePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFarePrice.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblFarePrice.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74); // Xanh lá sang
            this.lblFarePrice.Location = new System.Drawing.Point(3, 25);
            this.lblFarePrice.Name = "lblFarePrice";
            this.lblFarePrice.Size = new System.Drawing.Size(211, 48);
            this.lblFarePrice.TabIndex = 1;
            this.lblFarePrice.Text = "65.000đ";
            this.lblFarePrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblDistance (khoảng cách)
            this.lblDistance.AutoSize = false;
            this.lblDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDistance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblDistance.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblDistance.Location = new System.Drawing.Point(220, 0);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(139, 25);
            this.lblDistance.TabIndex = 2;
            this.lblDistance.Text = "12.5 km";
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.BottomRight;

            // lblDuration (thời gian)
            this.lblDuration.AutoSize = false;
            this.lblDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblDuration.Location = new System.Drawing.Point(220, 25);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(139, 48);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "~25 phút";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // ── lblDriverInfo ────────────────────────────────────────
            this.lblDriverInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDriverInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDriverInfo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblDriverInfo.Location = new System.Drawing.Point(3, 103);
            this.lblDriverInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblDriverInfo.Name = "lblDriverInfo";
            this.lblDriverInfo.Padding = new System.Windows.Forms.Padding(4);
            this.lblDriverInfo.Size = new System.Drawing.Size(362, 153);
            this.lblDriverInfo.Text = "⏳ Hệ thống đang điều phối tài xế gần nhất...";

            // ── ucTripStatus ─────────────────────────────────────────
            this.ucTripStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTripStatus.Location = new System.Drawing.Point(0, 256);
            this.ucTripStatus.Margin = new System.Windows.Forms.Padding(0);
            this.ucTripStatus.Name = "ucTripStatus";
            this.ucTripStatus.Size = new System.Drawing.Size(368, 120);
            this.ucTripStatus.TabIndex = 1;

            // ── btnCancelTrip (nút hủy chuyến màu đỏ nhạt) ──────────
            this.btnCancelTrip.BackColor = System.Drawing.Color.FromArgb(254, 202, 202);
            this.btnCancelTrip.FlatAppearance.BorderSize = 0;
            this.btnCancelTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelTrip.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnCancelTrip.Location = new System.Drawing.Point(0, 376);
            this.btnCancelTrip.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Size = new System.Drawing.Size(368, 52);
            this.btnCancelTrip.TabIndex = 2;
            this.btnCancelTrip.Text = "Hủy chuyến";
            this.btnCancelTrip.UseVisualStyleBackColor = false;
            // Đảm bảo nút lấp đầy ô (dock đã set trong tlpDetailsLayout, nhưng thêm dòng này cho chắc)
            this.btnCancelTrip.Dock = System.Windows.Forms.DockStyle.Fill;

            // ── ucTrip ──────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.Controls.Add(this.pnlTripDetails);
            this.Controls.Add(this.pnlNoTrip);
            this.Controls.Add(this.lblTripInfo);
            this.Name = "ucTrip";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(400, 500);

            this.pnlNoTrip.ResumeLayout(false);
            this.pnlTripDetails.ResumeLayout(false);
            this.tlpDetailsLayout.ResumeLayout(false);
            this.tlpInfoGrid.ResumeLayout(false);
            this.tlpFareDetails.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTripInfo;
        private System.Windows.Forms.Panel pnlNoTrip;
        private System.Windows.Forms.Label lblNoTripMsg;
        private System.Windows.Forms.Panel pnlTripDetails;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsLayout;
        private System.Windows.Forms.TableLayoutPanel tlpInfoGrid;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.Label lblDropoff;
        private System.Windows.Forms.TableLayoutPanel tlpFareDetails;
        private System.Windows.Forms.Label lblFareTitle;
        private System.Windows.Forms.Label lblFarePrice;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDriverInfo;
        private OOP2026.ucTripStatus ucTripStatus;
        private System.Windows.Forms.Button btnCancelTrip;
    }
}