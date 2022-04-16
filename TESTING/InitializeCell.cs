// <copyright file="Cell.cs" company="Alex Puga 011425121">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Helper Class to initialize a Cell.
    /// </summary>
    public class InitializeCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeCell"/> class.
        /// </summary>
        /// <param name="row">Int Row.</param>
        /// <param name="col">Int Column.</param>
        public InitializeCell(int row, int col)
            : base(row, col) { }
    }
}
