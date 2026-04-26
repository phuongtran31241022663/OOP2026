using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    partial class TripTrackingForm : BaseForm
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
            this._statusBar = new System.Windows.Forms.Panel();
            this._statusBarLabel = new System.Windows.Forms.Label();
            this._emptyPanel = new System.Windows.Forms.Panel();
            this.emptyLayout = new System.Windows.Forms.TableLayoutPanel();
            this._emptyMsgLabel = new System.Windows.Forms.Label();
            this._goHomeBtn = new System.Windows.Forms.Button();
            this._activePanel = new System.Windows.Forms.Panel();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._mapPlaceholder = new System.Windows.Forms.Panel();
            this.mapPlaceholderLabel = new System.Windows.Forms.Label();
            this.infoColumn = new System.Windows.Forms.Panel();
            this._cancelBtn = new System.Windows.Forms.Button();
            this._driverCard = new System.Windows.Forms.Panel();
            this.driverTitleLabel = new System.Windows.Forms.Label();
            this._driverNameLabel = new System.Windows.Forms.Label();
            this._driverPhoneLabel = new System.Windows.Forms.Label();
            this._driverReviewLabel = new System.Windows.Forms.Label();
            this._vehicleLabel = new System.Windows.Forms.Label();
            this.tripGroup = new System.Windows.Forms.GroupBox();
            this._fareLabel = new System.Windows.Forms.Label();
            this._destLabel = new System.Windows.Forms.Label();
            this._pickupLabel = new System.Windows.Forms.Label();
            this._statusBanner = new System.Windows.Forms.Panel();
            this._statusBannerLabel = new System.Windows.Forms.Label();
            this._statusBar.SuspendLayout();
            this._emptyPanel.SuspendLayout();
            this.emptyLayout.SuspendLayout();
            this._activePanel.SuspendLayout();
            this._contentPanel.SuspendLayout();
            this._mapPlaceholder.SuspendLayout();
            this.infoColumn.SuspendLayout();
            this._driverCard.SuspendLayout();
            this.tripGroup.SuspendLayout();
            this._statusBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // _statusBar
            // 
            this._statusBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this._statusBar.Controls.Add(this._statusBarLabel);
            this._statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusBar.Location = new System.Drawing.Point(0, 616);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(1024, 24);
            this._statusBar.TabIndex = 2;
            // 
            // _statusBarLabel
            // 
            this._statusBarLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusBarLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._statusBarLabel.Location = new System.Drawing.Point(0, 0);
            this._statusBarLabel.Name = "_statusBarLabel";
            this._statusBarLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._statusBarLabel.Size = new System.Drawing.Size(1024, 24);
            this._statusBarLabel.TabIndex = 0;
            this._statusBarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _emptyPanel
            // 
            this._emptyPanel.BackColor = System.Drawing.SystemColors.Control;
            this._emptyPanel.Controls.Add(this.emptyLayout);
            this._emptyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyPanel.Location = new System.Drawing.Point(0, 0);
            this._emptyPanel.Name = "_emptyPanel";
            this._emptyPanel.Size = new System.Drawing.Size(1024, 616);
            this._emptyPanel.TabIndex = 0;
            // 
            // emptyLayout
            // 
            this.emptyLayout.ColumnCount = 1;
            this.emptyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.emptyLayout.Controls.Add(this._emptyMsgLabel, 0, 1);
            this.emptyLayout.Controls.Add(this._goHomeBtn, 0, 2);
            this.emptyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emptyLayout.Location = new System.Drawing.Point(0, 0);
            this.emptyLayout.Name = "emptyLayout";
            this.emptyLayout.RowCount = 3;
            this.emptyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.emptyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.emptyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.emptyLayout.Size = new System.Drawing.Size(1024, 616);
            this.emptyLayout.TabIndex = 0;
            // 
            // _emptyMsgLabel
            // 
            this._emptyMsgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyMsgLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._emptyMsgLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this._emptyMsgLabel.Location = new System.Drawing.Point(3, 261);
            this._emptyMsgLabel.Name = "_emptyMsgLabel";
            this._emptyMsgLabel.Size = new System.Drawing.Size(1018, 36);
            this._emptyMsgLabel.TabIndex = 0;
            this._emptyMsgLabel.Text = "ChÆ°a cÃ³ chuyáº¿n Ä‘i nÃ o";
            this._emptyMsgLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // _goHomeBtn
            // 
            this._goHomeBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._goHomeBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._goHomeBtn.Location = new System.Drawing.Point(447, 458);
            this._goHomeBtn.Name = "_goHomeBtn";
            this._goHomeBtn.Size = new System.Drawing.Size(130, 28);
            this._goHomeBtn.TabIndex = 1;
            this._goHomeBtn.Text = "Vá» trang chá»§";
            this._goHomeBtn.UseVisualStyleBackColor = true;
            this._goHomeBtn.Click += new System.EventHandler(this.OnGoHomeClicked);
            // 
            // _activePanel
            // 
            this._activePanel.BackColor = System.Drawing.SystemColors.Control;
            this._activePanel.Controls.Add(this._contentPanel);
            this._activePanel.Controls.Add(this._statusBanner);
            this._activePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._activePanel.Location = new System.Drawing.Point(0, 0);
            this._activePanel.Name = "_activePanel";
            this._activePanel.Size = new System.Drawing.Size(1024, 616);
            this._activePanel.TabIndex = 1;
            this._activePanel.Visible = false;
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._mapPlaceholder);
            this._contentPanel.Controls.Add(this.infoColumn);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 36);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1024, 580);
            this._contentPanel.TabIndex = 1;
            // 
            // _mapPlaceholder
            // 
            this._mapPlaceholder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(232)))), ((int)(((byte)(208)))));
            this._mapPlaceholder.Controls.Add(this.mapPlaceholderLabel);
            this._mapPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapPlaceholder.Location = new System.Drawing.Point(0, 0);
            this._mapPlaceholder.Name = "_mapPlaceholder";
            this._mapPlaceholder.Size = new System.Drawing.Size(764, 580);
            this._mapPlaceholder.TabIndex = 0;
            // 
            // mapPlaceholderLabel
            // 
            this.mapPlaceholderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPlaceholderLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.mapPlaceholderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.mapPlaceholderLabel.Location = new System.Drawing.Point(0, 0);
            this.mapPlaceholderLabel.Name = "mapPlaceholderLabel";
            this.mapPlaceholderLabel.Size = new System.Drawing.Size(764, 580);
            this.mapPlaceholderLabel.TabIndex = 0;
            this.mapPlaceholderLabel.Text = "Live map - Ä‘iá»ƒm Ä‘Ã³n Â· tÃ i xáº¿ Â· Ä‘iá»ƒm Ä‘áº¿n";
            this.mapPlaceholderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoColumn
            // 
            this.infoColumn.BackColor = System.Drawing.SystemColors.Control;
            this.infoColumn.Controls.Add(this._cancelBtn);
            this.infoColumn.Controls.Add(this._driverCard);
            this.infoColumn.Controls.Add(this.tripGroup);
            this.infoColumn.Dock = System.Windows.Forms.DockStyle.Right;
            this.infoColumn.Location = new System.Drawing.Point(764, 0);
            this.infoColumn.Name = "infoColumn";
            this.infoColumn.Padding = new System.Windows.Forms.Padding(8);
            this.infoColumn.Size = new System.Drawing.Size(260, 580);
            this.infoColumn.TabIndex = 1;
            // 
            // _cancelBtn
            // 
            this._cancelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._cancelBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._cancelBtn.ForeColor = System.Drawing.Color.DarkRed;
            this._cancelBtn.Location = new System.Drawing.Point(8, 542);
            this._cancelBtn.Name = "_cancelBtn";
            this._cancelBtn.Size = new System.Drawing.Size(244, 30);
            this._cancelBtn.TabIndex = 2;
            this._cancelBtn.Text = "Há»§y chuyáº¿n";
            this._cancelBtn.UseVisualStyleBackColor = true;
            this._cancelBtn.Click += new System.EventHandler(this.OnCancelClicked);
            // 
            // _driverCard
            // 
            this._driverCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this._driverCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._driverCard.Controls.Add(this._vehicleLabel);
            this._driverCard.Controls.Add(this._driverReviewLabel);
            this._driverCard.Controls.Add(this._driverPhoneLabel);
            this._driverCard.Controls.Add(this._driverNameLabel);
            this._driverCard.Controls.Add(this.driverTitleLabel);
            this._driverCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverCard.Location = new System.Drawing.Point(8, 98);
            this._driverCard.Name = "_driverCard";
            this._driverCard.Padding = new System.Windows.Forms.Padding(6);
            this._driverCard.Size = new System.Drawing.Size(244, 95);
            this._driverCard.TabIndex = 1;
            this._driverCard.Visible = false;
            // 
            // driverTitleLabel
            // 
            this.driverTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.driverTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.driverTitleLabel.Location = new System.Drawing.Point(6, 6);
            this.driverTitleLabel.Name = "driverTitleLabel";
            this.driverTitleLabel.Size = new System.Drawing.Size(230, 18);
            this.driverTitleLabel.TabIndex = 0;
            this.driverTitleLabel.Text = "ThÃ´ng tin tÃ i xáº¿";
            // 
            // _driverNameLabel
            // 
            this._driverNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverNameLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverNameLabel.Location = new System.Drawing.Point(6, 24);
            this._driverNameLabel.Name = "_driverNameLabel";
            this._driverNameLabel.Size = new System.Drawing.Size(230, 18);
            this._driverNameLabel.TabIndex = 1;
            this._driverNameLabel.Text = "TÃªn: --";
            // 
            // _driverPhoneLabel
            // 
            this._driverPhoneLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverPhoneLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverPhoneLabel.Location = new System.Drawing.Point(6, 42);
            this._driverPhoneLabel.Name = "_driverPhoneLabel";
            this._driverPhoneLabel.Size = new System.Drawing.Size(230, 18);
            this._driverPhoneLabel.TabIndex = 2;
            this._driverPhoneLabel.Text = "SÄT: --";
            // 
            // _driverReviewLabel
            // 
            this._driverReviewLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._driverReviewLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._driverReviewLabel.Location = new System.Drawing.Point(6, 60);
            this._driverReviewLabel.Name = "_driverReviewLabel";
            this._driverReviewLabel.Size = new System.Drawing.Size(230, 18);
            this._driverReviewLabel.TabIndex = 3;
            this._driverReviewLabel.Text = "ÄÃ¡nh giÃ¡: --";
            // 
            // _vehicleLabel
            // 
            this._vehicleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._vehicleLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._vehicleLabel.Location = new System.Drawing.Point(6, 78);
            this._vehicleLabel.Name = "_vehicleLabel";
            this._vehicleLabel.Size = new System.Drawing.Size(230, 18);
            this._vehicleLabel.TabIndex = 4;
            this._vehicleLabel.Text = "Biá»ƒn sá»‘: --";
            // 
            // tripGroup
            // 
            this.tripGroup.Controls.Add(this._fareLabel);
            this.tripGroup.Controls.Add(this._destLabel);
            this.tripGroup.Controls.Add(this._pickupLabel);
            this.tripGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.tripGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tripGroup.Location = new System.Drawing.Point(8, 8);
            this.tripGroup.Name = "tripGroup";
            this.tripGroup.Padding = new System.Windows.Forms.Padding(4, 14, 4, 4);
            this.tripGroup.Size = new System.Drawing.Size(244, 90);
            this.tripGroup.TabIndex = 0;
            this.tripGroup.TabStop = false;
            this.tripGroup.Text = "Chuyáº¿n Ä‘i";
            // 
            // _fareLabel
            // 
            this._fareLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._fareLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._fareLabel.Location = new System.Drawing.Point(4, 65);
            this._fareLabel.Name = "_fareLabel";
            this._fareLabel.Size = new System.Drawing.Size(236, 18);
            this._fareLabel.TabIndex = 2;
            this._fareLabel.Text = "GiÃ¡: --";
            // 
            // _destLabel
            // 
            this._destLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._destLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._destLabel.Location = new System.Drawing.Point(4, 47);
            this._destLabel.Name = "_destLabel";
            this._destLabel.Size = new System.Drawing.Size(236, 18);
            this._destLabel.TabIndex = 1;
            this._destLabel.Text = "Äiá»ƒm Ä‘áº¿n: --";
            // 
            // _pickupLabel
            // 
            this._pickupLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._pickupLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._pickupLabel.Location = new System.Drawing.Point(4, 29);
            this._pickupLabel.Name = "_pickupLabel";
            this._pickupLabel.Size = new System.Drawing.Size(236, 18);
            this._pickupLabel.TabIndex = 0;
            this._pickupLabel.Text = "Äiá»ƒm Ä‘Ã³n: --";
            // 
            // _statusBanner
            // 
            this._statusBanner.BackColor = System.Drawing.Color.Khaki;
            this._statusBanner.Controls.Add(this._statusBannerLabel);
            this._statusBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this._statusBanner.Location = new System.Drawing.Point(0, 0);
            this._statusBanner.Name = "_statusBanner";
            this._statusBanner.Size = new System.Drawing.Size(1024, 36);
            this._statusBanner.TabIndex = 0;
            // 
            // _statusBannerLabel
            // 
            this._statusBannerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusBannerLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._statusBannerLabel.Location = new System.Drawing.Point(0, 0);
            this._statusBannerLabel.Name = "_statusBannerLabel";
            this._statusBannerLabel.Size = new System.Drawing.Size(1024, 36);
            this._statusBannerLabel.TabIndex = 0;
            this._statusBannerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TripTrackingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this._activePanel);
            this.Controls.Add(this._emptyPanel);
            this.Controls.Add(this._statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TripTrackingForm";
            this.Text = "Trip Tracking";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TripTrackingForm_Load);
            this._statusBar.ResumeLayout(false);
            this._emptyPanel.ResumeLayout(false);
            this.emptyLayout.ResumeLayout(false);
            this._activePanel.ResumeLayout(false);
            this._contentPanel.ResumeLayout(false);
            this._mapPlaceholder.ResumeLayout(false);
            this.infoColumn.ResumeLayout(false);
            this._driverCard.ResumeLayout(false);
            this.tripGroup.ResumeLayout(false);
            this._statusBanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _statusBar;
        private System.Windows.Forms.Label _statusBarLabel;
        private System.Windows.Forms.Panel _emptyPanel;
        private System.Windows.Forms.TableLayoutPanel emptyLayout;
        private System.Windows.Forms.Label _emptyMsgLabel;
        private System.Windows.Forms.Button _goHomeBtn;
        private System.Windows.Forms.Panel _activePanel;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _mapPlaceholder;
        private System.Windows.Forms.Label mapPlaceholderLabel;
        private System.Windows.Forms.Panel infoColumn;
        private System.Windows.Forms.Button _cancelBtn;
        private System.Windows.Forms.Panel _driverCard;
        private System.Windows.Forms.Label driverTitleLabel;
        private System.Windows.Forms.Label _driverNameLabel;
        private System.Windows.Forms.Label _driverPhoneLabel;
        private System.Windows.Forms.Label _driverReviewLabel;
        private System.Windows.Forms.Label _vehicleLabel;
        private System.Windows.Forms.GroupBox tripGroup;
        private System.Windows.Forms.Label _fareLabel;
        private System.Windows.Forms.Label _destLabel;
        private System.Windows.Forms.Label _pickupLabel;
        private System.Windows.Forms.Panel _statusBanner;
        private System.Windows.Forms.Label _statusBannerLabel;
    }
}

