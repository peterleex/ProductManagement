using ImageMagick;
using Microsoft.Extensions.Http.Logging;
using Microsoft.VisualBasic.FileIO;
using ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{

    public partial class LQPreImageProcess : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private static string moduleName = LQDefine.LQMessage(LQDefine.LQCode.C0037);

        public LQPreImageProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            WindowState = FormWindowState.Maximized;
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            ShowIcon = false;
        }

        private async void btnOpenFile_Click(object sender, EventArgs e)
        {
            List<ImageInfo> imageInfos = await LoadImage();

            new LQImageProcess(_serviceProvider)
            {
                MdiParent = MdiParent,
                ImageInfos = imageInfos,
            }.Show();

            Close();
        }
        private async Task<List<ImageInfo>> LoadImage()
        {
            List<ImageInfo> images = [];

            var files = await OpenFileDialogAsync();

            if (files == null || files.Length == 0)
                return images;

            var loadImageForm = new LQLoadImage(_serviceProvider)
            {
                Files = files,
            };

            loadImageForm.ShowDialog(this);

            return loadImageForm.ImageInfos;
        }

        private async Task<string[]> OpenFileDialogAsync()
        {
            var files = Array.Empty<string>();

            await Task.Run(() =>
            {
                var tcs = new TaskCompletionSource<bool>();
                var thread = new Thread(() =>
                {
                    using var dialog = new OpenFileDialog();
                    dialog.Multiselect = true;
                    dialog.Filter = LQDefine.SupportedFileType;
                    dialog.FilterIndex = 3; // all types
                    dialog.ShowDialog();
                    tcs.SetResult(true);

                    files = dialog.FileNames;
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                tcs.Task.Wait();
            });

            return files;
        }
    }
}
