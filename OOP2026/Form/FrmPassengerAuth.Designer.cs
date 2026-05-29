namespace OOP2026
{
    partial class FrmPassengerAuth
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.tlpTabsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnTabLogin = new System.Windows.Forms.Button();
            this.btnTabRegister = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tlpLoginLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDemoAccount = new System.Windows.Forms.Panel();
            this.lblDemoText = new System.Windows.Forms.Label();
            this.lblLoginPhoneTitle = new System.Windows.Forms.Label();
            this.txtLoginPhone = new System.Windows.Forms.TextBox();
            this.lblLoginPassTitle = new System.Windows.Forms.Label();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.tlpRegisterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblRegNameTitle = new System.Windows.Forms.Label();
            this.txtRegName = new System.Windows.Forms.TextBox();
            this.lblRegPhoneTitle = new System.Windows.Forms.Label();
            this.txtRegPhone = new System.Windows.Forms.TextBox();
            this.lblRegPassTitle = new System.Windows.Forms.Label();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.tlpTabsLayout.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.tlpLoginLayout.SuspendLayout();
            this.pnlDemoAccount.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.tlpRegisterLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(420, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Padding = new System.Windows.Forms.Padding(45, 0, 0, 0);
            this.lblHeaderTitle.Size = new System.Drawing.Size(375, 55);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Hành khách Xác thực";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(375, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 55);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // pnlTabs
            // 
            this.pnlTabs.Controls.Add(this.tlpTabsLayout);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Location = new System.Drawing.Point(0, 55);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(420, 46);
            this.pnlTabs.TabIndex = 1;
            // 
            // tlpTabsLayout
            // 
            this.tlpTabsLayout.ColumnCount = 2;
            this.tlpTabsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabsLayout.Controls.Add(this.btnTabLogin, 0, 0);
            this.tlpTabsLayout.Controls.Add(this.btnTabRegister, 1, 0);
            this.tlpTabsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTabsLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpTabsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTabsLayout.Name = "tlpTabsLayout";
            this.tlpTabsLayout.RowCount = 1;
            this.tlpTabsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTabsLayout.Size = new System.Drawing.Size(420, 46);
            this.tlpTabsLayout.TabIndex = 0;
            // 
            // btnTabLogin
            // 
            this.btnTabLogin.BackColor = System.Drawing.Color.White;
            this.btnTabLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTabLogin.FlatAppearance.BorderSize = 0;
            this.btnTabLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.btnTabLogin.Location = new System.Drawing.Point(0, 0);
            this.btnTabLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnTabLogin.Name = "btnTabLogin";
            this.btnTabLogin.Size = new System.Drawing.Size(210, 46);
            this.btnTabLogin.TabIndex = 0;
            this.btnTabLogin.Text = "Đăng nhập";
            this.btnTabLogin.UseVisualStyleBackColor = false;
            this.btnTabLogin.Click += new System.EventHandler(this.BtnTabLogin_Click);
            // 
            // btnTabRegister
            // 
            this.btnTabRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnTabRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTabRegister.FlatAppearance.BorderSize = 0;
            this.btnTabRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnTabRegister.Location = new System.Drawing.Point(210, 0);
            this.btnTabRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnTabRegister.Name = "btnTabRegister";
            this.btnTabRegister.Size = new System.Drawing.Size(210, 46);
            this.btnTabRegister.TabIndex = 1;
            this.btnTabRegister.Text = "Đăng ký";
            this.btnTabRegister.UseVisualStyleBackColor = false;
            this.btnTabRegister.Click += new System.EventHandler(this.BtnTabRegister_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlLogin);
            this.pnlContent.Controls.Add(this.pnlRegister);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 101);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlContent.Size = new System.Drawing.Size(420, 499);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.tlpLoginLayout);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(20, 16);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(380, 467);
            this.pnlLogin.TabIndex = 0;
            // 
            // tlpLoginLayout
            // 
            this.tlpLoginLayout.ColumnCount = 1;
            this.tlpLoginLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLoginLayout.Controls.Add(this.pnlDemoAccount, 0, 0);
            this.tlpLoginLayout.Controls.Add(this.lblLoginPhoneTitle, 0, 1);
            this.tlpLoginLayout.Controls.Add(this.txtLoginPhone, 0, 2);
            this.tlpLoginLayout.Controls.Add(this.lblLoginPassTitle, 0, 3);
            this.tlpLoginLayout.Controls.Add(this.txtLoginPassword, 0, 4);
            this.tlpLoginLayout.Controls.Add(this.btnLogin, 0, 5);
            this.tlpLoginLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpLoginLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpLoginLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLoginLayout.Name = "tlpLoginLayout";
            this.tlpLoginLayout.RowCount = 6;
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpLoginLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpLoginLayout.Size = new System.Drawing.Size(380, 257);
            this.tlpLoginLayout.TabIndex = 0;
            // 
            // pnlDemoAccount
            // 
            this.pnlDemoAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlDemoAccount.Controls.Add(this.lblDemoText);
            this.pnlDemoAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDemoAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDemoAccount.Location = new System.Drawing.Point(0, 0);
            this.pnlDemoAccount.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlDemoAccount.Name = "pnlDemoAccount";
            this.pnlDemoAccount.Size = new System.Drawing.Size(380, 51);
            this.pnlDemoAccount.TabIndex = 0;
            this.pnlDemoAccount.Click += new System.EventHandler(this.PnlDemoAccount_Click);
            // 
            // lblDemoText
            // 
            this.lblDemoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDemoText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDemoText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblDemoText.Location = new System.Drawing.Point(0, 0);
            this.lblDemoText.Name = "lblDemoText";
            this.lblDemoText.Size = new System.Drawing.Size(380, 51);
            this.lblDemoText.TabIndex = 0;
            this.lblDemoText.Text = "💡 Tài khoản hành khách dùng thử:\r\n🔹 SĐT: 0911111111   |   🔑 Mật khẩu: 123456";
            this.lblDemoText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDemoText.Click += new System.EventHandler(this.PnlDemoAccount_Click);
            // 
            // lblLoginPhoneTitle
            // 
            this.lblLoginPhoneTitle.AutoSize = true;
            this.lblLoginPhoneTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoginPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoginPhoneTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoginPhoneTitle.Location = new System.Drawing.Point(3, 65);
            this.lblLoginPhoneTitle.Name = "lblLoginPhoneTitle";
            this.lblLoginPhoneTitle.Size = new System.Drawing.Size(374, 24);
            this.lblLoginPhoneTitle.TabIndex = 1;
            this.lblLoginPhoneTitle.Text = "Số điện thoại";
            this.lblLoginPhoneTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtLoginPhone
            // 
            this.txtLoginPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoginPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginPhone.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtLoginPhone.Location = new System.Drawing.Point(0, 93);
            this.txtLoginPhone.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.txtLoginPhone.Name = "txtLoginPhone";
            this.txtLoginPhone.Size = new System.Drawing.Size(380, 31);
            this.txtLoginPhone.TabIndex = 1;
            // 
            // lblLoginPassTitle
            // 
            this.lblLoginPassTitle.AutoSize = true;
            this.lblLoginPassTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoginPassTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoginPassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoginPassTitle.Location = new System.Drawing.Point(3, 131);
            this.lblLoginPassTitle.Name = "lblLoginPassTitle";
            this.lblLoginPassTitle.Size = new System.Drawing.Size(374, 24);
            this.lblLoginPassTitle.TabIndex = 2;
            this.lblLoginPassTitle.Text = "Mật khẩu";
            this.lblLoginPassTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtLoginPassword.Location = new System.Drawing.Point(0, 157);
            this.txtLoginPassword.Margin = new System.Windows.Forms.Padding(0, 2, 0, 12);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '•';
            this.txtLoginPassword.Size = new System.Drawing.Size(380, 31);
            this.txtLoginPassword.TabIndex = 2;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(0, 209);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(380, 48);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // pnlRegister
            // 
            this.pnlRegister.Controls.Add(this.tlpRegisterLayout);
            this.pnlRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRegister.Location = new System.Drawing.Point(20, 16);
            this.pnlRegister.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(380, 467);
            this.pnlRegister.TabIndex = 1;
            this.pnlRegister.Visible = false;
            // 
            // tlpRegisterLayout
            // 
            this.tlpRegisterLayout.ColumnCount = 1;
            this.tlpRegisterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRegisterLayout.Controls.Add(this.lblRegNameTitle, 0, 0);
            this.tlpRegisterLayout.Controls.Add(this.txtRegName, 0, 1);
            this.tlpRegisterLayout.Controls.Add(this.lblRegPhoneTitle, 0, 2);
            this.tlpRegisterLayout.Controls.Add(this.txtRegPhone, 0, 3);
            this.tlpRegisterLayout.Controls.Add(this.lblRegPassTitle, 0, 4);
            this.tlpRegisterLayout.Controls.Add(this.txtRegPassword, 0, 5);
            this.tlpRegisterLayout.Controls.Add(this.btnRegister, 0, 6);
            this.tlpRegisterLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpRegisterLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpRegisterLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRegisterLayout.Name = "tlpRegisterLayout";
            this.tlpRegisterLayout.RowCount = 7;
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpRegisterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpRegisterLayout.Size = new System.Drawing.Size(380, 258);
            this.tlpRegisterLayout.TabIndex = 0;
            // 
            // lblRegNameTitle
            // 
            this.lblRegNameTitle.AutoSize = true;
            this.lblRegNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRegNameTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegNameTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRegNameTitle.Location = new System.Drawing.Point(3, 0);
            this.lblRegNameTitle.Name = "lblRegNameTitle";
            this.lblRegNameTitle.Size = new System.Drawing.Size(374, 24);
            this.lblRegNameTitle.TabIndex = 0;
            this.lblRegNameTitle.Text = "Họ và tên";
            this.lblRegNameTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtRegName
            // 
            this.txtRegName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegName.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtRegName.Location = new System.Drawing.Point(0, 28);
            this.txtRegName.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.Size = new System.Drawing.Size(380, 31);
            this.txtRegName.TabIndex = 0;
            // 
            // lblRegPhoneTitle
            // 
            this.lblRegPhoneTitle.AutoSize = true;
            this.lblRegPhoneTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRegPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegPhoneTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRegPhoneTitle.Location = new System.Drawing.Point(3, 66);
            this.lblRegPhoneTitle.Name = "lblRegPhoneTitle";
            this.lblRegPhoneTitle.Size = new System.Drawing.Size(374, 24);
            this.lblRegPhoneTitle.TabIndex = 1;
            this.lblRegPhoneTitle.Text = "Số điện thoại";
            this.lblRegPhoneTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtRegPhone
            // 
            this.txtRegPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegPhone.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtRegPhone.Location = new System.Drawing.Point(0, 94);
            this.txtRegPhone.Margin = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.txtRegPhone.Name = "txtRegPhone";
            this.txtRegPhone.Size = new System.Drawing.Size(380, 31);
            this.txtRegPhone.TabIndex = 1;
            // 
            // lblRegPassTitle
            // 
            this.lblRegPassTitle.AutoSize = true;
            this.lblRegPassTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRegPassTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegPassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRegPassTitle.Location = new System.Drawing.Point(3, 132);
            this.lblRegPassTitle.Name = "lblRegPassTitle";
            this.lblRegPassTitle.Size = new System.Drawing.Size(374, 24);
            this.lblRegPassTitle.TabIndex = 2;
            this.lblRegPassTitle.Text = "Thiết lập mật khẩu";
            this.lblRegPassTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtRegPassword.Location = new System.Drawing.Point(0, 158);
            this.txtRegPassword.Margin = new System.Windows.Forms.Padding(0, 2, 0, 14);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '•';
            this.txtRegPassword.Size = new System.Drawing.Size(380, 31);
            this.txtRegPassword.TabIndex = 2;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(0, 210);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(380, 48);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "TẠO TÀI KHOẢN MỚI";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // FrmPassengerAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPassengerAuth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passenger Authentication";
            this.pnlHeader.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.tlpTabsLayout.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.tlpLoginLayout.ResumeLayout(false);
            this.tlpLoginLayout.PerformLayout();
            this.pnlDemoAccount.ResumeLayout(false);
            this.pnlRegister.ResumeLayout(false);
            this.tlpRegisterLayout.ResumeLayout(false);
            this.tlpRegisterLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.TableLayoutPanel tlpTabsLayout;
        private System.Windows.Forms.Button btnTabLogin;
        private System.Windows.Forms.Button btnTabRegister;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TableLayoutPanel tlpLoginLayout;
        private System.Windows.Forms.Panel pnlDemoAccount;
        private System.Windows.Forms.Label lblDemoText;
        private System.Windows.Forms.Label lblLoginPhoneTitle;
        private System.Windows.Forms.TextBox txtLoginPhone;
        private System.Windows.Forms.Label lblLoginPassTitle;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.TableLayoutPanel tlpRegisterLayout;
        private System.Windows.Forms.Label lblRegNameTitle;
        private System.Windows.Forms.TextBox txtRegName;
        private System.Windows.Forms.Label lblRegPhoneTitle;
        private System.Windows.Forms.TextBox txtRegPhone;
        private System.Windows.Forms.Label lblRegPassTitle;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.Button btnRegister;
    }
}