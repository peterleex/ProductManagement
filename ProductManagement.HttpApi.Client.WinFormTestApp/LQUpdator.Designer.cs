namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQUpdator
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
            lblProgress = new Label();
            progressBarUpdate = new ProgressBar();
            SuspendLayout();
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(315, 214);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(42, 15);
            lblProgress.TabIndex = 3;
            lblProgress.Text = "label1";
            // 
            // progressBarUpdate
            // 
            progressBarUpdate.Location = new Point(385, 214);
            progressBarUpdate.Name = "progressBarUpdate";
            progressBarUpdate.Size = new Size(100, 23);
            progressBarUpdate.TabIndex = 2;
            // 
            // LQUpdator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblProgress);
            Controls.Add(progressBarUpdate);
            Name = "LQUpdator";
            Text = "LQUpdator";
            Load += LQUpdator_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProgress;
        private ProgressBar progressBarUpdate;
    }
}