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
            HookEvent();
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
            txtSearch.PlaceholderText = LQMessage(LQCode.C0064);

            btnAdd7Field.Text = LQMessage(LQCode.C0065);
            btnAdd7Field.Size = MiddleButtonSize;
            btnAdd7Field.Image = Resources.Add;
            btnAdd7Field.ImageAlign = ContentAlignment.MiddleCenter;
            btnAdd7Field.TextAlign = ContentAlignment.MiddleCenter;
            btnAdd7Field.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd7Field.TabIndex = 0;

            btnOpen7Field.Text = LQMessage(LQCode.C0066);
            btnOpen7Field.Size = MiddleButtonSize;
            btnOpen7Field.Image = Resources.DownArrow;
            btnOpen7Field.ImageAlign = ContentAlignment.MiddleCenter;
            btnOpen7Field.TextAlign = ContentAlignment.MiddleCenter;
            btnOpen7Field.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnOpen7Field.TabIndex = 2;
        }

        protected override void InitModule()
        {
            ModuleName = LQMessage(LQCode.C0063);
        }
    }
}
