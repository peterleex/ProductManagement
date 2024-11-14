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
            PbChooseImageFile = new PictureBox();
            plChooseImageFile = new Panel();
            pbPleaseSelectImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbChooseImageFile).BeginInit();
            plChooseImageFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPleaseSelectImage).BeginInit();
            SuspendLayout();
            // 
            // btnOpenFile
            // 
            btnOpenFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnOpenFile.BackColor = Color.FromArgb(167, 108, 86);
            btnOpenFile.FlatAppearance.BorderSize = 0;
            btnOpenFile.FlatStyle = FlatStyle.Flat;
            btnOpenFile.Font = new Font("Microsoft JhengHei UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btnOpenFile.ForeColor = SystemColors.ButtonHighlight;
            btnOpenFile.Location = new Point(341, 423);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(276, 60);
            btnOpenFile.TabIndex = 21;
            btnOpenFile.Text = "選擇檔案";
            btnOpenFile.UseVisualStyleBackColor = false;
            btnOpenFile.Click += BtnOpenFile_Click;
            // 
            // PbChooseImageFile
            // 
            PbChooseImageFile.Dock = DockStyle.Fill;
            PbChooseImageFile.Image = Properties.Resources.ChooseImageFile2;
            PbChooseImageFile.Location = new Point(0, 0);
            PbChooseImageFile.Name = "PbChooseImageFile";
            PbChooseImageFile.Size = new Size(958, 540);
            PbChooseImageFile.SizeMode = PictureBoxSizeMode.Zoom;
            PbChooseImageFile.TabIndex = 22;
            PbChooseImageFile.TabStop = false;
            // 
            // plChooseImageFile
            // 
            plChooseImageFile.BackColor = Color.White;
            plChooseImageFile.Controls.Add(btnOpenFile);
            plChooseImageFile.Controls.Add(PbChooseImageFile);
            plChooseImageFile.Location = new Point(380, 158);
            plChooseImageFile.Name = "plChooseImageFile";
            plChooseImageFile.Size = new Size(958, 540);
            plChooseImageFile.TabIndex = 23;
            // 
            // pbPleaseSelectImage
            // 
            pbPleaseSelectImage.BackColor = Color.Transparent;
            pbPleaseSelectImage.Image = Properties.Resources.lblPleaseSelectImage;
            pbPleaseSelectImage.Location = new Point(385, 93);
            pbPleaseSelectImage.Name = "pbPleaseSelectImage";
            pbPleaseSelectImage.Size = new Size(277, 44);
            pbPleaseSelectImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbPleaseSelectImage.TabIndex = 24;
            pbPleaseSelectImage.TabStop = false;
            // 
            // LQPreImageProcess
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroup;
            ClientSize = new Size(1602, 799);
            Controls.Add(pbPleaseSelectImage);
            Controls.Add(plChooseImageFile);
            Name = "LQPreImageProcess";
            Text = "ImageProcess";
            ((System.ComponentModel.ISupportInitialize)PbChooseImageFile).EndInit();
            plChooseImageFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPleaseSelectImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenFile;
        private PictureBox PbChooseImageFile;
        private Panel plChooseImageFile;
        private PictureBox pbPleaseSelectImage;
    }
}