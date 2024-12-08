using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;

namespace WordAddIn
{
    public partial class ThisAddIn
    {
        private NavigationPaneControl navigationPaneControl;
        private Microsoft.Office.Tools.CustomTaskPane customTaskPane;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //CloseNavigationPane();

            InitPane();
            CheckDocument();
        }

        private void CheckDocument()
        {
            navigationPaneControl.Info = "文檔中缺少新七欄表格。";
        }

        private void InitPane()
        {
            navigationPaneControl = new NavigationPaneControl();
            customTaskPane = CustomTaskPanes.Add(navigationPaneControl, "修改新七欄");
            customTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionLeft;
            customTaskPane.Width = Application.Width / 3; // Set the width to 1/3 of the Word window
            customTaskPane.Visible = true;
        }

        private bool CheckFor10FieldsTable()
        {
            var tables = Application.ActiveDocument.Tables;
            var tableWithTenColumns = tables.Cast<Word.Table>().Where(t => t.Columns.Count == 10).ToList();

            return tableWithTenColumns.Count == 1;
        }
        private void CloseNavigationPane()
        {
            var commandBars = Application.CommandBars;
            var navigationPane = commandBars["Navigation Pane"];
            if (navigationPane != null && navigationPane.Visible)
            {
                navigationPane.Visible = false;
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
