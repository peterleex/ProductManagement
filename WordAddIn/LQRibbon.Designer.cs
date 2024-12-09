namespace WordAddIn
{
    partial class LQRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public LQRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnModify10Fields = this.Factory.CreateRibbonButton();
            this.ebRowCount = this.Factory.CreateRibbonEditBox();
            this.btnAdd10FieldRow = this.Factory.CreateRibbonButton();
            this.btnRead = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Label = "題庫插件";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnModify10Fields);
            this.group1.Items.Add(this.ebRowCount);
            this.group1.Items.Add(this.btnAdd10FieldRow);
            this.group1.Label = "新七欄";
            this.group1.Name = "group1";
            // 
            // btnModify10Fields
            // 
            this.btnModify10Fields.Label = "顯示新七欄窗格";
            this.btnModify10Fields.Name = "btnModify10Fields";
            this.btnModify10Fields.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnModify10Fields_Click);
            // 
            // ebRowCount
            // 
            this.ebRowCount.Label = "新增筆數";
            this.ebRowCount.Name = "ebRowCount";
            this.ebRowCount.Text = "1";
            // 
            // btnAdd10FieldRow
            // 
            this.btnAdd10FieldRow.Label = "新增";
            this.btnAdd10FieldRow.Name = "btnAdd10FieldRow";
            this.btnAdd10FieldRow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAdd10FieldRow_Click);
            // 
            // btnRead
            // 
            this.btnRead.Label = "讀取";
            this.btnRead.Name = "btnRead";
            this.btnRead.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRead_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnRead);
            this.group2.Label = "讀取";
            this.group2.Name = "group2";
            // 
            // LQRibbon
            // 
            this.Name = "LQRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.LQRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnModify10Fields;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox ebRowCount;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAdd10FieldRow;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRead;
    }

    partial class ThisRibbonCollection
    {
        internal LQRibbon LQRibbon
        {
            get { return this.GetRibbon<LQRibbon>(); }
        }
    }
}
