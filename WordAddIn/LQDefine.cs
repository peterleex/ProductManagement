using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn
{
    internal class LQDefine
    {
        public enum LQCode
        {
            C0000,
            C0001,
            C0002,
        }

        public static readonly Dictionary<LQCode, string> LQMessages = new Dictionary<LQCode, string>()
        {
            {LQCode.C0000, "題庫插件" },
            {LQCode.C0001, "請輸入整數" },
            {LQCode.C0002, "修改新七欄" },
        };

        public const float DocumentMargin = 0.5f;

        public static string Field01Name => "狀態;編";
        public static string Field02Name => "題號";
        public static string Field03Name => "模組;題型";
        public static string Field04Name => "難易";
        public static string Field05Name => "發印年度-書號-章節-出處_註記";
        public static string Field06Name => "題目";
        public static string Field07Name => "答案";
        public static string Field08Name => "解析";
        public static string Field09Name => "限;彈";
        public static string Field10Name => "備;系";

        public static double TaskPaneWidthRadio => 0.45;

        public static string LQMessage(LQCode code)
        {
            return LQMessages[code];
        }
    }
}
