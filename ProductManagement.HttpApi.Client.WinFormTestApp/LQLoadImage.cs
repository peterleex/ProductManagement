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
    public partial class LQLoadImage : LQBaseForm
    {
        public string[] Files = [];
        public List<ImageInfo> ImageInfos = [];
        private string info = string.Empty;
        public LQLoadImage(IServiceProvider serviceProvider)
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
            await LoadImage();
            Close();
        }

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
            progressBar.Maximum = Files.Length;
            info = string.Format(LQMessage(LQCode.C0041), Files.Length);

            foreach (var file in Files)
            {
                var imageInfo = new ImageInfo(file);
                await imageInfo.LoadImage();
                ImageInfos.Add(imageInfo);

                progressBar.Value = ++count;
                UpdateProgressLabel();
            }
        }

        private void UpdateProgressLabel()
        {
            lblInfo.Text = info;
            percentageLabel.Text = $"{progressBar.Value * 100 / progressBar.Maximum}%";
        }
    }
}
