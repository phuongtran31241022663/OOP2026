namespace Presentation.UserControls
{
    using Presentation.Constants;

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
        private System.Windows.Forms.Label lblLoginPhone;
        private System.Windows.Forms.TextBox txtLoginPhone;
        private System.Windows.Forms.Label lblLoginPassword;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Button btnToggleLoginPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkToRegister;

        private System.Windows.Forms.Label lblRegisterTitle;
        private System.Windows.Forms.Label lblRegName;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.Label lblRegPhone;
        private System.Windows.Forms.TextBox txtRegPhone;
        private System.Windows.Forms.Label lblRegPassword;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.Button btnToggleRegPassword;
        private System.Windows.Forms.Label lblRole;
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
            this.lblLoginPhone = new System.Windows.Forms.Label();
            this.lblLoginPassword = new System.Windows.Forms.Label();
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
            this.lblRegName = new System.Windows.Forms.Label();
            this.lblRegPhone = new System.Windows.Forms.Label();
            this.lblRegPassword = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.pnlVehicleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacity)).BeginInit();
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
            this.tblMain.Location = Presentation.Constants.UiConstants.Layout.ZeroPoint;
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.HeaderRow));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.FooterRow));
            this.tblMain.Size = Presentation.Constants.UiConstants.Sizes.DefaultForm;
            this.tblMain.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = Presentation.Constants.UiConstants.Typography.Logo;
            this.lblLogo.ForeColor = Presentation.Constants.UiConstants.Colors.Primary;
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
            this.pnlCenter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCenter.ColumnCount = 1;
            this.pnlCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Presentation.Constants.UiConstants.Layout.FullPercent));
            this.pnlCenter.Controls.Add(this.pnlLogin, 0, 0);
            this.pnlCenter.Controls.Add(this.pnlRegister, 0, 1);
            this.pnlCenter.Location = new System.Drawing.Point(200, Presentation.Constants.UiConstants.Heights.HeaderRow + Presentation.Constants.UiConstants.Layout.FormPadding); // Adjusted based on HeaderRowHeight
            this.pnlCenter.MinimumSize = new System.Drawing.Size(Presentation.Constants.UiConstants.Sizes.MinContentPanelWidth, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.RowCount = 2;
            this.pnlCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlCenter.TabIndex = 1;
            // 
            // pnlLogin
            // 
            this.pnlLogin.AutoSize = true;
            this.pnlLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLogin.ColumnCount = 2;
            this.pnlLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Presentation.Constants.UiConstants.Layout.FullPercent));
            this.pnlLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Layout.PasswordToggleColumnWidth));
            this.pnlLogin.Controls.Add(this.lblLoginTitle, 0, 0);
            this.pnlLogin.Controls.Add(this.lblLoginPhone, 0, 1);
            this.pnlLogin.Controls.Add(this.txtLoginPhone, 0, 2);
            this.pnlLogin.Controls.Add(this.lblLoginPassword, 0, 3);
            this.pnlLogin.Controls.Add(this.txtLoginPassword, 0, 4);
            this.pnlLogin.Controls.Add(this.btnToggleLoginPassword, 1, 4);
            this.pnlLogin.Controls.Add(this.btnLogin, 0, 5);
            this.pnlLogin.Controls.Add(this.linkToRegister, 0, 6);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = Presentation.Constants.UiConstants.Layout.ZeroPoint;
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.RowCount = 7;
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.TitleRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.ActionButtonRow));
            this.pnlLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LinkRow));
            this.pnlLogin.TabIndex = 0;
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.lblLoginTitle, 2);
            this.lblLoginTitle.Font = Presentation.Constants.UiConstants.Typography.Header;
            this.lblLoginTitle.Location = new System.Drawing.Point(3, 10);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(145, 32);
            this.lblLoginTitle.TabIndex = 0;
            this.lblLoginTitle.Text = "Đăng nhập";
            // 
            // lblLoginPhone
            // 
            this.lblLoginPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLoginPhone.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.lblLoginPhone, 2);
            this.lblLoginPhone.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblLoginPhone.Location = new System.Drawing.Point(3, 57);
            this.lblLoginPhone.Name = "lblLoginPhone";
            this.lblLoginPhone.Size = new System.Drawing.Size(111, 23);
            this.lblLoginPhone.TabIndex = 1;
            this.lblLoginPhone.Text = "Số điện thoại";
            // 
            // txtLoginPhone
            // 
            this.pnlLogin.SetColumnSpan(this.txtLoginPhone, 2);
            this.txtLoginPhone.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine, no constant needed
            this.txtLoginPhone.Font = Presentation.Constants.UiConstants.Typography.Body;
            this.txtLoginPhone.Location = new System.Drawing.Point(3, 83);
            this.txtLoginPhone.Name = "txtLoginPhone";
            this.txtLoginPhone.Size = new System.Drawing.Size(494, 34);
            this.txtLoginPhone.TabIndex = 2;
            // 
            // lblLoginPassword
            // 
            this.lblLoginPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLoginPassword.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.lblLoginPassword, 2);
            this.lblLoginPassword.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblLoginPassword.Location = new System.Drawing.Point(3, 127);
            this.lblLoginPassword.Name = "lblLoginPassword";
            this.lblLoginPassword.Size = new System.Drawing.Size(82, 23);
            this.lblLoginPassword.TabIndex = 3;
            this.lblLoginPassword.Text = "Mật khẩu";
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine
            this.txtLoginPassword.Font = Presentation.Constants.UiConstants.Typography.Body;
            this.txtLoginPassword.Location = new System.Drawing.Point(3, 153);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(449, 34);
            this.txtLoginPassword.TabIndex = 4;
            // 
            // btnToggleLoginPassword
            //
            this.btnToggleLoginPassword.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceWhite;
            this.btnToggleLoginPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleLoginPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleLoginPassword.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.btnToggleLoginPassword.Location = new System.Drawing.Point(458, 153); // Relative position, keep as is
            this.btnToggleLoginPassword.Name = "btnToggleLoginPassword"; // Name, keep as is
            this.btnToggleLoginPassword.Size = Presentation.Constants.UiConstants.Sizes.PasswordToggleButton;
            this.btnToggleLoginPassword.TabIndex = 5;
            this.btnToggleLoginPassword.Text = "👁";
            this.btnToggleLoginPassword.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            this.pnlLogin.SetColumnSpan(this.btnLogin, 2);
            this.btnLogin.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnLogin.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnLogin.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnLogin.Location = new System.Drawing.Point(3, 193);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // linkToRegister
            // 
            this.linkToRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkToRegister.AutoSize = true;
            this.pnlLogin.SetColumnSpan(this.linkToRegister, 2);
            this.linkToRegister.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.linkToRegister.Location = new System.Drawing.Point(145, 268);
            this.linkToRegister.Name = "linkToRegister";
            this.linkToRegister.Size = new System.Drawing.Size(210, 23);
            this.linkToRegister.TabIndex = 7;
            this.linkToRegister.TabStop = true;
            this.linkToRegister.Text = "Chưa có tài khoản? Đăng ký";
            // 
            // pnlRegister
            // 
            this.pnlRegister.AutoSize = true;
            this.pnlRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRegister.ColumnCount = 2;
            this.pnlRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Presentation.Constants.UiConstants.Layout.FullPercent));
            this.pnlRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Layout.PasswordToggleColumnWidth));
            this.pnlRegister.Controls.Add(this.lblRegisterTitle, 0, 0);
            this.pnlRegister.Controls.Add(this.lblRegName, 0, 1);
            this.pnlRegister.Controls.Add(this.txtRegName, 0, 2);
            this.pnlRegister.Controls.Add(this.lblRegPhone, 0, 3);
            this.pnlRegister.Controls.Add(this.txtRegPhone, 0, 4);
            this.pnlRegister.Controls.Add(this.lblRegPassword, 0, 5);
            this.pnlRegister.Controls.Add(this.txtRegPassword, 0, 6);
            this.pnlRegister.Controls.Add(this.btnToggleRegPassword, 1, 6);
            this.pnlRegister.Controls.Add(this.lblRole, 0, 7);
            this.pnlRegister.Controls.Add(this.cmbRole, 0, 8);
            this.pnlRegister.Controls.Add(this.pnlVehicleInfo, 0, 9);
            this.pnlRegister.Controls.Add(this.btnRegister, 0, 10);
            this.pnlRegister.Controls.Add(this.linkToLogin, 0, 11);
            this.pnlRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegister.Location = new System.Drawing.Point(0, Presentation.Constants.UiConstants.Heights.TitleRow + Presentation.Constants.UiConstants.Heights.LabelRow + Presentation.Constants.UiConstants.Heights.InputControlRow + Presentation.Constants.UiConstants.Heights.LabelRow + Presentation.Constants.UiConstants.Heights.InputControlRow + Presentation.Constants.UiConstants.Heights.ActionButtonRow + Presentation.Constants.UiConstants.Heights.LinkRow); // Calculated based on pnlLogin height
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.RowCount = 12;
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.TitleRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LabelRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.InputControlRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.ActionButtonRow));
            this.pnlRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.LinkRow));
            this.pnlRegister.TabIndex = 1;
            this.pnlRegister.Visible = false;
            // 
            // lblRegisterTitle
            // 
            this.lblRegisterTitle.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRegisterTitle, 2);
            this.lblRegisterTitle.Font = Presentation.Constants.UiConstants.Typography.Header;
            this.lblRegisterTitle.Location = new System.Drawing.Point(3, 10);
            this.lblRegisterTitle.Name = "lblRegisterTitle";
            this.lblRegisterTitle.Size = new System.Drawing.Size(112, 32);
            this.lblRegisterTitle.TabIndex = 0;
            this.lblRegisterTitle.Text = "Đăng ký";
            // 
            // lblRegName
            // 
            this.lblRegName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRegName.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRegName, 2);
            this.lblRegName.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblRegName.Location = new System.Drawing.Point(3, 57);
            this.lblRegName.Name = "lblRegName";
            this.lblRegName.Size = new System.Drawing.Size(83, 23);
            this.lblRegName.TabIndex = 1;
            this.lblRegName.Text = "Họ và tên";
            // 
            // txtRegName
            // 
            this.pnlRegister.SetColumnSpan(this.txtRegName, 2);
            this.txtRegName.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine
            this.txtRegName.Font = Presentation.Constants.UiConstants.Typography.Body;
            this.txtRegName.Location = new System.Drawing.Point(3, 83);
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.Size = new System.Drawing.Size(494, 34);
            this.txtRegName.TabIndex = 2;
            // 
            // lblRegPhone
            // 
            this.lblRegPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRegPhone.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRegPhone, 2);
            this.lblRegPhone.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblRegPhone.Location = new System.Drawing.Point(3, 127);
            this.lblRegPhone.Name = "lblRegPhone";
            this.lblRegPhone.Size = new System.Drawing.Size(111, 23);
            this.lblRegPhone.TabIndex = 3;
            this.lblRegPhone.Text = "Số điện thoại";
            // 
            // txtRegPhone
            // 
            this.pnlRegister.SetColumnSpan(this.txtRegPhone, 2);
            this.txtRegPhone.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine
            this.txtRegPhone.Font = Presentation.Constants.UiConstants.Typography.Body;
            this.txtRegPhone.Location = new System.Drawing.Point(3, 153);
            this.txtRegPhone.Name = "txtRegPhone";
            this.txtRegPhone.Size = new System.Drawing.Size(494, 34);
            this.txtRegPhone.TabIndex = 4;
            // 
            // lblRegPassword
            // 
            this.lblRegPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRegPassword.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRegPassword, 2);
            this.lblRegPassword.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblRegPassword.Location = new System.Drawing.Point(3, 197);
            this.lblRegPassword.Name = "lblRegPassword";
            this.lblRegPassword.Size = new System.Drawing.Size(82, 23);
            this.lblRegPassword.TabIndex = 5;
            this.lblRegPassword.Text = "Mật khẩu";
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine
            this.txtRegPassword.Font = Presentation.Constants.UiConstants.Typography.Body;
            this.txtRegPassword.Location = new System.Drawing.Point(3, 223);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(449, 34);
            this.txtRegPassword.TabIndex = 6;
            // 
            // btnToggleRegPassword
            //
            this.btnToggleRegPassword.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceWhite;
            this.btnToggleRegPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToggleRegPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleRegPassword.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.btnToggleRegPassword.Location = new System.Drawing.Point(458, 223); // Relative position, keep as is
            this.btnToggleRegPassword.Name = "btnToggleRegPassword"; // Name, keep as is
            this.btnToggleRegPassword.Size = Presentation.Constants.UiConstants.Sizes.PasswordToggleButton;
            this.btnToggleRegPassword.TabIndex = 7;
            this.btnToggleRegPassword.Text = "👁";
            this.btnToggleRegPassword.UseVisualStyleBackColor = false;
            // 
            // lblRole
            // 
            this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRole.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.lblRole, 2);
            this.lblRole.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblRole.Location = new System.Drawing.Point(3, 267);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(148, 23);
            this.lblRole.TabIndex = 8;
            this.lblRole.Text = "Bạn là...";
            // 
            // cmbRole
            // 
            this.pnlRegister.SetColumnSpan(this.cmbRole, 2);
            this.cmbRole.Dock = System.Windows.Forms.DockStyle.Fill; // Dock.Fill is fine
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] { "Hành khách", "Tài xế" });
            this.cmbRole.Location = new System.Drawing.Point(3, 293);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(494, 36);
            this.cmbRole.TabIndex = 9;
            // 
            // pnlVehicleInfo
            // 
            this.pnlVehicleInfo.AutoSize = true;
            this.pnlVehicleInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlVehicleInfo.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceLight;
            this.pnlVehicleInfo.ColumnCount = 2;
            this.pnlRegister.SetColumnSpan(this.pnlVehicleInfo, 2);
            this.pnlVehicleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Presentation.Constants.UiConstants.Layout.LabelColumnPercent));
            this.pnlVehicleInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, Presentation.Constants.UiConstants.Layout.InputColumnPercent));
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
            this.pnlVehicleInfo.Location = new System.Drawing.Point(3, 333); // Relative position, keep as is
            this.pnlVehicleInfo.Margin = Presentation.Constants.UiConstants.Layout.VehicleInfoPanelMargin;
            this.pnlVehicleInfo.Name = "pnlVehicleInfo";
            this.pnlVehicleInfo.RowCount = 6;
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Presentation.Constants.UiConstants.Heights.VehicleInfoRow));
            this.pnlVehicleInfo.TabIndex = 10;
            this.pnlVehicleInfo.Visible = false;
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblVehicleType.Location = new System.Drawing.Point(3, 8);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(63, 23);
            this.lblVehicleType.TabIndex = 0;
            this.lblVehicleType.Text = "Loại xe:";
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; // Keep as is
            this.cmbVehicleType.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] { "Xe máy", "Ô tô" });
            this.cmbVehicleType.Location = new System.Drawing.Point(177, 5);
            this.cmbVehicleType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(314, 31);
            this.cmbVehicleType.TabIndex = 1;
            // 
            // lblPlate
            // 
            this.lblPlate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlate.AutoSize = true;
            this.lblPlate.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblPlate.Location = new System.Drawing.Point(3, 48);
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Size = new System.Drawing.Size(69, 23);
            this.lblPlate.TabIndex = 2;
            this.lblPlate.Text = "Biển số:";
            // 
            // txtPlate
            // 
            this.txtPlate.Dock = System.Windows.Forms.DockStyle.Fill; // Keep as is
            this.txtPlate.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.txtPlate.Location = new System.Drawing.Point(177, 45);
            this.txtPlate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtPlate.Name = "txtPlate";
            this.txtPlate.Size = new System.Drawing.Size(314, 30);
            this.txtPlate.TabIndex = 3;
            // 
            // lblBrand
            // 
            this.lblBrand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblBrand.Location = new System.Drawing.Point(3, 88);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(74, 23);
            this.lblBrand.TabIndex = 4;
            this.lblBrand.Text = "Hãng xe:";
            // 
            // txtBrand
            // 
            this.txtBrand.Dock = System.Windows.Forms.DockStyle.Fill; // Keep as is
            this.txtBrand.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.txtBrand.Location = new System.Drawing.Point(177, 85);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(314, 30);
            this.txtBrand.TabIndex = 5;
            // 
            // lblModel
            // 
            this.lblModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblModel.AutoSize = true;
            this.lblModel.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblModel.Location = new System.Drawing.Point(3, 128);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(70, 23);
            this.lblModel.TabIndex = 6;
            this.lblModel.Text = "Mẫu xe:";
            // 
            // txtModel
            // 
            this.txtModel.Dock = System.Windows.Forms.DockStyle.Fill; // Keep as is
            this.txtModel.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.txtModel.Location = new System.Drawing.Point(177, 125);
            this.txtModel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(314, 30);
            this.txtModel.TabIndex = 7;
            // 
            // lblColor
            // 
            this.lblColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColor.AutoSize = true;
            this.lblColor.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblColor.Location = new System.Drawing.Point(3, 168);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(61, 23);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "Màu xe:";
            // 
            // txtColor
            // 
            this.txtColor.Dock = System.Windows.Forms.DockStyle.Fill; // Keep as is
            this.txtColor.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.txtColor.Location = new System.Drawing.Point(177, 165);
            this.txtColor.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(314, 30);
            this.txtColor.TabIndex = 9;
            // 
            // lblCapacity
            // 
            this.lblCapacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.lblCapacity.Location = new System.Drawing.Point(3, 208);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(75, 23);
            this.lblCapacity.TabIndex = 10;
            this.lblCapacity.Text = "Sức chứa:";
            // 
            // numCapacity
            // 
            this.numCapacity.Anchor = System.Windows.Forms.AnchorStyles.Left; // Keep as is
            this.numCapacity.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.numCapacity.Location = new System.Drawing.Point(177, 205);
            this.numCapacity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.numCapacity.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.numCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numCapacity.Name = "numCapacity";
            this.numCapacity.Size = new System.Drawing.Size(100, 30);
            this.numCapacity.TabIndex = 11;
            this.numCapacity.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // btnRegister
            // 
            this.pnlRegister.SetColumnSpan(this.btnRegister, 2);
            this.btnRegister.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnRegister.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnRegister.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnRegister.Location = new System.Drawing.Point(3, 400);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.TabIndex = 11;
            this.btnRegister.Text = "Tạo tài khoản";
            this.btnRegister.UseVisualStyleBackColor = false;
            // 
            // linkToLogin
            // 
            this.linkToLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkToLogin.AutoSize = true;
            this.pnlRegister.SetColumnSpan(this.linkToLogin, 2);
            this.linkToLogin.Font = Presentation.Constants.UiConstants.Typography.Small;
            this.linkToLogin.Location = new System.Drawing.Point(145, 478);
            this.linkToLogin.Name = "linkToLogin";
            this.linkToLogin.Size = new System.Drawing.Size(210, 23);
            this.linkToLogin.TabIndex = 12;
            this.linkToLogin.TabStop = true;
            this.linkToLogin.Text = "Đã có tài khoản? Đăng nhập";
            // 
            // lblFooter
            // 
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooter.Font = Presentation.Constants.UiConstants.Typography.Tiny;
            this.lblFooter.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;
            this.lblFooter.Location = new System.Drawing.Point(3, 560);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(894, 40);
            this.lblFooter.TabIndex = 2;
            this.lblFooter.Text = "RideGo 2026 - Ứng dụng đặt xe";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // UcAuth
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Keep as is
            this.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceWhite;
            this.Controls.Add(this.tblMain);
            this.Name = "UcAuth";
            this.Size = Presentation.Constants.UiConstants.Sizes.DefaultForm;
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
