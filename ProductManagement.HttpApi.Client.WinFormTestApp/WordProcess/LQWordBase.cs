using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using IdentityModel.Client;
using iText.StyledXmlParser.Jsoup.Parser;
using ProductManagement.HttpApi.Client.WinFormTestApp.Domain;
using System.Text;
using System.Text.RegularExpressions;
using static ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess.Field_05;
using DocumentFormat.OpenXml.Drawing.Pictures;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess;
public enum FieldName
{
    Field_1_Status_QuestionCode_QuestionSystemCode,
    Field_2_QuestionSeq,
    Field_3_QuestionTypeModule_QuestionType,
    Field_4_Difficulty,
    Field_5_PublishYearCode_BookNo_ChapterCode_SourceName_SourceExtensionName,
    Field_6_Question,
    Field_7_Answer,
    Field_8_Analysis,
    Field_9_LimitName_FlexName,
    Field_10_Comment_ExportCheckBookAccountInfo,
}
public class LQWordBase
{
    public WordprocessingDocument WordprocessingDocument = null!;
    public MainDocumentPart MainDocumentPart { get; set; } = null!;
    protected Body documentBody = null!;
    protected FooterPart footerPart = null!;
    protected Footer footer = null!;
    public string WordPath { get; set; } = null!;

    public void Dispose()
    {
        WordprocessingDocument?.Dispose();
    }

    public void Save()
    {
        WordprocessingDocument.Save();
    }
}
public class TextField : CellField
{
    public string[] StringArray { get; set; } = null!;

    public TextField(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    {
        GetData();
        CheckField();
    }
    private void GetData()
    {
        GetPlainText();
        SplitString();
        ParseField();
    }
    protected virtual void SplitString()
    {
        SplitStringRemoveEmpty();
    }

    protected void SplitStringRemoveEmpty()
    {
        StringArray = PlainText.Split(LQDefine.Semicolon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    protected void SplitStringKeepEmpty()
    {
        StringArray = PlainText.Split(LQDefine.Semicolon, StringSplitOptions.TrimEntries);
    }

    protected virtual void CheckField()
    {

    }
    protected virtual void ParseField()
    {

    }
}
public class CellField
{
    public LQWord Word { get; set; } = null!;
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public FieldName Name { get; set; }
    public DocumentFormat.OpenXml.Wordprocessing.TableCell? CellValue { get; set; } = null;
    public string PlainText { get; private set; } = null!;
    public CellField(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
    {
        Word = word;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        Name = fieldName;
        CellValue = cell;
    }

    protected void GetPlainText()
    {
        if (CellValue == null)
        {
            var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0091), Word.WordPath, RowNumber, ColumnNumber);
            throw new LQ10FieldException(exInfo);
        }

        var textElements = CellValue.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

        PlainText = string.Join(string.Empty, textElements.Select(te => te.Text));
    }
    protected void RemoveAllBookmark()
    {
        var bookmarks = CellValue!.Descendants<BookmarkStart>().ToList();
        foreach (var bookmark in bookmarks)
        {
            var bookmarkEnd = CellValue!.Descendants<BookmarkEnd>()
                .FirstOrDefault(be => be.Id == bookmark.Id);
            bookmark.Remove();
            bookmarkEnd?.Remove();
        }
    }
    protected void CheckWarnningLevel()
    {
        // 檢查圖片
        var blips = CellValue!.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().ToArray();

        foreach (var blip in blips)
        {
            var embed = blip.Embed?.Value;
            if (embed != null)
            {
                var part = Word.MainDocumentPart.GetPartById(embed) as ImagePart;
                if (part != null)
                {
                    using var stream = part.GetStream();
                    var imageProps = new
                    {
                        Format = part.ContentType,      // LQDefine.ImageType_Jpeg
                        Rotation = GetImageRotation(blip),
                        Flip = GetImageFlip(blip),
                        IsImageCropped = IsImageCropped(blip),
                        Resolution = GetImageResolution(stream),
                        Scale = GetImageScale(blip),
                        Size = stream.Length / 1024.0, // Convert size to KB
                    };

                    //var bookmarkId = Word.GetNextBookmarkId().ToString();
                    //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
                    //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

                    //var run = image.Ancestors<DocumentFormat.OpenXml.Wordprocessing.Run>().First();
                    //run.Parent!.InsertBefore(bookmarkStart, run);
                    //run.Parent!.InsertAfter(bookmarkEnd, run);
                }
            }
        }

        // 檢查超連結
        var hyperlinks = CellValue.Descendants<DocumentFormat.OpenXml.Wordprocessing.Hyperlink>().ToArray();
        foreach (var hyperlink in hyperlinks)
        {
            var bookmarkId = Word.GetNextBookmarkId().ToString();
            var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //hyperlink.Parent!.InsertBefore(bookmarkStart, hyperlink);
            //hyperlink.Parent!.InsertAfter(bookmarkEnd, hyperlink);
        }

        // 檢查表格的欄位是否設定爲「固定欄寬」
        var tables = CellValue.Descendants<DocumentFormat.OpenXml.Wordprocessing.Table>();
        foreach (var table in tables)
        {
            var hasFixColumnWidth = false;

            foreach (var row in table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
            {
                foreach (var cell in row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
                {
                    var cellProperties = cell.TableCellProperties;
                    if (cellProperties != null)
                    {
                        var cellWidth = cellProperties.TableCellWidth;
                        if (cellWidth != null && cellWidth.Type != null && cellWidth.Type == TableWidthUnitValues.Dxa)
                        {
                            hasFixColumnWidth = true;
                            break;
                        }
                    }
                }
                if (hasFixColumnWidth)
                    break;
            }

            if (hasFixColumnWidth)
            {
                //var bookmarkId = Word.GetNextBookmarkId().ToString();
                //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
                //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

                //table.Parent!.InsertBefore(bookmarkStart, table);
                //table.Parent!.InsertAfter(bookmarkEnd, table);
            }
        }
    }
    protected void CheckErrorLevel()
    {
        // 功能變數 w:instrText
        var fieldCodes = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.FieldCode>();

        foreach (var fieldCode in fieldCodes)
        {
            //var bookmarkId = Word.GetNextBookmarkId().ToString();
            //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //fieldCode.Parent!.InsertBefore(bookmarkStart, fieldCode);
            //fieldCode.Parent!.InsertAfter(bookmarkEnd, fieldCode);
        }

        // 查找中文
        var chineseRuns = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>()
            .Where(run => Regex.IsMatch(run.InnerText, @"[\u4e00-\u9fa5]"))      // 是否中文的 Regular Expression
            .Where(run => run.RunProperties?.RunFonts?.EastAsia != "細明體");

        foreach (var run in chineseRuns)
        {
            //var bookmarkId = Word.GetNextBookmarkId().ToString();
            //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //run.Parent!.InsertBefore(bookmarkStart, run);
            //run.Parent!.InsertAfter(bookmarkEnd, run);
        }

        // 查找非標楷體的注音聲符
        var phoneticRuns = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>()
            .Where(run => run.RunProperties?.RunFonts?.EastAsia != "標楷體")
            .Where(run => Regex.IsMatch(run.InnerText, @"[\u3105-\u3129\u02C7\u02CA\u02CB\u02D9]")); // 注音聲符的 Regular Expression

        foreach (var run in phoneticRuns)
        {
            //var bookmarkId = Word.GetNextBookmarkId().ToString();
            //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //run.Parent!.InsertBefore(bookmarkStart, run);
            //run.Parent!.InsertAfter(bookmarkEnd, run);
        }

        // 查找所有英數字且字型不是「Times New Roman」的 Run
        var alphanumericRuns = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>()
            .Where(run => Regex.IsMatch(run.InnerText, @"[a-zA-Z0-9]")) // 是否英數字的 Regular Expression
            .Where(run => run.RunProperties?.RunFonts?.Ascii != "Times New Roman");

        foreach (var run in alphanumericRuns)
        {
            //var bookmarkId = Word.GetNextBookmarkId().ToString();
            //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //run.Parent!.InsertBefore(bookmarkStart, run);
            //run.Parent!.InsertAfter(bookmarkEnd, run);
        }

        // 查找所有帶圈數字且字型不是「MS Mincho」「MS Gothic」「Cambria Math」的 Run
        var circledNumberRuns = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>()
            .Where(run => Regex.IsMatch(run.InnerText, @"[\u2460-\u24FF]")) // 帶圈數字的 Regular Expression
            .Where(run => run.RunProperties?.RunFonts?.Ascii != "MS Mincho" &&
                          run.RunProperties?.RunFonts?.Ascii != "MS Gothic" &&
                          run.RunProperties?.RunFonts?.Ascii != "Cambria Math");
        foreach (var run in circledNumberRuns)
        {
            //var bookmarkId = Word.GetNextBookmarkId().ToString();
            //var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            //var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            //run.Parent!.InsertBefore(bookmarkStart, run);
            //run.Parent!.InsertAfter(bookmarkEnd, run);
        }

        // 查找所有包含（　　）符號的 Run
        var symbolRuns = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>()
            .Where(run => run.InnerText == @"（　　）"); // 符號的 Regular Expression

        foreach (var run in symbolRuns)
        {
            var bookmarkId = Word.GetNextBookmarkId().ToString();
            var bookmarkStart = new BookmarkStart() { Name = bookmarkId, Id = bookmarkId };
            var bookmarkEnd = new BookmarkEnd() { Id = bookmarkId };

            run.Parent!.InsertBefore(bookmarkStart, run);
            run.Parent!.InsertAfter(bookmarkEnd, run);
        }
    }

    public bool IsImageCropped(Blip blip)
    {
        // 检查 BlipFill 中的 FillRectangle
        var blipFill = blip.Parent as DocumentFormat.OpenXml.Drawing.Pictures.BlipFill;
        if (blipFill != null)
        {
            var srcRect = blipFill.GetFirstChild<SourceRectangle>();
            if (srcRect != null)
            {
                // 检查各属性是否存在且不为默认值（0 或 100000）
                bool isCropped = (srcRect.Left != null && srcRect.Left.Value != 0 && srcRect.Left.Value != 100000) ||
                                 (srcRect.Top != null && srcRect.Top.Value != 0 && srcRect.Top.Value != 100000) ||
                                 (srcRect.Right != null && srcRect.Right.Value != 0 && srcRect.Right.Value != 100000) ||
                                 (srcRect.Bottom != null && srcRect.Bottom.Value != 0 && srcRect.Bottom.Value != 100000);

                return isCropped;
            }
        }

        // 如果未找到裁剪属性，表示未裁剪
        return false;
    }

    // 返回旋轉度數
    private double GetImageRotation(DocumentFormat.OpenXml.Drawing.Blip image)
    {
        var transform = image.Ancestors<DocumentFormat.OpenXml.Wordprocessing.Drawing>().First().Descendants<DocumentFormat.OpenXml.Drawing.Transform2D>().FirstOrDefault();
        if (transform != null && transform.Rotation != null)
        {
            // Rotation is stored in 60000ths of a degree
            return transform.Rotation.Value / 60000.0;
        }
        return 0.0;
    }

    // 水平或垂直翻轉
    public (bool IsFlippedHorizontally, bool IsFlippedVertically) GetImageFlip(Blip blip)
    {
        var transform = blip.Ancestors<DocumentFormat.OpenXml.Wordprocessing.Drawing>().First().Descendants<DocumentFormat.OpenXml.Drawing.Transform2D>().FirstOrDefault();
        if (transform != null)
        {
            // 检查 FlipH 和 FlipV 属性
            bool isFlippedH = transform.HorizontalFlip != null && transform.HorizontalFlip.Value;
            bool isFlippedV = transform.VerticalFlip != null && transform.VerticalFlip.Value;

            return (isFlippedH, isFlippedV);
        }

        // 如果未找到对应的属性，默认返回未翻转
        return (false, false);
    }

    // 返回值範例：300, 300
    private (double Width, double Height) GetImageResolution(Stream stream)
    {
        using var image = System.Drawing.Image.FromStream(stream);
        return (image.HorizontalResolution, image.VerticalResolution);
    }

    private (double ScaleX, double ScaleY) GetImageScale(DocumentFormat.OpenXml.Drawing.Blip image)
    {
        return (1.0, 1.0); // 默认缩放比例为1.0
    }
}