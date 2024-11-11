using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQHelper;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess
{
    internal class LQImageFileHelper
    {
        private string _filePath;
        public string Extension => Path.GetExtension(_filePath).ToLowerInvariant();

        public LQImageFileHelper(string filePath) => _filePath = filePath;

        public FileType GetFileType()
        {
            return Extension switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" or ".ai" or ".eps" or ".tif" => FileType.Image,
                ".docx" => FileType.Docx,
                ".pdf" => FileType.Pdf,
                _ => throw new NotSupportedException($"File type {Extension} is not supported")
            };
        }

        public bool IsAiOrEps()
        {
            if (Extension == ".ai" || Extension == ".eps")
                return true;
            else
                return false;
        }
    }

    public enum FileType
    {
        Image,
        Docx,
        Pdf,
    }
}
