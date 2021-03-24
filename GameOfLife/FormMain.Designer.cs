
namespace GameOfLife
{
    partial class FormMain
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
            this.updateField = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // updateField
            // 
            this.updateField.Enabled = true;
            this.updateField.Tick += new System.EventHandler(this.updateField_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FormMain";
            this.Text = "Game of life";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer updateField;
    }
}

