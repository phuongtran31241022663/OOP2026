// Presentation/BaseShell.cs
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class BaseShell : Form
    {
        public TabControl MainTabControl => tabControlMain;

        public BaseShell()
        {
            InitializeComponent();
        }

        private void BaseShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                OnShellClosing(e);
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Loi nhap lieu");
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message, "Loi nghiep vu");
            }
            catch (Exception ex)
            {
                ShowError("Co loi he thong, vui long thu lai sau.", "Loi");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
            }
        }

        protected virtual void OnShellClosing(FormClosingEventArgs e) { }

        private void BaseShell_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    ShowShellHelp();
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
                }
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Loi nhap lieu");
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message, "Loi nghiep vu");
            }
            catch (Exception ex)
            {
                ShowError("Co loi he thong, vui long thu lai sau.", "Loi");
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
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
                "Phim tat:\n- Ctrl+Tab: Tab tiep theo\n- Ctrl+Shift+Tab: Tab truoc\n- F1: Tro giup",
                "Tro giup",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
            if (page == null || !tabControlMain.TabPages.Contains(page))
                return false;
            tabControlMain.TabPages.Remove(page);
            page.Dispose();
            return true;
        }

        public void CloseCurrentTab()
        {
            if (tabControlMain.SelectedTab != null)
                CloseTab(tabControlMain.SelectedTab);
        }

        protected void SetStatusText(string text)
            => toolStripStatusLabel.Text = text;

        protected void ShowProgress(bool visible, int percent = 0)
        {
            toolStripProgressBar.Visible = visible;
            if (visible)
                toolStripProgressBar.Value = Math.Max(0, Math.Min(100, percent));
        }

        protected void ShowInfo(string message, string caption = "Thong bao")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

        protected void ShowWarning(string message, string caption = "Canh bao")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        protected void ShowError(string message, string caption = "Loi")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        protected bool Confirm(string message, string caption = "Xac nhan")
            => MessageBox.Show(this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        protected void ShowFriendlyException(ArgumentException ex, string actionName)
        {
            ShowWarning($"[{actionName}] {ex.Message}", "Loi nhap lieu");
        }

        protected void ShowFriendlyException(InvalidOperationException ex, string actionName)
        {
            ShowError($"[{actionName}] {ex.Message}", "Loi nghiep vu");
        }

        protected void ShowFriendlyException(Exception ex, string actionName)
        {
            File.AppendAllText("error.log", $"{DateTime.Now}: [{actionName}] {ex}\n");
            ShowError("Co loi he thong, vui long thu lai sau.", "Loi");
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
                    await action();
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

        protected void RunOnUI(Action action)
        {
            if (InvokeRequired) Invoke(action);
            else action();
        }

        private void CloseTabMenuItem_Click(object sender, EventArgs e) => CloseCurrentTab();
        private void ExitMenuItem_Click(object sender, EventArgs e) => Close();
        private void AboutMenuItem_Click(object sender, EventArgs e)
            => ShowInfo("Ung dung Ride-sharing - Phien ban 1.0", "Gioi thieu");
    }
}
