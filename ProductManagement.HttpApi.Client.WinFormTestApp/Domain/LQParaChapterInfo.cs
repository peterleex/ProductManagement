using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Domain;

public class LQParaChapterInfo
{
    public string PublishYear { get; set; } = null!;
    public string BookNo { get; set; } = null!;
    public string ChapterCode { get; set; } = null!;
    public string SourceName { get; set; } = null!;
    public string? SourceExtensionName { get; set; }
    public string PlainText { get; set; } = null!;
}

public class LQVersion
{
    public string PublishYear { get; set; } = null!;
    public bool IsCurrentVersion { get; set; } = false; 
}

public class LQChapterGroup
{
    public LQVersion Version { get; set; } = null!;
    public List<LQParaChapterInfo> ChapterInfos { get; set; } = [];
}
