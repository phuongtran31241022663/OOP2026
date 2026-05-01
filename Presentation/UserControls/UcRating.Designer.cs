namespace Presentation.UserControls
{
    partial class UcRating
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStars;
        private System.Windows.Forms.Button btnStar1;
        private System.Windows.Forms.Button btnStar2;
        private System.Windows.Forms.Button btnStar3;
        private System.Windows.Forms.Button btnStar4;
        private System.Windows.Forms.Button btnStar5;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Button btnSubmit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblComment = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlStars = new System.Windows.Forms.Panel();
            this.btnStar5 = new System.Windows.Forms.Button();
            this.btnStar4 = new System.Windows.Forms.Button();
            this.btnStar3 = new System.Windows.Forms.Button();
            this.btnStar2 = new System.Windows.Forms.Button();
            this.btnStar1 = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pnlStars.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(528, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đánh giá tài xế";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pnlStars.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlStars.Controls.Add(this.btnStar5);
            this.pnlStars.Controls.Add(this.btnStar4);
            this.pnlStars.Controls.Add(this.btnStar3);
            this.pnlStars.Controls.Add(this.btnStar2);
            this.pnlStars.Controls.Add(this.btnStar1);
            this.pnlStars.Location = new System.Drawing.Point(140, 72);
            this.pnlStars.Name = "pnlStars";
            this.pnlStars.Size = new System.Drawing.Size(280, 48);
            this.pnlStars.TabIndex = 1;

            this.btnStar1.FlatAppearance.BorderSize = 0;
            this.btnStar1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStar1.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnStar1.Location = new System.Drawing.Point(0, 0);
            this.btnStar1.Name = "btnStar1";
            this.btnStar1.Size = new System.Drawing.Size(48, 48);
            this.btnStar1.TabIndex = 0;
            this.btnStar1.Text = "\u2606";
            this.btnStar1.UseVisualStyleBackColor = true;

            this.btnStar2.FlatAppearance.BorderSize = 0;
            this.btnStar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStar2.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnStar2.Location = new System.Drawing.Point(56, 0);
            this.btnStar2.Name = "btnStar2";
            this.btnStar2.Size = new System.Drawing.Size(48, 48);
            this.btnStar2.TabIndex = 1;
            this.btnStar2.Text = "\u2606";
            this.btnStar2.UseVisualStyleBackColor = true;

            this.btnStar3.FlatAppearance.BorderSize = 0;
            this.btnStar3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStar3.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnStar3.Location = new System.Drawing.Point(112, 0);
            this.btnStar3.Name = "btnStar3";
            this.btnStar3.Size = new System.Drawing.Size(48, 48);
            this.btnStar3.TabIndex = 2;
            this.btnStar3.Text = "\u2606";
            this.btnStar3.UseVisualStyleBackColor = true;

            this.btnStar4.FlatAppearance.BorderSize = 0;
            this.btnStar4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStar4.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnStar4.Location = new System.Drawing.Point(168, 0);
            this.btnStar4.Name = "btnStar4";
            this.btnStar4.Size = new System.Drawing.Size(48, 48);
            this.btnStar4.TabIndex = 3;
            this.btnStar4.Text = "\u2606";
            this.btnStar4.UseVisualStyleBackColor = true;

            this.btnStar5.FlatAppearance.BorderSize = 0;
            this.btnStar5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStar5.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnStar5.Location = new System.Drawing.Point(224, 0);
            this.btnStar5.Name = "btnStar5";
            this.btnStar5.Size = new System.Drawing.Size(48, 48);
            this.btnStar5.TabIndex = 4;
            this.btnStar5.Text = "\u2606";
            this.btnStar5.UseVisualStyleBackColor = true;

            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(16, 112);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(78, 20);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "Bình luận:";

            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComment.Location = new System.Drawing.Point(16, 136);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(528, 120);
            this.txtComment.TabIndex = 2;

            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243))))); // Premium Blue
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(180, 275);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(200, 50);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Gửi đánh giá ngay";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.pnlStars);
            this.Controls.Add(this.lblTitle);
            this.Name = "UcRating";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Size = new System.Drawing.Size(560, 350);
            this.pnlStars.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

