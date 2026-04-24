namespace Presentation.Components
{
    partial class LocationCard
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

            // Icon panel
            _iconPanel = new System.Windows.Forms.Panel
            {
                Width = 40,
                Height = 40,
                BackColor = System.Drawing.Color.Green,
                Location = new System.Drawing.Point(12, 12)
            };
            var iconLabel = new System.Windows.Forms.Label
            {
                Text = "📍",
                Font = new System.Drawing.Font("Segoe UI", 16),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            _iconPanel.Controls.Add(iconLabel);

            // Name label
            _lblName = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Black,
                AutoSize = false,
                Location = new System.Drawing.Point(60, 10),
                Width = 200,
                Height = 20,
                Text = "Tên địa điểm"
            };

            // Address label
            _lblAddress = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 9),
                ForeColor = System.Drawing.Color.Gray,
                AutoSize = false,
                Location = new System.Drawing.Point(60, 32),
                Width = 280,
                Height = 16,
                Text = "Địa chỉ chi tiết"
            };

            // Coordinates label
            _lblCoords = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Segoe UI", 8),
                ForeColor = System.Drawing.Color.DarkGray,
                AutoSize = false,
                Location = new System.Drawing.Point(60, 50),
                Width = 100,
                Height = 14,
                Text = ""
            };

            // Main container
            this.Size = new System.Drawing.Size(360, 64);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(_iconPanel);
            this.Controls.Add(_lblName);
            this.Controls.Add(_lblAddress);
            this.Controls.Add(_lblCoords);
        }
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblAddress;
        private System.Windows.Forms.Label _lblCoords;
        private System.Windows.Forms.Panel _iconPanel;
        #endregion
    }
}
