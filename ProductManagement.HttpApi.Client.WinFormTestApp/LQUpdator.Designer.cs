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
            percentageLabel = new Label();
            progressBar = new ProgressBar();
            label1 = new Label();
            SuspendLayout();
            // 
            // percentageLabel
            // 
            percentageLabel.AutoSize = true;
            percentageLabel.Location = new Point(338, 80);
            percentageLabel.Name = "percentageLabel";
            percentageLabel.Size = new Size(42, 15);
            percentageLabel.TabIndex = 9;
            percentageLabel.Text = "label1";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(45, 76);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(284, 23);
            progressBar.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            label1.Location = new Point(45, 36);
            label1.Name = "label1";
            label1.Size = new Size(165, 20);
            label1.TabIndex = 10;
            label1.Text = "版本更新中，請稍候...";
            // 
            // LQUpdator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 137);
            Controls.Add(label1);
            Controls.Add(percentageLabel);
            Controls.Add(progressBar);
            Name = "LQUpdator";
            Text = "LQUpdator";
            Load += LQUpdator_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label percentageLabel;
        private ProgressBar progressBar;
        private Label label1;
    }
}