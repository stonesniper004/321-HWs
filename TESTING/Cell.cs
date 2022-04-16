// <copyright file="Cell.cs" company="Alex Puga 011425121">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>



using System.ComponentModel;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Cell class stores information for each cell in DataGridView.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected int rowIndex;
        protected int coloumnIndex;
        protected string text;
        protected string value;
        protected uint BGColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="r">Int Row.</param>
        /// <param name="c">Int Column.</param>
        public Cell(int r, int c)
        {
            this.rowIndex = r;
            this.coloumnIndex = c;
            this.text = string.Empty;
            this.value = string.Empty;
            this.BGColor = 0xFFFFFFFF;
        }

        public uint BgColor
        {
            get { return this.BGColor; }

            set
            {
                if (this.BGColor == value)
                {
                    return;
                }

                this.BGColor = value;
                this.OnPropertyChanged("Color");
            }
        }

        /// <summary>
        /// Gets RowIndex of a Cell.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
            set { this.rowIndex = value; }
        }

        /// <summary>
        /// Gets ColumneIndex of a Cell.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.coloumnIndex; }
            set { this.coloumnIndex = value; }
        }

        /// <summary>
        /// Gets or sets for Value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            protected internal set
            {
                this.value = value;
                this.OnPropertyChanged("CellValue");
            }
        }

        public void SetVal(string s)
        {
            this.Value = s;
        }

        /// <summary>
        /// Gets or sets for Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }
                else
                {
                    this.text = value;
                    this.OnPropertyChanged("CellText");
                }
            }
        }

        /// <summary>
        /// Event for PropertyChanges.
        /// </summary>
        /// <param name="name">String.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
