namespace OOP2026
{
    partial class ucHistoryCard
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
            this.lblDate = new System.Windows.Forms.Label();
            this.lblRoute = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblFare = new System.Windows.Forms.Label();
            this.btnReview = new System.Windows.Forms.Button();
            this.pnlReview = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();

            this.tlpMain.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true; // BẬT: Để TableLayoutPanel tự nở theo nội dung
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.tlpMain.Controls.Add(this.pnlReview, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F)); // Header cố định 70px
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // SỬA: Tự động co giãn theo Panel Rev
            this.tlpMain.TabIndex = 0;

            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpHeader.Controls.Add(this.lblDate, 0, 0);
            this.tlpHeader.Controls.Add(this.lblRoute, 0, 1);
            this.tlpHeader.Controls.Add(this.lblStatus, 0, 2);
            this.tlpHeader.Controls.Add(this.lblFare, 1, 1);
            this.tlpHeader.Controls.Add(this.btnReview, 1, 2);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 3;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpHeader.Size = new System.Drawing.Size(330, 70);
            this.tlpHeader.TabIndex = 0;

            // 
            // lblDate
            // 
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Font = Typography.Font9Regular;
            this.lblDate.ForeColor = OOP2026.Colors.Gray;
            this.lblDate.Location = new System.Drawing.Point(3, 0);
            this.lblDate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(221, 20);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "00/00/0000 00:00";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblRoute
            // 
            this.lblRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoute.Font = Typography.Font10Bold;
            this.lblRoute.ForeColor = OOP2026.Colors.Black;
            this.lblRoute.Location = new System.Drawing.Point(3, 20);
            this.lblRoute.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(221, 28);
            this.lblRoute.TabIndex = 1;
            this.lblRoute.Text = "Điểm đón • Điểm đến";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = Typography.Font9Regular;
            this.lblStatus.ForeColor = OOP2026.Colors.Gray;
            this.lblStatus.Location = new System.Drawing.Point(3, 48);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(221, 22);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Trạng thái";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // lblFare
            // 
            this.lblFare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFare.Font = Typography.Font10Bold;
            this.lblFare.ForeColor = OOP2026.Colors.Gray;
            this.lblFare.Location = new System.Drawing.Point(230, 20);
            this.lblFare.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblFare.Name = "lblFare";
            this.lblFare.Size = new System.Drawing.Size(97, 28);
            this.lblFare.TabIndex = 3;
            this.lblFare.Text = "0d";
            this.lblFare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // btnReview
            // 
            this.btnReview.BackColor = OOP2026.Colors.White;
            this.btnReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReview.FlatAppearance.BorderColor = OOP2026.Colors.LightGray;
            this.btnReview.FlatAppearance.BorderSize = 1;
            this.btnReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReview.Font = Typography.Font10Bold;
            this.btnReview.ForeColor = OOP2026.Colors.Orange;
            this.btnReview.Location = new System.Drawing.Point(230, 48);
            this.btnReview.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(97, 22);
            this.btnReview.TabIndex = 4;
            this.btnReview.Text = "Đánh giá";
            this.btnReview.UseVisualStyleBackColor = false;
            this.btnReview.Click += new System.EventHandler(this.BtnReview_Click); // SỬA: Đăng ký Event rõ ràng

            // 
            // pnlReview
            // 
            this.pnlReview.AutoSize = true; // SỬA: Panel tự nở chiều cao dựa theo control ucReview bên trong
            this.pnlReview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlReview.Dock = System.Windows.Forms.DockStyle.Fill; // Đổi sang Fill để ăn theo cấu trúc Row của TableLayoutPanel
            this.pnlReview.Location = new System.Drawing.Point(0, 70);
            this.pnlReview.Margin = new System.Windows.Forms.Padding(0);
            this.pnlReview.Name = "pnlReview";
            this.pnlReview.Size = new System.Drawing.Size(330, 0); // Ban đầu chiều cao bằng 0
            this.pnlReview.TabIndex = 5;
            this.pnlReview.Visible = false;

            // 
            // ucHistoryCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true; // CHỐT HẠ: Ép Card co giãn linh hoạt
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = OOP2026.Colors.White; // Đổi từ LightGreen sang White cho tiệm cận màu UI thực tế
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.Name = "ucHistoryCard";
            this.Size = new System.Drawing.Size(330, 70); // Kích thước mặc định chỉ bằng phần Header

            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Controls declaration
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Panel pnlReview;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        #endregion
    }
}
