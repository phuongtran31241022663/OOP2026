// Presentation/BaseForm_Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace Presentation
{
    partial class BaseForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "BaseForm";

            this.Size = new Size(1024, 768);
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.KeyPreview = true;   // Cho phép form bắt KeyDown trước controls

            this.FormClosing += BaseForm_FormClosing;
            this.KeyDown += BaseForm_KeyDown;
        }
        #endregion
    }
}