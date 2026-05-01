using Presentation.Constants;

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
            this.components = new System.ComponentModel.Container();
            this._iconPanel = new System.Windows.Forms.Panel();
            this._lblName = new System.Windows.Forms.Label();
            this._lblAddress = new System.Windows.Forms.Label();
            this._lblCoords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _iconPanel
            // 
            this._iconPanel.BackColor = Presentation.Constants.UiConstants.Colors.Success;
            this._iconPanel.Location = new System.Drawing.Point(12, 12);
            this._iconPanel.Name = "_iconPanel";
            this._iconPanel.Size = new System.Drawing.Size(Presentation.Constants.UiConstants.Cards.IconSize, Presentation.Constants.UiConstants.Cards.IconSize);
            this._iconPanel.TabIndex = 0;
            // 
            // _iconLabel
            // 
            System.Windows.Forms.Label iconLabel = new System.Windows.Forms.Label();
            iconLabel.Text = "📍";
            iconLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            iconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            iconLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._iconPanel.Controls.Add(iconLabel);
            // 
            // _lblName
            // 
            this._lblName.Font = Presentation.Constants.UiConstants.Cards.Typography.Title;
            this._lblName.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            this._lblName.Location = new System.Drawing.Point(60, 10);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(200, 22);
            this._lblName.TabIndex = 1;
            this._lblName.Text = "Tên địa điểm";
            // 
            // _lblAddress
            // 
            this._lblAddress.Font = Presentation.Constants.UiConstants.Cards.Typography.Body;
            this._lblAddress.ForeColor = Presentation.Constants.UiConstants.Colors.TextSecondary;
            this._lblAddress.Location = new System.Drawing.Point(60, 32);
            this._lblAddress.Name = "_lblAddress";
            this._lblAddress.Size = new System.Drawing.Size(280, 16);
            this._lblAddress.TabIndex = 2;
            this._lblAddress.Text = "Địa chỉ chi tiết";
            // 
            // _lblCoords
            // 
            this._lblCoords.Font = Presentation.Constants.UiConstants.Cards.Typography.Info;
            this._lblCoords.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;
            this._lblCoords.Location = new System.Drawing.Point(60, 50);
            this._lblCoords.Name = "_lblCoords";
            this._lblCoords.Size = new System.Drawing.Size(100, 14);
            this._lblCoords.TabIndex = 3;
            // 
            // LocationCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Presentation.Constants.UiConstants.Colors.SurfaceWhite;
            this.Controls.Add(this._iconPanel);
            this.Controls.Add(this._lblName);
            this.Controls.Add(this._lblAddress);
            this.Controls.Add(this._lblCoords);
            this.Name = "LocationCard";
            this.Size = new System.Drawing.Size(Presentation.Constants.UiConstants.Cards.DefaultWidth, Presentation.Constants.UiConstants.Cards.CompactHeight);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblAddress;
        private System.Windows.Forms.Label _lblCoords;
        private System.Windows.Forms.Panel _iconPanel;
        #endregion
    }
}
