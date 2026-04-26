using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
namespace Presentation.Screens.PassengerScreen
{
    partial class ReviewForm : BaseForm
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
            this._header = new System.Windows.Forms.Panel();
            this._refreshBtn = new System.Windows.Forms.Button();
            this.headerTitleLabel = new System.Windows.Forms.Label();
            this._statusBar = new System.Windows.Forms.Panel();
            this._statusLabel = new System.Windows.Forms.Label();
            this._split = new System.Windows.Forms.SplitContainer();
            this._tripList = new System.Windows.Forms.ListBox();
            this._listHeader = new System.Windows.Forms.Label();
            this._ReviewPanel = new System.Windows.Forms.Panel();
            this.submitRow = new System.Windows.Forms.Panel();
            this._successLabel = new System.Windows.Forms.Label();
            this._submitBtn = new System.Windows.Forms.Button();
            this.commentGroup = new System.Windows.Forms.GroupBox();
            this._commentBox = new System.Windows.Forms.TextBox();
            this.starGroup = new System.Windows.Forms.GroupBox();
            this._scoreHint = new System.Windows.Forms.Label();
            this.starFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._starBtn1 = new System.Windows.Forms.Button();
            this._starBtn2 = new System.Windows.Forms.Button();
            this._starBtn3 = new System.Windows.Forms.Button();
            this._starBtn4 = new System.Windows.Forms.Button();
            this._starBtn5 = new System.Windows.Forms.Button();
            this.tripInfoGroup = new System.Windows.Forms.GroupBox();
            this._tripInfoLabel = new System.Windows.Forms.Label();
            this._promptLabel = new System.Windows.Forms.Panel();
            this.promptTextLabel = new System.Windows.Forms.Label();
            this._header.SuspendLayout();
            this._statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._split)).BeginInit();
            this._split.Panel1.SuspendLayout();
            this._split.Panel2.SuspendLayout();
            this._split.SuspendLayout();
            this._ReviewPanel.SuspendLayout();
            this.submitRow.SuspendLayout();
            this.commentGroup.SuspendLayout();
            this.starGroup.SuspendLayout();
            this.starFlow.SuspendLayout();
            this.tripInfoGroup.SuspendLayout();
            this._promptLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.SystemColors.ControlDark;
            this._header.Controls.Add(this._refreshBtn);
            this._header.Controls.Add(this.headerTitleLabel);
            this._header.Dock = System.Windows.Forms.DockStyle.Top;
            this._header.Location = new System.Drawing.Point(0, 0);
            this._header.Name = "_header";
            this._header.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this._header.Size = new System.Drawing.Size(1024, 40);
            this._header.TabIndex = 0;
            // 
            // _refreshBtn
            // 
            this._refreshBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this._refreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._refreshBtn.Location = new System.Drawing.Point(934, 0);
            this._refreshBtn.Name = "_refreshBtn";
            this._refreshBtn.Size = new System.Drawing.Size(80, 40);
            this._refreshBtn.TabIndex = 1;
            this._refreshBtn.Text = "LÃ m má»›i";
            this._refreshBtn.UseVisualStyleBackColor = true;
            this._refreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // headerTitleLabel
            // 
            this.headerTitleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.headerTitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.headerTitleLabel.Location = new System.Drawing.Point(10, 0);
            this.headerTitleLabel.Name = "headerTitleLabel";
            this.headerTitleLabel.Size = new System.Drawing.Size(240, 40);
            this.headerTitleLabel.TabIndex = 0;
            this.headerTitleLabel.Text = "ÄÃ¡nh giÃ¡ chuyáº¿n Ä‘i";
            this.headerTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _statusBar
            // 
            this._statusBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this._statusBar.Controls.Add(this._statusLabel);
            this._statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusBar.Location = new System.Drawing.Point(0, 618);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(1024, 22);
            this._statusBar.TabIndex = 2;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._statusLabel.Location = new System.Drawing.Point(0, 0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._statusLabel.Size = new System.Drawing.Size(1024, 22);
            this._statusLabel.TabIndex = 0;
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _split
            // 
            this._split.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._split.Dock = System.Windows.Forms.DockStyle.Fill;
            this._split.Location = new System.Drawing.Point(0, 40);
            this._split.Name = "_split";
            // 
            // _split.Panel1
            // 
            this._split.Panel1.Controls.Add(this._tripList);
            this._split.Panel1.Controls.Add(this._listHeader);
            this._split.Panel1MinSize = 240;
            // 
            // _split.Panel2
            // 
            this._split.Panel2.Controls.Add(this._ReviewPanel);
            this._split.Panel2.Controls.Add(this._promptLabel);
            this._split.Panel2MinSize = 300;
            this._split.Size = new System.Drawing.Size(1024, 578);
            this._split.SplitterDistance = 380;
            this._split.SplitterWidth = 4;
            this._split.TabIndex = 1;
            // 
            // _tripList
            // 
            this._tripList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tripList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tripList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._tripList.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._tripList.FormattingEnabled = true;
            this._tripList.IntegralHeight = false;
            this._tripList.ItemHeight = 64;
            this._tripList.Location = new System.Drawing.Point(0, 24);
            this._tripList.Name = "_tripList";
            this._tripList.Size = new System.Drawing.Size(380, 554);
            this._tripList.TabIndex = 1;
            this._tripList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.OnDrawTripItem);
            this._tripList.SelectedIndexChanged += new System.EventHandler(this.OnTripSelected);
            // 
            // _listHeader
            // 
            this._listHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this._listHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._listHeader.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._listHeader.Location = new System.Drawing.Point(0, 0);
            this._listHeader.Name = "_listHeader";
            this._listHeader.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._listHeader.Size = new System.Drawing.Size(380, 24);
            this._listHeader.TabIndex = 0;
            this._listHeader.Text = "Chuyáº¿n chá» Ä‘Ã¡nh giÃ¡";
            this._listHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ReviewPanel
            // 
            this._ReviewPanel.Controls.Add(this.submitRow);
            this._ReviewPanel.Controls.Add(this.commentGroup);
            this._ReviewPanel.Controls.Add(this.starGroup);
            this._ReviewPanel.Controls.Add(this.tripInfoGroup);
            this._ReviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ReviewPanel.Location = new System.Drawing.Point(0, 0);
            this._ReviewPanel.Name = "_ReviewPanel";
            this._ReviewPanel.Padding = new System.Windows.Forms.Padding(12);
            this._ReviewPanel.Size = new System.Drawing.Size(640, 578);
            this._ReviewPanel.TabIndex = 0;
            this._ReviewPanel.Visible = false;
            // 
            // submitRow
            // 
            this.submitRow.Controls.Add(this._successLabel);
            this.submitRow.Controls.Add(this._submitBtn);
            this.submitRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.submitRow.Location = new System.Drawing.Point(12, 256);
            this.submitRow.Name = "submitRow";
            this.submitRow.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.submitRow.Size = new System.Drawing.Size(616, 36);
            this.submitRow.TabIndex = 3;
            // 
            // _successLabel
            // 
            this._successLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._successLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._successLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this._successLabel.Location = new System.Drawing.Point(0, 4);
            this._successLabel.Name = "_successLabel";
            this._successLabel.Size = new System.Drawing.Size(180, 32);
            this._successLabel.TabIndex = 0;
            this._successLabel.Text = "âœ“ ÄÃ¡nh giÃ¡ thÃ nh cÃ´ng!";
            this._successLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._successLabel.Visible = false;
            // 
            // _submitBtn
            // 
            this._submitBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this._submitBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._submitBtn.Location = new System.Drawing.Point(506, 4);
            this._submitBtn.Name = "_submitBtn";
            this._submitBtn.Size = new System.Drawing.Size(110, 32);
            this._submitBtn.TabIndex = 1;
            this._submitBtn.Text = "Gá»­i Ä‘Ã¡nh giÃ¡";
            this._submitBtn.UseVisualStyleBackColor = true;
            this._submitBtn.Click += new System.EventHandler(this.OnSubmitClicked);
            // 
            // commentGroup
            // 
            this.commentGroup.Controls.Add(this._commentBox);
            this.commentGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.commentGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.commentGroup.Location = new System.Drawing.Point(12, 156);
            this.commentGroup.Name = "commentGroup";
            this.commentGroup.Padding = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.commentGroup.Size = new System.Drawing.Size(616, 100);
            this.commentGroup.TabIndex = 2;
            this.commentGroup.TabStop = false;
            this.commentGroup.Text = "Nháº­n xÃ©t";
            // 
            // _commentBox
            // 
            this._commentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._commentBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._commentBox.Location = new System.Drawing.Point(6, 32);
            this._commentBox.Multiline = true;
            this._commentBox.Name = "_commentBox";
            this._commentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._commentBox.Size = new System.Drawing.Size(604, 64);
            this._commentBox.TabIndex = 0;
            // 
            // starGroup
            // 
            this.starGroup.Controls.Add(this._scoreHint);
            this.starGroup.Controls.Add(this.starFlow);
            this.starGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.starGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.starGroup.Location = new System.Drawing.Point(12, 92);
            this.starGroup.Name = "starGroup";
            this.starGroup.Padding = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.starGroup.Size = new System.Drawing.Size(616, 64);
            this.starGroup.TabIndex = 1;
            this.starGroup.TabStop = false;
            this.starGroup.Text = "Sá»‘ sao";
            // 
            // _scoreHint
            // 
            this._scoreHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scoreHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._scoreHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this._scoreHint.Location = new System.Drawing.Point(156, 29);
            this._scoreHint.Name = "_scoreHint";
            this._scoreHint.Size = new System.Drawing.Size(454, 31);
            this._scoreHint.TabIndex = 1;
            this._scoreHint.Text = "5 / 5 sao";
            this._scoreHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // starFlow
            // 
            this.starFlow.Controls.Add(this._starBtn1);
            this.starFlow.Controls.Add(this._starBtn2);
            this.starFlow.Controls.Add(this._starBtn3);
            this.starFlow.Controls.Add(this._starBtn4);
            this.starFlow.Controls.Add(this._starBtn5);
            this.starFlow.Dock = System.Windows.Forms.DockStyle.Left;
            this.starFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.starFlow.Location = new System.Drawing.Point(6, 29);
            this.starFlow.Name = "starFlow";
            this.starFlow.Size = new System.Drawing.Size(150, 31);
            this.starFlow.TabIndex = 0;
            this.starFlow.WrapContents = false;
            // 
            // _starBtn1
            // 
            this._starBtn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this._starBtn1.FlatAppearance.BorderSize = 0;
            this._starBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._starBtn1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._starBtn1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this._starBtn1.Location = new System.Drawing.Point(0, 0);
            this._starBtn1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._starBtn1.Name = "_starBtn1";
            this._starBtn1.Size = new System.Drawing.Size(26, 26);
            this._starBtn1.TabIndex = 0;
            this._starBtn1.Tag = 1;
            this._starBtn1.Text = "â˜…";
            this._starBtn1.UseVisualStyleBackColor = true;
            this._starBtn1.Click += new System.EventHandler(this.OnStarClicked);
            // 
            // _starBtn2
            // 
            this._starBtn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this._starBtn2.FlatAppearance.BorderSize = 0;
            this._starBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._starBtn2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._starBtn2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this._starBtn2.Location = new System.Drawing.Point(28, 0);
            this._starBtn2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._starBtn2.Name = "_starBtn2";
            this._starBtn2.Size = new System.Drawing.Size(26, 26);
            this._starBtn2.TabIndex = 1;
            this._starBtn2.Tag = 2;
            this._starBtn2.Text = "â˜…";
            this._starBtn2.UseVisualStyleBackColor = true;
            this._starBtn2.Click += new System.EventHandler(this.OnStarClicked);
            // 
            // _starBtn3
            // 
            this._starBtn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this._starBtn3.FlatAppearance.BorderSize = 0;
            this._starBtn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._starBtn3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._starBtn3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this._starBtn3.Location = new System.Drawing.Point(56, 0);
            this._starBtn3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._starBtn3.Name = "_starBtn3";
            this._starBtn3.Size = new System.Drawing.Size(26, 26);
            this._starBtn3.TabIndex = 2;
            this._starBtn3.Tag = 3;
            this._starBtn3.Text = "â˜…";
            this._starBtn3.UseVisualStyleBackColor = true;
            this._starBtn3.Click += new System.EventHandler(this.OnStarClicked);
            // 
            // _starBtn4
            // 
            this._starBtn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this._starBtn4.FlatAppearance.BorderSize = 0;
            this._starBtn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._starBtn4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._starBtn4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this._starBtn4.Location = new System.Drawing.Point(84, 0);
            this._starBtn4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._starBtn4.Name = "_starBtn4";
            this._starBtn4.Size = new System.Drawing.Size(26, 26);
            this._starBtn4.TabIndex = 3;
            this._starBtn4.Tag = 4;
            this._starBtn4.Text = "â˜…";
            this._starBtn4.UseVisualStyleBackColor = true;
            this._starBtn4.Click += new System.EventHandler(this.OnStarClicked);
            // 
            // _starBtn5
            // 
            this._starBtn5.Cursor = System.Windows.Forms.Cursors.Hand;
            this._starBtn5.FlatAppearance.BorderSize = 0;
            this._starBtn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._starBtn5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._starBtn5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(0)))));
            this._starBtn5.Location = new System.Drawing.Point(112, 0);
            this._starBtn5.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._starBtn5.Name = "_starBtn5";
            this._starBtn5.Size = new System.Drawing.Size(26, 26);
            this._starBtn5.TabIndex = 4;
            this._starBtn5.Tag = 5;
            this._starBtn5.Text = "â˜…";
            this._starBtn5.UseVisualStyleBackColor = true;
            this._starBtn5.Click += new System.EventHandler(this.OnStarClicked);
            // 
            // tripInfoGroup
            // 
            this.tripInfoGroup.Controls.Add(this._tripInfoLabel);
            this.tripInfoGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.tripInfoGroup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tripInfoGroup.Location = new System.Drawing.Point(12, 12);
            this.tripInfoGroup.Name = "tripInfoGroup";
            this.tripInfoGroup.Padding = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.tripInfoGroup.Size = new System.Drawing.Size(616, 80);
            this.tripInfoGroup.TabIndex = 0;
            this.tripInfoGroup.TabStop = false;
            this.tripInfoGroup.Text = "ThÃ´ng tin chuyáº¿n Ä‘i";
            // 
            // _tripInfoLabel
            // 
            this._tripInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tripInfoLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._tripInfoLabel.Location = new System.Drawing.Point(6, 29);
            this._tripInfoLabel.Name = "_tripInfoLabel";
            this._tripInfoLabel.Size = new System.Drawing.Size(604, 47);
            this._tripInfoLabel.TabIndex = 0;
            // 
            // _promptLabel
            // 
            this._promptLabel.Controls.Add(this.promptTextLabel);
            this._promptLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promptLabel.Location = new System.Drawing.Point(0, 0);
            this._promptLabel.Name = "_promptLabel";
            this._promptLabel.Size = new System.Drawing.Size(640, 578);
            this._promptLabel.TabIndex = 1;
            // 
            // promptTextLabel
            // 
            this.promptTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.promptTextLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.promptTextLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.promptTextLabel.Location = new System.Drawing.Point(0, 0);
            this.promptTextLabel.Name = "promptTextLabel";
            this.promptTextLabel.Size = new System.Drawing.Size(640, 578);
            this.promptTextLabel.TabIndex = 0;
            this.promptTextLabel.Text = "Chá»n má»™t chuyáº¿n Ä‘i Ä‘á»ƒ Ä‘Ã¡nh giÃ¡";
            this.promptTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this._split);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this._header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReviewForm";
            this.Text = "Rate Trip";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReviewForm_Load);
            this._header.ResumeLayout(false);
            this._statusBar.ResumeLayout(false);
            this._split.Panel1.ResumeLayout(false);
            this._split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._split)).EndInit();
            this._split.ResumeLayout(false);
            this._ReviewPanel.ResumeLayout(false);
            this.submitRow.ResumeLayout(false);
            this.commentGroup.ResumeLayout(false);
            this.commentGroup.PerformLayout();
            this.starGroup.ResumeLayout(false);
            this.starFlow.ResumeLayout(false);
            this.tripInfoGroup.ResumeLayout(false);
            this._promptLabel.ResumeLayout(false);
            this.ResumeLayout(false);

            this._starBtns = new System.Windows.Forms.Button[]
            {
                this._starBtn1,
                this._starBtn2,
                this._starBtn3,
                this._starBtn4,
                this._starBtn5
            };
        }

        #endregion

        private System.Windows.Forms.Panel _header;
        private System.Windows.Forms.Button _refreshBtn;
        private System.Windows.Forms.Label headerTitleLabel;
        private System.Windows.Forms.Panel _statusBar;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.SplitContainer _split;
        private System.Windows.Forms.ListBox _tripList;
        private System.Windows.Forms.Label _listHeader;
        private System.Windows.Forms.Panel _ReviewPanel;
        private System.Windows.Forms.Panel submitRow;
        private System.Windows.Forms.Label _successLabel;
        private System.Windows.Forms.Button _submitBtn;
        private System.Windows.Forms.GroupBox commentGroup;
        private System.Windows.Forms.TextBox _commentBox;
        private System.Windows.Forms.GroupBox starGroup;
        private System.Windows.Forms.Label _scoreHint;
        private System.Windows.Forms.FlowLayoutPanel starFlow;
        private System.Windows.Forms.Button _starBtn1;
        private System.Windows.Forms.Button _starBtn2;
        private System.Windows.Forms.Button _starBtn3;
        private System.Windows.Forms.Button _starBtn4;
        private System.Windows.Forms.Button _starBtn5;
        private System.Windows.Forms.GroupBox tripInfoGroup;
        private System.Windows.Forms.Label _tripInfoLabel;
        private System.Windows.Forms.Panel _promptLabel;
        private System.Windows.Forms.Label promptTextLabel;
        private System.Windows.Forms.Button[] _starBtns;
    }
}

