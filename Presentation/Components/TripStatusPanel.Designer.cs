using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
namespace Presentation.Components
{
    partial class TripStatusPanel
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
            this._statusLabel = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._stepLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // _statusLabel
            //
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._statusLabel.Location = new System.Drawing.Point(10, 10);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(50, 19);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "Status";
            //
            // _progressBar
            //
            this._progressBar.Location = new System.Drawing.Point(10, 40);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(280, 20);
            this._progressBar.TabIndex = 1;
            //
            // _stepLabel
            //
            this._stepLabel.AutoSize = true;
            this._stepLabel.Location = new System.Drawing.Point(10, 70);
            this._stepLabel.Name = "_stepLabel";
            this._stepLabel.Size = new System.Drawing.Size(40, 15);
            this._stepLabel.TabIndex = 2;
            this._stepLabel.Text = "Step";
            //
            // TripStatusPanel
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._stepLabel);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._statusLabel);
            this.Name = "TripStatusPanel";
            this.Size = new System.Drawing.Size(300, 100);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Label _stepLabel;
    }
}

