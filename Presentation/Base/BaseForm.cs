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
                ShowFriendlyException(ex, "FormKeyDown");
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
                ShowFriendlyException(ex, "FormClosing");
            }
        }

        // ─── Virtual Methods ─────────────────────────────────────────────────
        protected virtual void OnEscapePressed(KeyEventArgs e) => Close();
        protected virtual void OnFormClosingInternal(FormClosingEventArgs e) { }

        protected virtual void ShowHelp()
        {
            MessageBox.Show(this,
                "Phím tắt:\n- F1: Trợ giúp\n- Esc: Đóng form",
                "Trợ giúp",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: [{actionName}] {ex}{Environment.NewLine}");
            }
            catch { /* Ignore logging errors */ }
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
