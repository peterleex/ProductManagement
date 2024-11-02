namespace LQClientAppUpdator
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
            progressBarUpdate = new ProgressBar();
            lblProgress = new Label();
            SuspendLayout();
            // 
            // progressBarUpdate
            // 
            progressBarUpdate.Location = new Point(357, 205);
            progressBarUpdate.Name = "progressBarUpdate";
            progressBarUpdate.Size = new Size(100, 23);
            progressBarUpdate.TabIndex = 0;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(287, 205);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(42, 15);
            lblProgress.TabIndex = 1;
            lblProgress.Text = "label1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblProgress);
            Controls.Add(progressBarUpdate);
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBarUpdate;
        private Label lblProgress;
    }
}
