using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
