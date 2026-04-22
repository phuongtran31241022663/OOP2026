using System.Windows.Forms;

namespace Presentation.Components
{
    partial class DriverCardControl
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
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._statusIndicator = new System.Windows.Forms.Panel();
            this._vehicleIcon = new System.Windows.Forms.PictureBox();
            this._lblName = new System.Windows.Forms.Label();
            this._lblPhone = new System.Windows.Forms.Label();
            this._lblReview = new System.Windows.Forms.Label();
            this._lblStatus = new System.Windows.Forms.Label();
            this._lblVehicle = new System.Windows.Forms.Label();
            this._lblDistance = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._vehicleIcon)).BeginInit();
            this.SuspendLayout();
            //
            // _statusIndicator
            //
            this._statusIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._statusIndicator.Location = new System.Drawing.Point(8, 8);
            this._statusIndicator.Name = "_statusIndicator";
            this._statusIndicator.Size = new System.Drawing.Size(12, 12);
            this._statusIndicator.TabIndex = 0;
            //
            // _vehicleIcon
            //
            this._vehicleIcon.BackColor = System.Drawing.Color.LightGray;
            this._vehicleIcon.Location = new System.Drawing.Point(8, 30);
            this._vehicleIcon.Name = "_vehicleIcon";
            this._vehicleIcon.Size = new System.Drawing.Size(32, 32);
            this._vehicleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._vehicleIcon.TabIndex = 1;
            this._vehicleIcon.TabStop = false;
            //
            // _lblName
            //
            this._lblName.AutoSize = true;
            this._lblName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblName.ForeColor = System.Drawing.Color.Black;
            this._lblName.Location = new System.Drawing.Point(50, 8);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(77, 20);
            this._lblName.TabIndex = 2;
            this._lblName.Text = "Tên tài xế";
            //
            // _lblPhone
            //
            this._lblPhone.AutoSize = true;
            this._lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblPhone.ForeColor = System.Drawing.Color.Gray;
            this._lblPhone.Location = new System.Drawing.Point(50, 30);
            this._lblPhone.Name = "_lblPhone";
            this._lblPhone.Size = new System.Drawing.Size(70, 15);
            this._lblPhone.TabIndex = 3;
            this._lblPhone.Text = "0123 456 789";
            //
            // _lblReview
            //
            this._lblReview.AutoSize = true;
            this._lblReview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblReview.ForeColor = System.Drawing.Color.Orange;
            this._lblReview.Location = new System.Drawing.Point(50, 50);
            this._lblReview.Name = "_lblName";
            this._lblReview.Size = new System.Drawing.Size(28, 15);
            this._lblReview.TabIndex = 4;
            this._lblReview.Text = "★ 4.8";
            //
            // _lblStatus
            //
            this._lblStatus.AutoSize = true;
            this._lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._lblStatus.Location = new System.Drawing.Point(150, 8);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(53, 13);
            this._lblStatus.TabIndex = 5;
            this._lblStatus.Text = "Trạng thái";
            //
            // _lblVehicle
            //
            this._lblVehicle.AutoSize = true;
            this._lblVehicle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._lblVehicle.ForeColor = System.Drawing.Color.DarkGray;
            this._lblVehicle.Location = new System.Drawing.Point(150, 30);
            this._lblVehicle.Name = "_lblVehicle";
            this._lblVehicle.Size = new System.Drawing.Size(75, 13);
            this._lblVehicle.TabIndex = 6;
            this._lblVehicle.Text = "Xe máy • ABC-123";
            //
            // _lblDistance
            //
            this._lblDistance.AutoSize = true;
            this._lblDistance.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._lblDistance.ForeColor = System.Drawing.Color.Blue;
            this._lblDistance.Location = new System.Drawing.Point(150, 50);
            this._lblDistance.Name = "_lblDistance";
            this._lblDistance.Size = new System.Drawing.Size(28, 13);
            this._lblDistance.TabIndex = 7;
            this._lblDistance.Text = "2.3 km";
            //
            // DriverCardControl
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._lblDistance);
            this.Controls.Add(this._lblVehicle);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this._lblReview);
            this.Controls.Add(this._lblPhone);
            this.Controls.Add(this._lblName);
            this.Controls.Add(this._vehicleIcon);
            this.Controls.Add(this._statusIndicator);
            this.Name = "DriverCardControl";
            this.Size = new System.Drawing.Size(320, 80);
            ((System.ComponentModel.ISupportInitialize)(this._vehicleIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            // Enable click events
            this.Cursor = Cursors.Hand;
            this.Click += OnCardClick;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Cursor = Cursors.Hand;
                ctrl.Click += OnCardClick;
            }

            // Hover effect
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

        #endregion

        private System.Windows.Forms.Panel _statusIndicator;
        private System.Windows.Forms.PictureBox _vehicleIcon;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblPhone;
        private System.Windows.Forms.Label _lblReview;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Label _lblVehicle;
        private System.Windows.Forms.Label _lblDistance;
    }
}
