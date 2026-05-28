namespace OOP2026
{
    partial class FrmPassengerAuth
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabLogin = new System.Windows.Forms.Button();
            this.btnTabRegister = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlDemoAccount = new System.Windows.Forms.Panel();
            this.lblDemoText = new System.Windows.Forms.Label();
            this.txtLoginPhone = new System.Windows.Forms.TextBox();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.txtRegName = new System.Windows.Forms.TextBox();
            this.txtRegPhone = new System.Windows.Forms.TextBox();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.txtRegPlate = new System.Windows.Forms.TextBox();
            this.txtRegBrand = new System.Windows.Forms.TextBox();
            this.txtRegModel = new System.Windows.Forms.TextBox();
            this.txtRegColor = new System.Windows.Forms.TextBox();
            this.numRegCapacity = new System.Windows.Forms.NumericUpDown();
            this.cmbRegVehicleType = new System.Windows.Forms.ComboBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlDemoAccount.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = OOP2026.Colors.Green;
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(450, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderTitle.Font = OOP2026.Typography.Font14Bold;
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(410, 60);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Hành khách — Đăng nhập";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = OOP2026.Typography.Font14Bold;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(410, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 60);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // pnlTabs
            // 
            this.pnlTabs.Controls.Add(this.btnTabLogin);
            this.pnlTabs.Controls.Add(this.btnTabRegister);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Location = new System.Drawing.Point(0, 60);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(450, 50);
            this.pnlTabs.TabIndex = 1;
            // 
            // btnTabLogin
            // 
            this.btnTabLogin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTabLogin.FlatAppearance.BorderSize = 0;
            this.btnTabLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabLogin.Font = OOP2026.Typography.Font10Bold;
            this.btnTabLogin.Location = new System.Drawing.Point(0, 0);
            this.btnTabLogin.Name = "btnTabLogin";
            this.btnTabLogin.Size = new System.Drawing.Size(225, 50);
            this.btnTabLogin.TabIndex = 0;
            this.btnTabLogin.Text = "Đăng nhập";
            this.btnTabLogin.UseVisualStyleBackColor = true;
            this.btnTabLogin.Click += new System.EventHandler(this.BtnTabLogin_Click);
            // 
            // btnTabRegister
            // 
            this.btnTabRegister.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTabRegister.FlatAppearance.BorderSize = 0;
            this.btnTabRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabRegister.Font = OOP2026.Typography.Font10Bold;
            this.btnTabRegister.Location = new System.Drawing.Point(225, 0);
            this.btnTabRegister.Name = "btnTabRegister";
            this.btnTabRegister.Size = new System.Drawing.Size(225, 50);
            this.btnTabRegister.TabIndex = 1;
            this.btnTabRegister.Text = "Đăng ký";
            this.btnTabRegister.UseVisualStyleBackColor = true;
            this.btnTabRegister.Click += new System.EventHandler(this.BtnTabRegister_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlLogin);
            this.pnlContent.Controls.Add(this.pnlRegister);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 110);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(450, 490);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.pnlDemoAccount);
            this.pnlLogin.Controls.Add(this.txtLoginPhone);
            this.pnlLogin.Controls.Add(this.txtLoginPassword);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(20, 20);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(410, 450);
            this.pnlLogin.TabIndex = 0;
            // 
            // pnlDemoAccount
            // 
            this.pnlDemoAccount.BackColor = OOP2026.Colors.LightGreen;
            this.pnlDemoAccount.Controls.Add(this.lblDemoText);
            this.pnlDemoAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDemoAccount.Location = new System.Drawing.Point(0, 10);
            this.pnlDemoAccount.Name = "pnlDemoAccount";
            this.pnlDemoAccount.Size = new System.Drawing.Size(410, 60);
            this.pnlDemoAccount.TabIndex = 0;
            this.pnlDemoAccount.Click += new System.EventHandler(this.PnlDemoAccount_Click);
            // 
            // lblDemoText
            // 
            this.lblDemoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDemoText.Font = OOP2026.Typography.Font9Regular;
            this.lblDemoText.Location = new System.Drawing.Point(0, 0);
            this.lblDemoText.Name = "lblDemoText";
            this.lblDemoText.Size = new System.Drawing.Size(410, 60);
            this.lblDemoText.TabIndex = 0;
            this.lblDemoText.Text = "Tài khoản demo:\r\n0911111111 / 123456";
            this.lblDemoText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDemoText.Click += new System.EventHandler(this.PnlDemoAccount_Click);
            // 
            // txtLoginPhone
            // 
            this.txtLoginPhone.Font = OOP2026.Typography.Font10Regular;
            this.txtLoginPhone.Location = new System.Drawing.Point(0, 100);
            this.txtLoginPhone.Name = "txtLoginPhone";
            this.txtLoginPhone.Size = new System.Drawing.Size(410, 30);
            this.txtLoginPhone.TabIndex = 1;
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Font = OOP2026.Typography.Font10Regular;
            this.txtLoginPassword.Location = new System.Drawing.Point(0, 150);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(410, 30);
            this.txtLoginPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = OOP2026.Colors.Green;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = OOP2026.Typography.Font10Bold;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(0, 220);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(410, 45);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // pnlRegister
            // 
            this.pnlRegister.AutoScroll = true;
            this.pnlRegister.Controls.Add(this.txtRegName);
            this.pnlRegister.Controls.Add(this.txtRegPhone);
            this.pnlRegister.Controls.Add(this.txtRegPassword);
            this.pnlRegister.Controls.Add(this.txtRegPlate);
            this.pnlRegister.Controls.Add(this.txtRegBrand);
            this.pnlRegister.Controls.Add(this.txtRegModel);
            this.pnlRegister.Controls.Add(this.txtRegColor);
            this.pnlRegister.Controls.Add(this.numRegCapacity);
            this.pnlRegister.Controls.Add(this.cmbRegVehicleType);
            this.pnlRegister.Controls.Add(this.btnRegister);
            this.pnlRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegister.Location = new System.Drawing.Point(20, 20);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(410, 450);
            this.pnlRegister.TabIndex = 1;
            this.pnlRegister.Visible = false;
            // 
            // txtRegName
            // 
            this.txtRegName.Font = OOP2026.Typography.Font10Regular;
            this.txtRegName.Location = new System.Drawing.Point(0, 10);
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.Size = new System.Drawing.Size(410, 30);
            this.txtRegName.TabIndex = 0;
            // 
            // txtRegPhone
            // 
            this.txtRegPhone.Font = OOP2026.Typography.Font10Regular;
            this.txtRegPhone.Location = new System.Drawing.Point(0, 60);
            this.txtRegPhone.Name = "txtRegPhone";
            this.txtRegPhone.Size = new System.Drawing.Size(410, 30);
            this.txtRegPhone.TabIndex = 1;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Font = OOP2026.Typography.Font10Regular;
            this.txtRegPassword.Location = new System.Drawing.Point(0, 110);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(410, 30);
            this.txtRegPassword.TabIndex = 2;
            // 
            // txtRegPlate
            // 
            this.txtRegPlate.Font = OOP2026.Typography.Font10Regular;
            this.txtRegPlate.Location = new System.Drawing.Point(0, 160);
            this.txtRegPlate.Name = "txtRegPlate";
            this.txtRegPlate.Size = new System.Drawing.Size(410, 30);
            this.txtRegPlate.TabIndex = 3;
            // 
            // txtRegBrand
            // 
            this.txtRegBrand.Font = OOP2026.Typography.Font10Regular;
            this.txtRegBrand.Location = new System.Drawing.Point(0, 210);
            this.txtRegBrand.Name = "txtRegBrand";
            this.txtRegBrand.Size = new System.Drawing.Size(200, 30);
            this.txtRegBrand.TabIndex = 4;
            // 
            // txtRegModel
            // 
            this.txtRegModel.Font = OOP2026.Typography.Font10Regular;
            this.txtRegModel.Location = new System.Drawing.Point(210, 210);
            this.txtRegModel.Name = "txtRegModel";
            this.txtRegModel.Size = new System.Drawing.Size(200, 30);
            this.txtRegModel.TabIndex = 5;
            // 
            // txtRegColor
            // 
            this.txtRegColor.Font = OOP2026.Typography.Font10Regular;
            this.txtRegColor.Location = new System.Drawing.Point(0, 260);
            this.txtRegColor.Name = "txtRegColor";
            this.txtRegColor.Size = new System.Drawing.Size(200, 30);
            this.txtRegColor.TabIndex = 6;
            // 
            // numRegCapacity
            // 
            this.numRegCapacity.Font = OOP2026.Typography.Font10Regular;
            this.numRegCapacity.Location = new System.Drawing.Point(210, 260);
            this.numRegCapacity.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            this.numRegCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numRegCapacity.Name = "numRegCapacity";
            this.numRegCapacity.Size = new System.Drawing.Size(200, 30);
            this.numRegCapacity.TabIndex = 7;
            this.numRegCapacity.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // cmbRegVehicleType
            // 
            this.cmbRegVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegVehicleType.Font = OOP2026.Typography.Font10Regular;
            this.cmbRegVehicleType.FormattingEnabled = true;
            this.cmbRegVehicleType.Items.AddRange(new object[] { "Xe máy", "Ô tô" });
            this.cmbRegVehicleType.Location = new System.Drawing.Point(0, 310);
            this.cmbRegVehicleType.Name = "cmbRegVehicleType";
            this.cmbRegVehicleType.Size = new System.Drawing.Size(410, 31);
            this.cmbRegVehicleType.TabIndex = 8;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = OOP2026.Colors.Green;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = OOP2026.Typography.Font10Bold;
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(0, 370);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(410, 45);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // FrmPassengerAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPassengerAuth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.pnlHeader.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlDemoAccount.ResumeLayout(false);
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegCapacity)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnTabLogin;
        private System.Windows.Forms.Button btnTabRegister;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel pnlDemoAccount;
        private System.Windows.Forms.Label lblDemoText;
        private System.Windows.Forms.TextBox txtLoginPhone;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.TextBox txtRegPhone;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.TextBox txtRegPlate;
        private System.Windows.Forms.TextBox txtRegBrand;
        private System.Windows.Forms.TextBox txtRegModel;
        private System.Windows.Forms.TextBox txtRegColor;
        private System.Windows.Forms.NumericUpDown numRegCapacity;
        private System.Windows.Forms.ComboBox cmbRegVehicleType;
        private System.Windows.Forms.Button btnRegister;
    }
}
