using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    /// <summary>
    /// Base class cho cac UserControl tai su dung trong ung dung.
    /// Cung cap helper methods, loading state va xu ly loi UI thong nhat.
    /// </summary>
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
            _validationErrorProvider = new ErrorProvider(components) { ContainerControl = this };
        }

        protected void ShowInfo(string message, string caption = "Thong bao")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Canh bao")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Loi")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xac nhan")
            => MessageBox.Show(FindForm(), message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected bool ConfirmDelete(string itemName = "muc nay")
            => Confirm("Ban co chac chan muon xoa " + itemName + "?", "Xoa");

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

        protected void RunOnUI(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        protected void ValidateControl(Control ctrl, bool isValid, string errorMessage)
        {
            if (ctrl == null || _validationErrorProvider == null)
            {
                return;
            }

            if (isValid)
            {
                _validationErrorProvider.SetError(ctrl, string.Empty);
            }
            else
            {
                _validationErrorProvider.SetError(ctrl, errorMessage);
            }
        }

        protected virtual void OnRefreshData() { }
    }
}
