using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OOP2026
{
    partial class ucBooking
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPickup = new System.Windows.Forms.Panel();
            this.tlpPickup = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPickupDot = new System.Windows.Forms.Panel();
            this.ucPickupPicker = new OOP2026.ucLocationPicker();
            this.pnlPickupButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearPickup = new System.Windows.Forms.Button();
            this.btnLocatePickup = new System.Windows.Forms.Button();
            this.lblPickupTitle = new System.Windows.Forms.Label();
            this.pnlSpacer1 = new System.Windows.Forms.Panel();
            this.pnlDropoff = new System.Windows.Forms.Panel();
            this.tlpDropoff = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDropoffDot = new System.Windows.Forms.Panel();
            this.ucDropoffPicker = new OOP2026.ucLocationPicker();
            this.pnlDropoffButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearDropoff = new System.Windows.Forms.Button();
            this.btnLocateDropoff = new System.Windows.Forms.Button();
            this.lblDropoffTitle = new System.Windows.Forms.Label();
            this.lblEstimateInfo = new System.Windows.Forms.Label();
            this.pnlVehicleSelector = new System.Windows.Forms.Panel();
            this.ucFareSelector = new OOP2026.ucFareSelector();
            this.btnRequestTrip = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlPickup.SuspendLayout();
            this.tlpPickup.SuspendLayout();
            this.pnlPickupButtons.SuspendLayout();
            this.pnlDropoff.SuspendLayout();
            this.tlpDropoff.SuspendLayout();
            this.pnlDropoffButtons.SuspendLayout();
            this.pnlVehicleSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlPickup, 0, 0);
            this.tlpMain.Controls.Add(this.pnlSpacer1, 0, 1);
            this.tlpMain.Controls.Add(this.pnlDropoff, 0, 2);
            this.tlpMain.Controls.Add(this.lblEstimateInfo, 0, 3);
            this.tlpMain.Controls.Add(this.pnlVehicleSelector, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(11, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(321, 458);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlPickup
            // 
            this.pnlPickup.Controls.Add(this.tlpPickup);
            this.pnlPickup.Controls.Add(this.lblPickupTitle);
            this.pnlPickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPickup.Location = new System.Drawing.Point(0, 0);
            this.pnlPickup.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPickup.Name = "pnlPickup";
            this.pnlPickup.AutoSize = true;
            this.pnlPickup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlPickup.TabIndex = 0;
            // 
            // tlpPickup
            // 
            this.tlpPickup.ColumnCount = 3;
            this.tlpPickup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpPickup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPickup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpPickup.Controls.Add(this.pnlPickupDot, 0, 0);
            this.tlpPickup.Controls.Add(this.ucPickupPicker, 1, 0);
            this.tlpPickup.Controls.Add(this.pnlPickupButtons, 2, 0);
            this.tlpPickup.Location = new System.Drawing.Point(0, 20);
            this.tlpPickup.Name = "tlpPickup";
            this.tlpPickup.RowCount = 1;
            this.tlpPickup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPickup.AutoSize = true;
            this.tlpPickup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpPickup.TabIndex = 0;
            // 
            // pnlPickupDot
            // 
            this.pnlPickupDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.pnlPickupDot.Location = new System.Drawing.Point(0, 0);
            this.pnlPickupDot.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPickupDot.Name = "pnlPickupDot";
            this.pnlPickupDot.Size = new System.Drawing.Size(12, 12);
            this.pnlPickupDot.TabIndex = 0;
            this.pnlPickupDot.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPickupDot_Paint);
            // 
            // ucPickupPicker
            // 
            this.ucPickupPicker.BackColor = System.Drawing.Color.White;
            this.ucPickupPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPickupPicker.Location = new System.Drawing.Point(37, 3);
            this.ucPickupPicker.MinimumSize = new System.Drawing.Size(206, 42);
            this.ucPickupPicker.Name = "ucPickupPicker";
            this.ucPickupPicker.Size = new System.Drawing.Size(212, 48);
            this.ucPickupPicker.TabIndex = 1;
            this.ucPickupPicker.AddressSelected += new System.EventHandler<OOP2026.LocationSelectedEventArgs>(this.PickupPicker_AddressSelected);
            // 
            // pnlPickupButtons
            // 
            this.pnlPickupButtons.Controls.Add(this.btnClearPickup);
            this.pnlPickupButtons.Controls.Add(this.btnLocatePickup);
            this.pnlPickupButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPickupButtons.Location = new System.Drawing.Point(252, 0);
            this.pnlPickupButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPickupButtons.Name = "pnlPickupButtons";
            this.pnlPickupButtons.Size = new System.Drawing.Size(69, 54);
            this.pnlPickupButtons.TabIndex = 2;
            // 
            // btnClearPickup
            // 
            this.btnClearPickup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.btnClearPickup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearPickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPickup.ForeColor = System.Drawing.Color.Black;
            this.btnClearPickup.Location = new System.Drawing.Point(3, 4);
            this.btnClearPickup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearPickup.Name = "btnClearPickup";
            this.btnClearPickup.Size = new System.Drawing.Size(27, 28);
            this.btnClearPickup.TabIndex = 0;
            this.btnClearPickup.Text = "×";
            this.btnClearPickup.UseVisualStyleBackColor = false;
            this.btnClearPickup.Click += new System.EventHandler(this.BtnClearPickup_Click);
            // 
            // btnLocatePickup
            // 
            this.btnLocatePickup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.btnLocatePickup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocatePickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocatePickup.Location = new System.Drawing.Point(36, 4);
            this.btnLocatePickup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLocatePickup.Name = "btnLocatePickup";
            this.btnLocatePickup.Size = new System.Drawing.Size(27, 28);
            this.btnLocatePickup.TabIndex = 1;
            this.btnLocatePickup.Text = "📍";
            this.btnLocatePickup.UseVisualStyleBackColor = false;
            this.btnLocatePickup.Click += new System.EventHandler(this.BtnLocatePickup_Click);
            // 
            // lblPickupTitle
            // 
            this.lblPickupTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPickupTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPickupTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblPickupTitle.Location = new System.Drawing.Point(3, 0);
            this.lblPickupTitle.Name = "lblPickupTitle";
            this.lblPickupTitle.Size = new System.Drawing.Size(100, 20);
            this.lblPickupTitle.TabIndex = 1;
            this.lblPickupTitle.Text = "Điểm đón";
            this.lblPickupTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSpacer1
            // 
            this.pnlSpacer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.pnlSpacer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSpacer1.Location = new System.Drawing.Point(0, 75);
            this.pnlSpacer1.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSpacer1.Name = "pnlSpacer1";
            this.pnlSpacer1.Size = new System.Drawing.Size(321, 6);
            this.pnlSpacer1.TabIndex = 1;
            // 
            // pnlDropoff
            // 
            this.pnlDropoff.Controls.Add(this.tlpDropoff);
            this.pnlDropoff.Controls.Add(this.lblDropoffTitle);
            this.pnlDropoff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDropoff.Location = new System.Drawing.Point(0, 81);
            this.pnlDropoff.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDropoff.Name = "pnlDropoff";
            this.pnlDropoff.AutoSize = true;
            this.pnlDropoff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDropoff.TabIndex = 2;
            // 
            // tlpDropoff
            // 
            this.tlpDropoff.ColumnCount = 3;
            this.tlpDropoff.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpDropoff.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDropoff.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpDropoff.Controls.Add(this.pnlDropoffDot, 0, 0);
            this.tlpDropoff.Controls.Add(this.ucDropoffPicker, 1, 0);
            this.tlpDropoff.Controls.Add(this.pnlDropoffButtons, 2, 0);
            this.tlpDropoff.Location = new System.Drawing.Point(0, 20);
            this.tlpDropoff.Name = "tlpDropoff";
            this.tlpDropoff.RowCount = 1;
            this.tlpDropoff.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDropoff.AutoSize = true;
            this.tlpDropoff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDropoff.TabIndex = 0;
            // 
            // pnlDropoffDot
            // 
            this.pnlDropoffDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(35)))), ((int)(((byte)(60)))));
            this.pnlDropoffDot.Location = new System.Drawing.Point(0, 0);
            this.pnlDropoffDot.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDropoffDot.Name = "pnlDropoffDot";
            this.pnlDropoffDot.Size = new System.Drawing.Size(12, 12);
            this.pnlDropoffDot.TabIndex = 0;
            this.pnlDropoffDot.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlDropoffDot_Paint);
            // 
            // ucDropoffPicker
            // 
            this.ucDropoffPicker.BackColor = System.Drawing.Color.White;
            this.ucDropoffPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDropoffPicker.Location = new System.Drawing.Point(37, 3);
            this.ucDropoffPicker.MinimumSize = new System.Drawing.Size(206, 42);
            this.ucDropoffPicker.Name = "ucDropoffPicker";
            this.ucDropoffPicker.Size = new System.Drawing.Size(212, 48);
            this.ucDropoffPicker.TabIndex = 1;
            this.ucDropoffPicker.AddressSelected += new System.EventHandler<OOP2026.LocationSelectedEventArgs>(this.DropoffPicker_AddressSelected);
            // 
            // pnlDropoffButtons
            // 
            this.pnlDropoffButtons.Controls.Add(this.btnClearDropoff);
            this.pnlDropoffButtons.Controls.Add(this.btnLocateDropoff);
            this.pnlDropoffButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDropoffButtons.Location = new System.Drawing.Point(252, 0);
            this.pnlDropoffButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDropoffButtons.Name = "pnlDropoffButtons";
            this.pnlDropoffButtons.Size = new System.Drawing.Size(69, 54);
            this.pnlDropoffButtons.TabIndex = 2;
            // 
            // btnClearDropoff
            // 
            this.btnClearDropoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.btnClearDropoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearDropoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDropoff.ForeColor = System.Drawing.Color.Black;
            this.btnClearDropoff.Location = new System.Drawing.Point(3, 4);
            this.btnClearDropoff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearDropoff.Name = "btnClearDropoff";
            this.btnClearDropoff.Size = new System.Drawing.Size(27, 28);
            this.btnClearDropoff.TabIndex = 0;
            this.btnClearDropoff.Text = "×";
            this.btnClearDropoff.UseVisualStyleBackColor = false;
            this.btnClearDropoff.Click += new System.EventHandler(this.BtnClearDropoff_Click);
            // 
            // btnLocateDropoff
            // 
            this.btnLocateDropoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.btnLocateDropoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocateDropoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocateDropoff.Location = new System.Drawing.Point(36, 4);
            this.btnLocateDropoff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLocateDropoff.Name = "btnLocateDropoff";
            this.btnLocateDropoff.Size = new System.Drawing.Size(27, 28);
            this.btnLocateDropoff.TabIndex = 1;
            this.btnLocateDropoff.Text = "📍";
            this.btnLocateDropoff.UseVisualStyleBackColor = false;
            this.btnLocateDropoff.Click += new System.EventHandler(this.BtnLocateDropoff_Click);
            // 
            // lblDropoffTitle
            // 
            this.lblDropoffTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDropoffTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDropoffTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblDropoffTitle.Location = new System.Drawing.Point(3, 0);
            this.lblDropoffTitle.Name = "lblDropoffTitle";
            this.lblDropoffTitle.Size = new System.Drawing.Size(100, 20);
            this.lblDropoffTitle.TabIndex = 1;
            this.lblDropoffTitle.Text = "Điểm đến";
            this.lblDropoffTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstimateInfo
            // 
            this.lblEstimateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstimateInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstimateInfo.ForeColor = System.Drawing.Color.Black;
            this.lblEstimateInfo.Location = new System.Drawing.Point(3, 156);
            this.lblEstimateInfo.Name = "lblEstimateInfo";
            this.lblEstimateInfo.Size = new System.Drawing.Size(315, 47);
            this.lblEstimateInfo.TabIndex = 3;
            this.lblEstimateInfo.Text = "--- km --- phút";
            this.lblEstimateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlVehicleSelector
            // 
            this.pnlVehicleSelector.Controls.Add(this.ucFareSelector);
            this.pnlVehicleSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehicleSelector.Location = new System.Drawing.Point(0, 203);
            this.pnlVehicleSelector.Margin = new System.Windows.Forms.Padding(0);
            this.pnlVehicleSelector.Name = "pnlVehicleSelector";
            this.pnlVehicleSelector.Size = new System.Drawing.Size(321, 255);
            this.pnlVehicleSelector.TabIndex = 4;
            // 
            // ucFareSelector
            // 
            this.ucFareSelector.BackColor = System.Drawing.Color.White;
            this.ucFareSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFareSelector.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ucFareSelector.Location = new System.Drawing.Point(0, 0);
            this.ucFareSelector.MinimumSize = new System.Drawing.Size(300, 110);
            this.ucFareSelector.Name = "ucFareSelector";
            this.ucFareSelector.Padding = new System.Windows.Forms.Padding(10);
            this.ucFareSelector.Size = new System.Drawing.Size(321, 130);
            this.ucFareSelector.TabIndex = 0;
            this.ucFareSelector.SelectionChanged += new System.EventHandler<OOP2026.VehicleType>(this.ucFareSelector_SelectionChanged);
            // 
            // btnRequestTrip
            // 
            this.btnRequestTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(192)))), ((int)(((byte)(123)))));
            this.btnRequestTrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRequestTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequestTrip.ForeColor = System.Drawing.Color.White;
            this.btnRequestTrip.Location = new System.Drawing.Point(11, 417);
            this.btnRequestTrip.Name = "btnRequestTrip";
            this.btnRequestTrip.Size = new System.Drawing.Size(321, 53);
            this.btnRequestTrip.TabIndex = 1;
            this.btnRequestTrip.Text = "Đặt chuyến";
            this.btnRequestTrip.UseVisualStyleBackColor = false;
            this.btnRequestTrip.Click += new System.EventHandler(this.BtnRequestTrip_Click);
            // 
            // ucBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnRequestTrip);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ucBooking";
            this.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Size = new System.Drawing.Size(343, 482);
            this.tlpMain.ResumeLayout(false);
            this.pnlPickup.ResumeLayout(false);
            this.tlpPickup.ResumeLayout(false);
            this.pnlPickupButtons.ResumeLayout(false);
            this.pnlDropoff.ResumeLayout(false);
            this.tlpDropoff.ResumeLayout(false);
            this.pnlDropoffButtons.ResumeLayout(false);
            this.pnlVehicleSelector.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // ====================================================================
        // CÁC EVENT HANDLER CHO PAINT (VẼ HÌNH TRÒN CHO DOT)
        // ====================================================================
        private void PnlPickupDot_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Panel p)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(p.BackColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, p.Width - 1, p.Height - 1);
                }
            }
        }

        private void PnlDropoffDot_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Panel p)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(p.BackColor))
                {
                    e.Graphics.FillEllipse(brush, 0, 0, p.Width - 1, p.Height - 1);
                }
            }
        }

        #region Controls declaration
        private TableLayoutPanel tlpMain;
        private Panel pnlPickup;
        private Label lblPickupTitle; // Đổi kiểu dữ liệu
        private TableLayoutPanel tlpPickup;
        private Panel pnlPickupDot;
        private ucLocationPicker ucPickupPicker;
        private FlowLayoutPanel pnlPickupButtons;
        private Button btnClearPickup; // Đổi kiểu dữ liệu
        private Button btnLocatePickup; // Đổi kiểu dữ liệu
        private Panel pnlSpacer1;
        private Panel pnlDropoff;
        private Label lblDropoffTitle; // Đổi kiểu dữ liệu
        private TableLayoutPanel tlpDropoff;
        private Panel pnlDropoffDot;
        private ucLocationPicker ucDropoffPicker;
        private FlowLayoutPanel pnlDropoffButtons;
        private Button btnClearDropoff; // Đổi kiểu dữ liệu
        private Button btnLocateDropoff; // Đổi kiểu dữ liệu
        private Label lblEstimateInfo;
        private Panel pnlVehicleSelector;
        private ucFareSelector ucFareSelector;
        private Button btnRequestTrip; // Đổi kiểu dữ liệu
        #endregion
    }
}
