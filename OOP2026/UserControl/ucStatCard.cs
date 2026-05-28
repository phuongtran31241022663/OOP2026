using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OOP2026
{
    public partial class ucStatCard : UserControl
    {
        public ucStatCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nạp dữ liệu thống kê an toàn đa luồng
        /// </summary>
        public void SetData(string title, string value)
        {
            lblTitle.Text = title;
            lblValue.Text = value;
        }
    }
}
