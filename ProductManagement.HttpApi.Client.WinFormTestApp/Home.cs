﻿using Autofac;
using Microsoft.Extensions.DependencyInjection;
using NUglify;
using ProductManagement.HttpApi.Client.WinFormTestApp.Authentication;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using System.Linq.Dynamic.Core;
using System.Windows.Forms;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class Home : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AccessTokenManager _accessTokenManager;
        private AccessTokenParser? _accessTokenParser = null;
        private ToolStripMenuItem? LoginOutMenuItem = null;

        public Home(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _accessTokenManager = _serviceProvider.GetRequiredService<AccessTokenManager>();
            _accessTokenManager.AccessTokenObtained += _accessTokenManager_AccessTokenObtained;
            _accessTokenManager.Logouted += _accessTokenManager_Logouted;

            InitializeComponent();
            HookEvent();
            InitForm();
            InitMenu();
            EnterSelectFunction();
        }

        private void HookEvent()
        {
            FormClosing += Home_FormClosing;
        }

        private void Home_FormClosing(object? sender, FormClosingEventArgs e)
        {
            var result = LQHelper.ConfirmMessage("您確定要離開本系統？");
            if (result == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void _accessTokenManager_Logouted(object? sender, EventArgs e)
        {
            CreateLoginOutMenuItem();
        }

        private void _accessTokenManager_AccessTokenObtained(object? sender, EventArgs e)
        {
            CreateLoginOutMenuItem();
        }

        private void EnterSelectFunction()
        {
            var selectFuntion = new SelectFunction
            {
                MdiParent = this
            };
            selectFuntion.Show();
        }

        private MenuStrip menuStrip = null!;

        private void InitForm()
        {
            IsMdiContainer = true;
            Hide();
            ShowIcon = false;
        }

        private static void CheckScreenResolution()
        {
            var currentScreenWidth = Screen.PrimaryScreen!.Bounds.Width;
            var currentScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (LQDefine.PreferScreenWidth != currentScreenWidth | LQDefine.PreferScreenHeight != currentScreenHeight)
            {
                string info = "請調整螢幕解析度為 1920 * 1080，以確保最佳畫面呈現。";
                LQHelper.InfoMessage(info);
            }
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

            // 建立登入/登出選單
            CreateLoginOutMenuItem();

            // 将選單添加到窗体
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void HomeMenuItem_Click(object? sender, EventArgs e)
        {
        }
        private void ImageProcessMenuItem_Click(object? sender, EventArgs e)
        {
            var imageProcess = new ImageProcess(_serviceProvider)
            {
                MdiParent = this
            };
            imageProcess.Show();
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

        //private void LoginedHeaderMenuItem_Click(object? sender, EventArgs e)
        //{
        //    if (_accessTokenManager.AccessToken == null)
        //    {
        //        new Login(_serviceProvider).ShowDialog(this);
        //    }
        //    else
        //    {
        //        // 呈現 Logout 選單
        //        string logout = LQDefine.LQMessage(LQDefine.LQCode.C0024);

        //        var logoutMenuItem = new ToolStripMenuItem(logout);
        //        logoutMenuItem.Click += async (s, args) =>
        //        {
        //            await _accessTokenManager.Logout();
        //            LoginOutMenuItem!.DropDownItems.Clear();
        //        };

        //        if (!LoginOutMenuItem!.DropDownItems.OfType<ToolStripMenuItem>().Any(item => item.Text == logout))
        //            LoginOutMenuItem.DropDownItems.Add(logoutMenuItem);
        //    }
        //}

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
