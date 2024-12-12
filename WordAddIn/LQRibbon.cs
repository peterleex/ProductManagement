using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordAddIn
{
    public partial class LQRibbon
    {
        private void LQRibbon_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void btnModify10Fields_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.CustomTaskPanes[0].Visible = true;
        }

        private void btnAdd10FieldRow_Click(object sender, RibbonControlEventArgs e)
        {
            var rows = ebRowCount.Text;

            if (int.TryParse(rows, out int rowCount))
            {
                Globals.ThisAddIn.Add10FieldsRow(rowCount);
            }
            else
            {
                LQHelper.InfoMessage(LQDefine.LQMessage(LQDefine.LQCode.C0001), LQDefine.LQMessage(LQDefine.LQCode.C0000));
            }
        }

        private void btnRead_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ReadQuestionOperationInfo();
        }

        private void btnReadBookmark_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ReadBookmark();
        }
    }
}
