namespace OOP2026
{
    partial class FrmDriver
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
            this.home = new OOP2026.ucDriverHome();
            this.map = new OOP2026.ucMap();
            this.spl = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.spl)).BeginInit();
            this.spl.SuspendLayout();
            this.SuspendLayout();

            this.home.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            // ------------------------------------------------------------------

            // 
            // spl
            // 
            this.spl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl.Panel1.Controls.Add(this.home); // Nên thêm 'this.' cho d?ng b?
            this.spl.Panel2.Controls.Add(this.map);      // Nên thêm 'this.' cho d?ng b?
            this.spl.Location = new System.Drawing.Point(0, 0);
            this.spl.Name = "spl";
            this.spl.Size = new System.Drawing.Size(1200, 800);
            this.spl.SplitterDistance = 400; // Panel 1 r?ng 400px, Panel 2 r?ng 800px
            this.spl.TabIndex = 0;
            // 
            // FrmDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(233, 244, 250);
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.spl);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmDriver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP2026 - Tài x?";
            ((System.ComponentModel.ISupportInitialize)(this.spl)).EndInit();
            this.spl.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private SplitContainer spl;
        private ucMap map;
        private ucDriverHome home;
    }
}
