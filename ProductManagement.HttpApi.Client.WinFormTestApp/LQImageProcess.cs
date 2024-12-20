﻿using ImageMagick;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProductManagement.HttpApi.Client.WinFormTestApp.ImageProcess;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using static ProductManagement.HttpApi.Client.WinFormTestApp.LQDefine;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQImageProcess : LQBaseForm
    {
        public List<ImageInfo> ImageInfos { get; set; } = [];
        public int SuccessCount { get; private set; } = 0;
        private Size imageSideLength;
        private Panel plCommand = null!;
        private FlowLayoutPanel plImages = null!;
        private Panel plProcess = null!;
        private PictureBox PbMagnifyImage = null!;

        class MagicPictureBox : PictureBox
        {
            public MagickImage MagickImage { get; set; } = null!;
        }

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
                BackColor = System.Drawing.Color.Transparent,
                BorderStyle = BorderStyle.Fixed3D,
            };
            PbMagnifyImage.Parent = this;
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
            InitMagnifyPictureBoxSize();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ShowImages(ImageInfos);
        }

        private void InitPanelContent()
        {
            InitCommandPanel();
            InitProcessPanlel();
        }

        CheckBox chkEqualWidth = null!;
        TextBox txtImageWidth = null!;
        CheckBox chkAdjustColor = null!;
        RadioButton rbKeepRgb = null!;
        RadioButton rbConvertToBlack = null!;
        CheckBox chkKeepDpi = null!;
        CheckBox chkConvertToPng = null!;
        RadioButton rbOutputFolder = null!;
        RadioButton rbCustomPath = null!;
        Label lblOutputPath = null!;
        Button btnSelectOutPutPath = null!;

        ProcessSetting Setting = null!;
        private ProcessSetting GetSetting()
        {
            Setting = new();
            if (chkEqualWidth.Checked && double.TryParse(txtImageWidth.Text, out double result))
                Setting.ImageWidthCm = result;

            if (chkAdjustColor.Checked && rbKeepRgb.Checked)
            {
                Setting.ConvertToGrayScale = false;
            }

            if (chkKeepDpi.Checked)
                Setting.DensitySetting = null;

            Setting.ConvertToPng = chkConvertToPng.Checked;

            ProcessSetting.IsOutputFolder = rbOutputFolder.Checked;

            ProcessSetting.CustomOutputPath = rbCustomPath.Checked ? lblOutputPath.Text : string.Empty;

            return Setting;
        }

        private void InitProcessPanlel()
        {
            var yStart = 10;

            var lblSetting = new Label
            {
                Text = "特殊設定",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(Margins.Left, yStart),
            };
            plProcess.Controls.Add(lblSetting);

            var row1Y = lblSetting.Bottom + VSpacing._10Pixel;
            chkEqualWidth = new CheckBox
            {
                Text = "寬度統一",
                Name = "chkEqualWidth",
                Location = new Point(Margins.Left, row1Y),
                TabIndex = 3,
            };
            plProcess.Controls.Add(chkEqualWidth);

            chkAdjustColor = new CheckBox
            {
                Text = "色彩調整",
                Name = "chkAdjustColor",
                Location = new Point(chkEqualWidth.Right + HSpacing._200Pixel, row1Y),
                TabIndex = 5,
            };
            plProcess.Controls.Add(chkAdjustColor);

            chkKeepDpi = new CheckBox
            {
                Text = "保留原解析度",
                AutoSize = true,
                Name = "chkKeepDpi",
                Location = new Point(chkAdjustColor.Right + HSpacing._200Pixel, row1Y),
                TabIndex = 6,
            };
            plProcess.Controls.Add(chkKeepDpi);

            chkConvertToPng = new CheckBox
            {
                Text = "轉爲 PNG",
                AutoSize = true,
                Name = "chkConvertToPng",
                Location = new Point(chkKeepDpi.Right + HSpacing._200Pixel, row1Y),
                TabIndex = 7,
            };
            plProcess.Controls.Add(chkConvertToPng);

            txtImageWidth = new TextBox
            {
                Name = "txtImageWidth",
                Location = new Point(Margins.Left, chkEqualWidth.Bottom + VSpacing._10Pixel),
                Enabled = false,
                TabIndex = 4,
                PlaceholderText = LQMessage(LQCode.C0052),
            };
            txtImageWidth.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' && (s as TextBox)!.Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            };
            txtImageWidth.Validating += (s, e) =>
            {
                if (double.TryParse(txtImageWidth.Text, out double value))
                {
                    if (value <= 0.0)
                    {
                        e.Cancel = true;
                        LQHelper.InfoMessage("請輸入大於0cm的數值");
                    }
                    else if (value > 14.0)
                    {
                        e.Cancel = true;
                        LQHelper.InfoMessage("上限14cm");
                    }
                    else
                    {
                        txtImageWidth.Text = value.ToString("F1");
                    }
                }
                else
                {
                    e.Cancel = true;
                    LQHelper.InfoMessage("請輸入有效的數值。");
                }
            };
            plProcess.Controls.Add(txtImageWidth);
            chkEqualWidth.CheckedChanged += (s, e) => txtImageWidth.Enabled = chkEqualWidth.Checked;

            var lblCm = new Label
            {
                Text = "cm",
                AutoSize = true,
                Location = new Point(txtImageWidth.Right + HSpacing._5Pixel, chkEqualWidth.Bottom + VSpacing._10Pixel),
            };
            plProcess.Controls.Add(lblCm);

            var lblWidthInfo = new Label
            {
                Name = "lblWidthInfo",
                Text = "寬度不超過14cm，以降低傳輸成本",
                AutoSize = true,
                ForeColor = PrimaryColor,
                Location = new Point(Margins.Left, txtImageWidth.Bottom + VSpacing._10Pixel),
            };
            plProcess.Controls.Add(lblWidthInfo);

            rbKeepRgb = new RadioButton
            {
                Name = "rbKeepRgb",
                AutoSize = true,
                Text = "保留 RGB",
                Location = new Point(chkAdjustColor.Left, chkAdjustColor.Bottom + VSpacing._10Pixel),
                Enabled = false,
                Checked = true,
            };
            plProcess.Controls.Add(rbKeepRgb);

            rbConvertToBlack = new RadioButton
            {
                Name = "rbConvertToBlack",
                AutoSize = true,
                Text = "整圖轉爲K100(純黑)",
                Location = new Point(chkAdjustColor.Left, rbKeepRgb.Bottom + VSpacing._10Pixel),
                Enabled = false,
            };
            plProcess.Controls.Add(rbConvertToBlack);
            chkAdjustColor.CheckedChanged += (s, e) =>
            {
                rbKeepRgb.Enabled = chkAdjustColor.Checked;
                rbConvertToBlack.Enabled = chkAdjustColor.Checked;
            };

            var lblOutput = new Label
            {
                Name = "lblOutput",
                AutoSize = true,
                Text = "輸出位置",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(chkConvertToPng.Right + HSpacing._200Pixel, yStart),
            };
            plProcess.Controls.Add(lblOutput);

            rbOutputFolder = new RadioButton
            {
                Name = "rbOutputFolder",
                Text = "原檔「output」資料夾",
                AutoSize = true,
                Location = new Point(lblOutput.Left, lblOutput.Bottom + VSpacing._10Pixel),
                Checked = true,
            };
            plProcess.Controls.Add(rbOutputFolder);
            rbOutputFolder.CheckedChanged += (s, e) => btnSelectOutPutPath.Enabled = !rbOutputFolder.Checked;

            rbCustomPath = new RadioButton
            {
                Name = "rbCustomPath",
                Text = "自訂路徑",
                AutoSize = true,
                Location = new Point(lblOutput.Left, rbOutputFolder.Bottom + VSpacing._10Pixel),
            };
            plProcess.Controls.Add(rbCustomPath);
            rbCustomPath.CheckedChanged += (s, e) => btnSelectOutPutPath.Enabled = rbCustomPath.Checked;

            lblOutputPath = new Label
            {
                Name = "lblOutputPath",
                Location = new Point(rbCustomPath.Left, rbCustomPath.Bottom + VSpacing._10Pixel),
                AutoSize = true,
                Text = LQHelper.ReadCustomSetting(CustomOutPathKey),
            };
            plProcess.Controls.Add(lblOutputPath);

            btnSelectOutPutPath = new Button
            {
                Text = LQMessage(LQCode.C0055),
                AutoSize = true,
                Location = new Point(rbCustomPath.Right, rbCustomPath.Top),
                Enabled = false,
            };
            btnSelectOutPutPath.Click += (s, e) => SelectOutPutPath();
            plProcess.Controls.Add(btnSelectOutPutPath);

            var btnClose = new Button
            {
                Name = "btnClose",
                Text = "離開",
                Size = MiddleButtonSize,
                Location = new Point(lblOutput.Right + HSpacing._200Pixel, yStart),
                BackColor = SecondaryColor,
                ForeColor = PrimaryColor,
                FlatStyle = FlatStyle.Flat,
                Font = SmallBoldFont,
            };
            btnClose.Click += (s, e) => Close();
            plProcess.Controls.Add(btnClose);

            var btnProcess = new Button
            {
                Name = "btnProcess",
                Text = LQMessage(LQCode.C0053),
                Size = MiddleButtonSize,
                Location = new Point(btnClose.Right + ButtonSpace, yStart),
                BackColor = PrimaryColor,
                ForeColor = SystemColors.ButtonHighlight,
                FlatStyle = FlatStyle.Flat,
                Font = SmallBoldFont,
            };
            btnProcess.Click += (s, e) => ProcessImages();
            plProcess.Controls.Add(btnProcess);

        }

        private void ProcessImages()
        {
            var setting = GetSetting();

            if (!ProcessSetting.IsOutputFolder && ProcessSetting.CustomOutputPath.IsNullOrEmpty())
            {
                LQHelper.InfoMessage(LQMessage(LQCode.C0061));
                return;
            }

            if(!Directory.Exists(ProcessSetting.CustomOutputPath))
            {
                LQHelper.InfoMessage(LQMessage(LQCode.C0062));
                return;
            }

            SuccessCount = ImageInfos.Sum(info => info.ImageCount);

            var loadImageForm = new LQImageProgress(_serviceProvider)
            {
                OperationType = OperationType.Save,
                LQImageConvert = new LQImageConvert(setting),
                ImageInfosToDisk = ImageInfos,
            };

            loadImageForm.ShowDialog(this);

            if (ImageInfos.Count == 0)
            {
                EnterSelectImage(true);
                Close();
            }
            else
                RefreshImage();
        }

        private void EnterSelectImage(bool success)
        {
            var selectImage = new LQSelectImage(_serviceProvider)
            {
                MdiParent = MdiParent
            };

            if (success)
                selectImage.RenewUI(SuccessCount);

            selectImage.Show();
        }

        //private async Task ConvertImages()
        //{
        //    foreach (var imageInfo in ImageInfos)
        //    {
        //        foreach (var image in imageInfo.Images)
        //        {
        //            SetImageDensity(image);
        //            SetImageWidthAndMaintainAspectRatio(image);
        //            SetImageGrayScale(image);
        //            await SaveImage(imageInfo, image);
        //        }
        //    }
        //}

        //private async Task SaveImage(ImageInfo imageInfo, MagickImage image)
        //{
        //    var path = GetOutputFilePath(imageInfo, image);
        //    if (path.IsNullOrEmpty())
        //        return;

        //    await image.WriteAsync(path);
        //}

        //private string GetOutputFilePath(ImageInfo imageInfo, MagickImage image)
        //{
        //    if (imageInfo.ImageCount == 0)
        //    {
        //        return string.Empty;
        //    }

        //    if (!ProcessSetting.IsOutputFolder && lblOutputPath.Text.IsNullOrEmpty())
        //    {
        //        LQHelper.InfoMessage(LQMessage(LQCode.C0061));
        //        return string.Empty;
        //    }

        //    var file = new LQImageFileHelper(imageInfo.FilePath);
        //    var fileIndex = imageInfo.Images.IndexOf(image);
        //    var isDocxOrPdf = file.IsDocxOrPdf();
        //    var originalFile = imageInfo.FilePath;
        //    var orginalPath = Path.GetDirectoryName(originalFile);
        //    var orignalFileName = Path.GetFileNameWithoutExtension(originalFile);
        //    var newExtension = Setting.ConvertToPng ? Png : Jpeg;
        //    var newFileName = isDocxOrPdf ? $"{orignalFileName}{++fileIndex}" : $"{orignalFileName}";
        //    newFileName = $"{newFileName}{newExtension}";
        //    var outputPath = string.Empty;

        //    if (ProcessSetting.IsOutputFolder)
        //    {
        //        outputPath = Path.Combine(orginalPath!, DefaultOutputFolder, file.GetDocxPdfName());
        //    }
        //    else
        //        outputPath = Path.Combine(orginalPath!, lblOutputPath.Text, file.GetDocxPdfName());

        //    if (!Directory.Exists(outputPath))
        //    {
        //        Directory.CreateDirectory(outputPath);
        //    }

        //    outputPath = Path.Combine(outputPath, newFileName);
        //    outputPath = GetUniqueFilePath(outputPath);


        //    return outputPath;
        //}

        private string GetUniqueFilePath(string outputPath)
        {
            int count = 1;
            string directory = Path.GetDirectoryName(outputPath)!;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputPath);
            string extension = Path.GetExtension(outputPath);
            string newFilePath = outputPath;

            while (File.Exists(newFilePath))
            {
                newFilePath = Path.Combine(directory, $"{fileNameWithoutExtension}({count}){extension}");
                count++;
            }

            return newFilePath;
        }

        private void SetImageDensity(MagickImage image)
        {
            MagickInfo info = new(image);

            if (info.IsAiOrEps() || image.Density.X == 0)
            {
                image.Density = ProcessSetting.DefaultDensitySetting;
                return;
            }

            if (Setting.DensitySetting != null)
                image.Density = Setting.DensitySetting;

        }

        private void SetImageGrayScale(MagickImage image)
        {
            if (Setting.ConvertToGrayScale && image.ColorType != ColorType.Grayscale)
                image.ColorType = ColorType.Grayscale;
        }

        private void SelectOutPutPath()
        {
            using var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                lblOutputPath.Text = folderBrowserDialog.SelectedPath;
                SavePathToJson(folderBrowserDialog.SelectedPath);
            }
        }

        private void SavePathToJson(string customPath)
        {
            if (!Directory.Exists(SettingPath))
            {
                Directory.CreateDirectory(SettingPath);
            }

            LQHelper.WriteCustomSetting(CustomOutPathKey, customPath);
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

        //private void CreateMagnifyImage()
        //{
        //    _enlargeImageForm = new MagnifyImage(imageSideLength);
        //}

        private void SetPanelLayout()
        {
            double commandWidthRadio = 1 / 12.0;
            double imageProcessWidthRadio = 2 / 12.0;

            plCommand = new Panel
            {
                Dock = DockStyle.Top,
                Height = (int)(ClientSize.Height * commandWidthRadio),
            };
            Controls.Add(plCommand);

            plProcess = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = (int)(ClientSize.Height * imageProcessWidthRadio),
            };
            Controls.Add(plProcess);

            plImages = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Width = ClientSize.Width,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
            };
            Controls.Add(plImages);
            plImages.BringToFront();        // 會被 plCommand 擋住頂部，故加上這句
        }

        private void InitCommandPanel()
        {
            var btnRefresh = new Button
            {
                Text = LQMessage(LQCode.C0047),
                Size = MiddleButtonSize,
                Image = Resources.Refresh,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TabIndex = 0,
            };
            btnRefresh.Click += BtnRefresh_Click;

            var btnAdd = new Button
            {
                Text = LQMessage(LQCode.C0048),
                Size = MiddleButtonSize,
                Image = Resources.Add,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TabIndex = 1,
            };
            btnAdd.Click += BtnAdd_Click;

            var btnClearAll = new Button
            {
                Text = LQMessage(LQCode.C0049),
                Size = MiddleButtonSize,
                Image = Resources.ClearAll,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TabIndex = 2,
            };
            btnClearAll.Click += (_, _) => Close();

            var lblInfo = new Label
            {
                Text = "圖檔原則：jpg、灰階、300dpi、寬小於14CM、檔案小於1000KB",
                ForeColor = Color.Red,
                AutoSize = true,
            };

            plCommand.Controls.Add(btnRefresh);
            plCommand.Controls.Add(btnAdd);
            plCommand.Controls.Add(btnClearAll);
            plCommand.Controls.Add(lblInfo);

            // Center the buttons vertically and arrange them horizontally
            int totalHeight = btnRefresh.Height;
            int startY = (plCommand.Height - totalHeight) / 2;

            btnRefresh.Location = new Point(Margins.Left, startY);
            btnAdd.Location = new Point(btnRefresh.Right + HSpacing._20Pixel, startY);
            btnClearAll.Location = new Point(btnAdd.Right + HSpacing._20Pixel, startY);
            lblInfo.Location = new Point(btnClearAll.Right + HSpacing._20Pixel, (btnClearAll.Height - lblInfo.Height) / 2 + startY);
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            RefreshImage();
        }

        private void RefreshImage()
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
            var alreadyAdded = ImageInfos.Select(i => i.FilePath);
            files = files
                .Where(f => !alreadyAdded.Contains(f))
                .ToArray();
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
            BackColor = System.Drawing.Color.White;
        }

        //class MagnifyImage : LQBaseForm
        //{
        //    //public Image PictureBoxImage = null!;
        //    private PictureBox _pictureBox = null!;
        //    public Image PictureBoxImage
        //    {
        //        get => _pictureBox.Image;
        //        set
        //        {
        //            _pictureBox.Image = value;
        //            _pictureBox.Invalidate(); // 觸發 PictureBox 的重新繪製
        //        }
        //    }

        //    private Size _formSize;

        //    public MagnifyImage(Size formSize)
        //    {
        //        _formSize = formSize * MagnifyIndex;

        //        InitForm();
        //        InitControl();
        //    }

        //    private void InitControl()
        //    {
        //        AddPictureBox();
        //    }

        //    protected override void InitForm()
        //    {
        //        FormBorderStyle = FormBorderStyle.None;
        //        StartPosition = FormStartPosition.CenterParent;
        //        Size = _formSize;
        //        ShowInTaskbar = false;
        //    }

        //    private void AddPictureBox()
        //    {
        //        _pictureBox = new PictureBox
        //        {
        //            Dock = DockStyle.Fill,
        //            SizeMode = PictureBoxSizeMode.Zoom,
        //        };

        //        Controls.Add(_pictureBox);
        //    }
        //}



        private void ShowImages(List<ImageInfo> imageInfos)
        {
            var pictureBoxHeigth = imageSideLength.Height;
            var pictureBoxWidth = imageSideLength.Width;

            // Create and configure the ProgressBar
            ProgressBar progressBar = new ProgressBar
            {
                Dock = DockStyle.Top,
                Maximum = imageInfos.Sum(info => info.Images.Count),
                Step = 1,
            };
            Controls.Add(progressBar);
            progressBar.BringToFront();

            // Create and configure the Label
            Label progressLabel = new Label
            {
                BackColor = System.Drawing.Color.Transparent, // Ensure the label background is transparent
                AutoSize = true,
                Parent = progressBar, // Set the parent of the label to the progress bar
            };
            progressLabel.Location = new Point((progressBar.Width - progressLabel.Width) / 2, (progressBar.Height - progressLabel.Height) / 2);
            progressBar.Controls.Add(progressLabel);
            progressLabel.BringToFront();

            int totalImages = progressBar.Maximum;
            int loadedImages = 0;

            for (int i = 0; i < imageInfos.Count; i++)
            {
                var imageInfo = imageInfos[i];

                if (imageInfo.Images.Count == 0)
                    continue;

                foreach (var image in imageInfo.Images)
                {
                    MagickInfo info = new(image);
                    var isJpeg = info.IsJpeg();
                    var widthInCm = info.GetWidthInCm();
                    var isBelow300Dpi = info.GetIsBelow300Dpi();
                    var fileFormat = info.GetFileFormat();
                    var imageDpi = info.GetDpiString();

                    var pbImage = new MagicPictureBox
                    {
                        Size = imageSideLength,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        //Image = image.GetThumbnail(imageSideLength).ToBitmap(),
                        Image = image.ToBitmap(),
                        MagickImage = image,
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


                    var lblRow1 = new Label
                    {
                        AutoSize = true,
                    };
                    lblRow1.Text = GetFileNameFitForUI(lblRow1, pictureBoxWidth, imageInfo.FilePath);
                    lblRow1.Location = new Point(0, pictureBoxHeigth);

                    var row1Height = lblRow1.Height;

                    var lblRow2_ExtensionName = new Label
                    {
                        AutoSize = true,
                        ForeColor = isJpeg ? DefaultColor : WarningColor,
                        Location = new Point(0, pictureBoxHeigth + lblRow1.Height),
                        Text = fileFormat
                    };
                    var row2Height = lblRow2_ExtensionName.Height;

                    var row2Y = pictureBoxHeigth + row1Height;
                    var lblRowTwo_Dpi = new Label
                    {
                        AutoSize = true,
                        ForeColor = !isBelow300Dpi ? DefaultColor : WarningColor,
                        Location = new Point(lblRow2_ExtensionName.PreferredWidth, row2Y),
                        Text = imageDpi,
                    };

                    var lblRowTwo_ImageWidth = new Label
                    {
                        AutoSize = true,
                        Text = widthInCm.ToString() + LQMessage(LQCode.C0050),
                        ForeColor = widthInCm <= AllowedMaxWidthInCm ? DefaultColor : WarningColor,
                        Location = new Point(lblRow2_ExtensionName.PreferredWidth + lblRowTwo_Dpi.PreferredWidth, row2Y),
                    };

                    var fileSize = info.GetFileSizeInKB();
                    var lblRowTwo_FileSize = new Label
                    {
                        AutoSize = true,
                        Text = fileSize.ToString() + LQMessage(LQCode.C0051),
                        ForeColor = fileSize <= AllowedMaxFileSizeInKb ? DefaultColor : WarningColor,
                        Location = new Point(lblRow2_ExtensionName.PreferredWidth + lblRowTwo_Dpi.PreferredWidth + lblRowTwo_ImageWidth.PreferredWidth, row2Y),
                    };

                    var row3Y = pictureBoxHeigth + row1Height + row2Height;
                    var lblRow3_ErrorInfo = new Label
                    {
                        AutoSize = true,
                        Text = isJpeg ? string.Empty : LQMessage(LQCode.C0043) + Space,
                        ForeColor = WarningColor,
                    };
                    var row3Height = lblRow3_ErrorInfo.Height;

                    lblRow3_ErrorInfo.Text += !isBelow300Dpi ? string.Empty : LQMessage(LQCode.C0044) + Space;
                    lblRow3_ErrorInfo.Text += widthInCm <= AllowedMaxWidthInCm ? string.Empty : LQMessage(LQCode.C0045) + Space;
                    lblRow3_ErrorInfo.Text += fileSize <= AllowedMaxFileSizeInKb ? string.Empty : LQMessage(LQCode.C0046);
                    lblRow3_ErrorInfo.Location = new Point(0, row3Y);

                    var plImage = new Panel
                    {
                        Width = pictureBoxWidth,
                        Height = pictureBoxHeigth + row1Height + row2Height + row3Height,
                        BorderStyle = BorderStyle.None,
                    };

                    plImage.Controls.Add(pbImage);
                    plImage.Controls.Add(pbCloseIcon);
                    pbCloseIcon.BringToFront();
                    plImage.Controls.Add(lblRow1);

                    plImage.Controls.Add(lblRow2_ExtensionName);
                    plImage.Controls.Add(lblRowTwo_Dpi);
                    plImage.Controls.Add(lblRowTwo_ImageWidth);
                    plImage.Controls.Add(lblRowTwo_FileSize);

                    plImage.Controls.Add(lblRow3_ErrorInfo);

                    plImages.Controls.Add(plImage);
                    plImages.Controls.SetChildIndex(plImage, 0);

                    pbImage.MouseEnter += (s, e) => ShowMagnify(pbImage);
                    pbImage.MouseLeave += (s, e) => HideMagnify(pbImage);
                    pbCloseIcon.Click += (s, e) => RemoveImage(plImage, pbImage);

                    // Update progress
                    loadedImages++;
                    progressBar.PerformStep();
                    progressLabel.Text = $"載入畫面: {loadedImages}/{totalImages}";
                }
            }

            // Remove the progress bar and label after loading is complete
            Controls.Remove(progressBar);
            Controls.Remove(progressLabel);
        }

        private void RemoveImage(Panel plImage, MagicPictureBox pb)
        {
            var imageInfo = ImageInfos.FirstOrDefault(i => i.Images.Contains(pb.MagickImage));

            if (imageInfo != null)
            {
                imageInfo.Images.Remove(pb.MagickImage);
                if (imageInfo.Images.Count == 0)
                {
                    ImageInfos.Remove(imageInfo);
                }
                plImages.Controls.Remove(plImage);
            }
        }

        private void RemoveImage(MagickImage magickImage)
        {
            var imageInfo = ImageInfos.FirstOrDefault(info => info.Images.Contains(magickImage));
            if (imageInfo != null)
            {
                imageInfo.Images.Remove(magickImage);

                if (!imageInfo.Images.Any())
                {
                    ImageInfos.Remove(imageInfo);
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

        private void ShowMagnify(PictureBox pb)
        {
            // PictureBox 無 Border Color 屬性
            pb.BorderStyle = BorderStyle.Fixed3D;

            var image = pb.Image;
            PbMagnifyImage.Image = image;
            PbMagnifyImage.Invalidate();
            PbMagnifyImage.Location = CenterLocation();
            PbMagnifyImage.BringToFront();
            PbMagnifyImage.Visible = true;
        }
        private void HideMagnify(PictureBox pb)
        {
            pb.BorderStyle = BorderStyle.None;

            if (!PbMagnifyImage.ClientRectangle.Contains(PbMagnifyImage.PointToClient(Cursor.Position)))
            {
                PbMagnifyImage.Visible = false;
            }
        }
        private Point CenterLocation()
        {
            return new Point((ClientSize.Width - PbMagnifyImage.Width) / 2, (ClientSize.Height - PbMagnifyImage.Height) / 2);
        }
        private void SetImageWidthAndMaintainAspectRatio(MagickImage image)
        {
            double widthInCm = image.Width / image.Density.X * InchToCmRadio;
            if (widthInCm > Setting.ImageWidthCm)
            {
                double aspectRatio = (double)image.Height / image.Width;
                var newWidth = (uint)(Setting.ImageWidthCm / InchToCmRadio * image.Density.X);
                var newHeight = (uint)(newWidth * aspectRatio);
                image.Resize(newWidth, newHeight);
            }
        }
    }

    public class ProcessSetting
    {
        public double ImageWidthCm { get; set; } = AllowedMaxWidthInCm;
        public Density? DensitySetting { get; set; } = new(300, 300, DensityUnit.PixelsPerInch);

        public static Density DefaultDensitySetting = new(300, 300, DensityUnit.PixelsPerInch);

        public bool ConvertToPng { get; set; }
        public bool ConvertToGrayScale { get; set; } = true;
        public static bool IsOutputFolder { get; set; } = true;
        public static string CustomOutputPath { get; internal set; } = LQHelper.ReadCustomSetting(CustomOutPathKey);

    }
}
