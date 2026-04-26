using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
namespace Presentation.Components
{
    partial class MapControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._gMapControl);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);
        }
    }
}

