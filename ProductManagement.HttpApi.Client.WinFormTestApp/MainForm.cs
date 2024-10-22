using Microsoft.AspNetCore.Identity;
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

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class MainForm : Form
    {
        private readonly IProfileAppService _profileAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IAccountAppService _accountAppService;

        public MainForm(IServiceProvider serviceProvider)
        {
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _identityUserAppService = serviceProvider.GetRequiredService<IIdentityUserAppService>();
            _accountAppService = serviceProvider.GetRequiredService<IAccountAppService>();

            InitializeComponent();
        }
        private async void btnGetProfile_Click(object sender, EventArgs e)
        {
            var profile = await _profileAppService.GetAsync();
            MessageBox.Show($"UserName: {profile.UserName}\nEmail: {profile.Email}");
        }

        private async void btnGetUsers_Click(object sender, EventArgs e)
        {
            var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
            MessageBox.Show($"Total users: {users.TotalCount}");
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var loginResult = await _accountAppService.LoginAsync(new UserLoginInfo
            {
                UserNameOrEmailAddress = "admin",
                Password = "1q2w3E*"
            });
            MessageBox.Show($"Login successful! Token: {loginResult.AccessToken}");
        }

    }
}
