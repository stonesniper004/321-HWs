

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Xml;

    //using System.Xml;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Event for Cells properties being changed.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

        public Cell[,] Sheet;
        private int columnCount;
        private int rowCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">Int Rows.</param>
        /// <param name="columns">Int Columns.</param>
        public Spreadsheet(int rows, int columns)
        {
            // setting the size of rows and columns for the spreadsheet
            this.columnCount = columns;
            this.rowCount = rows;
            this.Sheet = new Cell[rows, columns];

            // initializes the spreadsheet with given dimensions.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.Sheet[i, j] = new InitializeCell(i, j);
                    this.Sheet[i, j].PropertyChanged += this.Spreadsheet_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// Returns a Cell.
        /// </summary>
        /// <param name="row">Int Row.</param>
        /// <param name="col">Int Col.</param>
        /// <returns>Int.</returns>
        public Cell GetCell(int row, int col)
        {
            if ((Cell)this.Sheet[row, col] == null)
            {
                return null;
            }

            return (Cell)this.Sheet[row, col];
        }

        /// <summary>
        /// Performs a demo when button is clicked.
        /// </summary>
        public void DoDemo()
        {
            int randRow = 0;
            int randCol = 0;

            // quick demo, goes through randomly and fills each cell
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                randRow = random.Next(0, 49);
                randCol = random.Next(0, 25);
                this.Sheet[randRow, randCol].Text = "Demo Me";
            }

            // this is used to test the referencing
            for (int i = 0; i < 50; i++)
            {
                this.Sheet[i, 1].Text = "This is cell B" + (i + 1);
            }

            // these cells will be used for referencing An's value
            for (int i = 0; i < 50; i++)
            {
                this.Sheet[i, 0].Text = "=B" + (i + 1);
            }
        }

        /// <summary>
        /// Checks if the given char is an operator.
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

        public void Save(Stream stream)
        {
            XmlWriter writter = XmlWriter.Create(stream);
            writter.WriteStartDocument();
            writter.WriteStartElement("Spreadsheet");
            string cellName = string.Empty;

            for (int col = 0; col < 26; col++)
            {
                for (int row = 0; row < 50; row++)
                {
                    Cell tmp = this.GetCell(row, col);
                    if (tmp.Text != string.Empty || tmp.Value != string.Empty || tmp.BgColor != 0xFFFFFFFF)
                    {
                        // Grab the cell's row and column to assign it to a name.
                        cellName = ((char)(col + 65)).ToString() + (row + 1).ToString();
                        // Creates a element signifiying the cell
                        writter.WriteStartElement("Cell");
                        // Creates a element signifying a cell's name and assigning it -- might not need this 
                        /*writter.WriteStartElement("cell_name");
                        writter.WriteString(cellName);
                        writter.WriteEndElement();*/

                        // Creates a element signiyging a cell's text
                        writter.WriteStartElement("cell_text");
                        writter.WriteString(tmp.Text);
                        writter.WriteEndElement();

                        // Creates a element signiyging a cell's row index
                        writter.WriteStartElement("cell_row");
                        writter.WriteString(tmp.RowIndex.ToString());
                        writter.WriteEndElement();

                        // Creates a element signiyging a cell's col index
                        writter.WriteStartElement("cell_col");
                        writter.WriteString(tmp.ColumnIndex.ToString());
                        writter.WriteEndElement();

                        // Creates a element signiyging a cell's value
                        writter.WriteStartElement("cell_value");
                        writter.WriteString(tmp.Value);
                        writter.WriteEndElement();

                        // Creates a element signiyging a cell's BgColor
                        writter.WriteStartElement("cell_bgcolor");
                        writter.WriteString(tmp.BgColor.ToString());
                        writter.WriteEndElement();
                        writter.WriteEndElement();
                    }
                }
            }

            writter.WriteEndElement();
            writter.WriteEndDocument();
            writter.Close();
        }

        public void Load(Stream stream)
        {
            XmlReader reader = XmlReader.Create(stream);
            reader.ReadStartElement("Spreadsheet");
            string text = string.Empty, value = string.Empty;
            Cell cell;
            int col = 0, row = 0;
            uint bgcolor = 0;
            while (reader.Name == "Cell")
            {

                reader.ReadStartElement("Cell");
                // Gets the cells text
                reader.ReadStartElement("cell_text");
                text = reader.ReadContentAsString();
                reader.ReadEndElement();

                // Gets the cells row.
                reader.ReadStartElement("cell_row");
                row = reader.ReadContentAsInt();
                reader.ReadEndElement();

                // Gets the cells column
                reader.ReadStartElement("cell_col");
                col = reader.ReadContentAsInt();
                reader.ReadEndElement();

                // Gets the cells value
                reader.ReadStartElement("cell_value");
                value = reader.ReadContentAsString();
                reader.ReadEndElement();

                // Gets the cells bgcolor
                reader.ReadStartElement("cell_bgcolor");
                bgcolor = Convert.ToUInt32(reader.ReadContentAsString());
                reader.ReadEndElement();

                cell = this.GetCell(row, col);

                LoadCell(cell, row, col, bgcolor, value, text);
                reader.ReadEndElement();
            }

            // Exits the Spreadsheet element and closes the xml reader
            reader.ReadEndElement();
            reader.Close();
        }

        private void LoadCell(Cell cell, int row, int col, uint bgcolor, string value, string text)
        {
            cell.BgColor = bgcolor;
            cell.Text = text;
            cell.Value = value;
            cell.RowIndex = row;
            cell.ColumnIndex = col;
        }

        /// <summary>
        /// When a property is changed event.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">PropertyChangedEventArgs.</param>
        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // checks if the event was triggered of a Celltext modification.
            if (e.PropertyName == "CellText")
            {
                if (((Cell)sender).Text == string.Empty)
                {

                    return;
                }

                // Checks if its an expression or a reference
                if (((Cell)sender).Text[0] == '=')
                {
                    // reference to other cell
                    string cellContent = ((Cell)sender).Text;
                    if (cellContent.Length < 4)
                    {
                        if (char.IsDigit(cellContent[1]))
                        {
                            ((Cell)sender).Value = cellContent.Substring(1, cellContent.Length - 1);
                            return;
                        }

                        int col = (int)((Cell)sender).Text[1] - 65;
                        int row = Convert.ToInt32(((Cell)sender).Text.Substring(2)) - 1;
                        ((Cell)sender).Value = this.Sheet[row, col].Value;
                        return;
                    }

                    // Not a refrence but an expression tree
                    string[] vars;
                    string expression = string.Empty;

                    // Goes through and adds spaces to the expression
                    foreach (char a in cellContent)
                    {
                        if (this.CheckIfOp(a) == true)
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

                    double num = 0;
                    vars = expression.Split();
                    string builder = string.Empty;
                    foreach (string value in vars)
                    {
                        // cleans up the expression from extra white space.
                        if (value != "")
                        {
                            if (value.Length > 1 && ((int)value[0] >= 65 && (int)value[0] <= 90))
                            {
                                int col = value[0] - 65;
                                int row = Convert.ToInt32(value.Substring(1)) - 1;
                                builder += this.Sheet[row,col].Value + " ";
                            }
                            else if (double.TryParse(value, out num))
                            {
                                builder += num + " ";
                            }
                            else if (this.CheckIfOp(value[0]) == true)
                            {
                                builder += value + " ";
                            }
                            else
                            {
                                builder += value + " ";
                            }
                        }
                    }

                    // builds the expression tree and sets the new value to evaluated expression
                    builder = builder.TrimEnd(' ');
                    ExpressionTree test = new ExpressionTree(builder);
                    ((Cell)sender).Value = "" + test.Evaluate();
                }
                else if (e.PropertyName == "Color")
                {
                    ((Cell)sender).BgColor = this.Sheet[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex].BgColor;
                }
                else
                {
                    ((Cell)sender).Value = ((Cell)sender).Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
