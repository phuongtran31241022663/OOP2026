namespace Presentation.Shells
{
    partial class FrmMultiRole
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlPassenger;
        private System.Windows.Forms.Panel pnlDriver;

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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlPassenger = new System.Windows.Forms.Panel();
            this.pnlDriver = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.pnlPassenger);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.pnlDriver);
            this.splitContainer1.Size = new System.Drawing.Size(1400, 800);
            this.splitContainer1.SplitterDistance = 700;
            this.splitContainer1.TabIndex = 0;
            //
            // pnlPassenger
            //
            this.pnlPassenger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPassenger.Location = new System.Drawing.Point(0, 0);
            this.pnlPassenger.Name = "pnlPassenger";
            this.pnlPassenger.Size = new System.Drawing.Size(700, 800);
            this.pnlPassenger.TabIndex = 0;
            //
            // pnlDriver
            //
            this.pnlDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDriver.Location = new System.Drawing.Point(0, 0);
            this.pnlDriver.Name = "pnlDriver";
            this.pnlDriver.Size = new System.Drawing.Size(696, 800);
            this.pnlDriver.TabIndex = 0;
            //
            // FrmMultiRole
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "FrmMultiRole";
            this.Text = "RideGo - Multi-Role View";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}