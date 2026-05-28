namespace OOP2026
{
    partial class FrmPassenger
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
            this.home = new OOP2026.ucPassengerHome();
            this.map = new OOP2026.ucMap();
            this.splMain = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.SuspendLayout();
            this.SuspendLayout();

            // 
            // passengerHome
            // 
            this.home.Dock = System.Windows.Forms.DockStyle.Fill;
            this.home.Location = new System.Drawing.Point(0, 0);
            this.home.Name = "passengerHome";
            this.home.Size = new System.Drawing.Size(420, 800);
            this.home.TabIndex = 0;

            // 
            // ucMap
            // 
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Name = "ucMap";
            this.map.Size = new System.Drawing.Size(776, 800);
            this.map.TabIndex = 0;

            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Size = new System.Drawing.Size(1200, 800);

            // T? l? phân chia: Panel1 bên trái r?ng 420px (35%), còn l?i Panel2 bên ph?i r?ng 780px (65%)
            this.splMain.SplitterDistance = 420;
            this.splMain.TabIndex = 0;

            // Panel1 (Bên trái): G?n Menu ch?c nang passengerHome
            this.splMain.Panel1.Controls.Add(this.home);
            this.splMain.Panel1.Name = "splMain_Panel1";

            // Panel2 (Bên ph?i): G?n B?n d? l?n ucMap
            this.splMain.Panel2.Controls.Add(this.map);
            this.splMain.Panel2.Name = "splMain_Panel2";

            // 
            // FrmPassenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.splMain);
            this.Name = "FrmPassenger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OOP2026 - Hành khách";
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splMain;

        // 2. Khai báo bi?n thành viên cho c? 2 Usr Control
        private OOP2026.ucPassengerHome home;
        private OOP2026.ucMap map;
    }
}
