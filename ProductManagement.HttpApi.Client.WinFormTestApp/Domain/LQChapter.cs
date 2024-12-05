using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Domain;

public class LQChapter
{
    public string PublishYearCode { get; set; } = null!;
    public string BookNo { get; set; } = null!;
    public string ChapterCode { get; set; } = null!;
    public string SourceName { get; set; } = null!;
    public string? SourceExtensionName { get; set; }
}

public class LQYear
{
    public string Year { get; set; } = null!;
    public bool IsNewest { get; set; } = false; 
}

public class LQYearChapters
{
    public LQYear Year { get; set; } = null!;
    public List<LQChapter> Chapters { get; set; } = [];
}
