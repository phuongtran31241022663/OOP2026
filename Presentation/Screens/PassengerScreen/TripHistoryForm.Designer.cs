using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using System.Windows.Forms;

namespace Presentation.Screens.PassengerScreen
{
    partial class TripHistoryForm : BaseForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._header = new System.Windows.Forms.Panel();
            this._refreshBtn = new System.Windows.Forms.Button();
            this._titleLabel = new System.Windows.Forms.Label();
            this._summaryBar = new System.Windows.Forms.Panel();
            this.summaryTable = new System.Windows.Forms.TableLayoutPanel();
            this._totalLabel = new System.Windows.Forms.Label();
            this._spentLabel = new System.Windows.Forms.Label();
            this._completedLabel = new System.Windows.Forms.Label();
            this._cancelledLabel = new System.Windows.Forms.Label();
            this._statusBar = new System.Windows.Forms.Panel();
            this._statusLabel = new System.Windows.Forms.Label();
            this._grid = new System.Windows.Forms.DataGridView();
            this.colPickup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._header.SuspendLayout();
            this._summaryBar.SuspendLayout();
            this.summaryTable.SuspendLayout();
            this._statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.SystemColors.ControlDark;
            this._header.Controls.Add(this._refreshBtn);
            this._header.Controls.Add(this._titleLabel);
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
            this._refreshBtn.Click += new System.EventHandler(this.OnRefreshClicked);
            // 
            // _titleLabel
            // 
            this._titleLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._titleLabel.Location = new System.Drawing.Point(10, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(220, 40);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Lá»‹ch sá»­ chuyáº¿n Ä‘i";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _summaryBar
            // 
            this._summaryBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this._summaryBar.Controls.Add(this.summaryTable);
            this._summaryBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._summaryBar.Location = new System.Drawing.Point(0, 40);
            this._summaryBar.Name = "_summaryBar";
            this._summaryBar.Size = new System.Drawing.Size(1024, 40);
            this._summaryBar.TabIndex = 1;
            // 
            // summaryTable
            // 
            this.summaryTable.ColumnCount = 4;
            this.summaryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.summaryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.summaryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.summaryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.summaryTable.Controls.Add(this._totalLabel, 0, 0);
            this.summaryTable.Controls.Add(this._spentLabel, 1, 0);
            this.summaryTable.Controls.Add(this._completedLabel, 2, 0);
            this.summaryTable.Controls.Add(this._cancelledLabel, 3, 0);
            this.summaryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryTable.Location = new System.Drawing.Point(0, 0);
            this.summaryTable.Name = "summaryTable";
            this.summaryTable.RowCount = 1;
            this.summaryTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryTable.Size = new System.Drawing.Size(1024, 40);
            this.summaryTable.TabIndex = 0;
            // 
            // _totalLabel
            // 
            this._totalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._totalLabel.Location = new System.Drawing.Point(3, 0);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(250, 40);
            this._totalLabel.TabIndex = 0;
            this._totalLabel.Text = "Tá»•ng chuyáº¿n: 0";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _spentLabel
            // 
            this._spentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._spentLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._spentLabel.Location = new System.Drawing.Point(259, 0);
            this._spentLabel.Name = "_spentLabel";
            this._spentLabel.Size = new System.Drawing.Size(250, 40);
            this._spentLabel.TabIndex = 1;
            this._spentLabel.Text = "Tá»•ng chi tiÃªu: 0 Ä‘";
            this._spentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _completedLabel
            // 
            this._completedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._completedLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._completedLabel.Location = new System.Drawing.Point(515, 0);
            this._completedLabel.Name = "_completedLabel";
            this._completedLabel.Size = new System.Drawing.Size(250, 40);
            this._completedLabel.TabIndex = 2;
            this._completedLabel.Text = "HoÃ n thÃ nh: 0";
            this._completedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _cancelledLabel
            // 
            this._cancelledLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cancelledLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._cancelledLabel.Location = new System.Drawing.Point(771, 0);
            this._cancelledLabel.Name = "_cancelledLabel";
            this._cancelledLabel.Size = new System.Drawing.Size(250, 40);
            this._cancelledLabel.TabIndex = 3;
            this._cancelledLabel.Text = "ÄÃ£ há»§y: 0";
            this._cancelledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _statusBar
            // 
            this._statusBar.BackColor = System.Drawing.SystemColors.ControlDark;
            this._statusBar.Controls.Add(this._statusLabel);
            this._statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusBar.Location = new System.Drawing.Point(0, 618);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(1024, 22);
            this._statusBar.TabIndex = 4;
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
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.SystemColors.Window;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.ColumnHeadersHeight = 26;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPickup,
            this.colDestination,
            this.colDistance,
            this.colFare,
            this.colStatus,
            this.colDate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this._grid.Location = new System.Drawing.Point(0, 80);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            this._grid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this._grid.RowTemplate.Height = 22;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1024, 538);
            this._grid.TabIndex = 2;
            this._grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnCellFormatting);
            // 
            // colPickup
            // 
            this.colPickup.FillWeight = 2F;
            this.colPickup.HeaderText = "Äiá»ƒm Ä‘Ã³n";
            this.colPickup.MinimumWidth = 6;
            this.colPickup.Name = "Pickup";
            this.colPickup.ReadOnly = true;
            // 
            // colDestination
            // 
            this.colDestination.FillWeight = 2F;
            this.colDestination.HeaderText = "Äiá»ƒm Ä‘áº¿n";
            this.colDestination.MinimumWidth = 6;
            this.colDestination.Name = "Destination";
            this.colDestination.ReadOnly = true;
            // 
            // colDistance
            // 
            this.colDistance.FillWeight = 1F;
            this.colDistance.HeaderText = "Khoáº£ng cÃ¡ch";
            this.colDistance.MinimumWidth = 6;
            this.colDistance.Name = "Distance";
            this.colDistance.ReadOnly = true;
            // 
            // colFare
            // 
            this.colFare.FillWeight = 1F;
            this.colFare.HeaderText = "GiÃ¡ tiá»n";
            this.colFare.MinimumWidth = 6;
            this.colFare.Name = "Fare";
            this.colFare.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 1F;
            this.colStatus.HeaderText = "Tráº¡ng thÃ¡i";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "Status";
            this.colStatus.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.FillWeight = 1.1F;
            this.colDate.HeaderText = "Thá»i gian";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "Date";
            this.colDate.ReadOnly = true;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._emptyLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this._emptyLabel.Location = new System.Drawing.Point(0, 80);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(1024, 538);
            this._emptyLabel.TabIndex = 3;
            this._emptyLabel.Text = "ChÆ°a cÃ³ chuyáº¿n Ä‘i nÃ o";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._emptyLabel.Visible = false;
            // 
            // TripHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 640);
            this.Controls.Add(this._emptyLabel);
            this.Controls.Add(this._grid);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this._summaryBar);
            this.Controls.Add(this._header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TripHistoryForm";
            this.Text = "Trip History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TripHistoryForm_Load);
            this._header.ResumeLayout(false);
            this._summaryBar.ResumeLayout(false);
            this.summaryTable.ResumeLayout(false);
            this._statusBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(240, 244, 252);

            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        }

        #endregion

        private System.Windows.Forms.Panel _header;
        private System.Windows.Forms.Button _refreshBtn;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Panel _summaryBar;
        private System.Windows.Forms.TableLayoutPanel summaryTable;
        private System.Windows.Forms.Label _totalLabel;
        private System.Windows.Forms.Label _spentLabel;
        private System.Windows.Forms.Label _completedLabel;
        private System.Windows.Forms.Label _cancelledLabel;
        private System.Windows.Forms.Panel _statusBar;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.Label _emptyLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPickup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFare;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
    }
}

