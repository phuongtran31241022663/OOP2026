namespace Presentation.Components
{
    partial class StatusPanel : BaseUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox _logTextBox;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.Label _errorCountLabel;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _viewLogButton;
        private System.Windows.Forms.Panel _headerPanel;

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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the content of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._headerPanel = new System.Windows.Forms.Panel();
            this._statusLabel = new System.Windows.Forms.Label();
            this._errorCountLabel = new System.Windows.Forms.Label();
            this._clearButton = new System.Windows.Forms.Button();
            this._viewLogButton = new System.Windows.Forms.Button();
            this._logTextBox = new System.Windows.Forms.RichTextBox();
            this._headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._viewLogButton);
            this._headerPanel.Controls.Add(this._clearButton);
            this._headerPanel.Controls.Add(this._errorCountLabel);
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(400, 32);
            this._headerPanel.TabIndex = 0;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.Green;
            this._statusLabel.Location = new System.Drawing.Point(8, 8);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(65, 15);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "● Sẵn sàng";
            // 
            // _errorCountLabel
            // 
            this._errorCountLabel.AutoSize = true;
            this._errorCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._errorCountLabel.ForeColor = System.Drawing.Color.White;
            this._errorCountLabel.Location = new System.Drawing.Point(180, 8);
            this._errorCountLabel.Name = "_errorCountLabel";
            this._errorCountLabel.Size = new System.Drawing.Size(98, 15);
            this._errorCountLabel.TabIndex = 1;
            this._errorCountLabel.Text = "Lỗi: 0 | Cảnh báo: 0";
            // 
            // _clearButton
            // 
            this._clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this._clearButton.ForeColor = System.Drawing.Color.White;
            this._clearButton.Location = new System.Drawing.Point(380, 4);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(75, 24);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "Xóa log";
            this._clearButton.UseVisualStyleBackColor = true;
            // 
            // _viewLogButton
            // 
            this._viewLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewLogButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this._viewLogButton.ForeColor = System.Drawing.Color.White;
            this._viewLogButton.Location = new System.Drawing.Point(455, 4);
            this._viewLogButton.Name = "_viewLogButton";
            this._viewLogButton.Size = new System.Drawing.Size(90, 24);
            this._viewLogButton.TabIndex = 3;
            this._viewLogButton.Text = "Mở file log";
            this._viewLogButton.UseVisualStyleBackColor = true;
            // 
            // _logTextBox
            // 
            this._logTextBox.BackColor = System.Drawing.Color.Black;
            this._logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logTextBox.Font = new System.Drawing.Font("Consolas", 8.25F);
            this._logTextBox.ForeColor = System.Drawing.Color.LightGray;
            this._logTextBox.Location = new System.Drawing.Point(0, 32);
            this._logTextBox.Name = "_logTextBox";
            this._logTextBox.ReadOnly = true;
            this._logTextBox.Size = new System.Drawing.Size(400, 118);
            this._logTextBox.TabIndex = 1;
            this._logTextBox.Text = "";
            // 
            // StatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._logTextBox);
            this.Controls.Add(this._headerPanel);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "StatusPanel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(400, 150);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
