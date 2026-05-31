using System.Drawing;
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPickup = new System.Windows.Forms.Panel();
            this.tlpPickup = new System.Windows.Forms.TableLayoutPanel();
            this.txtPickup = new System.Windows.Forms.TextBox();
            this.pnlPickupButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearPickup = new System.Windows.Forms.Button();
            this.btnLocatePickup = new System.Windows.Forms.Button();
            this.lblPickupTitle = new System.Windows.Forms.Label();
            this.lblPickupAddressDetail = new System.Windows.Forms.Label();
            this.pnlSpacer1 = new System.Windows.Forms.Panel();
            this.pnlDropoff = new System.Windows.Forms.Panel();
            this.tlpDropoff = new System.Windows.Forms.TableLayoutPanel();
            this.txtDropoff = new System.Windows.Forms.TextBox();
            this.pnlDropoffButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearDropoff = new System.Windows.Forms.Button();
            this.btnLocateDropoff = new System.Windows.Forms.Button();
            this.lblDropoffTitle = new System.Windows.Forms.Label();
            this.lblDropoffAddressDetail = new System.Windows.Forms.Label();
            this.lblEstimateInfo = new System.Windows.Forms.Label();
            this.pnlVehicleSelector = new System.Windows.Forms.Panel();
            this.tlpVehicleOptions = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMoto = new System.Windows.Forms.Panel();
            this.tlpMoto = new System.Windows.Forms.TableLayoutPanel();
            this.lblMotoIcon = new System.Windows.Forms.Label();
            this.lblMotoPrice = new System.Windows.Forms.Label();
            this.pnlCar = new System.Windows.Forms.Panel();
            this.tlpCar = new System.Windows.Forms.TableLayoutPanel();
            this.lblCarIcon = new System.Windows.Forms.Label();
            this.lblCarPrice = new System.Windows.Forms.Label();
            this.btnRequestTrip = new System.Windows.Forms.Button();
            this.lstPickupSuggestions = new System.Windows.Forms.ListBox();
            this.lstDropoffSuggestions = new System.Windows.Forms.ListBox();
            this.tlpMain.SuspendLayout();
            this.pnlPickup.SuspendLayout();
            this.tlpPickup.SuspendLayout();
            this.pnlPickupButtons.SuspendLayout();
            this.pnlDropoff.SuspendLayout();
            this.tlpDropoff.SuspendLayout();
            this.pnlDropoffButtons.SuspendLayout();
            this.pnlVehicleSelector.SuspendLayout();
            this.tlpVehicleOptions.SuspendLayout();
            this.pnlMoto.SuspendLayout();
            this.tlpMoto.SuspendLayout();
            this.pnlCar.SuspendLayout();
            this.tlpCar.SuspendLayout();
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
            this.tlpMain.Controls.Add(this.btnRequestTrip, 0, 5);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(11, 12);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
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
            this.pnlPickup.Size = new System.Drawing.Size(321, 85);
            this.pnlPickup.TabIndex = 0;
            // 
            // tlpPickup
            // 
            this.tlpPickup.ColumnCount = 2;
            this.tlpPickup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPickup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpPickup.Controls.Add(this.txtPickup, 0, 0);
            this.tlpPickup.Controls.Add(this.pnlPickupButtons, 1, 0);
            this.tlpPickup.Controls.Add(this.lblPickupAddressDetail, 0, 1);
            this.tlpPickup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpPickup.Location = new System.Drawing.Point(0, 20);
            this.tlpPickup.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPickup.Name = "tlpPickup";
            this.tlpPickup.RowCount = 2;
            this.tlpPickup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpPickup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpPickup.Size = new System.Drawing.Size(321, 65);
            this.tlpPickup.TabIndex = 0;
            // 
            // txtPickup
            // 
            this.txtPickup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPickup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPickup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPickup.Location = new System.Drawing.Point(0, 5);
            this.txtPickup.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.txtPickup.Name = "txtPickup";
            this.txtPickup.Size = new System.Drawing.Size(235, 30);
            this.txtPickup.TabIndex = 1;
            // 
            // pnlPickupButtons
            // 
            this.pnlPickupButtons.Controls.Add(this.btnClearPickup);
            this.pnlPickupButtons.Controls.Add(this.btnLocatePickup);
            this.pnlPickupButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPickupButtons.Location = new System.Drawing.Point(241, 0);
            this.pnlPickupButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPickupButtons.Name = "pnlPickupButtons";
            this.pnlPickupButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlPickupButtons.Size = new System.Drawing.Size(80, 40);
            this.pnlPickupButtons.TabIndex = 2;
            // 
            // btnClearPickup
            // 
            this.btnClearPickup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearPickup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearPickup.FlatAppearance.BorderSize = 0;
            this.btnClearPickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPickup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearPickup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnClearPickup.Location = new System.Drawing.Point(2, 4);
            this.btnClearPickup.Margin = new System.Windows.Forms.Padding(2, 0, 4, 0);
            this.btnClearPickup.Name = "btnClearPickup";
            this.btnClearPickup.Size = new System.Drawing.Size(32, 32);
            this.btnClearPickup.TabIndex = 0;
            this.btnClearPickup.Text = "×";
            this.btnClearPickup.UseVisualStyleBackColor = false;
            this.btnClearPickup.Click += new System.EventHandler(this.BtnClearPickup_Click);
            // 
            // btnLocatePickup
            // 
            this.btnLocatePickup.BackColor = OOP2026.Colors.Green;
            this.btnLocatePickup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocatePickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocatePickup.Location = new System.Drawing.Point(40, 4);
            this.btnLocatePickup.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLocatePickup.Name = "btnLocatePickup";
            this.btnLocatePickup.Size = new System.Drawing.Size(32, 32);
            this.btnLocatePickup.TabIndex = 1;
            this.btnLocatePickup.Text = "📍";
            this.btnLocatePickup.UseVisualStyleBackColor = false;
            // 
            // lblPickupTitle
            // 
            this.lblPickupTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPickupTitle.ForeColor = OOP2026.Colors.Green;
            this.lblPickupTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPickupTitle.Name = "lblPickupTitle";
            this.lblPickupTitle.Size = new System.Drawing.Size(100, 20);
            this.lblPickupTitle.TabIndex = 1;
            this.lblPickupTitle.Text = "Điểm đón";
            // 
            // lblPickupAddressDetail
            // 
            this.lblPickupAddressDetail.AutoEllipsis = true;
            this.lblPickupAddressDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPickupAddressDetail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPickupAddressDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPickupAddressDetail.Location = new System.Drawing.Point(3, 40);
            this.lblPickupAddressDetail.Name = "lblPickupAddressDetail";
            this.lblPickupAddressDetail.Size = new System.Drawing.Size(235, 25);
            this.lblPickupAddressDetail.TabIndex = 3;
            this.lblPickupAddressDetail.Text = "Địa chỉ chi tiết điểm đón";
            this.lblPickupAddressDetail.Visible = false;
            // 
            // pnlSpacer1
            // 
            this.pnlSpacer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSpacer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSpacer1.Location = new System.Drawing.Point(0, 85);
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
            this.pnlDropoff.Location = new System.Drawing.Point(0, 91);
            this.pnlDropoff.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDropoff.Name = "pnlDropoff";
            this.pnlDropoff.Size = new System.Drawing.Size(321, 85);
            this.pnlDropoff.TabIndex = 2;
            // 
            // tlpDropoff
            // 
            this.tlpDropoff.ColumnCount = 2;
            this.tlpDropoff.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDropoff.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpDropoff.Controls.Add(this.txtDropoff, 0, 0);
            this.tlpDropoff.Controls.Add(this.pnlDropoffButtons, 1, 0);
            this.tlpDropoff.Controls.Add(this.lblDropoffAddressDetail, 0, 1);
            this.tlpDropoff.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpDropoff.Location = new System.Drawing.Point(0, 20);
            this.tlpDropoff.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDropoff.Name = "tlpDropoff";
            this.tlpDropoff.RowCount = 2;
            this.tlpDropoff.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDropoff.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDropoff.Size = new System.Drawing.Size(321, 65);
            this.tlpDropoff.TabIndex = 0;
            // 
            // txtDropoff
            // 
            this.txtDropoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDropoff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDropoff.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDropoff.Location = new System.Drawing.Point(0, 5);
            this.txtDropoff.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.txtDropoff.Name = "txtDropoff";
            this.txtDropoff.Size = new System.Drawing.Size(235, 30);
            this.txtDropoff.TabIndex = 1;
            // 
            // pnlDropoffButtons
            // 
            this.pnlDropoffButtons.Controls.Add(this.btnClearDropoff);
            this.pnlDropoffButtons.Controls.Add(this.btnLocateDropoff);
            this.pnlDropoffButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDropoffButtons.Location = new System.Drawing.Point(241, 0);
            this.pnlDropoffButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDropoffButtons.Name = "pnlDropoffButtons";
            this.pnlDropoffButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlDropoffButtons.Size = new System.Drawing.Size(80, 40);
            this.pnlDropoffButtons.TabIndex = 2;
            // 
            // btnClearDropoff
            // 
            this.btnClearDropoff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearDropoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearDropoff.FlatAppearance.BorderSize = 0;
            this.btnClearDropoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDropoff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearDropoff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnClearDropoff.Location = new System.Drawing.Point(2, 4);
            this.btnClearDropoff.Margin = new System.Windows.Forms.Padding(2, 0, 4, 0);
            this.btnClearDropoff.Name = "btnClearDropoff";
            this.btnClearDropoff.Size = new System.Drawing.Size(32, 32);
            this.btnClearDropoff.TabIndex = 0;
            this.btnClearDropoff.Text = "×";
            this.btnClearDropoff.UseVisualStyleBackColor = false;
            this.btnClearDropoff.Click += new System.EventHandler(this.BtnClearDropoff_Click);
            // 
            // btnLocateDropoff
            // 
            this.btnLocateDropoff.BackColor = OOP2026.Colors.Red;
            this.btnLocateDropoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocateDropoff.FlatAppearance.BorderSize = 0;
            this.btnLocateDropoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocateDropoff.Location = new System.Drawing.Point(40, 4);
            this.btnLocateDropoff.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLocateDropoff.Name = "btnLocateDropoff";
            this.btnLocateDropoff.Size = new System.Drawing.Size(32, 32);
            this.btnLocateDropoff.TabIndex = 1;
            this.btnLocateDropoff.Text = "📍";
            this.btnLocateDropoff.UseVisualStyleBackColor = false;
            // 
            // lblDropoffTitle
            // 
            this.lblDropoffTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDropoffTitle.ForeColor = OOP2026.Colors.Red;
            this.lblDropoffTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDropoffTitle.Name = "lblDropoffTitle";
            this.lblDropoffTitle.Size = new System.Drawing.Size(100, 20);
            this.lblDropoffTitle.TabIndex = 1;
            this.lblDropoffTitle.Text = "Điểm đến";
            // 
            // lblDropoffAddressDetail
            // 
            this.lblDropoffAddressDetail.AutoEllipsis = true;
            this.lblDropoffAddressDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropoffAddressDetail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDropoffAddressDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDropoffAddressDetail.Location = new System.Drawing.Point(3, 40);
            this.lblDropoffAddressDetail.Name = "lblDropoffAddressDetail";
            this.lblDropoffAddressDetail.Size = new System.Drawing.Size(235, 25);
            this.lblDropoffAddressDetail.TabIndex = 3;
            this.lblDropoffAddressDetail.Text = "Địa chỉ chi tiết điểm đến";
            this.lblDropoffAddressDetail.Visible = false;
            // 
            // lblEstimateInfo
            // 
            this.lblEstimateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstimateInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEstimateInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEstimateInfo.Location = new System.Drawing.Point(3, 176);
            this.lblEstimateInfo.Name = "lblEstimateInfo";
            this.lblEstimateInfo.Size = new System.Drawing.Size(315, 40);
            this.lblEstimateInfo.TabIndex = 3;
            this.lblEstimateInfo.Text = "📏 --- km   •   ⏱️ --- phút";
            this.lblEstimateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlVehicleSelector
            // 
            this.pnlVehicleSelector.Controls.Add(this.tlpVehicleOptions);
            this.pnlVehicleSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVehicleSelector.Location = new System.Drawing.Point(0, 216);
            this.pnlVehicleSelector.Margin = new System.Windows.Forms.Padding(0);
            this.pnlVehicleSelector.Name = "pnlVehicleSelector";
            this.pnlVehicleSelector.Size = new System.Drawing.Size(321, 192);
            this.pnlVehicleSelector.TabIndex = 4;
            // 
            // tlpVehicleOptions
            // 
            this.tlpVehicleOptions.ColumnCount = 1;
            this.tlpVehicleOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicleOptions.Controls.Add(this.pnlMoto, 0, 0);
            this.tlpVehicleOptions.Controls.Add(this.pnlCar, 0, 1);
            this.tlpVehicleOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpVehicleOptions.Location = new System.Drawing.Point(0, 0);
            this.tlpVehicleOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicleOptions.Name = "tlpVehicleOptions";
            this.tlpVehicleOptions.RowCount = 2;
            this.tlpVehicleOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicleOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicleOptions.Size = new System.Drawing.Size(321, 100);
            this.tlpVehicleOptions.TabIndex = 0;
            // 
            // pnlMoto
            // 
            this.pnlMoto.BackColor = System.Drawing.Color.White;
            this.pnlMoto.Controls.Add(this.tlpMoto);
            this.pnlMoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMoto.Location = new System.Drawing.Point(0, 0);
            this.pnlMoto.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlMoto.Name = "pnlMoto";
            this.pnlMoto.Size = new System.Drawing.Size(321, 46);
            this.pnlMoto.TabIndex = 0;
            this.pnlMoto.Click += new System.EventHandler(this.PnlMoto_Click);
            // 
            // tlpMoto
            // 
            this.tlpMoto.ColumnCount = 2;
            this.tlpMoto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpMoto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpMoto.Controls.Add(this.lblMotoIcon, 0, 0);
            this.tlpMoto.Controls.Add(this.lblMotoPrice, 1, 0);
            this.tlpMoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMoto.Location = new System.Drawing.Point(0, 0);
            this.tlpMoto.Name = "tlpMoto";
            this.tlpMoto.Padding = new System.Windows.Forms.Padding(8, 0, 12, 0);
            this.tlpMoto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMoto.Size = new System.Drawing.Size(321, 46);
            this.tlpMoto.TabIndex = 0;
            // 
            // lblMotoIcon
            // 
            this.lblMotoIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotoIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMotoIcon.ForeColor = System.Drawing.Color.Black;
            this.lblMotoIcon.Location = new System.Drawing.Point(11, 0);
            this.lblMotoIcon.Name = "lblMotoIcon";
            this.lblMotoIcon.Size = new System.Drawing.Size(189, 46);
            this.lblMotoIcon.TabIndex = 0;
            this.lblMotoIcon.Text = "🏍️ Xe máy";
            this.lblMotoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMotoIcon.Click += new System.EventHandler(this.PnlMoto_Click);
            // 
            // lblMotoPrice
            // 
            this.lblMotoPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotoPrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMotoPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblMotoPrice.Location = new System.Drawing.Point(206, 0);
            this.lblMotoPrice.Name = "lblMotoPrice";
            this.lblMotoPrice.Size = new System.Drawing.Size(100, 46);
            this.lblMotoPrice.TabIndex = 1;
            this.lblMotoPrice.Text = "0đ";
            this.lblMotoPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMotoPrice.Click += new System.EventHandler(this.PnlMoto_Click);
            // 
            // pnlCar
            // 
            this.pnlCar.BackColor = System.Drawing.Color.White;
            this.pnlCar.Controls.Add(this.tlpCar);
            this.pnlCar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCar.Location = new System.Drawing.Point(0, 54);
            this.pnlCar.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlCar.Name = "pnlCar";
            this.pnlCar.Size = new System.Drawing.Size(321, 46);
            this.pnlCar.TabIndex = 1;
            this.pnlCar.Click += new System.EventHandler(this.PnlCar_Click);
            // 
            // tlpCar
            // 
            this.tlpCar.ColumnCount = 2;
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpCar.Controls.Add(this.lblCarIcon, 0, 0);
            this.tlpCar.Controls.Add(this.lblCarPrice, 1, 0);
            this.tlpCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCar.Location = new System.Drawing.Point(0, 0);
            this.tlpCar.Name = "tlpCar";
            this.tlpCar.Padding = new System.Windows.Forms.Padding(8, 0, 12, 0);
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCar.Size = new System.Drawing.Size(321, 46);
            this.tlpCar.TabIndex = 0;
            // 
            // lblCarIcon
            // 
            this.lblCarIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarIcon.ForeColor = System.Drawing.Color.Black;
            this.lblCarIcon.Location = new System.Drawing.Point(11, 0);
            this.lblCarIcon.Name = "lblCarIcon";
            this.lblCarIcon.Size = new System.Drawing.Size(189, 46);
            this.lblCarIcon.TabIndex = 0;
            this.lblCarIcon.Text = "🚕 Xe ô tô";
            this.lblCarIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCarIcon.Click += new System.EventHandler(this.PnlCar_Click);
            // 
            // lblCarPrice
            // 
            this.lblCarPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarPrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCarPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblCarPrice.Location = new System.Drawing.Point(206, 0);
            this.lblCarPrice.Name = "lblCarPrice";
            this.lblCarPrice.Size = new System.Drawing.Size(100, 46);
            this.lblCarPrice.TabIndex = 1;
            this.lblCarPrice.Text = "0đ";
            this.lblCarPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCarPrice.Click += new System.EventHandler(this.PnlCar_Click);
            // 
            // btnRequestTrip
            // 
            this.btnRequestTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnRequestTrip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRequestTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRequestTrip.FlatAppearance.BorderSize = 0;
            this.btnRequestTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequestTrip.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRequestTrip.ForeColor = System.Drawing.Color.White;
            this.btnRequestTrip.Location = new System.Drawing.Point(0, 412);
            this.btnRequestTrip.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnRequestTrip.Name = "btnRequestTrip";
            this.btnRequestTrip.Size = new System.Drawing.Size(321, 46);
            this.btnRequestTrip.TabIndex = 1;
            this.btnRequestTrip.Text = "ĐẶT CHUYẾN XE";
            this.btnRequestTrip.UseVisualStyleBackColor = false;
            this.btnRequestTrip.Click += new System.EventHandler(this.BtnRequestTrip_Click);
            // 
            // lstPickupSuggestions
            // 
            this.lstPickupSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPickupSuggestions.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstPickupSuggestions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lstPickupSuggestions.FormattingEnabled = true;
            this.lstPickupSuggestions.ItemHeight = 21;
            this.lstPickupSuggestions.Location = new System.Drawing.Point(14, 65);
            this.lstPickupSuggestions.Name = "lstPickupSuggestions";
            this.lstPickupSuggestions.Size = new System.Drawing.Size(235, 107);
            this.lstPickupSuggestions.TabIndex = 5;
            this.lstPickupSuggestions.Visible = false;
            // 
            // lstDropoffSuggestions
            // 
            this.lstDropoffSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDropoffSuggestions.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lstDropoffSuggestions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lstDropoffSuggestions.FormattingEnabled = true;
            this.lstDropoffSuggestions.ItemHeight = 21;
            this.lstDropoffSuggestions.Location = new System.Drawing.Point(14, 131);
            this.lstDropoffSuggestions.Name = "lstDropoffSuggestions";
            this.lstDropoffSuggestions.Size = new System.Drawing.Size(235, 107);
            this.lstDropoffSuggestions.TabIndex = 6;
            this.lstDropoffSuggestions.Visible = false;
            // 
            // ucBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lstPickupSuggestions);
            this.Controls.Add(this.lstDropoffSuggestions);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ucBooking";
            this.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Size = new System.Drawing.Size(343, 482);
            this.tlpMain.ResumeLayout(false);
            this.pnlPickup.ResumeLayout(false);
            this.tlpPickup.ResumeLayout(false);
            this.tlpPickup.PerformLayout();
            this.pnlPickupButtons.ResumeLayout(false);
            this.pnlDropoff.ResumeLayout(false);
            this.tlpDropoff.ResumeLayout(false);
            this.tlpDropoff.PerformLayout();
            this.pnlDropoffButtons.ResumeLayout(false);
            this.pnlVehicleSelector.ResumeLayout(false);
            this.tlpVehicleOptions.ResumeLayout(false);
            this.pnlMoto.ResumeLayout(false);
            this.tlpMoto.ResumeLayout(false);
            this.pnlCar.ResumeLayout(false);
            this.tlpCar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // ========== Khai báo control (đã bỏ ucFareSelector) ==========
        private TableLayoutPanel tlpMain;
        private Panel pnlPickup;
        private Label lblPickupTitle;
        private TableLayoutPanel tlpPickup;
        private TextBox txtPickup;
        private ListBox lstPickupSuggestions;
        private FlowLayoutPanel pnlPickupButtons;
        private Button btnClearPickup;
        private Button btnLocatePickup;
        private Label lblPickupAddressDetail;

        private Panel pnlSpacer1;
        private Panel pnlDropoff;
        private Label lblDropoffTitle;
        private TableLayoutPanel tlpDropoff;
        private TextBox txtDropoff;
        private ListBox lstDropoffSuggestions;
        private FlowLayoutPanel pnlDropoffButtons;
        private Button btnClearDropoff;
        private Button btnLocateDropoff;
        private Label lblDropoffAddressDetail;

        private Label lblEstimateInfo;
        private Panel pnlVehicleSelector;

        // Các control mới cho chọn xe
        private TableLayoutPanel tlpVehicleOptions;
        private Panel pnlMoto;
        private TableLayoutPanel tlpMoto;
        private Label lblMotoIcon;
        private Label lblMotoPrice;
        private Panel pnlCar;
        private TableLayoutPanel tlpCar;
        private Label lblCarIcon;
        private Label lblCarPrice;

        private Button btnRequestTrip;
    }
}
