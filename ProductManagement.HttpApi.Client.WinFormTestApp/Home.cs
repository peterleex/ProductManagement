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
            EnterSelectFunction();
        }

        private void EnterSelectFunction()
        {
            var selectFuntion = new SelectFunction
            {
                MdiParent = this
            };
            selectFuntion.Show();
        }

        private MenuStrip menuStrip = null!;

        private void InitForm()
        {
            IsMdiContainer = true;
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


            LoginedHeaderMenuItem.Click += (sender, e) =>
            {
                new Login(_serviceProvider).ShowDialog(this);
            };
            ImageProcessMenuItem.Click += (sender, e) =>
            {
                var imageProcess = new ImageProcess(_serviceProvider);
                imageProcess.MdiParent = this;
                imageProcess.Show();
            };

            // 将菜单添加到窗体
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
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
    }
}
