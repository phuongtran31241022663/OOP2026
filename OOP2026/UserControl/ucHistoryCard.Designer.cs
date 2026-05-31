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

        #region Component Designer generated code

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

            // ── tlpMain ─────────────────────────────────────────────
            this.tlpMain.AutoSize = true;
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
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpMain.Size = new System.Drawing.Size(330, 85);
            this.tlpMain.TabIndex = 0;

            // ── tlpHeader ───────────────────────────────────────────
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpHeader.Controls.Add(this.lblDate, 0, 0);
            this.tlpHeader.Controls.Add(this.lblRoute, 0, 1);
            this.tlpHeader.Controls.Add(this.lblStatus, 0, 2);
            this.tlpHeader.Controls.Add(this.lblFare, 1, 1);
            this.tlpHeader.Controls.Add(this.btnReview, 1, 2);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.tlpHeader.RowCount = 3;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpHeader.Size = new System.Drawing.Size(330, 85);
            this.tlpHeader.TabIndex = 0;

            // ── lblDate ────────────────────────────────────────────
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblDate.Location = new System.Drawing.Point(11, 8);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(220, 20);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "28/05/2026 14:30";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblRoute ────────────────────────────────────────────
            this.lblRoute.AutoSize = true;
            this.lblRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoute.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoute.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblRoute.Location = new System.Drawing.Point(11, 28);
            this.lblRoute.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(220, 23);
            this.lblRoute.Text = "Điểm đón • Điểm đến";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblStatus ──────────────────────────────────────────
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblStatus.Location = new System.Drawing.Point(11, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(220, 34);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "✓ Đã hoàn thành";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── lblFare (Giá tiền màu xanh lá) ─────────────────────
            this.lblFare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFare.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFare.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblFare.Location = new System.Drawing.Point(234, 28);
            this.lblFare.Name = "lblFare";
            this.lblFare.Size = new System.Drawing.Size(88, 27);
            this.lblFare.Text = "45.000đ";
            this.lblFare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ── btnReview (Nền vàng nhạt, chữ vàng đậm) ───────────
            this.btnReview.BackColor = System.Drawing.Color.FromArgb(254, 252, 232); // Vàng nhạt
            this.btnReview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(234, 225, 184); // Viền vàng nhạt
            this.btnReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReview.ForeColor = System.Drawing.Color.FromArgb(184, 134, 11); // Vàng đậm
            this.btnReview.Location = new System.Drawing.Point(234, 57);
            this.btnReview.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(88, 30);
            this.btnReview.TabIndex = 4;
            this.btnReview.Text = "Đánh giá";
            this.btnReview.UseVisualStyleBackColor = false;
            this.btnReview.Click += new System.EventHandler(this.BtnReview_Click);

            // ── pnlReview ──────────────────────────────────────────
            this.pnlReview.AutoSize = true;
            this.pnlReview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlReview.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReview.Location = new System.Drawing.Point(0, 85);
            this.pnlReview.Margin = new System.Windows.Forms.Padding(0);
            this.pnlReview.Name = "pnlReview";
            this.pnlReview.Size = new System.Drawing.Size(330, 0);
            this.pnlReview.TabIndex = 5;
            this.pnlReview.Visible = false;

            // ── ucHistoryCard ──────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.Name = "ucHistoryCard";
            this.Size = new System.Drawing.Size(330, 85);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Panel pnlReview;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
    }
}