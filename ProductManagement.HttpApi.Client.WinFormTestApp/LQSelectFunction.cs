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
    public partial class LQSelectFunction : Form
    {
        private static string moduleName = LQDefine.LQMessage(LQDefine.LQCode.C0036);
        public LQSelectFunction()
        {
            InitializeComponent();
            InitForm();
            InitControl();
            HookEvent();
        }

        private void InitControl()
        {
        }

        private void HookEvent()
        {
            PbImageProcessProgram.MouseEnter += PbImageProcessProgram_MouseEnter;
            PbImageProcessProgram.MouseLeave += PbImageProcessProgram_MouseLeave;

            PbImageProcessProgram.Click += PbImageProcessProgram_Click;
        }

        private void PbImageProcessProgram_Click(object? sender, EventArgs e)
        {
            ((LQHome)MdiParent!).EnterImageProcess();
        }

        private void PbImageProcessProgram_MouseEnter(object? sender, EventArgs e)
        {
            PbImageProcessProgram.BackColor = Color.LightBlue;
        }
        private void PbImageProcessProgram_MouseLeave(object? sender, EventArgs e)
        {
            PbImageProcessProgram.BackColor = Color.Transparent;
        }

        private void InitForm()
        {
            WindowState = FormWindowState.Maximized;
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            ShowIcon = false;
        }

        private void CenterControls()
        {
            int spacing = 25; // 三個 PictureBox 的间距
            int totalWidth = PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + PbQuestionImportProgram.Width + 2 * spacing;
            int startX = (ClientSize.Width - totalWidth) / 2;
            int startY = (ClientSize.Height - PbImageProcessProgram.Height) / 2;

            PbImageProcessProgram.Location = new Point(startX, startY);
            PbQuestionCheckProgram.Location = new Point(startX + PbImageProcessProgram.Width + spacing, startY);
            PbQuestionImportProgram.Location = new Point(startX + PbImageProcessProgram.Width + PbQuestionCheckProgram.Width + 2 * spacing, startY);

            PbPleaseSelectProgram.Location = new Point(startX, PbImageProcessProgram.Top - PbPleaseSelectProgram.Height - spacing);
        }

        private void SelectFunction_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }
    }
}
