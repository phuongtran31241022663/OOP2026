// Presentation/BaseForm.cs
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class BaseForm : Form
    {
        public TabControl MainTabControl => tabControlMain;

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            protected set
            {
                _isLoading = value;
                if (value) ShowWaitCursor();
                else ShowDefaultCursor();
            }
        }

        public BaseForm()
        {
            InitializeComponent();
        }

        // ─── Event Handlers ──────────────────────────────────────────────────
        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    ShowHelp();
                    e.Handled = true;
                    return;
                }
                if (e.Control && !e.Shift && e.KeyCode == Keys.Tab)
                {
                    CycleTab(forward: true);
                    e.Handled = true;
                    return;
                }
                if (e.Control && e.Shift && e.KeyCode == Keys.Tab)
                {
                    CycleTab(forward: false);
                    e.Handled = true;
                    return;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    OnEscapePressed(e);
                    e.Handled = true;
                }
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Lỗi nhập liệu");
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message, "Lỗi nghiệp vụ");
            }
            catch (Exception ex)
            {
                ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
            }
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                OnFormClosingInternal(e);
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Lỗi nhập liệu");
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message, "Lỗi nghiệp vụ");
            }
            catch (Exception ex)
            {
                ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
            }
        }

        // ─── Virtual Methods ─────────────────────────────────────────────────
        protected virtual void OnEscapePressed(KeyEventArgs e) => Close();
        protected virtual void OnFormClosingInternal(FormClosingEventArgs e) { }

        protected virtual void ShowHelp()
        {
            MessageBox.Show(this,
                "Phím tắt:\n- Ctrl+Tab: Tab tiếp theo\n- Ctrl+Shift+Tab: Tab trước\n- F1: Trợ giúp\n- Esc: Đóng form",
                "Trợ giúp",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ─── Tab Control ──────────────────────────────────────────────────────
        private void CycleTab(bool forward)
        {
            int count = tabControlMain.TabCount;
            if (count == 0) return;
            int current = tabControlMain.SelectedIndex;
            tabControlMain.SelectedIndex = forward
                ? (current + 1) % count
                : (current - 1 + count) % count;
        }

        public void AddTab(BaseForm contentForm, string title, bool closeable = true)
        {
            if (contentForm == null) throw new ArgumentNullException(nameof(contentForm));
            contentForm.TopLevel = false;
            contentForm.FormBorderStyle = FormBorderStyle.None;
            contentForm.Dock = DockStyle.Fill;
            contentForm.Visible = true;
            var tabPage = new TabPage(title);
            tabPage.Controls.Add(contentForm);
            tabControlMain.TabPages.Add(tabPage);
            tabControlMain.SelectedTab = tabPage;
        }

        public bool CloseTab(TabPage page)
        {
            if (page == null || !tabControlMain.TabPages.Contains(page)) return false;
            tabControlMain.TabPages.Remove(page);
            page.Dispose();
            return true;
        }

        public void CloseCurrentTab()
        {
            if (tabControlMain.SelectedTab != null)
                CloseTab(tabControlMain.SelectedTab);
        }

        // ─── MessageBox Helpers ───────────────────────────────────────────────
        protected void ShowInfo(string message, string caption = "Thông báo")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Cảnh báo")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Lỗi")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xác nhận")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected bool ConfirmDelete(string itemName = "mục này")
            => Confirm($"Bạn có chắc chắn muốn xóa {itemName}?", "Xóa");

        // ─── Exception Helpers ────────────────────────────────────────────────
        protected void ShowFriendlyException(ArgumentException ex, string actionName)
            => ShowWarning($"[{actionName}] {ex.Message}", "Lỗi nhập liệu");

        protected void ShowFriendlyException(InvalidOperationException ex, string actionName)
            => ShowError($"[{actionName}] {ex.Message}", "Lỗi nghiệp vụ");

        protected void ShowFriendlyException(Exception ex, string actionName)
        {
            File.AppendAllText("error.log", $"{DateTime.Now}: [{actionName}] {ex}\n");
            ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
        }

        // ─── Safe Execution Wrappers ─────────────────────────────────────────
        protected void ExecuteWithHandling(string actionName, Action action, Action finallyAction = null)
        {
            try { action?.Invoke(); }
            catch (ArgumentException ex) { ShowFriendlyException(ex, actionName); }
            catch (InvalidOperationException ex) { ShowFriendlyException(ex, actionName); }
            catch (Exception ex) { ShowFriendlyException(ex, actionName); }
            finally { finallyAction?.Invoke(); }
        }

        protected async Task ExecuteWithHandlingAsync(string actionName, Func<Task> action, Action finallyAction = null)
        {
            try
            {
                if (action != null) await action();
            }
            catch (ArgumentException ex) { ShowFriendlyException(ex, actionName); }
            catch (InvalidOperationException ex) { ShowFriendlyException(ex, actionName); }
            catch (Exception ex) { ShowFriendlyException(ex, actionName); }
            finally { finallyAction?.Invoke(); }
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

        // ─── UI Thread Helper ─────────────────────────────────────────────────
        protected void RunOnUI(Action action)
        {
            if (InvokeRequired) Invoke(action);
            else action();
        }
    }
}