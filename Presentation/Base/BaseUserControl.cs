using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class BaseUserControl : UserControl
    {
        private bool _isLoading;
        // bỏ thằng error provider đi, throw new exception đã đủ lắm rồi

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

        protected void ShowFriendlyException(Exception ex)
        {
            ShowFriendlyException(ex, null);
        }

        protected void ShowFriendlyException(Exception ex, string context)
        {
            string message = string.IsNullOrEmpty(context) ? ex.Message : $"[{context}] {ex.Message}";
            if (ex is ArgumentException || ex is ArgumentNullException || ex is ArgumentOutOfRangeException)
            {
                ShowWarning(ex.Message, "Lỗi nhập liệu");
            }
            else if (ex is InvalidOperationException)
            {
                ShowError(ex.Message, "Lỗi nghiệp vụ");
            }
            else
            {
                LogException(ex);
                ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
            }
        }
        // kệ mẹ đi, lưu ra file tao có đọc đâu , nào đọc được dòng này xóa hàm này giùm tao, LogException(Exception ex)
        protected void LogException(Exception ex)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {ex}{Environment.NewLine}");
            }
            catch { /* Bỏ qua lỗi ghi log */ }
        }

        protected void ExecuteWithHandling(Action action)
        {
            ExecuteWithHandling(null, action, null);
        }

        protected void ExecuteWithHandling(string context, Action action)
        {
            ExecuteWithHandling(context, action, null);
        }
        // đủ loại lỗi chưa
        protected void ExecuteWithHandling(string actionName, Action action, Action finallyAction = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (ArgumentException ex)
            {
                ShowFriendlyException(ex, actionName);
            }
            catch (InvalidOperationException ex)
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


        protected async Task ExecuteWithHandlingAsync(string context, Func<Task> action, Action onComplete)
        {
            await ExecuteWithHandlingAsync(action, null, onComplete);
        }

        protected async Task ExecuteWithHandlingAsync(Func<Task> action, string successMessage, Action onComplete)
        {
            try
            {
                if (action != null) await action();
                if (!string.IsNullOrEmpty(successMessage))
                    ShowInfo(successMessage, "Thông báo");
            }
            catch (ArgumentException ex)
            {
                ShowFriendlyException(ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowFriendlyException(ex);
            }
            catch (Exception ex)
            {
                ShowFriendlyException(ex);
            }
            finally
            {
                onComplete?.Invoke();
            }
        }
        // bỏ error provider đi thì bỏ cái này ValidateControl(Control control, bool condition, string 
        protected bool ValidateControl(Control control, bool condition, string errorMessage)
        {
            return condition;
        }
    }
}
