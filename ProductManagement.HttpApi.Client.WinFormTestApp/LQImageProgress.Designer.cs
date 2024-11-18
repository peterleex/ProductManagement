namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQImageProgress
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
            percentageLabel = new Label();
            progressBar = new ProgressBar();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // percentageLabel
            // 
            percentageLabel.AutoSize = true;
            percentageLabel.Location = new Point(321, 44);
            percentageLabel.Name = "percentageLabel";
            percentageLabel.Size = new Size(42, 15);
            percentageLabel.TabIndex = 11;
            percentageLabel.Text = "label1";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(31, 40);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(284, 23);
            progressBar.TabIndex = 10;
            // 
            // lblFileName
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(35, 17);
            lblInfo.Name = "lblFileName";
            lblInfo.Size = new Size(42, 15);
            lblInfo.TabIndex = 12;
            lblInfo.Text = "label1";
            // 
            // LQLoadImage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 93);
            Controls.Add(lblInfo);
            Controls.Add(percentageLabel);
            Controls.Add(progressBar);
            Name = "LQLoadImage";
            Text = "LQLoadImage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label percentageLabel;
        private ProgressBar progressBar;
        private Label lblInfo;
    }
}