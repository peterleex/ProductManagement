using DocumentFormat.OpenXml.EMMA;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess
{
    public class LQImageConvert
    {
        private ProcessSetting Setting = null!;

        public LQImageConvert(ProcessSetting setting)
        {
            Setting = setting;
        }

        public static string SaveWarning { get; set; } = string.Empty;

        public async Task ConvertImages(ImageInfo imageInfo, MagickImage image)
        {
            SetImageDensity(image);
            SetImageWidthAndMaintainAspectRatio(image);
            SetImageGrayScale(image);
            await SaveImage(imageInfo, image);
        }

        private async Task SaveImage(ImageInfo imageInfo, MagickImage image)
        {
            var path = GetOutputFilePath(imageInfo, image);
            await image.WriteAsync(path);
            CheckFileSize(path);
        }

        private void CheckFileSize(string path)
        {

            var fileInfo = new FileInfo(path);
            long fileSizeInBytes = fileInfo.Length;
            double fileSizeInKb = fileSizeInBytes / 1024.0;

            if (fileSizeInKb > AllowedMaxFileSizeInKb)
            {
                SaveWarning += string.Format(LQMessage(LQCode.C0057), fileInfo.Name);
            }
        }

        public string GetOutputFileName(ImageInfo imageInfo, MagickImage image)
        {
            var path = GetOutputFilePath(imageInfo, image);
            return Path.GetFileName(path);

        }

        private string GetOutputFilePath(ImageInfo imageInfo, MagickImage image)
        {
            if (imageInfo.ImageCount == 0)
            {
                return string.Empty;
            }

            var file = new LQImageFileHelper(imageInfo.FilePath);
            var fileIndex = imageInfo.Images.IndexOf(image);
            var isDocxOrPdf = file.IsDocxOrPdf();
            var originalFile = imageInfo.FilePath;
            var orginalPath = Path.GetDirectoryName(originalFile);
            var orignalFileName = Path.GetFileNameWithoutExtension(originalFile);
            var newExtension = Setting.ConvertToPng ? Png : Jpeg;
            var newFileName = isDocxOrPdf ? $"{orignalFileName}{++fileIndex}" : $"{orignalFileName}";
            newFileName = $"{newFileName}{newExtension}";
            var outputPath = string.Empty;

            if (ProcessSetting.IsOutputFolder)
            {
                outputPath = Path.Combine(orginalPath!, DefaultOutputFolder, file.GetDocxPdfName());
            }
            else
                outputPath = Path.Combine(orginalPath!, ProcessSetting.CustomOutputPath, file.GetDocxPdfName());

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            outputPath = Path.Combine(outputPath, newFileName);
            outputPath = GetUniqueFilePath(outputPath);


            return outputPath;
        }

        private string GetUniqueFilePath(string outputPath)
        {
            int count = 1;
            string directory = Path.GetDirectoryName(outputPath)!;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPath);
            string extension = Path.GetExtension(outputPath);
            string newFilePath = outputPath;

            while (File.Exists(newFilePath))
            {
                newFilePath = Path.Combine(directory, $"{fileNameWithoutExtension}({count}){extension}");
                count++;
            }

            return newFilePath;
        }

        private void SetImageDensity(MagickImage image)
        {
            MagickInfo info = new(image);

            if (info.IsAiOrEps() || image.Density.X == 0)
            {
                image.Density = ProcessSetting.DefaultDensitySetting;
                return;
            }

            if (Setting.DensitySetting != null)
                image.Density = Setting.DensitySetting;

        }

        private void SetImageWidthAndMaintainAspectRatio(MagickImage image)
        {
            double widthInCm = image.Width / image.Density.X * InchToCmRadio;
            if (widthInCm > Setting.ImageWidthCm)
            {
                double aspectRatio = (double)image.Height / image.Width;
                var newWidth = (uint)(Setting.ImageWidthCm / InchToCmRadio * image.Density.X);
                var newHeight = (uint)(newWidth * aspectRatio);
                image.Resize(newWidth, newHeight);
            }
        }

        private void SetImageGrayScale(MagickImage image)
        {
            if (Setting.ConvertToGrayScale && image.ColorType != ColorType.Grayscale)
                image.ColorType = ColorType.Grayscale;
        }
    }
}
