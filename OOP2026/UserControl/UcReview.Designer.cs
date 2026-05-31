namespace OOP2026
{
    partial class ucReview
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlStars = new System.Windows.Forms.Panel();
            this.flpStars = new System.Windows.Forms.FlowLayoutPanel();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.lblStar3 = new System.Windows.Forms.Label();
            this.lblStar4 = new System.Windows.Forms.Label();
            this.lblStar5 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();

            this.tlpMain.SuspendLayout();
            this.pnlStars.SuspendLayout();
            this.flpStars.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();

            // ── tlpMain ─────────────────────────────────────────────
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.pnlStars, 0, 1);
            this.tlpMain.Controls.Add(this.txtComment, 0, 2);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(20, 20);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));  // Tăng nhẹ để chứa sao to
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpMain.Size = new System.Drawing.Size(340, 244);
            this.tlpMain.TabIndex = 0;

            // ── lblTitle ────────────────────────────────────────────
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(340, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đánh giá chuyến đi";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── pnlStars ────────────────────────────────────────────
            this.pnlStars.BackColor = System.Drawing.Color.White;
            this.pnlStars.Controls.Add(this.flpStars);
            this.pnlStars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStars.Location = new System.Drawing.Point(0, 35);
            this.pnlStars.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlStars.Name = "pnlStars";
            this.pnlStars.Size = new System.Drawing.Size(340, 42);
            this.pnlStars.TabIndex = 1;

            // ── flpStars (FlowLayoutPanel chứa các sao) ─────────────
            this.flpStars.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpStars.AutoSize = true;
            this.flpStars.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpStars.Controls.Add(this.lblStar1);
            this.flpStars.Controls.Add(this.lblStar2);
            this.flpStars.Controls.Add(this.lblStar3);
            this.flpStars.Controls.Add(this.lblStar4);
            this.flpStars.Controls.Add(this.lblStar5);
            this.flpStars.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpStars.Location = new System.Drawing.Point(55, 4); // Căn giữa thủ công (tính toán sau)
            this.flpStars.Margin = new System.Windows.Forms.Padding(0);
            this.flpStars.Name = "flpStars";
            this.flpStars.Size = new System.Drawing.Size(230, 34);
            this.flpStars.TabIndex = 0;
            this.flpStars.WrapContents = false;

            // ── lblStar1 ────────────────────────────────────────────
            this.lblStar1.AutoSize = true;
            this.lblStar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStar1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular);
            this.lblStar1.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21); // Vàng sao
            this.lblStar1.Location = new System.Drawing.Point(3, 0);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(40, 32);
            this.lblStar1.TabIndex = 0;
            this.lblStar1.Text = "★";
            this.lblStar1.Click += new System.EventHandler(this.Star_Click);
            this.lblStar1.MouseEnter += new System.EventHandler(this.Star_MouseEnter);
            this.lblStar1.MouseLeave += new System.EventHandler(this.Star_MouseLeave);

            // ── lblStar2 ────────────────────────────────────────────
            this.lblStar2.AutoSize = true;
            this.lblStar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStar2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblStar2.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21);
            this.lblStar2.Location = new System.Drawing.Point(49, 0);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(40, 32);
            this.lblStar2.TabIndex = 1;
            this.lblStar2.Text = "★";
            this.lblStar2.Click += new System.EventHandler(this.Star_Click);
            this.lblStar2.MouseEnter += new System.EventHandler(this.Star_MouseEnter);
            this.lblStar2.MouseLeave += new System.EventHandler(this.Star_MouseLeave);

            // ── lblStar3 ────────────────────────────────────────────
            this.lblStar3.AutoSize = true;
            this.lblStar3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStar3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblStar3.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21);
            this.lblStar3.Location = new System.Drawing.Point(95, 0);
            this.lblStar3.Name = "lblStar3";
            this.lblStar3.Size = new System.Drawing.Size(40, 32);
            this.lblStar3.TabIndex = 2;
            this.lblStar3.Text = "★";
            this.lblStar3.Click += new System.EventHandler(this.Star_Click);
            this.lblStar3.MouseEnter += new System.EventHandler(this.Star_MouseEnter);
            this.lblStar3.MouseLeave += new System.EventHandler(this.Star_MouseLeave);

            // ── lblStar4 ────────────────────────────────────────────
            this.lblStar4.AutoSize = true;
            this.lblStar4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStar4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblStar4.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21);
            this.lblStar4.Location = new System.Drawing.Point(141, 0);
            this.lblStar4.Name = "lblStar4";
            this.lblStar4.Size = new System.Drawing.Size(40, 32);
            this.lblStar4.TabIndex = 3;
            this.lblStar4.Text = "★";
            this.lblStar4.Click += new System.EventHandler(this.Star_Click);
            this.lblStar4.MouseEnter += new System.EventHandler(this.Star_MouseEnter);
            this.lblStar4.MouseLeave += new System.EventHandler(this.Star_MouseLeave);

            // ── lblStar5 ────────────────────────────────────────────
            this.lblStar5.AutoSize = true;
            this.lblStar5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStar5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblStar5.ForeColor = System.Drawing.Color.FromArgb(250, 204, 21);
            this.lblStar5.Location = new System.Drawing.Point(187, 0);
            this.lblStar5.Name = "lblStar5";
            this.lblStar5.Size = new System.Drawing.Size(40, 32);
            this.lblStar5.TabIndex = 4;
            this.lblStar5.Text = "★";
            this.lblStar5.Click += new System.EventHandler(this.Star_Click);
            this.lblStar5.MouseEnter += new System.EventHandler(this.Star_MouseEnter);
            this.lblStar5.MouseLeave += new System.EventHandler(this.Star_MouseLeave);

            // ── txtComment ──────────────────────────────────────────
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtComment.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.txtComment.Location = new System.Drawing.Point(0, 85);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(340, 99);
            this.txtComment.TabIndex = 2;

            // ── tlpButtons (Chứa 2 nút Bỏ qua và Gửi) ──────────────
            this.tlpButtons.ColumnCount = 2;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tlpButtons.Controls.Add(this.btnSkip, 0, 0);
            this.tlpButtons.Controls.Add(this.btnSubmit, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 196);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(340, 48);
            this.tlpButtons.TabIndex = 4;

            // ── btnSkip (Nút Bỏ qua) ────────────────────────────────
            this.btnSkip.BackColor = System.Drawing.Color.FromArgb(241, 245, 249); // Xám nhạt
            this.btnSkip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSkip.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnSkip.FlatAppearance.BorderSize = 1;
            this.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnSkip.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnSkip.Location = new System.Drawing.Point(3, 3);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(123, 42);
            this.btnSkip.TabIndex = 0;
            this.btnSkip.Text = "Bỏ qua";
            this.btnSkip.UseVisualStyleBackColor = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);

            // ── btnSubmit (Nút Gửi đánh giá – vàng nhạt) ───────────
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(254, 252, 232); // Vàng nhạt
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(234, 225, 184);
            this.btnSubmit.FlatAppearance.BorderSize = 1;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.FromArgb(184, 134, 11); // Vàng đậm
            this.btnSubmit.Location = new System.Drawing.Point(132, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(205, 42);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Gửi đánh giá";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // ── ucReview ────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(320, 280);
            this.Name = "ucReview";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(380, 284);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pnlStars.ResumeLayout(false);
            this.pnlStars.PerformLayout();
            this.flpStars.ResumeLayout(false);
            this.flpStars.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStars;
        private System.Windows.Forms.FlowLayoutPanel flpStars;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblStar3;
        private System.Windows.Forms.Label lblStar4;
        private System.Windows.Forms.Label lblStar5;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnSubmit;
    }
}