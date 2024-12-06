using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Domain
{
    public class LQStatusAndQuestionCodes
    {
        public ImportStatus Status { get; set; }
        public string? QuestionCode { get; set; }
        public string? QuestionSystemCode { get; set; }
    }

    public enum ImportStatus
    {
        New,
        Modify
    }
}
