using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Domain
{
    public static class LQIDQuestionTypeModule
    {
        public static Dictionary<QuestionTypeModuleDefinition, string> Modules =
            new()
            {
                {QuestionTypeModuleDefinition.TrueFalse, "是非題" },
                {QuestionTypeModuleDefinition.SingleChoice, "單選題" },
                {QuestionTypeModuleDefinition.MultiChoice, "多選題" },
                {QuestionTypeModuleDefinition.FillBlank, "填充題" },
                {QuestionTypeModuleDefinition.Matching, "配合題" },
                {QuestionTypeModuleDefinition.OpenResponse, "問答題" },
                {QuestionTypeModuleDefinition.FillBlank_line, "填充題_底線" },
                {QuestionTypeModuleDefinition.Matching_line, "配合題_底線" },
                {QuestionTypeModuleDefinition.Mix, "混合題" },
            };

        public static QuestionTypeModuleDefinition? GetKeyByValue(string value)
        {
            return Modules.FirstOrDefault(x => x.Value == value).Key;
        }
    }

    public enum QuestionTypeModuleDefinition
    {
        TrueFalse = 1,      // 是非題
        SingleChoice,       // 單選題
        MultiChoice,        // 多選題
        FillBlank,          // 填充題
        Matching,           // 配合題
        OpenResponse,       // 問答題
        FillBlank_line,     // 填充題_底線
        Matching_line,      // 配合題_底線
        Mix,                // 混合題
    }
}
