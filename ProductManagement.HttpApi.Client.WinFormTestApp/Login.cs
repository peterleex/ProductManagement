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
    public partial class Login : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public Login(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0023);
            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
