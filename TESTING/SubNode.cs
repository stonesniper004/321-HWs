// <copyright file="SubNode.cs" company="Alex Puga">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Inherited from OperatorNode: Used for Subtracting.
    /// </summary>
    public class SubNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubNode"/> class.
        /// </summary>
        public SubNode()
        {
            this.Operator = '-';
            this.Precedence = 2;
            this.Associativity = 0;
        }

        /// <inheritdoc/>
        public override double DoOperator(Dictionary<string, double> variables)
        {
            return this.Evaluate(this.Left, variables) - this.Evaluate(this.Right, variables);
        }
    }
}
