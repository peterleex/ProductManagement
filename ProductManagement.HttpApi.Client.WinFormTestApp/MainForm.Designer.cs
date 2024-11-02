namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    partial class MainForm
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
            btnGetProfile = new Button();
            btnGetUsers = new Button();
            btnLogin = new Button();
            btnRunUpdator = new Button();
            btnDownloadClientApp = new Button();
            progressBarDownload = new ProgressBar();
            SuspendLayout();
            // 
            // btnGetProfile
            // 
            btnGetProfile.Location = new Point(141, 26);
            btnGetProfile.Name = "btnGetProfile";
            btnGetProfile.Size = new Size(154, 43);
            btnGetProfile.TabIndex = 0;
            btnGetProfile.Text = "GetProfile";
            btnGetProfile.UseVisualStyleBackColor = true;
            btnGetProfile.Click += BtnGetProfile_Click;
            // 
            // btnGetUsers
            // 
            btnGetUsers.Location = new Point(298, 26);
            btnGetUsers.Name = "btnGetUsers";
            btnGetUsers.Size = new Size(154, 43);
            btnGetUsers.TabIndex = 1;
            btnGetUsers.Text = "GetUsers";
            btnGetUsers.UseVisualStyleBackColor = true;
            btnGetUsers.Click += BtnGetUsers_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(455, 26);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(154, 43);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnRunUpdator
            // 
            btnRunUpdator.Location = new Point(461, 112);
            btnRunUpdator.Name = "btnRunUpdator";
            btnRunUpdator.Size = new Size(154, 43);
            btnRunUpdator.TabIndex = 4;
            btnRunUpdator.Text = "執行更新程式";
            btnRunUpdator.UseVisualStyleBackColor = true;
            btnRunUpdator.Click += BtnRunUpdator_Click;
            // 
            // btnDownloadClientApp
            // 
            btnDownloadClientApp.Location = new Point(141, 189);
            btnDownloadClientApp.Name = "btnDownloadClientApp";
            btnDownloadClientApp.Size = new Size(154, 43);
            btnDownloadClientApp.TabIndex = 5;
            btnDownloadClientApp.Text = "下載更新程式";
            btnDownloadClientApp.UseVisualStyleBackColor = true;
            btnDownloadClientApp.Click += BtnDownloadClientApp_Click;
            // 
            // progressBarDownload
            // 
            progressBarDownload.Location = new Point(141, 238);
            progressBarDownload.Name = "progressBarDownload";
            progressBarDownload.Size = new Size(100, 23);
            progressBarDownload.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBarDownload);
            Controls.Add(btnDownloadClientApp);
            Controls.Add(btnRunUpdator);
            Controls.Add(btnLogin);
            Controls.Add(btnGetUsers);
            Controls.Add(btnGetProfile);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnGetProfile;
        private Button btnGetUsers;
        private Button btnLogin;
        private Button btnRunUpdator;
        private Button btnDownloadClientApp;
        private ProgressBar progressBarDownload;
    }
}