namespace OOP2026
{
    partial class ucWallet
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
            this.pnlBalanceCard = new System.Windows.Forms.TableLayoutPanel();
            this.lblBalanceTitle = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblIncomeTitle = new System.Windows.Forms.Label();
            this.lblIncomeValue = new System.Windows.Forms.Label();
            this.lblTripsTitle = new System.Windows.Forms.Label();
            this.lblTripsValue = new System.Windows.Forms.Label();
            this.grpTopup = new System.Windows.Forms.GroupBox();
            this.tlpTopupInternal = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectAmount = new System.Windows.Forms.Label();
            this.tlpQuickAmounts = new System.Windows.Forms.TableLayoutPanel();
            this.btn50k = new System.Windows.Forms.Button();
            this.btn100k = new System.Windows.Forms.Button();
            this.btn200k = new System.Windows.Forms.Button();
            this.txtCustomAmount = new System.Windows.Forms.TextBox();
            this.btnTopup = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlBalanceCard.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.grpTopup.SuspendLayout();
            this.tlpTopupInternal.SuspendLayout();
            this.tlpQuickAmounts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlBalanceCard, 0, 0);
            this.tlpMain.Controls.Add(this.pnlStats, 0, 1);
            this.tlpMain.Controls.Add(this.grpTopup, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(20, 20);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(340, 560);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlBalanceCard
            // 
            this.pnlBalanceCard.BackColor = OOP2026.Colors.Orange;
            this.pnlBalanceCard.ColumnCount = 1;
            this.pnlBalanceCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlBalanceCard.Controls.Add(this.lblBalanceTitle, 0, 0);
            this.pnlBalanceCard.Controls.Add(this.lblBalance, 0, 1);
            this.pnlBalanceCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBalanceCard.Location = new System.Drawing.Point(3, 3);
            this.pnlBalanceCard.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlBalanceCard.Name = "pnlBalanceCard";
            this.pnlBalanceCard.Padding = new System.Windows.Forms.Padding(16);
            this.pnlBalanceCard.RowCount = 2;
            this.pnlBalanceCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlBalanceCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlBalanceCard.Size = new System.Drawing.Size(334, 97);
            this.pnlBalanceCard.TabIndex = 0;
            // 
            // lblBalanceTitle
            // 
            this.lblBalanceTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.Font = OOP2026.Typography.Font10Regular;
            this.lblBalanceTitle.ForeColor = System.Drawing.Color.White;
            this.lblBalanceTitle.Location = new System.Drawing.Point(19, 23);
            this.lblBalanceTitle.Name = "lblBalanceTitle";
            this.lblBalanceTitle.Size = new System.Drawing.Size(71, 23);
            this.lblBalanceTitle.TabIndex = 0;
            this.lblBalanceTitle.Text = "Số dư ví";
            // 
            // lblBalance
            // 
            this.lblBalance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = OOP2026.Typography.Font14Bold;
            this.lblBalance.ForeColor = System.Drawing.Color.White;
            this.lblBalance.Location = new System.Drawing.Point(19, 46);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(34, 35);
            this.lblBalance.TabIndex = 1;
            this.lblBalance.Text = "d";
            // 
            // pnlStats
            // 
            this.pnlStats.ColumnCount = 2;
            this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlStats.Controls.Add(this.lblIncomeTitle, 0, 0);
            this.pnlStats.Controls.Add(this.lblIncomeValue, 0, 1);
            this.pnlStats.Controls.Add(this.lblTripsTitle, 1, 0);
            this.pnlStats.Controls.Add(this.lblTripsValue, 1, 1);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStats.Location = new System.Drawing.Point(3, 113);
            this.pnlStats.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlStats.RowCount = 2;
            this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlStats.Size = new System.Drawing.Size(334, 67);
            this.pnlStats.TabIndex = 2;
            // 
            // lblIncomeTitle
            // 
            this.lblIncomeTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIncomeTitle.AutoSize = true;
            this.lblIncomeTitle.Font = OOP2026.Typography.Font9Regular;
            this.lblIncomeTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblIncomeTitle.Location = new System.Drawing.Point(13, 5);
            this.lblIncomeTitle.Name = "lblIncomeTitle";
            this.lblIncomeTitle.Size = new System.Drawing.Size(125, 19);
            this.lblIncomeTitle.TabIndex = 0;
            this.lblIncomeTitle.Text = "Thu nhập hôm nay";
            // 
            // lblIncomeValue
            // 
            this.lblIncomeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIncomeValue.AutoSize = true;
            this.lblIncomeValue.Font = OOP2026.Typography.Font14Bold;
            this.lblIncomeValue.ForeColor = System.Drawing.Color.Black;
            this.lblIncomeValue.Location = new System.Drawing.Point(13, 34);
            this.lblIncomeValue.Name = "lblIncomeValue";
            this.lblIncomeValue.Size = new System.Drawing.Size(37, 28);
            this.lblIncomeValue.TabIndex = 1;
            this.lblIncomeValue.Text = "0d";
            // 
            // lblTripsTitle
            // 
            this.lblTripsTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTripsTitle.AutoSize = true;
            this.lblTripsTitle.Font = OOP2026.Typography.Font9Regular;
            this.lblTripsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblTripsTitle.Location = new System.Drawing.Point(170, 5);
            this.lblTripsTitle.Name = "lblTripsTitle";
            this.lblTripsTitle.Size = new System.Drawing.Size(88, 19);
            this.lblTripsTitle.TabIndex = 2;
            this.lblTripsTitle.Text = "Tổng chuyến";
            // 
            // lblTripsValue
            // 
            this.lblTripsValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTripsValue.AutoSize = true;
            this.lblTripsValue.Font = OOP2026.Typography.Font14Bold;
            this.lblTripsValue.ForeColor = System.Drawing.Color.Black;
            this.lblTripsValue.Location = new System.Drawing.Point(170, 34);
            this.lblTripsValue.Name = "lblTripsValue";
            this.lblTripsValue.Size = new System.Drawing.Size(24, 28);
            this.lblTripsValue.TabIndex = 3;
            this.lblTripsValue.Text = "0";
            // 
            // grpTopup
            // 
            this.grpTopup.Controls.Add(this.tlpTopupInternal);
            this.grpTopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTopup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTopup.Location = new System.Drawing.Point(3, 193);
            this.grpTopup.Name = "grpTopup";
            this.grpTopup.Padding = new System.Windows.Forms.Padding(12, 24, 12, 12);
            this.grpTopup.Size = new System.Drawing.Size(334, 364);
            this.grpTopup.TabIndex = 1;
            this.grpTopup.TabStop = false;
            this.grpTopup.Text = "Nạp tiền vào ví";
            // 
            // tlpTopupInternal
            // 
            this.tlpTopupInternal.ColumnCount = 1;
            this.tlpTopupInternal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopupInternal.Controls.Add(this.lblSelectAmount, 0, 0);
            this.tlpTopupInternal.Controls.Add(this.tlpQuickAmounts, 0, 1);
            this.tlpTopupInternal.Controls.Add(this.txtCustomAmount, 0, 2);
            this.tlpTopupInternal.Controls.Add(this.btnTopup, 0, 3);
            this.tlpTopupInternal.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTopupInternal.Location = new System.Drawing.Point(12, 47);
            this.tlpTopupInternal.Name = "tlpTopupInternal";
            this.tlpTopupInternal.RowCount = 4;
            this.tlpTopupInternal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpTopupInternal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTopupInternal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTopupInternal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTopupInternal.Size = new System.Drawing.Size(310, 180);
            this.tlpTopupInternal.TabIndex = 0;
            // 
            // lblSelectAmount
            // 
            this.lblSelectAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectAmount.AutoSize = true;
            this.lblSelectAmount.Location = new System.Drawing.Point(3, 7);
            this.lblSelectAmount.Name = "lblSelectAmount";
            this.lblSelectAmount.Size = new System.Drawing.Size(201, 23);
            this.lblSelectAmount.TabIndex = 0;
            this.lblSelectAmount.Text = "Chọn hoặc nhập số tiền:";
            // 
            // tlpQuickAmounts
            // 
            this.tlpQuickAmounts.ColumnCount = 3;
            this.tlpQuickAmounts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpQuickAmounts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpQuickAmounts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpQuickAmounts.Controls.Add(this.btn50k, 0, 0);
            this.tlpQuickAmounts.Controls.Add(this.btn100k, 1, 0);
            this.tlpQuickAmounts.Controls.Add(this.btn200k, 2, 0);
            this.tlpQuickAmounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpQuickAmounts.Location = new System.Drawing.Point(0, 30);
            this.tlpQuickAmounts.Margin = new System.Windows.Forms.Padding(0);
            this.tlpQuickAmounts.Name = "tlpQuickAmounts";
            this.tlpQuickAmounts.RowCount = 1;
            this.tlpQuickAmounts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpQuickAmounts.Size = new System.Drawing.Size(310, 50);
            this.tlpQuickAmounts.TabIndex = 1;
            // 
            // btn50k
            // 
            this.btn50k.BackColor = OOP2026.Colors.White;
            this.btn50k.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn50k.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 225, 230);
            this.btn50k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn50k.Font = OOP2026.Typography.Font10Regular;
            this.btn50k.ForeColor = System.Drawing.Color.Black;
            this.btn50k.Location = new System.Drawing.Point(3, 3);
            this.btn50k.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.btn50k.Name = "btn50k";
            this.btn50k.Size = new System.Drawing.Size(95, 44);
            this.btn50k.TabIndex = 0;
            this.btn50k.Text = "50.000d";
            this.btn50k.UseVisualStyleBackColor = false;
            this.btn50k.Click += new System.EventHandler(this.btn50k_Click);
            // 
            // btn100k
            // 
            this.btn100k.BackColor = OOP2026.Colors.White;
            this.btn100k.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn100k.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 225, 230);
            this.btn100k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn100k.Font = OOP2026.Typography.Font10Regular;
            this.btn100k.ForeColor = System.Drawing.Color.Black;
            this.btn100k.Location = new System.Drawing.Point(106, 3);
            this.btn100k.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.btn100k.Name = "btn100k";
            this.btn100k.Size = new System.Drawing.Size(95, 44);
            this.btn100k.TabIndex = 1;
            this.btn100k.Text = "100.000d";
            this.btn100k.UseVisualStyleBackColor = false;
            this.btn100k.Click += new System.EventHandler(this.btn100k_Click);
            // 
            // btn200k
            // 
            this.btn200k.BackColor = OOP2026.Colors.White;
            this.btn200k.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn200k.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 225, 230);
            this.btn200k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn200k.Font = OOP2026.Typography.Font10Regular;
            this.btn200k.ForeColor = System.Drawing.Color.Black;
            this.btn200k.Location = new System.Drawing.Point(211, 3);
            this.btn200k.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.btn200k.Name = "btn200k";
            this.btn200k.Size = new System.Drawing.Size(96, 44);
            this.btn200k.TabIndex = 2;
            this.btn200k.Text = "200.000d";
            this.btn200k.UseVisualStyleBackColor = false;
            this.btn200k.Click += new System.EventHandler(this.btn200k_Click);
            // 
            // txtCustomAmount
            // 
            this.txtCustomAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomAmount.Font = OOP2026.Typography.Font10Regular;
            this.txtCustomAmount.Location = new System.Drawing.Point(3, 85);
            this.txtCustomAmount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtCustomAmount.Name = "txtCustomAmount";
            this.txtCustomAmount.Size = new System.Drawing.Size(304, 30);
            this.txtCustomAmount.TabIndex = 2;
            this.txtCustomAmount.TextChanged += new System.EventHandler(this.txtCustomAmount_TextChanged);
            this.txtCustomAmount.Enter += new System.EventHandler(this.txtCustomAmount_Enter);
            this.txtCustomAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomAmount_KeyPress);
            this.txtCustomAmount.Leave += new System.EventHandler(this.txtCustomAmount_Leave);
            // 
            // btnTopup
            // 
            this.btnTopup.BackColor = OOP2026.Colors.Orange;
            this.btnTopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTopup.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTopup.FlatAppearance.BorderSize = 0;
            this.btnTopup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopup.Font = OOP2026.Typography.Font10Bold;
            this.btnTopup.ForeColor = System.Drawing.Color.White;
            this.btnTopup.Location = new System.Drawing.Point(3, 135);
            this.btnTopup.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnTopup.Name = "btnTopup";
            this.btnTopup.Size = new System.Drawing.Size(304, 42);
            this.btnTopup.TabIndex = 3;
            this.btnTopup.Text = "Nạp tiền";
            this.btnTopup.UseVisualStyleBackColor = false;
            this.btnTopup.Click += new System.EventHandler(this.btnTopup_Click);
            // 
            // ucWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.tlpMain);
            this.Font = OOP2026.Typography.Font10Regular;
            this.Name = "ucWallet";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(380, 600);
            this.tlpMain.ResumeLayout(false);
            this.pnlBalanceCard.ResumeLayout(false);
            this.pnlBalanceCard.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.grpTopup.ResumeLayout(false);
            this.tlpTopupInternal.ResumeLayout(false);
            this.tlpTopupInternal.PerformLayout();
            this.tlpQuickAmounts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel pnlBalanceCard;
        private System.Windows.Forms.Label lblBalanceTitle;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TableLayoutPanel pnlStats;
        private System.Windows.Forms.Label lblIncomeTitle;
        private System.Windows.Forms.Label lblIncomeValue;
        private System.Windows.Forms.Label lblTripsTitle;
        private System.Windows.Forms.Label lblTripsValue;
        private System.Windows.Forms.GroupBox grpTopup;
        private System.Windows.Forms.TableLayoutPanel tlpTopupInternal;
        private System.Windows.Forms.Label lblSelectAmount;
        private System.Windows.Forms.TableLayoutPanel tlpQuickAmounts;
        private System.Windows.Forms.Button btn50k;
        private System.Windows.Forms.Button btn100k;
        private System.Windows.Forms.Button btn200k;
        private System.Windows.Forms.TextBox txtCustomAmount;
        private System.Windows.Forms.Button btnTopup;
        #endregion
    }
}