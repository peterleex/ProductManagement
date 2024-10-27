﻿//using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static Volo.Abp.Identity.IdentityPermissions;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Account.Web.Areas.Account.Controllers.Models;
using Serilog.Core;
using Serilog;
using ProductManagement.ClientApplication;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics;
//using Microsoft.Extensions.Logging;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class MainForm : Form
    {
        private readonly IProfileAppService _profileAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly ClientDemoService _demo;
        private readonly IClientApplicationAppService _clientApplicationAppService;


        public MainForm(IServiceProvider serviceProvider)
        {
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _identityUserAppService = serviceProvider.GetRequiredService<IIdentityUserAppService>();
            _accountAppService = serviceProvider.GetRequiredService<IAccountAppService>();
            _demo = serviceProvider.GetRequiredService<ClientDemoService>();
            _clientApplicationAppService = serviceProvider.GetRequiredService<IClientApplicationAppService>();

            InitializeComponent();
        }
        private async void btnGetProfile_Click(object sender, EventArgs e)
        {
            try
            {
                var profile = await _profileAppService.GetAsync();
                MessageBox.Show($"UserName: {profile.UserName}\nEmail: {profile.Email}");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "讀取使用者資料失敗");

                MessageBox.Show(ex.Message);
            }
        }

        private async void btnGetUsers_Click(object sender, EventArgs e)
        {
            try
            {
                var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
                MessageBox.Show($"Total users: {users.TotalCount}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var userLoginInfo = new UserLoginInfo()
                {
                    UserNameOrEmailAddress = "admin",
                    Password = "1q2w3E*",
                    RememberMe = true
                };

                var loginResult = await _demo.LoginAsync(userLoginInfo);

                MessageBox.Show(loginResult == null ? "loginResult is null" : loginResult.Description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            Log.Information("Hello World");
        }

        private async void btnClientApplication_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(await IsUpdateAvailable() ? $"Server Is Newer" : "Client is newest");

            }
            catch (Exception ex)
            {
                var errorStr = "與 Server 比較小程式版本失敗！";
                MessageBox.Show(errorStr);
                Log.Error(ex, errorStr);
            }
        }

        private async Task<bool> IsUpdateAvailable()
        {
            var clientApp = await _clientApplicationAppService.GetAsync();
            var result = CurrentAppInfo.CompareVersion(clientApp.ClientAppVersion);
            return result < 0;
        }

        private const string _updatorFilePath = @"D:\SOURCE\LQSystem\Code\Training\ABP\ProductManagement\LQClientAppUpdator\bin\Debug\net8.0-windows\龍騰數位題庫應用程式更新程式.exe";
        private void btnRunUpdator_Click(object sender, EventArgs e)
        {
            Process.Start(_updatorFilePath);
            Application.Exit();
        }
    }
}
