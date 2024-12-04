using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using IdentityModel.Client;

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
        public Field_1(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 1 && StringArray.Length != 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0077), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }

            if (!LQDefine.Field1Keyword.Contains(StringArray[0]))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0086), Word.WordPath, RowNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.New && StringArray.Length == 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0087), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.Modify && StringArray.Length == 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0088), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_2 : TextField
    {
        public Field_2(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0078), Word.WordPath, RowNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }

            if (!int.TryParse(StringArray[0], out int result))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0090), Word.WordPath, RowNumber, StringArray[0]);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_3 : TextField
    {
        public Field_3(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0079), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_4 : TextField
    {
        public Field_4(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0080), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_5 : TextField
    {
        public Field_5(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length < 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0081), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }

        //protected override void SplitString()
        //{
        //    StringArray = PlainText.Split(LQDefine.Comma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        //}

    }

    public class Field_9 : TextField
    {
        public Field_9(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 0 && StringArray.Length != 1 && StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0082), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class Field_10 : TextField
    {
        public Field_10(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell) { }

        protected override void CheckField()
        {
            if (StringArray.Length != 0 && StringArray.Length != 1 && StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0083), Word.WordPath, RowNumber, PlainText);
                throw new FieldException(exInfo);
            }
        }
    }

    public class TextField : CellField
    {
        public string[] StringArray { get; set; } = null!;

        public TextField(LQWord word, int rowNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, fieldName, cell)
        {

            GetData();
            CheckField();
        }

        protected virtual void CheckField()
        {

        }
        private void GetData()
        {
            GetPlainText();
            SplitString();
        }

        protected virtual void SplitString()
        {
            StringArray = PlainText.Split(LQDefine.Semicolon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class CellField
    {
        public LQWord Word { get; set; } = null!;

        public int RowNumber { get; set; }
        public FieldName Name { get; set; }
        public DocumentFormat.OpenXml.Wordprocessing.TableCell? Value { get; set; } = null;
        public string PlainText { get; private set; } = null!;
        public CellField(LQWord word, int rowNumber, FieldName field10, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
        {
            Word = word;
            RowNumber = rowNumber;
            Name = field10;
            Value = cell;
        }

        protected void GetPlainText()
        {
            if (Value == null)
            {
                PlainText = string.Empty;
                return;
            }

            var textElements = Value.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>();

            PlainText = string.Join(string.Empty, textElements.Select(te => te.Text));
        }

    }
}