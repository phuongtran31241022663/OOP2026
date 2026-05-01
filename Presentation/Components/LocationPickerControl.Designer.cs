using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    partial class LocationPickerControl
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

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Create TextBox for search input
            _txtSearch.KeyDown += TxtSearch_KeyDown;
            _txtSearch.Enter += TxtSearch_Enter;
            _txtSearch.Leave += TxtSearch_Leave;

            // Create ListBox for suggestions
            _lstSuggestions = new ListBox();
            _lstSuggestions.Dock = DockStyle.Fill;
            _lstSuggestions.Font = new Font("Segoe UI", 9F);
            _lstSuggestions.ItemHeight = 28;
            _lstSuggestions.Click += LstSuggestions_Click;
            _lstSuggestions.DoubleClick += LstSuggestions_DoubleClick;
            _lstSuggestions.MouseEnter += LstSuggestions_MouseEnter;

            // Create the suggestions popup panel (hidden by default)
            _pnlSuggestions = new Panel();
            _pnlSuggestions.SuspendLayout();
            _pnlSuggestions.BackColor = Color.White;
            _pnlSuggestions.BorderStyle = BorderStyle.FixedSingle;
            _pnlSuggestions.Dock = DockStyle.None;
            _pnlSuggestions.Location = new Point(0, this.Height);
            _pnlSuggestions.Name = "_pnlSuggestions";
            _pnlSuggestions.Size = new Size(334, 200);
            _pnlSuggestions.Visible = false;
            _pnlSuggestions.Controls.Add(_lstSuggestions);

            // Create placeholder label
            _lblPlaceholder = new Label();
            _lblPlaceholder.Dock = DockStyle.Fill;
            _lblPlaceholder.Font = new Font("Segoe UI", 10F);
            _lblPlaceholder.ForeColor = Color.Gray;
            _lblPlaceholder.Text = "Chọn điểm đón...";
            _lblPlaceholder.TextAlign = ContentAlignment.MiddleLeft;
            _lblPlaceholder.Padding = new Padding(5, 0, 0, 0);
            _lblPlaceholder.Click += LblPlaceholder_Click;
            _lblPlaceholder.Visible = true;

            // Add to main control
            this.Controls.Add(_txtSearch);
            this.Controls.Add(_lblPlaceholder);

            _pnlSuggestions.ResumeLayout();
            this.ResumeLayout();

            // Show placeholder if no selection
            UpdatePlaceholder();
        }

    }
}
