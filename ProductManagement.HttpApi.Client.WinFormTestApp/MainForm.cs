using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Account.Web.Areas.Account.Controllers.Models;
using Serilog;
using ProductManagement.ClientApplication;
using System.Diagnostics;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;
using System.Security.Policy;
using System.IO.Compression;


namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class MainForm : Form
    {
        private readonly IProfileAppService _profileAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly ClientDemoService _demo;
        private readonly IClientApplicationAppService _clientApplicationAppService;

        public MainForm(IServiceProvider serviceProvider)
        {
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _identityUserAppService = serviceProvider.GetRequiredService<IIdentityUserAppService>();
            _demo = serviceProvider.GetRequiredService<ClientDemoService>();
            _clientApplicationAppService = serviceProvider.GetRequiredService<IClientApplicationAppService>();

            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            WindowState = FormWindowState.Maximized;
        }
        private async void BtnGetProfile_Click(object sender, EventArgs e)
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

        private async void BtnGetUsers_Click(object sender, EventArgs e)
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

        private async void BtnLogin_Click(object sender, EventArgs e)
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

        private async Task<bool> IsUpdateAvailable()
        {
            try
            {
                var clientApp = await _clientApplicationAppService.GetAsync();
                var result = CurrentAppInfo.CompareVersion(clientApp.ClientAppVersion);
                return result < 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, LQMessage(LQCode.C0000));
                throw;
            }
        }
        private async Task<string> GetClientAppDownloadUrl()
        {
            var clientAppUrl = await _clientApplicationAppService.GetDownloadClientAppUrlAsync();
            return clientAppUrl;
        }

        private const string _updatorFilePath = @"D:\SOURCE\LQSystem\Code\Training\ABP\ProductManagement\LQClientAppUpdator\bin\Debug\net8.0-windows\龍騰數位題庫應用程式更新程式.exe";
        private void BtnRunUpdator_Click(object sender, EventArgs e)
        {
            Process.Start(_updatorFilePath);
            Application.Exit();
        }

        private async void BtnDownloadClientApp_Click(object sender, EventArgs e)
        {

            try
            {
                SetControlsEnabled(false);

                LQWaiting.Instance.CenterTo(this);

                LQWaiting.Instance.ShowMessage(LQMessage(LQCode.C0005));

                var result = await ClientUpdateCheck();

                if (result == CanEnterSystemResult.No)
                {
                    LQHelper.ErrorMessage("you can't enter system.");
                    Application.Exit();
                }

                LQHelper.InfoMessage("you can enter the system.");
            }
            catch (Exception)
            {
            }
            finally
            {
                LQWaiting.Instance.Release();
                SetControlsEnabled(true);
            }
        }

        private async Task<CanEnterSystemResult> ClientUpdateCheck()
        {
            var result = await CheckUpdate();
            if (result == ClientCheckResult.CheckUpdateError)
            {
                LQHelper.InfoMessage(LQMessage(LQCode.C0000));
            }
            else if (result == ClientCheckResult.UpdateAvailable)
            {
                if (DialogResult.Cancel == LQHelper.ConfirmMessage(LQMessage(LQCode.C0002), LQMessage(LQCode.C0003)))
                    return CanEnterSystemResult.No;

                while (ClientDownloadResult.Error == await DownloadClientApp())
                {
                    if (DialogResult.Cancel == LQHelper.ConfirmMessage(LQMessage(LQCode.C0004), LQMessage(LQCode.C0003)))
                        return CanEnterSystemResult.No;
                }
            }
            return CanEnterSystemResult.Yes;
        }

        private async Task<ClientCheckResult> CheckUpdate()
        {
            try
            {
                var isUpdateAvailable = await IsUpdateAvailable();

                if (!isUpdateAvailable)
                    return ClientCheckResult.NoUpdate;
                else
                    return ClientCheckResult.UpdateAvailable;
            }
            catch (Exception)
            {
                return ClientCheckResult.CheckUpdateError;
            }
        }

        private async Task<ClientDownloadResult> DownloadClientApp()
        {
            try
            {
                var url = await GetClientAppDownloadUrl();
                return await DownloadAndUpdateAsync(url);
            }
            catch (Exception ex)
            {
                Log.Error(ex, LQMessage(LQCode.C0007));
                return ClientDownloadResult.Error;
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                control.Enabled = enabled;
            }
        }

        private async Task<ClientDownloadResult> DownloadAndUpdateAsync(string url)
        {
            var downloadFilePath = LQHelper.GetDownloadFilePath(url);
            await DownloadFileAsync(url, downloadFilePath);
            var extractPath = UnzipDownloadFile(downloadFilePath);
            UpdateFiles(extractPath);
            return ClientDownloadResult.Successful;
        }

        private static void UpdateFiles(string extractPath)
        {
            try
            {
                var files = Directory.GetFiles(extractPath, UpdateExeName, SearchOption.AllDirectories);

                if (files.Length > 0)
                {
                    var updateExePath = files[0];
                    Process.Start(updateExePath);
                    Application.Exit();
                }
                else
                {
                    Log.Error("Update executable not found in the extracted files.");
                    MessageBox.Show("Update executable not found.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating files.");
                MessageBox.Show("Error while updating files: " + ex.Message);
            }
        }

        private string UnzipDownloadFile(string downloadFilePath)
        {
            try
            {
                string extractPath = LQHelper.GetExtractPath(downloadFilePath);

                using ZipArchive archive = ZipFile.OpenRead(downloadFilePath);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.Combine(extractPath, entry.FullName);
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    else
                    {
                        entry.ExtractToFile(destinationPath);
                    }
                }

                return extractPath;
            }
            catch (Exception ex)
            {
                Log.Error(ex, LQMessage(LQCode.C0006));
                throw;
            }
        }

        private async Task DownloadFileAsync(string url, string downloadFilePath)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? 0L;
            var canReportProgress = totalBytes != 0;

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(downloadFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            var buffer = new byte[DownloadBufferSize];
            long totalRead = 0;
            int bytesRead;

            while ((bytesRead = await contentStream.ReadAsync(buffer)) != 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                totalRead += bytesRead;

                if (canReportProgress)
                {
                    var progress = (int)((totalRead * 100) / totalBytes);
                    progressBarDownload.Value = progress;
                }
            }
        }

    }
}
