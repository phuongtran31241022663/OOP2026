namespace Presentation.Shells
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnMultiRole;

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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnMultiRole = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 800);
            this.pnlContent.TabIndex = 0;
            // 
            // btnMultiRole
            // 
            this.btnMultiRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultiRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnMultiRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultiRole.Location = new System.Drawing.Point(1080, 10);
            this.btnMultiRole.Name = "btnMultiRole";
            this.btnMultiRole.Size = new System.Drawing.Size(100, 30);
            this.btnMultiRole.TabIndex = 1;
            this.btnMultiRole.Text = "Multi-Role";
            this.btnMultiRole.UseVisualStyleBackColor = false;
            // 
            // FrmMainShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.btnMultiRole);
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "FrmMainShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;// cái này khai báo chung được nè
            this.Text = "OOP";
            this.ResumeLayout(false);
            // màu màn hình chính và ucauth đồng bộ, cùng màu là 1 điểm hay
        }

        #endregion
    }
}

