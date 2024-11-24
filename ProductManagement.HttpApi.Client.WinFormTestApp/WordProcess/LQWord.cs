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
        private MainDocumentPart documentPart = null!;
        private Body documentBody = null!;
        private FooterPart footerPart = null!;

        public LQWord(string wordPath)
        {
            LoadFile(wordPath);
            Check7Field();
            LoadParts();
            FileNo = GetFooterRightText();
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
        }

        public string FileNo { get; private set; }
        private string GetFooterRightText()
        {
            var footer = footerPart.Footer;

            var paragraph = footer
                .Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>()
                .FirstOrDefault();
            if (paragraph == null)
                return string.Empty;

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
                        return result.ToString();
                    }
                }
                return string.Empty;
            }

            return string.Empty;
        }

        public void GetTable()
        {
            var tables = documentBody.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>();
            foreach (var table in tables)
            {
                var rows = table.Elements<DocumentFormat.OpenXml.Wordprocessing.TableRow>();
                foreach (var row in rows)
                {
                    var cells = row.Elements<DocumentFormat.OpenXml.Wordprocessing.TableCell>();

                    foreach (var cell in cells)
                    {
                        var paragraphs = cell.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>();
                        foreach (var paragraph in paragraphs)
                        {
                            var texts = paragraph.Elements<DocumentFormat.OpenXml.Wordprocessing.Text>();
                            foreach (var text in texts)
                            {
                                Log.Information(text.Text);
                            }
                        }
                    }
                }
            }
        }

        private void Check7Field()
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
