using System;
using System.Windows.Forms;

namespace OOP2026
{
    partial class ucPassengerHome
    {
        private System.ComponentModel.IContainer components = null;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblTrips = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBooking = new System.Windows.Forms.Button();
            this.btnTrip = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = OOP2026.Colors.Green;
            this.pnlTop.Controls.Add(this.lblName);
            this.pnlTop.Controls.Add(this.lblPhone);
            this.pnlTop.Controls.Add(this.lblTrips);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlTop.Size = new System.Drawing.Size(300, 110);
            this.pnlTop.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = OOP2026.Typography.Font14Bold;
            this.lblName.ForeColor = OOP2026.Colors.White;
            this.lblName.Location = new System.Drawing.Point(20, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(203, 32);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Psg Name";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = OOP2026.Typography.Font9Regular;
            this.lblPhone.ForeColor = OOP2026.Colors.LightGreen;
            this.lblPhone.Location = new System.Drawing.Point(20, 45);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(50, 20);
            this.lblPhone.TabIndex = 1;
            this.lblPhone.Text = "Phone";
            // 
            // lblTrips
            // 
            this.lblTrips.AutoSize = true;
            this.lblTrips.Font = OOP2026.Typography.Font10Bold;
            this.lblTrips.ForeColor = OOP2026.Colors.White;
            this.lblTrips.Location = new System.Drawing.Point(20, 65);
            this.lblTrips.Name = "lblTrips";
            this.lblTrips.Size = new System.Drawing.Size(37, 19);
            this.lblTrips.TabIndex = 2;
            this.lblTrips.Text = "Trips";
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = OOP2026.Colors.White;
            this.pnlMenu.Controls.Add(this.btnBooking);
            this.pnlMenu.Controls.Add(this.btnTrip);
            this.pnlMenu.Controls.Add(this.btnHistory);
            this.pnlMenu.Controls.Add(this.btnProfile);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenu.Location = new System.Drawing.Point(0, 100);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(300, 60);
            this.pnlMenu.TabIndex = 1;
            this.pnlMenu.WrapContents = false;
            // 
            // btnBooking
            // 
            this.btnBooking.BackColor = OOP2026.Colors.White;
            this.btnBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBooking.FlatAppearance.BorderSize = 0;
            this.btnBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBooking.Font = OOP2026.Typography.Font10Regular;
            this.btnBooking.ForeColor = OOP2026.Colors.Gray;
            this.btnBooking.Location = new System.Drawing.Point(0, 0);
            this.btnBooking.Margin = new System.Windows.Forms.Padding(0);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(75, 50);
            this.btnBooking.TabIndex = 0;
            this.btnBooking.Tag = "Booking";
            this.btnBooking.Text = "Đặt xe";
            this.btnBooking.UseVisualStyleBackColor = false;
            this.btnBooking.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnTrip
            // 
            this.btnTrip.BackColor = OOP2026.Colors.White;
            this.btnTrip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrip.FlatAppearance.BorderSize = 0;
            this.btnTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrip.Font = OOP2026.Typography.Font10Regular;
            this.btnTrip.ForeColor = OOP2026.Colors.Gray;
            this.btnTrip.Location = new System.Drawing.Point(75, 0);
            this.btnTrip.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrip.Name = "btnTrip";
            this.btnTrip.Size = new System.Drawing.Size(75, 50);
            this.btnTrip.TabIndex = 1;
            this.btnTrip.Tag = "Trip";
            this.btnTrip.Text = "Chuyến";
            this.btnTrip.UseVisualStyleBackColor = false;
            this.btnTrip.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = OOP2026.Colors.White;
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = OOP2026.Typography.Font10Regular;
            this.btnHistory.ForeColor = OOP2026.Colors.Gray;
            this.btnHistory.Location = new System.Drawing.Point(150, 0);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(0);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(75, 50);
            this.btnHistory.TabIndex = 2;
            this.btnHistory.Tag = "History";
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.Tab_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = OOP2026.Colors.White;
            this.btnProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = OOP2026.Typography.Font10Regular;
            this.btnProfile.ForeColor = OOP2026.Colors.Gray;
            this.btnProfile.Location = new System.Drawing.Point(225, 0);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(0);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(75, 50);
            this.btnProfile.TabIndex = 3;
            this.btnProfile.Tag = "Profile";
            this.btnProfile.Text = "Hồ sơ";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.Tab_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 170);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12);
            this.pnlContent.Size = new System.Drawing.Size(300, 450);
            this.pnlContent.TabIndex = 2;
            // 
            // ucPassengerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = OOP2026.Colors.White;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlTop);
            this.Font = OOP2026.Typography.Font10Regular;
            this.MinimumSize = new System.Drawing.Size(300, 480);
            this.Name = "ucPassengerHome";
            this.Size = new System.Drawing.Size(300, 600);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        #region Controls declaration
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblTrips;
        private System.Windows.Forms.FlowLayoutPanel pnlMenu;
        private System.Windows.Forms.Button btnBooking;
        private System.Windows.Forms.Button btnTrip;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Panel pnlContent;
        #endregion
    }
}