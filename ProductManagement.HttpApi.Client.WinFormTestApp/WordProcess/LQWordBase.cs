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
using static ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess.Field_5;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
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
        protected WordprocessingDocument wordDocument = null!;
        protected MainDocumentPart documentPart = null!;
        protected Body documentBody = null!;
        protected FooterPart footerPart = null!;
        protected Footer footer = null!;
        public string WordPath { get; set; } = null!;

        public void Dispose()
        {
            wordDocument?.Dispose();
        }
    }

    public class Field10Row
    {
        public Field_1 Field1 { get; set; } = null!;
        public Field_2 Field2 { get; set; } = null!;
        public Field_3 Field3 { get; set; } = null!;
        public Field_4 Field4 { get; set; } = null!;
        public Field_5 Field5 { get; set; } = null!;
        public CellField Field6 { get; set; } = null!;
        public CellField Field7 { get; set; } = null!;
        public CellField Field8 { get; set; } = null!;
        public Field_9 Field9 { get; set; } = null!;
        public Field_10 Field10 { get; set; } = null!;
    }

    public class Field_1 : TextField
    {
        public Field_1(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length != 1 && StringArray.Length != 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0077), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }

            if (!LQDefine.Field1Keyword.Contains(StringArray[0]))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0086), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.New && StringArray.Length == 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0087), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.Modify && StringArray.Length == 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0088), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_2 : TextField
    {
        public Field_2(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0078), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }

            if (!int.TryParse(StringArray[0], out int result))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0090), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_3 : TextField
    {
        public Field_3(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0079), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_4 : TextField
    {
        public Field_4(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {

        }

        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0080), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_5 : TextField
    {
        private LQYearChaptersParser _parser = null!;
        public List<LQYearChapters> YearChapterList { get; set; } = [];
        public string NewestChapters { get; set; } = string.Empty;
        public Field_5(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length < 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0081), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }

        protected override void ParseField()
        {
            ParseBoldText();
            Parser();
        }

        public void ParseBoldText()
        {
            var boldTexts = new StringBuilder();

            var textElements = CellValue!.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

            foreach (var textElement in textElements)
            {
                var runProperties = textElement.Parent?.Descendants<DocumentFormat.OpenXml.Wordprocessing.RunProperties>().FirstOrDefault();
                if (runProperties != null && runProperties.Bold != null)
                {
                    boldTexts.Append(textElement.Text);
                }
            }

            if (boldTexts.Length <= 0)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0092), Word.WordPath, RowNumber, ColumnNumber);
                throw new FieldException(exInfo);
            }

            NewestChapters = boldTexts.ToString().TrimEnd(LQDefine.Semicolon);
        }

        public void Parser()
        {
            foreach (var str in StringArray)
            {
                var charpts = str.Split(LQDefine.Comma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (charpts.Length < 1)
                {
                    var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0093), Word.WordPath, RowNumber, ColumnNumber, str);
                    throw new FieldException(exInfo);
                }

                ParseChapters(str, charpts);
            }
        }

        private string[] SplitChapterStr(string str)
        {
            return str.Split(LQDefine.Hyphen, LQDefine.Underscore);
        }

        private void ParseChapters(string str, string[] chapters)
        {
            var isNewest = str == NewestChapters;

            var year = new LQYear
            {
                IsNewest = isNewest
            };

            var x = new LQYearChapters()
            {
                Year = year,
            };

            foreach (var chapter in chapters)
            {
                var chapterElement = SplitChapterStr(chapter);
                if (chapterElement.Length != 4 && chapterElement.Length != 5)
                {
                    var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0093), Word.WordPath,RowNumber, ColumnNumber, chapter);
                    throw new FieldException(exInfo);
                }

                var chapterA = new LQChapter
                {
                    PublishYearCode = chapterElement[0],
                    BookNo = chapterElement[1],
                    ChapterCode = chapterElement[2],
                    SourceName = chapterElement[3],
                };

                if (chapterElement.Length == 5)
                    chapterA.SourceExtensionName = chapterElement[4];

                x.Chapters.Add(chapterA);

                YearChapterList.Add(x);
            }
        }
    }

    public class LQYearChaptersParser
    {




    }

public class Field_6 : CellField
{
    public Field_6(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    {
    }
}

public class Field_7 : CellField
{
    public Field_7(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    {

    }
}

public class Field_8 : CellField
{
    public Field_8(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    {

    }
}

public class Field_9 : TextField
{
    public Field_9(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    {
    }

    protected override void CheckField()
    {
        if (StringArray.Length != 0 && StringArray.Length != 1 && StringArray.Length != 2)
        {
            var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0082), Word.WordPath, RowNumber, ColumnNumber, PlainText);
            throw new FieldException(exInfo);
        }
    }
}

public class Field_10 : TextField
{
    public Field_10(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        : base(word, rowNumber, columnNumber, fieldName, cell)
    { }

    protected override void CheckField()
    {
        if (StringArray.Length != 0 && StringArray.Length != 1 && StringArray.Length != 2)
        {
            var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0083), Word.WordPath, RowNumber, ColumnNumber, PlainText);
            throw new FieldException(exInfo);
        }
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

    protected virtual void CheckField()
    {

    }

    protected virtual void SplitString()
    {
        StringArray = PlainText.Split(LQDefine.Semicolon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
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
    public CellField(LQWord word, int rowNumber, int columnNumber, FieldName field10, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
    {
        Word = word;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        Name = field10;
        CellValue = cell;
    }

    protected void GetPlainText()
    {
        if (CellValue == null)
        {
            var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0091), Word.WordPath, RowNumber, ColumnNumber);
            throw new FieldException(exInfo);
        }

        var textElements = CellValue.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

        PlainText = string.Join(string.Empty, textElements.Select(te => te.Text));
    }

}
}