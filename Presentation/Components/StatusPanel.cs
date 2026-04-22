using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Presentation.Components
{
    /// <summary>
    /// Panel hiển thị trạng thái hệ thống và log lỗi theo thời gian thực.
    /// Giúp người dùng dễ dàng nhận biết mọi lỗi xảy ra trong ứng dụng.
    /// </summary>
    public partial class StatusPanel : Panel
    {
        private readonly ConcurrentQueue<LogEntry> _logEntries = new ConcurrentQueue<LogEntry>();
        private int _errorCount = 0;
        private int _warningCount = 0;

        public StatusPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thêm một log entry và hiển thị
        /// </summary>
        public void AddLog(string message, LogLevel level)
        {
            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message,
                Level = level
            };

            _logEntries.Enqueue(entry);

            // Cập nhật màu sắc theo level
            Color textColor;
            string prefix;
            if (level == LogLevel.Error)
            {
                textColor = Color.Red;
                prefix = "[LỖI]";
            }
            else if (level == LogLevel.Warning)
            {
                textColor = Color.Yellow;
                prefix = "[CẢNH BÁO]";
            }
            else if (level == LogLevel.Debug)
            {
                textColor = Color.Gray;
                prefix = "[DEBUG]";
            }
            else
            {
                textColor = Color.Green;
                prefix = "[INFO]";
            }

            string logLine = $"[{entry.Timestamp:HH:mm:ss}] {prefix} {message}\n";

            // Cập nhật RichTextBox an toàn từ thread khác
            if (_logTextBox.InvokeRequired)
            {
                _logTextBox.Invoke(new Action<string, Color>((l, c) => AppendLogLine(l, c)), logLine, textColor);
            }
            else
            {
                AppendLogLine(logLine, textColor);
            }

            // Cập nhật đếm lỗi
            if (level == LogLevel.Error)
            {
                _errorCount++;
                UpdateErrorCount();
            }
            else if (level == LogLevel.Warning)
            {
                _warningCount++;
                UpdateErrorCount();
            }

            // Cập nhật status label nếu là lỗi
            if (level == LogLevel.Error)
            {
                UpdateStatus("● Có lỗi xảy ra!", Color.Red);
            }
        }

        private void AppendLogLine(string line, Color color)
        {
            _logTextBox.SelectionStart = _logTextBox.TextLength;
            _logTextBox.SelectionColor = color;
            _logTextBox.AppendText(line);
            _logTextBox.SelectionStart = _logTextBox.TextLength;
            _logTextBox.ScrollToCaret();
        }

        private void UpdateErrorCount()
        {
            _errorCountLabel.Text = $"Lỗi: {_errorCount} | Cảnh báo: {_warningCount}";

            // Đổi màu nếu có lỗi
            if (_errorCount > 0)
            {
                _errorCountLabel.ForeColor = Color.Red;
            }
            else if (_warningCount > 0)
            {
                _errorCountLabel.ForeColor = Color.Yellow;
            }
            else
            {
                _errorCountLabel.ForeColor = Color.White;
            }
        }

        private void UpdateStatus(string status, Color color)
        {
            _statusLabel.Text = status;
            _statusLabel.ForeColor = color;
        }

        private void ClearLogs(object sender, EventArgs e)
        {
            // _logEntries.Clear(); // No Clear method
            _errorCount = 0;
            _warningCount = 0;

            if (_logTextBox.InvokeRequired)
            {
                _logTextBox.Invoke(new Action(_logTextBox.Clear));
            }
            else
            {
                _logTextBox.Clear();
            }

            UpdateErrorCount();
            UpdateStatus("● Sẵn sàng", Color.Green);
        }

        private void OpenLogFile(object sender, EventArgs e)
        {
            try
            {
                string logPath = "log.txt"; // Mock
                if (File.Exists(logPath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = logPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("File log không tồn tại: " + logPath, "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở file log:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị thông báo lỗi với chi tiết đầy đủ
        /// </summary>
        public void ShowError(string title, string message, Exception ex = null)
        {
            string fullMessage = message;
            if (ex != null)
            {
                fullMessage += $"\n\nChi tiết lỗi:\n{ex.GetType().Name}: {ex.Message}";
                if (ex.StackTrace != null)
                {
                    fullMessage += $"\n\nStack Trace:\n{ex.StackTrace}";
                }
            }

            AddLog($"{title}: {message}", LogLevel.Error);

            // Hiển thị MessageBox
            MessageBox.Show(fullMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hiển thị cảnh báo
        /// </summary>
        public void ShowWarning(string title, string message)
        {
            AddLog($"{title}: {message}", LogLevel.Warning);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Hiển thị thông tin
        /// </summary>
        public void ShowInfo(string title, string message)
        {
            AddLog($"{title}: {message}", LogLevel.Info);
        }

        private class LogEntry
        {
            public DateTime Timestamp { get; set; }
            public string Message { get; set; } = "";
            public LogLevel Level { get; set; }
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }
}
