using System.Drawing;
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
        /// Nạp dữ liệu vào thẻ
        /// </summary>
        /// <param name="title">Tiêu đề (ví dụ: Tổng doanh thu)</param>
        /// <param name="value">Giá trị (ví dụ: 1.200.000đ)</param>
        /// <param name="description">Mô tả (ví dụ: từ chuyến hoàn thành)</param>
        public void SetData(string title, string value, string description)
        {
            lblTitle.Text = title;
            lblValue.Text = value;
            lblDescription.Text = description;
        }

        /// <summary>
        /// Thiết lập màu sắc (Theme) cho thẻ
        /// </summary>
        public void SetTheme(Color backColor, Color titleColor, Color valueColor, Color descColor)
        {
            this.BackColor = backColor;
            lblTitle.ForeColor = titleColor;
            lblValue.ForeColor = valueColor;
            lblDescription.ForeColor = descColor;
        }
    }
}