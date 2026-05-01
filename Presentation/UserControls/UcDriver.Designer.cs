namespace Presentation.UserControls
{
    using Presentation.Constants;
    using System.Windows.Forms;

    partial class UcDriver
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.FlowLayoutPanel flpTopBar;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Button btnToggleStatus;
        private System.Windows.Forms.Label lblWallet;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.SplitContainer splitContent;
        private System.Windows.Forms.Panel pnlRequests;
        private System.Windows.Forms.Label lblRequestsTitle;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.Button btnAcceptRequest;
        private System.Windows.Forms.Button btnRejectRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTripId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPickup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;

        private System.Windows.Forms.Panel pnlCurrentTrip;
        private System.Windows.Forms.Label lblTripStatus;
        private System.Windows.Forms.Panel pnlNoTrip;
        private System.Windows.Forms.Panel pnlTripActions;
        private System.Windows.Forms.Button btnArrived;
        private System.Windows.Forms.Button btnStartTrip;
        private System.Windows.Forms.Button btnCompleteTrip;
        private System.Windows.Forms.Button btnCancelTrip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpTopBar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTopBar = this.flpTopBar;
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblWallet = new System.Windows.Forms.Label();
            this.btnToggleStatus = new System.Windows.Forms.Button();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.splitContent = new System.Windows.Forms.SplitContainer();
            this.pnlRequests = new System.Windows.Forms.Panel();
            this.btnRejectRequest = new System.Windows.Forms.Button();
            this.btnAcceptRequest = new System.Windows.Forms.Button();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.colTripId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPickup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRequestsTitle = new System.Windows.Forms.Label();
            this.pnlCurrentTrip = new System.Windows.Forms.Panel();
            this.pnlTripActions = new System.Windows.Forms.Panel();
            this.btnCancelTrip = new System.Windows.Forms.Button();
            this.btnCompleteTrip = new System.Windows.Forms.Button();
            this.btnStartTrip = new System.Windows.Forms.Button();
            this.btnArrived = new System.Windows.Forms.Button();
            this.pnlNoTrip = new System.Windows.Forms.Panel();
            this.lblTripStatus = new System.Windows.Forms.Label();
            this.tblMain.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContent)).BeginInit();
            this.splitContent.Panel1.SuspendLayout();
            this.splitContent.Panel2.SuspendLayout();
            this.splitContent.SuspendLayout();
            this.pnlRequests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.pnlCurrentTrip.SuspendLayout();
            this.pnlTripActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.pnlTopBar, 0, 0);
            this.tblMain.Controls.Add(this.splitContent, 0, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(1200, 800);
            this.tblMain.TabIndex = 0;
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlTopBar.Controls.Add(this.btnLogout);
            this.pnlTopBar.Controls.Add(this.btnProfile);
            this.pnlTopBar.Controls.Add(this.lblRating);
            this.pnlTopBar.Controls.Add(this.lblWallet);
            this.pnlTopBar.Controls.Add(this.btnToggleStatus);
            this.pnlTopBar.Controls.Add(this.lblDriverName);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopBar.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1194, 58);
            this.pnlTopBar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(1104, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(90, 36);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Thoát";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            this.btnProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Location = new System.Drawing.Point(1020, 12);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(90, 36);
            this.btnProfile.TabIndex = 4;
            this.btnProfile.Text = "Hồ sơ";
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRating.Location = new System.Drawing.Point(520, 18);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(80, 23);
            this.lblRating.TabIndex = 3;
            this.lblRating.Text = "Sao: 0.0 *";
            // 
            // lblWallet
            // 
            this.lblWallet.AutoSize = true;
            this.lblWallet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWallet.Location = new System.Drawing.Point(300, 18);
            this.lblWallet.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblWallet.Name = "lblWallet";
            this.lblWallet.Size = new System.Drawing.Size(98, 23);
            this.lblWallet.TabIndex = 2;
            this.lblWallet.Text = "Ví: 0đ";
            // 
            // btnToggleStatus
            // 
            this.btnToggleStatus.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnToggleStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleStatus.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnToggleStatus.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnToggleStatus.AutoSize = true;
            this.btnToggleStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnToggleStatus.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnToggleStatus.MinimumSize = Presentation.Constants.UiConstants.ButtonSizes.Action;
            this.btnToggleStatus.Location = new System.Drawing.Point(160, 8);
            this.btnToggleStatus.Name = "btnToggleStatus";
            this.btnToggleStatus.Size = new System.Drawing.Size(160, 40);
            this.btnToggleStatus.TabIndex = 1;
            this.btnToggleStatus.Text = "Bắt đầu hoạt động";
            this.btnToggleStatus.UseVisualStyleBackColor = false;
            // 
            // lblDriverName
            // 
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDriverName.Location = new System.Drawing.Point(12, 14);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(65, 28);
            this.lblDriverName.TabIndex = 0;
            this.lblDriverName.Text = "Tài xế";
            // 
            // splitContent
            // 
            this.splitContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContent.Location = new System.Drawing.Point(3, 67);
            this.splitContent.Name = "splitContent";
            // 
            // splitContent.Panel1
            // 
            this.splitContent.Panel1.Controls.Add(this.pnlRequests);
            // 
            // splitContent.Panel2
            // 
            this.splitContent.Panel2.Controls.Add(this.pnlCurrentTrip);
            this.splitContent.Size = new System.Drawing.Size(1194, 730);
            this.splitContent.SplitterDistance = 417;
            this.splitContent.TabIndex = 1;
            // 
            // pnlRequests
            // 
            this.pnlRequests.Controls.Add(this.btnRejectRequest);
            this.pnlRequests.Controls.Add(this.btnAcceptRequest);
            this.pnlRequests.Controls.Add(this.dgvRequests);
            this.pnlRequests.Controls.Add(this.lblRequestsTitle);
            this.pnlRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRequests.Location = new System.Drawing.Point(0, 0);
            this.pnlRequests.Name = "pnlRequests";
            this.pnlRequests.Size = new System.Drawing.Size(417, 730);
            this.pnlRequests.TabIndex = 0;
            // 
            // btnRejectRequest
            // 
            this.btnRejectRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRejectRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejectRequest.Location = new System.Drawing.Point(216, 688);
            this.btnRejectRequest.Name = "btnRejectRequest";
            this.btnRejectRequest.Size = new System.Drawing.Size(192, 32);
            this.btnRejectRequest.TabIndex = 3;
            this.btnRejectRequest.Text = "Từ chối";
            this.btnRejectRequest.UseVisualStyleBackColor = true;
            // 
            // btnAcceptRequest
            // 
            this.btnAcceptRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAcceptRequest.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnAcceptRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcceptRequest.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnAcceptRequest.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnAcceptRequest.AutoSize = true;
            this.btnAcceptRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnAcceptRequest.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnAcceptRequest.Location = new System.Drawing.Point(8, 688);
            this.btnAcceptRequest.Name = "btnAcceptRequest";
            this.btnAcceptRequest.Size = new System.Drawing.Size(192, 32);
            this.btnAcceptRequest.TabIndex = 2;
            this.btnAcceptRequest.Text = "Chấp nhận";
            this.btnAcceptRequest.UseVisualStyleBackColor = false;
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.BackgroundColor = System.Drawing.Color.White;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTripId,
            this.colPickup,
            this.colDestination,
            this.colDistance,
            this.colAmount});
            this.dgvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequests.Location = new System.Drawing.Point(0, 33);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.RowTemplate.Height = 24;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(417, 655);
            this.dgvRequests.TabIndex = 1;
            // 
            // colTripId
            // 
            this.colTripId.HeaderText = "TripId";
            this.colTripId.Name = "colTripId";
            this.colTripId.ReadOnly = true;
            this.colTripId.Visible = false;
            // 
            // colPickup
            // 
            this.colPickup.HeaderText = "Điểm đón";
            this.colPickup.Name = "colPickup";
            this.colPickup.ReadOnly = true;
            // 
            // colDestination
            // 
            this.colDestination.HeaderText = "Điểm đến";
            this.colDestination.Name = "colDestination";
            this.colDestination.ReadOnly = true;
            // 
            // colDistance
            // 
            this.colDistance.HeaderText = "Quãng đường";
            this.colDistance.Name = "colDistance";
            this.colDistance.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Giá cước";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // lblRequestsTitle
            // 
            this.lblRequestsTitle.AutoSize = true;
            this.lblRequestsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRequestsTitle.Location = new System.Drawing.Point(8, 8);
            this.lblRequestsTitle.Name = "lblRequestsTitle";
            this.lblRequestsTitle.Size = new System.Drawing.Size(134, 25);
            this.lblRequestsTitle.TabIndex = 0;
            this.lblRequestsTitle.Text = "Yêu cầu mới";
            // 
            // pnlCurrentTrip
            // 
            this.pnlCurrentTrip.Controls.Add(this.pnlTripActions);
            this.pnlCurrentTrip.Controls.Add(this.pnlNoTrip);
            this.pnlCurrentTrip.Controls.Add(this.lblTripStatus);
            this.pnlCurrentTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCurrentTrip.Location = new System.Drawing.Point(0, 0);
            this.pnlCurrentTrip.Name = "pnlCurrentTrip";
            this.pnlCurrentTrip.Size = new System.Drawing.Size(773, 730);
            this.pnlCurrentTrip.TabIndex = 0;
            // 
            // pnlTripActions
            // 
            this.pnlTripActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTripActions.Controls.Add(this.btnCancelTrip);
            this.pnlTripActions.Controls.Add(this.btnCompleteTrip);
            this.pnlTripActions.Controls.Add(this.btnStartTrip);
            this.pnlTripActions.Controls.Add(this.btnArrived);
            this.pnlTripActions.Location = new System.Drawing.Point(16, 80);
            this.pnlTripActions.Name = "pnlTripActions";
            this.pnlTripActions.Size = new System.Drawing.Size(740, 400);
            this.pnlTripActions.TabIndex = 2;
            // 
            // btnCancelTrip
            // 
            this.btnCancelTrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTrip.BackColor = Presentation.Constants.UiConstants.Colors.Danger;
            this.btnCancelTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTrip.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnCancelTrip.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnCancelTrip.AutoSize = true;
            this.btnCancelTrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnCancelTrip.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnCancelTrip.Location = new System.Drawing.Point(0, 288);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Size = new System.Drawing.Size(740, 48);
            this.btnCancelTrip.TabIndex = 3;
            this.btnCancelTrip.Text = "Huỷ chuyến";
            this.btnCancelTrip.UseVisualStyleBackColor = false;
            // 
            // btnCompleteTrip
            // 
            this.btnCompleteTrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompleteTrip.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnCompleteTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleteTrip.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnCompleteTrip.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnCompleteTrip.AutoSize = true;
            this.btnCompleteTrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnCompleteTrip.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnCompleteTrip.Location = new System.Drawing.Point(0, 192);
            this.btnCompleteTrip.Name = "btnCompleteTrip";
            this.btnCompleteTrip.Size = new System.Drawing.Size(740, 80);
            this.btnCompleteTrip.TabIndex = 2;
            this.btnCompleteTrip.Text = "Hoàn thành";
            this.btnCompleteTrip.UseVisualStyleBackColor = false;
            // 
            // btnStartTrip
            // 
            this.btnStartTrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartTrip.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnStartTrip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTrip.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnStartTrip.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnStartTrip.AutoSize = true;
            this.btnStartTrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnStartTrip.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnStartTrip.Location = new System.Drawing.Point(0, 96);
            this.btnStartTrip.Name = "btnStartTrip";
            this.btnStartTrip.Size = new System.Drawing.Size(740, 80);
            this.btnStartTrip.TabIndex = 1;
            this.btnStartTrip.Text = "Bắt đầu chuyến";
            this.btnStartTrip.UseVisualStyleBackColor = false;
            // 
            // btnArrived
            // 
            this.btnArrived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArrived.BackColor = Presentation.Constants.UiConstants.Colors.Primary;
            this.btnArrived.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArrived.Font = Presentation.Constants.UiConstants.Typography.BodyBold;
            this.btnArrived.ForeColor = Presentation.Constants.UiConstants.Colors.TextOnKey;
            this.btnArrived.AutoSize = true;
            this.btnArrived.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.btnArrived.Padding = Presentation.Constants.UiConstants.Spacing.ButtonPadding;
            this.btnArrived.Location = new System.Drawing.Point(0, 0);
            this.btnArrived.Name = "btnArrived";
            this.btnArrived.Size = new System.Drawing.Size(740, 80);
            this.btnArrived.TabIndex = 0;
            this.btnArrived.Text = "Đã đến điểm đón";
            this.btnArrived.UseVisualStyleBackColor = false;
            // 
            // pnlNoTrip
            // 
            this.pnlNoTrip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlNoTrip.Location = new System.Drawing.Point(200, 300);
            this.pnlNoTrip.Name = "pnlNoTrip";
            this.pnlNoTrip.Size = new System.Drawing.Size(360, 80);
            this.pnlNoTrip.TabIndex = 1;
            // 
            // lblTripStatus
            // 
            this.lblTripStatus.AutoSize = true;
            this.lblTripStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTripStatus.Location = new System.Drawing.Point(16, 16);
            this.lblTripStatus.Name = "lblTripStatus";
            this.lblTripStatus.Size = new System.Drawing.Size(106, 28);
            this.lblTripStatus.TabIndex = 0;
            this.lblTripStatus.Text = "Đang rảnh";
            // 
            // UcDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMain);
            this.Name = "UcDriver";
            this.Size = new System.Drawing.Size(1200, 800);
            this.tblMain.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.splitContent.Panel1.ResumeLayout(false);
            this.splitContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContent)).EndInit();
            this.splitContent.ResumeLayout(false);
            this.pnlRequests.ResumeLayout(false);
            this.pnlRequests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.pnlCurrentTrip.ResumeLayout(false);
            this.pnlCurrentTrip.PerformLayout();
            this.pnlTripActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

