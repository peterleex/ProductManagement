using ImageMagick;
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
    public partial class LQImageProcess : LQBaseForm
    {
        public List<ImageInfo> ImageInfos { get; set; } = [];

        public LQImageProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            InitModule();
            InitForm();
        }
        protected override void InitModule()
        {
            ModuleName = LQDefine.LQMessage(LQDefine.LQCode.C0037); 
        }

        protected override void InitForm()
        {
            base.InitForm();
            Text = LQDefine.LQMessage(LQDefine.LQCode.C0022) + ModuleName;
        }
    }
}
