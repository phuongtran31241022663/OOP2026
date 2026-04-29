using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
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
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(113, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chi tiết giá cước";
            //
            // _separator
            //
            this._separator.BackColor = System.Drawing.Color.LightGray;
            this._separator.Location = new System.Drawing.Point(8, 35);
            this._separator.Name = "_separator";
            this._separator.Size = new System.Drawing.Size(280, 2);
            this._separator.TabIndex = 1;
            //
            // _lblBaseFare
            //
            this._lblBaseFare.AutoSize = true;
            this._lblBaseFare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBaseFare.Location = new System.Drawing.Point(8, 45);
            this._lblBaseFare.Name = "_lblBaseFare";
            this._lblBaseFare.Size = new System.Drawing.Size(89, 15);
            this._lblBaseFare.TabIndex = 2;
            this._lblBaseFare.Text = "Giá cơ bản: --";
            //
            // _lblDistanceFare
            //
            this._lblDistanceFare.AutoSize = true;
            this._lblDistanceFare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDistanceFare.Location = new System.Drawing.Point(8, 65);
            this._lblDistanceFare.Name = "_lblDistanceFare";
            this._lblDistanceFare.Size = new System.Drawing.Size(113, 15);
            this._lblDistanceFare.TabIndex = 3;
            this._lblDistanceFare.Text = "Phí khoảng cách: --";
            //
            // _lblCommission
            //
            this._lblCommission.AutoSize = true;
            this._lblCommission.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCommission.ForeColor = System.Drawing.Color.Red;
            this._lblCommission.Location = new System.Drawing.Point(8, 85);
            this._lblCommission.Name = "_lblCommission";
            this._lblCommission.Size = new System.Drawing.Size(68, 15);
            this._lblCommission.TabIndex = 4;
            this._lblCommission.Text = "Hoa hồng: --";
            //
            // _lblTotalFare
            //
            this._lblTotalFare.AutoSize = true;
            this._lblTotalFare.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTotalFare.ForeColor = System.Drawing.Color.Green;
            this._lblTotalFare.Location = new System.Drawing.Point(8, 110);
            this._lblTotalFare.Name = "_lblTotalFare";
            this._lblTotalFare.Size = new System.Drawing.Size(94, 20);
            this._lblTotalFare.TabIndex = 5;
            this._lblTotalFare.Text = "Tổng cộng: --";
            //
            // _lblBreakdown
            //
            this._lblBreakdown.AutoSize = true;
this._lblBreakdown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBreakdown.ForeColor = System.Drawing.Color.Gray;
            this._lblBreakdown.Location = new System.Drawing.Point(8, 135);
            this._lblBreakdown.Name = "_lblBreakdown";
            this._lblBreakdown.Size = new System.Drawing.Size(120, 12);
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
            this.Size = new System.Drawing.Size(300, 160);
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

