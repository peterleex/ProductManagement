namespace ProductManagement.HttpApi.Client.WinFormTestApp.Controls
{
public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        private CheckBox _checkBox;

        public DataGridViewCheckBoxHeaderCell()
        {
            _checkBox = new CheckBox
            {
                Size = new Size(15, 15),
                Text = string.Empty,
                //Location = new Point(5, 5),
            };
            _checkBox.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (DataGridView == null) return;

            DataGridView.CurrentCell = null;

            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                row.Cells[ColumnIndex].Value = _checkBox.Checked;
                row.Cells[ColumnIndex].DataGridView!.InvalidateCell(row.Cells[ColumnIndex]); // Force cell to redraw
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            //_checkBox.Location = new Point(cellBounds.Location.X + (cellBounds.Width - _checkBox.Width) / 2, cellBounds.Location.Y + (cellBounds.Height - _checkBox.Height) / 2);
            //_checkBox.Paint += (s, e) => e.Graphics.DrawImage(new Bitmap(_checkBox.ClientRectangle.Width, _checkBox.ClientRectangle.Height), _checkBox.ClientRectangle);
            //_checkBox.Invalidate();

            // Calculate the location of the CheckBox
            Point checkBoxLocation = new Point(
                cellBounds.Location.X + (cellBounds.Width - _checkBox.Width) / 2,
                cellBounds.Location.Y + (cellBounds.Height - _checkBox.Height) / 2
            );

            // Set the CheckBox location
            _checkBox.Location = checkBoxLocation;

            // Ensure the CheckBox is added to the DataGridView controls
            if (!DataGridView!.Controls.Contains(_checkBox))
            {
                DataGridView.Controls.Add(_checkBox);
            }

            // Draw the CheckBox manually to ensure borders are visible
            CheckBoxRenderer.DrawCheckBox(
                graphics,
                checkBoxLocation,
                _checkBox.Checked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal
            );

            // Draw the CheckBox border manually to ensure the top border is visible
            //ControlPaint.DrawBorder(graphics, new Rectangle(checkBoxLocation, _checkBox.Size), Color.Black, ButtonBorderStyle.Solid);
            ControlPaint.DrawBorder(graphics, new Rectangle(checkBoxLocation.X - 1, checkBoxLocation.Y - 1, _checkBox.Width + 1, _checkBox.Height + 1), Color.Black, ButtonBorderStyle.Solid);
        }

        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();
            if (DataGridView != null)
            {
                DataGridView.Controls.Add(_checkBox);
            }
        }
    }
}
