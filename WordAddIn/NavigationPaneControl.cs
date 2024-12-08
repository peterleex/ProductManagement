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
    public class LQQuestionOperation
    {
        public string QuestionCode { get; set; }
        public string QuestionSystemCode { get; set; }
        public LQOperation Operation { get; set; }
    }

    public enum LQOperation
    {
        Add = 0,
        Update,
        Ignor,
    }
    public partial class NavigationPaneControl : UserControl
    {
        public string Info
        {
            set
            {
                lblInfo.Text = value;
            }
        }

        public List<LQQuestionOperation> QuestionList { get; set; } = new List<LQQuestionOperation>();

        public void RefreshQuestionListGrid()
        {
            dgvQuestion.DataSource = null;
            dgvQuestion.DataSource = QuestionList;
            dgvQuestion.Refresh();
        }

        public NavigationPaneControl()
        {
            InitializeComponent();
            HookEvent();
            InitQuestionGrid();
        }

        private void InitQuestionGrid()
        {
            dgvQuestion.AutoGenerateColumns = false;
            dgvQuestion.ReadOnly = false;
            dgvQuestion.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2; // Enable editing on keystroke or F2
            dgvQuestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Set selection mode to full row

            ColumnQuestionCode.DataPropertyName = nameof(LQQuestionOperation.QuestionCode);
            ColumnQuestionSystemCode.DataPropertyName = nameof(LQQuestionOperation.QuestionSystemCode);
            ColumnOperation.DataPropertyName = nameof(LQQuestionOperation.Operation);
            ColumnOperation.ReadOnly = false;
            ColumnOperation.DataSource = Enum.GetValues(typeof(LQOperation));
            ColumnOperation.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            ColumnOperation.FlatStyle = FlatStyle.Flat;
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
