using Presentation.Constants;

namespace Presentation.Components
{
    partial class FarePanel
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
            this.lblTitle = new System.Windows.Forms.Label();
            this._separator = new System.Windows.Forms.Panel();
            this._lblBaseFare = new System.Windows.Forms.Label();
            this._lblDistanceFare = new System.Windows.Forms.Label();
            this._lblCommission = new System.Windows.Forms.Label();
            this._lblTotalFare = new System.Windows.Forms.Label();
            this._lblBreakdown = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._validationErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chi tiết giá cước";
            // 
            // _separator
            // 
            this._separator.BackColor = Presentation.Constants.UiConstants.Separators.Color;
            this._separator.Location = new System.Drawing.Point(8, 35);
            this._separator.Name = "_separator";
            this._separator.Size = new System.Drawing.Size(280, Presentation.Constants.UiConstants.Separators.DefaultHeight);
            this._separator.TabIndex = 1;
            // 
            // _lblBaseFare
            // 
            this._lblBaseFare.AutoEllipsis = true;
            this._lblBaseFare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBaseFare.Location = new System.Drawing.Point(9, 51);
            this._lblBaseFare.Name = "_lblBaseFare";
            this._lblBaseFare.Size = new System.Drawing.Size(280, 23);
            this._lblBaseFare.TabIndex = 2;
            this._lblBaseFare.Text = "Giá cơ bản: --";
            // 
            // _lblDistanceFare
            // 
            this._lblDistanceFare.AutoEllipsis = true;
            this._lblDistanceFare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDistanceFare.Location = new System.Drawing.Point(9, 83);
            this._lblDistanceFare.Name = "_lblDistanceFare";
            this._lblDistanceFare.Size = new System.Drawing.Size(280, 26);
            this._lblDistanceFare.TabIndex = 3;
            this._lblDistanceFare.Text = "Phí khoảng cách: --";
            // 
            // _lblCommission
            // 
            this._lblCommission.AutoEllipsis = true;
            this._lblCommission.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCommission.ForeColor = System.Drawing.Color.Red;
            this._lblCommission.Location = new System.Drawing.Point(9, 109);
            this._lblCommission.Name = "_lblCommission";
            this._lblCommission.Size = new System.Drawing.Size(280, 27);
            this._lblCommission.TabIndex = 4;
            this._lblCommission.Text = "Hoa hồng: --";
            // 
            // _lblTotalFare
            // 
            this._lblTotalFare.AutoEllipsis = true;
            this._lblTotalFare.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTotalFare.ForeColor = System.Drawing.Color.Green;
            this._lblTotalFare.Location = new System.Drawing.Point(8, 136);
            this._lblTotalFare.Name = "_lblTotalFare";
            this._lblTotalFare.Size = new System.Drawing.Size(280, 34);
            this._lblTotalFare.TabIndex = 5;
            this._lblTotalFare.Text = "Tổng cộng: --";
            // 
            // _lblBreakdown
            // 
            this._lblBreakdown.AutoSize = true;
            this._lblBreakdown.Font = Presentation.Constants.UiConstants.Cards.Typography.Info;
            this._lblBreakdown.ForeColor = Presentation.Constants.UiConstants.Colors.TextMuted;
            this._lblBreakdown.Location = new System.Drawing.Point(9, 170);
            this._lblBreakdown.Name = "_lblBreakdown";
            this._lblBreakdown.Size = new System.Drawing.Size(204, 20);
            this._lblBreakdown.TabIndex = 6;
            this._lblBreakdown.Text = "* Giá đã bao gồm thuế và phí";
            // 
            // FarePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._lblBreakdown);
            this.Controls.Add(this._lblTotalFare);
            this.Controls.Add(this._lblCommission);
            this.Controls.Add(this._lblDistanceFare);
            this.Controls.Add(this._lblBaseFare);
            this.Controls.Add(this._separator);
            this.Controls.Add(this.lblTitle);
            this.Name = "FarePanel";
            this.Size = new System.Drawing.Size(348, 206);
            ((System.ComponentModel.ISupportInitialize)(this._validationErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label _lblBaseFare;
        private System.Windows.Forms.Label _lblDistanceFare;
        private System.Windows.Forms.Label _lblCommission;
        private System.Windows.Forms.Label _lblTotalFare;
        private System.Windows.Forms.Label _lblBreakdown;
        private System.Windows.Forms.Panel _separator;
    }
}

