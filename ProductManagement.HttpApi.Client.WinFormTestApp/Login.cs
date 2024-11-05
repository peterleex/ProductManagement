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
using System.IdentityModel.Tokens.Jwt;


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
            await _serviceProvider.GetRequiredService<AccessTokenManager>().ObtainAccessToken("admin", "1q2w3E*");

            var token = _serviceProvider.GetRequiredService<AccessTokenManager>().AccessToken;

            var tokenWrapper = new AccessTokenWrapper(token!);

            System.Diagnostics.Debug.WriteLine($"Issuer: {tokenWrapper.Issuer}");
            System.Diagnostics.Debug.WriteLine($"Subject: {tokenWrapper.Subject}");
            System.Diagnostics.Debug.WriteLine($"Audience: {tokenWrapper.Audience}");
            System.Diagnostics.Debug.WriteLine($"Expiration: {tokenWrapper.Expiration}");
            System.Diagnostics.Debug.WriteLine($"Not Before: {tokenWrapper.NotBefore}");
            System.Diagnostics.Debug.WriteLine($"Issued At: {tokenWrapper.IssuedAt}");
            System.Diagnostics.Debug.WriteLine($"JWT ID: {tokenWrapper.JwtId}");
            System.Diagnostics.Debug.WriteLine($"Preferred Username: {tokenWrapper.PreferredUsername}");
            System.Diagnostics.Debug.WriteLine($"Given Name: {tokenWrapper.GivenName}");
        }

        private void PbShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }
    }
}
