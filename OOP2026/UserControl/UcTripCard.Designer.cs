namespace OOP2026
{
    partial class ucTripCard
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.lblNetEarnings = new System.Windows.Forms.Label();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDropoff = new System.Windows.Forms.Label();
            this.lblTripParams = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();

            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();

            // =================================================================
            // ========== tlpMain (Cải tiến hàng 2, 3 sang AutoSize) ==========
            // =================================================================
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F)); // Tăng không gian cho text dịch vụ
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMain.Controls.Add(this.lblServiceType, 0, 0);
            this.tlpMain.Controls.Add(this.lblNetEarnings, 1, 0);
            this.tlpMain.Controls.Add(this.lblPickup, 0, 1);
            this.tlpMain.Controls.Add(this.lblDropoff, 0, 2);
            this.tlpMain.Controls.Add(this.lblTripParams, 0, 3);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(16, 16);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Điểm đón tự co giãn theo độ dài địa chỉ
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Điểm đến tự co giãn theo độ dài địa chỉ
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.Size = new System.Drawing.Size(330, 220);
            this.tlpMain.TabIndex = 0;

            // ========== lblServiceType (Tag loại xe) ==========
            this.lblServiceType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServiceType.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            this.lblServiceType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblServiceType.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.lblServiceType.Location = new System.Drawing.Point(3, 10);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(95, 30);
            this.lblServiceType.Text = "🚗 Ô tô";
            this.lblServiceType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // Tận dụng kĩ thuật bo góc nhẹ nếu class có hỗ trợ vẽ (tạm thời dùng phẳng sạch sẽ)

            // ========== lblNetEarnings (Số tiền tài xế thực nhận - Tiêu điểm thị giác) ==========
            this.lblNetEarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetEarnings.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold); // Tăng size chữ tiền to rõ ràng
            this.lblNetEarnings.ForeColor = System.Drawing.Color.FromArgb(249, 115, 22); // Màu cam công nghệ nổi bật
            this.lblNetEarnings.Location = new System.Drawing.Point(184, 0);
            this.lblNetEarnings.Name = "lblNetEarnings";
            this.lblNetEarnings.Size = new System.Drawing.Size(143, 50);
            this.lblNetEarnings.Text = "45.000đ";
            this.lblNetEarnings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ========== lblPickup (Điểm đón khách) ==========
            this.tlpMain.SetColumnSpan(this.lblPickup, 2);
            this.lblPickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPickup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Đậm chữ điểm đón để tài xế chú ý
            this.lblPickup.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblPickup.Location = new System.Drawing.Point(3, 55); // Margin trên thoáng
            this.lblPickup.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblPickup.Padding = new System.Windows.Forms.Padding(24, 4, 4, 4); // Dành 24px lề trái để chứa Icon đồ họa (ví dụ: chấm tròn xanh)
            this.lblPickup.Text = "Đại học Kinh tế UEH - Cơ sở A";
            this.lblPickup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ========== lblDropoff (Điểm đến) ==========
            this.tlpMain.SetColumnSpan(this.lblDropoff, 2);
            this.lblDropoff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropoff.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDropoff.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblDropoff.Location = new System.Drawing.Point(3, 100);
            this.lblDropoff.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblDropoff.Padding = new System.Windows.Forms.Padding(24, 4, 4, 4); // Dành 24px lề trái để chứa Icon đồ họa (ví dụ: chấm tròn đỏ)
            this.lblDropoff.Text = "Chợ Bến Thành";
            this.lblDropoff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ========== lblTripParams (Thông số khoảng cách / thời gian) ==========
            this.tlpMain.SetColumnSpan(this.lblTripParams, 2);
            this.lblTripParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTripParams.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTripParams.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblTripParams.Location = new System.Drawing.Point(0, 140);
            this.lblTripParams.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblTripParams.Text = "📏 4.2 km  •  ⏱️ 15 phút  •  Cước gốc: 60.000đ";
            this.lblTripParams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // =================================================================
            // ========== tlpButtons (Cải tiến tỷ lệ 35% - 65% chống bấm nhầm) ==========
            // =================================================================
            this.tlpButtons.ColumnCount = 2;
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F)); // Nút từ chối bé lại
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F)); // Nút nhận cuốc siêu to dễ bấm
            this.tlpButtons.Controls.Add(this.btnReject, 0, 0);
            this.tlpButtons.Controls.Add(this.btnAccept, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 175);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(330, 45);

            // ========== btnReject (Nút từ chối tinh gọn) ==========
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(241, 245, 249); // Đổi sang xám nhạt tinh tế, giảm kích thích trực giác ghét bỏ
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Chuyển chữ sang màu xám đậm thanh lịch
            this.btnReject.Location = new System.Drawing.Point(2, 2);
            this.btnReject.Margin = new System.Windows.Forms.Padding(2, 2, 6, 2); // Khoảng trống an toàn ở bên phải
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(107, 41);
            this.btnReject.Text = "Bỏ qua"; // Chuyển từ "Từ chối" sang "Bỏ qua" nghe nhẹ nhàng hơn về mặt UX
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.BtnReject_Click);

            // ========== btnAccept (Nút Nhận cuốc chủ đạo) ==========
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(34, 197, 94); // Chuyển sang màu Xanh Lá Cây (Green) đại diện cho sự kích thích và đồng ý nhận cuốc
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); // Tăng size chữ hành động chính
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(121, 2);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(207, 41);
            this.btnAccept.Text = "NHẬN CUỐC";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.BtnAccept_Click);

            // ========== ucTripCard (Cấu hình bóng đổ/khung viền bên ngoài) ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucTripCard";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(362, 252);

            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.Label lblNetEarnings;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.Label lblDropoff;
        private System.Windows.Forms.Label lblTripParams;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnAccept;
        #endregion
    }
}