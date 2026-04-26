using Domain.ValueObjects;
using Domain.Entities.Users;
using Domain.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Screens.DriverScreen
{
    partial class TripNavigationForm : BaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "TripNavigationForm";
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Driver Navigation";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            // Create main layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Top bar
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Stats strip
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Content area
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150)); // Log area

            // Top bar
            InitializeTopBar();
            mainLayout.Controls.Add(_topBar, 0, 0);

            // Stats strip
            InitializeStatsStrip();
            mainLayout.Controls.Add(_statsStrip, 0, 1);

            // Content area
            InitializeContentArea();
            mainLayout.Controls.Add(_contentArea, 0, 2);

            // Log area
            InitializeLogArea();
            mainLayout.Controls.Add(_logListBox, 0, 3);

            this.Controls.Add(mainLayout);
        }

        private void InitializeTopBar()
        {
            _topBar = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightBlue };

            _titleLabel = new Label
            {
                Text = "Äiá»u phá»‘i chuyáº¿n",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            _refreshButton = new Button
            {
                Text = "LÃ m má»›i",
                Location = new Point(this.Width - 120, 10),
                Size = new Size(100, 35),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _refreshButton.Click += OnRefreshClicked;

            _topBar.Controls.AddRange(new Control[] { _titleLabel, _refreshButton });
        }

        private void InitializeStatsStrip()
        {
            _statsStrip = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightGray };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            _ReviewLabel = new Label { Text = "Review: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _totalTripsLabel = new Label { Text = "Trips: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _incomeLabel = new Label { Text = "Income: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _walletLabel = new Label { Text = "Wallet: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            _revenueTodayLabel = new Label { Text = "Today: --", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };

            layout.Controls.Add(_ReviewLabel, 0, 0);
            layout.Controls.Add(_totalTripsLabel, 1, 0);
            layout.Controls.Add(_incomeLabel, 2, 0);
            layout.Controls.Add(_walletLabel, 3, 0);
            layout.Controls.Add(_revenueTodayLabel, 4, 0);

            _statsStrip.Controls.Add(layout);
        }

        private void InitializeContentArea()
        {
            _contentArea = new Panel { Dock = DockStyle.Fill };

            // Empty panel
            InitializeEmptyPanel();

            // Request panel
            InitializeRequestPanel();

            // Active trip panel
            InitializeActiveTripPanel();

            _contentArea.Controls.AddRange(new Control[] { _emptyPanel, _requestPanel, _activeTripPanel });
        }

        private void InitializeEmptyPanel()
        {
            _emptyPanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            _emptyMessageLabel = new Label
            {
                Text = "KhÃ´ng cÃ³ chuyáº¿n nÃ o",
                Font = new Font("Arial", 14, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            _emptyPanel.Controls.Add(_emptyMessageLabel);
        }

        private void InitializeRequestPanel()
        {
            _requestPanel = new Panel { Dock = DockStyle.Fill, Visible = false, BackColor = Color.LightYellow };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            _requestInfoLabel = new Label
            {
                Text = "Trip request info",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(_requestInfoLabel, 0, 0);

            var buttonPanel = new Panel { Dock = DockStyle.Fill };
            _acceptButton = new Button
            {
                Text = "Cháº¥p nháº­n",
                Size = new Size(100, 35),
                Location = new Point(50, 5),
                BackColor = Color.LightGreen
            };
            _acceptButton.Click += OnAcceptClicked;

            _rejectButton = new Button
            {
                Text = "Tá»« chá»‘i",
                Size = new Size(100, 35),
                Location = new Point(160, 5),
                BackColor = Color.LightCoral
            };
            _rejectButton.Click += OnRejectClicked;

            buttonPanel.Controls.AddRange(new Control[] { _acceptButton, _rejectButton });
            layout.Controls.Add(buttonPanel, 0, 1);

            _requestPanel.Controls.Add(layout);
        }

        private void InitializeActiveTripPanel()
        {
            _activeTripPanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Route info
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Step bar
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Action button
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Spacer

            _routeInfoLabel = new Label
            {
                Text = "Route info",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(_routeInfoLabel, 0, 0);

            InitializeStepBar();
            layout.Controls.Add(_stepBar, 0, 1);

            _actionButton = new Button
            {
                Text = "Action",
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = Color.LightBlue
            };
            _actionButton.Click += OnActionClicked;
            layout.Controls.Add(_actionButton, 0, 2);

            _activeTripPanel.Controls.Add(layout);
        }

        private void InitializeStepBar()
        {
            _stepBar = new Panel { Dock = DockStyle.Fill };

            _stepDots = new PictureBox[4];
            _stepLabels = new Label[4];

            string[] stepTexts = { "ÄÃ£ ghÃ©p Ä‘Ã´i", "ÄÃ£ Ä‘áº¿n Ä‘iá»ƒm Ä‘Ã³n", "Báº¯t Ä‘áº§u chuyáº¿n", "HoÃ n thÃ nh" };

            for (int i = 0; i < 4; i++)
            {
                _stepDots[i] = new PictureBox
                {
                    Size = new Size(20, 20),
                    Location = new Point(50 + i * 100, 10),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                _stepBar.Controls.Add(_stepDots[i]);

                _stepLabels[i] = new Label
                {
                    Text = stepTexts[i],
                    Location = new Point(30 + i * 100, 35),
                    AutoSize = true,
                    Font = new Font("Arial", 8)
                };
                _stepBar.Controls.Add(_stepLabels[i]);
            }
        }

        private void InitializeLogArea()
        {
            _logListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                ScrollAlwaysVisible = true
            };
        }
// UI components
        private Panel _topBar;
        private Label _titleLabel;
        private Button _refreshButton;

        private Panel _statsStrip;
        private Label _ReviewLabel;
        private Label _totalTripsLabel;
        private Label _incomeLabel;
        private Label _walletLabel;
        private Label _revenueTodayLabel;

        private Panel _contentArea;
        private Panel _emptyPanel;
        private Label _emptyMessageLabel;

        private Panel _requestPanel;
        private Label _requestInfoLabel;
        private Button _acceptButton;
        private Button _rejectButton;

        private Panel _activeTripPanel;
        private Label _routeInfoLabel;
        private Panel _stepBar;
        private PictureBox[] _stepDots;
        private Label[] _stepLabels;
        private Button _actionButton;
        private ListBox _logListBox;
        #endregion
    }
}
