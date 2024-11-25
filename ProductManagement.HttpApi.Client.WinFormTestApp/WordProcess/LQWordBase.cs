using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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

        public void Dispose()
        {
            wordDocument?.Dispose();
        }
    }

    public class Field10Row
    {
        public Field10Cell Field1 { get; set; } = null!;
        public Field10Cell Field2 { get; set; } = null!;
        public Field10Cell Field3 { get; set; } = null!;
        public Field10Cell Field4 { get; set; } = null!;
        public Field10Cell Field5 { get; set; } = null!;
        public Field10Cell Field6 { get; set; } = null!;
        public Field10Cell Field7 { get; set; } = null!;
        public Field10Cell Field8 { get; set; } = null!;
        public Field10Cell Field9 { get; set; } = null!;
        public Field10Cell Field10 { get; set; } = null!;
    }

    public class Field10Cell
    {
        public FieldName Name { get; set; }
        public DocumentFormat.OpenXml.Wordprocessing.TableCell? Value { get; set; } = null;
        public Field10Cell(FieldName field7, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        {
            Name = field7;
            Value = cell;
        }

        public string GetPlainText()
        {
            if (Value == null)
            {
                return string.Empty;
            }

            // Get all the text elements within the TableCell
            var textElements = Value.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

            // Concatenate all the text elements' text
            return string.Join(string.Empty, textElements.Select(te => te.Text));
        }

    }
}