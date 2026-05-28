using System;
using System.Windows.Forms;

namespace OOP2026
{
    partial class ucLocationPicker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lstSuggestions = new System.Windows.Forms.ListBox();
            this.searchTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Top; // Thay d?i sang Top d? c? d?nh phía trên h?p g?i ý
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.ForeColor = System.Drawing.Color.Black; // Ð?i sang màu den d? nhìn rõ trên n?n tr?ng
            this.txtAddress.Location = new System.Drawing.Point(0, 0);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(320, 30);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.TextChanged += new System.EventHandler(this.TxtAddress_TextChanged);
            // 
            // lstSuggestions
            // 
            this.lstSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSuggestions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstSuggestions.FormattingEnabled = true;
            this.lstSuggestions.ItemHeight = 20;
            this.lstSuggestions.Location = new System.Drawing.Point(0, 32);
            this.lstSuggestions.Name = "lstSuggestions";
            this.lstSuggestions.Size = new System.Drawing.Size(320, 102);
            this.lstSuggestions.TabIndex = 1;
            this.lstSuggestions.Visible = false;
            this.lstSuggestions.Click += new System.EventHandler(this.LstSuggestions_Click);
            // 
            // searchTimer
            // 
            this.searchTimer.Interval = 400; // T?i uu xu?ng 400ms d? tang d? ph?n h?i
            this.searchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick); // FIX: Ðang ký s? ki?n Tick tr?c ti?p
            // 
            // ucLocationPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lstSuggestions);
            this.Controls.Add(this.txtAddress);
            this.MinimumSize = new System.Drawing.Size(200, 32);
            this.Name = "ucLocationPicker";
            this.Size = new System.Drawing.Size(320, 32); // Chi?u cao m?c d?nh v?a khít TextBox
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region Controls declaration
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ListBox lstSuggestions;
        private System.Windows.Forms.Timer searchTimer;
        #endregion
    }
}
