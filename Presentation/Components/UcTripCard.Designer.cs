using Presentation.Constants;

namespace Presentation.Components
{
    partial class UcTripCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._statusIndicator = new System.Windows.Forms.Panel();
            this._lblStatus = new System.Windows.Forms.Label();
            this._lblRoute = new System.Windows.Forms.Label();
            this._lblInfo = new System.Windows.Forms.Label();
            this._lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _statusIndicator
            // 
            this._statusIndicator.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this._statusIndicator.Location = new System.Drawing.Point(0, 0);
            this._statusIndicator.Name = "_statusIndicator";
            this._statusIndicator.Size = new System.Drawing.Size(Presentation.Constants.UiConstants.Cards.IndicatorWidth, Presentation.Constants.UiConstants.Cards.StandardHeight);
            this._statusIndicator.TabIndex = 0;
            // 
            // _lblStatus
            // 
            this._lblStatus.AutoSize = true;
            this._lblStatus.Font = Presentation.Constants.UiConstants.Cards.Fonts.BodyBold;
            this._lblStatus.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            this._lblStatus.Location = new System.Drawing.Point(16, 8);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(89, 20);
            this._lblStatus.TabIndex = 1;
this._lblStatus.Text = "Đang xử lý";
            // 
            // _lblRoute
            // 
            this._lblRoute.Font = Presentation.Constants.UiConstants.Cards.Fonts.Body;
            this._lblRoute.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            this._lblRoute.Location = new System.Drawing.Point(16, 30);
            this._lblRoute.Name = "_lblRoute";
            this._lblRoute.Size = new System.Drawing.Size(320, 18);
            this._lblRoute.TabIndex = 2;
this._lblRoute.Text = "Điểm đón → Điểm đến";
            // 
            // _lblInfo
            // 
            this._lblInfo.AutoSize = true;
            this._lblInfo.Font = Presentation.Constants.UiConstants.Cards.Fonts.Info;
            this._lblInfo.ForeColor = Presentation.Constants.UiConstants.Colors.TextSecondary;
            this._lblInfo.Location = new System.Drawing.Point(16, 52);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(148, 17);
            this._lblInfo.TabIndex = 3;
this._lblInfo.Text = "15.000 VNĐ • Xe máy";
            // 
            // _lblTime
            // 
            this._lblTime.AutoSize = true;
            this._lblTime.Font = Presentation.Constants.UiConstants.Cards.Fonts.Info;
            this._lblTime.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;
            this._lblTime.Location = new System.Drawing.Point(250, 52);
            this._lblTime.Name = "_lblTime";
            this._lblTime.Size = new System.Drawing.Size(43, 17);
            this._lblTime.TabIndex = 4;
            this._lblTime.Text = "10:30";
            // 
            // TripCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._statusIndicator);
            this.Controls.Add(this._lblStatus);
            this.Controls.Add(this._lblRoute);
            this.Controls.Add(this._lblInfo);
            this.Controls.Add(this._lblTime);
            this.Name = "TripCard";
            this.Size = new System.Drawing.Size(Presentation.Constants.UiConstants.Cards.DefaultWidth, 70);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Label _lblRoute;
        private System.Windows.Forms.Label _lblInfo;
        private System.Windows.Forms.Label _lblTime;
        private System.Windows.Forms.Panel _statusIndicator;
        #endregion
    }
}

