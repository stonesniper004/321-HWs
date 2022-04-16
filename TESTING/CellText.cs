using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SpreadsheetEngine
{
    public class CellText : ICommand
    {
        string description;
        string oldText;
        string newText;
        string oldValue;
        string newValue;
        Cell cell;
        System.Windows.Forms.DataGridViewCell dataCell;

        public CellText(string nText, string oText, Cell cell, System.Windows.Forms.DataGridViewCell dataCell, string des, string oldV, string newV)
        {
            this.oldValue = oldV;
            this.newValue = newV;
            this.description = des;
            this.oldText = oText;
            this.newText = nText;
            this.cell = cell;
            this.dataCell = dataCell;
        }

        public void Execute()
        {
            this.cell.Text = this.newText;
            this.cell.Value = this.newValue;
            this.dataCell.Value = this.newValue;
        }

        public string OldText
        {
            get { return this.oldText; }
        }

        public string NewText
        {
            get { return this.newText; }
        }

        public string OldValue
        {
            get { return this.oldValue; }
        }

        public string NewValue
        {
            get { return this.newValue; }
        }

        public System.Windows.Forms.DataGridViewCell DataCell
        {
            get { return this.dataCell; }
        }

        public Cell Cell
        {
            get { return this.cell; }
        }

        public void UnExecute()
        {
            this.cell.Text = this.oldText;
            this.cell.Value = this.oldValue;
            //this.newValue = this.oldValue;
            this.dataCell.Value = this.oldValue;
        }

        public string Description()
        {
            return this.description;
        }
    }
}
