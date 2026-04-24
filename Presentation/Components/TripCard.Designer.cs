namespace Presentation.Components
{
    partial class TripCard
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // Status indicator (colored bar)
            _statusIndicator = new System.Windows.Forms.Panel
            {
                Width = 4,
                Height = 70,
                Location = new System.Drawing.Point(0, 0),
                BackColor = System.Drawing.Color.Blue
            };

            // Status label
            _lblStatus = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(16, 8),
                AutoSize = true,
                Text = "Chờ xử lý"
            };

            // Route label
            _lblRoute = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 9),
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(16, 30),
                AutoSize = false,
                Width = 320,
                Height = 18,
                Text = "Điểm đón → Điểm đến"
            };

            // Info label (fare, vehicle type)
            _lblInfo = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 8),
                ForeColor = System.Drawing.Color.Gray,
                Location = new System.Drawing.Point(16, 52),
                AutoSize = true,
                Text = "15.000 VNĐ • Xe máy"
            };

            // Time label
            _lblTime = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 8),
                ForeColor = System.Drawing.Color.DarkGray,
                Location = new System.Drawing.Point(250, 52),
                AutoSize = true,
                Text = "10:30"
            };

            // Main container
            this.Size = new System.Drawing.Size(360, 70);
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);

            this.Controls.Add(_statusIndicator);
            this.Controls.Add(_lblStatus);
            this.Controls.Add(_lblRoute);
            this.Controls.Add(_lblInfo);
            this.Controls.Add(_lblTime);
        }

        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Label _lblRoute;
        private System.Windows.Forms.Label _lblInfo;
        private System.Windows.Forms.Label _lblTime;
        private System.Windows.Forms.Panel _statusIndicator;        #endregion
    }
}
