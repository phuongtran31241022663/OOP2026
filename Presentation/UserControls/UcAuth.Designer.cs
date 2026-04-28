namespace Presentation.UserControls
{
    partial class UcAuth
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.TableLayoutPanel pnlCenter;
        private System.Windows.Forms.TableLayoutPanel pnlLogin;
        private System.Windows.Forms.TableLayoutPanel pnlRegister;
        private System.Windows.Forms.Label lblFooter;

        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.TextBox txtLoginPhone;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Button btnToggleLoginPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkToRegister;

        private System.Windows.Forms.Label lblRegisterTitle;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.TextBox txtRegPhone;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.Button btnToggleRegPassword;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.TableLayoutPanel pnlVehicleInfo;
        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.TextBox txtPlate;
        private System.Windows.Forms.Label lblPlate;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.NumericUpDown numCapacity;
        private System.Windows.Forms.Label lblCapacity;
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

        private void InitializeComponent()
        {
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogin = new System.Windows.Forms.TableLayoutPanel();
            this.linkToRegister = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnToggleLoginPassword = new System.Windows.Forms.Button();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.txtLoginPhone = new System.Windows.Forms.TextBox();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.pnlRegister = new System.Windows.Forms.TableLayoutPanel();
            this.linkToLogin = new System.Windows.Forms.LinkLabel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.pnlVehicleInfo = new System.Windows.Forms.TableLayoutPanel();
            this.numCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.txtPlate = new System.Windows.Forms.TextBox();
            this.lblPlate = new System.Windows.Forms.Label();
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnToggleRegPassword = new System.Windows.Forms.Button();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.txtRegPhone = new System.Windows.Forms.TextBox();
            this.txtRegName = new System.Windows.Forms.TextBox();
            this.lblRegisterTitle = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.pnlVehicleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).BeginInit();
            this.SuspendLayout();
            
            // tblMain
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

            // lblLogo
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

            // pnlCenter
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenter.AutoSize = true;
            this.pnlCenter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCenter.ColumnCount = 1;
            this.pnlCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCenter.Controls.Add(this.pnlLogin, 0, 0);
            this.pnlCenter.Controls.Add(this.pnlRegister, 0, 1);
            this.pnlCenter.Location = new System.Drawing.Point(200, 110);
            this.pnlCenter.MinimumSize = new System.Drawing.Size(500, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.RowCount = 2;
            this.pnlCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlCenter.TabIndex = 1;

            // pnlLogin
            this.pnlLogin.AutoSize = true;
            this.pnlLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLogin.ColumnCount = 2;
            this.pnlLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.pnlLogin.Controls.Add(this.lblLoginTitle, 0, 0);
            this.pnlLogin.Controls.Add(this.txtLoginPhone, 0, 1);
            this.pnlLogin.Controls.Add(this.txtLoginPassword, 0, 2);
            this.pnlLogin.Controls.Add(this.btnToggleLoginPassword, 1, 2);
            this.pnlLogin.Controls.Add(this.btnLogin, 0, 3);
            this.pnlLogin.Controls.Add(this.linkToRegister, 0, 4);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.RowCount = 5;
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlLogin.TabIndex = 0;

            // lblLoginTitle
            this.lblLoginTitle.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.lblLoginTitle, 2);
            this.lblLoginTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLoginTitle.Location = new System.Drawing.Point(3, 10);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(145, 32);
            this.lblLoginTitle.TabIndex = 0;
            this.lblLoginTitle.Text = "Đăng nhập";

            // txtLoginPhone
            this.pnlLogin.SetColumnSpan(this.txtLoginPhone, 2);
            this.txtLoginPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoginPhone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLoginPhone.Location = new System.Drawing.Point(3, 53);
            this.txtLoginPhone.Name = "txtLoginPhone";
            this.txtLoginPhone.Size = new System.Drawing.Size(494, 34);
            this.txtLoginPhone.TabIndex = 1;

            // txtLoginPassword
            this.txtLoginPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoginPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLoginPassword.Location = new System.Drawing.Point(3, 103);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(449, 34);
            this.txtLoginPassword.TabIndex = 2;

            // btnToggleLoginPassword
            this.btnToggleLoginPassword.BackColor = System.Drawing.Color.White;
            this.btnToggleLoginPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleLoginPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleLoginPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleLoginPassword.Location = new System.Drawing.Point(458, 103);
            this.btnToggleLoginPassword.Name = "btnToggleLoginPassword";
            this.btnToggleLoginPassword.Size = new System.Drawing.Size(39, 34);
            this.btnToggleLoginPassword.TabIndex = 3;
            this.btnToggleLoginPassword.Text = "👁";
            this.btnToggleLoginPassword.UseVisualStyleBackColor = false;

            // btnLogin
            this.pnlLogin.SetColumnSpan(this.btnLogin, 2);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(3, 153);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(494, 64);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;

            // linkToRegister
            this.linkToRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkToRegister.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.linkToRegister, 2);
            this.linkToRegister.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.linkToRegister.Location = new System.Drawing.Point(145, 228);
            this.linkToRegister.Name = "linkToRegister";
            this.linkToRegister.Size = new System.Drawing.Size(210, 23);
            this.linkToRegister.TabIndex = 5;
            this.linkToRegister.TabStop = true;
            this.linkToRegister.Text = "Chưa có tài khoản? Đăng ký";

            // pnlRegister
            this.pnlRegister.AutoSize = true;
            this.pnlRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRegister.ColumnCount = 2;
            this.pnlRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.pnlRegister.Controls.Add(this.lblRegisterTitle, 0, 0);
            this.pnlRegister.Controls.Add(this.txtRegName, 0, 1);
            this.pnlRegister.Controls.Add(this.txtRegPhone, 0, 2);
            this.pnlRegister.Controls.Add(this.txtRegPassword, 0, 3);
            this.pnlRegister.Controls.Add(this.btnToggleRegPassword, 1, 3);
            this.pnlRegister.Controls.Add(this.cmbRole, 0, 4);
            this.pnlRegister.Controls.Add(this.pnlVehicleInfo, 0, 5);
            this.pnlRegister.Controls.Add(this.btnRegister, 0, 6);
            this.pnlRegister.Controls.Add(this.linkToLogin, 0, 7);
            this.pnlRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegister.Location = new System.Drawing.Point(0, 260);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.RowCount = 8;
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlRegister.TabIndex = 1;
            this.pnlRegister.Visible = false;

            // lblRegisterTitle
            this.lblRegisterTitle.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRegisterTitle, 2);
            this.lblRegisterTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRegisterTitle.Location = new System.Drawing.Point(3, 10);
            this.lblRegisterTitle.Name = "lblRegisterTitle";
            this.lblRegisterTitle.Size = new System.Drawing.Size(112, 32);
            this.lblRegisterTitle.TabIndex = 0;
            this.lblRegisterTitle.Text = "Đăng ký";

            // txtRegName
            this.pnlRegister.SetColumnSpan(this.txtRegName, 2);
            this.txtRegName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegName.Location = new System.Drawing.Point(3, 53);
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.Size = new System.Drawing.Size(494, 34);
            this.txtRegName.TabIndex = 1;

            // txtRegPhone
            this.pnlRegister.SetColumnSpan(this.txtRegPhone, 2);
            this.txtRegPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegPhone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegPhone.Location = new System.Drawing.Point(3, 103);
            this.txtRegPhone.Name = "txtRegPhone";
            this.txtRegPhone.Size = new System.Drawing.Size(494, 34);
            this.txtRegPhone.TabIndex = 2;

            // txtRegPassword
            this.txtRegPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegPassword.Location = new System.Drawing.Point(3, 153);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(449, 34);
            this.txtRegPassword.TabIndex = 3;

            // btnToggleRegPassword
            this.btnToggleRegPassword.BackColor = System.Drawing.Color.White;
            this.btnToggleRegPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleRegPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleRegPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToggleRegPassword.Location = new System.Drawing.Point(458, 153);
            this.btnToggleRegPassword.Name = "btnToggleRegPassword";
            this.btnToggleRegPassword.Size = new System.Drawing.Size(39, 34);
            this.btnToggleRegPassword.TabIndex = 4;
            this.btnToggleRegPassword.Text = "👁";
            this.btnToggleRegPassword.UseVisualStyleBackColor = false;

            // cmbRole
            this.pnlRegister.SetColumnSpan(this.cmbRole, 2);
            this.cmbRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] { "Hành khách", "Tài xế" });
            this.cmbRole.Location = new System.Drawing.Point(3, 203);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(494, 36);
            this.cmbRole.TabIndex = 5;

            // pnlVehicleInfo
            this.pnlVehicleInfo.AutoSize = true;
            this.pnlVehicleInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlVehicleInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlVehicleInfo.ColumnCount = 2;
            this.pnlRegister.SetColumnSpan(this.pnlVehicleInfo, 2);
            this.pnlVehicleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.pnlVehicleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.pnlVehicleInfo.Controls.Add(this.lblVehicleType, 0, 0);
            this.pnlVehicleInfo.Controls.Add(this.cmbVehicleType, 1, 0);
            this.pnlVehicleInfo.Controls.Add(this.lblPlate, 0, 1);
            this.pnlVehicleInfo.Controls.Add(this.txtPlate, 1, 1);
            this.pnlVehicleInfo.Controls.Add(this.lblBrand, 0, 2);
            this.pnlVehicleInfo.Controls.Add(this.txtBrand, 1, 2);
            this.pnlVehicleInfo.Controls.Add(this.lblModel, 0, 3);
            this.pnlVehicleInfo.Controls.Add(this.txtModel, 1, 3);
            this.pnlVehicleInfo.Controls.Add(this.lblColor, 0, 4);
            this.pnlVehicleInfo.Controls.Add(this.txtColor, 1, 4);
            this.pnlVehicleInfo.Controls.Add(this.lblCapacity, 0, 5);
            this.pnlVehicleInfo.Controls.Add(this.numCapacity, 1, 5);
            this.pnlVehicleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehicleInfo.Location = new System.Drawing.Point(3, 253);
            this.pnlVehicleInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlVehicleInfo.Name = "pnlVehicleInfo";
            this.pnlVehicleInfo.RowCount = 6;
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlVehicleInfo.TabIndex = 6;
            this.pnlVehicleInfo.Visible = false;

            // lblVehicleType
            this.lblVehicleType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleType.Location = new System.Drawing.Point(3, 8);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(134, 23);
            this.lblVehicleType.TabIndex = 0;
            this.lblVehicleType.Text = "Loại xe:";

            // cmbVehicleType
            this.cmbVehicleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] { "Xe máy", "Ô tô" });
            this.cmbVehicleType.Location = new System.Drawing.Point(177, 5);
            this.cmbVehicleType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(314, 31);
            this.cmbVehicleType.TabIndex = 1;

            // lblPlate
            this.lblPlate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlate.AutoSize = true;
            this.lblPlate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPlate.Location = new System.Drawing.Point(3, 48);
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Size = new System.Drawing.Size(69, 23);
            this.lblPlate.TabIndex = 2;
            this.lblPlate.Text = "Biển số:";

            // txtPlate
            this.txtPlate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPlate.Location = new System.Drawing.Point(177, 45);
            this.txtPlate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtPlate.Name = "txtPlate";
            this.txtPlate.Size = new System.Drawing.Size(314, 30);
            this.txtPlate.TabIndex = 3;

            // lblBrand
            this.lblBrand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBrand.Location = new System.Drawing.Point(3, 88);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(74, 23);
            this.lblBrand.TabIndex = 4;
            this.lblBrand.Text = "Hãng xe:";

            // txtBrand
            this.txtBrand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBrand.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBrand.Location = new System.Drawing.Point(177, 85);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(314, 30);
            this.txtBrand.TabIndex = 5;

            // lblModel
            this.lblModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblModel.Location = new System.Drawing.Point(3, 128);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(70, 23);
            this.lblModel.TabIndex = 6;
            this.lblModel.Text = "Mẫu xe:";

            // txtModel
            this.txtModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtModel.Location = new System.Drawing.Point(177, 125);
            this.txtModel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(314, 30);
            this.txtModel.TabIndex = 7;

            // lblColor
            this.lblColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblColor.Location = new System.Drawing.Point(3, 168);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(61, 23);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "Màu xe:";

            // txtColor
            this.txtColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColor.Location = new System.Drawing.Point(177, 165);
            this.txtColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(314, 30);
            this.txtColor.TabIndex = 9;

            // lblCapacity
            this.lblCapacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCapacity.Location = new System.Drawing.Point(3, 208);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(75, 23);
            this.lblCapacity.TabIndex = 10;
            this.lblCapacity.Text = "Sức chứa:";

            // numCapacity
            this.numCapacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numCapacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCapacity.Location = new System.Drawing.Point(177, 205);
            this.numCapacity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.numCapacity.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCapacity.Name = "numCapacity";
            this.numCapacity.Size = new System.Drawing.Size(100, 30);
            this.numCapacity.TabIndex = 11;
            this.numCapacity.Value = new decimal(new int[] { 2, 0, 0, 0 });

            // btnRegister
            this.pnlRegister.SetColumnSpan(this.btnRegister, 2);
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(3, 263);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(494, 64);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "Tạo tài khoản";
            this.btnRegister.UseVisualStyleBackColor = false;

            // linkToLogin
            this.linkToLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkToLogin.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.linkToLogin, 2);
            this.linkToLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.linkToLogin.Location = new System.Drawing.Point(145, 338);
            this.linkToLogin.Name = "linkToLogin";
            this.linkToLogin.Size = new System.Drawing.Size(210, 23);
            this.linkToLogin.TabIndex = 8;
            this.linkToLogin.TabStop = true;
            this.linkToLogin.Text = "Đã có tài khoản? Đăng nhập";

            // lblFooter
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Location = new System.Drawing.Point(3, 560);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(894, 40);
            this.lblFooter.TabIndex = 2;
            this.lblFooter.Text = "RideGo 2026 - Ứng dụng đặt xe";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // UcAuth
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tblMain);
            this.Name = "UcAuth";
            this.Size = new System.Drawing.Size(900, 600);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.pnlVehicleInfo.ResumeLayout(false);
            this.pnlVehicleInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
