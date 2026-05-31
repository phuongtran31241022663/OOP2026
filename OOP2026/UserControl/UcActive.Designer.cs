using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP2026
{
    partial class UcActive
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlpMain;

        private Label lblTitle;
        private Panel pnlDivider;

        private Label lblCompleted;
        private Label lblCompletedCount;

        private Label lblCancelled;
        private Label lblCancelledCount;

        private Label lblTimeout;
        private Label lblTimeoutCount;

        private Label lblActive;
        private Label lblActiveCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new TableLayoutPanel();
            this.lblTitle = new Label();
            this.pnlDivider = new Panel();
            this.lblCompleted = new Label();
            this.lblCompletedCount = new Label();
            this.lblCancelled = new Label();
            this.lblCancelledCount = new Label();
            this.lblTimeout = new Label();
            this.lblTimeoutCount = new Label();
            this.lblActive = new Label();
            this.lblActiveCount = new Label();

            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = Color.White;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.pnlDivider, 0, 1);
            this.tlpMain.Controls.Add(this.lblCompleted, 0, 2);
            this.tlpMain.Controls.Add(this.lblCompletedCount, 1, 2);
            this.tlpMain.Controls.Add(this.lblCancelled, 0, 3);
            this.tlpMain.Controls.Add(this.lblCancelledCount, 1, 3);
            this.tlpMain.Controls.Add(this.lblTimeout, 0, 4);
            this.tlpMain.Controls.Add(this.lblTimeoutCount, 1, 4);
            this.tlpMain.Controls.Add(this.lblActive, 0, 5);
            this.tlpMain.Controls.Add(this.lblActiveCount, 1, 5);
            this.tlpMain.Dock = DockStyle.Fill;
            this.tlpMain.Location = new Point(0, 0);
            this.tlpMain.Margin = new Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new Padding(12);
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tlpMain.Size = new Size(414, 236);
            this.tlpMain.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblTitle.Location = new Point(15, 12); // Padding 12 + margin?
            this.lblTitle.Margin = new Padding(3, 0, 3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(384, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Trạng thái chuyến";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.tlpMain.SetColumnSpan(this.lblTitle, 2);

            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = Color.Gainsboro;
            this.pnlDivider.Dock = DockStyle.Fill;
            this.pnlDivider.Location = new Point(15, 47);
            this.pnlDivider.Margin = new Padding(3, 0, 3, 8);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new Size(384, 1);
            this.pnlDivider.TabIndex = 1;
            this.tlpMain.SetColumnSpan(this.pnlDivider, 2);

            // 
            // lblCompleted
            // 
            this.lblCompleted.Dock = DockStyle.Fill;
            this.lblCompleted.Font = new Font("Segoe UI", 10F);
            this.lblCompleted.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblCompleted.Location = new Point(15, 56);
            this.lblCompleted.Margin = new Padding(3, 0, 3, 0);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new Size(310, 40);
            this.lblCompleted.TabIndex = 2;
            this.lblCompleted.Text = "Hoàn thành";
            this.lblCompleted.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblCompletedCount
            // 
            this.lblCompletedCount.Dock = DockStyle.Fill;
            this.lblCompletedCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCompletedCount.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblCompletedCount.Location = new Point(331, 56);
            this.lblCompletedCount.Margin = new Padding(3, 0, 3, 0);
            this.lblCompletedCount.Name = "lblCompletedCount";
            this.lblCompletedCount.Size = new Size(68, 40);
            this.lblCompletedCount.TabIndex = 3;
            this.lblCompletedCount.Text = "0";
            this.lblCompletedCount.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblCancelled
            // 
            this.lblCancelled.Dock = DockStyle.Fill;
            this.lblCancelled.Font = new Font("Segoe UI", 10F);
            this.lblCancelled.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblCancelled.Location = new Point(15, 96);
            this.lblCancelled.Margin = new Padding(3, 0, 3, 0);
            this.lblCancelled.Name = "lblCancelled";
            this.lblCancelled.Size = new Size(310, 40);
            this.lblCancelled.TabIndex = 4;
            this.lblCancelled.Text = "Đã hủy";
            this.lblCancelled.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblCancelledCount
            // 
            this.lblCancelledCount.Dock = DockStyle.Fill;
            this.lblCancelledCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCancelledCount.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblCancelledCount.Location = new Point(331, 96);
            this.lblCancelledCount.Margin = new Padding(3, 0, 3, 0);
            this.lblCancelledCount.Name = "lblCancelledCount";
            this.lblCancelledCount.Size = new Size(68, 40);
            this.lblCancelledCount.TabIndex = 5;
            this.lblCancelledCount.Text = "0";
            this.lblCancelledCount.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblTimeout
            // 
            this.lblTimeout.Dock = DockStyle.Fill;
            this.lblTimeout.Font = new Font("Segoe UI", 10F);
            this.lblTimeout.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblTimeout.Location = new Point(15, 136);
            this.lblTimeout.Margin = new Padding(3, 0, 3, 0);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new Size(310, 40);
            this.lblTimeout.TabIndex = 6;
            this.lblTimeout.Text = "Timeout";
            this.lblTimeout.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblTimeoutCount
            // 
            this.lblTimeoutCount.Dock = DockStyle.Fill;
            this.lblTimeoutCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTimeoutCount.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblTimeoutCount.Location = new Point(331, 136);
            this.lblTimeoutCount.Margin = new Padding(3, 0, 3, 0);
            this.lblTimeoutCount.Name = "lblTimeoutCount";
            this.lblTimeoutCount.Size = new Size(68, 40);
            this.lblTimeoutCount.TabIndex = 7;
            this.lblTimeoutCount.Text = "0";
            this.lblTimeoutCount.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblActive
            // 
            this.lblActive.Dock = DockStyle.Fill;
            this.lblActive.Font = new Font("Segoe UI", 10F);
            this.lblActive.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblActive.Location = new Point(15, 176);
            this.lblActive.Margin = new Padding(3, 0, 3, 0);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new Size(310, 40);
            this.lblActive.TabIndex = 8;
            this.lblActive.Text = "Đang hoạt động";
            this.lblActive.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblActiveCount
            // 
            this.lblActiveCount.Dock = DockStyle.Fill;
            this.lblActiveCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblActiveCount.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblActiveCount.Location = new Point(331, 176);
            this.lblActiveCount.Margin = new Padding(3, 0, 3, 0);
            this.lblActiveCount.Name = "lblActiveCount";
            this.lblActiveCount.Size = new Size(68, 40);
            this.lblActiveCount.TabIndex = 9;
            this.lblActiveCount.Text = "0";
            this.lblActiveCount.TextAlign = ContentAlignment.MiddleRight;

            // 
            // UcActive
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Controls.Add(this.tlpMain);
            this.Name = "UcActive";
            this.Size = new Size(414, 236);

            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}