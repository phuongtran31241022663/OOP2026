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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.spl = new System.Windows.Forms.SplitContainer();
            this.home = new OOP2026.ucDriverHome();
            this.map = new OOP2026.ucMap();
            ((System.ComponentModel.ISupportInitialize)(this.spl)).BeginInit();
            this.spl.Panel1.SuspendLayout(); // KÍCH HOẠT: Luồng bảo vệ tính toán hình học cho Panel trái
            this.spl.Panel2.SuspendLayout(); // KÍCH HOẠT: Luồng bảo vệ tính toán hình học cho Panel phải
            this.SuspendLayout();

            // 
            // spl (Khung chia đôi màn hình tương tác đa nghiệp vụ của Tài xế)
            // 
            this.spl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl.Location = new System.Drawing.Point(0, 0);
            this.spl.Name = "spl";
            this.spl.Size = new System.Drawing.Size(1200, 800);
            this.spl.SplitterDistance = 360; // SỬA: Đặt 360px là độ rộng tối ưu để hiển thị trọn vẹn ucDriverHome
            this.spl.SplitterWidth = 5;      // Độ dày vách ngăn phẳng mảnh tinh tế
            this.spl.TabIndex = 0;

            // CRITICAL: Khóa ranh giới co dãn thông minh chống sập Layout hệ thống
            this.spl.Panel1MinSize = 320;    // Không cho phép thu nhỏ thanh Dashboard tài xế dưới 320px
            this.spl.Panel2MinSize = 550;    // Đảm bảo không gian Bản đồ nhận cuốc luôn lớn hơn hoặc bằng 550px

            // Khớp nối các thành phần con vào lòng các Panel quản lý
            this.spl.Panel1.Controls.Add(this.home);
            this.spl.Panel2.Controls.Add(this.map);

            // 
            // home (UserControl Bảng điều khiển, trạng thái, ví tiền của Tài xế)
            // 
            this.home.BackColor = System.Drawing.Color.White;
            this.home.Dock = System.Windows.Forms.DockStyle.Fill; // Ép khít chặt ranh giới Panel 1
            this.home.Location = new System.Drawing.Point(0, 0);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(360, 800);
            this.home.TabIndex = 0;

            // 
            // map (UserControl Bản đồ mô phỏng tọa độ xe và khách hàng)
            // 
            this.map.Dock = System.Windows.Forms.DockStyle.Fill; // Ép khít chặt ranh giới Panel 2
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(835, 800);
            this.map.TabIndex = 0;

            // 
            // FrmDriver (Cửa sổ biểu mẫu chính toàn cục của Tài xế)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.spl);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F); // Đồng bộ font hệ thống phẳng thanh lịch

            // SỬA CHÍ MẠNG: Khóa ranh giới thu nhỏ Form toàn cục để chống vỡ ứng dụng trên màn hình bé
            this.MinimumSize = new System.Drawing.Size(950, 680);

            this.Name = "FrmDriver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Mở ứng dụng ngay tâm màn hình
            this.Text = "RideGo — Hệ thống Điều phối & Tiếp nhận Cuốc xe của Đối tác Tài xế";

            this.spl.Panel1.ResumeLayout(false);
            this.spl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spl)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer spl;
        private OOP2026.ucDriverHome home;
        private OOP2026.ucMap map;
    }
}