using System;
using System.Windows.Forms;

namespace Presentation
{
    /// <summary>
    /// Base class cho tất cả BaseUserControl tái sử dụng trong ứng dụng (TripCard, DriverCard...).
    /// Cung cấp: helper methods, invoke helper, loading state chung.
    /// Phân biệt với BaseTabBaseUserControl: BaseBaseUserControl là component nhúng vào form,
    /// không có khả năng yêu cầu đóng tab.
    /// </summary>
    public partial class BaseUserControl : UserControl
    {
        // ─── State ───────────────────────────────────────────────────────────
        private bool _isLoading;

        /// <summary>
        /// True khi control đang trong trạng thái loading.
        /// Set = true sẽ hiện wait cursor và disable control.
        /// </summary>
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

        // ─── Constructor ─────────────────────────────────────────────────────
        public BaseUserControl()
        {
            InitializeComponent();
        }

        // ─── MessageBox Helpers ───────────────────────────────────────────────
        /// <summary>
        /// Tìm ParentForm để làm owner cho MessageBox (tránh dialog bị che khuất).
        /// Nếu không có ParentForm thì show không có owner.
        /// </summary>
        protected void ShowInfo(string message, string caption = "Thông báo")
            => MessageBox.Show(FindForm(), message, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Cảnh báo")
            => MessageBox.Show(FindForm(), message, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Lỗi")
            => MessageBox.Show(FindForm(), message, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xác nhận")
            => MessageBox.Show(FindForm(), message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected bool ConfirmDelete(string itemName = "mục này")
            => Confirm($"Bạn có chắc chắn muốn xóa {itemName}?", "Xóa");

        // ─── Invoke Helper ────────────────────────────────────────────────────
        /// <summary>
        /// Chạy action trên UI thread. An toàn khi gọi từ background thread hoặc Timer.
        /// </summary>
        protected void RunOnUI(Action action)
        {
            if (InvokeRequired) Invoke(action);
            else action();
        }

        // ─── Virtual Hooks ────────────────────────────────────────────────────
        /// <summary>
        /// Gọi khi control cần load/refresh dữ liệu.
        /// Override trong subclass để tải dữ liệu từ service.
        /// </summary>
        protected virtual void OnRefreshData() { }
    }
}