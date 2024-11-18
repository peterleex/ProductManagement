using ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess;
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
    public partial class LQBaseForm : Form
    {
        protected IServiceProvider _serviceProvider = null!;
        protected string ModuleName { get; set; } = string.Empty;

        public LQBaseForm()
        {
            InitializeComponent();
        }

        protected virtual void InitModule()
        {
        }

        protected virtual void InitForm()
        {
            Size = LQDefine.DefaultWindowSize;
            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
        }

        protected List<ImageInfo> LoadImage(string[] files)
        {
            List<ImageInfo> images = [];

            if (files == null || files.Length == 0)
                return images;

            var loadImageForm = new LQImageProgress(_serviceProvider)
            {
                OperationType = OperationType.Load,
                Files = files,
            };

            loadImageForm.ShowDialog(this);

            return loadImageForm.ImageInfosFromDisk;
        }

        protected string[] OpenImageFileDialog()
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


    }
}
