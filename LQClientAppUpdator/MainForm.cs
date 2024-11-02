using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using Serilog;

namespace LQClientAppUpdator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            WaitForClientAppToExit();
            InitializeLogger();
        }

        private const string _clientAppProcessName = @"龍騰數位題庫應用程式";
        private const string _currentAppName = @"龍騰數位題庫應用程式更新程式";
        private readonly double _waitToExitSeconds = 30;
        private const string _logFilePath = @"Logs/龍騰數位題庫應用程式更新程式-.log";

        public static string AppFileVersion
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion!;
            }
        }

        private void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    _logFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} 程式版本：{AppFileVersion} [{Level}] {Message:lj} {NewLine}{Exception}"
                    )
                .Enrich.WithProperty("AppFileVersion", AppFileVersion)
                .CreateLogger();
        }

        private void WaitForClientAppToExit()
        {
            var processes = Process.GetProcessesByName(_clientAppProcessName);
            var result = false;
            if (processes.Any())
            {
                var process = processes.First();
                MessageBox.Show(process!.MainModule!.FileName);
                result = process.WaitForExit(TimeSpan.FromSeconds(_waitToExitSeconds));
            }
            else
                result = true;

            if (result)
                MessageBox.Show("龍騰數位題庫應用程式.exe 已結束。", _currentAppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("龍騰數位題庫應用程式.exe 未結束。", _currentAppName, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length < 4)
            {
                MessageBox.Show("请提供更新檔案路徑、更新目标路径以及執行檔路徑。", _currentAppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }

            var updateFilePath = Environment.GetCommandLineArgs()[1];
            var targetDirectory = Environment.GetCommandLineArgs()[2];
            var clientApp = Environment.GetCommandLineArgs()[3];

            try
            {
                await ExtractZipFileAsync(updateFilePath, targetDirectory);
                MessageBox.Show("更新完成。");

                System.Diagnostics.Process.Start(clientApp);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "更新失败");
                MessageBox.Show("更新失败：" + ex.Message, _currentAppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.Exit();
            }
        }

        private async Task ExtractZipFileAsync(string zipFilePath, string extractPath)
        {
            using var zip = ZipFile.OpenRead(zipFilePath);
            progressBarUpdate.Maximum = zip.Entries.Count;
            progressBarUpdate.Value = 0;

            foreach (var entry in zip.Entries)
            {
                var destinationPath = Path.Combine(extractPath, entry.FullName);

                if (string.IsNullOrEmpty(entry.Name))
                {
                    // 创建目录
                    Directory.CreateDirectory(destinationPath);
                }
                else
                {
                    // 解压文件
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);
                    using var entryStream = entry.Open();
                    using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    await entryStream.CopyToAsync(fileStream);
                }

                progressBarUpdate.Value++;
                lblProgress.Text = $"正在解压: {entry.FullName}";
            }
        }
    }
}
