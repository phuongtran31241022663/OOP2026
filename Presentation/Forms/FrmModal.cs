using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Shells
{
    /// <summary>
    /// Form phu – hop thoai tap trung cho moi tac vu can su chu y.
    /// Host bat ky UserControl nao ben trong mot Panel noi dung.
    /// </summary>
    public partial class FrmModal : Form
    {
        private FrmModal()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9.5f);
            Size = new Size(560, 480);
        }

        public string ModalTitle
        {
            set { lblTitle.Text = value; }
        }

        public static DialogResult ShowModal(IWin32Window owner, UserControl content, string title)
        {
            using (var modal = new FrmModal())
            {
                modal.Text = title;
                modal.ModalTitle = title; // BUG FIX
                modal.pnlModalContent.Controls.Clear();
                content.Dock = DockStyle.Fill;
                modal.pnlModalContent.Controls.Add(content);
                return modal.ShowDialog(owner);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

