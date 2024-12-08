using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordAddIn
{
    public partial class NavigationPaneControl : UserControl
    {
        public string Info
        {
            set
            {
                lblInfo.Text = value;
            }
        }
        public NavigationPaneControl()
        {
            InitializeComponent();
            HookEvent();
        }

        private void HookEvent()
        {
            dgvQuestion.CellClick += DgvQuestion_CellClick;
        }

        private void DgvQuestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cellValue = dgvQuestion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                LocateBookmark(cellValue);
            }
        }

        private void LocateBookmark(string bookmarkName)
        {
            var wordApp = Globals.ThisAddIn.Application;
            var bookmark = wordApp.ActiveDocument.Bookmarks[bookmarkName];
            if (bookmark != null)
            {
                bookmark.Select();
            }
        }
    }
}
