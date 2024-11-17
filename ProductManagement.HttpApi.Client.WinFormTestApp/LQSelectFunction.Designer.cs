namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQSelectFunction
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
            PbQuestionImportProgram = new PictureBox();
            PbQuestionCheckProgram = new PictureBox();
            PbImageProcessProgram = new PictureBox();
            PbPleaseSelectProgram = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbQuestionImportProgram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionCheckProgram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbImageProcessProgram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbPleaseSelectProgram).BeginInit();
            SuspendLayout();
            // 
            // PbQuestionImportProgram
            // 
            PbQuestionImportProgram.Image = Properties.Resources.QuestionImportProgram;
            PbQuestionImportProgram.Location = new Point(649, 160);
            PbQuestionImportProgram.Name = "PbQuestionImportProgram";
            PbQuestionImportProgram.Size = new Size(285, 262);
            PbQuestionImportProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbQuestionImportProgram.TabIndex = 6;
            PbQuestionImportProgram.TabStop = false;
            // 
            // PbQuestionCheckProgram
            // 
            PbQuestionCheckProgram.Image = Properties.Resources.QuestionCheckProgram;
            PbQuestionCheckProgram.Location = new Point(358, 160);
            PbQuestionCheckProgram.Name = "PbQuestionCheckProgram";
            PbQuestionCheckProgram.Size = new Size(285, 262);
            PbQuestionCheckProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbQuestionCheckProgram.TabIndex = 5;
            PbQuestionCheckProgram.TabStop = false;
            // 
            // PbImageProcessProgram
            // 
            PbImageProcessProgram.Image = Properties.Resources.ImageProcessProgram;
            PbImageProcessProgram.Location = new Point(67, 160);
            PbImageProcessProgram.Name = "PbImageProcessProgram";
            PbImageProcessProgram.Size = new Size(285, 262);
            PbImageProcessProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbImageProcessProgram.TabIndex = 4;
            PbImageProcessProgram.TabStop = false;
            // 
            // PbPleaseSelectProgram
            // 
            PbPleaseSelectProgram.BackColor = Color.Transparent;
            PbPleaseSelectProgram.Image = Properties.Resources.PleaseSelectProgram;
            PbPleaseSelectProgram.Location = new Point(67, 103);
            PbPleaseSelectProgram.Name = "PbPleaseSelectProgram";
            PbPleaseSelectProgram.Size = new Size(285, 51);
            PbPleaseSelectProgram.SizeMode = PictureBoxSizeMode.StretchImage;
            PbPleaseSelectProgram.TabIndex = 9;
            PbPleaseSelectProgram.TabStop = false;
            // 
            // LQSelectFunction
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.backgroup;
            ClientSize = new Size(1016, 578);
            Controls.Add(PbPleaseSelectProgram);
            Controls.Add(PbQuestionImportProgram);
            Controls.Add(PbQuestionCheckProgram);
            Controls.Add(PbImageProcessProgram);
            Name = "LQSelectFunction";
            Text = "SelectFunction";
            Resize += SelectFunction_Resize;
            ((System.ComponentModel.ISupportInitialize)PbQuestionImportProgram).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbQuestionCheckProgram).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbImageProcessProgram).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbPleaseSelectProgram).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PbQuestionImportProgram;
        private PictureBox PbQuestionCheckProgram;
        private PictureBox PbImageProcessProgram;
        private PictureBox PbPleaseSelectProgram;
        private Button BtnTest;
    }
}