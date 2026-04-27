namespace Presentation.UserControls
{
    partial class UcTripDetail
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblTripId;
        private System.Windows.Forms.Label lblPickup;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.Label lblFare;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label lblPassengerName;
        private System.Windows.Forms.Label lblRequestTime;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTripId = new System.Windows.Forms.Label();
            this.lblPickup = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.lblFare = new System.Windows.Forms.Label();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblPassengerName = new System.Windows.Forms.Label();
            this.lblRequestTime = new System.Windows.Forms.Label();

            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.RowCount = 9;
            this.tableLayout.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));

            this.lblTripId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tableLayout.Controls.Add(this.lblTripId, 0, 0);

            this.tableLayout.Controls.Add(this.lblStatus, 0, 1);
            this.tableLayout.Controls.Add(this.lblPickup, 0, 2);
            this.tableLayout.Controls.Add(this.lblDestination, 0, 3);
            this.tableLayout.Controls.Add(this.lblVehicleType, 0, 4);
            this.tableLayout.Controls.Add(this.lblFare, 0, 5);
            this.tableLayout.Controls.Add(this.lblDriverName, 0, 6);
            this.tableLayout.Controls.Add(this.lblPassengerName, 0, 7);
            this.tableLayout.Controls.Add(this.lblRequestTime, 0, 8);

            this.Controls.Add(this.tableLayout);
            this.Name = "UcTripDetail";
            this.Size = new System.Drawing.Size(480, 320);
        }
    }
}
