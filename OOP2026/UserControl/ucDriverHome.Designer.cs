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

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tlpHeaderGrid = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTextInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.pnlStatusWrapper = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabStatus = new System.Windows.Forms.TabPage();
            this.tabRequests = new System.Windows.Forms.TabPage();
            this.tabWallet = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tabProfile = new System.Windows.Forms.TabPage();
            this.pnlTop.SuspendLayout();
            this.tlpHeaderGrid.SuspendLayout();
            this.tlpTextInfo.SuspendLayout();
            this.pnlStatusWrapper.SuspendLayout();
            this.tab.SuspendLayout();
            this.SuspendLayout();

            // ── pnlTop (Thanh thông tin phía trên - Tông màu cam thương hiệu) ──
            this.pnlTop.BackColor = OOP2026.Colors.Orange;
            this.pnlTop.Controls.Add(this.tlpHeaderGrid);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(16, 14, 16, 12); // Tinh chỉnh khoảng đệm bảo vệ viền
            this.pnlTop.Size = new System.Drawing.Size(300, 115); // Tăng nhẹ chiều cao tạo không gian thông thoáng
            this.pnlTop.TabIndex = 0;

            // ── tlpHeaderGrid (Lưới chia vùng thông tin và nút Trạng thái nổi) ──
            this.tlpHeaderGrid.BackColor = System.Drawing.Color.Transparent;
            this.tlpHeaderGrid.ColumnCount = 2;
            this.tlpHeaderGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F)); // Vùng chữ chiếm 65%
            this.tlpHeaderGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F)); // Vùng phím bấm chiếm 35%
            this.tlpHeaderGrid.Controls.Add(this.tlpTextInfo, 0, 0);
            this.tlpHeaderGrid.Controls.Add(this.pnlStatusWrapper, 1, 0);
            this.tlpHeaderGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeaderGrid.Location = new System.Drawing.Point(16, 14);
            this.tlpHeaderGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeaderGrid.Name = "tlpHeaderGrid";
            this.tlpHeaderGrid.RowCount = 1;
            this.tlpHeaderGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderGrid.Size = new System.Drawing.Size(268, 89);
            this.tlpHeaderGrid.TabIndex = 0;

            // ── tlpTextInfo (Lưới xếp dọc tự động co dãn chống lỗi đè chữ) ──
            this.tlpTextInfo.ColumnCount = 1;
            this.tlpTextInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTextInfo.Controls.Add(this.lblName, 0, 0);
            this.tlpTextInfo.Controls.Add(this.lblPhone, 0, 1);
            this.tlpTextInfo.Controls.Add(this.lblStats, 0, 2);
            this.tlpTextInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTextInfo.Location = new System.Drawing.Point(0, 0);
            this.tlpTextInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTextInfo.Name = "tlpTextInfo";
            this.tlpTextInfo.RowCount = 3;
            this.tlpTextInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng tên tài xế
            this.tlpTextInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng số điện thoại
            this.tlpTextInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng Thống kê chiếm đáy
            this.tlpTextInfo.Size = new System.Drawing.Size(174, 89);
            this.tlpTextInfo.TabIndex = 0;

            // ── lblName (Nhãn hiển thị tên Tài xế) ──────────────────────
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold); // Khóa font cứng tránh lỗi biến tĩnh designer
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(174, 31);
            this.lblName.Text = "Tên tài xế";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblPhone (Nhãn hiển thị SĐT) ───────────────────────────
            this.lblPhone.AutoSize = true;
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(254, 215, 170); // Tông cam sữa cực dịu và sang trọng
            this.lblPhone.Location = new System.Drawing.Point(0, 33);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(174, 20);
            this.lblPhone.Text = "Điện thoại";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblStats (Nhãn hiển thị tóm tắt hiệu suất ngày) ─────────
            this.lblStats.AutoSize = true;
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStats.ForeColor = System.Drawing.Color.White;
            this.lblStats.Location = new System.Drawing.Point(0, 56);
            this.lblStats.Margin = new System.Windows.Forms.Padding(0);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(174, 33);
            this.lblStats.Text = "📊 Thống kê";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── pnlStatusWrapper (Vùng căn chỉnh nút Trạng thái nổi) ────
            this.pnlStatusWrapper.Controls.Add(this.lblStatus);
            this.pnlStatusWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatusWrapper.Location = new System.Drawing.Point(174, 0);
            this.pnlStatusWrapper.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStatusWrapper.Name = "pnlStatusWrapper";
            this.pnlStatusWrapper.Size = new System.Drawing.Size(94, 89);
            this.pnlStatusWrapper.TabIndex = 1;

            // ── lblStatus (Nút bật/tắt trạng thái nhận cuốc dạng Badge phẳng) ──
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Right; // Neo bám sát lề phải Form công nghệ
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(254, 242, 242); // Màu nền trắng đỏ nhạt trạng thái nghỉ
            this.lblStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68); // Chữ màu đỏ báo hiệu nghỉ ngơi
            this.lblStatus.Location = new System.Drawing.Point(2, 4); // SỬA: Tự do co dãn theo chiều rộng ô chứa 35%
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(90, 28); // Tăng diện tích phím bấm dày dặn dễ chạm tay
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ngoại tuyến";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_CheckedChanged);

            // ── tab (Hệ thống thanh Tab điều phối nghiệp vụ) ────────────
            this.tab.Controls.Add(this.tabStatus);
            this.tab.Controls.Add(this.tabRequests);
            this.tab.Controls.Add(this.tabWallet);
            this.tab.Controls.Add(this.tabHistory);
            this.tab.Controls.Add(this.tabProfile);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Segoe UI", 9.5F); // Đồng bộ font chữ hệ thống phím bấm Tab
            this.tab.Location = new System.Drawing.Point(0, 115);
            this.tab.Margin = new System.Windows.Forms.Padding(0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(300, 485);
            this.tab.TabIndex = 1;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.Tab_SelectedIndexChanged);

            // ── tabStatus (Tab Bản đồ / Trạng thái trực quan) ─────────────
            this.tabStatus.BackColor = System.Drawing.Color.White;
            this.tabStatus.Location = new System.Drawing.Point(4, 30);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Padding = new System.Windows.Forms.Padding(12); // SỬA: Tăng không gian đệm biên thoáng đãng 12px
            this.tabStatus.Size = new System.Drawing.Size(292, 451);
            this.tabStatus.TabIndex = 0;
            this.tabStatus.Text = "Trạng thái";

            // ── tabRequests (Tab Danh sách tiếp nhận cuốc xe nổ về) ──────
            this.tabRequests.BackColor = System.Drawing.Color.White;
            this.tabRequests.Location = new System.Drawing.Point(4, 30);
            this.tabRequests.Name = "tabRequests";
            this.tabRequests.Padding = new System.Windows.Forms.Padding(12);
            this.tabRequests.Size = new System.Drawing.Size(292, 451);
            this.tabRequests.TabIndex = 1;
            this.tabRequests.Text = "Cuốc xe";

            // ── tabWallet (Tab Quản lý ví tiền / Rút nạp doanh thu) ───────
            this.tabWallet.BackColor = System.Drawing.Color.White;
            this.tabWallet.Location = new System.Drawing.Point(4, 30);
            this.tabWallet.Name = "tabWallet";
            this.tabWallet.Padding = new System.Windows.Forms.Padding(12);
            this.tabWallet.Size = new System.Drawing.Size(292, 451);
            this.tabWallet.TabIndex = 2;
            this.tabWallet.Text = "Ví tiền";

            // ── tabHistory (Tab Xem báo cáo lịch sử chạy xe cũ) ──────────
            this.tabHistory.BackColor = System.Drawing.Color.White;
            this.tabHistory.Location = new System.Drawing.Point(4, 30);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(12);
            this.tabHistory.Size = new System.Drawing.Size(292, 451);
            this.tabHistory.TabIndex = 3;
            this.tabHistory.Text = "Lịch sử";

            // ── tabProfile (Tab Hồ sơ tài xế / Đổi mật khẩu bảo mật) ─────
            this.tabProfile.BackColor = System.Drawing.Color.White;
            this.tabProfile.Location = new System.Drawing.Point(4, 30);
            this.tabProfile.Name = "tabProfile";
            this.tabProfile.Padding = new System.Windows.Forms.Padding(12);
            this.tabProfile.Size = new System.Drawing.Size(292, 451);
            this.tabProfile.TabIndex = 4;
            this.tabProfile.Text = "Hồ sơ";

            // ── ucDriverHome (Main UserControl) ──────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tab);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(300, 500);
            this.Name = "ucDriverHome";
            this.Size = new System.Drawing.Size(300, 600);
            this.pnlTop.ResumeLayout(false);
            this.tlpHeaderGrid.ResumeLayout(false);
            this.tlpTextInfo.ResumeLayout(false);
            this.tlpTextInfo.PerformLayout();
            this.pnlStatusWrapper.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        #region Controls declaration
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderGrid;
        private System.Windows.Forms.TableLayoutPanel tlpTextInfo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Panel pnlStatusWrapper;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabStatus;
        private System.Windows.Forms.TabPage tabRequests;
        private System.Windows.Forms.TabPage tabWallet;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabProfile;
        #endregion
    }
}