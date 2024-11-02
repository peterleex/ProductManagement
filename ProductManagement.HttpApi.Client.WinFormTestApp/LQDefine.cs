namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public class LQDefine
    {
        public enum LQCode
        {
            C0000,
            C0001,
            C0002,
            C0003,
            C0004,
            C0005,
            C0006,
            C0007,
        }

        public static readonly Dictionary<LQCode, string> LQMessages = new()
                {
                    {LQCode.C0000, "無法連線確認最新軟體版本，請與管理員聯繫" },
                    {LQCode.C0001, "下載更新程式失败!" },
                    {LQCode.C0002, "請更新後再使用\n以確保題目符合最新規範" },
                    {LQCode.C0003, "有新版本" },
                    {LQCode.C0004, "更新失敗，請重試。如仍有異常，請連繫管理員。" },
                    {LQCode.C0005, "正在檢查更新，請稍候..." },
                    {LQCode.C0006, "解壓檔案失敗" },
                    {LQCode.C0007, "下載檔案失敗" },
                };

        public static string LQMessage(LQCode code)
        {
            return LQMessages[code];
        }

        public enum CanEnterSystemResult
        {
            Yes,
            No,
        }

        public enum ClientCheckResult
        {
            NoUpdate,
            UpdateAvailable,
            CheckUpdateError,
        }

        public enum ClientDownloadResult
        {
            Error,
            Successful
        }

        public const int DownloadBufferSize = 8192;

        public const string ExtractPath = "Extracted";

        public const string UpdateExeName = "龍騰數位題庫應用程式更新程式.exe";   
    }
}
