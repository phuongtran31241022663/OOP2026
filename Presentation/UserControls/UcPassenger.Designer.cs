namespace Presentation.UserControls
{
    using Presentation.Constants;
    using System.Windows.Forms;
    // cái cmb chọn loại xe nên gộp với thằng hiện giá
    partial class UcPassenger
    {
        private System.ComponentModel.IContainer components = null;
        private Presentation.Components.UcFareVehicleSelector farePanel;
        private System.Windows.Forms.SplitContainer splitMain;
        private Presentation.Components.UcMap mapControl;

        // Navigation and Layout
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.TableLayoutPanel tblRight;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.Panel pnlActionStage;
        private System.Windows.Forms.Label lblStatus;

        // Stage panels
        private System.Windows.Forms.Panel pnlBooking;
        private System.Windows.Forms.Label lblPickup;
        private Presentation.Components.UcLocationPicker pickupPicker;
        private System.Windows.Forms.Label lblDestination;
        private Presentation.Components.UcLocationPicker destinationPicker;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Button btnBook;

        private System.Windows.Forms.Panel pnlSearching;
        private System.Windows.Forms.ProgressBar progressSearching;
        private System.Windows.Forms.Label lblSearching;
        private System.Windows.Forms.Button btnCancelSearch;

        private System.Windows.Forms.Panel pnlTracking;
private Presentation.Components.TripStatusPanel tripStatusPanel;
        private Presentation.Components.UcDriverCard driverCard;
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

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.mapControl = new Presentation.Components.UcMap();
            this.tblRight = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.pnlActionStage = new System.Windows.Forms.Panel();
            this.pnlBooking = new System.Windows.Forms.Panel();
            this.btnBook = new System.Windows.Forms.Button();
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.destinationPicker = new Presentation.Components.UcLocationPicker();
            this.lblDestination = new System.Windows.Forms.Label();
            this.pickupPicker = new Presentation.Components.UcLocationPicker();
            this.lblPickup = new System.Windows.Forms.Label();
            this.pnlSearching = new System.Windows.Forms.Panel();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.lblSearching = new System.Windows.Forms.Label();
            this.progressSearching = new System.Windows.Forms.ProgressBar();
            this.pnlTracking = new System.Windows.Forms.Panel();
            this.btnCancelTrip = new System.Windows.Forms.Button();
            this.driverCard = new Presentation.Components.UcDriverCard();
this.tripStatusPanel = new Presentation.Components.TripStatusPanel();
            this.pnlPayment = new System.Windows.Forms.Panel();
            this.btnRateDriver = new System.Windows.Forms.Button();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.pnlHistory = new System.Windows.Forms.Panel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.farePanel = new Presentation.Components.UcFareVehicleSelector();
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
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // farePanel
            // 
            this.farePanel.BackColor = System.Drawing.Color.White;
            this.farePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.farePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.farePanel.Location = new System.Drawing.Point(0, 703);
            this.farePanel.Name = "farePanel";
            this.farePanel.Size = new System.Drawing.Size(768, 50);
            this.farePanel.TabIndex = 1;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.mapControl);
            this.splitMain.Panel1.Controls.Add(this.farePanel);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tblRight);
            this.splitMain.Size = new System.Drawing.Size(1200, 753);
            this.splitMain.SplitterDistance = 768;
            this.splitMain.TabIndex = 0;
            // 
            // mapControl
            // 
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.Location = new System.Drawing.Point(0, 0);
            this.mapControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(768, 753);
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
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblRight.Size = new System.Drawing.Size(428, 753);
            this.tblRight.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPassengerName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(422, 47);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblPassengerName
            // 
            this.lblPassengerName.AutoSize = true;
            this.lblPassengerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPassengerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblPassengerName.Location = new System.Drawing.Point(12, 11);
            this.lblPassengerName.Name = "lblPassengerName";
            this.lblPassengerName.Size = new System.Drawing.Size(117, 25);
            this.lblPassengerName.TabIndex = 0;
            this.lblPassengerName.Text = "Hành khách";
            // 
            // pnlActionStage
            // 
            this.pnlActionStage.Controls.Add(this.pnlBooking);
            this.pnlActionStage.Controls.Add(this.pnlSearching);
            this.pnlActionStage.Controls.Add(this.pnlTracking);
            this.pnlActionStage.Controls.Add(this.pnlPayment);
            this.pnlActionStage.Controls.Add(this.pnlHistory);
            this.pnlActionStage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActionStage.Location = new System.Drawing.Point(3, 56);
            this.pnlActionStage.Name = "pnlActionStage";
            this.pnlActionStage.Size = new System.Drawing.Size(422, 664);
            this.pnlActionStage.TabIndex = 1;
            // 
            // pnlBooking
            // 
            this.pnlBooking.Controls.Add(this.btnBook);
            this.pnlBooking.Controls.Add(this.cmbVehicleType);
            this.pnlBooking.Controls.Add(this.lblVehicleType);
            this.pnlBooking.Controls.Add(this.destinationPicker);
            this.pnlBooking.Controls.Add(this.lblDestination);
            this.pnlBooking.Controls.Add(this.pickupPicker);
            this.pnlBooking.Controls.Add(this.lblPickup);
            this.pnlBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBooking.Location = new System.Drawing.Point(0, 0);
            this.pnlBooking.Name = "pnlBooking";
            this.pnlBooking.Padding = new System.Windows.Forms.Padding(8);
            this.pnlBooking.Size = new System.Drawing.Size(422, 664);
            this.pnlBooking.TabIndex = 0;
            // 
            // btnBook
            // 
            this.btnBook.AutoSize = true;
            this.btnBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnBook.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBook.ForeColor = System.Drawing.Color.White;
            this.btnBook.Location = new System.Drawing.Point(8, 586);
            this.btnBook.MinimumSize = new System.Drawing.Size(200, 48);
            this.btnBook.Name = "btnBook";
            this.btnBook.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnBook.Size = new System.Drawing.Size(406, 70);
            this.btnBook.TabIndex = 3;
            this.btnBook.Text = "Đặt xe";
            this.btnBook.UseVisualStyleBackColor = false;
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] {
            "Xe máy",
            "Ô tô"});
            this.cmbVehicleType.Location = new System.Drawing.Point(8, 311);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(334, 31);
            this.cmbVehicleType.TabIndex = 2;
            this.cmbVehicleType.SelectedIndexChanged += new System.EventHandler(this.cmbVehicleType_SelectedIndexChanged);
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleType.Location = new System.Drawing.Point(8, 268);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(67, 23);
            this.lblVehicleType.TabIndex = 4;
            this.lblVehicleType.Text = "Loại xe:";
            // 
            // destinationPicker
            // 
            this.destinationPicker.BackColor = System.Drawing.Color.White;
            this.destinationPicker.CurrentDestination = null;
            this.destinationPicker.CurrentPickup = null;
            this.destinationPicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.destinationPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.destinationPicker.ForeColor = System.Drawing.Color.Black;
            this.destinationPicker.Location = new System.Drawing.Point(8, 102);
            this.destinationPicker.Name = "destinationPicker";
            this.destinationPicker.SelectedLocation = null;
            this.destinationPicker.Size = new System.Drawing.Size(406, 34);
            this.destinationPicker.SlotLabel = "";
            this.destinationPicker.TabIndex = 1;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDestination.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDestination.Location = new System.Drawing.Point(8, 67);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblDestination.Size = new System.Drawing.Size(74, 35);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Text = "Đến đâu?";
            // 
            // pickupPicker
            // 
            this.pickupPicker.BackColor = System.Drawing.Color.White;
            this.pickupPicker.CurrentDestination = null;
            this.pickupPicker.CurrentPickup = null;
            this.pickupPicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pickupPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pickupPicker.ForeColor = System.Drawing.Color.Black;
            this.pickupPicker.Location = new System.Drawing.Point(8, 33);
            this.pickupPicker.Name = "pickupPicker";
            this.pickupPicker.SelectedLocation = null;
            this.pickupPicker.Size = new System.Drawing.Size(406, 34);
            this.pickupPicker.SlotLabel = "";
            this.pickupPicker.TabIndex = 0;
            this.pickupPicker.Load += new System.EventHandler(this.pickupPicker_Load);
            // 
            // lblPickup
            // 
            this.lblPickup.AutoSize = true;
            this.lblPickup.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPickup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPickup.Location = new System.Drawing.Point(8, 8);
            this.lblPickup.Name = "lblPickup";
            this.lblPickup.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblPickup.Size = new System.Drawing.Size(65, 25);
            this.lblPickup.TabIndex = 6;
            this.lblPickup.Text = "Từ đâu?";
            // 
            // pnlSearching
            // 
            this.pnlSearching.Controls.Add(this.btnCancelSearch);
            this.pnlSearching.Controls.Add(this.lblSearching);
            this.pnlSearching.Controls.Add(this.progressSearching);
            this.pnlSearching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearching.Location = new System.Drawing.Point(0, 0);
            this.pnlSearching.Name = "pnlSearching";
            this.pnlSearching.Size = new System.Drawing.Size(422, 664);
            this.pnlSearching.TabIndex = 1;
            this.pnlSearching.Visible = false;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancelSearch.AutoSize = true;
            this.btnCancelSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSearch.Location = new System.Drawing.Point(136, 188);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnCancelSearch.Size = new System.Drawing.Size(150, 40);
            this.btnCancelSearch.TabIndex = 2;
            this.btnCancelSearch.Text = "Huỷ yêu cầu";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            // 
            // lblSearching
            // 
            this.lblSearching.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSearching.AutoSize = true;
            this.lblSearching.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearching.Location = new System.Drawing.Point(116, 113);
            this.lblSearching.Name = "lblSearching";
            this.lblSearching.Size = new System.Drawing.Size(156, 28);
            this.lblSearching.TabIndex = 1;
            this.lblSearching.Text = "Đang tìm tài xế...";
            // 
            // progressSearching
            // 
            this.progressSearching.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressSearching.Location = new System.Drawing.Point(76, 151);
            this.progressSearching.MarqueeAnimationSpeed = 30;
            this.progressSearching.Name = "progressSearching";
            this.progressSearching.Size = new System.Drawing.Size(270, 15);
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
            this.pnlTracking.Size = new System.Drawing.Size(422, 664);
            this.pnlTracking.TabIndex = 2;
            this.pnlTracking.Visible = false;
            // 
            // btnCancelTrip
            // 
            this.btnCancelTrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCancelTrip.Location = new System.Drawing.Point(0, 169);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnCancelTrip.Size = new System.Drawing.Size(422, 38);
            this.btnCancelTrip.TabIndex = 2;
            this.btnCancelTrip.Text = "Huỷ chuyến";
            this.btnCancelTrip.UseVisualStyleBackColor = false;
            // 
            // driverCard
            // 
            this.driverCard.BackColor = System.Drawing.Color.White;
            this.driverCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.driverCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.driverCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.driverCard.Location = new System.Drawing.Point(0, 94);
            this.driverCard.Name = "driverCard";
            this.driverCard.Size = new System.Drawing.Size(422, 75);
            this.driverCard.TabIndex = 3;
            // 
            // tripStatusPanel
            // 
            this.tripStatusPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tripStatusPanel.Location = new System.Drawing.Point(0, 0);
            this.tripStatusPanel.Name = "tripStatusPanel";
            this.tripStatusPanel.Size = new System.Drawing.Size(422, 94);
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
            this.pnlPayment.Size = new System.Drawing.Size(422, 664);
            this.pnlPayment.TabIndex = 3;
            this.pnlPayment.Visible = false;
            // 
            // btnRateDriver
            // 
            this.btnRateDriver.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRateDriver.AutoSize = true;
            this.btnRateDriver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRateDriver.Location = new System.Drawing.Point(136, 226);
            this.btnRateDriver.Name = "btnRateDriver";
            this.btnRateDriver.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnRateDriver.Size = new System.Drawing.Size(150, 40);
            this.btnRateDriver.TabIndex = 2;
            this.btnRateDriver.Text = "Đánh giá tài xế";
            this.btnRateDriver.UseVisualStyleBackColor = true;
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConfirmPayment.AutoSize = true;
            this.btnConfirmPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnConfirmPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmPayment.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmPayment.ForeColor = System.Drawing.Color.White;
            this.btnConfirmPayment.Location = new System.Drawing.Point(76, 151);
            this.btnConfirmPayment.MinimumSize = new System.Drawing.Size(200, 48);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnConfirmPayment.Size = new System.Drawing.Size(270, 70);
            this.btnConfirmPayment.TabIndex = 1;
            this.btnConfirmPayment.Text = "Xác nhận thanh toán";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(136, 75);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(62, 46);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "0đ";
            // 
            // pnlHistory
            // 
            this.pnlHistory.Controls.Add(this.dgvHistory);
            this.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistory.Location = new System.Drawing.Point(0, 0);
            this.pnlHistory.Name = "pnlHistory";
            this.pnlHistory.Size = new System.Drawing.Size(422, 664);
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
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(422, 664);
            this.dgvHistory.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(3, 723);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(422, 30);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Sẵn sàng";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnProfile);
            this.pnlSidebar.Controls.Add(this.btnHistory);
            this.pnlSidebar.Controls.Add(this.btnMenu);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 753);
            this.pnlSidebar.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 150);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(200, 50);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "⏻  Thoát";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            this.btnProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProfile.ForeColor = System.Drawing.Color.White;
            this.btnProfile.Location = new System.Drawing.Point(0, 100);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnProfile.Size = new System.Drawing.Size(200, 50);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "👤  Hồ sơ";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // btnHistory
            // 
            this.btnHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location = new System.Drawing.Point(0, 50);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHistory.Size = new System.Drawing.Size(200, 50);
            this.btnHistory.TabIndex = 1;
            this.btnHistory.Text = "🕒  Lịch sử";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistory.UseVisualStyleBackColor = true;
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnMenu.Size = new System.Drawing.Size(200, 50);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "☰  Menu";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.UseVisualStyleBackColor = true;
            // 
            // UcPassenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.splitMain);
            this.Name = "UcPassenger";
            this.Size = new System.Drawing.Size(1200, 753);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tblRight.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlActionStage.ResumeLayout(false);
            this.pnlBooking.ResumeLayout(false);
            this.pnlBooking.PerformLayout();
            this.pnlSearching.ResumeLayout(false);
            this.pnlSearching.PerformLayout();
            this.pnlTracking.ResumeLayout(false);
            this.pnlPayment.ResumeLayout(false);
            this.pnlPayment.PerformLayout();
            this.pnlHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
