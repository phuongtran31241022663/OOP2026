namespace Presentation.UserControls
{
    partial class UcAuth
    {
        private System.ComponentModel.IContainer components = null;

        // TableLayout
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.Label lblFooter;

        // Login controls
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.TextBox txtLoginPhone;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkToRegister;

        // Register controls
        private System.Windows.Forms.Label lblRegisterTitle;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.TextBox txtRegPhone;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel linkToLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.linkToRegister = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.txtLoginPhone = new System.Windows.Forms.TextBox();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.linkToLogin = new System.Windows.Forms.LinkLabel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.txtRegPhone = new System.Windows.Forms.TextBox();
            this.txtRegName = new System.Windows.Forms.TextBox();
            this.lblRegisterTitle = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.lblLogo, 0, 0);
            this.tblMain.Controls.Add(this.pnlCenter, 0, 1);
            this.tblMain.Controls.Add(this.lblFooter, 0, 2);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMain.Size = new System.Drawing.Size(900, 600);
            this.tblMain.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblLogo.Location = new System.Drawing.Point(364, 24);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(171, 50);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "RideGo";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenter.AutoSize = true;
            this.pnlCenter.Controls.Add(this.pnlLogin);
            this.pnlCenter.Controls.Add(this.pnlRegister);
            this.pnlCenter.Location = new System.Drawing.Point(250, 150);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(400, 320);
            this.pnlCenter.TabIndex = 1;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.linkToRegister);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.txtLoginPassword);
            this.pnlLogin.Controls.Add(this.txtLoginPhone);
            this.pnlLogin.Controls.Add(this.lblLoginTitle);
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(400, 320);
            this.pnlLogin.TabIndex = 0;
            // 
            // linkToRegister
            // 
            this.linkToRegister.AutoSize = true;
            this.linkToRegister.Location = new System.Drawing.Point(120, 280);
            this.linkToRegister.Name = "linkToRegister";
            this.linkToRegister.Size = new System.Drawing.Size(160, 20);
            this.linkToRegister.TabIndex = 4;
            this.linkToRegister.TabStop = true;
            this.linkToRegister.Text = "Chua co tai khoan? Dang ky";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(20, 200);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(360, 48);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Dang nhap";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLoginPassword.Location = new System.Drawing.Point(20, 140);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(360, 30);
            this.txtLoginPassword.TabIndex = 2;
            // 
            // txtLoginPhone
            // 
            this.txtLoginPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLoginPhone.Location = new System.Drawing.Point(20, 80);
            this.txtLoginPhone.Name = "txtLoginPhone";
            this.txtLoginPhone.Size = new System.Drawing.Size(360, 30);
            this.txtLoginPhone.TabIndex = 1;
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLoginTitle.Location = new System.Drawing.Point(20, 20);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(126, 32);
            this.lblLoginTitle.TabIndex = 0;
            this.lblLoginTitle.Text = "Dang nhap";
            // 
            // pnlRegister
            // 
            this.pnlRegister.Controls.Add(this.linkToLogin);
            this.pnlRegister.Controls.Add(this.btnRegister);
            this.pnlRegister.Controls.Add(this.cmbRole);
            this.pnlRegister.Controls.Add(this.txtRegPassword);
            this.pnlRegister.Controls.Add(this.txtRegPhone);
            this.pnlRegister.Controls.Add(this.txtRegName);
            this.pnlRegister.Controls.Add(this.lblRegisterTitle);
            this.pnlRegister.Location = new System.Drawing.Point(0, 0);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(400, 320);
            this.pnlRegister.TabIndex = 1;
            this.pnlRegister.Visible = false;
            // 
            // linkToLogin
            // 
            this.linkToLogin.AutoSize = true;
            this.linkToLogin.Location = new System.Drawing.Point(130, 290);
            this.linkToLogin.Name = "linkToLogin";
            this.linkToLogin.Size = new System.Drawing.Size(140, 20);
            this.linkToLogin.TabIndex = 6;
            this.linkToLogin.TabStop = true;
            this.linkToLogin.Text = "Da co tai khoan? Dang nhap";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(20, 230);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(360, 48);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Tao tai khoan";
            this.btnRegister.UseVisualStyleBackColor = false;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Passenger",
            "Driver"});
            this.cmbRole.Location = new System.Drawing.Point(20, 185);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(360, 31);
            this.cmbRole.TabIndex = 4;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegPassword.Location = new System.Drawing.Point(20, 140);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(360, 30);
            this.txtRegPassword.TabIndex = 3;
            // 
            // txtRegPhone
            // 
            this.txtRegPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegPhone.Location = new System.Drawing.Point(20, 95);
            this.txtRegPhone.Name = "txtRegPhone";
            this.txtRegPhone.Size = new System.Drawing.Size(360, 30);
            this.txtRegPhone.TabIndex = 2;
            // 
            // txtRegName
            // 
            this.txtRegName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRegName.Location = new System.Drawing.Point(20, 50);
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.Size = new System.Drawing.Size(360, 30);
            this.txtRegName.TabIndex = 1;
            // 
            // lblRegisterTitle
            // 
            this.lblRegisterTitle.AutoSize = true;
            this.lblRegisterTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRegisterTitle.Location = new System.Drawing.Point(20, 10);
            this.lblRegisterTitle.Name = "lblRegisterTitle";
            this.lblRegisterTitle.Size = new System.Drawing.Size(99, 32);
            this.lblRegisterTitle.TabIndex = 0;
            this.lblRegisterTitle.Text = "Dang ky";
            // 
            // lblFooter
            // 
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Location = new System.Drawing.Point(3, 563);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(894, 37);
            this.lblFooter.TabIndex = 2;
            this.lblFooter.Text = "RideGo 2026 - Ung dung dat xe";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblMain);
            this.Name = "UcAuth";
            this.Size = new System.Drawing.Size(900, 600);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

