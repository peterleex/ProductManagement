using Microsoft.Office.Tools.Word;
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
        public List<LQQuestionOperation> QuestionList { get; set; } = new List<LQQuestionOperation>();
        public List<LQQuestionOperation> FilteredQuestionList { get; set; } = new List<LQQuestionOperation>();
        public List<LQQuestionError> QuestionErrorList = new List<LQQuestionError>();
        public string QuestionFilter
        {
            get
            {
                return txtQuestionCode_SystemCode.Text;
            }
        }

        public string Info
        {
            set
            {
                lblInfo.Text = value;
            }
        }
        public void RefreshQuestionListGrid()
        {
            FilterQuestion();

            dgvQuestion.DataSource = null;
            dgvQuestion.DataSource = FilteredQuestionList;
            dgvQuestion.Refresh();
        }

        public void RefreshErrorsGrid()
        {
            ReadBookmark();
            BindErrorsGrid();
        }

        private void BindErrorsGrid()
        {
            dgvErrors.DataSource = null;
            dgvErrors.DataSource = QuestionErrorList;
            ColumnBookmarkName.DataPropertyName = "BookmarkName";
            dgvErrors.Refresh();
        }

        private void ReadBookmark()
        {
            QuestionErrorList.Clear();

            var wordApp = Globals.ThisAddIn.Application;
            var bookmarks = wordApp.ActiveDocument.Bookmarks;

            foreach (Microsoft.Office.Interop.Word.Bookmark bookmark in bookmarks)
            {
                QuestionErrorList.Add(new LQQuestionError() { Bookmark = bookmark });
            }

        }

        private void FilterQuestion()
        {
            if (string.IsNullOrEmpty(QuestionFilter))
            {
                FilteredQuestionList = QuestionList;
            }
            else
            {
                FilteredQuestionList = QuestionList.Where(x => x.QuestionCode.Contains(QuestionFilter) || x.QuestionSystemCode.Contains(QuestionFilter)).ToList();
            }
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
            dgvQuestion.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvQuestion.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Set selection mode to full row

            ColumnQuestionCode.DataPropertyName = nameof(LQQuestionOperation.QuestionCode);
            ColumnQuestionCode.ReadOnly = true;

            ColumnQuestionSystemCode.DataPropertyName = nameof(LQQuestionOperation.QuestionSystemCode);
            ColumnQuestionSystemCode.ReadOnly = true;

            ColumnOperation.DataPropertyName = nameof(LQQuestionOperation.Operation);
            ColumnOperation.ReadOnly = false;
            ColumnOperation.DataSource = new BindingSource(LQQuestionOperation.LQOperationDefine, null);
            ColumnOperation.ValueMember = "Key";
            ColumnOperation.DisplayMember = "Value";

            dgvErrors.AutoGenerateColumns = false;
            dgvErrors.ReadOnly = true;
        }

        private void HookEvent()
        {
            dgvQuestion.CellClick += DgvQuestion_CellClick;
            dgvQuestion.EditingControlShowing += DgvQuestion_EditingControlShowing;

            dgvErrors.CellClick += DgvErrors_CellClick;

            btnQuery.Click += BtnQuery_Click;
        }

        private void DgvErrors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                QuestionErrorList[e.RowIndex].Bookmark.Select();
            }
        }

        private void DgvQuestion_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvQuestion.CurrentCell.ColumnIndex == dgvQuestion.Columns["ColumnOperation"].Index && e.Control is ComboBox comboBox)
            {
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // 避免重複訂閱事件
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && dgvQuestion.CurrentRow != null && comboBox.SelectedValue != null)
            {
                var selectedOperation = (LQOperation)comboBox.SelectedValue;
                var rowIndex = dgvQuestion.CurrentRow.Index;

                FilteredQuestionList[rowIndex].Operation = selectedOperation;
                UpdateTableRowContent(FilteredQuestionList[rowIndex].RowNumber, FilteredQuestionList[rowIndex].ToString());
                UpdateQuestionList(FilteredQuestionList[rowIndex]);
                LocateTableRow(FilteredQuestionList[rowIndex].RowNumber);
            }
        }

        private void UpdateQuestionList(LQQuestionOperation lQQuestionOperation)
        {
            QuestionList.First(q => q.RowNumber == lQQuestionOperation.RowNumber).Operation = lQQuestionOperation.Operation;
        }

        private void UpdateTableRowContent(int rowNumber, string content)
        {
            var wordApp = Globals.ThisAddIn.Application;
            var table = wordApp.Selection.Tables[1];
            if (table != null && rowNumber <= table.Rows.Count)
            {
                table.Cell(rowNumber, 1).Range.Text = content;
            }
        }

        private void ColumnOperation_ValueChanged(object sender, EventArgs e)
        {
            var comboBox = sender as DataGridViewComboBoxEditingControl;
            if (comboBox != null && dgvQuestion.CurrentRow != null)
            {
                var selectedOperation = (LQOperation)comboBox.SelectedValue;
                var rowIndex = dgvQuestion.CurrentRow.Index;
                FilteredQuestionList[rowIndex].Operation = selectedOperation;
            }
        }

        private void ColumnOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && dgvQuestion.CurrentRow != null)
            {
                var selectedOperation = (LQOperation)comboBox.SelectedValue;
                var rowIndex = dgvQuestion.CurrentRow.Index;
                FilteredQuestionList[rowIndex].Operation = selectedOperation;
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            RefreshQuestionListGrid();
        }

        private void DgvQuestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var rowIndex = FilteredQuestionList[e.RowIndex].RowNumber;
                //var cellValue = dgvQuestion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                LocateTableRow(rowIndex);
            }
        }

        private void LocateTableRow(int rowIndex)
        {
            var wordApp = Globals.ThisAddIn.Application;
            var table = wordApp.Selection.Tables[1];
            if (table != null && rowIndex <= table.Rows.Count)
            {
                table.Rows[rowIndex].Select();
            }
        }
    }
    public class LQQuestionOperation
    {
        public string QuestionCode { get; set; } = string.Empty;
        public string QuestionSystemCode { get; set; } = string.Empty;
        public LQOperation? Operation { get; set; }

        public int RowNumber { get; private set; }

        private string _field01Value = string.Empty;
        public LQQuestionOperation(string field01Value, int rowIndex)
        {
            _field01Value = field01Value;
            RowNumber = rowIndex;

            Parse();
        }

        public override string ToString()
        {
            var result = $"{LQOperationDefine[Operation.Value]};{QuestionCode};{QuestionSystemCode}";
            return result;
        }

        private void Parse()
        {
            var values = _field01Value.Split(new char[] { LQDefine.Semicolon }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length == 0)
            {
                Operation = LQOperation.Add;
            }
            else if (values.Length == 1)
            {
                if (!ParseOperation(values[0]))
                    QuestionCode = LQDefine.LQMessage(LQDefine.LQCode.C0004);

            }
            else if (values.Length == 3)
            {
                if (!ParseOperation(values[0]))
                    QuestionCode = LQDefine.LQMessage(LQDefine.LQCode.C0004);
                else
                {
                    QuestionCode = values[1];
                    QuestionSystemCode = values[2];
                }
            }
            else
            {
                QuestionCode = LQDefine.LQMessage(LQDefine.LQCode.C0004);
            }
        }

        private bool ParseOperation(string value)
        {
            var op = GetOprationKey(value);
            if (op == null)
            {
                Operation = null;
                return false;
            }
            else
            {
                Operation = op;
                return true;
            }
        }
        public static Dictionary<LQOperation, string> LQOperationDefine = new Dictionary<LQOperation, string>()
        {
            { LQOperation.Add, LQDefine.Add },
            { LQOperation.Update, LQDefine.Modify },
            { LQOperation.Ignor, LQDefine.Ignor },
        };

        public static LQOperation? GetOprationKey(string operationValue)
        {
            return LQOperationDefine.Where(x => x.Value == operationValue.Trim()).FirstOrDefault().Key;
        }
    }
    public enum LQOperation
    {
        Add = 0,
        Update,
        Ignor,
    }

    public class LQQuestionError
    {
        public Microsoft.Office.Interop.Word.Bookmark Bookmark { get; set; }
        public string BookmarkName
        {
            get
            {
                return Bookmark.Name;
            }
        }
    }
}
