using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Presentation;
using ImageMagick;
using ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public enum OperationType
    {
        Load,
        Save,
    }
    public partial class LQImageProgress : LQBaseForm
    {
        public OperationType OperationType { get; set; } = OperationType.Load;
        public LQImageConvert LQImageConvert { get; set; } = null!;
        public string[] Files = [];
        public List<ImageInfo> ImageInfosFromDisk = [];
        public List<ImageInfo> ImageInfosToDisk = [];
        private string info = string.Empty;
        public LQImageProgress(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitForm();
            InitControl();
            HookEvent();
            InitializeCustomComponents();
        }

        private void InitControl()
        {
            lblInfo.Text = string.Empty;
        }

        private void InitializeCustomComponents()
        {
            progressBar.ForeColor = Colors.ProgressColor;
            percentageLabel.Text = string.Empty;
            percentageLabel.ForeColor = Colors.ProgressColor;
        }

        private void HookEvent()
        {
            Load += LQLoadImage_Load;
        }

        private async void LQLoadImage_Load(object? sender, EventArgs e)
        {
            if (OperationType == OperationType.Load)
            {
                await LoadImage();
            }
            else
            {
                await SaveImage();
            }
            Close();
        }
        //string SaveWarning = string.Empty;

        protected override void InitForm()
        {
            Text = LQMessage(LQCode.C0042);
            StartPosition = FormStartPosition.CenterParent;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ShowInTaskbar = false;
        }

        private async Task LoadImage()
        {
            int count = 0;
            int sum = Files.Length;
            progressBar.Maximum = sum;

            info = string.Format(LQMessage(LQCode.C0041), sum);

            foreach (var file in Files)
            {
                var imageInfo = new ImageInfo(file);
                await imageInfo.LoadImage();
                ImageInfosFromDisk.Add(imageInfo);

                progressBar.Value = ++count;
                UpdateProgressLabel();
            }
        }

        private async Task SaveImage()
        {
            int count = 0;
            int sum = ImageInfosToDisk.Sum(info => info.ImageCount);
            progressBar.Maximum = sum;
            info = string.Format(LQMessage(LQCode.C0056), sum);
            LQImageConvert.Warning = string.Empty;
            int failedCount = 0, successCount = 0;
            List<MagickImage> successConerted = [];

            foreach (var imageInfo in ImageInfosToDisk)
            {
                foreach (var image in imageInfo.Images)
                {
                    try
                    {
                        await LQImageConvert.ConvertImages(imageInfo, image);
                        ++successCount;
                        successConerted.Add(image);
                    }
                    catch (Exception)
                    {
                        ++failedCount;
                    }
                    progressBar.Value = ++count;
                    UpdateProgressLabel();
                }
            }

            ImageInfosToDisk.ForEach(info => info.Images.RemoveAll(i => successConerted.Contains(i)));
            ImageInfosToDisk.RemoveAll(info => info.Images.Count == 0);

            ShowResult(successCount, failedCount);
        }

        private static void ShowResult(int successCount, int failedCount)
        {
            var result = string.Format(LQMessage(LQCode.C0059), successCount, failedCount);
            if (!LQImageConvert.Warning.IsNullOrEmpty())
            {
                result += LQImageConvert.Warning;
            }

            LQHelper.InfoMessage(result);
        }

        private void UpdateProgressLabel()
        {
            lblInfo.Text = info;
            percentageLabel.Text = $"{progressBar.Value * 100 / progressBar.Maximum}%";
        }
    }
}
