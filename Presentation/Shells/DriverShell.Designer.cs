namespace Presentation.Shells
{
    partial class DriverShell : BaseShell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CleanupShell();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.btnToggleStatus = new System.Windows.Forms.Button();
            this._contentPanel = new System.Windows.Forms.Panel();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.btnToggleStatus);
            this.headerPanel.Controls.Add(this.lblDriverName);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1024, 50);
            this.headerPanel.TabIndex = 1;
            //
            // lblDriverName
            //
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriverName.Location = new System.Drawing.Point(20, 15);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(100, 20);
            this.lblDriverName.TabIndex = 0;
            this.lblDriverName.Text = "Driver Name";
            //
            // btnToggleStatus
            //
            this.btnToggleStatus.Location = new System.Drawing.Point(900, 10);
            this.btnToggleStatus.Name = "btnToggleStatus";
            this.btnToggleStatus.Size = new System.Drawing.Size(100, 30);
            this.btnToggleStatus.TabIndex = 1;
            this.btnToggleStatus.Text = "Go Online";
            this.btnToggleStatus.UseVisualStyleBackColor = true;
            this.btnToggleStatus.Click += new System.EventHandler(this.btnToggleStatus_Click);
            //
            // _contentPanel
            //
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 50);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1024, 590);
            this._contentPanel.TabIndex = 0;
            //
            // DriverShell
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "DriverShell";
            this.Text = "Driver Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DriverShell_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Button btnToggleStatus;
        private System.Windows.Forms.Panel _contentPanel;
    }
}
