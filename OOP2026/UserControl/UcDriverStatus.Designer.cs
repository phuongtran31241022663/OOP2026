namespace OOP2026
{
    partial class ucDriverStatus
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
            this.pnlToggle = new System.Windows.Forms.Panel();
            this.btnToggleStatus = new System.Windows.Forms.Button();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIncomeCard = new System.Windows.Forms.Panel();
            this.lblIncomeTitle = new System.Windows.Forms.Label();
            this.lblTodayIncome = new System.Windows.Forms.Label();
            this.pnlWalletCard = new System.Windows.Forms.Panel();
            this.lblWalletTitle = new System.Windows.Forms.Label();
            this.lblWalletBalance = new System.Windows.Forms.Label();
            this.pnlVehicle = new System.Windows.Forms.Panel();
            this.tlpVehicleDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.lblVehiclePlate = new System.Windows.Forms.Label();
            this.lblVehicleColor = new System.Windows.Forms.Label();
            this.lblVehicleType = new System.Windows.Forms.Label();

            this.tlpMain.SuspendLayout();
            this.pnlToggle.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlIncomeCard.SuspendLayout();
            this.pnlWalletCard.SuspendLayout();
            this.pnlVehicle.SuspendLayout();
            this.tlpVehicleDetails.SuspendLayout();
            this.SuspendLayout();

            // ── tlpMain (Khung bố cục tổng thể 3 tầng hành động) ───────
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlToggle, 0, 0);
            this.tlpMain.Controls.Add(this.tlpStats, 0, 1);
            this.tlpMain.Controls.Add(this.pnlVehicle, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(12, 12); // Đồng bộ lề an toàn rộng rãi 12px
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F)); // Hàng nút bật/tắt nhận cuốc
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F)); // SỬA: Tăng lên 100px bảo vệ không gian thẻ thống kê
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Khối thông tin xe chiếm trọn phần đáy
            this.tlpMain.Size = new System.Drawing.Size(276, 376);
            this.tlpMain.TabIndex = 0;

            // ── pnlToggle (Thanh trạng thái hoạt động chính) ────────────
            this.pnlToggle.BackColor = System.Drawing.Color.White;
            this.pnlToggle.Controls.Add(this.btnToggleStatus);
            this.pnlToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToggle.Location = new System.Drawing.Point(0, 0);
            this.pnlToggle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12); // Tạo khoảng đệm với hàng dưới
            this.pnlToggle.Name = "pnlToggle";
            this.pnlToggle.Size = new System.Drawing.Size(276, 40);
            this.pnlToggle.TabIndex = 0;

            // ── btnToggleStatus (Nút bật/tắt nhận cuốc siêu to dễ thao tác) ──
            this.btnToggleStatus.BackColor = System.Drawing.Color.White;
            this.btnToggleStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(245, 158, 11); // Viền màu cam hổ phách sắc nét
            this.btnToggleStatus.FlatAppearance.BorderSize = 1;
            this.btnToggleStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnToggleStatus.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11);
            this.btnToggleStatus.Location = new System.Drawing.Point(0, 0);
            this.btnToggleStatus.Name = "btnToggleStatus";
            this.btnToggleStatus.Size = new System.Drawing.Size(276, 40);
            this.btnToggleStatus.TabIndex = 1;
            this.btnToggleStatus.Text = "🔴 Đang tắt nhận cuốc"; // Thêm emoji trực quan hóa trạng thái
            this.btnToggleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnToggleStatus.UseVisualStyleBackColor = false;
            this.btnToggleStatus.Click += new System.EventHandler(this.BtnToggleStatus_Click);

            // ── tlpStats (Lưới chia đôi đều đặn cho 2 thẻ thống kê nhanh) ──
            this.tlpStats.ColumnCount = 2;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.Controls.Add(this.pnlIncomeCard, 0, 0);
            this.tlpStats.Controls.Add(this.pnlWalletCard, 1, 0);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Location = new System.Drawing.Point(0, 52);
            this.tlpStats.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(276, 100);
            this.tlpStats.TabIndex = 1;

            // ── pnlIncomeCard (Thẻ doanh thu của ngày) ───────────────────
            this.pnlIncomeCard.BackColor = System.Drawing.Color.White;
            this.pnlIncomeCard.Controls.Add(this.lblIncomeTitle);
            this.pnlIncomeCard.Controls.Add(this.lblTodayIncome);
            this.pnlIncomeCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIncomeCard.Location = new System.Drawing.Point(0, 0);
            // SỬA LỖI LỆCH TRỤC: Đồng bộ Margin cân đối 4px hai bên hông
            this.pnlIncomeCard.Margin = new System.Windows.Forms.Padding(0, 0, 4, 12);
            this.pnlIncomeCard.Name = "pnlIncomeCard";
            this.pnlIncomeCard.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8); // Dùng padding bảo vệ text nội bộ
            this.pnlIncomeCard.Size = new System.Drawing.Size(134, 88);
            this.pnlIncomeCard.TabIndex = 0;

            // ── lblIncomeTitle ──────────────────────────────────────────
            this.lblIncomeTitle.AutoSize = true;
            this.lblIncomeTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Đổi sang tông xám Slate dịu mắt
            this.lblIncomeTitle.Location = new System.Drawing.Point(10, 8);
            this.lblIncomeTitle.Name = "lblIncomeTitle";
            this.lblIncomeTitle.Size = new System.Drawing.Size(95, 20);
            this.lblIncomeTitle.TabIndex = 1;
            this.lblIncomeTitle.Text = "Thu nhập hôm nay";

            // ── lblTodayIncome ──────────────────────────────────────────
            this.lblTodayIncome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTodayIncome.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Đen Slate sâu thẳm quyền lực
            this.lblTodayIncome.Location = new System.Drawing.Point(3, 35);
            this.lblTodayIncome.Name = "lblTodayIncome";
            this.lblTodayIncome.Size = new System.Drawing.Size(128, 35);
            this.lblTodayIncome.TabIndex = 0;
            this.lblTodayIncome.Text = "0đ";
            this.lblTodayIncome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlWalletCard (Thẻ số dư tài khoản ví công nghệ) ────────
            this.pnlWalletCard.BackColor = System.Drawing.Color.FromArgb(249, 115, 22); // Màu cam chuẩn Tailwind công nghệ trẻ trung
            this.pnlWalletCard.Controls.Add(this.lblWalletTitle);
            this.pnlWalletCard.Controls.Add(this.lblWalletBalance);
            this.pnlWalletCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWalletCard.Location = new System.Drawing.Point(138, 0);
            this.pnlWalletCard.Margin = new System.Windows.Forms.Padding(4, 0, 0, 12);
            this.pnlWalletCard.Name = "pnlWalletCard";
            this.pnlWalletCard.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlWalletCard.Size = new System.Drawing.Size(138, 88);
            this.pnlWalletCard.TabIndex = 1;

            // ── lblWalletTitle ──────────────────────────────────────────
            this.lblWalletTitle.AutoSize = true;
            this.lblWalletTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblWalletTitle.ForeColor = System.Drawing.Color.FromArgb(254, 215, 170); // Cam pastel cực nhạt sang trọng
            this.lblWalletTitle.Location = new System.Drawing.Point(10, 8);
            this.lblWalletTitle.Name = "lblWalletTitle";
            this.lblWalletTitle.Size = new System.Drawing.Size(63, 20);
            this.lblWalletTitle.TabIndex = 1;
            this.lblWalletTitle.Text = "Số dư ví tài khoản";

            // ── lblWalletBalance ────────────────────────────────────────
            this.lblWalletBalance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWalletBalance.ForeColor = System.Drawing.Color.White;
            this.lblWalletBalance.Location = new System.Drawing.Point(3, 35);
            this.lblWalletBalance.Name = "lblWalletBalance";
            this.lblWalletBalance.Size = new System.Drawing.Size(132, 35);
            this.lblWalletBalance.TabIndex = 0;
            this.lblWalletBalance.Text = "0đ";
            this.lblWalletBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlVehicle (Khối thông tin phương tiện hoạt động) ────────
            this.pnlVehicle.BackColor = System.Drawing.Color.White;
            this.pnlVehicle.Controls.Add(this.tlpVehicleDetails);
            this.pnlVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehicle.Location = new System.Drawing.Point(0, 152);
            this.pnlVehicle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlVehicle.Name = "pnlVehicle";
            this.pnlVehicle.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14); // Khởi tạo khoảng đệm lòng an toàn
            this.pnlVehicle.Size = new System.Drawing.Size(276, 224);
            this.pnlVehicle.TabIndex = 2;

            // ── tlpVehicleDetails (SỬA LỖI CHẾT TOẠ ĐỘ: Lưới quản lý thông tin xe) ──
            this.tlpVehicleDetails.ColumnCount = 1;
            this.tlpVehicleDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicleDetails.Controls.Add(this.lblVehicleName, 0, 0);
            this.tlpVehicleDetails.Controls.Add(this.lblVehiclePlate, 0, 1);
            this.tlpVehicleDetails.Controls.Add(this.lblVehicleColor, 0, 2);
            this.tlpVehicleDetails.Controls.Add(this.lblVehicleType, 0, 3);
            this.tlpVehicleDetails.Dock = System.Windows.Forms.DockStyle.Top; // Đặt neo sát lề trên
            this.tlpVehicleDetails.Location = new System.Drawing.Point(16, 14);
            this.tlpVehicleDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicleDetails.Name = "tlpVehicleDetails";
            this.tlpVehicleDetails.RowCount = 4;
            this.tlpVehicleDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Tên xe tự dãn dòng nếu dài
            this.tlpVehicleDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpVehicleDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpVehicleDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpVehicleDetails.Size = new System.Drawing.Size(244, 125);
            this.tlpVehicleDetails.TabIndex = 0;

            // ── lblVehicleName ──────────────────────────────────────────
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblVehicleName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblVehicleName.Location = new System.Drawing.Point(0, 0);
            this.lblVehicleName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8); // Khoảng đệm rộng ngăn cách các dòng dưới
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.Size = new System.Drawing.Size(244, 30);
            this.lblVehicleName.Text = "Tên phương tiện";
            this.lblVehicleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblVehiclePlate ──────────────────────────────────────────
            this.lblVehiclePlate.AutoSize = true;
            this.lblVehiclePlate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePlate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVehiclePlate.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVehiclePlate.Location = new System.Drawing.Point(0, 38);
            this.lblVehiclePlate.Name = "lblVehiclePlate";
            this.lblVehiclePlate.Size = new System.Drawing.Size(244, 28);
            this.lblVehiclePlate.Text = "🪪 Biển số: ";
            this.lblVehiclePlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblVehicleColor ──────────────────────────────────────────
            this.lblVehicleColor.AutoSize = true;
            this.lblVehicleColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleColor.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVehicleColor.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVehicleColor.Location = new System.Drawing.Point(0, 66);
            this.lblVehicleColor.Name = "lblVehicleColor";
            this.lblVehicleColor.Size = new System.Drawing.Size(244, 28);
            this.lblVehicleColor.Text = "🎨 Màu sắc: ";
            this.lblVehicleColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblVehicleType ──────────────────────────────────────────
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVehicleType.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVehicleType.Location = new System.Drawing.Point(0, 94);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(244, 28);
            this.lblVehicleType.Text = "🚗 Dịch vụ: ";
            this.lblVehicleType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── ucDriverStatus (Cấu hình tổng thể UserControl) ───────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); // Đổi sang xám Slate nhạt cực sang để làm nổi bật các khối trắng bên trên
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(280, 300);
            this.Name = "ucDriverStatus";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(300, 400);
            this.tlpMain.ResumeLayout(false);
            this.pnlToggle.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlIncomeCard.ResumeLayout(false);
            this.pnlIncomeCard.PerformLayout();
            this.pnlWalletCard.ResumeLayout(false);
            this.pnlWalletCard.PerformLayout();
            this.pnlVehicle.ResumeLayout(false);
            this.tlpVehicleDetails.ResumeLayout(false);
            this.tlpVehicleDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlToggle;
        private System.Windows.Forms.Button btnToggleStatus;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Panel pnlIncomeCard;
        private System.Windows.Forms.Label lblIncomeTitle;
        private System.Windows.Forms.Label lblTodayIncome;
        private System.Windows.Forms.Panel pnlWalletCard;
        private System.Windows.Forms.Label lblWalletTitle;
        private System.Windows.Forms.Label lblWalletBalance;
        private System.Windows.Forms.Panel pnlVehicle;
        private System.Windows.Forms.TableLayoutPanel tlpVehicleDetails;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.Label lblVehiclePlate;
        private System.Windows.Forms.Label lblVehicleColor;
        private System.Windows.Forms.Label lblVehicleType;
    }
}