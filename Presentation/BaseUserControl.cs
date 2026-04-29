using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class BaseUserControl : UserControl
    {
        private bool _isLoading;
        protected ErrorProvider _validationErrorProvider;

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
            _validationErrorProvider = new ErrorProvider();
            _validationErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
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
            ExecuteWithHandling(action, null, null);
        }

        protected void ExecuteWithHandling(string context, Action action)
        {
            ExecuteWithHandling(action, null, context);
        }

        protected void ExecuteWithHandling(Action action, string successMessage, string errorContext)
        {
            try
            {
                action();
                if (!string.IsNullOrEmpty(successMessage))
                    ShowInfo(successMessage, "Thông báo");
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
                LogException(ex);
                ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
            }
        }

        protected async Task ExecuteWithHandlingAsync(Func<Task> action)
        {
            await ExecuteWithHandlingAsync(action, null, null);
        }

        protected async Task ExecuteWithHandlingAsync(string context, Func<Task> action, Action onComplete)
        {
            await ExecuteWithHandlingAsync(action, null, onComplete);
        }

        protected async Task ExecuteWithHandlingAsync(Func<Task> action, string successMessage, Action onSuccess)
        {
            try
            {
                await action();
                if (!string.IsNullOrEmpty(successMessage))
                    ShowInfo(successMessage, "Thông báo");
                onSuccess?.Invoke();
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
                LogException(ex);
                ShowError("Có lỗi hệ thống, vui lòng thử lại sau.", "Lỗi");
            }
        }

        protected bool ValidateControl(Control control, bool condition, string errorMessage)
        {
            if (condition)
            {
                _validationErrorProvider.SetError(control, null);
                return true;
            }
            else
            {
                _validationErrorProvider.SetError(control, errorMessage);
                return false;
            }
        }

        protected void ClearValidation()
        {
            _validationErrorProvider.Clear();
        }
    }
}
