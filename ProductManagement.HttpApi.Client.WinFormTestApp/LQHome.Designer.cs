namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQHome
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
            PbImageProcessProgram = new PictureBox();
            PbQuestionCheckProgram = new PictureBox();
            PbQuestionImportProgram = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbImageProcessProgram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionCheckProgram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionImportProgram).BeginInit();
            SuspendLayout();
            // 
            // PbImageProcessProgram
            // 
            PbImageProcessProgram.Image = Properties.Resources.ImageProcessProgram;
            PbImageProcessProgram.Location = new Point(15, 142);
            PbImageProcessProgram.Name = "PbImageProcessProgram";
            PbImageProcessProgram.Size = new Size(285, 262);
            PbImageProcessProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbImageProcessProgram.TabIndex = 1;
            PbImageProcessProgram.TabStop = false;
            PbImageProcessProgram.Visible = false;
            // 
            // PbQuestionCheckProgram
            // 
            PbQuestionCheckProgram.Image = Properties.Resources.QuestionCheckProgram;
            PbQuestionCheckProgram.Location = new Point(306, 142);
            PbQuestionCheckProgram.Name = "PbQuestionCheckProgram";
            PbQuestionCheckProgram.Size = new Size(285, 262);
            PbQuestionCheckProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbQuestionCheckProgram.TabIndex = 2;
            PbQuestionCheckProgram.TabStop = false;
            PbQuestionCheckProgram.Visible = false;
            // 
            // PbQuestionImportProgram
            // 
            PbQuestionImportProgram.Image = Properties.Resources.QuestionImportProgram;
            PbQuestionImportProgram.Location = new Point(597, 142);
            PbQuestionImportProgram.Name = "PbQuestionImportProgram";
            PbQuestionImportProgram.Size = new Size(285, 262);
            PbQuestionImportProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbQuestionImportProgram.TabIndex = 3;
            PbQuestionImportProgram.TabStop = false;
            PbQuestionImportProgram.Visible = false;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroup;
            ClientSize = new Size(896, 500);
            Controls.Add(PbQuestionImportProgram);
            Controls.Add(PbQuestionCheckProgram);
            Controls.Add(PbImageProcessProgram);
            Name = "Home";
            Text = "Home";
            Load += Home_Load;
            ((System.ComponentModel.ISupportInitialize)PbImageProcessProgram).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionCheckProgram).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionImportProgram).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PbImageProcessProgram;
        private PictureBox PbQuestionCheckProgram;
        private PictureBox PbQuestionImportProgram;
    }
}