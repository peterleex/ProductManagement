using ProductManagement.HttpApi.Client.WinFormTestApp.Controls;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using ProductManagement.HttpApi.Client.WinFormTestApp.WordProcess;
using Serilog;
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
    public partial class LQQuestionCheck : LQBaseForm
    {
        public LQQuestionCheck(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            InitModule();
            InitForm();
            InitControl();
            InitQuestionGrid();
            HookEvent();
        }

        private void InitQuestionGrid()
        {
            // Initialize DataGridView
            dgvFiles.Columns.Clear();
            dgvFiles.ReadOnly = false;

            // Add columns
            DataGridViewCheckBoxColumn selectFileColumn = new DataGridViewCheckBoxColumn();
            selectFileColumn.ReadOnly = false;
            selectFileColumn.HeaderText = "";
            selectFileColumn.TrueValue = true;
            selectFileColumn.FalseValue = false;
            selectFileColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            selectFileColumn.HeaderCell = new DataGridViewCheckBoxHeaderCell(); // Use custom header cell
            dgvFiles.Columns.Add(selectFileColumn);

            DataGridViewTextBoxColumn fileNameColumn = new();
            fileNameColumn.ReadOnly = true;
            fileNameColumn.HeaderText = "檔名";
            fileNameColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fileNameColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFiles.Columns.Add(fileNameColumn);

            DataGridViewTextBoxColumn questionCountColumn = new();
            questionCountColumn.HeaderText = "題數";
            questionCountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            questionCountColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFiles.Columns.Add(questionCountColumn);

            DataGridViewProgressColumn progressColumn = new();
            progressColumn.ProgressBarColor = Color.Green;
            progressColumn.DefaultCellStyle.ForeColor = Color.Black;
            progressColumn.HeaderText = "進度";
            progressColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            progressColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFiles.Columns.Add(progressColumn);

            DataGridViewImageColumn statusColumn = new();
            statusColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            statusColumn.HeaderText = "狀態";
            statusColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            statusColumn.DefaultCellStyle.NullValue = null;
            statusColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFiles.Columns.Add(statusColumn);

            DataGridViewTextBoxColumn errorColumn = new();
            errorColumn.HeaderText = "錯誤";
            errorColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            errorColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFiles.Columns.Add(errorColumn);

            // Add test data
            dgvFiles.Rows.Add(false, "108普高英文_112_20230428_再版修改段考題6-1~6-6.docx", "100", 0, Resources.Right, "-");
            dgvFiles.Rows.Add(false, "108普高英文_112_20230428_再版修改段考題6-1~6-6.docx", "100", 90, Resources.Wrong, "三筆");
            dgvFiles.Rows.Add(false, "108普高英文_112_20230428_再版修改段考題6-1~6-6.docx", "100", 100, null, "-");
        }

        private async Task TestCellProgress()
        {
            for (int progress = 0; progress <= 100; progress++)
            {
                dgvFiles.Rows[0].Cells[3].Value = progress;
                await Task.Delay(1000); // Wait for 1 second
            }
        }
        private async void btnTestProgressCell_Click(object sender, EventArgs e)
        {
            await TestCellProgress();
        }
        protected override void InitForm()
        {
            base.InitForm();

            Text = LQMessage(LQCode.C0022) + ModuleName;
            ShowIcon = false;
        }

        private void HookEvent()
        {
        }

        private void InitControl()
        {
            txtSearchFile.PlaceholderText = LQMessage(LQCode.C0064);
            txtSearchFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pbMagnifyFile.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            txtSearchQuestion.PlaceholderText = LQMessage(LQCode.C0070);
            txtSearchQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            pbMagnifyQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            lblLoad7Field.Font = BigBoldFont;
            lblQuestionBreif.Font = BigBoldFont;

            btnAdd7Field.Text = LQMessage(LQCode.C0065);
            btnAdd7Field.Size = MiddleButtonSize;
            btnAdd7Field.Image = Resources.Add;
            btnAdd7Field.ImageAlign = ContentAlignment.MiddleCenter;
            btnAdd7Field.TextAlign = ContentAlignment.MiddleCenter;
            btnAdd7Field.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd7Field.TabIndex = 0;

            btnDelete7Field.Text = LQMessage(LQCode.C0067);
            btnDelete7Field.Size = MiddleButtonSize;
            btnDelete7Field.Image = Resources.ClearAll;
            btnDelete7Field.ImageAlign = ContentAlignment.MiddleCenter;
            btnDelete7Field.TextAlign = ContentAlignment.MiddleCenter;
            btnDelete7Field.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete7Field.TabIndex = 1;

            btnOpen7Field.Text = LQMessage(LQCode.C0066);
            btnOpen7Field.Size = MiddleButtonSize;
            btnOpen7Field.Image = Resources.DownArrow;
            btnOpen7Field.ImageAlign = ContentAlignment.MiddleCenter;
            btnOpen7Field.TextAlign = ContentAlignment.MiddleCenter;
            btnOpen7Field.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnOpen7Field.TabIndex = 2;
            btnOpen7Field.Click += BtnOpen7Field_Click;


        }

        private void BtnOpen7Field_Click(object? sender, EventArgs e)
        {
            if (sender == null)
                return;

            var button = (Button)sender;

            CreateMenu(button);
        }

        private void CreateMenu(Button button)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem openFromComputer = new(LQMessage(LQCode.C0068), Resources.Computer, OpenFromComputer_Click)
            {
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            ToolStripMenuItem loadFromCloud = new(LQMessage(LQCode.C0069), Resources.Download, OpenFromCloud_Click)
            {
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            menu.Items.Add(openFromComputer);
            menu.Items.Add(loadFromCloud);

            menu.Show(button, new Point(0, button.Height));
        }

        private void OpenFromComputer_Click(object? sender, EventArgs e)
        {
            LQHelper.InfoMessage("Open From Computer");
        }

        private void OpenFromCloud_Click(object? sender, EventArgs e)
        {
            LQHelper.InfoMessage("Open From Cloud");
        }

        protected override void InitModule()
        {
            ModuleName = LQMessage(LQCode.C0063);
        }

        private void plSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private string wordFile = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\新七欄_測試.docx";
        private string wordFieldOk = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_正常.docx";
        private string wordField1Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第1欄.docx";
        private string wordField2Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第2欄.docx";
        private string wordField3Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第3欄.docx";
        private string wordField4Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第4欄.docx";
        private string wordField5Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第5欄.docx";
        private string wordField9Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第9欄.docx";
        private string wordField10Error = @"D:\DOC\龍騰題庫命題系統\Share\測試\小程式\測試資料\圖片小程式\新七欄\欄位檢查\新七欄_異常_第10欄.docx";
        private void btnGetWordFooterRightText_Click(object sender, EventArgs e)
        {
            try
            {
                using var word = new LQWord(wordFile);
                var footerText = word.FileNo;
            }
            catch (Exception ex)
            {
                LQHelper.ErrorMessage(ex.Message);
                Log.Error(ex, "GetWordFooterRightText Failed");
            }
        }

        private void btnGetTable_Click(object sender, EventArgs e)
        {
            try
            {
                using var wordBad = new LQWord(wordField1Error);
                wordBad.GetTable();
            }
            catch (Exception ex)
            {
                LQHelper.ErrorMessage(ex.Message);
                Log.Error(ex, "GetTable Failed");
            }
        }
    }
}
