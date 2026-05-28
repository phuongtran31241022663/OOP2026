namespace OOP20262026
{
    partial class Form1
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txt = new System.Windows.Forms.TextBox();
            this.lnk = new System.Windows.Forms.LinkLabel();
            this.lbl = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.spl = new System.Windows.Forms.SplitContainer();
            this.pnl = new System.Windows.Forms.Panel();
            this.btn = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl)).BeginInit();
            this.spl.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(71, 19);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(100, 22);
            this.txt.TabIndex = 1;
            this.txt.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // lnk
            // 
            this.lnk.AutoSize = true;
            this.lnk.Location = new System.Drawing.Point(21, 44);
            this.lnk.Name = "lnk";
            this.lnk.Size = new System.Drawing.Size(24, 16);
            this.lnk.TabIndex = 2;
            this.lnk.TabStop = true;
            this.lnk.Text = "lnk";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(21, 22);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(21, 16);
            this.lbl.TabIndex = 3;
            this.lbl.Text = "lbl";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Location = new System.Drawing.Point(258, 125);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(200, 100);
            this.tab.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 71);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(19, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 71);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flp
            // 
            this.flp.Location = new System.Drawing.Point(24, 76);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(200, 100);
            this.flp.TabIndex = 6;
            // 
            // spl
            // 
            this.spl.Location = new System.Drawing.Point(258, 19);
            this.spl.Name = "spl";
            this.spl.Size = new System.Drawing.Size(150, 100);
            this.spl.TabIndex = 7;
            // 
            // pnl
            // 
            this.pnl.Location = new System.Drawing.Point(24, 182);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(200, 100);
            this.pnl.TabIndex = 8;
            // 
            // btn
            // 
            this.btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btn.FlatAppearance.BorderSize = 0;
            this.btn.Location = new System.Drawing.Point(177, 19);
            this.btn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 9;
            this.btn.Text = "btn";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.spl);
            this.Controls.Add(this.flp);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.lnk);
            this.Controls.Add(this.txt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spl)).EndInit();
            this.spl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private NotifyIcon notifyIcon1;
        private TextBox txt;
        private LinkLabel lnk;
        private Label lbl;
        private TabControl tab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flp;
        private SplitContainer spl;
        private Panel pnl;
        private Button btn;
    }
}

