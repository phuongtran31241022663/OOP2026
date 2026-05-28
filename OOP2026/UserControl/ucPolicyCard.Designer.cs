namespace OOP2026
{
    partial class ucPolicyCard
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.lblKmPrice = new System.Windows.Forms.Label();
            this.lblCommission = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = OOP2026.Typography.Font14Bold;
            this.lblTitle.ForeColor = OOP2026.Colors.Black;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "Lo?i xe";

            // lblBasePrice
            this.lblBasePrice.Font = OOP2026.Typography.Font10Regular;
            this.lblBasePrice.ForeColor = OOP2026.Colors.Black;
            this.lblBasePrice.Location = new System.Drawing.Point(12, 50);
            this.lblBasePrice.AutoSize = true;
            this.lblBasePrice.Text = "?? M? c?a: 0d";

            // lblKmPrice
            this.lblKmPrice.Font = OOP2026.Typography.Font10Regular;
            this.lblKmPrice.ForeColor = OOP2026.Colors.Black;
            this.lblKmPrice.Location = new System.Drawing.Point(12, 75);
            this.lblKmPrice.AutoSize = true;
            this.lblKmPrice.Text = "?? Giá/km: 0d";

            // lblCommission
            this.lblCommission.Font = OOP2026.Typography.Font10Regular;
            this.lblCommission.ForeColor = OOP2026.Colors.Black;
            this.lblCommission.Location = new System.Drawing.Point(12, 100);
            this.lblCommission.AutoSize = true;
            this.lblCommission.Text = "?? Hoa h?ng: 0%";

            // ucPolicyCard
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Size = new System.Drawing.Size(280, 130);
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblBasePrice);
            this.Controls.Add(this.lblKmPrice);
            this.Controls.Add(this.lblCommission);
            this.Name = "ucPolicyCard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.Label lblKmPrice;
        private System.Windows.Forms.Label lblCommission;
    }
}
