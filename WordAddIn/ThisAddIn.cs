using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace WordAddIn
{
    public partial class ThisAddIn
    {
        private NavigationPaneControl navigationPaneControl;
        private Microsoft.Office.Tools.CustomTaskPane customTaskPane;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //CloseNavigationPane();
            InitDocumentPageSize();
            InitPane();
            InitGrid();
            //CheckFor10FieldsTable();
            HookEvent();
        }

        private void HookEvent()
        {
            Application.DocumentChange += new Word.ApplicationEvents4_DocumentChangeEventHandler(DocumentChanged);
            Application.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(SelectionChanged);
            Application.WindowBeforeRightClick += new Word.ApplicationEvents4_WindowBeforeRightClickEventHandler(BeforeRightClick);
            Application.WindowBeforeDoubleClick += new Word.ApplicationEvents4_WindowBeforeDoubleClickEventHandler(BeforeDoubleClick);
        }

        private void InitDocumentPageSize()
        {
            Word.Document doc = Application.ActiveDocument;
            doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA3;
            doc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            doc.PageSetup.TopMargin = Application.CentimetersToPoints(LQDefine.DocumentMargin);
            doc.PageSetup.BottomMargin = Application.CentimetersToPoints(LQDefine.DocumentMargin);
            doc.PageSetup.LeftMargin = Application.CentimetersToPoints(LQDefine.DocumentMargin);
            doc.PageSetup.RightMargin = Application.CentimetersToPoints(LQDefine.DocumentMargin);
        }

        private void InitPane()
        {
            navigationPaneControl = new NavigationPaneControl();
            customTaskPane = CustomTaskPanes.Add(navigationPaneControl, LQDefine.LQMessage(LQDefine.LQCode.C0002));
            customTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionLeft;
            customTaskPane.Width = (int)(Application.Width * LQDefine.TaskPaneWidthRadio);
            customTaskPane.Visible = true;

            navigationPaneControl.Info = string.Empty;
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

        internal void Add10FieldsRow(int rowCount)
        {
            Word.Document doc = Application.ActiveDocument;
            Word.Range range = Application.Selection.Range;
            Word.Table table = null;

            // Check if a table with 10 columns already exists
            var tables = doc.Tables.Cast<Word.Table>().Where(t => t.Columns.Count == 10).ToList();
            if (tables.Count == 1)
            {
                table = tables.First();
                range = table.Range;
                range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
            }

            if (table == null)
                ++rowCount;

            for (int i = 0; i < rowCount; i++)
            {
                if (table == null)
                {
                    table = doc.Tables.Add(range, 1, 10);
                    table.Borders.Enable = 1;

                    SetColumnWidth(table);
                    SetTableHeader(table);
                }
                else
                {
                    table.Rows.Add();
                }

                range = table.Range;
                range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
            }
        }

        private void DocumentChanged()
        {
            //ReadField01();
        }

        private void SelectionChanged(Word.Selection Sel)
        {
            //ReadField01();
        }

        private void BeforeRightClick(Word.Selection Sel, ref bool Cancel)
        {
            //ReadField01();
        }

        private void BeforeDoubleClick(Word.Selection Sel, ref bool Cancel)
        {
            //ReadField01();
        }

        public void ReadField01()
        {
            navigationPaneControl.QuestionList.Clear();

            Word.Document doc = Application.ActiveDocument;
            
            var tables = doc.Tables.Cast<Word.Table>().Where(t => t.Columns.Count == 10).ToList();

            if (tables.Count != 1)
            {
                navigationPaneControl.Info = LQDefine.LQMessage(LQDefine.LQCode.C0002);
                return;
            }

            Word.Table table = tables.First();
            for (int i = 2; i <= table.Rows.Count; i++) // Start from 2 to skip the first row
            {
                Word.Cell cell = table.Cell(i, 1);
                if (cell != null && !string.IsNullOrEmpty(cell.Range.Text.Trim()))
                {
                    var fieldValue = GetPlainTextFromCell(cell);

                    navigationPaneControl.QuestionList.Add(new LQQuestionOperation(fieldValue, i));
                }
            }

            navigationPaneControl.RefreshQuestionListGrid();
        }

        private string GetPlainTextFromCell(Word.Cell cell)
        {
            string text = cell.Range.Text;
            text = text.Replace("\r", "").Replace("\a", "").Trim();
            return text;
        }

        private void InitGrid()
        {
            //navigationPaneControl.QuestionList.AddRange(new List<LQQuestionOperation>
            //{
            //    new LQQuestionOperation { QuestionCode = "Q001", QuestionSystemCode = "Q001", Operation = LQOperation.Add },
            //    new LQQuestionOperation { QuestionCode = "Q002", QuestionSystemCode = "Q002", Operation = LQOperation.Update },
            //    new LQQuestionOperation { QuestionCode = "Q003", QuestionSystemCode = "Q003", Operation = LQOperation.Ignor },
            //});

            //navigationPaneControl.RefreshQuestionListGrid();
        }

        private void SetColumnWidth(Table table)
        {
            table.Columns[1].Width = Application.CentimetersToPoints(1.2f);
            table.Columns[2].Width = Application.CentimetersToPoints(0.9f);
            table.Columns[3].Width = Application.CentimetersToPoints(1.5f);
            table.Columns[4].Width = Application.CentimetersToPoints(0.7f);
            table.Columns[5].Width = Application.CentimetersToPoints(4.0f);
            table.Columns[6].Width = Application.CentimetersToPoints(14.2f);
            table.Columns[7].Width = Application.CentimetersToPoints(3.0f);
            table.Columns[8].Width = Application.CentimetersToPoints(14.2f);
            table.Columns[9].Width = Application.CentimetersToPoints(0.7f);
            table.Columns[10].Width = Application.CentimetersToPoints(0.7f);
        }

        private static void SetTableHeader(Table table)
        {
            table.Cell(1, 1).Range.Text = LQDefine.Field01Name;
            table.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;


            table.Cell(1, 2).Range.Text = LQDefine.Field02Name;
            table.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 3).Range.Text = LQDefine.Field03Name;
            table.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 4).Range.Text = LQDefine.Field04Name;
            table.Cell(1, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 5).Range.Text = LQDefine.Field05Name;
            table.Cell(1, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 6).Range.Text = LQDefine.Field06Name;
            table.Cell(1, 6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 7).Range.Text = LQDefine.Field07Name;
            table.Cell(1, 7).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 8).Range.Text = LQDefine.Field08Name;
            table.Cell(1, 8).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 9).Range.Text = LQDefine.Field09Name;
            table.Cell(1, 9).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            table.Cell(1, 10).Range.Text = LQDefine.Field10Name;
            table.Cell(1, 10).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }

        #endregion
    }
}
