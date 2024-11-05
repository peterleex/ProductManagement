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
            txtAccount = new TextBox();
            txtPassword = new TextBox();
            PbShowPassword = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PbLQLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbShowPassword).BeginInit();
            SuspendLayout();
            // 
            // PbLQLogo
            // 
            PbLQLogo.Image = Properties.Resources.Home;
            PbLQLogo.Location = new Point(47, 30);
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
            label1.Location = new Point(205, 62);
            label1.Name = "label1";
            label1.Size = new Size(109, 30);
            label1.TabIndex = 1;
            label1.Text = "歡迎使用";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            label2.Location = new Point(205, 102);
            label2.Name = "label2";
            label2.Size = new Size(265, 30);
            label2.TabIndex = 2;
            label2.Text = "龍騰數位題庫應用程式!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(205, 165);
            label3.Name = "label3";
            label3.Size = new Size(115, 15);
            label3.TabIndex = 3;
            label3.Text = "請輸入您的帳號密碼";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(195, 214);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 4;
            label4.Text = "帳號";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(195, 254);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 5;
            label5.Text = "密碼";
            // 
            // BtnLogin
            // 
            BtnLogin.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            BtnLogin.Location = new Point(195, 298);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(275, 43);
            BtnLogin.TabIndex = 2;
            BtnLogin.Text = "登入";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // txtAccount
            // 
            txtAccount.ImeMode = ImeMode.Off;
            txtAccount.Location = new Point(252, 211);
            txtAccount.Name = "txtAccount";
            txtAccount.PlaceholderText = "帳號";
            txtAccount.Size = new Size(218, 23);
            txtAccount.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.ImeMode = ImeMode.Off;
            txtPassword.Location = new Point(252, 251);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "密碼";
            txtPassword.Size = new Size(218, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // PbShowPassword
            // 
            PbShowPassword.Image = Properties.Resources.OpenEye;
            PbShowPassword.Location = new Point(476, 251);
            PbShowPassword.Name = "PbShowPassword";
            PbShowPassword.Size = new Size(28, 23);
            PbShowPassword.SizeMode = PictureBoxSizeMode.StretchImage;
            PbShowPassword.TabIndex = 9;
            PbShowPassword.TabStop = false;
            PbShowPassword.Click += PbShowPassword_Click;
            // 
            // Login
            // 
            AcceptButton = BtnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 385);
            Controls.Add(PbShowPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtAccount);
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
        private TextBox txtAccount;
        private TextBox txtPassword;
        private PictureBox PbShowPassword;
    }
}