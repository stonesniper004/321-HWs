
namespace Spreadsheet_Ethan_Miller
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using SpreadsheetEngine;

    /// <summary>
    /// Used as default Form class.
    /// </summary>
    public partial class Form1 : Form
    {
        private SpreadsheetEngine.Spreadsheet masterSheet = new SpreadsheetEngine.Spreadsheet(50, 26);

        private Stack<ICommand> myUndos = new Stack<ICommand>();
        private Stack<ICommand> myRedos = new Stack<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            dataGrid = new DataGridView();
            //File.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            InitializeComponent();
        }

        /// <summary>
        /// Sets up DataGridView with req. columns.
        /// </summary>
        /// <returns>Int.</returns>
        public int SetupColumns()
        {
            char letter;
            for (int i = 65; i < 91; i++)
            {
                // converts ascii to letter
                letter = (char)i;
                dataGrid.Columns.Add(letter.ToString(), letter.ToString());
            }

            return 1;
        }

        /// <summary>
        /// Sets up rows for req. amount.
        /// </summary>
        /// <returns>Int.</returns>
        public int SetupRows()
        {
            for (int i = 1; i < 51; i++)
            {
                // adds a new row until all 50 are set
                dataGrid.Rows.Add();
                dataGrid.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            return 1;
        }

        /// <summary>
        /// Event to track if a cell is entered.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">DataGridViewCellCancelEventArgs.</param>
        private void DataGrid_CellEntered(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = masterSheet.GetCell(e.RowIndex, e.ColumnIndex).Text;
        }

        private void DataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            AddUndo("", masterSheet.GetCell(e.RowIndex, e.ColumnIndex).Text, masterSheet.GetCell(e.RowIndex, e.ColumnIndex), dataGrid[e.ColumnIndex, e.RowIndex], "Cell Text", masterSheet.GetCell(e.RowIndex, e.ColumnIndex).Value, "");
            dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = masterSheet.GetCell(e.RowIndex, e.ColumnIndex).Text;
        }

        /// <summary>
        /// Event to track if Cell value is changed.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">PropertyChangedEventArgs.</param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // converts the object to an InitilizeCell
            InitializeCell sendIt = (InitializeCell)sender;
            if ((InitializeCell)sendIt != null && e.PropertyName == "CellValue")
            {
                // Sets the mastersheets text and value to the senders text
                masterSheet.Sheet[sendIt.RowIndex, sendIt.ColumnIndex].Text = sendIt.Text;
                dataGrid.Rows[sendIt.RowIndex].Cells[sendIt.ColumnIndex].Value = sendIt.Value;
            }else if ((InitializeCell)sendIt != null && e.PropertyName == "Color")
            {
                dataGrid.Rows[sendIt.RowIndex].Cells[sendIt.ColumnIndex].Style.BackColor = ToColor(sendIt.BgColor);
            }
            /////////////////////////could be here
        }

        /// <summary>
        /// Event when From1 is loaded.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">EventArgs.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Substribes to all these events
            masterSheet.CellPropertyChanged += OnCellPropertyChanged;
            dataGrid.CellBeginEdit += DataGrid_CellBeginEdit;
            dataGrid.CellEndEdit += DataGrid_CellEndEdit;
            SetupColumns();
            SetupRows();
            Edit_Drop_Down.DropDownItems[0].Enabled = false;
            Edit_Drop_Down.DropDownItems[1].Enabled = false;
        }

        /// <summary>
        /// Event which detects user has finished editing a cell.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">DataGridViewCellEventArgs.</param>
        private void DataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                // Checks to see if the Cells text entered is different
                // if it is, its going to update the new text entered
                CellTextChecker(dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                // Changes text and value to updated text/value
                masterSheet.Sheet[e.RowIndex, e.ColumnIndex].Text = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = masterSheet.Sheet[e.RowIndex, e.ColumnIndex].Value;
            }
            else
            {
                masterSheet.Sheet[e.RowIndex, e.ColumnIndex].Text = string.Empty;
                // Checks to see if the given Cell removed its Cell contexts
                CellTextChecker(dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }

            // referenced Cell gets updated
            if (masterSheet.Sheet[e.RowIndex, e.ColumnIndex].Text != string.Empty)
            {
                string cellName = ((char)(e.ColumnIndex + 65)).ToString() + (e.RowIndex + 1).ToString();
                Update(cellName);
            }
        }

        /// <summary>
        /// Updates the cells that have a referene to the CellName
        /// </summary>
        /// <param name="cellName">String.</param>
        private void Update(string cellName)
        {
            // Goes through the spreedsheet
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    // if the cells text contains the cellName, update the value
                    if (masterSheet.Sheet[j, i].Text.Contains(cellName))
                    {
                        ((Cell)masterSheet.Sheet[j, i]).SetVal(Evaluate(masterSheet.Sheet[j, i].Text));
                    }
                }
            }
        }

        /// <summary>
        /// Evaluates the new value for a cell.
        /// </summary>
        /// <param name="content">String.</param>
        /// <returns>string.</returns>
        private string Evaluate(string content)
        {
            if (content[0] == '=')
            {
                // making expression tree starts here
                if (content.Length < 4)
                {
                    // just a reference to another cell.
                    int col = (int)content[1] - 65;
                    int row = Convert.ToInt32(content.Substring(2)) - 1;
                    return this.masterSheet.Sheet[row, col].Value;
                }

                // Not a reference but an expression
                string[] vars;
                string expression = string.Empty;

                // Goes throguh each char to seperate the expression
                foreach (char a in content)
                {
                    if (CheckIfOp(a) == true)
                    {
                        expression += " " + a + " ";
                    }
                    else if (a == ')')
                    {
                        expression += " " + a;
                    }
                    else if (a == '=')
                    {
                        continue;
                    }
                    else
                    {
                        expression += a;
                    }
                }

                // rebuilds the expression without extra white spaces
                double num = 0;
                vars = expression.Split();
                string builder = string.Empty;
                foreach (string value in vars)
                {
                    // each value gets added to builder, the last transformation of the string
                    if (value != "")
                    {
                        if (value.Length > 1 && ((int)value[0] >= 65 && (int)value[0] <= 90))
                        {
                            int col = value[0] - 65;
                            int row = Convert.ToInt32(value.Substring(1)) - 1;
                            builder += masterSheet.Sheet[row, col].Value + " ";
                        }
                        else if (double.TryParse(value, out num))
                        {
                            builder += num + " ";
                        }
                        else if (CheckIfOp(value[0]) == true)
                        {
                            builder += value + " ";
                        }
                        else
                        {
                            builder += value + " ";
                        }
                    }
                }

                // removes the trailing white space
                builder = builder.TrimEnd(' ');

                // creates the expression tree and sets to new value.
                ExpressionTree test = new ExpressionTree(builder);
                return ("" + test.Evaluate());
            }
            else
            {
                return content;
            }
        }

        /// <summary>
        /// Checks if the char is an operator.
        /// </summary>
        /// <param name="op">char.</param>
        /// <returns>bool.</returns>
        private bool CheckIfOp(char op)
        {
            switch (op)
            {
                case '*':
                    return true;
                case '^':
                    return true;
                case '+':
                    return true;
                case '-':
                    return true;
                case '/':
                    return true;
                case '(':
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Event when Demo button is clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">EventArgs.</param>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            this.masterSheet.DoDemo();
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Undo_Button_Click(object sender, EventArgs e)
        {
            if (myUndos.Count > 0)
            {
                UnDo();
            }

            if (myUndos.Count > 0)
            {
                Edit_Drop_Down.DropDownItems[0].Enabled = true;
                Edit_Drop_Down.DropDownItems[0].Text = "Undo " + myUndos.Peek().Description();
            }
            else
            {
                Edit_Drop_Down.DropDownItems[0].Enabled = false;
                Edit_Drop_Down.DropDownItems[0].Text = "Undo";
            }
        }

        private void Redo_Button_Click(object sender, EventArgs e)
        {
            if (myRedos.Count > 0)
            {
                Redo();
            }

            if (myRedos.Count > 0)
            {
                Edit_Drop_Down.DropDownItems[1].Enabled = true;
                Edit_Drop_Down.DropDownItems[1].Text = "Redo " + myRedos.Peek().Description();
            }
            else
            {
                Edit_Drop_Down.DropDownItems[1].Enabled = false;
                Edit_Drop_Down.DropDownItems[1].Text = "Redo";
            }
        }

        // Grabed this function the internet to convert a color to a uint
        private static uint ToUint(System.Drawing.Color c)
        {
            return (uint)(((c.A << 24) | (c.R << 16) | (c.G << 8) | c.B) & 0xffffffffL);
        }

        // Grabed this function from the internet to convert a uint to a color
        private static Color ToColor(uint value)
        {
            return Color.FromArgb((byte)((value >> 24) & 0xFF),
                       (byte)((value >> 16) & 0xFF),
                       (byte)((value >> 8) & 0xFF),
                       (byte)(value & 0xFF));
        }

        private void Change_Back_ground_Color_Button_Click(object sender, EventArgs e)
        {
            if (color_Dialog_User.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {

                Int32 selectedCellCount = dataGrid.GetCellCount(DataGridViewElementStates.Selected);

                if (selectedCellCount > 0)
                {
                    AddUndo("Cell Color Selection", dataGrid.SelectedCells, color_Dialog_User.Color);
                    Edit_Drop_Down.DropDownItems[0].Enabled = true;
                    Edit_Drop_Down.DropDownItems[0].Text = "Undo Cell Color Selection";
                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        dataGrid.SelectedCells[i].Style.BackColor = color_Dialog_User.Color;
                        masterSheet.Sheet[dataGrid.SelectedCells[i].RowIndex, dataGrid.SelectedCells[i].ColumnIndex].BgColor = ToUint(color_Dialog_User.Color);
                    }
                }
            }
        }

        public void AddUndo(string nText, string oText, Cell cell, System.Windows.Forms.DataGridViewCell dataCell, string input, string oldVal, string newVal)
        {
            myUndos.Push(new CellText(nText, oText, cell, dataCell, input, oldVal,newVal));
        }

        public void AddUndo(string input, DataGridViewSelectedCellCollection cells, System.Drawing.Color color)
        {
            CellColor cellColor = new CellColor(cells, color, input);
            myUndos.Push(new CellColor(cells, color, input));
        } 

        public void AddUndo(ICommand command)
        {
            myUndos.Push(command);
        }

        public void Redo()
        {
            ICommand temp = myRedos.Pop();
            CellText a = temp as CellText;
            myUndos.Push(temp);
            Edit_Drop_Down.DropDownItems[0].Enabled = true;
            Edit_Drop_Down.DropDownItems[0].Text = "Undo " + temp.Description();
            temp.Execute();

            if (a != null)
            {
                char letter = ((char)(((CellText)temp).DataCell.ColumnIndex + 65));
                string cellName = letter.ToString() + (((CellText)temp).DataCell.RowIndex + 1).ToString();

                Update(cellName);
            }
        }

        // Performs an undo
        public void UnDo()
        {
            // pops top most undo item form stack.
            ICommand temp = myUndos.Pop();
            CellText a = temp as CellText;

            // checks to see if ts a CellText change
            if (a != null && a.NewValue == string.Empty)
            {
                // adds the new CellText to temp
                a = new CellText(a.NewText, a.OldText, a.Cell, a.DataCell, a.Description(), a.OldValue, a.DataCell.Value.ToString());
                temp = a;
            }

            // Pushes temp to the redoStack and then executes the undo
            myRedos.Push(temp);
            Edit_Drop_Down.DropDownItems[1].Enabled = true;
            Edit_Drop_Down.DropDownItems[1].Text = "Redo " + temp.Description();
            temp.UnExecute();

            if (a != null)
            {
                char letter = ((char)(((CellText)temp).DataCell.ColumnIndex + 65));
                string cellName = letter.ToString() + (((CellText)temp).DataCell.RowIndex + 1).ToString();

                Update(cellName);
            }
        }

        // Checsk to see if the cell text was changed.
        public void CellTextChecker(string newText)
        {
            Edit_Drop_Down.DropDownItems[0].Enabled = true;
            CellText temp = (CellText)myUndos.Pop();
            if (temp.OldText != newText)
            {
                // Adds the new text to the undo stack.
                AddUndo(newText, temp.OldText, temp.Cell, temp.DataCell, temp.Description(),temp.OldValue, "");
                Edit_Drop_Down.DropDownItems[0].Text = "Undo " + myUndos.Peek().Description();
            }
        }

        // Checks the value of a cell.  
        public void CellValueChecker(string newValue)
        {
            Edit_Drop_Down.DropDownItems[0].Enabled = true;
            CellText temp = (CellText)myUndos.Pop();
            // If the old value is the same as new value, dont do anything.
            if (temp.OldValue != newValue)
            {
                // Adds the new value to the undo stack.
                AddUndo(temp.NewValue, temp.OldText, temp.Cell, temp.DataCell, temp.Description(), temp.OldValue, newValue);
                Edit_Drop_Down.DropDownItems[0].Text = "Undo " + myUndos.Peek().Description();
            }
        }

        /// <summary>
        /// Saves the current datagrid and sheet contents
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">EventArgs.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // creates an savefiledialog and sets the filter to .xml files only
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "xml|*.xml";
            save.ShowDialog();

            // if the filename is not empty, then we save the xml file.
            if (save.FileName != string.Empty)
            {
                System.IO.Stream file = save.OpenFile();
                masterSheet.Save(file);
            }

        }

        /// <summary>
        /// Loads an xml file.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">EventArgs.</param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // creates a openfileDialog and sets the filter to xml files only
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "xml|*.xml";
            open.ShowDialog();

            // if the filename contains xml, clear the contents of the datagrid and sheet and load
            // the files contents.
            if (open.FileName.Contains("xml"))
            {
                Clear();
                System.IO.Stream stream = open.OpenFile();
                masterSheet.Load(stream);
            }
        }

        /// <summary>
        /// Clears the datagrid and sheet of all contents
        /// </summary>
        private void Clear()
        {
            // default colors for both the data grid and the cell
            uint defaultbgcolor = 0xFFFFFFFF;
            Color defaultColor = ToColor(defaultbgcolor);
            Cell temp;
            // traverses through the datagrid and sheet to remove all contents
            for (int col = 0; col < 26; col++)
            {
                for (int row = 0; row < 50; row++)
                {
                    // reinitialize each of the values to default
                    dataGrid[col, row].Value = string.Empty;
                    dataGrid[col, row].Style.BackColor = defaultColor;
                    temp = this.masterSheet.Sheet[row, col];
                    temp.BgColor = defaultbgcolor;
                    temp.Text = string.Empty;
                    temp.SetVal(string.Empty);
                }
            }
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
