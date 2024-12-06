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
    public class LQ10FieldBase
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
    public class TextField : CellField
    {
        public string[] StringArray { get; set; } = null!;

        public TextField(LQ10FieldWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
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
        public LQ10FieldWord Word { get; set; } = null!;

        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public FieldName Name { get; set; }
        public DocumentFormat.OpenXml.Wordprocessing.TableCell? CellValue { get; set; } = null;
        public string PlainText { get; private set; } = null!;
        public CellField(LQ10FieldWord word, int rowNumber, int columnNumber, FieldName field10, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
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
                throw new LQ10FieldException(exInfo);
            }

            var textElements = CellValue.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

            PlainText = string.Join(string.Empty, textElements.Select(te => te.Text));
        }

    }
}