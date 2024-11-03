﻿namespace ProductManagement.HttpApi.Client.WinFormTestApp
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
            C0008,
            C0009,
            C0010,
            C0011,
            C0012,
            C0013,
            C0014,
            C0015,
            C0016,
            C0017,
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
                    {LQCode.C0008, "正在解壓：" },
                    {LQCode.C0009, "正在下載：" },
                    {LQCode.C0010, "移除舊檔：" },
                    {LQCode.C0011, "複製新檔：" },
                    {LQCode.C0012, "更新檔案失敗" },
                    {LQCode.C0013, $"更新檔案失敗\n請確認離開「{MainExeFileProcessName}」後，點「確定」！" },
                    {LQCode.C0014, "更新包中，缺少檔案「{0}」，請與管理員聯繫" },
                    {LQCode.C0015, "準備更新檔案失敗，請與管理員聯繫" },
                    {LQCode.C0016, "有錯誤" },
                    {LQCode.C0017, "解壓縮檔案失敗，請重試。如仍有異常，請連繫管理員。" },
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

        public enum UpdateClientResult
        {
            Error,
            Successful
        }

        public const int DownloadBufferSize = 8192;

        public const string ExtractPath = "Extracted";

        public const string MainExeFileName = "龍騰數位題庫應用程式.exe";
        public static string MainExeFileProcessName => Path.GetFileNameWithoutExtension(MainExeFileName);

        public const string UpdatorExeFileName = "龍騰數位題庫應用程式更新程式.exe";   

        public static string UpdatorExeFileProcessName => Path.GetFileNameWithoutExtension(UpdatorExeFileName);

    }
}
