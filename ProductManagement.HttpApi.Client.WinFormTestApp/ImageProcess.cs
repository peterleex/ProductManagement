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

namespace ProductManagement.HttpApi.Client.WinFormTestApp
{

    public partial class ImageProcess : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const string moduleName = "Q001 小程式首頁";

        public ImageProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            WindowState = FormWindowState.Maximized;
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + moduleName;
            ShowIcon = false;

        }
    }
}
