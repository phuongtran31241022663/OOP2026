namespace Presentation
{
    partial class BaseUserControl
    {
        private System.ComponentModel.IContainer components = null;
        // bỏ error provider
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        // một baseusercontrol mà chả làm được con mẹ gì, đọc uc, usercontrol coi có cái gì lặp lại, chung mà có thể khai báo chung thì viết vào đây rồi dùng, trời ơi
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }
        #endregion
    }
}
