using System.Windows.Forms;

namespace Presentation.Screens.Driver
{
    partial class DriverDashboardForm : BaseForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this._mapControl = new Presentation.Components.MapControl();
            this._infoPanel = new System.Windows.Forms.Panel();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this._statusLabel = new System.Windows.Forms.Label();
            this._stepBar = new System.Windows.Forms.Panel();
            this._actionButton = new System.Windows.Forms.Button();
            this._paymentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this._infoPanel.SuspendLayout();
            this.layout.SuspendLayout();
            this._paymentPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // mainSplit
            //
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            this.mainSplit.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mainSplit.SplitterDistance = 768; // Will be adjusted to 75% in code
            this.mainSplit.Panel1MinSize = 400;
            this.mainSplit.Panel2MinSize = 250;
            //
            // mainSplit.Panel1
            //
            this.mainSplit.Panel1.Controls.Add(this._mapControl);
            //
            // mainSplit.Panel2
            //
            this.mainSplit.Panel2.Controls.Add(this._infoPanel);
            //
            // _mapControl
            //
            this._mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapControl.Location = new System.Drawing.Point(0, 0);
            this._mapControl.Name = "_mapControl";
            this._mapControl.Size = new System.Drawing.Size(768, 640);
            this._mapControl.TabIndex = 0;
            //
            // _infoPanel
            //
            this._infoPanel.Controls.Add(this.layout);
            this._infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoPanel.Location = new System.Drawing.Point(0, 0);
            this._infoPanel.Name = "_infoPanel";
            this._infoPanel.Size = new System.Drawing.Size(256, 640);
            this._infoPanel.TabIndex = 0;
            //
            // layout
            //
            this.layout.ColumnCount = 1;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this._statusLabel, 0, 0);
            this.layout.Controls.Add(this._stepBar, 0, 1);
            this.layout.Controls.Add(this._actionButton, 0, 2);
            this.layout.Controls.Add(this._paymentPanel, 0, 3);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 5;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(256, 640);
            this.layout.TabIndex = 0;
            //
            // _statusLabel
            //
            this._statusLabel.AutoSize = true;
            this._statusLabel.Location = new System.Drawing.Point(3, 13);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(35, 13);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.Text = "Status";
            //
            // _stepBar
            //
            this._stepBar.Location = new System.Drawing.Point(3, 53);
            this._stepBar.Name = "_stepBar";
            this._stepBar.Size = new System.Drawing.Size(250, 74);
            this._stepBar.TabIndex = 1;
            //
            // _actionButton
            //
            this._actionButton.Location = new System.Drawing.Point(3, 133);
            this._actionButton.Name = "_actionButton";
            this._actionButton.Size = new System.Drawing.Size(75, 23);
            this._actionButton.TabIndex = 2;
            this._actionButton.Text = "Action";
            this._actionButton.UseVisualStyleBackColor = true;
            //
            // _paymentPanel
            //
            this._paymentPanel.Controls.Add(this._fareLabel);
            this._paymentPanel.Controls.Add(this._commissionLabel);
            this._paymentPanel.Controls.Add(this._netEarningsLabel);
            this._paymentPanel.Controls.Add(this._confirmPaymentButton);
            this._paymentPanel.Location = new System.Drawing.Point(3, 189);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Size = new System.Drawing.Size(250, 94);
            this._paymentPanel.TabIndex = 3;
            this._paymentPanel.Visible = false;
            //
            // _fareLabel
            //
            this._fareLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this._fareLabel.AutoSize = true;
            this._fareLabel.Location = new System.Drawing.Point(3, 3);
            this._fareLabel.Name = "_fareLabel";
            this._fareLabel.Size = new System.Drawing.Size(31, 13);
            this._fareLabel.TabIndex = 0;
            this._fareLabel.Text = "Fare";
            //
            // _commissionLabel
            //
            this._commissionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this._commissionLabel.AutoSize = true;
            this._commissionLabel.Location = new System.Drawing.Point(3, 19);
            this._commissionLabel.Name = "_commissionLabel";
            this._commissionLabel.Size = new System.Drawing.Size(63, 13);
            this._commissionLabel.TabIndex = 1;
            this._commissionLabel.Text = "Commission";
            //
            // _netEarningsLabel
            //
            this._netEarningsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this._netEarningsLabel.AutoSize = true;
            this._netEarningsLabel.Location = new System.Drawing.Point(3, 35);
            this._netEarningsLabel.Name = "_netEarningsLabel";
            this._netEarningsLabel.Size = new System.Drawing.Size(70, 13);
            this._netEarningsLabel.TabIndex = 2;
            this._netEarningsLabel.Text = "Net Earnings";
            //
            // _confirmPaymentButton
            //
            this._confirmPaymentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this._confirmPaymentButton.Location = new System.Drawing.Point(3, 58);
            this._confirmPaymentButton.Name = "_confirmPaymentButton";
            this._confirmPaymentButton.Size = new System.Drawing.Size(100, 23);
            this._confirmPaymentButton.TabIndex = 3;
            this._confirmPaymentButton.Text = "Confirm Payment";
            this._confirmPaymentButton.UseVisualStyleBackColor = true;
            //
            // DriverDashboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this.mainSplit);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "DriverDashboardForm";
            this.Text = "Trip Execution";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this._infoPanel.ResumeLayout(false);
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this._paymentPanel.ResumeLayout(false);
            this._paymentPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.TableLayoutPanel layout;
    }
}