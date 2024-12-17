using ProductManagement.HttpApi.Client.WinFormTestApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{
    public class Field_01 : TextField
    {
        public LQStatusAndQuestionCodes StatusAndQuestionCodes { get; set; } = new LQStatusAndQuestionCodes();
        public Field_01(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }
        protected override void ParseField()
        {
            StatusAndQuestionCodes.Status = StringArray[0].Equals(LQDefine.New) ? ImportStatus.New : ImportStatus.Modify;
            if (StringArray.Length == 3)
            {
                StatusAndQuestionCodes.QuestionCode = StringArray[1];
                StatusAndQuestionCodes.QuestionSystemCode = StringArray[2];
            }
        }
        protected override void CheckField()
        {
            if (StringArray.Length != 1 && StringArray.Length != 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0077), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }

            if (!LQDefine.Field1Keyword.Contains(StringArray[0]))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0086), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new LQ10FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.New && StringArray.Length == 3)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0087), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }

            if (StringArray[0] == LQDefine.Modify && StringArray.Length == 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0088), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }
    }

    public class Field_02 : TextField
    {
        public int Seq { get; set; }
        public Field_02(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }
        protected override void ParseField()
        {
            Seq = int.Parse(StringArray[0]);
        }
        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0078), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new LQ10FieldException(exInfo);
            }

            if (!int.TryParse(StringArray[0], out _))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0090), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new LQ10FieldException(exInfo);
            }
        }
    }

    public class Field_03 : TextField
    {
        public LQParaQuestionType ParaQuestionType { get; set; } = new LQParaQuestionType();
        public Field_03(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0079), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }

        protected override void ParseField()
        {
            QuestionTypeModuleDefinition? module = LQIDQuestionTypeModule.GetKeyByValue(StringArray[0]);
            if (module == null)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0095), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new LQ10FieldException(exInfo);
            }

            ParaQuestionType.Module = (QuestionTypeModuleDefinition)module;
            ParaQuestionType.QuestionTypeName = StringArray[1];
        }

    }

    public class Field_04 : TextField
    {
        public DifficultyCode DifficultyCode { get; set; }
        public Field_04(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {

        }

        protected override void CheckField()
        {
            if (StringArray.Length != 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0080), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }
        protected override void ParseField()
        {
            var diff = Difficulty.GetKeyByValue(StringArray[0]);
            if (diff == null)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0096), Word.WordPath, RowNumber, ColumnNumber, StringArray[0]);
                throw new LQ10FieldException(exInfo);
            }

            DifficultyCode = (DifficultyCode)diff;
        }
    }

    public class Field_05 : TextField
    {
        public List<LQChapterGroup> ChapterGroups { get; set; } = [];

        private string _currentChapterGroup = string.Empty;

        public Field_05(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length < 1)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0081), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }

        protected override void ParseField()
        {
            ParseBoldText();
            ParserChapterGroup();
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
                throw new LQ10FieldException(exInfo);
            }

            _currentChapterGroup = boldTexts.ToString().TrimEnd(LQDefine.Semicolon);
        }

        public void ParserChapterGroup()
        {
            foreach (var chapterGroupPlainText in StringArray)
            {
                var chapterGroup = chapterGroupPlainText.Split(LQDefine.Comma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (chapterGroup.Length < 1)
                {
                    var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0093), Word.WordPath, RowNumber, ColumnNumber, chapterGroupPlainText);
                    throw new LQ10FieldException(exInfo);
                }

                ParseChapterInfo(chapterGroupPlainText, chapterGroup);
            }

            if (!ChapterGroups.Any(cg => cg.Version.IsCurrentVersion))
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0094), Word.WordPath, RowNumber, ColumnNumber);
                throw new LQ10FieldException(exInfo);
            }
        }

        private void ParseChapterInfo(string chapterGroupPlainText, string[] chapterGroup)
        {
            var isCurrentVersion = chapterGroupPlainText == _currentChapterGroup;

            var group = new LQChapterGroup()
            {
                Version = new LQVersion
                {
                    IsCurrentVersion = isCurrentVersion
                },
            };

            foreach (var chapterInfo in chapterGroup)
            {
                var chapterInfoElement = SplitChapterInfo(chapterInfo);
                if (chapterInfoElement.Length != 4 && chapterInfoElement.Length != 5)
                {
                    var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0093), Word.WordPath, RowNumber, ColumnNumber, chapterInfo);
                    throw new LQ10FieldException(exInfo);
                }

                var info = new LQParaChapterInfo
                {
                    PlainText = chapterInfo,
                    PublishYear = chapterInfoElement[0],
                    BookNo = chapterInfoElement[1],
                    ChapterCode = chapterInfoElement[2],
                    SourceName = chapterInfoElement[3],
                };

                if (chapterInfoElement.Length == 5)
                    info.SourceExtensionName = chapterInfoElement[4];


                group.ChapterInfos.Add(info);

                ChapterGroups.Add(group);
            }

            group.Version.PublishYear = group.ChapterInfos.Max(c => c.PublishYear)!;
        }

        private string[] SplitChapterInfo(string str)
        {
            var elements = str.Split([LQDefine.Hyphen, LQDefine.Underscore], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return elements;
        }
    }

    public class Field_06 : CellField
    {
        public Field_06(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
            RemoveAllBookmark();
            CheckWarnningLevel();
            CheckErrorLevel();

            Word.Save();
        }
    }

    public class Field_07 : CellField
    {
        public Field_07(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {

        }
    }

    public class Field_08 : CellField
    {
        public Field_08(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {

        }
    }

    public class Field_09 : TextField
    {
        public LQParaLimitAndFlex ParaLimitAndFlex { get; set; } = new();
        public Field_09(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        {
        }

        protected override void CheckField()
        {
            if (StringArray.Length != 0 && StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0082), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }

        protected override void SplitString()
        {
            SplitStringKeepEmpty();
        }

        protected override void ParseField()
        {
            if (StringArray.Length == 0)
                return;

            if (StringArray[0] != string.Empty)
            {
                var limits = StringArray[0].Split(LQDefine.Comma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var limit in limits)
                {
                    ParaLimitAndFlex.ParaLimits.Add(limit);
                }
            }

            if (StringArray[1] != string.Empty)
            {
                var flexs = StringArray[1].Split(LQDefine.Comma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var flex in flexs)
                {
                    ParaLimitAndFlex.ParaFlexs.Add(flex);
                }
            }
        }
    }

    public class Field_10 : TextField
    {
        public LQCommentAndAlert CommentAndAlert { get; set; } = new();
        public Field_10(LQWord word, int rowNumber, int columnNumber, FieldName fieldName, DocumentFormat.OpenXml.Wordprocessing.TableCell cell)
            : base(word, rowNumber, columnNumber, fieldName, cell)
        { }

        protected override void CheckField()
        {
            if (StringArray.Length != 0 && StringArray.Length != 2)
            {
                var exInfo = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0083), Word.WordPath, RowNumber, ColumnNumber, PlainText);
                throw new LQ10FieldException(exInfo);
            }
        }
        protected override void SplitString()
        {
            SplitStringKeepEmpty();
        }

        protected override void ParseField()
        {
            if (StringArray.Length == 2)
            {
                CommentAndAlert.Comment = StringArray[0];
                CommentAndAlert.Alert = StringArray[1];
            }
        }
    }

}
