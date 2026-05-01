namespace Presentation.UserControls
{
    using Presentation.Constants;
    using System.Windows.Forms;

    partial class UcProfile
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblWallet;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlTopUp;
        private System.Windows.Forms.Label lblTopUpAmount;
        private System.Windows.Forms.TextBox txtTopUpAmount;
        private System.Windows.Forms.Button btnTopUp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblWallet = new System.Windows.Forms.Label();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlTopUp = new System.Windows.Forms.Panel();
            this.btnTopUp = new System.Windows.Forms.Button();
            this.txtTopUpAmount = new System.Windows.Forms.TextBox();
            this.lblTopUpAmount = new System.Windows.Forms.Label();
            this.pnlTopUp.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(528, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Hồ sơ cá nhân";

            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 72);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Họ tên";

            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(16, 96);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 30);
            this.txtName.TabIndex = 2;

            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(16, 136);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(82, 20);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Điện thoại";

            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(16, 160);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(320, 30);
            this.txtPhone.TabIndex = 4;

            this.lblWallet.AutoSize = true;
            this.lblWallet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWallet.Location = new System.Drawing.Point(360, 72);
            this.lblWallet.Name = "lblWallet";
            this.lblWallet.Size = new System.Drawing.Size(56, 23);
            this.lblWallet.TabIndex = 5;
            this.lblWallet.Text = "Ví: 0đ";

            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Location = new System.Drawing.Point(16, 200);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(110, 20);
            this.lblCurrentPassword.TabIndex = 6;
            this.lblCurrentPassword.Text = "Mật khẩu hiện tại";

            this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentPassword.Location = new System.Drawing.Point(16, 224);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.PasswordChar = '*';
            this.txtCurrentPassword.Size = new System.Drawing.Size(320, 30);
            this.txtCurrentPassword.TabIndex = 7;

            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(16, 264);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(93, 20);
            this.lblNewPassword.TabIndex = 8;
            this.lblNewPassword.Text = "Mật khẩu mới";

            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new System.Drawing.Point(16, 288);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(320, 30);
            this.txtNewPassword.TabIndex = 9;

            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(16, 328);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(140, 20);
            this.lblConfirmPassword.TabIndex = 10;
            this.lblConfirmPassword.Text = "Xác nhận mật khẩu mới";

            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(16, 352);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(320, 30);
            this.txtConfirmPassword.TabIndex = 11;

            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnSave.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnSave.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnSave.MinimumSize = Presentation.Constants.UiConstants.ButtonSizes.Action;
            this.btnSave.Location = new System.Drawing.Point(416, 416);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 40);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;

            this.pnlTopUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopUp.Controls.Add(this.lblTopUpAmount);
            this.pnlTopUp.Controls.Add(this.btnTopUp);
            this.pnlTopUp.Controls.Add(this.txtTopUpAmount);
            this.pnlTopUp.Location = new System.Drawing.Point(360, 160);
            this.pnlTopUp.Name = "pnlTopUp";
            this.pnlTopUp.Size = new System.Drawing.Size(184, 120);
            this.pnlTopUp.TabIndex = 13;

            this.lblTopUpAmount.AutoSize = true;
            this.lblTopUpAmount.Location = new System.Drawing.Point(0, 0);
            this.lblTopUpAmount.Name = "lblTopUpAmount";
            this.lblTopUpAmount.Size = new System.Drawing.Size(110, 20);
            this.lblTopUpAmount.TabIndex = 2;
            this.lblTopUpAmount.Text = "Số tiền cần nạp";

            this.txtTopUpAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTopUpAmount.Location = new System.Drawing.Point(0, 24);
            this.txtTopUpAmount.Name = "txtTopUpAmount";
            this.txtTopUpAmount.Size = new System.Drawing.Size(184, 30);
            this.txtTopUpAmount.TabIndex = 0;

            this.btnTopUp.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnTopUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopUp.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnTopUp.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnTopUp.AutoSize = true;
            this.btnTopUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnTopUp.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnTopUp.MinimumSize = Presentation.Constants.UiConstants.ButtonSizes.Default;
            this.btnTopUp.Location = new System.Drawing.Point(0, 60);
            this.btnTopUp.Name = "btnTopUp";
            this.btnTopUp.Size = new System.Drawing.Size(184, 36);
            this.btnTopUp.TabIndex = 1;
            this.btnTopUp.Text = "Nạp tiền";
            this.btnTopUp.UseVisualStyleBackColor = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlTopUp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtCurrentPassword);
            this.Controls.Add(this.lblCurrentPassword);
            this.Controls.Add(this.lblWallet);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.Name = "UcProfile";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(560, 480);
            this.pnlTopUp.ResumeLayout(false);
            this.pnlTopUp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

