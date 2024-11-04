using Autofac;
using NUglify;
using ProductManagement.HttpApi.Client.WinFormTestApp.Properties;
using System.Windows.Forms;

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class Home : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public Home(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitForm();
            InitMenu();
        }
        private const string moduleName = "Q001 小程式首頁";
        private MenuStrip menuStrip = null!;

        private void InitForm()
        {
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            Hide();
            ShowIcon = false;
        }

        private static void CheckScreenResolution()
        {
            var currentScreenWidth = Screen.PrimaryScreen!.Bounds.Width;
            var currentScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (LQDefine.PreferScreenWidth != currentScreenWidth | LQDefine.PreferScreenHeight != currentScreenHeight)
            {
                string info = "請調整螢幕解析度為 1920 * 1080，以確保最佳畫面呈現。";
                LQHelper.InfoMessage(info);
            }
        }

        private void InitMenu()
        {
            menuStrip = new MenuStrip();
            ToolStripMenuItem HomeMenuItem = new("首頁");
            ToolStripMenuItem ImageProcessMenuItem = new("圖片小程式");
            ToolStripMenuItem QuestionCheckMenuItem = new("題目檢查");
            ToolStripMenuItem QuestionImportMenuItem = new("題目匯入");
            ToolStripMenuItem LoginedHeaderMenuItem = new("Mei，您好！");

            HomeMenuItem.Image = Resources.Home;
            ImageProcessMenuItem.Image = Resources.ImageProcessIcon;
            QuestionCheckMenuItem.Image = Resources.QuestionCheckIcon;
            QuestionImportMenuItem.Image = Resources.QuestionImportIcon;
            LoginedHeaderMenuItem.Image = Resources.LoginedHeader;

            menuStrip.Items.Add(HomeMenuItem);
            menuStrip.Items.Add(ImageProcessMenuItem);
            menuStrip.Items.Add(QuestionCheckMenuItem);
            menuStrip.Items.Add(QuestionImportMenuItem);
            menuStrip.Items.Add(LoginedHeaderMenuItem);

            // 将菜单添加到窗体
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void CenterPictureBoxes()
        {
            //int totalWidth = PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + PbQuestionImportProgram.Width;
            //int startX = (ClientSize.Width - totalWidth) / 2;
            //int startY = (ClientSize.Height - PbImageProcessProgram.Height) / 2;

            //PbImageProcessProgram.Location = new Point(startX, startY);
            //PbQuestionCheckProgram.Location = new Point(startX + PbImageProcessProgram.Width, startY);
            //PbQuestionImportProgram.Location = new Point(startX + PbImageProcessProgram.Width + PbQuestionCheckProgram.Width, startY);

            int spacing = 20; // 三個 PictureBox 的间距
            int totalWidth = PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + PbQuestionImportProgram.Width + 2 * spacing;
            int startX = (ClientSize.Width - totalWidth) / 2;
            int startY = (ClientSize.Height - PbImageProcessProgram.Height) / 2;

            PbImageProcessProgram.Location = new Point(startX, startY);
            PbQuestionCheckProgram.Location = new Point(startX + PbImageProcessProgram.Width + spacing, startY);
            PbQuestionImportProgram.Location = new Point(startX + PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + 2 * spacing, startY);

        }

        private void Home_Load(object sender, EventArgs e)
        {
            var lqUpdateForm = new LQUpdate(_serviceProvider);
            var result = lqUpdateForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                WindowState = FormWindowState.Maximized;
                CheckScreenResolution();
                Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            CenterPictureBoxes();
        }
    }
}
