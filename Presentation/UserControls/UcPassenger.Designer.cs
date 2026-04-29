namespace Presentation.UserControls
{
    partial class UcPassenger
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.SplitContainer splitMain;
        private Presentation.Components.MapControl mapControl;

        // Right panel structure
        private System.Windows.Forms.TableLayoutPanel tblRight;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlActionStage;
        private System.Windows.Forms.Label lblStatus;

        // Stage panels
        private System.Windows.Forms.Panel pnlBooking;
        private Presentation.Components.LocationPickerControl pickupPicker;
        private Presentation.Components.LocationPickerControl destinationPicker;
        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Button btnBook;

        private System.Windows.Forms.Panel pnlSearching;
        private System.Windows.Forms.ProgressBar progressSearching;
        private System.Windows.Forms.Label lblSearching;
        private System.Windows.Forms.Button btnCancelSearch;

        private System.Windows.Forms.Panel pnlTracking;
        private Presentation.Components.TripStatusPanel tripStatusPanel;
        private Presentation.Components.DriverCardControl driverCard;
        private System.Windows.Forms.Button btnCancelTrip;

        private System.Windows.Forms.Panel pnlPayment;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.Button btnRateDriver;

        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.DataGridView dgvHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (disposing && (errorProvider != null))
            {
                errorProvider.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.errorProvider = new System.Windows.Forms.ErrorProvider();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.mapControl = new Presentation.Components.MapControl();
            this.tblRight = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            this.pnlActionStage = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            this.pnlBooking = new System.Windows.Forms.Panel();
            this.btnBook = new System.Windows.Forms.Button();
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.destinationPicker = new Presentation.Components.LocationPickerControl();
            this.pickupPicker = new Presentation.Components.LocationPickerControl();
            this.pnlSearching = new System.Windows.Forms.Panel();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.lblSearching = new System.Windows.Forms.Label();
            this.progressSearching = new System.Windows.Forms.ProgressBar();
            this.pnlTracking = new System.Windows.Forms.Panel();
            this.btnCancelTrip = new System.Windows.Forms.Button();
            this.driverCard = new Presentation.Components.DriverCardControl();
            this.tripStatusPanel = new Presentation.Components.TripStatusPanel();
            this.pnlPayment = new System.Windows.Forms.Panel();
            this.btnRateDriver = new System.Windows.Forms.Button();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.pnlHistory = new System.Windows.Forms.Panel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tblRight.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlActionStage.SuspendLayout();
            this.pnlBooking.SuspendLayout();
            this.pnlSearching.SuspendLayout();
            this.pnlTracking.SuspendLayout();
            this.pnlPayment.SuspendLayout();
            this.pnlHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.mapControl);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tblRight);
            this.splitMain.Size = new System.Drawing.Size(1200, 800);
            this.splitMain.SplitterDistance = 840;
            this.splitMain.TabIndex = 0;
            // 
            // mapControl
            // 
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.Location = new System.Drawing.Point(0, 0);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(840, 800);
            this.mapControl.TabIndex = 0;
            // 
            // tblRight
            // 
            this.tblRight.ColumnCount = 1;
            this.tblRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.Controls.Add(this.pnlHeader, 0, 0);
            this.tblRight.Controls.Add(this.pnlActionStage, 0, 1);
            this.tblRight.Controls.Add(this.lblStatus, 0, 2);
            this.tblRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRight.Location = new System.Drawing.Point(0, 0);
            this.tblRight.Name = "tblRight";
            this.tblRight.RowCount = 3;
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblRight.Size = new System.Drawing.Size(356, 800);
            this.tblRight.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.btnProfile);
            this.pnlHeader.Controls.Add(this.btnHistory);
            this.pnlHeader.Controls.Add(this.lblPassengerName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(350, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(296, 8);
            this.btnLogout.Name = "btnLogout";
this.btnLogout.Size = new System.Drawing.Size(80, 32);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Thoat";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            this.btnProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Location = new System.Drawing.Point(240, 8);
            this.btnProfile.Name = "btnProfile";
this.btnProfile.Size = new System.Drawing.Size(80, 32);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Ho so";
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Location = new System.Drawing.Point(184, 8);
            this.btnHistory.Name = "btnHistory";
this.btnHistory.Size = new System.Drawing.Size(80, 32);
            this.btnHistory.TabIndex = 1;
            this.btnHistory.Text = "Lich su";
            this.btnHistory.UseVisualStyleBackColor = true;
            // 
            // lblPassengerName
            // 
            this.lblPassengerName.AutoSize = true;
            this.lblPassengerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPassengerName.Location = new System.Drawing.Point(8, 12);
            this.lblPassengerName.Name = "lblPassengerName";
            this.lblPassengerName.Size = new System.Drawing.Size(93, 23);
            this.lblPassengerName.TabIndex = 0;
            this.lblPassengerName.Text = "Passenger";
            // 
            // pnlActionStage
            // 
            this.pnlActionStage.Controls.Add(this.pnlBooking);
            this.pnlActionStage.Controls.Add(this.pnlSearching);
            this.pnlActionStage.Controls.Add(this.pnlTracking);
            this.pnlActionStage.Controls.Add(this.pnlPayment);
            this.pnlActionStage.Controls.Add(this.pnlHistory);
            this.pnlActionStage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActionStage.Location = new System.Drawing.Point(3, 59);
            this.pnlActionStage.Name = "pnlActionStage";
            this.pnlActionStage.Size = new System.Drawing.Size(350, 706);
            this.pnlActionStage.TabIndex = 1;
            // 
            // pnlBooking
            // 
            this.pnlBooking.Controls.Add(this.btnBook);
            this.pnlBooking.Controls.Add(this.cmbVehicleType);
            this.pnlBooking.Controls.Add(this.destinationPicker);
            this.pnlBooking.Controls.Add(this.pickupPicker);
            this.pnlBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBooking.Location = new System.Drawing.Point(0, 0);
            this.pnlBooking.Name = "pnlBooking";
            this.pnlBooking.Padding = new System.Windows.Forms.Padding(8);
            this.pnlBooking.Size = new System.Drawing.Size(350, 706);
            this.pnlBooking.TabIndex = 0;
            // 
            // btnBook
            // 
            this.btnBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnBook.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBook.ForeColor = System.Drawing.Color.White;
            this.btnBook.Location = new System.Drawing.Point(8, 630);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(334, 68);
            this.btnBook.TabIndex = 3;
            this.btnBook.Text = "Dat xe";
            this.btnBook.UseVisualStyleBackColor = false;
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] {
            "Xe may",
            "O to"});
            this.cmbVehicleType.Location = new System.Drawing.Point(8, 240);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(334, 31);
            this.cmbVehicleType.TabIndex = 2;
            // 
            // destinationPicker
            // 
            this.destinationPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.destinationPicker.Location = new System.Drawing.Point(8, 120);
            this.destinationPicker.Name = "destinationPicker";
this.destinationPicker.Size = new System.Drawing.Size(334, 60);
            this.destinationPicker.TabIndex = 1;
            // 
            // pickupPicker
            // 
            this.pickupPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pickupPicker.Location = new System.Drawing.Point(8, 8);
            this.pickupPicker.Name = "pickupPicker";
this.pickupPicker.Size = new System.Drawing.Size(334, 60);
            this.pickupPicker.TabIndex = 0;
            // 
            // pnlSearching
            // 
            this.pnlSearching.Controls.Add(this.btnCancelSearch);
            this.pnlSearching.Controls.Add(this.lblSearching);
            this.pnlSearching.Controls.Add(this.progressSearching);
            this.pnlSearching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearching.Location = new System.Drawing.Point(0, 0);
            this.pnlSearching.Name = "pnlSearching";
            this.pnlSearching.Size = new System.Drawing.Size(350, 706);
            this.pnlSearching.TabIndex = 1;
            this.pnlSearching.Visible = false;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancelSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSearch.Location = new System.Drawing.Point(100, 200);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(150, 40);
            this.btnCancelSearch.TabIndex = 2;
            this.btnCancelSearch.Text = "Huy yeu cau";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            // 
            // lblSearching
            // 
            this.lblSearching.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSearching.AutoSize = true;
            this.lblSearching.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearching.Location = new System.Drawing.Point(80, 120);
            this.lblSearching.Name = "lblSearching";
            this.lblSearching.Size = new System.Drawing.Size(191, 28);
            this.lblSearching.TabIndex = 1;
            this.lblSearching.Text = "Dang tim tai xe...";
            // 
            // progressSearching
            // 
            this.progressSearching.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressSearching.Location = new System.Drawing.Point(40, 160);
            this.progressSearching.MarqueeAnimationSpeed = 30;
            this.progressSearching.Name = "progressSearching";
            this.progressSearching.Size = new System.Drawing.Size(270, 16);
            this.progressSearching.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressSearching.TabIndex = 0;
            // 
            // pnlTracking
            // 
            this.pnlTracking.Controls.Add(this.btnCancelTrip);
            this.pnlTracking.Controls.Add(this.driverCard);
            this.pnlTracking.Controls.Add(this.tripStatusPanel);
            this.pnlTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTracking.Location = new System.Drawing.Point(0, 0);
            this.pnlTracking.Name = "pnlTracking";
            this.pnlTracking.Size = new System.Drawing.Size(350, 706);
            this.pnlTracking.TabIndex = 2;
            this.pnlTracking.Visible = false;
            // 
            // btnCancelTrip
            // 
            this.btnCancelTrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnCancelTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTrip.ForeColor = System.Drawing.Color.White;
            this.btnCancelTrip.Location = new System.Drawing.Point(8, 660);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Size = new System.Drawing.Size(334, 40);
            this.btnCancelTrip.TabIndex = 2;
            this.btnCancelTrip.Text = "Huy chuyen";
            this.btnCancelTrip.UseVisualStyleBackColor = false;
            // 
            // driverCard
            // 
            this.driverCard.Location = new System.Drawing.Point(8, 120);
            this.driverCard.Name = "driverCard";
            this.driverCard.Size = new System.Drawing.Size(334, 120);
            this.driverCard.TabIndex = 1;
            // 
            // tripStatusPanel
            // 
            this.tripStatusPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tripStatusPanel.Location = new System.Drawing.Point(0, 0);
            this.tripStatusPanel.Name = "tripStatusPanel";
            this.tripStatusPanel.Size = new System.Drawing.Size(350, 100);
            this.tripStatusPanel.TabIndex = 0;
            // 
            // pnlPayment
            // 
            this.pnlPayment.Controls.Add(this.btnRateDriver);
            this.pnlPayment.Controls.Add(this.btnConfirmPayment);
            this.pnlPayment.Controls.Add(this.lblTotalAmount);
            this.pnlPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPayment.Location = new System.Drawing.Point(0, 0);
            this.pnlPayment.Name = "pnlPayment";
            this.pnlPayment.Size = new System.Drawing.Size(350, 706);
            this.pnlPayment.TabIndex = 3;
            this.pnlPayment.Visible = false;
            // 
            // btnRateDriver
            // 
            this.btnRateDriver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRateDriver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateDriver.Location = new System.Drawing.Point(100, 240);
            this.btnRateDriver.Name = "btnRateDriver";
            this.btnRateDriver.Size = new System.Drawing.Size(150, 40);
            this.btnRateDriver.TabIndex = 2;
            this.btnRateDriver.Text = "Danh gia tai xe";
            this.btnRateDriver.UseVisualStyleBackColor = true;
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConfirmPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnConfirmPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmPayment.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirmPayment.ForeColor = System.Drawing.Color.White;
            this.btnConfirmPayment.Location = new System.Drawing.Point(40, 160);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(270, 56);
            this.btnConfirmPayment.TabIndex = 1;
            this.btnConfirmPayment.Text = "Xac nhan thanh toan";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(100, 80);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(151, 46);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "0d";
            // 
            // pnlHistory
            // 
            this.pnlHistory.Controls.Add(this.dgvHistory);
            this.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistory.Location = new System.Drawing.Point(0, 0);
            this.pnlHistory.Name = "pnlHistory";
            this.pnlHistory.Size = new System.Drawing.Size(350, 706);
            this.pnlHistory.TabIndex = 4;
            this.pnlHistory.Visible = false;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(350, 706);
            this.dgvHistory.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(3, 771);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(350, 26);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "San sang";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcPassenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "UcPassenger";
            this.Size = new System.Drawing.Size(1200, 800);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tblRight.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlActionStage.ResumeLayout(false);
            this.pnlBooking.ResumeLayout(false);
            this.pnlSearching.ResumeLayout(false);
            this.pnlSearching.PerformLayout();
            this.pnlTracking.ResumeLayout(false);
            this.pnlPayment.ResumeLayout(false);
            this.pnlPayment.PerformLayout();
            this.pnlHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
