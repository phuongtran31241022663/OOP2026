namespace OOP2026
{
    partial class ucUserCount
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
            this.flpStats = new System.Windows.Forms.FlowLayoutPanel();
            this.statTotal = new OOP2026.ucStatCard();
            this.statDrivers = new OOP2026.ucStatCard();
            this.statPassengers = new OOP2026.ucStatCard();
            this.statTrips = new OOP2026.ucStatCard();

            this.flpStats.SuspendLayout();
            this.SuspendLayout();

            // ── flpStats ─────────────────────────────────────
            this.flpStats.AutoScroll = false;
            this.flpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStats.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpStats.Name = "flpStats";
            this.flpStats.Padding = new System.Windows.Forms.Padding(4);
            this.flpStats.WrapContents = true;
            this.flpStats.Controls.Add(this.statTotal);
            this.flpStats.Controls.Add(this.statDrivers);
            this.flpStats.Controls.Add(this.statPassengers);
            this.flpStats.Controls.Add(this.statTrips);

            // ── 4 ucStatCard ─────────────────────────────────
            // Mỗi card được init với placeholder; UpdateLabels() ghi đè khi load xong
            this.statTotal.Size = new System.Drawing.Size(200, 90);
            this.statTotal.Margin = new System.Windows.Forms.Padding(4);
            this.statTotal.TabIndex = 0;

            this.statDrivers.Size = new System.Drawing.Size(200, 90);
            this.statDrivers.Margin = new System.Windows.Forms.Padding(4);
            this.statDrivers.TabIndex = 1;

            this.statPassengers.Size = new System.Drawing.Size(200, 90);
            this.statPassengers.Margin = new System.Windows.Forms.Padding(4);
            this.statPassengers.TabIndex = 2;

            this.statTrips.Size = new System.Drawing.Size(200, 90);
            this.statTrips.Margin = new System.Windows.Forms.Padding(4);
            this.statTrips.TabIndex = 3;

            // ── ucUserCount ───────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Controls.Add(this.flpStats);
            this.MinimumSize = new System.Drawing.Size(440, 110);
            this.Name = "ucUserCount";
            this.Size = new System.Drawing.Size(880, 110);

            this.flpStats.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel flpStats;
        private OOP2026.ucStatCard statTotal;
        private OOP2026.ucStatCard statDrivers;
        private OOP2026.ucStatCard statPassengers;
        private OOP2026.ucStatCard statTrips;
    }
}