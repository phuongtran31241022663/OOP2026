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
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.home = new OOP2026.ucPassengerHome();
            this.map = new OOP2026.ucMap();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout(); // KÍCH HOẠT: Bật luồng bảo vệ vẽ cho Panel 1 (Menu trái)
            this.splMain.Panel2.SuspendLayout(); // KÍCH HOẠT: Bật luồng bảo vệ vẽ cho Panel 2 (Bản đồ phải)
            this.SuspendLayout();

            // ── splMain (Khung chia đôi màn hình tương tác thông minh) ──────
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Size = new System.Drawing.Size(1200, 800);
            this.splMain.SplitterDistance = 380; // SỬA: Đặt khoảng cách 380px (Tỉ lệ vàng cho thanh Dashboard gọi xe)
            this.splMain.SplitterWidth = 5;      // Độ dày vách ngăn mảnh mai, tinh tế
            this.splMain.TabIndex = 0;

            // Cấu hình giới hạn ranh giới co dãn thông minh chống vỡ giao diện
            this.splMain.Panel1MinSize = 300;   // Không cho phép thu nhỏ thanh Menu trái dưới 300px
            this.splMain.Panel2MinSize = 500;   // Bảo vệ không gian hiển thị cho Bản đồ phải luôn lớn hơn 500px

            // Nạp thành phần vào Panel 1 (Cột bên trái)
            this.splMain.Panel1.Controls.Add(this.home);
            // Nạp thành phần vào Panel 2 (Cột bên phải chiếm diện tích lớn)
            this.splMain.Panel2.Controls.Add(this.map);

            // ── home (Giao diện bảng điều khiển & chức năng Hành khách) ──
            this.home.Dock = System.Windows.Forms.DockStyle.Fill; // Ép khít chặt lòng Panel 1
            this.home.Location = new System.Drawing.Point(0, 0);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(380, 800);
            this.home.TabIndex = 0;

            // ── map (Giao diện Bản đồ vệ tinh hiển thị vị trí) ──────────
            this.map.Dock = System.Windows.Forms.DockStyle.Fill; // Ép khít chặt lòng Panel 2
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(815, 800);
            this.map.TabIndex = 0;

            // ── FrmPassenger (Cửa sổ biểu mẫu chính) ──────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.splMain);

            // SỬA CHÍ MẠNG: Khóa ranh giới thu nhỏ Form toàn cục bảo vệ cấu trúc app gọi xe chống sập layout
            this.MinimumSize = new System.Drawing.Size(900, 650);

            this.Name = "FrmPassenger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Mở biểu mẫu ngay tâm màn hình
            this.Text = "OOP2026 - Hệ thống Đặt xe Hành khách";

            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.SplitContainer splMain;

        private OOP2026.ucPassengerHome home;
        private OOP2026.ucMap map;
    }
}
