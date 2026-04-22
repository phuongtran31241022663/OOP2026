// Presentation/Helpers/UIHelper.cs
using System;
using System.Windows.Forms;

namespace Presentation.Helpers
{
    public static class UIHelper
    {
        public static void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void ShowMessage(string message, string title = "Thông báo")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string message, string title = "Lỗi")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirm(string message, string title = "Xác nhận")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}