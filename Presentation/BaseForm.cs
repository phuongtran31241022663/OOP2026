﻿// Presentation/BaseForm.cs
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    /// <summary>
    /// Base class cho tất cả dialog và screen Form trong ứng dụng.
    /// Cung cấp: keyboard shortcuts, helper methods, cursor management, loading state.
    /// KHÔNG dùng cho shell chính (dùng BaseShell thay thế).
    /// </summary>
    public partial class BaseForm : Form
    {
        // ─── State ───────────────────────────────────────────────────────────
        private bool _isLoading;

        /// <summary>
        /// True khi form đang trong trạng thái loading (block input).
        /// </summary>
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

        // ─── Constructor ─────────────────────────────────────────────────────
        public BaseForm()
        {
            InitializeComponent();
        }

        // ─── Keyboard Hooks ───────────────────────────────────────────────────
        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        OnEscapePressed(e);
                        break;
                    case Keys.F1:
                        ShowHelp();
                        e.Handled = true;
                        break;
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

        /// <summary>
        /// Hành vi mặc định khi nhấn Escape: đóng form.
        /// Override để thay đổi hành vi (ví dụ: confirm trước khi đóng).
        /// </summary>
        protected virtual void OnEscapePressed(KeyEventArgs e)
        {
            Close();
        }

        // ─── Lifecycle Hooks ─────────────────────────────────────────────────
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

        /// <summary>
        /// Override để chặn đóng form (ví dụ: xác nhận khi có dữ liệu chưa lưu).
        /// </summary>
        protected virtual void OnFormClosingInternal(FormClosingEventArgs e) { }

        // ─── Help ─────────────────────────────────────────────────────────────
        /// <summary>
        /// Override để cung cấp nội dung trợ giúp riêng cho từng form.
        /// </summary>
        protected virtual void ShowHelp()
        {
            MessageBox.Show(this,
                "Chưa có trợ giúp cho form này.",
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
            => MessageBox.Show(this, message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected bool ConfirmDelete(string itemName = "mục này")
            => Confirm($"Bạn có chắc chắn muốn xóa {itemName}?", "Xóa");

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

        protected void ShowFriendlyException(ArgumentException ex, string actionName)
        {
            ShowWarning(ex.Message, "Lỗi nhập liệu");
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
            catch (ArgumentException ex)
            {
                ShowFriendlyException(ex, actionName);
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
            catch (ArgumentException ex)
            {
                ShowFriendlyException(ex, actionName);
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
        /// <summary>
        /// Chạy action trên UI thread. An toàn khi gọi từ background thread.
        /// </summary>
        protected void RunOnUI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
