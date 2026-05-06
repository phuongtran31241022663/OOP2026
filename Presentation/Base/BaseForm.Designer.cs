using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Base
{
    partial class BaseForm
    {
        private System.ComponentModel.IContainer components = null;

// Controls - FrmMain dùng Panel riêng, không cần TabControl

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
protected void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 774);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
this.Text = "Ứng dụng";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}

