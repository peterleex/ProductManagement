using Microsoft.Extensions.DependencyInjection;
using ProductManagement.ClientApplication;
using Serilog;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Areas.Account.Controllers.Models;
using Volo.Abp.Identity;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;


namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQUpdate : Form
    {
        private readonly IProfileAppService _profileAppService;
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly ClientDemoService _demo;
        private readonly IClientApplicationAppService _clientApplicationAppService;

        public LQUpdate(IServiceProvider serviceProvider)
        {
            _profileAppService = serviceProvider.GetRequiredService<IProfileAppService>();
            _identityUserAppService = serviceProvider.GetRequiredService<IIdentityUserAppService>();
            _demo = serviceProvider.GetRequiredService<ClientDemoService>();
            _clientApplicationAppService = serviceProvider.GetRequiredService<IClientApplicationAppService>();

            InitializeComponent();
            InitForm();
            InitializeCustomComponents();
        }

        private void InitForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            Text = LQMessage(LQCode.C0019);
        }

        private void InitializeCustomComponents()
        {
            progressBar.ForeColor = ProgressColor;
            percentageLabel.Text = string.Empty;
            percentageLabel.ForeColor = ProgressColor;
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

                Text = LQMessage(LQCode.C0018);
                LQWaiting.Instance.Release();
                SetControlsVisible(true);

                var downloadResult = await DownloadClientApp();
                while (!downloadResult.IsSuccessful)
                {
                    if (DialogResult.Cancel == LQHelper.ConfirmMessage(LQMessage(LQCode.C0004), LQMessage(LQCode.C0003)))
                        return CanEnterSystemResult.No;
                    downloadResult = await DownloadClientApp();
                }

                var unzipResult = await UnzipDownloadFile(downloadResult.DownloaFilePath!);
                while (!unzipResult.IsSuccessful)
                {
                    if (DialogResult.Cancel == LQHelper.ConfirmMessage(LQMessage(LQCode.C0017), LQMessage(LQCode.C0016)))
                        return CanEnterSystemResult.No;
                    unzipResult = await UnzipDownloadFile(downloadResult.DownloaFilePath!);
                }

                BeginToUpdate(unzipResult.ExtractPath);
            }
            return CanEnterSystemResult.Yes;
        }

        class ClientDownload
        {
            public string DownloaFilePath { get; set; } = string.Empty;
            public bool IsSuccessful { get; set; }
        }

        class UnzipResult
        {
            public string ExtractPath { get; set; } = string.Empty;
            public bool IsSuccessful { get; set; }
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

        private async Task<ClientDownload> DownloadClientApp()
        {
            try
            {
                var url = await GetClientAppDownloadUrl();
                return await DownloadFile(url);
            }
            catch (Exception ex)
            {
                Log.Error(ex, LQMessage(LQCode.C0007));
                return new ClientDownload { IsSuccessful = false };
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control control in Controls)
            {
                control.Enabled = enabled;
            }
        }

        private async Task<ClientDownload> DownloadFile(string url)
        {
            var downloadFilePath = LQHelper.GetDownloadFilePath(url);
            await DownloadFileAsync(url, downloadFilePath);
            return new ClientDownload
            {
                IsSuccessful = true,
                DownloaFilePath = downloadFilePath
            };
        }

        private static void BeginToUpdate(string extractPath)
        {
            try
            {
                var updatorExePath = FindUpdatorFile(extractPath);
                if (updatorExePath != null)
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = updatorExePath,
                        WorkingDirectory = Path.GetDirectoryName(updatorExePath)!,
                        ArgumentList = {
                            Path.GetDirectoryName(Environment.ProcessPath)!
                        },
                    };
                    Process.Start(startInfo);
                    Application.Exit();
                }
                else
                {
                    var msg = string.Format(LQMessage(LQCode.C0014), UpdatorExeFileName);
                    Log.Error(msg);
                    LQHelper.ErrorMessage(msg);
                }
            }
            catch (Exception ex)
            {
                LQHelper.LogAndShowError(ex, LQMessage(LQCode.C0015));
            }
        }

        private static string? FindUpdatorFile(string extractPath)
        {
            var files = Directory.GetFiles(extractPath, UpdatorExeFileName, SearchOption.AllDirectories);
            return files.Length > 0 ? files[0] : null;
        }

        private async Task<UnzipResult> UnzipDownloadFile(string downloadFilePath)
        {
            try
            {
                string extractPath = LQHelper.GetExtractPath(downloadFilePath);
                int count = 0;
                // 處理檔名中含中文字會出現亂碼的問題
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using FileStream fs = File.OpenRead(downloadFilePath);
                using ZipArchive archive = new(fs, ZipArchiveMode.Read, false, Encoding.GetEncoding(950));

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.Combine(extractPath, entry.FullName);
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        await Task.Run(() => Directory.CreateDirectory(destinationPath));
                    }
                    else
                    {
                        await Task.Run(() => entry.ExtractToFile(destinationPath));
                    }
                    var progress = (int)((++count * 100) / (float)archive.Entries.Count);

                    progressBar.Value = progress;
                    UpdateProgressLabel(LQMessage(LQCode.C0008));
                }

                return new UnzipResult
                {
                    IsSuccessful = true,
                    ExtractPath = extractPath,
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, LQMessage(LQCode.C0006));
                return new UnzipResult
                {
                    IsSuccessful = false,
                };
            }
        }

        private void UpdateProgressLabel(string info)
        {
            percentageLabel.Text = $"{info}{progressBar.Value * 100 / progressBar.Maximum}%";
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
                    progressBar.Value = progress;
                    UpdateProgressLabel(LQMessage(LQCode.C0009));
                }
            }
        }

        private async void LQUpdate_Load(object sender, EventArgs e)
        {
            await StartUpdate();
        }

        private async Task StartUpdate()
        {
            try
            {
                SetControlsVisible(false);

                LQWaiting.Instance.CenterTo(this);

                LQWaiting.Instance.ShowMessage(LQMessage(LQCode.C0005));

                var result = await ClientUpdateCheck();

                if (result == CanEnterSystemResult.No)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                //LQWaiting.Instance.Release();
                //SetControlsVisible(true);
            }
        }

        private void SetControlsVisible(bool visible)
        {
            foreach (Control control in Controls)
            {
                control.Visible = visible;
            }
        }
    }
}
