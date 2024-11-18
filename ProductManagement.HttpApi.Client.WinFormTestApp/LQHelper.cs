using Serilog;
using System.Text.Json;
using System.Windows.Forms;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public class LQHelper
    {
        public static void WriteCustomSetting(string key, string customPath)
        {
            var settings = new Dictionary<string, string>();

            if (File.Exists(LQDefine.SettingFilePath))
            {
                var json = File.ReadAllText(LQDefine.SettingFilePath);
                settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }

            settings![key] = customPath;

            var updatedJson = JsonSerializer.Serialize(settings);
            File.WriteAllText(LQDefine.SettingFilePath, updatedJson);
        }

        public static string ReadCustomSetting(string key)
        {
            if (!new FileInfo(LQDefine.SettingFilePath).Exists)
                return string.Empty;

            var json = File.ReadAllText(LQDefine.SettingFilePath);
            var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (settings != null && settings.TryGetValue(key, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public static void InfoMessage(string message, string title = "")
        {
            MessageBox.Show(
               message, title,
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        public static void ErrorMessage(string message, string title = "")
        {
            MessageBox.Show(
               message, title,
               MessageBoxButtons.OK,
               MessageBoxIcon.Error);
        }

        public static DialogResult ConfirmMessage(string message, string title = "")
        {
            return MessageBox.Show(
                message, title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        public static void LogAndShowError(Exception ex, string message, string title = "錯誤")
        {
            Log.Error(ex, message);
            ErrorMessage($"{message}\n{ex.Message}", title);
        }

        public static string GetTempPath()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            return tempPath;
        }

        public static string GetDownloadFilePath(string url)
        {
            var fileName = Path.GetFileName(url);
            var tempPath = GetTempPath();
            var downloadFilePath = Path.Combine(tempPath, fileName);

            Log.Information($"下載URL：{url}，下載檔案路徑：{downloadFilePath}");

            return downloadFilePath;
        }

        public static string GetExtractPath(string downloadFilePath)
        {
            var extractPath = Path.Combine(Path.GetDirectoryName(downloadFilePath)!, LQDefine.ExtractPath);
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }
            Directory.CreateDirectory(extractPath);

            Log.Information($"解壓縮檔案路徑：{extractPath}");

            return extractPath;
        }

        internal static void LogAndShowErrorWithoutExceptionMessage(Exception ex, string message, string title = "錯誤")
        {
            Log.Error(ex, message);
            ErrorMessage($"{message}", title);
        }

        public static string CurrentProcessDirectory => Path.GetDirectoryName(Environment.ProcessPath)!;
    }
}
