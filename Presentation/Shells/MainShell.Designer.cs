namespace Presentation.Shells
{
    partial class MainShell : BaseShell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Button ButtonLogin;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Button ButtonRegister;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Button ButtonDual;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Button ButtonExit;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Panel _statusPanel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Panel header;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Label lblTitle;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Label lblSub;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Panel contentHost;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Panel card;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.TableLayoutPanel stack;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Label lblTest;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.CheckBox chkSim;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonLogin = new System.Windows.Forms.Button();
            this.ButtonRegister = new System.Windows.Forms.Button();
            this.ButtonDual = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.lblTest = new System.Windows.Forms.Label();
            this.chkSim = new System.Windows.Forms.CheckBox();
            this.stack = new System.Windows.Forms.TableLayoutPanel();
            this.card = new System.Windows.Forms.Panel();
            this.contentHost = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.header = new System.Windows.Forms.Panel();
            this._statusPanel = new System.Windows.Forms.Panel();
            this.stack.SuspendLayout();
            this.card.SuspendLayout();
            this.contentHost.SuspendLayout();
            this.header.SuspendLayout();
            this.SuspendLayout();
            //
            // ButtonLogin
            //
            this.ButtonLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonLogin.Height = 42;
            this.ButtonLogin.Location = new System.Drawing.Point(28, 20);
            this.ButtonLogin.Name = "ButtonLogin";
            this.ButtonLogin.Size = new System.Drawing.Size(484, 42);
            this.ButtonLogin.TabIndex = 0;
            this.ButtonLogin.Text = "Đăng nhập";
            this.ButtonLogin.UseVisualStyleBackColor = true;
            //
            // ButtonRegister
            //
            this.ButtonRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonRegister.Height = 42;
            this.ButtonRegister.Location = new System.Drawing.Point(28, 72);
            this.ButtonRegister.Name = "ButtonRegister";
            this.ButtonRegister.Size = new System.Drawing.Size(484, 42);
            this.ButtonRegister.TabIndex = 1;
            this.ButtonRegister.Text = "Đăng ký";
            this.ButtonRegister.UseVisualStyleBackColor = true;
            //
            // ButtonDual
            //
            this.ButtonDual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonDual.Height = 38;
            this.ButtonDual.Location = new System.Drawing.Point(28, 124);
            this.ButtonDual.Name = "ButtonDual";
            this.ButtonDual.Size = new System.Drawing.Size(484, 38);
            this.ButtonDual.TabIndex = 2;
            this.ButtonDual.Text = "Mở song song KH + TX";
            this.ButtonDual.UseVisualStyleBackColor = true;
            //
            // ButtonExit
            //
            this.ButtonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonExit.Height = 36;
            this.ButtonExit.Location = new System.Drawing.Point(28, 192);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(484, 36);
            this.ButtonExit.TabIndex = 5;
            this.ButtonExit.Text = "Thoát";
            this.ButtonExit.UseVisualStyleBackColor = true;
            //
            // lblTest
            //
            this.lblTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTest.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTest.ForeColor = System.Drawing.Color.Gray;
            this.lblTest.Location = new System.Drawing.Point(28, 162);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(484, 32);
            this.lblTest.TabIndex = 3;
            this.lblTest.Text = "Test: KH 0900000001 / 123456  •  TX 0900000003 / 123456";
            this.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // chkSim
            //
            this.chkSim.Checked = true;
            this.chkSim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSim.Location = new System.Drawing.Point(28, 194);
            this.chkSim.Name = "chkSim";
            this.chkSim.Size = new System.Drawing.Size(484, 38);
            this.chkSim.TabIndex = 4;
            this.chkSim.Text = "Bật mô phỏng tự động";
            this.chkSim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSim.UseVisualStyleBackColor = true;
            //
            // stack
            //
            this.stack.ColumnCount = 1;
            this.stack.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.stack.Controls.Add(this.ButtonLogin, 0, 0);
            this.stack.Controls.Add(this.ButtonRegister, 0, 1);
            this.stack.Controls.Add(this.ButtonDual, 0, 2);
            this.stack.Controls.Add(this.lblTest, 0, 3);
            this.stack.Controls.Add(this.chkSim, 0, 4);
            this.stack.Controls.Add(this.ButtonExit, 0, 5);
            this.stack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stack.Location = new System.Drawing.Point(0, 0);
            this.stack.Name = "stack";
            this.stack.Padding = new System.Windows.Forms.Padding(28, 20, 28, 16);
            this.stack.RowCount = 6;
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.stack.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.stack.Size = new System.Drawing.Size(540, 384);
            this.stack.TabIndex = 0;
            //
            // card
            //
            this.card.BackColor = System.Drawing.Color.White;
            this.card.Controls.Add(this.stack);
            this.card.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card.Location = new System.Drawing.Point(28, 20);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(540, 384);
            this.card.TabIndex = 0;
            //
            // contentHost
            //
            this.contentHost.BackColor = System.Drawing.Color.LightGray;
            this.contentHost.Controls.Add(this.card);
            this.contentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentHost.Location = new System.Drawing.Point(0, 96);
            this.contentHost.Name = "contentHost";
            this.contentHost.Padding = new System.Windows.Forms.Padding(28, 20, 28, 16);
            this.contentHost.Size = new System.Drawing.Size(540, 404);
            this.contentHost.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Height = 40;
            this.lblTitle.Location = new System.Drawing.Point(28, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(484, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "OOP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSub
            //
            this.lblSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.ForeColor = System.Drawing.Color.LightBlue;
            this.lblSub.Height = 20;
            this.lblSub.Location = new System.Drawing.Point(28, 56);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(484, 20);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Nền tảng đặt xe nội bộ — demo hệ thống";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // header
            //
            this.header.BackColor = System.Drawing.Color.Blue;
            this.header.Controls.Add(this.lblSub);
            this.header.Controls.Add(this.lblTitle);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Height = 96;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(28, 16, 28, 10);
            this.header.Size = new System.Drawing.Size(540, 96);
            this.header.TabIndex = 0;
            //
            // _statusPanel
            //
            this._statusPanel.BackColor = System.Drawing.Color.DarkGray;
            this._statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusPanel.Height = 120;
            this._statusPanel.Location = new System.Drawing.Point(0, 500);
            this._statusPanel.Name = "_statusPanel";
            this._statusPanel.Size = new System.Drawing.Size(540, 120);
            this._statusPanel.TabIndex = 1;
            //
            // MainShell
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(540, 620);
            this.Controls.Add(this.contentHost);
            this.Controls.Add(this.header);
            this.Controls.Add(this._statusPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 540);
            this.Name = "MainShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP Ride-Hailing";
            this.stack.ResumeLayout(false);
            this.card.ResumeLayout(false);
            this.contentHost.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}