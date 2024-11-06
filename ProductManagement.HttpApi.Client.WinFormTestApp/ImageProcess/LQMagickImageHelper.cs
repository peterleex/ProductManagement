using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using ImageMagick;
using Serilog;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using DocumentFormat.OpenXml.Features;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Diagnostics.Tracing;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess
{
    internal static class LQMagickImageExtensions
    {
        public static Bitmap ToBitmap(this MagickImage magickImage)
        {
            using var memoryStream = new MemoryStream();
            if (magickImage.Format == MagickFormat.Ept || magickImage.Format == MagickFormat.Eps || magickImage.Format == MagickFormat.Ai)
                magickImage.Write(memoryStream, MagickFormat.Bmp);
            else
                magickImage.Write(memoryStream);
            memoryStream.Position = 0;
            return new Bitmap(memoryStream);
        }

        public static List<MagickImage> ExtractImagesFromDocx(string filePath)
        {
            var images = new List<MagickImage>();

            using var document = WordprocessingDocument.Open(filePath, false);

            if (document.MainDocumentPart == null)
            {
                var info = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0040), filePath);
                throw new Exception(info);
            }

            var imageParts = document.MainDocumentPart!.ImageParts;

            foreach (var imagePart in imageParts)
            {
                using var stream = imagePart.GetStream();
                var image = new MagickImage(stream);
                images.Add(image);
            }

            return images;
        }

        public static List<MagickImage> ExtractImagesFromPdf(string pdfFilePath)
        {
            var extractedImages = new List<MagickImage>();

            using (var pdfReader = new PdfReader(pdfFilePath))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                for (int pageIndex = 1; pageIndex <= pdfDocument.GetNumberOfPages(); pageIndex++)
                {
                    var page = pdfDocument.GetPage(pageIndex);
                    var strategy = new MagickImageRenderListener(extractedImages);
                    PdfCanvasProcessor parser = new(strategy);
                    parser.ProcessPageContent(page);
                }
            }

            return extractedImages;
        }
    }

    public class MagickImageRenderListener : IEventListener
    {
        private readonly List<MagickImage> _images;

        public MagickImageRenderListener(List<MagickImage> images)
        {
            _images = images;
        }

        public void EventOccurred(IEventData data, iText.Kernel.Pdf.Canvas.Parser.EventType type)
        {
            if (type == iText.Kernel.Pdf.Canvas.Parser.EventType.RENDER_IMAGE)
            {
                var renderInfo = (ImageRenderInfo)data;
                var imageObject = renderInfo.GetImage();

                if (imageObject != null)
                {
                    var imageBytes = imageObject.GetImageBytes();
                    using var ms = new MemoryStream(imageBytes);
                    var readSettings = new MagickReadSettings
                    {
                        Density = new Density(300, 300, DensityUnit.PixelsPerInch),
                    };
                    var magickImage = new MagickImage(ms, readSettings);
                    magickImage.Density = new Density(300, 300, DensityUnit.PixelsPerInch);
                    _images.Add(magickImage);
                }
            }
        }

        ICollection<iText.Kernel.Pdf.Canvas.Parser.EventType> IEventListener.GetSupportedEvents()
        {
            return new HashSet<iText.Kernel.Pdf.Canvas.Parser.EventType> { iText.Kernel.Pdf.Canvas.Parser.EventType.RENDER_IMAGE };
        }
    }
}


