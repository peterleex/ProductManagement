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
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;
using Serilog;


namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class Login : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProfileAppService _profileAppService;
        private readonly AccessTokenManager _accessTokenManager;

        public Login(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _accessTokenManager = _serviceProvider.GetRequiredService<AccessTokenManager>();

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
            ShowInTaskbar = false;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            //帳號或密碼錯誤
            if (txtAccount.Text.IsNullOrWhiteSpace())
            {
                LQHelper.InfoMessage(LQDefine.LQMessage(LQDefine.LQCode.C0025));
                txtAccount.Focus();
                return;
            }
            if (txtPassword.Text.IsNullOrWhiteSpace())
            {
                LQHelper.InfoMessage(LQDefine.LQMessage(LQDefine.LQCode.C0026));
                txtPassword.Focus();
                return;
            }

            SetControlsEnabled(false);
            LQWaiting.Instance.CenterTo(this);
            LQWaiting.Instance.ShowMessage(LQMessage(LQCode.C0029));

            await LoginAsync();

            LQWaiting.Instance.Release();
            SetControlsEnabled(true);

        }

        private async Task LoginAsync()
        {
            try
            {
                await _accessTokenManager.ObtainAccessToken(txtAccount.Text, txtPassword.Text);
                if (_accessTokenManager.AccessToken.IsNullOrEmpty())
                {
                    LQHelper.InfoMessage(LQMessage(LQCode.C0027));
                }
            }
            catch (Exception ex)
            {
                var info = LQMessage(LQCode.C0028);
                Log.Error(ex, info);
                LQHelper.InfoMessage(info);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                control.Enabled = enabled;
            }
        }

        private void PbShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }
    }
}
