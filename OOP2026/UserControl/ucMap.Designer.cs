using System;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucMap
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private GMap.NET.WindowsForms.GMapControl _gMapControl;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Ng?t liên k?t các s? ki?n ph?n c?ng d? GC d? dàng thu h?i
                if (_gMapControl != null)
                {
                    _gMapControl.OnMarkerClick -= OnMarkerClick;
                    _gMapControl.OnMapDrag -= OnMapDrag;
                    _gMapControl.MouseDown -= OnMapMouseDown;
                    _gMapControl.MouseUp -= OnMapMouseUp;
                    _gMapControl.MouseClick -= OnMapMouseClick;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the content of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // _gMapControl
            // 
            this._gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gMapControl.Location = new System.Drawing.Point(0, 0);
            this._gMapControl.Name = "_gMapControl";
            this._gMapControl.Size = new System.Drawing.Size(400, 300);
            this._gMapControl.TabIndex = 0;
            // 
            // ucMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gMapControl);
            this.Name = "ucMap"; // S?A: Ð?i tên d?nh danh d?ng b? chính xác v?i Class
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
