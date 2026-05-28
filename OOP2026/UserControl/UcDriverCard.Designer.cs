namespace OOP2026
{
    partial class ucDriverCard
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
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.btnCall = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lblAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAvatar
            // 
            this.lblAvatar.Location = new System.Drawing.Point(14, 14);
            this.lblAvatar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Text = "??";
            this.lblAvatar.Size = new System.Drawing.Size(64, 66);
            this.lblAvatar.TabIndex = 0;
            this.lblAvatar.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(89, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Tên tài x?";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(89, 40);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(82, 20);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "090xxxxxxx";
            // 
            // lblVehicle
            // 
            this.lblVehicle.AutoSize = true;
            this.lblVehicle.Location = new System.Drawing.Point(89, 61);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(88, 20);
            this.lblVehicle.TabIndex = 3;
            this.lblVehicle.Text = "Xe - Bi?n s?";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(183, 61);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(45, 20);
            this.lblRating.TabIndex = 4;
            this.lblRating.Text = "? 0.0";
            // 
            // btnCall
            // 
            this.btnCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCall.FlatAppearance.BorderSize = 0;
            this.btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCall.Location = new System.Drawing.Point(346, 33);
            this.btnCall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(80, 35);
            this.btnCall.TabIndex = 5;
            this.btnCall.Text = "?? G?i";
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.BtnCall_Click);
            // 
            // ucDriverCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.lblVehicle);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblAvatar);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 9);
            this.Name = "ucDriverCard";
            this.Size = new System.Drawing.Size(457, 89);
            ((System.ComponentModel.ISupportInitialize)(this.lblAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Controls declaration
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Button btnCall;
        #endregion
    }
}
