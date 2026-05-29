namespace OOP2026
{
    partial class ucPassengerHome
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tlpHeaderLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblTrips = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabDatxe = new System.Windows.Forms.TabPage();
            this.tabChuyendi = new System.Windows.Forms.TabPage();
            this.tabLichsu = new System.Windows.Forms.TabPage();
            this.tabHoso = new System.Windows.Forms.TabPage();
            this.pnlTop.SuspendLayout();
            this.tlpHeaderLayout.SuspendLayout();
            this.tab.SuspendLayout();
            this.SuspendLayout();

            // ── pnlTop (Thanh thông tin phía trên - Tông màu xanh lá công nghệ) ──
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(13, 192, 123); // Giữ nguyên màu xanh thương hiệu cực đẹp
            this.pnlTop.Controls.Add(this.tlpHeaderLayout);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20, 16, 20, 12); // Tinh chỉnh khoảng đệm bảo vệ viền
            this.pnlTop.Size = new System.Drawing.Size(300, 115); // Tăng nhẹ 5px để không gian thở thoải mái hơn
            this.pnlTop.TabIndex = 0;

            // ── tlpHeaderLayout (Giải pháp chống đè chữ khi đổi tên dài) ──
            this.tlpHeaderLayout.BackColor = System.Drawing.Color.Transparent;
            this.tlpHeaderLayout.ColumnCount = 1;
            this.tlpHeaderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderLayout.Controls.Add(this.lblName, 0, 0);
            this.tlpHeaderLayout.Controls.Add(this.lblPhone, 0, 1);
            this.tlpHeaderLayout.Controls.Add(this.lblTrips, 0, 2);
            this.tlpHeaderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeaderLayout.Location = new System.Drawing.Point(20, 16);
            this.tlpHeaderLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeaderLayout.Name = "tlpHeaderLayout";
            this.tlpHeaderLayout.RowCount = 3;
            this.tlpHeaderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng tên hành khách
            this.tlpHeaderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng số điện thoại
            this.tlpHeaderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng số chuyến đi chiếm đáy
            this.tlpHeaderLayout.Size = new System.Drawing.Size(260, 87);
            this.tlpHeaderLayout.TabIndex = 0;

            // ── lblName (Nhãn hiển thị tên Hành khách) ─────────────────
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold); // Khóa font cứng thay thế biến tĩnh tránh lỗi designer
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(260, 31);
            this.lblName.Text = "Hành khách";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblPhone (Nhãn hiển thị SĐT) ───────────────────────────
            this.lblPhone.AutoSize = true;
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(220, 252, 231); // Đổi sang tông xanh mint nhạt cực sang
            this.lblPhone.Location = new System.Drawing.Point(0, 33);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(260, 20);
            this.lblPhone.Text = "0901234567";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblTrips (Nhãn hiển thị số chuyến tích lũy) ──────────────
            this.lblTrips.AutoSize = true;
            this.lblTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrips.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTrips.ForeColor = System.Drawing.Color.White;
            this.lblTrips.Location = new System.Drawing.Point(0, 57);
            this.lblTrips.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrips.Name = "lblTrips";
            this.lblTrips.Size = new System.Drawing.Size(260, 30);
            this.lblTrips.Text = "✨ Tích lũy: 0 chuyến";
            this.lblTrips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── tab (Hệ thống điều hướng chính điều khiển TabControl) ──
            this.tab.Controls.Add(this.tabDatxe);
            this.tab.Controls.Add(this.tabChuyendi);
            this.tab.Controls.Add(this.tabLichsu);
            this.tab.Controls.Add(this.tabHoso);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Segoe UI", 9.5F); // Đồng bộ font chữ hệ thống Tab điều hướng
            this.tab.Location = new System.Drawing.Point(0, 115);
            this.tab.Margin = new System.Windows.Forms.Padding(0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(300, 485);
            this.tab.TabIndex = 1;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.Tab_SelectedIndexChanged);

            // ── tabDatxe (Tab Nghiệp vụ đặt chuyến mới) ─────────────────
            this.tabDatxe.BackColor = System.Drawing.Color.White;
            this.tabDatxe.Location = new System.Drawing.Point(4, 30);
            this.tabDatxe.Name = "tabDatxe";
            this.tabDatxe.Padding = new System.Windows.Forms.Padding(12); // Tăng padding lên 12px rông rãi cho các Control con lồng vào sau này
            this.tabDatxe.Size = new System.Drawing.Size(292, 451);
            this.tabDatxe.TabIndex = 0;
            this.tabDatxe.Text = "Đặt xe";

            // ── tabChuyendi (Tab Theo dõi trạng thái chuyến đi hiện tại) ──
            this.tabChuyendi.BackColor = System.Drawing.Color.White;
            this.tabChuyendi.Location = new System.Drawing.Point(4, 30);
            this.tabChuyendi.Name = "tabChuyendi";
            this.tabChuyendi.Padding = new System.Windows.Forms.Padding(12);
            this.tabChuyendi.Size = new System.Drawing.Size(292, 451);
            this.tabChuyendi.TabIndex = 1;
            this.tabChuyendi.Text = "Chuyến đi";

            // ── tabLichsu (Tab Xem danh sách chuyến đi trong quá khứ) ──
            this.tabLichsu.BackColor = System.Drawing.Color.White;
            this.tabLichsu.Location = new System.Drawing.Point(4, 30);
            this.tabLichsu.Name = "tabLichsu";
            this.tabLichsu.Padding = new System.Windows.Forms.Padding(12);
            this.tabLichsu.Size = new System.Drawing.Size(292, 451);
            this.tabLichsu.TabIndex = 2;
            this.tabLichsu.Text = "Lịch sử";

            // ── tabHoso (Tab Chỉnh sửa thông tin cá nhân & Đổi mật khẩu) ──
            this.tabHoso.BackColor = System.Drawing.Color.White;
            this.tabHoso.Location = new System.Drawing.Point(4, 30);
            this.tabHoso.Name = "tabHoso";
            this.tabHoso.Padding = new System.Windows.Forms.Padding(12);
            this.tabHoso.Size = new System.Drawing.Size(292, 451);
            this.tabHoso.TabIndex = 3;
            this.tabHoso.Text = "Hồ sơ";

            // ── ucPassengerHome (Cấu hình tổng thể UserControl chính) ──
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tab);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(300, 500);
            this.Name = "ucPassengerHome";
            this.Size = new System.Drawing.Size(300, 600);
            this.pnlTop.ResumeLayout(false);
            this.tlpHeaderLayout.ResumeLayout(false);
            this.tlpHeaderLayout.PerformLayout();
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        #region Controls declaration
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderLayout;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlTopSpacer;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblTrips;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabDatxe;
        private System.Windows.Forms.TabPage tabChuyendi;
        private System.Windows.Forms.TabPage tabLichsu;
        private System.Windows.Forms.TabPage tabHoso;
        #endregion
    }
}