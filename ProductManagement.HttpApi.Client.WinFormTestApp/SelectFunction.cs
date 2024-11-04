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
    public partial class SelectFunction : Form
    {
        public SelectFunction()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            WindowState = FormWindowState.Maximized;

        }

        private void CenterPictureBoxes()
        {
            int spacing = 25; // 三個 PictureBox 的间距
            int totalWidth = PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + PbQuestionImportProgram.Width + 2 * spacing;
            int startX = (ClientSize.Width - totalWidth) / 2;
            int startY = (ClientSize.Height - PbImageProcessProgram.Height) / 2;

            PbImageProcessProgram.Location = new Point(startX, startY);
            PbQuestionCheckProgram.Location = new Point(startX + PbImageProcessProgram.Width + spacing, startY);
            PbQuestionImportProgram.Location = new Point(startX + PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + 2 * spacing, startY);

        }

        private void SelectFunction_Resize(object sender, EventArgs e)
        {
            CenterPictureBoxes();
        }
    }
}
