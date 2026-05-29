namespace OOP2026
{
    partial class UcAdminUser
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpDriver = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDriverTitle = new System.Windows.Forms.Label();
            this.lblDriverSub = new System.Windows.Forms.Label();
            this.lblDriverCount = new System.Windows.Forms.Label();
            this.flpPass = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPassTitle = new System.Windows.Forms.Label();
            this.lblPassSub = new System.Windows.Forms.Label();
            this.lblPassCount = new System.Windows.Forms.Label();
            this.flpAdmin = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblAdminSub = new System.Windows.Forms.Label();
            this.lblAdminCount = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.flpDriver.SuspendLayout();
            this.flpPass.SuspendLayout();
            this.flpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpMain.Controls.Add(this.flpDriver, 0, 0);
            this.tlpMain.Controls.Add(this.lblDriverCount, 1, 0);
            this.tlpMain.Controls.Add(this.flpPass, 0, 1);
            this.tlpMain.Controls.Add(this.lblPassCount, 1, 1);
            this.tlpMain.Controls.Add(this.flpAdmin, 0, 2);
            this.tlpMain.Controls.Add(this.lblAdminCount, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(10);
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpMain.Size = new System.Drawing.Size(150, 150);
            this.tlpMain.TabIndex = 0;
            // 
            // flpDriver
            // 
            this.flpDriver.AutoSize = true;
            this.flpDriver.Controls.Add(this.lblDriverTitle);
            this.flpDriver.Controls.Add(this.lblDriverSub);
            this.flpDriver.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDriver.Location = new System.Drawing.Point(13, 13);
            this.flpDriver.Name = "flpDriver";
            this.flpDriver.Size = new System.Drawing.Size(57, 14);
            this.flpDriver.TabIndex = 0;
            // 
            // lblDriverTitle
            // 
            this.lblDriverTitle.AutoSize = true;
            this.lblDriverTitle.Location = new System.Drawing.Point(3, 0);
            this.lblDriverTitle.Name = "lblDriverTitle";
            this.lblDriverTitle.Size = new System.Drawing.Size(44, 16);
            this.lblDriverTitle.TabIndex = 0;
            this.lblDriverTitle.Text = "Tài xế";
            // 
            // lblDriverSub
            // 
            this.lblDriverSub.AutoSize = true;
            this.lblDriverSub.ForeColor = System.Drawing.Color.Gray;
            this.lblDriverSub.Location = new System.Drawing.Point(3, 16);
            this.lblDriverSub.Name = "lblDriverSub";
            this.lblDriverSub.Size = new System.Drawing.Size(87, 16);
            this.lblDriverSub.TabIndex = 1;
            this.lblDriverSub.Text = "0 đang online";
            // 
            // lblDriverCount
            // 
            this.lblDriverCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDriverCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDriverCount.Location = new System.Drawing.Point(104, 10);
            this.lblDriverCount.Name = "lblDriverCount";
            this.lblDriverCount.Size = new System.Drawing.Size(33, 20);
            this.lblDriverCount.TabIndex = 1;
            this.lblDriverCount.Text = "0";
            this.lblDriverCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flpPass
            // 
            this.flpPass.AutoSize = true;
            this.flpPass.Controls.Add(this.lblPassTitle);
            this.flpPass.Controls.Add(this.lblPassSub);
            this.flpPass.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPass.Location = new System.Drawing.Point(13, 33);
            this.flpPass.Name = "flpPass";
            this.flpPass.Size = new System.Drawing.Size(85, 14);
            this.flpPass.TabIndex = 2;
            // 
            // lblPassTitle
            // 
            this.lblPassTitle.AutoSize = true;
            this.lblPassTitle.Location = new System.Drawing.Point(3, 0);
            this.lblPassTitle.Name = "lblPassTitle";
            this.lblPassTitle.Size = new System.Drawing.Size(78, 16);
            this.lblPassTitle.TabIndex = 0;
            this.lblPassTitle.Text = "Hành khách";
            // 
            // lblPassSub
            // 
            this.lblPassSub.AutoSize = true;
            this.lblPassSub.ForeColor = System.Drawing.Color.Gray;
            this.lblPassSub.Location = new System.Drawing.Point(3, 16);
            this.lblPassSub.Name = "lblPassSub";
            this.lblPassSub.Size = new System.Drawing.Size(74, 16);
            this.lblPassSub.TabIndex = 1;
            this.lblPassSub.Text = "đã đăng ký";
            // 
            // lblPassCount
            // 
            this.lblPassCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPassCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPassCount.Location = new System.Drawing.Point(104, 30);
            this.lblPassCount.Name = "lblPassCount";
            this.lblPassCount.Size = new System.Drawing.Size(33, 20);
            this.lblPassCount.TabIndex = 3;
            this.lblPassCount.Text = "0";
            this.lblPassCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flpAdmin
            // 
            this.flpAdmin.AutoSize = true;
            this.flpAdmin.Controls.Add(this.lblAdminTitle);
            this.flpAdmin.Controls.Add(this.lblAdminSub);
            this.flpAdmin.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAdmin.Location = new System.Drawing.Point(13, 53);
            this.flpAdmin.Name = "flpAdmin";
            this.flpAdmin.Size = new System.Drawing.Size(84, 32);
            this.flpAdmin.TabIndex = 4;
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Location = new System.Drawing.Point(3, 0);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(45, 16);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "Admin";
            // 
            // lblAdminSub
            // 
            this.lblAdminSub.AutoSize = true;
            this.lblAdminSub.ForeColor = System.Drawing.Color.Gray;
            this.lblAdminSub.Location = new System.Drawing.Point(3, 16);
            this.lblAdminSub.Name = "lblAdminSub";
            this.lblAdminSub.Size = new System.Drawing.Size(78, 16);
            this.lblAdminSub.TabIndex = 1;
            this.lblAdminSub.Text = "quản trị viên";
            // 
            // lblAdminCount
            // 
            this.lblAdminCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAdminCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAdminCount.Location = new System.Drawing.Point(104, 50);
            this.lblAdminCount.Name = "lblAdminCount";
            this.lblAdminCount.Size = new System.Drawing.Size(33, 90);
            this.lblAdminCount.TabIndex = 5;
            this.lblAdminCount.Text = "0";
            this.lblAdminCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UcAdminUser
            // 
            this.Controls.Add(this.tlpMain);
            this.Name = "UcAdminUser";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.flpDriver.ResumeLayout(false);
            this.flpDriver.PerformLayout();
            this.flpPass.ResumeLayout(false);
            this.flpPass.PerformLayout();
            this.flpAdmin.ResumeLayout(false);
            this.flpAdmin.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.FlowLayoutPanel flpDriver, flpPass, flpAdmin;
        private System.Windows.Forms.Label lblDriverTitle, lblDriverSub, lblDriverCount;
        private System.Windows.Forms.Label lblPassTitle, lblPassSub, lblPassCount;
        private System.Windows.Forms.Label lblAdminTitle, lblAdminSub, lblAdminCount;
    }
}