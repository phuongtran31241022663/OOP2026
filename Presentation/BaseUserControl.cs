using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class BaseUserControl : UserControl
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            protected set
            {
                _isLoading = value;
                Enabled = !value;
                Cursor.Current = value ? Cursors.WaitCursor : Cursors.Default;
            }
        }

        public BaseUserControl()
        {
            InitializeComponent();
        }

        protected void ShowInfo(string message, string caption = "Thông báo")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Cảnh báo")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Lỗi")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xác nhận")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected void RunOnUI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}