namespace OOP2026
{
    partial class ucTripStatus
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
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStatusRow = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStatusIcon = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.ProgressBar(); // Thanh trạng thái mới

            this.tlpRoot.SuspendLayout();
            this.tlpStatusRow.SuspendLayout();
            this.pnlStatusIcon.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Controls.Add(this.tlpStatusRow, 0, 0);
            this.tlpRoot.Controls.Add(this.pbStatus, 0, 1);           // Thêm thanh trạng thái
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(12, 8);
            this.tlpRoot.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng nội dung
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));  // Thanh trạng thái
            this.tlpRoot.Size = new System.Drawing.Size(536, 110);    // Tổng chiều cao giữ nguyên, thanh tiến trình nằm dưới
            this.tlpRoot.TabIndex = 0;

            // 
            // tlpStatusRow
            // 
            this.tlpStatusRow.ColumnCount = 2;
            this.tlpStatusRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpStatusRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatusRow.Controls.Add(this.pnlStatusIcon, 0, 0);
            this.tlpStatusRow.Controls.Add(this.tlpInfo, 1, 0);
            this.tlpStatusRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatusRow.Location = new System.Drawing.Point(0, 0);
            this.tlpStatusRow.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStatusRow.Name = "tlpStatusRow";
            this.tlpStatusRow.RowCount = 1;
            this.tlpStatusRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatusRow.Size = new System.Drawing.Size(536, 86);
            this.tlpStatusRow.TabIndex = 0;

            // 
            // pnlStatusIcon
            // 
            this.pnlStatusIcon.BackColor = System.Drawing.Color.FromArgb(224, 242, 254);
            this.pnlStatusIcon.Controls.Add(this.lblIcon);
            this.pnlStatusIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatusIcon.Location = new System.Drawing.Point(0, 0);
            this.pnlStatusIcon.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlStatusIcon.Name = "pnlStatusIcon";
            this.pnlStatusIcon.Size = new System.Drawing.Size(52, 86);
            this.pnlStatusIcon.TabIndex = 0;

            // 
            // lblIcon
            // 
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 18F);
            this.lblIcon.ForeColor = System.Drawing.Color.FromArgb(2, 132, 199);
            this.lblIcon.Location = new System.Drawing.Point(0, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(52, 86);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "⏳";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // tlpInfo
            // 
            this.tlpInfo.ColumnCount = 1;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Controls.Add(this.lblTitle, 0, 0);
            this.tlpInfo.Controls.Add(this.lblDescription, 0, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfo.Location = new System.Drawing.Point(64, 0);
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 2;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Size = new System.Drawing.Size(472, 86);
            this.tlpInfo.TabIndex = 1;

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(472, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đang xử lý";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblDescription.Location = new System.Drawing.Point(0, 28);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(472, 58);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Vui lòng đợi trong giây lát";

            // 
            // pbStatus (Thanh trạng thái)
            // 
            this.pbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbStatus.Location = new System.Drawing.Point(0, 86);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(536, 24);
            this.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee; // Hiệu ứng động khi đang xử lý
            this.pbStatus.TabIndex = 2;
            this.pbStatus.MarqueeAnimationSpeed = 30;

            // 
            // ucTripStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Controls.Add(this.tlpRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(360, 110);
            this.Name = "ucTripStatus";
            this.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.Size = new System.Drawing.Size(560, 110);

            this.tlpRoot.ResumeLayout(false);
            this.tlpStatusRow.ResumeLayout(false);
            this.pnlStatusIcon.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Khai báo controls
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.TableLayoutPanel tlpStatusRow;
        private System.Windows.Forms.Panel pnlStatusIcon;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ProgressBar pbStatus; // Thanh trạng thái mới
    }
}