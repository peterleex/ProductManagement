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
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + ModuleName;
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
                    var pictureBox = new PictureBox
                    {
                        Width = imageFrameWidth,
                        Height = imageFrameHeight,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = image.ToBitmap()
                    };

                    var label = new Label
                    {
                        AutoSize = true,
                        ForeColor = GetImageInfoColor(imageInfo),
                        Location = new Point(0, imageFrameHeight),
                    };
                    label.Text = GetFileName(label, imageFrameWidth, imageInfo.FilePath);
                    //Location = new Point((imageFrameWidth - label.PreferredWidth) / 2, imageFrameHeight);

                    var panel = new Panel
                    {
                        Width = imageFrameWidth,
                        Height = imageFrameHeight + label.Height,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(label);
                    imagePanel.Controls.Add(panel);

                    pictureBox.MouseEnter += (s, e) => ShowImagePreview(pictureBox, imageInfo);
                    pictureBox.MouseLeave += (s, e) => HideImagePreview();
                    pictureBox.MouseClick += (s, e) => RemoveImage(imageInfo);
                }
            }
        }

        private string GetImageInfoText(Label label, ImageInfo imageInfo, MagickImage image, int imageFrameWidth)
        {
            return GetFileName(label, imageFrameWidth, imageInfo.FilePath);
        }

        private static string GetFileName(Label label, int imageFrameWidth, string filePath)
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
