using System.Collections.Generic;


namespace SpreadsheetEngine
{
    public class CellColor : ICommand
    {
        string description;
        private System.Windows.Forms.DataGridViewSelectedCellCollection cells;
        private List<System.Drawing.Color> oldColors;
        private System.Drawing.Color color;

        public CellColor(System.Windows.Forms.DataGridViewSelectedCellCollection cells, System.Drawing.Color color, string des)
        {
            this.description = des;
            this.cells = cells;
            this.oldColors = new List<System.Drawing.Color>();
            this.color = color;
            foreach (System.Windows.Forms.DataGridViewCell cell in this.cells)
            {
                oldColors.Add(cell.Style.BackColor);
            }
        }

        public void Execute()
        {
            foreach (System.Windows.Forms.DataGridViewCell cell in this.cells)
            {
                oldColors.Add(cell.Style.BackColor);
                cell.Style.BackColor = this.color;
            }
        }

        public void UnExecute()
        {
            int i = 0;
            foreach (System.Windows.Forms.DataGridViewCell cell in this.cells)
            {
                cell.Style.BackColor = oldColors[i];
                i++;
            }
        }

        public string Description()
        {
            return this.description;
        }
    }
}
