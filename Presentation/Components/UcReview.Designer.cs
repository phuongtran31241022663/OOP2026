using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    partial class UcReview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
protected void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.pnlStars = new Panel();
            this.btnStar5 = new Button();
            this.btnStar4 = new Button();
            this.btnStar3 = new Button();
            this.btnStar2 = new Button();
            this.btnStar1 = new Button();
            this.txtComment = new TextBox();
            this.lblComment = new Label();
            this.btnSubmit = new Button();
            this.pnlStars.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(528, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đánh giá tài xế";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlStars
            // 
            this.pnlStars.Anchor = AnchorStyles.Top;
            this.pnlStars.Controls.Add(this.btnStar5);
            this.pnlStars.Controls.Add(this.btnStar4);
            this.pnlStars.Controls.Add(this.btnStar3);
            this.pnlStars.Controls.Add(this.btnStar2);
            this.pnlStars.Controls.Add(this.btnStar1);
            this.pnlStars.Location = new Point(140, 72);
            this.pnlStars.Name = "pnlStars";
            this.pnlStars.Size = new Size(280, 48);
            this.pnlStars.TabIndex = 1;
            // 
            // btnStar1
            // 
            this.btnStar1.FlatAppearance.BorderSize = 0;
            this.btnStar1.FlatStyle = FlatStyle.Flat;
            this.btnStar1.Font = new Font("Segoe UI", 20F);
            this.btnStar1.Location = new Point(0, 0);
            this.btnStar1.Name = "btnStar1";
            this.btnStar1.Size = new Size(48, 48);
            this.btnStar1.TabIndex = 0;
            this.btnStar1.Text = "☆";
            this.btnStar1.UseVisualStyleBackColor = true;
            // 
            // btnStar2
            // 
            this.btnStar2.FlatAppearance.BorderSize = 0;
            this.btnStar2.FlatStyle = FlatStyle.Flat;
            this.btnStar2.Font = new Font("Segoe UI", 20F);
            this.btnStar2.Location = new Point(56, 0);
            this.btnStar2.Name = "btnStar2";
            this.btnStar2.Size = new Size(48, 48);
            this.btnStar2.TabIndex = 1;
            this.btnStar2.Text = "☆";
            this.btnStar2.UseVisualStyleBackColor = true;
            // 
            // btnStar3
            // 
            this.btnStar3.FlatAppearance.BorderSize = 0;
            this.btnStar3.FlatStyle = FlatStyle.Flat;
            this.btnStar3.Font = new Font("Segoe UI", 20F);
            this.btnStar3.Location = new Point(112, 0);
            this.btnStar3.Name = "btnStar3";
            this.btnStar3.Size = new Size(48, 48);
            this.btnStar3.TabIndex = 2;
            this.btnStar3.Text = "☆";
            this.btnStar3.UseVisualStyleBackColor = true;
            // 
            // btnStar4
            // 
            this.btnStar4.FlatAppearance.BorderSize = 0;
            this.btnStar4.FlatStyle = FlatStyle.Flat;
            this.btnStar4.Font = new Font("Segoe UI", 20F);
            this.btnStar4.Location = new Point(168, 0);
            this.btnStar4.Name = "btnStar4";
            this.btnStar4.Size = new Size(48, 48);
            this.btnStar4.TabIndex = 3;
            this.btnStar4.Text = "☆";
            this.btnStar4.UseVisualStyleBackColor = true;
            // 
            // btnStar5
            // 
            this.btnStar5.FlatAppearance.BorderSize = 0;
            this.btnStar5.FlatStyle = FlatStyle.Flat;
            this.btnStar5.Font = new Font("Segoe UI", 20F);
            this.btnStar5.Location = new Point(224, 0);
            this.btnStar5.Name = "btnStar5";
            this.btnStar5.Size = new Size(48, 48);
            this.btnStar5.TabIndex = 4;
            this.btnStar5.Text = "☆";
            this.btnStar5.UseVisualStyleBackColor = true;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.txtComment.Font = new Font("Segoe UI", 10F);
            this.txtComment.Location = new Point(16, 136);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new Size(528, 120);
            this.txtComment.TabIndex = 2;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new Point(16, 112);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new Size(78, 20);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "Bình luận:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = AnchorStyles.Bottom;
            this.btnSubmit.BackColor = Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSubmit.FlatStyle = FlatStyle.Flat;
            this.btnSubmit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSubmit.ForeColor = Color.White;
            this.btnSubmit.Location = new Point(404, 272);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(140, 40);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Gửi đánh giá ngay";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // UcReview
            // 
            this.AutoScaleDimensions = new SizeF(8F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlStars);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.btnSubmit);
            this.Name = "UcReview";
            this.Padding = new Padding(16);
            this.Size = new Size(560, 350);
            this.pnlStars.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlStars;
        private Button btnStar1;
        private Button btnStar2;
        private Button btnStar3;
        private Button btnStar4;
        private Button btnStar5;
        private TextBox txtComment;
        private Label lblComment;
        private Button btnSubmit;
    }
}

