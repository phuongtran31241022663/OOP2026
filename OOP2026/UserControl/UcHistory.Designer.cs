namespace OOP2026
{
    partial class ucHistory
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpTrips = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // ── tlpMain (Khung quản lý layout gốc) ─────────────────────
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.flpTrips, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(16, 16); // Tăng nhẹ khoảng đệm biên cho thoáng đãng
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F)); // Tăng lên 45px cho tiêu đề cân đối
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(368, 468);
            this.tlpMain.TabIndex = 0;

            // ── lblTitle (Tiêu đề danh sách Lịch sử) ──────────────────
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold); // Khóa font cứng tránh lỗi biến tĩnh tĩnh trên designer
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Tông đen Slate hiện đại, sâu thẳm
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(368, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📜 Lịch sử chuyến đi";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── flpTrips (Bảng danh sách cuộn chứa các thẻ lịch sử) ──
            this.flpTrips.AutoScroll = true;
            // SỬA LỖI CHÍ MẠNG: Chuyển sang LeftToRight + WrapContents = true để card con ăn theo độ rộng tối đa và co giãn dòng hoàn hảo
            this.flpTrips.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpTrips.WrapContents = true;
            this.flpTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTrips.Location = new System.Drawing.Point(0, 45);
            this.flpTrips.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0); // Tạo khoảng cách nhỏ ngăn cách với tiêu đề
            // SỬA: Dành 18px lề phải làm không gian an toàn cho Thanh cuộn (Scrollbar) xuất hiện không đè lên Card con
            this.flpTrips.Padding = new System.Windows.Forms.Padding(2, 4, 18, 4);
            this.flpTrips.Name = "flpTrips";
            this.flpTrips.TabIndex = 1;

            // ── ucHistory (Main UserControl) ──────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); // Đổi sang màu xám Slate cực nhạt giúp các Card trắng nổi bật lên
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "ucHistory";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(400, 500);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpTrips;
    }
}