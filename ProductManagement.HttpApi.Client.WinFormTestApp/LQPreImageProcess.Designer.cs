namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQPreImageProcess
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
            btnOpenFile = new Button();
            PbImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbImage).BeginInit();
            SuspendLayout();
            // 
            // btnOpenFile
            // 
            btnOpenFile.BackColor = Color.FromArgb(167, 108, 86);
            btnOpenFile.FlatAppearance.BorderSize = 0;
            btnOpenFile.FlatStyle = FlatStyle.Flat;
            btnOpenFile.Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnOpenFile.ForeColor = SystemColors.ButtonHighlight;
            btnOpenFile.Location = new Point(191, 266);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(404, 44);
            btnOpenFile.TabIndex = 21;
            btnOpenFile.Text = "選擇檔案";
            btnOpenFile.UseVisualStyleBackColor = false;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // PbImage
            // 
            PbImage.Location = new Point(241, 66);
            PbImage.Name = "PbImage";
            PbImage.Size = new Size(300, 168);
            PbImage.TabIndex = 22;
            PbImage.TabStop = false;
            // 
            // LQPreImageProcess
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroup;
            ClientSize = new Size(800, 450);
            Controls.Add(PbImage);
            Controls.Add(btnOpenFile);
            Name = "LQPreImageProcess";
            Text = "ImageProcess";
            ((System.ComponentModel.ISupportInitialize)PbImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenFile;
        private PictureBox PbImage;
    }
}