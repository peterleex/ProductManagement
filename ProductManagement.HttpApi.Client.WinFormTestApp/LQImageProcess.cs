using DocumentFormat.OpenXml.Wordprocessing;
using ImageMagick;
using Microsoft.Extensions.DependencyInjection;
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
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQImageProcess : LQBaseForm
    {
        public List<ImageInfo> ImageInfos { get; set; } = [];

        public LQImageProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitModule();
            InitForm();
            HookEvent();
        }

        private void HookEvent()
        {
            Load += LQImageProcess_Load;
            Layout += LQImageProcess_Layout;
            Resize += LQImageProcess_Resize;
        }

        private FlowLayoutPanel imagePanel;
        private Panel controlPanel;
        private void LQImageProcess_Load(object? sender, EventArgs e)
        {
            InitPanel();
            LoadImages();
        }

        private void LQImageProcess_Layout(object? sender, LayoutEventArgs e)
        {
            //LoadImages();
        }

        private void LQImageProcess_Resize(object? sender, EventArgs e)
        {
            //LoadImages();
        }

        private void InitPanel()
        {
            imagePanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = (int)(ClientSize.Height * 5 / 6.0),
                Width = ClientSize.Width,
                AutoScroll = true,
                //BorderStyle = BorderStyle.FixedSingle,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(imagePanel);

            // Initialize control panel
            controlPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = (int)(ClientSize.Height * 1 / 6.0)
            };
            Controls.Add(controlPanel);
        }

        protected override void InitModule()
        {
            ModuleName = LQDefine.LQMessage(LQDefine.LQCode.C0037);
        }

        protected override void InitForm()
        {
            base.InitForm();
            Text = LQMessage(LQCode.C0022) + ModuleName;
        }

        private void LoadImages()
        {
            int imageFrameWidth = (int)(imagePanel.ClientSize.Width / 7.5);
            int imageFrameHeight = (int)(imagePanel.ClientSize.Height / 3.5);

            for (int i = 0; i < ImageInfos.Count; i++)
            {
                var imageInfo = ImageInfos[i];

                if (imageInfo.Images.Count == 0)
                    continue;

                foreach (var image in imageInfo.Images)
                {
                    MagickInfo info = new(image);
                    var isJpeg = info.GetIsJpeg();
                    var widthInCm = info.GetWidthInCm();
                    var isBelow300Dpi = info.GetIsBelow300Dpi();

                    var pictureBox = new PictureBox
                    {
                        Width = imageFrameWidth,
                        Height = imageFrameHeight,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = image.ToBitmap()
                    };

                    var lblRowOne_FileName = new Label
                    {
                        AutoSize = true,
                        Location = new Point(0, imageFrameHeight),
                    };
                    lblRowOne_FileName.Text = GetFileNameFitForUI(lblRowOne_FileName, imageFrameWidth, imageInfo.FilePath);

                    var lblRowTwo_ExtensionName = new Label
                    {
                        AutoSize = true,
                        ForeColor = isJpeg ? DefaultColor : WarningColor,
                        Location = new Point(0, imageFrameHeight + lblRowOne_FileName.Height),
                        Text = info.FileFormat
                    };

                    var lblRowTwo_Dpi = new Label
                    {
                        AutoSize = true,
                        ForeColor = !isBelow300Dpi ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width, imageFrameHeight + lblRowOne_FileName.Height),
                        Text = info.Dpi,
                    };

                    var lblRowTwo_ImageWidth = new Label
                    {
                        AutoSize = true,
                        Text = widthInCm.ToString(),
                        ForeColor = widthInCm <= AllowedMaxWidthInCm ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width + lblRowTwo_Dpi.Width, imageFrameHeight + lblRowOne_FileName.Height),
                    };

                    var fileSize = info.GetFileSizeInKB();
                    var lblRowTwo_FileSize = new Label
                    {
                        AutoSize = true,
                        Text = fileSize.ToString(),
                        ForeColor = fileSize <= AllowedMaxFileSizeInKb ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width + lblRowTwo_Dpi.Width + lblRowTwo_ImageWidth.Width, imageFrameHeight + lblRowOne_FileName.Height),
                    };

                    var lblRowThree_FileExtentisonInfo = new Label
                    {
                        AutoSize = true,
                        Text = isJpeg? string.Empty:LQMessage(LQCode.C0043),
                        ForeColor = WarningColor,
                        Location = new Point(0, imageFrameHeight + lblRowOne_FileName.Height + lblRowTwo_ExtensionName.Height),
                    };

                    var lblRowThree_DpiInfo = new Label
                    {
                        AutoSize = true,
                        Text = !isBelow300Dpi ? string.Empty : LQMessage(LQCode.C0044),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width, imageFrameHeight + lblRowOne_FileName.Height + lblRowTwo_ExtensionName.Height),
                    };

                    var lblRowThree_WidthInfo = new Label
                    {
                        AutoSize = true,
                        Text = widthInCm <= AllowedMaxWidthInCm ? string.Empty : LQMessage(LQCode.C0045),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width + lblRowThree_DpiInfo.Width, imageFrameHeight + lblRowOne_FileName.Height + lblRowTwo_ExtensionName.Height),
                    };

                    var lblRowThree_FileSizeInfo = new Label
                    {
                        AutoSize = true,
                        Text = fileSize <= AllowedMaxFileSizeInKb ? string.Empty : LQMessage(LQCode.C0046),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width + lblRowThree_DpiInfo.Width + lblRowThree_WidthInfo.Width, imageFrameHeight + lblRowOne_FileName.Height + lblRowTwo_ExtensionName.Height),
                    };

                    var panel = new Panel
                    {
                        Width = imageFrameWidth,
                        Height = imageFrameHeight + lblRowOne_FileName.Height,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(lblRowOne_FileName);

                    panel.Controls.Add(lblRowTwo_ExtensionName);
                    panel.Controls.Add(lblRowTwo_Dpi);
                    panel.Controls.Add(lblRowTwo_ImageWidth);
                    panel.Controls.Add(lblRowTwo_FileSize);

                    panel.Controls.Add(lblRowThree_FileExtentisonInfo);
                    panel.Controls.Add(lblRowThree_DpiInfo);
                    panel.Controls.Add(lblRowThree_WidthInfo);

                    imagePanel.Controls.Add(panel);

                    pictureBox.MouseEnter += (s, e) => ShowImagePreview(pictureBox, imageInfo);
                    pictureBox.MouseLeave += (s, e) => HideImagePreview();
                    pictureBox.MouseClick += (s, e) => RemoveImage(imageInfo);
                }
            }
        }

        //private string GetImageInfoText(Label label, ImageInfo imageInfo, MagickImage image, int imageFrameWidth)
        //{
        //    return GetFileName(label, imageFrameWidth, imageInfo.FilePath);
        //}

        private static string GetFileNameFitForUI(Label label, int imageFrameWidth, string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            using (Graphics g = label.CreateGraphics())
            {
                SizeF size = g.MeasureString(fileName, label.Font);
                if (size.Width <= imageFrameWidth)
                {
                    return fileName;
                }
                else
                {
                    int charCount = fileName.Length;
                    for (int i = 1; i < charCount; i++)
                    {
                        string truncatedFileName = fileName.Substring(0, i) + "…" + fileName.Substring(charCount - i);
                        size = g.MeasureString(truncatedFileName, label.Font);
                        if (size.Width > imageFrameWidth)
                        {
                            return fileName.Substring(0, i - 1) + "…" + fileName.Substring(charCount - (i - 1));
                        }
                    }
                }
            }
            return fileName;
        }

        //return $"{imageInfo.FilePath}\n{imageInfo.Images.First().Format}\n{imageInfo.Images.First().Density} dpi\n{imageInfo.Images.First().Width / 100.0} cm\n{imageInfo.Images.First().Height / 1024.0} KB";

        private System.Drawing.Color GetImageInfoColor(ImageInfo imageInfo)
        {
            // Implement logic to get image info color based on conditions
            return System.Drawing.Color.Black;
        }

        private void ShowImagePreview(PictureBox pictureBox, ImageInfo imageInfo)
        {
            var previewForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Size = new Size(pictureBox.Width * 2, pictureBox.Height * 2),
                Location = new Point(Cursor.Position.X, Cursor.Position.Y)
            };

            var previewPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = pictureBox.Image
            };

            previewForm.Controls.Add(previewPictureBox);
            previewForm.Show();

            pictureBox.MouseLeave += (s, e) => previewForm.Close();
        }

        private void HideImagePreview()
        {
            // Implement logic to hide image preview
        }

        private void RemoveImage(ImageInfo imageInfo)
        {
            // Implement logic to remove image
        }
    }
}
