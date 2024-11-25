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
            List<Field10Row> Field10Table = [];

            var tables = documentBody.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>();
            foreach (var table in tables)
            {
                var rows = table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>();
                var firstRow = rows.First();
                foreach (var row in rows)
                {
                    if (row == firstRow)
                    {
                        continue;
                    }

                    var cells = row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>();

                    Field10Row field10Row = new Field10Row()
                    {
                        Field1 = new Field10Cell(FieldName.Field_1_Status_QuestionCode_QuestionSystemCode, cells.ElementAt(0)),
                        Field2 = new Field10Cell(FieldName.Field_2_QuestionSeq, cells.ElementAt(1)),
                        Field3 = new Field10Cell(FieldName.Field_3_QuestionTypeModule_QuestionType, cells.ElementAt(2)),
                        Field4 = new Field10Cell(FieldName.Field_4_Difficulty, cells.ElementAt(3)),
                        Field5 = new Field10Cell(FieldName.Field_5_PublishYearCode_BookNo_ChapterCode_SourceName_SourceExtensionName, cells.ElementAt(4)),
                        Field6 = new Field10Cell(FieldName.Field_6_Question, cells.ElementAt(5)),
                        Field7 = new Field10Cell(FieldName.Field_7_Answer, cells.ElementAt(6)),
                        Field8 = new Field10Cell(FieldName.Field_8_Analysis, cells.ElementAt(7)),
                        Field9 = new Field10Cell(FieldName.Field_9_LimitName_FlexName, cells.ElementAt(8)),
                        Field10 = new Field10Cell(FieldName.Field_10_Comment_ExportCheckBookAccountInfo, cells.ElementAt(9)),
                    };

                    var field1Text = field10Row.Field1.GetPlainText();

                    Field10Table.Add(field10Row);

                    //foreach (var cell in cells)
                    //{
                    //    var paragraphs = cell.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>();
                    //    foreach (var paragraph in paragraphs)
                    //    {
                    //        var texts = paragraph.Elements<DocumentFormat.OpenXml.Wordprocessing.Text>();
                    //        foreach (var text in texts)
                    //        {
                    //            Log.Information(text.Text);
                    //        }
                    //    }
                    //}
                }
            }
        }

        private void CheckFile()
        {
            string info;

            if (wordDocument.MainDocumentPart == null)
            {
                info = string.Format(LQMessage(LQCode.C0073));
                throw new SevenFieldException(info);
            }

            if (wordDocument.MainDocumentPart.Document.Body == null)
            {
                info = string.Format(LQMessage(LQCode.C0074));
                throw new SevenFieldException(info);
            }

            if (!wordDocument.MainDocumentPart.FooterParts.Any())
            {
                info = string.Format(LQMessage(LQCode.C0076));
                throw new SevenFieldException(info);
            }

            var table = wordDocument.MainDocumentPart.Document.Body
                .Elements<DocumentFormat.OpenXml.Wordprocessing.Table>()
                .FirstOrDefault();
            if (table == null)
            {
                info = string.Format(LQMessage(LQCode.C0072));
                throw new SevenFieldException(info);
            }

            var row = table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>().FirstOrDefault();

            if (row == null)
            {
                info = string.Format(LQMessage(LQCode.C0075));
                throw new SevenFieldException(info);
            }

            var cellCount = row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>().Count();
            if (cellCount != SevenFieldsCount)
            {
                info = string.Format(LQMessage(LQCode.C0071), cellCount);
                throw new SevenFieldException(info);
            }

        }
    }
}
