namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class LQQuestionCheck
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
            lblSuccessInfo = new Label();
            btnOpenFile = new Button();
            plSearch = new Panel();
            pbMagnify = new PictureBox();
            txtSearch = new TextBox();
            btnAdd7Field = new Button();
            btnOpen7Field = new Button();
            plSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbMagnify).BeginInit();
            SuspendLayout();
            // 
            // lblSuccessInfo
            // 
            lblSuccessInfo.AutoSize = true;
            lblSuccessInfo.BackColor = Color.Transparent;
            lblSuccessInfo.Font = new Font("Microsoft JhengHei UI", 16F);
            lblSuccessInfo.ForeColor = Color.FromArgb(167, 108, 86);
            lblSuccessInfo.Location = new Point(95, 45);
            lblSuccessInfo.Name = "lblSuccessInfo";
            lblSuccessInfo.Size = new Size(75, 28);
            lblSuccessInfo.TabIndex = 24;
            lblSuccessInfo.Text = "label1";
            // 
            // btnOpenFile
            // 
            btnOpenFile.BackColor = Color.FromArgb(167, 108, 86);
            btnOpenFile.FlatAppearance.BorderSize = 0;
            btnOpenFile.FlatStyle = FlatStyle.Flat;
            btnOpenFile.Font = new Font("Microsoft JhengHei UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btnOpenFile.ForeColor = SystemColors.ButtonHighlight;
            btnOpenFile.Location = new Point(207, 33);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(230, 51);
            btnOpenFile.TabIndex = 25;
            btnOpenFile.Text = "選擇檔案";
            btnOpenFile.UseVisualStyleBackColor = false;
            // 
            // plSearch
            // 
            plSearch.Controls.Add(pbMagnify);
            plSearch.Controls.Add(txtSearch);
            plSearch.Location = new Point(95, 108);
            plSearch.Name = "plSearch";
            plSearch.Size = new Size(374, 40);
            plSearch.TabIndex = 26;
            // 
            // pbMagnify
            // 
            pbMagnify.Image = Properties.Resources.Magify;
            pbMagnify.Location = new Point(344, 9);
            pbMagnify.Name = "pbMagnify";
            pbMagnify.Size = new Size(20, 20);
            pbMagnify.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMagnify.TabIndex = 1;
            pbMagnify.TabStop = false;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(3, 7);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(335, 23);
            txtSearch.TabIndex = 0;
            // 
            // btnAdd7Field
            // 
            btnAdd7Field.Location = new Point(98, 201);
            btnAdd7Field.Name = "btnAdd7Field";
            btnAdd7Field.Size = new Size(108, 30);
            btnAdd7Field.TabIndex = 27;
            btnAdd7Field.Text = "新增範本";
            btnAdd7Field.UseVisualStyleBackColor = true;
            // 
            // btnOpen7Field
            // 
            btnOpen7Field.Location = new Point(242, 201);
            btnOpen7Field.Name = "btnOpen7Field";
            btnOpen7Field.Size = new Size(108, 30);
            btnOpen7Field.TabIndex = 28;
            btnOpen7Field.Text = "開啓七欄位";
            btnOpen7Field.UseVisualStyleBackColor = true;
            // 
            // LQQuestionCheck
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1093, 631);
            Controls.Add(btnOpen7Field);
            Controls.Add(btnAdd7Field);
            Controls.Add(plSearch);
            Controls.Add(btnOpenFile);
            Controls.Add(lblSuccessInfo);
            Name = "LQQuestionCheck";
            Text = "LQQuestionCheck";
            plSearch.ResumeLayout(false);
            plSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbMagnify).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSuccessInfo;
        private Button btnOpenFile;
        private Panel plSearch;
        private PictureBox pbMagnify;
        private TextBox txtSearch;
        private Button btnAdd7Field;
        private Button btnOpen7Field;
    }
}