﻿using DocumentFormat.OpenXml.Wordprocessing;
using ImageMagick;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
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
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQImageProcess : LQBaseForm
    {
        public List<ImageInfo> ImageInfos { get; set; } = [];

        private Size imageSideLength;
        private Panel plCommand = null!;
        private FlowLayoutPanel plImages = null!;
        private Panel plImageProcess = null!;
        private MagnifyImage _enlargeImageForm = null!;
        private PictureBox PbMagnifyImage = null!;

        public LQImageProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitModule();
            InitForm();
            InitControl();
            HookEvent();
        }

        private void InitControl()
        {
            PbMagnifyImage = new()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false,
                BackColor = System.Drawing.Color.Transparent
            };
            Controls.Add(PbMagnifyImage);
        }

        private void HookEvent()
        {
            Load += LQImageProcess_Load;
        }


        private void LQImageProcess_Load(object? sender, EventArgs e)
        {
            SetPanelLayout();
            InitPanelContent();
            InitImageSquare();
            //CreateMagnifyImage();
            InitMagnifyPictureBoxSize();

            ShowImages(ImageInfos);
        }

        private void InitPanelContent()
        {
            InitCommandPanel();
        }

        private void InitMagnifyPictureBoxSize()
        {
            PbMagnifyImage.Size = imageSideLength * MagnifyIndex;
        }

        private void InitImageSquare()
        {
            var len = (int)(plImages.ClientSize.Width / WidthDivider);
            imageSideLength = new Size(len, len);
        }

        private void CreateMagnifyImage()
        {
            _enlargeImageForm = new MagnifyImage(imageSideLength);
        }

        private void SetPanelLayout()
        {
            double commandWidthRadio = 1 / 12.0;
            double imageProcessWidthRadio =  2 / 12.0;

            plCommand = new Panel
            {
                Dock = DockStyle.Top,
                Height = (int)(ClientSize.Height * commandWidthRadio),
            };
            Controls.Add(plCommand);

            plImageProcess = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = (int)(ClientSize.Height * imageProcessWidthRadio),
            };
            Controls.Add(plImageProcess);

            plImages = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                //Height = (int)(ClientSize.Height * 5 / 8.0),
                Width = ClientSize.Width,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            Controls.Add(plImages);
            plImages.BringToFront();        // 會被頂部 Panel 擋住，所以加上這句
        }

        private void InitCommandPanel()
        {
            var spacing = 25;
            var buttonHeight = 40;
            var buttonWidth = 150;
            Size buttonSize = new(buttonWidth, buttonHeight);

            var btnRefresh = new Button
            {
                Text = LQMessage(LQCode.C0047),
                Size = buttonSize,
                Image = Resources.Refresh,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
            };
            btnRefresh.Click += BtnRefresh_Click; 

            var btnAdd = new Button
            {
                Text = LQMessage(LQCode.C0048),
                Size = buttonSize,
                Image = Resources.Add,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
            };
            btnAdd.Click += BtnAdd_Click;

            var btnClearAll = new Button
            {
                Text = LQMessage(LQCode.C0049),
                Size = buttonSize,
                Image = Resources.ClearAll,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
            };
            btnClearAll.Click += (_, _) => Close();

            plCommand.Controls.Add(btnRefresh);
            plCommand.Controls.Add(btnAdd);
            plCommand.Controls.Add(btnClearAll);

            // Center the buttons vertically and arrange them horizontally
            int totalHeight = btnRefresh.Height;
            int startY = (plCommand.Height - totalHeight) / 2;

            btnRefresh.Location = new Point(0, startY);
            btnAdd.Location = new Point(btnRefresh.Width + spacing, startY);
            btnClearAll.Location = new Point(btnRefresh.Width + spacing + btnAdd.Width + spacing, startY);
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            ClearImages();

            var files = ImageInfos.Select(i => i.FilePath).ToArray();
            var imageInfos = LoadImage(files);

            ShowImages(imageInfos);
        }

        private void ClearImages()
        {
            plImages.Controls.Clear();
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var files = OpenImageFileDialog();
            var imageInfos = LoadImage(files);
            ImageInfos.AddRange(imageInfos);
            ShowImages(imageInfos);
        }

        protected override void InitModule()
        {
            ModuleName = LQMessage(LQCode.C0037);
        }

        protected override void InitForm()
        {
            base.InitForm();
            Text = LQMessage(LQCode.C0022) + ModuleName;
        }

        class MagnifyImage : LQBaseForm
        {
            //public Image PictureBoxImage = null!;
            private PictureBox _pictureBox = null!;
            public Image PictureBoxImage
            {
                get => _pictureBox.Image;
                set
                {
                    _pictureBox.Image = value;
                    _pictureBox.Invalidate(); // 觸發 PictureBox 的重新繪製
                }
            }

            private Size _formSize;

            public MagnifyImage(Size formSize)
            {
                _formSize = formSize * MagnifyIndex;

                InitForm();
                InitControl();
            }

            private void InitControl()
            {
                AddPictureBox();
            }

            protected override void InitForm()
            {
                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.CenterParent;
                Size = _formSize;
                ShowInTaskbar = false;
            }

            private void AddPictureBox()
            {
                _pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                };

                Controls.Add(_pictureBox);
            }
        }

        private void ShowImages(List<ImageInfo> imageInfos)
        {
            var pictureBoxHeigth = imageSideLength.Height;
            var pictureBoxWidth = imageSideLength.Width;

            for (int i = 0; i < imageInfos.Count; i++)
            {
                var imageInfo = imageInfos[i];

                if (imageInfo.Images.Count == 0)
                    continue;

                foreach (var image in imageInfo.Images)
                {
                    MagickInfo info = new(image);
                    var isJpeg = info.GetIsJpeg();
                    var widthInCm = info.GetWidthInCm();
                    var isBelow300Dpi = info.GetIsBelow300Dpi();
                    var fileFormat = info.GetFileFormat();
                    var imageDpi = info.GetDpi();

                    var pbImage = new PictureBox
                    {
                        Size = imageSideLength,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        //Image = image.GetThumbnail(imageSideLength).ToBitmap(),
                        Image = image.ToBitmap(),
                    };

                    var pbCloseIcon = new PictureBox
                    {
                        Width = IconWidth,
                        Height = IconHeight,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Resources.CloseIcon,
                        Location = new Point(pbImage.Width - IconWidth, 0),
                        Cursor = Cursors.Hand,
                    };


                    var lblRowOne_FileName = new Label
                    {
                        AutoSize = true,
                        Location = new Point(0, pictureBoxHeigth),
                    };
                    lblRowOne_FileName.Text = GetFileNameFitForUI(lblRowOne_FileName, pictureBoxWidth, imageInfo.FilePath);
                    var row1Height = lblRowOne_FileName.Height;

                    var lblRowTwo_ExtensionName = new Label
                    {
                        AutoSize = true,
                        ForeColor = isJpeg ? DefaultColor : WarningColor,
                        Location = new Point(0, pictureBoxHeigth + lblRowOne_FileName.Height),
                        Text = fileFormat
                    };
                    var row2Height = lblRowTwo_ExtensionName.Height;

                    var row2Y = pictureBoxHeigth + row1Height;
                    var lblRowTwo_Dpi = new Label
                    {
                        AutoSize = true,
                        ForeColor = !isBelow300Dpi ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width, row2Y),
                        Text = imageDpi,
                    };

                    var lblRowTwo_ImageWidth = new Label
                    {
                        AutoSize = true,
                        Text = widthInCm.ToString(),
                        ForeColor = widthInCm <= AllowedMaxWidthInCm ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width + lblRowTwo_Dpi.Width, row2Y),
                    };

                    var fileSize = info.GetFileSizeInKB();
                    var lblRowTwo_FileSize = new Label
                    {
                        AutoSize = true,
                        Text = fileSize.ToString(),
                        ForeColor = fileSize <= AllowedMaxFileSizeInKb ? DefaultColor : WarningColor,
                        Location = new Point(lblRowTwo_ExtensionName.Width + lblRowTwo_Dpi.Width + lblRowTwo_ImageWidth.Width, row2Y),
                    };

                    var row3Y = pictureBoxHeigth + row1Height + row2Height;
                    var lblRowThree_FileExtentisonInfo = new Label
                    {
                        AutoSize = true,
                        Text = isJpeg ? string.Empty : LQMessage(LQCode.C0043),
                        ForeColor = WarningColor,
                        Location = new Point(0, row3Y),
                    };
                    var row3Height = lblRowThree_FileExtentisonInfo.Height;

                    var lblRowThree_DpiInfo = new Label
                    {
                        AutoSize = true,
                        Text = !isBelow300Dpi ? string.Empty : LQMessage(LQCode.C0044),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width, row3Y),
                    };
                    row3Height = Math.Max(row3Height, lblRowThree_DpiInfo.Height);

                    var lblRowThree_WidthInfo = new Label
                    {
                        AutoSize = true,
                        Text = widthInCm <= AllowedMaxWidthInCm ? string.Empty : LQMessage(LQCode.C0045),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width + lblRowThree_DpiInfo.Width, row3Y),
                    };
                    row3Height = Math.Max(row3Height, lblRowThree_WidthInfo.Height);

                    var lblRowThree_FileSizeInfo = new Label
                    {
                        AutoSize = true,
                        Text = fileSize <= AllowedMaxFileSizeInKb ? string.Empty : LQMessage(LQCode.C0046),
                        ForeColor = WarningColor,
                        Location = new Point(lblRowThree_FileExtentisonInfo.Width + lblRowThree_DpiInfo.Width + lblRowThree_WidthInfo.Width, row3Y),
                    };
                    row3Height = Math.Max(row3Height, lblRowThree_FileSizeInfo.Height);

                    var plImage = new Panel
                    {
                        Width = pictureBoxWidth,
                        Height = pictureBoxHeigth + row1Height + row2Height + row3Height,
                        BorderStyle = BorderStyle.None,
                    };

                    plImage.Controls.Add(pbImage);
                    plImage.Controls.Add(pbCloseIcon);
                    pbCloseIcon.BringToFront();
                    plImage.Controls.Add(lblRowOne_FileName);

                    plImage.Controls.Add(lblRowTwo_ExtensionName);
                    plImage.Controls.Add(lblRowTwo_Dpi);
                    plImage.Controls.Add(lblRowTwo_ImageWidth);
                    plImage.Controls.Add(lblRowTwo_FileSize);

                    plImage.Controls.Add(lblRowThree_FileExtentisonInfo);
                    plImage.Controls.Add(lblRowThree_DpiInfo);
                    plImage.Controls.Add(lblRowThree_WidthInfo);

                    plImages.Controls.Add(plImage);
                    plImages.Controls.SetChildIndex(plImage, 0);

                    //pbImage.Click += (s, e) => ShowMagnify(pbImage.Image);
                    pbImage.MouseEnter += (s, e) => ShowMagnify(pbImage.Image);
                    pbImage.MouseLeave += (s, e) => HideMagnify();
                    pbCloseIcon.Click += (s, e) => plImages.Controls.Remove(plImage);
                }
            }
        }

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

        //private void ShowMagnify1(Image image)
        //{
        //    _enlargeImageForm.PictureBoxImage = image;
        //    _enlargeImageForm.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
        //    _enlargeImageForm.BringToFront();
        //    _enlargeImageForm.Visible = true;
        //}

        private void ShowMagnify(Image image)
        {
            PbMagnifyImage.Image = image;
            PbMagnifyImage.Invalidate();
            PbMagnifyImage.Location = CenterLocation();
            PbMagnifyImage.BringToFront();
            PbMagnifyImage.Visible = true;
        }
        private void HideMagnify()
        {
            if (!PbMagnifyImage.ClientRectangle.Contains(PbMagnifyImage.PointToClient(Cursor.Position)))
            {
                PbMagnifyImage.Visible = false;
            }
        }
        private Point CenterLocation()
        {
            return new Point((ClientSize.Width - PbMagnifyImage.Width) / 2, (ClientSize.Height - PbMagnifyImage.Height) / 2);
        }
    }
}
