using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volo.Abp.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using System.Net.Http.Headers;
using ProductManagement.HttpApi.Client.WinFormTestApp.Authentication;


namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class Login : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProfileAppService _profileAppService;
        private readonly HttpClient _httpClient;
        public Login(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _httpClient = serviceProvider.GetRequiredService<HttpClient>();

            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0023);
            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            //var profileDto = await _profileAppService.GetAsync();

            //var accessTokenManager =  _serviceProvider.GetRequiredService<AccessTokenManager>();
            //await accessTokenManager.ObtainAccessToken("admin", "1q2w3E*");
            //var token = accessTokenManager.AccessToken;
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _serviceProvider.GetRequiredService<AccessTokenManager>().ObtainAccessToken("admin", "1q2w3E*");

            //var  profileDto = await _profileAppService.GetAsync();

            var token = _serviceProvider.GetRequiredService<AccessTokenManager>().AccessToken;
        }

        private void PbShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }
    }
}
