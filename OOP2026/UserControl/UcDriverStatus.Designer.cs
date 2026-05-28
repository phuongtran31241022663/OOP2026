namespace OOP2026
{
    partial class ucDriverStatus
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlToggle = new System.Windows.Forms.Panel();
            this.chkToggleStatus = new System.Windows.Forms.CheckBox();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIncomeCard = new System.Windows.Forms.Panel();
            this.lblIncomeTitle = new System.Windows.Forms.Label();
            this.lblTodayIncome = new System.Windows.Forms.Label();
            this.pnlWalletCard = new System.Windows.Forms.Panel();
            this.lblWalletTitle = new System.Windows.Forms.Label();
            this.lblWalletBalance = new System.Windows.Forms.Label();
            this.pnlVehicle = new System.Windows.Forms.Panel();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.lblVehiclePlate = new System.Windows.Forms.Label();
            this.lblVehicleColor = new System.Windows.Forms.Label();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlToggle.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlIncomeCard.SuspendLayout();
            this.pnlWalletCard.SuspendLayout();
            this.pnlVehicle.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlToggle, 0, 0);
            this.tlpMain.Controls.Add(this.tlpStats, 0, 1);
            this.tlpMain.Controls.Add(this.pnlVehicle, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(280, 380);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlToggle
            // 
            this.pnlToggle.BackColor = System.Drawing.Color.White;
            this.pnlToggle.Controls.Add(this.chkToggleStatus);
            this.pnlToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToggle.Location = new System.Drawing.Point(0, 0);
            this.pnlToggle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlToggle.Name = "pnlToggle";
            this.pnlToggle.Size = new System.Drawing.Size(280, 50);
            this.pnlToggle.TabIndex = 0;
            // 
            // chkToggleStatus
            // 
            this.chkToggleStatus.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkToggleStatus.BackColor = System.Drawing.Color.White;
            this.chkToggleStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkToggleStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkToggleStatus.FlatAppearance.BorderSize = 0;
            this.chkToggleStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkToggleStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkToggleStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(5)))));
            this.chkToggleStatus.Location = new System.Drawing.Point(0, 0);
            this.chkToggleStatus.Name = "chkToggleStatus";
            this.chkToggleStatus.Size = new System.Drawing.Size(280, 50);
            this.chkToggleStatus.TabIndex = 1;
            this.chkToggleStatus.Text = "Đang tắt nhận cuốc";
            this.chkToggleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkToggleStatus.UseVisualStyleBackColor = false;
            this.chkToggleStatus.CheckedChanged += new System.EventHandler(this.ChkToggleStatus_CheckedChanged);
            // 
            // tlpStats
            // 
            this.tlpStats.ColumnCount = 2;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.Controls.Add(this.pnlIncomeCard, 0, 0);
            this.tlpStats.Controls.Add(this.pnlWalletCard, 1, 0);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Location = new System.Drawing.Point(0, 50);
            this.tlpStats.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(280, 95);
            this.tlpStats.TabIndex = 1;
            // 
            // pnlIncomeCard
            // 
            this.pnlIncomeCard.BackColor = System.Drawing.Color.White;
            this.pnlIncomeCard.Controls.Add(this.lblIncomeTitle);
            this.pnlIncomeCard.Controls.Add(this.lblTodayIncome);
            this.pnlIncomeCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIncomeCard.Location = new System.Drawing.Point(0, 10);
            this.pnlIncomeCard.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.pnlIncomeCard.Name = "pnlIncomeCard";
            this.pnlIncomeCard.Size = new System.Drawing.Size(135, 75);
            this.pnlIncomeCard.TabIndex = 0;
            // 
            // lblIncomeTitle
            // 
            this.lblIncomeTitle.AutoSize = true;
            this.lblIncomeTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblIncomeTitle.Location = new System.Drawing.Point(8, 10);
            this.lblIncomeTitle.Name = "lblIncomeTitle";
            this.lblIncomeTitle.Size = new System.Drawing.Size(131, 20);
            this.lblIncomeTitle.TabIndex = 1;
            this.lblIncomeTitle.Text = "Thu nhập hôm nay";
            // 
            // lblTodayIncome
            // 
            this.lblTodayIncome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTodayIncome.ForeColor = System.Drawing.Color.Black;
            this.lblTodayIncome.Location = new System.Drawing.Point(3, 35);
            this.lblTodayIncome.Name = "lblTodayIncome";
            this.lblTodayIncome.Size = new System.Drawing.Size(129, 35);
            this.lblTodayIncome.TabIndex = 0;
            this.lblTodayIncome.Text = "0d";
            this.lblTodayIncome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlWalletCard
            // 
            this.pnlWalletCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(5)))));
            this.pnlWalletCard.Controls.Add(this.lblWalletTitle);
            this.pnlWalletCard.Controls.Add(this.lblWalletBalance);
            this.pnlWalletCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWalletCard.Location = new System.Drawing.Point(145, 10);
            this.pnlWalletCard.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlWalletCard.Name = "pnlWalletCard";
            this.pnlWalletCard.Size = new System.Drawing.Size(135, 75);
            this.pnlWalletCard.TabIndex = 1;
            // 
            // lblWalletTitle
            // 
            this.lblWalletTitle.AutoSize = true;
            this.lblWalletTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWalletTitle.ForeColor = System.Drawing.Color.White;
            this.lblWalletTitle.Location = new System.Drawing.Point(8, 10);
            this.lblWalletTitle.Name = "lblWalletTitle";
            this.lblWalletTitle.Size = new System.Drawing.Size(63, 20);
            this.lblWalletTitle.TabIndex = 1;
            this.lblWalletTitle.Text = "Số dư ví";
            // 
            // lblWalletBalance
            // 
            this.lblWalletBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWalletBalance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblWalletBalance.ForeColor = System.Drawing.Color.White;
            this.lblWalletBalance.Location = new System.Drawing.Point(3, 35);
            this.lblWalletBalance.Name = "lblWalletBalance";
            this.lblWalletBalance.Size = new System.Drawing.Size(129, 35);
            this.lblWalletBalance.TabIndex = 0;
            this.lblWalletBalance.Text = "0d";
            this.lblWalletBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlVehicle
            // 
            this.pnlVehicle.BackColor = System.Drawing.Color.White;
            this.pnlVehicle.Controls.Add(this.lblVehicleName);
            this.pnlVehicle.Controls.Add(this.lblVehiclePlate);
            this.pnlVehicle.Controls.Add(this.lblVehicleColor);
            this.pnlVehicle.Controls.Add(this.lblVehicleType);
            this.pnlVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehicle.Location = new System.Drawing.Point(0, 145);
            this.pnlVehicle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlVehicle.Name = "pnlVehicle";
            this.pnlVehicle.Size = new System.Drawing.Size(280, 235);
            this.pnlVehicle.TabIndex = 2;
            // 
            // lblVehicleName
            // 
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblVehicleName.ForeColor = System.Drawing.Color.Black;
            this.lblVehicleName.Location = new System.Drawing.Point(15, 15);
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.Size = new System.Drawing.Size(87, 32);
            this.lblVehicleName.TabIndex = 0;
            this.lblVehicleName.Text = "Tên xe";
            // 
            // lblVehiclePlate
            // 
            this.lblVehiclePlate.AutoSize = true;
            this.lblVehiclePlate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehiclePlate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblVehiclePlate.Location = new System.Drawing.Point(15, 50);
            this.lblVehiclePlate.Name = "lblVehiclePlate";
            this.lblVehiclePlate.Size = new System.Drawing.Size(74, 23);
            this.lblVehiclePlate.TabIndex = 1;
            this.lblVehiclePlate.Text = "Biển số: ";
            // 
            // lblVehicleColor
            // 
            this.lblVehicleColor.AutoSize = true;
            this.lblVehicleColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblVehicleColor.Location = new System.Drawing.Point(15, 80);
            this.lblVehicleColor.Name = "lblVehicleColor";
            this.lblVehicleColor.Size = new System.Drawing.Size(82, 23);
            this.lblVehicleColor.TabIndex = 2;
            this.lblVehicleColor.Text = "Màu sắc: ";
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblVehicleType.Location = new System.Drawing.Point(15, 110);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(72, 23);
            this.lblVehicleType.TabIndex = 3;
            this.lblVehicleType.Text = "Loại xe: ";
            // 
            // ucDriverStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(280, 300);
            this.Name = "ucDriverStatus";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(300, 400);
            this.tlpMain.ResumeLayout(false);
            this.pnlToggle.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlIncomeCard.ResumeLayout(false);
            this.pnlIncomeCard.PerformLayout();
            this.pnlWalletCard.ResumeLayout(false);
            this.pnlWalletCard.PerformLayout();
            this.pnlVehicle.ResumeLayout(false);
            this.pnlVehicle.PerformLayout();
            this.ResumeLayout(false);

        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlToggle;
        private System.Windows.Forms.CheckBox chkToggleStatus;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Panel pnlIncomeCard;
        private System.Windows.Forms.Label lblIncomeTitle;
        private System.Windows.Forms.Label lblTodayIncome;
        private System.Windows.Forms.Panel pnlWalletCard;
        private System.Windows.Forms.Label lblWalletTitle;
        private System.Windows.Forms.Label lblWalletBalance;
        private System.Windows.Forms.Panel pnlVehicle;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.Label lblVehiclePlate;
        private System.Windows.Forms.Label lblVehicleColor;
        private System.Windows.Forms.Label lblVehicleType;
        #endregion
    }
}