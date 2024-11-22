using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Serilog;
using Serilog.Core; // 引入 Open XML SDK 命名空間

namespace ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess
{

    public class LQWord : IDisposable
    {
        private WordprocessingDocument wordDocument;

        public LQWord(string wordPath)
        {
            wordDocument = WordprocessingDocument.Open(wordPath, true);
            FileNo = GetFooterRightText();
        }

        public void Dispose()
        {
            if (wordDocument != null)
            {
                wordDocument.Dispose();
            }
        }

        public string FileNo { get; private set; }
        private string GetFooterRightText()
        {
            var footerPart = wordDocument.MainDocumentPart.FooterParts.FirstOrDefault();
            if (footerPart == null)
            {
                return string.Empty;
            }

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
    }
}
