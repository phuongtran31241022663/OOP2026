using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Shells
{
    /// <summary>
    /// Popup thong bao real-time, khong cuop focus.
    /// Tu dong dong sau khoang thoi gian dinh san.
    /// </summary>
    public partial class FrmToast : Form
    {
        private Timer _closeTimer;
        private int _durationMs;

        private FrmToast()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.FromArgb(50, 50, 50);
            ForeColor = Color.White;
            Opacity = 0.95;
            Size = new Size(360, 64);
            Padding = new Padding(16);
        }

        public static void Show(IWin32Window owner, string message, int durationMs = 3000)
        {
            Form ownerForm = owner as Form;
            FrmToast toast = new FrmToast();
            toast._durationMs = durationMs;
            toast.lblMessage.Text = message;

            if (ownerForm != null)
            {
                Rectangle workingArea = Screen.FromControl(ownerForm).WorkingArea;
                toast.Location = new Point(
                    workingArea.Right - toast.Width - 24,
                    workingArea.Bottom - toast.Height - 24);
            }
            else
            {
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                toast.Location = new Point(
                    workingArea.Right - toast.Width - 24,
                    workingArea.Bottom - toast.Height - 24);
            }

            toast.Show(owner);
            toast.StartCloseTimer();
        }

        private void StartCloseTimer()
        {
            _closeTimer = new Timer();
            _closeTimer.Interval = _durationMs;
            _closeTimer.Tick += (s, e) =>
            {
                _closeTimer.Stop();
                this.Close();
                this.Dispose();
            };
            _closeTimer.Start();
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= 0x8000000; // WS_EX_NOACTIVATE
                return baseParams;
            }
        }
    }
}

