namespace OOP2026
{
    partial class ucTripStatus
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStatusRow = new System.Windows.Forms.Panel();
            this.pnlStatusIcon = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRate = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();

            this.tlpRoot.SuspendLayout();
            this.pnlStatusRow.SuspendLayout();
            this.pnlStatusIcon.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();

            // ── tlpRoot: 2 hàng (icon+text | buttons) ──────────
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Controls.Add(this.pnlStatusRow, 0, 0);
            this.tlpRoot.Controls.Add(this.pnlActions, 0, 1);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ── pnlStatusRow: chứa icon + text ngang hàng ──────
            this.pnlStatusRow.Controls.Add(this.tlpInfo);
            this.pnlStatusRow.Controls.Add(this.pnlStatusIcon);
            this.pnlStatusRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatusRow.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStatusRow.Name = "pnlStatusRow";

            // ── pnlStatusIcon ───────────────────────────────────
            this.pnlStatusIcon.Controls.Add(this.lblIcon);
            this.pnlStatusIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlStatusIcon.BackColor = System.Drawing.Color.LightBlue;
            this.pnlStatusIcon.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlStatusIcon.Name = "pnlStatusIcon";
            this.pnlStatusIcon.Size = new System.Drawing.Size(52, 56);

            // ── lblIcon ─────────────────────────────────────────
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblIcon.ForeColor = Colors.Adm;
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Text = "⏳";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── tlpInfo ─────────────────────────────────────────
            this.tlpInfo.ColumnCount = 1;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInfo.Controls.Add(this.lblTitle, 0, 0);
            this.tlpInfo.Controls.Add(this.lblDescription, 0, 1);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 2;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpInfo.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);

            // ── lblTitle ────────────────────────────────────────
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Đang xử lý";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // ── lblDescription ──────────────────────────────────
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDescription.ForeColor = System.Drawing.Color.Gray;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Text = "Vui lòng đợi trong giây lát";

            // ── pnlActions: chứa 3 nút ─────────────────────────
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Controls.Add(this.btnRate);
            this.pnlActions.Controls.Add(this.btnRetry);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlActions.Name = "pnlActions";

            // ── btnCancel ───────────────────────────────────────
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Text = "Hủy chuyến";
            this.btnCancel.Font = Typography.Font10Bold;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.BackColor = Colors.Red;
            this.btnCancel.ForeColor = Colors.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ── btnRate ─────────────────────────────────────────
            this.btnRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRate.Text = "⭐ Đánh giá tài xế";
            this.btnRate.Font = Typography.Font10Bold;
            this.btnRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRate.FlatAppearance.BorderSize = 0;
            this.btnRate.BackColor = Colors.Orange;
            this.btnRate.ForeColor = Colors.White;
            this.btnRate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRate.Name = "btnRate";
            this.btnRate.Visible = false;
            this.btnRate.Click += new System.EventHandler(this.btnRate_Click);

            // ── btnRetry ────────────────────────────────────────
            this.btnRetry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRetry.Text = "🔄 Đặt lại chuyến";
            this.btnRetry.Font = Typography.Font10Bold;
            this.btnRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetry.FlatAppearance.BorderSize = 0;
            this.btnRetry.BackColor = Colors.Adm;
            this.btnRetry.ForeColor = Colors.White;
            this.btnRetry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Visible = false;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);

            // ── ucTripStatus ────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(320, 60);
            this.Name = "ucTripStatus";
            this.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.Size = new System.Drawing.Size(560, 110);
            this.Controls.Add(this.tlpRoot);

            this.tlpRoot.ResumeLayout(false);
            this.pnlStatusRow.ResumeLayout(false);
            this.pnlStatusIcon.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlStatusRow;
        private System.Windows.Forms.Panel pnlStatusIcon;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRate;
        private System.Windows.Forms.Button btnRetry;
    }
}