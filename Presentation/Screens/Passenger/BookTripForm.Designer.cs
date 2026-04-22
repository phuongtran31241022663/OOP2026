namespace Presentation.Screens.Passenger
{
    partial class BookTripForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._mapControl = new Presentation.Components.MapControl();
            this._sidebar = new System.Windows.Forms.Panel();
            this.sidebarTitle = new System.Windows.Forms.Label();
            this.locationGroup = new System.Windows.Forms.GroupBox();
            this.pickupRow = new System.Windows.Forms.TableLayoutPanel();
            this.pickupSlotLabel = new System.Windows.Forms.Label();
            this._pickupPicker = new Presentation.Components.LocationPickerControl();
            this.destinationRow = new System.Windows.Forms.TableLayoutPanel();
            this.destinationSlotLabel = new System.Windows.Forms.Label();
            this._destinationPicker = new Presentation.Components.LocationPickerControl();
            this.vehicleGroup = new System.Windows.Forms.GroupBox();
            this._vehicleCombo = new System.Windows.Forms.ComboBox();
            this.fareGroup = new System.Windows.Forms.GroupBox();
            this._fareLabel = new System.Windows.Forms.Label();
            this._distanceLabel = new System.Windows.Forms.Label();
            this._durationLabel = new System.Windows.Forms.Label();
            this.nearbyGroup = new System.Windows.Forms.GroupBox();
            this._nearbyLabel = new System.Windows.Forms.Label();
            this._driverCard = new System.Windows.Forms.Panel();
            this._driverNameLabel = new System.Windows.Forms.Label();
            this._driverPhoneLabel = new System.Windows.Forms.Label();
            this._driverReviewLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this._cancelBtn = new System.Windows.Forms.Button();
            this._requestBtn = new System.Windows.Forms.Button();
            this._suggestions = new System.Windows.Forms.ListBox();
            this._statusBar = new System.Windows.Forms.Panel();
            this._statusLabel = new System.Windows.Forms.Label();
            this.rootLayout.SuspendLayout();
            this._sidebar.SuspendLayout();
            this.locationGroup.SuspendLayout();
            this.pickupRow.SuspendLayout();
            this.destinationRow.SuspendLayout();
            this.vehicleGroup.SuspendLayout();
            this.fareGroup.SuspendLayout();
            this.nearbyGroup.SuspendLayout();
            this._driverCard.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this._statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.ColumnCount = 2;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.rootLayout.Controls.Add(this._mapControl, 0, 0);
            this.rootLayout.Controls.Add(this._sidebar, 1, 0);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 1;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Size = new System.Drawing.Size(1024, 640);
            this.rootLayout.TabIndex = 0;
            // 
            // _mapControl
            // 
            this._mapControl.BackColor = System.Drawing.SystemColors.Control;
            this._mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapControl.Location = new System.Drawing.Point(3, 3);
            this._mapControl.Name = "_mapControl";
            this._mapControl.Size = new System.Drawing.Size(710, 634);
            this._mapControl.TabIndex = 0;
            this._mapControl.MapClicked += OnMapClicked;
            // 
            // _sidebar
            // 
            this._sidebar.BackColor = System.Drawing.SystemColors.Control;
            this._sidebar.Controls.Add(this._driverCard);
            this._sidebar.Controls.Add(this.nearbyGroup);
            this._sidebar.Controls.Add(this.fareGroup);
            this._sidebar.Controls.Add(this.vehicleGroup);
            this._sidebar.Controls.Add(this.locationGroup);
            this._sidebar.Controls.Add(this.sidebarTitle);
            this._sidebar.Controls.Add(this._suggestions);
            this._sidebar.Controls.Add(this.buttonPanel);
            this._sidebar.Controls.Add(this._statusBar);
            this._sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sidebar.Location = new System.Drawing.Point(719, 3);
            this._sidebar.Name = "_sidebar";
            this._sidebar.Padding = new System.Windows.Forms.Padding(8);
            this._sidebar.Size = new System.Drawing.Size(302, 634);
            this._sidebar.TabIndex = 1;
            // 
            // sidebarTitle
            // 
            this.sidebarTitle.BackColor = System.Drawing.SystemColors.ControlDark;
            this.sidebarTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidebarTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.sidebarTitle.Location = new System.Drawing.Point(8, 8);
            this.sidebarTitle.Name = "sidebarTitle";
            this.sidebarTitle.Size = new System.Drawing.Size(286, 28);
            this.sidebarTitle.TabIndex = 0;
            this.sidebarTitle.Text = "Đặt chuyến đi";
            this.sidebarTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // locationGroup
            // 
            this.locationGroup.Controls.Add(this.destinationRow);
            this.locationGroup.Controls.Add(this.pickupRow);
            this.locationGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.locationGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.locationGroup.Location = new System.Drawing.Point(8, 36);
            this.locationGroup.Name = "locationGroup";
            this.locationGroup.Padding = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.locationGroup.Size = new System.Drawing.Size(286, 110);
            this.locationGroup.TabIndex = 1;
            this.locationGroup.TabStop = false;
            this.locationGroup.Text = "Địa điểm";
            // 
            // pickupRow
            // 
            this.pickupRow.ColumnCount = 2;
            this.pickupRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pickupRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pickupRow.Controls.Add(this.pickupSlotLabel, 0, 0);
            this.pickupRow.Controls.Add(this._pickupPicker, 1, 0);
            this.pickupRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pickupRow.Location = new System.Drawing.Point(4, 29);
            this.pickupRow.Name = "pickupRow";
            this.pickupRow.RowCount = 1;
            this.pickupRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pickupRow.Size = new System.Drawing.Size(278, 28);
            this.pickupRow.TabIndex = 0;
            // 
            // pickupSlotLabel
            // 
            this.pickupSlotLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pickupSlotLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pickupSlotLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.pickupSlotLabel.Location = new System.Drawing.Point(3, 0);
            this.pickupSlotLabel.Name = "pickupSlotLabel";
            this.pickupSlotLabel.Size = new System.Drawing.Size(14, 28);
            this.pickupSlotLabel.TabIndex = 0;
            this.pickupSlotLabel.Text = "A";
            this.pickupSlotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _pickupPicker
            // 
            this._pickupPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pickupPicker.Location = new System.Drawing.Point(23, 3);
            this._pickupPicker.Name = "_pickupPicker";
            this._pickupPicker.Placeholder = "Điểm đón";
            this._pickupPicker.Size = new System.Drawing.Size(252, 22);
            this._pickupPicker.TabIndex = 1;
            this._pickupPicker.LocationSelected += OnPickupSelected;
            // 
            // destinationRow
            // 
            this.destinationRow.ColumnCount = 2;
            this.destinationRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.destinationRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.destinationRow.Controls.Add(this.destinationSlotLabel, 0, 0);
            this.destinationRow.Controls.Add(this._destinationPicker, 1, 0);
            this.destinationRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.destinationRow.Location = new System.Drawing.Point(4, 57);
            this.destinationRow.Name = "destinationRow";
            this.destinationRow.RowCount = 1;
            this.destinationRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.destinationRow.Size = new System.Drawing.Size(278, 28);
            this.destinationRow.TabIndex = 1;
            // 
            // destinationSlotLabel
            // 
            this.destinationSlotLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.destinationSlotLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.destinationSlotLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.destinationSlotLabel.Location = new System.Drawing.Point(3, 0);
            this.destinationSlotLabel.Name = "destinationSlotLabel";
            this.destinationSlotLabel.Size = new System.Drawing.Size(14, 28);
            this.destinationSlotLabel.TabIndex = 0;
            this.destinationSlotLabel.Text = "B";
            this.destinationSlotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _destinationPicker
            // 
            this._destinationPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this._destinationPicker.Location = new System.Drawing.Point(23, 3);
            this._destinationPicker.Name = "_destinationPicker";
            this._destinationPicker.Placeholder = "Điểm đến";
            this._destinationPicker.Size = new System.Drawing.Size(252, 22);
            this._destinationPicker.TabIndex = 1;
            this._destinationPicker.LocationSelected += OnDestinationLocationSelected;
            // 
            // vehicleGroup
            // 
            this.vehicleGroup.Controls.Add(this._vehicleCombo);
            this.vehicleGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.vehicleGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.vehicleGroup.Location = new System.Drawing.Point(8, 146);
            this.vehicleGroup.Name = "vehicleGroup";
            this.vehicleGroup.Padding = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.vehicleGroup.Size = new System.Drawing.Size(286, 52);
            this.vehicleGroup.TabIndex = 2;
            this.vehicleGroup.TabStop = false;
            this.vehicleGroup.Text = "Loại xe";
            // 
            // _vehicleCombo
            // 
            this._vehicleCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._vehicleCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this._vehicleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._vehicleCombo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._vehicleCombo.FormattingEnabled = true;
            this._vehicleCombo.Items.AddRange(new object[] {
            "Motorbike",
            "Car"});
            this._vehicleCombo.Location = new System.Drawing.Point(4, 29);
            this._vehicleCombo.Name = "_vehicleCombo";
            this._vehicleCombo.Size = new System.Drawing.Size(278, 28);
            this._vehicleCombo.TabIndex = 0;
            this._vehicleCombo.SelectedIndexChanged += new System.EventHandler(this.OnVehicleChanged);
            // 
            // fareGroup
            // 
            this.fareGroup.Controls.Add(this._durationLabel);
            this.fareGroup.Controls.Add(this._distanceLabel);
            this.fareGroup.Controls.Add(this._fareLabel);
            this.fareGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.fareGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.fareGroup.Location = new System.Drawing.Point(8, 198);
            this.fareGroup.Name = "fareGroup";
            this.fareGroup.Padding = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.fareGroup.Size = new System.Drawing.Size(286, 92);
            this.fareGroup.TabIndex = 3;
            this.fareGroup.TabStop = false;
            this.fareGroup.Text = "Ước tính";
            // 
            // _fareLabel
            // 
            this._fareLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._fareLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._fareLabel.Location = new System.Drawing.Point(4, 29);
            this._fareLabel.Name = "_fareLabel";
            this._fareLabel.Size = new System.Drawing.Size(278, 18);
            this._fareLabel.TabIndex = 0;
            this._fareLabel.Text = "Giá: --";
            // 
            // _distanceLabel
            // 
            this._distanceLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._distanceLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._distanceLabel.Location = new System.Drawing.Point(4, 47);
            this._distanceLabel.Name = "_distanceLabel";
            this._distanceLabel.Size = new System.Drawing.Size(278, 18);
            this._distanceLabel.TabIndex = 1;
            this._distanceLabel.Text = "Khoảng cách: --";
            // 
            // _durationLabel
            // 
            this._durationLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._durationLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._durationLabel.Location = new System.Drawing.Point(4, 65);
            this._durationLabel.Name = "_durationLabel";
            this._durationLabel.Size = new System.Drawing.Size(278, 18);
            this._durationLabel.TabIndex = 2;
            this._durationLabel.Text = "Thời gian: --";
            // 
            // nearbyGroup
            // 
            this.nearbyGroup.Controls.Add(this._nearbyLabel);
            this.nearbyGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.nearbyGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nearbyGroup.Location = new System.Drawing.Point(8, 290);
            this.nearbyGroup.Name = "nearbyGroup";
            this.nearbyGroup.Padding = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.nearbyGroup.Size = new System.Drawing.Size(286, 48);
            this.nearbyGroup.TabIndex = 4;
            this.nearbyGroup.TabStop = false;
            this.nearbyGroup.Text = "Tài xế gần đây";
            // 
            // _nearbyLabel
            // 
            this._nearbyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._nearbyLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._nearbyLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this._nearbyLabel.Location = new System.Drawing.Point(4, 29);
            this._nearbyLabel.Name = "_nearbyLabel";
            this._nearbyLabel.Size = new System.Drawing.Size(278, 18);
            this._nearbyLabel.TabIndex = 0;
            this._nearbyLabel.Text = "Chưa có dữ liệu";
            // 
            // _driverCard
            // 
            this._driverCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this._driverCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._driverCard.Controls.Add(this._driverReviewLabel);
            this._driverCard.Controls.Add(this._driverPhoneLabel);
            this._driverCard.Controls.Add(this._driverNameLabel);
            this._driverCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverCard.Location = new System.Drawing.Point(8, 338);
            this._driverCard.Name = "_driverCard";
            this._driverCard.Padding = new System.Windows.Forms.Padding(6);
            this._driverCard.Size = new System.Drawing.Size(286, 80);
            this._driverCard.TabIndex = 5;
            this._driverCard.Visible = false;
            // 
            // _driverNameLabel
            // 
            this._driverNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverNameLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverNameLabel.Location = new System.Drawing.Point(6, 6);
            this._driverNameLabel.Name = "_driverNameLabel";
            this._driverNameLabel.Size = new System.Drawing.Size(272, 18);
            this._driverNameLabel.TabIndex = 0;
            this._driverNameLabel.Text = "Tài xế: --";
            // 
            // _driverPhoneLabel
            // 
            this._driverPhoneLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverPhoneLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverPhoneLabel.Location = new System.Drawing.Point(6, 24);
            this._driverPhoneLabel.Name = "_driverPhoneLabel";
            this._driverPhoneLabel.Size = new System.Drawing.Size(272, 18);
            this._driverPhoneLabel.TabIndex = 1;
            this._driverPhoneLabel.Text = "SĐT: --";
            // 
            // _driverReviewLabel
            // 
            this._driverReviewLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverReviewLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverReviewLabel.Location = new System.Drawing.Point(6, 42);
            this._driverReviewLabel.Name = "_driverReviewLabel";
            this._driverReviewLabel.Size = new System.Drawing.Size(272, 18);
            this._driverReviewLabel.TabIndex = 2;
            this._driverReviewLabel.Text = "Đánh giá: --";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this._cancelBtn);
            this.buttonPanel.Controls.Add(this._requestBtn);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(8, 558);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.buttonPanel.Size = new System.Drawing.Size(286, 34);
            this.buttonPanel.TabIndex = 7;
            // 
            // _cancelBtn
            // 
            this._cancelBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this._cancelBtn.ForeColor = System.Drawing.Color.DarkRed;
            this._cancelBtn.Location = new System.Drawing.Point(0, 4);
            this._cancelBtn.Name = "_cancelBtn";
            this._cancelBtn.Size = new System.Drawing.Size(90, 30);
            this._cancelBtn.TabIndex = 0;
            this._cancelBtn.Text = "Hủy chuyến";
            this._cancelBtn.UseVisualStyleBackColor = true;
            this._cancelBtn.Visible = false;
            this._cancelBtn.Click += new System.EventHandler(this.OnCancelClicked);
            // 
            // _requestBtn
            // 
            this._requestBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this._requestBtn.Location = new System.Drawing.Point(196, 4);
            this._requestBtn.Name = "_requestBtn";
            this._requestBtn.Size = new System.Drawing.Size(90, 30);
            this._requestBtn.TabIndex = 1;
            this._requestBtn.Text = "Đặt xe ngay";
            this._requestBtn.UseVisualStyleBackColor = true;
            this._requestBtn.Click += new System.EventHandler(this.OnRequestClicked);
            // 
            // _suggestions
            // 
            this._suggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._suggestions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._suggestions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._suggestions.FormattingEnabled = true;
            this._suggestions.IntegralHeight = false;
            this._suggestions.ItemHeight = 28;
            this._suggestions.Location = new System.Drawing.Point(8, 418);
            this._suggestions.Name = "_suggestions";
            this._suggestions.Size = new System.Drawing.Size(286, 140);
            this._suggestions.TabIndex = 6;
            this._suggestions.Visible = false;
            this._suggestions.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.OnDrawSuggestionItem);
            this._suggestions.SelectedIndexChanged += new System.EventHandler(this.OnSuggestionSelected);
            // 
            // _statusBar
            // 
            this._statusBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this._statusBar.Controls.Add(this._statusLabel);
            this._statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusBar.Location = new System.Drawing.Point(8, 592);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(286, 34);
            this._statusBar.TabIndex = 8;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._statusLabel.Location = new System.Drawing.Point(0, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._statusLabel.Size = new System.Drawing.Size(286, 34);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "Sẵn sàng";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BookTripForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this.rootLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "BookTripForm";
            this.Text = "Book Trip";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BookTripForm_Load);
            this.rootLayout.ResumeLayout(false);
            this._sidebar.ResumeLayout(false);
            this.locationGroup.ResumeLayout(false);
            this.pickupRow.ResumeLayout(false);
            this.destinationRow.ResumeLayout(false);
            this.vehicleGroup.ResumeLayout(false);
            this.fareGroup.ResumeLayout(false);
            this.nearbyGroup.ResumeLayout(false);
            this._driverCard.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this._statusBar.ResumeLayout(false);
            this.ResumeLayout(false);

            this._vehicleCombo.SelectedIndex = 0;
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private Presentation.Components.MapControl _mapControl;
        private System.Windows.Forms.Panel _sidebar;
        private System.Windows.Forms.Label sidebarTitle;
        private System.Windows.Forms.GroupBox locationGroup;
        private System.Windows.Forms.TableLayoutPanel pickupRow;
        private System.Windows.Forms.Label pickupSlotLabel;
        private Presentation.Components.LocationPickerControl _pickupPicker;
        private System.Windows.Forms.TableLayoutPanel destinationRow;
        private System.Windows.Forms.Label destinationSlotLabel;
        private Presentation.Components.LocationPickerControl _destinationPicker;
        private System.Windows.Forms.GroupBox vehicleGroup;
        private System.Windows.Forms.ComboBox _vehicleCombo;
        private System.Windows.Forms.GroupBox fareGroup;
        private System.Windows.Forms.Label _fareLabel;
        private System.Windows.Forms.Label _distanceLabel;
        private System.Windows.Forms.Label _durationLabel;
        private System.Windows.Forms.GroupBox nearbyGroup;
        private System.Windows.Forms.Label _nearbyLabel;
        private System.Windows.Forms.Panel _driverCard;
        private System.Windows.Forms.Label _driverNameLabel;
        private System.Windows.Forms.Label _driverPhoneLabel;
        private System.Windows.Forms.Label _driverReviewLabel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button _cancelBtn;
        private System.Windows.Forms.Button _requestBtn;
        private System.Windows.Forms.ListBox _suggestions;
        private System.Windows.Forms.Panel _statusBar;
        private System.Windows.Forms.Label _statusLabel;
    }
}
