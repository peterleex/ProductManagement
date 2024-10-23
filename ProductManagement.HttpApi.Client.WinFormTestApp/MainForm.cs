//using Microsoft.AspNetCore.Identity;
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
//using Microsoft.Extensions.Logging;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class MainForm : Form
    {
        private readonly IProfileAppService _profileAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly ClientDemoService _demo;

        public MainForm(IServiceProvider serviceProvider)
        {
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _identityUserAppService = serviceProvider.GetRequiredService<IIdentityUserAppService>();
            _accountAppService = serviceProvider.GetRequiredService<IAccountAppService>();
            _demo = serviceProvider.GetRequiredService<ClientDemoService>();

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

                MessageBox.Show(loginResult == null? "loginResult is null" : loginResult.Description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
