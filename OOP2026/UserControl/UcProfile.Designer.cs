using System;
using System.Windows.Forms;

namespace OOP2026
{
    partial class ucProfile
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

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPersonalInfo = new System.Windows.Forms.Panel();
            this.tlpPersonalInfo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAvatarBg = new System.Windows.Forms.Panel();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblTrips = new System.Windows.Forms.Label();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblChangePassTitle = new System.Windows.Forms.Label();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.btnChangePass = new System.Windows.Forms.Button();

            this.tlpMain.SuspendLayout();
            this.pnlPersonalInfo.SuspendLayout();
            this.tlpPersonalInfo.SuspendLayout();
            this.pnlAvatarBg.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpMain (Khung bố cục tổng thể ứng dụng)
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlPersonalInfo, 0, 0);
            this.tlpMain.Controls.Add(this.lblChangePassTitle, 0, 1);
            this.tlpMain.Controls.Add(this.txtOldPass, 0, 2);
            this.tlpMain.Controls.Add(this.txtNewPass, 0, 3);
            this.tlpMain.Controls.Add(this.btnChangePass, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(24, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle()); // Kích thước tự động co giãn theo Panel thông tin
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(332, 552);
            this.tlpMain.TabIndex = 0;

            // 
            // pnlPersonalInfo (Vùng chứa nền bo góc thông tin cá nhân)
            // 
            this.pnlPersonalInfo.BackColor = OOP2026.Colors.LightGray;
            this.pnlPersonalInfo.Controls.Add(this.tlpPersonalInfo);
            this.pnlPersonalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPersonalInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlPersonalInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnlPersonalInfo.Name = "pnlPersonalInfo";
            this.pnlPersonalInfo.Padding = new System.Windows.Forms.Padding(16);
            this.pnlPersonalInfo.Size = new System.Drawing.Size(332, 320);
            this.pnlPersonalInfo.TabIndex = 0;

            // 
            // tlpPersonalInfo (SỬA LỖI LAYOUT: Chuyển cấu trúc sang lưới dạng linh hoạt)
            // 
            this.tlpPersonalInfo.ColumnCount = 1;
            this.tlpPersonalInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // Chỉ định rõ ràng vị trí ô lưới (Cột, Hàng) tránh chồng lấn khi đổi giao diện
            this.tlpPersonalInfo.Controls.Add(this.pnlAvatarBg, 0, 0);
            this.tlpPersonalInfo.Controls.Add(this.lblName, 0, 1);
            this.tlpPersonalInfo.Controls.Add(this.txtName, 0, 2);
            this.tlpPersonalInfo.Controls.Add(this.lblPhone, 0, 3);
            this.tlpPersonalInfo.Controls.Add(this.txtPhone, 0, 4);
            this.tlpPersonalInfo.Controls.Add(this.lblTrips, 0, 5);
            this.tlpPersonalInfo.Controls.Add(this.lblLicense, 0, 6);
            this.tlpPersonalInfo.Controls.Add(this.lblRating, 0, 7);
            this.tlpPersonalInfo.Controls.Add(this.btnEdit, 0, 8);

            this.tlpPersonalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPersonalInfo.Location = new System.Drawing.Point(16, 16);
            this.tlpPersonalInfo.Name = "tlpPersonalInfo";
            this.tlpPersonalInfo.RowCount = 9;

            // Sử dụng AutoSize giúp các dòng tự ẩn gần hoàn toàn (0px) khi Control bên trong đặt Visible = false
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F)); // Avatar
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));        // lblName
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));  // txtName
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));        // lblPhone
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));  // txtPhone
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));        // lblTrips
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));        // lblLicense
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));        // lblRating
            this.tlpPersonalInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));  // btnEdit
            this.tlpPersonalInfo.Size = new System.Drawing.Size(300, 288);
            this.tlpPersonalInfo.TabIndex = 0;

            // 
            // pnlAvatarBg
            // 
            this.pnlAvatarBg.Controls.Add(this.lblAvatar);
            this.pnlAvatarBg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAvatarBg.Location = new System.Drawing.Point(3, 3);
            this.pnlAvatarBg.Name = "pnlAvatarBg";
            this.pnlAvatarBg.Size = new System.Drawing.Size(294, 114);
            this.pnlAvatarBg.TabIndex = 0;

            // 
            // lblAvatar
            // 
            this.lblAvatar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAvatar.BackColor = OOP2026.Colors.LightGray;
            this.lblAvatar.Font = new System.Drawing.Font("Segoe UI", 48F);
            this.lblAvatar.Location = new System.Drawing.Point(92, 2);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(110, 110);
            this.lblAvatar.TabIndex = 0;
            this.lblAvatar.Text = "👤";
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //
            // lblName
            //
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(3, 122);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Họ tên";

            //
            // txtName
            //
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = OOP2026.Colors.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(3, 155);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(294, 23);
            this.txtName.TabIndex = 2;
            this.txtName.Visible = false;

            //
            // lblPhone
            //
            this.lblPhone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblPhone.Location = new System.Drawing.Point(3, 187);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(270, 25);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Số điện thoại";

            //
            // txtPhone
            //
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.BackColor = OOP2026.Colors.White;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(3, 220);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(294, 23);
            this.txtPhone.TabIndex = 4;
            this.txtPhone.Visible = false;

            //
            // lblTrips
            //
            this.lblTrips.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTrips.AutoSize = true;
            this.lblTrips.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblTrips.Location = new System.Drawing.Point(3, 252);
            this.lblTrips.Name = "lblTrips";
            this.lblTrips.Size = new System.Drawing.Size(268, 25);
            this.lblTrips.TabIndex = 5;
            this.lblTrips.Text = "Tổng chuyến đi";

            //
            // lblLicense
            //
            this.lblLicense.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLicense.AutoSize = true;
            this.lblLicense.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblLicense.Location = new System.Drawing.Point(3, 282);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(118, 25);
            this.lblLicense.TabIndex = 6;
            this.lblLicense.Text = "Số GPLX";

            //
            // lblRating
            //
            this.lblRating.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblRating.Location = new System.Drawing.Point(3, 312);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(146, 25);
            this.lblRating.TabIndex = 7;
            this.lblRating.Text = "Đánh giá";

            //
            // btnEdit
            //
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = OOP2026.Colors.White;
            this.btnEdit.FlatAppearance.BorderColor = OOP2026.Colors.LightGray;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Location = new System.Drawing.Point(3, 343);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(294, 34);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Chỉnh sửa thông tin";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // 
            // lblChangePassTitle
            // 
            this.lblChangePassTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChangePassTitle.AutoSize = true;
            this.lblChangePassTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChangePassTitle.ForeColor = OOP2026.Colors.Black;
            this.lblChangePassTitle.Location = new System.Drawing.Point(3, 358);
            this.lblChangePassTitle.Name = "lblChangePassTitle";
            this.lblChangePassTitle.Size = new System.Drawing.Size(167, 32);
            this.lblChangePassTitle.TabIndex = 1;
            this.lblChangePassTitle.Text = "Đổi mật khẩu";

            // 
            // txtOldPass
            // 
            this.txtOldPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldPass.BackColor = OOP2026.Colors.White;
            this.txtOldPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOldPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOldPass.Location = new System.Drawing.Point(3, 392);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Size = new System.Drawing.Size(326, 20);
            this.txtOldPass.TabIndex = 2;

            // 
            // txtNewPass
            // 
            this.txtNewPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPass.BackColor = OOP2026.Colors.White;
            this.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewPass.Location = new System.Drawing.Point(3, 437);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(326, 20);
            this.txtNewPass.TabIndex = 3;

            // 
            // btnChangePass
            // 
            this.btnChangePass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePass.BackColor = OOP2026.Colors.DarkBlue;
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangePass.ForeColor = OOP2026.Colors.White;
            this.btnChangePass.Location = new System.Drawing.Point(3, 473);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(326, 38);
            this.btnChangePass.TabIndex = 4;
            this.btnChangePass.Text = "Đổi mật khẩu";
            this.btnChangePass.UseVisualStyleBackColor = false;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);

            // 
            // ucProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ucProfile";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.Size = new System.Drawing.Size(380, 600);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pnlPersonalInfo.ResumeLayout(false);
            this.tlpPersonalInfo.ResumeLayout(false);
            this.tlpPersonalInfo.PerformLayout();
            this.pnlAvatarBg.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlPersonalInfo;
        private System.Windows.Forms.TableLayoutPanel tlpPersonalInfo;
        private System.Windows.Forms.Panel pnlAvatarBg;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblTrips;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblChangePassTitle;
        private System.Windows.Forms.TextBox txtOldPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Button btnChangePass;
        #endregion
    }
}
