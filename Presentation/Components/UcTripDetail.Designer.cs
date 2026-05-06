





namespace Presentation.Components
{
    partial class UcTripDetail
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayout;
protected System.Windows.Forms.Label lblTripId;
protected System.Windows.Forms.Label lblPickup;
protected System.Windows.Forms.Label lblDestination;
protected System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.Label lblRequestTime;

        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTripId = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.lblFare = new System.Windows.Forms.Label();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.lblRequestTime = new System.Windows.Forms.Label();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.lblTripId, 0, 0);
            this.tableLayout.Controls.Add(this.lblStatus, 0, 1);
            this.tableLayout.Controls.Add(this.lblPickup, 0, 2);
            this.tableLayout.Controls.Add(this.lblDestination, 0, 3);
            this.tableLayout.Controls.Add(this.lblVehicleType, 0, 4);
            this.tableLayout.Controls.Add(this.lblFare, 0, 5);
            this.tableLayout.Controls.Add(this.lblDriverName, 0, 6);
            this.tableLayout.Controls.Add(this.lblPassengerName, 0, 7);
            this.tableLayout.Controls.Add(this.lblRequestTime, 0, 8);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayout.RowCount = 9;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.Size = new System.Drawing.Size(480, 320);
            this.tableLayout.TabIndex = 0;
            // 
            // lblTripId
            // 
            this.lblTripId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTripId.Location = new System.Drawing.Point(15, 12);
            this.lblTripId.Name = "lblTripId";
            this.lblTripId.Size = new System.Drawing.Size(100, 23);
            this.lblTripId.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.TabIndex = 1;
            // 
            // lblPickup
            // 
            this.lblPickup.Location = new System.Drawing.Point(15, 68);
            this.lblPickup.Name = "lblPickup";
            this.lblPickup.Size = new System.Drawing.Size(100, 23);
            this.lblPickup.TabIndex = 2;
            // 
            // lblDestination
            // 
            this.lblDestination.Location = new System.Drawing.Point(15, 96);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(100, 23);
            this.lblDestination.TabIndex = 3;
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.Location = new System.Drawing.Point(15, 124);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(100, 23);
            this.lblVehicleType.TabIndex = 4;
            // 
            // lblFare
            // 
            this.lblFare.Location = new System.Drawing.Point(15, 152);
            this.lblFare.Name = "lblFare";
            this.lblFare.Size = new System.Drawing.Size(100, 23);
            this.lblFare.TabIndex = 5;
            // 
            // lblDriverName
            // 
            this.lblDriverName.Location = new System.Drawing.Point(15, 180);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(100, 23);
            this.lblDriverName.TabIndex = 6;
            // 
            // lblPassengerName
            // 
            this.lblPassengerName.Location = new System.Drawing.Point(15, 208);
            this.lblPassengerName.Name = "lblPassengerName";
            this.lblPassengerName.Size = new System.Drawing.Size(100, 23);
            this.lblPassengerName.TabIndex = 7;
            // 
            // lblRequestTime
            // 
            this.lblRequestTime.Location = new System.Drawing.Point(15, 236);
            this.lblRequestTime.Name = "lblRequestTime";
            this.lblRequestTime.Size = new System.Drawing.Size(100, 23);
            this.lblRequestTime.TabIndex = 8;
            // 
            // UcTripDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Controls.Add(this.tableLayout);
            this.Name = "UcTripDetail";
            this.Size = new System.Drawing.Size(480, 320);
            this.tableLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}





