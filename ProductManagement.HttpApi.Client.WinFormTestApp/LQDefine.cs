using DocumentFormat.OpenXml.Wordprocessing;
using ImageMagick;
using System.Runtime.CompilerServices;

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
            C0018,
            C0019,
            C0020,
            C0021,
            C0022,
            C0023,
            C0024,
            C0025,
            C0026,
            C0027,
            C0028,
            C0029,
            C0030,
            C0031,
            C0032,
            C0033,
            C0034,
            C0035,
            C0036,
            C0037,
            C0038,
            C0039,
            C0040,
            C0041,
            C0042,
            C0043,
            C0044,
            C0045,
            C0046,
            C0047,
            C0048,
            C0049,
            C0050,
            C0051,
            C0052,
            C0053,
            C0054,
            C0055,
            C0056,
            C0057,
            C0058,
            C0059,
            C0060,
            C0061,
            C0062,
            C0063,
            C0064,
            C0065,
            C0066,
            C0067,
            C0069,
            C0068,
            C0070,
            C0071,
            C0072,
            C0073,
            C0074,
            C0075,
            C0076,
            C0077,
            C0078,
            C0079,
            C0080,
            C0081,
            C0082,
            C0083,
            C0084,
            C0085,
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
                    {LQCode.C0018, "版本下載中" },
                    {LQCode.C0019, "檢查更新中" },
                    {LQCode.C0020, "版本更新中" },
                    {LQCode.C0021, "龍騰數位題庫應用程式" },
                    {LQCode.C0022, $"龍騰數位題庫應用程式 V{CurrentAppInfo.FileVer} - " },
                    {LQCode.C0023, "登入" },
                    {LQCode.C0024, "登出" },
                    {LQCode.C0025, "請輸入帳號" },
                    {LQCode.C0026, "請輸入密碼" },
                    {LQCode.C0027, "帳號或密碼錯誤" },
                    {LQCode.C0028, "登入時，無法與伺服器連線，請與管理員聯繫" },
                    {LQCode.C0029, "登入..." },
                    {LQCode.C0030, "更新失敗" },
                    {LQCode.C0031, "登入失敗，請與管理員聯繫" },
                    {LQCode.C0032, "首頁" },
                    {LQCode.C0033, "圖片小程式" },
                    {LQCode.C0034, "題目檢查" },
                    {LQCode.C0035, "題目匯入" },
                    {LQCode.C0036, "Q001 小程式首頁" },
                    {LQCode.C0037, "Q002 圖片小程式" },
                    {LQCode.C0038, "離開龍騰數位題庫系統" },
                    {LQCode.C0039, "您確定要離開本系統？" },
                    {LQCode.C0040, "docx 檔案沒有包含 MainDocumentPart\n{0}" },
                    {LQCode.C0041, "有 {0} 張圖正在載入中，請稍候…" },
                    {LQCode.C0042, "載入中" },
                    {LQCode.C0043, "非jpg" },
                    {LQCode.C0044, "未達300dpi" },
                    {LQCode.C0045, "寬大於14" },
                    {LQCode.C0046, "大於1000KB" },
                    {LQCode.C0047, "刷新頁面" },
                    {LQCode.C0048, "新增" },
                    {LQCode.C0049, "全部清空" },
                    {LQCode.C0050, "cm" },
                    {LQCode.C0051, "kb" },
                    {LQCode.C0052, "請輸入" },
                    {LQCode.C0053, "批次處理" },
                    {LQCode.C0054, "重新批次處理" },
                    {LQCode.C0055, "..." },
                    {LQCode.C0056, "有 {0} 張圖儲存中，請稍候…" },
                    {LQCode.C0057, "{0}轉檔後大於1000KB\n" },
                    {LQCode.C0058, "警告" },
                    {LQCode.C0059, "處理成功 {0} 筆，失敗 {1} 筆\n" },
                    {LQCode.C0060, "處理成功 {0} 筆" },
                    {LQCode.C0061, "請選擇輸出位置" },
                    {LQCode.C0062, "輸出位置不存在，請重新選擇" },
                    {LQCode.C0063, "Q003 圖片檢查程式" },
                    {LQCode.C0064, "題目編碼/系統編碼/文件編號" },
                    {LQCode.C0065, "新增範本" },
                    {LQCode.C0066, "開啓七欄位" },
                    {LQCode.C0067, "刪除" },
                    {LQCode.C0068, "從電腦開啓" },
                    {LQCode.C0069, "從雲端母庫載入" },
                    {LQCode.C0070, "題目編碼/系統編碼" },
                    {LQCode.C0071, "新七欄檔案格式錯誤，應該是10欄，目前是{0}欄" },
                    {LQCode.C0072, "新七欄檔案格式錯誤，未找到表格" },
                    {LQCode.C0073, "新七欄檔案格式錯誤，未找到 MainDocumentPart" },
                    {LQCode.C0074, "新七欄檔案格式錯誤，未找到 Body" },
                    {LQCode.C0075, "新七欄檔案格式錯誤，表格中無任何資料" },
                    {LQCode.C0076, "新七欄檔案格式錯誤，檔案無頁尾" },
                    {LQCode.C0077, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 1 欄需包含 0 個或 2 個分號，分號前後不可空白" },
                    {LQCode.C0078, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 2 欄不應包含分號" },
                    {LQCode.C0079, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 3 欄需包含 1 個分號，分號前後不可空白" },
                    {LQCode.C0080, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 4 欄不應包含分號" },
                    {LQCode.C0081, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 5 欄不應包含分號" },
                    {LQCode.C0082, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 9 欄需包含 0 個或 1 個分號，分號前後不可空白" },
                    {LQCode.C0083, "新七欄檔案格式錯誤\n檔案：\n{0}\n第 {1} 列，第 10 欄需包含 0 個或 1 個分號，分號前後不可空白" },
                    {LQCode.C0084, "新七欄檔案格式錯誤\n檔案中不包含表格" },
                    {LQCode.C0085, "新七欄檔案格式錯誤\n檔案只允許包含一個表格" },

                };

        public static System.Drawing.Color PrimaryColor => System.Drawing.Color.FromArgb(167, 108, 86);
        public static System.Drawing.Color SecondaryColor => System.Drawing.Color.FromArgb(237, 226, 221);
        public static System.Drawing.Color WarningColor => System.Drawing.Color.Red;
        public static System.Drawing.Color DefaultColor => System.Drawing.Color.Black;

        public static string Space => " ";

        public static string LQMessage(LQCode code)
        {
            return LQMessages[code];
        }

        public enum CanEnterSystemResult
        {
            Yes,
            No,
        }

        public enum LoginResult
        {
            InvalidUsernameOrPassword,
            Successs,
            Failed,
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

        public const string SupportedFileType = "Image Files|*.gif;*.jpg;*.jpeg;*.png;*.eps;*.ai;*.tif|Documents|*.docx;*.pdf|All Files|*.gif;*.jpg;*.jpeg;*.png;*.eps;*.ai;*.tif;*.docx;*.pdf";

        public static readonly string[] AllSupportedFileType = { ".gif", ".jpg", ".jpeg", ".png", ".eps", ".ai", ".tif", ".docx", ".pdf" };

        public const int DownloadBufferSize = 8192;

        public const string ExtractPath = "Extracted";

        public const string MainExeFileName = "龍騰數位題庫應用程式.exe";
        public static string MainExeFileProcessName => Path.GetFileNameWithoutExtension(MainExeFileName);

        public const string UpdatorExeFileName = "龍騰數位題庫應用程式更新程式.exe";
        public static string UpdatorExeFileProcessName => Path.GetFileNameWithoutExtension(UpdatorExeFileName);

        public static string DefaultOutputFolder => "output";

        public static int PreferScreenWidth => 1920;
        public static int PreferScreenHeight => 1080;
        public static double AllowedMaxWidthInCm => 14f;

        public static double AllowedMaxFileSizeInKb => 1000f;

        public static double DefaultDpi => 300;

        public static double WidthDivider => 7.5;
        public static double HeightDivider => 3.5;

        public static int IconWidth => 32;
        public static int IconHeight => 32;

        public static int MagnifyIndex => 2;

        public static System.Drawing.Size DefaultWindowSize => new(1920, 1080);

        public static System.Drawing.Font BigBoldFont => new("Microsoft JhengHei UI", 16, FontStyle.Bold);
        public static System.Drawing.Font MiddleBoldFont => new("Microsoft JhengHei UI", 14, FontStyle.Bold);
        public static System.Drawing.Font SmallBoldFont => new("Microsoft JhengHei UI", 12, FontStyle.Bold);

        public static Size MiddleButtonSize => new(160, 48);

        public static int ButtonSpace => 16;

        public static string Jpeg => ".jpeg";
        public static string Png => ".png";

        public class MagickInfo
        {
            private readonly MagickImage _magickImage;
            public MagickInfo(MagickImage magickImage) => _magickImage = magickImage;

            public bool IsJpeg()
            {
                MagickFormat format = _magickImage.Format;

                switch (format)
                {
                    case MagickFormat.J2c:
                    case MagickFormat.J2k:
                    case MagickFormat.Jng:
                    case MagickFormat.Jp2:
                    case MagickFormat.Jpc:
                    case MagickFormat.Jpe:
                    case MagickFormat.Jpeg:
                    case MagickFormat.Jpg:
                    case MagickFormat.Jpm:
                    case MagickFormat.Jps:
                    case MagickFormat.Jpt:
                        return true;
                    default:
                        return false;
                }
            }

            public bool IsAiOrEps()
            {
                MagickFormat format = _magickImage.Format;
                switch (format)
                {
                    case MagickFormat.Ai:
                    case MagickFormat.Eps:
                    case MagickFormat.Eps2:
                    case MagickFormat.Eps3:
                    case MagickFormat.Epsf:
                    case MagickFormat.Epsi:
                        return true;
                    default:
                        return false;
                }
            }

            public string GetFileFormat()
            {
                return Enum.GetName(typeof(MagickFormat), _magickImage.Format) ?? string.Empty;
            }

            public string GetDpiString()
            {
                return _magickImage.Density.ToString(DensityUnit.PixelsPerInch);
            }

            public Density GetDpi()
            {
                return _magickImage.Density;
            }

            public bool GetIsBelow300Dpi()
            {
                return _magickImage.Density.X < DefaultDpi || _magickImage.Density.Y < DefaultDpi;
            }

            public double GetWidthInCm()
            {
                // 获取图像的宽度（像素）
                uint widthInPixels = _magickImage.Width;

                // 获取图像的分辨率（DPI）
                double dpi = _magickImage.Density.X;

                // 将像素转换为 CM
                double widthInInches = widthInPixels / dpi;
                double widthInCm = widthInInches * InchToCmRadio;

                // 四舍五入保留一位小数
                return Math.Round(widthInCm, 1);
            }



            public double GetFileSizeInKB()
            {
                // 将图像转换为字节数组
                byte[] imageBytes = _magickImage.ToByteArray();

                // 获取文件大小（字节）
                long fileSizeInBytes = imageBytes.Length;

                // 将字节转换为KB
                double fileSizeInKB = fileSizeInBytes / 1024.0;

                // 四舍五入保留两位小数
                return Math.Round(fileSizeInKB, 2);
            }

        }

        public static double InchToCmRadio => 2.54;

        public static string SettingFolder => ".LQClientApp";
        public static string WorkSpace => @".LQClientApp\WorkSpace\";

        public static string WordNo => "文件編號";

        public static string SettingPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), SettingFolder);
        public static string WorkSpacePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), WorkSpace);

        public static string SettingFile => "setting.json";
        public static string SettingFilePath => Path.Combine(SettingPath, SettingFile);

        public static string CustomOutPathKey => "CustomOutputPath";

        public static int SevenFieldsCount => 10;

        public static char Semicolon => ';';

        public static class HSpacing
        {
            public static int _5Pixel => 5;
            public static int _20Pixel => 20;
            public static int _200Pixel => 200;
        }

        public static class VSpacing
        {
            public static int _10Pixel => 10;
            public static int _200Pixel => 200;
        }

        public static class Colors
        {
            public static System.Drawing.Color ProgressColor => System.Drawing.Color.Green;
            public static System.Drawing.Color ImageBorderColor => System.Drawing.Color.Blue;
        }

        public static class Margins
        {
            public static int Left => 40;
        }
    }


}
