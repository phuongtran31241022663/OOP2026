namespace OOP2026
{
    partial class ucStatCard
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false; // T?t AutoSize d? ki?m soát Dock t?t hon
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(168, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tổng thu nhập";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Font = OOP2026.Typography.Font9Regular;
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128); // Màu xám nh?t hi?n d?i
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = false;
            this.lblValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValue.Location = new System.Drawing.Point(16, 38);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(168, 36);
            this.lblValue.TabIndex = 1;
            this.lblValue.Text = "2,450,000d"; // Gi? ch? th? v?i chu?i ti?n t? dài
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblValue.ForeColor = OOP2026.Colors.Black;
            // 
            // ucStatCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucStatCard";
            this.Padding = new System.Windows.Forms.Padding(16); // T?o kho?ng tr?ng vi?n d?u d?n
            this.Size = new System.Drawing.Size(200, 90);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
    }
}
