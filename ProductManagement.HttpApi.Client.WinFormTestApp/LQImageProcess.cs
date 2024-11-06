using ImageMagick;
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

    public partial class LQImageProcess : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private static string moduleName = LQDefine.LQMessage(LQDefine.LQCode.C0037);

        public LQImageProcess(IServiceProvider serviceProvider)
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
            var files = await OpenFileDialogAsync();

            //PbImage.Image = new MagickImage(files[0]).ToBitmap();

            //var image = LQMagickImageExtensions.ExtractImagesFromDocx(files[0]);
            //if (image.Count > 0)
            //    PbImage.Image = image[0].ToBitmap();

            var image = LQMagickImageExtensions.ExtractImagesFromPdf(files[0]);
            if (image.Count > 0)
                PbImage.Image = image[0].ToBitmap();
            
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
