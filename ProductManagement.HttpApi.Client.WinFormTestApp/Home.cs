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

        private MenuStrip menuStrip = null!;

        private void InitForm()
        {
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0021);
            Hide();
            ShowIcon = false;
        }

        private void InitMenu()
        {
            menuStrip = new MenuStrip();
            ToolStripMenuItem homeMenuItem = new("首頁");
            ToolStripMenuItem imageAppMenuItem = new("圖片小程式");
            ToolStripMenuItem questionCheckMenuItem = new("題目檢查");
            ToolStripMenuItem questionImportMenuItem = new("題目匯入");
            ToolStripMenuItem greetingMenuItem = new("Mei，您好！");

            homeMenuItem.Image = Resources.Home;

            menuStrip.Items.Add(homeMenuItem);
            menuStrip.Items.Add(imageAppMenuItem);
            menuStrip.Items.Add(questionCheckMenuItem);
            menuStrip.Items.Add(questionImportMenuItem);
            menuStrip.Items.Add(greetingMenuItem);

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
