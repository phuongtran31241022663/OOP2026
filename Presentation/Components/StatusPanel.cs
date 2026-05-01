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
    public partial class StatusPanel : BaseUserControl
    {
        private readonly ConcurrentQueue<LogEntry> _logEntries = new ConcurrentQueue<LogEntry>();
        private int _errorCount = 0;
        private int _warningCount = 0;

        public StatusPanel()
        {
            InitializeComponent();
            _clearButton.Click += ClearLogs;
            _viewLogButton.Click += OpenLogFile;
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
                textColor = Color.Orange;
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
                _errorCountLabel.ForeColor = Color.Orange;
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
            while (_logEntries.TryDequeue(out _)) { }
            _errorCount = 0;
            _warningCount = 0;
            if (_logTextBox.InvokeRequired)
                _logTextBox.Invoke(new Action(_logTextBox.Clear));
            else
                _logTextBox.Clear();
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
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Khong the mo tep nhat ky luc nay.\nChi tiet: " + ex.Message, "Loi thao tac", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Duong dan tep nhat ky khong dung dinh dang.\nChi tiet: " + ex.Message, "Loi dinh dang", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the mo tep nhat ky.\nChi tiet: " + ex.Message, "Loi he thong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ghi log và hiển thị thông báo lỗi với chi tiết đầy đủ
        /// </summary>
        public void LogAndShowError(string title, string message, Exception ex = null)
        {
            AddLog($"{title}: {message}", LogLevel.Error);
            base.ShowError(message, title);
        }

        /// <summary>
        /// Ghi log và hiển thị cảnh báo
        /// </summary>
        public void LogAndShowWarning(string title, string message)
        {
            AddLog($"{title}: {message}", LogLevel.Warning);
            base.ShowWarning(message, title);
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
}

