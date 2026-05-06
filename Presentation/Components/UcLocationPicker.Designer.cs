using System.Drawing;
using System.Windows.Forms;

namespace Presentation.Components
{
    partial class UcLocationPicker
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Create TextBox for search input
            _txtSearch = new TextBox();
            _txtSearch.Location = new Point(0, 0);
            _txtSearch.Size = new Size(334, 28);
            _txtSearch.Font = Presentation.Constants.UiConstants.Typography.Small;
            _txtSearch.ForeColor = Presentation.Constants.UiConstants.Colors.TextPrimary;
            _txtSearch.KeyDown += TxtSearch_KeyDown;
            _txtSearch.Enter += TxtSearch_Enter;
            _txtSearch.Leave += TxtSearch_Leave;
            _txtSearch.TextChanged += TxtSearch_TextChanged;

            // Create ListBox for suggestions
            _lstSuggestions = new ListBox();
            _lstSuggestions.Dock = DockStyle.Fill;
            _lstSuggestions.BorderStyle = BorderStyle.None;
            _lstSuggestions.Font = Presentation.Constants.UiConstants.Cards.Fonts.Body;
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
            _pnlSuggestions.Location = new Point(0, 34); // Ngay dưới textbox
            _pnlSuggestions.Name = "_pnlSuggestions";
            _pnlSuggestions.Size = new Size(334, 200);
            _pnlSuggestions.Visible = false;
            _pnlSuggestions.Controls.Add(_lstSuggestions);
            _pnlSuggestions.AutoSize = false;

            // Add to main control (panel phải được thêm sau để nằm trên cùng)
            this.Controls.Add(_txtSearch);
            this.Controls.Add(_pnlSuggestions);

            _pnlSuggestions.ResumeLayout();
            this.ResumeLayout();

        }
    }
}
