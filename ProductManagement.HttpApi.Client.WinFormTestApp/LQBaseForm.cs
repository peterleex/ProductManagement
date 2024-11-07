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
    public partial class LQBaseForm : Form
    {
        protected IServiceProvider _serviceProvider = null!;
        protected string ModuleName { get; set; } = string.Empty;

        public LQBaseForm()
        {
            InitializeComponent();
        }

        protected virtual void InitModule()
        {
        }

        protected virtual void InitForm()
        {
            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
        }
    }
}
