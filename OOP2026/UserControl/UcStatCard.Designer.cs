namespace OOP2026
{
    partial class ucStatCard
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
            this.tlpCardLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label(); // Thêm Label mô tả

            this.tlpCardLayout.SuspendLayout();
            this.SuspendLayout();

            // ── tlpCardLayout: Cấu trúc 3 hàng ──────────
            this.tlpCardLayout.ColumnCount = 1;
            this.tlpCardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCardLayout.RowCount = 3;
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng tiêu đề
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng giá trị
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng mô tả

            this.tlpCardLayout.Controls.Add(this.lblTitle, 0, 0);
            this.tlpCardLayout.Controls.Add(this.lblValue, 0, 1);
            this.tlpCardLayout.Controls.Add(this.lblDescription, 0, 2);

            this.tlpCardLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCardLayout.Location = new System.Drawing.Point(16, 14);
            this.tlpCardLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCardLayout.Name = "tlpCardLayout";
            this.tlpCardLayout.Size = new System.Drawing.Size(168, 72);
            this.tlpCardLayout.TabIndex = 0;

            // ── lblTitle: Tiêu đề ──────────
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 15);
            this.lblTitle.Text = "Tổng thu nhập";

            // ── lblValue: Giá trị chính ──────────
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblValue.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblValue.Location = new System.Drawing.Point(0, 15);
            this.lblValue.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(145, 32);
            this.lblValue.Text = "2.450.000đ";

            // ── lblDescription: Thông tin phụ ──────────
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblDescription.Location = new System.Drawing.Point(0, 49);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(126, 15);
            this.lblDescription.Text = "từ chuyến hoàn thành";

            // ── ucStatCard ──────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpCardLayout);
            this.Name = "ucStatCard";
            this.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.Size = new System.Drawing.Size(200, 100);
            this.tlpCardLayout.ResumeLayout(false);
            this.tlpCardLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCardLayout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblDescription; // Đừng quên khai báo biến này
    }
}