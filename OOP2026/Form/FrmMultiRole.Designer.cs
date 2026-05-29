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
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.tlpTopBar = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblSearchingStatus = new System.Windows.Forms.Label();
            this.pnlPassengerInfo = new System.Windows.Forms.Panel();
            this.flpPassenger = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPassengerIcon = new System.Windows.Forms.Label();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.lnkPassengerChange = new System.Windows.Forms.LinkLabel();
            this.pnlDriverInfo = new System.Windows.Forms.Panel();
            this.flpDriver = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlOnlineDot = new System.Windows.Forms.Panel();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblOnline = new System.Windows.Forms.Label();
            this.lnkDriverChange = new System.Windows.Forms.LinkLabel();
            this.pnlRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.lblDriverCount = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this._passengerPanel = new OOP2026.ucPassengerHome();
            this._map = new OOP2026.ucMap();
            this._driverHome = new OOP2026.ucDriverHome();
            this.pnlTopBar.SuspendLayout();
            this.tlpTopBar.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlPassengerInfo.SuspendLayout();
            this.flpPassenger.SuspendLayout();
            this.pnlDriverInfo.SuspendLayout();
            this.flpDriver.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // =================================================================
            // ── pnlTopBar (Thanh điều hướng đỉnh cao cấp - Tông màu Slate sâu) ─
            // =================================================================
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); // Tông màu đen Slate sâu thẳm
            this.pnlTopBar.Controls.Add(this.tlpTopBar);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1280, 50);
            this.pnlTopBar.TabIndex = 1;

            // ── tlpTopBar (SỬA LỖI ĐÈ NÚT ADMIN: Khóa cứng cột phải 200px) ─────
            this.tlpTopBar.ColumnCount = 2;
            this.tlpTopBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Cột trái chiếm trọn diện tích còn lại
            this.tlpTopBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F)); // Khóa chết 220px bảo vệ vùng phím Admin
            this.tlpTopBar.Controls.Add(this.pnlLeft, 0, 0);
            this.tlpTopBar.Controls.Add(this.pnlRight, 1, 0);
            this.tlpTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopBar.Location = new System.Drawing.Point(0, 0);
            this.tlpTopBar.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTopBar.Name = "tlpTopBar";
            this.tlpTopBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0); // Khoảng đệm hông biên rộng rãi
            this.tlpTopBar.RowCount = 1;
            this.tlpTopBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopBar.Size = new System.Drawing.Size(1280, 50);
            this.tlpTopBar.TabIndex = 0;

            // ── pnlLeft (Bảng dòng chảy chứa Logo và các Badge thông tin tài khoản) ──
            this.pnlLeft.Controls.Add(this.lblLogo);
            this.pnlLeft.Controls.Add(this.pnlPassengerInfo);
            this.pnlLeft.Controls.Add(this.pnlDriverInfo);
            this.pnlLeft.Controls.Add(this.lblSearchingStatus);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(16, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0); // Căn giữa trục dọc hoàn hảo cho dòng chảy
            this.pnlLeft.Size = new System.Drawing.Size(1028, 50);
            this.pnlLeft.TabIndex = 0;
            this.pnlLeft.WrapContents = false;

            // ── lblLogo (Thương hiệu hệ thống) ──────────────────────────
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 7);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(98, 32);
            this.lblLogo.Text = "RideGo";

            // ── pnlPassengerInfo (Badge trạng thái tài khoản Hành khách) ──
            this.pnlPassengerInfo.AutoSize = true;
            this.pnlPassengerInfo.BackColor = System.Drawing.Color.FromArgb(22, 163, 74); // Xanh lục chuẩn công nghệ phẳng
            this.pnlPassengerInfo.Controls.Add(this.flpPassenger);
            this.pnlPassengerInfo.Location = new System.Drawing.Point(118, 9);
            this.pnlPassengerInfo.Margin = new System.Windows.Forms.Padding(0, 2, 12, 0);
            this.pnlPassengerInfo.Name = "pnlPassengerInfo";
            this.pnlPassengerInfo.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.pnlPassengerInfo.Size = new System.Drawing.Size(146, 31);
            this.pnlPassengerInfo.TabIndex = 1;

            // ── flpPassenger ────────────────────────────────────────────
            this.flpPassenger.AutoSize = true;
            this.flpPassenger.Controls.Add(this.lblPassengerIcon);
            this.flpPassenger.Controls.Add(this.lblPassengerName);
            this.flpPassenger.Controls.Add(this.lnkPassengerChange);
            this.flpPassenger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPassenger.Location = new System.Drawing.Point(10, 4);
            this.flpPassenger.Margin = new System.Windows.Forms.Padding(0);
            this.flpPassenger.Name = "flpPassenger";
            this.flpPassenger.Size = new System.Drawing.Size(126, 23);
            this.flpPassenger.TabIndex = 0;
            this.flpPassenger.WrapContents = false;

            // ── lblPassengerIcon ────────────────────────────────────────
            this.lblPassengerIcon.AutoSize = true;
            this.lblPassengerIcon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPassengerIcon.ForeColor = System.Drawing.Color.White;
            this.lblPassengerIcon.Location = new System.Drawing.Point(0, 0);
            this.lblPassengerIcon.Margin = new System.Windows.Forms.Padding(0, 1, 4, 0);
            this.lblPassengerIcon.Name = "lblPassengerIcon";
            this.lblPassengerIcon.Size = new System.Drawing.Size(26, 21);
            this.lblPassengerIcon.Text = "👤";

            // ── lblPassengerName ────────────────────────────────────────
            this.lblPassengerName.AutoSize = true;
            this.lblPassengerName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPassengerName.ForeColor = System.Drawing.Color.White;
            this.lblPassengerName.Location = new System.Drawing.Point(30, 0);
            this.lblPassengerName.Name = "lblPassengerName";
            this.lblPassengerName.Size = new System.Drawing.Size(51, 21);
            this.lblPassengerName.Text = "Khách";

            // ── lnkPassengerChange ──────────────────────────────────────
            this.lnkPassengerChange.AutoSize = true;
            this.lnkPassengerChange.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lnkPassengerChange.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline; // Bỏ gạch chân thô cứng
            this.lnkPassengerChange.LinkColor = System.Drawing.Color.FromArgb(187, 247, 208); // Xanh lục pastel nhạt
            this.lnkPassengerChange.Location = new System.Drawing.Point(94, 0);
            this.lnkPassengerChange.Margin = new System.Windows.Forms.Padding(10, 1, 0, 0);
            this.lnkPassengerChange.Name = "lnkPassengerChange";
            this.lnkPassengerChange.Size = new System.Drawing.Size(32, 20);
            this.lnkPassengerChange.Text = "Đổi";
            this.lnkPassengerChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSwitchAcc_LinkClicked);

            // ── pnlDriverInfo (Badge trạng thái tài khoản Tài xế hoạt động) ──
            this.pnlDriverInfo.AutoSize = true;
            this.pnlDriverInfo.BackColor = System.Drawing.Color.FromArgb(249, 115, 22); // Màu cam công nghệ
            this.pnlDriverInfo.Controls.Add(this.flpDriver);
            this.pnlDriverInfo.Location = new System.Drawing.Point(276, 9);
            this.pnlDriverInfo.Margin = new System.Windows.Forms.Padding(0, 2, 16, 0);
            this.pnlDriverInfo.Name = "pnlDriverInfo";
            this.pnlDriverInfo.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.pnlDriverInfo.Size = new System.Drawing.Size(194, 31);
            this.pnlDriverInfo.TabIndex = 2;

            // ── flpDriver ───────────────────────────────────────────────
            this.flpDriver.AutoSize = true;
            this.flpDriver.Controls.Add(this.pnlOnlineDot);
            this.flpDriver.Controls.Add(this.lblDriverName);
            this.flpDriver.Controls.Add(this.lblOnline);
            this.flpDriver.Controls.Add(this.lnkDriverChange);
            this.flpDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDriver.Location = new System.Drawing.Point(10, 4);
            this.flpDriver.Margin = new System.Windows.Forms.Padding(0);
            this.flpDriver.Name = "flpDriver";
            this.flpDriver.Size = new System.Drawing.Size(174, 23);
            this.flpDriver.TabIndex = 0;
            this.flpDriver.WrapContents = false;

            // ── pnlOnlineDot (Chấm tròn nhỏ chỉ báo trực tuyến) ───────────
            this.pnlOnlineDot.BackColor = System.Drawing.Color.FromArgb(34, 197, 94); // Xanh lá cây rực rỡ
            this.pnlOnlineDot.Location = new System.Drawing.Point(0, 6);
            this.pnlOnlineDot.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.pnlOnlineDot.Name = "pnlOnlineDot";
            this.pnlOnlineDot.Size = new System.Drawing.Size(8, 8);
            this.pnlOnlineDot.TabIndex = 0;

            // ── lblDriverName ───────────────────────────────────────────
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDriverName.ForeColor = System.Drawing.Color.White;
            this.lblDriverName.Location = new System.Drawing.Point(14, 0);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(51, 21);
            this.lblDriverName.Text = "Tài xế";

            // ── lblOnline ───────────────────────────────────────────────
            this.lblOnline.AutoSize = true;
            this.lblOnline.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblOnline.ForeColor = System.Drawing.Color.FromArgb(254, 215, 170);
            this.lblOnline.Location = new System.Drawing.Point(68, 1);
            this.lblOnline.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(64, 20);
            this.lblOnline.Text = "(Online)";

            // ── lnkDriverChange ─────────────────────────────────────────
            this.lnkDriverChange.AutoSize = true;
            this.lnkDriverChange.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lnkDriverChange.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkDriverChange.LinkColor = System.Drawing.Color.FromArgb(254, 215, 170);
            this.lnkDriverChange.Location = new System.Drawing.Point(142, 1);
            this.lnkDriverChange.Margin = new System.Windows.Forms.Padding(10, 1, 0, 0);
            this.lnkDriverChange.Name = "lnkDriverChange";
            this.lnkDriverChange.Size = new System.Drawing.Size(32, 20);
            this.lnkDriverChange.Text = "Đổi";
            this.lnkDriverChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSwitchAcc_LinkClicked);

            // ── lblSearchingStatus (Nhãn thông báo hệ thống nổ cuốc) ──────
            this.lblSearchingStatus.AutoSize = true;
            this.lblSearchingStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchingStatus.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.lblSearchingStatus.Location = new System.Drawing.Point(486, 12);
            this.lblSearchingStatus.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblSearchingStatus.Name = "lblSearchingStatus";
            this.lblSearchingStatus.Size = new System.Drawing.Size(131, 21);
            this.lblSearchingStatus.Text = "🔍 Đang tìm tài xế...";
            this.lblSearchingStatus.Visible = false;

            // ── pnlRight (Vùng nút bám lề bên hữu của thanh điều hướng) ──
            this.pnlRight.Controls.Add(this.btnAdmin);
            this.pnlRight.Controls.Add(this.lblDriverCount);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlRight.Location = new System.Drawing.Point(1044, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0); // Đẩy lề dọc cân đối phím bấm
            this.pnlRight.Size = new System.Drawing.Size(220, 50);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.WrapContents = false;

            // ── btnAdmin (Nút truy cập Dashboard phân tích trung tâm) ───────
            this.btnAdmin.BackColor = System.Drawing.Color.FromArgb(79, 70, 229); // Đổi sang tông tím hoàng gia chuẩn Indigo tinh xảo
            this.btnAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdmin.FlatAppearance.BorderSize = 0;
            this.btnAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdmin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdmin.ForeColor = System.Drawing.Color.White;
            this.btnAdmin.Location = new System.Drawing.Point(115, 10);
            this.btnAdmin.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(105, 30); // SỬA: Tăng lên 105px bảo vệ chuỗi chữ không bị cắt cụt
            this.btnAdmin.Text = "⚙️ Quản trị";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.BtnAdmin_Click);

            // ── lblDriverCount (Nhãn đếm số tài xế online ngoài thực địa) ──
            this.lblDriverCount.AutoSize = true;
            this.lblDriverCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblDriverCount.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); // Tông xám nhạt Slate dịu mắt
            this.lblDriverCount.Location = new System.Drawing.Point(3, 15);
            this.lblDriverCount.Margin = new System.Windows.Forms.Padding(0, 5, 12, 0);
            this.lblDriverCount.Name = "lblDriverCount";
            this.lblDriverCount.Size = new System.Drawing.Size(100, 20);
            this.lblDriverCount.Text = "0 tài xế Online";

            // =================================================================
            // ── tlpMain (LƯỚI LÀM MÀN HÌNH CHÍNH - ĐỒNG BỘ KHỚP KHÍT SỐ ĐO) ──
            // =================================================================
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F)); // SỬA: Khóa chết độ rộng cột trái 330px
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Cột giữa (Bản đồ) co dãn 100% diện tích còn lại
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F)); // SỬA: Khóa chết độ rộng cột phải 330px
            this.tlpMain.Controls.Add(this._passengerPanel, 0, 0);
            this.tlpMain.Controls.Add(this._map, 1, 0);
            this.tlpMain.Controls.Add(this._driverHome, 2, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 50);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1280, 670);
            this.tlpMain.TabIndex = 0;

            // ── _passengerPanel ─────────────────────────────────────────
            this._passengerPanel.BackColor = System.Drawing.Color.White;
            this._passengerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._passengerPanel.Location = new System.Drawing.Point(0, 0);
            this._passengerPanel.Margin = new System.Windows.Forms.Padding(0);
            this._passengerPanel.MinimumSize = new System.Drawing.Size(300, 480);
            this._passengerPanel.Name = "_passengerPanel";
            this._passengerPanel.Size = new System.Drawing.Size(330, 670); // SỬA CHÍ MẠNG: Đồng bộ khít chặn bằng 330px theo ô lưới TableLayout
            this._passengerPanel.TabIndex = 0;

            // ── _map ────────────────────────────────────────────────────
            this._map.Dock = System.Windows.Forms.DockStyle.Fill;
            this._map.Location = new System.Drawing.Point(330, 0);
            this._map.Margin = new System.Windows.Forms.Padding(0);
            this._map.Name = "_map";
            this._map.Size = new System.Drawing.Size(620, 670);
            this._map.TabIndex = 1;

            // ── _driverHome ─────────────────────────────────────────────
            this._driverHome.BackColor = System.Drawing.Color.White;
            this._driverHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this._driverHome.Location = new System.Drawing.Point(950, 0);
            this._driverHome.Margin = new System.Windows.Forms.Padding(0);
            this._driverHome.Name = "_driverHome";
            this._driverHome.Size = new System.Drawing.Size(330, 670); // SỬA CHÍ MẠNG: Đồng bộ khít chặn bằng 330px theo ô lưới TableLayout
            this._driverHome.TabIndex = 2;

            // =================================================================
            // ── FrmMultiRole (Cửa sổ biểu mẫu Tổng thể) ──────────────────────
            // =================================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.pnlTopBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(1024, 600); // Khóa lùi bảo vệ cấu trúc đa góc nhìn
            this.Name = "FrmMultiRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RideGo — Hệ thống Giả lập Điều phối Giao dịch Đa vai trò (OOP2026)";
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
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.TableLayoutPanel tlpTopBar;
        private System.Windows.Forms.FlowLayoutPanel pnlLeft;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSearchingStatus;
        private System.Windows.Forms.Panel pnlPassengerInfo;
        private System.Windows.Forms.FlowLayoutPanel flpPassenger;
        private System.Windows.Forms.Label lblPassengerIcon;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.LinkLabel lnkPassengerChange;
        private System.Windows.Forms.Panel pnlDriverInfo;
        private System.Windows.Forms.FlowLayoutPanel flpDriver;
        private System.Windows.Forms.Panel pnlOnlineDot;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label lblOnline;
        private System.Windows.Forms.LinkLabel lnkDriverChange;
        private System.Windows.Forms.FlowLayoutPanel pnlRight;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Label lblDriverCount;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ucMap _map;
        private ucPassengerHome _passengerPanel;
        private ucDriverHome _driverHome;
    }
}