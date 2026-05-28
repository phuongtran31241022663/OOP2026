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

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlStars = new System.Windows.Forms.Panel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();

            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.pnlStars, 0, 1);
            this.tlpMain.Controls.Add(this.txtComment, 0, 2);
            this.tlpMain.Controls.Add(this.btnSubmit, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(16, 16);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.Size = new System.Drawing.Size(348, 252);
            this.tlpMain.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = OOP2026.Typography.Font14Bold; // Gi?m nh? t? l? ch? cho cân d?i v?i form nh?
            this.lblTitle.ForeColor = OOP2026.Colors.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(348, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ðánh giá chuy?n di";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Can gi?a tiêu d? tang tính cân x?ng

            // 
            // pnlStars
            // 
            this.pnlStars.BackColor = OOP2026.Colors.White;
            this.pnlStars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStars.Location = new System.Drawing.Point(0, 35);
            this.pnlStars.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlStars.Name = "pnlStars";
            this.pnlStars.Size = new System.Drawing.Size(348, 46);
            this.pnlStars.TabIndex = 1;

            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Font = OOP2026.Typography.Font10Regular;
            this.txtComment.Location = new System.Drawing.Point(4, 89);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 8);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            // --- TÍNH NANG ÐÁNG GIÁ: G?i ý m? vi?t nh?n xét không lo t?n di?n tích nhãn label ---
            this.txtComment.Text = "Chia s? tr?i nghi?m c?a b?n v? chuy?n di này...";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(340, 100);
            this.txtComment.TabIndex = 2;

            // 
            // btnSubmit (Chuy?n sang ch? d? Full-width tr?i r?ng chân phuong)
            // 
            this.btnSubmit.BackColor = OOP2026.Colors.Green;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = OOP2026.Typography.Font10Bold;
            this.btnSubmit.ForeColor = OOP2026.Colors.White;
            this.btnSubmit.Location = new System.Drawing.Point(4, 201);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(340, 47);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "G?i dánh giá";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // 
            // ucReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(320, 260);
            this.Name = "ucReview";
            this.Padding = new System.Windows.Forms.Padding(16); // Padding r?ng rãi, thoáng m?t
            this.Size = new System.Drawing.Size(380, 284);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStars;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnSubmit;
    }
}
