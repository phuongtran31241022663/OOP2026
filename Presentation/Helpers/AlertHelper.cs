using System;
using System.Windows.Forms;

namespace Presentation.Helpers
{
    internal static class AlertHelper
    {
        public static void ShowFriendlyException(IWin32Window owner, InvalidOperationException ex, string actionName)
        {
            MessageBox.Show(
                owner,
                actionName + " khong the thuc hien luc nay. Vui long thu lai.\nChi tiet: " + ex.Message,
                "Loi thao tac",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void ShowFriendlyException(IWin32Window owner, FormatException ex, string actionName)
        {
            MessageBox.Show(
                owner,
                "Du lieu khong dung dinh dang khi " + actionName.ToLower() + ".\nChi tiet: " + ex.Message,
                "Loi dinh dang",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void ShowFriendlyException(IWin32Window owner, Exception ex, string actionName)
        {
            MessageBox.Show(
                owner,
                actionName + " that bai do loi khong mong muon. Vui long thu lai.\nChi tiet: " + ex.Message,
                "Loi he thong",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
