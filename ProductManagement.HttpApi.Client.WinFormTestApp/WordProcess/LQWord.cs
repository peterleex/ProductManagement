using System.Text;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Layout.Element;
using Serilog;
using Volo.Abp.MultiTenancy;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{

    public class LQWord : LQWordBase, IDisposable
    {
        public LQWord(string wordPath)
        {
            try
            {
                LoadFile(wordPath);
                CheckFile();
                LoadParts();

                SetFileNo();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        private void LoadFile(string wordPath)
        {
            WordPath = wordPath;
            wordDocument = WordprocessingDocument.Open(wordPath, true);
        }

        private void LoadParts()
        {
            documentPart = wordDocument.MainDocumentPart!;
            documentBody = wordDocument.MainDocumentPart!.Document.Body!;
            footerPart = wordDocument.MainDocumentPart!.FooterParts.First();
            footer = footerPart.Footer;
        }

        public string FileNo { get; private set; } = string.Empty;
        private void SetFileNo()
        {
            FileNo = string.Empty;

            var paragraph = footer
                .Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>()
                .FirstOrDefault();
            if (paragraph == null)
                return;

            var justification = paragraph.ParagraphProperties?.Justification;
            if (justification != null && justification.Val != null && justification.Val == JustificationValues.Right)
            {
                var texts = paragraph.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>().ToArray();
                for (int i = 0; i < texts.Length; i++)
                {
                    if (texts[i].Text.Contains(LQDefine.WordNo))
                    {
                        StringBuilder result = new StringBuilder();
                        for (int j = i + 1; j < texts.Length; j++)
                        {
                            result.Append(texts[j].Text);
                        }
                        FileNo = result.ToString();
                    }
                }
                return;
            }

            return;
        }

        public void GetTable()
        {
            StringBuilder exInfos = new();

            List<Field10Row> Field10Table = [];


            var tables = documentBody.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>();
            if (!tables.Any())
                throw new FieldException(LQMessage(LQCode.C0084));

            if (tables.Count() > 1)
                throw new FieldException(LQMessage(LQCode.C0085));

            var table = tables.First();
            var rows = table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>();
            var rowNumber = 0;

            foreach (var row in rows.Skip(Field10Const.HeaderRowCount))
            {
                ++rowNumber;

                try
                {
                    var cells = row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>();

                    var field10Row = new Field10Row()
                    {
                        Field1 = new Field_1(this, rowNumber, FieldName.Field_1_Status_QuestionCode_QuestionSystemCode, cells.ElementAt(0)),
                        Field2 = new Field_2(this, rowNumber, FieldName.Field_2_QuestionSeq, cells.ElementAt(1)),
                        Field3 = new Field_3(this, rowNumber, FieldName.Field_3_QuestionTypeModule_QuestionType, cells.ElementAt(2)),
                        Field4 = new Field_4(this, rowNumber, FieldName.Field_4_Difficulty, cells.ElementAt(3)),
                        Field5 = new Field_5(this, rowNumber, FieldName.Field_5_PublishYearCode_BookNo_ChapterCode_SourceName_SourceExtensionName, cells.ElementAt(4)),
                        Field6 = new CellField(this, rowNumber, FieldName.Field_6_Question, cells.ElementAt(5)),
                        Field7 = new CellField(this, rowNumber, FieldName.Field_7_Answer, cells.ElementAt(6)),
                        Field8 = new CellField(this, rowNumber, FieldName.Field_8_Analysis, cells.ElementAt(7)),
                        Field9 = new Field_9(this, rowNumber, FieldName.Field_9_LimitName_FlexName, cells.ElementAt(8)),
                        Field10 = new Field_10(this, rowNumber, FieldName.Field_10_Comment_ExportCheckBookAccountInfo, cells.ElementAt(9)),
                    };

                    Field10Table.Add(field10Row);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, ex.Message);
                    exInfos.AppendLine(ex.Message);
                }
            }

            if (exInfos.Length > 0)
                LQHelper.ErrorMessage(exInfos.ToString(), LQMessage(LQCode.C0089));
        }

        private void CheckFile()
        {
            string info;

            if (wordDocument.MainDocumentPart == null)
            {
                info = string.Format(LQMessage(LQCode.C0073));
                throw new FieldException(info);
            }

            if (wordDocument.MainDocumentPart.Document.Body == null)
            {
                info = string.Format(LQMessage(LQCode.C0074));
                throw new FieldException(info);
            }

            if (!wordDocument.MainDocumentPart.FooterParts.Any())
            {
                info = string.Format(LQMessage(LQCode.C0076));
                throw new FieldException(info);
            }

            var table = wordDocument.MainDocumentPart.Document.Body
                .Elements<DocumentFormat.OpenXml.Wordprocessing.Table>()
                .FirstOrDefault();
            if (table == null)
            {
                info = string.Format(LQMessage(LQCode.C0072));
                throw new FieldException(info);
            }

            var row = table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>().FirstOrDefault();

            if (row == null)
            {
                info = string.Format(LQMessage(LQCode.C0075));
                throw new FieldException(info);
            }

            var cellCount = row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>().Count();
            if (cellCount != SevenFieldsCount)
            {
                info = string.Format(LQMessage(LQCode.C0071), cellCount);
                throw new FieldException(info);
            }

        }
    }
}
