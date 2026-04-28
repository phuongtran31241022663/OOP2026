// Presentation/BaseShell.cs
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    /// <summary>
    /// Base class cho tất cả shell chính của ứng dụng (PassengerShell, DriverShell...).
    /// Shell = cửa sổ top-level có TabControl, MenuStrip, StatusStrip.
    /// Phân biệt với BaseForm: BaseShell là khung chứa, BaseForm là nội dung bên trong.
    /// </summary>
    public partial class BaseShell : Form
    {
        // ─── Accessor ─────────────────────────────────────────────────────────
        /// <summary>Expose TabControl để subclass điều hướng tab.</summary>
        public TabControl MainTabControl => tabControlMain;

        // ─── Constructor ─────────────────────────────────────────────────────
        public BaseShell()
        {
            InitializeComponent();
        }

        // ─── Lifecycle ────────────────────────────────────────────────────────
        private void BaseShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnShellClosing(e);
        }

        /// <summary>Override để xử lý logic trước khi shell đóng (confirm, cleanup...).</summary>
        protected virtual void OnShellClosing(FormClosingEventArgs e) { }

        // ─── Keyboard Shortcuts ───────────────────────────────────────────────
        private void BaseShell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ShowShellHelp();
                e.Handled = true;
                return;
            }

            // Ctrl+Tab: chuyển tab tiếp theo
            if (e.Control && !e.Shift && e.KeyCode == Keys.Tab)
            {
                CycleTab(forward: true);
                e.Handled = true;
                return;
            }

            // Ctrl+Shift+Tab: chuyển tab trước đó
            if (e.Control && e.Shift && e.KeyCode == Keys.Tab)
            {
                CycleTab(forward: false);
                e.Handled = true;
            }
        }

        private void CycleTab(bool forward)
        {
            int count = tabControlMain.TabCount;
            if (count == 0) return;

            int current = tabControlMain.SelectedIndex;
            tabControlMain.SelectedIndex = forward
                ? (current + 1) % count
                : (current - 1 + count) % count;
        }

        protected virtual void ShowShellHelp()
        {
            MessageBox.Show(this,
                "Phím tắt:\n- Ctrl+Tab: Tab tiếp theo\n- Ctrl+Shift+Tab: Tab trước\n- F1: Trợ giúp",
                "Trợ giúp",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        public void AddTab(BaseForm contentForm, string title, bool closeable = true)
        {
            if (contentForm == null) throw new ArgumentNullException(nameof(contentForm));

            // Chuẩn bị form để nhúng
            contentForm.TopLevel = false;
            contentForm.FormBorderStyle = FormBorderStyle.None;
            contentForm.Dock = DockStyle.Fill;
            contentForm.Visible = true;

            var tabPage = new TabPage(title);
            tabPage.Controls.Add(contentForm);
            tabControlMain.TabPages.Add(tabPage);
            tabControlMain.SelectedTab = tabPage;

            if (closeable)
            {
                // Có thể đăng ký sự kiện đóng tab từ form con (tuỳ chọn)
                // contentForm.FormClosing += (s, e) => { if (e.CloseReason == CloseReason.UserClosing) CloseTab(tabPage); };
            }
        }

        /// <summary>Đóng và giải phóng TabPage chỉ định. Trả về false nếu không tìm thấy.</summary>
        public bool CloseTab(TabPage page)
        {
            if (page == null || !tabControlMain.TabPages.Contains(page))
                return false;

            tabControlMain.TabPages.Remove(page);
            page.Dispose();
            return true;
        }

        /// <summary>Đóng tab hiện tại đang được chọn.</summary>
        public void CloseCurrentTab()
        {
            if (tabControlMain.SelectedTab != null)
                CloseTab(tabControlMain.SelectedTab);
        }

        // ─── Status Bar ────────────────────────────────────────────────────────
        protected void SetStatusText(string text)
            => toolStripStatusLabel.Text = text;

        protected void ShowProgress(bool visible, int percent = 0)
        {
            toolStripProgressBar.Visible = visible;
            if (visible)
                toolStripProgressBar.Value = Math.Max(0, Math.Min(100, percent));
        }

        // ─── MessageBox Helpers ───────────────────────────────────────────────
        protected void ShowInfo(string message, string caption = "Thông báo")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Cảnh báo")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Lỗi")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xác nhận")
            => MessageBox.Show(this, message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected void ShowFriendlyException(InvalidOperationException ex, string actionName)
        {
            ShowError(
                actionName + " khong the thuc hien luc nay. Vui long thu lai.\nChi tiet: " + ex.Message,
                "Loi thao tac");
        }

        protected void ShowFriendlyException(FormatException ex, string actionName)
        {
            ShowError(
                "Du lieu nhap vao khong dung dinh dang khi " + actionName.ToLower() + ".\nChi tiet: " + ex.Message,
                "Loi dinh dang");
        }

        protected void ShowFriendlyException(Exception ex, string actionName)
        {
            ShowError(
                actionName + " that bai do loi khong mong muon. Vui long thu lai.\nChi tiet: " + ex.Message,
                "Loi he thong");
        }

        protected void ExecuteWithHandling(string actionName, Action action, Action finallyAction = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        protected async Task ExecuteWithHandlingAsync(string actionName, Func<Task> action, Action finallyAction = null)
        {
            try
            {
                if (action != null)
                {
                    await action();
                }
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            catch (FormatException ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        // ─── Cursor Helpers ────────────────────────────────────────────────────
        protected void ShowWaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
            UseWaitCursor = true;
        }

        protected void ShowDefaultCursor()
        {
            Cursor.Current = Cursors.Default;
            UseWaitCursor = false;
        }

        // ─── Invoke Helper ────────────────────────────────────────────────────
        /// <summary>Chạy action trên UI thread. An toàn từ background thread.</summary>
        protected void RunOnUI(Action action)
        {
            if (InvokeRequired) Invoke(action);
            else action();
        }

        // ─── Menu Handlers ────────────────────────────────────────────────────
        private void CloseTabMenuItem_Click(object sender, EventArgs e) => CloseCurrentTab();
        private void ExitMenuItem_Click(object sender, EventArgs e) => Close();
        private void AboutMenuItem_Click(object sender, EventArgs e)
            => ShowInfo("Ứng dụng Ride-sharing - Phiên bản 1.0", "Giới thiệu");
    }
}
