using System.Drawing;
using System.Windows.Forms;

namespace OOP2026
{
    partial class ucFareSelector
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMotorbike = new System.Windows.Forms.Panel();
            this.tlpMotorbike = new System.Windows.Forms.TableLayoutPanel();
            this.lblMotorbikeIcon = new System.Windows.Forms.Label();
            this.lblMotorbikePrice = new System.Windows.Forms.Label();
            this.pnlCar = new System.Windows.Forms.Panel();
            this.tlpCar = new System.Windows.Forms.TableLayoutPanel();
            this.lblCarIcon = new System.Windows.Forms.Label();
            this.lblCarPrice = new System.Windows.Forms.Label();

            this.tlpMain.SuspendLayout();
            this.pnlMotorbike.SuspendLayout();
            this.tlpMotorbike.SuspendLayout();
            this.pnlCar.SuspendLayout();
            this.tlpCar.SuspendLayout();
            this.SuspendLayout();

            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlMotorbike, 0, 0);
            this.tlpMain.Controls.Add(this.pnlCar, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(380, 90);
            this.tlpMain.TabIndex = 0;

            // 
            // pnlMotorbike
            // 
            this.pnlMotorbike.BackColor = System.Drawing.Color.White;
            this.pnlMotorbike.Controls.Add(this.tlpMotorbike);
            this.pnlMotorbike.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMotorbike.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMotorbike.Location = new System.Drawing.Point(0, 0);
            this.pnlMotorbike.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlMotorbike.Name = "pnlMotorbike";
            this.pnlMotorbike.Size = new System.Drawing.Size(380, 41);
            this.pnlMotorbike.TabIndex = 0;
            this.pnlMotorbike.Click += new System.EventHandler(this.OnMotorbikeClick);

            // 
            // tlpMotorbike
            // 
            this.tlpMotorbike.ColumnCount = 2;
            this.tlpMotorbike.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpMotorbike.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpMotorbike.Controls.Add(this.lblMotorbikeIcon, 0, 0);
            this.tlpMotorbike.Controls.Add(this.lblMotorbikePrice, 1, 0);
            this.tlpMotorbike.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotorbike.Location = new System.Drawing.Point(0, 0);
            this.tlpMotorbike.Name = "tlpMotorbike";
            this.tlpMotorbike.Padding = new System.Windows.Forms.Padding(8, 0, 12, 0);
            this.tlpMotorbike.RowCount = 1;
            this.tlpMotorbike.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotorbike.Size = new System.Drawing.Size(380, 41);
            this.tlpMotorbike.TabIndex = 0;

            // 
            // lblMotorbikeIcon
            // 
            this.lblMotorbikeIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotorbikeIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMotorbikeIcon.ForeColor = System.Drawing.Color.Black;
            this.lblMotorbikeIcon.Location = new System.Drawing.Point(11, 0);
            this.lblMotorbikeIcon.Name = "lblMotorbikeIcon";
            this.lblMotorbikeIcon.Size = new System.Drawing.Size(228, 41);
            this.lblMotorbikeIcon.TabIndex = 0;
            this.lblMotorbikeIcon.Text = "🏍️ Xe máy";
            this.lblMotorbikeIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMotorbikeIcon.Click += new System.EventHandler(this.OnMotorbikeClick);

            // 
            // lblMotorbikePrice
            // 
            this.lblMotorbikePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotorbikePrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMotorbikePrice.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74); // Màu xanh lá
            this.lblMotorbikePrice.Location = new System.Drawing.Point(245, 0);
            this.lblMotorbikePrice.Name = "lblMotorbikePrice";
            this.lblMotorbikePrice.Size = new System.Drawing.Size(120, 41);
            this.lblMotorbikePrice.TabIndex = 1;
            this.lblMotorbikePrice.Text = "0đ";
            this.lblMotorbikePrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMotorbikePrice.Click += new System.EventHandler(this.OnMotorbikeClick);

            // 
            // pnlCar
            // 
            this.pnlCar.BackColor = System.Drawing.Color.White;
            this.pnlCar.Controls.Add(this.tlpCar);
            this.pnlCar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCar.Location = new System.Drawing.Point(0, 45);
            this.pnlCar.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlCar.Name = "pnlCar";
            this.pnlCar.Size = new System.Drawing.Size(380, 41);
            this.pnlCar.TabIndex = 1;
            this.pnlCar.Click += new System.EventHandler(this.OnCarClick);

            // 
            // tlpCar
            // 
            this.tlpCar.ColumnCount = 2;
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpCar.Controls.Add(this.lblCarIcon, 0, 0);
            this.tlpCar.Controls.Add(this.lblCarPrice, 1, 0);
            this.tlpCar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCar.Location = new System.Drawing.Point(0, 0);
            this.tlpCar.Name = "tlpCar";
            this.tlpCar.Padding = new System.Windows.Forms.Padding(8, 0, 12, 0);
            this.tlpCar.RowCount = 1;
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCar.Size = new System.Drawing.Size(380, 41);
            this.tlpCar.TabIndex = 0;

            // 
            // lblCarIcon
            // 
            this.lblCarIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarIcon.ForeColor = System.Drawing.Color.Black;
            this.lblCarIcon.Location = new System.Drawing.Point(11, 0);
            this.lblCarIcon.Name = "lblCarIcon";
            this.lblCarIcon.Size = new System.Drawing.Size(228, 41);
            this.lblCarIcon.TabIndex = 0;
            this.lblCarIcon.Text = "🚕 Xe ô tô";
            this.lblCarIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCarIcon.Click += new System.EventHandler(this.OnCarClick);

            // 
            // lblCarPrice
            // 
            this.lblCarPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarPrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCarPrice.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblCarPrice.Location = new System.Drawing.Point(245, 0);
            this.lblCarPrice.Name = "lblCarPrice";
            this.lblCarPrice.Size = new System.Drawing.Size(120, 41);
            this.lblCarPrice.TabIndex = 1;
            this.lblCarPrice.Text = "0đ";
            this.lblCarPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCarPrice.Click += new System.EventHandler(this.OnCarClick);

            // 
            // ucFareSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(300, 110);
            this.Name = "ucFareSelector";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(400, 110);

            this.tlpMain.ResumeLayout(false);
            this.pnlMotorbike.ResumeLayout(false);
            this.tlpMotorbike.ResumeLayout(false);
            this.pnlCar.ResumeLayout(false);
            this.tlpCar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #region Controls declaration
        private TableLayoutPanel tlpMain;
        private Panel pnlMotorbike;
        private TableLayoutPanel tlpMotorbike;
        private Label lblMotorbikeIcon;
        private Label lblMotorbikePrice;
        private Panel pnlCar;
        private TableLayoutPanel tlpCar;
        private Label lblCarIcon;
        private Label lblCarPrice;
        #endregion
    }
}