﻿using Microsoft.Extensions.DependencyInjection;
using ProductManagement.HttpApi.Client.WinFormTestApp.Authentication;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;


namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQHome : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccessTokenManager _accessTokenManager;
        private AccessTokenParser? _accessTokenParser = null;
        private ToolStripMenuItem? LoginOutMenuItem = null;

        public LQHome(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _accessTokenManager = _serviceProvider.GetRequiredService<AccessTokenManager>();
            _accessTokenManager.AccessTokenObtained += _accessTokenManager_AccessTokenObtained;
            _accessTokenManager.Logouted += AccessTokenManager_Logouted;

            InitializeComponent();
            HookEvent();
            InitForm();
            InitMenu();
            InitWork();
#if DEBUG
            EnterQuestionCheck();
#else
            EnterSelectFunction();
#endif
        }

        private void InitWork()
        {
            LQHelper.CreateWorkSpace();
        }

        private void HookEvent()
        {
            FormClosing += Home_FormClosing;
        }

        private void Home_FormClosing(object? sender, FormClosingEventArgs e)
        {
#if DEBUG
            e.Cancel = false;
#else
            var result = LQHelper.ConfirmMessage(LQDefine.LQMessage(LQDefine.LQCode.C0039), LQDefine.LQMessage(LQDefine.LQCode.C0038));
            if (result == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
#endif
        }

        private void AccessTokenManager_Logouted(object? sender, EventArgs e)
        {
            CreateLoginOutMenuItem();
        }

        private void _accessTokenManager_AccessTokenObtained(object? sender, EventArgs e)
        {
            CreateLoginOutMenuItem();
        }

        private void EnterSelectFunction()
        {
            var selectFuntion = new LQSelectFunction
            {
                MdiParent = this
            };
            selectFuntion.Show();
        }

        private MenuStrip menuStrip = null!;

        private void InitForm()
        {
            Size = LQDefine.DefaultWindowSize;
            IsMdiContainer = true;
            Hide();
            ShowIcon = false;
        }

        private static void CheckScreenResolution()
        {
#if !DEBUG
            var currentScreenWidth = Screen.PrimaryScreen!.Bounds.Width;
            var currentScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (LQDefine.PreferScreenWidth != currentScreenWidth | LQDefine.PreferScreenHeight != currentScreenHeight)
            {
                string info = "請調整螢幕解析度為 1920 * 1080，以確保最佳畫面呈現。";
                LQHelper.InfoMessage(info);
            }
#endif
        }


        private void InitMenu()
        {
            menuStrip = new MenuStrip();
            ToolStripMenuItem HomeMenuItem = new(LQDefine.LQMessage(LQDefine.LQCode.C0032));
            ToolStripMenuItem ImageProcessMenuItem = new(LQDefine.LQMessage(LQDefine.LQCode.C0033));
            ToolStripMenuItem QuestionCheckMenuItem = new(LQDefine.LQMessage(LQDefine.LQCode.C0034));
            ToolStripMenuItem QuestionImportMenuItem = new(LQDefine.LQMessage(LQDefine.LQCode.C0035));

            HomeMenuItem.Image = Resources.Home;
            ImageProcessMenuItem.Image = Resources.ImageProcessIcon1;
            QuestionCheckMenuItem.Image = Resources.QuestionCheckIcon1;
            QuestionImportMenuItem.Image = Resources.QuestionImportIcon1;

            menuStrip.Items.Add(HomeMenuItem);
            menuStrip.Items.Add(ImageProcessMenuItem);
            menuStrip.Items.Add(QuestionCheckMenuItem);
            menuStrip.Items.Add(QuestionImportMenuItem);

            HomeMenuItem.Click += HomeMenuItem_Click;

            ImageProcessMenuItem.Click += ImageProcessMenuItem_Click;
            QuestionCheckMenuItem.Click += QuestionCheckMenuItem_Click;
            // 建立登入/登出選單
            CreateLoginOutMenuItem();

            // 将選單添加到窗体
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void QuestionCheckMenuItem_Click(object? sender, EventArgs e)
        {
            EnterQuestionCheck();
        }

        public void EnterQuestionCheck()
        {
            var questionCheck = new LQQuestionCheck(_serviceProvider)
            {
                MdiParent = this
            };
            questionCheck.Show();
        }

        private void HomeMenuItem_Click(object? sender, EventArgs e)
        {
        }
        private void ImageProcessMenuItem_Click(object? sender, EventArgs e)
        {
            EnterSelectImage();
        }

        public void EnterSelectImage()
        {
            var selectImage = new LQSelectImage(_serviceProvider)
            {
                MdiParent = this
            };
            selectImage.Show();
        }

        private void CreateLoginOutMenuItem()
        {
            if (LoginOutMenuItem != null)
                menuStrip.Items.Remove(LoginOutMenuItem);

            if (_accessTokenManager.AccessToken == null)
            {
                LoginOutMenuItem = new(LQDefine.LQMessage(LQDefine.LQCode.C0023));
                LoginOutMenuItem.Click += (s, args) =>
                {
                    new Login(_serviceProvider).ShowDialog(this);
                };
            }
            else
            {
                _accessTokenParser = new AccessTokenParser(_accessTokenManager.AccessToken);

                var loginInfo = $"您好，{_accessTokenParser.PreferredUsername}/{_accessTokenParser.GivenName}";
                LoginOutMenuItem = new(loginInfo)
                {
                    Image = Resources.LoginedHeader1
                };
                string logout = LQDefine.LQMessage(LQDefine.LQCode.C0024);

                var logoutMenuItem = new ToolStripMenuItem(logout);
                logoutMenuItem.Click += LogoutMenuItem_Click;
                LoginOutMenuItem.DropDownItems.Add(logoutMenuItem);
            }

            menuStrip.Items.Add(LoginOutMenuItem);
        }

        private async void LogoutMenuItem_Click(object? sender, EventArgs e)
        {
            if (LQHelper.ConfirmMessage("您確定要登出？") == DialogResult.Cancel)
                return;

            await _accessTokenManager.Logout();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            var lqUpdateForm = new LQUpdate(_serviceProvider);
            var result = lqUpdateForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                WindowState = FormWindowState.Maximized;
                CheckScreenResolution();
                Show();
            }
            else
            {
                FormClosing -= Home_FormClosing;
                Close();
            }
        }
    }
}
