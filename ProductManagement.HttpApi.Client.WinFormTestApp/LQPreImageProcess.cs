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
            InitControls();
            HookEvent();
        }

        private void InitControls()
        {
            PbChooseImageFile.AllowDrop = true;
        }

        private void HookEvent()
        {
            Resize += LQPreImageProcess_Resize;
            PbChooseImageFile.DragEnter += PbChooseImageFile_DragEnter;
            PbChooseImageFile.DragDrop += PbChooseImageFile_DragDrop;
        }

        private void PbChooseImageFile_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PbChooseImageFile_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
                if (files != null && files.Length > 0)
                {
                    ProcessDroppedFiles(files);
                }
            }
        }

        private void ProcessDroppedFiles(string[] files)
        {
            var filterFile = files
                .Where(f => LQDefine.AllSupportedFileType.Contains(Path.GetExtension(f).ToLower()))
                .ToArray();
            OpenImage(filterFile);
        }

        private void LQPreImageProcess_Resize(object? sender, EventArgs e)
        {
            CenterControls();
        }

        private void CenterControls()
        {
            var spacing = 25;

            plChooseImageFile.Location = new Point(
                (ClientSize.Width - plChooseImageFile.Width) / 2,
                (ClientSize.Height - plChooseImageFile.Height) / 2
            );

            pbPleaseSelectImage.Location = new Point(plChooseImageFile.Left, plChooseImageFile.Top - pbPleaseSelectImage.Height - spacing);
        }

        private void InitForm()
        {
            Size = LQDefine.DefaultWindowSize;
            WindowState = FormWindowState.Maximized;
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            ShowIcon = false;
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            //var files = await OpenFileDialogAsync();
            var files = OpenFileDialog();

            OpenImage(files);
        }

        private string[] OpenFileDialog()
        {
            using var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = LQDefine.SupportedFileType;
            dialog.FilterIndex = 3; // all types
            if (dialog.ShowDialog(this) == DialogResult.OK)
                return dialog.FileNames;
            else
                return [];
        }

        private void OpenImage(string[] files)
        {
            List<ImageInfo> imageInfos = LoadImage(files);

            new LQImageProcess(_serviceProvider)
            {
                MdiParent = MdiParent,
                ImageInfos = imageInfos,
            }.Show();

            Close();
        }

        private List<ImageInfo> LoadImage(string[] files)
        {
            List<ImageInfo> images = [];

            if (files == null || files.Length == 0)
                return images;

            var loadImageForm = new LQLoadImage(_serviceProvider)
            {
                Files = files,
            };

            loadImageForm.ShowDialog(this);

            return loadImageForm.ImageInfos;
        }

        //private async Task<string[]> OpenFileDialogAsync()
        //{
        //    var files = Array.Empty<string>();

        //    await Task.Run(() =>
        //    {
        //        var tcs = new TaskCompletionSource<bool>();
        //        var thread = new Thread(() =>
        //        {
        //            using var dialog = new OpenFileDialog();
        //            dialog.Multiselect = true;
        //            dialog.Filter = LQDefine.SupportedFileType;
        //            dialog.FilterIndex = 3; // all types
        //            dialog.ShowDialog();
        //            tcs.SetResult(true);

        //            files = dialog.FileNames;
        //        });
        //        thread.SetApartmentState(ApartmentState.STA);
        //        thread.Start();
        //        tcs.Task.Wait();
        //    });

        //    return files;
        //}
    }
}
