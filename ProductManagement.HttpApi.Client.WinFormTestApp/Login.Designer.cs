namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class Login
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
            PbLQLogo = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            BtnLogin = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            PbShowPassword = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbLQLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbShowPassword).BeginInit();
            SuspendLayout();
            // 
            // PbLQLogo
            // 
            PbLQLogo.Image = Properties.Resources.Home;
            PbLQLogo.Location = new Point(47, 37);
            PbLQLogo.Name = "PbLQLogo";
            PbLQLogo.Size = new Size(71, 67);
            PbLQLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            PbLQLogo.TabIndex = 0;
            PbLQLogo.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label1.Location = new Point(205, 89);
            label1.Name = "label1";
            label1.Size = new Size(109, 30);
            label1.TabIndex = 1;
            label1.Text = "歡迎使用";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label2.Location = new Point(205, 129);
            label2.Name = "label2";
            label2.Size = new Size(265, 30);
            label2.TabIndex = 2;
            label2.Text = "龍騰數位題庫應用程式!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(205, 192);
            label3.Name = "label3";
            label3.Size = new Size(115, 15);
            label3.TabIndex = 3;
            label3.Text = "請輸入您的帳號密碼";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(195, 255);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 4;
            label4.Text = "帳號";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(195, 295);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 5;
            label5.Text = "密碼";
            // 
            // BtnLogin
            // 
            BtnLogin.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            BtnLogin.Location = new Point(195, 339);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(275, 43);
            BtnLogin.TabIndex = 2;
            BtnLogin.Text = "登入";
            BtnLogin.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(252, 252);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(218, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(252, 292);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(218, 23);
            textBox2.TabIndex = 1;
            // 
            // PbShowPassword
            // 
            PbShowPassword.Image = Properties.Resources.OpenEye;
            PbShowPassword.Location = new Point(476, 292);
            PbShowPassword.Name = "PbShowPassword";
            PbShowPassword.Size = new Size(28, 23);
            PbShowPassword.SizeMode = PictureBoxSizeMode.StretchImage;
            PbShowPassword.TabIndex = 9;
            PbShowPassword.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 431);
            Controls.Add(PbShowPassword);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(BtnLogin);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PbLQLogo);
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)PbLQLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbShowPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox PbLQLogo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button BtnLogin;
        private TextBox textBox1;
        private TextBox textBox2;
        private PictureBox PbShowPassword;
    }
}