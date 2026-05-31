namespace OOP2026
{
    partial class ucDriverCard
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
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAvatar = new System.Windows.Forms.Panel();
            this.lblAvatarEmoji = new System.Windows.Forms.Label();
            this.tlpInfoGrid = new System.Windows.Forms.TableLayoutPanel();
            this.tlpNameRow = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.pnlButtonWrapper = new System.Windows.Forms.Panel();
            this.btnCall = new System.Windows.Forms.Button();

            this.tlpMainLayout.SuspendLayout();
            this.pnlAvatar.SuspendLayout();
            this.tlpInfoGrid.SuspendLayout();
            this.tlpNameRow.SuspendLayout();
            this.pnlButtonWrapper.SuspendLayout();
            this.SuspendLayout();

            // ── tlpMainLayout (Lưới tổng thể phân chia 3 khu vực chức năng) ──
            this.tlpMainLayout.ColumnCount = 3;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));   // Cột 1: Vùng Avatar
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Cột 2: Khối văn bản tự co giãn
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));   // Cột 3: Vùng chứa nút Gọi điện
            this.tlpMainLayout.Controls.Add(this.pnlAvatar, 0, 0);
            this.tlpMainLayout.Controls.Add(this.tlpInfoGrid, 1, 0);
            this.tlpMainLayout.Controls.Add(this.pnlButtonWrapper, 2, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(14, 10);
            this.tlpMainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 1;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Size = new System.Drawing.Size(422, 66);
            this.tlpMainLayout.TabIndex = 0;

            // ── pnlAvatar (Khung bọc ảnh đại diện tròn giả lập) ──
            this.pnlAvatar.BackColor = System.Drawing.Color.FromArgb(249, 115, 22); // Đổi sang tông cam hiện đại chuẩn chuỗi hệ thống
            this.pnlAvatar.Controls.Add(this.lblAvatarEmoji);
            this.pnlAvatar.Dock = System.Windows.Forms.DockStyle.Top; // Đặt bám sát lề trên ô lưới
            this.pnlAvatar.Location = new System.Drawing.Point(0, 2);
            this.pnlAvatar.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pnlAvatar.Name = "pnlAvatar";
            this.pnlAvatar.Size = new System.Drawing.Size(56, 56); // Giữ nguyên kích thước 56x56 vuông vắn
            this.pnlAvatar.TabIndex = 0;

            // ── lblAvatarEmoji ───────────────────────────────────────────────
            this.lblAvatarEmoji.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatarEmoji.Font = new System.Drawing.Font("Segoe UI Emoji", 22F);
            this.lblAvatarEmoji.ForeColor = System.Drawing.Color.White;
            this.lblAvatarEmoji.Location = new System.Drawing.Point(0, 0);
            this.lblAvatarEmoji.Name = "lblAvatarEmoji";
            this.lblAvatarEmoji.Size = new System.Drawing.Size(56, 56);
            this.lblAvatarEmoji.Text = "🧑";
            this.lblAvatarEmoji.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── tlpInfoGrid (Lưới nội bộ xếp dọc khối văn bản - Chống tuyệt đối lỗi đè chữ) ──
            this.tlpInfoGrid.ColumnCount = 1;
            this.tlpInfoGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfoGrid.Controls.Add(this.tlpNameRow, 0, 0);
            this.tlpInfoGrid.Controls.Add(this.lblPhone, 0, 1);
            this.tlpInfoGrid.Controls.Add(this.lblVehicle, 0, 2);
            this.tlpInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfoGrid.Location = new System.Drawing.Point(70, 0);
            this.tlpInfoGrid.Margin = new System.Windows.Forms.Padding(0);
            this.tlpInfoGrid.Name = "tlpInfoGrid";
            this.tlpInfoGrid.RowCount = 3;
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // Hàng tên tài xế + Số sao
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));  // Hàng số điện thoại
            this.tlpInfoGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Hàng thông tin xe chiếm phần đáy lưới
            this.tlpInfoGrid.Size = new System.Drawing.Size(262, 66);
            this.tlpInfoGrid.TabIndex = 1;

            // ── tlpNameRow (Hàng ngang thông minh lồng ghép Tên tài xế và Số sao) ──
            this.tlpNameRow.ColumnCount = 2;
            this.tlpNameRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize)); // Tên tài xế tự co giãn độ rộng
            this.tlpNameRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Số sao bám sát ngay phía sau
            this.tlpNameRow.Controls.Add(this.lblName, 0, 0);
            this.tlpNameRow.Controls.Add(this.lblRating, 1, 0);
            this.tlpNameRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNameRow.Location = new System.Drawing.Point(0, 0);
            this.tlpNameRow.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNameRow.Name = "tlpNameRow";
            this.tlpNameRow.RowCount = 1;
            this.tlpNameRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNameRow.Size = new System.Drawing.Size(262, 24);
            this.tlpNameRow.TabIndex = 0;

            // ── lblName (Nhãn hiển thị tên tài xế) ──────────────────────────
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42); // Tông đen Slate sâu thẳm cực sang
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(84, 24);
            this.lblName.Text = "Tên tài xế";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblRating (Nhãn hiển thị số sao đánh giá - Sửa lỗi đè chữ) ──
            this.lblRating.AutoSize = true;
            this.lblRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); // Đậm chữ số sao nổi bật hiệu suất
            this.lblRating.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11); // Sử dụng tông màu cam Amber UI chuẩn
            this.lblRating.Location = new System.Drawing.Point(90, 0); // Đứng cách tên tài xế một khoảng đệm nhỏ 6px an toàn
            this.lblRating.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(172, 24);
            this.lblRating.Text = "⭐ 5.0";
            this.lblRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; // Căn lề trái đứng khít đuôi chữ Tên

            // ── lblPhone (Nhãn hiển thị SĐT tài xế) ─────────────────────────
            this.lblPhone.AutoSize = true;
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); // Đổi sang tông xám Slate dịu mắt hơn
            this.lblPhone.Location = new System.Drawing.Point(0, 24);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(262, 18);
            this.lblPhone.Text = "090xxxxxxx";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblVehicle (Nhãn hiển thị Loại xe và Biển số xe thực tế) ────
            this.lblVehicle.AutoSize = true;
            this.lblVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVehicle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblVehicle.Location = new System.Drawing.Point(0, 44);
            this.lblVehicle.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(262, 22);
            this.lblVehicle.Text = "🚙 Xe máy • Biển số: 29A1-234.56";
            this.lblVehicle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── pnlButtonWrapper (Vùng khóa chặt vị trí nút bấm bên bìa phải) ──
            this.pnlButtonWrapper.Controls.Add(this.btnCall);
            this.pnlButtonWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtonWrapper.Location = new System.Drawing.Point(332, 0);
            this.pnlButtonWrapper.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtonWrapper.Name = "pnlButtonWrapper";
            this.pnlButtonWrapper.Size = new System.Drawing.Size(90, 66);
            this.pnlButtonWrapper.TabIndex = 2;

            // ── btnCall (Nút hành động Gọi điện chính) ─────────────────────
            this.btnCall.Anchor = System.Windows.Forms.AnchorStyles.Right; // Neo chặt bên phải ô lưới cố định 90px
            this.btnCall.BackColor = System.Drawing.Color.FromArgb(34, 197, 94); // Chuyển đổi sang màu xanh Green hiện đại tinh tế (chuẩn Tailwind)
            this.btnCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCall.FlatAppearance.BorderSize = 0;
            this.btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCall.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold); // Đậm chữ phím bấm dễ thao tác nhanh
            this.btnCall.ForeColor = System.Drawing.Color.White;
            this.btnCall.Location = new System.Drawing.Point(6, 14); // Căn giữa dòng theo trục dọc hoàn hảo
            this.btnCall.Margin = new System.Windows.Forms.Padding(0);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(84, 35); // Tăng diện tích phím bấm dày dặn chống tương tác trượt
            this.btnCall.Text = "📞 Gọi điện";
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.BtnCall_Click);

            // ── ucDriverCard (Main UserControl) ───────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMainLayout);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10); // Khoảng trống ngăn cách các thẻ khi lồng vào list cuộn
            this.Name = "ucDriverCard";
            this.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10); // Đồng bộ khoảng đệm biên đều đặn bốn phía vuông vắn
            this.Size = new System.Drawing.Size(450, 86);
            this.tlpMainLayout.ResumeLayout(false);
            this.pnlAvatar.ResumeLayout(false);
            this.tlpInfoGrid.ResumeLayout(false);
            this.tlpInfoGrid.PerformLayout();
            this.tlpNameRow.ResumeLayout(false);
            this.tlpNameRow.PerformLayout();
            this.pnlButtonWrapper.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.Panel pnlAvatar;
        private System.Windows.Forms.Label lblAvatarEmoji;
        private System.Windows.Forms.TableLayoutPanel tlpInfoGrid;
        private System.Windows.Forms.TableLayoutPanel tlpNameRow;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.Panel pnlButtonWrapper;
        private System.Windows.Forms.Button btnCall;
    }
}