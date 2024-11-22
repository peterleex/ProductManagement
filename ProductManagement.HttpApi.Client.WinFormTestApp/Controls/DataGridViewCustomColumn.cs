using System;
using System.Drawing;
using System.Windows.Forms;
namespace ProductManagement.HttpApi.Client.WinFormTestApp.Controls
{
    public class DataGridViewCustomColumn : DataGridViewColumn
    {
        public DataGridViewCustomColumn() : base(new DataGridViewCustomCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewCustomCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewCustomCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewCustomCell : DataGridViewTextBoxCell
    {
        public enum CellType
        {
            Text,
            Image,
            Progress
        }

        public CellType Type { get; set; }
        public Image? Image { get; set; }
        public Color ProgressBarColor { get; set; } = Color.Green;

        public DataGridViewCustomCell()
        {
            this.Type = CellType.Text;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            switch (Type)
            {
                case CellType.Image:
                    if (Image != null)
                    {
                        graphics.DrawImage(Image, cellBounds);
                    }
                    break;
                case CellType.Progress:
                    if (value is int progressValue)
                    {
                        //float percentage = (float)progressValue / 100.0f;
                        //Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                        //Brush foreColorBrush = new SolidBrush(ProgressBarColor);

                        //graphics.FillRectangle(backColorBrush, cellBounds);

                        //Rectangle progressBarRect = new Rectangle(cellBounds.X + 2, cellBounds.Y + 2, (int)((cellBounds.Width - 4) * percentage), cellBounds.Height - 4);
                        //graphics.FillRectangle(foreColorBrush, progressBarRect);

                        //string text = progressValue.ToString() + "%";
                        //TextRenderer.DrawText(graphics, text, cellStyle.Font, cellBounds, cellStyle.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        if (Convert.ToInt16(value) == 0 || value == null)
                        {
                            value = 0;
                        }

                        int progressVal = Convert.ToInt32(value);

                        float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
                        Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                        Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

                        // Draws the cell grid
                        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                        float posX = cellBounds.X;
                        float posY = cellBounds.Y;

                        float textWidth = TextRenderer.MeasureText(progressVal.ToString() + "%", cellStyle.Font).Width;
                        float textHeight = TextRenderer.MeasureText(progressVal.ToString() + "%", cellStyle.Font).Height;

                        //evaluating text position according to alignment
                        switch (cellStyle.Alignment)
                        {
                            case DataGridViewContentAlignment.BottomCenter:
                                posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                                posY = cellBounds.Y + cellBounds.Height - textHeight;
                                break;
                            case DataGridViewContentAlignment.BottomLeft:
                                posX = cellBounds.X;
                                posY = cellBounds.Y + cellBounds.Height - textHeight;
                                break;
                            case DataGridViewContentAlignment.BottomRight:
                                posX = cellBounds.X + cellBounds.Width - textWidth;
                                posY = cellBounds.Y + cellBounds.Height - textHeight;
                                break;
                            case DataGridViewContentAlignment.MiddleCenter:
                                posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                                posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                                break;
                            case DataGridViewContentAlignment.MiddleLeft:
                                posX = cellBounds.X;
                                posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                                break;
                            case DataGridViewContentAlignment.MiddleRight:
                                posX = cellBounds.X + cellBounds.Width - textWidth;
                                posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                                break;
                            case DataGridViewContentAlignment.TopCenter:
                                posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                                posY = cellBounds.Y;
                                break;
                            case DataGridViewContentAlignment.TopLeft:
                                posX = cellBounds.X;
                                posY = cellBounds.Y;
                                break;

                            case DataGridViewContentAlignment.TopRight:
                                posX = cellBounds.X + cellBounds.Width - textWidth;
                                posY = cellBounds.Y;
                                break;

                        }

                        if (percentage >= 0.0)
                        {

                            // Draw the progress 
                            graphics.FillRectangle(new SolidBrush(ProgressBarColor), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height / 1 - 5);
                            //Draw text
                            graphics.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, posX, posY);
                        }
                        else
                        {
                            //if percentage is negative, we don't want to draw progress bar
                            //wa want only text
                            if (this.DataGridView.CurrentRow.Index == rowIndex)
                            {
                                graphics.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), posX, posX);
                            }
                            else
                            {
                                graphics.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, posX, posY);
                            }
                        }
                    }
                    break;
                case CellType.Text:
                default:
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                    break;
            }
        }
    }
}
