namespace Presentation.Components
{
    partial class UcFareVehicleSelector
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the content of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.lblFareAmount = new System.Windows.Forms.Label();
            this.lblVehicleLabel = new System.Windows.Forms.Label();
            this.lblFareLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] {
            "Xe máy",
            "Ô tô"});
            this.cmbVehicleType.Location = new System.Drawing.Point(0, 25);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(120, 24);
            this.cmbVehicleType.TabIndex = 0;
            this.cmbVehicleType.SelectedIndexChanged += new System.EventHandler(this.cmbVehicleType_SelectedIndexChanged);
            // 
            // lblFareAmount
            // 
            this.lblFareAmount.AutoSize = true;
            this.lblFareAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFareAmount.ForeColor = System.Drawing.Color.FromArgb(0, 150, 136);
            this.lblFareAmount.Location = new System.Drawing.Point(130, 20);
            this.lblFareAmount.Name = "lblFareAmount";
            this.lblFareAmount.Size = new System.Drawing.Size(80, 25);
            this.lblFareAmount.TabIndex = 1;
            this.lblFareAmount.Text = "---đ";
            // 
            // lblVehicleLabel
            // 
            this.lblVehicleLabel.AutoSize = true;
            this.lblVehicleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVehicleLabel.Location = new System.Drawing.Point(0, 0);
            this.lblVehicleLabel.Name = "lblVehicleLabel";
            this.lblVehicleLabel.Size = new System.Drawing.Size(54, 15);
            this.lblVehicleLabel.TabIndex = 2;
            this.lblVehicleLabel.Text = "Loại xe:";
            // 
            // lblFareLabel
            // 
            this.lblFareLabel.AutoSize = true;
            this.lblFareLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFareLabel.Location = new System.Drawing.Point(130, 0);
            this.lblFareLabel.Name = "lblFareLabel";
            this.lblFareLabel.Size = new System.Drawing.Size(42, 15);
            this.lblFareLabel.TabIndex = 3;
            this.lblFareLabel.Text = "Giá:";
            // 
            // UcFareVehicleSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFareLabel);
            this.Controls.Add(this.lblVehicleLabel);
            this.Controls.Add(this.lblFareAmount);
            this.Controls.Add(this.cmbVehicleType);
            this.Name = "UcFareVehicleSelector";
            this.Size = new System.Drawing.Size(220, 50);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Label lblFareAmount;
        private System.Windows.Forms.Label lblVehicleLabel;
        private System.Windows.Forms.Label lblFareLabel;
    }
}