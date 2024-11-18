using ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using System.Data;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{

    public partial class LQSelectImage : LQBaseForm
    {
        private static string moduleName = LQDefine.LQMessage(LQDefine.LQCode.C0037);

        public LQSelectImage(IServiceProvider serviceProvider)
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

            btnOpenFile.BackColor = LQDefine.PrimaryColor;
            btnOpenFile.ForeColor = SystemColors.ButtonHighlight;
            btnOpenFile.FlatStyle = FlatStyle.Flat;
            btnOpenFile.Font = LQDefine.BigBoldFont;

            lblSuccessInfo.ForeColor = LQDefine.PrimaryColor;
            lblSuccessInfo.BackColor = System.Drawing.Color.Transparent;
            lblSuccessInfo.Font = LQDefine.BigBoldFont;
            lblSuccessInfo.AutoSize = true;
            lblSuccessInfo.Visible = false;
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
            var imageInfos = LoadImage(filterFile);
            EnterImageProcess(imageInfos);
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
            lblSuccessInfo.Location = new Point(plChooseImageFile.Width / 2 - lblSuccessInfo.Width / 2, spacing);
        }

        protected override void InitForm()
        {
            base.InitForm();

            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            ShowIcon = false;
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenImageFile();
        }

        protected void OpenImageFile()
        {
            var files = OpenImageFileDialog();
            var imageInfos = LoadImage(files);

            EnterImageProcess(imageInfos);
        }

        private void EnterImageProcess(List<ImageInfo> imageInfos)
        {
            var imageProcess = new LQImageProcess(_serviceProvider)
            {
                MdiParent = MdiParent,
                ImageInfos = imageInfos,
            };
            imageProcess.Show();
            Close();
        }

        public void RenewUI(int successCount)
        {
            PbChooseImageFile.Image = Resources.ConvertSuccess1;
            PbChooseImageFile.Refresh();

            var info = string.Format(LQDefine.LQMessage(LQDefine.LQCode.C0060), successCount);
            lblSuccessInfo.Text = $"{info}";
            lblSuccessInfo.Visible = true;

            CenterControls();
        }

        private void PbChooseImageFile_Click(object sender, EventArgs e)
        {

        }
    }
}
