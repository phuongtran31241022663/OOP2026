namespace OOP2026
{
    partial class ucRequest
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.flpRequests = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpActiveTrip = new System.Windows.Forms.TableLayoutPanel();
            this.lblActiveBanner = new System.Windows.Forms.Label();
            this.lblRouteInfo = new System.Windows.Forms.Label();
            this.lblTripSummary = new System.Windows.Forms.Label();
            this.tlpActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnArrived = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            this.tlpActiveTrip.SuspendLayout();
            this.tlpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chuyến được đề nghị (0)";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.flpRequests);
            this.pnlContent.Controls.Add(this.tlpActiveTrip);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(12, 44);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(276, 344);
            this.pnlContent.TabIndex = 1;
            // 
            // flpRequests
            // 
            this.flpRequests.AutoScroll = true;
            this.flpRequests.BackColor = System.Drawing.Color.White;
            this.flpRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRequests.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRequests.Location = new System.Drawing.Point(0, 0);
            this.flpRequests.Name = "flpRequests";
            this.flpRequests.Padding = new System.Windows.Forms.Padding(4);
            this.flpRequests.Size = new System.Drawing.Size(276, 344);
            this.flpRequests.TabIndex = 0;
            this.flpRequests.WrapContents = false;
            // 
            // tlpActiveTrip
            // 
            this.tlpActiveTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.tlpActiveTrip.ColumnCount = 1;
            this.tlpActiveTrip.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActiveTrip.Controls.Add(this.lblActiveBanner, 0, 0);
            this.tlpActiveTrip.Controls.Add(this.lblRouteInfo, 0, 1);
            this.tlpActiveTrip.Controls.Add(this.lblTripSummary, 0, 2);
            this.tlpActiveTrip.Controls.Add(this.tlpActions, 0, 3);
            this.tlpActiveTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpActiveTrip.Location = new System.Drawing.Point(0, 0);
            this.tlpActiveTrip.Name = "tlpActiveTrip";
            this.tlpActiveTrip.RowCount = 4;
            this.tlpActiveTrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpActiveTrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActiveTrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpActiveTrip.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpActiveTrip.Size = new System.Drawing.Size(276, 344);
            this.tlpActiveTrip.TabIndex = 1;
            this.tlpActiveTrip.Visible = false;
            // 
            // lblActiveBanner
            // 
            this.lblActiveBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.lblActiveBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveBanner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblActiveBanner.ForeColor = System.Drawing.Color.White;
            this.lblActiveBanner.Location = new System.Drawing.Point(0, 0);
            this.lblActiveBanner.Margin = new System.Windows.Forms.Padding(0);
            this.lblActiveBanner.Name = "lblActiveBanner";
            this.lblActiveBanner.Size = new System.Drawing.Size(276, 36);
            this.lblActiveBanner.TabIndex = 0;
            this.lblActiveBanner.Text = "🚗 ĐANG TRONG CHUYẾN ĐI";
            this.lblActiveBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRouteInfo
            // 
            this.lblRouteInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRouteInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRouteInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblRouteInfo.Location = new System.Drawing.Point(8, 44);
            this.lblRouteInfo.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.lblRouteInfo.Name = "lblRouteInfo";
            this.lblRouteInfo.Size = new System.Drawing.Size(260, 212);
            this.lblRouteInfo.TabIndex = 1;
            this.lblRouteInfo.Text = "📍 Điểm đón: Đang cập nhật...\n\n🏁 Điểm trả: Đang cập nhật...";
            // 
            // lblTripSummary
            // 
            this.lblTripSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTripSummary.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTripSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(5)))));
            this.lblTripSummary.Location = new System.Drawing.Point(8, 256);
            this.lblTripSummary.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTripSummary.Name = "lblTripSummary";
            this.lblTripSummary.Size = new System.Drawing.Size(260, 40);
            this.lblTripSummary.TabIndex = 2;
            this.lblTripSummary.Text = "0.0 km  •  0đ";
            this.lblTripSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpActions
            // 
            this.tlpActions.ColumnCount = 1;
            this.tlpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActions.Controls.Add(this.btnArrived, 0, 0);
            this.tlpActions.Controls.Add(this.btnStart, 0, 0);
            this.tlpActions.Controls.Add(this.btnComplete, 0, 0);
            this.tlpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpActions.Location = new System.Drawing.Point(0, 296);
            this.tlpActions.Margin = new System.Windows.Forms.Padding(0);
            this.tlpActions.Name = "tlpActions";
            this.tlpActions.RowCount = 1;
            this.tlpActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpActions.Size = new System.Drawing.Size(276, 48);
            this.tlpActions.TabIndex = 3;
            // 
            // btnArrived
            // 
            this.btnArrived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(236)))));
            this.btnArrived.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnArrived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArrived.FlatAppearance.BorderSize = 0;
            this.btnArrived.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArrived.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnArrived.ForeColor = System.Drawing.Color.White;
            this.btnArrived.Location = new System.Drawing.Point(0, 28);
            this.btnArrived.Margin = new System.Windows.Forms.Padding(0);
            this.btnArrived.Name = "btnArrived";
            this.btnArrived.Size = new System.Drawing.Size(276, 20);
            this.btnArrived.TabIndex = 0;
            this.btnArrived.Text = "📍 ĐÃ ĐẾN ĐIỂM ĐÓN";
            this.btnArrived.UseVisualStyleBackColor = false;
            this.btnArrived.Click += new System.EventHandler(this.BtnArrived_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(5)))));
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(0, 8);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(276, 20);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "🚗 BẮT ĐẦU DI CHUYỂN";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.btnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComplete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(0, 0);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(0);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(276, 8);
            this.btnComplete.TabIndex = 2;
            this.btnComplete.Text = "✅ HOÀN THÀNH CHUYẾN ĐI";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Visible = false;
            this.btnComplete.Click += new System.EventHandler(this.BtnComplete_Click);
            // 
            // ucRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(280, 340);
            this.Name = "ucRequest";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(300, 400);
            this.pnlContent.ResumeLayout(false);
            this.tlpActiveTrip.ResumeLayout(false);
            this.tlpActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.FlowLayoutPanel flpRequests;
        private System.Windows.Forms.TableLayoutPanel tlpActiveTrip;
        private System.Windows.Forms.Label lblActiveBanner;
        private System.Windows.Forms.Label lblRouteInfo;
        private System.Windows.Forms.Label lblTripSummary;
        private System.Windows.Forms.TableLayoutPanel tlpActions;
        private System.Windows.Forms.Button btnArrived;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnComplete;
    }
}