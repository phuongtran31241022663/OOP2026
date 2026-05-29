namespace OOP2026
{
    partial class ucPolicyCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpCardLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.lblKmPrice = new System.Windows.Forms.Label();
            this.lblCommission = new System.Windows.Forms.Label();
            this.tlpCardLayout.SuspendLayout();
            this.SuspendLayout();

            // ── tlpCardLayout (Lưới tự động co dãn bảo vệ dòng) ───────
            this.tlpCardLayout.ColumnCount = 1;
            this.tlpCardLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCardLayout.Controls.Add(this.lblTitle, 0, 0);
            this.tlpCardLayout.Controls.Add(this.lblBasePrice, 0, 1);
            this.tlpCardLayout.Controls.Add(this.lblKmPrice, 0, 2);
            this.tlpCardLayout.Controls.Add(this.lblCommission, 0, 3);
            this.tlpCardLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCardLayout.Location = new System.Drawing.Point(16, 14); // Khớp nối đồng bộ với Padding viền ngoài
            this.tlpCardLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCardLayout.Name = "tlpCardLayout";
            this.tlpCardLayout.RowCount = 4;
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng tiêu đề loại xe
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng Giá mở cửa
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng Giá/km
            this.tlpCardLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng Hoa hồng chiếm trọn đáy
            this.tlpCardLayout.Size = new System.Drawing.Size(248, 102);
            this.tlpCardLayout.TabIndex = 0;

            // ── lblTitle (Tiêu đề loại xe - ví dụ: Ô tô 4 chỗ, Xe máy) ──
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold); // Cỡ chữ lớn, rõ ràng
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Tông màu đen Slate sâu thẳm
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10); // Tạo khoảng trống 10px ngăn cách với khối giá bên dưới
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 30);
            this.lblTitle.Text = "🚗 Loại xe";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblBasePrice (Thông số Giá mở cửa) ────────────────────
            this.lblBasePrice.AutoSize = true;
            this.lblBasePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBasePrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBasePrice.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); // Xám đậm thanh lịch
            this.lblBasePrice.Location = new System.Drawing.Point(0, 40);
            this.lblBasePrice.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(248, 21);
            this.lblBasePrice.Text = "💵 Giá mở cửa: 12.000đ";
            this.lblBasePrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblKmPrice (Thông số Giá theo kilomet) ────────────────
            this.lblKmPrice.AutoSize = true;
            this.lblKmPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKmPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKmPrice.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblKmPrice.Location = new System.Drawing.Point(0, 65);
            this.lblKmPrice.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.lblKmPrice.Name = "lblKmPrice";
            this.lblKmPrice.Size = new System.Drawing.Size(248, 21);
            this.lblKmPrice.Text = "📏 Giá/km: 15.000đ";
            this.lblKmPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblCommission (Thông số % chiết khấu hệ thống) ─────────
            this.lblCommission.AutoSize = true;
            this.lblCommission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCommission.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold); // Đậm chữ hoa hồng để lưu ý quản trị
            this.lblCommission.ForeColor = System.Drawing.Color.FromArgb(249, 115, 22); // Đổi sang tông màu Cam kích thích thị giác
            this.lblCommission.Location = new System.Drawing.Point(0, 90);
            this.lblCommission.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(248, 21);
            this.lblCommission.Text = "📈 Chiết khấu: 20%";
            this.lblCommission.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // ── ucPolicyCard (Cấu hình tổng thể khung thẻ) ─────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpCardLayout);
            this.Name = "ucPolicyCard";
            this.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14); // Tinh chỉnh khoảng đệm bao quanh cân đối
            this.Size = new System.Drawing.Size(280, 130);
            this.tlpCardLayout.ResumeLayout(false);
            this.tlpCardLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpCardLayout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.Label lblKmPrice;
        private System.Windows.Forms.Label lblCommission;
    }
}