using System.Windows.Forms;

namespace textbox_with_time_validation
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.maskedTextBox = new textbox_with_time_validation.MaskedTextBoxEx();
            this.buttonUnfocus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // maskedTextBox
            // 
            this.maskedTextBox.Location = new System.Drawing.Point(226, 48);
            this.maskedTextBox.Name = "maskedTextBox";
            this.maskedTextBox.Size = new System.Drawing.Size(75, 31);
            this.maskedTextBox.TabIndex = 1;
            this.maskedTextBox.Text = "00:00";
            this.maskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonUnfocus
            // 
            this.buttonUnfocus.Location = new System.Drawing.Point(33, 48);
            this.buttonUnfocus.Name = "buttonUnfocus";
            this.buttonUnfocus.Size = new System.Drawing.Size(112, 34);
            this.buttonUnfocus.TabIndex = 2;
            this.buttonUnfocus.Text = "Unfocus";
            this.buttonUnfocus.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 244);
            this.Controls.Add(this.buttonUnfocus);
            this.Controls.Add(this.maskedTextBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaskedTextBoxEx maskedTextBox;
        private Button buttonUnfocus;
    }
}