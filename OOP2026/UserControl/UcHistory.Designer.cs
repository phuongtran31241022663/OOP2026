namespace OOP2026
{
    partial class ucHistory
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpTrips = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // ========== tlpMain ==========
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.flpTrips, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(376, 476);
            this.tlpMain.TabIndex = 0;

            // ========== lblTitle ==========
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = Typography.Font14Bold;
            this.lblTitle.ForeColor = OOP2026.Colors.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(376, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "?? L?ch s? chuy?n di";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ========== flpTrips ==========
            this.flpTrips.AutoScroll = true;
            this.flpTrips.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTrips.WrapContents = false;
            this.flpTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTrips.Margin = new System.Windows.Forms.Padding(0);
            this.flpTrips.Padding = new System.Windows.Forms.Padding(0);
            this.flpTrips.Name = "flpTrips";

            // ========== ucHistory ==========
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.tlpMain);
            this.Font = Typography.Font9Regular;
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "ucHistory";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(400, 500);

            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Controls declaration
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpTrips;
        #endregion
    }
}
